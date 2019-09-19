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

                //������ƽ
                SelectedComboItem("COM" + commonDAO.GetAppletConfigInt32("��ƽ1����"), cmbLibra_COM);
                SelectedComboItem(commonDAO.GetAppletConfigString("��ƽ1������"), cmbLibra_Bandrate);
                SelectedComboItem(commonDAO.GetAppletConfigString("��ƽ1����λ"), cmbDataBits);
                SelectedComboItem(commonDAO.GetAppletConfigString("��ƽ1ֹͣλ"), cmbParity);

                SelectedComboItem("COM" + commonDAO.GetAppletConfigInt32("��ƽ2����"), cmbLibra_COM2);
                SelectedComboItem(commonDAO.GetAppletConfigString("��ƽ2������"), cmbLibra_Bandrate2);
                SelectedComboItem(commonDAO.GetAppletConfigString("��ƽ2����λ"), cmbDataBits2);
                SelectedComboItem(commonDAO.GetAppletConfigString("��ƽ2ֹͣλ"), cmbParity2);

                SelectedComboItem("COM" + commonDAO.GetAppletConfigInt32("��ƽ3����"), cmbLibra_COM3);
                SelectedComboItem(commonDAO.GetAppletConfigString("��ƽ3������"), cmbLibra_Bandrate3);
                SelectedComboItem(commonDAO.GetAppletConfigString("��ƽ3����λ"), cmbDataBits3);
                SelectedComboItem(commonDAO.GetAppletConfigString("��ƽ3ֹͣλ"), cmbParity3);

                SelectedComboItem("COM" + commonDAO.GetAppletConfigInt32("��ƽ4����"), cmbLibra_COM4);
                SelectedComboItem(commonDAO.GetAppletConfigString("��ƽ4������"), cmbLibra_Bandrate4);
                SelectedComboItem(commonDAO.GetAppletConfigString("��ƽ4����λ"), cmbDataBits4);
                SelectedComboItem(commonDAO.GetAppletConfigString("��ƽ4ֹͣλ"), cmbParity4);

                SelectedComboItem("COM" + commonDAO.GetAppletConfigInt32("��ƽ5����"), cmbLibra_COM5);
                SelectedComboItem(commonDAO.GetAppletConfigString("��ƽ5������"), cmbLibra_Bandrate5);
                SelectedComboItem(commonDAO.GetAppletConfigString("��ƽ5����λ"), cmbDataBits5);
                SelectedComboItem(commonDAO.GetAppletConfigString("��ƽ5ֹͣλ"), cmbParity5);

                SelectedComboItem("COM" + commonDAO.GetAppletConfigInt32("��ƽ6����"), cmbLibra_COM6);
                SelectedComboItem(commonDAO.GetAppletConfigString("��ƽ6������"), cmbLibra_Bandrate6);
                SelectedComboItem(commonDAO.GetAppletConfigString("��ƽ6����λ"), cmbDataBits6);
                SelectedComboItem(commonDAO.GetAppletConfigString("��ƽ6ֹͣλ"), cmbParity6);

                // ȫ�ֲ���
                Old_Param = (cmbLibra_COM.SelectedIndex + 1).ToString() + (cmbLibra_Bandrate.SelectedItem as ComboItem).Text
                    + (cmbDataBits.SelectedItem as ComboItem).Text + (cmbParity.SelectedItem as ComboItem).Text;
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
            //������ƽ
            commonDAO.SetAppletConfig("��ƽ1����", (cmbLibra_COM.SelectedIndex + 1).ToString());
            commonDAO.SetAppletConfig("��ƽ1������", (cmbLibra_Bandrate.SelectedItem as ComboItem).Text);
            commonDAO.SetAppletConfig("��ƽ1����λ", (cmbDataBits.SelectedItem as ComboItem).Text);
            commonDAO.SetAppletConfig("��ƽ1ֹͣλ", (cmbParity.SelectedItem as ComboItem).Text);

            commonDAO.SetAppletConfig("��ƽ2����", (cmbLibra_COM2.SelectedIndex + 1).ToString());
            commonDAO.SetAppletConfig("��ƽ2������", (cmbLibra_Bandrate2.SelectedItem as ComboItem).Text);
            commonDAO.SetAppletConfig("��ƽ2����λ", (cmbDataBits2.SelectedItem as ComboItem).Text);
            commonDAO.SetAppletConfig("��ƽ2ֹͣλ", (cmbParity2.SelectedItem as ComboItem).Text);

            commonDAO.SetAppletConfig("��ƽ3����", (cmbLibra_COM3.SelectedIndex + 1).ToString());
            commonDAO.SetAppletConfig("��ƽ3������", (cmbLibra_Bandrate3.SelectedItem as ComboItem).Text);
            commonDAO.SetAppletConfig("��ƽ3����λ", (cmbDataBits3.SelectedItem as ComboItem).Text);
            commonDAO.SetAppletConfig("��ƽ3ֹͣλ", (cmbParity3.SelectedItem as ComboItem).Text);

            commonDAO.SetAppletConfig("��ƽ4����", (cmbLibra_COM4.SelectedIndex + 1).ToString());
            commonDAO.SetAppletConfig("��ƽ4������", (cmbLibra_Bandrate4.SelectedItem as ComboItem).Text);
            commonDAO.SetAppletConfig("��ƽ4����λ", (cmbDataBits4.SelectedItem as ComboItem).Text);
            commonDAO.SetAppletConfig("��ƽ4ֹͣλ", (cmbParity4.SelectedItem as ComboItem).Text);

            commonDAO.SetAppletConfig("��ƽ5����", (cmbLibra_COM5.SelectedIndex + 1).ToString());
            commonDAO.SetAppletConfig("��ƽ5������", (cmbLibra_Bandrate5.SelectedItem as ComboItem).Text);
            commonDAO.SetAppletConfig("��ƽ5����λ", (cmbDataBits5.SelectedItem as ComboItem).Text);
            commonDAO.SetAppletConfig("��ƽ5ֹͣλ", (cmbParity5.SelectedItem as ComboItem).Text);

            commonDAO.SetAppletConfig("��ƽ6����", (cmbLibra_COM6.SelectedIndex + 1).ToString());
            commonDAO.SetAppletConfig("��ƽ6������", (cmbLibra_Bandrate6.SelectedItem as ComboItem).Text);
            commonDAO.SetAppletConfig("��ƽ6����λ", (cmbDataBits6.SelectedItem as ComboItem).Text);
            commonDAO.SetAppletConfig("��ƽ6ֹͣλ", (cmbParity6.SelectedItem as ComboItem).Text);

            // �رճ������³�ʼ���豸
            if (Old_Param != (cmbLibra_COM.SelectedIndex + 1).ToString() + (cmbLibra_Bandrate.SelectedItem as ComboItem).Text
                + (cmbDataBits.SelectedItem as ComboItem).Text + (cmbParity.SelectedItem as ComboItem).Text)
            {
                if (MessageBoxEx.Show("���ĵ�������Ҫ�������������Ч���Ƿ�����������", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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