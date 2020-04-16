using System;
using System.Drawing;
using System.Windows.Forms;
//
using DevComponents.DotNetBar.Metro;
using CMCS.Common.Entities.CarTransport;
using CMCS.CarTransport.DAO;
using CMCS.Common.Enums;
using CMCS.CarTransport.Queue.Core;
using DevComponents.DotNetBar;
using CMCS.Common;
using CMCS.Common.DAO;
using CMCS.CarTransport.Queue.Frms.Transport.Print.SaleFuelPrint;

namespace CMCS.CarTransport.Queue.Frms.Transport.Print
{
	/// <summary>
	/// 打印预览
	/// </summary>
	public partial class FrmPrintRemark : DevComponents.DotNetBar.Metro.MetroForm
	{
		WeighterDAO weighterDAO = WeighterDAO.GetInstance();

		WagonPrinter wagonPrinter = null;
		SaleFuelWagonPrinter wagonPrinter_Sale = null;
		CmcsBuyFuelTransport _BuyFuelTransport = null;
		CmcsSaleFuelTransport _SaleFuelTransport = null;
		Font TitleFont = new Font("宋体", 24, FontStyle.Bold, GraphicsUnit.Pixel);
		Font ContentFont = new Font("宋体", 14, FontStyle.Regular, GraphicsUnit.Pixel);

		public FrmPrintRemark(CmcsBuyFuelTransport buyfueltransport = null, CmcsSaleFuelTransport salefueltransport = null)
		{
			_BuyFuelTransport = buyfueltransport;
			_SaleFuelTransport = salefueltransport;
			InitializeComponent();

			if (this._BuyFuelTransport != null)
			{
				txt_PrintCount.Text = this._BuyFuelTransport.PrintCount.ToString();
				txt_PrintTime.Text = this._BuyFuelTransport.PrintTime.ToString();
				txt_Remark.Text = this._BuyFuelTransport.Remark;
			}
			else if (this._SaleFuelTransport != null)
			{
				txt_PrintCount.Text = this._SaleFuelTransport.PrintCount.ToString();
				txt_PrintTime.Text = this._SaleFuelTransport.PrintTime.ToString();
				txt_Remark.Text = this._SaleFuelTransport.Remark;
			}
		}

		/// <summary>
		/// 打印磅单
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tsmiPrint_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrEmpty(txt_Remark.Text))
			{
				MessageBoxEx.Show("请填写备注", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			if (this._BuyFuelTransport != null)
			{
				//this._BuyFuelTransport.PrintCount++;
				//this._BuyFuelTransport.PrintTime = GlobalVars.ServerNowDateTime;
				this._BuyFuelTransport.Remark = txt_Remark.Text;
				CommonDAO.GetInstance().SelfDber.Update(this._BuyFuelTransport);
				this.wagonPrinter.Print(this._BuyFuelTransport);
			}
			else if (this._SaleFuelTransport != null)
			{
				//this._SaleFuelTransport.PrintCount++;
				//this._SaleFuelTransport.PrintTime = GlobalVars.ServerNowDateTime;
				this._SaleFuelTransport.Remark = txt_Remark.Text;
				CommonDAO.GetInstance().SelfDber.Update(this._SaleFuelTransport);
				this.wagonPrinter_Sale.Print(this._SaleFuelTransport);
			}

			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.No;
			this.Close();
		}

		private void FrmPrintWeb_Load(object sender, EventArgs e)
		{
			this.wagonPrinter = new WagonPrinter(printDocument1);
			this.wagonPrinter_Sale = new SaleFuelWagonPrinter(printDocument2);
		}
	}
}
