using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CMCS.DumblyConcealer.Win.Core;
using CMCS.Common.Utilities;
using CMCS.DumblyConcealer.Tasks.AssayDevice;
using CMCS.DumblyConcealer.Enums;
using CMCS.DumblyConcealer.Tasks.TemperatureTest;
using CMCS.Common.DAO;
using CMCS.Common.Entities.BaseInfo;

namespace CMCS.DumblyConcealer.Win.DumblyTasks
{
    public partial class FrmTemperator : TaskForm
    {

        RTxtOutputer rTxtOutputer;
        TaskSimpleScheduler taskSimpleScheduler = new TaskSimpleScheduler();
        List<ITemperGraber> Grabers = new List<ITemperGraber>();
        CommonDAO commonDAO = CommonDAO.GetInstance();
        public FrmTemperator()
        {
            InitializeComponent();
        }

        private void FrmAssayDevice_Load(object sender, EventArgs e)
        {
            this.Text = "煤场测温仪接口业务";

            this.rTxtOutputer = new RTxtOutputer(rtxtOutput);
            ExecuteAllTask();
        }

        /// <summary>
        /// 执行所有任务
        /// </summary>
        void ExecuteAllTask()
        {
            taskSimpleScheduler.StartNewTask("煤场测温仪", () =>
            {
                InitPerformer();
                foreach (ITemperGraber graber in Grabers)
                {
                    graber.ComMethod.SyncTemp(this.rTxtOutputer.Output, graber.Milliseconds);
                }
            }, 0, OutputError);
        }

        /// <summary>
        /// 初始化
        /// </summary>
        void InitPerformer()
        {
            try
            {
                CmcsCMEquipment TempEquipment = commonDAO.SelfDber.Entity<CmcsCMEquipment>("where EquipmentName='煤场测温仪'");
                if (TempEquipment == null)
                {
                    this.rTxtOutputer.Output("煤场测温仪设备未配置", eOutputType.Warn);
                    return;
                }
                foreach (CmcsCMEquipment item in commonDAO.SelfDber.Entities<CmcsCMEquipment>("where Parentid=:Parentid", new { Parentid = TempEquipment.Id }))
                {
                    string[] paramer = item.EquipmentCode.Split('|');
                    if (paramer.Length != 5)
                    {
                        this.rTxtOutputer.Output(string.Format("{0}参数配置错误", item.EquipmentName), eOutputType.Warn);
                        continue;
                    }
                    ITemperGraber entity = new ITemperGraber();
                    entity.FacilityNumber = item.EquipmentName;
                    entity.Com = paramer[0];
                    entity.Addr = Convert.ToInt32(paramer[1]);
                    entity.CRC8 = Convert.ToInt32(paramer[2]);
                    entity.Milliseconds = Convert.ToInt32(paramer[3]);
                    entity.DelDays = Convert.ToInt32(paramer[4]);
                    entity.ComMethod = new TemperatureDAO(entity.Com, entity.Addr, entity.CRC8, entity.DelDays, entity.FacilityNumber);
                    this.Grabers.Add(entity);
                }
            }
            catch (Exception ex)
            {
                OutputError("初始化异常", ex);
            }
        }

        /// <summary>
        /// 输出异常信息
        /// </summary>
        /// <param name="text"></param>
        /// <param name="ex"></param>
        void OutputError(string text, Exception ex)
        {
            this.rTxtOutputer.Output(text + Environment.NewLine + ex.Message, eOutputType.Error);
        }

        /// <summary>
        /// 窗体关闭后
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmAssayDevice_FormClosed(object sender, FormClosedEventArgs e)
        {
            foreach (ITemperGraber graber in Grabers)
            {
                graber.ComMethod.CloseCom();
            }
            // 注意：必须取消任务
            this.taskSimpleScheduler.Cancal();
        }
    }
}
