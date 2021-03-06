using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Metro;
using CMCS.Common;
using CMCS.Common.Entities.CarTransport;
using DevComponents.DotNetBar.SuperGrid;
using CMCS.Common.Entities;
using CMCS.CarTransport.Queue.Frms.Transport.TransportPicture;
using CMCS.Common.Entities.Fuel;
using CMCS.CarTransport.Queue.Frms.Transport.Print.SaleFuelPrint;
using CMCS.Common.Entities.BaseInfo;
using CMCS.Common.DAO;
using CMCS.Common.Enums;
using CMCS.CarTransport.DAO;
using CMCS.CarTransport.Queue.Core;

namespace CMCS.CarTransport.Queue.Frms.Transport.SaleFuelTransport
{
    public partial class FrmSaleFuelTransport_List : MetroAppForm
    {
        /// <summary>
        /// 窗体唯一标识符
        /// </summary>
        public static string UniqueKey = "FrmSaleFuelTransport_List";


        /// <summary>
        /// 每页显示行数
        /// </summary>
        int PageSize = 18;

        /// <summary>
        /// 总页数
        /// </summary>
        int PageCount = 0;

        /// <summary>
        /// 总记录数
        /// </summary>
        int TotalCount = 0;

        /// <summary>
        /// 当前页索引
        /// </summary>
        int CurrentIndex = 0;

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
                superGridControl1.PrimaryGrid.Columns["clmEdit"].Visible = value;
                superGridControl1.PrimaryGrid.Columns["clmDelete"].Visible = value;
            }
        }

        public FrmSaleFuelTransport_List()
        {
            InitializeComponent();
        }

        private void FrmSaleFuelTransport_List_Load(object sender, EventArgs e)
        {
            superGridControl1.PrimaryGrid.AutoGenerateColumns = false;
            dtpStartTime.Value = DateTime.Now;
            dtpEndTime.Value = DateTime.Now;
            btnSearch_Click(null, null);

            HasManagePower = CommonDAO.GetInstance().HasResourcePowerByResCode(SelfVars.LoginUser.UserAccount, eUserRoleCodes.汽车智能化信息维护.ToString());
        }

        public void BindData()
        {
            string tempSqlWhere = this.SqlWhere;
            object param = new { StartTime = dtpStartTime.Value.Date, EndTime = dtpEndTime.Value.AddDays(1).Date };
            List<CmcsSaleFuelTransport> list = Dbers.GetInstance().SelfDber.ExecutePager<CmcsSaleFuelTransport>(PageSize, CurrentIndex, tempSqlWhere + " order by SerialNumber desc", param);
            superGridControl1.PrimaryGrid.DataSource = list;

            GetTotalCount(tempSqlWhere, param);
            PagerControlStatue();

            lblPagerInfo.Text = string.Format("共 {0} 条记录，每页 {1} 条，共 {2} 页，当前第 {3} 页", TotalCount, PageSize, PageCount, CurrentIndex + 1);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.SqlWhere = " where 1=1";
            if (!string.IsNullOrEmpty(dtpStartTime.Text)) this.SqlWhere += " and InFactoryTime >=:StartTime";
            if (!string.IsNullOrEmpty(dtpEndTime.Text)) this.SqlWhere += " and InFactoryTime<:EndTime";
            if (!string.IsNullOrEmpty(txtCarNumber_Ser.Text)) this.SqlWhere += " and CarNumber like '%" + txtCarNumber_Ser.Text + "%'";

            CurrentIndex = 0;
            BindData();
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            this.SqlWhere = string.Empty;
            txtCarNumber_Ser.Text = string.Empty;

            CurrentIndex = 0;
            BindData();
        }

        private void btnInStore_Click(object sender, EventArgs e)
        {
            FrmSaleFuelTransport_Oper frm = new FrmSaleFuelTransport_Oper();
            frm.ShowDialog();

            BindData();
        }

        #region Pager

        private void btnPagerCommand_Click(object sender, EventArgs e)
        {
            ButtonX btn = sender as ButtonX;
            switch (btn.CommandParameter.ToString())
            {
                case "First":
                    CurrentIndex = 0;
                    break;
                case "Previous":
                    CurrentIndex = CurrentIndex - 1;
                    break;
                case "Next":
                    CurrentIndex = CurrentIndex + 1;
                    break;
                case "Last":
                    CurrentIndex = PageCount - 1;
                    break;
            }

            BindData();
        }

        public void PagerControlStatue()
        {
            if (PageCount <= 1)
            {
                btnFirst.Enabled = false;
                btnPrevious.Enabled = false;
                btnLast.Enabled = false;
                btnNext.Enabled = false;

                return;
            }

            if (CurrentIndex == 0)
            {
                // 首页
                btnFirst.Enabled = false;
                btnPrevious.Enabled = false;
                btnLast.Enabled = true;
                btnNext.Enabled = true;
            }

            if (CurrentIndex > 0 && CurrentIndex < PageCount - 1)
            {
                // 上一页/下一页
                btnFirst.Enabled = true;
                btnPrevious.Enabled = true;
                btnLast.Enabled = true;
                btnNext.Enabled = true;
            }

            if (CurrentIndex == PageCount - 1)
            {
                // 末页
                btnFirst.Enabled = true;
                btnPrevious.Enabled = true;
                btnLast.Enabled = false;
                btnNext.Enabled = false;
            }
        }

        private void GetTotalCount(string sqlWhere, object param)
        {
            TotalCount = Dbers.GetInstance().SelfDber.Count<CmcsSaleFuelTransport>(sqlWhere, param);
            if (TotalCount % PageSize != 0)
                PageCount = TotalCount / PageSize + 1;
            else
                PageCount = TotalCount / PageSize;
        }
        #endregion

        #region DataGridView

        private void superGridControl1_BeginEdit(object sender, DevComponents.DotNetBar.SuperGrid.GridEditEventArgs e)
        {
            // 取消编辑
            e.Cancel = true;
        }

        private void superGridControl1_CellMouseDown(object sender, DevComponents.DotNetBar.SuperGrid.GridCellMouseEventArgs e)
        {
            CmcsSaleFuelTransport entity = Dbers.GetInstance().SelfDber.Get<CmcsSaleFuelTransport>(superGridControl1.PrimaryGrid.GetCell(e.GridCell.GridRow.Index, superGridControl1.PrimaryGrid.Columns["clmId"].ColumnIndex).Value.ToString());
            switch (superGridControl1.PrimaryGrid.Columns[e.GridCell.ColumnIndex].Name)
            {

                case "clmShow":
                    FrmSaleFuelTransport_Oper frmShow = new FrmSaleFuelTransport_Oper(entity.Id, false);
                    if (frmShow.ShowDialog() == DialogResult.OK)
                    {
                        BindData();
                    }
                    break;
                case "clmEdit":
                    FrmSaleFuelTransport_Oper frmEdit = new FrmSaleFuelTransport_Oper(entity.Id, true);
                    if (frmEdit.ShowDialog() == DialogResult.OK)
                    {
                        BindData();
                    }
                    break;
                case "clmDelete":
                    // 查询正在使用该记录的车数 
                    if (entity.GrossWeight > 0 || entity.TareWeight > 0)
                    {
                        MessageBoxEx.Show("该记录已有重量，禁止删除！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    if (MessageBoxEx.Show("确定要删除该记录？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        try
                        {
                            CommonDAO.GetInstance().SaveAppletLog(eAppletLogLevel.Warn, "删除出场煤运输记录：", string.Format("车号:{0};毛重:{1};皮重:{2};矿点:{3};操作人:{4}", entity.CarNumber, entity.GrossWeight, entity.TareWeight, entity.SupplierName, SelfVars.LoginUser.UserName));

                            if (QueuerDAO.GetInstance().DeleteSaleTransport(entity.Id))
                                CommonDAO.GetInstance().InsertWaitForHandleEvent("汽车智能化_删除出厂煤运输记录", entity.Id);
                        }
                        catch (Exception)
                        {
                            MessageBoxEx.Show("该记录正在使用中，禁止删除！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }

                        BindData();
                    }
                    break;
                case "clmPic":

                    if (Dbers.GetInstance().SelfDber.Entities<CmcsTransportPicture>(String.Format(" where TransportId='{0}'", entity.Id)).Count > 0)
                    {
                        FrmTransportPicture frmPic = new FrmTransportPicture(entity.Id, entity.CarNumber);
                        if (frmPic.ShowDialog() == DialogResult.OK)
                        {
                            BindData();
                        }
                    }
                    else
                    {
                        MessageBoxEx.Show("暂无抓拍图片！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                    break;
            }
        }

        private void superGridControl1_DataBindingComplete(object sender, DevComponents.DotNetBar.SuperGrid.GridDataBindingCompleteEventArgs e)
        {
            foreach (GridRow gridRow in e.GridPanel.Rows)
            {
                CmcsSaleFuelTransport cmcsSaleFuelTransport = gridRow.DataItem as CmcsSaleFuelTransport;
                if (cmcsSaleFuelTransport == null)
                {
                    break;
                }
                gridRow.Cells["clmIsUse"].Value = ((cmcsSaleFuelTransport.IsUse == 1) ? "是" : "否");

                gridRow.Cells["SupplierName"].Value = cmcsSaleFuelTransport.TheSupplier != null ? cmcsSaleFuelTransport.TheSupplier.Name : "";
                CmcsTransportCompany cmcsTransportCompany = Dbers.GetInstance().SelfDber.Get<CmcsTransportCompany>(cmcsSaleFuelTransport.TransportCompanyId);
                if (cmcsTransportCompany != null)
                {
                    gridRow.Cells["TransportCompanyName"].Value = cmcsTransportCompany.Name;
                }
                if (cmcsSaleFuelTransport.GrossWeight > 0 || cmcsSaleFuelTransport.TareWeight > 0)
                    gridRow.Cells["clmDelete"].Value = "";
                if (cmcsSaleFuelTransport.GrossWeight > 0m && cmcsSaleFuelTransport.TareWeight > 0m)
                {
                    gridRow.CellStyles.Default.TextColor = System.Drawing.Color.Green;
                }
                else if (cmcsSaleFuelTransport.GrossWeight == 0m && cmcsSaleFuelTransport.TareWeight == 0m)
                {
                    gridRow.CellStyles.Default.TextColor = System.Drawing.Color.Red;
                }
            }
        }

        #endregion

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            FrmSaleFuelTransport_Oper frmEdit = new FrmSaleFuelTransport_Oper(String.Empty, true);
            if (frmEdit.ShowDialog() == DialogResult.OK)
            {
                BindData();
            }
        }

        private void tsmiPrint_Click(object sender, EventArgs e)
        {
            GridRow gridRow = superGridControl1.PrimaryGrid.ActiveRow as GridRow;
            if (gridRow == null) return;
            CmcsSaleFuelTransport entity = gridRow.DataItem as CmcsSaleFuelTransport;
            FrmSaleFuelPrintWeb frm = new FrmSaleFuelPrintWeb(entity);
            frm.ShowDialog();
        }
    }
}
