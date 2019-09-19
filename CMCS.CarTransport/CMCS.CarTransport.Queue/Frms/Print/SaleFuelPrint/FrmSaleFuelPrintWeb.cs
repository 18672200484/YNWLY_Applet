using System;
using System.Drawing;
using System.Windows.Forms;
//
using DevComponents.DotNetBar.Metro;
using CMCS.Common.Entities.CarTransport;
using CMCS.CarTransport.DAO;
using CMCS.Common.Enums;
using CMCS.CarTransport.Queue.Core;
using CMCS.Common.Entities.BaseInfo;
using CMCS.Common;
using CMCS.Common.Entities.Fuel;

namespace CMCS.CarTransport.Queue.Frms.Transport.Print.SaleFuelPrint
{
    /// <summary>
    /// 打印预览
    /// </summary>
    public partial class FrmSaleFuelPrintWeb : DevComponents.DotNetBar.Metro.MetroForm
    {
        WeighterDAO weighterDAO = WeighterDAO.GetInstance();

        SaleFuelWagonPrinter wagonPrinter = null;
        CmcsSaleFuelTransport _BuyFuelTransport = null;
        Font TitleFont = new Font("宋体", 24, FontStyle.Bold, GraphicsUnit.Pixel);
        Font ContentFont = new Font("宋体", 14, FontStyle.Regular, GraphicsUnit.Pixel);

        int PageIndex = 1;

        public FrmSaleFuelPrintWeb(CmcsSaleFuelTransport buyfueltransport)
        {
            _BuyFuelTransport = buyfueltransport;
            InitializeComponent();
        }

        /// <summary>
        /// 打印磅单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiPrint_Click(object sender, EventArgs e)
        {
            this.wagonPrinter.Print(this._BuyFuelTransport);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;

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
                g.DrawString("河南煤炭储配交易中心有限公司", new Font("黑体", 16, FontStyle.Bold, GraphicsUnit.Pixel), Brushes.White, 30, TopValue);
                TopValue += 34;

                g.DrawString("打印时间：" + DateTime.Now.ToString("yyyy-MM-dd HH:mm"), ContentFont, Brushes.White, 30, TopValue);
                TopValue += 24;

                g.DrawLine(new Pen(Color.White, 2), 0, TopValue, 300 - 10, TopValue);
                TopValue += 15;

                g.DrawString("车 牌 号：" + CarNumber, ContentFont, Brushes.White, 30, TopValue);
                TopValue += 24;

                printValue = printValue = "发 货 方：鹤壁园区";
                if (printValue.Length > 19)
                {
                    g.DrawString(printValue.Substring(0, 19), ContentFont, Brushes.White, 30, TopValue);
                    TopValue += 24;
                    g.DrawString(printValue.Substring(19, printValue.Length - 19), ContentFont, Brushes.White, 105, TopValue);
                    TopValue += 24;
                }
                else
                {
                    g.DrawString(printValue, ContentFont, Brushes.White, 30, TopValue);
                    TopValue += 24;
                }

                printValue = printValue = "收货单位：" + CustomerName;
                if (printValue.Length > 19)
                {
                    g.DrawString(printValue.Substring(0, 19), ContentFont, Brushes.White, 30, TopValue);
                    TopValue += 24;
                    g.DrawString(printValue.Substring(19, printValue.Length - 19), ContentFont, Brushes.White, 105, TopValue);
                    TopValue += 24;
                }
                else
                {
                    g.DrawString(printValue, ContentFont, Brushes.White, 30, TopValue);
                    TopValue += 24;
                }

                printValue = printValue = "运输单位：" + CompanyName;
                if (printValue.Length > 18)
                {
                    g.DrawString(printValue.Substring(0, 18), ContentFont, Brushes.White, 30, TopValue);
                    TopValue += 24;
                    g.DrawString(printValue.Substring(18, printValue.Length - 18), ContentFont, Brushes.White, 105, TopValue);
                    TopValue += 24;
                }
                else
                {
                    g.DrawString(printValue, ContentFont, Brushes.White, 30, TopValue);
                    TopValue += 24;
                }

                g.DrawString("煤    种：" + FuelKindName, ContentFont, Brushes.White, 30, TopValue);
                TopValue += 24;

                g.DrawString(string.Format("毛    重：{0} 吨", GrossWeight), ContentFont, Brushes.White, 30, TopValue);
                TopValue += 24;

                g.DrawString(string.Format("皮    重：{0} 吨", TareWeight), ContentFont, Brushes.White, 30, TopValue);
                TopValue += 24;

                g.DrawString(string.Format("净    重：{0} 吨", SuttleWeight), ContentFont, Brushes.White, 30, TopValue);
                TopValue += 24;

                g.DrawString(string.Format("操 作 员：{0}", SelfVars.LoginUser.UserName), ContentFont, Brushes.White, 30, TopValue);
                TopValue += 24;

                g.DrawString(string.Format("签    字："), ContentFont, Brushes.White, 30, TopValue);
                TopValue += 34;

                // g.DrawString(PageIndex.ToString() + "联", ContentFont, Brushes.White, 110, TopValue);
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
            this.wagonPrinter = new SaleFuelWagonPrinter(printDocument1);
        }
    }
}
