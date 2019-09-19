using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using CMCS.Common.DAO;
using CMCS.Common.Entities.CarTransport;
using CMCS.Common.Enums;
using CMCS.Common.Entities.Sys;
using CMCS.Common.Entities.Fuel;
using CMCS.MobilePad.Win.Frms.Common;

namespace CMCS.MobilePad.Win.Frms
{
	public partial class FrmTransportPlan_Confirm : DevComponents.DotNetBar.Metro.MetroForm
	{
		string autotruckId;
		string transportId;
		string carType;

		CommonDAO commonDAO = CommonDAO.GetInstance();

		public FrmTransportPlan_Confirm(string transportId)
		{
			InitializeComponent();

			this.transportId = transportId;
			CmcsLMYB entity = commonDAO.SelfDber.Get<CmcsLMYB>(this.transportId);
			if (entity != null)
			{
				txtYBNum.Text = entity.YbNum;
			}
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.No;
			this.Close();
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrWhiteSpace(txtCarNum.Text))
			{
				MessageBoxEx.Show("请填写车牌号！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			CmcsLMYB entity = commonDAO.SelfDber.Get<CmcsLMYB>(this.transportId);
			if (entity != null)
			{
				string where = string.Format(" where LMYBId='{0}'", entity.Id);

				List<CmcsLMYBDetail> list = commonDAO.SelfDber.Entities<CmcsLMYBDetail>(where);
				if (list.Where(t => t.CarNumber == txtCarNum.Text && t.IsFinish != "已完成").Count() > 0)
				{
					MessageBoxEx.Show("调运明细中车号重复！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning); return;
				}

				CmcsLMYBDetail detail = new CmcsLMYBDetail();
				detail.LMYBId = entity.Id;
				detail.CarNumber = txtCarNum.Text;
				detail.TicketWeight = Convert.ToDecimal(txtTicketQty.Text);
				commonDAO.SelfDber.Insert<CmcsLMYBDetail>(detail);

				entity.CoalNumber = entity.CoalNumber + detail.TicketWeight;
				entity.TransportNumber += 1;
				commonDAO.SelfDber.Update<CmcsLMYB>(entity);

				this.DialogResult = DialogResult.OK;
				this.Close();
			}
		}

		private void btnSelectAutotruck_BuyFuel_Click(object sender, EventArgs e)
		{
			FrmAutotruck_Select frm = new FrmAutotruck_Select(" and IsUse=1 order by CarNumber asc");
			if (frm.ShowDialog() == DialogResult.OK)
			{
				this.txtCarNum.Text = frm.Output.CarNumber;
			}
		}
	}
}