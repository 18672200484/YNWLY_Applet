using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using CMCS.Common;
using CMCS.Common.Entities.CarTransport;
using CMCS.Common.Entities;
using CMCS.Common.Entities.BaseInfo;
using CMCS.Common.Entities.Fuel;
using CMCS.Common.DAO;
using CMCS.Common.Utilities;
using DevComponents.DotNetBar.Controls;
using CMCS.CarTransport.DAO;

namespace CMCS.CarTransport.Queue.Frms.Transport.SaleFuelTransport
{
	public partial class FrmSaleFuelTransport_Oper : DevComponents.DotNetBar.Metro.MetroForm
	{
		String id = String.Empty;
		bool edit = false;
		CmcsSaleFuelTransport cmcsSaleFuelTransport;
		CmcsLMYB cmcsTransportSales;
		public FrmSaleFuelTransport_Oper()
		{
			InitializeComponent();
		}
		public FrmSaleFuelTransport_Oper(String pId, bool pEdit)
		{
			InitializeComponent();
			id = pId;
			edit = pEdit;
		}


		private void FrmSaleFuelTransport_Oper_Load(object sender, EventArgs e)
		{
			LoadFuelkind(cmbFuelName_SaleFuel);

			if (!String.IsNullOrEmpty(id))
			{
				this.cmcsSaleFuelTransport = Dbers.GetInstance().SelfDber.Get<CmcsSaleFuelTransport>(this.id);
				if (!String.IsNullOrEmpty(this.cmcsSaleFuelTransport.TransportSalesId))
				{
					cmcsTransportSales = Dbers.GetInstance().SelfDber.Get<CmcsLMYB>(this.cmcsSaleFuelTransport.TransportSalesId);
				}
				txt_SerialNumber.Text = cmcsSaleFuelTransport.SerialNumber;
				txt_CarNumber.Text = cmcsSaleFuelTransport.CarNumber;
				txt_TransportSalesNum.Text = cmcsSaleFuelTransport.TransportSalesNum;
				txt_TransportNo.Text = cmcsSaleFuelTransport.TransportNo;
				txt_LoadArea.Text = cmcsSaleFuelTransport.LoadArea;
				cmbFuelName_SaleFuel.SelectedValue = cmcsSaleFuelTransport.FuelKindId;
				CmcsSupplier supplier = Dbers.GetInstance().SelfDber.Get<CmcsSupplier>(cmcsSaleFuelTransport.SupplierId);
				if (supplier != null)
					txt_SupplierName.Text = supplier.Name;

				CmcsTransportCompany company = Dbers.GetInstance().SelfDber.Get<CmcsTransportCompany>(cmcsSaleFuelTransport.TransportCompanyId);
				if (company != null)
					txt_TransportCompanyName.Text = company.Name;
				dbi_GrossWeight.Value = (double)cmcsSaleFuelTransport.GrossWeight;
				dbi_TareWeight.Value = (double)cmcsSaleFuelTransport.TareWeight;
				dbi_SuttleWeight.Value = (double)cmcsSaleFuelTransport.SuttleWeight;
				txt_InFactoryTime.Text = cmcsSaleFuelTransport.InFactoryTime.Year == 1 ? "" : cmcsSaleFuelTransport.InFactoryTime.ToString();
				txt_GrossTime.Text = cmcsSaleFuelTransport.GrossTime.Year == 1 ? "" : cmcsSaleFuelTransport.GrossTime.ToString();
				txt_TareTime.Text = cmcsSaleFuelTransport.TareTime.Year == 1 ? "" : cmcsSaleFuelTransport.TareTime.ToString();
				txt_OutFactoryTime.Text = cmcsSaleFuelTransport.OutFactoryTime.Year == 1 ? "" : cmcsSaleFuelTransport.OutFactoryTime.ToString();
				txt_LoadTime.Text = cmcsSaleFuelTransport.LoadTime.Year == 1 ? "" : cmcsSaleFuelTransport.LoadTime.ToString();
				txt_Remark.Text = cmcsSaleFuelTransport.Remark;
				chb_IsFinish.Checked = (cmcsSaleFuelTransport.IsFinish == 1);
				chb_IsUse.Checked = (cmcsSaleFuelTransport.IsUse == 1);
			}
			if (!edit)
			{
				btnSubmit.Enabled = false;
				CMCS.CarTransport.Queue.Utilities.Helper.ControlReadOnly(panelEx2);
			}
		}

		/// <summary>
		/// 加载煤种
		/// </summary>
		void LoadFuelkind(ComboBoxEx comboBoxEx)
		{
			comboBoxEx.DisplayMember = "FuelName";
			comboBoxEx.ValueMember = "Id";
			comboBoxEx.DataSource = Dbers.GetInstance().SelfDber.Entities<CmcsFuelKind>("where Valid='有效' and ParentId is not null");
		}


