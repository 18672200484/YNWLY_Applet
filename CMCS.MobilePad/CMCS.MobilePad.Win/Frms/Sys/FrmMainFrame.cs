using System;
using System.Windows.Forms;
//
using DevComponents.DotNetBar;
using CMCS.Common.DAO;
using DevComponents.DotNetBar.Metro;
using CMCS.Common.Enums;
using CMCS.Common;
using CMCS.MobilePad.Win.Core;
using CMCS.MobilePad.Win.Frms.DataTarget;
using CMCS.MobilePad.Win.Utilities;
using CMCS.MobilePad.Win.Frms.UnLoadPlan;
using CMCS.MobilePad.Win.Frms.CarBreakRules;
using CMCS.MobilePad.Win.Frms.CarShippChange;
using CMCS.MobilePad.Win.Frms.Queue;

namespace CMCS.MobilePad.Win.Frms.Sys
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

            OpenHome();

            //MetroColorGeneratorParameters[] metroThemes = MetroColorGeneratorParameters.GetAllPredefinedThemes();
            //foreach (MetroColorGeneratorParameters item in metroThemes)
            //{
            //    ButtonItem btn = new ButtonItem(item.ThemeName, item.ThemeName);
            //    btn.Tag = item;
            //    btn.Click += new EventHandler(btnChangeTheme_Click);
            //    btnColorTheme.SubItems.Add(btn);

            //    btn.Checked = (item.ThemeName == ClientConfig.GetInstance().ColorTheme.ThemeName);
            //}
            //StyleManager.MetroColorGeneratorParameters = MetroColorGeneratorParameters.Bordeaux;
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


        #region 打开切换主界面
        /// <summary>
        /// 打开主界面
        /// </summary>
        public void OpenHome()
        {
            string uniqueKey = FrmHome.UniqueKey;

            if (FrmMainFrame.superTabControlManager.GetTab(uniqueKey) == null)
            {
                FrmHome frm = new FrmHome();
                FrmMainFrame.superTabControlManager.CreateTab(frm.Text, uniqueKey, frm, true, false);
            }
            else
                FrmMainFrame.superTabControlManager.ChangeToTab(uniqueKey);
        }

        /// <summary>
        /// 打开排队界面
        /// </summary>
        public void OpenQueue()
        {
            string uniqueKey = FrmMobileQueue.UniqueKey;

            if (FrmMainFrame.superTabControlManager.GetTab(uniqueKey) == null)
            {
                FrmMobileQueue frm = new FrmMobileQueue();
                FrmMainFrame.superTabControlManager.CreateTab(frm.Text, uniqueKey, frm, true, true);
            }
            else
                FrmMainFrame.superTabControlManager.ChangeToTab(uniqueKey);
        }

        /// <summary>
        /// 打开扣吨界面
        /// </summary>
        public void OpenCarDeduction()
        {
            string uniqueKey = FrmCarDeduction.UniqueKey;

            if (FrmMainFrame.superTabControlManager.GetTab(uniqueKey) == null)
            {
                FrmCarDeduction frm = new FrmCarDeduction();
                FrmMainFrame.superTabControlManager.CreateTab(frm.Text, uniqueKey, frm, true, true);
            }
            else
                FrmMainFrame.superTabControlManager.ChangeToTab(uniqueKey);
        }

        /// <summary>
        /// 打开调运管理界面
        /// </summary>
        public void OpenLMYB()
        {
            string uniqueKey = FrmTransportPlan.UniqueKey;

            if (FrmMainFrame.superTabControlManager.GetTab(uniqueKey) == null)
            {
                FrmTransportPlan frm = new FrmTransportPlan();
                FrmMainFrame.superTabControlManager.CreateTab(frm.Text, uniqueKey, frm, true, true);
            }
            else
                FrmMainFrame.superTabControlManager.ChangeToTab(uniqueKey);
        }

        /// <summary>
        /// 打开车辆管理界面
        /// </summary>
        public void OpenCarUpdate()
        {
            string uniqueKey = FrmCarManage.UniqueKey;

            if (FrmMainFrame.superTabControlManager.GetTab(uniqueKey) == null)
            {
                FrmCarManage frm = new FrmCarManage();
                FrmMainFrame.superTabControlManager.CreateTab(frm.Text, uniqueKey, frm, true, true);
            }
            else
                FrmMainFrame.superTabControlManager.ChangeToTab(uniqueKey);
        }

        /// <summary>
        /// 打开接卸方案界面
        /// </summary>
        public void OpenUnLoad()
        {
            string uniqueKey = FrmUnLoadPlan.UniqueKey;

            if (FrmMainFrame.superTabControlManager.GetTab(uniqueKey) == null)
            {
                FrmUnLoadPlan frm = new FrmUnLoadPlan();
                FrmMainFrame.superTabControlManager.CreateTab(frm.Text, uniqueKey, frm, true, true);
            }
            else
                FrmMainFrame.superTabControlManager.ChangeToTab(uniqueKey);
        }

        /// <summary>
        /// 打开车辆违章界面
        /// </summary>
        public void OpenCarBreakRules()
        {
            string uniqueKey = FrmCarBreakRules.UniqueKey;

            if (FrmMainFrame.superTabControlManager.GetTab(uniqueKey) == null)
            {
                FrmCarBreakRules frm = new FrmCarBreakRules();
                FrmMainFrame.superTabControlManager.CreateTab(frm.Text, uniqueKey, frm, true, true);
            }
            else
                FrmMainFrame.superTabControlManager.ChangeToTab(uniqueKey);
        }

        /// <summary>
        /// 打开车辆仓位调整界面
        /// </summary>
        public void OpenCarShippChange()
        {
            string uniqueKey = FrmCarShippChange.UniqueKey;

            if (FrmMainFrame.superTabControlManager.GetTab(uniqueKey) == null)
            {
                FrmCarShippChange frm = new FrmCarShippChange();
                FrmMainFrame.superTabControlManager.CreateTab(frm.Text, uniqueKey, frm, true, true);
            }
            else
                FrmMainFrame.superTabControlManager.ChangeToTab(uniqueKey);
        }

        /// <summary>
        /// 打开煤场测温杆设置
        /// </summary>
        public void OpenStorageTemperature()
        {
            string uniqueKey = FrmStorageTemperature.UniqueKey;

            if (FrmMainFrame.superTabControlManager.GetTab(uniqueKey) == null)
            {
                FrmStorageTemperature frm = new FrmStorageTemperature();
                FrmMainFrame.superTabControlManager.CreateTab(frm.Text, uniqueKey, frm, true, true);
            }
            else
                FrmMainFrame.superTabControlManager.ChangeToTab(uniqueKey);
        }
        #endregion

        #region Button事件

        /// <summary>
        /// 打开参数设置界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenSetting_Click(object sender, EventArgs e)
        {
            FrmSetting frm = new FrmSetting();
            frm.ShowDialog();
        }

        private void btnOpenDeduct_Click(object sender, EventArgs e)
        {
            OpenCarDeduction();
        }

        private void btnOpenHome_Click(object sender, EventArgs e)
        {
            OpenHome();
        }

        private void btnOpenLMYB_Click(object sender, EventArgs e)
        {
            OpenLMYB();
        }

        private void btnOpenCarUpdate_Click(object sender, EventArgs e)
        {
            OpenCarUpdate();
        }

        private void btnOpenUnLoad_Click(object sender, EventArgs e)
        {
            OpenUnLoad();
        }

        private void btnOpenCarBreak_Click(object sender, EventArgs e)
        {
            OpenCarBreakRules();
        }

        private void btnOpenCarShipping_Click(object sender, EventArgs e)
        {
            OpenCarShippChange();
        }

        private void btnOpenTemper_Click(object sender, EventArgs e)
        {
            OpenStorageTemperature();
        }

        /// <summary>
        /// 排队
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenQueue_Click(object sender, EventArgs e)
        {
            OpenQueue();
        }

        #endregion


    }
}
