using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.IO;
using System.Linq;
//
using CMCS.Common.DAO;
using CMCS.WeighCheck.DAO;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using DevComponents.DotNetBar.Metro;
using CMCS.Common.Utilities;
using CMCS.Common.Entities.Fuel;
using DevComponents.DotNetBar.SuperGrid;
using RW.HFReader;
using CMCS.WeighCheck.MakeChange.Enums;

using ThoughtWorks.QRCode.Codec;
using CMCS.WeighCheck.MakeChange.Utilities;


namespace CMCS.WeighCheck.MakeChange.Frms
{
    public partial class FrmSpotCheck : MetroForm
    {
        public FrmSpotCheck()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体唯一标识符
        /// </summary>
        public static string UniqueKey = "FrmSpotCheck";

        #region 业务处理类

        CommonDAO commonDAO = CommonDAO.GetInstance();
        CZYHandlerDAO czyHandlerDAO = CZYHandlerDAO.GetInstance();

        #endregion

        #region Vars

        CodePrinter _CodePrinter = null;
        QRCodePrinter _QRCodePrinter = null;

        /// <summary>
        /// 当前抽查样记录
        /// </summary>
        public CmcsRCAssay CurrentAssay;

        /// <summary>
        /// 当前化验记录
        /// </summary>
        IList<CmcsRCAssay> CurrentAssays = null;

        /// <summary>
        /// 当前化验记录
        /// </summary>
        SpotAssay CurrentSpotAssay = null;

        /// <summary>
        /// 当前化验记录
        /// </summary>
        IList<SpotAssay> CurrentSpotAssays = null;

        string resMessage = string.Empty;

        #endregion

        /// <summary>
        /// 初始化
        /// </summary>
        public void InitFrom()
        {
            //superGridControl1.PrimaryGrid.ColumnAutoSizeMode = ColumnAutoSizeMode.AllCells;

            this._CodePrinter = new CodePrinter(printDocument1);
            this._QRCodePrinter = new QRCodePrinter(printDocument1);

            LoadDetail();
        }

        private void FrmSpotCheck_Load(object sender, EventArgs e)
        {
            // 初始化
            InitFrom();
            this.Focus();
            btnReset_Click(null, null);
        }

        #region 业务

        /// <summary>
        /// 根据制样码加载化验码及制样明细
        /// </summary>
        /// <param name="makeCode"></param>
        private void LoadCode()
        {
            txtInputSpotAssayCode.Text = czyHandlerDAO.GetSpotAssayCodeByAssayCode(txtInputAssayCode.Text);
            this.CurrentAssay = commonDAO.SelfDber.Entity<CmcsRCAssay>("where AssayCode=:AssayCode", new { AssayCode = this.txtInputSpotAssayCode.Text });

            #region DotNetBar
            this.picEncode.Image = QRCodeBar(this.txtInputSpotAssayCode.Text.Trim());
            this.picEncode.Width = 200;
            this.picEncode.Height = 200;
            #endregion

            #region 第三方插件
            //this.picEncode.Image = QRCode(this.txtInputAssayCode.Text.Trim(), 4);
            //picEncode.Width = this.picEncode.Image.Width;
            //picEncode.Height = this.picEncode.Image.Height;
            #endregion

            LoadDetail();
        }

        #endregion

        #region 操作

