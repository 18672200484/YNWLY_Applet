using System;
using DevComponents.DotNetBar.Metro;
using System.Collections.Generic;
using CMCS.Common.Entities.CarTransport;
using CMCS.Common;
using DevComponents.DotNetBar.SuperGrid;
using DevComponents.DotNetBar;

namespace CMCS.MobilePad.Win.Frms
{
    public partial class FrmCarManage : MetroAppForm
    {
        /// <summary>
        /// 窗体唯一标识符
        /// </summary>
        public static string UniqueKey = "FrmCarManage";

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

        public FrmCarManage()
        {
            InitializeComponent();
        }

        private void FrmHome_Load(object sender, EventArgs e)
        {
            btnSearch_Click(null,null);
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

            CmcsAutotruck buyFuelTransport = btn.EditorCell.GridRow.DataItem as CmcsAutotruck;
            if (buyFuelTransport == null) return;

            new FrmCarManage_Confirm(buyFuelTransport.Id).ShowDialog();

            BindData();
        }

        public void BindData()
        {
            string tempSqlWhere = this.SqlWhere;

            List<CmcsAutotruck> list = Dbers.GetInstance().SelfDber.Entities<CmcsAutotruck>(tempSqlWhere + " order by CreateDate desc");

            superGridControl1.PrimaryGrid.DataSource = list;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.SqlWhere = string.Empty;

            if (!String.IsNullOrEmpty((String)txtInput.Text))
            {
                SqlWhere += " and CarNumber like '%" + txtInput.Text + "%' ";
            }

            if (!String.IsNullOrEmpty(this.SqlWhere))
            {
                SqlWhere = " where 1=1 " + SqlWhere;
            }
            BindData();
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            this.SqlWhere = string.Empty;
            txtInput.Text = "";

            if (!String.IsNullOrEmpty(this.SqlWhere))
            {
                SqlWhere = " where 1=1 " + SqlWhere;
            }
            BindData();
        }

        private void superGridControl1_DataBindingComplete(object sender, DevComponents.DotNetBar.SuperGrid.GridDataBindingCompleteEventArgs e)
        {
            foreach (GridRow item in e.GridPanel.Rows)
            {
                try
                {
                    CmcsAutotruck CmcsAutotruck = item.DataItem as CmcsAutotruck;

                    item.Cells["cellIsUse"].Value = CmcsAutotruck.IsUse == 1 ? "有效" : (CmcsAutotruck.IsUse ==  -1 ? "黑名单" : "无效");
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
