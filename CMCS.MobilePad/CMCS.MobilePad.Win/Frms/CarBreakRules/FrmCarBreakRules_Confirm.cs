using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using CMCS.Common.DAO;
using CMCS.Common.Entities.CarTransport;
using CMCS.Common.Enums;
using CMCS.Common.Entities.Sys;
using CMCS.MobilePad.Win.Frms.Camera;
using System.Drawing.Imaging;
using System.IO;
using CMCS.MobilePad.Win.Core;
using CMCS.CarTransport.DAO;

namespace CMCS.MobilePad.Win.Frms.CarBreakRules
{
    public partial class FrmCarBreakRules_Confirm : DevComponents.DotNetBar.Metro.MetroForm
    {
        CmcsBuyFuelTransport buyFuelTransport;
        CmcsSaleFuelTransport saleFuelTransport;
        CmcsBreakRules CurrentBreakRules;
        List<string> imageFileName;
        public Image ImageOut;
        string CurrentTransportId;
        string CurrentTransportType;
        string ImagePath;
        CommonDAO commonDAO = CommonDAO.GetInstance();

        public FrmCarBreakRules_Confirm(string transportId, string transportType)
        {
            InitializeComponent();

            this.CurrentTransportId = transportId;
            this.CurrentTransportType = transportType;
            imageFileName = new List<string>();
            ImagePath = AppDomain.CurrentDomain.BaseDirectory + @"/Camera/";

            if (this.CurrentTransportType.Contains("入场"))
            {
                buyFuelTransport = commonDAO.SelfDber.Get<CmcsBuyFuelTransport>(this.CurrentTransportId);
                if (buyFuelTransport != null)
                {
                    txtCarNum.Text = buyFuelTransport.CarNumber;
                    txtKGWeight.Text = buyFuelTransport.KgWeight.ToString();
                    txtKSWeight.Text = buyFuelTransport.KsWeight.ToString();
                }
            }
            else
            {
                saleFuelTransport = commonDAO.SelfDber.Get<CmcsSaleFuelTransport>(this.CurrentTransportId);
                if (saleFuelTransport != null)
                {
                    txtCarNum.Text = saleFuelTransport.CarNumber;
                }
            }
            CurrentBreakRules = commonDAO.SelfDber.Entity<CmcsBreakRules>("where TransportId=:TransportId order by CreateDate ", new { TransportId = this.CurrentTransportId });
            if (CurrentBreakRules != null)
            {
                cmbBreakRules.Text = CurrentBreakRules.BreakRulesType;
                cmbBreakRulesResult.Text = CurrentBreakRules.BreakRulesResult;
                LoadImage(CurrentBreakRules.Id);
            }

            LoadBreakRulesType();
            LoadBreakRulesResult();
        }

