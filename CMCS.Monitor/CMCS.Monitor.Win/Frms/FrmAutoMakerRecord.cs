using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CMCS.Monitor.Win.Core;
using CMCS.Common.Entities;
using CMCS.Common.DAO;
using CMCS.Common;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Metro;
using CMCS.Common.Entities.AutoMaker;

namespace CMCS.Monitor.Win.Frms
{
    public partial class FrmAutoMakerRecord : MetroForm 
    {
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

        /// <summary>
        /// 当前日期
        /// </summary>
        DateTime CurrentDay = DateTime.Now;

        public FrmAutoMakerRecord()
        {
            InitializeComponent();
        }

        private void FrmAutoMakerRecord_Load(object sender, EventArgs e)
        {
            superGridControl1.PrimaryGrid.AutoGenerateColumns = false;
            InitTimeControl();
            btnSearch_Click(null, null);
        }

        public void BindData()
        {
            this.SqlWhere = " where 1=1";

            if (!string.IsNullOrEmpty(txtBarrelCode_Ser.Text))
                this.SqlWhere += " and BarrelCode like '%" + txtBarrelCode_Ser.Text + "%'";

            if (!string.IsNullOrEmpty(dtInputStart.Text))
                this.SqlWhere += " and CreateDate >= '" + dtInputStart.Value + "'";

            if (!string.IsNullOrEmpty(dtInputEnd.Text))
                this.SqlWhere += " and CreateDate <= '" + dtInputEnd.Value + "'";

            List<InfMakerRecord> list = AutoMakerDAO.GetInstance().ExecutePager(PageSize, CurrentIndex, SqlWhere);
            superGridControl1.PrimaryGrid.DataSource = list;

            GetTotalCount(SqlWhere);
            PagerControlStatue();

            lblPagerInfo.Text = string.Format("共 {0} 条记录，每页 {1} 条，共 {2} 页，当前第 {3} 页", TotalCount, PageSize, PageCount, CurrentIndex + 1);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            CurrentIndex = 0;
            BindData();
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            this.SqlWhere = string.Empty;
            txtBarrelCode_Ser.Text = string.Empty;
            dtInputStart.Text = string.Empty;
            dtInputEnd.Text = string.Empty;

            CurrentIndex = 0;
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
            {// 首页
                btnFirst.Enabled = false;
                btnPrevious.Enabled = false;
                btnLast.Enabled = true;
                btnNext.Enabled = true;
            }

            if (CurrentIndex > 0 && CurrentIndex < PageCount - 1)
            {// 上一页/下一页
                btnFirst.Enabled = true;
                btnPrevious.Enabled = true;
                btnLast.Enabled = true;
                btnNext.Enabled = true;
            }

            if (CurrentIndex == PageCount - 1)
            {// 末页
                btnFirst.Enabled = true;
                btnPrevious.Enabled = true;
                btnLast.Enabled = false;
                btnNext.Enabled = false;
            }
        }

        private void GetTotalCount(string sqlWhere)
        {
            TotalCount = AutoMakerDAO.GetInstance().GetTotalCount(sqlWhere);
            if (TotalCount % PageSize != 0)
                PageCount = TotalCount / PageSize + 1;
            else
                PageCount = TotalCount / PageSize;
        }
        #endregion

        #region Last Next Day
        private void btnDay_Click(object sender, EventArgs e)
        {
            ButtonX btn = sender as ButtonX;
            switch (btn.CommandParameter.ToString())
            {
                case "Last":
                    CurrentDay = CurrentDay.AddDays(-1);
                    break;
                case "Next":
                    CurrentDay = CurrentDay.AddDays(1);
                    break;
                case "Today":
                    CurrentDay = DateTime.Now;
                    break;
            }

            InitTimeControl();
            BindData();
        }

        public void InitTimeControl()
        {
            dtInputStart.Value = DateTime.Parse(CurrentDay.ToString("yyyy-MM-dd"));
            dtInputEnd.Value = dtInputStart.Value.AddDays(1).AddSeconds(-1);
        }
        #endregion

        private void superGridControl1_BeginEdit(object sender, DevComponents.DotNetBar.SuperGrid.GridEditEventArgs e)
        {
            //取消编辑
            e.Cancel = true;
        }
    }
}
