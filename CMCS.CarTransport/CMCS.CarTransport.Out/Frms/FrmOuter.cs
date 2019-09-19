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
        /// ����Ψһ��ʶ��
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

                panCurrentCarNumber.Refresh();

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

                panCurrentCarNumber.Refresh();

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

                panCurrentCarNumber.Refresh();

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

                panCurrentCarNumber.Refresh();

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
                    panCurrentCarNumber.Text = value.Voucher;
                else
                    panCurrentCarNumber.Text = "�ȴ�����";
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
            FrmDebugConsole.GetInstance().Show();
#else
            //lblFlowFlag.Visible = false;
#endif

            // Ĭ���Զ�
            sbtnChangeAutoHandMode.Value = true;

            // ���ó���Զ�̿�������
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
            });
        }

        /// <summary>
        /// ����ͨ��
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
        /// ���ǰ��
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

        #region ������

        public void Rwer1_OnReadSuccess(string carnumber)
        {
            if (carnumber != "�޳���" && this.CurrentFlowFlag == eFlowFlag.�ȴ�����)
            {
                passCarQueuer.Enqueue(ePassWay.Way1, carnumber, true);
                this.CurrentFlowFlag = eFlowFlag.��֤��Ϣ;
                timer1_Tick(null, null);
                UpdateLedShow(carnumber);
                Log4Neter.Info(string.Format("����ʶ��1ʶ�𵽳��ţ�{0}", carnumber));
            }
        }

        void Rwer1_OnScanError(Exception ex)
        {
            Log4Neter.Error("������1", ex);
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

        public void Rwer2_OnReadSuccess(string carnumber)
        {
            if (carnumber != "�޳���" && this.CurrentFlowFlag == eFlowFlag.�ȴ�����)
            {
                passCarQueuer.Enqueue(ePassWay.Way2, carnumber, true);
                this.CurrentFlowFlag = eFlowFlag.��֤��Ϣ;
                timer1_Tick(null, null);
                UpdateLedShow(carnumber);
                Log4Neter.Info(string.Format("����ʶ��2ʶ�𵽳��ţ�{0}", carnumber));
            }
        }

        void Rwer2_OnScanError(Exception ex)
        {
            Log4Neter.Error("������2", ex);
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
            if (this.CurrentImperfectCar == null) return;

            if (this.CurrentImperfectCar.PassWay == ePassWay.Way1)
                UpdateLed1Show(value1, value2);
            else if (this.CurrentImperfectCar.PassWay == ePassWay.Way2)
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
            FrmDebugConsole.GetInstance().Output("����LED1:|" + value1 + "|" + value2 + "|");

            if (!this.LED1ConnectStatus) return;
            if (this.LED1PrevLedFileContent == value1 + value2) return;

            string ledContent = GenerateFillLedContent12(value1) + GenerateFillLedContent12(value2);

            File.WriteAllText(this.LED1TempFile, ledContent, Encoding.UTF8);

            if (LED1m_bSendBusy == false)
            {
                LED1m_bSendBusy = true;

                //int nResult = YB14DynamicAreaLeder.SendDynamicAreaInfoCommand(this.LED1nScreenNo, this.LED1DYArea_ID);
                //if (nResult != YB14DynamicAreaLeder.RETURN_NOERROR) Log4Neter.Error("����LED��̬����", new Exception(YB14DynamicAreaLeder.GetErrorMessage("SendDynamicAreaInfoCommand", nResult)));

                LED1m_bSendBusy = false;
            }

            this.LED1PrevLedFileContent = value1 + value2;
        }

        #endregion

        #region LED2���ƿ�

        /// <summary>
        /// LED2���ƿ�����
        /// </summary>
        int LED2nScreenNo = 1;
        /// <summary>
        /// LED2��̬�����
        /// </summary>
        int LED2DYArea_ID = 1;
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
            FrmDebugConsole.GetInstance().Output("����LED2:|" + value1 + "|" + value2 + "|");

            if (!this.LED1ConnectStatus) return;
            if (this.LED2PrevLedFileContent == value1 + value2) return;

            string ledContent = GenerateFillLedContent12(value1) + GenerateFillLedContent12(value2);

            File.WriteAllText(this.LED1TempFile, ledContent, Encoding.UTF8);

            if (LED2m_bSendBusy == false)
            {
                LED2m_bSendBusy = true;

                //int nResult = YB14DynamicAreaLeder.SendDynamicAreaInfoCommand(this.LED2nScreenNo, this.LED2DYArea_ID);
                //if (nResult != YB14DynamicAreaLeder.RETURN_NOERROR) Log4Neter.Error("����LED��̬����", new Exception(YB14DynamicAreaLeder.GetErrorMessage("SendDynamicAreaInfoCommand", nResult)));

                LED2m_bSendBusy = false;
            }

            this.LED2PrevLedFileContent = value1 + value2;
        }

        #endregion

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

                // IO������
                Hardwarer.Iocer.OnReceived += new IOC.JMDM20DIOV2.JMDM20DIOV2Iocer.ReceivedEventHandler(Iocer_Received);
                Hardwarer.Iocer.OnStatusChange += new IOC.JMDM20DIOV2.JMDM20DIOV2Iocer.StatusChangeHandler(Iocer_StatusChange);
                success = Hardwarer.Iocer.OpenCom(commonDAO.GetAppletConfigInt32("IO������_����"), commonDAO.GetAppletConfigInt32("IO������_������"), commonDAO.GetAppletConfigInt32("IO������_����λ"), (StopBits)commonDAO.GetAppletConfigInt32("IO������_ֹͣλ"), (Parity)commonDAO.GetAppletConfigInt32("IO������_У��λ"));
                if (!success) MessageBoxEx.Show("IO����������ʧ�ܣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.iocControler = new IocControler(Hardwarer.Iocer);

                if (commonDAO.GetAppletConfigString("���ó���ʶ��1") == "1")
                {
                    // ����ʶ��1
                    Hardwarer.Rwer1.OnActionStatusChange = Rwer1_OnStatusChange;
                    Hardwarer.Rwer1.OnActionScanError = Rwer1_OnScanError;
                    Hardwarer.Rwer1.OnActionReadSuccess = Rwer1_OnReadSuccess;
                    success = Hardwarer.Rwer1.ConnectCamera(commonDAO.GetAppletConfigString("����ʶ��1_IP��ַ"), IntPtr.Zero);
                    if (!success)
                    {
                        MessageBoxEx.Show("����ʶ��1����ʧ�ܣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        Hardwarer.Rwer1.StartPreview(panVideo1.Handle);
                    }
                }

                if (commonDAO.GetAppletConfigString("���ó���ʶ��2") == "1")
                {
                    // ����ʶ��2
                    Hardwarer.Rwer2.OnActionStatusChange = Rwer2_OnStatusChange;
                    Hardwarer.Rwer2.OnActionScanError = Rwer2_OnScanError;
                    Hardwarer.Rwer2.OnActionReadSuccess = Rwer2_OnReadSuccess;
                    success = Hardwarer.Rwer2.ConnectCamera(commonDAO.GetAppletConfigString("����ʶ��2_IP��ַ"), IntPtr.Zero);
                    if (!success)
                    {
                        MessageBoxEx.Show("����ʶ��2����ʧ�ܣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    else
                    {
                        Hardwarer.Rwer2.StartPreview(panVideo2.Handle);
                    }
                }


                #region LED���ƿ�1
                /*
                string led1SocketIP = commonDAO.GetAppletConfigString("LED��ʾ��1_IP��ַ");
                if (!string.IsNullOrEmpty(led1SocketIP))
                {
                    int nResult = YB14DynamicAreaLeder.AddScreen(YB14DynamicAreaLeder.CONTROLLER_BX_5E1, this.LED1nScreenNo, YB14DynamicAreaLeder.SEND_MODE_NETWORK, 96, 32, 1, 1, "", 0, led1SocketIP, 5005, "");
                    if (nResult == YB14DynamicAreaLeder.RETURN_NOERROR)
                    {
                        nResult = YB14DynamicAreaLeder.AddScreenDynamicArea(this.LED1nScreenNo, this.LED1DYArea_ID, 0, 10, 1, "", 0, 0, 0, 96, 32, 255, 0, 255, 7, 6, 1);
                        if (nResult == YB14DynamicAreaLeder.RETURN_NOERROR)
                        {
                            nResult = YB14DynamicAreaLeder.AddScreenDynamicAreaFile(this.LED1nScreenNo, this.LED1DYArea_ID, this.LED1TempFile, 0, "����", 12, 0, 120, 1, 3, 0);
                            if (nResult == YB14DynamicAreaLeder.RETURN_NOERROR)
                            {
                                // ��ʼ���ɹ�
                                this.LED1ConnectStatus = true;
                                UpdateLed1Show("  �ȴ�����");

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
                */
                #endregion

                #region LED���ƿ�2
                /*
                string led2SocketIP = commonDAO.GetAppletConfigString("LED��ʾ��2_IP��ַ");
                if (!string.IsNullOrEmpty(led2SocketIP))
                {
                    int nResult = YB14DynamicAreaLeder.AddScreen(YB14DynamicAreaLeder.CONTROLLER_BX_5E1, this.LED2nScreenNo, YB14DynamicAreaLeder.SEND_MODE_NETWORK, 96, 32, 1, 1, "", 0, led2SocketIP, 5005, "");
                    if (nResult == YB14DynamicAreaLeder.RETURN_NOERROR)
                    {
                        nResult = YB14DynamicAreaLeder.AddScreenDynamicArea(this.LED2nScreenNo, this.LED2DYArea_ID, 0, 10, 1, "", 0, 0, 0, 96, 32, 255, 0, 255, 7, 6, 1);
                        if (nResult == YB14DynamicAreaLeder.RETURN_NOERROR)
                        {
                            nResult = YB14DynamicAreaLeder.AddScreenDynamicAreaFile(this.LED2nScreenNo, this.LED2DYArea_ID, this.LED2TempFile, 0, "����", 12, 0, 120, 1, 3, 0);
                            if (nResult == YB14DynamicAreaLeder.RETURN_NOERROR)
                            {
                                // ��ʼ���ɹ�
                                this.LED2ConnectStatus = true;
                                UpdateLed2Show("  �ȴ�����");
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
                */
                #endregion

                timer1.Enabled = true;
            }
            catch (Exception ex)
            {
                Log4Neter.Error("�豸��ʼ��", ex);
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

        #region ��բ���ư�ť

        /// <summary>
        /// ��բ1����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGate1Up_Click(object sender, EventArgs e)
        {
            if (this.iocControler != null) this.iocControler.Gate1Up();
        }

        /// <summary>
        /// ��բ1����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGate1Down_Click(object sender, EventArgs e)
        {
            if (this.iocControler != null) this.iocControler.Gate1Down();
        }

        /// <summary>
        /// ��բ2����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGate2Up_Click(object sender, EventArgs e)
        {
            if (this.iocControler != null) this.iocControler.Gate2Up();
        }

        /// <summary>
        /// ��բ2����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnGate2Down_Click(object sender, EventArgs e)
        {
            if (this.iocControler != null) this.iocControler.Gate2Down();
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
            timer1.Interval = 2000;

            try
            {
                // ִ��Զ������
                ExecAppRemoteControlCmd();

                switch (this.CurrentFlowFlag)
                {
                    case eFlowFlag.�ȴ�����:
                        #region

                        if (passCarQueuer.Count > 0) this.CurrentFlowFlag = eFlowFlag.ʶ����;

                        #endregion
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
                        UpdateLedShow(this.CurrentImperfectCar.Voucher);

                        if (this.CurrentAutotruck != null)
                        {
                            if (this.CurrentAutotruck.IsUse == 1)
                            {
                                if (this.CurrentAutotruck.IsInner == 1)
                                {
                                    LetPass();
                                    this.timer_Visit_Cancel = false;
                                    this.CurrentFlowFlag = eFlowFlag.�ȴ��뿪;
                                    timer_Visit_Tick(null, null);
                                    Log4Neter.Info(string.Format("���ƺţ�{0} �ڲ�����", this.CurrentAutotruck.CarNumber));
                                }
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

                                timer1.Interval = 20000;
                            }
                        }
                        else
                        {
                            UpdateLedShow(this.CurrentImperfectCar.Voucher, "δ�Ǽ�");

                            // ��ʽһ������ʶ��
                            this.voiceSpeaker.Speak("���ƺ� " + this.CurrentImperfectCar.Voucher + " δ�Ǽǣ���ֹͨ��", 2, false);

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

                // ���ó���
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
        /// �г����ڵ�ǰ��·��
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

                this.CurrentFlowFlag = eFlowFlag.ʶ����;
            }
        }

        /// <summary>
        /// �����볧ú�����¼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveTransport_BuyFuel_Click(object sender, EventArgs e)
        {
            if (!SaveBuyFuelTransport()) MessageBoxEx.Show("����ʧ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                if (outerDAO.SaveBuyFuelTransport(this.CurrentBuyFuelTransport.Id, DateTime.Now))
                {
                    // ��ӡ����
                    // TODO

                    btnSaveTransport_BuyFuel.Enabled = false;
                    this.CurrentFlowFlag = eFlowFlag.�ȴ��뿪;

                    UpdateLedShow("�����ɹ�", "���뿪");

                    if (!this.AutoHandMode) MessageBoxEx.Show("�����ɹ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadTodayUnFinishBuyFuelTransport();
                    LoadTodayFinishBuyFuelTransport();

                    LetPass();

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

            btnSaveTransport_BuyFuel.Enabled = false;

            LetBlocking();
            UpdateLedShow("  �ȴ�����");

            // �������
            this.CurrentImperfectCar = null;
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
                                //if (carTransportDAO.CheckNextTruckInFactoryWay(this.CurrentAutotruck.CarType, this.CurrentBuyFuelTransport.StepName, "����", CommonAppConfig.GetInstance().AppIdentifier, out nextPlace))
                                if (true)
                                {
                                    if (this.CurrentBuyFuelTransport.SuttleWeight > 0)
                                    {
                                        this.CurrentFlowFlag = eFlowFlag.������Ϣ;
                                    }
                                    else
                                    {
                                        UpdateLedShow(this.CurrentAutotruck.CarNumber, "����δ���");
                                        this.voiceSpeaker.Speak("���ƺ� " + this.CurrentAutotruck.CarNumber + " ����δ���", 2, false);

                                        timer_BuyFuel.Interval = 20000;
                                    }
                                }
                                else
                                {
                                    UpdateLedShow("·�ߴ���", "��ֹͨ��");
                                    this.voiceSpeaker.Speak("·�ߴ��� ��ֹͨ�� " + (!string.IsNullOrEmpty(nextPlace) ? "��ǰ��" + nextPlace : ""), 2, false);

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
                            UpdateLedShow(this.CurrentAutotruck.CarNumber, "δ�Ŷ�");
                            this.voiceSpeaker.Speak("���ƺ� " + this.CurrentAutotruck.CarNumber + " δ�ҵ��ŶӼ�¼", 2, false);

                            timer_BuyFuel.Interval = 20000;
                        }

                        #endregion
                        break;

                    case eFlowFlag.������Ϣ:
                        #region

                        if (this.AutoHandMode)
                        {
                            // �Զ�ģʽ
                            if (!SaveBuyFuelTransport())
                            {
                                UpdateLedShow(this.CurrentAutotruck.CarNumber, "����ʧ��");
                                this.voiceSpeaker.Speak("���ƺ� " + this.CurrentAutotruck.CarNumber + " ����ʧ�ܣ�����ϵ����Ա", 2, false);
                            }
                        }
                        else
                        {
                            // �ֶ�ģʽ 
                        }

                        #endregion
                        break;

                    case eFlowFlag.�ȴ��뿪:
                        #region

                        // ��ǰ��·�ظ����ź�ʱ����
                        if (!HasCarOnCurrentWay()) ResetBuyFuel();

                        // ����������
                        timer_BuyFuel.Interval = 4000;

                        #endregion
                        break;
                }

                // ��ǰ��·�ظ����ź�ʱ����
                if (!HasCarOnCurrentWay() && this.CurrentFlowFlag != eFlowFlag.�ȴ����� && (this.CurrentImperfectCar != null && this.CurrentImperfectCar.IsFromDevice)) ResetBuyFuel();
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
            superGridControl1_BuyFuel.PrimaryGrid.DataSource = outerDAO.GetUnFinishBuyFuelTransport();
        }

        /// <summary>
        /// ��ȡָ����������ɵ��볧ú��¼
        /// </summary>
        void LoadTodayFinishBuyFuelTransport()
        {
            superGridControl2_BuyFuel.PrimaryGrid.DataSource = outerDAO.GetFinishedBuyFuelTransport(DateTime.Now.Date, DateTime.Now.Date.AddDays(1));
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
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.��ǰ�����¼Id.ToString(), string.Empty);
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
        /// ѡ����
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

                this.CurrentFlowFlag = eFlowFlag.ʶ����;
            }
        }

        /// <summary>
        /// �����볧ú�����¼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveTransport_SaleFuel_Click(object sender, EventArgs e)
        {
            if (!SaveSaleFuelTransport()) MessageBoxEx.Show("����ʧ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                if (outerDAO.SaveSaleFuelTransport(this.CurrentSaleFuelTransport.Id, DateTime.Now))
                {
                    // ��ӡ����

                    btnSaveTransport_SaleFuel.Enabled = false;
                    this.CurrentFlowFlag = eFlowFlag.�ȴ��뿪;

                    UpdateLedShow("�����ɹ�", "���뿪");

                    if (!this.AutoHandMode) MessageBoxEx.Show("�����ɹ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadTodayUnFinishSaleFuelTransport();
                    LoadTodayFinishSaleFuelTransport();

                    LetPass();

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

            btnSaveTransport_SaleFuel.Enabled = false;

            LetBlocking();
            UpdateLedShow("  �ȴ�����");

            // �������
            this.CurrentImperfectCar = null;
        }

        /// <summary>
        /// �볧ú�����¼ҵ��ʱ��
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

                        if (this.CurrentUnFinishTransport != null)
                        {
                            this.CurrentSaleFuelTransport = commonDAO.SelfDber.Get<CmcsSaleFuelTransport>(this.CurrentUnFinishTransport.TransportId);
                            if (this.CurrentSaleFuelTransport != null)
                            {
                                // �ж�·������
                                string nextPlace;
                                //if (carTransportDAO.CheckNextTruckInFactoryWay(this.CurrentAutotruck.CarType, this.CurrentSaleFuelTransport.StepName, "����", CommonAppConfig.GetInstance().AppIdentifier, out nextPlace))
                                if (true)
                                {
                                    if (this.CurrentSaleFuelTransport.SuttleWeight > 0)
                                    {
                                        this.CurrentFlowFlag = eFlowFlag.������Ϣ;

                                        UpdateLedShow(this.CurrentAutotruck.CarNumber, "���뿪");
                                    }
                                    else
                                    {
                                        UpdateLedShow(this.CurrentAutotruck.CarNumber, "����δ���");
                                        this.voiceSpeaker.Speak("���ƺ� " + this.CurrentAutotruck.CarNumber + " ����δ���", 2, false);

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
                        }
                        else
                        {
                            UpdateLedShow(this.CurrentAutotruck.CarNumber, "δ�Ŷ�");
                            this.voiceSpeaker.Speak("���ƺ� " + this.CurrentAutotruck.CarNumber + " δ�ҵ��ŶӼ�¼", 2, false);

                            timer_SaleFuel.Interval = 20000;
                        }

                        #endregion
                        break;

                    case eFlowFlag.������Ϣ:
                        #region
                        if (this.AutoHandMode)
                        {
                            // �Զ�ģʽ
                            if (!SaveSaleFuelTransport())
                            {
                                UpdateLedShow(this.CurrentAutotruck.CarNumber, "����ʧ��");
                                this.voiceSpeaker.Speak("���ƺ� " + this.CurrentAutotruck.CarNumber + " ����ʧ�ܣ�����ϵ����Ա", 2, false);
                            }
                        }
                        else
                        {
                            btnSaveTransport_SaleFuel.Enabled = true;
                            // �ֶ�ģʽ 
                        }

                        #endregion
                        break;

                    case eFlowFlag.�ȴ��뿪:
                        #region

                        // ��ǰ��·�ظ����ź�ʱ����
                        if (!HasCarOnCurrentWay()) ResetSaleFuel();

                        // ����������
                        timer_SaleFuel.Interval = 4000;

                        #endregion
                        break;
                }

                // ��ǰ��·�ظ����ź�ʱ����
                if (!HasCarOnCurrentWay() && this.CurrentFlowFlag != eFlowFlag.�ȴ����� && (this.CurrentImperfectCar != null && this.CurrentImperfectCar.IsFromDevice)) ResetSaleFuel();
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
        /// ��ȡδ��ɵ��볧ú��¼
        /// </summary>
        void LoadTodayUnFinishSaleFuelTransport()
        {
            superGridControl1_SaleFuel.PrimaryGrid.DataSource = outerDAO.GetUnFinishSaleFuelTransport();
        }

        /// <summary>
        /// ��ȡָ����������ɵ��볧ú��¼
        /// </summary>
        void LoadTodayFinishSaleFuelTransport()
        {
            superGridControl2_SaleFuel.PrimaryGrid.DataSource = outerDAO.GetFinishedSaleFuelTransport(DateTime.Now.Date, DateTime.Now.Date.AddDays(1));
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
        /// ѡ����
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

                this.CurrentFlowFlag = eFlowFlag.ʶ����;
            }
        }

        /// <summary>
        /// ���������¼
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
                if (outerDAO.SaveGoodsTransport(this.CurrentGoodsTransport.Id, DateTime.Now))
                {
                    this.CurrentGoodsTransport = commonDAO.SelfDber.Get<CmcsGoodsTransport>(this.CurrentGoodsTransport.Id);

                    btnSaveTransport_Goods.Enabled = false;
                    this.CurrentFlowFlag = eFlowFlag.�ȴ��뿪;

                    UpdateLedShow("�����ɹ�", "���뿪");
                    if (!this.AutoHandMode) MessageBoxEx.Show("�����ɹ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadTodayUnFinishGoodsTransport();
                    LoadTodayFinishGoodsTransport();

                    LetPass();

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

            btnSaveTransport_Goods.Enabled = false;

            LetBlocking();
            UpdateLedShow("  �ȴ�����");

            // �������
            this.CurrentImperfectCar = null;
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
                        if (this.CurrentUnFinishTransport != null)
                        {
                            this.CurrentGoodsTransport = commonDAO.SelfDber.Get<CmcsGoodsTransport>(this.CurrentUnFinishTransport.TransportId);
                            if (this.CurrentGoodsTransport != null)
                            {
                                // �ж�·������
                                string nextPlace;
                                //if (carTransportDAO.CheckNextTruckInFactoryWay(this.CurrentAutotruck.CarType, this.CurrentBuyFuelTransport.StepName, "����", CommonAppConfig.GetInstance().AppIdentifier, out nextPlace))
                                if (true)
                                {
                                    if (this.CurrentGoodsTransport.SuttleWeight > 0)
                                    {
                                        this.CurrentFlowFlag = eFlowFlag.������Ϣ;

                                        UpdateLedShow(this.CurrentAutotruck.CarNumber, "���뿪");
                                    }
                                    else
                                    {
                                        UpdateLedShow(this.CurrentAutotruck.CarNumber, "����δ���");
                                        this.voiceSpeaker.Speak("���ƺ� " + this.CurrentAutotruck.CarNumber + " ����δ���", 2, false);

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
                        }
                        else
                        {
                            UpdateLedShow(this.CurrentAutotruck.CarNumber, "δ�Ŷ�");
                            this.voiceSpeaker.Speak("���ƺ� " + this.CurrentAutotruck.CarNumber + " δ�ҵ��ŶӼ�¼", 2, false);

                            timer_Goods.Interval = 20000;
                        }

                        #endregion
                        break;

                    case eFlowFlag.������Ϣ:
                        #region

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

                        #endregion
                        break;

                    case eFlowFlag.�ȴ��뿪:
                        #region

                        // ��ǰ��·�ظ����ź�ʱ����
                        if (!HasCarOnCurrentWay()) ResetGoods();

                        // ����������
                        timer_Goods.Interval = 4000;

                        #endregion
                        break;
                }

                // ��ǰ��·�ظ����ź�ʱ����
                if (!HasCarOnCurrentWay() && this.CurrentFlowFlag != eFlowFlag.�ȴ����� && (this.CurrentImperfectCar != null && this.CurrentImperfectCar.IsFromDevice)) ResetGoods();
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
            superGridControl1_Goods.PrimaryGrid.DataSource = outerDAO.GetUnFinishGoodsTransport();
        }

        /// <summary>
        /// ��ȡָ����������ɵ��������ʼ�¼
        /// </summary>
        void LoadTodayFinishGoodsTransport()
        {
            superGridControl2_Goods.PrimaryGrid.DataSource = outerDAO.GetFinishedGoodsTransport(DateTime.Now.Date, DateTime.Now.Date.AddDays(1));
        }

        #endregion

        #region ���ó���

        bool timer_Visit_Cancel = true;

        CmcsVisitTransport currentVisitTransport;
        /// <summary>
        /// ��ǰ�����¼
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
        /// ѡ����
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

                this.CurrentFlowFlag = eFlowFlag.ʶ����;
            }
        }

        /// <summary>
        /// ���������¼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveTransport_Visit_Click(object sender, EventArgs e)
        {
            if (!SaveVisitTransport()) MessageBoxEx.Show("����ʧ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// ���������¼
        /// </summary>
        /// <returns></returns>
        bool SaveVisitTransport()
        {
            if (this.CurrentVisitTransport == null) return false;

            try
            {
                // �����볧ú�ŶӼ�¼��ͬʱ����������Ϣ�Լ����ƻ���������
                if (outerDAO.SaveVisitTransport(this.CurrentVisitTransport.Id, DateTime.Now))
                {
                    btnSaveTransport_Visit.Enabled = false;
                    this.CurrentFlowFlag = eFlowFlag.�ȴ��뿪;

                    UpdateLedShow("�����ɹ�", "���뿪");
                    if (!this.AutoHandMode) MessageBoxEx.Show("�����ɹ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadTodayUnFinishVisitTransport();
                    LoadTodayFinishVisitTransport();

                    LetPass();

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
        private void btnReset_Visit_Click(object sender, EventArgs e)
        {
            ResetVisit();
        }

        /// <summary>
        /// ������Ϣ
        /// </summary>
        void ResetVisit()
        {
            this.timer_Visit_Cancel = true;

            this.CurrentFlowFlag = eFlowFlag.�ȴ�����;

            this.CurrentAutotruck = null;

            btnSaveTransport_Visit.Enabled = true;

            LetBlocking();
            UpdateLedShow("  �ȴ�����");

            // �������
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
                    case eFlowFlag.��֤��Ϣ:
                        #region
                        if (this.CurrentUnFinishTransport != null)
                        {
                            this.CurrentVisitTransport = commonDAO.SelfDber.Get<CmcsVisitTransport>(this.CurrentUnFinishTransport.TransportId);
                            if (this.CurrentVisitTransport != null)
                            {
                                this.CurrentFlowFlag = eFlowFlag.������Ϣ;
                            }
                            else
                            {
                                commonDAO.SelfDber.Delete<CmcsUnFinishTransport>(this.CurrentUnFinishTransport.Id);
                            }
                        }
                        else
                        {
                            UpdateLedShow(this.CurrentAutotruck.CarNumber, "δ�Ŷ�");
                            this.voiceSpeaker.Speak("���ƺ� " + this.CurrentAutotruck.CarNumber + " δ�ҵ��ŶӼ�¼", 2, false);

                            timer_Visit.Interval = 20000;
                        }

                        #endregion
                        break;

                    case eFlowFlag.������Ϣ:
                        #region

                        if (this.AutoHandMode)
                        {
                            // �Զ�ģʽ
                            if (!SaveVisitTransport())
                            {
                                UpdateLedShow(this.CurrentAutotruck.CarNumber, "����ʧ��");
                                this.voiceSpeaker.Speak("���ƺ� " + this.CurrentAutotruck.CarNumber + " ����ʧ�ܣ�����ϵ����Ա", 2, false);
                            }
                        }
                        else
                        {
                            // �ֶ�ģʽ 
                        }

                        #endregion
                        break;

                    case eFlowFlag.�ȴ��뿪:
                        #region

                        // ��ǰ��·�ظ����ź�ʱ����
                        if (!HasCarOnCurrentWay()) ResetVisit();

                        // ����������
                        timer_Visit.Interval = 4000;

                        #endregion
                        break;
                }

                // ��ǰ��·�ظ����ź�ʱ����
                if (!HasCarOnCurrentWay() && this.CurrentFlowFlag != eFlowFlag.�ȴ����� && (this.CurrentImperfectCar != null && this.CurrentImperfectCar.IsFromDevice)) ResetVisit();
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
        /// ��ȡδ��ɵ����ó�����¼
        /// </summary>
        void LoadTodayUnFinishVisitTransport()
        {
            superGridControl1_Visit.PrimaryGrid.DataSource = outerDAO.GetUnFinishVisitTransport();
        }

        /// <summary>
        /// ��ȡָ����������ɵ����ó�����¼
        /// </summary>
        void LoadTodayFinishVisitTransport()
        {
            superGridControl2_Visit.PrimaryGrid.DataSource = outerDAO.GetFinishedVisitTransport(DateTime.Now.Date, DateTime.Now.Date.AddDays(1));
        }

        #endregion

        #region ��������

        Pen redPen3 = new Pen(Color.Red, 3);
        Pen greenPen3 = new Pen(Color.Lime, 3);
        Pen greenPen1 = new Pen(Color.Lime, 1);

        /// <summary>
        /// ��ǰ�Ǳ�����������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panCurrentWeight_Paint(object sender, PaintEventArgs e)
        {
            PanelEx panel = sender as PanelEx;

            // ���Ƶظ�1
            e.Graphics.DrawLine(this.InductorCoil1 ? redPen3 : greenPen3, 15, 10, 15, 30);
            // ���Ƶظ�2                                                               
            e.Graphics.DrawLine(this.InductorCoil2 ? redPen3 : greenPen3, 25, 10, 25, 30);
            // ���Ʒָ���
            e.Graphics.DrawLine(greenPen1, 5, 34, 35, 34);
            // ���Ƶظ�3
            e.Graphics.DrawLine(this.InductorCoil3 ? redPen3 : greenPen3, 15, 38, 15, 58);
            // ���Ƶظ�4                                                               
            e.Graphics.DrawLine(this.InductorCoil4 ? redPen3 : greenPen3, 25, 38, 25, 58);
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
