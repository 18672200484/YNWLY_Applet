using System;
using DevComponents.DotNetBar.Metro;
using System.Collections.Generic;
using CMCS.Common.Entities.CarTransport;
using CMCS.Common;
using DevComponents.DotNetBar.SuperGrid;
using DevComponents.DotNetBar;

namespace CMCS.MobilePad.Win.Frms
{
    public partial class FrmCarDeduction : MetroAppForm
    {
        /// <summary>
        /// 窗体唯一标识符
        /// </summary>
        public static string UniqueKey = "FrmCarDeduction";

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

        public FrmCarDeduction()
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

            CmcsBuyFuelTransport buyFuelTransport = btn.EditorCell.GridRow.DataItem as CmcsBuyFuelTransport;
            if (buyFuelTransport == null) return;

            new FrmCarDeduction_Confirm(buyFuelTransport.Id).ShowDialog();

            BindData();
        }

        public void BindData()
        {
            string tempSqlWhere = this.SqlWhere;

            List<CmcsBuyFuelTransport> list = Dbers.GetInstance().SelfDber.Entities<CmcsBuyFuelTransport>(tempSqlWhere);

            superGridControl1.PrimaryGrid.DataSource = list;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.SqlWhere = "where 1=1 ";

            SqlWhere += " and GrossTime>= to_date('" + DateTime.Now.Date.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss') ";
            SqlWhere += " and GrossTime< to_date('" + DateTime.Now.Date.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss') ";
            SqlWhere += " and ((GrossWeight>0 and TareWeight<0) or (KsWeight>0 or KgWeight>0))";

            if (!String.IsNullOrEmpty((String)txtInput.Text))
            {
                SqlWhere += " and CarNumber like '%" + txtInput.Text + "%' ";
            }
            SqlWhere += " order by GrossTime,TareTime";
            BindData();
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            txtInput.Text = "";
            btnSearch_Click(null, null);
        }

        private void superGridControl1_DataBindingComplete(object sender, DevComponents.DotNetBar.SuperGrid.GridDataBindingCompleteEventArgs e)
        {
            foreach (GridRow item in e.GridPanel.Rows)
            {
                try
                {
                    CmcsBuyFuelTransport cmcsBuyFuelTransport = item.DataItem as CmcsBuyFuelTransport;
                    item.Cells["cellGrossWeight"].Value = cmcsBuyFuelTransport.GrossWeight.ToString("f2");
                    item.Cells["cellTareWeight"].Value = cmcsBuyFuelTransport.TareWeight.ToString("f2");
                    item.Cells["cellSuttleWeight"].Value = cmcsBuyFuelTransport.SuttleWeight.ToString("f2");
                    item.Cells["cellKgWeight"].Value = cmcsBuyFuelTransport.KgWeight.ToString("f2");
                    item.Cells["cellKsWeight"].Value = cmcsBuyFuelTransport.KsWeight.ToString("f2");
                    item.Cells["cellkdzt"].Value = (cmcsBuyFuelTransport.KgWeight + cmcsBuyFuelTransport.KsWeight) > 0 ? "已扣吨" : "";
                    if (cmcsBuyFuelTransport.SuttleWeight > 0)
                        item.Cells["operation"].Visible = false;
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
