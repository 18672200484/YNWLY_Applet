using System;
using System.Windows.Forms;
//
using DevComponents.DotNetBar;
using CMCS.Common.DAO;
using DevComponents.DotNetBar.Metro;
using CMCS.CarTransport.WeighterHand.Utilities;
using CMCS.CarTransport.WeighterHand.Core;
using CMCS.Common.Enums;
using CMCS.Common;

namespace CMCS.CarTransport.WeighterHand.Frms.Sys
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
            string uniqueKey = FrmWeighter.UniqueKey;

            if (FrmMainFrame.superTabControlManager.GetTab(uniqueKey) == null)
            {
                FrmWeighter frm = new FrmWeighter();
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
        #region
        /// <summary>
        /// 煤种管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenFuelKindlLoad_Click(object sender, EventArgs e)
        {
            string uniqueKey = CMCS.CarTransport.WeighterHand.Frms.BaseInfo.FuelKind.FrmFuelKind_List.UniqueKey;

            if (FrmMainFrame.superTabControlManager.GetTab(uniqueKey) == null)
            {
                CMCS.CarTransport.WeighterHand.Frms.BaseInfo.FuelKind.FrmFuelKind_List frm = new CMCS.CarTransport.WeighterHand.Frms.BaseInfo.FuelKind.FrmFuelKind_List();
                FrmMainFrame.superTabControlManager.CreateTab(frm.Text, uniqueKey, frm, true, true);
            }
            else
                FrmMainFrame.superTabControlManager.ChangeToTab(uniqueKey);
        }

        /// <summary>
        /// 矿点管理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenMineLoad_Click(object sender, EventArgs e)
        {
            string uniqueKey = CMCS.CarTransport.WeighterHand.Frms.BaseInfo.Mine.FrmMine_List.UniqueKey;

            if (FrmMainFrame.superTabControlManager.GetTab(uniqueKey) == null)
            {
                CMCS.CarTransport.WeighterHand.Frms.BaseInfo.Mine.FrmMine_List frm = new CMCS.CarTransport.WeighterHand.Frms.BaseInfo.Mine.FrmMine_List();
                FrmMainFrame.superTabControlManager.CreateTab(frm.Text, uniqueKey, frm, true, true);
            }
            else
                FrmMainFrame.superTabControlManager.ChangeToTab(uniqueKey);
        }

        /// <summary>
        /// 运输记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenBuyFuelTransportLoad_Click(object sender, EventArgs e)
        {
            string uniqueKey = CMCS.CarTransport.WeighterHand.Frms.Transport.BuyFuelTransport.FrmBuyFuelTransport_List.UniqueKey;

            if (FrmMainFrame.superTabControlManager.GetTab(uniqueKey) == null)
            {
                CMCS.CarTransport.WeighterHand.Frms.Transport.BuyFuelTransport.FrmBuyFuelTransport_List frm = new CMCS.CarTransport.WeighterHand.Frms.Transport.BuyFuelTransport.FrmBuyFuelTransport_List();
                FrmMainFrame.superTabControlManager.CreateTab(frm.Text, uniqueKey, frm, true, true);
            }
            else
                FrmMainFrame.superTabControlManager.ChangeToTab(uniqueKey);
        }

        /// <summary>
        /// 汇总报表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenBuyFuelTransportCollectLoad_Click(object sender, EventArgs e)
        {
            string uniqueKey = CMCS.CarTransport.WeighterHand.Frms.Transport.BuyFuelTransport.FrmBuyFuelTransport_Collect.UniqueKey;

            if (FrmMainFrame.superTabControlManager.GetTab(uniqueKey) == null)
            {
                CMCS.CarTransport.WeighterHand.Frms.Transport.BuyFuelTransport.FrmBuyFuelTransport_Collect frm = new CMCS.CarTransport.WeighterHand.Frms.Transport.BuyFuelTransport.FrmBuyFuelTransport_Collect();
                FrmMainFrame.superTabControlManager.CreateTab(frm.Text, uniqueKey, frm, true, true);
            }
            else
                FrmMainFrame.superTabControlManager.ChangeToTab(uniqueKey);
        }
        /// <summary>
        /// 明细报表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBuyFuelDetail_Click(object sender, EventArgs e)
        {
            string uniqueKey = CMCS.CarTransport.WeighterHand.Frms.Transport.BuyFuelTransport.FrmBuyFuelTransport_Detail.UniqueKey;

            if (FrmMainFrame.superTabControlManager.GetTab(uniqueKey) == null)
            {
                CMCS.CarTransport.WeighterHand.Frms.Transport.BuyFuelTransport.FrmBuyFuelTransport_Detail frm = new CMCS.CarTransport.WeighterHand.Frms.Transport.BuyFuelTransport.FrmBuyFuelTransport_Detail();
                FrmMainFrame.superTabControlManager.CreateTab(frm.Text, uniqueKey, frm, true, true);
            }
            else
                FrmMainFrame.superTabControlManager.ChangeToTab(uniqueKey);
        }

        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenChangePassword_Click(object sender, EventArgs e)
        {
            FrmPassword frmpassword = new FrmPassword();
            frmpassword.ShowDialog();
            if (frmpassword.DialogResult == DialogResult.OK)
            {
                MessageBoxEx.Show("修改密码成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        #endregion

    }
}
