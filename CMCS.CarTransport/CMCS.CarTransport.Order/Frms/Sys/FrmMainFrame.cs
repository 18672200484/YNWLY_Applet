﻿using System;
using System.Windows.Forms;
//
using DevComponents.DotNetBar;
using CMCS.Common.DAO;
using DevComponents.DotNetBar.Metro;
using CMCS.CarTransport.Order.Utilities;
using CMCS.CarTransport.Order.Core;
using CMCS.Common.Enums;
using CMCS.Common;

namespace CMCS.CarTransport.Order.Frms.Sys
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

            OpenWeight();
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            if (SelfVars.LoginUser == null) SelfVars.LoginUser = commonDAO.GetAdminUser();
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
        /// 打开过衡界面
        /// </summary>
        public void OpenWeight()
        {
            string uniqueKey = FrmOrder.UniqueKey;

            if (FrmMainFrame.superTabControlManager.GetTab(uniqueKey) == null)
            {
                FrmOrder frm = new FrmOrder();
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