        private void LoadImage(string breakRulesId)
        {
            IList<CmcsTransportPicture> pictures = commonDAO.SelfDber.Entities<CmcsTransportPicture>("where TransportId=:TransportId and CaptureType = '违章拍照' order by CreateDate desc", new { TransportId = breakRulesId });
            foreach (CmcsTransportPicture item in pictures)
            {
                if (!File.Exists(Path.Combine(this.ImagePath, item.PicturePath + ".bmp")))
                    continue;
                Image CurrentImage = Image.FromFile(Path.Combine(this.ImagePath, item.PicturePath + ".bmp"));
                if (this.imageFileName.Count == 0)
                {
                    this.pictureBox1.Image = ZoomImage(CurrentImage, this.pictureBox1.Height, this.pictureBox1.Width + 10);
                    this.pictureBox1.Image.Tag = item.PicturePath;
                }
                else if (this.imageFileName.Count == 1)
                {
                    this.pictureBox2.Image = ZoomImage(CurrentImage, this.pictureBox2.Height, this.pictureBox2.Width + 10);
                    this.pictureBox2.Image.Tag = item.PicturePath;
                }
                else if (this.imageFileName.Count == 2)
                {
                    this.pictureBox3.Image = ZoomImage(CurrentImage, this.pictureBox3.Height, this.pictureBox3.Width + 10);
                    this.pictureBox3.Image.Tag = item.PicturePath;
                }
                this.imageFileName.Add(item.PicturePath);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.CurrentBreakRules == null)
            {
                CurrentBreakRules = new CmcsBreakRules();
                CurrentBreakRules.TransportId = this.CurrentTransportId;
                CurrentBreakRules.TransportType = this.CurrentTransportType;
                CurrentBreakRules.BreakRulesType = cmbBreakRules.Text;
                CurrentBreakRules.BreakRulesResult = cmbBreakRulesResult.Text;
                CurrentBreakRules.OperUser = SelfVars.LoginUser.UserName;
                commonDAO.SelfDber.Insert<CmcsBreakRules>(CurrentBreakRules);

                for (int i = 0; i < this.imageFileName.Count; i++)
                {
                    string imgName = imageFileName[i];
                    CmcsTransportPicture transportPicture = new CmcsTransportPicture();
                    transportPicture.TransportId = CurrentBreakRules.Id;
                    transportPicture.CaptureType = "违章拍照";
                    transportPicture.CaptureTime = DateTime.Now;
                    transportPicture.PicturePath = imgName;
                    commonDAO.SelfDber.Insert(transportPicture);
                }
            }
            else
            {
                CurrentBreakRules.TransportType = this.CurrentTransportType;
                CurrentBreakRules.BreakRulesType = cmbBreakRules.Text;
                CurrentBreakRules.BreakRulesResult = cmbBreakRulesResult.Text;
                CurrentBreakRules.OperDate = DateTime.Now;

                commonDAO.SelfDber.Update<CmcsBreakRules>(CurrentBreakRules);
                commonDAO.SelfDber.DeleteBySQL<CmcsTransportPicture>("where TransportId=:TransportId", new { TransportId = this.CurrentBreakRules.Id });
                for (int i = 0; i < this.imageFileName.Count; i++)
                {
                    string imgName = imageFileName[i];
                    CmcsTransportPicture transportPicture = new CmcsTransportPicture();
                    transportPicture.TransportId = CurrentBreakRules.Id;
                    transportPicture.CaptureType = "违章拍照";
                    transportPicture.CaptureTime = DateTime.Now;
                    transportPicture.PicturePath = imgName;
                    commonDAO.SelfDber.Insert(transportPicture);
                }
            }
            if (this.buyFuelTransport != null)
            {
                this.buyFuelTransport.KsWeight = Convert.ToDecimal(txtKSWeight.Value);
                this.buyFuelTransport.KgWeight = Convert.ToDecimal(txtKGWeight.Value);
                this.buyFuelTransport.Remark += Environment.NewLine + "违章处理扣吨";
                WeighterDAO.GetInstance().SaveBuyFuelTransport(buyFuelTransport);
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// 绑定违章类型
        /// </summary>
        private void LoadBreakRulesType()
        {
            //List<CodeContent> content = CommonDAO.GetInstance().GetCodeContentByKind("违章类型");
            //foreach (CodeContent item in content)
            //{
            //    this.cmbBreakRules.Items.Add(item.Content);
            //}

            this.cmbBreakRules.Items.Add("超载");
            this.cmbBreakRules.Items.Add("超速");
            this.cmbBreakRules.Items.Add("副驾驶未下车");
            this.cmbBreakRules.Items.Add("违规行驶");
            this.cmbBreakRules.SelectedIndex = 0;
        }

        /// <summary>
        /// 绑定违章处理结果
        /// </summary>
        private void LoadBreakRulesResult()
        {
            //List<CodeContent> content = CommonDAO.GetInstance().GetCodeContentByKind("违章处理结果");
            //foreach (CodeContent item in content)
            //{
            //    this.cmbBreakRules.Items.Add(item.Content);
            //}
            if (buyFuelTransport != null)
                this.cmbBreakRulesResult.Items.Add("扣吨");
            this.cmbBreakRulesResult.Items.Add("罚款");
            this.cmbBreakRulesResult.Items.Add("禁止入场");
            this.cmbBreakRulesResult.Items.Add("加入黑名单");
            this.cmbBreakRulesResult.SelectedIndex = 0;
        }

        /// <summary>
        /// 改变图片大小
        /// </summary>
        /// <param name="bitmap"></param>
        /// <param name="destHeight"></param>
        /// <param name="destWidth"></param>
        /// <returns></returns>
        private Image ZoomImage(Image bitmap, int destHeight, int destWidth)
        {
            try
            {
                System.Drawing.Image sourImage = bitmap;
                int width = 0, height = 0;
                //按比例缩放           
                int sourWidth = sourImage.Width;
                int sourHeight = sourImage.Height;
                if (sourHeight > destHeight || sourWidth > destWidth)
                {
                    if ((sourWidth * destHeight) > (sourHeight * destWidth))
                    {
                        width = destWidth;
                        height = (destWidth * sourHeight) / sourWidth;
                    }
                    else
                    {
                        height = destHeight;
                        width = (sourWidth * destHeight) / sourHeight;
                    }
                }
                else
                {
                    width = sourWidth;
                    height = sourHeight;
                }
                Bitmap destBitmap = new Bitmap(destWidth, destHeight);
                Graphics g = Graphics.FromImage(destBitmap);
                g.Clear(Color.Transparent);
                //设置画布的描绘质量         
                g.CompositingQuality = System.Drawing.Drawing2D.CompositingQuality.HighQuality;
                g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.HighQuality;
                g.InterpolationMode = System.Drawing.Drawing2D.InterpolationMode.HighQualityBicubic;
                g.DrawImage(sourImage, new Rectangle((destWidth - width) / 2, (destHeight - height) / 2, width, height), 0, 0, sourImage.Width, sourImage.Height, GraphicsUnit.Pixel);
                g.Dispose();
                //设置压缩质量     
                System.Drawing.Imaging.EncoderParameters encoderParams = new System.Drawing.Imaging.EncoderParameters();
                long[] quality = new long[1];
                quality[0] = 100;
                System.Drawing.Imaging.EncoderParameter encoderParam = new System.Drawing.Imaging.EncoderParameter(System.Drawing.Imaging.Encoder.Quality, quality);
                encoderParams.Param[0] = encoderParam;
                sourImage.Dispose();
                return destBitmap;
            }
            catch
            {
                return bitmap;
            }
        }

        private void btn_Shoot_Click(object sender, EventArgs e)
        {
            if (this.imageFileName.Count == 3)
            {
                MessageBoxEx.Show("最多添加3张图片", "提示");
                return;
            }
            string imageName = DateTime.Now.ToString("yyMMddHHmmssffff") + this.txtCarNum.Text;

            FrmCamera frm = new FrmCamera(this, imageName);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                if (this.pictureBox1.Image == null)
                {
                    this.pictureBox1.Image = ZoomImage(this.ImageOut, this.pictureBox1.Height, this.pictureBox1.Width + 10);
                    this.pictureBox1.Image.Tag = imageName;
                }
                else if (this.pictureBox2.Image == null)
                {
                    this.pictureBox2.Image = ZoomImage(this.ImageOut, this.pictureBox1.Height, this.pictureBox1.Width);
                    this.pictureBox2.Image.Tag = imageName;
                }
                else if (this.pictureBox3.Image == null)
                {
                    this.pictureBox3.Image = ZoomImage(this.ImageOut, this.pictureBox1.Height, this.pictureBox1.Width);
                    this.pictureBox3.Image.Tag = imageName;
                }
                this.imageFileName.Add(imageName);
            }
        }

        private void btn_Select_Click(object sender, EventArgs e)
        {
            if (this.imageFileName.Count == 3)
            {
                MessageBoxEx.Show("最多添加3张图片", "提示");
                return;
            }
            OpenFileDialog fileDialog = new OpenFileDialog();
            DialogResult result = fileDialog.ShowDialog();

            string imageName = DateTime.Now.ToString("yyMMddHHmmssffff") + this.txtCarNum.Text;

            if (result == DialogResult.OK)
            {
                if (this.pictureBox1.Image == null)
                {
                    this.pictureBox1.Image = ZoomImage(Image.FromFile(fileDialog.FileName), this.pictureBox1.Height, this.pictureBox1.Width);
                    this.pictureBox1.Image.Tag = imageName;
                }
                else if (this.pictureBox2.Image == null)
                {
                    this.pictureBox2.Image = ZoomImage(Image.FromFile(fileDialog.FileName), this.pictureBox2.Height, this.pictureBox2.Width);
                    this.pictureBox2.Image.Tag = imageName;
                }
                else if (this.pictureBox3.Image == null)
                {
                    this.pictureBox3.Image = ZoomImage(Image.FromFile(fileDialog.FileName), this.pictureBox3.Height, this.pictureBox3.Width);
                    this.pictureBox3.Image.Tag = imageName;
                }
                this.imageFileName.Add(imageName);
                Image img = Image.FromFile(fileDialog.FileName);

                if (!Directory.Exists(ImagePath))
                    Directory.CreateDirectory(ImagePath);
                img.Save(ImagePath + imageName + ".bmp", ImageFormat.Bmp);
            }
        }

        private void pictureBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            PictureBox pictureBox = (PictureBox)sender;
            if (pictureBox.Image == null) return;
            this.imageFileName.Remove(pictureBox.Image.Tag.ToString());
            File.Delete(Path.Combine(this.ImagePath, pictureBox.Image.Tag + ".bmp"));
            pictureBox.Image = null;
        }

        private void cmbBreakRulesResult_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbBreakRulesResult.Text == "扣吨")
            {
                lbl_KgWeight.Visible = true;
                lbl_KsWeight.Visible = true;
                txtKGWeight.Visible = true;
                txtKSWeight.Visible = true;
            }
            else
            {
                lbl_KgWeight.Visible = false;
                lbl_KsWeight.Visible = false;
                txtKGWeight.Visible = false;
                txtKSWeight.Visible = false;
            }
        }
    }
}