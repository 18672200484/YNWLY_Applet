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

namespace CMCS.WeighCheck.SampleMake.Frms
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
                labelX11.ForeColor = Color.Red;
                labelX25.ForeColor = Color.Red;
                labelX30.ForeColor = Color.Red;

                txtAppIdentifier.Text = CommonAppConfig.GetInstance().AppIdentifier;
                txtSelfConnStr.Text = CommonAppConfig.GetInstance().SelfConnStr;

                SelectedComboItem(commonDAO.GetCommonAppletConfigString(CommonAppConfig.GetInstance().AppIdentifier + "��Ӧ������"), cmbMake);

                //���ӳ�
                SelectedComboItem("COM" + commonDAO.GetAppletConfigInt32("���ӳӴ���"), cmbLibra_COM);
                SelectedComboItem(commonDAO.GetAppletConfigString("���ӳӲ�����"), cmbLibra_Bandrate);
                SelectedComboItem(commonDAO.GetAppletConfigString("���ӳ�����λ"), cmbDataBits);
                SelectedComboItem(commonDAO.GetAppletConfigString("���ӳ�ֹͣλ"), cmbParity);
                dInputLibraWeight.Value = commonDAO.GetAppletConfigDouble("���ӳ���С����");
                chkIsUseWeight.Checked = Convert.ToBoolean(commonDAO.GetAppletConfigInt32("���ó���"));
                dobBarrelWeight.Value = commonDAO.GetAppletConfigDouble("�˹���Ͱ����");

                //������ƽ
                SelectedComboItem("COM" + commonDAO.GetAppletConfigInt32("������ƽ����"), cmbLibramin_COM);
                SelectedComboItem(commonDAO.GetAppletConfigString("������ƽ������"), cmbLibramin_Bandrate);
                SelectedComboItem(commonDAO.GetAppletConfigString("������ƽ����λ"), cmbminDataBits);
                SelectedComboItem(commonDAO.GetAppletConfigString("������ƽֹͣλ"), cmbminParity);
                dInputLibraminWeight.Value = commonDAO.GetAppletConfigDouble("������ƽ��С����");
                chkminIsUseWeight.Checked = Convert.ToBoolean(commonDAO.GetAppletConfigInt32("������ƽ���ó���"));

                // ������
                iptxtIP.Value = commonDAO.GetAppletConfigString("������IP");
                txtPort.Text = commonDAO.GetAppletConfigString("�������˿�");
                SelectedComboItem(commonDAO.GetAppletConfigString("����������"), cmbSecNumber);
                SelectedComboItem(commonDAO.GetAppletConfigString("����������"), cmbBlockNumber);

                //����
                db2Weight.Value = commonDAO.GetCommonAppletConfigDouble("0.2mm������");
                db3Weight.Value = commonDAO.GetCommonAppletConfigDouble("3mm������");
                db6Weight.Value = commonDAO.GetCommonAppletConfigDouble("6mm������");

                db2OverWeight.Value = commonDAO.GetCommonAppletConfigDouble("0.2mm������");
                db3OverWeight.Value = commonDAO.GetCommonAppletConfigDouble("3mm������");
                db6OverWeight.Value = commonDAO.GetCommonAppletConfigDouble("6mm������");

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

            commonDAO.SetCommonAppletConfig(CommonAppConfig.GetInstance().AppIdentifier + "��Ӧ������", (cmbMake.SelectedItem as ComboItem).Text);

            //���ӳ�
            commonDAO.SetAppletConfig("���ӳӴ���", (cmbLibra_COM.SelectedIndex + 1).ToString());
            commonDAO.SetAppletConfig("���ӳӲ�����", (cmbLibra_Bandrate.SelectedItem as ComboItem).Text);
            commonDAO.SetAppletConfig("���ӳ�����λ", (cmbDataBits.SelectedItem as ComboItem).Text);
            commonDAO.SetAppletConfig("���ӳ�ֹͣλ", (cmbParity.SelectedItem as ComboItem).Text);
            commonDAO.SetAppletConfig("���ӳ���С����", dInputLibraWeight.Value.ToString());
            commonDAO.SetAppletConfig("���ó���", (chkIsUseWeight.Checked ? 1 : 0).ToString());
            commonDAO.SetAppletConfig("�˹���Ͱ����", (dobBarrelWeight.Value).ToString());

            //������ƽ
            commonDAO.SetAppletConfig("������ƽ����", (cmbLibramin_COM.SelectedIndex + 1).ToString());
            commonDAO.SetAppletConfig("������ƽ������", (cmbLibramin_Bandrate.SelectedItem as ComboItem).Text);
            commonDAO.SetAppletConfig("������ƽ����λ", (cmbminDataBits.SelectedItem as ComboItem).Text);
            commonDAO.SetAppletConfig("������ƽֹͣλ", (cmbminParity.SelectedItem as ComboItem).Text);
            commonDAO.SetAppletConfig("������ƽ��С����", dInputLibraminWeight.Value.ToString());
            commonDAO.SetAppletConfig("������ƽ���ó���", (chkminIsUseWeight.Checked ? 1 : 0).ToString());

            //������
            commonDAO.SetAppletConfig("������IP", iptxtIP.Value);
            commonDAO.SetAppletConfig("�������˿�", txtPort.Text);
            commonDAO.SetAppletConfig("����������", (cmbSecNumber.SelectedItem as ComboItem).Text);
            commonDAO.SetAppletConfig("����������", (cmbBlockNumber.SelectedItem as ComboItem).Text);

            //���� ����
            commonDAO.SetCommonAppletConfig("0.2mm������", db2Weight.Value.ToString());
            commonDAO.SetCommonAppletConfig("3mm������", db3Weight.Value.ToString());
            commonDAO.SetCommonAppletConfig("6mm������", db6Weight.Value.ToString());

            commonDAO.SetCommonAppletConfig("0.2mm������", db2OverWeight.Value.ToString());
            commonDAO.SetCommonAppletConfig("3mm������", db3OverWeight.Value.ToString());
            commonDAO.SetCommonAppletConfig("6mm������", db6OverWeight.Value.ToString());

            // �رճ������³�ʼ���豸
            if (Old_Param != (cmbLibra_COM.SelectedIndex + 1).ToString() + (cmbLibra_Bandrate.SelectedItem as ComboItem).Text
                + (cmbDataBits.SelectedItem as ComboItem).Text + (cmbParity.SelectedItem as ComboItem).Text
                    + dInputLibraWeight.Value.ToString() + chkIsUseWeight.Checked.ToString())
            {
                if (MessageBoxEx.Show("���ĵ�������Ҫ�������������Ч���Ƿ�����������", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    Application.Restart();
                else
                    this.Close();
            }
            else
                this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}