using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Printing;

namespace CMCS.DataTester.Frms
{
    public partial class ImagePrint : Form
    {
        Font TitleFont = new Font("宋体", 24, FontStyle.Bold, GraphicsUnit.Pixel);
        Font ContentFont = new Font("宋体", 14, FontStyle.Regular, GraphicsUnit.Pixel);
        PrintDocument _PrintDocument = null;
        Image Img = null;
        public ImagePrint(PrintDocument printDoc, Image img)
        {
            Img = img;
            this._PrintDocument = printDoc;
            this._PrintDocument.DefaultPageSettings.PaperSize = new PaperSize("Custum", 300, 300);
            this._PrintDocument.OriginAtMargins = true;
            this._PrintDocument.DefaultPageSettings.Margins.Left = 10;
            this._PrintDocument.DefaultPageSettings.Margins.Right = 0;
            this._PrintDocument.DefaultPageSettings.Margins.Top = 0;
            this._PrintDocument.DefaultPageSettings.Margins.Bottom = 0;
            this._PrintDocument.PrintController = new StandardPrintController();
            this._PrintDocument.PrintPage += _PrintDocument_PrintPage;
        }

        public void Print()
        {
            try
            {
                this._PrintDocument.Print();
            }
            catch
            {
                MessageBox.Show("打印异常，请检查打印机！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void _PrintDocument_PrintPage(object sender, PrintPageEventArgs e)
        {
            Graphics g = Graphics.FromImage(this.Img);
            g.DrawString("河南煤炭储配交易中心", new Font("黑体", 20, FontStyle.Bold, GraphicsUnit.Pixel), Brushes.Black, 30, 270);
        }

    }
}
