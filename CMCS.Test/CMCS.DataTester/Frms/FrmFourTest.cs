using System;
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
	public partial class FrmFourTest : Form
	{
		private SerialPort serialPort = new SerialPort();

		DataTesterDAO dataTesterDAO = DataTesterDAO.GetInstance();

		public FrmFourTest()
		{
			InitializeComponent();
		}

		private void FrmTest_Load(object sender, EventArgs e)
		{

		}

		private void button1_Click(object sender, EventArgs e)
		{
			decimal value1 = Math.Round(Convert.ToDecimal(txt_InPut.Text), Convert.ToInt32(txt_Dts.Text));
			decimal value2 = Math.Round(Convert.ToDecimal(txt_InPut.Text), Convert.ToInt32(txt_Dts.Text), MidpointRounding.AwayFromZero);
			decimal value3 = Math.Round(Convert.ToDecimal(txt_InPut.Text), Convert.ToInt32(txt_Dts.Text), MidpointRounding.ToEven);
			double value4 = Convert.ToDouble(txt_InPut.Text);

			txt_Value1.Text = value1.ToString();
			txt_Value2.Text = value2.ToString();
			txt_Value3.Text = value3.ToString();
			txt_Value4.Text = value4.ToString("#.##");
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
