using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using CMCS.CarTransport.Queue.Core;
using System.Threading;
using System.IO;
using CMCS.CarTransport.DAO;
using CMCS.Common.DAO;
using System.IO.Ports;
using CMCS.Common.Utilities;
using LED.YB14;
using CMCS.CarTransport.Queue.Enums;
using CMCS.Common.Entities;
using CMCS.Common.Entities.CarTransport;
using CMCS.Common;
using CMCS.CarTransport.Queue.Frms.Sys;
using DevComponents.DotNetBar.Controls;
using CMCS.Common.Views;
using DevComponents.DotNetBar.SuperGrid;
using CMCS.Common.Enums;
using CMCS.Common.Entities.BaseInfo;
using CMCS.Common.Entities.Sys;
using CMCS.Common.Entities.Fuel;
using CMCS.CarTransport.Queue.Frms.BaseInfo.Autotruck;
using CMCS.CarTransport.Queue.Frms.Transport.Print;
using CMCS.CarTransport.Queue.Frms.Transport.Print.SaleFuelPrint;
using DevComponents.DotNetBar.SuperGrid.Style;

namespace CMCS.CarTransport.Queue.Frms
{
	public partial class FrmQueuer : DevComponents.DotNetBar.Metro.MetroForm
	{
		/// <summary>
		/// 窗体唯一标识符
		/// </summary>
		public static string UniqueKey = "FrmQueuer";

		public FrmQueuer()
		{
			InitializeComponent();
		}

		#region 业务处理类
		CarTransportDAO carTransportDAO = CarTransportDAO.GetInstance();
		QueuerDAO queuerDAO = QueuerDAO.GetInstance();
		CommonDAO commonDAO = CommonDAO.GetInstance();
		#endregion

		#region 设备属性

		IocControler iocControler;
		/// <summary>
		/// 语音播报
		/// </summary>
		VoiceSpeaker voiceSpeaker = new VoiceSpeaker();

		bool inductorCoil1 = false;
		/// <summary>
		/// 地感1状态 true=有信号  false=无信号
		/// </summary>
		public bool InductorCoil1
		{
			get
			{
				return inductorCoil1;
			}
			set
			{
				inductorCoil1 = value;

				panelEx10.Refresh();

				commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.地感1信号.ToString(), value ? "1" : "0");
			}
		}

		int inductorCoil1Port;
		/// <summary>
		/// 地感1端口
		/// </summary>
		public int InductorCoil1Port
		{
			get { return inductorCoil1Port; }
			set { inductorCoil1Port = value; }
		}

