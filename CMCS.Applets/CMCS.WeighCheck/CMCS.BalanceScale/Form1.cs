using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CMCS.Common;
using CMCS.Common.DAO;
using CMCS.Common.Entities;
using CMCS.Common.Enums;
using CMCS.Monitor.DAO;
using CMCS.BalanceScale.Frms;
//
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Metro;
using DevComponents.DotNetBar.SuperGrid;
using CMCS.Common.Utilities;
using CMCS.BalanceScale.Enums;
using CMCS.BalanceScale.Utilities;
using CMCS.BalanceScale.DAO;

namespace CMCS.BalanceScale
{
    public partial class Form1 : MetroForm
    {
        public static SuperTabControlManager superTabControlManager;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lblVersion.Text = "�汾��" + new AU.Updater().Version;

            superTabControlManager = new SuperTabControlManager(superTabControl1);

            CreateTrainTipperTab();

            timer1.Enabled = true;
        }

        /// <summary>
        /// ������ƽѡ�
        /// </summary>
        private void CreateTrainTipperTab()
        {
            superTabControl1.SuspendLayout();

            string[] carriageRecognitionerMachineCodes = new string[] { "��ƽ1", "��ƽ2", "��ƽ3", "��ƽ4", "��ƽ5" };
            for (int i = 0; i < carriageRecognitionerMachineCodes.Length; i++)
            {
                if (string.IsNullOrEmpty(carriageRecognitionerMachineCodes[i])) continue;
                superTabControlManager.CreateTab(carriageRecognitionerMachineCodes[i], carriageRecognitionerMachineCodes[i], new FrmBalanceer(carriageRecognitionerMachineCodes[i]), true, false);
            }

            superTabControl1.ResumeLayout();

            // ѡ�е�һ��ѡ�
            if (superTabControl1.Tabs.Count > 0)
                superTabControl1.SelectedTabIndex = 0;
            else
            {
                MessageBoxEx.Show("��ƽ����δ���ã�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Application.Exit();
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                if (MessageBoxEx.Show("ȷ���˳�ϵͳ��", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    Application.Exit();
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }
    }
}
