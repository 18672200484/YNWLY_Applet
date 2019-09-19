using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CMCS.DumblyConcealer.Win.Core;

using CMCS.DumblyConcealer.Tasks.BeltSampler;
using CMCS.Common.Utilities;
using CMCS.DumblyConcealer.Enums;
using CMCS.DumblyConcealer.Tasks.WeightBridger;

namespace CMCS.DumblyConcealer.Win.DumblyTasks
{
    public partial class FrmWeightBridger : TaskForm
    {
        RTxtOutputer rTxtOutputer;
        TaskSimpleScheduler taskSimpleScheduler = new TaskSimpleScheduler();

        public FrmWeightBridger()
        {
            InitializeComponent();
        }

        private void FrmWeightBridger_Load(object sender, EventArgs e)
        {
            this.Text = "轨道衡、入厂车号识别数据同步业务";

            this.rTxtOutputer = new RTxtOutputer(rtxtOutput);

            ExecuteAllTask();
        }

        /// <summary>
        /// 执行所有任务
        /// </summary>
        void ExecuteAllTask()
        {
            taskSimpleScheduler.StartNewTask("同步轨道衡过衡数据", () =>
            {
                EquWeightBridgerDAO weightBridger_DAO = EquWeightBridgerDAO.GetInstance();
                weightBridger_DAO.SyncTrainWeightInfo(this.rTxtOutputer.Output);

            }, 60000, OutputError);

            taskSimpleScheduler.StartNewTask("同步火车车号识别数据", () =>
            {
                EquWeightBridgerDAO weightBridger_DAO = EquWeightBridgerDAO.GetInstance();
                weightBridger_DAO.SyncTrainRecognitionInfo(this.rTxtOutputer.Output);

            }, 2000, OutputError);
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
        private void FrmWeightBridger_FormClosed(object sender, FormClosedEventArgs e)
        {
            // 注意：必须取消任务
            this.taskSimpleScheduler.Cancal();
        }
    }
}
