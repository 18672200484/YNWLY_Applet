using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CMCS.Common;
using CMCS.Common.DAO;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using DevComponents.Editors;

namespace CMCS.BalanceScale.Frms
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
                labelX12.ForeColor = Color.Red;
                labelX17.ForeColor = Color.Red;
                labelX23.ForeColor = Color.Red;

                txtAppIdentifier.Text = CommonAppConfig.GetInstance().AppIdentifier;
                txtSelfConnStr.Text = CommonAppConfig.GetInstance().SelfConnStr;

                //电子天平
                SelectedComboItem("COM" + commonDAO.GetAppletConfigInt32("天平1串口"), cmbLibra_COM);
                SelectedComboItem(commonDAO.GetAppletConfigString("天平1波特率"), cmbLibra_Bandrate);
                SelectedComboItem(commonDAO.GetAppletConfigString("天平1数据位"), cmbDataBits);
                SelectedComboItem(commonDAO.GetAppletConfigString("天平1停止位"), cmbParity);

                SelectedComboItem("COM" + commonDAO.GetAppletConfigInt32("天平2串口"), cmbLibra_COM2);
                SelectedComboItem(commonDAO.GetAppletConfigString("天平2波特率"), cmbLibra_Bandrate2);
                SelectedComboItem(commonDAO.GetAppletConfigString("天平2数据位"), cmbDataBits2);
                SelectedComboItem(commonDAO.GetAppletConfigString("天平2停止位"), cmbParity2);

                SelectedComboItem("COM" + commonDAO.GetAppletConfigInt32("天平3串口"), cmbLibra_COM3);
                SelectedComboItem(commonDAO.GetAppletConfigString("天平3波特率"), cmbLibra_Bandrate3);
                SelectedComboItem(commonDAO.GetAppletConfigString("天平3数据位"), cmbDataBits3);
                SelectedComboItem(commonDAO.GetAppletConfigString("天平3停止位"), cmbParity3);

                SelectedComboItem("COM" + commonDAO.GetAppletConfigInt32("天平4串口"), cmbLibra_COM4);
                SelectedComboItem(commonDAO.GetAppletConfigString("天平4波特率"), cmbLibra_Bandrate4);
                SelectedComboItem(commonDAO.GetAppletConfigString("天平4数据位"), cmbDataBits4);
                SelectedComboItem(commonDAO.GetAppletConfigString("天平4停止位"), cmbParity4);

                SelectedComboItem("COM" + commonDAO.GetAppletConfigInt32("天平5串口"), cmbLibra_COM5);
                SelectedComboItem(commonDAO.GetAppletConfigString("天平5波特率"), cmbLibra_Bandrate5);
                SelectedComboItem(commonDAO.GetAppletConfigString("天平5数据位"), cmbDataBits5);
                SelectedComboItem(commonDAO.GetAppletConfigString("天平5停止位"), cmbParity5);

                SelectedComboItem("COM" + commonDAO.GetAppletConfigInt32("天平6串口"), cmbLibra_COM6);
                SelectedComboItem(commonDAO.GetAppletConfigString("天平6波特率"), cmbLibra_Bandrate6);
                SelectedComboItem(commonDAO.GetAppletConfigString("天平6数据位"), cmbDataBits6);
                SelectedComboItem(commonDAO.GetAppletConfigString("天平6停止位"), cmbParity6);

                // 全局参数
                Old_Param = (cmbLibra_COM.SelectedIndex + 1).ToString() + (cmbLibra_Bandrate.SelectedItem as ComboItem).Text
                    + (cmbDataBits.SelectedItem as ComboItem).Text + (cmbParity.SelectedItem as ComboItem).Text;
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
            //电子天平
            commonDAO.SetAppletConfig("天平1串口", (cmbLibra_COM.SelectedIndex + 1).ToString());
            commonDAO.SetAppletConfig("天平1波特率", (cmbLibra_Bandrate.SelectedItem as ComboItem).Text);
            commonDAO.SetAppletConfig("天平1数据位", (cmbDataBits.SelectedItem as ComboItem).Text);
            commonDAO.SetAppletConfig("天平1停止位", (cmbParity.SelectedItem as ComboItem).Text);

            commonDAO.SetAppletConfig("天平2串口", (cmbLibra_COM2.SelectedIndex + 1).ToString());
            commonDAO.SetAppletConfig("天平2波特率", (cmbLibra_Bandrate2.SelectedItem as ComboItem).Text);
            commonDAO.SetAppletConfig("天平2数据位", (cmbDataBits2.SelectedItem as ComboItem).Text);
            commonDAO.SetAppletConfig("天平2停止位", (cmbParity2.SelectedItem as ComboItem).Text);

            commonDAO.SetAppletConfig("天平3串口", (cmbLibra_COM3.SelectedIndex + 1).ToString());
            commonDAO.SetAppletConfig("天平3波特率", (cmbLibra_Bandrate3.SelectedItem as ComboItem).Text);
            commonDAO.SetAppletConfig("天平3数据位", (cmbDataBits3.SelectedItem as ComboItem).Text);
            commonDAO.SetAppletConfig("天平3停止位", (cmbParity3.SelectedItem as ComboItem).Text);

            commonDAO.SetAppletConfig("天平4串口", (cmbLibra_COM4.SelectedIndex + 1).ToString());
            commonDAO.SetAppletConfig("天平4波特率", (cmbLibra_Bandrate4.SelectedItem as ComboItem).Text);
            commonDAO.SetAppletConfig("天平4数据位", (cmbDataBits4.SelectedItem as ComboItem).Text);
            commonDAO.SetAppletConfig("天平4停止位", (cmbParity4.SelectedItem as ComboItem).Text);

            commonDAO.SetAppletConfig("天平5串口", (cmbLibra_COM5.SelectedIndex + 1).ToString());
            commonDAO.SetAppletConfig("天平5波特率", (cmbLibra_Bandrate5.SelectedItem as ComboItem).Text);
            commonDAO.SetAppletConfig("天平5数据位", (cmbDataBits5.SelectedItem as ComboItem).Text);
            commonDAO.SetAppletConfig("天平5停止位", (cmbParity5.SelectedItem as ComboItem).Text);

            commonDAO.SetAppletConfig("天平6串口", (cmbLibra_COM6.SelectedIndex + 1).ToString());
            commonDAO.SetAppletConfig("天平6波特率", (cmbLibra_Bandrate6.SelectedItem as ComboItem).Text);
            commonDAO.SetAppletConfig("天平6数据位", (cmbDataBits6.SelectedItem as ComboItem).Text);
            commonDAO.SetAppletConfig("天平6停止位", (cmbParity6.SelectedItem as ComboItem).Text);

            // 关闭程序，重新初始化设备
            if (Old_Param != (cmbLibra_COM.SelectedIndex + 1).ToString() + (cmbLibra_Bandrate.SelectedItem as ComboItem).Text
                + (cmbDataBits.SelectedItem as ComboItem).Text + (cmbParity.SelectedItem as ComboItem).Text)
            {
                if (MessageBoxEx.Show("更改的配置需要重启程序才能生效，是否立刻重启？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    Application.Restart();
                else
                    this.Close();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}