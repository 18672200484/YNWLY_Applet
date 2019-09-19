using System.Collections.Generic;
using System.Windows.Forms;
using DevComponents.DotNetBar.SuperGrid;
using CMCS.Common.DAO;
using CMCS.Common.Entities.BaseInfo;

namespace CMCS.WeighCheck.SampleMake.Frms
{
    public partial class FrmSendUnitSelect : DevComponents.DotNetBar.Metro.MetroForm
    {
        /// <summary>
        /// ѡ�е�ʵ��
        /// </summary>
        public CmcsSendUnit Output;

        /// <summary>
        /// �������
        /// </summary>
        string sqlWhere;

        public FrmSendUnitSelect()
        {
            InitializeComponent();
        }

        public FrmSendUnitSelect(string sqlWhere)
        {
            InitializeComponent();

            this.sqlWhere = sqlWhere;
            Search("");
        }

        private void FrmSupplier_Select_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Output = null;
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        private void txtInput_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Down)
            {
                if (superGridControl1.PrimaryGrid.Rows.Count > 0) superGridControl1.Focus();
            }
            else if (e.KeyCode == Keys.Enter)
            {
                Return();
            }
            else if (!string.IsNullOrEmpty(txtInput.Text.Trim()))
            {
                Search(txtInput.Text.Trim());
            }
            else if (string.IsNullOrEmpty(txtInput.Text.Trim()))
            {
                superGridControl1.PrimaryGrid.DataSource = CommonDAO.GetInstance().GetSendUnitByNameOrChs("", sqlWhere);
            }
        }

        void Search(string input)
        {
            List<CmcsSendUnit> list = CommonDAO.GetInstance().GetSendUnitByNameOrChs(input.Trim(), sqlWhere);
            superGridControl1.PrimaryGrid.DataSource = list;
        }

        void Return()
        {
            GridRow gridRow = superGridControl1.PrimaryGrid.ActiveRow as GridRow;
            if (gridRow == null) return;

            this.Output = (gridRow.DataItem as CmcsSendUnit);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void superGridControl1_BeginEdit(object sender, DevComponents.DotNetBar.SuperGrid.GridEditEventArgs e)
        {
            // ȡ���༭ģʽ
            e.Cancel = true;
        }

        /// <summary>
        /// �����к�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void superGridControl_GetRowHeaderText(object sender, DevComponents.DotNetBar.SuperGrid.GridGetRowHeaderTextEventArgs e)
        {
            e.Text = (e.GridRow.RowIndex + 1).ToString();
        }

        private void superGridControl1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) Return();
        }

        private void superGridControl1_CellDoubleClick(object sender, GridCellDoubleClickEventArgs e)
        {
            Return();
        }
    }
}