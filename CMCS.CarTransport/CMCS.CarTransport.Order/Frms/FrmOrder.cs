using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using CMCS.CarTransport.Order.Core;
using System.Threading;
using System.IO;
using CMCS.CarTransport.DAO;
using CMCS.Common.DAO;
using System.IO.Ports;
using CMCS.Common.Utilities;
using CMCS.CarTransport.Order.Enums;
using CMCS.Common.Entities;
using CMCS.Common.Entities.CarTransport;
using CMCS.Common;
using CMCS.CarTransport.Order.Frms.Sys;
using DevComponents.DotNetBar.Controls;
using CMCS.Common.Views;
using DevComponents.DotNetBar.SuperGrid;
using CMCS.Common.Enums;
using CMCS.Common.Entities.Sys;
using CMCS.Common.Entities.Fuel;
using CMCS.Common.Entities.BaseInfo;

namespace CMCS.CarTransport.Order.Frms
{
    public partial class FrmOrder : DevComponents.DotNetBar.Metro.MetroForm
    {
        /// <summary>
        /// 窗体唯一标识符
        /// </summary>
        public static string UniqueKey = "FrmOrder";

        public FrmOrder()
        {
            InitializeComponent();
        }

        #region Vars

        CarTransportDAO carTransportDAO = CarTransportDAO.GetInstance();
        OrderDAO orderDAO = OrderDAO.GetInstance();
        CommonDAO commonDAO = CommonDAO.GetInstance();

        IocControler iocControler;
        /// <summary>
        /// 语音播报
        /// </summary>
        //VoiceSpeaker voiceSpeaker = new VoiceSpeaker();

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

                btnSelectAutotruck_SaleFuel1.Visible = !value;

                btnSaveTransport_SaleFuel1.Visible = !value;

                btnReset_SaleFuel1.Visible = !value;

                btnSelectAutotruck_SaleFuel2.Visible = !value;

                btnSaveTransport_SaleFuel2.Visible = !value;