		bool inductorCoil2 = false;
		/// <summary>
		/// 地感2状态 true=有信号  false=无信号
		/// </summary>
		public bool InductorCoil2
		{
			get
			{
				return inductorCoil2;
			}
			set
			{
				inductorCoil2 = value;

				panelEx10.Refresh();

				commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.地感2信号.ToString(), value ? "1" : "0");
			}
		}

		int inductorCoil2Port;
		/// <summary>
		/// 地感2端口
		/// </summary>
		public int InductorCoil2Port
		{
			get { return inductorCoil2Port; }
			set { inductorCoil2Port = value; }
		}

		bool inductorCoil3 = false;
		/// <summary>
		/// 地感3状态 true=有信号  false=无信号
		/// </summary>
		public bool InductorCoil3
		{
			get
			{
				return inductorCoil3;
			}
			set
			{
				inductorCoil3 = value;

				panelEx10.Refresh();

				commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.地感3信号.ToString(), value ? "1" : "0");
			}
		}

		int inductorCoil3Port;
		/// <summary>
		/// 地感3端口
		/// </summary>
		public int InductorCoil3Port
		{
			get { return inductorCoil3Port; }
			set { inductorCoil3Port = value; }
		}

		bool inductorCoil4 = false;
		/// <summary>
		/// 地感4状态 true=有信号  false=无信号
		/// </summary>
		public bool InductorCoil4
		{
			get
			{
				return inductorCoil4;
			}
			set
			{
				inductorCoil4 = value;

				panelEx10.Refresh();

				commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.地感4信号.ToString(), value ? "1" : "0");
			}
		}

		int inductorCoil4Port;
		/// <summary>
		/// 地感4端口
		/// </summary>
		public int InductorCoil4Port
		{
			get { return inductorCoil4Port; }
			set { inductorCoil4Port = value; }
		}

		#endregion

		#region 公共Vars

		/// <summary>
		/// 当前选择的道路
		/// </summary>
		private ePassWay CurrentWay = ePassWay.Way1;

		/// <summary>
		/// 是否自动匹配预报（多个预报时按时间先后匹配）
		/// </summary>
		private bool AutoForecast = true;

		private string queueType;
		/// <summary>
		/// 排队类型
		/// </summary>
		public string QueueType
		{
			get { return queueType; }
			set
			{
				queueType = value;
				if (value == eTransportType.原料煤入场.ToString() || value == eTransportType.仓储煤入场.ToString() || value == eTransportType.中转煤入场.ToString())
					this.superTabControl2.SelectedTab = superTabItem_BuyFuel;
				else if (value == eTransportType.仓储煤出场.ToString() || value == eTransportType.中转煤出场.ToString() || value == eTransportType.销售掺配煤.ToString() || value == eTransportType.销售直销煤.ToString())
					this.superTabControl2.SelectedTab = superTabItem_SaleFuel;
				else if (value == eTransportType.其他物资.ToString())
					this.superTabControl2.SelectedTab = superTabItem_Goods;
				else if (value == eTransportType.来访车辆.ToString())
					this.superTabControl2.SelectedTab = superTabItem_Visit;
			}
		}

		#region Vars1
		public static PassCarQueuer passCarQueuer = new PassCarQueuer();

		ImperfectCar currentImperfectCar;
		/// <summary>
		/// 识别或选择的车辆凭证
		/// </summary>
		public ImperfectCar CurrentImperfectCar
		{
			get { return currentImperfectCar; }
			set
			{
				currentImperfectCar = value;

				if (value != null)
				{
					panCurrentCarNumber.Text = value.Voucher;
					//识别到车辆开始预警检测
					timer_EarlyWarning.Enabled = true;
				}
				else
				{
					panCurrentCarNumber.Text = "等待车辆";
					//重置后 结束预警检测
					timer_EarlyWarning.Enabled = false;
					commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.超时预警.ToString(), "0");
					OverTime_EarlyWarningCount = 0;
				}
			}
		}

		eFlowFlag currentFlowFlag = eFlowFlag.等待车辆;
		/// <summary>
		/// 当前业务流程标识
		/// </summary>
		public eFlowFlag CurrentFlowFlag
		{
			get { return currentFlowFlag; }
			set
			{
				currentFlowFlag = value;

				lblFlowFlag.Text = value.ToString();
			}
		}

		CmcsAutotruck currentAutotruck;
		/// <summary>
		/// 当前车
		/// </summary>
		public CmcsAutotruck CurrentAutotruck
		{
			get { return currentAutotruck; }
			set
			{
				currentAutotruck = value;

				if (value != null)
				{
					commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.当前车Id.ToString(), value.Id);
					commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.当前车号.ToString(), value.CarNumber);

					panCurrentCarNumber.Text = value.CarNumber;

					if (this.CurrentImperfectCar != null && !this.CurrentImperfectCar.IsFromDevice)//不是来自设备的车辆手动进行匹配
					{
						if (superTabControl2.SelectedTab == superTabItem_BuyFuel)
							txtCarNumber_BuyFuel.Text = value.CarNumber;
						else if (superTabControl2.SelectedTab == superTabItem_SaleFuel)
							txtCarNumber_SaleFuel.Text = value.CarNumber;
						else if (superTabControl2.SelectedTab == superTabItem_Goods)
							txtCarNumber_Goods.Text = value.CarNumber;
						else if (superTabControl2.SelectedTab == superTabItem_Visit)
							txtCarNumber_Visit.Text = value.CarNumber;
					}
				}
				else
				{
					//重置车辆信息
					commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.当前车Id.ToString(), string.Empty);
					commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.当前车号.ToString(), string.Empty);

					txtCarNumber_BuyFuel.ResetText();
					txtCarNumber_SaleFuel.ResetText();
					txtCarNumber_Goods.ResetText();
					txtCarNumber_Visit.ResetText();

					panCurrentCarNumber.ResetText();
				}
			}
		}

		private string carnumber1;
		/// <summary>
		/// 当前车号1
		/// </summary>
		public string CarNumber1
		{
			get { return carnumber1; }
			set
			{
				carnumber1 = value;
			}
		}

		private eCarType currentCarType;
		/// <summary>
		/// 当前车类型
		/// </summary>
		public eCarType CurrentCarType
		{
			get { return currentCarType; }
			set
			{
				currentCarType = value;
			}
		}

		/// <summary>
		/// 预警检测
		/// </summary>
		System.Timers.Timer timer_EarlyWarning = new System.Timers.Timer();

		/// <summary>
		/// 超时预警计数
		/// </summary>
		static int OverTime_EarlyWarningCount = 0;

		static CmcsUnFinishTransport currentUnFinishTransport;
		/// <summary>
		/// 当前未完成运输记录
		/// </summary>
		public CmcsUnFinishTransport CurrentUnFinishTransport
		{
			get { return currentUnFinishTransport; }
			set
			{
				currentUnFinishTransport = value;
				if (value != null)
				{
					if (value.CarType == eTransportType.原料煤入场.ToString() || value.CarType == eTransportType.仓储煤入场.ToString() || value.CarType == eTransportType.中转煤入场.ToString())
					{
						txtCarNumber_BuyFuel.Text = this.CurrentAutotruck.CarNumber;
						superTabControl2.SelectedTab = superTabItem_BuyFuel;
						this.timer_BuyFuel_Cancel = false;
						timer_BuyFuel_Tick(null, null);
					}
					else if (value.CarType == eTransportType.仓储煤出场.ToString() || value.CarType == eTransportType.中转煤出场.ToString() || value.CarType == eTransportType.销售掺配煤.ToString() || value.CarType == eTransportType.销售直销煤.ToString())
					{
						txtCarNumber_SaleFuel.Text = this.CurrentAutotruck.CarNumber;
						superTabControl2.SelectedTab = superTabItem_SaleFuel;
						this.timer_SaleFuel_Cancel = false;
						timer_SaleFuel_Tick(null, null);
					}
					else if (value.CarType == eTransportType.其他物资.ToString())
					{
						txtCarNumber_Goods.Text = this.CurrentAutotruck.CarNumber;
						superTabControl2.SelectedTab = superTabItem_Goods;
						this.timer_Goods_Cancel = false;
						timer_Goods_Tick(null, null);
					}
				}
			}
		}

		#endregion

		#region Vars2

		public static PassCarQueuer passCarQueuer2 = new PassCarQueuer();

		ImperfectCar currentImperfectCar2;
		/// <summary>
		/// 识别或选择的车辆凭证2
		/// </summary>
		public ImperfectCar CurrentImperfectCar2
		{
			get { return currentImperfectCar2; }
			set
			{
				currentImperfectCar2 = value;

				if (value != null)
				{
					panCurrentCarNumber2.Text = value.Voucher;
					//识别到车辆开始预警检测
					timer_EarlyWarning2.Enabled = true;
				}
				else
				{
					panCurrentCarNumber2.Text = "等待车辆";
					//重置后 结束预警检测
					timer_EarlyWarning2.Enabled = false;
					commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.超时预警2.ToString(), "0");
					OverTime_EarlyWarningCount2 = 0;
				}
			}
		}

		eFlowFlag currentFlowFlag2 = eFlowFlag.等待车辆;
		/// <summary>
		/// 当前业务流程标识2
		/// </summary>
		public eFlowFlag CurrentFlowFlag2
		{
			get { return currentFlowFlag2; }
			set
			{
				currentFlowFlag2 = value;

				lblFlowFlag2.Text = value.ToString();
			}
		}

		CmcsAutotruck currentAutotruck2;
		/// <summary>
		/// 当前车2
		/// </summary>
		public CmcsAutotruck CurrentAutotruck2
		{
			get { return currentAutotruck2; }
			set
			{
				currentAutotruck2 = value;

				panCurrentCarNumber2.ResetText();

				if (value != null)
				{
					commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.当前车Id2.ToString(), value.Id);
					commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.当前车号2.ToString(), value.CarNumber);

					panCurrentCarNumber2.Text = value.CarNumber;

					if (this.CurrentImperfectCar2 != null && !this.CurrentImperfectCar2.IsFromDevice)//不是来自设备的车辆手动进行匹配
					{
						if (superTabControl2.SelectedTab == superTabItem_BuyFuel)
							txtCarNumber_BuyFuel2.Text = value.CarNumber;
						else if (superTabControl2.SelectedTab == superTabItem_SaleFuel)
							txtCarNumber_SaleFuel2.Text = value.CarNumber;
						else if (superTabControl2.SelectedTab == superTabItem_Goods)
							txtCarNumber_Goods.Text = value.CarNumber;
						else if (superTabControl2.SelectedTab == superTabItem_Visit)
							txtCarNumber_Visit.Text = value.CarNumber;
					}
				}
				else
				{
					commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.当前车Id2.ToString(), string.Empty);
					commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.当前车号2.ToString(), string.Empty);

					txtCarNumber_BuyFuel2.ResetText();
					txtCarNumber_SaleFuel2.ResetText();
					txtCarNumber_Goods.ResetText();
					txtCarNumber_Visit.ResetText();

					panCurrentCarNumber2.ResetText();
				}
			}
		}

		private string carnumber2;
		/// <summary>
		/// 当前车号2
		/// </summary>
		public string CarNumber2
		{
			get { return carnumber2; }
			set
			{
				carnumber2 = value;
			}
		}

		private eCarType currentCarType2;
		/// <summary>
		/// 当前车类型2
		/// </summary>
		public eCarType CurrentCarType2
		{
			get { return currentCarType2; }
			set
			{
				currentCarType2 = value;
			}
		}

		/// <summary>
		/// 预警检测2
		/// </summary>
		System.Timers.Timer timer_EarlyWarning2 = new System.Timers.Timer();

		/// <summary>
		/// 超时预警计数2
		/// </summary>
		static int OverTime_EarlyWarningCount2 = 0;

		private bool isUseYB = false;
		/// <summary>
		/// 是否启用预报
		/// </summary>
		public bool IsUseYB
		{
			get { return isUseYB; }
			set
			{
				isUseYB = value;

				//入场煤
				btnSelectTransportCompany_BuyFuel.Enabled = !value;
				btnSelectSupplier_BuyFuel.Enabled = !value;
				btnSelectMine_BuyFuel.Enabled = !value;
				cmbFuelName_BuyFuel.Enabled = !value;
				cmbSamplingType_BuyFuel.Enabled = !value;
				cmbBuyFuelType.Enabled = !value;

				btnSelectTransportCompany_BuyFuel2.Enabled = !value;
				btnSelectSupplier_BuyFuel2.Enabled = !value;
				btnSelectMine_BuyFuel2.Enabled = !value;
				cmbFuelName_BuyFuel2.Enabled = !value;
				cmbSamplingType_BuyFuel2.Enabled = !value;
				cmbBuyFuelType2.Enabled = !value;

				//出场煤
				btnSelectTransportCompany_SaleFuel.Enabled = !value;
				btnSelectSupplyReceive_SaleFuel.Enabled = !value;
				cmbFuelName_SaleFuel.Enabled = !value;
				cmbSalesType.Enabled = !value;
				cmbSamplingType_SaleFuel.Enabled = !value;
				//cmb_CPC.Enabled = !value;
				//cmb_Storage.Enabled = !value;

				btnSelectTransportCompany_SaleFuel2.Enabled = !value;
				btnSelectSupplyReceive_SaleFuel2.Enabled = !value;
				cmbFuelName_SaleFuel2.Enabled = !value;
				cmbSalesType2.Enabled = !value;
				cmbSamplingType_SaleFuel2.Enabled = !value;
				//cmb_CPC2.Enabled = !value;
				//cmb_Storage2.Enabled = !value;
			}
		}
		#endregion

		#endregion

		/// <summary>
		/// 窗体初始化
		/// </summary>
		private void InitForm()
		{
#if DEBUG
			lblFlowFlag.Visible = true;
			FrmDebugConsole.GetInstance().Show();
#else
#endif

			// 重置程序远程控制命令
			commonDAO.ResetAppRemoteControlCmd(CommonAppConfig.GetInstance().AppIdentifier);
			//加载煤种
			LoadFuelkind(new ComboBoxEx[] { cmbFuelName_BuyFuel, cmbFuelName_BuyFuel2, cmbFuelName_SaleFuel, cmbFuelName_SaleFuel2 });
			//加载采样方式
			LoadSampleType(new ComboBoxEx[] { cmbSamplingType_BuyFuel, cmbSamplingType_BuyFuel2, cmbSamplingType_SaleFuel, cmbSamplingType_SaleFuel2 });
			//加载入厂煤类型
			LoadBuyFuelType(new ComboBoxEx[] { cmbBuyFuelType, cmbBuyFuelType2 });
			//加载出厂煤类型
			LoadSaleType(new ComboBoxEx[] { cmbSalesType, cmbSalesType2 });
			//加载成品仓
			LoadCPC(new ComboBoxEx[] { cmb_CPC });
			try
			{
				this.IsUseYB = commonDAO.GetAppletConfigBoolen("是否启用预报");
			}
			catch { }
			timer_EarlyWarning.Interval = 1000;
			timer_EarlyWarning.Elapsed += timer_EarlyWarning_Elapsed;

			timer_EarlyWarning2.Interval = 1000;
			timer_EarlyWarning2.Elapsed += timer_EarlyWarning2_Elapsed;

			QueueType = commonDAO.GetAppletConfigString("排队类型");
		}

		#region 预警检测
		void timer_EarlyWarning_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			if (OverTime_EarlyWarningCount > commonDAO.GetCommonAppletConfigInt32("入厂超长预警时间"))
			{
				commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.超时预警.ToString(), "1");
				timer_EarlyWarning.Enabled = false;
			}
			else
			{
				OverTime_EarlyWarningCount++;
			}
		}
		void timer_EarlyWarning2_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			if (OverTime_EarlyWarningCount2 > commonDAO.GetCommonAppletConfigInt32("入厂超长预警时间"))
			{
				commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.超时预警2.ToString(), "1");
				timer_EarlyWarning2.Enabled = false;
			}
			else
			{
				OverTime_EarlyWarningCount2++;
			}
		}
		#endregion

		private void FrmQueuer_Load(object sender, EventArgs e)
		{
		}

		/// <summary>
		/// 对窗体及其所有子控件进行双重缓冲
		/// </summary>
		//protected override CreateParams CreateParams
		//{
		//    get
		//    {
		//        CreateParams cp = base.CreateParams;
		//        cp.ExStyle |= 0x02000000;
		//        return cp;
		//    }
		//}

		private void FrmQueuer_Shown(object sender, EventArgs e)
		{
			if (commonDAO.GetAppletConfigString("启用设备") == "1")
				InitHardware();

			timer1.Enabled = true;
			timer2.Enabled = true;
			timer_BuyFuel.Enabled = true;
			timer_Goods.Enabled = true;
			timer_SaleFuel.Enabled = true;
			timer_Visit.Enabled = true;

			timer_BuyFuel2.Enabled = true;
			timer_Goods2.Enabled = true;
			timer_SaleFuel2.Enabled = true;
			timer_Visit2.Enabled = true;

			InitForm();
		}

		private void FrmQueuer_FormClosing(object sender, FormClosingEventArgs e)
		{
			// 卸载设备
			UnloadHardware();
		}

		#region 设备相关

		#region IO控制器

		void Iocer_StatusChange(bool status)
		{
			// 接收IO控制器状态 
			InvokeEx(() =>
			{
				slightIOC.LightColor = (status ? Color.Green : Color.Red);

				commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.IO控制器_连接状态.ToString(), status ? "1" : "0");
			});
		}

		/// <summary>
		/// IO控制器接收数据时触发
		/// </summary>
		/// <param name="receiveValue"></param>
		void Iocer_Received(int[] receiveValue)
		{
			// 接收地感状态  
			InvokeEx(() =>
			{
				this.InductorCoil1 = (receiveValue[this.InductorCoil1Port - 1] == 1);
				this.InductorCoil2 = (receiveValue[this.InductorCoil2Port - 1] == 1);
				this.InductorCoil3 = (receiveValue[this.InductorCoil3Port - 1] == 1);
				this.InductorCoil4 = (receiveValue[this.InductorCoil4Port - 1] == 1);
			});
		}

		/// <summary>
		/// 允许通行
		/// </summary>
		void LetPass()
		{
			if (this.CurrentImperfectCar != null && this.CurrentImperfectCar.PassWay == ePassWay.Way1)
			{
				if (this.iocControler != null)
				{
					this.iocControler.Gate1Up();
					this.iocControler.GreenLight1();
				}
				this.CurrentFlowFlag = eFlowFlag.等待离开;
			}
			else if (this.CurrentImperfectCar2 != null && this.CurrentImperfectCar2.PassWay == ePassWay.Way2)
			{
				if (this.iocControler != null)
				{
					this.iocControler.Gate2Up();
					this.iocControler.GreenLight2();
				}
				this.CurrentFlowFlag2 = eFlowFlag.等待离开;
			}
		}

		/// <summary>
		/// 阻断前行
		/// </summary>
		void LetBlocking()
		{
			if (this.iocControler == null) return;
			if (this.CurrentImperfectCar != null && this.CurrentImperfectCar.PassWay == ePassWay.Way1)
			{
				this.iocControler.Gate1Down();
				this.iocControler.RedLight1();
			}
			else if (this.CurrentImperfectCar2 != null && this.CurrentImperfectCar2.PassWay == ePassWay.Way2)
			{
				this.iocControler.Gate2Down();
				this.iocControler.RedLight2();
			}
		}

		#endregion

		#region 车号识别

		void Rwer1_OnScanError(Exception ex)
		{
			Log4Neter.Error("车号识别1", ex);
		}

		void Rwer1_OnStatusChange(bool status)
		{
			// 接收车号识别1状态 
			InvokeEx(() =>
			 {
				 slightRwer1.LightColor = (status ? Color.Green : Color.Red);

				 commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.车号识别1_连接状态.ToString(), status ? "1" : "0");
			 });
		}
		public void Rwer1_OnReadSuccess(string carnumber)
		{
			if (carnumber != "无车牌" && this.CurrentFlowFlag == eFlowFlag.等待车辆)
			{
				this.CarNumber1 = carnumber;
				passCarQueuer.Enqueue(ePassWay.Way1, CarNumber1, true);
				this.CurrentFlowFlag = eFlowFlag.验证车辆;
				timer1_Tick(null, null);
				UpdateLedShow(carnumber);
				Log4Neter.Info(string.Format("车号识别1识别到车号：{0}", carnumber));

				// 摄像机1
				//string picture1FileName = Path.Combine(SelfVars.CapturePicturePath, Guid.NewGuid().ToString() + ".bmp");
				//Hardwarer.Rwer1.Capture(picture1FileName);
			}
		}

		void Rwer2_OnScanError(Exception ex)
		{
			Log4Neter.Error("车号识别2", ex);
		}

		void Rwer2_OnStatusChange(bool status)
		{
			// 接收车号识别2状态 
			InvokeEx(() =>
			  {
				  slightRwer2.LightColor = (status ? Color.Green : Color.Red);

				  commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.车号识别2_连接状态.ToString(), status ? "1" : "0");
			  });
		}
		public void Rwer2_OnReadSuccess(string carnumber)
		{
			if (carnumber != "无车牌" && this.CurrentFlowFlag2 == eFlowFlag.等待车辆)
			{
				this.CarNumber2 = carnumber;
				passCarQueuer2.Enqueue(ePassWay.Way2, CarNumber1, true);
				this.CurrentFlowFlag2 = eFlowFlag.验证车辆;
				timer2_Tick(null, null);
				UpdateLedShow(carnumber);
				Log4Neter.Info(string.Format("车号识别2识别到车号：{0}", carnumber));

				// 摄像机2
				//string picture1FileName = Path.Combine(SelfVars.CapturePicturePath, Guid.NewGuid().ToString() + ".bmp");
				//Hardwarer.Rwer2.Capture(picture1FileName);
			}
		}

		#endregion

		#region LED显示屏

		/// <summary>
		/// 生成12字节的文本内容
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		private string GenerateFillLedContent12(string value)
		{
			int length = Encoding.Default.GetByteCount(value);
			if (length < 12) return value + "".PadRight(12 - length, ' ');

			return value;
		}

		/// <summary>
		/// 更新LED动态区域
		/// </summary>
		/// <param name="value1">第一行内容</param>
		/// <param name="value2">第二行内容</param>
		private void UpdateLedShow(string value1 = "", string value2 = "")
		{
			if (this.CurrentImperfectCar == null) return;

			if (this.CurrentImperfectCar.PassWay == ePassWay.Way1)
				UpdateLed1Show(value1, value2);
			else if (this.CurrentImperfectCar.PassWay == ePassWay.Way2)
				UpdateLed2Show(value1, value2);
		}

		#region LED1控制卡

		/// <summary>
		/// LED1控制卡屏号
		/// </summary>
		int LED1nScreenNo = 1;
		/// <summary>
		/// LED1动态区域号
		/// </summary>
		int LED1DYArea_ID = 1;
		/// <summary>
		/// LED1更新标识
		/// </summary>
		bool LED1m_bSendBusy = false;

		private bool _LED1ConnectStatus;
		/// <summary>
		/// LED1连接状态
		/// </summary>
		public bool LED1ConnectStatus
		{
			get
			{
				return _LED1ConnectStatus;
			}

			set
			{
				_LED1ConnectStatus = value;

				slightLED1.LightColor = (value ? Color.Green : Color.Red);

				commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.LED屏1_连接状态.ToString(), value ? "1" : "0");
			}
		}

		/// <summary>
		/// LED1显示内容文本
		/// </summary>
		string LED1TempFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Led1TempFile.txt");

		/// <summary>
		/// LED1上一次显示内容
		/// </summary>
		string LED1PrevLedFileContent = string.Empty;

		/// <summary>
		/// 更新LED1动态区域
		/// </summary>
		/// <param name="value1">第一行内容</param>
		/// <param name="value2">第二行内容</param>
		private void UpdateLed1Show(string value1 = "", string value2 = "")
		{
#if DEBUG
			FrmDebugConsole.GetInstance().Output("更新LED1:|" + value1 + "|" + value2 + "|");
#else

#endif
			if (!this.LED1ConnectStatus) return;
			if (this.LED1PrevLedFileContent == value1 + value2) return;

			string ledContent = GenerateFillLedContent12(value1) + GenerateFillLedContent12(value2);

			File.WriteAllText(this.LED1TempFile, ledContent, Encoding.UTF8);

			if (LED1m_bSendBusy == false)
			{
				LED1m_bSendBusy = true;

				//int nResult = YB14DynamicAreaLeder.SendDynamicAreaInfoCommand(this.LED1nScreenNo, this.LED1DYArea_ID);
				//if (nResult != YB14DynamicAreaLeder.RETURN_NOERROR) Log4Neter.Error("更新LED动态区域", new Exception(YB14DynamicAreaLeder.GetErrorMessage("SendDynamicAreaInfoCommand", nResult)));

				LED1m_bSendBusy = false;
			}

			this.LED1PrevLedFileContent = value1 + value2;
		}

		#endregion

		#region LED2控制卡

		/// <summary>
		/// LED2控制卡屏号
		/// </summary>
		int LED2nScreenNo = 1;
		/// <summary>
		/// LED2动态区域号
		/// </summary>
		int LED2DYArea_ID = 1;
		/// <summary>
		/// LED2更新标识
		/// </summary>
		bool LED2m_bSendBusy = false;

		private bool _LED2ConnectStatus;
		/// <summary>
		/// LED2连接状态
		/// </summary>
		public bool LED2ConnectStatus
		{
			get
			{
				return _LED2ConnectStatus;
			}

			set
			{
				_LED2ConnectStatus = value;

				slightLED2.LightColor = (value ? Color.Green : Color.Red);

				commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.LED屏2_连接状态.ToString(), value ? "1" : "0");
			}
		}

		/// <summary>
		/// LED2显示内容文本
		/// </summary>
		string LED2TempFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Led2TempFile.txt");

		/// <summary>
		/// LED2上一次显示内容
		/// </summary>
		string LED2PrevLedFileContent = string.Empty;

		/// <summary>
		/// 更新LED2动态区域
		/// </summary>
		/// <param name="value1">第一行内容</param>
		/// <param name="value2">第二行内容</param>
		private void UpdateLed2Show(string value1 = "", string value2 = "")
		{
#if DEBUG
			FrmDebugConsole.GetInstance().Output("更新LED2:|" + value1 + "|" + value2 + "|");
#else
#endif
			if (!this.LED1ConnectStatus) return;
			if (this.LED2PrevLedFileContent == value1 + value2) return;

			string ledContent = GenerateFillLedContent12(value1) + GenerateFillLedContent12(value2);

			File.WriteAllText(this.LED1TempFile, ledContent, Encoding.UTF8);

			if (LED2m_bSendBusy == false)
			{
				LED2m_bSendBusy = true;

				//int nResult = YB14DynamicAreaLeder.SendDynamicAreaInfoCommand(this.LED2nScreenNo, this.LED2DYArea_ID);
				//if (nResult != YB14DynamicAreaLeder.RETURN_NOERROR) Log4Neter.Error("更新LED动态区域", new Exception(YB14DynamicAreaLeder.GetErrorMessage("SendDynamicAreaInfoCommand", nResult)));

				LED2m_bSendBusy = false;
			}

			this.LED2PrevLedFileContent = value1 + value2;
		}

		#endregion

		#endregion

		#region 设备初始化与卸载

		/// <summary>
		/// 初始化外接设备
		/// </summary>
		private void InitHardware()
		{
			try
			{
				bool success = false;
				if (commonDAO.GetAppletConfigString("启用控制器") == "1")
				{
					this.InductorCoil1Port = commonDAO.GetAppletConfigInt32("IO控制器_地感1端口");
					this.InductorCoil2Port = commonDAO.GetAppletConfigInt32("IO控制器_地感2端口");
					this.InductorCoil3Port = commonDAO.GetAppletConfigInt32("IO控制器_地感3端口");
					this.InductorCoil4Port = commonDAO.GetAppletConfigInt32("IO控制器_地感4端口");

					// IO控制器
					Hardwarer.Iocer.OnReceived += new IOC.JMDM20DIOV2.JMDM20DIOV2Iocer.ReceivedEventHandler(Iocer_Received);
					Hardwarer.Iocer.OnStatusChange += new IOC.JMDM20DIOV2.JMDM20DIOV2Iocer.StatusChangeHandler(Iocer_StatusChange);
					success = Hardwarer.Iocer.OpenCom(commonDAO.GetAppletConfigInt32("IO控制器_串口"), commonDAO.GetAppletConfigInt32("IO控制器_波特率"), commonDAO.GetAppletConfigInt32("IO控制器_数据位"), (StopBits)commonDAO.GetAppletConfigInt32("IO控制器_停止位"), (Parity)commonDAO.GetAppletConfigInt32("IO控制器_校验位"));
					if (!success) MessageBoxEx.Show("IO控制器连接失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					this.iocControler = new IocControler(Hardwarer.Iocer);
				}

				if (commonDAO.GetAppletConfigString("启用车号识别1") == "1")
				{
					// 车号识别1
					Hardwarer.Rwer1.OnActionStatusChange = Rwer1_OnStatusChange;
					Hardwarer.Rwer1.OnActionScanError = Rwer1_OnScanError;
					Hardwarer.Rwer1.OnActionReadSuccess = Rwer1_OnReadSuccess;
					success = Hardwarer.Rwer1.ConnectCamera(commonDAO.GetAppletConfigString("车号识别1_IP地址"), IntPtr.Zero);
					if (!success)
					{
						MessageBoxEx.Show("车号识别1连接失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					}
					else
					{
						Hardwarer.Rwer1.StartPreview(panVideo1.Handle);
					}
				}

				if (commonDAO.GetAppletConfigString("启用车号识别2") == "1")
				{
					// 车号识别2
					Hardwarer.Rwer2.OnActionStatusChange = Rwer2_OnStatusChange;
					Hardwarer.Rwer2.OnActionScanError = Rwer2_OnScanError;
					Hardwarer.Rwer2.OnActionReadSuccess = Rwer2_OnReadSuccess;
					success = Hardwarer.Rwer2.ConnectCamera(commonDAO.GetAppletConfigString("车号识别2_IP地址"), IntPtr.Zero);
					if (!success)
					{
						MessageBoxEx.Show("车号识别2连接失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					}
					else
					{
						Hardwarer.Rwer2.StartPreview(panVideo2.Handle);
					}
				}
				#region LED控制卡1
				if (commonDAO.GetAppletConfigString("LED显示屏1") == "1")
				{
					string led1SocketIP = commonDAO.GetAppletConfigString("LED显示屏1_IP地址");
					if (!string.IsNullOrEmpty(led1SocketIP))
					{
						int nResult = YB14DynamicAreaLeder.AddScreen(YB14DynamicAreaLeder.CONTROLLER_BX_5E1, this.LED1nScreenNo, YB14DynamicAreaLeder.SEND_MODE_NETWORK, 96, 32, 1, 1, "", 0, led1SocketIP, 5005, "");
						if (nResult == YB14DynamicAreaLeder.RETURN_NOERROR)
						{
							nResult = YB14DynamicAreaLeder.AddScreenDynamicArea(this.LED1nScreenNo, this.LED1DYArea_ID, 0, 10, 1, "", 0, 0, 0, 96, 32, 255, 0, 255, 7, 6, 1);
							if (nResult == YB14DynamicAreaLeder.RETURN_NOERROR)
							{
								nResult = YB14DynamicAreaLeder.AddScreenDynamicAreaFile(this.LED1nScreenNo, this.LED1DYArea_ID, this.LED1TempFile, 0, "宋体", 12, 0, 120, 1, 3, 0);
								if (nResult == YB14DynamicAreaLeder.RETURN_NOERROR)
								{
									// 初始化成功
									this.LED1ConnectStatus = true;
									UpdateLed1Show("  等待车辆");
								}
								else
								{
									this.LED1ConnectStatus = false;
									Log4Neter.Error("初始化LED1控制卡", new Exception(YB14DynamicAreaLeder.GetErrorMessage("AddScreenDynamicAreaFile", nResult)));
									MessageBoxEx.Show("LED1控制卡连接失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
								}
							}
							else
							{
								this.LED1ConnectStatus = false;
								Log4Neter.Error("初始化LED1控制卡", new Exception(YB14DynamicAreaLeder.GetErrorMessage("AddScreenDynamicArea", nResult)));
								MessageBoxEx.Show("LED1控制卡连接失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
							}
						}
						else
						{
							this.LED1ConnectStatus = false;
							Log4Neter.Error("初始化LED1控制卡", new Exception(YB14DynamicAreaLeder.GetErrorMessage("AddScreen", nResult)));
							MessageBoxEx.Show("LED1控制卡连接失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						}
					}
				}
				commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.LED屏1_连接状态.ToString(), this.LED1ConnectStatus ? "1" : "0");

				#endregion

				#region LED控制卡2
				if (commonDAO.GetAppletConfigString("LED显示屏2") == "1")
				{
					string led2SocketIP = commonDAO.GetAppletConfigString("LED显示屏2_IP地址");
					if (!string.IsNullOrEmpty(led2SocketIP))
					{
						int nResult = YB14DynamicAreaLeder.AddScreen(YB14DynamicAreaLeder.CONTROLLER_BX_5E1, this.LED2nScreenNo, YB14DynamicAreaLeder.SEND_MODE_NETWORK, 96, 32, 1, 1, "", 0, led2SocketIP, 5005, "");
						if (nResult == YB14DynamicAreaLeder.RETURN_NOERROR)
						{
							nResult = YB14DynamicAreaLeder.AddScreenDynamicArea(this.LED2nScreenNo, this.LED2DYArea_ID, 0, 10, 1, "", 0, 0, 0, 96, 32, 255, 0, 255, 7, 6, 1);
							if (nResult == YB14DynamicAreaLeder.RETURN_NOERROR)
							{
								nResult = YB14DynamicAreaLeder.AddScreenDynamicAreaFile(this.LED2nScreenNo, this.LED2DYArea_ID, this.LED2TempFile, 0, "宋体", 12, 0, 120, 1, 3, 0);
								if (nResult == YB14DynamicAreaLeder.RETURN_NOERROR)
								{
									// 初始化成功
									this.LED2ConnectStatus = true;
									UpdateLed2Show("  等待车辆");
								}
								else
								{
									this.LED1ConnectStatus = false;
									Log4Neter.Error("初始化LED1控制卡", new Exception(YB14DynamicAreaLeder.GetErrorMessage("AddScreenDynamicAreaFile", nResult)));
									MessageBoxEx.Show("LED1控制卡连接失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
								}
							}
							else
							{
								this.LED1ConnectStatus = false;
								Log4Neter.Error("初始化LED1控制卡", new Exception(YB14DynamicAreaLeder.GetErrorMessage("AddScreenDynamicArea", nResult)));
								MessageBoxEx.Show("LED1控制卡连接失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
							}
						}
						else
						{
							this.LED1ConnectStatus = false;
							Log4Neter.Error("初始化LED1控制卡", new Exception(YB14DynamicAreaLeder.GetErrorMessage("AddScreen", nResult)));
							MessageBoxEx.Show("LED1控制卡连接失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						}
					}
				}
				commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.LED屏2_连接状态.ToString(), this.LED2ConnectStatus ? "1" : "0");

				#endregion

			}
			catch (Exception ex)
			{
				Log4Neter.Error("设备初始化", ex);
			}
		}

		/// <summary>
		/// 卸载设备
		/// </summary>
		private void UnloadHardware()
		{
			// 注意此段代码
			Application.DoEvents();

			try
			{
				if (Hardwarer.Iocer.Status)
				{
					Hardwarer.Iocer.OnReceived -= new IOC.JMDM20DIOV2.JMDM20DIOV2Iocer.ReceivedEventHandler(Iocer_Received);
					Hardwarer.Iocer.OnStatusChange -= new IOC.JMDM20DIOV2.JMDM20DIOV2Iocer.StatusChangeHandler(Iocer_StatusChange);

					Hardwarer.Iocer.CloseCom();
				}
			}
			catch { }
			try
			{
				if (this.slightRwer1.LightColor == Color.Green)
					Hardwarer.Rwer1.Close();
			}
			catch { }
			try
			{
				if (this.slightRwer2.LightColor == Color.Green)
					Hardwarer.Rwer2.Close();
			}
			catch { }
			try
			{
				if (this.slightLED1.LightColor == Color.Green)
				{
					YB14DynamicAreaLeder.SendDeleteDynamicAreasCommand(this.LED1nScreenNo, 1, "");
					YB14DynamicAreaLeder.DeleteScreen(this.LED1nScreenNo);
				}
			}
			catch { }
			try
			{
				if (this.slightLED2.LightColor == Color.Green)
				{
					YB14DynamicAreaLeder.SendDeleteDynamicAreasCommand(this.LED2nScreenNo, 1, "");
					YB14DynamicAreaLeder.DeleteScreen(this.LED2nScreenNo);
				}
			}
			catch { }
		}

		#endregion

		#endregion

		#region 道闸控制按钮

		/// <summary>
		/// 道闸1升杆
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnGate1Up_Click(object sender, EventArgs e)
		{
			if (this.iocControler != null) this.iocControler.Gate1Up();
		}

		/// <summary>
		/// 道闸1降杆
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnGate1Down_Click(object sender, EventArgs e)
		{
			if (this.iocControler != null) this.iocControler.Gate1Down();
		}

		/// <summary>
		/// 道闸2升杆
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnGate2Up_Click(object sender, EventArgs e)
		{
			if (this.iocControler != null) this.iocControler.Gate2Up();
		}

		/// <summary>
		/// 道闸2降杆
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnGate2Down_Click(object sender, EventArgs e)
		{
			if (this.iocControler != null) this.iocControler.Gate2Down();
		}

		#endregion

		#region 公共业务

		/// <summary>
		/// 读卡、车号识别任务
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void timer1_Tick(object sender, EventArgs e)
		{
			timer1.Stop();
			timer1.Interval = 2000;

			try
			{
				// 执行远程命令
				ExecAppRemoteControlCmd();

				switch (this.CurrentFlowFlag)
				{
					case eFlowFlag.等待车辆:
						#region
						if (passCarQueuer.Count > 0) this.CurrentFlowFlag = eFlowFlag.验证车辆;
						#endregion
						break;

					case eFlowFlag.验证车辆:
						#region

						//队列中无车时，等待车辆
						if (passCarQueuer.Count == 0)
						{
							this.CurrentFlowFlag = eFlowFlag.等待离开;
							break;
						}

						this.CurrentImperfectCar = passCarQueuer.Dequeue();

						// 方式一：根据识别的车牌号查找车辆信息
						this.CurrentAutotruck = carTransportDAO.GetAutotruckByCarNumber(this.CurrentImperfectCar.Voucher);
						UpdateLedShow(this.CurrentImperfectCar.Voucher);

						if (this.CurrentAutotruck != null)
						{
							if (this.CurrentAutotruck.IsUse == 1)
							{
								this.CurrentUnFinishTransport = carTransportDAO.GetUnFinishTransportByAutotruckId(this.CurrentAutotruck.Id);
								if (this.CurrentUnFinishTransport != null)
								{
									LetPass();
								}
								else if (this.CurrentAutotruck.IsInner == 1)
								{
									LetPass();
								}
								else
								{
									#region 匹配预报
									if (!MatchingForecast(this.CurrentImperfectCar))
										this.CurrentFlowFlag = eFlowFlag.数据录入;
									#endregion
								}
							}
							else
							{
								UpdateLedShow(this.CurrentAutotruck.CarNumber, "已停用");
								this.voiceSpeaker.Speak("车牌号 " + this.CurrentAutotruck.CarNumber + " 已停用，禁止通过", 2, false);

								timer1.Interval = 20000;
							}
						}
						else
						{
							this.voiceSpeaker.Speak("车牌号 " + this.CurrentImperfectCar.Voucher + " 未登记，禁止通过", 2, false);
							UpdateLedShow(this.CurrentImperfectCar.Voucher, "未登记");

							timer1.Interval = 20000;
						}

						#endregion
						break;
				}
			}
			catch (Exception ex)
			{
				Log4Neter.Error("timer1_Tick", ex);
			}
			finally
			{
				timer1.Start();
			}
		}

		/// <summary>
		/// 读卡、车号识别任务2
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void timer2_Tick(object sender, EventArgs e)
		{
			timer2.Stop();
			timer2.Interval = 2000;

			try
			{
				switch (this.CurrentFlowFlag2)
				{
					case eFlowFlag.等待车辆:
						#region

						if (passCarQueuer2.Count > 0) this.CurrentFlowFlag2 = eFlowFlag.验证车辆;

						#endregion
						break;

					case eFlowFlag.验证车辆:
						#region

						// 队列中无车时，等待车辆
						if (passCarQueuer2.Count == 0)
						{
							this.CurrentFlowFlag2 = eFlowFlag.等待车辆;
							break;
						}

						this.CurrentImperfectCar2 = passCarQueuer2.Dequeue();


						// 根据识别的车牌号查找车辆信息
						this.CurrentAutotruck2 = carTransportDAO.GetAutotruckByCarNumber(this.CurrentImperfectCar2.Voucher);
						UpdateLedShow(this.CurrentImperfectCar2.Voucher);

						if (this.CurrentAutotruck2 != null)
						{
							if (this.CurrentAutotruck2.IsUse == 1)
							{
								// 判断是否存在未完结的运输记录，若存在则需用户确认
								bool hasUnFinish = false;
								CmcsUnFinishTransport unFinishTransport = carTransportDAO.GetUnFinishTransportByAutotruckId(this.CurrentAutotruck2.Id, this.CurrentAutotruck2.CarType);
								if (unFinishTransport != null)
								{
									commonDAO.InsertConfirmEvent(unFinishTransport.TransportId, eConfirmType.未完成运输记录.ToString());

									FrmTransport_Confirm frm = new FrmTransport_Confirm(unFinishTransport.TransportId, unFinishTransport.CarType);
									if (frm.ShowDialog() == DialogResult.Yes)
									{
										timer3_Tick(null, null);
									}
									else
									{
										this.CurrentAutotruck2 = null;
										this.CurrentFlowFlag2 = eFlowFlag.等待车辆;
										timer2.Interval = 10000;
										hasUnFinish = true;
									}
								}

								if (!hasUnFinish)
								{
									//匹配预报
									#region
									if (MatchingForecast(this.CurrentImperfectCar2))
										LetPass();
									else
										this.CurrentFlowFlag2 = eFlowFlag.数据录入;

									#endregion
								}
							}
							else
							{
								UpdateLedShow(this.CurrentAutotruck2.CarNumber, "已停用");
								this.voiceSpeaker.Speak("车牌号 " + this.CurrentAutotruck2.CarNumber + " 已停用，禁止通过", 2, false);

								timer2.Interval = 20000;
							}
						}
						else
						{
							// 方式一：车号识别
							this.voiceSpeaker.Speak("车牌号 " + this.CurrentImperfectCar2.Voucher + " 未登记，禁止通过", 2, false);
							UpdateLedShow(this.CurrentImperfectCar2.Voucher, "未登记");

							timer2.Interval = 20000;
						}

						#endregion
						break;
				}
			}
			catch (Exception ex)
			{
				Log4Neter.Error("timer2_Tick", ex);
			}
			finally
			{
				timer2.Start();
			}
		}

		/// <summary>
		/// 匹配来煤预报及入厂信息
		/// </summary>
		/// <param name="carNumber"></param>
		/// <param name="BuyFuelLMYB"></param>
		/// <param name="SaleFuelLMYB"></param>
		private bool MatchingForecast(ImperfectCar ImperfectCar, string supplierName = "")
		{
			bool Success = false;
			CmcsLMYB TheLMYB = null;
			#region 出入场来煤预报
			CmcsLMYBDetail lMYBDetail = queuerDAO.GetBuyFuelForecastDetailByCarNumber(ImperfectCar.Voucher);

			if (lMYBDetail == null)
				return false;
			TheLMYB = lMYBDetail.TheLMYB;
			eTransportType YBType;
			Enum.TryParse(TheLMYB.InFactoryType, out YBType);
			this.QueueType = YBType.ToString();
			if (YBType == eTransportType.原料煤入场 || YBType == eTransportType.仓储煤入场 || YBType == eTransportType.中转煤入场)
			{
				if (ImperfectCar.PassWay == ePassWay.Way1)
				{
					this.SelectedLMYB_BuyFuel = TheLMYB;
					this.cmbBuyFuelType.Text = TheLMYB.InFactoryType;
					this.txtTicketWeight_BuyFuel.Value = (double)lMYBDetail.TicketWeight;
					this.timer_BuyFuel_Cancel = false;
					if (queuerDAO.JoinQueueBuyFuelTransport(this.CurrentAutotruck, lMYBDetail, DateTime.Now))
					{
						btnSaveTransport_BuyFuel.Enabled = false;
						LetPass();
						return true;
					}
				}
				else
				{
					this.SelectedLMYB_BuyFuel2 = TheLMYB;
					this.cmbBuyFuelType2.Text = TheLMYB.InFactoryType;
					this.txtTicketWeight_BuyFuel2.Value = (double)lMYBDetail.TicketWeight;
					this.timer_BuyFuel_Cancel2 = false;
					if (queuerDAO.JoinQueueBuyFuelTransport(this.CurrentAutotruck2, lMYBDetail, DateTime.Now))
					{
						btnSaveTransport_BuyFuel2.Enabled = false;
						LetPass();
						return true;
					}
				}
			}
			else if (YBType == eTransportType.仓储煤出场 || YBType == eTransportType.中转煤出场 || YBType == eTransportType.销售掺配煤 || YBType == eTransportType.销售直销煤)
			{
				if (ImperfectCar.PassWay == ePassWay.Way1)
				{
					this.SelectedCmcsTransportSales = TheLMYB;
					if (lMYBDetail.CPCName.Contains("1"))

						this.timer_SaleFuel_Cancel = false;
					if (queuerDAO.JoinQueueSaleFuelTransport(this.CurrentAutotruck, lMYBDetail, DateTime.Now))
					{
						btnSaveTransport_SaleFuel.Enabled = false;
						LetPass();
						return true;
					}
				}
				else
				{
					this.SelectedCmcsTransportSales2 = TheLMYB;
					if (lMYBDetail.StorageName.Contains("1"))
						this.cmb_CPC2.Text = "#1成品仓";
					else if (lMYBDetail.StorageName.Contains("2"))
						this.cmb_CPC2.Text = "#2成品仓";
					else if (lMYBDetail.StorageName.Contains("3"))
						this.cmb_CPC2.Text = "#3成品仓";

					this.timer_SaleFuel_Cancel2 = false;
					if (queuerDAO.JoinQueueSaleFuelTransport(this.CurrentAutotruck2, lMYBDetail, DateTime.Now))
					{
						btnSaveTransport_SaleFuel2.Enabled = false;
						LetPass();
						return true;
					}
				}
			}

			return Success;
			#endregion

			#region 其他物资

			#endregion

			#region 来访车辆

			#endregion

			return Success;
		}

		/// <summary>
		/// 慢速任务
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void timer3_Tick(object sender, EventArgs e)
		{
			timer3.Stop();
			// 三分钟执行一次
			timer3.Interval = 180000;

			try
			{
				// 入厂煤
				LoadTodayUnFinishBuyFuelTransport();
				LoadTodayFinishBuyFuelTransport();
				LoadTitleBuyFuelTransport();
				// 销售煤 
				LoadTodayUnFinishSaleFuelTransport();
				LoadTodayFinishSaleFuelTransport();

				// 其他物资
				LoadTodayUnFinishGoodsTransport();
				LoadTodayFinishGoodsTransport();

				// 来访车辆
				LoadTodayUnFinishVisitTransport();
				LoadTodayFinishVisitTransport();
			}
			catch (Exception ex)
			{
				Log4Neter.Error("timer3_Tick", ex);
			}
			finally
			{
				timer3.Start();
			}
		}

		/// <summary>
		/// 有车辆在当前道路上
		/// </summary>
		/// <returns></returns>
		bool HasCarOnCurrentWay()
		{
			if (this.CurrentImperfectCar == null) return false;

			if (this.CurrentImperfectCar.PassWay == ePassWay.UnKnow)
				return false;
			else if (this.CurrentImperfectCar.PassWay == ePassWay.Way1)
				return this.InductorCoil1 || this.InductorCoil2;
			else if (this.CurrentImperfectCar.PassWay == ePassWay.Way2)
				return this.InductorCoil3 || this.InductorCoil4;

			return true;
		}

		/// <summary>
		/// 加载煤种
		/// </summary>
		void LoadFuelkind(ComboBoxEx[] comboBoxEx)
		{
			IList<CmcsFuelKind> FuelKindList = Dbers.GetInstance().SelfDber.Entities<CmcsFuelKind>("where Valid='有效' and ParentId is not null");
			for (int i = 0; i < comboBoxEx.Length; i++)
			{
				comboBoxEx[i].DisplayMember = "FuelName";
				comboBoxEx[i].ValueMember = "Id";
				comboBoxEx[i].DataSource = FuelKindList;
			}
		}

		/// <summary>
		/// 加载采样方式
		/// </summary>
		void LoadSampleType(ComboBoxEx[] comboBoxEx)
		{
			List<CMCS.Common.Entities.iEAA.CodeContent> contentList = commonDAO.GetCodeContentByKind("采样方式");
			for (int i = 0; i < comboBoxEx.Length; i++)
			{
				comboBoxEx[i].DisplayMember = "Content";
				comboBoxEx[i].ValueMember = "Code";
				comboBoxEx[i].DataSource = contentList;

				comboBoxEx[i].Text = eSamplingType.机械采样.ToString();
			}
		}

		/// <summary>
		/// 加载入厂煤类型
		/// </summary>
		void LoadBuyFuelType(ComboBoxEx[] comboBoxEx)
		{
			for (int i = 0; i < comboBoxEx.Length; i++)
			{
				comboBoxEx[i].Items.Add(new ComboBoxItem(eTransportType.原料煤入场.ToString(), eTransportType.原料煤入场.ToString()));
				comboBoxEx[i].Items.Add(new ComboBoxItem(eTransportType.仓储煤入场.ToString(), eTransportType.仓储煤入场.ToString()));
				comboBoxEx[i].Items.Add(new ComboBoxItem(eTransportType.中转煤入场.ToString(), eTransportType.中转煤入场.ToString()));
				comboBoxEx[i].SelectedIndex = -1;
			}
		}

		/// <summary>
		/// 加载出厂煤类型
		/// </summary>
		void LoadSaleType(ComboBoxEx[] comboBoxEx)
		{
			for (int i = 0; i < comboBoxEx.Length; i++)
			{
				comboBoxEx[i].Items.Add(new ComboBoxItem(eTransportType.仓储煤出场.ToString(), eTransportType.仓储煤出场.ToString()));
				comboBoxEx[i].Items.Add(new ComboBoxItem(eTransportType.中转煤出场.ToString(), eTransportType.中转煤出场.ToString()));
				comboBoxEx[i].Items.Add(new ComboBoxItem(eTransportType.销售直销煤.ToString(), eTransportType.销售直销煤.ToString()));
				comboBoxEx[i].Items.Add(new ComboBoxItem(eTransportType.销售掺配煤.ToString(), eTransportType.销售掺配煤.ToString()));
				comboBoxEx[i].SelectedIndex = -1;
			}
		}

		/// <summary>
		/// 加载成品仓
		/// </summary>
		void LoadCPC(ComboBoxEx[] comboBoxEx)
		{
			DataTable data = commonDAO.SelfDber.ExecuteDataTable("select Id,PotName from fultbcoalpot where PotName like '%成品仓%' order by PotName");
			if (data != null && data.Rows.Count > 0)
			{
				for (int i = 0; i < comboBoxEx.Length; i++)
				{
					comboBoxEx[i].Items.Clear();
					foreach (DataRow item in data.Rows)
					{
						comboBoxEx[i].Items.Add(new ComboBoxItem(item["Id"].ToString(), item["PotName"].ToString()));
					}
					comboBoxEx[i].SelectedIndex = -1;
				}
			}
		}

		/// <summary>
		/// 加载自定义数据
		/// </summary>
		void LoadSource(ComboBoxEx[] comboBoxEx, string dataSourceValue, string dataSourceText, char split = '|', char split2 = '|')
		{
			if (string.IsNullOrEmpty(dataSourceValue) || string.IsNullOrEmpty(dataSourceText))
			{
				for (int i = 0; i < comboBoxEx.Length; i++)
				{
					comboBoxEx[i].Items.Clear();
				}
				return;
			}
			string[] dataValue = dataSourceValue.Split(split);
			string[] dataText = dataSourceText.Split(split2);
			for (int i = 0; i < comboBoxEx.Length; i++)
			{
				comboBoxEx[i].Items.Clear();
				for (int j = 0; j < dataValue.Length; j++)
				{
					comboBoxEx[i].Items.Add(new ComboBoxItem(dataValue[j], dataText[j]));
				}
				comboBoxEx[i].SelectedIndex = 0;
			}
		}


		/// <summary>
		/// 执行远程命令
		/// </summary>
		void ExecAppRemoteControlCmd()
		{
			// 获取最新的命令
			CmcsAppRemoteControlCmd appRemoteControlCmd = commonDAO.GetNewestAppRemoteControlCmd(CommonAppConfig.GetInstance().AppIdentifier);
			if (appRemoteControlCmd != null)
			{
				if (appRemoteControlCmd.CmdCode == "控制道闸")
				{
					Log4Neter.Info("接收远程命令：" + appRemoteControlCmd.CmdCode + "，参数：" + appRemoteControlCmd.Param);

					if (appRemoteControlCmd.Param.Equals("Gate1Up", StringComparison.CurrentCultureIgnoreCase))
						this.iocControler.Gate1Up();
					else if (appRemoteControlCmd.Param.Equals("Gate1Down", StringComparison.CurrentCultureIgnoreCase))
						this.iocControler.Gate1Down();
					else if (appRemoteControlCmd.Param.Equals("Gate2Up", StringComparison.CurrentCultureIgnoreCase))
						this.iocControler.Gate2Up();
					else if (appRemoteControlCmd.Param.Equals("Gate2Down", StringComparison.CurrentCultureIgnoreCase))
						this.iocControler.Gate2Down();

					// 更新执行结果
					commonDAO.SetAppRemoteControlCmdResultCode(appRemoteControlCmd, eEquInfCmdResultCode.成功);
				}
			}
		}

		/// <summary>
		/// 道路选项卡切换
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void superTabControl_SelectedTabChanged(object sender, SuperTabStripSelectedTabChangedEventArgs e)
		{
			SuperTabControl tabControl = sender as SuperTabControl;
			if (tabControl.SelectedTab.Name.Contains("1"))
			{
				this.CurrentWay = ePassWay.Way1;
			}
			else if (tabControl.SelectedTab.Name.Contains("2"))
			{
				this.CurrentWay = ePassWay.Way2;
			}
		}

		#endregion

		#region 入场煤业务
		#region Vars1

		bool timer_BuyFuel_Cancel = true;

		private CmcsSupplier selectedSupplier_BuyFuel;
		/// <summary>
		/// 选择的供煤单位
		/// </summary>
		public CmcsSupplier SelectedSupplier_BuyFuel
		{
			get { return selectedSupplier_BuyFuel; }
			set
			{
				selectedSupplier_BuyFuel = value;

				if (value != null)
				{
					txtSupplierName_BuyFuel.Text = value.Name;
				}
				else
				{
					txtSupplierName_BuyFuel.ResetText();
				}
			}
		}

		private CmcsTransportCompany selectedTransportCompany_BuyFuel;
		/// <summary>
		/// 选择的运输单位
		/// </summary>
		public CmcsTransportCompany SelectedTransportCompany_BuyFuel
		{
			get { return selectedTransportCompany_BuyFuel; }
			set
			{
				selectedTransportCompany_BuyFuel = value;

				if (value != null)
				{
					txtTransportCompanyName_BuyFuel.Text = value.Name;
				}
				else
				{
					txtTransportCompanyName_BuyFuel.ResetText();
				}
			}
		}

		private CmcsMine selectedMine_BuyFuel;
		/// <summary>
		/// 选择的矿点
		/// </summary>
		public CmcsMine SelectedMine_BuyFuel
		{
			get { return selectedMine_BuyFuel; }
			set
			{
				selectedMine_BuyFuel = value;

				if (value != null)
				{
					txtMineName_BuyFuel.Text = value.Name;
				}
				else
				{
					txtMineName_BuyFuel.ResetText();
				}
			}
		}

		private CmcsFuelKind selectedFuelKind_BuyFuel;
		/// <summary>
		/// 选择的煤种
		/// </summary>
		public CmcsFuelKind SelectedFuelKind_BuyFuel
		{
			get { return selectedFuelKind_BuyFuel; }
			set
			{
				if (value != null)
				{
					selectedFuelKind_BuyFuel = value;
					cmbFuelName_BuyFuel.Text = value.FuelName;
				}
				else
				{
					cmbFuelName_BuyFuel.SelectedIndex = 0;
				}
			}
		}

		private CmcsLMYB selectedLMYB_BuyFuel;
		/// <summary>
		/// 选择的来煤预报
		/// </summary>
		public CmcsLMYB SelectedLMYB_BuyFuel
		{
			get { return selectedLMYB_BuyFuel; }
			set
			{
				selectedLMYB_BuyFuel = value;
				if (this.CurrentAutotruck != null) txtCarNumber_BuyFuel.Text = this.CurrentAutotruck.CarNumber;
				try
				{
					if (value != null)
					{
						this.SelectedFuelKind_BuyFuel = commonDAO.SelfDber.Get<CmcsFuelKind>(value.FuelKindId);
						this.SelectedMine_BuyFuel = commonDAO.SelfDber.Get<CmcsMine>(value.MineId);
						this.SelectedSupplier_BuyFuel = commonDAO.SelfDber.Get<CmcsSupplier>(value.SupplierId);
						this.SelectedTransportCompany_BuyFuel = commonDAO.SelfDber.Get<CmcsTransportCompany>(value.TransportCompanyId);
						this.cmbBuyFuelType.Text = value.InFactoryType;
						this.cmbSamplingType_BuyFuel.Text = value.SamplingType;
						this.txt_LMYB_BuyFuel.Text = value.YbNum;
					}
					else
					{
						this.txt_LMYB_BuyFuel.ResetText();
						//this.SelectedFuelKind_BuyFuel = null;
						//this.SelectedMine_BuyFuel = null;
						//this.SelectedSupplier_BuyFuel = null;
						//this.SelectedTransportCompany_BuyFuel = null;
					}
				}
				catch { }
			}
		}

		#endregion

		#region Vars2

		bool timer_BuyFuel_Cancel2 = true;

		private CmcsSupplier selectedSupplier_BuyFuel2;
		/// <summary>
		/// 选择的供煤单位
		/// </summary>
		public CmcsSupplier SelectedSupplier_BuyFuel2
		{
			get { return selectedSupplier_BuyFuel2; }
			set
			{
				selectedSupplier_BuyFuel2 = value;

				if (value != null)
				{
					txtSupplierName_BuyFuel2.Text = value.Name;
				}
				else
				{
					txtSupplierName_BuyFuel2.ResetText();
				}
			}
		}

		private CmcsTransportCompany selectedTransportCompany_BuyFuel2;
		/// <summary>
		/// 选择的运输单位
		/// </summary>
		public CmcsTransportCompany SelectedTransportCompany_BuyFuel2
		{
			get { return selectedTransportCompany_BuyFuel2; }
			set
			{
				selectedTransportCompany_BuyFuel2 = value;

				if (value != null)
				{
					txtTransportCompanyName_BuyFuel2.Text = value.Name;
				}
				else
				{
					txtTransportCompanyName_BuyFuel2.ResetText();
				}
			}
		}

		private CmcsMine selectedMine_BuyFuel2;
		/// <summary>
		/// 选择的矿点
		/// </summary>
		public CmcsMine SelectedMine_BuyFuel2
		{
			get { return selectedMine_BuyFuel2; }
			set
			{
				selectedMine_BuyFuel2 = value;

				if (value != null)
				{
					txtMineName_BuyFuel2.Text = value.Name;
				}
				else
				{
					txtMineName_BuyFuel2.ResetText();
				}
			}
		}

		private CmcsFuelKind selectedFuelKind_BuyFuel2;
		/// <summary>
		/// 选择的煤种
		/// </summary>
		public CmcsFuelKind SelectedFuelKind_BuyFuel2
		{
			get { return selectedFuelKind_BuyFuel2; }
			set
			{
				if (value != null)
				{
					selectedFuelKind_BuyFuel2 = value;
					cmbFuelName_BuyFuel2.Text = value.FuelName;
				}
				else
				{
					cmbFuelName_BuyFuel2.SelectedIndex = 0;
				}
			}
		}

		private CmcsLMYB selectedLMYB_BuyFuel2;
		/// <summary>
		/// 选择的来煤预报
		/// </summary>
		public CmcsLMYB SelectedLMYB_BuyFuel2
		{
			get { return selectedLMYB_BuyFuel2; }
			set
			{
				selectedLMYB_BuyFuel2 = value;
				if (this.CurrentAutotruck2 != null) txtCarNumber_BuyFuel2.Text = this.CurrentAutotruck2.CarNumber;
				try
				{
					if (value != null)
					{
						this.SelectedFuelKind_BuyFuel2 = commonDAO.SelfDber.Get<CmcsFuelKind>(value.FuelKindId);
						this.SelectedMine_BuyFuel2 = commonDAO.SelfDber.Get<CmcsMine>(value.MineId);
						this.SelectedSupplier_BuyFuel2 = commonDAO.SelfDber.Get<CmcsSupplier>(value.SupplierId);
						this.SelectedTransportCompany_BuyFuel2 = commonDAO.SelfDber.Get<CmcsTransportCompany>(value.TransportCompanyId);
						this.cmbBuyFuelType.Text = value.InFactoryType;
						this.cmbSamplingType_BuyFuel2.Text = value.SamplingType;
						this.txt_LMYB_BuyFuel2.Text = value.YbNum;
					}
					else
					{
						this.txt_LMYB_BuyFuel2.ResetText();
						//this.SelectedFuelKind_BuyFuel2 = null;
						//this.SelectedMine_BuyFuel2 = null;
						//this.SelectedSupplier_BuyFuel2 = null;
						//this.SelectedTransportCompany_BuyFuel2 = null;
					}
				}
				catch { }
			}
		}

		#endregion

		#region 选择事件

		/// <summary>
		/// 选择煤种
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cmbFuelName_BuyFuel_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (CurrentWay == ePassWay.Way1)
			{
				this.SelectedFuelKind_BuyFuel = cmbFuelName_BuyFuel.SelectedItem as CmcsFuelKind;
			}
			else if (CurrentWay == ePassWay.Way2)
			{
				this.SelectedFuelKind_BuyFuel2 = cmbFuelName_BuyFuel.SelectedItem as CmcsFuelKind;
			}
		}

		/// <summary>
		/// 选择车辆
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSelectAutotruck_BuyFuel_Click(object sender, EventArgs e)
		{
			FrmAutotruck_Select frm = new FrmAutotruck_Select(" and IsUse=1 order by CarNumber asc");
			if (frm.ShowDialog() == DialogResult.OK)
			{
				if (CurrentWay == ePassWay.Way1)
				{
					passCarQueuer.Enqueue(ePassWay.Way1, frm.Output.CarNumber, false);
					//无设备情况下 直接验证
					//this.CurrentAutotruck = carTransportDAO.GetAutotruckByCarNumber(frm.Output.CarNumber);
					this.timer_BuyFuel_Cancel = false;
					this.CurrentFlowFlag = eFlowFlag.验证车辆;
					timer1_Tick(null, null);
				}
				else if (CurrentWay == ePassWay.Way2)
				{
					passCarQueuer2.Enqueue(ePassWay.Way2, frm.Output.CarNumber, false);
					//无设备情况下 直接验证
					//this.CurrentAutotruck2 = carTransportDAO.GetAutotruckByCarNumber(frm.Output.CarNumber);
					this.timer_BuyFuel_Cancel = false;
					this.CurrentFlowFlag2 = eFlowFlag.验证车辆;
					timer2_Tick(null, null);
				}
			}
		}

		/// <summary>
		/// 选择供煤单位
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSelectSupplier_BuyFuel_Click(object sender, EventArgs e)
		{
			FrmSupplier_Select frm = new FrmSupplier_Select("where Valid='有效' order by Name asc");
			if (frm.ShowDialog() == DialogResult.OK)
			{
				if (CurrentWay == ePassWay.Way1)
				{
					this.SelectedSupplier_BuyFuel = frm.Output;
				}
				else if (CurrentWay == ePassWay.Way2)
				{
					this.SelectedSupplier_BuyFuel2 = frm.Output;
				}
			}
		}

		/// <summary>
		/// 选择矿点
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSelectMine_BuyFuel_Click(object sender, EventArgs e)
		{
			FrmMine_Select frm = new FrmMine_Select("where Valid='有效' order by Name asc");
			if (frm.ShowDialog() == DialogResult.OK)
			{
				if (CurrentWay == ePassWay.Way1)
				{
					this.SelectedMine_BuyFuel = frm.Output;
				}
				else if (CurrentWay == ePassWay.Way2)
				{
					this.SelectedMine_BuyFuel2 = frm.Output;
				}
			}
		}

		/// <summary>
		/// 选择运输单位
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSelectTransportCompany_BuyFuel_Click(object sender, EventArgs e)
		{
			FrmTransportCompany_Select frm = new FrmTransportCompany_Select("where IsUse=1 order by Name asc");
			if (frm.ShowDialog() == DialogResult.OK)
			{
				if (CurrentWay == ePassWay.Way1)
				{
					this.SelectedTransportCompany_BuyFuel = frm.Output;
				}
				else if (CurrentWay == ePassWay.Way2)
				{
					this.SelectedTransportCompany_BuyFuel2 = frm.Output;
				}
			}
		}

		/// <summary>
		/// 新车登记
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnNewAutotruck_BuyFuel_Click(object sender, EventArgs e)
		{
			new FrmAutotruck_Oper("", true).Show();
		}

		/// <summary>
		/// 选择入厂煤来煤预报
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSelectForecast_BuyFuel_Click(object sender, EventArgs e)
		{
			FrmBuyFuelForecast_Select frm = new FrmBuyFuelForecast_Select(string.Format("where (InFactoryType='{0}' or InFactoryType='{1}' or InFactoryType='{2}') ", eTransportType.原料煤入场, eTransportType.仓储煤入场, eTransportType.中转煤入场));
			if (frm.ShowDialog() == DialogResult.OK)
			{
				if (CurrentWay == ePassWay.Way1)
				{
					this.SelectedLMYB_BuyFuel = frm.Output;
				}
				else if (CurrentWay == ePassWay.Way2)
				{
					this.SelectedLMYB_BuyFuel2 = frm.Output;
				}
			}
		}

		#endregion

		#region 保存运输记录

		/// <summary>
		/// 保存入厂煤运输记录
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSaveTransport_BuyFuel_Click(object sender, EventArgs e)
		{
			if (this.CurrentWay == ePassWay.Way1)
				SaveBuyFuelTransport();
			else if (this.CurrentWay == ePassWay.Way2)
				SaveBuyFuelTransport2();
		}

		/// <summary>
		/// 保存运输记录
		/// </summary>
		/// <returns></returns>
		bool SaveBuyFuelTransport()
		{
			//if (IsUseYB && this.SelectedLMYB_BuyFuel == null)
			//{
			//    MessageBoxEx.Show("请选择预报信息", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			//    return false;
			//}

			if (this.CurrentAutotruck == null)
			{
				MessageBoxEx.Show("请选择车辆", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}
			if (queuerDAO.CheckHasUnFinishTransport(this.CurrentAutotruck))
			{
				MessageBoxEx.Show("存在未完成的运输记录", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}
			if (string.IsNullOrEmpty(this.cmbFuelName_BuyFuel.Text))
			{
				MessageBoxEx.Show("请选择煤种", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}
			if (this.SelectedMine_BuyFuel == null)
			{
				MessageBoxEx.Show("请选择矿点", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}
			if (this.SelectedSupplier_BuyFuel == null)
			{
				MessageBoxEx.Show("请选择供煤单位", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}
			if (this.SelectedTransportCompany_BuyFuel == null)
			{
				MessageBoxEx.Show("请选择运输单位", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}
			//if (txtTicketWeight_BuyFuel.Value <= 0)
			//{
			//    MessageBoxEx.Show("请输入有效的矿发量", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			//    return false;
			//}
			if (string.IsNullOrEmpty(cmbBuyFuelType.Text))
			{
				MessageBoxEx.Show("请选择入场煤类型", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}

			try
			{
				// 生成入厂煤排队记录，同时生成批次信息以及采制化三级编码
				if (queuerDAO.JoinQueueBuyFuelTransport(this.CurrentAutotruck, this.SelectedSupplier_BuyFuel, this.SelectedMine_BuyFuel, this.SelectedTransportCompany_BuyFuel, this.cmbFuelName_BuyFuel.SelectedItem as CmcsFuelKind, (decimal)txtTicketWeight_BuyFuel.Value, DateTime.Now, txtRemark_BuyFuel.Text, cmbSamplingType_BuyFuel.Text, this.SelectedLMYB_BuyFuel, cmbBuyFuelType.Text))
				{
					btnSaveTransport_BuyFuel.Enabled = false;
					this.timer_BuyFuel_Cancel = false;

					Log4Neter.Info(string.Format("车牌号:{0}排队成功", this.txtCarNumber_BuyFuel.Text));
					UpdateLedShow("排队成功", "请离开");
					MessageBoxEx.Show("排队成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

					LoadTodayUnFinishBuyFuelTransport();
					LoadTodayFinishBuyFuelTransport();

					LetPass();
					//ResetBuyFuel();
					return true;
				}
			}
			catch (Exception ex)
			{
				MessageBoxEx.Show("保存失败\r\n" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);

				Log4Neter.Error("保存运输记录", ex);
			}

			return false;
		}

		/// <summary>
		/// 保存运输记录
		/// </summary>
		/// <returns></returns>
		bool SaveBuyFuelTransport2()
		{
			if (this.CurrentAutotruck2 == null)
			{
				MessageBoxEx.Show("请选择车辆", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}
			if (queuerDAO.CheckHasUnFinishTransport(this.CurrentAutotruck))
			{
				MessageBoxEx.Show("存在未完成的运输记录", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}
			if (string.IsNullOrEmpty(this.cmbFuelName_BuyFuel2.Text))
			{
				MessageBoxEx.Show("请选择煤种", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}
			if (this.SelectedMine_BuyFuel2 == null)
			{
				MessageBoxEx.Show("请选择矿点", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}
			if (this.SelectedSupplier_BuyFuel2 == null)
			{
				MessageBoxEx.Show("请选择供煤单位", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}
			if (this.SelectedTransportCompany_BuyFuel2 == null)
			{
				MessageBoxEx.Show("请选择运输单位", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}
			//if (txtTicketWeight_BuyFuel2.Value <= 0)
			//{
			//    MessageBoxEx.Show("请输入有效的矿发量", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			//    return false;
			//}
			if (string.IsNullOrEmpty(cmbBuyFuelType2.Text))
			{
				MessageBoxEx.Show("请选择入厂煤类型", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}

			try
			{
				// 生成入厂煤排队记录，同时生成批次信息以及采制化三级编码
				if (queuerDAO.JoinQueueBuyFuelTransport(this.CurrentAutotruck2, this.SelectedSupplier_BuyFuel2, this.SelectedMine_BuyFuel2, this.SelectedTransportCompany_BuyFuel, this.cmbFuelName_BuyFuel2.SelectedItem as CmcsFuelKind, (decimal)txtTicketWeight_BuyFuel2.Value, DateTime.Now, txtRemark_BuyFuel2.Text, cmbSamplingType_BuyFuel2.Text, this.SelectedLMYB_BuyFuel2, cmbBuyFuelType2.Text))
				{
					btnSaveTransport_BuyFuel2.Enabled = false;
					this.timer_BuyFuel_Cancel2 = false;
					Log4Neter.Info(string.Format("车牌号:{0}排队成功", this.txtCarNumber_BuyFuel2.Text));
					UpdateLedShow("排队成功", "请离开");
					MessageBoxEx.Show("排队成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

					LoadTodayUnFinishBuyFuelTransport();
					LoadTodayFinishBuyFuelTransport();

					LetPass();
					//ResetBuyFuel();
					return true;
				}
			}
			catch (Exception ex)
			{
				MessageBoxEx.Show("保存失败\r\n" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);

				Log4Neter.Error("保存运输记录", ex);
			}

			return false;
		}

		#endregion

		/// <summary>
		/// 重置入厂煤运输记录
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnReset_BuyFuel_Click(object sender, EventArgs e)
		{
			ResetBuyFuel();
		}

		/// <summary>
		/// 重置信息
		/// </summary>
		void ResetBuyFuel()
		{
			if (this.CurrentWay == ePassWay.Way1)
			{
				this.timer_BuyFuel_Cancel = true;

				this.CurrentFlowFlag = eFlowFlag.等待车辆;

				this.CurrentAutotruck = null;
				this.SelectedLMYB_BuyFuel = null;
				//this.SelectedMine_BuyFuel = null;
				//this.SelectedSupplier_BuyFuel = null;
				//this.SelectedTransportCompany_BuyFuel = null;

				txtTicketWeight_BuyFuel.Value = 0;
				txtRemark_BuyFuel.ResetText();

				btnSaveTransport_BuyFuel.Enabled = true;
				// 最后重置
				this.CurrentImperfectCar = null;
			}
			else if (this.CurrentWay == ePassWay.Way2)
			{
				this.timer_BuyFuel_Cancel2 = true;

				this.CurrentFlowFlag2 = eFlowFlag.等待车辆;

				this.CurrentAutotruck2 = null;
				this.SelectedLMYB_BuyFuel2 = null;
				//this.SelectedMine_BuyFuel2 = null;
				//this.SelectedSupplier_BuyFuel2 = null;
				//this.SelectedTransportCompany_BuyFuel2 = null;

				txtTicketWeight_BuyFuel2.Value = 0;
				txtRemark_BuyFuel2.ResetText();

				btnSaveTransport_BuyFuel2.Enabled = true;
				// 最后重置
				this.CurrentImperfectCar2 = null;
			}

			LetBlocking();
			UpdateLedShow("  等待车辆");
		}

		/// <summary>
		/// 入厂煤运输记录业务定时器
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void timer_BuyFuel_Tick(object sender, EventArgs e)
		{
			if (this.timer_BuyFuel_Cancel) return;

			timer_BuyFuel.Stop();
			timer_BuyFuel.Interval = 2000;

			try
			{
				switch (this.CurrentFlowFlag)
				{
					//case eFlowFlag.匹配预报:
					//    #region

					//    List<CmcsLMYB> lMYBs = queuerDAO.GetBuyFuelForecastByCarNumber(this.CurrentAutotruck.CarNumber, DateTime.Now);
					//    if (lMYBs.Count > 1)
					//    {
					//        // 当来煤预报存在多条时，弹出选择确认框
					//        FrmBuyFuelForecast_Select frm = new FrmBuyFuelForecast_Select();
					//        frm.InLYMBS = lMYBs;
					//        if (frm.ShowDialog() == DialogResult.OK) BorrowForecast_BuyFuel(frm.Output);
					//    }
					//    else if (lMYBs.Count == 1)
					//    {
					//        BorrowForecast_BuyFuel(lMYBs[0]);
					//    }

					//    this.CurrentFlowFlag = eFlowFlag.数据录入;

					//    #endregion
					//    break;

					case eFlowFlag.数据录入:
						#region

						#endregion
						break;

					case eFlowFlag.等待离开:
						#region

						// 当前道路地感无信号时重置
						if (!HasCarOnCurrentWay()) ResetBuyFuel();

						// 降低灵敏度
						timer_BuyFuel.Interval = 4000;

						#endregion
						break;
				}

				// 当前道路地感无信号时重置
				if (!HasCarOnCurrentWay() && this.CurrentFlowFlag != eFlowFlag.等待车辆 && (this.CurrentImperfectCar != null && this.CurrentImperfectCar.IsFromDevice)) ResetBuyFuel();
			}
			catch (Exception ex)
			{
				Log4Neter.Error("timer_BuyFuel_Tick", ex);
			}
			finally
			{
				timer_BuyFuel.Start();
			}
		}


		/// <summary>
		/// 入厂煤运输记录业务定时器
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void timer_BuyFuel2_Tick(object sender, EventArgs e)
		{
			if (this.timer_BuyFuel_Cancel2) return;

			timer_BuyFuel2.Stop();
			timer_BuyFuel2.Interval = 2000;

			try
			{
				switch (this.CurrentFlowFlag2)
				{
					//case eFlowFlag.匹配预报:
					//    #region

					//    List<CmcsLMYB> lMYBs = queuerDAO.GetBuyFuelForecastByCarNumber(this.CurrentAutotruck.CarNumber, DateTime.Now);
					//    if (lMYBs.Count > 1)
					//    {
					//        // 当来煤预报存在多条时，弹出选择确认框
					//        FrmBuyFuelForecast_Select frm = new FrmBuyFuelForecast_Select();
					//        frm.InLYMBS = lMYBs;
					//        if (frm.ShowDialog() == DialogResult.OK) BorrowForecast_BuyFuel(frm.Output);
					//    }
					//    else if (lMYBs.Count == 1)
					//    {
					//        BorrowForecast_BuyFuel(lMYBs[0]);
					//    }

					//    this.CurrentFlowFlag = eFlowFlag.数据录入;

					//    #endregion
					//    break;

					case eFlowFlag.数据录入:
						#region

						#endregion
						break;

					case eFlowFlag.等待离开:
						#region

						// 当前道路地感无信号时重置
						if (!HasCarOnCurrentWay()) ResetBuyFuel();

						// 降低灵敏度
						timer_BuyFuel.Interval = 4000;

						#endregion
						break;
				}

				// 当前道路地感无信号时重置
				if (!HasCarOnCurrentWay() && this.CurrentFlowFlag2 != eFlowFlag.等待车辆 && (this.CurrentImperfectCar2 != null && this.CurrentImperfectCar2.IsFromDevice)) ResetBuyFuel();
			}
			catch (Exception ex)
			{
				Log4Neter.Error("timer_BuyFuel2_Tick", ex);
			}
			finally
			{
				timer_BuyFuel2.Start();
			}
		}

		/// <summary>
		/// 获取未完成的入厂煤记录
		/// </summary>
		void LoadTodayUnFinishBuyFuelTransport()
		{
			superGridControl1_BuyFuel.PrimaryGrid.DataSource = queuerDAO.GetUnFinishBuyFuelTransport();
		}

		/// <summary>
		/// 获取指定日期已完成的入厂煤记录
		/// </summary>
		void LoadTodayFinishBuyFuelTransport()
		{
			superGridControl2_BuyFuel.PrimaryGrid.DataSource = queuerDAO.GetFinishedBuyFuelTransport(DateTime.Now.Date, DateTime.Now.Date.AddDays(1));
		}

		void LoadTitleBuyFuelTransport()
		{
			List<CmcsBuyFuelTransport> tran = queuerDAO.GetBuyFuelTransportByDate(DateTime.Now.Date, DateTime.Now.Date.AddDays(1));
			txtTitle_BuyFuel.Text = string.Format("已登记总量：{0}   已称重：{1}  未称重：{2}   已称重未回皮：{3}   已回皮：{4}", tran.Count(), tran.Where(a => a.GrossWeight > 0).Count(), tran.Where(a => a.GrossWeight == 0).Count(), tran.Where(a => a.GrossWeight > 0 && a.TareWeight == 0).Count(), tran.Where(a => a.GrossWeight > 0 && a.TareWeight > 0).Count());
			txtTitle_BuyFuel2.Text = txtTitle_BuyFuel.Text;
		}

		/// <summary>
		/// 提取预报信息
		/// </summary>
		/// <param name="lMYB">来煤预报</param>
		void BorrowForecast_BuyFuel(CmcsLMYB lMYB)
		{
			if (lMYB == null) return;
			this.SelectedLMYB_BuyFuel = lMYB;
			this.SelectedFuelKind_BuyFuel = commonDAO.SelfDber.Get<CmcsFuelKind>(lMYB.FuelKindId);
			this.SelectedMine_BuyFuel = commonDAO.SelfDber.Get<CmcsMine>(lMYB.MineId);
			this.SelectedSupplier_BuyFuel = commonDAO.SelfDber.Get<CmcsSupplier>(lMYB.SupplierId);
			this.SelectedTransportCompany_BuyFuel = commonDAO.SelfDber.Get<CmcsTransportCompany>(lMYB.TransportCompanyId);
		}

		#region DataGridView

		/// <summary>
		/// 双击行时，自动填充供煤单位、矿点等信息
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void superGridControl_BuyFuel_CellDoubleClick(object sender, DevComponents.DotNetBar.SuperGrid.GridCellDoubleClickEventArgs e)
		{
			GridRow gridRow = (sender as SuperGridControl).PrimaryGrid.ActiveRow as GridRow;
			if (gridRow == null) return;

			View_BuyFuelTransport entity = (gridRow.DataItem as View_BuyFuelTransport);
			if (entity != null)
			{
				if (this.CurrentWay == ePassWay.Way1)
				{
					this.SelectedFuelKind_BuyFuel = commonDAO.SelfDber.Get<CmcsFuelKind>(entity.FuelKindId);
					this.SelectedMine_BuyFuel = commonDAO.SelfDber.Get<CmcsMine>(entity.MineId);
					this.SelectedSupplier_BuyFuel = commonDAO.SelfDber.Get<CmcsSupplier>(entity.SupplierId);
					this.SelectedTransportCompany_BuyFuel = commonDAO.SelfDber.Get<CmcsTransportCompany>(entity.TransportCompanyId);
					this.SelectedLMYB_BuyFuel = commonDAO.SelfDber.Entity<CmcsLMYB>("where YbNum=:YbNum order by Createdate desc", new { YbNum = entity.YbNum });
					cmbSamplingType_BuyFuel.Text = entity.SamplingType;
				}
				else if (this.CurrentWay == ePassWay.Way2)
				{
					this.SelectedFuelKind_BuyFuel2 = commonDAO.SelfDber.Get<CmcsFuelKind>(entity.FuelKindId);
					this.SelectedMine_BuyFuel2 = commonDAO.SelfDber.Get<CmcsMine>(entity.MineId);
					this.SelectedSupplier_BuyFuel2 = commonDAO.SelfDber.Get<CmcsSupplier>(entity.SupplierId);
					this.SelectedTransportCompany_BuyFuel2 = commonDAO.SelfDber.Get<CmcsTransportCompany>(entity.TransportCompanyId);
					this.SelectedLMYB_BuyFuel2 = commonDAO.SelfDber.Entity<CmcsLMYB>("where YbNum=:YbNum order by Createdate desc", new { YbNum = entity.YbNum });
					cmbSamplingType_BuyFuel2.Text = entity.SamplingType;
				}
			}
		}

		private void superGridControl1_BuyFuel_CellClick(object sender, GridCellClickEventArgs e)
		{
			View_BuyFuelTransport entity = e.GridCell.GridRow.DataItem as View_BuyFuelTransport;
			if (entity == null) return;

			// 更改有效状态
			if (e.GridCell.GridColumn.Name == "ChangeIsUse") queuerDAO.ChangeBuyFuelTransportToInvalid(entity.Id, Convert.ToBoolean(e.GridCell.Value));
		}

		private void superGridControl1_BuyFuel_DataBindingComplete(object sender, GridDataBindingCompleteEventArgs e)
		{
			foreach (GridRow gridRow in e.GridPanel.Rows)
			{
				View_BuyFuelTransport entity = gridRow.DataItem as View_BuyFuelTransport;
				if (entity == null) return;

				// 填充有效状态
				gridRow.Cells["ChangeIsUse"].Value = Convert.ToBoolean(entity.IsUse);
				if (entity.GrossWeight > 0 && entity.TareWeight > 0)
					gridRow.CellStyles.Default.TextColor = Color.Green;
				else if (entity.GrossWeight == 0 && entity.TareWeight == 0)
					gridRow.CellStyles.Default.TextColor = Color.Red;
			}
		}

		private void superGridControl2_BuyFuel_CellClick(object sender, GridCellClickEventArgs e)
		{
			View_BuyFuelTransport entity = e.GridCell.GridRow.DataItem as View_BuyFuelTransport;
			if (entity == null) return;

			// 更改有效状态
			if (e.GridCell.GridColumn.Name == "ChangeIsUse") queuerDAO.ChangeBuyFuelTransportToInvalid(entity.Id, Convert.ToBoolean(e.GridCell.Value));
		}

		private void superGridControl2_BuyFuel_DataBindingComplete(object sender, GridDataBindingCompleteEventArgs e)
		{
			foreach (GridRow gridRow in e.GridPanel.Rows)
			{
				View_BuyFuelTransport entity = gridRow.DataItem as View_BuyFuelTransport;
				if (entity == null) return;

				// 填充有效状态
				gridRow.Cells["ChangeIsUse"].Value = Convert.ToBoolean(entity.IsUse);
				if (entity.GrossWeight > 0 && entity.TareWeight > 0)
					gridRow.CellStyles.Default.TextColor = Color.Green;
				else if (entity.GrossWeight == 0 && entity.TareWeight == 0)
					gridRow.CellStyles.Default.TextColor = Color.Red;
			}
		}

		#endregion

		/// <summary>
		/// 打印磅单
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tsmiPrint_Click(object sender, EventArgs e)
		{
			GridRow gridRow = superGridControl2_BuyFuel.PrimaryGrid.ActiveRow as GridRow;
			if (gridRow == null) return;
			View_BuyFuelTransport entity = gridRow.DataItem as View_BuyFuelTransport;
			CmcsBuyFuelTransport entity2 = Dbers.GetInstance().SelfDber.Get<CmcsBuyFuelTransport>(entity.Id);
			FrmPrintWeb frm = new FrmPrintWeb(entity2);
			frm.ShowDialog();
		}


		#endregion

		#region 出场煤业务

		#region Var1
		bool timer_SaleFuel_Cancel = true;

		private CmcsLMYB selectedCmcsTransportSales;
		List<String> StorageNames = new List<string>();
		/// <summary>
		/// 选择的销售煤预报
		/// </summary>
		public CmcsLMYB SelectedCmcsTransportSales
		{
			get { return selectedCmcsTransportSales; }
			set
			{
				selectedCmcsTransportSales = value;

				if (value != null)
				{
					if (this.CurrentAutotruck != null) this.txtCarNumber_SaleFuel.Text = this.CurrentAutotruck.CarNumber;
					this.txt_YBNumber1.Text = value.YbNum;
					this.txt_TransportNo1.Text = value.TransportNo;
					this.txt_Consignee1.Text = value.SupplierName;
					this.SelectedReceive_SaleFuel = commonDAO.SelfDber.Get<CmcsSupplier>(value.SupplierId);
					this.SelectedTransportCompany_SaleFuel = Dbers.GetInstance().SelfDber.Get<CmcsTransportCompany>(value.TransportCompanyId);
					this.cmbFuelName_SaleFuel.Text = value.FuelKindName;
					this.cmbSalesType.Text = value.InFactoryType;
					this.cmbSamplingType_SaleFuel.Text = value.SamplingType;
					if (value.InFactoryType == eTransportType.销售直销煤.ToString() || value.InFactoryType == eTransportType.销售掺配煤.ToString() || value.InFactoryType == eTransportType.仓储煤出场.ToString())
					{
						LoadSource(new ComboBoxEx[] { cmb_CPC }, value.CPCId, value.CPCName);
						LoadSource(new ComboBoxEx[] { cmb_Storage }, value.StorageId, value.StorageName);
					}
					ChangeCPCVisible(value.InFactoryType);
				}
				else
				{
					this.txt_YBNumber1.ResetText();
					this.txt_TransportNo1.ResetText();
					this.txt_Consignee1.ResetText();
				}
			}
		}

		private CmcsSupplier selectedReceive_SaleFuel;
		/// <summary>
		/// 选择的收货单位
		/// </summary>
		public CmcsSupplier SelectedReceive_SaleFuel
		{
			get { return selectedReceive_SaleFuel; }
			set
			{
				selectedReceive_SaleFuel = value;

				if (value != null)
				{
					txt_Consignee1.Text = value.Name;
				}
				else
				{
					txt_Consignee1.ResetText();
				}
			}
		}

		private CmcsTransportCompany selectedTransportCompany_SaleFuel;
		/// <summary>
		/// 选择的运输单位
		/// </summary>
		public CmcsTransportCompany SelectedTransportCompany_SaleFuel
		{
			get { return selectedTransportCompany_SaleFuel; }
			set
			{
				selectedTransportCompany_SaleFuel = value;

				if (value != null)
				{
					txt_TransportCompayName1.Text = value.Name;
				}
				else
				{
					txt_TransportCompayName1.ResetText();
				}
			}
		}

		void ChangeCPCVisible(string outFactoryType)
		{
			if (outFactoryType == eTransportType.销售掺配煤.ToString() || outFactoryType == eTransportType.销售直销煤.ToString() || outFactoryType == eTransportType.仓储煤出场.ToString())
			{
				lab_CPC.Visible = true;
				lab_Storage.Visible = true;
				cmb_Storage.Visible = true;
				cmb_CPC.Visible = true;
			}
			else
			{
				lab_CPC.Visible = false;
				lab_Storage.Visible = false;
				cmb_Storage.Visible = false;
				cmb_CPC.Visible = false;
			}
		}
		#endregion

		#region Var2
		bool timer_SaleFuel_Cancel2 = true;

		private CmcsLMYB selectedCmcsTransportSales2;
		List<String> StorageNames2 = new List<string>();
		/// <summary>
		/// 选择的销售煤预报
		/// </summary>
		public CmcsLMYB SelectedCmcsTransportSales2
		{
			get { return selectedCmcsTransportSales2; }
			set
			{
				selectedCmcsTransportSales2 = value;

				if (value != null)
				{
					if (this.CurrentAutotruck2 != null) txtCarNumber_SaleFuel2.Text = this.CurrentAutotruck2.CarNumber;
					this.txt_YBNumber2.Text = value.YbNum;
					this.txt_TransportNo2.Text = "";
					this.txt_TransportNo2.Text = value.TransportNo;
					this.txt_Consignee2.Text = value.SupplierName;
					this.SelectedReceive_SaleFuel2 = commonDAO.SelfDber.Get<CmcsSupplier>(value.FuelSupplierId);
					this.SelectedTransportCompany_SaleFuel2 = Dbers.GetInstance().SelfDber.Get<CmcsTransportCompany>(value.TransportCompanyId);
					this.cmbFuelName_SaleFuel2.Text = value.FuelKindName;
					this.cmbSalesType2.Text = value.InFactoryType;
					this.cmbSamplingType_SaleFuel2.Text = value.SamplingType;
					if (value.InFactoryType == eTransportType.销售直销煤.ToString() || value.InFactoryType == eTransportType.销售掺配煤.ToString() || value.InFactoryType == eTransportType.仓储煤出场.ToString())
					{
						LoadSource(new ComboBoxEx[] { cmb_CPC2 }, value.CPCId, value.CPCName);
						LoadSource(new ComboBoxEx[] { cmb_Storage2 }, value.StorageId, value.StorageName, '|');
					}
					ChangeCPCVisible2(value.InFactoryType);
				}
				else
				{
					this.txt_YBNumber2.ResetText();
					this.txt_TransportNo2.ResetText();
					this.txt_Consignee2.ResetText();
					//this.SelectedTransportCompany_SaleFuel2 = null;
				}
			}
		}

		private CmcsSupplier selectedReceive_SaleFuel2;
		/// <summary>
		/// 选择的收货单位2
		/// </summary>
		public CmcsSupplier SelectedReceive_SaleFuel2
		{
			get { return selectedReceive_SaleFuel2; }
			set
			{
				selectedReceive_SaleFuel2 = value;

				if (value != null)
				{
					txt_Consignee2.Text = value.Name;
				}
				else
				{
					txt_Consignee2.ResetText();
				}
			}
		}

		private CmcsTransportCompany selectedTransportCompany_SaleFuel2;
		/// <summary>
		/// 选择的运输单位2
		/// </summary>
		public CmcsTransportCompany SelectedTransportCompany_SaleFuel2
		{
			get { return selectedTransportCompany_SaleFuel2; }
			set
			{
				selectedTransportCompany_SaleFuel2 = value;

				if (value != null)
				{
					txt_TransportCompayName2.Text = value.Name;
				}
				else
				{
					txt_TransportCompayName2.ResetText();
				}
			}
		}

		void ChangeCPCVisible2(string outFactoryType)
		{
			if (outFactoryType == eTransportType.销售掺配煤.ToString() || outFactoryType == eTransportType.销售直销煤.ToString() || outFactoryType == eTransportType.仓储煤出场.ToString())
			{
				lab_CPC2.Visible = true;
				lab_Storage2.Visible = true;
				cmb_Storage2.Visible = true;
				cmb_CPC2.Visible = true;
			}
			else
			{
				lab_CPC2.Visible = false;
				lab_Storage2.Visible = false;
				cmb_Storage2.Visible = false;
				cmb_CPC2.Visible = false;
			}
		}
		#endregion

		#region 事件

		private void cmbSalesType_SelectedIndexChanged(object sender, EventArgs e)
		{
			ChangeCPCVisible(cmbSalesType.Text);
		}

		private void cmbSalesType2_SelectedIndexChanged(object sender, EventArgs e)
		{
			ChangeCPCVisible2(cmbSalesType2.Text);
		}
		#endregion

		#region 公共方法

		/// <summary>
		/// 选择来煤预报
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSelectForecast_SaleFuel_Click(object sender, EventArgs e)
		{
			FrmSaleFuelForecast_Select frm = new FrmSaleFuelForecast_Select(string.Format(" where (InFactoryType='{0}' or InFactoryType='{1}' or InFactoryType='{2}' or InFactoryType='{3}')", eTransportType.仓储煤出场.ToString(), eTransportType.中转煤出场.ToString(), eTransportType.销售掺配煤.ToString(), eTransportType.销售直销煤.ToString()));
			if (frm.ShowDialog() == DialogResult.OK)
			{
				if (this.CurrentWay == ePassWay.Way1)
				{
					SelectedCmcsTransportSales = frm.Output;
				}
				else if (this.CurrentWay == ePassWay.Way2)
				{
					SelectedCmcsTransportSales2 = frm.Output;
				}
			}
		}

		/// <summary>
		/// 选择车辆
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSelectAutotruck_SaleFuel_Click(object sender, EventArgs e)
		{
			FrmAutotruck_Select frm = new FrmAutotruck_Select(" and IsUse=1 order by CarNumber asc");
			if (frm.ShowDialog() == DialogResult.OK)
			{
				if (this.CurrentWay == ePassWay.Way1)
				{
					passCarQueuer.Enqueue(ePassWay.Way1, frm.Output.CarNumber, false);
					this.timer_SaleFuel_Cancel = false;
					this.CurrentFlowFlag = eFlowFlag.验证车辆;
				}
				else if (this.CurrentWay == ePassWay.Way2)
				{
					passCarQueuer2.Enqueue(ePassWay.Way2, frm.Output.CarNumber, false);
					this.timer_SaleFuel_Cancel2 = false;
					this.CurrentFlowFlag2 = eFlowFlag.验证车辆;
				}
			}
		}

		/// <summary>
		/// 选择运输单位
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSelectTransportCompany_SaleFuel_Click(object sender, EventArgs e)
		{
			FrmTransportCompany_Select frm = new FrmTransportCompany_Select("where IsUse=1 order by Name asc");
			if (frm.ShowDialog() == DialogResult.OK)
			{
				if (this.CurrentWay == ePassWay.Way1)
				{
					this.SelectedTransportCompany_SaleFuel = frm.Output;
				}
				else if (this.CurrentWay == ePassWay.Way2)
				{
					this.SelectedTransportCompany_SaleFuel2 = frm.Output;
				}
			}
		}

		/// <summary>
		/// 选择收货单位
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSelectSupplyReceive_SaleFuel_Click(object sender, EventArgs e)
		{
			FrmSupplier_Select frm = new FrmSupplier_Select("where Valid='有效' order by Name asc");
			if (frm.ShowDialog() == DialogResult.OK)
			{
				if (this.CurrentWay == ePassWay.Way1)
				{
					this.SelectedReceive_SaleFuel = frm.Output;
				}
				else if (this.CurrentWay == ePassWay.Way2)
				{
					this.SelectedReceive_SaleFuel2 = frm.Output;
				}
			}
		}

		/// <summary>
		/// 新车登记
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnNewAutotruck_SaleFuel_Click(object sender, EventArgs e)
		{
			new FrmAutotruck_Oper("", true).Show();
		}

		/// <summary>
		/// 销售煤打印
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void toolStripMenuItem1_Click(object sender, EventArgs e)
		{
			GridRow gridRow = superGridControl2_SaleFuel.PrimaryGrid.ActiveRow as GridRow;
			if (gridRow == null) return;
			View_SaleFuelTransport entity = gridRow.DataItem as View_SaleFuelTransport;
			CmcsSaleFuelTransport entity2 = Dbers.GetInstance().SelfDber.Get<CmcsSaleFuelTransport>(entity.Id);
			FrmSaleFuelPrintWeb frm = new FrmSaleFuelPrintWeb(entity2);
			frm.ShowDialog();
		}

		/// <summary>
		/// 成品仓单选
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void CheckBox_CheckedChanged(object sender, EventArgs e)
		{
			if ((sender as CheckBoxX).Checked == true)
			{
				foreach (CheckBoxX chk in (sender as CheckBoxX).Parent.Controls)
				{
					if (chk != sender)
					{
						chk.Checked = false;
					}
				}
			}
		}

		/// <summary>
		/// 加载未完成运输记录
		/// </summary>
		void LoadTodayUnFinishSaleFuelTransport()
		{
			superGridControl1_SaleFuel.PrimaryGrid.DataSource = queuerDAO.GetUnFinishSaleFuelTransport();
		}

		/// <summary>
		/// 加载今日已完成运输记录
		/// </summary>
		void LoadTodayFinishSaleFuelTransport()
		{
			superGridControl2_SaleFuel.PrimaryGrid.DataSource = queuerDAO.GetFinishedSaleFuelTransport(DateTime.Now.Date, DateTime.Now.Date.AddDays(1));
		}

		#endregion

		#region 保存运输记录

		/// <summary>
		/// 登记
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSaveTransport_SaleFuel_Click(object sender, EventArgs e)
		{
			SaveSaleFuelTransport();
		}

		/// <summary>
		/// 保存运输记录
		/// </summary>
		/// <returns></returns>
		bool SaveSaleFuelTransport()
		{
			if (this.CurrentAutotruck == null)
			{
				MessageBoxEx.Show("请选择车辆", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}
			if (queuerDAO.CheckHasUnFinishTransport(this.CurrentAutotruck))
			{
				MessageBoxEx.Show("存在未完成的运输记录", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}
			if (this.SelectedTransportCompany_SaleFuel == null)
			{
				MessageBoxEx.Show("请选择运输单位", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}
			if (this.SelectedReceive_SaleFuel == null)
			{
				MessageBoxEx.Show("请选择收货单位", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}
			if (string.IsNullOrEmpty(this.cmbFuelName_SaleFuel.Text))
			{
				MessageBoxEx.Show("请选择煤种", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}
			//if (this.SelectedCmcsTransportSales == null)
			//{
			//    MessageBoxEx.Show("请选择销售煤订单", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			//    return false;
			//}
			//if (string.IsNullOrEmpty(cmbCPC.Text))
			//{
			//    MessageBoxEx.Show("请选择对应成品仓", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			//    return false;
			//}
			if (string.IsNullOrEmpty(cmbSalesType.Text))
			{
				MessageBoxEx.Show("请选择出场类型", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}
			try
			{
				ComboBoxItem storageItem = (ComboBoxItem)cmb_Storage.SelectedItem;
				ComboBoxItem cPCItem = (ComboBoxItem)cmb_CPC.SelectedItem;
				// 生成销售煤排队记录 同时生成批次信息
				if (queuerDAO.JoinQueueSaleFuelTransport(this.CurrentAutotruck, this.SelectedCmcsTransportSales, this.SelectedReceive_SaleFuel, this.SelectedTransportCompany_SaleFuel, (this.cmbFuelName_SaleFuel.SelectedItem as CmcsFuelKind), DateTime.Now, txt_ReMark1.Text, CommonAppConfig.GetInstance().AppIdentifier, cmb_CPC.Text, cmbSalesType.Text, cmbSamplingType_SaleFuel.Text, new Tuple<string, string>(cPCItem != null ? cPCItem.Name : "", cPCItem != null ? cPCItem.Text : ""), new Tuple<string, string>(storageItem != null ? storageItem.Name : "", storageItem != null ? storageItem.Text : "")))
				{
					btnSaveTransport_SaleFuel.Enabled = false;
					this.CurrentFlowFlag = eFlowFlag.等待离开;

					UpdateLedShow("排队成功", "请离开");
					MessageBoxEx.Show("排队成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

					LoadTodayUnFinishSaleFuelTransport();
					LoadTodayFinishSaleFuelTransport();

					LetPass();
					ResetSaleFuel();
					return true;
				}
			}
			catch (Exception ex)
			{
				MessageBoxEx.Show("保存失败\r\n" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);

				Log4Neter.Error("保存运输记录", ex);
			}

			return false;
		}

		/// <summary>
		/// 登记2
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSaveTransport_SaleFuel2_Click(object sender, EventArgs e)
		{
			SaveSaleFuelTransport2();
		}

		/// <summary>
		/// 保存运输记录2
		/// </summary>
		/// <returns></returns>
		bool SaveSaleFuelTransport2()
		{
			if (this.CurrentAutotruck2 == null)
			{
				MessageBoxEx.Show("请选择车辆", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}
			if (queuerDAO.CheckHasUnFinishTransport(this.CurrentAutotruck))
			{
				MessageBoxEx.Show("存在未完成的运输记录", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}
			if (this.SelectedTransportCompany_SaleFuel2 == null)
			{
				MessageBoxEx.Show("请选择运输单位", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}
			if (this.SelectedReceive_SaleFuel2 == null)
			{
				MessageBoxEx.Show("请选择收货单位", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}
			if (string.IsNullOrEmpty(this.cmbFuelName_SaleFuel2.Text))
			{
				MessageBoxEx.Show("请选择煤种", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}
			//if (this.SelectedCmcsTransportSales2 == null)
			//{
			//    MessageBoxEx.Show("请选择销售煤订单", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			//    return false;
			//}
			//if (!cbCoalProduct4.Checked && !cbCoalProduct5.Checked && !cbCoalProduct6.Checked)
			//{
			//    MessageBoxEx.Show("请选择对应成品仓", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			//    return false;
			//}
			if (string.IsNullOrEmpty(cmbSalesType2.Text))
			{
				MessageBoxEx.Show("请选择出场类型", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}
			try
			{
				ComboBoxItem cPCItem = (ComboBoxItem)cmb_CPC2.SelectedItem;
				ComboBoxItem storageItem = (ComboBoxItem)cmb_Storage2.SelectedItem;
				// 生成销售煤排队记录 同时生成批次信息
				if (queuerDAO.JoinQueueSaleFuelTransport(this.CurrentAutotruck2, this.SelectedCmcsTransportSales2, this.SelectedReceive_SaleFuel2, this.SelectedTransportCompany_SaleFuel2, (this.cmbFuelName_SaleFuel2.SelectedItem as CmcsFuelKind), DateTime.Now, txt_ReMark2.Text, CommonAppConfig.GetInstance().AppIdentifier, cmb_CPC.Text, cmbSalesType2.Text, cmbSamplingType_SaleFuel2.Text, new Tuple<string, string>(cPCItem != null ? cPCItem.Name : "", cPCItem != null ? cPCItem.Text : ""), new Tuple<string, string>(storageItem != null ? storageItem.Name : "", storageItem != null ? storageItem.Text : "")))
				{
					btnSaveTransport_SaleFuel2.Enabled = false;
					this.CurrentFlowFlag2 = eFlowFlag.等待离开;

					UpdateLedShow("排队成功", "请离开");
					MessageBoxEx.Show("排队成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

					LoadTodayUnFinishSaleFuelTransport();
					LoadTodayFinishSaleFuelTransport();

					LetPass();
					ResetSaleFuel2();
					return true;
				}
			}
			catch (Exception ex)
			{
				MessageBoxEx.Show("保存失败\r\n" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);

				Log4Neter.Error("保存运输记录", ex);
			}

			return false;
		}

		#endregion

		#region 流程主业务

		/// <summary>
		/// 销售煤定时任务
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void timer_SaleFuel_Tick(object sender, EventArgs e)
		{

			if (this.timer_SaleFuel_Cancel) return;

			timer_SaleFuel.Stop();
			timer_SaleFuel.Interval = 2000;

			try
			{
				switch (this.CurrentFlowFlag)
				{
					case eFlowFlag.数据录入:
						#region
						//if (this.CurrentAutotruck != null && this.SelectedCmcsTransportSales != null && this.SelectedReceive_SaleFuel != null && this.SelectedTransportCompany_SaleFuel != null)
						//{
						//    // 生成销售煤排队记录 同时生成批次信息
						//    if (queuerDAO.JoinQueueSaleFuelTransport(this.CurrentAutotruck, this.SelectedCmcsTransportSales, this.SelectedReceive_SaleFuel, this.SelectedTransportCompany_SaleFuel, (this.cmbFuelName_SaleFuel.SelectedItem as CmcsFuelKind), DateTime.Now, txt_ReMark1.Text, CommonAppConfig.GetInstance().AppIdentifier, ((cbCoalProduct1.Checked ? "#1|" : "|") + (cbCoalProduct2.Checked ? "#2|" : "|") + (cbCoalProduct3.Checked ? "#3|" : "|")).TrimEnd('|'), cmbSalesType.Text))
						//    {
						//        btnSaveTransport_BuyFuel.Enabled = false;
						//        this.CurrentFlowFlag = eFlowFlag.等待离开;

						//        LoadTodayUnFinishSaleFuelTransport();
						//        LoadTodayFinishSaleFuelTransport();

						//        LetPass();
						//        ResetSaleFuel();
						//        this.CurrentFlowFlag = eFlowFlag.等待离开;
						//    }
						//}
						#endregion
						break;

					case eFlowFlag.等待离开:
						#region
						// 当前道路地感无信号时重置
						if (!HasCarOnCurrentWay()) ResetSaleFuel();

						// 降低灵敏度
						timer_SaleFuel.Interval = 4000;

						#endregion
						break;
				}

				// 当前道路地感无信号时重置
				if (!HasCarOnCurrentWay() && this.CurrentFlowFlag != eFlowFlag.等待车辆 && (this.CurrentImperfectCar != null && this.CurrentImperfectCar.IsFromDevice)) ResetSaleFuel();
			}
			catch (Exception ex)
			{
				Log4Neter.Error("timer_SaleFuel_Tick", ex);
			}
			finally
			{
				timer_SaleFuel.Start();
			}
		}


		/// <summary>
		/// 销售煤定时任务2
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void timer_SaleFuel2_Tick(object sender, EventArgs e)
		{
			if (this.timer_SaleFuel_Cancel2) return;

			timer_SaleFuel2.Stop();
			timer_SaleFuel2.Interval = 2000;

			try
			{
				switch (this.CurrentFlowFlag2)
				{
					case eFlowFlag.数据录入:
						//#region
						//if (this.CurrentAutotruck2 != null && this.SelectedCmcsTransportSales2 != null && this.SelectedReceive_SaleFuel2 != null && this.SelectedTransportCompany_SaleFuel2 != null)
						//{
						//    // 生成销售煤排队记录 同时生成批次信息
						//    if (queuerDAO.JoinQueueSaleFuelTransport(this.CurrentAutotruck2, this.SelectedCmcsTransportSales2, this.SelectedReceive_SaleFuel2, this.SelectedTransportCompany_SaleFuel2, (this.cmbFuelName_SaleFuel.SelectedItem as CmcsFuelKind), DateTime.Now, txt_ReMark1.Text, CommonAppConfig.GetInstance().AppIdentifier, ((cbCoalProduct1.Checked ? "#1|" : "|") + (cbCoalProduct2.Checked ? "#2|" : "|") + (cbCoalProduct3.Checked ? "#3|" : "|")).TrimEnd('|'), cmbSalesType2.Text))
						//    {
						//        btnSaveTransport_BuyFuel.Enabled = false;
						//        this.CurrentFlowFlag2 = eFlowFlag.等待离开;

						//        LoadTodayUnFinishSaleFuelTransport();
						//        LoadTodayFinishSaleFuelTransport();

						//        LetPass();
						//        ResetSaleFuel();
						//        this.CurrentFlowFlag2 = eFlowFlag.等待离开;
						//    }
						//}
						//#endregion
						break;

					case eFlowFlag.等待离开:
						#region
						// 当前道路地感无信号时重置
						if (!HasCarOnCurrentWay()) ResetSaleFuel();

						// 降低灵敏度
						timer_SaleFuel2.Interval = 4000;

						#endregion
						break;
				}

				// 当前道路地感无信号时重置
				if (!HasCarOnCurrentWay() && this.CurrentFlowFlag2 != eFlowFlag.等待车辆 && (this.CurrentImperfectCar2 != null && this.CurrentImperfectCar2.IsFromDevice)) ResetSaleFuel();
			}
			catch (Exception ex)
			{
				Log4Neter.Error("timer_SaleFuel2_Tick", ex);
			}
			finally
			{
				timer_SaleFuel2.Start();
			}
		}

		#endregion

		#region 重置

		private void btnReset_SaleFuel_Click(object sender, EventArgs e)
		{
			ResetSaleFuel();
		}

		void ResetSaleFuel()
		{
			this.timer_SaleFuel_Cancel = true;

			this.CurrentFlowFlag = eFlowFlag.等待车辆;

			this.CurrentAutotruck = null;

			//this.SelectedCmcsTransportSales = null;

			txtRemark_BuyFuel.ResetText();

			btnSaveTransport_SaleFuel.Enabled = true;

			LetBlocking();
			UpdateLedShow("  等待车辆");

			// 最后重置
			this.CurrentImperfectCar = null;
			//LoadCPC(new ComboBoxEx[] { cmb_CPC });
			//cmb_Storage.Items.Clear();
		}

		private void btnReset_SaleFuel2_Click(object sender, EventArgs e)
		{
			ResetSaleFuel2();
		}

		void ResetSaleFuel2()
		{
			this.timer_SaleFuel_Cancel2 = true;

			this.CurrentFlowFlag2 = eFlowFlag.等待车辆;

			this.CurrentAutotruck2 = null;

			//this.SelectedCmcsTransportSales2 = null;

			txt_ReMark2.ResetText();
			btnSaveTransport_SaleFuel2.Enabled = true;

			LetBlocking();
			UpdateLedShow("  等待车辆");

			// 最后重置
			this.CurrentImperfectCar2 = null;
			LoadCPC(new ComboBoxEx[] { cmb_CPC2 });
			cmb_Storage2.Items.Clear();
		}

		#endregion

		#region DataGridView

		/// <summary>
		/// 双击行时，自动填充供煤单位、矿点等信息
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void superGridControl2_SaleFuel_CellDoubleClick(object sender, DevComponents.DotNetBar.SuperGrid.GridCellDoubleClickEventArgs e)
		{
			GridRow gridRow = (sender as SuperGridControl).PrimaryGrid.ActiveRow as GridRow;
			if (gridRow == null) return;

			View_SaleFuelTransport entity = (gridRow.DataItem as View_SaleFuelTransport);
			if (entity != null)
			{
				if (this.CurrentWay == ePassWay.Way1)
				{
					this.cmbFuelName_SaleFuel.Text = entity.FuelKindName;
					this.SelectedReceive_SaleFuel = commonDAO.SelfDber.Get<CmcsSupplier>(entity.SupplierId);
					this.SelectedTransportCompany_SaleFuel = commonDAO.SelfDber.Get<CmcsTransportCompany>(entity.TransportCompanyId);
					this.cmbSalesType.Text = entity.OutFactoryType;
					this.cmbFuelName_SaleFuel.Text = commonDAO.SelfDber.Get<CmcsFuelKind>(entity.FuelKindId).FuelName;
					this.SelectedCmcsTransportSales = commonDAO.SelfDber.Entity<CmcsLMYB>("where YbNum=:YbNum order by CreateDate desc", new { YbNum = entity.TransportSalesNum });
				}
				else if (this.CurrentWay == ePassWay.Way2)
				{
					this.cmbFuelName_SaleFuel2.SelectedItem = commonDAO.SelfDber.Get<CmcsFuelKind>(entity.FuelKindId);
					this.SelectedReceive_SaleFuel2 = commonDAO.SelfDber.Get<CmcsSupplier>(entity.SupplierId);
					this.SelectedTransportCompany_SaleFuel2 = commonDAO.SelfDber.Get<CmcsTransportCompany>(entity.TransportCompanyId);
					this.cmbSalesType2.Text = entity.OutFactoryType;
					this.cmbFuelName_SaleFuel2.Text = commonDAO.SelfDber.Get<CmcsFuelKind>(entity.FuelKindId).FuelName;
					this.SelectedCmcsTransportSales2 = commonDAO.SelfDber.Entity<CmcsLMYB>("where YbNum=:YbNum order by CreateDate desc", new { YbNum = entity.TransportSalesNum });
				}
			}
		}

		private void superGridControl1_SaleFuel_DataBindingComplete(object sender, GridDataBindingCompleteEventArgs e)
		{
			foreach (GridRow gridRow in e.GridPanel.Rows)
			{
				View_SaleFuelTransport entity = gridRow.DataItem as View_SaleFuelTransport;
				if (entity == null) return;

				// 填充有效状态
				gridRow.Cells["ChangeIsUse"].Value = Convert.ToBoolean(entity.IsUse);
				if (entity.TareWeight > 0 && entity.GrossWeight > 0)
					gridRow.CellStyles.Default.TextColor = Color.Green;
				else if (entity.TareWeight == 0 && entity.GrossWeight == 0)
					gridRow.CellStyles.Default.TextColor = Color.Red;
			}
		}

		private void superGridControl2_SaleFuel_DataBindingComplete(object sender, GridDataBindingCompleteEventArgs e)
		{

			foreach (GridRow gridRow in e.GridPanel.Rows)
			{
				View_SaleFuelTransport entity = gridRow.DataItem as View_SaleFuelTransport;
				if (entity == null) return;
				// 填充有效状态
				gridRow.Cells["ChangeIsUse"].Value = Convert.ToBoolean(entity.IsUse);
				if (entity.TareWeight > 0 && entity.GrossWeight > 0)
					gridRow.CellStyles.Default.TextColor = Color.Green;
				else if (entity.TareWeight == 0 && entity.GrossWeight == 0)
					gridRow.CellStyles.Default.TextColor = Color.Red;
			}
		}

		#endregion

		#endregion

		#region 其他物资业务

		#region Vars1

		bool timer_Goods_Cancel = true;
		private CmcsSupplyReceive selectedSupplyUnit_Goods;
		/// <summary>
		/// 选择的供货单位
		/// </summary>
		public CmcsSupplyReceive SelectedSupplyUnit_Goods
		{
			get { return selectedSupplyUnit_Goods; }
			set
			{
				selectedSupplyUnit_Goods = value;

				if (value != null)
				{
					txtSupplyUnitName_Goods.Text = value.UnitName;
				}
				else
				{
					txtSupplyUnitName_Goods.ResetText();
				}
			}
		}

		private CmcsSupplyReceive selectedReceiveUnit_Goods;
		/// <summary>
		/// 选择的收货单位
		/// </summary>
		public CmcsSupplyReceive SelectedReceiveUnit_Goods
		{
			get { return selectedReceiveUnit_Goods; }
			set
			{
				selectedReceiveUnit_Goods = value;

				if (value != null)
				{
					txtReceiveUnitName_Goods.Text = value.UnitName;
				}
				else
				{
					txtReceiveUnitName_Goods.ResetText();
				}
			}
		}

		private CmcsGoodsType selectedGoodsType_Goods;
		/// <summary>
		/// 选择的物资类型
		/// </summary>
		public CmcsGoodsType SelectedGoodsType_Goods
		{
			get { return selectedGoodsType_Goods; }
			set
			{
				selectedGoodsType_Goods = value;

				if (value != null)
				{
					txtGoodsTypeName_Goods.Text = value.GoodsName;
				}
				else
				{
					txtGoodsTypeName_Goods.ResetText();
				}
			}
		}

		#endregion

		#region Vars1

		bool timer_Goods_Cancel2 = true;
		private CmcsSupplyReceive selectedSupplyUnit_Goods2;
		/// <summary>
		/// 选择的供货单位
		/// </summary>
		public CmcsSupplyReceive SelectedSupplyUnit_Goods2
		{
			get { return selectedSupplyUnit_Goods2; }
			set
			{
				selectedSupplyUnit_Goods2 = value;

				if (value != null)
				{
					txtSupplyUnitName_Goods2.Text = value.UnitName;
				}
				else
				{
					txtSupplyUnitName_Goods2.ResetText();
				}
			}
		}

		private CmcsSupplyReceive selectedReceiveUnit_Goods2;
		/// <summary>
		/// 选择的收货单位
		/// </summary>
		public CmcsSupplyReceive SelectedReceiveUnit_Goods2
		{
			get { return selectedReceiveUnit_Goods2; }
			set
			{
				selectedReceiveUnit_Goods2 = value;

				if (value != null)
				{
					txtReceiveUnitName_Goods2.Text = value.UnitName;
				}
				else
				{
					txtReceiveUnitName_Goods2.ResetText();
				}
			}
		}

		private CmcsGoodsType selectedGoodsType_Goods2;
		/// <summary>
		/// 选择的物资类型
		/// </summary>
		public CmcsGoodsType SelectedGoodsType_Goods2
		{
			get { return selectedGoodsType_Goods2; }
			set
			{
				selectedGoodsType_Goods2 = value;

				if (value != null)
				{
					txtGoodsTypeName_Goods2.Text = value.GoodsName;
				}
				else
				{
					txtGoodsTypeName_Goods2.ResetText();
				}
			}
		}

		#endregion

		#region 事件

		/// <summary>
		/// 选择车辆
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSelectAutotruck_Goods_Click(object sender, EventArgs e)
		{
			FrmAutotruck_Select frm = new FrmAutotruck_Select(" IsUse=1 order by CarNumber asc");
			if (frm.ShowDialog() == DialogResult.OK)
			{
				if (this.CurrentWay == ePassWay.Way1)
				{
					passCarQueuer.Enqueue(ePassWay.Way1, frm.Output.CarNumber, false);
					//this.CurrentAutotruck = carTransportDAO.GetAutotruckByCarNumber(frm.Output.CarNumber);
					this.CurrentFlowFlag = eFlowFlag.验证车辆;
				}
				else if (this.CurrentWay == ePassWay.Way2)
				{
					passCarQueuer2.Enqueue(ePassWay.Way2, frm.Output.CarNumber, false);
					//this.CurrentAutotruck2 = carTransportDAO.GetAutotruckByCarNumber(frm.Output.CarNumber);
					this.CurrentFlowFlag2 = eFlowFlag.验证车辆;
				}
			}
		}

		/// <summary>
		/// 选择供货单位
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnbtnSelectSupply_Goods_Click(object sender, EventArgs e)
		{
			FrmSupplyReceive_Select frm = new FrmSupplyReceive_Select("where IsValid=1 order by UnitName asc");
			if (frm.ShowDialog() == DialogResult.OK)
			{
				if (this.CurrentWay == ePassWay.Way1)
				{
					this.SelectedSupplyUnit_Goods = frm.Output;
				}
				else if (this.CurrentWay == ePassWay.Way2)
				{
					this.SelectedSupplyUnit_Goods2 = frm.Output;
				}
			}
		}

		/// <summary>
		/// 选择收货单位
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSelectReceive_Goods_Click(object sender, EventArgs e)
		{
			FrmSupplyReceive_Select frm = new FrmSupplyReceive_Select("where IsValid=1 order by UnitName asc");
			if (frm.ShowDialog() == DialogResult.OK)
			{
				if (this.CurrentWay == ePassWay.Way1)
				{
					this.SelectedReceiveUnit_Goods = frm.Output;
				}
				else if (this.CurrentWay == ePassWay.Way2)
				{
					this.SelectedReceiveUnit_Goods2 = frm.Output;
				}
			}
		}

		/// <summary>
		/// 选择物资类型
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSelectGoodsType_Goods_Click(object sender, EventArgs e)
		{
			FrmGoodsType_Select frm = new FrmGoodsType_Select();
			if (frm.ShowDialog() == DialogResult.OK)
			{
				if (this.CurrentWay == ePassWay.Way1)
				{
					this.SelectedGoodsType_Goods = frm.Output;
				}
				else if (this.CurrentWay == ePassWay.Way2)
				{
					this.SelectedGoodsType_Goods2 = frm.Output;
				}
			}
		}

		/// <summary>
		/// 新车登记
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnNewAutotruck_Goods_Click(object sender, EventArgs e)
		{
			// eCarType.其他物资 

			new FrmAutotruck_Oper("", false, eCarType.其他物资.ToString()).Show();
		}

		#endregion

		/// <summary>
		/// 保存排队记录
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSaveTransport_Goods_Click(object sender, EventArgs e)
		{
			if (this.CurrentWay == ePassWay.Way1)
				SaveGoodsTransport();
			else if (this.CurrentWay == ePassWay.Way2)
				SaveGoodsTransport2();
		}

		/// <summary>
		/// 保存运输记录
		/// </summary>
		/// <returns></returns>
		bool SaveGoodsTransport()
		{
			if (this.CurrentAutotruck == null)
			{
				MessageBoxEx.Show("请选择车辆", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}
			if (this.SelectedSupplyUnit_Goods == null)
			{
				MessageBoxEx.Show("请选择供货单位", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}
			if (this.SelectedReceiveUnit_Goods == null)
			{
				MessageBoxEx.Show("请选择收货单位", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}
			if (this.SelectedGoodsType_Goods == null)
			{
				MessageBoxEx.Show("请选择物资类型", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}

			try
			{
				// 生成排队记录
				if (queuerDAO.JoinQueueGoodsTransport(this.CurrentAutotruck, this.SelectedSupplyUnit_Goods, this.SelectedReceiveUnit_Goods, this.SelectedGoodsType_Goods, DateTime.Now, txtRemark_Goods.Text, CommonAppConfig.GetInstance().AppIdentifier))
				{
					LetPass();

					btnSaveTransport_Goods.Enabled = false;
					this.CurrentFlowFlag = eFlowFlag.等待离开;

					UpdateLedShow("排队成功", "请离开");
					MessageBoxEx.Show("排队成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

					LoadTodayUnFinishGoodsTransport();
					LoadTodayFinishGoodsTransport();
					ResetGoods();
					return true;
				}
			}
			catch (Exception ex)
			{
				MessageBoxEx.Show("保存失败\r\n" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);

				Log4Neter.Error("保存运输记录", ex);
			}

			return false;
		}

		/// <summary>
		/// 保存运输记录2
		/// </summary>
		/// <returns></returns>
		bool SaveGoodsTransport2()
		{
			if (this.CurrentAutotruck2 == null)
			{
				MessageBoxEx.Show("请选择车辆", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}
			if (this.SelectedSupplyUnit_Goods2 == null)
			{
				MessageBoxEx.Show("请选择供货单位", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}
			if (this.SelectedReceiveUnit_Goods2 == null)
			{
				MessageBoxEx.Show("请选择收货单位", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}
			if (this.SelectedGoodsType_Goods2 == null)
			{
				MessageBoxEx.Show("请选择物资类型", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}

			try
			{
				// 生成排队记录
				if (queuerDAO.JoinQueueGoodsTransport(this.CurrentAutotruck2, this.SelectedSupplyUnit_Goods2, this.SelectedReceiveUnit_Goods2, this.SelectedGoodsType_Goods2, DateTime.Now, txtRemark_Goods2.Text, CommonAppConfig.GetInstance().AppIdentifier))
				{
					LetPass();

					btnSaveTransport_Goods2.Enabled = false;
					this.CurrentFlowFlag2 = eFlowFlag.等待离开;

					UpdateLedShow("排队成功", "请离开");
					MessageBoxEx.Show("排队成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

					LoadTodayUnFinishGoodsTransport();
					LoadTodayFinishGoodsTransport();
					ResetGoods2();
					return true;
				}
			}
			catch (Exception ex)
			{
				MessageBoxEx.Show("保存失败\r\n" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);

				Log4Neter.Error("保存运输记录", ex);
			}

			return false;
		}

		/// <summary>
		/// 重置信息
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnReset_Goods_Click(object sender, EventArgs e)
		{
			if (this.CurrentWay == ePassWay.Way1)
				ResetGoods();
			else if (this.CurrentWay == ePassWay.Way2)
				ResetGoods2();
		}

		/// <summary>
		/// 重置信息
		/// </summary>
		void ResetGoods()
		{
			this.timer_Goods_Cancel = true;

			this.CurrentFlowFlag = eFlowFlag.等待车辆;

			this.CurrentAutotruck = null;
			this.SelectedSupplyUnit_Goods = null;
			this.SelectedReceiveUnit_Goods = null;
			this.txtGoodsTypeName_Goods = null;

			txtRemark_Goods.ResetText();

			btnSaveTransport_Goods.Enabled = true;

			LetBlocking();
			UpdateLedShow("  等待车辆");

			// 最后重置
			this.CurrentImperfectCar = null;
		}

		/// <summary>
		/// 重置信息2
		/// </summary>
		void ResetGoods2()
		{
			this.timer_Goods_Cancel2 = true;

			this.CurrentFlowFlag2 = eFlowFlag.等待车辆;

			this.CurrentAutotruck2 = null;
			this.SelectedSupplyUnit_Goods2 = null;
			this.SelectedReceiveUnit_Goods2 = null;
			this.txtGoodsTypeName_Goods2 = null;

			txtRemark_Goods2.ResetText();

			btnSaveTransport_Goods2.Enabled = true;

			LetBlocking();
			UpdateLedShow("  等待车辆");

			// 最后重置
			this.CurrentImperfectCar2 = null;
		}

		/// <summary>
		/// 其他物资运输记录业务定时器
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void timer_Goods_Tick(object sender, EventArgs e)
		{
			timer_Goods.Stop();
			timer_Goods.Interval = 2000;

			try
			{
				switch (this.CurrentFlowFlag)
				{
					case eFlowFlag.数据录入:
						#region



						#endregion
						break;

					case eFlowFlag.等待离开:
						#region

						// 当前道路地感无信号时重置
						if (!HasCarOnCurrentWay()) ResetGoods();

						#endregion
						break;
				}

				// 当前道路地感无信号时重置
				if (!HasCarOnCurrentWay() && this.CurrentFlowFlag != eFlowFlag.等待车辆 && (this.CurrentImperfectCar != null && this.CurrentImperfectCar.IsFromDevice)) ResetGoods();
			}
			catch (Exception ex)
			{
				Log4Neter.Error("timer_Goods_Tick", ex);
			}
			finally
			{
				timer_Goods.Start();
			}
		}

		/// <summary>
		/// 其他物资运输记录业务定时器2
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void timer_Goods2_Tick(object sender, EventArgs e)
		{
			timer_Goods2.Stop();
			timer_Goods2.Interval = 2000;

			try
			{
				switch (this.CurrentFlowFlag2)
				{
					case eFlowFlag.数据录入:
						#region



						#endregion
						break;

					case eFlowFlag.等待离开:
						#region

						// 当前道路地感无信号时重置
						if (!HasCarOnCurrentWay()) ResetGoods2();

						#endregion
						break;
				}

				// 当前道路地感无信号时重置
				if (!HasCarOnCurrentWay() && this.CurrentFlowFlag2 != eFlowFlag.等待车辆 && (this.CurrentImperfectCar != null && this.CurrentImperfectCar.IsFromDevice)) ResetGoods();
			}
			catch (Exception ex)
			{
				Log4Neter.Error("timer_Goods_Tick", ex);
			}
			finally
			{
				timer_Goods.Start();
			}
		}

		/// <summary>
		/// 获取未完成的其他物资记录
		/// </summary>
		void LoadTodayUnFinishGoodsTransport()
		{
			superGridControl1_Goods.PrimaryGrid.DataSource = queuerDAO.GetUnFinishGoodsTransport();
		}

		/// <summary>
		/// 获取指定日期已完成的其他物资记录
		/// </summary>
		void LoadTodayFinishGoodsTransport()
		{
			superGridControl2_Goods.PrimaryGrid.DataSource = queuerDAO.GetFinishedGoodsTransport(DateTime.Now.Date, DateTime.Now.Date.AddDays(1));
		}

		#region DataGridView

		/// <summary>
		/// 双击行时，自动填充录入信息
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void superGridControl_Goods_CellDoubleClick(object sender, DevComponents.DotNetBar.SuperGrid.GridCellDoubleClickEventArgs e)
		{
			GridRow gridRow = (sender as SuperGridControl).PrimaryGrid.ActiveRow as GridRow;
			if (gridRow == null) return;

			CmcsGoodsTransport entity = (gridRow.DataItem as CmcsGoodsTransport);
			if (entity != null)
			{
				this.SelectedSupplyUnit_Goods = commonDAO.SelfDber.Get<CmcsSupplyReceive>(entity.SupplyUnitId);
				this.SelectedReceiveUnit_Goods = commonDAO.SelfDber.Get<CmcsSupplyReceive>(entity.ReceiveUnitId);
				this.SelectedGoodsType_Goods = commonDAO.SelfDber.Get<CmcsGoodsType>(entity.GoodsTypeId);
			}
		}

		private void superGridControl1_Goods_CellClick(object sender, GridCellClickEventArgs e)
		{
			CmcsGoodsTransport entity = e.GridCell.GridRow.DataItem as CmcsGoodsTransport;
			if (entity == null) return;

			// 更改有效状态
			if (e.GridCell.GridColumn.Name == "ChangeIsUse") queuerDAO.ChangeGoodsTransportToInvalid(entity.Id, Convert.ToBoolean(e.GridCell.Value));
		}

		private void superGridControl1_Goods_DataBindingComplete(object sender, GridDataBindingCompleteEventArgs e)
		{
			foreach (GridRow gridRow in e.GridPanel.Rows)
			{
				CmcsGoodsTransport entity = gridRow.DataItem as CmcsGoodsTransport;
				if (entity == null) return;

				// 填充有效状态
				gridRow.Cells["ChangeIsUse"].Value = Convert.ToBoolean(entity.IsUse);
				CmcsSupplyReceive supplyunit = Dbers.GetInstance().SelfDber.Get<CmcsSupplyReceive>(entity.SupplyUnitId);
				if (supplyunit != null)
				{
					gridRow.Cells["SupplyUnitName"].Value = supplyunit.UnitName;
				}
				CmcsSupplyReceive receiveunit = Dbers.GetInstance().SelfDber.Get<CmcsSupplyReceive>(entity.ReceiveUnitId);
				if (receiveunit != null)
				{
					gridRow.Cells["ReceiveUnitName"].Value = receiveunit.UnitName;
				}
				CmcsGoodsType goodstype = Dbers.GetInstance().SelfDber.Get<CmcsGoodsType>(entity.GoodsTypeId);
				if (goodstype != null)
				{
					gridRow.Cells["GoodsTypeName"].Value = goodstype.GoodsName;
				}
				if (entity.FirstWeight > 0 && entity.SecondWeight > 0)
					gridRow.CellStyles.Default.TextColor = Color.Green;
				else if (entity.FirstWeight == 0 && entity.SecondWeight == 0)
					gridRow.CellStyles.Default.TextColor = Color.Red;
			}
		}

		private void superGridControl2_Goods_CellClick(object sender, GridCellClickEventArgs e)
		{
			CmcsGoodsTransport entity = e.GridCell.GridRow.DataItem as CmcsGoodsTransport;
			if (entity == null) return;

			// 更改有效状态
			if (e.GridCell.GridColumn.Name == "ChangeIsUse") queuerDAO.ChangeGoodsTransportToInvalid(entity.Id, Convert.ToBoolean(e.GridCell.Value));
		}

		private void superGridControl2_Goods_DataBindingComplete(object sender, GridDataBindingCompleteEventArgs e)
		{
			foreach (GridRow gridRow in e.GridPanel.Rows)
			{
				CmcsGoodsTransport entity = gridRow.DataItem as CmcsGoodsTransport;
				if (entity == null) return;

				// 填充有效状态
				gridRow.Cells["ChangeIsUse"].Value = Convert.ToBoolean(entity.IsUse);

				CmcsSupplyReceive supplyunit = Dbers.GetInstance().SelfDber.Get<CmcsSupplyReceive>(entity.SupplyUnitId);
				if (supplyunit != null)
				{
					gridRow.Cells["SupplyUnitName"].Value = supplyunit.UnitName;
				}
				CmcsSupplyReceive receiveunit = Dbers.GetInstance().SelfDber.Get<CmcsSupplyReceive>(entity.ReceiveUnitId);
				if (receiveunit != null)
				{
					gridRow.Cells["ReceiveUnitName"].Value = receiveunit.UnitName;
				}
				CmcsGoodsType goodstype = Dbers.GetInstance().SelfDber.Get<CmcsGoodsType>(entity.GoodsTypeId);
				if (goodstype != null)
				{
					gridRow.Cells["GoodsTypeName"].Value = goodstype.GoodsName;
				}
				if (entity.FirstWeight > 0 && entity.SecondWeight > 0)
					gridRow.CellStyles.Default.TextColor = Color.Green;
				else if (entity.FirstWeight == 0 && entity.SecondWeight == 0)
					gridRow.CellStyles.Default.TextColor = Color.Red;
			}
		}

		#endregion

		#endregion

		#region 来访车辆业务

		bool timer_Visit_Cancel = true;

		/// <summary>
		/// 选择车辆
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSelectAutotruck_Visit_Click(object sender, EventArgs e)
		{
			FrmAutotruck_Select frm = new FrmAutotruck_Select("and CarType='" + eCarType.来访车辆.ToString() + "' and IsUse=1 order by CarNumber asc");
			if (frm.ShowDialog() == DialogResult.OK)
			{
				passCarQueuer.Enqueue(ePassWay.UnKnow, frm.Output.CarNumber, false);
				//this.CurrentAutotruck = carTransportDAO.GetAutotruckByCarNumber(frm.Output.CarNumber);
				this.CurrentFlowFlag = eFlowFlag.验证车辆;
			}
		}

		/// <summary>
		/// 新车登记
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnNewAutotruck_Visit_Click(object sender, EventArgs e)
		{
			new FrmAutotruck_Oper("", false, eCarType.来访车辆.ToString()).Show();
		}

		/// <summary>
		/// 保存排队记录
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSaveTransport_Visit_Click(object sender, EventArgs e)
		{
			SaveVisitTransport();
		}

		/// <summary>
		/// 保存运输记录
		/// </summary>
		/// <returns></returns>
		bool SaveVisitTransport()
		{
			if (this.CurrentAutotruck == null)
			{
				MessageBoxEx.Show("请选择车辆", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}

			try
			{
				// 生成入厂煤排队记录，同时生成批次信息以及采制化三级编码
				if (queuerDAO.JoinQueueVisitTransport(this.CurrentAutotruck, DateTime.Now, txtRemark_Visit.Text, CommonAppConfig.GetInstance().AppIdentifier))
				{
					LetPass();

					btnSaveTransport_Visit.Enabled = false;
					this.CurrentFlowFlag = eFlowFlag.等待离开;

					UpdateLedShow("排队成功", "请离开");
					MessageBoxEx.Show("排队成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

					LoadTodayUnFinishVisitTransport();
					LoadTodayFinishVisitTransport();
					ResetVisit();
					return true;
				}
			}
			catch (Exception ex)
			{
				MessageBoxEx.Show("保存失败\r\n" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);

				Log4Neter.Error("保存运输记录", ex);
			}

			return false;
		}

		/// <summary>
		/// 重置信息
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnReset_Visit_Click(object sender, EventArgs e)
		{
			ResetVisit();
		}

		/// <summary>
		/// 重置信息
		/// </summary>
		void ResetVisit()
		{
			this.timer_Visit_Cancel = true;

			this.CurrentFlowFlag = eFlowFlag.等待车辆;

			this.CurrentAutotruck = null;

			txtRemark_Visit.ResetText();

			btnSaveTransport_Visit.Enabled = true;

			LetBlocking();
			UpdateLedShow("  等待车辆");

			// 最后重置
			this.CurrentImperfectCar = null;
		}

		private void timer_Visit_Tick(object sender, EventArgs e)
		{
			if (this.timer_Visit_Cancel) return;

			timer_Visit.Stop();
			timer_Visit.Interval = 2000;

			try
			{
				switch (this.CurrentFlowFlag)
				{
					case eFlowFlag.数据录入:
						#region



						#endregion
						break;

					case eFlowFlag.等待离开:
						#region

						// 当前道路地感无信号时重置
						if (!HasCarOnCurrentWay()) ResetVisit();

						#endregion
						break;
				}

				// 当前道路地感无信号时重置
				if (!HasCarOnCurrentWay() && this.CurrentFlowFlag != eFlowFlag.等待车辆 && (this.CurrentImperfectCar != null && this.CurrentImperfectCar.IsFromDevice)) ResetVisit();
			}
			catch (Exception ex)
			{
				Log4Neter.Error("timer_Visit_Tick", ex);
			}
			finally
			{
				timer_Visit.Start();
			}
		}

		/// <summary>
		/// 获取未完成的来访车辆记录
		/// </summary>
		void LoadTodayUnFinishVisitTransport()
		{
			superGridControl1_Visit.PrimaryGrid.DataSource = queuerDAO.GetUnFinishVisitTransport();
		}

		/// <summary>
		/// 获取指定日期已完成的来访车辆记录
		/// </summary>
		void LoadTodayFinishVisitTransport()
		{
			superGridControl2_Visit.PrimaryGrid.DataSource = queuerDAO.GetFinishedVisitTransport(DateTime.Now.Date, DateTime.Now.Date.AddDays(1));
		}

		private void superGridControl1_Visit_CellClick(object sender, GridCellClickEventArgs e)
		{
			CmcsVisitTransport entity = e.GridCell.GridRow.DataItem as CmcsVisitTransport;
			if (entity == null) return;

			// 更改有效状态
			if (e.GridCell.GridColumn.Name == "ChangeIsUse") queuerDAO.ChangeVisitTransportToInvalid(entity.Id, Convert.ToBoolean(e.GridCell.Value));
		}

		private void superGridControl1_Visit_DataBindingComplete(object sender, GridDataBindingCompleteEventArgs e)
		{
			foreach (GridRow gridRow in e.GridPanel.Rows)
			{
				CmcsVisitTransport entity = gridRow.DataItem as CmcsVisitTransport;
				if (entity == null) return;

				// 填充有效状态
				gridRow.Cells["ChangeIsUse"].Value = Convert.ToBoolean(entity.IsUse);
			}
		}

		private void superGridControl2_Visit_CellClick(object sender, GridCellClickEventArgs e)
		{
			CmcsVisitTransport entity = e.GridCell.GridRow.DataItem as CmcsVisitTransport;
			if (entity == null) return;

			// 更改有效状态
			if (e.GridCell.GridColumn.Name == "ChangeIsUse") queuerDAO.ChangeVisitTransportToInvalid(entity.Id, Convert.ToBoolean(e.GridCell.Value));
		}

		private void superGridControl2_Visit_DataBindingComplete(object sender, GridDataBindingCompleteEventArgs e)
		{
			foreach (GridRow gridRow in e.GridPanel.Rows)
			{
				CmcsVisitTransport entity = gridRow.DataItem as CmcsVisitTransport;
				if (entity == null) return;

				// 填充有效状态
				gridRow.Cells["ChangeIsUse"].Value = Convert.ToBoolean(entity.IsUse);
			}
		}

		#endregion

		#region 其他函数

		Pen redPen3 = new Pen(Color.Red, 3);
		Pen greenPen3 = new Pen(Color.Lime, 3);
		Pen greenPen1 = new Pen(Color.Lime, 1);

		/// <summary>
		/// 当前车号面板绘制
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void panCurrentCarNumber_Paint(object sender, PaintEventArgs e)
		{
			PanelEx panel = sender as PanelEx;

			// 绘制地感1
			e.Graphics.DrawLine(this.InductorCoil1 ? redPen3 : greenPen3, 15, 10, 15, 55);
			// 绘制地感2                                                               
			e.Graphics.DrawLine(this.InductorCoil2 ? redPen3 : greenPen3, 25, 10, 25, 55);
			//// 绘制分割线
			//e.Graphics.DrawLine(greenPen1, 5, 34, 35, 34);
			//// 绘制地感3
			//e.Graphics.DrawLine(this.InductorCoil3 ? redPen3 : greenPen3, 15, 38, 15, 58);
			//// 绘制地感4                                                               
			//e.Graphics.DrawLine(this.InductorCoil4 ? redPen3 : greenPen3, 25, 38, 25, 58);
		}

		/// <summary>
		/// 当前车号面板绘制
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void panCurrentCarNumber2_Paint(object sender, PaintEventArgs e)
		{
			PanelEx panel = sender as PanelEx;

			//// 绘制地感1
			//e.Graphics.DrawLine(this.InductorCoil1 ? redPen3 : greenPen3, 15, 10, 15, 30);
			//// 绘制地感2                                                               
			//e.Graphics.DrawLine(this.InductorCoil2 ? redPen3 : greenPen3, 25, 10, 25, 30);
			//// 绘制分割线
			//e.Graphics.DrawLine(greenPen1, 5, 34, 35, 34);
			// 绘制地感3
			e.Graphics.DrawLine(this.InductorCoil3 ? redPen3 : greenPen3, 15, 10, 15, 55);
			// 绘制地感4                                                               
			e.Graphics.DrawLine(this.InductorCoil4 ? redPen3 : greenPen3, 25, 10, 25, 55);
		}

		private void superGridControl_BeginEdit(object sender, DevComponents.DotNetBar.SuperGrid.GridEditEventArgs e)
		{
			if (e.GridCell.GridColumn.DataPropertyName != "IsUse")
			{
				// 取消进入编辑
				e.Cancel = true;
			}
		}

		/// <summary>
		/// 设置行号
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void superGridControl_GetRowHeaderText(object sender, DevComponents.DotNetBar.SuperGrid.GridGetRowHeaderTextEventArgs e)
		{
			e.Text = (e.GridRow.RowIndex + 1).ToString();
		}

		/// <summary>
		/// Invoke封装
		/// </summary>
		/// <param name="action"></param>
		public void InvokeEx(Action action)
		{
			if (this.IsDisposed || !this.IsHandleCreated) return;

			this.Invoke(action);
		}

		#endregion
	}
}
