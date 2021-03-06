﻿using System;
using System.Drawing;
using System.Windows.Forms;
//
using DevComponents.DotNetBar.Metro;
using CMCS.Common.Entities.CarTransport;
using CMCS.CarTransport.DAO;
using CMCS.Common.Enums;
using CMCS.CarTransport.Queue.Core;
using CMCS.Common.DAO;
using DevComponents.DotNetBar;
using CMCS.Common;

namespace CMCS.CarTransport.Queue.Frms.Transport.Print
{
	/// <summary>
	/// 打印预览
	/// </summary>
	public partial class FrmPrintWeb : DevComponents.DotNetBar.Metro.MetroForm
	{
		WeighterDAO weighterDAO = WeighterDAO.GetInstance();

		WagonPrinter wagonPrinter = null;
		CmcsBuyFuelTransport _BuyFuelTransport = null;
		Font TitleFont = new Font("宋体", 24, FontStyle.Bold, GraphicsUnit.Pixel);
		Font ContentFont = new Font("宋体", 14, FontStyle.Regular, GraphicsUnit.Pixel);

		int PageIndex = 1;

		public FrmPrintWeb(CmcsBuyFuelTransport buyfueltransport)
		{
			_BuyFuelTransport = CommonDAO.GetInstance().SelfDber.Get<CmcsBuyFuelTransport>(buyfueltransport.Id);
			InitializeComponent();
		}

