using System;
using DevComponents.DotNetBar.Metro;
using System.Collections.Generic;
using CMCS.Common.Entities.CarTransport;
using CMCS.Common;
using DevComponents.DotNetBar.SuperGrid;
using DevComponents.DotNetBar;
using CMCS.Common.Entities.Fuel;
using CMCS.Common.Entities.BaseInfo;

namespace CMCS.MobilePad.Win.Frms
{
    public partial class FrmTransportPlan : MetroAppForm
    {
        /// <summary>
        /// 窗体唯一标识符
        /// </summary>
        public static string UniqueKey = "FrmTransportPlan";

        string SqlWhere = string.Empty;

        bool hasManagePower = false;
        /// <summary>
        /// 对否有维护权限
        /// </summary>
        public bool HasManagePower
        {
            get
            {
                return hasManagePower;
            }
            set
            {
                hasManagePower = value;
            }
        }

        public FrmTransportPlan()
        {
            InitializeComponent();
        }

        private void FrmHome_Load(object sender, EventArgs e)
        {
            btnSearch_Click(null, null);
            InitFrom();
        }

        public void InitFrom()
        {
            // 打印编码按钮
            GridButtonXEditControl btnPrintCode = superGridControl1.PrimaryGrid.Columns["operation"].EditControl as GridButtonXEditControl;
            btnPrintCode.ColorTable = eButtonColor.BlueWithBackground;
            btnPrintCode.Click += new EventHandler(btnCarDeduction_Click);
        }

        /// <summary>
        /// 打印编码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnCarDeduction_Click(object sender, EventArgs e)
        {
            GridButtonXEditControl btn = sender as GridButtonXEditControl;
            if (btn == null) return;

            CmcsLMYB entity = btn.EditorCell.GridRow.DataItem as CmcsLMYB;
            if (entity == null) return;

            new FrmTransportPlan_Confirm(entity.Id).ShowDialog();

            BindData();
        }

        public void BindData()
        {
            string tempSqlWhere = this.SqlWhere;

            List<CmcsLMYB> list = Dbers.GetInstance().SelfDber.Entities<CmcsLMYB>(tempSqlWhere + " order by InFactoryTime desc");

            superGridControl1.PrimaryGrid.DataSource = list;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.SqlWhere = "where 1=1 ";

            SqlWhere += " and InFactoryTime>= to_date('" + DateTime.Now.Date.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss') ";
            SqlWhere += " and InFactoryTime< to_date('" + DateTime.Now.Date.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss') ";

            if (!String.IsNullOrEmpty((String)txtInput.Text))
            {
                SqlWhere += " and SupplierName like '%" + txtInput.Text + "%' ";
            }
            if (!String.IsNullOrEmpty((String)txtInput2.Text))
            {
                SqlWhere += " and MineName like '%" + txtInput2.Text + "%' ";
            }

            BindData();
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            txtInput.Text = "";
            txtInput2.Text = "";
            btnSearch_Click(null, null);
        }

        private void superGridControl1_DataBindingComplete(object sender, DevComponents.DotNetBar.SuperGrid.GridDataBindingCompleteEventArgs e)
        {
            foreach (GridRow item in e.GridPanel.Rows)
            {
                try
                {
                    CmcsLMYB CmcsLMYB = item.DataItem as CmcsLMYB;

                    CmcsSupplier fuelSupplier = Dbers.GetInstance().SelfDber.Get<CmcsSupplier>(CmcsLMYB.FuelSupplierId);
                    if (fuelSupplier != null)
                    {
                        item.Cells["cellFuelSupplierName"].Value = fuelSupplier != null ? fuelSupplier.Name : "";
                    }
                    item.Cells["cellInFactoryTime"].Value = CmcsLMYB.InFactoryTime.ToString("yyyy/MM/dd");

                    item.Cells["cellCoalNumber"].Value = CmcsLMYB.CoalNumber.ToString("f2");
                }
                catch (Exception)
                {
                }
            }
        }

        private void superGridControl1_GetRowHeaderText(object sender, DevComponents.DotNetBar.SuperGrid.GridGetRowHeaderTextEventArgs e)
        {
            e.Text = (e.GridRow.RowIndex + 1).ToString();
        }

    }
}
