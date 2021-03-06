using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using CMCS.CarTransport.Out.Core;
using System.Threading;
using System.IO;
using CMCS.CarTransport.DAO;
using CMCS.Common.DAO;
using System.IO.Ports;
using CMCS.Common.Utilities;
using LED.YB14;
using CMCS.CarTransport.Out.Enums;
using CMCS.Common.Entities;
using CMCS.Common.Entities.CarTransport;
using CMCS.Common;
using CMCS.CarTransport.Out.Frms.Sys;
using DevComponents.DotNetBar.Controls;
using CMCS.Common.Views;
using DevComponents.DotNetBar.SuperGrid;
using CMCS.Common.Enums;
using CMCS.Common.Entities.Sys;
using CMCS.Common.Entities.Fuel;
using CMCS.Common.Entities.BaseInfo;
using CMCS.CarTransport.Out.Frms.Print;

namespace CMCS.CarTransport.Out.Frms
{
    public partial class FrmOuter : DevComponents.DotNetBar.Metro.MetroForm
    {
        /// <summary>
        /// 窗体唯一标识符
        /// </summary>
        public static string UniqueKey = "FrmOuter";

        public FrmOuter()
        {
            InitializeComponent();
        }

        #region Vars

        CarTransportDAO carTransportDAO = CarTransportDAO.GetInstance();
        OuterDAO outerDAO = OuterDAO.GetInstance();
        CommonDAO commonDAO = CommonDAO.GetInstance();

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

                panCurrentCarNumber.Refresh();

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

                panCurrentCarNumber.Refresh();

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

                panCurrentCarNumber.Refresh();

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

                panCurrentCarNumber.Refresh();

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
                    panCurrentCarNumber.Text = value.Voucher;
                else
                    panCurrentCarNumber.Text = "等待车辆";
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
            FrmDebugConsole.GetInstance().Show();
#else
            //lblFlowFlag.Visible = false;
#endif

            // 默认自动
            sbtnChangeAutoHandMode.Value = true;

