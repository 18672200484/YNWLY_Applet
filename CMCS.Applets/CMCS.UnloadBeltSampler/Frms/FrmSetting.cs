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
		/// 保存配置
		/// </summary>
		bool SaveAppConfig()
		{
			commonAppConfig.AppIdentifier = txtAppIdentifier.Text.Trim();
			commonAppConfig.SelfConnStr = txtSelfConnStr.Text;
			commonAppConfig.Save();
			commonDAO.SetAppletConfig("采样机设备编码", txtSampleCode.Text);
			commonDAO.SetAppletConfig("入厂制样机设备编码", txtRCMakeCode.Text);
			commonDAO.SetAppletConfig("出厂制样机设备编码", txtCCMakeCode.Text);
			return true;
		}

		private void FrmSetting_Load(object sender, EventArgs e)
		{
			try
			{
				txtSampleCode.Text = commonDAO.GetAppletConfigString("采样机设备编码");
				txtRCMakeCode.Text = commonDAO.GetAppletConfigString("入厂制样机设备编码");
				txtCCMakeCode.Text = commonDAO.GetAppletConfigString("出厂制样机设备编码");
				txtAppIdentifier.Text = CommonAppConfig.GetInstance().AppIdentifier;
				txtSelfConnStr.Text = CommonAppConfig.GetInstance().SelfConnStr;
				// 全局参数
				Old_Param = txtSampleCode.Text;
			}
			catch (Exception ex)
			{
				MessageBoxEx.Show("参数初始化失败" + ex.Message, "系统提示", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
			}
		}

		/// <summary>
		/// 选中ComboItem
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

			if (MessageBoxEx.Show("更改的配置需要重启程序才能生效，是否立刻重启？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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