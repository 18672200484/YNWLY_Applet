using System;
using System.Windows.Forms;
using CMCS.Common.Utilities;
using CMCS.DumblyConcealer.Enums;
using CMCS.DumblyConcealer.Tasks.BeltSampler;
using CMCS.DumblyConcealer.Win.Core;
using CMCS.Common.DAO;
using CMCS.DapperDber.Dbs.SqlServerDb;

namespace CMCS.DumblyConcealer.Win.DumblyTasks
{
    public partial class FrmBeltSampler : TaskForm
    {
        RTxtOutputer rTxtOutputer;
        TaskSimpleScheduler taskSimpleScheduler = new TaskSimpleScheduler();

        public FrmBeltSampler()
        {
            InitializeComponent();
        }

        private void FrmBeltSampler_NCGM_Load(object sender, EventArgs e)
        {
            this.Text = "皮带采样机接口业务";

            this.rTxtOutputer = new RTxtOutputer(rtxtOutput);

            ExecuteAllTask();
        }

        /// <summary>
        /// 执行所有任务
        /// </summary>
        void ExecuteAllTask()
        {
            SqlServerDapperDber BeltSampler_Dber_Out = null;
            EquBeltSamplerDAO beltSamplerDAO_Out = null;

            taskSimpleScheduler.StartNewTask("出场皮带采样机快速同步", () =>
            {
                BeltSampler_Dber_Out = new SqlServerDapperDber(CommonDAO.GetInstance().GetCommonAppletConfigString("出场皮带采样机接口连接字符串"));
                beltSamplerDAO_Out = new EquBeltSamplerDAO("出场", BeltSampler_Dber_Out);
                if (CommonDAO.GetInstance().TestPing(BeltSampler_Dber_Out.Connection.DataSource))
                {
                    beltSamplerDAO_Out.SyncSignal(this.rTxtOutputer.Output);
                    beltSamplerDAO_Out.SyncError(this.rTxtOutputer.Output);
                    beltSamplerDAO_Out.SyncBarrel(this.rTxtOutputer.Output);
                    beltSamplerDAO_Out.SyncPlan(this.rTxtOutputer.Output);
                    beltSamplerDAO_Out.SyncUnloadCmd(this.rTxtOutputer.Output);
                    beltSamplerDAO_Out.SyncCmd(this.rTxtOutputer.Output);
                    beltSamplerDAO_Out.SyncUnloadResult(this.rTxtOutputer.Output);
                }
                else
                    rTxtOutputer.Output("出场皮带采样机网络不通");
            }, 3000, OutputError);

            this.taskSimpleScheduler.StartNewTask("出场皮带采样机运行状态-心跳", () =>
            {
                if (CommonDAO.GetInstance().TestPing(BeltSampler_Dber_Out.Connection.DataSource))
                {
                    beltSamplerDAO_Out.SyncHeartbeatSignal();
                }
                else
                    rTxtOutputer.Output("出场皮带采样机网络不通");
            }, 30000, OutputError);

            #region 入场
            SqlServerDapperDber BeltSampler_Dber_In = null;
            EquBeltSamplerDAO beltSamplerDAO_In = null;

            taskSimpleScheduler.StartNewTask("入场皮带采样机快速同步", () =>
            {
                BeltSampler_Dber_In = new SqlServerDapperDber(CommonDAO.GetInstance().GetCommonAppletConfigString("入场皮带采样机接口连接字符串"));
                beltSamplerDAO_In = new EquBeltSamplerDAO("入场", BeltSampler_Dber_In);
                if (CommonDAO.GetInstance().TestPing(BeltSampler_Dber_In.Connection.DataSource))
                {
                    beltSamplerDAO_In.SyncSignal(this.rTxtOutputer.Output);
                    beltSamplerDAO_In.SyncError(this.rTxtOutputer.Output);
                    beltSamplerDAO_In.SyncBarrel(this.rTxtOutputer.Output);
                    beltSamplerDAO_In.SyncPlan(this.rTxtOutputer.Output);
                    beltSamplerDAO_In.SyncUnloadCmd(this.rTxtOutputer.Output);
                    beltSamplerDAO_In.SyncCmd(this.rTxtOutputer.Output);
                    beltSamplerDAO_In.SyncUnloadResult(this.rTxtOutputer.Output);
                }
                else
                    rTxtOutputer.Output("入场皮带采样机网络不通");
            }, 3000, OutputError);

            this.taskSimpleScheduler.StartNewTask("入场皮带采样机运行状态-心跳", () =>
            {
                if (CommonDAO.GetInstance().TestPing(BeltSampler_Dber_In.Connection.DataSource))
                {
                    beltSamplerDAO_In.SyncHeartbeatSignal();
                }
                else
                    rTxtOutputer.Output("入场皮带采样机网络不通");
            }, 30000, OutputError);
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
        private void FrmBeltSampler_NCGM_FormClosed(object sender, FormClosedEventArgs e)
        {
            // 注意：必须取消任务
            this.taskSimpleScheduler.Cancal();
        }
    }
}
