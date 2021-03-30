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
using CMCS.DumblyConcealer.Tasks.HighBeltWeight;

namespace CMCS.DumblyConcealer.Win.DumblyTasks
{
	public partial class FrmBeltWeight : TaskForm
	{

		RTxtOutputer rTxtOutputer;
		TaskSimpleScheduler taskSimpleScheduler = new TaskSimpleScheduler();
		List<IBeltWeightGraber> Grabers = new List<IBeltWeightGraber>();
		CommonDAO commonDAO = CommonDAO.GetInstance();
		public FrmBeltWeight()
		{
			InitializeComponent();
		}

		private void FrmAssayDevice_Load(object sender, EventArgs e)
		{
			this.Text = "皮带秤接口业务";

			this.rTxtOutputer = new RTxtOutputer(rtxtOutput);
			InitPerformer();
			ExecuteAllTask();
		}

		/// <summary>
		/// 执行所有任务
		/// </summary>
		void ExecuteAllTask()
		{
			taskSimpleScheduler.StartNewTask("皮带秤", () =>
			{
				foreach (IBeltWeightGraber graber in Grabers)
				{
					graber.SocketMethod.StartListening(graber.Milliseconds * 1000, graber.DelDays);
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
				CmcsCMEquipment TempEquipment = commonDAO.SelfDber.Entity<CmcsCMEquipment>("where EquipmentName='皮带秤'");
				if (TempEquipment == null)
				{
					this.rTxtOutputer.Output("皮带秤设备未配置", eOutputType.Warn);
					return;
				}
				foreach (CmcsCMEquipment item in commonDAO.SelfDber.Entities<CmcsCMEquipment>("where Parentid=:Parentid", new { Parentid = TempEquipment.Id }))
				{
					string[] paramer = item.EquipmentCode.Split('|');
					if (paramer.Length != 3)
					{
						this.rTxtOutputer.Output(string.Format("{0}参数配置错误", item.EquipmentName), eOutputType.Warn);
						continue;
					}
					IBeltWeightGraber entity = new IBeltWeightGraber();
					entity.FacilityNumber = item.EquipmentName;
					entity.Ip = paramer[0];
					entity.Port = 502;
					if (!CommonDAO.GetInstance().TestPing(entity.Ip))
					{
						this.rTxtOutputer.Output(string.Format("{0}网络不通", entity.FacilityNumber), eOutputType.Warn);
						//continue;
					}

					entity.DelDays = Convert.ToInt32(paramer[2]);
					entity.Milliseconds = Convert.ToInt32(paramer[1]);
					entity.SocketMethod = new EquHighBeltWeightDAO();
					entity.SocketMethod.InitListening(entity.Ip, entity.Port, entity.LocalPort, entity.FacilityNumber, this.rTxtOutputer.Output);
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
			foreach (IBeltWeightGraber graber in Grabers)
			{
				graber.SocketMethod.Close();
			}
			// 注意：必须取消任务
			this.taskSimpleScheduler.Cancal();
		}
	}
}
