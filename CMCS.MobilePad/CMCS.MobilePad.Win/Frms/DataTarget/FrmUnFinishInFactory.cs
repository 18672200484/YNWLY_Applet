using System;
using DevComponents.DotNetBar.Metro;
using System.Collections.Generic;
using CMCS.Common.Entities.CarTransport;
using CMCS.Common;
using DevComponents.DotNetBar.SuperGrid;
using DevComponents.DotNetBar;
using CMCS.Common.Entities.Fuel;

namespace CMCS.MobilePad.Win.Frms.DataTarget
{
    public partial class FrmUnFinishInFactory : MetroAppForm
    {
        /// <summary>
        /// ����Ψһ��ʶ��
        /// </summary>
        public static string UniqueKey = "FrmUnFinishInFactory";

        string SqlWhere = string.Empty;
        string Type = "�볡ú";

        /// <summary>
        /// �Ƿ�Ԥ�Ƶ���
        /// </summary>
        bool IsYJInFactory = false;

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

        public FrmUnFinishInFactory(string type, bool isYJInFactory)
        {
            InitializeComponent();
            Type = type;
            IsYJInFactory = isYJInFactory;
            if (IsYJInFactory)
                this.Text = "Ԥ�Ƶ���������Ϣ";
            else
                this.Text = "δ��ɳ�����Ϣ";

            if (Type == "�볡ú")
            {
                superGridControl1.PrimaryGrid.Columns["clmSupplierName"].HeaderText = "��ú��λ";
            }
            else
            {
                superGridControl1.PrimaryGrid.Columns["clmSupplierName"].HeaderText = "�ջ���λ";
            }
        }

        private void FrmHome_Load(object sender, EventArgs e)
        {
            btnSearch_Click(null, null);
            InitFrom();
        }

        public void InitFrom()
        {
            // ��ť
            GridButtonXEditControl btnPrintCode = superGridControl1.PrimaryGrid.Columns["operation"].EditControl as GridButtonXEditControl;
            btnPrintCode.ColorTable = eButtonColor.BlueWithBackground;
            btnPrintCode.Click += new EventHandler(btnCarDeduction_Click);
        }

        /// <summary>
        /// �༭
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnCarDeduction_Click(object sender, EventArgs e)
        {
            GridButtonXEditControl btn = sender as GridButtonXEditControl;
            if (btn == null) return;

            CmcsLMYBDetail buyFuelTransport = btn.EditorCell.GridRow.DataItem as CmcsLMYBDetail;
            if (buyFuelTransport == null) return;

            new FrmUnFinishInFactory_Confirm(buyFuelTransport).ShowDialog();

            BindData();
        }

        public void BindData()
        {
            //List<CmcsLMYBDetail> list = Dbers.GetInstance().SelfDber.Entities<CmcsLMYBDetail>(this.SqlWhere + " order by CreateDate desc");
            IList<CmcsLMYBDetail> list = Dbers.GetInstance().SelfDber.Query<CmcsLMYBDetail>(this.SqlWhere + " order by t.CreateDate desc").AsList<CmcsLMYBDetail>();
            superGridControl1.PrimaryGrid.DataSource = list;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.SqlWhere = "select t.* from FultbLMYBDetail t inner join FultbLMYB b on t.lmybid=b.id where 1=1 ";
            if (!IsYJInFactory)
            {
                this.SqlWhere += " and b.Infactorytype like '%" + Type + "%' and t.isfinish ='δ���' ";
            }
            else
            {
                this.SqlWhere += " and b.Infactorytype like '" + Type + "' and Trunc(b.InFactoryTime)=Trunc(Sysdate) ";
            }

            if (!String.IsNullOrWhiteSpace(txtInput.Text))
            {
                SqlWhere += " and t.CarNumber like '%" + txtInput.Text + "%' ";
            }

            BindData();
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            txtInput.Text = "";
            btnSearch_Click(null, null);
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
                    CmcsLMYBDetail entity = item.DataItem as CmcsLMYBDetail;
                    item.Cells["clmSupplierName"].Value = entity.TheLMYB.SupplierName;
                    item.Cells["clmMineName"].Value = entity.TheLMYB.MineName;

                    item.Cells["clmStepName"].Value = "�ȴ��볡";
                    if (entity.IsFinish == "�����")
                        item.Cells["operation"].Visible = false;
                    if (this.Type == "�볡ú")
                    {

                        CmcsBuyFuelTransport transport = Dbers.GetInstance().SelfDber.Entity<CmcsBuyFuelTransport>("where LMYBDetailId=:LMYBDetailId order by CreateDate desc", new { LMYBDetailId = entity.Id });
                        if (transport != null)
                        {
                            item.Cells["clmInFactoryTime"].Value = transport.InFactoryTime.ToString("yyyy-MMM-dd");
                            item.Cells["clmStepName"].Value = transport.StepName;
                        }
                    }
                    else
                    {
                        CmcsSaleFuelTransport transport = Dbers.GetInstance().SelfDber.Entity<CmcsSaleFuelTransport>("where LMYBDetailId=:LMYBDetailId order by CreateDate desc", new { LMYBDetailId = entity.Id });
                        if (transport != null)
                        {
                            item.Cells["clmInFactoryTime"].Value = transport.InFactoryTime.ToString("yyyy-MMM-dd");
                            item.Cells["clmStepName"].Value = transport.StepName;
                        }
                    }

                    CmcsAutotruck autoTruck = Dbers.GetInstance().SelfDber.Entity<CmcsAutotruck>("where CarNumber=:CarNumber order by CreateDate desc", new { CarNumber = entity.CarNumber });
                    if (autoTruck != null)
                    {
                        item.Cells["clmDriver"].Value = autoTruck.Driver;
                        item.Cells["clmPhoneNumber"].Value = autoTruck.CellPhoneNumber;
                    }
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
