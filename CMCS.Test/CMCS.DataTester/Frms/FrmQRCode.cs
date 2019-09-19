using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//
using ThinkCameraSDK.Core;
using RW.HFReader;
using ThoughtWorks.QRCode.Codec;
using ThoughtWorks.QRCode.Codec.Data;
using System.IO;
using System.Net;

namespace CMCS.DataTester.Frms
{
    public partial class FrmQRCode : Form
    {
        HFReaderRwer hfReader = new HFReaderRwer();
        public FrmQRCode()
        {
            InitializeComponent();
            string[] cbversion = new string[41];
            for (int i = 0; i <= 40; i++)
            {
                cbversion[i] = i.ToString();
            }
            cbVersion.DataSource = cbversion;
            cbVersion.Text = "7";
            cbEncoding.Text = "Byte";
            cbCorrectionLevel.Text = "M";
            btnQRCodeForegroundColor.BackColor = Color.Black;
            btnQRCodeBackgroundColor.BackColor = Color.White;
        }

        private void btnEncode_Click(object sender, EventArgs e)
        {
            string encoding = cbEncoding.Text;
            string correctionLever = cbCorrectionLevel.Text;
            int version = Convert.ToInt32(cbVersion.Text);
            int scale;
            if (!int.TryParse(txtScale.Text.Trim(), out scale))
            {
                MessageBox.Show("Scale必须为数字");
                return;
            }
            string data = txtData.Text.Trim();
            if (data == string.Empty)
            {
                MessageBox.Show("数据不能为空，请输入数据哦！");
                return;
            }
            QRCodeEncoder qrCodeEncoder = new QRCodeEncoder();//创建一个对象
            switch (encoding)//设置编码模式 
            {
                case "Byte":
                    qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.BYTE;
                    break;

                case "AlphaNumeric":
                    qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.ALPHA_NUMERIC;
                    break;

                case "Numeric":
                    qrCodeEncoder.QRCodeEncodeMode = QRCodeEncoder.ENCODE_MODE.NUMERIC;
                    break;
            }
            //设置编码测量度
            qrCodeEncoder.QRCodeScale = scale;
            //设置编码版本
            qrCodeEncoder.QRCodeVersion = version;
            if (correctionLever == "L")//设置编码错误纠正
            {
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.L;
            }
            else if (correctionLever == "M")
            {
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.M;
            }
            else if (correctionLever == "Q")
            {
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.Q;
            }
            else if (correctionLever == "H")
            {
                qrCodeEncoder.QRCodeErrorCorrect = QRCodeEncoder.ERROR_CORRECTION.H;
            }
            qrCodeEncoder.QRCodeForegroundColor = btnQRCodeForegroundColor.BackColor;//设置二维码前景色
            qrCodeEncoder.QRCodeBackgroundColor = btnQRCodeBackgroundColor.BackColor;//设置二维码背景色
            Image image = qrCodeEncoder.Encode(data, Encoding.UTF8);//生成二维码图片

            if (txtLogo.Text.Trim() != string.Empty)//如果有logo的话则添加logo
            {
                Bitmap btm = new Bitmap(txtLogo.Text);
                Bitmap copyImage = new Bitmap(btm, image.Width / 5, image.Height / 5);
                Graphics g = Graphics.FromImage(image);
                int x = image.Width / 2 - copyImage.Width / 2;
                int y = image.Height / 2 - copyImage.Height / 2;
                g.DrawImage(copyImage, x, y);
            }
            picEncode.Image = image;

            Font font = new Font("宋体", 16, FontStyle.Bold);

            //文字嵌入图片内  影响文字查看
            //Bitmap bmp = new Bitmap(image, image.Width, image.Height + 10);
            //Graphics gs = Graphics.FromImage(bmp);

            //SolidBrush sbrush = new SolidBrush(btnQRCodeForegroundColor.BackColor);
            //gs.DrawString(txtData.Text, font, sbrush, new PointF(5, image.Height - 10));
            //MemoryStream ms = new MemoryStream();
            //bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            //picEncode.Image = Image.FromStream(ms);

            Bitmap bmp = TextToBitmap(txtData.Text, font, Rectangle.Empty, btnQRCodeForegroundColor.BackColor, btnQRCodeForegroundColor.BackColor);

            //文字图片嵌入图片内 影响图片查看
            //Graphics gs = Graphics.FromImage(image);
            //gs.DrawImage(bmp, (image.Width - bmp.Width) / 2, image.Height - bmp.Height);

            //文字图片与原图片合成新的图片
            Bitmap newbmp = new Bitmap(image.Width, image.Height + image.Height);
            Graphics gsnew = Graphics.FromImage(newbmp);
            gsnew.DrawImage(image, 0, 0);
            gsnew.DrawImage(bmp, (image.Width - bmp.Width) / 2, image.Height);
            MemoryStream ms = new MemoryStream();
            newbmp.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            picEncode.Image = Image.FromStream(ms);
        }

