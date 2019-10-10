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
	public partial class FrmCodeChange : Form
	{
		private SerialPort serialPort = new SerialPort();

		DataTesterDAO dataTesterDAO = DataTesterDAO.GetInstance();

		public FrmCodeChange()
		{
			InitializeComponent();
		}

		private void FrmWBYaoHua_Load(object sender, EventArgs e)
		{
		}

		private void FrmWBYaoHua_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (serialPort.IsOpen) btnColseCom_Click(null, null);
		}

		private void btnOpenCom_Click(object sender, EventArgs e)
		{
			textBox2.Text = string.Empty;
			try
			{
				string input = "Hello World!";
				input = textBox1.Text.Trim();
				char[] values = input.ToCharArray();
				foreach (char letter in values)
				{
					// Get the integral value of the character.
					int value = Convert.ToInt32(letter);
					// Convert the decimal value to a hexadecimal value in string form.
					string hexOutput = String.Format("{0:X}", value);
					textBox2.Text += hexOutput + " ";
					//Console.WriteLine("Hexadecimal value of {0} is {1}", letter, hexOutput);
				}

			}
			catch (Exception ex)
			{
				MessageBox.Show("操作失败：" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		private void btnColseCom_Click(object sender, EventArgs e)
		{
			textBox2.Text = string.Empty;
			try
			{
				string hexValues = "48 65 6C 6C 6F 20 57 6F 72 6C 64 21";
				hexValues = textBox1.Text.Trim();
				hexValues.Replace(" ", "");
				hexValues = InsertFormat(hexValues, 2, " ");
				string[] hexValuesSplit = hexValues.Split(' ');
				foreach (String hex in hexValuesSplit)
				{
					int value = Convert.ToInt32(hex, 16);
					string stringValue = Char.ConvertFromUtf32(value);
					char charValue = (char)value;
					textBox2.Text += charValue.ToString();
				}
			}
			catch (Exception ex)
			{
				MessageBox.Show("操作失败：" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		private void button1_Click(object sender, EventArgs e)
		{
			textBox2.Text = string.Empty;
			try
			{
				textBox2.Text = Convert.ToInt32(textBox1.Text, 16).ToString();
			}
			catch (Exception ex)
			{
				MessageBox.Show("操作失败：" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}

		private void button2_Click(object sender, EventArgs e)
		{
			textBox2.Text = string.Empty;
			try
			{
				//textBox2.Text = string.Format("{0:X}", textBox1.Text);
				textBox2.Text = Convert.ToString(int.Parse(textBox1.Text), 16);
			}
			catch (Exception ex)
			{
				MessageBox.Show("操作失败：" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
			}
		}
		/// <summary>  
		/// 每隔n个字符插入一个字符  
		/// </summary>  
		/// <param name="input">源字符串</param>  
		/// <param name="interval">间隔字符数</param>  
		/// <param name="value">待插入值</param>  
		/// <returns>返回新生成字符串</returns>  
		public static string InsertFormat(string input, int interval, string value)
		{
			for (int i = interval; i < input.Length; i += interval + 1)
				input = input.Insert(i, value);
			return input;
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
