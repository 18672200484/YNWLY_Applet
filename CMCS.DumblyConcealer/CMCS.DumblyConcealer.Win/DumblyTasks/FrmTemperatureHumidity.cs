using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CMCS.DumblyConcealer.Win.Core;
using CMCS.DumblyConcealer.Tasks.TemperatureHumidityInstrument;
using CMCS.Common.Utilities;
using CMCS.DumblyConcealer.Enums;
using System.Net.Sockets;
using System.Xml;
using System.IO;
using System.Reflection;
using System.Threading;
using CMCS.Common.DAO;

namespace CMCS.DumblyConcealer.Win.DumblyTasks
{
	public partial class FrmTemperatureHumidity : TaskForm
	{
		RTxtOutputer rTxtOutputer;
		TaskSimpleScheduler taskSimpleScheduler = new TaskSimpleScheduler();

		bool isStart = false;
		/// <summary>
		/// 是否启动
		/// </summary>
		bool IsStart
		{
			get { return isStart; }
			set { isStart = value; }
		}

		List<IAssayGraber> Grabers = new List<IAssayGraber>();

		public FrmTemperatureHumidity()
		{
			InitializeComponent();
		}

		private void TemperatureHumidity_Load(object sender, EventArgs e)
		{
			this.Text = "温湿度仪数据同步";

			this.rTxtOutputer = new RTxtOutputer(rtxtOutput);
			//InitPerformer();
			ExecuteAllTask();
		}

		/// <summary>
		/// 初始化
		/// </summary>
		void InitPerformer()
		{
			XmlDocument xdoc = new XmlDocument();
			xdoc.Load(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Common.AppConfig.xml"));
			foreach (XmlNode xnItem in xdoc.SelectNodes("/CommonAppConfig/Instrument/Item[@Enabled='true']"))
			{
				try
				{
					XmlNodeList xnlParam = xnItem.SelectNodes("Param");
					object[] objParam = new object[xnlParam.Count];
					for (int i = 0; i < xnlParam.Count; i++)
					{
						objParam[i] = xnlParam[i].InnerText;
					}
					IAssayGraber entity = new IAssayGraber();
					entity.FacilityNumber = xnItem.Attributes["FacilityNumber"].Value;
					entity.Ip = objParam[0].ToString();
					if (this.Grabers.Select(a => a.FacilityNumber).ToList().Contains(entity.FacilityNumber))
						continue;
					entity.Port = Convert.ToInt32(objParam[1].ToString());
					entity.DelDays = Convert.ToInt32(objParam[2].ToString());
					entity.Milliseconds = Convert.ToInt32(objParam[3].ToString());
					entity.LocalPort = Convert.ToInt32(objParam[4].ToString());
					entity.SocketMethod = new THDiscriminatorTCPIP();
					entity.SocketMethod.InitConnect(entity.Ip, entity.Port, entity.LocalPort, entity.FacilityNumber, entity.DelDays, this.rTxtOutputer.Output);
					this.Grabers.Add(entity);
				}
				catch (Exception ex)
				{
					OutputError(string.Format("【实例化失败】 - {0}", xnItem.Attributes["FacilityNumber"].Value), ex);
				}
			}
		}

		/// <summary>
		/// 关闭连接
		/// </summary>
		void ClosePerformer()
		{
			foreach (IAssayGraber graber in Grabers)
			{
				if (graber.SocketMethod != null) graber.SocketMethod.Close();
			}
		}
		/// <summary>
		/// 执行所有任务
		/// </summary>
		void ExecuteAllTask()
		{
			//taskSimpleScheduler.StartNewTask("定时检测设备", () =>
			//{
			//    InitPerformer();
			//    foreach (IAssayGraber graber in Grabers)
			//    {
			//        graber.SocketMethod.StartListening(graber.Milliseconds * 1000 / 2);
			//    }
			//}, 0, OutputError);

			taskSimpleScheduler.StartNewTask("定时检测设备", () =>
			{
				//DateTime time = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " " + CommonDAO.GetInstance().GetCommonAppletConfigString("温湿度仪写入时间"));
				//if (!IsStart && DateTime.Now >= time && DateTime.Now < time.AddMinutes(2))
				//{
				InitPerformer();
				IsStart = true;
				foreach (IAssayGraber graber in Grabers)
				{
					graber.SocketMethod.StartListening(graber.Milliseconds * 1000 / 2);
				}
				//}
				//else if (IsStart && DateTime.Now >= time.AddMinutes(2))
				//{
				//	IsStart = false;
				//	ClosePerformer();
				//}
			}, 0, OutputError);
		}

		/// </summary>
		/// <param name="text"></param>
		/// <param name="ex"></param>
		void OutputError(string text, Exception ex)
		{
			this.rTxtOutputer.Output(text + Environment.NewLine + ex.Message, eOutputType.Error);

			Log4Neter.Error(text, ex);
			/// <summary>
			/// 输出异常信息
		}

		/// <summary>
		/// 窗体关闭后
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void FrmAutoCupboard_NCGM_FormClosed(object sender, FormClosedEventArgs e)
		{
			try
			{
				ClosePerformer();
				// 注意：必须取消任务
				this.taskSimpleScheduler.Cancal();
			}
			catch (Exception ex)
			{
				OutputError("取消任务", ex);
			}
		}
	}
}
