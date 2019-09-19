using System;
using DevComponents.DotNetBar.Metro;
using System.Collections.Generic;
using CMCS.Common.Entities.CarTransport;
using CMCS.Common;
using DevComponents.DotNetBar.SuperGrid;
using DevComponents.DotNetBar;
using CMCS.Common.Entities.Storage;
using System.Windows.Forms;

namespace CMCS.MobilePad.Win.Frms
{
    public partial class FrmStorageTemperature : MetroAppForm
    {
        /// <summary>
        /// 窗体唯一标识符
        /// </summary>
        public static string UniqueKey = "FrmStorageTemperature";

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

        public FrmStorageTemperature()
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

            GridButtonXEditControl btnClmDelete = superGridControl1.PrimaryGrid.Columns["clmDelete"].EditControl as GridButtonXEditControl;
            btnClmDelete.ColorTable = eButtonColor.BlueWithBackground;
            btnClmDelete.Click += new EventHandler(btnDelete_Click);
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnDelete_Click(object sender, EventArgs e)
        {
            GridButtonXEditControl btn = sender as GridButtonXEditControl;
            if (btn == null) return;

            StorageTemperature entity = btn.EditorCell.GridRow.DataItem as StorageTemperature;
            if (entity == null) return;
 
            if (MessageBoxEx.Show("确定要删除该测温杆记录？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    Dbers.GetInstance().SelfDber.Delete<StorageTemperature>(entity.Id);
                }
                catch (Exception)
                {
                  
                }

                BindData();
            }
        }

        /// <summary>
        /// 修改
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnCarDeduction_Click(object sender, EventArgs e)
        {
            GridButtonXEditControl btn = sender as GridButtonXEditControl;
            if (btn == null) return;

            StorageTemperature buyFuelTransport = btn.EditorCell.GridRow.DataItem as StorageTemperature;
            if (buyFuelTransport == null) return;

            new FrmStorageTemperature_Confirm(buyFuelTransport.Id).ShowDialog();

            BindData();
        }

        public void BindData()
        {
            string tempSqlWhere = this.SqlWhere;

            List<StorageTemperature> list = Dbers.GetInstance().SelfDber.Entities<StorageTemperature>(tempSqlWhere + " order by CreateDate desc");

            superGridControl1.PrimaryGrid.DataSource = list;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.SqlWhere = string.Empty;

            if (!String.IsNullOrEmpty((String)txtInput.Text))
            {
                SqlWhere += " and PoleCode like '%" + txtInput.Text + "%' ";
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
                    StorageTemperature entity = item.DataItem as StorageTemperature;

                    item.Cells["cellPointX"].Value = entity.PointX.ToString("f2");
                    item.Cells["cellPointY"].Value = entity.PointY.ToString("f2");
                    item.Cells["cellTemperature"].Value = entity.Temperature.ToString("f2");
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

        private void btn_SetUnload_Click(object sender, EventArgs e)
        {
            new FrmStorageTemperature_Confirm("").ShowDialog();

            BindData();
        }

    }
}