		private void btnSubmit_Click(object sender, EventArgs e)
		{
			if (txt_SerialNumber.Text.Length == 0)
			{
				MessageBoxEx.Show("该标车牌号不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			if ((cmcsSaleFuelTransport == null || cmcsSaleFuelTransport.CarNumber != txt_SerialNumber.Text) && Dbers.GetInstance().SelfDber.Entities<CmcsSaleFuelTransport>(" where CarNumber=:CarNumber", new { CarNumber = txt_SerialNumber.Text }).Count > 0)
			{
				MessageBoxEx.Show("该标车牌号不可重复！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			if (cmcsSaleFuelTransport != null)
			{
				//if (!CompareClass.CompareClassValue(this.cmcsSaleFuelTransport, Dbers.GetInstance().SelfDber.Get<CmcsSaleFuelTransport>(this.id)))
				//{
				//    MessageBoxEx.Show("数据已更改请重新打开页面修改保存！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				//    return;
				//}
				if (cmcsSaleFuelTransport.CarNumber != txt_CarNumber.Text)
				{
					CmcsAutotruck autotruck = Dbers.GetInstance().SelfDber.Entity<CmcsAutotruck>(" where CarNumber=:CarNumber", new { CarNumber = this.txt_CarNumber.Text });
					if (autotruck != null)
					{
						cmcsSaleFuelTransport.AutotruckId = autotruck.Id;
						CmcsUnFinishTransport unfinish = Dbers.GetInstance().SelfDber.Entity<CmcsUnFinishTransport>(" where TransportId=:TransportId", new { TransportId = cmcsSaleFuelTransport.Id });
						if (unfinish != null)
						{
							unfinish.AutotruckId = autotruck.Id;
							Dbers.GetInstance().SelfDber.Update(unfinish);
						}
					}
				}
				cmcsSaleFuelTransport.SerialNumber = txt_SerialNumber.Text;
				cmcsSaleFuelTransport.CarNumber = txt_CarNumber.Text;
				cmcsSaleFuelTransport.GrossWeight = (decimal)dbi_GrossWeight.Value;
				cmcsSaleFuelTransport.TareWeight = (decimal)dbi_TareWeight.Value;
				cmcsSaleFuelTransport.FuelKindId = cmbFuelName_SaleFuel.SelectedValue.ToString();
				cmcsSaleFuelTransport.Remark = txt_Remark.Text;
				cmcsSaleFuelTransport.IsFinish = (chb_IsFinish.Checked ? 1 : 0);
				cmcsSaleFuelTransport.IsUse = (chb_IsUse.Checked ? 1 : 0);
				cmcsSaleFuelTransport.IsSynch = "0";
				if (cmcsTransportSales != null)
				{
					cmcsSaleFuelTransport.TransportSalesId = cmcsTransportSales.Id;
					cmcsSaleFuelTransport.SupplierId = cmcsTransportSales.SupplierId;
					cmcsSaleFuelTransport.TransportCompanyId = cmcsTransportSales.TransportCompanyId;
				}

				WeighterDAO.GetInstance().SaveSaleFuelTransport(cmcsSaleFuelTransport);
			}
			else
			{
				cmcsSaleFuelTransport = new CmcsSaleFuelTransport();
				cmcsSaleFuelTransport.SerialNumber = txt_SerialNumber.Text;
				cmcsSaleFuelTransport.CarNumber = txt_CarNumber.Text;
				cmcsSaleFuelTransport.GrossWeight = (decimal)dbi_GrossWeight.Value;
				cmcsSaleFuelTransport.TareWeight = (decimal)dbi_TareWeight.Value;
				cmcsSaleFuelTransport.SuttleWeight = (decimal)dbi_SuttleWeight.Value;
				txt_Remark.Text = cmcsSaleFuelTransport.Remark;
				cmcsSaleFuelTransport.IsFinish = (chb_IsFinish.Checked ? 1 : 0);
				cmcsSaleFuelTransport.IsUse = (chb_IsUse.Checked ? 1 : 0);
				Dbers.GetInstance().SelfDber.Insert(cmcsSaleFuelTransport);
				//SaveAndUpdate(cmcsSaleFuelTransport, cmcsSaleFueltransportdeducts);
			}
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		private void buttonX2_Click(object sender, EventArgs e)
		{


		}


		private void btnSupplier_Click(object sender, EventArgs e)
		{
		}

		private void btnTransportCompany_Click(object sender, EventArgs e)
		{
		}

		private void BtnMine_Click(object sender, EventArgs e)
		{
		}

		private void btnFuelKind_Click(object sender, EventArgs e)
		{
		}

		private void btnSelectForecast_SaleFuel_Click(object sender, EventArgs e)
		{

			FrmSaleFuelForecast_Select frm = new FrmSaleFuelForecast_Select();
			if (frm.ShowDialog() == DialogResult.OK)
			{
				cmcsTransportSales = frm.Output;
				txt_SupplierName.Text = frm.Output.TheFuelSupplier != null ? frm.Output.TheFuelSupplier.Name : "";
				txt_TransportCompanyName.Text = Dbers.GetInstance().SelfDber.Get<CmcsTransportCompany>(frm.Output.TransportCompanyId).Name;
				txt_TransportNo.Text = frm.Output.TransportNo;
				txt_TransportSalesNum.Text = frm.Output.YbNum;
			}
		}

		private void btnCarNumber_Click(object sender, EventArgs e)
		{
			FrmAutotruck_Select frm = new FrmAutotruck_Select(" and IsUse=1 order by CarNumber asc");
			if (frm.ShowDialog() == DialogResult.OK)
			{
				this.txt_CarNumber.Text = frm.Output.CarNumber;
			}
		}

		/// <summary>
		/// 选择收货单位
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSelectSupplyReceive_SaleFuel_Click(object sender, EventArgs e)
		{
			FrmSupplyReceive_Select frm = new FrmSupplyReceive_Select("where IsValid=1 order by UnitName asc");
			if (frm.ShowDialog() == DialogResult.OK)
			{
				this.txt_SupplierName.Text = frm.Output.UnitName;
			}
		}
		/// <summary>
		/// 选择运输单位
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSelectTransportCompany_SaleFuel_Click(object sender, EventArgs e)
		{
			FrmTransportCompany_Select frm = new FrmTransportCompany_Select("where IsUse=1 order by Name asc");
			if (frm.ShowDialog() == DialogResult.OK)
			{
				this.txt_TransportCompanyName.Text = frm.Output.Name;
			}
		}

	}
}