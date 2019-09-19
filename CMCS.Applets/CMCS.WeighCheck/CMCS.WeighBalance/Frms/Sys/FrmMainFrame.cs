using System;
using System.Windows.Forms;
using CMCS.Common;
using CMCS.Common.DAO;
using CMCS.Common.Enums;
using CMCS.WeighBalance.Utilities;
//
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Metro;

namespace CMCS.WeighBalance.Frms.Sys
{
    public partial class FrmMainFrame : MetroForm
    {
        CommonDAO commonDAO = CommonDAO.GetInstance();

        public static SuperTabControlManager superTabControlManager;
        public static string[] carriageRecognitionerMachineCodes;
        public FrmMainFrame()
        {
            InitializeComponent();
        }

        public FrmMainFrame(string[] machineCodes)
        {
            InitializeComponent();
            carriageRecognitionerMachineCodes = machineCodes;
            this.Text = string.Format("武汉博晟燃料集中管控-{0}称重程序", machineCodes[0]);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lblVersion.Text = new AU.Updater().Version;

            this.superTabControl1.Tabs.Clear();
            FrmMainFrame.superTabControlManager = new SuperTabControlManager(this.superTabControl1);
            CreateTrainTipperTab();
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


        /// <summary>
        /// 创建天平选项卡
        /// </summary>
        private void CreateTrainTipperTab()
        {
            superTabControl1.SuspendLayout();

            //string[] carriageRecognitionerMachineCodes = new string[] { "天平1", "天平2", "天平3", "天平4", "天平5" };
            for (int i = 0; i < carriageRecognitionerMachineCodes.Length; i++)
            {
                if (string.IsNullOrEmpty(carriageRecognitionerMachineCodes[i])) continue;
                superTabControlManager.CreateTab(carriageRecognitionerMachineCodes[i], carriageRecognitionerMachineCodes[i], new Frms.FrmBalanceer(carriageRecognitionerMachineCodes[i]), true, false);
            }

            superTabControl1.ResumeLayout();

            // 选中第一个选项卡
            if (superTabControl1.Tabs.Count > 0)
                superTabControl1.SelectedTabIndex = 0;
            else
            {
                MessageBoxEx.Show("天平参数未设置！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Application.Exit();
            }
        }

    }
}
