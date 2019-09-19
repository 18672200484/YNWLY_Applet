using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using CMCS.CarTransport.Weighter.Core;
using System.Threading;
using System.IO;
using CMCS.CarTransport.DAO;
using CMCS.Common.DAO;
using System.IO.Ports;
using CMCS.Common.Utilities;
using LED.YB14;
using CMCS.CarTransport.Weighter.Enums;
using CMCS.Common.Entities;
using CMCS.Common.Entities.CarTransport;
using CMCS.Common;
using CMCS.CarTransport.Weighter.Frms.Sys;
using DevComponents.DotNetBar.Controls;
using CMCS.Common.Views;
using DevComponents.DotNetBar.SuperGrid;
using CMCS.Common.Enums;
using CMCS.Common.Entities.Sys;
using HikVisionSDK.Core;
using CMCS.Common.Entities.BaseInfo;
using CMCS.Common.Entities.Fuel;

namespace CMCS.CarTransport.Weighter.Frms
{
    public partial class FrmWeighter : DevComponents.DotNetBar.Metro.MetroForm
    {
        /// <summary>
        /// 窗体唯一标识符
        /// </summary>
        public static string UniqueKey = "FrmWeighter";

        public FrmWeighter()
        {
            InitializeComponent();
        }

        #region 业务处理类
        CarTransportDAO carTransportDAO = CarTransportDAO.GetInstance();
        WeighterDAO weighterDAO = WeighterDAO.GetInstance();
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

                panCurrentWeight.Refresh();

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

                panCurrentWeight.Refresh();

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

                panCurrentWeight.Refresh();

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

                panCurrentWeight.Refresh();

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

        bool infraredSensor1 = false;
        /// <summary>
        /// 对射1状态 true=遮挡  false=连通
        /// </summary>
        public bool InfraredSensor1
        {
            get
            {
                return infraredSensor1;
            }
            set
            {
                infraredSensor1 = value;

                panCurrentWeight.Refresh();

                commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.对射1信号.ToString(), value ? "1" : "0");
            }
        }

        int infraredSensor1Port;
        /// <summary>
        /// 对射1端口
        /// </summary>
        public int InfraredSensor1Port
        {
            get { return infraredSensor1Port; }
            set { infraredSensor1Port = value; }
        }

        bool infraredSensor2 = false;
        /// <summary>
        /// 对射2状态 true=遮挡  false=连通
        /// </summary>
        public bool InfraredSensor2
        {
            get
            {
                return infraredSensor2;
            }
            set
            {
                infraredSensor2 = value;

                panCurrentWeight.Refresh();

                commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.对射2信号.ToString(), value ? "1" : "0");
            }
        }

        int infraredSensor2Port;
        /// <summary>
        /// 对射2端口
        /// </summary>
        public int InfraredSensor2Port
        {
            get { return infraredSensor2Port; }
            set { infraredSensor2Port = value; }
        }

        bool wbSteady = false;
        /// <summary>
        /// 地磅仪表稳定状态
        /// </summary>
        public bool WbSteady
        {
            get { return wbSteady; }
            set
            {
                wbSteady = value;

                this.panCurrentWeight.Style.ForeColor.Color = (value ? Color.Lime : Color.Red);

                panCurrentWeight.Refresh();

                commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.地磅仪表_稳定.ToString(), value ? "1" : "0");
            }
        }

        double wbMinWeight = 0;
        /// <summary>
        /// 地磅仪表最小称重 单位：吨
        /// </summary>
        public double WbMinWeight
        {
            get { return wbMinWeight; }
            set
            {
                wbMinWeight = value;
            }
        }

        #endregion

        #region 公共Vars

        /// <summary>
        /// 预警检测
        /// </summary>
        System.Timers.Timer timer_EarlyWarning = new System.Timers.Timer();

        /// <summary>
        /// 超时预警计数
        /// </summary>
        static int OverTime_EarlyWarningCount = 0;

        /// <summary>
        /// 重置计数
        /// </summary>
        static int ResetCount = 0;

        /// <summary>
        /// 等待上传的抓拍
        /// </summary>
        Queue<string> waitForUpload = new Queue<string>();

        bool autoHandMode = true;
        /// <summary>
        /// 自动模式=true  手动模式=false
        /// </summary>
        public bool AutoHandMode
        {
            get { return autoHandMode; }
            set
            {
                autoHandMode = value;

                btnSelectAutotruck_BuyFuel.Visible = !value;
                btnSelectAutotruck_SaleFuel.Visible = !value;
                btnSelectAutotruck_Goods.Visible = !value;

                btnSaveTransport_BuyFuel.Visible = !value;
                btnSaveTransport_SaleFuel.Visible = !value;
                btnSaveTransport_Goods.Visible = !value;

                btnReset_BuyFuel.Visible = !value;
                btnReset_SaleFuel.Visible = !value;
                btnReset_Goods.Visible = !value;
            }
        }

        private string carnumber;
        public string CarNumber
        {
            get { return carnumber; }
            set
            {
                carnumber = value;
            }
        }

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
                    //timer_EarlyWarning.Enabled = true;
                }
                else
                {
                    panCurrentCarNumber.Text = "等待车辆";
                    //重置后 结束预警检测
                    //timer_EarlyWarning.Enabled = false;
                    //commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.超时预警.ToString(), "0");
                    //OverTime_EarlyWarningCount = 0;
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
                }
                else
                {
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.当前车Id.ToString(), string.Empty);
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.当前车号.ToString(), string.Empty);

                    txtCarNumber_BuyFuel.ResetText();
                    txtCarNumber_SaleFuel.ResetText();
                    txtCarNumber_Goods.ResetText();

                    panCurrentCarNumber.ResetText();
                }
            }
        }


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
                    }
                    else if (value.CarType == eTransportType.仓储煤出场.ToString() || value.CarType == eTransportType.中转煤出场.ToString() || value.CarType == eTransportType.销售掺配煤.ToString() || value.CarType == eTransportType.销售直销煤.ToString())
                    {
                        txtCarNumber_SaleFuel.Text = this.CurrentAutotruck.CarNumber;
                        superTabControl2.SelectedTab = superTabItem_SaleFuel;
                    }
                    else if (value.CarType == eTransportType.其他物资.ToString())
                    {
                        txtCarNumber_Goods.Text = this.CurrentAutotruck.CarNumber;
                        superTabControl2.SelectedTab = superTabItem_Goods;
                    }
                }
            }
        }

        #endregion

        /// <summary>
        /// 窗体初始化
        /// </summary>
        private void InitForm()
        {
#if DEBUG
            lblFlowFlag.Visible = true;
            FrmDebugConsole.GetInstance(this).Show();
#else
            //lblFlowFlag.Visible = false;
#endif
            if (commonDAO.GetAppletConfigString("双向磅") != "1")
            {
                slightLED2.Visible = false;
                labelX7.Visible = false;
                slightRwer2.Visible = false;
                labelX4.Visible = false;
                panVideo2.Visible = false;
            }
            string inFactoryType = commonDAO.GetAppletConfigString("入厂类型");
            if (string.IsNullOrEmpty(inFactoryType))
            {
                if (inFactoryType == "入厂煤")
                    this.superTabControl2.SelectedTab = superTabItem_BuyFuel;
                else if (inFactoryType == "销售煤")
                    this.superTabControl2.SelectedTab = superTabItem_SaleFuel;
            }
            // 默认自动
            sbtnChangeAutoHandMode.Value = true;

            // 重置程序远程控制命令
            commonDAO.ResetAppRemoteControlCmd(CommonAppConfig.GetInstance().AppIdentifier);

            //timer_EarlyWarning.Interval = 1000;
            //timer_EarlyWarning.Elapsed += timer_EarlyWarning_Elapsed;
        }

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

        private void FrmWeighter_Load(object sender, EventArgs e)
        {
        }

        private void FrmWeighter_Shown(object sender, EventArgs e)
        {
            InitHardware();

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
            // 接收设备状态 
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
                this.InfraredSensor1 = (receiveValue[this.InfraredSensor1Port - 1] == 1);
                this.InfraredSensor2 = (receiveValue[this.InfraredSensor2Port - 1] == 1);
            });
        }

        /// <summary>
        /// 前方升杆
        /// </summary>
        void FrontGateUp()
        {
            if (this.CurrentImperfectCar == null) return;

            if (this.CurrentImperfectCar.PassWay == eDirection.Way1)
            {
                this.iocControler.Gate2Up();
                this.iocControler.GreenLight2();
            }
            else if (this.CurrentImperfectCar.PassWay == eDirection.Way2)
            {
                this.iocControler.Gate1Up();
                this.iocControler.GreenLight1();
            }
        }

        /// <summary>
        /// 前方降杆
        /// </summary>
        void FrontGateDown()
        {
            if (this.CurrentImperfectCar == null) return;

            if (this.CurrentImperfectCar.PassWay == eDirection.Way1 && !HasCarOnLeaveWay())
            {
                this.iocControler.Gate2Down();
                this.iocControler.RedLight2();
            }
            else if (this.CurrentImperfectCar.PassWay == eDirection.Way2 && !HasCarOnLeaveWay())
            {
                this.iocControler.Gate1Down();
                this.iocControler.RedLight1();
            }
        }

        /// <summary>
        /// 后方升杆
        /// </summary>
        void BackGateUp()
        {
            if (this.CurrentImperfectCar == null) return;

            if (this.CurrentImperfectCar.PassWay == eDirection.Way1)
            {
                this.iocControler.Gate1Up();
                this.iocControler.GreenLight1();
                Log4Neter.Info(string.Format("车牌号：{0} 方向1升道闸", this.CurrentAutotruck != null ? this.CurrentAutotruck.CarNumber : "无车牌"));
            }
            else if (this.CurrentImperfectCar.PassWay == eDirection.Way2)
            {
                this.iocControler.Gate2Up();
                this.iocControler.GreenLight2();
                Log4Neter.Info(string.Format("车牌号：{0} 方向2升道闸", this.CurrentAutotruck != null ? this.CurrentAutotruck.CarNumber : "无车牌"));
            }
        }

        /// <summary>
        /// 后方降杆
        /// </summary>
        void BackGateDown()
        {
            if (this.CurrentImperfectCar == null) return;

            if (this.CurrentImperfectCar.PassWay == eDirection.Way1 && !HasCarOnEnterWay())
            {
                this.iocControler.Gate1Down();
                this.iocControler.RedLight1();
                Log4Neter.Info(string.Format("车牌号：{0} 方向1降道闸", this.CurrentAutotruck != null ? this.CurrentAutotruck.CarNumber : "无车牌"));
            }
            else if (this.CurrentImperfectCar.PassWay == eDirection.Way2 && !HasCarOnEnterWay())
            {
                this.iocControler.Gate2Down();
                this.iocControler.RedLight2();
                Log4Neter.Info(string.Format("车牌号：{0} 方向2将道闸", this.CurrentAutotruck != null ? this.CurrentAutotruck.CarNumber : "无车牌"));
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
            // 接收设备状态 
            InvokeEx(() =>
            {
                slightRwer1.LightColor = (status ? Color.Green : Color.Red);

                commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.车号识别1_连接状态.ToString(), status ? "1" : "0");
            });
        }

        public void Rwer1_OnReadSuccess(string carnumber)
        {
            InvokeEx(() =>
            {
                if (carnumber != "无车牌" && this.CurrentFlowFlag == eFlowFlag.等待车辆)
                {
                    this.CarNumber = carnumber;
                    passCarQueuer.Enqueue(eDirection.Way1, CarNumber);
                    this.CurrentFlowFlag = eFlowFlag.识别车辆;
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.上磅方向.ToString(), "1");
                    timer1_Tick(null, null);
                    UpdateLedShow(carnumber);
                    Log4Neter.Info(string.Format("车号识别1识别到车号：{0}", carnumber));
                }
            });
        }

        void Rwer2_OnScanError(Exception ex)
        {
            Log4Neter.Error("车号识别2", ex);
        }

        void Rwer2_OnStatusChange(bool status)
        {
            // 接收设备状态 
            InvokeEx(() =>
            {
                slightRwer2.LightColor = (status ? Color.Green : Color.Red);

                commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.车号识别2_连接状态.ToString(), status ? "1" : "0");
            });
        }
        public void Rwer2_OnReadSuccess(string carnumber)
        {
            InvokeEx(() =>
             {
                 if (carnumber != "无车牌" && this.CurrentFlowFlag == eFlowFlag.等待车辆)
                 {
                     this.CarNumber = carnumber;
                     passCarQueuer.Enqueue(eDirection.Way2, CarNumber);
                     this.CurrentFlowFlag = eFlowFlag.识别车辆;
                     commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.上磅方向.ToString(), "0");
                     timer1_Tick(null, null);
                     UpdateLedShow(carnumber);
                     Log4Neter.Info(string.Format("车号识别2识别到车号：{0}", carnumber));
                 }
             });
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
            UpdateLed1Show(value1, value2);
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
             FrmDebugConsole.GetInstance(this).Output("更新LED1:|" + value1 + "|" + value2 + "|");