        #region 检测按键
        /// <summary>
        /// 键入Enter检测有效性
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtInputMakeCode_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)//加载
            {
                LoadCode();
            }
            else if (e.KeyCode == Keys.Escape)//重置
            {
                btnReset_Click(null, null);
            }
        }

        /// <summary>
        /// 文本改变事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtInputMakeCode_TextChanged(object sender, EventArgs e)
        {
            txtInputAssayCode.Text = txtInputAssayCode.Text.Trim().ToUpper();
            txtInputAssayCode.SelectionStart = txtInputAssayCode.Text.Length;

            if (txtInputAssayCode.Text.Trim().Length == 12)//化验码
            {
                LoadCode();
            }
            else
            {
                txtInputSpotAssayCode.ResetText();
                picEncode.Image = null;
            }
        }

        private void txtInputMakeCode_MouseMove(object sender, MouseEventArgs e)
        {
            // 方便客户快速使用，获取焦点
            if (!txtInputAssayCode.Focused)
            {
                FoucsAndSelect();
            }
        }

        /// <summary>
        /// 重绘事件 用于获取焦点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmMakeChange_Paint(object sender, PaintEventArgs e)
        {
            FoucsAndSelect();
        }

        /// <summary>
        /// 获取焦点并选中
        /// </summary>
        private void FoucsAndSelect()
        {
            txtInputAssayCode.Focus();
            txtInputAssayCode.SelectAll();
        }
        #endregion

        /// <summary>
        /// 加载明细记录
        /// </summary>
        private void LoadDetail()
        {
            IList<CmcsRCAssay> rCMakeDetails = czyHandlerDAO.GetSpotAssayByDate(DateTime.Now);
            IList<SpotAssay> MakeDetails = DataChange(rCMakeDetails);
            superGridControl1.PrimaryGrid.DataSource = MakeDetails;

            if (this.CurrentAssay == null) return;
            this.CurrentSpotAssay = DataChange(this.CurrentAssay);

            if (MakeDetails.Select(a => a.AssayId).ToList().IndexOf(this.CurrentSpotAssay.AssayId) == -1)
            {
                MakeDetails.Insert(0, CurrentSpotAssay);
                superGridControl1.PrimaryGrid.DataSource = MakeDetails;
            }
            else
            {
                MakeDetails = DataChange(rCMakeDetails);
                superGridControl1.PrimaryGrid.DataSource = MakeDetails;
                int index = MakeDetails.Select(a => a.AssayId).ToList().IndexOf(this.CurrentSpotAssay.AssayId);
                ((GridRow)superGridControl1.PrimaryGrid.Rows[index + 1]).CellStyles.Default.TextColor = Color.Blue;
            }
        }
        #endregion

        #region 其他

        private void ShowMessage(string info, eOutputType outputType)
        {
            OutputRunInfo(rtxtMakeWeightInfo, info, outputType);
        }

        /// <summary>
        /// 输出运行信息
        /// </summary>
        /// <param name="richTextBox"></param>
        /// <param name="text"></param>
        /// <param name="outputType"></param>
        private void OutputRunInfo(RichTextBoxEx richTextBox, string text, eOutputType outputType = eOutputType.Normal)
        {
            this.Invoke((EventHandler)(delegate
            {
                if (richTextBox.TextLength > 100000) richTextBox.Clear();

                text = string.Format("{0}  {1}", DateTime.Now.ToString("HH:mm:ss"), text);

                richTextBox.SelectionStart = richTextBox.TextLength;

                switch (outputType)
                {
                    case eOutputType.Normal:
                        richTextBox.SelectionColor = ColorTranslator.FromHtml("#BD86FA");
                        break;
                    case eOutputType.Important:
                        richTextBox.SelectionColor = ColorTranslator.FromHtml("#A50081");
                        break;
                    case eOutputType.Warn:
                        richTextBox.SelectionColor = ColorTranslator.FromHtml("#F9C916");
                        break;
                    case eOutputType.Error:
                        richTextBox.SelectionColor = ColorTranslator.FromHtml("#DB2606");
                        break;
                    default:
                        richTextBox.SelectionColor = Color.White;
                        break;
                }

                richTextBox.AppendText(string.Format("{0}\r", text));

                richTextBox.ScrollToCaret();

            }));
        }

        /// <summary>
        /// 输出信息类型
        /// </summary>
        public enum eOutputType
        {
            /// <summary>
            /// 普通
            /// </summary>
            [Description("#BD86FA")]
            Normal,
            /// <summary>
            /// 重要
            /// </summary>
            [Description("#A50081")]
            Important,
            /// <summary>
            /// 警告
            /// </summary>
            [Description("#F9C916")]
            Warn,
            /// <summary>
            /// 错误
            /// </summary>
            [Description("#DB2606")]
            Error
        }

        /// <summary>
        /// Invoke封装
        /// </summary>
        /// <param name="action"></param>
        public void InvokeEx(Action action)
        {
            if (this.IsDisposed || !this.IsHandleCreated) return;

            this.Invoke(action);
        }

        #endregion

        #region 二维码
        /// <summary>
        /// 生成二维码 DotNetBar自带插件
        /// </summary>
        /// <param name="data"></param>
        private Image QRCodeBar(string data)
        {
            DotNetBarcode bc = new DotNetBarcode();
            bc.Type = DotNetBarcode.Types.QRCode;
            bc.PrintCheckDigitChar = true;
            bc.PrintChar = true;
            bc.PrintCheckDigitChar = true;
            Bitmap btm = new Bitmap(this.picEncode.Width, this.picEncode.Height);
            Graphics g = Graphics.FromImage(btm);
            bc.WriteBar(data, 0, 0, this.picEncode.Width + 5, this.picEncode.Width + 5, g);
            Font FontContent = new Font("宋体", 16, FontStyle.Bold);
            float titleWidth = g.MeasureString(data, FontContent).Width;
            g.DrawString(data, FontContent, Brushes.Black, new PointF((this.picEncode.Width - titleWidth) / 2, this.picEncode.Height - 25));
            MemoryStream ms = new MemoryStream();
            btm.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            Image image = Image.FromStream(ms);

            return image;
        }

        /// <summary>
        /// 生成二维码 第三方插件
        /// </summary>
        /// <param name="data">待生成的文字</param>
        /// <param name="scale">二维码大小</param>
        /// <param name="encoding">编码格式</param>
        /// <param name="version">版本</param>
        /// <param name="correctionLever">编码纠正错误</param>
        /// <param name="txtLogo">Logo路径</param>
        private Image QRCode(string data, int scale = 4, string encoding = "Byte", int version = 7, string correctionLever = "H", string txtLogo = "")
        {
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
            qrCodeEncoder.QRCodeForegroundColor = Color.Black;//设置二维码前景色
            qrCodeEncoder.QRCodeBackgroundColor = Color.White;//设置二维码背景色
            Image image = qrCodeEncoder.Encode(data, Encoding.UTF8);//生成二维码图片
            txtLogo = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logo.jpg");
            //if (txtLogo.Trim() != string.Empty)//如果有logo的话则添加logo
            //{
            //    Bitmap btm = new Bitmap(txtLogo);
            //    Bitmap copyImage = new Bitmap(btm, image.Width / 5 + 5, image.Height / 5 + 5);

            //    Graphics g = Graphics.FromImage(image);
            //    g.SmoothingMode = SmoothingMode.HighQuality;
            //    g.CompositingQuality = CompositingQuality.HighQuality;
            //    g.InterpolationMode = InterpolationMode.HighQualityBicubic;
            //    int x = image.Width / 2 - copyImage.Width / 2;
            //    int y = image.Height / 2 - copyImage.Height / 2;
            //    g.DrawImage(copyImage, x, y);
            //}
            //return image;
            Font font = new Font("宋体", scale * 4, FontStyle.Bold);

            //文字嵌入图片内  影响文字查看
            //Bitmap bmp = new Bitmap(image, image.Width, image.Height + 10);
            //Graphics gs = Graphics.FromImage(bmp);

            //SolidBrush sbrush = new SolidBrush(btnQRCodeForegroundColor.BackColor);
            //gs.DrawString(txtData.Text, font, sbrush, new PointF(5, image.Height - 10));
            //MemoryStream ms = new MemoryStream();
            //bmp.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            //picEncode.Image = Image.FromStream(ms);

            Bitmap bmp = TextToBitmap(data, font, new Rectangle(0, 0, image.Width, scale * 5 + 5), Color.Black, Color.White);

            //文字图片嵌入图片内 影响图片查看
            //Graphics gs = Graphics.FromImage(image);
            //gs.DrawImage(bmp, (image.Width - bmp.Width) / 2, image.Height - bmp.Height);

            //文字图片与原图片合成新的图片
            Bitmap newbmp = new Bitmap(image.Width, image.Height + bmp.Height);
            //Bitmap newbmp = new Bitmap(picEncode.Width, picEncode.Height);
            Graphics gsnew = Graphics.FromImage(newbmp);
            gsnew.SmoothingMode = SmoothingMode.HighQuality;
            gsnew.CompositingQuality = CompositingQuality.HighQuality;
            gsnew.InterpolationMode = InterpolationMode.HighQualityBicubic;
            gsnew.DrawImage(image, 0, 0);
            gsnew.DrawImage(bmp, 0, image.Height);
            MemoryStream ms = new MemoryStream();
            newbmp.Save(ms, System.Drawing.Imaging.ImageFormat.Bmp);
            return Image.FromStream(ms);

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
            //计算绘制文字所需的区域大小（根据宽度计算长度），重新创建矩形区域绘图
            SizeF sizef = new SizeF();
            if (rect == Rectangle.Empty)
            {
                bmp = new Bitmap(1, 1);
                g = Graphics.FromImage(bmp);
                sizef = g.MeasureString(text, font, PointF.Empty, format);
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
            sizef = g.MeasureString(text, font, PointF.Empty, format);
            //使用ClearType字体功能
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.ClearTypeGridFit;
            g.SmoothingMode = SmoothingMode.HighQuality;
            g.CompositingQuality = CompositingQuality.HighQuality;
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.FillRectangle(new SolidBrush(backColor), rect);
            g.DrawString(text, font, Brushes.Black, (bmp.Width - sizef.Width) / 2, 0);
            return bmp;
        }
        #endregion

        #region Button事件

        /// <summary>
        /// 重置
        /// </summary>
        private void Restet()
        {
            txtInputAssayCode.ButtonCustom.Enabled = false;
            txtInputAssayCode.ResetText();
            txtInputSpotAssayCode.ResetText();
            picEncode.Image = null;
            superGridControl1.PrimaryGrid.DataSource = null;
            FoucsAndSelect();

            LoadDetail();
        }

        /// <summary>
        /// 打印二维码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            _CodePrinter.Print(this.txtInputSpotAssayCode.Text.Trim());
            LoadDetail();
            FoucsAndSelect();
        }

        /// <summary>
        /// 重置
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_Click(object sender, EventArgs e)
        {
            Restet();
        }

        /// <summary>
        /// 生成抽查样
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_SpotCheck_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtInputAssayCode.Text))
            {
                MessageBoxEx.Show("请先输入化验码", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            FrmAssayTypeSelect frm = new FrmAssayTypeSelect(this, this.txtInputAssayCode.Text);
            frm.ShowDialog();
            LoadCode();
        }
        #endregion

        #region DataGridView

        private void superGridControl1_BeginEdit(object sender, GridEditEventArgs e)
        {
            // 取消编辑
            e.Cancel = true;
        }

        /// <summary>
        /// 设置行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void superGridControl_GetRowHeaderText(object sender, DevComponents.DotNetBar.SuperGrid.GridGetRowHeaderTextEventArgs e)
        {
            e.Text = (e.GridRow.RowIndex + 1).ToString();
        }

        #endregion

        #region 数据转换

        /// <summary>
        /// 临时类转换
        /// </summary>
        /// <param name="rCMakeDetail"></param>
        /// <returns></returns>
        private IList<SpotAssay> DataChange(IList<CmcsRCAssay> rCAssay)
        {
            IList<SpotAssay> spotAssays = new List<SpotAssay>();
            foreach (CmcsRCAssay item in rCAssay)
            {
                SpotAssay entity = new SpotAssay();
                CmcsRCAssay assay_old = czyHandlerDAO.GetAssayBySpotMakeId(item.MakeId);
                if (assay_old != null)
                {
                    entity.AssayCode = assay_old.AssayCode;
                }
                entity.SpotAssayCode = item.AssayCode;
                entity.AssayId = item.Id;
                entity.AssayPoint = item.AssayPoint;
                try
                {
                    entity.BackBatchDate = item.TheRcMake.TheRcSampling.TheInFactoryBatch.BACKBATCHDATE.ToString("yyyy-MM-dd HH:mm:ss");
                }
                catch
                {
                    entity.BackBatchDate = item.TheRcMake.UseTime.ToString("yyyy-MM-dd HH:mm:ss");
                }
                entity.CheckUser = item.AssayPle;
                entity.SpotCount = czyHandlerDAO.GetSpotCountBySpotMakeId(item.TheRcMake.Id, item.AssayCode);
                entity.CheckUser = item.AssayPle;

                spotAssays.Add(entity);
            }

            return spotAssays;
        }

        /// <summary>
        /// 临时类转换
        /// </summary>
        /// <param name="rCMakeDetail"></param>
        /// <returns></returns>
        private SpotAssay DataChange(CmcsRCAssay rCAssay)
        {
            SpotAssay entity = new SpotAssay();
            CmcsRCAssay assay_old = czyHandlerDAO.GetAssayBySpotMakeId(rCAssay.MakeId);
            if (assay_old != null)
            {
                entity.AssayCode = assay_old.AssayCode;
            }
            entity.SpotAssayCode = rCAssay.AssayCode;
            entity.AssayId = rCAssay.Id;
            entity.AssayPoint = rCAssay.AssayPoint;
            entity.BackBatchDate = rCAssay.TheRcMake.TheRcSampling.TheInFactoryBatch.BACKBATCHDATE.ToString("yyyyMMdd HH:mm:ss");
            entity.CheckUser = rCAssay.AssayPle;
            entity.SpotCount = czyHandlerDAO.GetSpotCountBySpotMakeId(rCAssay.TheRcMake.Id, rCAssay.AssayCode);
            entity.CheckUser = rCAssay.AssayPle;

            return entity;
        }

        /// <summary>
        /// 数据类型转换
        /// </summary>
        /// <param name="weight"></param>
        /// <returns></returns>
        private double StringToDouble(string weight)
        {
            try
            {
                return Convert.ToDouble(weight);
            }
            catch (Exception)
            {
                return 0;
            }
        }

        private DateTime StringToDate(string date)
        {
            try
            {
                return Convert.ToDateTime(date);
            }
            catch (Exception)
            {
                return DateTime.MinValue;
            }
        }
        #endregion
    }

    class SpotAssay
    {
        /// <summary>
        /// 化验Id
        /// </summary>
        public string AssayId { get; set; }

        /// <summary>
        /// 化验码
        /// </summary>
        public string AssayCode { get; set; }

        /// <summary>
        /// 归批日期
        /// </summary>
        public string BackBatchDate { get; set; }

        /// <summary>
        /// 抽查样编码
        /// </summary>
        public string SpotAssayCode { get; set; }

        /// <summary>
        /// 抽查次数
        /// </summary>
        public string SpotCount { get; set; }

        /// <summary>
        /// 操作人
        /// </summary>
        public string CheckUser { get; set; }

        private string _AssayPoint = "日常分析";
        /// <summary>
        /// 化验指标
        /// </summary>
        public string AssayPoint
        {
            get { return _AssayPoint; }
            set { _AssayPoint = value; }
        }
    }
}
