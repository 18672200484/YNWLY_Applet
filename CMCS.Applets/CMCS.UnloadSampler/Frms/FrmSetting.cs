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

        string Old_Param = string.Empty;
        public FrmSetting()
        {
            InitializeComponent();
        }

        private void FrmSetting_Load(object sender, EventArgs e)
        {
            try
            {
                txtCommonMake.Text = commonDAO.GetCommonAppletConfigString(CommonAppConfig.GetInstance().AppIdentifier + "对应制样机");
                txtCommonAppConfig.Text = CommonAppConfig.GetInstance().AppIdentifier;
                // 全局参数
                Old_Param = txtCommonMake.Text;
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
            commonDAO.SetCommonAppletConfig(CommonAppConfig.GetInstance().AppIdentifier + "对应制样机", txtCommonMake.Text);
            // 关闭程序，重新初始化设备
            if (Old_Param != txtCommonMake.Text)
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