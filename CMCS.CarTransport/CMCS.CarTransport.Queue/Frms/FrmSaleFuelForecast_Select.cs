using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using CMCS.Common;
using DevComponents.DotNetBar.SuperGrid;
using CMCS.Common.Entities;
using CMCS.CarTransport.DAO;
using CMCS.Common.DAO;
using CMCS.Common.Entities.CarTransport;
using CMCS.Common.Entities.Fuel;
using CMCS.Common.Entities.Sys;
using CMCS.Common.Enums;
using CMCS.Common.Utilities;


namespace CMCS.CarTransport.Queue.Frms
{
    /// <summary>
    /// 销售煤预报选择
    /// </summary>
    public partial class FrmSaleFuelForecast_Select : DevComponents.DotNetBar.Metro.MetroForm
    {
        /// <summary>
        /// 选中的实体
        /// </summary>
        public CmcsLMYB Output;

        /// <summary>
        /// 车辆Id
        /// </summary>
        private string CurrentAutotruckId;

        private string strWhere;

        private DateTime dtCurrent;
        /// <summary>
        /// 预计到厂时间
        /// </summary>
        public DateTime DtCurrent
        {
            get { return dtCurrent; }
            set
            {
                dtCurrent = value;

                Search(value);
            }
        }

        private List<CmcsLMYB> inLYMBS;
        /// <summary>
        /// 多来煤预报选择
        /// </summary>
        public List<CmcsLMYB> InLYMBS
        {
            get { return inLYMBS; }
            set { inLYMBS = value; }
        }

        public FrmSaleFuelForecast_Select(string strWhere = " where TransportTypeName='汽车' ")
        {
            InitializeComponent();
            this.strWhere = strWhere;
            //timer1.Enabled = true;
        }

        private void FrmSaleFuelForecast_Select_Shown(object sender, EventArgs e)
        {
            this.DtCurrent = DateTime.Now;
        }

        private void FrmSaleFuelForecast_Select_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                this.Output = null;
                this.DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        void Search(DateTime inFactoryTime)
        {
            if (this.InLYMBS != null && this.InLYMBS.Count > 0)
            {
                btnToday.Visible = false;
                btnPrevDay.Visible = false;
                btnNextDay.Visible = false;
                superGridControl1.PrimaryGrid.DataSource = this.InLYMBS;
            }
            else
            {
                List<CmcsLMYB> list = CommonDAO.GetInstance().SelfDber.Entities<CmcsLMYB>(strWhere + " and TransportTypeName='汽车' and trunc(ZcDate)=trunc(:ZcDate) and ischeck=1 order by ZcDate desc", new { ZcDate = inFactoryTime });
                superGridControl1.PrimaryGrid.DataSource = list;
            }
        }

        void Return()
        {
            GridRow gridRow = superGridControl1.PrimaryGrid.ActiveRow as GridRow;
            if (gridRow == null) return;

            this.Output = (gridRow.DataItem as CmcsLMYB);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void superGridControl1_BeginEdit(object sender, DevComponents.DotNetBar.SuperGrid.GridEditEventArgs e)
        {
            // 取消编辑模式
            e.Cancel = true;
        }

        /// <summary>
        /// 设置行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void superGridControl_GetRowHeaderText(object sender, DevComponents.DotNetBar.SuperGrid.GridGetRowHeaderTextEventArgs e)
        {
            e.Text = (e.GridRow.RowIndex + 1).ToString();
        }

        private void superGridControl_DataBindingComplete(object sender, GridDataBindingCompleteEventArgs e)
        {
            foreach (GridRow gridRow in e.GridPanel.Rows)
            {
                try
                {
                    CmcsLMYB entity = gridRow.DataItem as CmcsLMYB;
                    if (entity == null) return;

                    // 填充有效状态
                    gridRow.Cells["clmTransportCompanyName"].Value = entity.TheTransportCompany.Name;
                }
                catch (Exception ex)
                {
                    Log4Neter.Error("出场煤调运加载", ex);
                }
            }
        }
        private void superGridControl1_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter) Return();
        }

        private void superGridControl1_CellDoubleClick(object sender, GridCellDoubleClickEventArgs e)
        {
            Return();
        }

        /// <summary>
        /// 上一天
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrevDay_Click(object sender, EventArgs e)
        {
            this.DtCurrent = this.DtCurrent.AddDays(-1);
        }

        /// <summary>
        /// 今天
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnToday_Click(object sender, EventArgs e)
        {
            this.DtCurrent = DateTime.Now;
        }

        /// <summary>
        /// 下一天
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNextDay_Click(object sender, EventArgs e)
        {
            this.DtCurrent = this.DtCurrent.AddDays(1);
        }

        private void FrmSaleFuelForecast_Select_Load(object sender, EventArgs e)
        {
            //if (this.InLYMBS.Count>0)
            //{
            //    btnToday.Visible = false;
            //    btnPrevDay.Visible = false;
            //    btnNextDay.Visible = false;
            //    superGridControl1.PrimaryGrid.DataSource = this.InLYMBS;
            //}
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            try
            {
                CmcsConfirm confirm = CommonDAO.GetInstance().GetConfirm(this.CurrentAutotruckId, eConfirmType.来煤预报.ToString());
                if (confirm != null && !string.IsNullOrEmpty(confirm.ResultId))
                {
                    this.Output = CommonDAO.GetInstance().SelfDber.Get<CmcsLMYB>(confirm.ResultId);
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch { }
            timer1.Start();
        }
    }
}