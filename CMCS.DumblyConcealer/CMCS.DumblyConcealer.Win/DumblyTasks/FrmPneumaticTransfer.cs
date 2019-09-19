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
using CMCS.Common.DAO;
using CMCS.DapperDber.Dbs.SqlServerDb;

namespace CMCS.DumblyConcealer.Win.DumblyTasks
{
    public partial class FrmPneumaticTransfer : TaskForm
    {
        RTxtOutputer rTxtOutputer;

        TaskSimpleScheduler taskSimpleScheduler = new TaskSimpleScheduler();

        public FrmPneumaticTransfer()
        {
            InitializeComponent();
        }

        private void FrmPneumaticTransfer_Load(object sender, EventArgs e)
        {
            this.Text = "气动传输接口业务";

            this.rTxtOutputer = new RTxtOutputer(rtxtOutput);

            ExecuteAllTask();
        }

        /// <summary>
        /// 执行所有任务
        /// </summary>
        void ExecuteAllTask()
        {
            #region 气动传输
            taskSimpleScheduler.StartNewTask("气动传输-快速同步", () =>
             {
                 SqlServerDapperDber PneumaticTransfer_Dber = new SqlServerDapperDber(CommonDAO.GetInstance().GetCommonAppletConfigString("气动传输接口连接字符串"));
                 EquPneumaticTransferDAO dAO = new EquPneumaticTransferDAO(GlobalVars.MachineCode_QD, PneumaticTransfer_Dber);
                 if (CommonDAO.GetInstance().TestPing(dAO.EquDber.Connection.DataSource))
                 {
                     dAO.SyncCYGError(this.rTxtOutputer.Output);
                     dAO.SyncQDCSCmd(this.rTxtOutputer.Output);
                     dAO.SyncQDCSRecord(this.rTxtOutputer.Output);
                     dAO.SyncSignalDatal(this.rTxtOutputer.Output);
                 }
                 else
                     rTxtOutputer.Output(dAO.MachineCode + "网络不通", eOutputType.Warn);
             }, 5000, OutputError);
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
