using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using CMCS.CarTransport.DAO;
using CMCS.CarTransport.JxSampler.Core;
using CMCS.CarTransport.JxSampler.Enums;
using CMCS.Common;
using CMCS.Common.DAO;
using CMCS.Common.Entities;
using CMCS.Common.Entities.CarTransport;
using CMCS.Common.Utilities;
using DevComponents.DotNetBar;
using LED.YB14;
using CMCS.Common.Enums;
using CMCS.CarTransport.JxSampler.Frms.Sys;
using CMCS.Common.Entities.Sys;
using CMCS.Common.Entities.Fuel;
using CMCS.Common.Entities.BaseInfo;

namespace CMCS.CarTransport.JxSampler.Frms
{
    public partial class FrmJxSampler : DevComponents.DotNetBar.Metro.MetroForm
    {
        /// <summary>
        /// 窗体唯一标识符
        /// </summary>
        public static string UniqueKey = "FrmCarSampler";

        public FrmJxSampler()
        {
            InitializeComponent();
        }

        #region 业务处理类
        CarTransportDAO carTransportDAO = CarTransportDAO.GetInstance();
        JxSamplerDAO jxSamplerDAO = JxSamplerDAO.GetInstance();
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

        bool affirmCoil = false;
        /// <summary>
        /// 按钮状态 true=有信号  false=无信号
        /// </summary>
        public bool AffirmCoil
        {
            get
            {
                return affirmCoil;
            }
            set
            {

                if (value)
                    this.slightAffirm.LightColor = Color.Green;
                else
                    this.slightAffirm.LightColor = Color.Red;
                if (this.CurrentFlowFlag == eFlowFlag.发送计划 && value)
                    affirmCoil = value;
                else if (this.CurrentFlowFlag != eFlowFlag.发送计划)
                    affirmCoil = value;
                commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.确认按钮.ToString(), value ? "1" : "0");
            }
        }

        int affirmPort;
        /// <summary>
        /// 按钮端口
        /// </summary>
        public int AffirmPort
        {
            get { return affirmPort; }
            set { affirmPort = value; }
        }

        #endregion

        #region 公共Vars

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

