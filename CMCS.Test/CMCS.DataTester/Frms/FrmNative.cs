﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CMCS.DataTester.DAO;
using System.IO.Ports;

namespace CMCS.DataTester.Frms
{
    public partial class FrmNative : Form
    {
        private SerialPort serialPort = new SerialPort();

        DataTesterDAO dataTesterDAO = DataTesterDAO.GetInstance();

        public FrmNative()
        {
            InitializeComponent();
        }

        private void FrmTest_Load(object sender, EventArgs e)
        {

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

        private void button1_Click(object sender, EventArgs e)
        {
            LoginDAO.CreateCred("10.90.20.10", "assayuploader", "123456");
        }
    }
}
