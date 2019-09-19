using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CMCS.Common;
using CMCS.Common.DAO;
using CMCS.WeighCheck.MakeWeight.Frms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using DevComponents.Editors;

namespace CMCS.WeighCheck.MakeWeight.Frms
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
                labelX11.ForeColor = Color.Red;
                labelX12.ForeColor = Color.Red;

                txtAppIdentifier.Text = CommonAppConfig.GetInstance().AppIdentifier;
                txtSelfConnStr.Text = CommonAppConfig.GetInstance().SelfConnStr;

                //���ӳ�
                SelectedComboItem("COM" + commonDAO.GetAppletConfigInt32("���ӳӴ���"), cmbLibra_COM);
                SelectedComboItem(commonDAO.GetAppletConfigString("���ӳӲ�����"), cmbLibra_Bandrate);
                SelectedComboItem(commonDAO.GetAppletConfigString("���ӳ�����λ"), cmbDataBits);
                SelectedComboItem(commonDAO.GetAppletConfigString("���ӳ�ֹͣλ"), cmbParity);
                dInputLibraWeight.Value = commonDAO.GetAppletConfigDouble("���ӳ���С����");
                chkIsUseWeight.Checked = Convert.ToBoolean(commonDAO.GetAppletConfigInt32("���ó���"));

                // ������
                iptxtIP.Value = commonDAO.GetAppletConfigString("������IP");
                txtPort.Text = commonDAO.GetAppletConfigString("�������˿�");
                SelectedComboItem(commonDAO.GetAppletConfigString("����������"), cmbSecNumber);
                SelectedComboItem(commonDAO.GetAppletConfigString("����������"), cmbBlockNumber);

                db2Weight.Value = commonDAO.GetAppletConfigDouble("0.2mm������");
                db3Weight.Value = commonDAO.GetAppletConfigDouble("3mm������");
                db6Weight.Value = commonDAO.GetAppletConfigDouble("6mm������");

                // ȫ�ֲ���
                Old_Param = (cmbLibra_COM.SelectedIndex + 1).ToString() + (cmbLibra_Bandrate.SelectedItem as ComboItem).Text
                    + (cmbDataBits.SelectedItem as ComboItem).Text + (cmbParity.SelectedItem as ComboItem).Text
                    + dInputLibraWeight.Value.ToString() + chkIsUseWeight.Checked.ToString();
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
            CommonAppConfig.GetInstance().AppIdentifier = txtAppIdentifier.Text.Trim();
            CommonAppConfig.GetInstance().SelfConnStr = txtSelfConnStr.Text;
            CommonAppConfig.GetInstance().Save();

            //���ӳ�
            commonDAO.SetAppletConfig("���ӳӴ���", (cmbLibra_COM.SelectedIndex + 1).ToString());
            commonDAO.SetAppletConfig("���ӳӲ�����", (cmbLibra_Bandrate.SelectedItem as ComboItem).Text);
            commonDAO.SetAppletConfig("���ӳ�����λ", (cmbDataBits.SelectedItem as ComboItem).Text);
            commonDAO.SetAppletConfig("���ӳ�ֹͣλ", (cmbParity.SelectedItem as ComboItem).Text);
            commonDAO.SetAppletConfig("���ӳ���С����", dInputLibraWeight.Value.ToString());
            commonDAO.SetAppletConfig("���ó���", (chkIsUseWeight.Checked ? 1 : 0).ToString());

            //������
            commonDAO.SetAppletConfig("������IP", iptxtIP.Value);
            commonDAO.SetAppletConfig("�������˿�", txtPort.Text);
            commonDAO.SetAppletConfig("����������", (cmbSecNumber.SelectedItem as ComboItem).Text);
            commonDAO.SetAppletConfig("����������", (cmbBlockNumber.SelectedItem as ComboItem).Text);

            //����
            commonDAO.SetAppletConfig("0.2mm������", db2Weight.Value.ToString());
            commonDAO.SetAppletConfig("3mm������", db3Weight.Value.ToString());
            commonDAO.SetAppletConfig("6mm������", db6Weight.Value.ToString());

            // �رճ������³�ʼ���豸
            if (Old_Param != (cmbLibra_COM.SelectedIndex + 1).ToString() + (cmbLibra_Bandrate.SelectedItem as ComboItem).Text
                + (cmbDataBits.SelectedItem as ComboItem).Text + (cmbParity.SelectedItem as ComboItem).Text
                    + dInputLibraWeight.Value.ToString() + chkIsUseWeight.Checked.ToString())
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