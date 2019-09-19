using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CMCS.Common;
using CMCS.Common.DAO;
using CMCS.Common.Entities;
using CMCS.Common.Entities.iEAA;
using CMCS.Common.Utilities;
using CMCS.WeighCheck.MakeChange.Utilities;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using DevComponents.DotNetBar.Metro.ColorTables;
using CMCS.Common.Enums;
using ZAZ.Finger;
using CMCS.Common.Entities.Fuel;
using System.Threading;
using System.IO;

namespace CMCS.WeighCheck.MakeChange.Frms.Sys
{
    public partial class FrmLogin : DevComponents.DotNetBar.Metro.MetroForm
    {
        public FrmLogin()
        {
            InitializeComponent();

            //StyleManager.MetroColorGeneratorParameters = MetroColorGeneratorParameters.BlackSky;
        }

        CommonDAO commonDao = CommonDAO.GetInstance();
        FingerMachine FingerDll = new FingerMachine();
        TaskSimpleScheduler taskSimpleScheduler = new TaskSimpleScheduler();
        User user;
        User user2;
        List<CmcsFinger> finger;

        /// <summary>
        /// ָ����֤�Ƿ�ͨ��
        /// </summary>
        bool GrossCheck = false;

        /// <summary>
        /// ָ�Ƽ����� ����ָ����֤
        /// </summary>
        int FingerCheckCount = 1;

        //ָ��ʶ�����
        System.Timers.Timer timer_Finger;

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            FormInit();
        }

        /// <summary>
        /// �����ʼ��
        /// </summary>
        private void FormInit()
        {
            // �����û�
            cmbUserAccount.DataSource = commonDao.GetAllSystemUser(eUserRoleCodes.����Ա.ToString());
            cmbUserAccount.DisplayMember = "UserName";
            cmbUserAccount.ValueMember = "UserAccount";
            //cmbUserAccount.ForeColor = Color.Green;

            timer_Finger = new System.Timers.Timer(1000)
            {
                AutoReset = true
            };
            timer_Finger.Elapsed += new System.Timers.ElapsedEventHandler(timer_Finger_Elapsed);
        }

        /// <summary>
        /// ��½
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            #region ��֤

            if (cmbUserAccount.SelectedItem == null)
            {
                ShowToolTip("��ѡ���û�");
                return;
            }
            if (string.IsNullOrEmpty(txtUserPassword.Text))
            {
                ShowToolTip("����������");
                return;
            }

            #endregion

            user = commonDao.Login(eUserRoleCodes.����Ա.ToString(), cmbUserAccount.SelectedValue.ToString(), MD5Util.Encrypt(txtUserPassword.Text));

