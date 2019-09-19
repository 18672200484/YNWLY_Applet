using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//
using System.Drawing;
using System.Drawing.Printing;
using DevComponents.DotNetBar;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using CMCS.Common.Entities.CarTransport;
using CMCS.Common;
using CMCS.CarTransport.Queue.Core;
using CMCS.Common.Entities.BaseInfo;
using CMCS.Common.Entities.Fuel;

namespace CMCS.CarTransport.Queue.Frms.Transport.Print.SaleFuelPrint
{
    /// <summary>
    /// 磅单打印
    /// </summary>
    class SaleFuelWagonPrinter : MetroForm
    {
        Font TitleFont = new Font("宋体", 24, FontStyle.Bold, GraphicsUnit.Pixel);
        Font ContentFont = new Font("宋体", 14, FontStyle.Regular, GraphicsUnit.Pixel);
        PrintDocument _PrintDocument = null;
        CmcsSaleFuelTransport _BuyFuelTransport = null;
        int PageIndex = 1;
        Graphics gs = null;
        public SaleFuelWagonPrinter(PrintDocument printDoc)
        {
            this._PrintDocument = printDoc;
            this._PrintDocument.DefaultPageSettings.PaperSize = new PaperSize("Custum", 850, 368);
            this._PrintDocument.OriginAtMargins = true;
            this._PrintDocument.DefaultPageSettings.Margins.Left = 10;
            this._PrintDocument.DefaultPageSettings.Margins.Right = 0;
            this._PrintDocument.DefaultPageSettings.Margins.Top = 0;
            this._PrintDocument.DefaultPageSettings.Margins.Bottom = 0;
            this._PrintDocument.PrintController = new StandardPrintController();
            this._PrintDocument.PrintPage += _PrintDocument_PrintPage;
        }
        public void Print(CmcsSaleFuelTransport buyfueltransport)
        {
            _BuyFuelTransport = buyfueltransport;
            try
            {
                this._PrintDocument.Print();
            }
            catch
            {
                MessageBoxEx.Show("打印异常，请检查打印机！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            _BuyFuelTransport = null;
        }

        private void _PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = e.Graphics;
            if (this.gs != null)
                g = this.gs;

            string SerialNumber = string.Empty,
                    CarNumber = string.Empty,
                    CustomerName = string.Empty,
                    CompanyName = string.Empty,
                    FuelKindName = string.Empty,
                    GrossTime = string.Empty,
                    TareTime = string.Empty,
                    TicketWeight = string.Empty,
                    GrossWeight = string.Empty,
                    TareWeight = string.Empty,
                    SuttleWeight = string.Empty,
                    UserName = string.Empty;
            if (this._BuyFuelTransport != null)
            {
                SerialNumber = this._BuyFuelTransport.SerialNumber;
                CarNumber = this._BuyFuelTransport.CarNumber;
                CmcsFuelKind fuelkind = Dbers.GetInstance().SelfDber.Get<CmcsFuelKind>(this._BuyFuelTransport.FuelKindId);
                if (fuelkind != null)
                    FuelKindName = fuelkind.FuelName;
                CmcsSupplier customer = this._BuyFuelTransport.TheSupplier;
                if (customer != null)
                    CustomerName = customer.Name;
                CmcsTransportCompany company = Dbers.GetInstance().SelfDber.Get<CmcsTransportCompany>(this._BuyFuelTransport.TransportCompanyId);
                if (company != null)
                    CompanyName = company.Name;
                GrossTime = DisposeTime(this._BuyFuelTransport.GrossTime.ToString(), "yyyy-MM-dd HH:mm");
                TareTime = DisposeTime(this._BuyFuelTransport.TareTime.ToString(), "yyyy-MM-dd HH:mm");
                GrossWeight = this._BuyFuelTransport.GrossWeight.ToString("F2").PadLeft(6, ' ');
                TareWeight = this._BuyFuelTransport.TareWeight.ToString("F2").PadLeft(6, ' ');
                SuttleWeight = this._BuyFuelTransport.SuttleWeight.ToString("F2").PadLeft(6, ' ');

                #region 销售煤
                // 行间距 24 
                float TopValue = 53;
                string printValue = "";
                g.DrawString("河南煤炭储配交易中心有限公司", new Font("黑体", 16, FontStyle.Bold, GraphicsUnit.Pixel), Brushes.Black, 30, TopValue);
                TopValue += 34;

                g.DrawString("打印时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm"), ContentFont, Brushes.Black, 30, TopValue);
                TopValue += 24;

                g.DrawLine(new Pen(Color.Black, 2), 0, TopValue, 300 - 10, TopValue);
                TopValue += 15;

                g.DrawString("车 牌 号：" + CarNumber, ContentFont, Brushes.Black, 30, TopValue);
                TopValue += 24;

                printValue = printValue = "发 货 方：鹤壁园区";
                if (printValue.Length > 19)
                {
                    g.DrawString(printValue.Substring(0, 19), ContentFont, Brushes.Black, 30, TopValue);
                    TopValue += 24;
                    g.DrawString(printValue.Substring(19, printValue.Length - 19), ContentFont, Brushes.Black, 105, TopValue);
                    TopValue += 24;
                }
                else
                {
                    g.DrawString(printValue, ContentFont, Brushes.Black, 30, TopValue);
                    TopValue += 24;
                }

                printValue = printValue = "收货单位：" + CustomerName;
                if (printValue.Length > 19)
                {
                    g.DrawString(printValue.Substring(0, 19), ContentFont, Brushes.Black, 30, TopValue);
                    TopValue += 24;
                    g.DrawString(printValue.Substring(19, printValue.Length - 19), ContentFont, Brushes.Black, 105, TopValue);
                    TopValue += 24;
                }
                else
                {
                    g.DrawString(printValue, ContentFont, Brushes.Black, 30, TopValue);
                    TopValue += 24;
                }

                printValue = printValue = "运输单位：" + CompanyName;
                if (printValue.Length > 19)
                {
                    g.DrawString(printValue.Substring(0, 19), ContentFont, Brushes.Black, 30, TopValue);
                    TopValue += 24;
                    g.DrawString(printValue.Substring(19, printValue.Length - 19), ContentFont, Brushes.Black, 105, TopValue);
                    TopValue += 24;
                }
                else
                {
                    g.DrawString(printValue, ContentFont, Brushes.Black, 30, TopValue);
                    TopValue += 24;
                }

                g.DrawString("煤    种：" + FuelKindName, ContentFont, Brushes.Black, 30, TopValue);
                TopValue += 24;

                g.DrawString(string.Format("毛    重：{0} 吨", GrossWeight), ContentFont, Brushes.Black, 30, TopValue);
                TopValue += 24;

                g.DrawString(string.Format("皮    重：{0} 吨", TareWeight), ContentFont, Brushes.Black, 30, TopValue);
                TopValue += 24;

                g.DrawString(string.Format("净    重：{0} 吨", SuttleWeight), ContentFont, Brushes.Black, 30, TopValue);
                TopValue += 24;

                g.DrawString(string.Format("操 作 员：{0}", SelfVars.LoginUser.UserName), ContentFont, Brushes.Black, 30, TopValue);
                TopValue += 24;

                g.DrawString(string.Format("签    字："), ContentFont, Brushes.Black, 30, TopValue);
                TopValue += 34;

                // g.DrawString(PageIndex.ToString() + "联", ContentFont, Brushes.Black, 110, TopValue);
                //TopValue += 24;
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

        private void InitializeComponent()
        {
            this.SuspendLayout();
            // 
            // WagonPrinter
            // 
            this.ClientSize = new System.Drawing.Size(362, 227);
            this.DoubleBuffered = true;
            this.Name = "WagonPrinter";
            this.ResumeLayout(false);

        }
    }
}
