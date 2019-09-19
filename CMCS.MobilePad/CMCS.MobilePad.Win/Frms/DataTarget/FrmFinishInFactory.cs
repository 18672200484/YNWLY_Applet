using System;
using DevComponents.DotNetBar.Metro;
using System.Collections.Generic;
using System.Linq;
//
using CMCS.Common.Entities.CarTransport;
using CMCS.Common;
using DevComponents.DotNetBar.SuperGrid;
using DevComponents.DotNetBar;
using CMCS.Common.Entities.Fuel;

namespace CMCS.MobilePad.Win.Frms.DataTarget
{
    public partial class FrmFinishInFactory : MetroAppForm
    {
        /// <summary>
        /// 窗体唯一标识符
        /// </summary>
        public static string UniqueKey = "FrmFinishInFactory";
        string InFactoryType;
        string FlowStatus;
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

        public FrmFinishInFactory(string status, string type)
        {
            InitializeComponent();
            FlowStatus = status;
            InFactoryType = type;
        }

        private void FrmHome_Load(object sender, EventArgs e)
        {
            InitFrom();
            btnSearch_Click(null, null);
        }

        public void InitFrom()
        {
            //superGridControl1.PrimaryGrid.ColumnAutoSizeMode = ColumnAutoSizeMode.AllCells;
            LoadStatus();
        }

        public void BindData()
        {
            ChangeColumn();
            List<CmcsBuyFuelTransport> list = Dbers.GetInstance().SelfDber.Entities<CmcsBuyFuelTransport>(this.SqlWhere + " order by CreateDate desc");
            if (ddlStatus.Text == eFlowStatus.出场.ToString() && list.Count > 0)
            {
                CmcsBuyFuelTransport total = new CmcsBuyFuelTransport();
                total.CarNumber = "合计";
                total.GrossWeight = list.Sum(a => a.GrossWeight);
                total.TareWeight = list.Sum(a => a.TareWeight);
                total.CheckWeight = list.Sum(a => a.CheckWeight);
                total.KgWeight = list.Sum(a => a.KgWeight);
                total.KsWeight = list.Sum(a => a.KsWeight);
                list.Insert(list.Count, total);
            }
            superGridControl1.PrimaryGrid.DataSource = list;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.SqlWhere = "where 1=1 ";
            if (!string.IsNullOrWhiteSpace(InFactoryType))
            {
                SqlWhere += " and InFactoryType='" + InFactoryType + "'";
            }

            if (!String.IsNullOrWhiteSpace(ddlStatus.Text))
            {
                if (ddlStatus.Text == eFlowStatus.未完成.ToString())
                    SqlWhere += " and Trunc(InFactoryTime)=Trunc(Sysdate) and SuttleWeight=0";

                else if (ddlStatus.Text == eFlowStatus.入场.ToString())
                    SqlWhere += " and Trunc(InFactoryTime)=Trunc(Sysdate)";

                else if (ddlStatus.Text == eFlowStatus.重车.ToString())
                    SqlWhere += " and Trunc(GrossTime)=Trunc(Sysdate)";

                else if (ddlStatus.Text == eFlowStatus.采样.ToString())
                    SqlWhere += " and Trunc(SamplingTime)=Trunc(Sysdate)";

                else if (ddlStatus.Text == eFlowStatus.卸煤.ToString())
                    SqlWhere += " and Trunc(UploadTime)=Trunc(Sysdate)";

                else if (ddlStatus.Text == eFlowStatus.轻车.ToString())
                    SqlWhere += " and Trunc(TareTime)=Trunc(Sysdate)";

                else if (ddlStatus.Text == eFlowStatus.出场.ToString())
                    SqlWhere += " and Trunc(OutFactoryTime)=Trunc(Sysdate)";
            }

            if (!String.IsNullOrWhiteSpace(txtInput.Text))
            {
                SqlWhere += " and ( CarNumber like '%" + txtInput.Text + "%' ";

                SqlWhere += " or SupplierName like '%" + txtInput.Text + "%' ";

                SqlWhere += " or MineName like '%" + txtInput.Text + "%') ";
            }

            BindData();
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            this.SqlWhere = "where 1=1";
            txtInput.Text = "";
            //ddlStatus.SelectedIndex = 0;
            btnSearch_Click(null, null);
        }

        private void LoadStatus()
        {
            ddlStatus.Items.Add(eFlowStatus.未完成.ToString());
            ddlStatus.Items.Add(eFlowStatus.入场.ToString());
            ddlStatus.Items.Add(eFlowStatus.重车.ToString());
            ddlStatus.Items.Add(eFlowStatus.采样.ToString());
            ddlStatus.Items.Add(eFlowStatus.卸煤.ToString());
            ddlStatus.Items.Add(eFlowStatus.轻车.ToString());
            ddlStatus.Items.Add(eFlowStatus.出场.ToString());
            ddlStatus.Text = FlowStatus;
        }

