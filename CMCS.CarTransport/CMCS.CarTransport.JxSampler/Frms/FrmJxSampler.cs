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
        /// ����Ψһ��ʶ��
        /// </summary>
        public static string UniqueKey = "FrmCarSampler";

        public FrmJxSampler()
        {
            InitializeComponent();
        }

        #region ҵ������
        CarTransportDAO carTransportDAO = CarTransportDAO.GetInstance();
        JxSamplerDAO jxSamplerDAO = JxSamplerDAO.GetInstance();
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

        bool affirmCoil = false;
        /// <summary>
        /// ��ť״̬ true=���ź�  false=���ź�
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
                if (this.CurrentFlowFlag == eFlowFlag.���ͼƻ� && value)
                    affirmCoil = value;
                else if (this.CurrentFlowFlag != eFlowFlag.���ͼƻ�)
                    affirmCoil = value;
                commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.ȷ�ϰ�ť.ToString(), value ? "1" : "0");
            }
        }

        int affirmPort;
        /// <summary>
        /// ��ť�˿�
        /// </summary>
        public int AffirmPort
        {
            get { return affirmPort; }
            set { affirmPort = value; }
        }

        #endregion

        #region ����Vars

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

                btnSendSamplingPlan.Visible = !value;
                btnSelectAutotruck.Visible = !value;
                btnReset.Visible = !value;
            }
        }

        bool handSend = false;
        /// <summary>
        /// �ֶ�����= true �Զ�����=false 
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

        private CmcsBuyFuelTransport currentBuyFuelTransport;
        /// <summary>
        /// ��ǰ�볡ú�����¼
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
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, "����", value.TicketWeight.ToString());
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.��ǰ����.ToString(), value.CarNumber);
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.��������.ToString(), dbtxtCYPoint.Text);
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
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.��ǰ�����¼Id.ToString(), string.Empty);
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, "����", string.Empty);
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.��ǰ����.ToString(), string.Empty);
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.��������.ToString(), string.Empty);

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
        /// ��ǰ����ú�����¼
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
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.��ǰ����.ToString(), value.CarNumber);
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.��������.ToString(), dbtxtCYPoint.Text);

                    labSupplierName.Text = "���յ�λ";
                    CmcsSupplier supplier = Dbers.GetInstance().SelfDber.Get<CmcsSupplier>(value.SupplierId);
                    if (supplier != null)
                        txtSupplierName.Text = supplier.Name;
                    txtTransportCompanyName.Text = value.TransportCompanyName;

                    labMineName.Visible = false;
                    txtMineName.Visible = false;

                    CmcsFuelKind fuelkind = Dbers.GetInstance().SelfDber.Get<CmcsFuelKind>(value.FuelKindId);
                    txtFuelKindName.Text = fuelkind.FuelName;

                    labTicketWeight.Text = "��ú��";
                    txtTicketWeight.Text = value.Outweight.ToString();
                    txtSamplingType.Text = "��е����";
                }
                else
                {
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.��ǰ�����¼Id.ToString(), string.Empty);
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.��ǰ����.ToString(), string.Empty);
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.��������.ToString(), string.Empty);

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
        /// ��ǰʶ�𳵺�
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
        /// ���ڲ���ʱʶ�𵽵ĳ���
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

                    txtCarNumber.Text = value.CarNumber;
                    panCurrentCarNumber.Text = value.CarNumber;
                }
                else
                {
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.��ǰ��Id.ToString(), string.Empty);
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.��ǰ����.ToString(), string.Empty);

                    txtCarNumber.ResetText();
                    panCurrentCarNumber.ResetText();
                }

                PreviewCarCarriage(value);
            }
        }

        private InfQCJXCYSampleCMD currentSampleCMD;
        /// <summary>
        /// ��ǰ��������
        /// </summary>
        public InfQCJXCYSampleCMD CurrentSampleCMD
        {
            get { return currentSampleCMD; }
            set { currentSampleCMD = value; }
        }

        private CmcsRCSampling currentSample;
        /// <summary>
        /// ��ǰ������
        /// </summary>
        public CmcsRCSampling CurrentSample
        {
            get { return currentSample; }
            set { currentSample = value; }
        }

        private eEquInfSamplerSystemStatus samplerSystemStatus;
        /// <summary>
        /// ������ϵͳ״̬
        /// </summary>
        public eEquInfSamplerSystemStatus SamplerSystemStatus
        {
            get { return samplerSystemStatus; }
            set
            {
                samplerSystemStatus = value;

                if (value == eEquInfSamplerSystemStatus.�������� || value == eEquInfSamplerSystemStatus.�������)
                    slightSamplerStatus.LightColor = EquipmentStatusColors.BeReady;
                else if (value == eEquInfSamplerSystemStatus.�������� || value == eEquInfSamplerSystemStatus.����ж��)
                    slightSamplerStatus.LightColor = EquipmentStatusColors.Working;
                else //if (value == eEquInfSamplerSystemStatus.��������)
                    slightSamplerStatus.LightColor = EquipmentStatusColors.Breakdown;
            }
        }

        /// <summary>
        /// �������豸����
        /// </summary>
        public string SamplerMachineCode;
        /// <summary>
        /// �������豸����
        /// </summary>
        public string SamplerMachineName;

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

            // �������豸����
            //this.SamplerMachineCode = commonDAO.GetAppletConfigString("�������豸����");
            this.SamplerMachineCode = CommonAppConfig.GetInstance().AppIdentifier;
            this.SamplerMachineName = commonDAO.GetMachineNameByCode(this.SamplerMachineCode);
            lblSampleStatus.Text = SamplerMachineName;

            // Ĭ���Զ�
            sbtnChangeAutoHandMode.Value = true;

            // ���ó���Զ�̿�������
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
            // ж���豸
            UnloadHardware();
        }

        #region �豸���

        #region IO������

        void Iocer_StatusChange(bool status)
        {
            // ����IO������״̬ 
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

                  this.AffirmCoil = (receiveValue[this.AffirmPort - 1] == 1);
              });
        }

        /// <summary>
        /// ������
        /// </summary>
        void BackGateUp()
        {
            this.iocControler.Gate1Up();
        }

        /// <summary>
        /// �󷽽���
        /// </summary>
        void BackGateDown()
        {
            if (!this.InductorCoil2)
                this.iocControler.Gate1Down();
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
                    this.txtCarNumber.Text = carnumber;
                    passCarQueuer.Enqueue(CarNumber);
                    this.CurrentFlowFlag = eFlowFlag.��֤����;
                    this.NextCarNumber = null;//����״̬��ʶ�𵽳��� ��һ������Ϊ��
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.��ǰ����.ToString(), carnumber);
                    timer1_Tick(null, null);
                }
                else if (carnumber != "�޳���" && this.CurrentFlowFlag != eFlowFlag.�ȴ�����)//ǰ��δ�뿪ʱ ���ѽ���ʶ������
                {
                    this.NextCarNumber = carnumber;
                }
                Log4Neter.Info(string.Format("����ʶ��1ʶ�𵽳��ţ�{0}", carnumber));
            });
        }
        #endregion

        #region LED���ƿ�

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

                slightLED1.LightColor = (_LED1ConnectStatus ? Color.Green : Color.Red);

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
        /// ����LED��̬����
        /// </summary>
        /// <param name="value1">��һ������</param>
        /// <param name="value2">�ڶ�������</param>
        private void UpdateLedShow(string value1 = "", string value2 = "")
        {
#if DEBUG
            FrmDebugConsole.GetInstance().Output("����LED1:|" + value1 + "|" + value2 + "|");
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
                this.AffirmPort = commonDAO.GetAppletConfigInt32("IO������_ȷ�ϰ�ť�˿�");
                // IO������
                Hardwarer.Iocer.OnReceived += new IOC.JMDM20DIOV2.JMDM20DIOV2Iocer.ReceivedEventHandler(Iocer_Received);
                Hardwarer.Iocer.OnStatusChange += new IOC.JMDM20DIOV2.JMDM20DIOV2Iocer.StatusChangeHandler(Iocer_StatusChange);
                success = Hardwarer.Iocer.OpenCom(commonDAO.GetAppletConfigInt32("IO������_����"), commonDAO.GetAppletConfigInt32("IO������_������"), commonDAO.GetAppletConfigInt32("IO������_����λ"), (StopBits)commonDAO.GetAppletConfigInt32("IO������_ֹͣλ"), (Parity)commonDAO.GetAppletConfigInt32("IO������_У��λ"));
                if (!success) MessageBoxEx.Show("IO����������ʧ�ܣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.iocControler = new IocControler(Hardwarer.Iocer);

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

                #region LED���ƿ�

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
                                UpdateLedShow("  �ȴ�����");
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

            }
            catch (Exception ex)
            {
                Log4Neter.Error("�豸��ʼ��", ex);
            }
            finally
            {
                timer1.Enabled = true;
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
            if (this.iocControler != null && !this.InductorCoil2) this.iocControler.Gate1Down();
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

                        if (this.InductorCoil1 && this.NextCarNumber != null)
                        {
                            // ����������ظ����źţ����Ѿ���ǰ��ȡ����һ��������Ϣ
                            passCarQueuer.Enqueue(this.NextCarNumber);
                        }

                        if (passCarQueuer.Count > 0) this.CurrentFlowFlag = eFlowFlag.��֤����;

                        #endregion
                        break;

                    case eFlowFlag.��֤����:
                        #region

                        // �������޳�ʱ���ȴ�����
                        //if (passCarQueuer.Count == 0)
                        //{
                        //    this.CurrentFlowFlag = eFlowFlag.�ȴ��뿪;
                        //    break;
                        //}

                        this.CurrentImperfectCar = passCarQueuer.Dequeue();

                        // ����ʶ��ĳ��ƺŲ��ҳ�����Ϣ
                        this.CurrentAutotruck = carTransportDAO.GetAutotruckByCarNumber(this.CurrentImperfectCar.Voucher);
                        UpdateLedShow(this.CurrentImperfectCar.Voucher);

                        if (this.CurrentAutotruck != null)
                        {
                            if (this.CurrentAutotruck.IsUse == 1)
                            {
                                if (this.CurrentAutotruck.CarriageLength > 0 && this.CurrentAutotruck.CarriageWidth > 0 && this.CurrentAutotruck.CarriageBottomToFloor > 0)
                                {
                                    // δ��������¼
                                    CmcsUnFinishTransport unFinishTransport = carTransportDAO.GetUnFinishTransportByAutotruckId(this.CurrentAutotruck.Id);
                                    if (unFinishTransport != null)
                                    {
                                        if (unFinishTransport.CarType == eTransportType.ԭ��ú�볡.ToString() || unFinishTransport.CarType == eTransportType.�ִ�ú�볡.ToString() || unFinishTransport.CarType == eTransportType.��תú�볡.ToString())
                                            this.CurrentBuyFuelTransport = carTransportDAO.GetBuyFuelTransportById(unFinishTransport.TransportId);
                                        else if (unFinishTransport.CarType == eTransportType.�ִ�ú����.ToString() || unFinishTransport.CarType == eTransportType.��תú����.ToString() || unFinishTransport.CarType == eTransportType.����ֱ��ú.ToString() || unFinishTransport.CarType == eTransportType.���۲���ú.ToString())
                                            this.CurrentSaleFuelTransport = carTransportDAO.GetSaleFuelTransportById(unFinishTransport.TransportId);

                                        if (this.CurrentBuyFuelTransport != null || this.CurrentSaleFuelTransport != null)
                                        {
                                            // �ж�·������
                                            string nextPlace;
                                            if (true)
                                            //if (carTransportDAO.CheckNextTruckInFactoryWay(this.CurrentAutotruck.CarType, this.CurrentBuyFuelTransport.StepName, "����", CommonAppConfig.GetInstance().AppIdentifier, out nextPlace))
                                            {
                                                BackGateUp();

                                                btnSendSamplingPlan.Enabled = true;

                                                this.CurrentFlowFlag = eFlowFlag.�ȴ�ʻ��;

                                                UpdateLedShow(this.CurrentAutotruck.CarNumber, "ʻ����������");
                                                this.voiceSpeaker.Speak("���ƺ� " + this.CurrentAutotruck.CarNumber + " ʻ����������", 2, false);
                                            }
                                            else
                                            {
                                                UpdateLedShow("·�ߴ���", "��ֹͨ��");
                                                this.voiceSpeaker.Speak("·�ߴ��� ��ֹͨ�� " + (!string.IsNullOrEmpty(nextPlace) ? "��ǰ��" + nextPlace : ""), 2, false);
                                                timer1.Interval = 5000;
                                                this.CurrentFlowFlag = eFlowFlag.��֤ʧ��;
                                            }
                                        }
                                        else
                                        {
                                            this.UpdateLedShow(this.CurrentAutotruck.CarNumber, "δ�ҵ������¼");
                                            this.voiceSpeaker.Speak("���ƺ� " + this.CurrentAutotruck.CarNumber + " δ�ҵ������¼", 2, false);
                                            commonDAO.SelfDber.Delete<CmcsUnFinishTransport>(unFinishTransport.Id);
                                            timer1.Interval = 5000;
                                            this.CurrentFlowFlag = eFlowFlag.��֤ʧ��;
                                        }
                                    }
                                    else
                                    {
                                        this.UpdateLedShow(this.CurrentAutotruck.CarNumber, "δ�Ŷ�");
                                        this.voiceSpeaker.Speak("���ƺ� " + this.CurrentAutotruck.CarNumber + " δ�ҵ��ŶӼ�¼", 2, false);
                                        timer1.Interval = 5000;
                                        this.CurrentFlowFlag = eFlowFlag.��֤ʧ��;
                                    }
                                }
                                else
                                {
                                    this.UpdateLedShow(this.CurrentAutotruck.CarNumber, "����δ����");
                                    this.voiceSpeaker.Speak("���ƺ� " + this.CurrentAutotruck.CarNumber + " ����δ����", 2, false);

                                    timer1.Interval = 5000;
                                    this.CurrentFlowFlag = eFlowFlag.��֤ʧ��;
                                }
                            }
                            else
                            {
                                UpdateLedShow(this.CurrentAutotruck.CarNumber, "��ͣ��");
                                this.voiceSpeaker.Speak("���ƺ� " + this.CurrentAutotruck.CarNumber + " ��ͣ�ã���ֹͨ��", 2, false);
                                timer1.Interval = 5000;
                                this.CurrentFlowFlag = eFlowFlag.��֤ʧ��;
                            }
                        }
                        else
                        {
                            UpdateLedShow(this.CurrentImperfectCar.Voucher, "δ�Ǽ�");

                            this.voiceSpeaker.Speak("���ƺ� " + this.CurrentImperfectCar.Voucher + " δ�Ǽǣ���ֹͨ��", 2, false);

                            timer1.Interval = 5000;
                            this.CurrentFlowFlag = eFlowFlag.��֤ʧ��;
                        }

                        #endregion
                        break;

                    case eFlowFlag.��֤ʧ��:
                        #region MyRegion
                        if (!this.InductorCoil1)
                            ResetBuyFuel();
                        #endregion
                        break;

                    case eFlowFlag.�ȴ�ʻ��:
                        #region

                        // ����ڶ����ظ�/��բ�ظ����ź�
                        if (!this.InductorCoil1 && !this.InductorCoil2)
                        {
                            BackGateDown();
                            this.CurrentFlowFlag = eFlowFlag.���ͼƻ�;
                            this.UpdateLedShow("���³�", "���ȷ�ϰ�ť");
                            this.voiceSpeaker.Speak("˾�����³����ȷ�ϰ�ť", 2, false);
                        }

                        // ���������
                        timer1.Interval = 200;

                        #endregion
                        break;

                    case eFlowFlag.���ͼƻ�:
                        #region

                        // ��������ظ�,��⳵���Ƿ�ָ������
                        if ((this.AutoHandMode && (!this.InductorCoil1 && !this.InductorCoil2)) || (HandSend && !this.InductorCoil2))
                        {
                            if (HandSend || this.AffirmCoil)
                            {
                                if (this.SamplerSystemStatus == eEquInfSamplerSystemStatus.�������� || this.SamplerSystemStatus == eEquInfSamplerSystemStatus.�������)
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
                                            // ����Ԥ��
                                            TicketWeight = 0,
                                            // ����Ԥ��
                                            CarCount = 0,
                                            // ����������������߼�����
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
                                            ResultCode = eEquInfCmdResultCode.Ĭ��.ToString(),
                                            DataFlag = 0
                                        };

                                        // ���Ͳ����ƻ�
                                        if (commonDAO.SelfDber.Insert<InfQCJXCYSampleCMD>(CurrentSampleCMD) > 0)
                                        {
                                            this.CurrentFlowFlag = eFlowFlag.�ȴ�����;
                                            commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.��ʼʱ��.ToString(), DateTime.Now.Date.ToString("hh:mm:ss"));
                                            commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.��������.ToString(), CurrentSampleCMD.SampleCode);
                                            commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.��������.ToString(), CurrentSampleCMD.PointCount.ToString());
                                        }
                                    }
                                    else
                                    {
                                        this.UpdateLedShow("δ�ҵ���������Ϣ");
                                        this.voiceSpeaker.Speak("δ�ҵ���������Ϣ ����ϵ����Ա", 2, false);

                                        timer1.Interval = 5000;
                                    }
                                }
                                else
                                {
                                    this.UpdateLedShow("������δ����");
                                    this.voiceSpeaker.Speak("������δ����", 1, false);

                                    timer1.Interval = 5000;
                                }
                            }
                        }
                        else
                        {
                            this.UpdateLedShow("׼������", "Զ���������");
                            this.voiceSpeaker.Speak("׼������˾����Զ���������", 2, false);
                        }

                        #endregion
                        break;

                    case eFlowFlag.�ȴ�����:
                        #region
                        UpdateLedShow("  " + this.CurrentAutotruck.CarNumber, "  ���ڲ���");
                        // �жϲ����Ƿ����
                        InfQCJXCYSampleCMD qCJXCYSampleCMD = commonDAO.SelfDber.Get<InfQCJXCYSampleCMD>(this.CurrentSampleCMD.Id);
                        if (qCJXCYSampleCMD.ResultCode == eEquInfCmdResultCode.�ɹ�.ToString())
                        {
                            if (this.CurrentBuyFuelTransport != null)
                            {
                                if (jxSamplerDAO.SaveBuyFuelTransport(this.CurrentBuyFuelTransport.Id, DateTime.Now, CommonAppConfig.GetInstance().AppIdentifier))
                                {
                                    this.UpdateLedShow("  �������", "   ���뿪");
                                    this.voiceSpeaker.Speak("����������뿪��������", 2, false);

                                    this.CurrentFlowFlag = eFlowFlag.�ȴ��뿪;
                                    //������ɺ�������ã���Ԫ���Ƶ�բ�ó����뿪
                                }
                            }
                            else if (this.CurrentSaleFuelTransport != null)
                            {
                                if (jxSamplerDAO.SaveSaleFuelTransport(this.CurrentSaleFuelTransport.Id, DateTime.Now, CommonAppConfig.GetInstance().AppIdentifier))
                                {
                                    this.UpdateLedShow("  �������", "   ���뿪");
                                    this.voiceSpeaker.Speak("����������뿪��������", 2, false);

                                    this.CurrentFlowFlag = eFlowFlag.�ȴ��뿪;
                                    //������ɺ�������ã���Ԫ���Ƶ�բ�ó����뿪
                                }
                            }
                        }

                        // ����������
                        timer1.Interval = 4000;

                        #endregion
                        break;

                    case eFlowFlag.�ȴ��뿪:
                        #region
                        InfQCJXCYSampleCMD qCJXCYSampleCMD1 = commonDAO.SelfDber.Get<InfQCJXCYSampleCMD>(this.CurrentSampleCMD.Id);
                        if (qCJXCYSampleCMD1.ResultCode == eEquInfCmdResultCode.�ɹ�.ToString())
                        {
                            ResetBuyFuel();
                        }
                        // ����������
                        timer1.Interval = 4000;

                        #endregion
                        break;
                }

                // ���еظ����ź�/�����¼Ϊ��ʱ����
                if (this.AutoHandMode && !this.InductorCoil1 && !this.InductorCoil2 && this.CurrentFlowFlag != eFlowFlag.�ȴ�����
                    && this.CurrentBuyFuelTransport == null && this.CurrentSaleFuelTransport == null) ResetBuyFuel();
                commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.��������״̬.ToString(), this.CurrentFlowFlag.ToString());
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
            // ��������
            timer2.Interval = 180000;

            try
            {
                // �볡ú
                LoadTodayUnFinishBuyFuelTransport();
                LoadTodayFinishBuyFuelTransport();
                //����ú
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
        /// �л��ֶ�/�Զ�ģʽ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void sbtnChangeAutoHandMode_ValueChanged(object sender, EventArgs e)
        {
            this.AutoHandMode = sbtnChangeAutoHandMode.Value;
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

        #endregion

        #region �볧úҵ��

        /// <summary>
        /// ���Ͳ����ƻ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSendSamplingPlan_Click(object sender, EventArgs e)
        {
            if (SendSamplingPlan()) MessageBoxEx.Show("����ʧ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// ���Ͳ����ƻ�
        /// </summary>
        /// <returns></returns>
        bool SendSamplingPlan()
        {
            if (this.CurrentBuyFuelTransport == null && this.CurrentSaleFuelTransport == null)
            {
                MessageBoxEx.Show("��ѡ����", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            HandSend = true;
            this.CurrentFlowFlag = eFlowFlag.���ͼƻ�;

            return false;
        }

        /// <summary>
        /// �����볧ú�����¼
        /// </summary>
        void ResetBuyFuel()
        {
            this.CurrentFlowFlag = eFlowFlag.�ȴ�����;

            this.CarNumber = null;
            if (!InductorCoil1) this.NextCarNumber = null;
            this.CurrentAutotruck = null;
            this.CurrentBuyFuelTransport = null;
            this.CurrentSaleFuelTransport = null;
            this.CurrentSample = null;
            this.HandSend = false;
            btnSendSamplingPlan.Enabled = false;

            BackGateDown();

            UpdateLedShow("  �ȴ�����");

            // �������
            this.CurrentImperfectCar = null;
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetBuyFuel();
        }

        /// <summary>
        /// ˢ������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefres_Click(object sender, EventArgs e)
        {
            timer2_Tick(null, null);
        }

        /// <summary>
        /// ��ȡδ��ɵ��볧ú��¼
        /// </summary>
        void LoadTodayUnFinishBuyFuelTransport()
        {
            superGridControl1.PrimaryGrid.DataSource = jxSamplerDAO.GetUnFinishBuyFuelTransport();
        }

        /// <summary>
        /// ��ȡָ����������ɵĳ���ú��¼
        /// </summary>
        void LoadTodayFinishBuyFuelTransport()
        {
            superGridControl2.PrimaryGrid.DataSource = jxSamplerDAO.GetFinishedBuyFuelTransport(DateTime.Now.Date, DateTime.Now.Date.AddDays(1));
        }

        /// <summary>
        /// ��ȡδ��ɵĳ���ú��¼
        /// </summary>
        void LoadTodayUnFinishSaleFuelTransport()
        {
            superGridControl1_SaleFuel.PrimaryGrid.DataSource = QueuerDAO.GetInstance().GetUnFinishSaleFuelTransport();
        }

        /// <summary>
        /// ��ȡָ����������ɵ��볧ú��¼
        /// </summary>
        void LoadTodayFinishSaleFuelTransport()
        {
            superGridControl2_SaleFuel.PrimaryGrid.DataSource = QueuerDAO.GetInstance().GetFinishedSaleFuelTransport(DateTime.Now.Date, DateTime.Now.Date.AddDays(1));
        }

        #endregion

        #region ������Ϣ

        /// <summary>
        /// ѡ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectAutotruck_Click(object sender, EventArgs e)
        {
            FrmUnFinishTransport_Select frm = new FrmUnFinishTransport_Select(" order by CreateDate desc");
            if (frm.ShowDialog() == DialogResult.OK)
            {
                passCarQueuer.Enqueue(frm.Output.CarNumber);
                this.CurrentFlowFlag = eFlowFlag.��֤����;
            }
        }

        #endregion

        #region ����

        Pen redPen3 = new Pen(Color.Red, 3);
        Pen greenPen3 = new Pen(Color.Lime, 3);

        /// <summary>
        /// ��ǰ����������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void panCurrentCarNumber_Paint(object sender, PaintEventArgs e)
        {
            PanelEx panel = sender as PanelEx;

            int height = 12;

            // ���Ƶظ�1
            e.Graphics.DrawLine(this.InductorCoil1 ? redPen3 : greenPen3, 15, 1, 15, height);
            e.Graphics.DrawLine(this.InductorCoil1 ? redPen3 : greenPen3, 15, panel.Height - height, 15, panel.Height - 1);
            // ���Ƶظ�2
            e.Graphics.DrawLine(this.InductorCoil2 ? redPen3 : greenPen3, 25, 1, 25, height);
            e.Graphics.DrawLine(this.InductorCoil2 ? redPen3 : greenPen3, 25, panel.Height - height, 25, panel.Height - 1);
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

        /// <summary>
        /// ���²�����״̬
        /// </summary>
        private void RefreshEquStatus()
        {
            string systemStatus = commonDAO.GetSignalDataValue(this.SamplerMachineCode, eSignalDataName.�豸״̬.ToString());
            eEquInfSamplerSystemStatus result;
            if (Enum.TryParse(systemStatus, out result)) SamplerSystemStatus = result;
        }

        /// <summary>
        /// Ԥ������������Ϣͼ
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
