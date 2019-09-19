using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//
using ZAZ.Finger;
using CMCS.Common.Utilities;

namespace CMCS.DataTester.Frms
{
    public partial class FrmFinger : Form
    {
        FingerMachine finger = new FingerMachine();
        TaskSimpleScheduler taskSimpleScheduler = new TaskSimpleScheduler();

        public FrmFinger()
        {
            InitializeComponent();
        }

        private void btnOpen_Click(object sender, EventArgs e)
        {
            if (finger.OpenDeviceEx())
                txtIndex.Text = finger.ReadIndex();
            this.txtMessage.AppendText(finger.MessageStr);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            finger.CloseDeviceEx();
            this.txtMessage.AppendText(finger.MessageStr);
        }

        private void btnRegsit_Click(object sender, EventArgs e)
        {
            int number = 0;
            try
            {
                number = int.Parse(txtFingerNumber.Text);
            }
            catch (Exception)
            {
            }
            taskSimpleScheduler.StartNewTask("注册指纹", () =>
            {
                if (finger.RegistFinger(number))
                    taskSimpleScheduler.Cancal();
                InvokeEx(() =>
                {
                    this.txtMessage.Text = finger.MessageStr;
                });
            }, 1000);

            finger.RegistFinger(number);
            if (finger.CurrentImage != null)
                this.picFinger.Image = finger.CurrentImage;
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

    }
}