        private void ChangeColumn()
        {
            foreach (GridColumn item in superGridControl1.PrimaryGrid.Columns)
            {
                item.Visible = true;
            }
            eFlowStatus enumType;
            Enum.TryParse<eFlowStatus>(ddlStatus.Text, out enumType);
            switch (enumType)
            {
                case eFlowStatus.未完成:
                    superGridControl1.PrimaryGrid.Columns["clmInFactoryTime"].Visible = false;
                    superGridControl1.PrimaryGrid.Columns["clmGrossTime"].Visible = false;
                    superGridControl1.PrimaryGrid.Columns["clmGrossWeight"].Visible = false;
                    superGridControl1.PrimaryGrid.Columns["clmTareTime"].Visible = false;
                    superGridControl1.PrimaryGrid.Columns["clmTareWeight"].Visible = false;
                    superGridControl1.PrimaryGrid.Columns["clmSamplingTime"].Visible = false;
                    superGridControl1.PrimaryGrid.Columns["clmOutFactoryTime"].Visible = false;
                    superGridControl1.PrimaryGrid.Columns["clmCheckWeight"].Visible = false;
                    superGridControl1.PrimaryGrid.Columns["clmKgWeight"].Visible = false;
                    superGridControl1.PrimaryGrid.Columns["clmKsWeight"].Visible = false;
                    break;
                case eFlowStatus.入场:
                    superGridControl1.PrimaryGrid.Columns["clmGrossTime"].Visible = false;
                    superGridControl1.PrimaryGrid.Columns["clmGrossWeight"].Visible = false;
                    superGridControl1.PrimaryGrid.Columns["clmTareTime"].Visible = false;
                    superGridControl1.PrimaryGrid.Columns["clmTareWeight"].Visible = false;
                    superGridControl1.PrimaryGrid.Columns["clmSamplingTime"].Visible = false;
                    superGridControl1.PrimaryGrid.Columns["clmOutFactoryTime"].Visible = false;
                    superGridControl1.PrimaryGrid.Columns["clmCheckWeight"].Visible = false;
                    superGridControl1.PrimaryGrid.Columns["clmKgWeight"].Visible = false;
                    superGridControl1.PrimaryGrid.Columns["clmKsWeight"].Visible = false;
                    break;
                case eFlowStatus.重车:
                    superGridControl1.PrimaryGrid.Columns["clmTareTime"].Visible = false;
                    superGridControl1.PrimaryGrid.Columns["clmTareWeight"].Visible = false;
                    superGridControl1.PrimaryGrid.Columns["clmSamplingTime"].Visible = false;
                    superGridControl1.PrimaryGrid.Columns["clmOutFactoryTime"].Visible = false;
                    superGridControl1.PrimaryGrid.Columns["clmCheckWeight"].Visible = false;
                    superGridControl1.PrimaryGrid.Columns["clmKgWeight"].Visible = false;
                    superGridControl1.PrimaryGrid.Columns["clmKsWeight"].Visible = false;
                    break;
                case eFlowStatus.采样:
                    superGridControl1.PrimaryGrid.Columns["clmInFactoryTime"].Visible = false;
                    superGridControl1.PrimaryGrid.Columns["clmTareTime"].Visible = false;
                    superGridControl1.PrimaryGrid.Columns["clmTareWeight"].Visible = false;
                    superGridControl1.PrimaryGrid.Columns["clmGrossWeight"].Visible = false;
                    superGridControl1.PrimaryGrid.Columns["clmOutFactoryTime"].Visible = false;
                    superGridControl1.PrimaryGrid.Columns["clmCheckWeight"].Visible = false;
                    superGridControl1.PrimaryGrid.Columns["clmKgWeight"].Visible = false;
                    superGridControl1.PrimaryGrid.Columns["clmKsWeight"].Visible = false;
                    break;
                case eFlowStatus.卸煤:
                    superGridControl1.PrimaryGrid.Columns["clmTareTime"].Visible = false;
                    superGridControl1.PrimaryGrid.Columns["clmTareWeight"].Visible = false;
                    superGridControl1.PrimaryGrid.Columns["clmOutFactoryTime"].Visible = false;
                    superGridControl1.PrimaryGrid.Columns["clmOutFactoryTime"].Visible = false;
                    superGridControl1.PrimaryGrid.Columns["clmCheckWeight"].Visible = false;
                    superGridControl1.PrimaryGrid.Columns["clmKgWeight"].Visible = false;
                    superGridControl1.PrimaryGrid.Columns["clmKsWeight"].Visible = false;
                    break;
                case eFlowStatus.装车:
                    break;
                case eFlowStatus.轻车:
                    superGridControl1.PrimaryGrid.Columns["clmInFactoryTime"].Visible = false;
                    superGridControl1.PrimaryGrid.Columns["clmGrossWeight"].Visible = false;
                    superGridControl1.PrimaryGrid.Columns["clmGrossTime"].Visible = false;
                    superGridControl1.PrimaryGrid.Columns["clmSamplingTime"].Visible = false;
                    superGridControl1.PrimaryGrid.Columns["clmOutFactoryTime"].Visible = false;
                    superGridControl1.PrimaryGrid.Columns["clmCheckWeight"].Visible = false;
                    superGridControl1.PrimaryGrid.Columns["clmKgWeight"].Visible = false;
                    superGridControl1.PrimaryGrid.Columns["clmKsWeight"].Visible = false;
                    break;
                case eFlowStatus.出场:
                    superGridControl1.PrimaryGrid.Columns["clmInFactoryTime"].Visible = false;
                    superGridControl1.PrimaryGrid.Columns["clmGrossTime"].Visible = false;
                    superGridControl1.PrimaryGrid.Columns["clmTareTime"].Visible = false;
                    superGridControl1.PrimaryGrid.Columns["clmSamplingTime"].Visible = false;
                    break;
                default:
                    break;
            }
        }

        #region GridView

        private void superGridControl1_BeginEdit(object sender, DevComponents.DotNetBar.SuperGrid.GridEditEventArgs e)
        {
            // 取消编辑
            e.Cancel = true;
        }

        private void superGridControl1_DataBindingComplete(object sender, DevComponents.DotNetBar.SuperGrid.GridDataBindingCompleteEventArgs e)
        {
            foreach (GridRow item in e.GridPanel.Rows)
            {
                try
                {
                    CmcsBuyFuelTransport entity = item.DataItem as CmcsBuyFuelTransport;

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
}
