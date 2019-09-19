using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using CMCS.CommonSynch.Utilities;
using DevComponents.DotNetBar.Metro;
using DevComponents.DotNetBar.SuperGrid;
using DevComponents.DotNetBar;

namespace CMCS.CommonSynch
{
    public partial class FrmConfigSet : MetroForm
    {
        CommonAppConfig _CommonAppConfig;

        public FrmConfigSet()
        {
            this._CommonAppConfig = CommonAppConfig.GetInstance();

            InitializeComponent();
        }

        private void FrmConfigSet_Load(object sender, EventArgs e)
        {
            superGridControl1.PrimaryGrid.AutoGenerateColumns = false;

            txtCommonAppConfig.Text = this._CommonAppConfig.AppIdentifier;
            intIptSynchInterval.Value = this._CommonAppConfig.SynchInterval;
            iptxt_ServerIP.Value = this._CommonAppConfig.ServerIp;
            chkStartup.Checked = this._CommonAppConfig.Startup;
            txtServerConnStr.Text = this._CommonAppConfig.ServerConnStr;
            txtClientConnStr.Text = this._CommonAppConfig.ClientConnStr;
            txtSync.Text = this._CommonAppConfig.SyncIdentifier;
            GridComboBoxExEditControl control = superGridControl1.PrimaryGrid.Columns["SynchType"].EditControl as GridComboBoxExEditControl;
            BindGridCombox(control, new List<string>() { "�ϴ�", "�´�", "˫��" });

            superGridControl1.PrimaryGrid.DataSource = this._CommonAppConfig.TableSynchs;
        }

        #region ����

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            List<TableSynch> list = GetGridTableSynch();
            //list.Add(new TableSynch() { Enabled = false, SynchField = "Id", PrimaryKey = "Id", Sequence = 1, SynchTitle = "aa", SynchType = "�ϴ�", TableName = "aa", TableZNName = "aa" });
            list.Add(new TableSynch());
            superGridControl1.PrimaryGrid.DataSource = list;
        }

        /// <summary>
        /// ɾ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnDel_Click(object sender, EventArgs e)
        {
            if (superGridControl1.PrimaryGrid.ActiveRow != null) superGridControl1.PrimaryGrid.Rows.Remove(superGridControl1.PrimaryGrid.ActiveRow as GridRow);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtCommonAppConfig.Text.Trim()))
            {
                MessageBoxEx.Show("����Ψһ��ʶ����Ϊ��!");
                return;
            }
            if (string.IsNullOrEmpty(txtSync.Text.Trim()))
            {
                MessageBoxEx.Show("ͬ����ʶ����Ϊ��!");
                return;
            }
            if (string.IsNullOrEmpty(iptxt_ServerIP.Value.Trim()))
            {
                MessageBoxEx.Show("������IP����Ϊ��!");
                return;
            }
            if (string.IsNullOrEmpty(txtServerConnStr.Text.Trim()))
            {
                MessageBoxEx.Show("��������Oracle���ݿ������ַ�������Ϊ��!");
                return;
            }
            if (string.IsNullOrEmpty(txtClientConnStr.Text.Trim()))
            {
                MessageBoxEx.Show("�͵ض�Oracle���ݿ������ַ�������Ϊ��!");
                return;
            }

            string message = string.Empty;
            List<TableSynch> list = GetGridTableSynch();
            if (!VerifyGridData(list, ref message))
            {
                MessageBoxEx.Show(message);
                return;
            }

            this._CommonAppConfig.AppIdentifier = txtCommonAppConfig.Text;
            this._CommonAppConfig.ServerConnStr = txtServerConnStr.Text;
            this._CommonAppConfig.ClientConnStr = txtClientConnStr.Text;
            this._CommonAppConfig.SynchInterval = intIptSynchInterval.Value;
            this._CommonAppConfig.SyncIdentifier = txtSync.Text;
            this._CommonAppConfig.ServerIp = iptxt_ServerIP.Value;
            this._CommonAppConfig.Startup = chkStartup.Checked;
            this._CommonAppConfig.TableSynchs = list;
            this._CommonAppConfig.Save();

            MessageBoxEx.Show("����ɹ�");

            //ˢ��
            superGridControl1.PrimaryGrid.DataSource = this._CommonAppConfig.TableSynchs;
        }

        /// <summary>
        /// ȡ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCancel_Click(object sender, EventArgs e)
        {

        }

        #endregion

        #region ����

        /// <summary>
        /// ��Grid�����б�
        /// </summary>
        /// <param name="control"></param>
        /// <param name="list"></param>
        private void BindGridCombox(GridComboBoxExEditControl control, List<string> list)
        {
            control.DataSource = list;
        }

        /// <summary>
        /// ��ȡGrid�б�����
        /// </summary>
        /// <returns></returns>
        private List<TableSynch> GetGridTableSynch()
        {
            List<TableSynch> list = new List<TableSynch>();
            foreach (GridRow item in superGridControl1.PrimaryGrid.GridPanel.Rows)
            {
                list.Add(item.DataItem as TableSynch);
            }
            return list;
        }

        /// <summary>
        /// ��֤Grid����
        /// </summary>
        /// <returns></returns>
        private bool VerifyGridData(List<TableSynch> list, ref string message)
        {
            for (int i = 0; i < list.Count; i++)
            {
                TableSynch item = list[i];

                string res = string.Empty;
                if (string.IsNullOrEmpty(item.TableName.Trim()))
                    res += "��������Ϊ��!��";
                if (string.IsNullOrEmpty(item.PrimaryKey.Trim()))
                    res += "����������Ϊ��!��";
                if (string.IsNullOrEmpty(item.SynchTitle.Trim()))
                    res += "���ⲻ��Ϊ��!��";
                if (string.IsNullOrEmpty(item.SynchField.Trim()))
                    res += "ͬ����ʶ�ֶβ���Ϊ��!��";
                if (string.IsNullOrEmpty(item.SynchType.Trim()))
                    res += "ͬ�����Ͳ���Ϊ��!��";

                if (!string.IsNullOrEmpty(res))
                    message += "��" + (i + 1) + "�У�" + res.Substring(0, res.Length - 1) + "<br />";
            }
            if (!string.IsNullOrEmpty(message))
                return false;
            else
                return true;
        }

        #endregion

        /// <summary>
        /// �ػ����к�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void superGridControl1_GetRowHeaderText(object sender, GridGetRowHeaderTextEventArgs e)
        {
            e.Text = (e.GridRow.RowIndex + 1).ToString();
        }

        private void superGridControl1_CellActivated(object sender, GridCellActivatedEventArgs e)
        {
            e.NewActiveCell.BeginEdit(true);
        }

    }
}