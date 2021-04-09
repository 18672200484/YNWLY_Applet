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

namespace CMCS.DumblyConcealer.Win.DumblyTasks
{
	public partial class FrmAutoMaker : TaskForm
	{
		RTxtOutputer rTxtOutputer;

		TaskSimpleScheduler taskSimpleScheduler = new TaskSimpleScheduler();
		CommonDAO commonDAO = CommonDAO.GetInstance();

		public FrmAutoMaker()
		{
			InitializeComponent();
		}

		private void FrmAutoMaker_NCGM_Load(object sender, EventArgs e)
		{
			this.Text = "全自动制样机接口业务";

			this.rTxtOutputer = new RTxtOutputer(rtxtOutput);

			ExecuteAllTask();

		}

		/// <summary>
		/// 执行所有任务
		/// </summary>
		void ExecuteAllTask()
		{
			#region #1全自动制样机
			taskSimpleScheduler.StartNewTask("#1全自动制样机-快速同步", () =>
			{
				EquAutoMakerDAO autoMakerDAO1 = new EquAutoMakerDAO(GlobalVars.MachineCode_QZDZYJ_1, new DapperDber.Dbs.SqlServerDb.SqlServerDapperDber(CommonDAO.GetInstance().GetCommonAppletConfigString("#1全自动制样机接口连接字符串")));
				if (commonDAO.TestPing(autoMakerDAO1.EquDber.Connection.DataSource))
				{
					autoMakerDAO1.SyncCmd(this.rTxtOutputer.Output);
					autoMakerDAO1.SyncSignal(this.rTxtOutputer.Output);
					autoMakerDAO1.SyncMakeDetail(this.rTxtOutputer.Output);
					autoMakerDAO1.SyncError(this.rTxtOutputer.Output);
				}
				else
					rTxtOutputer.Output(autoMakerDAO1.MachineCode + "网络不通", eOutputType.Warn);
			}, 3000, OutputError);

			#endregion

			#region #2全自动制样机
			taskSimpleScheduler.StartNewTask("#2全自动制样机-快速同步", () =>
			{
				EquAutoMakerDAO autoMakerDAO2 = new EquAutoMakerDAO(GlobalVars.MachineCode_QZDZYJ_2, new DapperDber.Dbs.SqlServerDb.SqlServerDapperDber(CommonDAO.GetInstance().GetCommonAppletConfigString("#2全自动制样机接口连接字符串")));
				if (commonDAO.TestPing(autoMakerDAO2.EquDber.Connection.DataSource))
				{
					autoMakerDAO2.SyncCmd(this.rTxtOutputer.Output);
					autoMakerDAO2.SyncXLCmd(this.rTxtOutputer.Output);
					autoMakerDAO2.SyncSignal(this.rTxtOutputer.Output);
					autoMakerDAO2.SyncMakeDetail(this.rTxtOutputer.Output);
					autoMakerDAO2.SyncError(this.rTxtOutputer.Output);
				}
				else
					rTxtOutputer.Output(autoMakerDAO2.MachineCode + "网络不通", eOutputType.Warn);
			}, 2000, OutputError);
			#endregion

			#region #3全自动制样机
			taskSimpleScheduler.StartNewTask("#3全自动制样机-快速同步", () =>
			{
				EquAutoMakerDAO autoMakerDAO3 = new EquAutoMakerDAO(GlobalVars.MachineCode_QZDZYJ_3, new DapperDber.Dbs.SqlServerDb.SqlServerDapperDber(CommonDAO.GetInstance().GetCommonAppletConfigString("#3全自动制样机接口连接字符串")));
				if (commonDAO.TestPing(autoMakerDAO3.EquDber.Connection.DataSource))
				{
					autoMakerDAO3.SyncCmd(this.rTxtOutputer.Output);
					autoMakerDAO3.SyncXLCmd(this.rTxtOutputer.Output);
					autoMakerDAO3.SyncSignal(this.rTxtOutputer.Output);
					autoMakerDAO3.SyncMakeDetail(this.rTxtOutputer.Output);
					autoMakerDAO3.SyncError(this.rTxtOutputer.Output);
				}
				else
					rTxtOutputer.Output(autoMakerDAO3.MachineCode + "网络不通", eOutputType.Warn);
			}, 3000, OutputError);
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
