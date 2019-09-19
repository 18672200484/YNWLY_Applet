using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using CMCS.Common.Utilities;
using CMCS.DumblyConcealer.Enums;
using CMCS.DumblyConcealer.Tasks.AutoMaker;
using CMCS.DumblyConcealer.Win.Core;
using CMCS.Common;
using CMCS.Common.DAO;
using CMCS.DumblyConcealer.Tasks.PackagingBatch;

namespace CMCS.DumblyConcealer.Win.DumblyTasks
{
    public partial class FrmPackingBatch : TaskForm
    {
        RTxtOutputer rTxtOutputer;

        TaskSimpleScheduler taskSimpleScheduler = new TaskSimpleScheduler();

        public FrmPackingBatch()
        {
            InitializeComponent();
        }

        private void FrmAutoMaker_NCGM_Load(object sender, EventArgs e)
        {
            this.Text = "封装归批机接口业务";

            this.rTxtOutputer = new RTxtOutputer(rtxtOutput);

            ExecuteAllTask();
        }

        /// <summary>
        /// 执行所有任务
        /// </summary>
        void ExecuteAllTask()
        {
            #region 封装归批机
            try
            {
                taskSimpleScheduler.StartNewTask("矩阵合样归批机-快速同步", () =>
                {
                    EquPackagingBatchDAO batchDAO = new EquPackagingBatchDAO(new DapperDber.Dbs.SqlServerDb.SqlServerDapperDber(CommonDAO.GetInstance().GetCommonAppletConfigString("矩阵合样归批机接口连接字符串")));
                    if (CommonDAO.GetInstance().TestPing(batchDAO.EquDber.Connection.DataSource))
                    {
                        batchDAO.SyncCmd(this.rTxtOutputer.Output);
                        batchDAO.SyncCoord(this.rTxtOutputer.Output);
                        batchDAO.SyncError(this.rTxtOutputer.Output);
                        batchDAO.SyncSignal(this.rTxtOutputer.Output);
                    }
                    else
                        this.rTxtOutputer.Output(string.Format("{0}网络不通", batchDAO.MachineCode), eOutputType.Warn);
                }, 5000, OutputError);
            }
            catch (Exception ex)
            {
                OutputError("矩阵合样归批机-快速同步", ex);
            }
            #endregion
        }

        /// <summary>
        /// 输出异常信息
        /// </summary>
        /// <param name="text"></param>
        /// <param name="ex"></param>
        void OutputError(string text, Exception ex)
        {
            this.rTxtOutputer.Output(text + Environment.NewLine + ex.Message, eOutputType.Error);

            Log4Neter.Error(text, ex);
        }

        /// <summary>
        /// 窗体关闭后
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmAutoMaker_NCGM_FormClosed(object sender, FormClosedEventArgs e)
        {
            // 注意：必须取消任务
            this.taskSimpleScheduler.Cancal();
        }
    }
}