#else

#endif
            try
            {

                if (!this.LED1ConnectStatus) return;
                if (this.LED1PrevLedFileContent == value1 + value2) return;

                string ledContent = GenerateFillLedContent12(value1) + GenerateFillLedContent12(value2);

                File.WriteAllText(this.LED1TempFile, ledContent, Encoding.UTF8);

                if (LED1m_bSendBusy == false)
                {
                    LED1m_bSendBusy = true;

                    int nResult = YB14DynamicAreaLeder.SendDynamicAreaInfoCommand(this.LED1nScreenNo, this.LED1DYArea_ID);
                    if (nResult == YB14DynamicAreaLeder.RETURN_NOERROR)
                    {
                        // 初始化成功
                        this.LED1ConnectStatus = true;
                    }
                    else
                    {
                        this.LED1ConnectStatus = false;
                    }
                    if (nResult != YB14DynamicAreaLeder.RETURN_NOERROR) Log4Neter.Error("更新LED动态区域", new Exception(YB14DynamicAreaLeder.GetErrorMessage("SendDynamicAreaInfoCommand", nResult)));

                    LED1m_bSendBusy = false;
                }

                this.LED1PrevLedFileContent = value1 + value2;
            }
            catch (Exception ex)
            {
                Log4Neter.Error("发送LED信息", ex);
            }
        }

        #endregion

        #region LED2控制卡

        /// <summary>
        /// LED2控制卡屏号
        /// </summary>
        int LED2nScreenNo = 2;
        /// <summary>
        /// LED2动态区域号
        /// </summary>
        int LED2DYArea_ID = 2;
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
           FrmDebugConsole.GetInstance(this).Output("更新LED2:|" + value1 + "|" + value2 + "|");
#else

