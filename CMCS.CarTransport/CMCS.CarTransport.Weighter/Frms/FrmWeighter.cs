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
        /// ����Ψһ��ʶ��
        /// </summary>
        public static string UniqueKey = "FrmWeighter";

        public FrmWeighter()
        {
            InitializeComponent();
        }

        #region ҵ������
        CarTransportDAO carTransportDAO = CarTransportDAO.GetInstance();
        WeighterDAO weighterDAO = WeighterDAO.GetInstance();
        CommonDAO commonDAO = CommonDAO.GetInstance();
        #endregion

        #region �豸����

        IocControler iocControler;
        /// <summary>
        /// ��������
        /// </summary>
        VoiceSpeaker voiceSpeaker = new VoiceSpeaker();

        bool inductorCoil1 = false;
        /// <summary>
        /// �ظ�1״̬ true=���ź�  false=���ź�
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

                commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.�ظ�1�ź�.ToString(), value ? "1" : "0");
            }
        }

        int inductorCoil1Port;
        /// <summary>
        /// �ظ�1�˿�
        /// </summary>
        public int InductorCoil1Port
        {
            get { return inductorCoil1Port; }
            set { inductorCoil1Port = value; }
        }

        bool inductorCoil2 = false;
        /// <summary>
        /// �ظ�2״̬ true=���ź�  false=���ź�
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

                commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.�ظ�2�ź�.ToString(), value ? "1" : "0");
            }
        }

        int inductorCoil2Port;
        /// <summary>
        /// �ظ�2�˿�
        /// </summary>
        public int InductorCoil2Port
        {
            get { return inductorCoil2Port; }
            set { inductorCoil2Port = value; }
        }

        bool inductorCoil3 = false;
        /// <summary>
        /// �ظ�3״̬ true=���ź�  false=���ź�
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

                commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.�ظ�3�ź�.ToString(), value ? "1" : "0");
            }
        }

        int inductorCoil3Port;
        /// <summary>
        /// �ظ�3�˿�
        /// </summary>
        public int InductorCoil3Port
        {
            get { return inductorCoil3Port; }
            set { inductorCoil3Port = value; }
        }

        bool inductorCoil4 = false;
        /// <summary>
        /// �ظ�4״̬ true=���ź�  false=���ź�
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

                commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.�ظ�4�ź�.ToString(), value ? "1" : "0");
            }
        }

        int inductorCoil4Port;
        /// <summary>
        /// �ظ�4�˿�
        /// </summary>
        public int InductorCoil4Port
        {
            get { return inductorCoil4Port; }
            set { inductorCoil4Port = value; }
        }

        bool infraredSensor1 = false;
        /// <summary>
        /// ����1״̬ true=�ڵ�  false=��ͨ
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

                commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.����1�ź�.ToString(), value ? "1" : "0");
            }
        }

        int infraredSensor1Port;
        /// <summary>
        /// ����1�˿�
        /// </summary>
        public int InfraredSensor1Port
        {
            get { return infraredSensor1Port; }
            set { infraredSensor1Port = value; }
        }

        bool infraredSensor2 = false;
        /// <summary>
        /// ����2״̬ true=�ڵ�  false=��ͨ
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

                commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.����2�ź�.ToString(), value ? "1" : "0");
            }
        }

        int infraredSensor2Port;
        /// <summary>
        /// ����2�˿�
        /// </summary>
        public int InfraredSensor2Port
        {
            get { return infraredSensor2Port; }
            set { infraredSensor2Port = value; }
        }

        bool wbSteady = false;
        /// <summary>
        /// �ذ��Ǳ��ȶ�״̬
        /// </summary>
        public bool WbSteady
        {
            get { return wbSteady; }
            set
            {
                wbSteady = value;

                this.panCurrentWeight.Style.ForeColor.Color = (value ? Color.Lime : Color.Red);

                panCurrentWeight.Refresh();

                commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.�ذ��Ǳ�_�ȶ�.ToString(), value ? "1" : "0");
            }
        }

        double wbMinWeight = 0;
        /// <summary>
        /// �ذ��Ǳ���С���� ��λ����
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

        #region ����Vars

        /// <summary>
        /// Ԥ�����
        /// </summary>
        System.Timers.Timer timer_EarlyWarning = new System.Timers.Timer();

        /// <summary>
        /// ��ʱԤ������
        /// </summary>
        static int OverTime_EarlyWarningCount = 0;

        /// <summary>
        /// ���ü���
        /// </summary>
        static int ResetCount = 0;

        /// <summary>
        /// �ȴ��ϴ���ץ��
        /// </summary>
        Queue<string> waitForUpload = new Queue<string>();

        bool autoHandMode = true;
        /// <summary>
        /// �Զ�ģʽ=true  �ֶ�ģʽ=false
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
        /// ʶ���ѡ��ĳ���ƾ֤
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
                    //ʶ�𵽳�����ʼԤ�����
                    //timer_EarlyWarning.Enabled = true;
                }
                else
                {
                    panCurrentCarNumber.Text = "�ȴ�����";
                    //���ú� ����Ԥ�����
                    //timer_EarlyWarning.Enabled = false;
                    //commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.��ʱԤ��.ToString(), "0");
                    //OverTime_EarlyWarningCount = 0;
                }
            }
        }

        eFlowFlag currentFlowFlag = eFlowFlag.�ȴ�����;
        /// <summary>
        /// ��ǰҵ�����̱�ʶ
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
        /// ��ǰ��
        /// </summary>
        public CmcsAutotruck CurrentAutotruck
        {
            get { return currentAutotruck; }
            set
            {
                currentAutotruck = value;

                if (value != null)
                {
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.��ǰ��Id.ToString(), value.Id);
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.��ǰ����.ToString(), value.CarNumber);

                    panCurrentCarNumber.Text = value.CarNumber;
                }
                else
                {
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.��ǰ��Id.ToString(), string.Empty);
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.��ǰ����.ToString(), string.Empty);

                    txtCarNumber_BuyFuel.ResetText();
                    txtCarNumber_SaleFuel.ResetText();
                    txtCarNumber_Goods.ResetText();

                    panCurrentCarNumber.ResetText();
                }
            }
        }


        static CmcsUnFinishTransport currentUnFinishTransport;
        /// <summary>
        /// ��ǰδ��������¼
        /// </summary>
        public CmcsUnFinishTransport CurrentUnFinishTransport
        {
            get { return currentUnFinishTransport; }
            set
            {
                currentUnFinishTransport = value;
                if (value != null)
                {
                    if (value.CarType == eTransportType.ԭ��ú�볡.ToString() || value.CarType == eTransportType.�ִ�ú�볡.ToString() || value.CarType == eTransportType.��תú�볡.ToString())
                    {
                        txtCarNumber_BuyFuel.Text = this.CurrentAutotruck.CarNumber;
                        superTabControl2.SelectedTab = superTabItem_BuyFuel;
                    }
                    else if (value.CarType == eTransportType.�ִ�ú����.ToString() || value.CarType == eTransportType.��תú����.ToString() || value.CarType == eTransportType.���۲���ú.ToString() || value.CarType == eTransportType.����ֱ��ú.ToString())
                    {
                        txtCarNumber_SaleFuel.Text = this.CurrentAutotruck.CarNumber;
                        superTabControl2.SelectedTab = superTabItem_SaleFuel;
                    }
                    else if (value.CarType == eTransportType.��������.ToString())
                    {
                        txtCarNumber_Goods.Text = this.CurrentAutotruck.CarNumber;
                        superTabControl2.SelectedTab = superTabItem_Goods;
                    }
                }
            }
        }

        #endregion

        /// <summary>
        /// �����ʼ��
        /// </summary>
        private void InitForm()
        {
#if DEBUG
            lblFlowFlag.Visible = true;
            FrmDebugConsole.GetInstance(this).Show();
#else
            //lblFlowFlag.Visible = false;
#endif
            if (commonDAO.GetAppletConfigString("˫���") != "1")
            {
                slightLED2.Visible = false;
                labelX7.Visible = false;
                slightRwer2.Visible = false;
                labelX4.Visible = false;
                panVideo2.Visible = false;
            }
            string inFactoryType = commonDAO.GetAppletConfigString("�볧����");
            if (string.IsNullOrEmpty(inFactoryType))
            {
                if (inFactoryType == "�볧ú")
                    this.superTabControl2.SelectedTab = superTabItem_BuyFuel;
                else if (inFactoryType == "����ú")
                    this.superTabControl2.SelectedTab = superTabItem_SaleFuel;
            }
            // Ĭ���Զ�
            sbtnChangeAutoHandMode.Value = true;

            // ���ó���Զ�̿�������
            commonDAO.ResetAppRemoteControlCmd(CommonAppConfig.GetInstance().AppIdentifier);

            //timer_EarlyWarning.Interval = 1000;
            //timer_EarlyWarning.Elapsed += timer_EarlyWarning_Elapsed;
        }

        void timer_EarlyWarning_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (OverTime_EarlyWarningCount > commonDAO.GetCommonAppletConfigInt32("�볧����Ԥ��ʱ��"))
            {
                commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.��ʱԤ��.ToString(), "1");
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
            // ж���豸
            UnloadHardware();

        }

        #region �豸���

        #region IO������

        void Iocer_StatusChange(bool status)
        {
            // �����豸״̬ 
            InvokeEx(() =>
            {
                slightIOC.LightColor = (status ? Color.Green : Color.Red);

                commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.IO������_����״̬.ToString(), status ? "1" : "0");
            });
        }

        /// <summary>
        /// IO��������������ʱ����
        /// </summary>
        /// <param name="receiveValue"></param>
        void Iocer_Received(int[] receiveValue)
        {
            // ���յظ�״̬  
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
        /// ǰ������
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
        /// ǰ������
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
        /// ������
        /// </summary>
        void BackGateUp()
        {
            if (this.CurrentImperfectCar == null) return;

            if (this.CurrentImperfectCar.PassWay == eDirection.Way1)
            {
                this.iocControler.Gate1Up();
                this.iocControler.GreenLight1();
                Log4Neter.Info(string.Format("���ƺţ�{0} ����1����բ", this.CurrentAutotruck != null ? this.CurrentAutotruck.CarNumber : "�޳���"));
            }
            else if (this.CurrentImperfectCar.PassWay == eDirection.Way2)
            {
                this.iocControler.Gate2Up();
                this.iocControler.GreenLight2();
                Log4Neter.Info(string.Format("���ƺţ�{0} ����2����բ", this.CurrentAutotruck != null ? this.CurrentAutotruck.CarNumber : "�޳���"));
            }
        }

        /// <summary>
        /// �󷽽���
        /// </summary>
        void BackGateDown()
        {
            if (this.CurrentImperfectCar == null) return;

            if (this.CurrentImperfectCar.PassWay == eDirection.Way1 && !HasCarOnEnterWay())
            {
                this.iocControler.Gate1Down();
                this.iocControler.RedLight1();
                Log4Neter.Info(string.Format("���ƺţ�{0} ����1����բ", this.CurrentAutotruck != null ? this.CurrentAutotruck.CarNumber : "�޳���"));
            }
            else if (this.CurrentImperfectCar.PassWay == eDirection.Way2 && !HasCarOnEnterWay())
            {
                this.iocControler.Gate2Down();
                this.iocControler.RedLight2();
                Log4Neter.Info(string.Format("���ƺţ�{0} ����2����բ", this.CurrentAutotruck != null ? this.CurrentAutotruck.CarNumber : "�޳���"));
            }
        }

        #endregion

        #region ����ʶ��

        void Rwer1_OnScanError(Exception ex)
        {
            Log4Neter.Error("����ʶ��1", ex);
        }

        void Rwer1_OnStatusChange(bool status)
        {
            // �����豸״̬ 
            InvokeEx(() =>
            {
                slightRwer1.LightColor = (status ? Color.Green : Color.Red);

                commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.����ʶ��1_����״̬.ToString(), status ? "1" : "0");
            });
        }

        public void Rwer1_OnReadSuccess(string carnumber)
        {
            InvokeEx(() =>
            {
                if (carnumber != "�޳���" && this.CurrentFlowFlag == eFlowFlag.�ȴ�����)
                {
                    this.CarNumber = carnumber;
                    passCarQueuer.Enqueue(eDirection.Way1, CarNumber);
                    this.CurrentFlowFlag = eFlowFlag.ʶ����;
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.�ϰ�����.ToString(), "1");
                    timer1_Tick(null, null);
                    UpdateLedShow(carnumber);
                    Log4Neter.Info(string.Format("����ʶ��1ʶ�𵽳��ţ�{0}", carnumber));
                }
            });
        }

        void Rwer2_OnScanError(Exception ex)
        {
            Log4Neter.Error("����ʶ��2", ex);
        }

        void Rwer2_OnStatusChange(bool status)
        {
            // �����豸״̬ 
            InvokeEx(() =>
            {
                slightRwer2.LightColor = (status ? Color.Green : Color.Red);

                commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.����ʶ��2_����״̬.ToString(), status ? "1" : "0");
            });
        }
        public void Rwer2_OnReadSuccess(string carnumber)
        {
            InvokeEx(() =>
             {
                 if (carnumber != "�޳���" && this.CurrentFlowFlag == eFlowFlag.�ȴ�����)
                 {
                     this.CarNumber = carnumber;
                     passCarQueuer.Enqueue(eDirection.Way2, CarNumber);
                     this.CurrentFlowFlag = eFlowFlag.ʶ����;
                     commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.�ϰ�����.ToString(), "0");
                     timer1_Tick(null, null);
                     UpdateLedShow(carnumber);
                     Log4Neter.Info(string.Format("����ʶ��2ʶ�𵽳��ţ�{0}", carnumber));
                 }
             });
        }

        #endregion

        #region LED��ʾ��

        /// <summary>
        /// ����12�ֽڵ��ı�����
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
        /// ����LED��̬����
        /// </summary>
        /// <param name="value1">��һ������</param>
        /// <param name="value2">�ڶ�������</param>
        private void UpdateLedShow(string value1 = "", string value2 = "")
        {
            UpdateLed1Show(value1, value2);
            UpdateLed2Show(value1, value2);
        }

        #region LED1���ƿ�

        /// <summary>
        /// LED1���ƿ�����
        /// </summary>
        int LED1nScreenNo = 1;
        /// <summary>
        /// LED1��̬�����
        /// </summary>
        int LED1DYArea_ID = 1;
        /// <summary>
        /// LED1���±�ʶ
        /// </summary>
        bool LED1m_bSendBusy = false;

        private bool _LED1ConnectStatus;
        /// <summary>
        /// LED1����״̬
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

                commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.LED��1_����״̬.ToString(), value ? "1" : "0");
            }
        }

        /// <summary>
        /// LED1��ʾ�����ı�
        /// </summary>
        string LED1TempFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Led1TempFile.txt");

        /// <summary>
        /// LED1��һ����ʾ����
        /// </summary>
        string LED1PrevLedFileContent = string.Empty;

        /// <summary>
        /// ����LED1��̬����
        /// </summary>
        /// <param name="value1">��һ������</param>
        /// <param name="value2">�ڶ�������</param>
        private void UpdateLed1Show(string value1 = "", string value2 = "")
        {
#if DEBUG
             FrmDebugConsole.GetInstance(this).Output("����LED1:|" + value1 + "|" + value2 + "|");
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
                        // ��ʼ���ɹ�
                        this.LED1ConnectStatus = true;
                    }
                    else
                    {
                        this.LED1ConnectStatus = false;
                    }
                    if (nResult != YB14DynamicAreaLeder.RETURN_NOERROR) Log4Neter.Error("����LED��̬����", new Exception(YB14DynamicAreaLeder.GetErrorMessage("SendDynamicAreaInfoCommand", nResult)));

                    LED1m_bSendBusy = false;
                }

                this.LED1PrevLedFileContent = value1 + value2;
            }
            catch (Exception ex)
            {
                Log4Neter.Error("����LED��Ϣ", ex);
            }
        }

        #endregion

        #region LED2���ƿ�

        /// <summary>
        /// LED2���ƿ�����
        /// </summary>
        int LED2nScreenNo = 2;
        /// <summary>
        /// LED2��̬�����
        /// </summary>
        int LED2DYArea_ID = 2;
        /// <summary>
        /// LED2���±�ʶ
        /// </summary>
        bool LED2m_bSendBusy = false;

        private bool _LED2ConnectStatus;
        /// <summary>
        /// LED2����״̬
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

                commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.LED��2_����״̬.ToString(), value ? "1" : "0");
            }
        }

        /// <summary>
        /// LED2��ʾ�����ı�
        /// </summary>
        string LED2TempFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Led2TempFile.txt");

        /// <summary>
        /// LED2��һ����ʾ����
        /// </summary>
        string LED2PrevLedFileContent = string.Empty;

        /// <summary>
        /// ����LED2��̬����
        /// </summary>
        /// <param name="value1">��һ������</param>
        /// <param name="value2">�ڶ�������</param>
        private void UpdateLed2Show(string value1 = "", string value2 = "")
        {
#if DEBUG
           FrmDebugConsole.GetInstance(this).Output("����LED2:|" + value1 + "|" + value2 + "|");
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
                if (nResult != YB14DynamicAreaLeder.RETURN_NOERROR) Log4Neter.Error("����LED��̬����", new Exception(YB14DynamicAreaLeder.GetErrorMessage("SendDynamicAreaInfoCommand", nResult)));

                LED2m_bSendBusy = false;
            }

            this.LED2PrevLedFileContent = value1 + value2;
        }

        #endregion

        #endregion

        #region �ذ��Ǳ�

        /// <summary>
        /// �����ȶ��¼�
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
        /// �ذ��Ǳ�״̬�仯
        /// </summary>
        /// <param name="status"></param>
        void Wber_OnStatusChange(bool status)
        {
            // �����豸״̬ 
            InvokeEx(() =>
            {
                slightWber.LightColor = (status ? Color.Green : Color.Red);

                commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.�ذ��Ǳ�_����״̬.ToString(), status ? "1" : "0");
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

        #region ������Ƶ

        /// <summary>
        /// �������������
        /// </summary>
        IPCer iPCer1 = new IPCer();
        IPCer iPCer2 = new IPCer();

        /// <summary>
        /// ִ������ͷץ�ģ�����������
        /// </summary>
        /// <param name="transportId">�����¼Id</param>
        private void CamareCapturePicture(string transportId)
        {
            try
            {
                // ץ����Ƭ������������ַ
                string pictureWebUrl = commonDAO.GetCommonAppletConfigString("�������ܻ�_ץ����Ƭ����·��");

                // �����1
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

                // �����2
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
                Log4Neter.Error("�����ץ��", ex);
            }
        }

        /// <summary>
        /// �ϴ�ץ����Ƭ�������������ļ���
        /// </summary>
        private void UploadCapturePicture()
        {
            string serverPath = commonDAO.GetCommonAppletConfigString("�������ܻ�_ץ����Ƭ����������·��");
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
                            Log4Neter.Info(string.Format("�ϴ�ͼƬ{0}", fileName));
                        }
                    }
                    catch (Exception ex)
                    {
                        Log4Neter.Error("�ϴ�ץ����Ƭ", ex);

                        break;
                    }
                }

            } while (fileName != null);
        }

        /// <summary>
        /// ������ڵ�ץ����Ƭ
        /// </summary> 
        public void ClearExpireCapturePicture()
        {
            foreach (string item in Directory.GetFiles(SelfVars.CapturePicturePath).Where(a =>
            {
                return new FileInfo(a).LastWriteTime > DateTime.Now.AddMonths(commonDAO.GetAppletConfigInt32("�������ܻ�_ץ����Ƭ��������"));
            }))
            {
                try
                {
                    File.Delete(item);
                    Log4Neter.Info(string.Format("ɾ��ͼƬ{0}", item));
                }
                catch { }
            }
        }

        #endregion


        #region ͨͨͣ��

        /// <summary>
        /// ִ������ͷץ�ģ�����������
        /// </summary>
        /// <param name="transportId">�����¼Id</param>
        private void ThinkCamareCapturePicture(string transportId)
        {
            if (this.CurrentImperfectCar == null) return;
            try
            {
                // ץ����Ƭ������������ַ
                string pictureWebUrl = commonDAO.GetCommonAppletConfigString("�������ܻ�_ץ����Ƭ����·��");
                if (this.CurrentImperfectCar.PassWay == eDirection.Way1)
                {
                    // �����1
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
                    // �����2
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
                Log4Neter.Error("ͨͨͣ�������ץ��", ex);
            }
        }

        #endregion

        #region �豸��ʼ����ж��

        /// <summary>
        /// ��ʼ������豸
        /// </summary>
        private void InitHardware()
        {
            try
            {
                bool success = false;

                this.InductorCoil1Port = commonDAO.GetAppletConfigInt32("IO������_�ظ�1�˿�");
                this.InductorCoil2Port = commonDAO.GetAppletConfigInt32("IO������_�ظ�2�˿�");
                this.InductorCoil3Port = commonDAO.GetAppletConfigInt32("IO������_�ظ�3�˿�");
                this.InductorCoil4Port = commonDAO.GetAppletConfigInt32("IO������_�ظ�4�˿�");
                this.InductorCoil3Port = commonDAO.GetAppletConfigInt32("IO������_�ظ�3�˿�");
                this.InfraredSensor1Port = commonDAO.GetAppletConfigInt32("IO������_����1�˿�");
                this.InfraredSensor2Port = commonDAO.GetAppletConfigInt32("IO������_����2�˿�");

                this.WbMinWeight = commonDAO.GetAppletConfigDouble("�ذ��Ǳ�_��С����");

                // IO������
                Hardwarer.Iocer.OnReceived += new IOC.JMDM20DIOV2.JMDM20DIOV2Iocer.ReceivedEventHandler(Iocer_Received);
                Hardwarer.Iocer.OnStatusChange += new IOC.JMDM20DIOV2.JMDM20DIOV2Iocer.StatusChangeHandler(Iocer_StatusChange);
                success = Hardwarer.Iocer.OpenCom(commonDAO.GetAppletConfigInt32("IO������_����"), commonDAO.GetAppletConfigInt32("IO������_������"), commonDAO.GetAppletConfigInt32("IO������_����λ"), (StopBits)commonDAO.GetAppletConfigInt32("IO������_ֹͣλ"), (Parity)commonDAO.GetAppletConfigInt32("IO������_У��λ"));
                if (!success) MessageBoxEx.Show("IO����������ʧ�ܣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.iocControler = new IocControler(Hardwarer.Iocer);

                // �ذ��Ǳ�
                Hardwarer.Wber.OnStatusChange += new WB.TOLEDO.YAOHUA.TOLEDO_YAOHUAWber.StatusChangeHandler(Wber_OnStatusChange);
                Hardwarer.Wber.OnSteadyChange += new WB.TOLEDO.YAOHUA.TOLEDO_YAOHUAWber.SteadyChangeEventHandler(Wber_OnSteadyChange);
                Hardwarer.Wber.OnWeightChange += new WB.TOLEDO.YAOHUA.TOLEDO_YAOHUAWber.WeightChangeEventHandler(Wber_OnWeightChange);
                success = Hardwarer.Wber.OpenCom(commonDAO.GetAppletConfigInt32("�ذ��Ǳ�_����"), commonDAO.GetAppletConfigInt32("�ذ��Ǳ�_������"), commonDAO.GetAppletConfigInt32("�ذ��Ǳ�_����λ"), commonDAO.GetAppletConfigInt32("�ذ��Ǳ�_ֹͣλ"));

                TaskSimpleScheduler taskSimpleScheduler = new TaskSimpleScheduler();

                // ����ʶ��1
                Hardwarer.Rwer1.OnActionReadSuccess = Rwer1_OnReadSuccess;
                Hardwarer.Rwer1.OnActionScanError = Rwer1_OnScanError;
                Hardwarer.Rwer1.OnActionStatusChange = Rwer1_OnStatusChange;
                success = Hardwarer.Rwer1.ConnectCamera(commonDAO.GetAppletConfigString("����ʶ��1_IP��ַ"), IntPtr.Zero);
                if (!success)
                {
                    MessageBoxEx.Show("����ʶ��1����ʧ�ܣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
                else
                {
                    InvokeEx(() =>
                      {
                          Hardwarer.Rwer1.StartPreview(picVideo1.Handle);
                      });
                }

                if (commonDAO.GetAppletConfigString("˫���") != "0")
                {
                    // ����ʶ��2
                    Hardwarer.Rwer2.OnActionReadSuccess = Rwer2_OnReadSuccess;
                    Hardwarer.Rwer2.OnActionScanError = Rwer2_OnScanError;
                    Hardwarer.Rwer2.OnActionStatusChange = Rwer2_OnStatusChange;
                    success = Hardwarer.Rwer2.ConnectCamera(commonDAO.GetAppletConfigString("����ʶ��2_IP��ַ"), IntPtr.Zero);
                    if (!success)
                    {
                        MessageBoxEx.Show("����ʶ��2����ʧ�ܣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        InvokeEx(() =>
                      {
                          Hardwarer.Rwer2.StartPreview(this.picVideo2.Handle);
                      });
                    }
                }

                #region ������Ƶ

                //IPCer.InitSDK();

                //CmcsCamare video1 = commonDAO.SelfDber.Entity<CmcsCamare>("where EquipmentCode=:EquipmentCode", new { EquipmentCode = "�����1" });
                //if (video1 != null)
                //{
                //    if (iPCer1.Login(video1.Ip, video1.Port, video1.UserName, video1.Password))
                //        iPCer1.StartPreview(panVideo1.Handle, video1.Channel);
                //}

                //CmcsCamare video2 = commonDAO.SelfDber.Entity<CmcsCamare>("where EquipmentCode=:EquipmentCode", new { EquipmentCode = "�����2" });
                //if (video2 != null)
                //{
                //    if (iPCer2.Login(video2.Ip, video2.Port, video2.UserName, video2.Password))
                //        iPCer2.StartPreview(panVideo2.Handle, video2.Channel);
                //}

                #endregion

                #region LED���ƿ�1

                string led1SocketIP = commonDAO.GetAppletConfigString("LED��ʾ��1_IP��ַ");
                if (!string.IsNullOrEmpty(led1SocketIP) && commonDAO.TestPing(led1SocketIP))
                {
                    int nResult = YB14DynamicAreaLeder.AddScreen(YB14DynamicAreaLeder.CONTROLLER_BX_5E1, this.LED1nScreenNo, YB14DynamicAreaLeder.SEND_MODE_NETWORK, 96, 64, 1, 1, "", 0, led1SocketIP, 5005, "");
                    if (nResult == YB14DynamicAreaLeder.RETURN_NOERROR)
                    {
                        nResult = YB14DynamicAreaLeder.AddScreenDynamicArea(this.LED1nScreenNo, this.LED1DYArea_ID, 1, 10, 1, "", 0, 0, 0, 96, 64, 255, 0, 255, 7, 6, 1);
                        if (nResult == YB14DynamicAreaLeder.RETURN_NOERROR)
                        {
                            nResult = YB14DynamicAreaLeder.AddScreenDynamicAreaFile(this.LED1nScreenNo, this.LED1DYArea_ID, this.LED1TempFile, 0, "����", 12, 0, 120, 1, 3, 0);
                            if (nResult == YB14DynamicAreaLeder.RETURN_NOERROR)
                            {
                                nResult = YB14DynamicAreaLeder.SendDynamicAreaInfoCommand(this.LED1nScreenNo, this.LED1DYArea_ID);
                                if (nResult == YB14DynamicAreaLeder.RETURN_NOERROR)
                                {
                                    // ��ʼ���ɹ�
                                    this.LED1ConnectStatus = true;
                                    UpdateLed1Show("  �ȴ��ϰ�");
                                }
                            }
                            else
                            {
                                this.LED1ConnectStatus = false;
                                Log4Neter.Error("��ʼ��LED1���ƿ�", new Exception(YB14DynamicAreaLeder.GetErrorMessage("AddScreenDynamicAreaFile", nResult)));
                                MessageBoxEx.Show("LED1���ƿ�����ʧ�ܣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {
                            this.LED1ConnectStatus = false;
                            Log4Neter.Error("��ʼ��LED1���ƿ�", new Exception(YB14DynamicAreaLeder.GetErrorMessage("AddScreenDynamicArea", nResult)));
                            MessageBoxEx.Show("LED1���ƿ�����ʧ�ܣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                    else
                    {
                        this.LED1ConnectStatus = false;
                        Log4Neter.Error("��ʼ��LED1���ƿ�", new Exception(YB14DynamicAreaLeder.GetErrorMessage("AddScreen", nResult)));
                        MessageBoxEx.Show("LED1���ƿ�����ʧ�ܣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }

                #endregion

                #region LED���ƿ�2
                if (commonDAO.GetAppletConfigString("˫���") != "0")
                {
                    string led2SocketIP = commonDAO.GetAppletConfigString("LED��ʾ��2_IP��ַ");
                    if (!string.IsNullOrEmpty(led2SocketIP) && commonDAO.TestPing(led2SocketIP))
                    {
                        int nResult = YB14DynamicAreaLeder.AddScreen(YB14DynamicAreaLeder.CONTROLLER_BX_5E1, this.LED2nScreenNo, YB14DynamicAreaLeder.SEND_MODE_NETWORK, 96, 64, 1, 1, "", 57600, led2SocketIP, 5005, "");
                        if (nResult == YB14DynamicAreaLeder.RETURN_NOERROR)
                        {
                            nResult = YB14DynamicAreaLeder.AddScreenDynamicArea(this.LED2nScreenNo, this.LED2DYArea_ID, 0, 10, 1, "", 0, 0, 0, 96, 64, 255, 0, 255, 7, 6, 1);
                            if (nResult == YB14DynamicAreaLeder.RETURN_NOERROR)
                            {
                                nResult = YB14DynamicAreaLeder.AddScreenDynamicAreaFile(this.LED2nScreenNo, this.LED2DYArea_ID, this.LED2TempFile, 0, "����", 12, 0, 120, 1, 3, 0);
                                if (nResult == YB14DynamicAreaLeder.RETURN_NOERROR)
                                {
                                    // ��ʼ���ɹ�
                                    this.LED2ConnectStatus = true;
                                    UpdateLed2Show("  �ȴ��ϰ�");
                                }
                                else
                                {
                                    this.LED2ConnectStatus = false;
                                    Log4Neter.Error("��ʼ��LED2���ƿ�", new Exception(YB14DynamicAreaLeder.GetErrorMessage("AddScreenDynamicAreaFile", nResult)));
                                    MessageBoxEx.Show("LED2���ƿ�����ʧ�ܣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                                }
                            }
                            else
                            {
                                this.LED2ConnectStatus = false;
                                Log4Neter.Error("��ʼ��LED2���ƿ�", new Exception(YB14DynamicAreaLeder.GetErrorMessage("AddScreenDynamicArea", nResult)));
                                MessageBoxEx.Show("LED2���ƿ�����ʧ�ܣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            }
                        }
                        else
                        {
                            this.LED2ConnectStatus = false;
                            Log4Neter.Error("��ʼ��LED2���ƿ�", new Exception(YB14DynamicAreaLeder.GetErrorMessage("AddScreen", nResult)));
                            MessageBoxEx.Show("LED2���ƿ�����ʧ�ܣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                Log4Neter.Error("�豸��ʼ��", ex);
                if (iocControler != null)
                {
                    iocControler.RedLight1();
                    iocControler.RedLight2();
                }
            }
        }

        /// <summary>
        /// ж���豸
        /// </summary>
        private void UnloadHardware()
        {
            // ע��˶δ���
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
                if (commonDAO.GetAppletConfigString("˫���") != "0")
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
                if (commonDAO.GetAppletConfigString("˫���") != "0")
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

        #region ��բ���ư�ť

        /// <summary>
        /// ��բ1����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGate1Up_Click(object sender, EventArgs e)
        {
            if (this.iocControler != null)
            {
                this.iocControler.Gate1Up();
                Log4Neter.Info(string.Format("�ֶ�����բ1 ���ƺ�:{0}", this.CurrentAutotruck != null ? this.CurrentAutotruck.CarNumber : "�޳���"));
            }
        }

        /// <summary>
        /// ��բ1����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGate1Down_Click(object sender, EventArgs e)
        {
            if (this.iocControler != null && !EastOnEnterWay())
            {
                this.iocControler.Gate1Down();
                Log4Neter.Info(string.Format("�ֶ�����բ1 ���ƺ�:{0}", this.CurrentAutotruck != null ? this.CurrentAutotruck.CarNumber : "�޳���"));
            }
        }

        /// <summary>
        /// ��բ2����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGate2Up_Click(object sender, EventArgs e)
        {
            if (this.iocControler != null)
            {
                this.iocControler.Gate2Up();
                Log4Neter.Info(string.Format("�ֶ�����բ2 ���ƺ�:{0}", this.CurrentAutotruck != null ? this.CurrentAutotruck.CarNumber : "�޳���"));
            }
        }

        /// <summary>
        /// ��բ2����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGate2Down_Click(object sender, EventArgs e)
        {
            if (this.iocControler != null && !WestOnEnterWay())
            {
                this.iocControler.Gate2Down();
                Log4Neter.Info(string.Format("�ֶ�����բ2 ���ƺ�:{0}", this.CurrentAutotruck != null ? this.CurrentAutotruck.CarNumber : "�޳���"));
            }
        }

        #endregion

        #region ����ҵ��

        /// <summary>
        /// ����������ʶ������
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
                    case eFlowFlag.�ȴ�����:
                        // ��ǰ�ذ�����С����С���������еظС��������ź�ʱ����
                        if (Hardwarer.Wber.Weight < this.WbMinWeight && !HasCarOnEnterWay() && !HasCarOnLeaveWay() && this.CurrentAutotruck != null
                            && this.CurrentImperfectCar != null) ResetBuyFuel();

                        break;

                    case eFlowFlag.ʶ����:
                        #region

                        // �������޳�ʱ���ȴ�����
                        if (passCarQueuer.Count == 0)
                        {
                            this.CurrentFlowFlag = eFlowFlag.�ȴ�����;
                            break;
                        }

                        this.CurrentImperfectCar = passCarQueuer.Dequeue();

                        // ��ʽһ������ʶ��ĳ��ƺŲ��ҳ�����Ϣ
                        this.CurrentAutotruck = carTransportDAO.GetAutotruckByCarNumber(this.CurrentImperfectCar.Voucher);

                        if (this.CurrentAutotruck != null)
                        {
                            if (this.CurrentAutotruck.IsUse == 1)
                            {
                                CurrentUnFinishTransport = carTransportDAO.GetUnFinishTransportByAutotruckId(this.CurrentAutotruck.Id);
                                if (CurrentUnFinishTransport != null)
                                {
                                    if (CurrentUnFinishTransport.CarType == eTransportType.ԭ��ú�볡.ToString() || CurrentUnFinishTransport.CarType == eTransportType.�ִ�ú�볡.ToString() || CurrentUnFinishTransport.CarType == eTransportType.��תú�볡.ToString())
                                    {
                                        this.timer_BuyFuel_Cancel = false;
                                        this.CurrentFlowFlag = eFlowFlag.��֤��Ϣ;
                                        timer_BuyFuel_Tick(null, null);
                                        Log4Neter.Info(string.Format("���ƺţ�{0} �볡ú", this.CurrentAutotruck.CarNumber));
                                    }
                                    else if (CurrentUnFinishTransport.CarType == eTransportType.�ִ�ú����.ToString() || CurrentUnFinishTransport.CarType == eTransportType.��תú����.ToString() || CurrentUnFinishTransport.CarType == eTransportType.���۲���ú.ToString() || CurrentUnFinishTransport.CarType == eTransportType.����ֱ��ú.ToString())
                                    {
                                        this.timer_SaleFuel_Cancel = false;
                                        this.CurrentFlowFlag = eFlowFlag.��֤��Ϣ;
                                        timer_SaleFuel_Tick(null, null);
                                        Log4Neter.Info(string.Format("���ƺţ�{0} ����ú", this.CurrentAutotruck.CarNumber));
                                    }
                                    else if (CurrentUnFinishTransport.CarType == eCarType.��������.ToString())
                                    {
                                        this.timer_Goods_Cancel = false;
                                        this.CurrentFlowFlag = eFlowFlag.��֤��Ϣ;
                                        timer_Goods_Tick(null, null);
                                        Log4Neter.Info(string.Format("���ƺţ�{0} ��������", this.CurrentAutotruck.CarNumber));
                                    }
                                }
                                else
                                {
                                    UpdateLedShow(this.CurrentAutotruck.CarNumber, "δ�Ŷ�");
                                    this.voiceSpeaker.Speak("���ƺ� " + this.CurrentAutotruck.CarNumber + " δ�Ŷ�", 2, false);
                                    Log4Neter.Info(string.Format("���ƺţ�{0} δ�Ŷ�", this.CurrentAutotruck.CarNumber));
                                }
                            }
                            else
                            {
                                UpdateLedShow(this.CurrentAutotruck.CarNumber, "��ͣ��");
                                this.voiceSpeaker.Speak("���ƺ� " + this.CurrentAutotruck.CarNumber + " ��ͣ�ã���ֹͨ��", 2, false);

                                //timer1.Interval = 20000;
                                Log4Neter.Info(string.Format("���ƺţ�{0} ��ͣ��", this.CurrentAutotruck.CarNumber));
                            }
                        }
                        else
                        {
                            UpdateLedShow(this.CurrentImperfectCar.Voucher, "δ�Ǽ�");

                            // ��ʽһ������ʶ��
                            this.voiceSpeaker.Speak("���ƺ� " + this.CurrentImperfectCar.Voucher + " δ�Ǽ� ��ֹͨ��", 2, false);

                            timer1.Interval = 20000;
                        }
                        // ��ǰ�ذ�����С����С���������еظС��������ź�ʱ����

                        if (Hardwarer.Wber.Weight < this.WbMinWeight && !HasCarOnEnterWay() && !HasCarOnLeaveWay() && this.CurrentFlowFlag != eFlowFlag.�ȴ�����
                            && this.CurrentImperfectCar != null)
                        {
                            ResetBuyFuel();
                        }

                        #endregion
                        break;
                }

                commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.�ذ��Ǳ�_ʵʱ����.ToString(), Hardwarer.Wber.Weight.ToString());
                // ִ��Զ������
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
        /// ��������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer2_Tick(object sender, EventArgs e)
        {
            timer2.Stop();
            // ������ִ��һ��
            timer2.Interval = 180000;

            try
            {
                // �볧ú
                LoadTodayUnFinishBuyFuelTransport();
                LoadTodayFinishBuyFuelTransport();

                // ����ú 
                LoadTodayUnFinishSaleFuelTransport();
                LoadTodayFinishSaleFuelTransport();

                // ��������
                LoadTodayUnFinishGoodsTransport();
                LoadTodayFinishGoodsTransport();

                // �ϴ�ץ����Ƭ
                UploadCapturePicture();
                // ����ץ����Ƭ
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
        /// �г������ϰ��ĵ�·��
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
        /// �г������°��ĵ�·��
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
        /// ���ϰ�����������
        /// </summary>
        /// <returns></returns>
        bool EastOnEnterWay()
        {
            return this.InductorCoil1 || this.InductorCoil2 || this.InfraredSensor1;
        }

        /// <summary>
        /// ���ϰ�����������
        /// </summary>
        /// <returns></returns>
        bool WestOnEnterWay()
        {
            return this.InductorCoil3 || this.InductorCoil4 || this.InfraredSensor2;
        }

        /// <summary>
        /// ִ��Զ������
        /// </summary>
        void ExecAppRemoteControlCmd()
        {
            // ��ȡ���µ�����
            CmcsAppRemoteControlCmd appRemoteControlCmd = commonDAO.GetNewestAppRemoteControlCmd(CommonAppConfig.GetInstance().AppIdentifier);
            if (appRemoteControlCmd != null)
            {
                if (appRemoteControlCmd.CmdCode == "���Ƶ�բ")
                {
                    Log4Neter.Info("����Զ�����" + appRemoteControlCmd.CmdCode + "��������" + appRemoteControlCmd.Param);

                    if (appRemoteControlCmd.Param.Equals("Gate1Up", StringComparison.CurrentCultureIgnoreCase))
                        this.iocControler.Gate1Up();
                    else if (appRemoteControlCmd.Param.Equals("Gate1Down", StringComparison.CurrentCultureIgnoreCase))
                        this.iocControler.Gate1Down();
                    else if (appRemoteControlCmd.Param.Equals("Gate2Up", StringComparison.CurrentCultureIgnoreCase))
                        this.iocControler.Gate2Up();
                    else if (appRemoteControlCmd.Param.Equals("Gate2Down", StringComparison.CurrentCultureIgnoreCase))
                        this.iocControler.Gate2Down();

                    // ����ִ�н��
                    commonDAO.SetAppRemoteControlCmdResultCode(appRemoteControlCmd, eEquInfCmdResultCode.�ɹ�);
                }
            }
        }

        /// <summary>
        /// �л��ֶ�/�Զ�ģʽ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sbtnChangeAutoHandMode_ValueChanged(object sender, EventArgs e)
        {
            this.AutoHandMode = sbtnChangeAutoHandMode.Value;
        }

        #endregion

        #region �볧úҵ��

        bool timer_BuyFuel_Cancel = true;

        CmcsBuyFuelTransport currentBuyFuelTransport;
        /// <summary>
        /// ��ǰ�����¼
        /// </summary>
        public CmcsBuyFuelTransport CurrentBuyFuelTransport
        {
            get { return currentBuyFuelTransport; }
            set
            {
                currentBuyFuelTransport = value;

                if (value != null)
                {
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.��ǰ�����¼Id.ToString(), value.Id);
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
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.��ǰ�����¼Id.ToString(), string.Empty);

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
        /// ѡ����
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
                this.CurrentFlowFlag = eFlowFlag.ʶ����;
                timer1_Tick(null, null);
            }
        }

        /// <summary>
        /// �����볧ú�����¼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveTransport_BuyFuel_Click(object sender, EventArgs e)
        {
            btnSaveTransport_BuyFuel.Enabled = false;
            if (!SaveBuyFuelTransport()) MessageBoxEx.Show("����ʧ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            Log4Neter.Info("�볧ú�ֶ���������");
            btnSaveTransport_BuyFuel.Enabled = true;
        }

        /// <summary>
        /// ���������¼
        /// </summary>
        /// <returns></returns>
        bool SaveBuyFuelTransport()
        {
            if (this.CurrentBuyFuelTransport == null) return false;

            try
            {
                if (this.CurrentBuyFuelTransport.GrossWeight > 0 && (double)(this.CurrentBuyFuelTransport.GrossWeight - (decimal)Hardwarer.Wber.Weight) < commonDAO.GetCommonAppletConfigDouble("�ذ��Ǳ�_ëƤ��"))
                {
                    UpdateLedShow("���ز���ȷ", "��ϵ����Ա");
                    this.voiceSpeaker.Speak("�����쳣 ", 2, false);
                    return false;
                }
                if (weighterDAO.SaveBuyFuelTransport(this.CurrentBuyFuelTransport.Id, (decimal)Hardwarer.Wber.Weight, DateTime.Now, CommonAppConfig.GetInstance().AppIdentifier))
                {
                    this.CurrentBuyFuelTransport = commonDAO.SelfDber.Get<CmcsBuyFuelTransport>(this.CurrentBuyFuelTransport.Id);

                    FrontGateUp();

                    //btnSaveTransport_BuyFuel.Enabled = false;
                    this.CurrentFlowFlag = eFlowFlag.�ȴ��뿪;

                    UpdateLedShow("  �������", "  ���°�");
                    this.voiceSpeaker.Speak("����������°�", 2, false);

                    LoadTodayUnFinishBuyFuelTransport();
                    LoadTodayFinishBuyFuelTransport();

                    CamareCapturePicture(this.CurrentBuyFuelTransport.Id);

                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show("����ʧ��\r\n" + ex.Message, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);

                Log4Neter.Error("���������¼", ex);
            }

            return false;
        }

        /// <summary>
        /// �����볧ú�����¼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_BuyFuel_Click(object sender, EventArgs e)
        {
            ResetBuyFuel();
        }

        /// <summary>
        /// ������Ϣ
        /// </summary>
        void ResetBuyFuel()
        {
            this.timer_BuyFuel_Cancel = true;

            this.CurrentFlowFlag = eFlowFlag.�ȴ�����;

            this.CurrentAutotruck = null;
            this.CurrentBuyFuelTransport = null;
            this.CurrentUnFinishTransport = null;
            //btnSaveTransport_BuyFuel.Enabled = false;
            this.CarNumber = null;
            FrontGateDown();
            BackGateDown();

            // �������
            this.CurrentImperfectCar = null;
            iocControler.GreenLight1();
            iocControler.GreenLight2();
            UpdateLedShow("  �ȴ�����");
            ResetCount = 0;
        }

        /// <summary>
        /// �볧ú�����¼ҵ��ʱ��
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
                    case eFlowFlag.��֤��Ϣ:
                        #region
                        if (this.CurrentUnFinishTransport != null)
                        {
                            this.CurrentBuyFuelTransport = commonDAO.SelfDber.Get<CmcsBuyFuelTransport>(this.CurrentUnFinishTransport.TransportId);
                            if (this.CurrentBuyFuelTransport != null)
                            {
                                // �ж�·������
                                string nextPlace;
                                //BSϵͳûʹ��·���ж�û��ʹ�� ȥ��·���ж�
                                //if (carTransportDAO.CheckNextTruckInFactoryWay(this.CurrentAutotruck.CarType, this.CurrentBuyFuelTransport.StepName, "�س�|�ᳵ", CommonAppConfig.GetInstance().AppIdentifier, out nextPlace))
                                if (true)
                                {
                                    if (this.CurrentBuyFuelTransport.SuttleWeight == 0)
                                    {
                                        BackGateUp();
                                        ThinkCamareCapturePicture(this.CurrentBuyFuelTransport.Id);
                                        this.CurrentFlowFlag = eFlowFlag.�ȴ��ϰ�;

                                        UpdateLedShow(this.CurrentAutotruck.CarNumber, "���ϰ�");
                                        this.voiceSpeaker.Speak("���ƺ� " + this.CurrentAutotruck.CarNumber + " ���ϰ�", 2, false);
                                    }
                                    else
                                    {
                                        UpdateLedShow(this.CurrentAutotruck.CarNumber, "�ѳ���");
                                        this.voiceSpeaker.Speak("���ƺ� " + this.CurrentAutotruck.CarNumber + " �ѳ���", 2, false);

                                        timer_BuyFuel.Interval = 20000;
                                        Log4Neter.Info(string.Format("���ƺţ�{0} �ѳ���", this.CurrentAutotruck.CarNumber));
                                    }
                                }
                                else
                                {
                                    UpdateLedShow("·�ߴ���", "��ֹͨ��");
                                    this.voiceSpeaker.Speak("·�ߴ��� ��ֹͨ�� " + (!string.IsNullOrEmpty(nextPlace) ? "��ǰ��" + nextPlace : ""), 2, false);

                                    timer_BuyFuel.Interval = 20000;
                                    Log4Neter.Info(string.Format("���ƺţ�{0} ·�ߴ���", this.CurrentAutotruck.CarNumber));
                                }
                            }
                            else
                            {
                                UpdateLedShow("δ�ҵ������¼", "��ֹͨ��");
                                this.voiceSpeaker.Speak("δ�ҵ������¼ ��ֹͨ�� ", 2, false);

                                commonDAO.SelfDber.Delete<CmcsUnFinishTransport>(this.CurrentUnFinishTransport.Id);
                            }
                        }
                        else
                        {
                            UpdateLedShow("δ�Ŷ�", "��ֹͨ��");
                            this.voiceSpeaker.Speak("δ�Ŷ� ��ֹͨ�� ", 2, false);

                        }

                        #endregion
                        break;

                    case eFlowFlag.�ȴ��ϰ�:
                        #region

                        // ����������
                        timer_BuyFuel.Interval = 6000;

                        // ���ذ��Ǳ�����������С��������������ĵظ����������źţ����ж����Ѿ���ȫ�ϰ�
                        if (Hardwarer.Wber.Weight >= this.WbMinWeight && !HasCarOnEnterWay())
                        {
                            BackGateDown();

                            this.CurrentFlowFlag = eFlowFlag.�ȴ��ȶ�;
                        }

                        #endregion
                        break;

                    case eFlowFlag.�ȴ��ȶ�:
                        #region

                        // ���������
                        timer_BuyFuel.Interval = 1000;

                        //btnSaveTransport_BuyFuel.Enabled = this.WbSteady;

                        UpdateLedShow(this.CurrentAutotruck.CarNumber, Hardwarer.Wber.Weight.ToString("#0.######"));

                        if (this.WbSteady)
                        {
                            if (this.AutoHandMode)
                            {
                                // �Զ�ģʽ
                                if (!SaveBuyFuelTransport())
                                {
                                    UpdateLedShow(this.CurrentAutotruck.CarNumber, "����ʧ��");
                                    this.voiceSpeaker.Speak("���ƺ� " + this.CurrentAutotruck.CarNumber + " ����ʧ�� ����ϵ����Ա", 2, false);
                                    Log4Neter.Info(string.Format("���ƺţ�{0} ����ʧ��", this.CurrentAutotruck.CarNumber));
                                }
                            }
                            else
                            {
                                // �ֶ�ģʽ 
                            }
                        }

                        #endregion
                        break;

                    case eFlowFlag.�ȴ��뿪:
                        #region

                        // ��ǰ�ذ�����С����С���������еظС��������ź�ʱ����
                        if (Hardwarer.Wber.Weight < this.WbMinWeight && !HasCarOnLeaveWay() && !HasCarOnEnterWay())
                        {
                            ResetBuyFuel();
                            Log4Neter.Info(string.Format("���ƺţ�{0} ���°�", this.CurrentAutotruck.CarNumber));
                        }

                        // ����������
                        timer_BuyFuel.Interval = 4000;

                        #endregion
                        break;
                }

                // ��ǰ�ذ�����С����С���������еظС��������ź�ʱ����
                if (Hardwarer.Wber.Weight < this.WbMinWeight && !HasCarOnEnterWay() && !HasCarOnLeaveWay() && this.CurrentFlowFlag != eFlowFlag.�ȴ�����
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
        /// ��ȡδ��ɵ��볧ú��¼
        /// </summary>
        void LoadTodayUnFinishBuyFuelTransport()
        {
            superGridControl1_BuyFuel.PrimaryGrid.DataSource = weighterDAO.GetUnFinishBuyFuelTransport();
        }

        /// <summary>
        /// ��ȡָ����������ɵ��볧ú��¼
        /// </summary>
        void LoadTodayFinishBuyFuelTransport()
        {
            superGridControl2_BuyFuel.PrimaryGrid.DataSource = weighterDAO.GetFinishedBuyFuelTransport(DateTime.Now.Date, DateTime.Now.Date.AddDays(1));
        }

        #endregion

        #region ����úҵ��

        bool timer_SaleFuel_Cancel = true;
        CmcsSaleFuelTransport currentSaleFuelTransport;
        /// <summary>
        /// ��ǰ�����¼
        /// </summary>
        public CmcsSaleFuelTransport CurrentSaleFuelTransport
        {
            get { return currentSaleFuelTransport; }
            set
            {
                currentSaleFuelTransport = value;

                if (value != null)
                {
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.��ǰ�����¼Id.ToString(), value.Id);
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
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.��ǰ�����¼Id.ToString(), string.Empty);
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
        /// ѡ����
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

                this.CurrentFlowFlag = eFlowFlag.ʶ����;
                txtCarNumber_SaleFuel.Text = frm.Output.CarNumber;
                timer1_Tick(null, null);
            }
        }




        /// <summary>
        /// �����ŶӼ�¼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveTransport_SaleFuel_Click(object sender, EventArgs e)
        {
            if (!SaveSaleFuelTransport()) MessageBoxEx.Show("����ʧ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            Log4Neter.Info("����ú�ֶ���������");
        }

        /// <summary>
        /// ���������¼
        /// </summary>
        /// <returns></returns>
        bool SaveSaleFuelTransport()
        {
            if (this.CurrentSaleFuelTransport == null) return false;

            try
            {
                if (this.CurrentSaleFuelTransport.TareWeight > 0 && (double)((decimal)Hardwarer.Wber.Weight - this.CurrentSaleFuelTransport.TareWeight) < commonDAO.GetCommonAppletConfigDouble("�ذ��Ǳ�_ëƤ��"))
                {
                    UpdateLedShow("���ز���ȷ", "��ϵ����Ա");
                    this.voiceSpeaker.Speak("�����쳣 ", 2, false);
                    return false;
                }
                if (weighterDAO.SaveSaleFuelTransport(this.CurrentSaleFuelTransport.Id, (decimal)Hardwarer.Wber.Weight, DateTime.Now, CommonAppConfig.GetInstance().AppIdentifier))
                {
                    this.CurrentSaleFuelTransport = commonDAO.SelfDber.Get<CmcsSaleFuelTransport>(this.CurrentSaleFuelTransport.Id);

                    FrontGateUp();

                    this.CurrentFlowFlag = eFlowFlag.�ȴ��뿪;

                    UpdateLedShow("  �������", "  ���°�");
                    this.voiceSpeaker.Speak("����������°�", 2, false);

                    LoadTodayUnFinishSaleFuelTransport();
                    LoadTodayFinishSaleFuelTransport();

                    ThinkCamareCapturePicture(this.CurrentSaleFuelTransport.Id);

                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show("����ʧ��\r\n" + ex.Message, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);

                Log4Neter.Error("���������¼", ex);
            }

            return false;
        }

        /// <summary>
        /// ������Ϣ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_SaleFuel_Click(object sender, EventArgs e)
        {
            ResetSaleFuel();
        }

        /// <summary>
        /// ������Ϣ
        /// </summary>
        void ResetSaleFuel()
        {
            this.timer_SaleFuel_Cancel = true;

            this.CurrentFlowFlag = eFlowFlag.�ȴ�����;

            this.CurrentAutotruck = null;
            this.CurrentSaleFuelTransport = null;
            this.CurrentUnFinishTransport = null;

            //btnSaveTransport_SaleFuel.Enabled = false;

            FrontGateDown();
            BackGateDown();

            UpdateLedShow("  �ȴ�����");
            iocControler.GreenLight1();
            iocControler.GreenLight2();
            // �������
            this.CurrentImperfectCar = null;
            ResetCount = 0;
        }

        /// <summary>
        /// ����ú�����¼ҵ��ʱ��
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
                    case eFlowFlag.��֤��Ϣ:
                        #region

                        this.CurrentSaleFuelTransport = commonDAO.SelfDber.Get<CmcsSaleFuelTransport>(this.CurrentUnFinishTransport.TransportId);
                        if (this.CurrentSaleFuelTransport != null)
                        {
                            // �ж�·������
                            string nextPlace;
                            if (true)
                            //if (carTransportDAO.CheckNextTruckInFactoryWay(this.CurrentAutotruck.CarType, this.CurrentSaleFuelTransport.StepName, "�س�|�ᳵ", CommonAppConfig.GetInstance().AppIdentifier, out nextPlace))
                            {
                                if (this.CurrentSaleFuelTransport.SuttleWeight == 0)
                                {
                                    BackGateUp();
                                    ThinkCamareCapturePicture(this.CurrentSaleFuelTransport.Id);
                                    this.CurrentFlowFlag = eFlowFlag.�ȴ��ϰ�;

                                    UpdateLedShow(this.CurrentAutotruck.CarNumber, "���ϰ�");
                                    this.voiceSpeaker.Speak("���ƺ� " + this.CurrentAutotruck.CarNumber + " ���ϰ�", 2, false);
                                }
                                else
                                {
                                    UpdateLedShow(this.CurrentAutotruck.CarNumber, "�ѳ���");
                                    this.voiceSpeaker.Speak("���ƺ� " + this.CurrentAutotruck.CarNumber + " �ѳ���", 2, false);

                                    timer_SaleFuel.Interval = 20000;
                                }
                            }
                            else
                            {
                                UpdateLedShow("·�ߴ���", "��ֹͨ��");
                                this.voiceSpeaker.Speak("·�ߴ��� ��ֹͨ�� " + (!string.IsNullOrEmpty(nextPlace) ? "��ǰ��" + nextPlace : ""), 2, false);

                                timer_SaleFuel.Interval = 20000;
                            }
                        }
                        else
                        {
                            commonDAO.SelfDber.Delete<CmcsUnFinishTransport>(this.CurrentUnFinishTransport.Id);
                        }

                        #endregion
                        break;

                    case eFlowFlag.�ȴ��ϰ�:
                        #region

                        // ���ذ��Ǳ�����������С��������������ĵظ����������źţ����ж����Ѿ���ȫ�ϰ�
                        if (Hardwarer.Wber.Weight >= this.WbMinWeight && !HasCarOnEnterWay())
                        {
                            BackGateDown();

                            this.CurrentFlowFlag = eFlowFlag.�ȴ��ȶ�;
                        }

                        // ����������
                        timer_SaleFuel.Interval = 4000;

                        #endregion
                        break;

                    case eFlowFlag.�ȴ��ȶ�:
                        #region

                        // ���������
                        timer_SaleFuel.Interval = 1000;

                        btnSaveTransport_SaleFuel.Enabled = this.WbSteady;

                        UpdateLedShow(this.CurrentAutotruck.CarNumber, Hardwarer.Wber.Weight.ToString("#0.######"));

                        if (this.WbSteady)
                        {
                            if (this.AutoHandMode)
                            {
                                // �Զ�ģʽ
                                if (!SaveSaleFuelTransport())
                                {
                                    UpdateLedShow(this.CurrentAutotruck.CarNumber, "����ʧ��");
                                    this.voiceSpeaker.Speak("���ƺ� " + this.CurrentAutotruck.CarNumber + "����ʧ�ܣ�����ϵ����Ա", 2, false);
                                }
                            }
                            else
                            {
                                // �ֶ�ģʽ 
                            }
                        }

                        #endregion
                        break;

                    case eFlowFlag.�ȴ��뿪:
                        #region

                        // ��ǰ�ذ�����С����С���������еظС��������ź�ʱ����
                        if (Hardwarer.Wber.Weight < this.WbMinWeight && !HasCarOnLeaveWay()) ResetSaleFuel();

                        // ����������
                        timer_SaleFuel.Interval = 4000;

                        #endregion
                        break;
                }

                // ��ǰ�ذ�����С����С���������еظС��������ź�ʱ����
                if (Hardwarer.Wber.Weight < this.WbMinWeight && !HasCarOnEnterWay() && !HasCarOnLeaveWay() && this.CurrentFlowFlag != eFlowFlag.�ȴ�����
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
        /// ��ȡδ��ɵ����ۼ�¼
        /// </summary>
        void LoadTodayUnFinishSaleFuelTransport()
        {
            superGridControl1_SaleFuel.PrimaryGrid.DataSource = weighterDAO.GetUnFinishSaleFuelTransport();
        }

        /// <summary>
        /// ��ȡָ����������ɵ����ۼ�¼
        /// </summary>
        void LoadTodayFinishSaleFuelTransport()
        {
            superGridControl2_SaleFuel.PrimaryGrid.DataSource = weighterDAO.GetFinishedSaleFuelTransport(DateTime.Now.Date, DateTime.Now.Date.AddDays(1));
        }
        #endregion

        #region ��������ҵ��

        bool timer_Goods_Cancel = true;

        CmcsGoodsTransport currentGoodsTransport;
        /// <summary>
        /// ��ǰ�����¼
        /// </summary>
        public CmcsGoodsTransport CurrentGoodsTransport
        {
            get { return currentGoodsTransport; }
            set
            {
                currentGoodsTransport = value;

                if (value != null)
                {
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.��ǰ�����¼Id.ToString(), value.Id);
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
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.��ǰ�����¼Id.ToString(), string.Empty);

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
        /// ѡ����
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

                this.CurrentFlowFlag = eFlowFlag.ʶ����;
            }
        }

        /// <summary>
        /// �����ŶӼ�¼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveTransport_Goods_Click(object sender, EventArgs e)
        {
            if (!SaveGoodsTransport()) MessageBoxEx.Show("����ʧ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// ���������¼
        /// </summary>
        /// <returns></returns>
        bool SaveGoodsTransport()
        {
            if (this.CurrentGoodsTransport == null) return false;

            try
            {
                if (this.CurrentGoodsTransport.FirstWeight > 0 && (double)(this.CurrentGoodsTransport.FirstWeight - (decimal)Hardwarer.Wber.Weight) < commonDAO.GetCommonAppletConfigDouble("�ذ��Ǳ�_ëƤ��"))
                {
                    UpdateLedShow("���ز���ȷ", "��ϵ����Ա");
                    this.voiceSpeaker.Speak("�����쳣 ", 2, false);
                    return false;
                }
                if (weighterDAO.SaveGoodsTransport(this.CurrentGoodsTransport.Id, (decimal)Hardwarer.Wber.Weight, DateTime.Now, CommonAppConfig.GetInstance().AppIdentifier))
                {
                    this.CurrentGoodsTransport = commonDAO.SelfDber.Get<CmcsGoodsTransport>(this.CurrentGoodsTransport.Id);

                    FrontGateUp();

                    btnSaveTransport_Goods.Enabled = false;
                    this.CurrentFlowFlag = eFlowFlag.�ȴ��뿪;

                    UpdateLedShow("  �������", "  ���°�");
                    this.voiceSpeaker.Speak("����������°�", 2, false);

                    LoadTodayUnFinishGoodsTransport();
                    LoadTodayFinishGoodsTransport();

                    CamareCapturePicture(this.CurrentGoodsTransport.Id);

                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show("����ʧ��\r\n" + ex.Message, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);

                Log4Neter.Error("���������¼", ex);
            }

            return false;
        }

        /// <summary>
        /// ������Ϣ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_Goods_Click(object sender, EventArgs e)
        {
            ResetGoods();
        }

        /// <summary>
        /// ������Ϣ
        /// </summary>
        void ResetGoods()
        {
            this.timer_Goods_Cancel = true;

            this.CurrentFlowFlag = eFlowFlag.�ȴ�����;

            this.CurrentAutotruck = null;
            this.CurrentGoodsTransport = null;
            this.CurrentUnFinishTransport = null;

            btnSaveTransport_Goods.Enabled = false;

            FrontGateDown();
            BackGateDown();

            UpdateLedShow("  �ȴ�����");
            iocControler.GreenLight1();
            iocControler.GreenLight2();
            // �������
            this.CurrentImperfectCar = null;
            ResetCount = 0;
        }

        /// <summary>
        /// �������������¼ҵ��ʱ��
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
                    case eFlowFlag.��֤��Ϣ:
                        #region

                        this.CurrentGoodsTransport = commonDAO.SelfDber.Get<CmcsGoodsTransport>(this.CurrentUnFinishTransport.TransportId);
                        if (this.CurrentGoodsTransport != null)
                        {
                            // �ж�·������
                            string nextPlace;
                            //if (carTransportDAO.CheckNextTruckInFactoryWay(this.CurrentAutotruck.CarType, this.CurrentGoodsTransport.StepName, "��һ�γ���|�ڶ��γ���", CommonAppConfig.GetInstance().AppIdentifier, out nextPlace))
                            if (true)
                            {
                                if (this.CurrentGoodsTransport.SuttleWeight == 0)
                                {
                                    BackGateUp();
                                    ThinkCamareCapturePicture(this.CurrentGoodsTransport.Id);
                                    this.CurrentFlowFlag = eFlowFlag.�ȴ��ϰ�;

                                    UpdateLedShow(this.CurrentAutotruck.CarNumber, "���ϰ�");
                                    this.voiceSpeaker.Speak("���ƺ� " + this.CurrentAutotruck.CarNumber + " ���ϰ�", 2, false);
                                }
                                else
                                {
                                    UpdateLedShow(this.CurrentAutotruck.CarNumber, "�ѳ���");
                                    this.voiceSpeaker.Speak("���ƺ� " + this.CurrentAutotruck.CarNumber + " �ѳ���", 2, false);

                                    timer_Goods.Interval = 20000;
                                }
                            }
                            else
                            {
                                UpdateLedShow("·�ߴ���", "��ֹͨ��");
                                this.voiceSpeaker.Speak("·�ߴ��� ��ֹͨ�� " + (!string.IsNullOrEmpty(nextPlace) ? "��ǰ��" + nextPlace : ""), 2, false);

                                timer_Goods.Interval = 20000;
                            }
                        }
                        else
                        {
                            commonDAO.SelfDber.Delete<CmcsUnFinishTransport>(this.CurrentUnFinishTransport.Id);
                        }

                        #endregion
                        break;

                    case eFlowFlag.�ȴ��ϰ�:
                        #region

                        // ���ذ��Ǳ�����������С��������������ĵظ����������źţ����ж����Ѿ���ȫ�ϰ�
                        if (Hardwarer.Wber.Weight >= this.WbMinWeight && !HasCarOnEnterWay())
                        {
                            BackGateDown();

                            this.CurrentFlowFlag = eFlowFlag.�ȴ��ȶ�;
                        }

                        // ����������
                        timer_Goods.Interval = 4000;

                        #endregion
                        break;

                    case eFlowFlag.�ȴ��ȶ�:
                        #region

                        // ���������
                        timer_Goods.Interval = 1000;

                        btnSaveTransport_Goods.Enabled = this.WbSteady;

                        UpdateLedShow(this.CurrentAutotruck.CarNumber, Hardwarer.Wber.Weight.ToString("#0.######"));

                        if (this.WbSteady)
                        {
                            if (this.AutoHandMode)
                            {
                                // �Զ�ģʽ
                                if (!SaveGoodsTransport())
                                {
                                    UpdateLedShow(this.CurrentAutotruck.CarNumber, "����ʧ��");
                                    this.voiceSpeaker.Speak("���ƺ� " + this.CurrentAutotruck.CarNumber + " ����ʧ�ܣ�����ϵ����Ա", 2, false);
                                }
                            }
                            else
                            {
                                // �ֶ�ģʽ 
                            }
                        }

                        #endregion
                        break;

                    case eFlowFlag.�ȴ��뿪:
                        #region

                        // ��ǰ�ذ�����С����С���������еظС��������ź�ʱ����
                        if (Hardwarer.Wber.Weight < this.WbMinWeight && !HasCarOnLeaveWay()) ResetGoods();

                        // ����������
                        timer_Goods.Interval = 4000;

                        #endregion
                        break;
                }

                // ��ǰ�ذ�����С����С���������еظС��������ź�ʱ����
                if (Hardwarer.Wber.Weight < this.WbMinWeight && !HasCarOnEnterWay() && !HasCarOnLeaveWay() && this.CurrentFlowFlag != eFlowFlag.�ȴ�����
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
        /// ��ȡδ��ɵ��������ʼ�¼
        /// </summary>
        void LoadTodayUnFinishGoodsTransport()
        {
            superGridControl1_Goods.PrimaryGrid.DataSource = weighterDAO.GetUnFinishGoodsTransport();
        }

        /// <summary>
        /// ��ȡָ����������ɵ��������ʼ�¼
        /// </summary>
        void LoadTodayFinishGoodsTransport()
        {
            superGridControl2_Goods.PrimaryGrid.DataSource = weighterDAO.GetFinishedGoodsTransport(DateTime.Now.Date, DateTime.Now.Date.AddDays(1));
        }

        #endregion

        #region ��������

        Font directionFont = new Font("΢���ź�", 16);

        Pen redPen1 = new Pen(Color.Red, 1);
        Pen greenPen1 = new Pen(Color.Lime, 1);
        Pen redPen3 = new Pen(Color.Red, 3);
        Pen greenPen3 = new Pen(Color.Lime, 3);

        /// <summary>
        /// ��ǰ�Ǳ�����������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panCurrentWeight_Paint(object sender, PaintEventArgs e)
        {
            PanelEx panel = sender as PanelEx;

            int height = 12;

            // ���Ƶظ�1
            e.Graphics.DrawLine(this.InductorCoil1 ? redPen3 : greenPen3, 15, 1, 15, height);
            e.Graphics.DrawLine(this.InductorCoil1 ? redPen3 : greenPen3, 15, panel.Height - height, 15, panel.Height - 1);
            // ���Ƶظ�2
            e.Graphics.DrawLine(this.InductorCoil2 ? redPen3 : greenPen3, 25, 1, 25, height);
            e.Graphics.DrawLine(this.InductorCoil2 ? redPen3 : greenPen3, 25, panel.Height - height, 25, panel.Height - 1);
            // ���ƶ���1
            e.Graphics.DrawLine(this.InfraredSensor1 ? redPen1 : greenPen1, 35, 1, 35, height);
            e.Graphics.DrawLine(this.InfraredSensor1 ? redPen1 : greenPen1, 35, panel.Height - height, 35, panel.Height - 1);

            // ���ƶ���2
            e.Graphics.DrawLine(this.InfraredSensor2 ? redPen1 : greenPen1, panel.Width - 35, 1, panel.Width - 35, height);
            e.Graphics.DrawLine(this.InfraredSensor2 ? redPen1 : greenPen1, panel.Width - 35, panel.Height - height, panel.Width - 35, panel.Height - 1);
            // ���Ƶظ�3
            e.Graphics.DrawLine(this.InductorCoil3 ? redPen3 : greenPen3, panel.Width - 25, 1, panel.Width - 25, height);
            e.Graphics.DrawLine(this.InductorCoil3 ? redPen3 : greenPen3, panel.Width - 25, panel.Height - height, panel.Width - 25, panel.Height - 1);
            // ���Ƶظ�4
            e.Graphics.DrawLine(this.InductorCoil4 ? redPen3 : greenPen3, panel.Width - 15, 1, panel.Width - 15, height);
            e.Graphics.DrawLine(this.InductorCoil4 ? redPen3 : greenPen3, panel.Width - 15, panel.Height - height, panel.Width - 15, panel.Height - 1);

            // �ϰ�����
            eDirection direction = eDirection.UnKnow;
            if (this.CurrentImperfectCar != null) direction = this.CurrentImperfectCar.PassWay;
            e.Graphics.DrawString("��>", directionFont, direction == eDirection.Way1 ? Brushes.Red : Brushes.Lime, 2, 17);
            e.Graphics.DrawString("<��", directionFont, direction == eDirection.Way2 ? Brushes.Red : Brushes.Lime, panel.Width - 47, 17);
        }

        private void superGridControl_BeginEdit(object sender, DevComponents.DotNetBar.SuperGrid.GridEditEventArgs e)
        {
            // ȡ������༭
            e.Cancel = true;
        }

        /// <summary>
        /// �����к�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void superGridControl_GetRowHeaderText(object sender, DevComponents.DotNetBar.SuperGrid.GridGetRowHeaderTextEventArgs e)
        {
            e.Text = (e.GridRow.RowIndex + 1).ToString();
        }

        /// <summary>
        /// Invoke��װ
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
            string picture1FileName = Path.Combine(SelfVars.CapturePicturePath, string.Format("{0}_{1}.bmp", this.CurrentImperfectCar != null ? this.CurrentImperfectCar.Voucher : "�޳���", DateTime.Now.ToString("yyyyMMddHHmmssff")));
            Hardwarer.Rwer1.Capture(picture1FileName);
        }

        private void btnCapture_Click(object sender, EventArgs e)
        {
            string picture1FileName = Path.Combine(SelfVars.CapturePicturePath, string.Format("{0}_{1}.bmp", this.CurrentImperfectCar != null ? this.CurrentImperfectCar.Voucher : "�޳���", DateTime.Now.ToString("yyyyMMddHHmmssff")));
            Hardwarer.Rwer2.Capture(picture1FileName);
        }

        /// <summary>
        /// �ؼ��ػ�ʱ����Ԥ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panVideo1_Paint(object sender, PaintEventArgs e)
        {
            //Hardwarer.Rwer1.Preview(this.panVideo1.Handle);
        }

        /// <summary>
        /// �ؼ��ػ�ʱ����Ԥ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panVideo2_Paint(object sender, PaintEventArgs e)
        {
            //Hardwarer.Rwer2.Preview(this.panVideo2.Handle);
        }
    }
}
