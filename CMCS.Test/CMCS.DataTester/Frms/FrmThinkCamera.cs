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

namespace CMCS.DataTester.Frms
{
    public partial class FrmThinkCamera : Form
    {
        ThinkCameraData thinkCamera = null;
        public FrmThinkCamera()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (thinkCamera.ConnectCamera(txtIp.Text, panel1.Handle))
            {
                MessageBox.Show("连接成功");
                txtCarNumber.Text = thinkCamera.CarNumber;
            }
            else
            {
                MessageBox.Show("连接失败"+thinkCamera.ErrorStr);
            }

        }

        private void btnCapture_Click(object sender, EventArgs e)
        {
            //if (thinkCamera.Capture())
            //    MessageBox.Show("抓拍成功");
            //else
            //    MessageBox.Show("抓拍失败" + thinkCamera.ErrorStr);
        }

        private void FrmThinkCamera_Load(object sender, EventArgs e)
        {
            thinkCamera = new ThinkCameraData();
            thinkCamera.OnActionReadSuccess =ReadSuccess;
        }

        void ReadSuccess(string carnumber)
        {
            this.txtCarNumber.Text = carnumber;
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (thinkCamera.Close())
            {
                MessageBox.Show("关闭成功");
            }
            else
            {
                MessageBox.Show("关闭失败" + thinkCamera.ErrorStr);
            }
        }

        private void btnStartPreview_Click(object sender, EventArgs e)
        {
            thinkCamera.StartPreview(this.panel1.Handle);
        }

        private void btnStopPreview_Click(object sender, EventArgs e)
        {
            thinkCamera.StopPreview();
        }
    }
}
