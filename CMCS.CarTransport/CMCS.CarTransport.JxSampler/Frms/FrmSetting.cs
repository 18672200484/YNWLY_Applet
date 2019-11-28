using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using CMCS.CarTransport.JxSampler.Core;
using System.IO.Ports;
using CMCS.Common.DAO;
using CMCS.Common;
using CMCS.DapperDber.Dbs.OracleDb;
using CMCS.Common.Utilities;

namespace CMCS.CarTransport.JxSampler.Frms
{
    public partial class FrmSetting : DevComponents.DotNetBar.Metro.MetroForm
    {
        CommonDAO commonDAO = CommonDAO.GetInstance();

        CommonAppConfig commonAppConfig = CommonAppConfig.GetInstance();

        public FrmSetting()
        {
            InitializeComponent();
        }

        void InitForm()
        {
            InitBandrateComboBoxs(cmbIocerBandrate);
            InitNumberAscComboBoxs(5, 8, cmbIocerDataBits);
            InitNumberAscComboBoxs(1, 15, cmbInductorCoil1Port, cmbInductorCoil2Port, cmbGate1UpPort, cmbGate1DownPort, cmbIocerCom, cmbAffirmPort);
            InitStopBitsComboBoxs(cmbIocerStopBits);
            InitParityComboBoxs(cmbIocerParity);
        }

        private void FrmSetting_Load(object sender, EventArgs e)
        {

        }

        private void FrmSetting_Shown(object sender, EventArgs e)
        {
            InitForm();

            LoadAppConfig();
        }