                btnSendSamplingPlan.Visible = !value;
                btnSelectAutotruck.Visible = !value;
                btnReset.Visible = !value;
            }
        }

        bool handSend = false;
        /// <summary>
        /// 手动发送= true 自动发送=false 
        /// </summary>
        public bool HandSend
        {
            get { return handSend; }
            set
            {
                handSend = value;
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

        private CmcsBuyFuelTransport currentBuyFuelTransport;
        /// <summary>
        /// 当前入场煤运输记录
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
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, "矿发量", value.TicketWeight.ToString());
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.当前车号.ToString(), value.CarNumber);
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.采样点数.ToString(), dbtxtCYPoint.Text);
                    CmcsSupplier supplier = Dbers.GetInstance().SelfDber.Get<CmcsSupplier>(value.SupplierId);
                    if (supplier != null)
                        txtSupplierName.Text = supplier.Name;
                    CmcsTransportCompany company = Dbers.GetInstance().SelfDber.Get<CmcsTransportCompany>(value.TransportCompanyId);
                    if (company != null)
                        txtTransportCompanyName.Text = company.Name;
                    CmcsMine mine = Dbers.GetInstance().SelfDber.Get<CmcsMine>(value.MineId);
                    if (mine != null)
                        txtMineName.Text = mine.Name; CmcsFuelKind fuelkind = Dbers.GetInstance().SelfDber.Get<CmcsFuelKind>(value.FuelKindId);
                    if (fuelkind != null)
                        txtFuelKindName.Text = fuelkind.FuelName;

                    txtTicketWeight.Text = value.TicketWeight.ToString();
                    txtSamplingType.Text = value.SamplingType;
                }
                else
                {
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.当前运输记录Id.ToString(), string.Empty);
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, "矿发量", string.Empty);
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.当前车号.ToString(), string.Empty);
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.采样点数.ToString(), string.Empty);

                    txtSupplierName.ResetText();
                    txtMineName.ResetText();
                    txtTransportCompanyName.ResetText();
                    txtFuelKindName.ResetText();
                    txtSamplingType.ResetText();
                    txtTicketWeight.ResetText();
                }
            }
        }


        private CmcsSaleFuelTransport currentSaleFuelTransport;
        /// <summary>
        /// 当前出场煤运输记录
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
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.当前车号.ToString(), value.CarNumber);
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.采样点数.ToString(), dbtxtCYPoint.Text);

                    labSupplierName.Text = "接收单位";
                    CmcsSupplier supplier = Dbers.GetInstance().SelfDber.Get<CmcsSupplier>(value.SupplierId);
                    if (supplier != null)
                        txtSupplierName.Text = supplier.Name;
                    txtTransportCompanyName.Text = value.TransportCompanyName;

                    labMineName.Visible = false;
                    txtMineName.Visible = false;

                    CmcsFuelKind fuelkind = Dbers.GetInstance().SelfDber.Get<CmcsFuelKind>(value.FuelKindId);
                    txtFuelKindName.Text = fuelkind.FuelName;

                    labTicketWeight.Text = "出煤量";
                    txtTicketWeight.Text = value.Outweight.ToString();
                    txtSamplingType.Text = "机械采样";
                }
                else
                {
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.当前运输记录Id.ToString(), string.Empty);
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.当前车号.ToString(), string.Empty);
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.采样点数.ToString(), string.Empty);

                    txtSupplierName.ResetText();
                    txtMineName.ResetText();
                    txtTransportCompanyName.ResetText();
                    txtFuelKindName.ResetText();
                    txtSamplingType.ResetText();
                    txtTicketWeight.ResetText();
                }
            }
        }

        private string carnumber;
        /// <summary>
        /// 当前识别车号
        /// </summary>
        public string CarNumber
        {
            get { return carnumber; }
            set
            {
                carnumber = value;
            }
        }

        private string nextCarnumber;
        /// <summary>
        /// 正在采样时识别到的车号
        /// </summary>
        public string NextCarNumber
        {
            get { return nextCarnumber; }
            set
            {
                nextCarnumber = value;
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

                    txtCarNumber.Text = value.CarNumber;
                    panCurrentCarNumber.Text = value.CarNumber;
                }
                else
                {
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.当前车Id.ToString(), string.Empty);
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.当前车号.ToString(), string.Empty);

                    txtCarNumber.ResetText();
                    panCurrentCarNumber.ResetText();
                }

                PreviewCarCarriage(value);
            }
        }

        private InfQCJXCYSampleCMD currentSampleCMD;
        /// <summary>
        /// 当前采样命令
        /// </summary>
        public InfQCJXCYSampleCMD CurrentSampleCMD
        {
            get { return currentSampleCMD; }
            set { currentSampleCMD = value; }
        }

        private CmcsRCSampling currentSample;
        /// <summary>
        /// 当前采样单
        /// </summary>
        public CmcsRCSampling CurrentSample
        {
            get { return currentSample; }
            set { currentSample = value; }
        }

        private eEquInfSamplerSystemStatus samplerSystemStatus;
        /// <summary>
        /// 采样机系统状态
        /// </summary>
        public eEquInfSamplerSystemStatus SamplerSystemStatus
        {
            get { return samplerSystemStatus; }
            set
            {
                samplerSystemStatus = value;

                if (value == eEquInfSamplerSystemStatus.就绪待机 || value == eEquInfSamplerSystemStatus.采样完成)
                    slightSamplerStatus.LightColor = EquipmentStatusColors.BeReady;
                else if (value == eEquInfSamplerSystemStatus.正在运行 || value == eEquInfSamplerSystemStatus.正在卸样)
                    slightSamplerStatus.LightColor = EquipmentStatusColors.Working;
                else //if (value == eEquInfSamplerSystemStatus.发生故障)
                    slightSamplerStatus.LightColor = EquipmentStatusColors.Breakdown;
            }
        }

        /// <summary>
        /// 采样机设备编码
        /// </summary>
        public string SamplerMachineCode;
        /// <summary>
        /// 采样机设备名称
        /// </summary>
        public string SamplerMachineName;

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

            // 采样机设备编码
            //this.SamplerMachineCode = commonDAO.GetAppletConfigString("采样机设备编码");
            this.SamplerMachineCode = CommonAppConfig.GetInstance().AppIdentifier;
            this.SamplerMachineName = commonDAO.GetMachineNameByCode(this.SamplerMachineCode);
            lblSampleStatus.Text = SamplerMachineName;

            // 默认自动
            sbtnChangeAutoHandMode.Value = true;

            // 重置程序远程控制命令
            commonDAO.ResetAppRemoteControlCmd(CommonAppConfig.GetInstance().AppIdentifier);
        }

        private void FrmCarSampler_Load(object sender, EventArgs e)
        {

        }

        private void FrmCarSampler_Shown(object sender, EventArgs e)
        {
            InitHardware();

            InitForm();
        }

        private void FrmCarSampler_FormClosing(object sender, FormClosingEventArgs e)
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

                  this.AffirmCoil = (receiveValue[this.AffirmPort - 1] == 1);
              });
        }

        /// <summary>
        /// 后方升杆
        /// </summary>
        void BackGateUp()
        {
            this.iocControler.Gate1Up();
        }

        /// <summary>
        /// 后方降杆
        /// </summary>
        void BackGateDown()
        {
            if (!this.InductorCoil2)
                this.iocControler.Gate1Down();
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
                    this.txtCarNumber.Text = carnumber;
                    passCarQueuer.Enqueue(CarNumber);
                    this.CurrentFlowFlag = eFlowFlag.验证车辆;
                    this.NextCarNumber = null;//正常状态下识别到车号 下一辆车号为空
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.当前车号.ToString(), carnumber);
                    timer1_Tick(null, null);
                }
                else if (carnumber != "无车牌" && this.CurrentFlowFlag != eFlowFlag.等待车辆)//前车未离开时 后车已进入识别区域
                {
                    this.NextCarNumber = carnumber;
                }
                Log4Neter.Info(string.Format("车号识别1识别到车号：{0}", carnumber));
            });
        }
        #endregion

        #region LED控制卡

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

                slightLED1.LightColor = (_LED1ConnectStatus ? Color.Green : Color.Red);

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
        /// 更新LED动态区域
        /// </summary>
        /// <param name="value1">第一行内容</param>
        /// <param name="value2">第二行内容</param>
        private void UpdateLedShow(string value1 = "", string value2 = "")
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
                this.AffirmPort = commonDAO.GetAppletConfigInt32("IO控制器_确认按钮端口");
                // IO控制器
                Hardwarer.Iocer.OnReceived += new IOC.JMDM20DIOV2.JMDM20DIOV2Iocer.ReceivedEventHandler(Iocer_Received);
                Hardwarer.Iocer.OnStatusChange += new IOC.JMDM20DIOV2.JMDM20DIOV2Iocer.StatusChangeHandler(Iocer_StatusChange);
                success = Hardwarer.Iocer.OpenCom(commonDAO.GetAppletConfigInt32("IO控制器_串口"), commonDAO.GetAppletConfigInt32("IO控制器_波特率"), commonDAO.GetAppletConfigInt32("IO控制器_数据位"), (StopBits)commonDAO.GetAppletConfigInt32("IO控制器_停止位"), (Parity)commonDAO.GetAppletConfigInt32("IO控制器_校验位"));
                if (!success) MessageBoxEx.Show("IO控制器连接失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.iocControler = new IocControler(Hardwarer.Iocer);

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

                #region LED控制卡

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
                                UpdateLedShow("  等待车辆");
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

            }
            catch (Exception ex)
            {
                Log4Neter.Error("设备初始化", ex);
            }
            finally
            {
                timer1.Enabled = true;
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
                YB14DynamicAreaLeder.SendDeleteDynamicAreasCommand(this.LED1nScreenNo, 1, "");
                YB14DynamicAreaLeder.DeleteScreen(this.LED1nScreenNo);
            }
            catch { }
            try
            {
                Hardwarer.Rwer1.Close();
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
            if (this.iocControler != null && !this.InductorCoil2) this.iocControler.Gate1Down();
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

                        if (this.InductorCoil1 && this.NextCarNumber != null)
                        {
                            // 当读卡区域地感有信号，且已经提前读取到下一辆车的信息
                            passCarQueuer.Enqueue(this.NextCarNumber);
                        }

                        if (passCarQueuer.Count > 0) this.CurrentFlowFlag = eFlowFlag.验证车辆;

                        #endregion
                        break;

                    case eFlowFlag.验证车辆:
                        #region

                        // 队列中无车时，等待车辆
                        //if (passCarQueuer.Count == 0)
                        //{
                        //    this.CurrentFlowFlag = eFlowFlag.等待离开;
                        //    break;
                        //}

                        this.CurrentImperfectCar = passCarQueuer.Dequeue();

                        // 根据识别的车牌号查找车辆信息
                        this.CurrentAutotruck = carTransportDAO.GetAutotruckByCarNumber(this.CurrentImperfectCar.Voucher);
                        UpdateLedShow(this.CurrentImperfectCar.Voucher);

                        if (this.CurrentAutotruck != null)
                        {
                            if (this.CurrentAutotruck.IsUse == 1)
                            {
                                if (this.CurrentAutotruck.CarriageLength > 0 && this.CurrentAutotruck.CarriageWidth > 0 && this.CurrentAutotruck.CarriageBottomToFloor > 0)
                                {
                                    // 未完成运输记录
                                    CmcsUnFinishTransport unFinishTransport = carTransportDAO.GetUnFinishTransportByAutotruckId(this.CurrentAutotruck.Id);
                                    if (unFinishTransport != null)
                                    {
                                        if (unFinishTransport.CarType == eTransportType.原料煤入场.ToString() || unFinishTransport.CarType == eTransportType.仓储煤入场.ToString() || unFinishTransport.CarType == eTransportType.中转煤入场.ToString())
                                            this.CurrentBuyFuelTransport = carTransportDAO.GetBuyFuelTransportById(unFinishTransport.TransportId);
                                        else if (unFinishTransport.CarType == eTransportType.仓储煤出场.ToString() || unFinishTransport.CarType == eTransportType.中转煤出场.ToString() || unFinishTransport.CarType == eTransportType.销售直销煤.ToString() || unFinishTransport.CarType == eTransportType.销售掺配煤.ToString())
                                            this.CurrentSaleFuelTransport = carTransportDAO.GetSaleFuelTransportById(unFinishTransport.TransportId);

                                        if (this.CurrentBuyFuelTransport != null || this.CurrentSaleFuelTransport != null)
                                        {
                                            // 判断路线设置
                                            string nextPlace;
                                            if (true)
                                            //if (carTransportDAO.CheckNextTruckInFactoryWay(this.CurrentAutotruck.CarType, this.CurrentBuyFuelTransport.StepName, "采样", CommonAppConfig.GetInstance().AppIdentifier, out nextPlace))
                                            {
                                                BackGateUp();

                                                btnSendSamplingPlan.Enabled = true;

                                                this.CurrentFlowFlag = eFlowFlag.等待驶入;

                                                UpdateLedShow(this.CurrentAutotruck.CarNumber, "驶至采样区域");
                                                this.voiceSpeaker.Speak("车牌号 " + this.CurrentAutotruck.CarNumber + " 驶至采样区域", 2, false);
                                            }
                                            else
                                            {
                                                UpdateLedShow("路线错误", "禁止通过");
                                                this.voiceSpeaker.Speak("路线错误 禁止通过 " + (!string.IsNullOrEmpty(nextPlace) ? "请前往" + nextPlace : ""), 2, false);
                                                timer1.Interval = 5000;
                                                this.CurrentFlowFlag = eFlowFlag.验证失败;
                                            }
                                        }
                                        else
                                        {
                                            this.UpdateLedShow(this.CurrentAutotruck.CarNumber, "未找到运输记录");
                                            this.voiceSpeaker.Speak("车牌号 " + this.CurrentAutotruck.CarNumber + " 未找到运输记录", 2, false);
                                            commonDAO.SelfDber.Delete<CmcsUnFinishTransport>(unFinishTransport.Id);
                                            timer1.Interval = 5000;
                                            this.CurrentFlowFlag = eFlowFlag.验证失败;
                                        }
                                    }
                                    else
                                    {
                                        this.UpdateLedShow(this.CurrentAutotruck.CarNumber, "未排队");
                                        this.voiceSpeaker.Speak("车牌号 " + this.CurrentAutotruck.CarNumber + " 未找到排队记录", 2, false);
                                        timer1.Interval = 5000;
                                        this.CurrentFlowFlag = eFlowFlag.验证失败;
                                    }
                                }
                                else
                                {
                                    this.UpdateLedShow(this.CurrentAutotruck.CarNumber, "车厢未测量");
                                    this.voiceSpeaker.Speak("车牌号 " + this.CurrentAutotruck.CarNumber + " 车厢未测量", 2, false);

                                    timer1.Interval = 5000;
                                    this.CurrentFlowFlag = eFlowFlag.验证失败;
                                }
                            }
                            else
                            {
                                UpdateLedShow(this.CurrentAutotruck.CarNumber, "已停用");
                                this.voiceSpeaker.Speak("车牌号 " + this.CurrentAutotruck.CarNumber + " 已停用，禁止通过", 2, false);
                                timer1.Interval = 5000;
                                this.CurrentFlowFlag = eFlowFlag.验证失败;
                            }
                        }
                        else
                        {
                            UpdateLedShow(this.CurrentImperfectCar.Voucher, "未登记");

                            this.voiceSpeaker.Speak("车牌号 " + this.CurrentImperfectCar.Voucher + " 未登记，禁止通过", 2, false);

                            timer1.Interval = 5000;
                            this.CurrentFlowFlag = eFlowFlag.验证失败;
                        }

                        #endregion
                        break;

                    case eFlowFlag.验证失败:
                        #region MyRegion
                        if (!this.InductorCoil1)
                            ResetBuyFuel();
                        #endregion
                        break;

                    case eFlowFlag.等待驶入:
                        #region

                        // 当入口读卡地感/道闸地感无信号
                        if (!this.InductorCoil1 && !this.InductorCoil2)
                        {
                            BackGateDown();
                            this.CurrentFlowFlag = eFlowFlag.发送计划;
                            this.UpdateLedShow("请下车", "点击确认按钮");
                            this.voiceSpeaker.Speak("司机请下车点击确认按钮", 2, false);
                        }

                        // 提高灵敏度
                        timer1.Interval = 200;

                        #endregion
                        break;

                    case eFlowFlag.发送计划:
                        #region

                        // 采样区域地感,检测车辆是否到指定区域
                        if ((this.AutoHandMode && (!this.InductorCoil1 && !this.InductorCoil2)) || (HandSend && !this.InductorCoil2))
                        {
                            if (HandSend || this.AffirmCoil)
                            {
                                if (this.SamplerSystemStatus == eEquInfSamplerSystemStatus.就绪待机 || this.SamplerSystemStatus == eEquInfSamplerSystemStatus.采样完成)
                                {
                                    this.CurrentSample = carTransportDAO.GetRCSamplingById(this.CurrentBuyFuelTransport != null ? this.CurrentBuyFuelTransport.SamplingId : this.CurrentSaleFuelTransport.SamplingId);
                                    if (this.CurrentSample != null)
                                    {
                                        this.CurrentSampleCMD = new InfQCJXCYSampleCMD()
                                        {
                                            MachineCode = this.SamplerMachineCode,
                                            CarNumber = this.txtCarNumber.Text,
                                            InFactoryBatchId = this.CurrentBuyFuelTransport != null ? this.CurrentBuyFuelTransport.InFactoryBatchId : this.CurrentSaleFuelTransport.InOutBatchId,
                                            SampleCode = this.CurrentSample.SampleCode,
                                            Mt = 0,
                                            // 根据预报
                                            TicketWeight = 0,
                                            // 根据预报
                                            CarCount = 0,
                                            // 采样点数根据相关逻辑计算
                                            PointCount = (int)this.dbtxtCYPoint.Value,
                                            CarriageLength = this.CurrentAutotruck.CarriageLength,
                                            CarriageWidth = this.CurrentAutotruck.CarriageWidth,
                                            CarriageBottomToFloor = this.CurrentAutotruck.CarriageBottomToFloor,
                                            Obstacle1 = this.CurrentAutotruck.LeftObstacle1 + "|" + this.CurrentAutotruck.RightObstacle1,
                                            Obstacle2 = this.CurrentAutotruck.LeftObstacle2 + "|" + this.CurrentAutotruck.RightObstacle2,
                                            Obstacle3 = this.CurrentAutotruck.LeftObstacle3 + "|" + this.CurrentAutotruck.RightObstacle3,
                                            Obstacle4 = this.CurrentAutotruck.LeftObstacle4 + "|" + this.CurrentAutotruck.RightObstacle4,
                                            Obstacle5 = this.CurrentAutotruck.LeftObstacle5 + "|" + this.CurrentAutotruck.RightObstacle5,
                                            Obstacle6 = this.CurrentAutotruck.LeftObstacle6 + "|" + this.CurrentAutotruck.RightObstacle6,
                                            ResultCode = eEquInfCmdResultCode.默认.ToString(),
                                            DataFlag = 0
                                        };

                                        // 发送采样计划
                                        if (commonDAO.SelfDber.Insert<InfQCJXCYSampleCMD>(CurrentSampleCMD) > 0)
                                        {
                                            this.CurrentFlowFlag = eFlowFlag.等待采样;
                                            commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.开始时间.ToString(), DateTime.Now.Date.ToString("hh:mm:ss"));
                                            commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.采样编码.ToString(), CurrentSampleCMD.SampleCode);
                                            commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.采样点数.ToString(), CurrentSampleCMD.PointCount.ToString());
                                        }
                                    }
                                    else
                                    {
                                        this.UpdateLedShow("未找到采样单信息");
                                        this.voiceSpeaker.Speak("未找到采样单信息 请联系管理员", 2, false);

                                        timer1.Interval = 5000;
                                    }
                                }
                                else
                                {
                                    this.UpdateLedShow("采样机未就绪");
                                    this.voiceSpeaker.Speak("采样机未就绪", 1, false);

                                    timer1.Interval = 5000;
                                }
                            }
                        }
                        else
                        {
                            this.UpdateLedShow("准备采样", "远离采样区域");
                            this.voiceSpeaker.Speak("准备采样司机请远离采样区域", 2, false);
                        }

                        #endregion
                        break;

                    case eFlowFlag.等待采样:
                        #region
                        UpdateLedShow("  " + this.CurrentAutotruck.CarNumber, "  正在采样");
                        // 判断采样是否完成
                        InfQCJXCYSampleCMD qCJXCYSampleCMD = commonDAO.SelfDber.Get<InfQCJXCYSampleCMD>(this.CurrentSampleCMD.Id);
                        if (qCJXCYSampleCMD.ResultCode == eEquInfCmdResultCode.成功.ToString())
                        {
                            if (this.CurrentBuyFuelTransport != null)
                            {
                                if (jxSamplerDAO.SaveBuyFuelTransport(this.CurrentBuyFuelTransport.Id, DateTime.Now, CommonAppConfig.GetInstance().AppIdentifier))
                                {
                                    this.UpdateLedShow("  采样完成", "   请离开");
                                    this.voiceSpeaker.Speak("采样完成请离开采样区域", 2, false);

                                    this.CurrentFlowFlag = eFlowFlag.等待离开;
                                    //采样完成后可以重置，开元控制道闸让车辆离开
                                }
                            }
                            else if (this.CurrentSaleFuelTransport != null)
                            {
                                if (jxSamplerDAO.SaveSaleFuelTransport(this.CurrentSaleFuelTransport.Id, DateTime.Now, CommonAppConfig.GetInstance().AppIdentifier))
                                {
                                    this.UpdateLedShow("  采样完成", "   请离开");
                                    this.voiceSpeaker.Speak("采样完成请离开采样区域", 2, false);

                                    this.CurrentFlowFlag = eFlowFlag.等待离开;
                                    //采样完成后可以重置，开元控制道闸让车辆离开
                                }
                            }
                        }

                        // 降低灵敏度
                        timer1.Interval = 4000;

                        #endregion
                        break;

                    case eFlowFlag.等待离开:
                        #region
                        InfQCJXCYSampleCMD qCJXCYSampleCMD1 = commonDAO.SelfDber.Get<InfQCJXCYSampleCMD>(this.CurrentSampleCMD.Id);
                        if (qCJXCYSampleCMD1.ResultCode == eEquInfCmdResultCode.成功.ToString())
                        {
                            ResetBuyFuel();
                        }
                        // 降低灵敏度
                        timer1.Interval = 4000;

                        #endregion
                        break;
                }

                // 所有地感无信号/运输记录为空时重置
                if (this.AutoHandMode && !this.InductorCoil1 && !this.InductorCoil2 && this.CurrentFlowFlag != eFlowFlag.等待车辆
                    && this.CurrentBuyFuelTransport == null && this.CurrentSaleFuelTransport == null) ResetBuyFuel();
                commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.采样流程状态.ToString(), this.CurrentFlowFlag.ToString());
                RefreshEquStatus();
            }
            catch (Exception ex)
            {
                Log4Neter.Error("timer1_Tick", ex);
            }
            finally
            {
                timer1.Start();
            }

            timer1.Start();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            timer2.Stop();
            // 慢速任务
            timer2.Interval = 180000;

            try
            {
                // 入场煤
                LoadTodayUnFinishBuyFuelTransport();
                LoadTodayFinishBuyFuelTransport();
                //出场煤
                LoadTodayUnFinishSaleFuelTransport();
                LoadTodayFinishSaleFuelTransport();
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
        /// 切换手动/自动模式
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sbtnChangeAutoHandMode_ValueChanged(object sender, EventArgs e)
        {
            this.AutoHandMode = sbtnChangeAutoHandMode.Value;
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

        #endregion

        #region 入厂煤业务

        /// <summary>
        /// 发送采样计划
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSendSamplingPlan_Click(object sender, EventArgs e)
        {
            if (SendSamplingPlan()) MessageBoxEx.Show("发送失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// 发送采样计划
        /// </summary>
        /// <returns></returns>
        bool SendSamplingPlan()
        {
            if (this.CurrentBuyFuelTransport == null && this.CurrentSaleFuelTransport == null)
            {
                MessageBoxEx.Show("请选择车辆", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            HandSend = true;
            this.CurrentFlowFlag = eFlowFlag.发送计划;

            return false;
        }

        /// <summary>
        /// 重置入厂煤运输记录
        /// </summary>
        void ResetBuyFuel()
        {
            this.CurrentFlowFlag = eFlowFlag.等待车辆;

            this.CarNumber = null;
            if (!InductorCoil1) this.NextCarNumber = null;
            this.CurrentAutotruck = null;
            this.CurrentBuyFuelTransport = null;
            this.CurrentSaleFuelTransport = null;
            this.CurrentSample = null;
            this.HandSend = false;
            btnSendSamplingPlan.Enabled = false;

            BackGateDown();

            UpdateLedShow("  等待车辆");

            // 最后重置
            this.CurrentImperfectCar = null;
        }

        /// <summary>
        /// 重置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetBuyFuel();
        }

        /// <summary>
        /// 刷新数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefres_Click(object sender, EventArgs e)
        {
            timer2_Tick(null, null);
        }

        /// <summary>
        /// 获取未完成的入厂煤记录
        /// </summary>
        void LoadTodayUnFinishBuyFuelTransport()
        {
            superGridControl1.PrimaryGrid.DataSource = jxSamplerDAO.GetUnFinishBuyFuelTransport();
        }

        /// <summary>
        /// 获取指定日期已完成的出厂煤记录
        /// </summary>
        void LoadTodayFinishBuyFuelTransport()
        {
            superGridControl2.PrimaryGrid.DataSource = jxSamplerDAO.GetFinishedBuyFuelTransport(DateTime.Now.Date, DateTime.Now.Date.AddDays(1));
        }

        /// <summary>
        /// 获取未完成的出厂煤记录
        /// </summary>
        void LoadTodayUnFinishSaleFuelTransport()
        {
            superGridControl1_SaleFuel.PrimaryGrid.DataSource = QueuerDAO.GetInstance().GetUnFinishSaleFuelTransport();
        }

        /// <summary>
        /// 获取指定日期已完成的入厂煤记录
        /// </summary>
        void LoadTodayFinishSaleFuelTransport()
        {
            superGridControl2_SaleFuel.PrimaryGrid.DataSource = QueuerDAO.GetInstance().GetFinishedSaleFuelTransport(DateTime.Now.Date, DateTime.Now.Date.AddDays(1));
        }

        #endregion

        #region 基础信息

        /// <summary>
        /// 选择车辆
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectAutotruck_Click(object sender, EventArgs e)
        {
            FrmUnFinishTransport_Select frm = new FrmUnFinishTransport_Select(" order by CreateDate desc");
            if (frm.ShowDialog() == DialogResult.OK)
            {
                passCarQueuer.Enqueue(frm.Output.CarNumber);
                this.CurrentFlowFlag = eFlowFlag.验证车辆;
            }
        }

        #endregion

        #region 其他

        Pen redPen3 = new Pen(Color.Red, 3);
        Pen greenPen3 = new Pen(Color.Lime, 3);

        /// <summary>
        /// 当前车号面板绘制
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panCurrentCarNumber_Paint(object sender, PaintEventArgs e)
        {
            PanelEx panel = sender as PanelEx;

            int height = 12;

            // 绘制地感1
            e.Graphics.DrawLine(this.InductorCoil1 ? redPen3 : greenPen3, 15, 1, 15, height);
            e.Graphics.DrawLine(this.InductorCoil1 ? redPen3 : greenPen3, 15, panel.Height - height, 15, panel.Height - 1);
            // 绘制地感2
            e.Graphics.DrawLine(this.InductorCoil2 ? redPen3 : greenPen3, 25, 1, 25, height);
            e.Graphics.DrawLine(this.InductorCoil2 ? redPen3 : greenPen3, 25, panel.Height - height, 25, panel.Height - 1);
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

        /// <summary>
        /// 更新采样机状态
        /// </summary>
        private void RefreshEquStatus()
        {
            string systemStatus = commonDAO.GetSignalDataValue(this.SamplerMachineCode, eSignalDataName.设备状态.ToString());
            eEquInfSamplerSystemStatus result;
            if (Enum.TryParse(systemStatus, out result)) SamplerSystemStatus = result;
        }

        /// <summary>
        /// 预览车辆拉筋信息图
        /// </summary>
        /// <param name="autotruck"></param>
        private void PreviewCarCarriage(CmcsAutotruck autotruck)
        {
            pboxMeiChe.BackgroundImageLayout = ImageLayout.Stretch;

            if (autotruck != null && autotruck.CarriageLength > 0 && autotruck.CarriageWidth > 0)
            {
                Bitmap res = new Bitmap(CMCS.CarTransport.JxSampler.Properties.Resources.Autotruck);
                PreviewCarBmp carBmp = new PreviewCarBmp(autotruck);
                pboxMeiChe.BackgroundImage = carBmp.GetPreviewBitmap(res, res.Width, res.Height);
            }
            else
            {
                pboxMeiChe.BackgroundImage = CMCS.CarTransport.JxSampler.Properties.Resources.Autotruck;
            }
        }

        #endregion

    }
}