            // 重置程序远程控制命令
            commonDAO.ResetAppRemoteControlCmd(CommonAppConfig.GetInstance().AppIdentifier);
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
            });
        }

        /// <summary>
        /// 允许通行
        /// </summary>
        void LetPass()
        {
            if (this.CurrentImperfectCar == null) return;

            if (this.CurrentImperfectCar.PassWay == ePassWay.Way1)
            {
                this.iocControler.Gate1Up();
                this.iocControler.GreenLight1();
            }
            else if (this.CurrentImperfectCar.PassWay == ePassWay.Way2)
            {
                this.iocControler.Gate2Up();
                this.iocControler.GreenLight2();
            }
        }

        /// <summary>
        /// 阻断前行
        /// </summary>
        void LetBlocking()
        {
            if (this.CurrentImperfectCar == null) return;

            if (this.CurrentImperfectCar.PassWay == ePassWay.Way1)
            {
                this.iocControler.Gate1Down();
                this.iocControler.RedLight1();
            }
            else if (this.CurrentImperfectCar.PassWay == ePassWay.Way2)
            {
                this.iocControler.Gate2Down();
                this.iocControler.RedLight2();
            }
        }

        #endregion

        #region 读卡器

        public void Rwer1_OnReadSuccess(string carnumber)
        {
            if (carnumber != "无车牌" && this.CurrentFlowFlag == eFlowFlag.等待车辆)
            {
                passCarQueuer.Enqueue(ePassWay.Way1, carnumber, true);
                this.CurrentFlowFlag = eFlowFlag.验证信息;
                timer1_Tick(null, null);
                UpdateLedShow(carnumber);
                Log4Neter.Info(string.Format("车号识别1识别到车号：{0}", carnumber));
            }
        }

        void Rwer1_OnScanError(Exception ex)
        {
            Log4Neter.Error("读卡器1", ex);
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

        public void Rwer2_OnReadSuccess(string carnumber)
        {
            if (carnumber != "无车牌" && this.CurrentFlowFlag == eFlowFlag.等待车辆)
            {
                passCarQueuer.Enqueue(ePassWay.Way2, carnumber, true);
                this.CurrentFlowFlag = eFlowFlag.验证信息;
                timer1_Tick(null, null);
                UpdateLedShow(carnumber);
                Log4Neter.Info(string.Format("车号识别2识别到车号：{0}", carnumber));
            }
        }

        void Rwer2_OnScanError(Exception ex)
        {
            Log4Neter.Error("读卡器2", ex);
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
            FrmDebugConsole.GetInstance().Output("更新LED1:|" + value1 + "|" + value2 + "|");

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
            FrmDebugConsole.GetInstance().Output("更新LED2:|" + value1 + "|" + value2 + "|");

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

                this.InductorCoil1Port = commonDAO.GetAppletConfigInt32("IO控制器_地感1端口");
                this.InductorCoil2Port = commonDAO.GetAppletConfigInt32("IO控制器_地感2端口");
                this.InductorCoil3Port = commonDAO.GetAppletConfigInt32("IO控制器_地感3端口");
                this.InductorCoil4Port = commonDAO.GetAppletConfigInt32("IO控制器_地感4端口");
                this.InductorCoil3Port = commonDAO.GetAppletConfigInt32("IO控制器_地感3端口");

                // IO控制器
                Hardwarer.Iocer.OnReceived += new IOC.JMDM20DIOV2.JMDM20DIOV2Iocer.ReceivedEventHandler(Iocer_Received);
                Hardwarer.Iocer.OnStatusChange += new IOC.JMDM20DIOV2.JMDM20DIOV2Iocer.StatusChangeHandler(Iocer_StatusChange);
                success = Hardwarer.Iocer.OpenCom(commonDAO.GetAppletConfigInt32("IO控制器_串口"), commonDAO.GetAppletConfigInt32("IO控制器_波特率"), commonDAO.GetAppletConfigInt32("IO控制器_数据位"), (StopBits)commonDAO.GetAppletConfigInt32("IO控制器_停止位"), (Parity)commonDAO.GetAppletConfigInt32("IO控制器_校验位"));
                if (!success) MessageBoxEx.Show("IO控制器连接失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.iocControler = new IocControler(Hardwarer.Iocer);

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
                /*
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
                */
                #endregion

                #region LED控制卡2
                /*
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
                */
                #endregion

                timer1.Enabled = true;
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
                Hardwarer.Iocer.OnReceived -= new IOC.JMDM20DIOV2.JMDM20DIOV2Iocer.ReceivedEventHandler(Iocer_Received);
                Hardwarer.Iocer.OnStatusChange -= new IOC.JMDM20DIOV2.JMDM20DIOV2Iocer.StatusChangeHandler(Iocer_StatusChange);

                Hardwarer.Iocer.CloseCom();
            }
            catch { }
            try
            {
                Hardwarer.Rwer1.Close();
            }
            catch { }
            try
            {
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
                YB14DynamicAreaLeder.SendDeleteDynamicAreasCommand(this.LED2nScreenNo, 1, "");
                YB14DynamicAreaLeder.DeleteScreen(this.LED2nScreenNo);
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

                        if (passCarQueuer.Count > 0) this.CurrentFlowFlag = eFlowFlag.识别车辆;

                        #endregion
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
                        UpdateLedShow(this.CurrentImperfectCar.Voucher);

                        if (this.CurrentAutotruck != null)
                        {
                            if (this.CurrentAutotruck.IsUse == 1)
                            {
                                if (this.CurrentAutotruck.IsInner == 1)
                                {
                                    LetPass();
                                    this.timer_Visit_Cancel = false;
                                    this.CurrentFlowFlag = eFlowFlag.等待离开;
                                    timer_Visit_Tick(null, null);
                                    Log4Neter.Info(string.Format("车牌号：{0} 内部车辆", this.CurrentAutotruck.CarNumber));
                                }
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

                                timer1.Interval = 20000;
                            }
                        }
                        else
                        {
                            UpdateLedShow(this.CurrentImperfectCar.Voucher, "未登记");

                            // 方式一：车号识别
                            this.voiceSpeaker.Speak("车牌号 " + this.CurrentImperfectCar.Voucher + " 未登记，禁止通过", 2, false);

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

                // 来访车辆
                LoadTodayUnFinishVisitTransport();
                LoadTodayFinishVisitTransport();
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

                    txtFuelKindName_BuyFuel.Text = value.FuelKindName;
                    txtMineName_BuyFuel.Text = value.MineName;
                    txtSupplierName_BuyFuel.Text = value.SupplierName;
                    txtTransportCompanyName_BuyFuel.Text = value.TheTransportCompany != null ? value.TheTransportCompany.Name : "";

                    txtGrossWeight_BuyFuel.Text = value.GrossWeight.ToString("F2");
                    txtTicketWeight_BuyFuel.Text = value.TicketWeight.ToString("F2");
                    txtTareWeight_BuyFuel.Text = value.TareWeight.ToString("F2");
                    txtDeductWeight_BuyFuel.Text = value.DeductWeight.ToString("F2");
                    txtSuttleWeight_BuyFuel.Text = value.SuttleWeight.ToString("F2");
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
            FrmUnFinishTransport_Select frm = new FrmUnFinishTransport_Select("order by CreateDate desc");
            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (this.CurrentImperfectCar != null) return;

                if (!HasCarOnCurrentWay())
                    passCarQueuer.Enqueue(ePassWay.UnKnow, frm.Output.CarNumber, false);
                else if (this.InductorCoil1)
                    passCarQueuer.Enqueue(ePassWay.Way1, frm.Output.CarNumber, false);
                else if (this.InductorCoil2)
                    passCarQueuer.Enqueue(ePassWay.Way2, frm.Output.CarNumber, false);

                this.CurrentFlowFlag = eFlowFlag.识别车辆;
            }
        }

        /// <summary>
        /// 保存入厂煤运输记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveTransport_BuyFuel_Click(object sender, EventArgs e)
        {
            if (!SaveBuyFuelTransport()) MessageBoxEx.Show("保存失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                if (outerDAO.SaveBuyFuelTransport(this.CurrentBuyFuelTransport.Id, DateTime.Now))
                {
                    // 打印磅单
                    // TODO

                    btnSaveTransport_BuyFuel.Enabled = false;
                    this.CurrentFlowFlag = eFlowFlag.等待离开;

                    UpdateLedShow("出厂成功", "请离开");

                    if (!this.AutoHandMode) MessageBoxEx.Show("出厂成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadTodayUnFinishBuyFuelTransport();
                    LoadTodayFinishBuyFuelTransport();

                    LetPass();

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

            btnSaveTransport_BuyFuel.Enabled = false;

            LetBlocking();
            UpdateLedShow("  等待车辆");

            // 最后重置
            this.CurrentImperfectCar = null;
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
                                //if (carTransportDAO.CheckNextTruckInFactoryWay(this.CurrentAutotruck.CarType, this.CurrentBuyFuelTransport.StepName, "出厂", CommonAppConfig.GetInstance().AppIdentifier, out nextPlace))
                                if (true)
                                {
                                    if (this.CurrentBuyFuelTransport.SuttleWeight > 0)
                                    {
                                        this.CurrentFlowFlag = eFlowFlag.保存信息;
                                    }
                                    else
                                    {
                                        UpdateLedShow(this.CurrentAutotruck.CarNumber, "称重未完成");
                                        this.voiceSpeaker.Speak("车牌号 " + this.CurrentAutotruck.CarNumber + " 称重未完成", 2, false);

                                        timer_BuyFuel.Interval = 20000;
                                    }
                                }
                                else
                                {
                                    UpdateLedShow("路线错误", "禁止通过");
                                    this.voiceSpeaker.Speak("路线错误 禁止通过 " + (!string.IsNullOrEmpty(nextPlace) ? "请前往" + nextPlace : ""), 2, false);

                                    timer_BuyFuel.Interval = 20000;
                                }
                            }
                            else
                            {
                                commonDAO.SelfDber.Delete<CmcsUnFinishTransport>(this.CurrentUnFinishTransport.Id);
                            }
                        }
                        else
                        {
                            UpdateLedShow(this.CurrentAutotruck.CarNumber, "未排队");
                            this.voiceSpeaker.Speak("车牌号 " + this.CurrentAutotruck.CarNumber + " 未找到排队记录", 2, false);

                            timer_BuyFuel.Interval = 20000;
                        }

                        #endregion
                        break;

                    case eFlowFlag.保存信息:
                        #region

                        if (this.AutoHandMode)
                        {
                            // 自动模式
                            if (!SaveBuyFuelTransport())
                            {
                                UpdateLedShow(this.CurrentAutotruck.CarNumber, "保存失败");
                                this.voiceSpeaker.Speak("车牌号 " + this.CurrentAutotruck.CarNumber + " 保存失败，请联系管理员", 2, false);
                            }
                        }
                        else
                        {
                            // 手动模式 
                        }

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
        /// 获取未完成的入厂煤记录
        /// </summary>
        void LoadTodayUnFinishBuyFuelTransport()
        {
            superGridControl1_BuyFuel.PrimaryGrid.DataSource = outerDAO.GetUnFinishBuyFuelTransport();
        }

        /// <summary>
        /// 获取指定日期已完成的入厂煤记录
        /// </summary>
        void LoadTodayFinishBuyFuelTransport()
        {
            superGridControl2_BuyFuel.PrimaryGrid.DataSource = outerDAO.GetFinishedBuyFuelTransport(DateTime.Now.Date, DateTime.Now.Date.AddDays(1));
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
                    txt_Consignee1.Text = value.SupplierName;
                    txt_TransportCompayName1.Text = value.TransportCompanyName;
                    txt_YBNumber1.Text = value.TransportSalesNum;
                    txt_TransportNo1.Text = value.TransportNo;
                    txtGrossWeight_SaleFuel.Text = value.GrossWeight.ToString("F2");
                    txtTareWeight_SaleFuel.Text = value.TareWeight.ToString("F2");
                    txtSuttleWeight_SaleFuel.Text = value.SuttleWeight.ToString("F2");
                }
                else
                {
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.当前运输记录Id.ToString(), string.Empty);
                    txt_Consignee1.ResetText();
                    txt_TransportCompayName1.ResetText();
                    txt_YBNumber1.ResetText();
                    txt_TransportNo1.ResetText();
                    txtGrossWeight_SaleFuel.ResetText();
                    txtTareWeight_SaleFuel.ResetText();
                    txtSuttleWeight_SaleFuel.ResetText();
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
            FrmUnFinishTransport_Select frm = new FrmUnFinishTransport_Select("order by CreateDate desc");
            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (this.CurrentImperfectCar != null) return;

                if (!HasCarOnCurrentWay())
                    passCarQueuer.Enqueue(ePassWay.UnKnow, frm.Output.CarNumber, false);
                else if (this.InductorCoil1)
                    passCarQueuer.Enqueue(ePassWay.Way1, frm.Output.CarNumber, false);
                else if (this.InductorCoil2)
                    passCarQueuer.Enqueue(ePassWay.Way2, frm.Output.CarNumber, false);

                this.CurrentFlowFlag = eFlowFlag.识别车辆;
            }
        }

        /// <summary>
        /// 保存入厂煤运输记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveTransport_SaleFuel_Click(object sender, EventArgs e)
        {
            if (!SaveSaleFuelTransport()) MessageBoxEx.Show("保存失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                if (outerDAO.SaveSaleFuelTransport(this.CurrentSaleFuelTransport.Id, DateTime.Now))
                {
                    // 打印磅单

                    btnSaveTransport_SaleFuel.Enabled = false;
                    this.CurrentFlowFlag = eFlowFlag.等待离开;

                    UpdateLedShow("出厂成功", "请离开");

                    if (!this.AutoHandMode) MessageBoxEx.Show("出厂成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadTodayUnFinishSaleFuelTransport();
                    LoadTodayFinishSaleFuelTransport();

                    LetPass();

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

            btnSaveTransport_SaleFuel.Enabled = false;

            LetBlocking();
            UpdateLedShow("  等待车辆");

            // 最后重置
            this.CurrentImperfectCar = null;
        }

        /// <summary>
        /// 入厂煤运输记录业务定时器
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

                        if (this.CurrentUnFinishTransport != null)
                        {
                            this.CurrentSaleFuelTransport = commonDAO.SelfDber.Get<CmcsSaleFuelTransport>(this.CurrentUnFinishTransport.TransportId);
                            if (this.CurrentSaleFuelTransport != null)
                            {
                                // 判断路线设置
                                string nextPlace;
                                //if (carTransportDAO.CheckNextTruckInFactoryWay(this.CurrentAutotruck.CarType, this.CurrentSaleFuelTransport.StepName, "出厂", CommonAppConfig.GetInstance().AppIdentifier, out nextPlace))
                                if (true)
                                {
                                    if (this.CurrentSaleFuelTransport.SuttleWeight > 0)
                                    {
                                        this.CurrentFlowFlag = eFlowFlag.保存信息;

                                        UpdateLedShow(this.CurrentAutotruck.CarNumber, "请离开");
                                    }
                                    else
                                    {
                                        UpdateLedShow(this.CurrentAutotruck.CarNumber, "称重未完成");
                                        this.voiceSpeaker.Speak("车牌号 " + this.CurrentAutotruck.CarNumber + " 称重未完成", 2, false);

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
                        }
                        else
                        {
                            UpdateLedShow(this.CurrentAutotruck.CarNumber, "未排队");
                            this.voiceSpeaker.Speak("车牌号 " + this.CurrentAutotruck.CarNumber + " 未找到排队记录", 2, false);

                            timer_SaleFuel.Interval = 20000;
                        }

                        #endregion
                        break;

                    case eFlowFlag.保存信息:
                        #region
                        if (this.AutoHandMode)
                        {
                            // 自动模式
                            if (!SaveSaleFuelTransport())
                            {
                                UpdateLedShow(this.CurrentAutotruck.CarNumber, "保存失败");
                                this.voiceSpeaker.Speak("车牌号 " + this.CurrentAutotruck.CarNumber + " 保存失败，请联系管理员", 2, false);
                            }
                        }
                        else
                        {
                            btnSaveTransport_SaleFuel.Enabled = true;
                            // 手动模式 
                        }

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
        /// 获取未完成的入厂煤记录
        /// </summary>
        void LoadTodayUnFinishSaleFuelTransport()
        {
            superGridControl1_SaleFuel.PrimaryGrid.DataSource = outerDAO.GetUnFinishSaleFuelTransport();
        }

        /// <summary>
        /// 获取指定日期已完成的入厂煤记录
        /// </summary>
        void LoadTodayFinishSaleFuelTransport()
        {
            superGridControl2_SaleFuel.PrimaryGrid.DataSource = outerDAO.GetFinishedSaleFuelTransport(DateTime.Now.Date, DateTime.Now.Date.AddDays(1));
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
                    txtSupplyUnitName_Goods.Text = Dbers.GetInstance().SelfDber.Get<CmcsSupplyReceive>(value.SupplyUnitId).UnitName;
                    txtReceiveUnitName_Goods.Text = Dbers.GetInstance().SelfDber.Get<CmcsSupplyReceive>(value.ReceiveUnitId).UnitName;
                    txtGoodsTypeName_Goods.Text = Dbers.GetInstance().SelfDber.Get<CmcsGoodsType>(value.GoodsTypeId).GoodsName;

                    txtFirstWeight_Goods.Text = value.FirstWeight.ToString("F2");
                    txtSecondWeight_Goods.Text = value.SecondWeight.ToString("F2");
                    txtSuttleWeight_Goods.Text = value.SuttleWeight.ToString("F2");
                }
                else
                {
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
            FrmUnFinishTransport_Select frm = new FrmUnFinishTransport_Select("order by CreateDate desc");
            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (this.CurrentImperfectCar != null) return;

                if (!HasCarOnCurrentWay())
                    passCarQueuer.Enqueue(ePassWay.UnKnow, frm.Output.CarNumber, false);
                else if (this.InductorCoil1)
                    passCarQueuer.Enqueue(ePassWay.Way1, frm.Output.CarNumber, false);
                else if (this.InductorCoil2)
                    passCarQueuer.Enqueue(ePassWay.Way2, frm.Output.CarNumber, false);

                this.CurrentFlowFlag = eFlowFlag.识别车辆;
            }
        }

        /// <summary>
        /// 保存运输记录
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
                if (outerDAO.SaveGoodsTransport(this.CurrentGoodsTransport.Id, DateTime.Now))
                {
                    this.CurrentGoodsTransport = commonDAO.SelfDber.Get<CmcsGoodsTransport>(this.CurrentGoodsTransport.Id);

                    btnSaveTransport_Goods.Enabled = false;
                    this.CurrentFlowFlag = eFlowFlag.等待离开;

                    UpdateLedShow("出厂成功", "请离开");
                    if (!this.AutoHandMode) MessageBoxEx.Show("出厂成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadTodayUnFinishGoodsTransport();
                    LoadTodayFinishGoodsTransport();

                    LetPass();

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

            btnSaveTransport_Goods.Enabled = false;

            LetBlocking();
            UpdateLedShow("  等待车辆");

            // 最后重置
            this.CurrentImperfectCar = null;
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
                        if (this.CurrentUnFinishTransport != null)
                        {
                            this.CurrentGoodsTransport = commonDAO.SelfDber.Get<CmcsGoodsTransport>(this.CurrentUnFinishTransport.TransportId);
                            if (this.CurrentGoodsTransport != null)
                            {
                                // 判断路线设置
                                string nextPlace;
                                //if (carTransportDAO.CheckNextTruckInFactoryWay(this.CurrentAutotruck.CarType, this.CurrentBuyFuelTransport.StepName, "出厂", CommonAppConfig.GetInstance().AppIdentifier, out nextPlace))
                                if (true)
                                {
                                    if (this.CurrentGoodsTransport.SuttleWeight > 0)
                                    {
                                        this.CurrentFlowFlag = eFlowFlag.保存信息;

                                        UpdateLedShow(this.CurrentAutotruck.CarNumber, "请离开");
                                    }
                                    else
                                    {
                                        UpdateLedShow(this.CurrentAutotruck.CarNumber, "称重未完成");
                                        this.voiceSpeaker.Speak("车牌号 " + this.CurrentAutotruck.CarNumber + " 称重未完成", 2, false);

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
                        }
                        else
                        {
                            UpdateLedShow(this.CurrentAutotruck.CarNumber, "未排队");
                            this.voiceSpeaker.Speak("车牌号 " + this.CurrentAutotruck.CarNumber + " 未找到排队记录", 2, false);

                            timer_Goods.Interval = 20000;
                        }

                        #endregion
                        break;

                    case eFlowFlag.保存信息:
                        #region

                        if (this.AutoHandMode)
                        {
                            // 自动模式
                            if (!SaveGoodsTransport())
                            {
                                UpdateLedShow(this.CurrentAutotruck.CarNumber, "保存失败");
                                this.voiceSpeaker.Speak("车牌号 " + this.CurrentAutotruck.CarNumber + " 保存失败，请联系管理员", 2, false);
                            }
                        }
                        else
                        {
                            // 手动模式 
                        }

                        #endregion
                        break;

                    case eFlowFlag.等待离开:
                        #region

                        // 当前道路地感无信号时重置
                        if (!HasCarOnCurrentWay()) ResetGoods();

                        // 降低灵敏度
                        timer_Goods.Interval = 4000;

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
        /// 获取未完成的其他物资记录
        /// </summary>
        void LoadTodayUnFinishGoodsTransport()
        {
            superGridControl1_Goods.PrimaryGrid.DataSource = outerDAO.GetUnFinishGoodsTransport();
        }

        /// <summary>
        /// 获取指定日期已完成的其他物资记录
        /// </summary>
        void LoadTodayFinishGoodsTransport()
        {
            superGridControl2_Goods.PrimaryGrid.DataSource = outerDAO.GetFinishedGoodsTransport(DateTime.Now.Date, DateTime.Now.Date.AddDays(1));
        }

        #endregion

        #region 来访车辆

        bool timer_Visit_Cancel = true;

        CmcsVisitTransport currentVisitTransport;
        /// <summary>
        /// 当前运输记录
        /// </summary>
        public CmcsVisitTransport CurrentVisitTransport
        {
            get { return currentVisitTransport; }
            set
            {
                currentVisitTransport = value;

                if (value != null)
                {
                    txtRemark_Visit.Text = value.Remark;
                }
                else
                {
                    txtRemark_Visit.ResetText();
                }
            }
        }

        /// <summary>
        /// 选择车辆
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectAutotruck_Visit_Click(object sender, EventArgs e)
        {
            FrmUnFinishTransport_Select frm = new FrmUnFinishTransport_Select("order by CreateDate desc");
            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (this.CurrentImperfectCar != null) return;

                if (!HasCarOnCurrentWay())
                    passCarQueuer.Enqueue(ePassWay.UnKnow, frm.Output.CarNumber, false);
                else if (this.InductorCoil1)
                    passCarQueuer.Enqueue(ePassWay.Way1, frm.Output.CarNumber, false);
                else if (this.InductorCoil2)
                    passCarQueuer.Enqueue(ePassWay.Way2, frm.Output.CarNumber, false);

                this.CurrentFlowFlag = eFlowFlag.识别车辆;
            }
        }

        /// <summary>
        /// 保存运输记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveTransport_Visit_Click(object sender, EventArgs e)
        {
            if (!SaveVisitTransport()) MessageBoxEx.Show("保存失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// 保存运输记录
        /// </summary>
        /// <returns></returns>
        bool SaveVisitTransport()
        {
            if (this.CurrentVisitTransport == null) return false;

            try
            {
                // 生成入厂煤排队记录，同时生成批次信息以及采制化三级编码
                if (outerDAO.SaveVisitTransport(this.CurrentVisitTransport.Id, DateTime.Now))
                {
                    btnSaveTransport_Visit.Enabled = false;
                    this.CurrentFlowFlag = eFlowFlag.等待离开;

                    UpdateLedShow("出厂成功", "请离开");
                    if (!this.AutoHandMode) MessageBoxEx.Show("出厂成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadTodayUnFinishVisitTransport();
                    LoadTodayFinishVisitTransport();

                    LetPass();

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
                    case eFlowFlag.验证信息:
                        #region
                        if (this.CurrentUnFinishTransport != null)
                        {
                            this.CurrentVisitTransport = commonDAO.SelfDber.Get<CmcsVisitTransport>(this.CurrentUnFinishTransport.TransportId);
                            if (this.CurrentVisitTransport != null)
                            {
                                this.CurrentFlowFlag = eFlowFlag.保存信息;
                            }
                            else
                            {
                                commonDAO.SelfDber.Delete<CmcsUnFinishTransport>(this.CurrentUnFinishTransport.Id);
                            }
                        }
                        else
                        {
                            UpdateLedShow(this.CurrentAutotruck.CarNumber, "未排队");
                            this.voiceSpeaker.Speak("车牌号 " + this.CurrentAutotruck.CarNumber + " 未找到排队记录", 2, false);

                            timer_Visit.Interval = 20000;
                        }

                        #endregion
                        break;

                    case eFlowFlag.保存信息:
                        #region

                        if (this.AutoHandMode)
                        {
                            // 自动模式
                            if (!SaveVisitTransport())
                            {
                                UpdateLedShow(this.CurrentAutotruck.CarNumber, "保存失败");
                                this.voiceSpeaker.Speak("车牌号 " + this.CurrentAutotruck.CarNumber + " 保存失败，请联系管理员", 2, false);
                            }
                        }
                        else
                        {
                            // 手动模式 
                        }

                        #endregion
                        break;

                    case eFlowFlag.等待离开:
                        #region

                        // 当前道路地感无信号时重置
                        if (!HasCarOnCurrentWay()) ResetVisit();

                        // 降低灵敏度
                        timer_Visit.Interval = 4000;

                        #endregion
                        break;
                }

                // 当前道路地感无信号时重置
                if (!HasCarOnCurrentWay() && this.CurrentFlowFlag != eFlowFlag.等待车辆 && (this.CurrentImperfectCar != null && this.CurrentImperfectCar.IsFromDevice)) ResetVisit();
            }
            catch (Exception ex)
            {
                Log4Neter.Error("timer_BuyFuel_Tick", ex);
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
            superGridControl1_Visit.PrimaryGrid.DataSource = outerDAO.GetUnFinishVisitTransport();
        }

        /// <summary>
        /// 获取指定日期已完成的来访车辆记录
        /// </summary>
        void LoadTodayFinishVisitTransport()
        {
            superGridControl2_Visit.PrimaryGrid.DataSource = outerDAO.GetFinishedVisitTransport(DateTime.Now.Date, DateTime.Now.Date.AddDays(1));
        }

        #endregion

        #region 其他函数

        Pen redPen3 = new Pen(Color.Red, 3);
        Pen greenPen3 = new Pen(Color.Lime, 3);
        Pen greenPen1 = new Pen(Color.Lime, 1);

        /// <summary>
        /// 当前仪表重量面板绘制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panCurrentWeight_Paint(object sender, PaintEventArgs e)
        {
            PanelEx panel = sender as PanelEx;

            // 绘制地感1
            e.Graphics.DrawLine(this.InductorCoil1 ? redPen3 : greenPen3, 15, 10, 15, 30);
            // 绘制地感2                                                               
            e.Graphics.DrawLine(this.InductorCoil2 ? redPen3 : greenPen3, 25, 10, 25, 30);
            // 绘制分割线
            e.Graphics.DrawLine(greenPen1, 5, 34, 35, 34);
            // 绘制地感3
            e.Graphics.DrawLine(this.InductorCoil3 ? redPen3 : greenPen3, 15, 38, 15, 58);
            // 绘制地感4                                                               
            e.Graphics.DrawLine(this.InductorCoil4 ? redPen3 : greenPen3, 25, 38, 25, 58);
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

        private void tsmiPrint_Click(object sender, EventArgs e)
        {
            GridRow gridRow = superGridControl2_BuyFuel.PrimaryGrid.ActiveRow as GridRow;
            if (gridRow == null) return;
            CmcsBuyFuelTransport entity = gridRow.DataItem as CmcsBuyFuelTransport;
            CmcsBuyFuelTransport entity2 = Dbers.GetInstance().SelfDber.Get<CmcsBuyFuelTransport>(entity.Id);
            FrmPrintWeb frm = new FrmPrintWeb(entity2);
            frm.ShowDialog();
        }
    }
}