            if (user != null)
            {
                if (user.UserAccount == GlobalVars.AdminAccount)
                {
                    LoginSuccess();
                    return;
                }
                if (FingerCheckCount == 1)//�û�1
                {
                    finger = commonDao.SelfDber.Entities<CmcsFinger>("where UserId=:UserId", new { Userid = user.PartyId });
                    if (finger == null || finger.Count == 0)
                    {
                        ShowToolTip("δע��ָ��");
                        return;
                    }
                }

                if (Fingerdll.ZAZOpenDeviceEx(ref hHandle, 2, 0, 0, 2, 0) == 0)
                {
                    GrossCheck = false;

                    timer_Finger.Enabled = true;
                }
                else
                {
                    ShowToolTip("ָ���豸δ����");
                    return;
                }
            }
            else
            {
                ShowToolTip("�ʺŻ�����������������룡");
                txtUserPassword.ResetText();
                txtUserPassword.Focus();
            }
        }

        /// <summary>
        /// ����¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void timer_Finger_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            timer_Finger.Stop();
            StartCheck();
            timer_Finger.Start();
        }

        /// <summary>
        /// ��ʼָ�Ƽ��
        /// </summary>
        public void StartCheck()
        {
            GrossCheck = false;
            if (GetFinger(1) == 1)
            {
                if (FingerCheckCount == 2)
                    finger = commonDao.SelfDber.Entities<CmcsFinger>("where UserId!=:UserId", new { UserId = SelfVars.LoginUser.PartyId });
                foreach (CmcsFinger item in finger)
                {
                    Fingerdll.ZAZDownCharFromFile(hHandle, nDevAddr, 2, item.FingerUrl);
                    int[] nScore = new int[1];
                    ret = Fingerdll.ZAZMatch(hHandle, nDevAddr, nScore);  //�ȶ�ģ��
                    if (nScore[0] > 50)
                    {
                        if (FingerCheckCount == 1)
                        {
                            ShowToolTip("ƥ��ɹ��ල�˽���ָ����֤");
                            SelfVars.LoginUser = user;
                            GrossCheck = true;
                            FingerCheckCount = 2;
                            break;
                        }
                        else if (FingerCheckCount == 2)
                        {
                            SelfVars.LoginUser2 = commonDao.SelfDber.Get<User>(item.UserId);
                            GrossCheck = true;
                            taskSimpleScheduler.Cancal();
                            InvokeEx(() =>
                            {
                                if (GrossCheck)
                                {
                                    ShowToolTip("ָ��ƥ��ɹ�...");
                                    LoginSuccess();
                                }
                            });
                            break;
                        }
                    }
                }
                if (!GrossCheck) ShowToolTip("ָ�Ʋ�ƥ��...");
            }
        }

        /// <summary>
        /// ��½�ɹ�
        /// </summary>
        private void LoginSuccess()
        {
            SelfVars.LoginUser = user;
            SelfVars.LoginUserNames = user.UserName;
            SelfVars.LoginUserAccounts = user.UserAccount;
            if (SelfVars.LoginUser2 != null)
            {
                SelfVars.LoginUserNames += "," + SelfVars.LoginUser2.UserName;
                SelfVars.LoginUserAccounts += "," + SelfVars.LoginUser2.UserAccount;
            }
            timer_Finger.Enabled = false;
            GrossCheck = false;
            FingerCheckCount = 1;
            this.Hide();
            SelfVars.MainFrameForm = new FrmMainFrame();
            SelfVars.MainFrameForm.Show();
        }

        /// <summary>
        /// ��Ϣ���
        /// </summary>
        /// <param name="message"></param>
        private void ShowToolTip(string message)
        {
            InvokeEx(() =>
            {
                this.lbeToolTip.Visible = true;
                this.lbeToolTip.ForeColor = Color.Red;
                this.lbeToolTip.Text = message;
            });
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

        #region ָ��ʶ��

        #region �豸����
        Int32 fingerIndex;
        /// <summary>
        /// ��ǰָ�Ʊ��
        /// </summary>
        public Int32 FingerIndex
        {
            get { return fingerIndex; }
            set
            {
                fingerIndex = value;
            }
        }

        bool isConnect;
        /// <summary>
        /// �豸����״̬
        /// </summary>
        public bool IsConnect
        {
            get { return isConnect; }
            set
            {
                isConnect = value;
            }
        }

        /// <summary>
        /// ��ǰ���Ӿ��
        /// </summary>
        IntPtr hHandle = new IntPtr();

        /// <summary>
        /// �豸��ַ
        /// </summary>
        private UInt32 nDevAddr = 0xffffffff;

        /// <summary>
        /// ͼ���С
        /// </summary>
        public const int ImageSize = (256 * 288);

        /// <summary>
        /// ִ�н��
        /// </summary>
        private int ret = 0;

        private Int32 timeout = 20;
        /// <summary>
        /// �ȴ���ʱ
        /// </summary>
        public Int32 TimeOut
        {
            get { return timeout; }
            set
            {
                timeout = value;
            }
        }

        private string strFile;
        /// <summary>
        /// dat�ļ�·��
        /// </summary>
        public string StrFile
        {
            get { return strFile; }
            set
            {
                strFile = value;
            }
        }
        #endregion

        /// <summary>
        /// ��ȡָ��
        /// </summary>
        /// <param name="buffer">ָ�����豸����ʱ���λ�� 1 2</param>
        /// <returns></returns>
        int GetFinger(int buffer)
        {
            TimeOut = 20;
            int ret = 0;
            byte[] ImgData = new byte[ImageSize];
            int[] ImgLen = new int[1];
            int iBuffer = 1;

        BEIG1:
            ret = Fingerdll.ZAZGetImage(hHandle, nDevAddr);  //��ȡͼ�� 
            if (ret == 0)
            {
                ShowToolTip("��ȡͼ��ɹ�...");
            }
            else if (ret == 2)
            {
                //��ʱ�ж�
                ShowToolTip("�ȴ���ָƽ���ڴ�������-" + TimeOut.ToString() + "��");
                if (TimeOut < 0)
                {
                    ShowToolTip("�ȴ���ʱ");
                    return 0;
                }
                TimeOut--;
                Thread.Sleep(1000);
                goto BEIG1;
            }
            else
            {
                //ShowToolTip(Fingerdll.ZAZErr2Strt(ret));
                return 0;
            }

            //////////////////////////////////////////////////////////////////////////
            //���漰ͼ���������ʡ��
            /****************�ϴ�ͼ��*********/

            ShowToolTip("�����ϴ�ͼ����ȴ�...");
            ret = Fingerdll.ZAZUpImage(hHandle, nDevAddr, ImgData, ImgLen);  //�ϴ�ͼ��
            if (ret != 0)
            {
                ShowToolTip(Fingerdll.ZAZErr2Strt(ret));
                return 0;
            }

            ret = Fingerdll.ZAZGenChar(hHandle, nDevAddr, iBuffer);  //����ģ��
            if (ret != 0)
            {
                ShowToolTip(Fingerdll.ZAZErr2Strt(ret));
                return 0;
            }
            else
            {
                ShowToolTip("����ָ������" + buffer.ToString());
                return 1;
            }
        BEIG2:
            if (ret == 0)
            {
                ret = Fingerdll.ZAZGetImage(hHandle, nDevAddr);  //��ȡͼ�� 
                ShowToolTip("�ȴ���ָ�ÿ�-");

                goto BEIG2;
            }
            else if (ret == 1)
            {
                ShowToolTip(Fingerdll.ZAZErr2Strt(ret));
                return 0;
            }
            return 1;
        }

        #endregion

        private void cmbUserAccount_SelectedIndexChanged(object sender, EventArgs e)
        {
            taskSimpleScheduler.Cancal();
        }

        public void Ex(string text, Exception ex)
        {
            Log4Neter.Error(text, ex);
        }
    }
}