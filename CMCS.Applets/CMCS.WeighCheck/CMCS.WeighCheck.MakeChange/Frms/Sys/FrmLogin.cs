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
        /// 指纹验证是否通过
        /// </summary>
        bool GrossCheck = false;

        /// <summary>
        /// 指纹检测次数 多人指纹验证
        /// </summary>
        int FingerCheckCount = 1;

        //指纹识别进程
        System.Timers.Timer timer_Finger;

        private void FrmLogin_Load(object sender, EventArgs e)
        {
            FormInit();
        }

        /// <summary>
        /// 窗体初始化
        /// </summary>
        private void FormInit()
        {
            // 加载用户
            cmbUserAccount.DataSource = commonDao.GetAllSystemUser(eUserRoleCodes.化验员.ToString());
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
        /// 登陆
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLogin_Click(object sender, EventArgs e)
        {
            #region 验证

            if (cmbUserAccount.SelectedItem == null)
            {
                ShowToolTip("请选择用户");
                return;
            }
            if (string.IsNullOrEmpty(txtUserPassword.Text))
            {
                ShowToolTip("请输入密码");
                return;
            }

            #endregion

            user = commonDao.Login(eUserRoleCodes.化验员.ToString(), cmbUserAccount.SelectedValue.ToString(), MD5Util.Encrypt(txtUserPassword.Text));

            if (user != null)
            {
                if (user.UserAccount == GlobalVars.AdminAccount)
                {
                    LoginSuccess();
                    return;
                }
                if (FingerCheckCount == 1)//用户1
                {
                    finger = commonDao.SelfDber.Entities<CmcsFinger>("where UserId=:UserId", new { Userid = user.PartyId });
                    if (finger == null || finger.Count == 0)
                    {
                        ShowToolTip("未注册指纹");
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
                    ShowToolTip("指纹设备未连接");
                    return;
                }
            }
            else
            {
                ShowToolTip("帐号或密码错误，请重新输入！");
                txtUserPassword.ResetText();
                txtUserPassword.Focus();
            }
        }

        /// <summary>
        /// 间隔事件
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
        /// 开始指纹检测
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
                    ret = Fingerdll.ZAZMatch(hHandle, nDevAddr, nScore);  //比对模板
                    if (nScore[0] > 50)
                    {
                        if (FingerCheckCount == 1)
                        {
                            ShowToolTip("匹配成功监督人进行指纹验证");
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
                                    ShowToolTip("指纹匹配成功...");
                                    LoginSuccess();
                                }
                            });
                            break;
                        }
                    }
                }
                if (!GrossCheck) ShowToolTip("指纹不匹配...");
            }
        }

        /// <summary>
        /// 登陆成功
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
        /// 信息输出
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
        /// Invoke封装
        /// </summary>
        /// <param name="action"></param>
        public void InvokeEx(Action action)
        {
            if (this.IsDisposed || !this.IsHandleCreated) return;

            this.Invoke(action);
        }

        #region 指纹识别

        #region 设备属性
        Int32 fingerIndex;
        /// <summary>
        /// 当前指纹编号
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
        /// 设备连接状态
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
        /// 当前连接句柄
        /// </summary>
        IntPtr hHandle = new IntPtr();

        /// <summary>
        /// 设备地址
        /// </summary>
        private UInt32 nDevAddr = 0xffffffff;

        /// <summary>
        /// 图像大小
        /// </summary>
        public const int ImageSize = (256 * 288);

        /// <summary>
        /// 执行结果
        /// </summary>
        private int ret = 0;

        private Int32 timeout = 20;
        /// <summary>
        /// 等待超时
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
        /// dat文件路径
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
        /// 获取指纹
        /// </summary>
        /// <param name="buffer">指纹在设备中临时存放位置 1 2</param>
        /// <returns></returns>
        int GetFinger(int buffer)
        {
            TimeOut = 20;
            int ret = 0;
            byte[] ImgData = new byte[ImageSize];
            int[] ImgLen = new int[1];
            int iBuffer = 1;

        BEIG1:
            ret = Fingerdll.ZAZGetImage(hHandle, nDevAddr);  //获取图象 
            if (ret == 0)
            {
                ShowToolTip("获取图像成功...");
            }
            else if (ret == 2)
            {
                //超时判断
                ShowToolTip("等待手指平放在传感器上-" + TimeOut.ToString() + "秒");
                if (TimeOut < 0)
                {
                    ShowToolTip("等待超时");
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
            //不涉及图像，下面可以省略
            /****************上传图像*********/

            ShowToolTip("正在上传图像请等待...");
            ret = Fingerdll.ZAZUpImage(hHandle, nDevAddr, ImgData, ImgLen);  //上传图象
            if (ret != 0)
            {
                ShowToolTip(Fingerdll.ZAZErr2Strt(ret));
                return 0;
            }

            ret = Fingerdll.ZAZGenChar(hHandle, nDevAddr, iBuffer);  //生成模板
            if (ret != 0)
            {
                ShowToolTip(Fingerdll.ZAZErr2Strt(ret));
                return 0;
            }
            else
            {
                ShowToolTip("生成指纹特征" + buffer.ToString());
                return 1;
            }
        BEIG2:
            if (ret == 0)
            {
                ret = Fingerdll.ZAZGetImage(hHandle, nDevAddr);  //获取图象 
                ShowToolTip("等待手指拿开-");

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