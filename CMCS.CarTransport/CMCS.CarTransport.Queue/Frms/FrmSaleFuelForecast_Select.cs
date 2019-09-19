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
    /// ����úԤ��ѡ��
    /// </summary>
    public partial class FrmSaleFuelForecast_Select : DevComponents.DotNetBar.Metro.MetroForm
    {
        /// <summary>
        /// ѡ�е�ʵ��
        /// </summary>
        public CmcsLMYB Output;

        /// <summary>
        /// ����Id
        /// </summary>
        private string CurrentAutotruckId;

        private string strWhere;

        private DateTime dtCurrent;
        /// <summary>
        /// Ԥ�Ƶ���ʱ��
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
        /// ����úԤ��ѡ��
        /// </summary>
        public List<CmcsLMYB> InLYMBS
        {
            get { return inLYMBS; }
            set { inLYMBS = value; }
        }

        public FrmSaleFuelForecast_Select(string strWhere = " where TransportTypeName='����' ")
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
                List<CmcsLMYB> list = CommonDAO.GetInstance().SelfDber.Entities<CmcsLMYB>(strWhere + " and TransportTypeName='����' and trunc(ZcDate)=trunc(:ZcDate) and ischeck=1 order by ZcDate desc", new { ZcDate = inFactoryTime });
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
            // ȡ���༭ģʽ
            e.Cancel = true;
        }

        /// <summary>
        /// �����к�
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

                    // �����Ч״̬
                    gridRow.Cells["clmTransportCompanyName"].Value = entity.TheTransportCompany.Name;
                }
                catch (Exception ex)
                {
                    Log4Neter.Error("����ú���˼���", ex);
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
        /// ��һ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrevDay_Click(object sender, EventArgs e)
        {
            this.DtCurrent = this.DtCurrent.AddDays(-1);
        }

        /// <summary>
        /// ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnToday_Click(object sender, EventArgs e)
        {
            this.DtCurrent = DateTime.Now;
        }

        /// <summary>
        /// ��һ��
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
                CmcsConfirm confirm = CommonDAO.GetInstance().GetConfirm(this.CurrentAutotruckId, eConfirmType.��úԤ��.ToString());
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