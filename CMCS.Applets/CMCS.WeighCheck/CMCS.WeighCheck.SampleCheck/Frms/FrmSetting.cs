using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CMCS.Common;
using CMCS.Common.DAO;
using CMCS.WeighCheck.SampleCheck.Frms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using DevComponents.Editors;

namespace CMCS.WeighCheck.SampleCheck.Frms
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
                labelX1.ForeColor = Color.Red;
                labelX4.ForeColor = Color.Red;

                txtAppIdentifier.Text = CommonAppConfig.GetInstance().AppIdentifier;
                txtSelfConnStr.Text = CommonAppConfig.GetInstance().SelfConnStr;

                //电子秤
                SelectedComboItem("COM" + commonDAO.GetAppletConfigInt32("电子秤串口"), cmbLibra_COM);
                SelectedComboItem(commonDAO.GetAppletConfigString("电子秤波特率"), cmbLibra_Bandrate);
                SelectedComboItem(commonDAO.GetAppletConfigString("电子秤数据位"), cmbDataBits);
                SelectedComboItem(commonDAO.GetAppletConfigString("电子秤停止位"), cmbParity);
                dInputLibraWeight.Value = commonDAO.GetAppletConfigDouble("电子秤最小重量");
                chkIsUseWeight.Checked = Convert.ToBoolean(commonDAO.GetAppletConfigInt32("启用称重"));

                cmbMake.Text = commonDAO.GetCommonAppletConfigString(CommonAppConfig.GetInstance().AppIdentifier + "对应制样机");
                // 全局参数
                Old_Param = (cmbLibra_COM.SelectedIndex + 1).ToString() + (cmbLibra_Bandrate.SelectedItem as ComboItem).Text
                    + (cmbDataBits.SelectedItem as ComboItem).Text + (cmbParity.SelectedItem as ComboItem).Text
                    + dInputLibraWeight.Value.ToString() + chkIsUseWeight.Checked.ToString() + cmbMake.Text;
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
            CommonAppConfig.GetInstance().AppIdentifier = txtAppIdentifier.Text;
            CommonAppConfig.GetInstance().SelfConnStr = txtSelfConnStr.Text;
            CommonAppConfig.GetInstance().Save();

            //电子秤
            commonDAO.SetAppletConfig("电子秤串口", (cmbLibra_COM.SelectedIndex + 1).ToString());
            commonDAO.SetAppletConfig("电子秤波特率", (cmbLibra_Bandrate.SelectedItem as ComboItem).Text);
            commonDAO.SetAppletConfig("电子秤数据位", (cmbDataBits.SelectedItem as ComboItem).Text);
            commonDAO.SetAppletConfig("电子秤停止位", (cmbParity.SelectedItem as ComboItem).Text);
            commonDAO.SetAppletConfig("电子秤最小重量", dInputLibraWeight.Value.ToString());
            commonDAO.SetAppletConfig("启用称重", (chkIsUseWeight.Checked ? 1 : 0).ToString());

            commonDAO.SetCommonAppletConfig(CommonAppConfig.GetInstance().AppIdentifier + "对应制样机", cmbMake.Text);
            // 关闭程序，重新初始化设备
            if (Old_Param != (cmbLibra_COM.SelectedIndex + 1).ToString() + (cmbLibra_Bandrate.SelectedItem as ComboItem).Text
                + (cmbDataBits.SelectedItem as ComboItem).Text + (cmbParity.SelectedItem as ComboItem).Text
                    + dInputLibraWeight.Value.ToString() + chkIsUseWeight.Checked.ToString() + cmbMake.Text)
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