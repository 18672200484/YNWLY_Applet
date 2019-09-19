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
using CMCS.DumblyConcealer.Enums;
using CMCS.DumblyConcealer.Tasks.PneumaticTransfer;
using CMCS.Common;
using CMCS.DumblyConcealer.Tasks.PLC;

namespace CMCS.DumblyConcealer.Win.DumblyTasks
{
    public partial class FrmPLC : TaskForm
    {
        RTxtOutputer rTxtOutputer;

        TaskSimpleScheduler taskSimpleScheduler = new TaskSimpleScheduler();

        public FrmPLC()
        {
            InitializeComponent();
        }

        private void FrmPneumaticTransfer_Load(object sender, EventArgs e)
        {
            this.Text = "下位机信号点同步";

            this.rTxtOutputer = new RTxtOutputer(rtxtOutput);

            ExecuteAllTask();
        }

        /// <summary>
        /// 执行所有任务
        /// </summary>
        void ExecuteAllTask()
        {
            #region
            EquPLCDAO equPLCDAO = new EquPLCDAO();
            taskSimpleScheduler.StartNewTask("气动传输-快速同步", () =>
            {
                equPLCDAO.StartTask(this.rTxtOutputer.Output);
            }, 0, OutputError);
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

        private void FrmPneumaticTransfer_FormClosed(object sender, FormClosedEventArgs e)
        {
            // 注意：必须取消任务
            this.taskSimpleScheduler.Cancal();
        }

    }
}
