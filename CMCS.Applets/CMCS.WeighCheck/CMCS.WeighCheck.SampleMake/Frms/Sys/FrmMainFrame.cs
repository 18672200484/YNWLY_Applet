using System;
using System.Windows.Forms;
//
using DevComponents.DotNetBar;
using CMCS.Common.DAO;
using DevComponents.DotNetBar.Metro;
using CMCS.WeighCheck.SampleMake.Utilities;
using CMCS.Common;
using CMCS.Common.Enums;
using CMCS.WeighCheck.SampleMake.Frms.Sample;
using CMCS.WeighCheck.SampleMake.Frms.Make;
using CMCS.WeighCheck.SampleMake.Frms.JDYMake;

namespace CMCS.WeighCheck.SampleMake.Frms.Sys
{
    public partial class FrmMainFrame : MetroForm
    {
        CommonDAO commonDAO = CommonDAO.GetInstance();

        public static SuperTabControlManager superTabControlManager;

        public FrmMainFrame()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lblVersion.Text = new AU.Updater().Version;

            this.superTabControl1.Tabs.Clear();
            FrmMainFrame.superTabControlManager = new SuperTabControlManager(this.superTabControl1);
            if (CommonDAO.GetInstance().CheckUserRole(eUserRoleCodes.采样员.ToString(), SelfVars.LoginUser.UserAccount))
            {
                OpenSampleWeight();
                OpenSampleCheck();
                OpenSampleWeight();
            }
            else if (CommonDAO.GetInstance().CheckUserRole(eUserRoleCodes.制样员.ToString(), SelfVars.LoginUser.UserAccount))
            {
                OpenMakeTake();
                OpenWeighCheck();
                OpenMakeTake();
                OpenJDYMake();
            }
            if (CommonDAO.GetInstance().CheckUserRole(eUserRoleCodes.采样员.ToString(), SelfVars.LoginUser.UserAccount) && CommonDAO.GetInstance().CheckUserRole(eUserRoleCodes.采样员.ToString(), SelfVars.LoginUser.UserAccount))
            {
                OpenSampleWeight();
                OpenSampleCheck();
                OpenMakeTake();
                OpenWeighCheck();
                OpenJDYMake();
                OpenSampleWeight();
            }
            this.WindowState = FormWindowState.Maximized;
        }

		private void Form1_Shown(object sender, EventArgs e)
        {
            if (SelfVars.LoginUser != null) lblLoginUserName.Text = SelfVars.LoginUser.UserName;

            commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.程序状态.ToString(), "1");
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                if (MessageBoxEx.Show("确认退出系统？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.程序状态.ToString(), "0");

                    Application.Exit();
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        /// <summary>
        /// 退出系统
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnApplicationExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timer_CurrentTime_Tick(object sender, EventArgs e)
        {
            lblCurrentTime.Text = DateTime.Now.ToString("yyyy年MM月dd日 HH:mm:ss");
        }

        #region 打开/切换可视主界面

        #region 弹出窗体

        /// <summary>
        /// 打开采样登记界面
        /// </summary>
        public void OpenSampleWeight()
        {
            string uniqueKey = FrmSampleWeigth.UniqueKey;

            if (FrmMainFrame.superTabControlManager.GetTab(uniqueKey) == null)
            {
                FrmSampleWeigth frm = new FrmSampleWeigth();
                FrmMainFrame.superTabControlManager.CreateTab(frm.Text, uniqueKey, frm, true, false);
            }
            else
                FrmMainFrame.superTabControlManager.ChangeToTab(uniqueKey);
        }

        /// <summary>
        /// 打开采样验证界面
        /// </summary>
        public void OpenSampleCheck()
        {
            string uniqueKey = FrmSampleCheck.UniqueKey;

            if (FrmMainFrame.superTabControlManager.GetTab(uniqueKey) == null)
            {
                FrmSampleCheck frm = new FrmSampleCheck();
                FrmMainFrame.superTabControlManager.CreateTab(frm.Text, uniqueKey, frm, true, false);
            }
            else
                FrmMainFrame.superTabControlManager.ChangeToTab(uniqueKey);
        }

        /// <summary>
        /// 打开制样接样界面
        /// </summary>
        public void OpenMakeTake()
        {
            string uniqueKey = FrmMakeTake.UniqueKey;

            if (FrmMainFrame.superTabControlManager.GetTab(uniqueKey) == null)
            {
                FrmMakeTake frm = new FrmMakeTake();
                FrmMainFrame.superTabControlManager.CreateTab(frm.Text, uniqueKey, frm, true, false);
            }
            else
                FrmMainFrame.superTabControlManager.ChangeToTab(uniqueKey);
        }

        /// <summary>
        /// 打开制样明细界面
        /// </summary>
        public void OpenWeighCheck()
        {
            string uniqueKey = FrmMakeWeight.UniqueKey;

            if (FrmMainFrame.superTabControlManager.GetTab(uniqueKey) == null)
            {
                FrmMakeWeight frm = new FrmMakeWeight();
                FrmMainFrame.superTabControlManager.CreateTab(frm.Text, uniqueKey, frm, true, false);
            }
            else
                FrmMainFrame.superTabControlManager.ChangeToTab(uniqueKey);
        }

        /// <summary>
        /// 打开监督样界面
        /// </summary>
        public void OpenJDYMake()
        {
            string uniqueKey = FrmJDYMake_List.UniqueKey;

            if (FrmMainFrame.superTabControlManager.GetTab(uniqueKey) == null)
            {
                FrmJDYMake_List frm = new FrmJDYMake_List();
                FrmMainFrame.superTabControlManager.CreateTab(frm.Text, uniqueKey, frm, true, false);
            }
            else
                FrmMainFrame.superTabControlManager.ChangeToTab(uniqueKey);
        }

        /// <summary>
        /// 打开参数设置界面
        /// </summary>
        public void OpenSetting()
        {
            FrmSetting frm = new FrmSetting();
            frm.ShowDialog();
        }

        #endregion

        /// <summary>
        /// 打开参数设置界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenSetting_Click(object sender, EventArgs e)
        {
            OpenSetting();
        }

        #endregion
    }
}
