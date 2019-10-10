using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Metro;
using CMCS.Common;
using CMCS.Common.Entities.CarTransport;
using DevComponents.DotNetBar.SuperGrid;
using CMCS.Common.Entities.Fuel;
using CMCS.Common.Entities.BaseInfo;
using CMCS.Common.DAO;
using CMCS.Common.Enums;
using CMCS.WeighCheck.SampleMake.Utilities;

namespace CMCS.WeighCheck.SampleMake.Frms.JDYMake
{
    public partial class FrmJDYMake_List : MetroAppForm
    {
        /// <summary>
        /// 窗体唯一标识符
        /// </summary>
        public static string UniqueKey = "FrmJDYMake_List";


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

        string SqlWhere = "where MakeType='监督样制样' ";

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

                superGridControl1.PrimaryGrid.Columns["clmDelete"].Visible = value;
            }
        }

        public FrmJDYMake_List()
        {
            InitializeComponent();
        }

        private void FrmJDYMake_List_Load(object sender, EventArgs e)
        {
            superGridControl1.PrimaryGrid.AutoGenerateColumns = false;
            dt_StartTime.Value = DateTime.Now;
            dt_EndTime.Value = DateTime.Now;

            btnSearch_Click(null, null);
        }

        public void BindData()
        {
            string tempSqlWhere = this.SqlWhere;
            object param = new { StartTime = dt_StartTime.Value.Date, EndTime = dt_EndTime.Value.AddDays(1).Date };
            List<CmcsRCMake> list = Dbers.GetInstance().SelfDber.ExecutePager<CmcsRCMake>(PageSize, CurrentIndex, tempSqlWhere + " order by CreateDate desc", param);
            superGridControl1.PrimaryGrid.DataSource = list;

            GetTotalCount(tempSqlWhere, param);
            PagerControlStatue();

            lblPagerInfo.Text = string.Format("共 {0} 条记录，每页 {1} 条，共 {2} 页，当前第 {3} 页", TotalCount, PageSize, PageCount, CurrentIndex + 1);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.SqlWhere = "where MakeType='监督样制样' ";

            if (!string.IsNullOrEmpty(txt_SearchSendUnitName.Text)) this.SqlWhere += " and SendUnit like '%" + txt_SearchSendUnitName.Text + "%'";
            if (!string.IsNullOrEmpty(txt_SearchMakeCode.Text)) this.SqlWhere += " and MakeCode like '%" + txt_SearchMakeCode.Text + "%'";
            if (!string.IsNullOrEmpty(dt_StartTime.Text)) this.SqlWhere += " and MakeStartTime >=:StartTime";
            if (!string.IsNullOrEmpty(dt_EndTime.Text)) this.SqlWhere += " and MakeStartTime < :EndTime";

            CurrentIndex = 0;
            BindData();
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            this.SqlWhere = "where MakeType='监督样制样' ";
            txt_SearchSendUnitName.Text = string.Empty;
            txt_SearchMakeCode.Text = string.Empty;
            dt_StartTime.Text = string.Empty;
            dt_EndTime.Text = string.Empty;

            CurrentIndex = 0;
            BindData();
        }

        private void btnInStore_Click(object sender, EventArgs e)
        {
            FrmJDYMake_Oper frm = new FrmJDYMake_Oper();
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
            TotalCount = Dbers.GetInstance().SelfDber.Count<CmcsRCMake>(sqlWhere, param);
            if (TotalCount % PageSize != 0)
                PageCount = TotalCount / PageSize + 1;
            else
                PageCount = TotalCount / PageSize;
        }
        #endregion

        private void superGridControl1_BeginEdit(object sender, DevComponents.DotNetBar.SuperGrid.GridEditEventArgs e)
        {
            // 取消编辑
            e.Cancel = true;
        }

        private void superGridControl1_CellMouseDown(object sender, DevComponents.DotNetBar.SuperGrid.GridCellMouseEventArgs e)
        {
            CmcsRCMake entity = Dbers.GetInstance().SelfDber.Get<CmcsRCMake>(superGridControl1.PrimaryGrid.GetCell(e.GridCell.GridRow.Index, superGridControl1.PrimaryGrid.Columns["clmId"].ColumnIndex).Value.ToString());
            switch (superGridControl1.PrimaryGrid.Columns[e.GridCell.ColumnIndex].Name)
            {
                case "clmShow":
                    FrmJDYMake_Oper frmShow = new FrmJDYMake_Oper(entity.Id, false);
                    if (frmShow.ShowDialog() == DialogResult.OK)
                    {
                        BindData();
                    }
                    break;
                case "clmEdit":
                    FrmJDYMake_Oper frmEdit = new FrmJDYMake_Oper(entity.Id, true);
                    if (frmEdit.ShowDialog() == DialogResult.OK)
                    {
                        BindData();
                    }
                    break;
                case "clmDelete":
                    if (MessageBoxEx.Show("确定要删除该记录？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        try
                        {
                            CommonDAO.GetInstance().SaveAppletLog(eAppletLogLevel.Warn, "删除监督样记录：", string.Format("送样单位:{0};接样人:{1};制样人:{2};制样码:{3};操作人:{4}", entity.SendUnit, entity.GetPle, entity.MakePle, entity.MakeCode, SelfVars.LoginUser.UserName));
                            Dbers.GetInstance().SelfDber.DeleteBySQL<CmcsRCMakeDetail>("where MakeId=:MakeId", new { MakeId = entity.Id });
                            Dbers.GetInstance().SelfDber.Delete<CmcsRCMake>(entity.Id);
                        }
                        catch (Exception)
                        {
                            MessageBoxEx.Show("该记录正在使用中，禁止删除！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }

                        BindData();
                    }
                    break;
            }
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            FrmJDYMake_Oper frmEdit = new FrmJDYMake_Oper(String.Empty, true);
            if (frmEdit.ShowDialog() == DialogResult.OK)
            {
                BindData();
            }
        }
    }
}
