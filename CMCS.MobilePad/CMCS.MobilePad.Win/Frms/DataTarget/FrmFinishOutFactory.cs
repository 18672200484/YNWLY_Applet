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
    public partial class FrmFinishOutFactory : MetroAppForm
    {
        /// <summary>
        /// ����Ψһ��ʶ��
        /// </summary>
        public static string UniqueKey = "FrmFinishOutFactory";
        string OutFactoryType;
        string FlowStatus;
        string SqlWhere = string.Empty;

        bool hasManagePower = false;
        /// <summary>
        /// �Է���ά��Ȩ��
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

        public FrmFinishOutFactory(string status, string type)
        {
            InitializeComponent();
            OutFactoryType = type;
            FlowStatus = status;
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
            List<CmcsSaleFuelTransport> list = Dbers.GetInstance().SelfDber.Entities<CmcsSaleFuelTransport>(this.SqlWhere + " order by CreateDate desc");

            superGridControl1.PrimaryGrid.DataSource = list;
            if ((ddlStatus.Text == eFlowStatus.�س�.ToString() || ddlStatus.Text == eFlowStatus.����.ToString()) && list.Count > 0)
            {
                CmcsSaleFuelTransport total = new CmcsSaleFuelTransport();
                total.CarNumber = "�ϼ�";
                total.GrossWeight = list.Sum(a => a.GrossWeight);
                total.TareWeight = list.Sum(a => a.TareWeight);
                total.SuttleWeight = list.Sum(a => a.SuttleWeight);
                list.Insert(list.Count, total);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.SqlWhere = "where 1=1 ";

            if (!String.IsNullOrWhiteSpace(ddlStatus.Text))
            {
                if (ddlStatus.Text == eFlowStatus.δ���.ToString())
                    SqlWhere += " and Trunc(InFactoryTime)=Trunc(Sysdate) and SuttleWeight=0";

                else if (ddlStatus.Text == eFlowStatus.�볡.ToString())
                    SqlWhere += " and Trunc(InFactoryTime)=Trunc(Sysdate)";

                else if (ddlStatus.Text == eFlowStatus.�س�.ToString())
                    SqlWhere += " and Trunc(GrossTime)=Trunc(Sysdate)";

                else if (ddlStatus.Text == eFlowStatus.����.ToString())
                    SqlWhere += " and Trunc(SamplingTime)=Trunc(Sysdate)";

                else if (ddlStatus.Text == eFlowStatus.װ��.ToString())
                    SqlWhere += " and Trunc(LoadTime)=Trunc(Sysdate)";

                else if (ddlStatus.Text == eFlowStatus.�ᳵ.ToString())
                    SqlWhere += " and Trunc(TareTime)=Trunc(Sysdate)";

                else if (ddlStatus.Text == eFlowStatus.����.ToString())
                    SqlWhere += " and Trunc(OutFactoryTime)=Trunc(Sysdate)";
            }

            if (!String.IsNullOrWhiteSpace(OutFactoryType))
            {
                SqlWhere += " and OutFactoryType like '%" + OutFactoryType + "%' ";
            }
            if (!String.IsNullOrWhiteSpace(txtInput.Text))
            {
                SqlWhere += " and (CarNumber like '%" + txtInput.Text + "%' ";

                SqlWhere += " or SupplierName like '%" + txtInput.Text + "%') ";
            }
            BindData();
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            this.SqlWhere = "where 1=1 ";
            txtInput.Text = "";
            btnSearch_Click(null, null);
        }

        private void LoadStatus()
        {
            ddlStatus.Items.Add(eFlowStatus.δ���.ToString());
            ddlStatus.Items.Add(eFlowStatus.�볡.ToString());
            ddlStatus.Items.Add(eFlowStatus.�ᳵ.ToString());
            ddlStatus.Items.Add(eFlowStatus.װ��.ToString());
            ddlStatus.Items.Add(eFlowStatus.�س�.ToString());
            ddlStatus.Items.Add(eFlowStatus.����.ToString());
            ddlStatus.Items.Add(eFlowStatus.����.ToString());
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
                case eFlowStatus.δ���:
                    superGridControl1.PrimaryGrid.Columns["clmInFactoryTime"].Visible = false;
                    superGridControl1.PrimaryGrid.Columns["clmGrossTime"].Visible = false;
                    superGridControl1.PrimaryGrid.Columns["clmGrossWeight"].Visible = false;
                    superGridControl1.PrimaryGrid.Columns["clmTareTime"].Visible = false;
                    superGridControl1.PrimaryGrid.Columns["clmTareWeight"].Visible = false;
                    superGridControl1.PrimaryGrid.Columns["clmSamplingTime"].Visible = false;
                    superGridControl1.PrimaryGrid.Columns["clmLoadTime"].Visible = false;
                    superGridControl1.PrimaryGrid.Columns["clmOutFactoryTime"].Visible = false;
                    superGridControl1.PrimaryGrid.Columns["clmCheckWeight"].Visible = false;
                    break;
                case eFlowStatus.�볡:
                    superGridControl1.PrimaryGrid.Columns["clmGrossTime"].Visible = false;
                    superGridControl1.PrimaryGrid.Columns["clmGrossWeight"].Visible = false;
                    superGridControl1.PrimaryGrid.Columns["clmTareTime"].Visible = false;
                    superGridControl1.PrimaryGrid.Columns["clmTareWeight"].Visible = false;
                    superGridControl1.PrimaryGrid.Columns["clmSamplingTime"].Visible = false;
                    superGridControl1.PrimaryGrid.Columns["clmLoadTime"].Visible = false;
                    superGridControl1.PrimaryGrid.Columns["clmOutFactoryTime"].Visible = false;
                    superGridControl1.PrimaryGrid.Columns["clmCheckWeight"].Visible = false;
                    break;
                case eFlowStatus.�ᳵ:
                    superGridControl1.PrimaryGrid.Columns["clmInFactoryTime"].Visible = false;
                    superGridControl1.PrimaryGrid.Columns["clmGrossWeight"].Visible = false;
                    superGridControl1.PrimaryGrid.Columns["clmGrossTime"].Visible = false;
                    superGridControl1.PrimaryGrid.Columns["clmSamplingTime"].Visible = false;
                    superGridControl1.PrimaryGrid.Columns["clmLoadTime"].Visible = false;
                    superGridControl1.PrimaryGrid.Columns["clmOutFactoryTime"].Visible = false;
                    superGridControl1.PrimaryGrid.Columns["clmCheckWeight"].Visible = false;
                    break;
                case eFlowStatus.װ��:
                    superGridControl1.PrimaryGrid.Columns["clmGrossTime"].Visible = false;
                    superGridControl1.PrimaryGrid.Columns["clmTareTime"].Visible = false;
                    superGridControl1.PrimaryGrid.Columns["clmTareWeight"].Visible = false;
                    superGridControl1.PrimaryGrid.Columns["clmInFactoryTime"].Visible = false;
                    superGridControl1.PrimaryGrid.Columns["clmOutFactoryTime"].Visible = false;
                    superGridControl1.PrimaryGrid.Columns["clmCheckWeight"].Visible = false;
                    break;
                case eFlowStatus.�س�:
                    superGridControl1.PrimaryGrid.Columns["clmTareTime"].Visible = false;
                    superGridControl1.PrimaryGrid.Columns["clmTareWeight"].Visible = false;
                    superGridControl1.PrimaryGrid.Columns["clmSamplingTime"].Visible = false;
                    superGridControl1.PrimaryGrid.Columns["clmLoadTime"].Visible = false;
                    superGridControl1.PrimaryGrid.Columns["clmOutFactoryTime"].Visible = false;
                    superGridControl1.PrimaryGrid.Columns["clmCheckWeight"].Visible = false;
                    break;
                case eFlowStatus.����:
                    superGridControl1.PrimaryGrid.Columns["clmInFactoryTime"].Visible = false;
                    superGridControl1.PrimaryGrid.Columns["clmTareTime"].Visible = false;
                    superGridControl1.PrimaryGrid.Columns["clmTareWeight"].Visible = false;
                    superGridControl1.PrimaryGrid.Columns["clmGrossWeight"].Visible = false;
                    superGridControl1.PrimaryGrid.Columns["clmOutFactoryTime"].Visible = false;
                    superGridControl1.PrimaryGrid.Columns["clmCheckWeight"].Visible = false;
                    superGridControl1.PrimaryGrid.Columns["clmLoadTime"].Visible = false;
                    break;
                case eFlowStatus.����:
                    superGridControl1.PrimaryGrid.Columns["clmInFactoryTime"].Visible = false;
                    superGridControl1.PrimaryGrid.Columns["clmGrossTime"].Visible = false;
                    superGridControl1.PrimaryGrid.Columns["clmTareTime"].Visible = false;
                    superGridControl1.PrimaryGrid.Columns["clmSamplingTime"].Visible = false;
                    superGridControl1.PrimaryGrid.Columns["clmLoadTime"].Visible = false;
                    break;
                default:
                    break;
            }
        }

        #region GridView

        private void superGridControl1_BeginEdit(object sender, DevComponents.DotNetBar.SuperGrid.GridEditEventArgs e)
        {
            // ȡ���༭
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