		/// <summary>
		/// 打印磅单
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void tsmiPrint_Click(object sender, EventArgs e)
		{
			if (this._BuyFuelTransport.PrintCount == 0)
			{
				this.wagonPrinter.Print(this._BuyFuelTransport);
			}
			else if (this._BuyFuelTransport.PrintCount > 0 && CommonDAO.GetInstance().HasResourcePowerByResCode(SelfVars.LoginUser.UserAccount, eUserRoleCodes.汽车智能化信息维护.ToString()))
			{
				FrmPrintRemark frm = new FrmPrintRemark(this._BuyFuelTransport);
				frm.ShowDialog();
			}
			else
			{
				MessageBoxEx.Show("已打印，无权限再次打印", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			this._BuyFuelTransport.PrintCount++;
			this._BuyFuelTransport.PrintTime = GlobalVars.ServerNowDateTime;
			CommonDAO.GetInstance().SelfDber.Update(this._BuyFuelTransport);
			this.DialogResult = DialogResult.OK;
			this.Close();
		}

		private void panel1_Paint(object sender, PaintEventArgs e)
		{
			Graphics g = e.Graphics;

			string SerialNumber = string.Empty,
					CarNumber = string.Empty,
					MineName = string.Empty,
					FuelKindName = string.Empty,
					GrossTime = string.Empty,
					TareTime = string.Empty,
					TicketWeight = string.Empty,
					GrossWeight = string.Empty,
					TareWeight = string.Empty,
					SuttleWeight = string.Empty,
					KgWeight = string.Empty,
					KsWeight = string.Empty,
					CheckWeight = string.Empty,
					UserName = string.Empty;
			if (this._BuyFuelTransport != null)
			{
				SerialNumber = this._BuyFuelTransport.SerialNumber;
				CarNumber = this._BuyFuelTransport.CarNumber;
				MineName = this._BuyFuelTransport.MineName;
				FuelKindName = this._BuyFuelTransport.FuelKindName;
				GrossTime = DisposeTime(this._BuyFuelTransport.GrossTime.ToString(), "yyyy-MM-dd HH:mm");
				TareTime = DisposeTime(this._BuyFuelTransport.TareTime.ToString(), "yyyy-MM-dd HH:mm");
				TicketWeight = this._BuyFuelTransport.TicketWeight.ToString("F2").PadLeft(6, ' ');
				GrossWeight = this._BuyFuelTransport.GrossWeight.ToString("F2").PadLeft(6, ' ');
				TareWeight = this._BuyFuelTransport.TareWeight.ToString("F2").PadLeft(6, ' ');
				SuttleWeight = this._BuyFuelTransport.SuttleWeight.ToString("F2").PadLeft(6, ' ');
				KgWeight = this._BuyFuelTransport.KgWeight.ToString("F2").PadLeft(6, ' ');
				KsWeight = this._BuyFuelTransport.KsWeight.ToString("F2").PadLeft(6, ' ');
				CheckWeight = this._BuyFuelTransport.CheckWeight.ToString("F2").PadLeft(6, ' ');
				#region 入厂煤
				// 行间距 24 
				float TopValue = 53;
				string printValue = "";
				g.DrawString("河南煤炭储配交易中心", new Font("黑体", 20, FontStyle.Bold, GraphicsUnit.Pixel), Brushes.White, 30, TopValue);
				TopValue += 34;

				g.DrawString("打印时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm"), ContentFont, Brushes.White, 30, TopValue);
				TopValue += 24;

				g.DrawLine(new Pen(Color.White, 2), 0, TopValue, 300 - 10, TopValue);
				TopValue += 15;

				g.DrawString("车 牌 号：" + CarNumber, ContentFont, Brushes.White, 30, TopValue);
				TopValue += 24;

				printValue = printValue = "矿    点：" + MineName;
				if (printValue.Length > 12)
				{
					g.DrawString(printValue.Substring(0, 12), ContentFont, Brushes.White, 30, TopValue);
					TopValue += 24;
					g.DrawString(printValue.Substring(12, printValue.Length - 12), ContentFont, Brushes.White, 105, TopValue);
					TopValue += 24;
				}
				else
				{
					g.DrawString(printValue, ContentFont, Brushes.White, 30, TopValue);
					TopValue += 24;
				}

				g.DrawString("煤    种：" + FuelKindName, ContentFont, Brushes.White, 30, TopValue);
				TopValue += 24;

				g.DrawString(string.Format("矿 发 量：{0} 吨", TicketWeight), ContentFont, Brushes.White, 30, TopValue);
				TopValue += 24;

				g.DrawString(string.Format("毛    重：{0} 吨", GrossWeight), ContentFont, Brushes.White, 30, TopValue);
				TopValue += 24;

				g.DrawString(string.Format("皮    重：{0} 吨", TareWeight), ContentFont, Brushes.White, 30, TopValue);
				TopValue += 24;

				g.DrawString(string.Format("扣    矸：{0} 吨", KgWeight), ContentFont, Brushes.White, 30, TopValue);
				TopValue += 24;

				g.DrawString(string.Format("扣    水：{0} 吨", KsWeight), ContentFont, Brushes.White, 30, TopValue);
				TopValue += 24;

				g.DrawString(string.Format("验 收 量：{0} 吨", CheckWeight), ContentFont, Brushes.White, 30, TopValue);
				TopValue += 24;

				g.DrawString(string.Format("操 作 员：{0}", SelfVars.LoginUser.UserName), ContentFont, Brushes.White, 30, TopValue);
				TopValue += 24;

				g.DrawString(string.Format("签    字："), ContentFont, Brushes.White, 30, TopValue);
				TopValue += 34;

				g.DrawString(PageIndex.ToString() + "联", ContentFont, Brushes.White, 110, TopValue);
				TopValue += 24;
				#endregion
			}
		}
		public static string DisposeTime(string dt, string format)
		{
			if (!string.IsNullOrEmpty(dt))
			{
				DateTime dti = DateTime.Parse(dt);
				if (dti > new DateTime(2000, 1, 1))
					return dti.ToString(format);
			}
			return string.Empty;
		}


		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.No;
			this.Close();
		}

		private void FrmPrintWeb_Load(object sender, EventArgs e)
		{
			this.wagonPrinter = new WagonPrinter(printDocument1);
		}
	}
}