        private void btnDecode_Click(object sender, EventArgs e)
        {
            try
            {
                string decodedString = new QRCodeDecoder().decode(new QRCodeBitmapImage(new Bitmap(picEncode.Image)), Encoding.UTF8);
                txtData.Text = decodedString;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)//保存二维码到磁盘
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif|PNG Image|*.png";
            sfd.Title = "保存二维码";
            sfd.FileName = string.Empty;
            if (picEncode.Image != null)
            {
                if (sfd.ShowDialog() == DialogResult.OK && sfd.FileName != "")
                {
                    using (FileStream fs = (FileStream)sfd.OpenFile())
                    {
                        switch (sfd.FilterIndex)
                        {
                            case 1:
                                picEncode.Image.Save(fs, System.Drawing.Imaging.ImageFormat.Jpeg);
                                break;
                            case 2:
                                picEncode.Image.Save(fs, System.Drawing.Imaging.ImageFormat.Bmp);
                                break;
                            case 3:
                                picEncode.Image.Save(fs, System.Drawing.Imaging.ImageFormat.Gif);
                                break;
                            case 4:
                                picEncode.Image.Save(fs, System.Drawing.Imaging.ImageFormat.Png);
                                break;
                        }
                    }
                    MessageBox.Show("恭喜，保存成功！");
                }
            }
            else
            {
                MessageBox.Show("抱歉，没有要保存的图片哦！");
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            ImagePrint imgprint = new ImagePrint(printDocument1, picEncode.Image);
            imgprint.Print();
        }

        private void btnOpen_Click(object sender, EventArgs e)//打开要选的logo文件
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif|PNG Image|*.png";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                String fileName = ofd.FileName;
                picEncode.Image = new Bitmap(fileName);

            }
        }

        private void btnChooseLogo_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "JPeg Image|*.jpg|Bitmap Image|*.bmp|Gif Image|*.gif|PNG Image|*.png";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                txtLogo.Text = ofd.FileName;
            }
        }

        private void btnQRCodeForegroundColor_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == DialogResult.OK)
            {
                btnQRCodeForegroundColor.BackColor = cd.Color;
            }
        }

        private void btnQRCodeBackgroundColor_Click(object sender, EventArgs e)
        {
            ColorDialog cd = new ColorDialog();
            if (cd.ShowDialog() == DialogResult.OK)
            {
                btnQRCodeBackgroundColor.BackColor = cd.Color;
            }
        }

        /// <summary>
        /// 把文字转换为Bitmap
        /// </summary>
        /// <param name="text">需要转换的文字</param>
        /// <param name="font">文字样式</param>
        /// <param name="rect">用于输出的矩形，文字在这个矩形内显示，为空时自动计算</param>
        /// <param name="fontcolor">字体颜色</param>
        /// <param name="backColor">背景颜色</param>
        /// <returns></returns>
        private Bitmap TextToBitmap(string text, Font font, Rectangle rect, Color fontcolor, Color backColor)
        {
            Graphics g;
            Bitmap bmp;
            StringFormat format = new StringFormat(StringFormatFlags.NoClip);
            if (rect == Rectangle.Empty)
            {
                bmp = new Bitmap(1, 1);
                g = Graphics.FromImage(bmp);
                //计算绘制文字所需的区域大小（根据宽度计算长度），重新创建矩形区域绘图
                SizeF sizef = g.MeasureString(text, font, PointF.Empty, format);

                int width = (int)(sizef.Width);
                int height = (int)(sizef.Height);
                rect = new Rectangle(0, 0, width, height);
                bmp.Dispose();

                bmp = new Bitmap(width, height);
            }
            else
            {
                bmp = new Bitmap(rect.Width, rect.Height);
            }

            g = Graphics.FromImage(bmp);

            //使用ClearType字体功能
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            g.FillRectangle(new SolidBrush(backColor), rect);
            g.DrawString(text, font, Brushes.Black, rect, format);
            return bmp;
        }
    }
}
