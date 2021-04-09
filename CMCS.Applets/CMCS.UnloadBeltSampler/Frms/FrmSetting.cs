using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CMCS.Common;
using CMCS.Common.DAO;
using CMCS.UnloadSampler.Frms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using DevComponents.Editors;

namespace CMCS.UnloadSampler.Frms
{
	public partial class FrmSetting : DevComponents.DotNetBar.Metro.MetroForm
	{
		CommonDAO commonDAO = CommonDAO.GetInstance();
		CommonAppConfig commonAppConfig = CommonAppConfig.GetInstance();

		string Old_Param = string.Empty;
		public FrmSetting()
		{
			InitializeComponent();
		}

		/// <summary>
		/// ��������
		/// </summary>
		bool SaveAppConfig()
		{
			commonAppConfig.AppIdentifier = txtAppIdentifier.Text.Trim();
			commonAppConfig.SelfConnStr = txtSelfConnStr.Text;
			commonAppConfig.Save();
			commonDAO.SetAppletConfig("�������豸����", txtSampleCode.Text);
			commonDAO.SetAppletConfig("�볧�������豸����", txtRCMakeCode.Text);
			commonDAO.SetAppletConfig("�����������豸����", txtCCMakeCode.Text);
			return true;
		}

		private void FrmSetting_Load(object sender, EventArgs e)
		{
			try
			{
				txtSampleCode.Text = commonDAO.GetAppletConfigString("�������豸����");
				txtRCMakeCode.Text = commonDAO.GetAppletConfigString("�볧�������豸����");
				txtCCMakeCode.Text = commonDAO.GetAppletConfigString("�����������豸����");
				txtAppIdentifier.Text = CommonAppConfig.GetInstance().AppIdentifier;
				txtSelfConnStr.Text = CommonAppConfig.GetInstance().SelfConnStr;
				// ȫ�ֲ���
				Old_Param = txtSampleCode.Text;
			}
			catch (Exception ex)
			{
				MessageBoxEx.Show("������ʼ��ʧ��" + ex.Message, "ϵͳ��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
			}
		}

		/// <summary>
		/// ѡ��ComboItem
		/// </summary>
		/// <param name="text"></param>
		/// <param name="cmb"></param>
		private void SelectedComboItem(string text, ComboBoxEx cmb)
		{
			foreach (ComboItem item in cmb.Items)
			{
				if (item.Text == text)
				{
					cmb.SelectedItem = item;
					break;
				}
			}
		}

		private void btnSubmit_Click(object sender, EventArgs e)
		{
			if (!SaveAppConfig()) return;

			if (MessageBoxEx.Show("���ĵ�������Ҫ�������������Ч���Ƿ�����������", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				Application.Restart();
			else
				this.Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}
	}
}