                btnReset_SaleFuel2.Visible = !value;
            }
        }

        public static PassCarQueuer passCarQueuer1 = new PassCarQueuer();
        public static PassCarQueuer passCarQueuer2 = new PassCarQueuer();

        ImperfectCar currentImperfectCar1;
        /// <summary>
        /// 识别或选择的车辆凭证
        /// </summary>
        public ImperfectCar CurrentImperfectCar1
        {
            get { return currentImperfectCar1; }
            set
            {
                currentImperfectCar1 = value;

                if (value != null)
                    panCurrentCarNumber.Text = value.Voucher + "|" + panCurrentCarNumber.Text.Split('|')[1];
                else
                    panCurrentCarNumber.Text = "等待车辆|" + panCurrentCarNumber.Text.Split('|')[1];
            }
        }

        ImperfectCar currentImperfectCar2;
        /// <summary>
        /// 识别或选择的车辆凭证
        /// </summary>
        public ImperfectCar CurrentImperfectCar2
        {
            get { return currentImperfectCar2; }
            set
            {
                currentImperfectCar2 = value;

                if (value != null)
                    panCurrentCarNumber.Text = panCurrentCarNumber.Text.Split('|')[0] + "|" + value.Voucher;
                else
                    panCurrentCarNumber.Text = panCurrentCarNumber.Text.Split('|')[0] + "|等待车辆";
            }
        }

        eFlowFlag currentFlowFlag1 = eFlowFlag.等待车辆;
        eFlowFlag currentFlowFlag2 = eFlowFlag.等待车辆;
        /// <summary>
        /// 当前业务流程标识
        /// </summary>
        public eFlowFlag CurrentFlowFlag1
        {
            get { return currentFlowFlag1; }
            set
            {
                currentFlowFlag1 = value;

                lblFlowFlag.Text = value.ToString() + "|" + lblFlowFlag.Text.Split('|')[1];
            }
        }
        /// <summary>
        /// 当前业务流程标识
        /// </summary>
        public eFlowFlag CurrentFlowFlag2
        {
            get { return currentFlowFlag2; }
            set
            {
                currentFlowFlag2 = value;

                lblFlowFlag.Text = lblFlowFlag.Text.Split('|')[0] + "|" + value.ToString();
            }
        }

        CmcsAutotruck currentAutotruck1;
        /// <summary>
        /// 当前车
        /// </summary>
        public CmcsAutotruck CurrentAutotruck1
        {
            get { return currentAutotruck1; }
            set
            {
                currentAutotruck1 = value;

                if (value != null)
                {
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.当前车Id.ToString() + 1, value.Id);
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.当前车号.ToString() + 1, value.CarNumber);

                    txtCarNumber_SaleFuel1.Text = value.CarNumber;
                    this.txtCarNumber_SaleFuel1.Text = value.CarNumber;
                    superTabControl2.SelectedTab = superTabItem_SaleFuel1;

                    CurrentImperfectCar1.Voucher = value.CarNumber;


                }
                else
                {
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.当前车Id.ToString() + 1, string.Empty);
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.当前车号.ToString() + 1, string.Empty);

                    txtCarNumber_SaleFuel1.ResetText();

                    CurrentImperfectCar1.Voucher = null;
                }
            }
        }


        CmcsAutotruck currentAutotruck2;
        /// <summary>
        /// 当前车
        /// </summary>
        public CmcsAutotruck CurrentAutotruck2
        {
            get { return currentAutotruck2; }
            set
            {
                currentAutotruck2 = value;

                if (value != null)
                {
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.当前车Id.ToString() + 2, value.Id);
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.当前车号.ToString() + 2, value.CarNumber);

                    txtCarNumber_SaleFuel2.Text = value.CarNumber;
                    this.txtCarNumber_SaleFuel2.Text = value.CarNumber;
                    superTabControl2.SelectedTab = superTabItem_SaleFuel2;

                    CurrentImperfectCar2.Voucher = value.CarNumber;
                }
                else
                {
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.当前车Id.ToString() + 2, string.Empty);
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.当前车号.ToString() + 2, string.Empty);

                    txtCarNumber_SaleFuel2.ResetText();

                    CurrentImperfectCar1.Voucher = null;
                }
            }
        }

        CmcsUnFinishTransport currentUnFinishTransport1;
        /// <summary>
        /// 当前未完成运输记录1
        /// </summary>
        public CmcsUnFinishTransport CurrentUnFinishTransport1
        {
            get { return currentUnFinishTransport1; }
            set
            {
                currentUnFinishTransport1 = value;
            }
        }

        CmcsUnFinishTransport currentUnFinishTransport2;
        /// <summary>
        /// 当前未完成运输记录2
        /// </summary>
        public CmcsUnFinishTransport CurrentUnFinishTransport2
        {
            get { return currentUnFinishTransport2; }
            set
            {
                currentUnFinishTransport2 = value;
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
        /// 1升杆
        /// </summary>
        void FrontGateUp1()
        {
            if (this.CurrentImperfectCar1 == null) return;

            this.iocControler.Gate1Up();
        }

        /// <summary>
        /// 1降杆
        /// </summary>
        void FrontGateDown1()
        {
            if (this.CurrentImperfectCar1 == null) return;

            this.iocControler.Gate1Down();
        }

        /// <summary>
        /// 2降杆
        /// </summary>
        void FrontGateDown2()
        {
            if (this.CurrentImperfectCar2 == null) return;

            this.iocControler.Gate2Down();
        }


        /// <summary>
        /// 2升杆
        /// </summary>
        void FrontGateUp2()
        {
            if (this.CurrentImperfectCar2 == null) return;

            this.iocControler.Gate2Up();
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
                if (carnumber != "无车牌" && this.CurrentFlowFlag1 == eFlowFlag.等待车辆)
                {
                    passCarQueuer1.Enqueue(ePassWay.Way1, carnumber, true);
                    this.CurrentFlowFlag1 = eFlowFlag.识别车辆;
                    timer1_Tick(null, null);
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
                 if (carnumber != "无车牌" && this.CurrentFlowFlag2 == eFlowFlag.等待车辆)
                 {
                     passCarQueuer2.Enqueue(ePassWay.Way1, carnumber, true);
                     this.CurrentFlowFlag2 = eFlowFlag.识别车辆;
                     timer1_Tick(null, null);
                     Log4Neter.Info(string.Format("车号识别2识别到车号：{0}", carnumber));
                 }
             });
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

                // IO控制器
                Hardwarer.Iocer.OnReceived += new IOC.JMDM20DIOV2.JMDM20DIOV2Iocer.ReceivedEventHandler(Iocer_Received);
                Hardwarer.Iocer.OnStatusChange += new IOC.JMDM20DIOV2.JMDM20DIOV2Iocer.StatusChangeHandler(Iocer_StatusChange);
                success = Hardwarer.Iocer.OpenCom(commonDAO.GetAppletConfigInt32("IO控制器_串口"), commonDAO.GetAppletConfigInt32("IO控制器_波特率"), commonDAO.GetAppletConfigInt32("IO控制器_数据位"), (StopBits)commonDAO.GetAppletConfigInt32("IO控制器_停止位"), (Parity)commonDAO.GetAppletConfigInt32("IO控制器_校验位"));
                if (!success) MessageBoxEx.Show("IO控制器连接失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.iocControler = new IocControler(Hardwarer.Iocer);

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
        }


        private void FrmOrder_FormClosing(object sender, FormClosingEventArgs e)
        {

            // 卸载设备
            UnloadHardware();
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

                try
                {
                    switch (this.CurrentFlowFlag1)
                    {
                        case eFlowFlag.等待车辆:
                            #region

                            #endregion
                            break;

                        case eFlowFlag.识别车辆:
                            #region

                            // 队列中无车时，等待车辆
                            if (passCarQueuer1.Count == 0)
                            {
                                this.CurrentFlowFlag1 = eFlowFlag.等待车辆;
                                break;
                            }

                            this.CurrentImperfectCar1 = passCarQueuer1.Dequeue();

                            // 方式一：根据识别的车牌号查找车辆信息
                            this.CurrentAutotruck1 = carTransportDAO.GetAutotruckByCarNumber(this.CurrentImperfectCar1.Voucher);

                            if (this.CurrentAutotruck1 != null)
                            {
                                if (this.CurrentAutotruck1.IsUse == 1)
                                {
                                    // 查找该车未完成的运输记录
                                    CurrentUnFinishTransport1 = carTransportDAO.GetUnFinishTransportByAutotruckId(this.CurrentAutotruck1.Id);
                                    if (CurrentUnFinishTransport1 != null)
                                    {
                                        this.timer_SaleFuel1_Cancel = false;
                                        this.CurrentFlowFlag1 = eFlowFlag.验证信息;
                                        timer_SaleFuel1_Tick(null, null);
                                    }
                                    else
                                    {
                                        Log4Neter.Info(string.Format("车牌号：{0} 未排队", this.CurrentAutotruck1.CarNumber));
                                    }
                                }
                                else
                                {
                                    //this.voiceSpeaker.Speak("车牌号 " + this.CurrentAutotruck1.CarNumber + " 已停用，禁止通过", 2, false);

                                    timer1.Interval = 20000;
                                }
                            }
                            else
                            {

                                // 方式一：车号识别
                                //this.voiceSpeaker.Speak("车牌号 " + this.CurrentImperfectCar1.Voucher + " 未登记，禁止通过", 2, false);

                                timer1.Interval = 20000;
                            }

                            #endregion
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Log4Neter.Error("timer1_Tick：CurrentFlowFlag1", ex);
                }

                try
                {
                    switch (this.CurrentFlowFlag2)
                    {
                        case eFlowFlag.等待车辆:
                            #region

                            #endregion
                            break;

                        case eFlowFlag.识别车辆:
                            #region

                            // 队列中无车时，等待车辆
                            if (passCarQueuer2.Count == 0)
                            {
                                this.CurrentFlowFlag2 = eFlowFlag.等待车辆;
                                break;
                            }

                            this.CurrentImperfectCar2 = passCarQueuer2.Dequeue();

                            // 方式一：根据识别的车牌号查找车辆信息
                            this.CurrentAutotruck2 = carTransportDAO.GetAutotruckByCarNumber(this.CurrentImperfectCar2.Voucher);

                            if (this.CurrentAutotruck2 != null)
                            {
                                if (this.CurrentAutotruck2.IsUse == 1)
                                {
                                    // 查找该车未完成的运输记录
                                    CurrentUnFinishTransport2 = carTransportDAO.GetUnFinishTransportByAutotruckId(this.CurrentAutotruck2.Id);
                                    if (CurrentUnFinishTransport2 != null)
                                    {
                                        this.timer_SaleFuel2_Cancel = false;
                                        this.CurrentFlowFlag2 = eFlowFlag.验证信息;
                                        timer_SaleFuel2_Tick(null, null);
                                    }
                                }
                                else
                                {
                                    //this.voiceSpeaker.Speak("车牌号 " + this.CurrentAutotruck2.CarNumber + " 已停用，禁止通过", 2, false);

                                    timer2.Interval = 20000;
                                }
                            }
                            else
                            {

                                // 方式一：车号识别
                                //this.voiceSpeaker.Speak("车牌号 " + this.CurrentImperfectCar2.Voucher + " 未登记，禁止通过", 2, false);
                                //// 方式二：刷卡方式
                                //this.voiceSpeaker.Speak("卡号未登记，禁止通过", 2, false);

                                timer2.Interval = 20000;
                            }

                            #endregion
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Log4Neter.Error("timer1_Tick：CurrentFlowFlag2", ex);
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

                // 销售煤 
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

        void LoadTodayUnFinishSaleFuelTransport()
        {
            superGridControl1_SaleFuel.PrimaryGrid.DataSource = orderDAO.GetUnFinishSaleFuelTransport();
            superGridControl3_SaleFuel.PrimaryGrid.DataSource = orderDAO.GetUnFinishSaleFuelTransport();
        }

        void LoadTodayFinishSaleFuelTransport()
        {
            superGridControl2_SaleFuel.PrimaryGrid.DataSource = orderDAO.GetFinishedSaleFuelTransport(DateTime.Now.Date, DateTime.Now.Date.AddDays(1));
            superGridControl4_SaleFuel.PrimaryGrid.DataSource = orderDAO.GetFinishedSaleFuelTransport(DateTime.Now.Date, DateTime.Now.Date.AddDays(1));
        }


        /// <summary>
        /// 1道有车辆在当前道路上
        /// </summary>
        /// <returns></returns>
        bool HasCarOnCurrentWay1()
        {
            if (this.CurrentImperfectCar1 == null) return false;

            return this.InductorCoil1 || this.InductorCoil2;
        }

        /// <summary>
        /// 2道有车辆在当前道路上
        /// </summary>
        /// <returns></returns>
        bool HasCarOnCurrentWay2()
        {
            if (this.CurrentImperfectCar2 == null) return false;

            return this.InductorCoil3 || this.InductorCoil4;
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

        #region 销售煤业务

        bool timer_SaleFuel1_Cancel = true;
        bool timer_SaleFuel2_Cancel = true;

        CmcsSaleFuelTransport currentSaleFuelTransport1;
        /// <summary>
        /// 当前运输记录1
        /// </summary>
        public CmcsSaleFuelTransport CurrentSaleFuelTransport1
        {
            get { return currentSaleFuelTransport1; }
            set
            {
                currentSaleFuelTransport1 = value;

                if (value != null)
                {
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.当前运输记录Id.ToString(), value.Id);

                    try
                    {
                        txt_YBNumber1.Text = value.TransportSalesNum;
                        txt_TransportNo1.Text = value.TransportNo;
                        txt_Consignee1.Text = Dbers.GetInstance().SelfDber.Get<CmcsSupplier>(value.SupplierId).Name;
                        txt_TransportCompayName1.Text = Dbers.GetInstance().SelfDber.Get<CmcsTransportCompany>(value.TransportCompanyId).Name;
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
                else
                {
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.当前运输记录Id.ToString(), string.Empty);
                    txtCarNumber_SaleFuel1.ResetText();
                    txt_YBNumber1.ResetText();
                    txt_TransportNo1.ResetText();
                    txt_Consignee1.ResetText();
                    txt_TransportCompayName1.ResetText();
                    dbi_OutWeight1.ResetText();
                    btnSaveTransport_SaleFuel1.Text = "开始取煤";
                    btnSaveTransport_SaleFuel1.Enabled = false;
                    txt_ReMark1.ResetText();
                }
            }
        }

        CmcsSaleFuelTransport currentSaleFuelTransport2;
        /// <summary>
        /// 当前运输记录2
        /// </summary>
        public CmcsSaleFuelTransport CurrentSaleFuelTransport2
        {
            get { return currentSaleFuelTransport2; }
            set
            {
                currentSaleFuelTransport2 = value;

                if (value != null)
                {
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.当前运输记录Id.ToString(), value.Id);

                    try
                    {
                        txt_YBNumber1.Text = value.TransportSalesNum;
                        txt_TransportNo1.Text = value.TransportNo;
                        txt_Consignee1.Text = Dbers.GetInstance().SelfDber.Get<CmcsSupplier>(value.SupplierId).Name;
                        txt_TransportCompayName1.Text = Dbers.GetInstance().SelfDber.Get<CmcsTransportCompany>(value.TransportCompanyId).Name;
                    }
                    catch (Exception)
                    {

                        throw;
                    }
                }
                else
                {
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.当前运输记录Id.ToString(), string.Empty);
                    txtCarNumber_SaleFuel2.ResetText();
                    txt_YBNumber2.ResetText();
                    txt_TransportNo2.ResetText();
                    txt_Consignee2.ResetText();
                    txt_TransportCompayName2.ResetText();
                    dbi_OutWeight2.ResetText();
                    btnSaveTransport_SaleFuel2.Text = "开始取煤";
                    btnSaveTransport_SaleFuel2.Enabled = false;
                    txt_ReMark2.ResetText();
                }
            }
        }

        /// <summary>
        /// 道路1定时器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_SaleFuel1_Tick(object sender, EventArgs e)
        {
            if (this.timer_SaleFuel1_Cancel) return;

            timer_SaleFuel1.Stop();
            timer_SaleFuel1.Interval = 2000;

            try
            {
                switch (this.CurrentFlowFlag1)
                {
                    case eFlowFlag.验证信息:
                        #region
                        if (this.CurrentUnFinishTransport1 != null)
                        {
                            this.CurrentSaleFuelTransport1 = commonDAO.SelfDber.Get<CmcsSaleFuelTransport>(CurrentUnFinishTransport1.TransportId);
                            if (this.CurrentSaleFuelTransport1 != null)
                            {
                                //// 判断路线设置
                                //string nextPlace;
                                //if (carTransportDAO.CheckNextTruckInFactoryWay(this.CurrentAutotruck1.CarType, this.CurrentSaleFuelTransport1.StepName, "装载", CommonAppConfig.GetInstance().AppIdentifier, out nextPlace))
                                //{
                                //if (CommonAppConfig.GetInstance().AppIdentifier.Contains(this.CurrentSaleFuelTransport1.LoadArea))
                                //{
                                FrontGateUp1();
                                this.CurrentFlowFlag1 = eFlowFlag.等待进入;
                                //}
                                //else
                                //{
                                //    //this.voiceSpeaker.Speak("路线错误 禁止通过 " + (!string.IsNullOrEmpty(this.CurrentSaleFuelTransport1.LoadArea) ? "请前往" + this.CurrentSaleFuelTransport1.LoadArea : ""), 2, false);

                                //    timer_SaleFuel1.Interval = 20000;
                                //}
                                //}
                                //else
                                //{
                                //    //this.voiceSpeaker.Speak("路线错误 禁止通过 " + (!string.IsNullOrEmpty(nextPlace) ? "请前往" + nextPlace : ""), 2, false);

                                //    timer_SaleFuel1.Interval = 20000;
                                //}
                            }
                            else
                            {
                                commonDAO.SelfDber.Delete<CmcsUnFinishTransport>(CurrentUnFinishTransport1.Id);
                            }
                        }
                        else
                        {
                            //this.voiceSpeaker.Speak("未排队 禁止通过 ", 2, false);
                        }
                        #endregion
                        break;
                    case eFlowFlag.等待进入:
                        #region
                        this.CurrentFlowFlag1 = eFlowFlag.正在取煤;
                        // 降低灵敏度
                        timer_SaleFuel1.Interval = 4000;

                        #endregion
                        break;
                    case eFlowFlag.正在取煤:
                        btnSaveTransport_SaleFuel1.Enabled = true;
                        break;
                    case eFlowFlag.取煤完成:
                        break;
                    case eFlowFlag.保存信息:
                        // 提高灵敏度
                        timer_SaleFuel1.Interval = 1000;

                        btnSaveTransport_SaleFuel1.Enabled = true;

                        if (this.AutoHandMode)
                        {
                            //自动模式
                            if (!SaveSaleFuelTransport1())
                            {
                                //this.voiceSpeaker.Speak("车牌号 " + this.CurrentAutotruck1.CarNumber + " 保存失败，请联系管理员", 2, false);
                            }
                            else
                            {
                                this.CurrentFlowFlag1 = eFlowFlag.等待离开;
                            }
                        }
                        else
                        {
                            // 手动模式 
                        }
                        break;
                    case eFlowFlag.等待离开:
                        #region
                        // 所有地感无信号时重置
                        if (!HasCarOnLeaveWay1())
                        {
                            ResetSelFuel1();
                        }
                        // 降低灵敏度
                        timer_SaleFuel1.Interval = 4000;

                        #endregion
                        break;
                }

                // 当前地磅重量小于最小称重且所有地感、对射无信号时重置
                //if (!HasCarOnEnterWay1() && !HasCarOnLeaveWay1() && this.CurrentFlowFlag1 != eFlowFlag.等待车辆
                //    && this.CurrentImperfectCar1 != null) ResetSelFuel1();
            }
            catch (Exception ex)
            {
                Log4Neter.Error("timer_SaleFuel1_Tick", ex);
            }
            finally
            {
                timer_SaleFuel1.Start();
            }
        }

        /// <summary>
        /// 道路2定时器
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_SaleFuel2_Tick(object sender, EventArgs e)
        {
            if (this.timer_SaleFuel2_Cancel) return;

            timer_SaleFuel2.Stop();
            timer_SaleFuel2.Interval = 2000;

            try
            {
                switch (this.CurrentFlowFlag2)
                {
                    case eFlowFlag.验证信息:
                        #region
                        if (this.CurrentUnFinishTransport2 != null)
                        {
                            this.CurrentSaleFuelTransport2 = commonDAO.SelfDber.Get<CmcsSaleFuelTransport>(CurrentUnFinishTransport2.TransportId);
                            if (this.CurrentSaleFuelTransport2 != null)
                            {
                                // 判断路线设置
                                string nextPlace;
                                if (carTransportDAO.CheckNextTruckInFactoryWay(this.CurrentAutotruck2.CarType, this.CurrentSaleFuelTransport2.StepName, "装载", CommonAppConfig.GetInstance().AppIdentifier, out nextPlace))
                                {
                                    if (CommonAppConfig.GetInstance().AppIdentifier.Contains(this.CurrentSaleFuelTransport2.LoadArea))
                                    {
                                        FrontGateUp2();
                                        this.CurrentFlowFlag2 = eFlowFlag.等待进入;
                                    }
                                    else
                                    {
                                        //this.voiceSpeaker.Speak("路线错误 禁止通过 " + (!string.IsNullOrEmpty(this.CurrentSaleFuelTransport2.LoadArea) ? "请前往" + this.CurrentSaleFuelTransport2.LoadArea : ""), 2, false);

                                        timer_SaleFuel2.Interval = 20000;
                                    }
                                }
                                else
                                {
                                    //this.voiceSpeaker.Speak("路线错误 禁止通过 " + (!string.IsNullOrEmpty(nextPlace) ? "请前往" + nextPlace : ""), 2, false);

                                    timer_SaleFuel2.Interval = 20000;
                                }
                            }
                            else
                            {
                                commonDAO.SelfDber.Delete<CmcsUnFinishTransport>(CurrentUnFinishTransport2.Id);
                            }
                        }
                        else
                        {
                            //this.voiceSpeaker.Speak("车牌号 " + this.CurrentAutotruck2.CarNumber + " 未找到排队记录", 2, false);

                            timer_SaleFuel2.Interval = 20000;
                        }

                        #endregion
                        break;
                    case eFlowFlag.等待进入:
                        #region
                        this.CurrentFlowFlag2 = eFlowFlag.正在取煤;
                        // 降低灵敏度
                        timer_SaleFuel2.Interval = 4000;

                        #endregion
                        break;
                    case eFlowFlag.正在取煤:
                        btnSaveTransport_SaleFuel2.Enabled = true;
                        break;
                    case eFlowFlag.取煤完成:
                        break;
                    case eFlowFlag.保存信息:
                        // 提高灵敏度
                        timer_SaleFuel2.Interval = 2000;

                        btnSaveTransport_SaleFuel2.Enabled = true;

                        if (this.AutoHandMode)
                        {
                            // 自动模式
                            if (!SaveSaleFuelTransport2())
                            {
                                //this.voiceSpeaker.Speak("车牌号 " + this.CurrentAutotruck2.CarNumber + " 保存失败，请联系管理员", 2, false);
                            }
                            else
                            {
                                this.CurrentFlowFlag2 = eFlowFlag.等待离开;
                            }
                        }
                        else
                        {
                            // 手动模式 
                        }
                        break;
                    case eFlowFlag.等待离开:
                        #region

                        // 所有地感无信号时重置
                        if (!HasCarOnLeaveWay2())
                        {
                            ResetSelFuel2();
                        }
                        // 降低灵敏度
                        timer_SaleFuel2.Interval = 4000;

                        #endregion
                        break;
                }

                // 当前地磅重量小于最小称重且所有地感、对射无信号时重置
                if (!HasCarOnEnterWay2() && !HasCarOnLeaveWay2() && this.CurrentFlowFlag2 != eFlowFlag.等待车辆
                    && this.CurrentImperfectCar2 != null) ResetSelFuel2();
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
            e.Graphics.DrawLine(this.InductorCoil1 ? redPen3 : greenPen3, 15, 5, 15, panel.Height - 5);
            // 绘制地感2                                                               
            e.Graphics.DrawLine(this.InductorCoil2 ? redPen3 : greenPen3, 25, 5, 25, panel.Height - 5);
            // 绘制地感3
            e.Graphics.DrawLine(this.InductorCoil3 ? redPen3 : greenPen3, panel.Width - 25, 5, panel.Width - 25, panel.Height - 5);
            // 绘制地感4
            e.Graphics.DrawLine(this.InductorCoil4 ? redPen3 : greenPen3, panel.Width - 15, 5, panel.Width - 15, panel.Height - 5);

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

        private void btnSaveTransport_SaleFuel1_Click(object sender, EventArgs e)
        {
            if (btnSaveTransport_SaleFuel1.Text == "开始取煤")
            {
                this.CurrentFlowFlag1 = eFlowFlag.取煤完成;
                btnSaveTransport_SaleFuel1.Text = "结束放煤";
            }
            else if (btnSaveTransport_SaleFuel1.Text == "结束放煤")
            {
                this.CurrentFlowFlag1 = eFlowFlag.保存信息;
                btnSaveTransport_SaleFuel1.Enabled = false;
            }
        }
        /// <summary>
        /// 重置信息1
        /// </summary>
        void ResetSelFuel1()
        {
            this.timer_SaleFuel1_Cancel = true;

            this.CurrentFlowFlag1 = eFlowFlag.等待车辆;

            this.CurrentAutotruck1 = null;
            this.CurrentSaleFuelTransport1 = null;
            this.CurrentUnFinishTransport1 = null;

            btnSaveTransport_SaleFuel1.Enabled = false;

            FrontGateDown1();

            // 最后重置
            this.CurrentImperfectCar1 = null;
        }

        /// <summary>
        /// 重置信息2
        /// </summary>
        void ResetSelFuel2()
        {
            this.timer_SaleFuel2_Cancel = true;

            this.CurrentFlowFlag2 = eFlowFlag.等待车辆;

            this.CurrentAutotruck2 = null;
            this.CurrentSaleFuelTransport2 = null;

            btnSaveTransport_SaleFuel2.Enabled = false;

            FrontGateDown2();

            // 最后重置
            this.CurrentImperfectCar2 = null;
        }

        private void btnSaveTransport_SaleFuel2_Click(object sender, EventArgs e)
        {

            if (btnSaveTransport_SaleFuel2.Text == "开始取煤")
            {
                this.CurrentFlowFlag2 = eFlowFlag.取煤完成;
                btnSaveTransport_SaleFuel2.Text = "结束放煤";
            }
            else if (btnSaveTransport_SaleFuel2.Text == "结束放煤")
            {
                this.CurrentFlowFlag2 = eFlowFlag.保存信息;
                btnSaveTransport_SaleFuel2.Enabled = false;
            }
        }

        /// <summary>
        /// 保存运输记录
        /// </summary>
        /// <returns></returns>
        bool SaveSaleFuelTransport1()
        {
            //FrmNum fn = new FrmNum();
            //if (fn.ShowDialog() != DialogResult.OK)
            //{
            //    return false;
            //}
            ////取煤量
            //decimal weight = fn.OutWeight;
            if (this.CurrentSaleFuelTransport1 == null) return false;

            try
            {
                if (orderDAO.SaveSaleFuelTransport(this.CurrentSaleFuelTransport1.Id, DateTime.Now))
                {
                    this.CurrentSaleFuelTransport1 = commonDAO.SelfDber.Get<CmcsSaleFuelTransport>(this.CurrentSaleFuelTransport1.Id);


                    btnSaveTransport_SaleFuel1.Enabled = false;
                    this.CurrentFlowFlag1 = eFlowFlag.等待离开;
                    //this.voiceSpeaker.Speak("装载完成请离开！", 2, false);

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
        bool SaveSaleFuelTransport2()
        {
            //FrmNum fn = new FrmNum();
            //if (fn.ShowDialog() != DialogResult.OK)
            //{
            //    return false;
            //}
            ////取煤量
            //decimal weight = fn.OutWeight;
            if (this.CurrentSaleFuelTransport2 == null) return false;

            try
            {
                if (orderDAO.SaveSaleFuelTransport(this.CurrentSaleFuelTransport2.Id, DateTime.Now))
                {
                    this.CurrentSaleFuelTransport2 = commonDAO.SelfDber.Get<CmcsSaleFuelTransport>(this.CurrentSaleFuelTransport2.Id);


                    btnSaveTransport_SaleFuel2.Enabled = false;
                    this.CurrentFlowFlag2 = eFlowFlag.等待离开;

                    //this.voiceSpeaker.Speak("装载完成请离开！", 2, false);

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
        /// 有车辆1在下磅的道路上
        /// </summary>
        /// <returns></returns>
        bool HasCarOnLeaveWay1()
        {
            if (this.CurrentImperfectCar1 == null) return false;

            if (this.CurrentImperfectCar1.PassWay == ePassWay.UnKnow)
                return false;
            else if (this.CurrentImperfectCar1.PassWay == ePassWay.Way1)
                return this.InductorCoil2 || this.InductorCoil3 || this.InductorCoil4;

            return true;
        }

        /// <summary>
        /// 有车辆2在下磅的道路上
        /// </summary>
        /// <returns></returns>
        bool HasCarOnLeaveWay2()
        {
            if (this.CurrentImperfectCar2 == null) return false;

            if (this.CurrentImperfectCar2.PassWay == ePassWay.UnKnow)
                return false;
            else if (this.CurrentImperfectCar2.PassWay == ePassWay.Way2)
                return this.InductorCoil3 || this.InductorCoil4;

            return true;
        }

        /// <summary>
        /// 有车辆1在上磅的道路上
        /// </summary>
        /// <returns></returns>
        bool HasCarOnEnterWay1()
        {
            if (this.CurrentImperfectCar1 == null) return false;

            if (this.CurrentImperfectCar1.PassWay == ePassWay.UnKnow)
                return false;
            else if (this.CurrentImperfectCar1.PassWay == ePassWay.Way1)
                return this.InductorCoil1 || this.InductorCoil2;

            return true;
        }

        /// <summary>
        /// 有车辆2在上磅的道路上
        /// </summary>
        /// <returns></returns>
        bool HasCarOnEnterWay2()
        {
            if (this.CurrentImperfectCar2 == null) return false;

            if (this.CurrentImperfectCar2.PassWay == ePassWay.UnKnow)
                return false;
            else if (this.CurrentImperfectCar2.PassWay == ePassWay.Way2)
                return this.InductorCoil3 || this.InductorCoil4;

            return true;
        }

        private void btnSelectAutotruck_SaleFuel1_Click(object sender, EventArgs e)
        {

            FrmUnFinishTransport_Select frm = new FrmUnFinishTransport_Select(" order by CreateDate desc");
            if (frm.ShowDialog() == DialogResult.OK)
            {
                passCarQueuer1.Enqueue(ePassWay.Way1, frm.Output.CarNumber, false);

                this.CurrentFlowFlag1 = eFlowFlag.识别车辆;
            }
        }

        private void btnSelectAutotruck_SaleFuel2_Click(object sender, EventArgs e)
        {
            FrmUnFinishTransport_Select frm = new FrmUnFinishTransport_Select(" order by CreateDate desc");
            if (frm.ShowDialog() == DialogResult.OK)
            {
                passCarQueuer2.Enqueue(ePassWay.Way2, frm.Output.CarNumber, false);

                this.CurrentFlowFlag2 = eFlowFlag.识别车辆;
            }
        }

        private void btnReset_SaleFuel2_Click(object sender, EventArgs e)
        {
            ResetSelFuel2();
        }

        private void btnReset_SaleFuel1_Click(object sender, EventArgs e)
        {
            ResetSelFuel1();
        }

    }
}
