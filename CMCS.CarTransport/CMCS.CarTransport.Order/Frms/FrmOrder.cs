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
        /// ����Ψһ��ʶ��
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
        /// ��������
        /// </summary>
        //VoiceSpeaker voiceSpeaker = new VoiceSpeaker();

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
        /// ʶ���ѡ��ĳ���ƾ֤
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
                    panCurrentCarNumber.Text = "�ȴ�����|" + panCurrentCarNumber.Text.Split('|')[1];
            }
        }

        ImperfectCar currentImperfectCar2;
        /// <summary>
        /// ʶ���ѡ��ĳ���ƾ֤
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
                    panCurrentCarNumber.Text = panCurrentCarNumber.Text.Split('|')[0] + "|�ȴ�����";
            }
        }

        eFlowFlag currentFlowFlag1 = eFlowFlag.�ȴ�����;
        eFlowFlag currentFlowFlag2 = eFlowFlag.�ȴ�����;
        /// <summary>
        /// ��ǰҵ�����̱�ʶ
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
        /// ��ǰҵ�����̱�ʶ
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
        /// ��ǰ��
        /// </summary>
        public CmcsAutotruck CurrentAutotruck1
        {
            get { return currentAutotruck1; }
            set
            {
                currentAutotruck1 = value;

                if (value != null)
                {
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.��ǰ��Id.ToString() + 1, value.Id);
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.��ǰ����.ToString() + 1, value.CarNumber);

                    txtCarNumber_SaleFuel1.Text = value.CarNumber;
                    this.txtCarNumber_SaleFuel1.Text = value.CarNumber;
                    superTabControl2.SelectedTab = superTabItem_SaleFuel1;

                    CurrentImperfectCar1.Voucher = value.CarNumber;


                }
                else
                {
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.��ǰ��Id.ToString() + 1, string.Empty);
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.��ǰ����.ToString() + 1, string.Empty);

                    txtCarNumber_SaleFuel1.ResetText();

                    CurrentImperfectCar1.Voucher = null;
                }
            }
        }


        CmcsAutotruck currentAutotruck2;
        /// <summary>
        /// ��ǰ��
        /// </summary>
        public CmcsAutotruck CurrentAutotruck2
        {
            get { return currentAutotruck2; }
            set
            {
                currentAutotruck2 = value;

                if (value != null)
                {
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.��ǰ��Id.ToString() + 2, value.Id);
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.��ǰ����.ToString() + 2, value.CarNumber);

                    txtCarNumber_SaleFuel2.Text = value.CarNumber;
                    this.txtCarNumber_SaleFuel2.Text = value.CarNumber;
                    superTabControl2.SelectedTab = superTabItem_SaleFuel2;

                    CurrentImperfectCar2.Voucher = value.CarNumber;
                }
                else
                {
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.��ǰ��Id.ToString() + 2, string.Empty);
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.��ǰ����.ToString() + 2, string.Empty);

                    txtCarNumber_SaleFuel2.ResetText();

                    CurrentImperfectCar1.Voucher = null;
                }
            }
        }

        CmcsUnFinishTransport currentUnFinishTransport1;
        /// <summary>
        /// ��ǰδ��������¼1
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
        /// ��ǰδ��������¼2
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
        /// 1����
        /// </summary>
        void FrontGateUp1()
        {
            if (this.CurrentImperfectCar1 == null) return;

            this.iocControler.Gate1Up();
        }

        /// <summary>
        /// 1����
        /// </summary>
        void FrontGateDown1()
        {
            if (this.CurrentImperfectCar1 == null) return;

            this.iocControler.Gate1Down();
        }

        /// <summary>
        /// 2����
        /// </summary>
        void FrontGateDown2()
        {
            if (this.CurrentImperfectCar2 == null) return;

            this.iocControler.Gate2Down();
        }


        /// <summary>
        /// 2����
        /// </summary>
        void FrontGateUp2()
        {
            if (this.CurrentImperfectCar2 == null) return;

            this.iocControler.Gate2Up();
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
                if (carnumber != "�޳���" && this.CurrentFlowFlag1 == eFlowFlag.�ȴ�����)
                {
                    passCarQueuer1.Enqueue(ePassWay.Way1, carnumber, true);
                    this.CurrentFlowFlag1 = eFlowFlag.ʶ����;
                    timer1_Tick(null, null);
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
                 if (carnumber != "�޳���" && this.CurrentFlowFlag2 == eFlowFlag.�ȴ�����)
                 {
                     passCarQueuer2.Enqueue(ePassWay.Way1, carnumber, true);
                     this.CurrentFlowFlag2 = eFlowFlag.ʶ����;
                     timer1_Tick(null, null);
                     Log4Neter.Info(string.Format("����ʶ��2ʶ�𵽳��ţ�{0}", carnumber));
                 }
             });
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

                // IO������
                Hardwarer.Iocer.OnReceived += new IOC.JMDM20DIOV2.JMDM20DIOV2Iocer.ReceivedEventHandler(Iocer_Received);
                Hardwarer.Iocer.OnStatusChange += new IOC.JMDM20DIOV2.JMDM20DIOV2Iocer.StatusChangeHandler(Iocer_StatusChange);
                success = Hardwarer.Iocer.OpenCom(commonDAO.GetAppletConfigInt32("IO������_����"), commonDAO.GetAppletConfigInt32("IO������_������"), commonDAO.GetAppletConfigInt32("IO������_����λ"), (StopBits)commonDAO.GetAppletConfigInt32("IO������_ֹͣλ"), (Parity)commonDAO.GetAppletConfigInt32("IO������_У��λ"));
                if (!success) MessageBoxEx.Show("IO����������ʧ�ܣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.iocControler = new IocControler(Hardwarer.Iocer);

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
        }


        private void FrmOrder_FormClosing(object sender, FormClosingEventArgs e)
        {

            // ж���豸
            UnloadHardware();
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

                try
                {
                    switch (this.CurrentFlowFlag1)
                    {
                        case eFlowFlag.�ȴ�����:
                            #region

                            #endregion
                            break;

                        case eFlowFlag.ʶ����:
                            #region

                            // �������޳�ʱ���ȴ�����
                            if (passCarQueuer1.Count == 0)
                            {
                                this.CurrentFlowFlag1 = eFlowFlag.�ȴ�����;
                                break;
                            }

                            this.CurrentImperfectCar1 = passCarQueuer1.Dequeue();

                            // ��ʽһ������ʶ��ĳ��ƺŲ��ҳ�����Ϣ
                            this.CurrentAutotruck1 = carTransportDAO.GetAutotruckByCarNumber(this.CurrentImperfectCar1.Voucher);

                            if (this.CurrentAutotruck1 != null)
                            {
                                if (this.CurrentAutotruck1.IsUse == 1)
                                {
                                    // ���Ҹó�δ��ɵ������¼
                                    CurrentUnFinishTransport1 = carTransportDAO.GetUnFinishTransportByAutotruckId(this.CurrentAutotruck1.Id);
                                    if (CurrentUnFinishTransport1 != null)
                                    {
                                        this.timer_SaleFuel1_Cancel = false;
                                        this.CurrentFlowFlag1 = eFlowFlag.��֤��Ϣ;
                                        timer_SaleFuel1_Tick(null, null);
                                    }
                                    else
                                    {
                                        Log4Neter.Info(string.Format("���ƺţ�{0} δ�Ŷ�", this.CurrentAutotruck1.CarNumber));
                                    }
                                }
                                else
                                {
                                    //this.voiceSpeaker.Speak("���ƺ� " + this.CurrentAutotruck1.CarNumber + " ��ͣ�ã���ֹͨ��", 2, false);

                                    timer1.Interval = 20000;
                                }
                            }
                            else
                            {

                                // ��ʽһ������ʶ��
                                //this.voiceSpeaker.Speak("���ƺ� " + this.CurrentImperfectCar1.Voucher + " δ�Ǽǣ���ֹͨ��", 2, false);

                                timer1.Interval = 20000;
                            }

                            #endregion
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Log4Neter.Error("timer1_Tick��CurrentFlowFlag1", ex);
                }

                try
                {
                    switch (this.CurrentFlowFlag2)
                    {
                        case eFlowFlag.�ȴ�����:
                            #region

                            #endregion
                            break;

                        case eFlowFlag.ʶ����:
                            #region

                            // �������޳�ʱ���ȴ�����
                            if (passCarQueuer2.Count == 0)
                            {
                                this.CurrentFlowFlag2 = eFlowFlag.�ȴ�����;
                                break;
                            }

                            this.CurrentImperfectCar2 = passCarQueuer2.Dequeue();

                            // ��ʽһ������ʶ��ĳ��ƺŲ��ҳ�����Ϣ
                            this.CurrentAutotruck2 = carTransportDAO.GetAutotruckByCarNumber(this.CurrentImperfectCar2.Voucher);

                            if (this.CurrentAutotruck2 != null)
                            {
                                if (this.CurrentAutotruck2.IsUse == 1)
                                {
                                    // ���Ҹó�δ��ɵ������¼
                                    CurrentUnFinishTransport2 = carTransportDAO.GetUnFinishTransportByAutotruckId(this.CurrentAutotruck2.Id);
                                    if (CurrentUnFinishTransport2 != null)
                                    {
                                        this.timer_SaleFuel2_Cancel = false;
                                        this.CurrentFlowFlag2 = eFlowFlag.��֤��Ϣ;
                                        timer_SaleFuel2_Tick(null, null);
                                    }
                                }
                                else
                                {
                                    //this.voiceSpeaker.Speak("���ƺ� " + this.CurrentAutotruck2.CarNumber + " ��ͣ�ã���ֹͨ��", 2, false);

                                    timer2.Interval = 20000;
                                }
                            }
                            else
                            {

                                // ��ʽһ������ʶ��
                                //this.voiceSpeaker.Speak("���ƺ� " + this.CurrentImperfectCar2.Voucher + " δ�Ǽǣ���ֹͨ��", 2, false);
                                //// ��ʽ����ˢ����ʽ
                                //this.voiceSpeaker.Speak("����δ�Ǽǣ���ֹͨ��", 2, false);

                                timer2.Interval = 20000;
                            }

                            #endregion
                            break;
                    }
                }
                catch (Exception ex)
                {
                    Log4Neter.Error("timer1_Tick��CurrentFlowFlag2", ex);
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

                // ����ú 
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
        /// 1���г����ڵ�ǰ��·��
        /// </summary>
        /// <returns></returns>
        bool HasCarOnCurrentWay1()
        {
            if (this.CurrentImperfectCar1 == null) return false;

            return this.InductorCoil1 || this.InductorCoil2;
        }

        /// <summary>
        /// 2���г����ڵ�ǰ��·��
        /// </summary>
        /// <returns></returns>
        bool HasCarOnCurrentWay2()
        {
            if (this.CurrentImperfectCar2 == null) return false;

            return this.InductorCoil3 || this.InductorCoil4;
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

        #region ����úҵ��

        bool timer_SaleFuel1_Cancel = true;
        bool timer_SaleFuel2_Cancel = true;

        CmcsSaleFuelTransport currentSaleFuelTransport1;
        /// <summary>
        /// ��ǰ�����¼1
        /// </summary>
        public CmcsSaleFuelTransport CurrentSaleFuelTransport1
        {
            get { return currentSaleFuelTransport1; }
            set
            {
                currentSaleFuelTransport1 = value;

                if (value != null)
                {
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.��ǰ�����¼Id.ToString(), value.Id);

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
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.��ǰ�����¼Id.ToString(), string.Empty);
                    txtCarNumber_SaleFuel1.ResetText();
                    txt_YBNumber1.ResetText();
                    txt_TransportNo1.ResetText();
                    txt_Consignee1.ResetText();
                    txt_TransportCompayName1.ResetText();
                    dbi_OutWeight1.ResetText();
                    btnSaveTransport_SaleFuel1.Text = "��ʼȡú";
                    btnSaveTransport_SaleFuel1.Enabled = false;
                    txt_ReMark1.ResetText();
                }
            }
        }

        CmcsSaleFuelTransport currentSaleFuelTransport2;
        /// <summary>
        /// ��ǰ�����¼2
        /// </summary>
        public CmcsSaleFuelTransport CurrentSaleFuelTransport2
        {
            get { return currentSaleFuelTransport2; }
            set
            {
                currentSaleFuelTransport2 = value;

                if (value != null)
                {
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.��ǰ�����¼Id.ToString(), value.Id);

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
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.��ǰ�����¼Id.ToString(), string.Empty);
                    txtCarNumber_SaleFuel2.ResetText();
                    txt_YBNumber2.ResetText();
                    txt_TransportNo2.ResetText();
                    txt_Consignee2.ResetText();
                    txt_TransportCompayName2.ResetText();
                    dbi_OutWeight2.ResetText();
                    btnSaveTransport_SaleFuel2.Text = "��ʼȡú";
                    btnSaveTransport_SaleFuel2.Enabled = false;
                    txt_ReMark2.ResetText();
                }
            }
        }

        /// <summary>
        /// ��·1��ʱ��
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
                    case eFlowFlag.��֤��Ϣ:
                        #region
                        if (this.CurrentUnFinishTransport1 != null)
                        {
                            this.CurrentSaleFuelTransport1 = commonDAO.SelfDber.Get<CmcsSaleFuelTransport>(CurrentUnFinishTransport1.TransportId);
                            if (this.CurrentSaleFuelTransport1 != null)
                            {
                                //// �ж�·������
                                //string nextPlace;
                                //if (carTransportDAO.CheckNextTruckInFactoryWay(this.CurrentAutotruck1.CarType, this.CurrentSaleFuelTransport1.StepName, "װ��", CommonAppConfig.GetInstance().AppIdentifier, out nextPlace))
                                //{
                                //if (CommonAppConfig.GetInstance().AppIdentifier.Contains(this.CurrentSaleFuelTransport1.LoadArea))
                                //{
                                FrontGateUp1();
                                this.CurrentFlowFlag1 = eFlowFlag.�ȴ�����;
                                //}
                                //else
                                //{
                                //    //this.voiceSpeaker.Speak("·�ߴ��� ��ֹͨ�� " + (!string.IsNullOrEmpty(this.CurrentSaleFuelTransport1.LoadArea) ? "��ǰ��" + this.CurrentSaleFuelTransport1.LoadArea : ""), 2, false);

                                //    timer_SaleFuel1.Interval = 20000;
                                //}
                                //}
                                //else
                                //{
                                //    //this.voiceSpeaker.Speak("·�ߴ��� ��ֹͨ�� " + (!string.IsNullOrEmpty(nextPlace) ? "��ǰ��" + nextPlace : ""), 2, false);

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
                            //this.voiceSpeaker.Speak("δ�Ŷ� ��ֹͨ�� ", 2, false);
                        }
                        #endregion
                        break;
                    case eFlowFlag.�ȴ�����:
                        #region
                        this.CurrentFlowFlag1 = eFlowFlag.����ȡú;
                        // ����������
                        timer_SaleFuel1.Interval = 4000;

                        #endregion
                        break;
                    case eFlowFlag.����ȡú:
                        btnSaveTransport_SaleFuel1.Enabled = true;
                        break;
                    case eFlowFlag.ȡú���:
                        break;
                    case eFlowFlag.������Ϣ:
                        // ���������
                        timer_SaleFuel1.Interval = 1000;

                        btnSaveTransport_SaleFuel1.Enabled = true;

                        if (this.AutoHandMode)
                        {
                            //�Զ�ģʽ
                            if (!SaveSaleFuelTransport1())
                            {
                                //this.voiceSpeaker.Speak("���ƺ� " + this.CurrentAutotruck1.CarNumber + " ����ʧ�ܣ�����ϵ����Ա", 2, false);
                            }
                            else
                            {
                                this.CurrentFlowFlag1 = eFlowFlag.�ȴ��뿪;
                            }
                        }
                        else
                        {
                            // �ֶ�ģʽ 
                        }
                        break;
                    case eFlowFlag.�ȴ��뿪:
                        #region
                        // ���еظ����ź�ʱ����
                        if (!HasCarOnLeaveWay1())
                        {
                            ResetSelFuel1();
                        }
                        // ����������
                        timer_SaleFuel1.Interval = 4000;

                        #endregion
                        break;
                }

                // ��ǰ�ذ�����С����С���������еظС��������ź�ʱ����
                //if (!HasCarOnEnterWay1() && !HasCarOnLeaveWay1() && this.CurrentFlowFlag1 != eFlowFlag.�ȴ�����
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
        /// ��·2��ʱ��
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
                    case eFlowFlag.��֤��Ϣ:
                        #region
                        if (this.CurrentUnFinishTransport2 != null)
                        {
                            this.CurrentSaleFuelTransport2 = commonDAO.SelfDber.Get<CmcsSaleFuelTransport>(CurrentUnFinishTransport2.TransportId);
                            if (this.CurrentSaleFuelTransport2 != null)
                            {
                                // �ж�·������
                                string nextPlace;
                                if (carTransportDAO.CheckNextTruckInFactoryWay(this.CurrentAutotruck2.CarType, this.CurrentSaleFuelTransport2.StepName, "װ��", CommonAppConfig.GetInstance().AppIdentifier, out nextPlace))
                                {
                                    if (CommonAppConfig.GetInstance().AppIdentifier.Contains(this.CurrentSaleFuelTransport2.LoadArea))
                                    {
                                        FrontGateUp2();
                                        this.CurrentFlowFlag2 = eFlowFlag.�ȴ�����;
                                    }
                                    else
                                    {
                                        //this.voiceSpeaker.Speak("·�ߴ��� ��ֹͨ�� " + (!string.IsNullOrEmpty(this.CurrentSaleFuelTransport2.LoadArea) ? "��ǰ��" + this.CurrentSaleFuelTransport2.LoadArea : ""), 2, false);

                                        timer_SaleFuel2.Interval = 20000;
                                    }
                                }
                                else
                                {
                                    //this.voiceSpeaker.Speak("·�ߴ��� ��ֹͨ�� " + (!string.IsNullOrEmpty(nextPlace) ? "��ǰ��" + nextPlace : ""), 2, false);

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
                            //this.voiceSpeaker.Speak("���ƺ� " + this.CurrentAutotruck2.CarNumber + " δ�ҵ��ŶӼ�¼", 2, false);

                            timer_SaleFuel2.Interval = 20000;
                        }

                        #endregion
                        break;
                    case eFlowFlag.�ȴ�����:
                        #region
                        this.CurrentFlowFlag2 = eFlowFlag.����ȡú;
                        // ����������
                        timer_SaleFuel2.Interval = 4000;

                        #endregion
                        break;
                    case eFlowFlag.����ȡú:
                        btnSaveTransport_SaleFuel2.Enabled = true;
                        break;
                    case eFlowFlag.ȡú���:
                        break;
                    case eFlowFlag.������Ϣ:
                        // ���������
                        timer_SaleFuel2.Interval = 2000;

                        btnSaveTransport_SaleFuel2.Enabled = true;

                        if (this.AutoHandMode)
                        {
                            // �Զ�ģʽ
                            if (!SaveSaleFuelTransport2())
                            {
                                //this.voiceSpeaker.Speak("���ƺ� " + this.CurrentAutotruck2.CarNumber + " ����ʧ�ܣ�����ϵ����Ա", 2, false);
                            }
                            else
                            {
                                this.CurrentFlowFlag2 = eFlowFlag.�ȴ��뿪;
                            }
                        }
                        else
                        {
                            // �ֶ�ģʽ 
                        }
                        break;
                    case eFlowFlag.�ȴ��뿪:
                        #region

                        // ���еظ����ź�ʱ����
                        if (!HasCarOnLeaveWay2())
                        {
                            ResetSelFuel2();
                        }
                        // ����������
                        timer_SaleFuel2.Interval = 4000;

                        #endregion
                        break;
                }

                // ��ǰ�ذ�����С����С���������еظС��������ź�ʱ����
                if (!HasCarOnEnterWay2() && !HasCarOnLeaveWay2() && this.CurrentFlowFlag2 != eFlowFlag.�ȴ�����
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
            e.Graphics.DrawLine(this.InductorCoil1 ? redPen3 : greenPen3, 15, 5, 15, panel.Height - 5);
            // ���Ƶظ�2                                                               
            e.Graphics.DrawLine(this.InductorCoil2 ? redPen3 : greenPen3, 25, 5, 25, panel.Height - 5);
            // ���Ƶظ�3
            e.Graphics.DrawLine(this.InductorCoil3 ? redPen3 : greenPen3, panel.Width - 25, 5, panel.Width - 25, panel.Height - 5);
            // ���Ƶظ�4
            e.Graphics.DrawLine(this.InductorCoil4 ? redPen3 : greenPen3, panel.Width - 15, 5, panel.Width - 15, panel.Height - 5);

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

        private void btnSaveTransport_SaleFuel1_Click(object sender, EventArgs e)
        {
            if (btnSaveTransport_SaleFuel1.Text == "��ʼȡú")
            {
                this.CurrentFlowFlag1 = eFlowFlag.ȡú���;
                btnSaveTransport_SaleFuel1.Text = "������ú";
            }
            else if (btnSaveTransport_SaleFuel1.Text == "������ú")
            {
                this.CurrentFlowFlag1 = eFlowFlag.������Ϣ;
                btnSaveTransport_SaleFuel1.Enabled = false;
            }
        }
        /// <summary>
        /// ������Ϣ1
        /// </summary>
        void ResetSelFuel1()
        {
            this.timer_SaleFuel1_Cancel = true;

            this.CurrentFlowFlag1 = eFlowFlag.�ȴ�����;

            this.CurrentAutotruck1 = null;
            this.CurrentSaleFuelTransport1 = null;
            this.CurrentUnFinishTransport1 = null;

            btnSaveTransport_SaleFuel1.Enabled = false;

            FrontGateDown1();

            // �������
            this.CurrentImperfectCar1 = null;
        }

        /// <summary>
        /// ������Ϣ2
        /// </summary>
        void ResetSelFuel2()
        {
            this.timer_SaleFuel2_Cancel = true;

            this.CurrentFlowFlag2 = eFlowFlag.�ȴ�����;

            this.CurrentAutotruck2 = null;
            this.CurrentSaleFuelTransport2 = null;

            btnSaveTransport_SaleFuel2.Enabled = false;

            FrontGateDown2();

            // �������
            this.CurrentImperfectCar2 = null;
        }

        private void btnSaveTransport_SaleFuel2_Click(object sender, EventArgs e)
        {

            if (btnSaveTransport_SaleFuel2.Text == "��ʼȡú")
            {
                this.CurrentFlowFlag2 = eFlowFlag.ȡú���;
                btnSaveTransport_SaleFuel2.Text = "������ú";
            }
            else if (btnSaveTransport_SaleFuel2.Text == "������ú")
            {
                this.CurrentFlowFlag2 = eFlowFlag.������Ϣ;
                btnSaveTransport_SaleFuel2.Enabled = false;
            }
        }

        /// <summary>
        /// ���������¼
        /// </summary>
        /// <returns></returns>
        bool SaveSaleFuelTransport1()
        {
            //FrmNum fn = new FrmNum();
            //if (fn.ShowDialog() != DialogResult.OK)
            //{
            //    return false;
            //}
            ////ȡú��
            //decimal weight = fn.OutWeight;
            if (this.CurrentSaleFuelTransport1 == null) return false;

            try
            {
                if (orderDAO.SaveSaleFuelTransport(this.CurrentSaleFuelTransport1.Id, DateTime.Now))
                {
                    this.CurrentSaleFuelTransport1 = commonDAO.SelfDber.Get<CmcsSaleFuelTransport>(this.CurrentSaleFuelTransport1.Id);


                    btnSaveTransport_SaleFuel1.Enabled = false;
                    this.CurrentFlowFlag1 = eFlowFlag.�ȴ��뿪;
                    //this.voiceSpeaker.Speak("װ��������뿪��", 2, false);

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
        /// ���������¼
        /// </summary>
        /// <returns></returns>
        bool SaveSaleFuelTransport2()
        {
            //FrmNum fn = new FrmNum();
            //if (fn.ShowDialog() != DialogResult.OK)
            //{
            //    return false;
            //}
            ////ȡú��
            //decimal weight = fn.OutWeight;
            if (this.CurrentSaleFuelTransport2 == null) return false;

            try
            {
                if (orderDAO.SaveSaleFuelTransport(this.CurrentSaleFuelTransport2.Id, DateTime.Now))
                {
                    this.CurrentSaleFuelTransport2 = commonDAO.SelfDber.Get<CmcsSaleFuelTransport>(this.CurrentSaleFuelTransport2.Id);


                    btnSaveTransport_SaleFuel2.Enabled = false;
                    this.CurrentFlowFlag2 = eFlowFlag.�ȴ��뿪;

                    //this.voiceSpeaker.Speak("װ��������뿪��", 2, false);

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
        /// �г���1���°��ĵ�·��
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
        /// �г���2���°��ĵ�·��
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
        /// �г���1���ϰ��ĵ�·��
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
        /// �г���2���ϰ��ĵ�·��
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

                this.CurrentFlowFlag1 = eFlowFlag.ʶ����;
            }
        }

        private void btnSelectAutotruck_SaleFuel2_Click(object sender, EventArgs e)
        {
            FrmUnFinishTransport_Select frm = new FrmUnFinishTransport_Select(" order by CreateDate desc");
            if (frm.ShowDialog() == DialogResult.OK)
            {
                passCarQueuer2.Enqueue(ePassWay.Way2, frm.Output.CarNumber, false);

                this.CurrentFlowFlag2 = eFlowFlag.ʶ����;
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
