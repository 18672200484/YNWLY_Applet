using System;
using DevComponents.DotNetBar.Metro;
using System.Collections.Generic;
using CMCS.Common.Entities.CarTransport;
using CMCS.Common;
using DevComponents.DotNetBar.SuperGrid;
using DevComponents.DotNetBar;
using CMCS.Common.Entities.Fuel;

namespace CMCS.MobilePad.Win.Frms.UnLoadPlan
{
	public partial class FrmUnLoadPlan : MetroAppForm
	{
		/// <summary>
		/// 窗体唯一标识符
		/// </summary>
		public static string UniqueKey = "FrmUnLoadPlan";

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

		public FrmUnLoadPlan()
		{
			InitializeComponent();
		}

		private void FrmHome_Load(object sender, EventArgs e)
		{
			LoadUnLoadStatus();
			btnSearch_Click(null, null);
			this.CurrentList = new List<FulUnLoadPlanDetail>();
			InitFrom();
		}

		public void InitFrom()
		{
			GridCheckBoxXEditControl btnPrintCode = superGridControl1.PrimaryGrid.Columns["operation"].EditControl as GridCheckBoxXEditControl;
			if (btnPrintCode != null) btnPrintCode.CheckedChanged += new EventHandler(btnCarDeduction_Click);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void btnCarDeduction_Click(object sender, EventArgs e)
		{
			GridCheckBoxXEditControl btn = sender as GridCheckBoxXEditControl;
			if (btn == null) return;

			FulUnLoadPlanDetail entity = btn.EditorCell.GridRow.DataItem as FulUnLoadPlanDetail;
			if (entity == null) return;

			if (btn.Checked && !this.CurrentList.Contains(entity))
				this.CurrentList.Add(entity);
			else if (!btn.Checked && this.CurrentList.Contains(entity))
				this.CurrentList.Remove(entity);
		}

		public void BindData()
		{
			string tempSqlWhere = this.SqlWhere;

			List<FulUnLoadPlanDetail> list = Dbers.GetInstance().SelfDber.Query<FulUnLoadPlanDetail>(tempSqlWhere + " ").AsList();

			superGridControl1.PrimaryGrid.DataSource = list;
		}

		private void btnSearch_Click(object sender, EventArgs e)
		{
			this.SqlWhere = "select * from (select t.* from fultbunloadareaplandetail t inner join fultblmybdetail a on t.lmybdetailid=a.id left join fultblmyb b on a.lmybid=b.id ";

			SqlWhere += " and b.infactorytime>= to_date('" + DateTime.Now.Date.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss') ";
			SqlWhere += " and b.infactorytime< to_date('" + DateTime.Now.Date.AddDays(1).ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss') order by b.infactorytime desc) where 1=1 ";

			if (!String.IsNullOrEmpty(this.cmbUnLoad.Text))
			{
				SqlWhere += " and IsUnLoad = '" + ((ComboBoxItem)this.cmbUnLoad.SelectedItem).Name + "' ";
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
			cmbUnLoad.Text = "";
			btnSearch_Click(null, null);
		}

		private void btn_SetUnload_Click(object sender, EventArgs e)
		{
			if (this.CurrentList.Count == 0)
			{
				MessageBoxEx.Show("请选择数据！", "提示", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Stop);
				return;
			}
			new FrmUnLoadPlan_Confirm(this.CurrentList).ShowDialog();
		}

		private void LoadUnLoadStatus()
		{
			this.cmbUnLoad.Items.Add(new ComboBoxItem("", ""));
			this.cmbUnLoad.Items.Add(new ComboBoxItem("0", "未卸煤"));
			this.cmbUnLoad.Items.Add(new ComboBoxItem("1", "已卸煤"));
			this.cmbUnLoad.SelectedIndex = 0;
		}

		#region GridView
		private void superGridControl1_DataBindingComplete(object sender, DevComponents.DotNetBar.SuperGrid.GridDataBindingCompleteEventArgs e)
		{
			foreach (GridRow item in e.GridPanel.Rows)
			{
				try
				{
					FulUnLoadPlanDetail entity = item.DataItem as FulUnLoadPlanDetail;
					item.Cells["clmSupplierName"].Value = entity.TheLMYBDetail.TheLMYB.SupplierName;
					item.Cells["clmMineName"].Value = entity.TheLMYBDetail.TheLMYB.MineName;
					item.Cells["clmIsUnload"].Value = entity.IsUnLoad == "0" ? "未卸煤" : "已卸煤";
					if (entity.IsUnLoad == "1")
						item.Cells["operation"].Visible = false;

					if (entity.TheLMYBDetail.TheLMYB.InFactoryType.Contains("入场"))
					{
						item.Cells["clmGrossTime"].Value = entity.TheLMYBDetail.TheBuyFuelTransport.GrossTime.ToString("yyyy-MM-dd HH:mm:ss");
						item.Cells["clmGrossWeight"].Value = entity.TheLMYBDetail.TheBuyFuelTransport.GrossWeight.ToString("f2");
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