        /// <summary>
        /// �������ݿ�����
        /// </summary>
        /// <returns></returns>
        private bool TestDBConnect()
        {
            if (string.IsNullOrEmpty(txtSelfConnStr.Text.Trim()))
            {
                MessageBoxEx.Show("�����������ݿ������ַ���", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            try
            {
                OracleDapperDber dber = new OracleDapperDber(txtSelfConnStr.Text.Trim());
                if (dber.Connection.State != ConnectionState.Open)
                {
                    dber.Connection.Open();
                    dber.Connection.Close();
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show("���ݿ�����ʧ�ܣ�" + ex.Message, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
        }

        /// <summary>
        /// ��������
        /// </summary>
        void LoadAppConfig()
        {
            txtAppIdentifier.Text = commonAppConfig.AppIdentifier;
            txtSelfConnStr.Text = commonAppConfig.SelfConnStr;
            chbStartup.Checked = (commonDAO.GetAppletConfigString("��������") == "1");

            txtJxSamplerMachineCode.Text = commonDAO.GetAppletConfigString("�������豸����");
            if (txtJxSamplerMachineCode.Text.Contains("1"))
                txtSqlConnStr.Text = commonDAO.GetCommonAppletConfigString("#1������е�������ӿ������ַ���");
            else if (txtJxSamplerMachineCode.Text.Contains("2"))
                txtSqlConnStr.Text = commonDAO.GetCommonAppletConfigString("#2������е�������ӿ������ַ���");
            // IO������
            SelectedComboBoxItem(cmbIocerCom, commonDAO.GetAppletConfigInt32("IO������_����").ToString());
            SelectedComboBoxItem(cmbIocerBandrate, commonDAO.GetAppletConfigInt32("IO������_������").ToString());
            SelectedComboBoxItem(cmbIocerDataBits, commonDAO.GetAppletConfigInt32("IO������_����λ").ToString());
            SelectedComboBoxItem(cmbIocerStopBits, commonDAO.GetAppletConfigInt32("IO������_ֹͣλ").ToString());
            SelectedComboBoxItem(cmbIocerParity, commonDAO.GetAppletConfigInt32("IO������_У��λ").ToString());
            SelectedComboBoxItem(cmbInductorCoil1Port, commonDAO.GetAppletConfigInt32("IO������_�ظ�1�˿�").ToString());
            SelectedComboBoxItem(cmbInductorCoil2Port, commonDAO.GetAppletConfigInt32("IO������_�ظ�2�˿�").ToString());
            SelectedComboBoxItem(cmbGate1UpPort, commonDAO.GetAppletConfigInt32("IO������_��բ1���˶˿�").ToString());
            SelectedComboBoxItem(cmbGate1DownPort, commonDAO.GetAppletConfigInt32("IO������_��բ1���˶˿�").ToString());
            SelectedComboBoxItem(cmbAffirmPort, commonDAO.GetAppletConfigInt32("IO������_ȷ�ϰ�ť�˿�").ToString());

            // ����ʶ��
            iptxtRwer1.Value = commonDAO.GetAppletConfigString("����ʶ��1_IP��ַ");

            // LED��ʾ��
            iptxtLED1IP.Value = commonDAO.GetAppletConfigString("LED��ʾ��1_IP��ַ");
        }

        /// <summary>
        /// ��������
        /// </summary>
        bool SaveAppConfig()
        {
            if (!TestDBConnect()) return false;

            commonAppConfig.AppIdentifier = txtAppIdentifier.Text.Trim();
            commonAppConfig.SelfConnStr = txtSelfConnStr.Text;
            commonAppConfig.Save();
            commonDAO.SetAppletConfig("��������", Convert.ToInt16(chbStartup.Checked).ToString());
            if (txtJxSamplerMachineCode.Text.Contains("1"))
            {
                commonDAO.SetCommonAppletConfig("#1������е�������ӿ������ַ���", txtSqlConnStr.Text.Trim());
            }
            else if (txtJxSamplerMachineCode.Text.Contains("2"))
            {
                commonDAO.SetCommonAppletConfig("#2������е�������ӿ������ַ���", txtSqlConnStr.Text.Trim());
            }
            commonDAO.SetAppletConfig("�������豸����", txtJxSamplerMachineCode.Text.Trim());

            try
            {
#if DEBUG

#else
                // ���ӡ�ȡ����������
                if (chbStartup.Checked)
                    StartUpUtil.InsertStartUp(Application.ProductName, Application.ExecutablePath);
                else
                    StartUpUtil.DeleteStartUp(Application.ProductName);
#endif
            }
            catch { }

            // IO������
            commonDAO.SetAppletConfig("IO������_����", (cmbIocerCom.SelectedItem as DataItem).Value);
            commonDAO.SetAppletConfig("IO������_������", (cmbIocerBandrate.SelectedItem as DataItem).Value);
            commonDAO.SetAppletConfig("IO������_����λ", (cmbIocerDataBits.SelectedItem as DataItem).Value);
            commonDAO.SetAppletConfig("IO������_ֹͣλ", (cmbIocerStopBits.SelectedItem as DataItem).Value);
            commonDAO.SetAppletConfig("IO������_У��λ", (cmbIocerParity.SelectedItem as DataItem).Value);
            commonDAO.SetAppletConfig("IO������_�ظ�1�˿�", (cmbInductorCoil1Port.SelectedItem as DataItem).Value);
            commonDAO.SetAppletConfig("IO������_�ظ�2�˿�", (cmbInductorCoil2Port.SelectedItem as DataItem).Value);
            commonDAO.SetAppletConfig("IO������_��բ1���˶˿�", (cmbGate1UpPort.SelectedItem as DataItem).Value);
            commonDAO.SetAppletConfig("IO������_��բ1���˶˿�", (cmbGate1DownPort.SelectedItem as DataItem).Value);
            commonDAO.SetAppletConfig("IO������_ȷ�ϰ�ť�˿�", (cmbAffirmPort.SelectedItem as DataItem).Value);

            // ����ʶ��
            commonDAO.SetAppletConfig("����ʶ��1_IP��ַ", iptxtRwer1.Value);

            // LED��ʾ��
            commonDAO.SetAppletConfig("LED��ʾ��1_IP��ַ", iptxtLED1IP.Value);

            return true;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!ValidateInputEmpty(new List<string> { "����Ψһ��ʶ��", "���ݿ������ַ���" }, new List<Control> { txtAppIdentifier, txtSelfConnStr })) return;

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

        #region ��������

        /// <summary>
        /// ��֤�����ؼ�Ϊ�գ�����ʾ
        /// </summary>
        /// <param name="tipsNames"></param>
        /// <param name="controls"></param>
        /// <returns></returns>
        public static bool ValidateInputEmpty(List<string> tipsNames, List<Control> controls)
        {
            for (int i = 0; i < controls.Count; i++)
            {
                Control control = controls[i];

                if (control is TextBoxX && string.IsNullOrEmpty(((TextBoxX)control).Text))
                {
                    control.Focus();
                    MessageBoxEx.Show("������" + tipsNames[i] + "��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                    return false;
                }
            }

            return true;
        }

        /// <summary>
        /// ѡ��������ѡ��
        /// </summary>
        /// <param name="cmb"></param>
        /// <param name="text"></param>
        private void SelectedComboBoxItem(ComboBoxEx cmb, string value)
        {
            foreach (DataItem dataItem in cmb.Items)
            {
                if (dataItem.Value == value) cmb.SelectedItem = dataItem;
            }
        }

        /// <summary>
        /// ��ʼ������������
        /// </summary>
        /// <param name="cmb"></param>
        void InitComPortComboBox(ComboBoxEx cmb)
        {
            cmb.Items.Clear();

            cmb.DisplayMember = "Text";
            cmb.ValueMember = "Value";

            for (int i = 1; i < 20; i++)
            {
                cmb.Items.Add(new DataItem("COM" + i.ToString(), i.ToString()));
            }

            cmb.SelectedIndex = 0;
        }

        /// <summary>
        /// ��ʼ������������
        /// </summary>
        /// <param name="cmbs"></param>
        void InitComPortComboBoxs(params ComboBoxEx[] cmbs)
        {
            foreach (ComboBoxEx cmb in cmbs)
            {
                InitComPortComboBox(cmb);
            }
        }

        /// <summary>
        /// ��ʼ��������������
        /// </summary>
        /// <param name="cmb"></param>
        private void InitBandrateComboBox(ComboBoxEx cmb)
        {
            cmb.Items.Clear();

            cmb.DisplayMember = "Text";
            cmb.ValueMember = "Value";

            cmb.Items.Add(new DataItem("1200"));
            cmb.Items.Add(new DataItem("4800"));
            cmb.Items.Add(new DataItem("9600"));
            cmb.Items.Add(new DataItem("14400"));
            cmb.Items.Add(new DataItem("19200"));
            cmb.Items.Add(new DataItem("38400"));
            cmb.Items.Add(new DataItem("56000"));
            cmb.Items.Add(new DataItem("57600"));
            cmb.Items.Add(new DataItem("115200"));

            cmb.SelectedIndex = 0;
        }

        /// <summary>
        /// ��ʼ��������������
        /// </summary>
        /// <param name="cmbs"></param>
        void InitBandrateComboBoxs(params ComboBoxEx[] cmbs)
        {
            foreach (ComboBoxEx cmb in cmbs)
            {
                InitBandrateComboBox(cmb);
            }
        }

        /// <summary>
        /// ��ʼ������������
        /// </summary>
        /// <param name="cmb"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        void InitNumberAscComboBox(int start, int end, ComboBoxEx cmb)
        {
            cmb.Items.Clear();

            cmb.DisplayMember = "Text";
            cmb.ValueMember = "Value";

            for (int i = start; i <= end; i++)
            {
                cmb.Items.Add(new DataItem(i.ToString()));
            }

            if (cmb.Items.Count > 0) cmb.SelectedIndex = 0;
        }

        /// <summary>
        /// ��ʼ������������
        /// </summary>
        /// <param name="cmb"></param>
        /// <param name="start"></param>
        /// <param name="end"></param>
        void InitNumberAscComboBoxs(int start, int end, params ComboBoxEx[] cmbs)
        {
            foreach (ComboBoxEx cmb in cmbs)
            {
                InitNumberAscComboBox(start, end, cmb);
            }
        }

        /// <summary>
        /// ��ʼ��ֹͣλ������
        /// </summary>
        /// <param name="cmb"></param>
        void InitStopBitsComboBox(ComboBoxEx cmb)
        {
            cmb.Items.Clear();

            cmb.DisplayMember = "Text";
            cmb.ValueMember = "Value";

            cmb.Items.Add(new DataItem(StopBits.None.ToString(), ((int)StopBits.None).ToString()));
            cmb.Items.Add(new DataItem(StopBits.One.ToString(), ((int)StopBits.One).ToString()));
            cmb.Items.Add(new DataItem(StopBits.OnePointFive.ToString(), ((int)StopBits.OnePointFive).ToString()));
            cmb.Items.Add(new DataItem(StopBits.Two.ToString(), ((int)StopBits.Two).ToString()));

            cmb.SelectedIndex = 0;
        }

        /// <summary>
        /// ��ʼ��ֹͣλ������
        /// </summary>
        /// <param name="cmbs"></param>
        void InitStopBitsComboBoxs(params ComboBoxEx[] cmbs)
        {
            foreach (ComboBoxEx cmb in cmbs)
            {
                InitStopBitsComboBox(cmb);
            }
        }

        /// <summary>
        /// ��ʼ��У��λ������
        /// </summary>
        /// <param name="cmb"></param>
        void InitParityComboBox(ComboBoxEx cmb)
        {
            cmb.Items.Clear();

            cmb.DisplayMember = "Text";
            cmb.ValueMember = "Value";

            cmb.Items.Add(new DataItem(Parity.None.ToString(), ((int)Parity.None).ToString()));
            cmb.Items.Add(new DataItem(Parity.Odd.ToString(), ((int)Parity.Odd).ToString()));
            cmb.Items.Add(new DataItem(Parity.Even.ToString(), ((int)Parity.Even).ToString()));
            cmb.Items.Add(new DataItem(Parity.Mark.ToString(), ((int)Parity.Mark).ToString()));
            cmb.Items.Add(new DataItem(Parity.Space.ToString(), ((int)Parity.Space).ToString()));

            cmb.SelectedIndex = 0;
        }

        /// <summary>
        /// ��ʼ��У��λ������
        /// </summary>
        /// <param name="cmbs"></param>
        void InitParityComboBoxs(params ComboBoxEx[] cmbs)
        {
            foreach (ComboBoxEx cmb in cmbs)
            {
                InitParityComboBox(cmb);
            }
        }

        #endregion
    }
}