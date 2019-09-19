using System;
using DevComponents.DotNetBar.Metro;
using System.Collections.Generic;
using CMCS.Common.Entities.CarTransport;
using CMCS.Common;
using DevComponents.DotNetBar.SuperGrid;
using DevComponents.DotNetBar;

namespace CMCS.MobilePad.Win.Frms.CarBreakRules
{
	public partial class FrmCarBreakRules : MetroAppForm
	{
		/// <summary>
		/// 窗体唯一标识符
		/// </summary>
		public static string UniqueKey = "FrmCarBreakRules";

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

		public FrmCarBreakRules()
		{
			InitializeComponent();
		}

		private void FrmHome_Load(object sender, EventArgs e)
		{
			btnSearch_Click(null, null);
			InitFrom();
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

			CarBreakRules entity = btn.EditorCell.GridRow.DataItem as CarBreakRules;
			if (entity == null) return;

			new FrmCarBreakRules_Confirm(entity.Id, entity.InFactoryType).ShowDialog();

			BindData();
		}

		public void BindData()
		{
			string tempSqlWhere = this.SqlWhere;

			List<CarBreakRules> list = Dbers.GetInstance().SelfDber.Query<CarBreakRules>(tempSqlWhere + " order by t.infactorytime desc").AsList();

			superGridControl1.PrimaryGrid.DataSource = list;
		}

		private void btnSearch_Click(object sender, EventArgs e)
		{
			this.SqlWhere = @"select t.*,a.driver,a.cellphonenumber phonenumber,b.BreakRulesType,b.BreakRulesResult,b.operuser,b.OPERDATE  from
                                (select id,Infactorytime,carnumber,infactorytype from cmcstbbuyfueltransport where trunc(infactorytime)>=trunc(sysdate)
                                union all 
                                select id,Infactorytime,carnumber,outfactorytype infactorytype from cmcstbsalefueltransport where trunc(infactorytime)>=trunc(sysdate)) t
                                inner join cmcstbautotruck a on t.carnumber=a.carnumber left join CmcsTbBreakRules b on t.id=b.TransportId where 1=1 ";

			if (!String.IsNullOrEmpty((String)txtInput.Text))
			{
				SqlWhere += " and t.CarNumber like '%" + txtInput.Text + "%' ";
			}

			BindData();
		}

		private void btnAll_Click(object sender, EventArgs e)
		{
			this.SqlWhere = string.Empty;
			txtInput.Text = "";
			BindData();
		}

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
	}

	class CarBreakRules
	{
		public string Id { get; set; }

		public string CarNumber { get; set; }

		public string InFactoryTime { get; set; }

		public string Driver { get; set; }

		public string PhoneNumber { get; set; }

		public string BreakRulesType { get; set; }

		public string BreakRulesResult { get; set; }

		public string OperUser { get; set; }

		public string OperDate { get; set; }

		public string InFactoryType { get; set; }
	}
}