#endif
            if (!this.LED2ConnectStatus) return;
            if (this.LED2PrevLedFileContent == value1 + value2) return;

            string ledContent = GenerateFillLedContent12(value1) + GenerateFillLedContent12(value2);

            File.WriteAllText(this.LED2TempFile, ledContent, Encoding.UTF8);

            if (LED2m_bSendBusy == false)
            {
                LED2m_bSendBusy = true;
                int nResult = YB14DynamicAreaLeder.SendDynamicAreaInfoCommand(this.LED2nScreenNo, this.LED2DYArea_ID);
                if (nResult != YB14DynamicAreaLeder.RETURN_NOERROR) Log4Neter.Error("更新LED动态区域", new Exception(YB14DynamicAreaLeder.GetErrorMessage("SendDynamicAreaInfoCommand", nResult)));

                LED2m_bSendBusy = false;
            }

            this.LED2PrevLedFileContent = value1 + value2;
        }

        #endregion

        #endregion

        #region 地磅仪表

        /// <summary>
        /// 重量稳定事件
        /// </summary>
        /// <param name="steady"></param>
        void Wber_OnSteadyChange(bool steady)
        {
            InvokeEx(() =>
              {
                  this.WbSteady = steady;
              });
        }

        /// <summary>
        /// 地磅仪表状态变化
        /// </summary>
        /// <param name="status"></param>
        void Wber_OnStatusChange(bool status)
        {
            // 接收设备状态 
            InvokeEx(() =>
            {
                slightWber.LightColor = (status ? Color.Green : Color.Red);

                commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.地磅仪表_连接状态.ToString(), status ? "1" : "0");
            });
        }

        void Wber_OnWeightChange(double weight)
        {
            InvokeEx(() =>
            {
                panCurrentWeight.Text = weight.ToString();
            });
        }

        #endregion

        #region 海康视频

        /// <summary>
        /// 海康网络摄像机
        /// </summary>
        IPCer iPCer1 = new IPCer();
        IPCer iPCer2 = new IPCer();

        /// <summary>
        /// 执行摄像头抓拍，并保存数据
        /// </summary>
        /// <param name="transportId">运输记录Id</param>
        private void CamareCapturePicture(string transportId)
        {
            try
            {
                // 抓拍照片服务器发布地址
                string pictureWebUrl = commonDAO.GetCommonAppletConfigString("汽车智能化_抓拍照片发布路径");

                // 摄像机1
                string picture1FileName = Path.Combine(SelfVars.CapturePicturePath, Guid.NewGuid().ToString() + ".bmp");
                if (iPCer1.CapturePicture(picture1FileName))
                {
                    CmcsTransportPicture transportPicture = new CmcsTransportPicture()
                    {
                        CaptureTime = DateTime.Now,
                        CaptureType = CommonAppConfig.GetInstance().AppIdentifier,
                        TransportId = transportId,
                        PicturePath = pictureWebUrl + Path.GetFileName(picture1FileName)
                    };

                    if (commonDAO.SelfDber.Insert(transportPicture) > 0) waitForUpload.Enqueue(picture1FileName);
                }

                // 摄像机2
                string picture2FileName = Path.Combine(SelfVars.CapturePicturePath, "Camera", Guid.NewGuid().ToString() + ".bmp");
                if (iPCer2.CapturePicture(picture2FileName))
                {
                    CmcsTransportPicture transportPicture = new CmcsTransportPicture()
                    {
                        CaptureTime = DateTime.Now,
                        CaptureType = CommonAppConfig.GetInstance().AppIdentifier,
                        TransportId = transportId,
                        PicturePath = pictureWebUrl + Path.GetFileName(picture1FileName)
                    };

                    if (commonDAO.SelfDber.Insert(transportPicture) > 0) waitForUpload.Enqueue(picture2FileName);
                }
            }
            catch (Exception ex)
            {
                Log4Neter.Error("摄像机抓拍", ex);
            }
        }

        /// <summary>
        /// 上传抓拍照片到服务器共享文件夹
        /// </summary>
        private void UploadCapturePicture()
        {
            string serverPath = commonDAO.GetCommonAppletConfigString("汽车智能化_抓拍照片服务器共享路径");
            if (string.IsNullOrEmpty(serverPath) || this.waitForUpload == null || this.waitForUpload.Count == 0) return;
            
            string fileName = string.Empty;
            do
            {
                fileName = this.waitForUpload.Dequeue();
                if (!string.IsNullOrEmpty(fileName))
                {
                    try
                    {
                        if (File.Exists(serverPath))
                        {
                            File.Copy(fileName, Path.Combine(serverPath, Path.GetFileName(fileName)), true);
                            Log4Neter.Info(string.Format("上传图片{0}", fileName));
                        }
                    }
                    catch (Exception ex)
                    {
                        Log4Neter.Error("上传抓拍照片", ex);

                        break;
                    }
                }

            } while (fileName != null);
        }

        /// <summary>
        /// 清理过期的抓拍照片
        /// </summary> 
        public void ClearExpireCapturePicture()
        {
            foreach (string item in Directory.GetFiles(SelfVars.CapturePicturePath).Where(a =>
            {
                return new FileInfo(a).LastWriteTime > DateTime.Now.AddMonths(commonDAO.GetAppletConfigInt32("汽车智能化_抓拍照片保存天数"));
            }))
            {
                try
                {
                    File.Delete(item);
                    Log4Neter.Info(string.Format("删除图片{0}", item));
                }
                catch { }
            }
        }

        #endregion


        #region 通通停车

        /// <summary>
        /// 执行摄像头抓拍，并保存数据
        /// </summary>
        /// <param name="transportId">运输记录Id</param>
        private void ThinkCamareCapturePicture(string transportId)
        {
            if (this.CurrentImperfectCar == null) return;
            try
            {
                // 抓拍照片服务器发布地址
                string pictureWebUrl = commonDAO.GetCommonAppletConfigString("汽车智能化_抓拍照片发布路径");
                if (this.CurrentImperfectCar.PassWay == eDirection.Way1)
                {
                    // 摄像机1
                    string picture1FileName = Path.Combine(SelfVars.CapturePicturePath, string.Format("{0}_{1}.bmp", this.CurrentImperfectCar.Voucher, DateTime.Now.ToString("yyyyMMddHHmmssff")));
                    if (Hardwarer.Rwer1.Capture(picture1FileName))
                    {
                        CmcsTransportPicture transportPicture = new CmcsTransportPicture()
                        {
                            CaptureTime = DateTime.Now,
                            CaptureType = CommonAppConfig.GetInstance().AppIdentifier,
                            TransportId = transportId,
                            PicturePath = pictureWebUrl + Path.GetFileName(picture1FileName)
                        };

                        if (commonDAO.SelfDber.Insert(transportPicture) > 0) waitForUpload.Enqueue(picture1FileName);
                    }
                }
                else if (this.CurrentImperfectCar.PassWay == eDirection.Way2)
                {
                    // 摄像机2
                    string picture2FileName = Path.Combine(SelfVars.CapturePicturePath, "Camera", string.Format("{0}_{1}.bmp", this.CurrentImperfectCar.Voucher, DateTime.Now.ToString("yyyyMMddHHmmssff")));
                    if (Hardwarer.Rwer1.Capture(picture2FileName))
                    {
                        CmcsTransportPicture transportPicture = new CmcsTransportPicture()
                        {
                            CaptureTime = DateTime.Now,
                            CaptureType = CommonAppConfig.GetInstance().AppIdentifier,
                            TransportId = transportId,
                            PicturePath = pictureWebUrl + Path.GetFileName(picture2FileName)
                        };

                        if (commonDAO.SelfDber.Insert(transportPicture) > 0) waitForUpload.Enqueue(picture2FileName);
                    }
                }
            }
            catch (Exception ex)
            {
                Log4Neter.Error("通通停车摄像机抓拍", ex);
            }
        }

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

                this.InductorCoil1Port = commonDAO.GetAppletConfigInt32("IO控制器_地感1端口");
                this.InductorCoil2Port = commonDAO.GetAppletConfigInt32("IO控制器_地感2端口");
                this.InductorCoil3Port = commonDAO.GetAppletConfigInt32("IO控制器_地感3端口");
                this.InductorCoil4Port = commonDAO.GetAppletConfigInt32("IO控制器_地感4端口");
                this.InductorCoil3Port = commonDAO.GetAppletConfigInt32("IO控制器_地感3端口");
                this.InfraredSensor1Port = commonDAO.GetAppletConfigInt32("IO控制器_对射1端口");
                this.InfraredSensor2Port = commonDAO.GetAppletConfigInt32("IO控制器_对射2端口");

                this.WbMinWeight = commonDAO.GetAppletConfigDouble("地磅仪表_最小称重");

                // IO控制器
                Hardwarer.Iocer.OnReceived += new IOC.JMDM20DIOV2.JMDM20DIOV2Iocer.ReceivedEventHandler(Iocer_Received);
                Hardwarer.Iocer.OnStatusChange += new IOC.JMDM20DIOV2.JMDM20DIOV2Iocer.StatusChangeHandler(Iocer_StatusChange);
                success = Hardwarer.Iocer.OpenCom(commonDAO.GetAppletConfigInt32("IO控制器_串口"), commonDAO.GetAppletConfigInt32("IO控制器_波特率"), commonDAO.GetAppletConfigInt32("IO控制器_数据位"), (StopBits)commonDAO.GetAppletConfigInt32("IO控制器_停止位"), (Parity)commonDAO.GetAppletConfigInt32("IO控制器_校验位"));
                if (!success) MessageBoxEx.Show("IO控制器连接失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.iocControler = new IocControler(Hardwarer.Iocer);

                // 地磅仪表
                Hardwarer.Wber.OnStatusChange += new WB.TOLEDO.YAOHUA.TOLEDO_YAOHUAWber.StatusChangeHandler(Wber_OnStatusChange);
                Hardwarer.Wber.OnSteadyChange += new WB.TOLEDO.YAOHUA.TOLEDO_YAOHUAWber.SteadyChangeEventHandler(Wber_OnSteadyChange);
                Hardwarer.Wber.OnWeightChange += new WB.TOLEDO.YAOHUA.TOLEDO_YAOHUAWber.WeightChangeEventHandler(Wber_OnWeightChange);
                success = Hardwarer.Wber.OpenCom(commonDAO.GetAppletConfigInt32("地磅仪表_串口"), commonDAO.GetAppletConfigInt32("地磅仪表_波特率"), commonDAO.GetAppletConfigInt32("地磅仪表_数据位"), commonDAO.GetAppletConfigInt32("地磅仪表_停止位"));

                TaskSimpleScheduler taskSimpleScheduler = new TaskSimpleScheduler();

                // 车号识别1
                Hardwarer.Rwer1.OnActionReadSuccess = Rwer1_OnReadSuccess;
                Hardwarer.Rwer1.OnActionScanError = Rwer1_OnScanError;
                Hardwarer.Rwer1.OnActionStatusChange = Rwer1_OnStatusChange;
                success = Hardwarer.Rwer1.ConnectCamera(commonDAO.GetAppletConfigString("车号识别1_IP地址"), IntPtr.Zero);
                if (!success)
                {
                    MessageBoxEx.Show("车号识别1连接失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    InvokeEx(() =>
                      {
                          Hardwarer.Rwer1.StartPreview(picVideo1.Handle);
                      });
                }

                if (commonDAO.GetAppletConfigString("双向磅") != "0")
                {
                    // 车号识别2
                    Hardwarer.Rwer2.OnActionReadSuccess = Rwer2_OnReadSuccess;
                    Hardwarer.Rwer2.OnActionScanError = Rwer2_OnScanError;
                    Hardwarer.Rwer2.OnActionStatusChange = Rwer2_OnStatusChange;
                    success = Hardwarer.Rwer2.ConnectCamera(commonDAO.GetAppletConfigString("车号识别2_IP地址"), IntPtr.Zero);
                    if (!success)
                    {
                        MessageBoxEx.Show("车号识别2连接失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        InvokeEx(() =>
                      {
                          Hardwarer.Rwer2.StartPreview(this.picVideo2.Handle);
                      });
                    }
                }

                #region 海康视频

                //IPCer.InitSDK();

                //CmcsCamare video1 = commonDAO.SelfDber.Entity<CmcsCamare>("where EquipmentCode=:EquipmentCode", new { EquipmentCode = "摄像机1" });
                //if (video1 != null)
                //{
                //    if (iPCer1.Login(video1.Ip, video1.Port, video1.UserName, video1.Password))
                //        iPCer1.StartPreview(panVideo1.Handle, video1.Channel);
                //}

                //CmcsCamare video2 = commonDAO.SelfDber.Entity<CmcsCamare>("where EquipmentCode=:EquipmentCode", new { EquipmentCode = "摄像机2" });
                //if (video2 != null)
                //{
                //    if (iPCer2.Login(video2.Ip, video2.Port, video2.UserName, video2.Password))
                //        iPCer2.StartPreview(panVideo2.Handle, video2.Channel);
                //}

                #endregion

                #region LED控制卡1

                string led1SocketIP = commonDAO.GetAppletConfigString("LED显示屏1_IP地址");
                if (!string.IsNullOrEmpty(led1SocketIP) && commonDAO.TestPing(led1SocketIP))
                {
                    int nResult = YB14DynamicAreaLeder.AddScreen(YB14DynamicAreaLeder.CONTROLLER_BX_5E1, this.LED1nScreenNo, YB14DynamicAreaLeder.SEND_MODE_NETWORK, 96, 64, 1, 1, "", 0, led1SocketIP, 5005, "");
                    if (nResult == YB14DynamicAreaLeder.RETURN_NOERROR)
                    {
                        nResult = YB14DynamicAreaLeder.AddScreenDynamicArea(this.LED1nScreenNo, this.LED1DYArea_ID, 1, 10, 1, "", 0, 0, 0, 96, 64, 255, 0, 255, 7, 6, 1);
                        if (nResult == YB14DynamicAreaLeder.RETURN_NOERROR)
                        {
                            nResult = YB14DynamicAreaLeder.AddScreenDynamicAreaFile(this.LED1nScreenNo, this.LED1DYArea_ID, this.LED1TempFile, 0, "宋体", 12, 0, 120, 1, 3, 0);
                            if (nResult == YB14DynamicAreaLeder.RETURN_NOERROR)
                            {
                                nResult = YB14DynamicAreaLeder.SendDynamicAreaInfoCommand(this.LED1nScreenNo, this.LED1DYArea_ID);
                                if (nResult == YB14DynamicAreaLeder.RETURN_NOERROR)
                                {
                                    // 初始化成功
                                    this.LED1ConnectStatus = true;
                                    UpdateLed1Show("  等待上磅");
                                }
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

                #endregion

                #region LED控制卡2
                if (commonDAO.GetAppletConfigString("双向磅") != "0")
                {
                    string led2SocketIP = commonDAO.GetAppletConfigString("LED显示屏2_IP地址");
                    if (!string.IsNullOrEmpty(led2SocketIP) && commonDAO.TestPing(led2SocketIP))
                    {
                        int nResult = YB14DynamicAreaLeder.AddScreen(YB14DynamicAreaLeder.CONTROLLER_BX_5E1, this.LED2nScreenNo, YB14DynamicAreaLeder.SEND_MODE_NETWORK, 96, 64, 1, 1, "", 57600, led2SocketIP, 5005, "");
                        if (nResult == YB14DynamicAreaLeder.RETURN_NOERROR)
                        {
                            nResult = YB14DynamicAreaLeder.AddScreenDynamicArea(this.LED2nScreenNo, this.LED2DYArea_ID, 0, 10, 1, "", 0, 0, 0, 96, 64, 255, 0, 255, 7, 6, 1);
                            if (nResult == YB14DynamicAreaLeder.RETURN_NOERROR)
                            {
                                nResult = YB14DynamicAreaLeder.AddScreenDynamicAreaFile(this.LED2nScreenNo, this.LED2DYArea_ID, this.LED2TempFile, 0, "宋体", 12, 0, 120, 1, 3, 0);
                                if (nResult == YB14DynamicAreaLeder.RETURN_NOERROR)
                                {
                                    // 初始化成功
                                    this.LED2ConnectStatus = true;
                                    UpdateLed2Show("  等待上磅");
                                }
                                else
                                {
                                    this.LED2ConnectStatus = false;
                                    Log4Neter.Error("初始化LED2控制卡", new Exception(YB14DynamicAreaLeder.GetErrorMessage("AddScreenDynamicAreaFile", nResult)));
                                    MessageBoxEx.Show("LED2控制卡连接失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                            else
                            {
                                this.LED2ConnectStatus = false;
                                Log4Neter.Error("初始化LED2控制卡", new Exception(YB14DynamicAreaLeder.GetErrorMessage("AddScreenDynamicArea", nResult)));
                                MessageBoxEx.Show("LED2控制卡连接失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {
                            this.LED2ConnectStatus = false;
                            Log4Neter.Error("初始化LED2控制卡", new Exception(YB14DynamicAreaLeder.GetErrorMessage("AddScreen", nResult)));
                            MessageBoxEx.Show("LED2控制卡连接失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                #endregion

                timer1.Enabled = true;
                iocControler.GreenLight1();
                iocControler.GreenLight2();
            }
            catch (Exception ex)
            {
                Log4Neter.Error("设备初始化", ex);
                if (iocControler != null)
                {
                    iocControler.RedLight1();
                    iocControler.RedLight2();
                }
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
                iocControler.RedLight1();
                iocControler.RedLight2();
                Hardwarer.Iocer.OnReceived -= new IOC.JMDM20DIOV2.JMDM20DIOV2Iocer.ReceivedEventHandler(Iocer_Received);
                Hardwarer.Iocer.OnStatusChange -= new IOC.JMDM20DIOV2.JMDM20DIOV2Iocer.StatusChangeHandler(Iocer_StatusChange);
                Hardwarer.Iocer.CloseCom();
            }
            catch { }
            try
            {
                Hardwarer.Wber.CloseCom();
            }
            catch { }
            try
            {
                Hardwarer.Rwer1.Close();
            }
            catch { }
            try
            {
                if (commonDAO.GetAppletConfigString("双向磅") != "0")
                    Hardwarer.Rwer2.Close();
            }
            catch { }
            try
            {
                YB14DynamicAreaLeder.SendDeleteDynamicAreasCommand(this.LED1nScreenNo, 1, "");
                YB14DynamicAreaLeder.DeleteScreen(this.LED1nScreenNo);
            }
            catch { }
            try
            {
                if (commonDAO.GetAppletConfigString("双向磅") != "0")
                {
                    YB14DynamicAreaLeder.SendDeleteDynamicAreasCommand(this.LED2nScreenNo, 1, "");
                    YB14DynamicAreaLeder.DeleteScreen(this.LED2nScreenNo);
                }
            }
            catch { }
            try
            {
                IPCer.CleanupSDK();
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
            if (this.iocControler != null)
            {
                this.iocControler.Gate1Up();
                Log4Neter.Info(string.Format("手动升道闸1 车牌号:{0}", this.CurrentAutotruck != null ? this.CurrentAutotruck.CarNumber : "无车牌"));
            }
        }

        /// <summary>
        /// 道闸1降杆
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGate1Down_Click(object sender, EventArgs e)
        {
            if (this.iocControler != null && !EastOnEnterWay())
            {
                this.iocControler.Gate1Down();
                Log4Neter.Info(string.Format("手动降道闸1 车牌号:{0}", this.CurrentAutotruck != null ? this.CurrentAutotruck.CarNumber : "无车牌"));
            }
        }

        /// <summary>
        /// 道闸2升杆
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGate2Up_Click(object sender, EventArgs e)
        {
            if (this.iocControler != null)
            {
                this.iocControler.Gate2Up();
                Log4Neter.Info(string.Format("手动升道闸2 车牌号:{0}", this.CurrentAutotruck != null ? this.CurrentAutotruck.CarNumber : "无车牌"));
            }
        }

        /// <summary>
        /// 道闸2降杆
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGate2Down_Click(object sender, EventArgs e)
        {
            if (this.iocControler != null && !WestOnEnterWay())
            {
                this.iocControler.Gate2Down();
                Log4Neter.Info(string.Format("手动降道闸2 车牌号:{0}", this.CurrentAutotruck != null ? this.CurrentAutotruck.CarNumber : "无车牌"));
            }
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
            timer1.Interval = 1000;

            try
            {

                switch (this.CurrentFlowFlag)
                {
                    case eFlowFlag.等待车辆:
                        // 当前地磅重量小于最小称重且所有地感、对射无信号时重置
                        if (Hardwarer.Wber.Weight < this.WbMinWeight && !HasCarOnEnterWay() && !HasCarOnLeaveWay() && this.CurrentAutotruck != null
                            && this.CurrentImperfectCar != null) ResetBuyFuel();

                        break;

                    case eFlowFlag.识别车辆:
                        #region

                        // 队列中无车时，等待车辆
                        if (passCarQueuer.Count == 0)
                        {
                            this.CurrentFlowFlag = eFlowFlag.等待车辆;
                            break;
                        }

                        this.CurrentImperfectCar = passCarQueuer.Dequeue();

                        // 方式一：根据识别的车牌号查找车辆信息
                        this.CurrentAutotruck = carTransportDAO.GetAutotruckByCarNumber(this.CurrentImperfectCar.Voucher);

                        if (this.CurrentAutotruck != null)
                        {
                            if (this.CurrentAutotruck.IsUse == 1)
                            {
                                CurrentUnFinishTransport = carTransportDAO.GetUnFinishTransportByAutotruckId(this.CurrentAutotruck.Id);
                                if (CurrentUnFinishTransport != null)
                                {
                                    if (CurrentUnFinishTransport.CarType == eTransportType.原料煤入场.ToString() || CurrentUnFinishTransport.CarType == eTransportType.仓储煤入场.ToString() || CurrentUnFinishTransport.CarType == eTransportType.中转煤入场.ToString())
                                    {
                                        this.timer_BuyFuel_Cancel = false;
                                        this.CurrentFlowFlag = eFlowFlag.验证信息;
                                        timer_BuyFuel_Tick(null, null);
                                        Log4Neter.Info(string.Format("车牌号：{0} 入场煤", this.CurrentAutotruck.CarNumber));
                                    }
                                    else if (CurrentUnFinishTransport.CarType == eTransportType.仓储煤出场.ToString() || CurrentUnFinishTransport.CarType == eTransportType.中转煤出场.ToString() || CurrentUnFinishTransport.CarType == eTransportType.销售掺配煤.ToString() || CurrentUnFinishTransport.CarType == eTransportType.销售直销煤.ToString())
                                    {
                                        this.timer_SaleFuel_Cancel = false;
                                        this.CurrentFlowFlag = eFlowFlag.验证信息;
                                        timer_SaleFuel_Tick(null, null);
                                        Log4Neter.Info(string.Format("车牌号：{0} 出场煤", this.CurrentAutotruck.CarNumber));
                                    }
                                    else if (CurrentUnFinishTransport.CarType == eCarType.其他物资.ToString())
                                    {
                                        this.timer_Goods_Cancel = false;
                                        this.CurrentFlowFlag = eFlowFlag.验证信息;
                                        timer_Goods_Tick(null, null);
                                        Log4Neter.Info(string.Format("车牌号：{0} 其他物资", this.CurrentAutotruck.CarNumber));
                                    }
                                }
                                else
                                {
                                    UpdateLedShow(this.CurrentAutotruck.CarNumber, "未排队");
                                    this.voiceSpeaker.Speak("车牌号 " + this.CurrentAutotruck.CarNumber + " 未排队", 2, false);
                                    Log4Neter.Info(string.Format("车牌号：{0} 未排队", this.CurrentAutotruck.CarNumber));
                                }
                            }
                            else
                            {
                                UpdateLedShow(this.CurrentAutotruck.CarNumber, "已停用");
                                this.voiceSpeaker.Speak("车牌号 " + this.CurrentAutotruck.CarNumber + " 已停用，禁止通过", 2, false);

                                //timer1.Interval = 20000;
                                Log4Neter.Info(string.Format("车牌号：{0} 已停用", this.CurrentAutotruck.CarNumber));
                            }
                        }
                        else
                        {
                            UpdateLedShow(this.CurrentImperfectCar.Voucher, "未登记");

                            // 方式一：车号识别
                            this.voiceSpeaker.Speak("车牌号 " + this.CurrentImperfectCar.Voucher + " 未登记 禁止通过", 2, false);

                            timer1.Interval = 20000;
                        }
                        // 当前地磅重量小于最小称重且所有地感、对射无信号时重置

                        if (Hardwarer.Wber.Weight < this.WbMinWeight && !HasCarOnEnterWay() && !HasCarOnLeaveWay() && this.CurrentFlowFlag != eFlowFlag.等待车辆
                            && this.CurrentImperfectCar != null)
                        {
                            ResetBuyFuel();
                        }

                        #endregion
                        break;
                }

                commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.地磅仪表_实时重量.ToString(), Hardwarer.Wber.Weight.ToString());
                // 执行远程命令
                ExecAppRemoteControlCmd();

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
        /// 慢速任务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer2_Tick(object sender, EventArgs e)
        {
            timer2.Stop();
            // 三分钟执行一次
            timer2.Interval = 180000;

            try
            {
                // 入厂煤
                LoadTodayUnFinishBuyFuelTransport();
                LoadTodayFinishBuyFuelTransport();

                // 销售煤 
                LoadTodayUnFinishSaleFuelTransport();
                LoadTodayFinishSaleFuelTransport();

                // 其他物资
                LoadTodayUnFinishGoodsTransport();
                LoadTodayFinishGoodsTransport();

                // 上传抓拍照片
                UploadCapturePicture();
                // 清理抓拍照片
                if (DateTime.Now.Hour == 0) ClearExpireCapturePicture();
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
        /// 有车辆在上磅的道路上
        /// </summary>
        /// <returns></returns>
        bool HasCarOnEnterWay()
        {
            if (this.CurrentImperfectCar == null) return false;

            if (this.CurrentImperfectCar.PassWay == eDirection.UnKnow)
                return false;
            else if (this.CurrentImperfectCar.PassWay == eDirection.Way1)
                return this.InductorCoil1 || this.InductorCoil2 || this.InfraredSensor1;
            else if (this.CurrentImperfectCar.PassWay == eDirection.Way2)
                return this.InductorCoil3 || this.InductorCoil4 || this.InfraredSensor2;

            return true;
        }

        /// <summary>
        /// 有车辆在下磅的道路上
        /// </summary>
        /// <returns></returns>
        bool HasCarOnLeaveWay()
        {
            if (this.CurrentImperfectCar == null) return false;

            if (this.CurrentImperfectCar.PassWay == eDirection.UnKnow)
                return false;
            else if (this.CurrentImperfectCar.PassWay == eDirection.Way1)
                return this.InductorCoil3 || this.InductorCoil4 || this.InfraredSensor2;
            else if (this.CurrentImperfectCar.PassWay == eDirection.Way2)
                return this.InductorCoil1 || this.InductorCoil2 || this.InfraredSensor1;

            return true;
        }

        /// <summary>
        /// 东上磅方向有物体
        /// </summary>
        /// <returns></returns>
        bool EastOnEnterWay()
        {
            return this.InductorCoil1 || this.InductorCoil2 || this.InfraredSensor1;
        }

        /// <summary>
        /// 西上磅方向有物体
        /// </summary>
        /// <returns></returns>
        bool WestOnEnterWay()
        {
            return this.InductorCoil3 || this.InductorCoil4 || this.InfraredSensor2;
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
        /// 切换手动/自动模式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sbtnChangeAutoHandMode_ValueChanged(object sender, EventArgs e)
        {
            this.AutoHandMode = sbtnChangeAutoHandMode.Value;
        }

        #endregion

        #region 入厂煤业务

        bool timer_BuyFuel_Cancel = true;

        CmcsBuyFuelTransport currentBuyFuelTransport;
        /// <summary>
        /// 当前运输记录
        /// </summary>
        public CmcsBuyFuelTransport CurrentBuyFuelTransport
        {
            get { return currentBuyFuelTransport; }
            set
            {
                currentBuyFuelTransport = value;

                if (value != null)
                {
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.当前运输记录Id.ToString(), value.Id);
                    CmcsFuelKind fuelkind = Dbers.GetInstance().SelfDber.Get<CmcsFuelKind>(value.FuelKindId);
                    if (fuelkind != null)
                        txtFuelKindName_BuyFuel.Text = fuelkind.FuelName;
                    CmcsMine mine = Dbers.GetInstance().SelfDber.Get<CmcsMine>(value.MineId);
                    if (mine != null)
                        txtMineName_BuyFuel.Text = mine.Name;
                    CmcsSupplier supplier = Dbers.GetInstance().SelfDber.Get<CmcsSupplier>(value.SupplierId);
                    if (supplier != null)
                        txtSupplierName_BuyFuel.Text = supplier.Name;
                    CmcsTransportCompany company = Dbers.GetInstance().SelfDber.Get<CmcsTransportCompany>(value.TransportCompanyId);
                    if (company != null)
                        txtTransportCompanyName_BuyFuel.Text = company.Name;

                    txtGrossWeight_BuyFuel.Text = value.GrossWeight.ToString("F2");
                    txtTicketWeight_BuyFuel.Text = value.TicketWeight.ToString("F2");
                    txtTareWeight_BuyFuel.Text = value.TareWeight.ToString("F2");
                    txtDeductWeight_BuyFuel.Text = value.DeductWeight.ToString("F2");
                    txtSuttleWeight_BuyFuel.Text = value.SuttleWeight.ToString("F2");
                    txtCheckWeight.Text = value.CheckWeight.ToString("F2");
                    txtInFactoryType_BuyFuel.Text = value.InFactoryType;
                }
                else
                {
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.当前运输记录Id.ToString(), string.Empty);

                    txtFuelKindName_BuyFuel.ResetText();
                    txtMineName_BuyFuel.ResetText();
                    txtSupplierName_BuyFuel.ResetText();
                    txtTransportCompanyName_BuyFuel.ResetText();

                    txtGrossWeight_BuyFuel.ResetText();
                    txtTicketWeight_BuyFuel.ResetText();
                    txtTareWeight_BuyFuel.ResetText();
                    txtDeductWeight_BuyFuel.ResetText();
                    txtSuttleWeight_BuyFuel.ResetText();
                    txtCheckWeight.ResetText();
                    txtInFactoryType_BuyFuel.ResetText();
                }
            }
        }

        /// <summary>
        /// 选择车辆
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectAutotruck_BuyFuel_Click(object sender, EventArgs e)
        {
            FrmUnFinishTransport_Select frm = new FrmUnFinishTransport_Select(" order by CreateDate desc");
            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (this.InductorCoil1)
                    passCarQueuer.Enqueue(eDirection.Way1, frm.Output.CarNumber);
                else if (this.InductorCoil4)
                    passCarQueuer.Enqueue(eDirection.Way2, frm.Output.CarNumber);
                else
                    passCarQueuer.Enqueue(eDirection.UnKnow, frm.Output.CarNumber);
                this.CurrentFlowFlag = eFlowFlag.识别车辆;
                timer1_Tick(null, null);
            }
        }

        /// <summary>
        /// 保存入厂煤运输记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveTransport_BuyFuel_Click(object sender, EventArgs e)
        {
            btnSaveTransport_BuyFuel.Enabled = false;
            if (!SaveBuyFuelTransport()) MessageBoxEx.Show("保存失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            Log4Neter.Info("入厂煤手动保存重量");
            btnSaveTransport_BuyFuel.Enabled = true;
        }

        /// <summary>
        /// 保存运输记录
        /// </summary>
        /// <returns></returns>
        bool SaveBuyFuelTransport()
        {
            if (this.CurrentBuyFuelTransport == null) return false;

            try
            {
                if (this.CurrentBuyFuelTransport.GrossWeight > 0 && (double)(this.CurrentBuyFuelTransport.GrossWeight - (decimal)Hardwarer.Wber.Weight) < commonDAO.GetCommonAppletConfigDouble("地磅仪表_毛皮差"))
                {
                    UpdateLedShow("净重不正确", "联系过磅员");
                    this.voiceSpeaker.Speak("净重异常 ", 2, false);
                    return false;
                }
                if (weighterDAO.SaveBuyFuelTransport(this.CurrentBuyFuelTransport.Id, (decimal)Hardwarer.Wber.Weight, DateTime.Now, CommonAppConfig.GetInstance().AppIdentifier))
                {
                    this.CurrentBuyFuelTransport = commonDAO.SelfDber.Get<CmcsBuyFuelTransport>(this.CurrentBuyFuelTransport.Id);

                    FrontGateUp();

                    //btnSaveTransport_BuyFuel.Enabled = false;
                    this.CurrentFlowFlag = eFlowFlag.等待离开;

                    UpdateLedShow("  称重完成", "  请下磅");
                    this.voiceSpeaker.Speak("撑众完成请下磅", 2, false);

                    LoadTodayUnFinishBuyFuelTransport();
                    LoadTodayFinishBuyFuelTransport();

                    CamareCapturePicture(this.CurrentBuyFuelTransport.Id);

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
            this.timer_BuyFuel_Cancel = true;

            this.CurrentFlowFlag = eFlowFlag.等待车辆;

            this.CurrentAutotruck = null;
            this.CurrentBuyFuelTransport = null;
            this.CurrentUnFinishTransport = null;
            //btnSaveTransport_BuyFuel.Enabled = false;
            this.CarNumber = null;
            FrontGateDown();
            BackGateDown();

            // 最后重置
            this.CurrentImperfectCar = null;
            iocControler.GreenLight1();
            iocControler.GreenLight2();
            UpdateLedShow("  等待车辆");
            ResetCount = 0;
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
                    case eFlowFlag.验证信息:
                        #region
                        if (this.CurrentUnFinishTransport != null)
                        {
                            this.CurrentBuyFuelTransport = commonDAO.SelfDber.Get<CmcsBuyFuelTransport>(this.CurrentUnFinishTransport.TransportId);
                            if (this.CurrentBuyFuelTransport != null)
                            {
                                // 判断路线设置
                                string nextPlace;
                                //BS系统没使用路线判断没法使用 去掉路线判断
                                //if (carTransportDAO.CheckNextTruckInFactoryWay(this.CurrentAutotruck.CarType, this.CurrentBuyFuelTransport.StepName, "重车|轻车", CommonAppConfig.GetInstance().AppIdentifier, out nextPlace))
                                if (true)
                                {
                                    if (this.CurrentBuyFuelTransport.SuttleWeight == 0)
                                    {
                                        BackGateUp();
                                        ThinkCamareCapturePicture(this.CurrentBuyFuelTransport.Id);
                                        this.CurrentFlowFlag = eFlowFlag.等待上磅;

                                        UpdateLedShow(this.CurrentAutotruck.CarNumber, "请上磅");
                                        this.voiceSpeaker.Speak("车牌号 " + this.CurrentAutotruck.CarNumber + " 请上磅", 2, false);
                                    }
                                    else
                                    {
                                        UpdateLedShow(this.CurrentAutotruck.CarNumber, "已称重");
                                        this.voiceSpeaker.Speak("车牌号 " + this.CurrentAutotruck.CarNumber + " 已称重", 2, false);

                                        timer_BuyFuel.Interval = 20000;
                                        Log4Neter.Info(string.Format("车牌号：{0} 已称重", this.CurrentAutotruck.CarNumber));
                                    }
                                }
                                else
                                {
                                    UpdateLedShow("路线错误", "禁止通过");
                                    this.voiceSpeaker.Speak("路线错误 禁止通过 " + (!string.IsNullOrEmpty(nextPlace) ? "请前往" + nextPlace : ""), 2, false);

                                    timer_BuyFuel.Interval = 20000;
                                    Log4Neter.Info(string.Format("车牌号：{0} 路线错误", this.CurrentAutotruck.CarNumber));
                                }
                            }
                            else
                            {
                                UpdateLedShow("未找到运输记录", "禁止通过");
                                this.voiceSpeaker.Speak("未找到运输记录 禁止通过 ", 2, false);

                                commonDAO.SelfDber.Delete<CmcsUnFinishTransport>(this.CurrentUnFinishTransport.Id);
                            }
                        }
                        else
                        {
                            UpdateLedShow("未排队", "禁止通过");
                            this.voiceSpeaker.Speak("未排队 禁止通过 ", 2, false);

                        }

                        #endregion
                        break;

                    case eFlowFlag.等待上磅:
                        #region

                        // 降低灵敏度
                        timer_BuyFuel.Interval = 6000;

                        // 当地磅仪表重量大于最小称重且来车方向的地感与对射均无信号，则判定车已经完全上磅
                        if (Hardwarer.Wber.Weight >= this.WbMinWeight && !HasCarOnEnterWay())
                        {
                            BackGateDown();

                            this.CurrentFlowFlag = eFlowFlag.等待稳定;
                        }

                        #endregion
                        break;

                    case eFlowFlag.等待稳定:
                        #region

                        // 提高灵敏度
                        timer_BuyFuel.Interval = 1000;

                        //btnSaveTransport_BuyFuel.Enabled = this.WbSteady;

                        UpdateLedShow(this.CurrentAutotruck.CarNumber, Hardwarer.Wber.Weight.ToString("#0.######"));

                        if (this.WbSteady)
                        {
                            if (this.AutoHandMode)
                            {
                                // 自动模式
                                if (!SaveBuyFuelTransport())
                                {
                                    UpdateLedShow(this.CurrentAutotruck.CarNumber, "称重失败");
                                    this.voiceSpeaker.Speak("车牌号 " + this.CurrentAutotruck.CarNumber + " 撑众失败 请联系管理员", 2, false);
                                    Log4Neter.Info(string.Format("车牌号：{0} 称重失败", this.CurrentAutotruck.CarNumber));
                                }
                            }
                            else
                            {
                                // 手动模式 
                            }
                        }

                        #endregion
                        break;

                    case eFlowFlag.等待离开:
                        #region

                        // 当前地磅重量小于最小称重且所有地感、对射无信号时重置
                        if (Hardwarer.Wber.Weight < this.WbMinWeight && !HasCarOnLeaveWay() && !HasCarOnEnterWay())
                        {
                            ResetBuyFuel();
                            Log4Neter.Info(string.Format("车牌号：{0} 已下磅", this.CurrentAutotruck.CarNumber));
                        }

                        // 降低灵敏度
                        timer_BuyFuel.Interval = 4000;

                        #endregion
                        break;
                }

                // 当前地磅重量小于最小称重且所有地感、对射无信号时重置
                if (Hardwarer.Wber.Weight < this.WbMinWeight && !HasCarOnEnterWay() && !HasCarOnLeaveWay() && this.CurrentFlowFlag != eFlowFlag.等待车辆
                    && this.CurrentImperfectCar != null)
                {
                    if (ResetCount > 2)
                        ResetBuyFuel();
                    else
                        ResetCount++;
                }

#if DEBUG
                Log4Neter.Info(Hardwarer.Wber.Weight + "t," + this.InductorCoil1 + this.InductorCoil2 + this.InductorCoil3 + this.InductorCoil4 + this.InfraredSensor1 + this.InfraredSensor2);
#else
#endif
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
        /// 获取未完成的入厂煤记录
        /// </summary>
        void LoadTodayUnFinishBuyFuelTransport()
        {
            superGridControl1_BuyFuel.PrimaryGrid.DataSource = weighterDAO.GetUnFinishBuyFuelTransport();
        }

        /// <summary>
        /// 获取指定日期已完成的入厂煤记录
        /// </summary>
        void LoadTodayFinishBuyFuelTransport()
        {
            superGridControl2_BuyFuel.PrimaryGrid.DataSource = weighterDAO.GetFinishedBuyFuelTransport(DateTime.Now.Date, DateTime.Now.Date.AddDays(1));
        }

        #endregion

        #region 销售煤业务

        bool timer_SaleFuel_Cancel = true;
        CmcsSaleFuelTransport currentSaleFuelTransport;
        /// <summary>
        /// 当前运输记录
        /// </summary>
        public CmcsSaleFuelTransport CurrentSaleFuelTransport
        {
            get { return currentSaleFuelTransport; }
            set
            {
                currentSaleFuelTransport = value;

                if (value != null)
                {
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.当前运输记录Id.ToString(), value.Id);
                    txtCarNumber_SaleFuel.Text = value.CarNumber;
                    if (!string.IsNullOrEmpty(value.TransportSalesNum))
                        txt_YBNumber1.Text = value.TransportSalesNum;
                    if (!string.IsNullOrEmpty(value.TransportNo))
                        txt_TransportNo1.Text = value.TransportNo;
                    CmcsSupplier customer = value.TheSupplier;
                    if (customer != null)
                        txt_Consignee1.Text = customer.Name;
                    CmcsTransportCompany company = Dbers.GetInstance().SelfDber.Get<CmcsTransportCompany>(value.TransportCompanyId);
                    if (company != null)
                        txt_TransportCompayName1.Text = company.Name;
                    CmcsFuelKind fuelkind = Dbers.GetInstance().SelfDber.Get<CmcsFuelKind>(value.FuelKindId);
                    if (fuelkind != null)
                        txtFuelKindName_SaleFuel.Text = fuelkind.FuelName;
                    txtGrossWeight_SaleFuel.Text = value.GrossWeight.ToString("F2");
                    txtTareWeight_SaleFuel.Text = value.TareWeight.ToString("F2");
                    txtSuttleWeight_SaleFuel.Text = value.SuttleWeight.ToString("F2");
                    txtOutFactoryType_SalesFuel.Text = value.OutFactoryType;
                }
                else
                {
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.当前运输记录Id.ToString(), string.Empty);
                    txtCarNumber_SaleFuel.ResetText();
                    txt_YBNumber1.ResetText();
                    txt_TransportNo1.ResetText();
                    txt_Consignee1.ResetText();
                    txt_TransportCompayName1.ResetText();
                    txtFuelKindName_SaleFuel.ResetText();
                    txtGrossWeight_SaleFuel.ResetText();
                    txtTareWeight_SaleFuel.ResetText();
                    txtSuttleWeight_SaleFuel.ResetText();
                    txtOutFactoryType_SalesFuel.ResetText();
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
            FrmUnFinishTransport_Select frm = new FrmUnFinishTransport_Select(" order by CreateDate desc");
            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (this.InductorCoil1)
                    passCarQueuer.Enqueue(eDirection.Way1, frm.Output.CarNumber);
                else if (this.InductorCoil4)
                    passCarQueuer.Enqueue(eDirection.Way2, frm.Output.CarNumber);
                else
                    passCarQueuer.Enqueue(eDirection.UnKnow, frm.Output.CarNumber);

                this.CurrentFlowFlag = eFlowFlag.识别车辆;
                txtCarNumber_SaleFuel.Text = frm.Output.CarNumber;
                timer1_Tick(null, null);
            }
        }




        /// <summary>
        /// 保存排队记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveTransport_SaleFuel_Click(object sender, EventArgs e)
        {
            if (!SaveSaleFuelTransport()) MessageBoxEx.Show("保存失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            Log4Neter.Info("出场煤手动保存重量");
        }

        /// <summary>
        /// 保存运输记录
        /// </summary>
        /// <returns></returns>
        bool SaveSaleFuelTransport()
        {
            if (this.CurrentSaleFuelTransport == null) return false;

            try
            {
                if (this.CurrentSaleFuelTransport.TareWeight > 0 && (double)((decimal)Hardwarer.Wber.Weight - this.CurrentSaleFuelTransport.TareWeight) < commonDAO.GetCommonAppletConfigDouble("地磅仪表_毛皮差"))
                {
                    UpdateLedShow("净重不正确", "联系过磅员");
                    this.voiceSpeaker.Speak("净重异常 ", 2, false);
                    return false;
                }
                if (weighterDAO.SaveSaleFuelTransport(this.CurrentSaleFuelTransport.Id, (decimal)Hardwarer.Wber.Weight, DateTime.Now, CommonAppConfig.GetInstance().AppIdentifier))
                {
                    this.CurrentSaleFuelTransport = commonDAO.SelfDber.Get<CmcsSaleFuelTransport>(this.CurrentSaleFuelTransport.Id);

                    FrontGateUp();

                    this.CurrentFlowFlag = eFlowFlag.等待离开;

                    UpdateLedShow("  称重完成", "  请下磅");
                    this.voiceSpeaker.Speak("撑众完成请下磅", 2, false);

                    LoadTodayUnFinishSaleFuelTransport();
                    LoadTodayFinishSaleFuelTransport();

                    ThinkCamareCapturePicture(this.CurrentSaleFuelTransport.Id);

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
        private void btnReset_SaleFuel_Click(object sender, EventArgs e)
        {
            ResetSaleFuel();
        }

        /// <summary>
        /// 重置信息
        /// </summary>
        void ResetSaleFuel()
        {
            this.timer_SaleFuel_Cancel = true;

            this.CurrentFlowFlag = eFlowFlag.等待车辆;

            this.CurrentAutotruck = null;
            this.CurrentSaleFuelTransport = null;
            this.CurrentUnFinishTransport = null;

            //btnSaveTransport_SaleFuel.Enabled = false;

            FrontGateDown();
            BackGateDown();

            UpdateLedShow("  等待车辆");
            iocControler.GreenLight1();
            iocControler.GreenLight2();
            // 最后重置
            this.CurrentImperfectCar = null;
            ResetCount = 0;
        }

        /// <summary>
        /// 销售煤运输记录业务定时器
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
                    case eFlowFlag.验证信息:
                        #region

                        this.CurrentSaleFuelTransport = commonDAO.SelfDber.Get<CmcsSaleFuelTransport>(this.CurrentUnFinishTransport.TransportId);
                        if (this.CurrentSaleFuelTransport != null)
                        {
                            // 判断路线设置
                            string nextPlace;
                            if (true)
                            //if (carTransportDAO.CheckNextTruckInFactoryWay(this.CurrentAutotruck.CarType, this.CurrentSaleFuelTransport.StepName, "重车|轻车", CommonAppConfig.GetInstance().AppIdentifier, out nextPlace))
                            {
                                if (this.CurrentSaleFuelTransport.SuttleWeight == 0)
                                {
                                    BackGateUp();
                                    ThinkCamareCapturePicture(this.CurrentSaleFuelTransport.Id);
                                    this.CurrentFlowFlag = eFlowFlag.等待上磅;

                                    UpdateLedShow(this.CurrentAutotruck.CarNumber, "请上磅");
                                    this.voiceSpeaker.Speak("车牌号 " + this.CurrentAutotruck.CarNumber + " 请上磅", 2, false);
                                }
                                else
                                {
                                    UpdateLedShow(this.CurrentAutotruck.CarNumber, "已称重");
                                    this.voiceSpeaker.Speak("车牌号 " + this.CurrentAutotruck.CarNumber + " 已称重", 2, false);

                                    timer_SaleFuel.Interval = 20000;
                                }
                            }
                            else
                            {
                                UpdateLedShow("路线错误", "禁止通过");
                                this.voiceSpeaker.Speak("路线错误 禁止通过 " + (!string.IsNullOrEmpty(nextPlace) ? "请前往" + nextPlace : ""), 2, false);

                                timer_SaleFuel.Interval = 20000;
                            }
                        }
                        else
                        {
                            commonDAO.SelfDber.Delete<CmcsUnFinishTransport>(this.CurrentUnFinishTransport.Id);
                        }

                        #endregion
                        break;

                    case eFlowFlag.等待上磅:
                        #region

                        // 当地磅仪表重量大于最小称重且来车方向的地感与对射均无信号，则判定车已经完全上磅
                        if (Hardwarer.Wber.Weight >= this.WbMinWeight && !HasCarOnEnterWay())
                        {
                            BackGateDown();

                            this.CurrentFlowFlag = eFlowFlag.等待稳定;
                        }

                        // 降低灵敏度
                        timer_SaleFuel.Interval = 4000;

                        #endregion
                        break;

                    case eFlowFlag.等待稳定:
                        #region

                        // 提高灵敏度
                        timer_SaleFuel.Interval = 1000;

                        btnSaveTransport_SaleFuel.Enabled = this.WbSteady;

                        UpdateLedShow(this.CurrentAutotruck.CarNumber, Hardwarer.Wber.Weight.ToString("#0.######"));

                        if (this.WbSteady)
                        {
                            if (this.AutoHandMode)
                            {
                                // 自动模式
                                if (!SaveSaleFuelTransport())
                                {
                                    UpdateLedShow(this.CurrentAutotruck.CarNumber, "称重失败");
                                    this.voiceSpeaker.Speak("车牌号 " + this.CurrentAutotruck.CarNumber + "撑众失败，请联系管理员", 2, false);
                                }
                            }
                            else
                            {
                                // 手动模式 
                            }
                        }

                        #endregion
                        break;

                    case eFlowFlag.等待离开:
                        #region

                        // 当前地磅重量小于最小称重且所有地感、对射无信号时重置
                        if (Hardwarer.Wber.Weight < this.WbMinWeight && !HasCarOnLeaveWay()) ResetSaleFuel();

                        // 降低灵敏度
                        timer_SaleFuel.Interval = 4000;

                        #endregion
                        break;
                }

                // 当前地磅重量小于最小称重且所有地感、对射无信号时重置
                if (Hardwarer.Wber.Weight < this.WbMinWeight && !HasCarOnEnterWay() && !HasCarOnLeaveWay() && this.CurrentFlowFlag != eFlowFlag.等待车辆
                    && this.CurrentImperfectCar != null)
                {
                    if (ResetCount > 2)
                        ResetSaleFuel();
                    else
                        ResetCount++;
                }
#if DEBUG
                Log4Neter.Info(Hardwarer.Wber.Weight + "t," + this.InductorCoil1 + this.InductorCoil2 + this.InductorCoil3 + this.InductorCoil4 + this.InfraredSensor1 + this.InfraredSensor2);
#else
#endif
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
        /// 获取未完成的销售记录
        /// </summary>
        void LoadTodayUnFinishSaleFuelTransport()
        {
            superGridControl1_SaleFuel.PrimaryGrid.DataSource = weighterDAO.GetUnFinishSaleFuelTransport();
        }

        /// <summary>
        /// 获取指定日期已完成的销售记录
        /// </summary>
        void LoadTodayFinishSaleFuelTransport()
        {
            superGridControl2_SaleFuel.PrimaryGrid.DataSource = weighterDAO.GetFinishedSaleFuelTransport(DateTime.Now.Date, DateTime.Now.Date.AddDays(1));
        }
        #endregion

        #region 其他物资业务

        bool timer_Goods_Cancel = true;

        CmcsGoodsTransport currentGoodsTransport;
        /// <summary>
        /// 当前运输记录
        /// </summary>
        public CmcsGoodsTransport CurrentGoodsTransport
        {
            get { return currentGoodsTransport; }
            set
            {
                currentGoodsTransport = value;

                if (value != null)
                {
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.当前运输记录Id.ToString(), value.Id);
                    CmcsSupplyReceive supplyReceive = Dbers.GetInstance().SelfDber.Get<CmcsSupplyReceive>(value.SupplyUnitId);
                    if (supplyReceive != null)
                        txtSupplyUnitName_Goods.Text = supplyReceive.UnitName;
                    CmcsSupplyReceive Receive = Dbers.GetInstance().SelfDber.Get<CmcsSupplyReceive>(value.ReceiveUnitId);
                    if (Receive != null)
                        txtReceiveUnitName_Goods.Text = Receive.UnitName;
                    txtGoodsTypeName_Goods.Text = Dbers.GetInstance().SelfDber.Get<CmcsGoodsType>(value.GoodsTypeId).GoodsName;
                    txtFirstWeight_Goods.Text = value.FirstWeight.ToString("F2");
                    txtSecondWeight_Goods.Text = value.SecondWeight.ToString("F2");
                    txtSuttleWeight_Goods.Text = value.SuttleWeight.ToString("F2");
                }
                else
                {
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.当前运输记录Id.ToString(), string.Empty);

                    txtSupplyUnitName_Goods.ResetText();
                    txtReceiveUnitName_Goods.ResetText();
                    txtGoodsTypeName_Goods.ResetText();

                    txtFirstWeight_Goods.ResetText();
                    txtSecondWeight_Goods.ResetText();
                    txtSuttleWeight_Goods.ResetText();
                }
            }
        }

        /// <summary>
        /// 选择车辆
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectAutotruck_Goods_Click(object sender, EventArgs e)
        {
            FrmUnFinishTransport_Select frm = new FrmUnFinishTransport_Select(" order by CreateDate desc");
            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (this.InductorCoil1)
                    passCarQueuer.Enqueue(eDirection.Way1, frm.Output.CarNumber);
                else if (this.InductorCoil4)
                    passCarQueuer.Enqueue(eDirection.Way2, frm.Output.CarNumber);
                else
                    passCarQueuer.Enqueue(eDirection.UnKnow, frm.Output.CarNumber);

                this.CurrentFlowFlag = eFlowFlag.识别车辆;
            }
        }

        /// <summary>
        /// 保存排队记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveTransport_Goods_Click(object sender, EventArgs e)
        {
            if (!SaveGoodsTransport()) MessageBoxEx.Show("保存失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// 保存运输记录
        /// </summary>
        /// <returns></returns>
        bool SaveGoodsTransport()
        {
            if (this.CurrentGoodsTransport == null) return false;

            try
            {
                if (this.CurrentGoodsTransport.FirstWeight > 0 && (double)(this.CurrentGoodsTransport.FirstWeight - (decimal)Hardwarer.Wber.Weight) < commonDAO.GetCommonAppletConfigDouble("地磅仪表_毛皮差"))
                {
                    UpdateLedShow("净重不正确", "联系过磅员");
                    this.voiceSpeaker.Speak("净重异常 ", 2, false);
                    return false;
                }
                if (weighterDAO.SaveGoodsTransport(this.CurrentGoodsTransport.Id, (decimal)Hardwarer.Wber.Weight, DateTime.Now, CommonAppConfig.GetInstance().AppIdentifier))
                {
                    this.CurrentGoodsTransport = commonDAO.SelfDber.Get<CmcsGoodsTransport>(this.CurrentGoodsTransport.Id);

                    FrontGateUp();

                    btnSaveTransport_Goods.Enabled = false;
                    this.CurrentFlowFlag = eFlowFlag.等待离开;

                    UpdateLedShow("  称重完成", "  请下磅");
                    this.voiceSpeaker.Speak("撑众完成请下磅", 2, false);

                    LoadTodayUnFinishGoodsTransport();
                    LoadTodayFinishGoodsTransport();

                    CamareCapturePicture(this.CurrentGoodsTransport.Id);

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
            ResetGoods();
        }

        /// <summary>
        /// 重置信息
        /// </summary>
        void ResetGoods()
        {
            this.timer_Goods_Cancel = true;

            this.CurrentFlowFlag = eFlowFlag.等待车辆;

            this.CurrentAutotruck = null;
            this.CurrentGoodsTransport = null;
            this.CurrentUnFinishTransport = null;

            btnSaveTransport_Goods.Enabled = false;

            FrontGateDown();
            BackGateDown();

            UpdateLedShow("  等待车辆");
            iocControler.GreenLight1();
            iocControler.GreenLight2();
            // 最后重置
            this.CurrentImperfectCar = null;
            ResetCount = 0;
        }

        /// <summary>
        /// 其他物资运输记录业务定时器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_Goods_Tick(object sender, EventArgs e)
        {
            if (this.timer_Goods_Cancel) return;

            timer_Goods.Stop();
            timer_Goods.Interval = 2000;

            try
            {
                switch (this.CurrentFlowFlag)
                {
                    case eFlowFlag.验证信息:
                        #region

                        this.CurrentGoodsTransport = commonDAO.SelfDber.Get<CmcsGoodsTransport>(this.CurrentUnFinishTransport.TransportId);
                        if (this.CurrentGoodsTransport != null)
                        {
                            // 判断路线设置
                            string nextPlace;
                            //if (carTransportDAO.CheckNextTruckInFactoryWay(this.CurrentAutotruck.CarType, this.CurrentGoodsTransport.StepName, "第一次称重|第二次称重", CommonAppConfig.GetInstance().AppIdentifier, out nextPlace))
                            if (true)
                            {
                                if (this.CurrentGoodsTransport.SuttleWeight == 0)
                                {
                                    BackGateUp();
                                    ThinkCamareCapturePicture(this.CurrentGoodsTransport.Id);
                                    this.CurrentFlowFlag = eFlowFlag.等待上磅;

                                    UpdateLedShow(this.CurrentAutotruck.CarNumber, "请上磅");
                                    this.voiceSpeaker.Speak("车牌号 " + this.CurrentAutotruck.CarNumber + " 请上磅", 2, false);
                                }
                                else
                                {
                                    UpdateLedShow(this.CurrentAutotruck.CarNumber, "已称重");
                                    this.voiceSpeaker.Speak("车牌号 " + this.CurrentAutotruck.CarNumber + " 已称重", 2, false);

                                    timer_Goods.Interval = 20000;
                                }
                            }
                            else
                            {
                                UpdateLedShow("路线错误", "禁止通过");
                                this.voiceSpeaker.Speak("路线错误 禁止通过 " + (!string.IsNullOrEmpty(nextPlace) ? "请前往" + nextPlace : ""), 2, false);

                                timer_Goods.Interval = 20000;
                            }
                        }
                        else
                        {
                            commonDAO.SelfDber.Delete<CmcsUnFinishTransport>(this.CurrentUnFinishTransport.Id);
                        }

                        #endregion
                        break;

                    case eFlowFlag.等待上磅:
                        #region

                        // 当地磅仪表重量大于最小称重且来车方向的地感与对射均无信号，则判定车已经完全上磅
                        if (Hardwarer.Wber.Weight >= this.WbMinWeight && !HasCarOnEnterWay())
                        {
                            BackGateDown();

                            this.CurrentFlowFlag = eFlowFlag.等待稳定;
                        }

                        // 降低灵敏度
                        timer_Goods.Interval = 4000;

                        #endregion
                        break;

                    case eFlowFlag.等待稳定:
                        #region

                        // 提高灵敏度
                        timer_Goods.Interval = 1000;

                        btnSaveTransport_Goods.Enabled = this.WbSteady;

                        UpdateLedShow(this.CurrentAutotruck.CarNumber, Hardwarer.Wber.Weight.ToString("#0.######"));

                        if (this.WbSteady)
                        {
                            if (this.AutoHandMode)
                            {
                                // 自动模式
                                if (!SaveGoodsTransport())
                                {
                                    UpdateLedShow(this.CurrentAutotruck.CarNumber, "称重失败");
                                    this.voiceSpeaker.Speak("车牌号 " + this.CurrentAutotruck.CarNumber + " 称重失败，请联系管理员", 2, false);
                                }
                            }
                            else
                            {
                                // 手动模式 
                            }
                        }

                        #endregion
                        break;

                    case eFlowFlag.等待离开:
                        #region

                        // 当前地磅重量小于最小称重且所有地感、对射无信号时重置
                        if (Hardwarer.Wber.Weight < this.WbMinWeight && !HasCarOnLeaveWay()) ResetGoods();

                        // 降低灵敏度
                        timer_Goods.Interval = 4000;

                        #endregion
                        break;
                }

                // 当前地磅重量小于最小称重且所有地感、对射无信号时重置
                if (Hardwarer.Wber.Weight < this.WbMinWeight && !HasCarOnEnterWay() && !HasCarOnLeaveWay() && this.CurrentFlowFlag != eFlowFlag.等待车辆
                    && this.CurrentImperfectCar != null)
                {
                    if (ResetCount > 2)
                        ResetGoods();
                    else
                        ResetCount++;
                }
#if DEBUG
                Log4Neter.Info(Hardwarer.Wber.Weight + "t," + this.InductorCoil1 + this.InductorCoil2 + this.InductorCoil3 + this.InductorCoil4 + this.InfraredSensor1 + this.InfraredSensor2);
#else
#endif
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
            superGridControl1_Goods.PrimaryGrid.DataSource = weighterDAO.GetUnFinishGoodsTransport();
        }

        /// <summary>
        /// 获取指定日期已完成的其他物资记录
        /// </summary>
        void LoadTodayFinishGoodsTransport()
        {
            superGridControl2_Goods.PrimaryGrid.DataSource = weighterDAO.GetFinishedGoodsTransport(DateTime.Now.Date, DateTime.Now.Date.AddDays(1));
        }

        #endregion

        #region 其他函数

        Font directionFont = new Font("微软雅黑", 16);

        Pen redPen1 = new Pen(Color.Red, 1);
        Pen greenPen1 = new Pen(Color.Lime, 1);
        Pen redPen3 = new Pen(Color.Red, 3);
        Pen greenPen3 = new Pen(Color.Lime, 3);

        /// <summary>
        /// 当前仪表重量面板绘制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panCurrentWeight_Paint(object sender, PaintEventArgs e)
        {
            PanelEx panel = sender as PanelEx;

            int height = 12;

            // 绘制地感1
            e.Graphics.DrawLine(this.InductorCoil1 ? redPen3 : greenPen3, 15, 1, 15, height);
            e.Graphics.DrawLine(this.InductorCoil1 ? redPen3 : greenPen3, 15, panel.Height - height, 15, panel.Height - 1);
            // 绘制地感2
            e.Graphics.DrawLine(this.InductorCoil2 ? redPen3 : greenPen3, 25, 1, 25, height);
            e.Graphics.DrawLine(this.InductorCoil2 ? redPen3 : greenPen3, 25, panel.Height - height, 25, panel.Height - 1);
            // 绘制对射1
            e.Graphics.DrawLine(this.InfraredSensor1 ? redPen1 : greenPen1, 35, 1, 35, height);
            e.Graphics.DrawLine(this.InfraredSensor1 ? redPen1 : greenPen1, 35, panel.Height - height, 35, panel.Height - 1);

            // 绘制对射2
            e.Graphics.DrawLine(this.InfraredSensor2 ? redPen1 : greenPen1, panel.Width - 35, 1, panel.Width - 35, height);
            e.Graphics.DrawLine(this.InfraredSensor2 ? redPen1 : greenPen1, panel.Width - 35, panel.Height - height, panel.Width - 35, panel.Height - 1);
            // 绘制地感3
            e.Graphics.DrawLine(this.InductorCoil3 ? redPen3 : greenPen3, panel.Width - 25, 1, panel.Width - 25, height);
            e.Graphics.DrawLine(this.InductorCoil3 ? redPen3 : greenPen3, panel.Width - 25, panel.Height - height, panel.Width - 25, panel.Height - 1);
            // 绘制地感4
            e.Graphics.DrawLine(this.InductorCoil4 ? redPen3 : greenPen3, panel.Width - 15, 1, panel.Width - 15, height);
            e.Graphics.DrawLine(this.InductorCoil4 ? redPen3 : greenPen3, panel.Width - 15, panel.Height - height, panel.Width - 15, panel.Height - 1);

            // 上磅方向
            eDirection direction = eDirection.UnKnow;
            if (this.CurrentImperfectCar != null) direction = this.CurrentImperfectCar.PassWay;
            e.Graphics.DrawString("﹥>", directionFont, direction == eDirection.Way1 ? Brushes.Red : Brushes.Lime, 2, 17);
            e.Graphics.DrawString("<﹤", directionFont, direction == eDirection.Way2 ? Brushes.Red : Brushes.Lime, panel.Width - 47, 17);
        }

        private void superGridControl_BeginEdit(object sender, DevComponents.DotNetBar.SuperGrid.GridEditEventArgs e)
        {
            // 取消进入编辑
            e.Cancel = true;
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

        private void btnCapture1_Click(object sender, EventArgs e)
        {
            string picture1FileName = Path.Combine(SelfVars.CapturePicturePath, string.Format("{0}_{1}.bmp", this.CurrentImperfectCar != null ? this.CurrentImperfectCar.Voucher : "无车牌", DateTime.Now.ToString("yyyyMMddHHmmssff")));
            Hardwarer.Rwer1.Capture(picture1FileName);
        }

        private void btnCapture_Click(object sender, EventArgs e)
        {
            string picture1FileName = Path.Combine(SelfVars.CapturePicturePath, string.Format("{0}_{1}.bmp", this.CurrentImperfectCar != null ? this.CurrentImperfectCar.Voucher : "无车牌", DateTime.Now.ToString("yyyyMMddHHmmssff")));
            Hardwarer.Rwer2.Capture(picture1FileName);
        }

        /// <summary>
        /// 控件重绘时重新预览
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panVideo1_Paint(object sender, PaintEventArgs e)
        {
            //Hardwarer.Rwer1.Preview(this.panVideo1.Handle);
        }

        /// <summary>
        /// 控件重绘时重新预览
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panVideo2_Paint(object sender, PaintEventArgs e)
        {
            //Hardwarer.Rwer2.Preview(this.panVideo2.Handle);
        }
    }
}
