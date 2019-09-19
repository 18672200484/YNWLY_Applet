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
//
using AForge;
using AForge.Video;
using AForge.Video.DirectShow;
using AForge.Imaging;
using AForge.Imaging.Filters;
using System.Drawing.Imaging;
using System.IO;
using CMCS.MobilePad.Win.Frms.CarBreakRules;

namespace CMCS.MobilePad.Win.Frms.Camera
{
    public partial class FrmCamera : DevComponents.DotNetBar.Metro.MetroForm
    {
        private FilterInfoCollection videoDevices;
        private int Index;
        public System.Drawing.Image Out;
        public FrmCarBreakRules_Confirm ParentFrom;
        public string FileName;
        public FrmCamera(FrmCarBreakRules_Confirm from, string fileName)
        {
            InitializeComponent();
            this.ParentFrom = from;
            this.FileName = fileName;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }

        private void FrmCamera_Load(object sender, EventArgs e)
        {
            int crmeraCount = InitCamera();
            this.Index = crmeraCount == 1 ? 0 : 1;
            CameraConn(Index);
        }

        /// <summary>
        /// 初始化摄像头
        /// </summary>
        private int InitCamera()
        {
            try
            {
                // 枚举所有视频输入设备
                videoDevices = new FilterInfoCollection(FilterCategory.VideoInputDevice);

                if (videoDevices.Count == 0)
                    throw new ApplicationException();

                //foreach (FilterInfo device in videoDevices)
                //{
                //    ddlPoleCode.Items.Add(device.Name);
                //}
            }
            catch (ApplicationException)
            {
                videoDevices = null;
            }
            return videoDevices.Count;
        }

        /// <summary>
        /// 连接摄像头
        /// </summary>
        private void CameraConn(int index)
        {
            VideoCaptureDevice videoSource = new VideoCaptureDevice(videoDevices[index].MonikerString);
            videoSource.DesiredFrameSize = new Size(320, 240);
            videoSource.DesiredFrameRate = 1;

            videPlayer.VideoSource = videoSource;
            videPlayer.Start();
        }

        /// <summary>
        /// 断开连接
        /// </summary>
        private void CameraClose()
        {
            videPlayer.SignalToStop();
            videPlayer.WaitForStop();
        }

        /// <summary>
        /// 拍摄
        /// </summary>
        /// <returns></returns>
        private System.Drawing.Image Shoot()
        {
            System.Drawing.Image img = new Bitmap(videPlayer.Width, videPlayer.Height);
            videPlayer.DrawToBitmap((Bitmap)img, new Rectangle(0, 0, videPlayer.Width, videPlayer.Height));
            string filePath = AppDomain.CurrentDomain.BaseDirectory + @"/Camera/";
            if (!Directory.Exists(filePath))
                Directory.CreateDirectory(filePath);
            img.Save(filePath + this.FileName + ".bmp", ImageFormat.Bmp);
            return img;
        }

        private void btnChange_Click(object sender, EventArgs e)
        {
            Index = Index == 0 ? 1 : 0;
            CameraClose();
            CameraConn(Index);
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            this.ParentFrom.ImageOut = Shoot();
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void FrmCamera_FormClosed(object sender, FormClosedEventArgs e)
        {
            CameraClose();
        }

    }
}