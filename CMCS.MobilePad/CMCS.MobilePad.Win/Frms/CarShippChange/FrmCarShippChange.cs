using System;
using System.Windows.Forms;
//
using DevComponents.DotNetBar.Metro;
using System.Collections.Generic;
using CMCS.Common.Entities.CarTransport;
using CMCS.Common;
using DevComponents.DotNetBar.SuperGrid;
using DevComponents.DotNetBar;
using CMCS.Common.Entities.Fuel;

namespace CMCS.MobilePad.Win.Frms.CarShippChange
{
    public partial class FrmCarShippChange : MetroAppForm
    {
        /// <summary>
        /// 窗体唯一标识符
        /// </summary>
        public static string UniqueKey = "FrmCarShippChange";

        public List<FulUnLoadPlanDetail> CurrentList;
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

        public FrmCarShippChange()
        {
            InitializeComponent();
        }

        private void FrmHome_Load(object sender, EventArgs e)
        {
            LoadUnLoadStatus();
            btnSearch_Click(null, null);
            this.CurrentList = new List<FulUnLoadPlanDetail>();
            InitFrom();


            this.superGridControl1.PrimaryGrid.DefaultRowHeight = 30;
            this.superGridControl1.PrimaryGrid.DefaultVisualStyles.CellStyles.Default.Font = new System.Drawing.Font("微软雅黑", 14.25F);
            this.superGridControl1.PrimaryGrid.DefaultVisualStyles.ColumnHeaderStyles.Default.Font = new System.Drawing.Font("微软雅黑", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
       
        }

        public void InitFrom()
        {
            GridButtonXEditControl btnPrintCode = superGridControl1.PrimaryGrid.Columns["operation"].EditControl as GridButtonXEditControl;
            btnPrintCode.ColorTable = eButtonColor.BlueWithBackground;
            btnPrintCode.Click += new EventHandler(btnCarDeduction_Click);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnCarDeduction_Click(object sender, EventArgs e)
        {
            GridButtonXEditControl btn = sender as GridButtonXEditControl;
            if (btn == null) return;

            CarShippChange entity = btn.EditorCell.GridRow.DataItem as CarShippChange;
            if (entity == null) return;
            FrmCarShippChange_Confirm frm = new FrmCarShippChange_Confirm(entity.Id, entity.Id,entity.lmybid);
            frm.ShowDialog();
            if (frm.DialogResult == DialogResult.OK)
                BindData();
        }

        public void BindData()
        {
            string tempSqlWhere = this.SqlWhere;

            List<CarShippChange> list = Dbers.GetInstance().SelfDber.Query<CarShippChange>(tempSqlWhere + " order by infactorytime desc").AsList();

            superGridControl1.PrimaryGrid.DataSource = list;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.SqlWhere = @"select * from (select c.id,c.carnumber,c.infactorytime,c.cpcname,c.cpcid,c.storageid,c.storagename,
                                nvl(c.cpcname,c.storagename) qmwz,outweight,c.lmybid,
                                case when c.outweight>0 then '已装车' else '未装车' end ZCStatus,d.driver,d.cellphonenumber PhoneNumber
                                from cmcstbsalefueltransport c 
                                inner join cmcstbautotruck d on c.carnumber=d.carnumber
                                left join fultbinfactorybatch e on c.inoutbatchid = e.id
                                where trunc(c.infactorytime)=trunc(sysdate)) where 1=1    ";

            if (!String.IsNullOrEmpty(this.cmbZC.Text))
            {
                if (this.cmbZC.Text == "未装车")
                    SqlWhere += " and outweight = 0";
                else
                    SqlWhere += " and outweight >0";
            }

            if (!String.IsNullOrEmpty((String)txtInput.Text))
            {
                SqlWhere += " and CarNumber like '%" + txtInput.Text + "%' ";
            }

            BindData();
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            txtInput.Text = "";
            cmbZC.Text = "";
            btnSearch_Click(null, null);
        }

        private void btn_SetUnload_Click(object sender, EventArgs e)
        {
            if (this.CurrentList.Count == 0)
            {
                MessageBoxEx.Show("请选择数据！", "提示", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Stop);
                return;
            }
            //new FrmCarShippChange_Confirm(this.CurrentList).ShowDialog();
        }

        private void LoadUnLoadStatus()
        {
            this.cmbZC.Items.Add(new ComboBoxItem("", ""));
            this.cmbZC.Items.Add(new ComboBoxItem("0", "未装车"));
            this.cmbZC.Items.Add(new ComboBoxItem("1", "已装车"));
            this.cmbZC.SelectedIndex = 0;
        }

        #region GridView
        private void superGridControl1_DataBindingComplete(object sender, DevComponents.DotNetBar.SuperGrid.GridDataBindingCompleteEventArgs e)
        {
            foreach (GridRow item in e.GridPanel.Rows)
            {
                try
                {

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
        #endregion

    }

    class CarShippChange
    {
        public string Id { get; set; }

        public string CarNumber { get; set; }

        public string InFactoryTime { get; set; }

        public string StorageName { get; set; }

        public string StorageRolad { get; set; }

        public string Driver { get; set; }

        public string PhoneNumber { get; set; }

        public string ZCStatus { get; set; }

        public string SalesOrderId { get; set; }

        public string qmwz { get; set; }

        public string lmybid { get; set; }
        
    }
}
