using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using CMCS.CarTransport.Weighter.Core;
using System.IO.Ports;
using CMCS.Common.DAO;
using CMCS.Common;
using CMCS.DapperDber.Dbs.OracleDb;
using CMCS.Common.Utilities;

namespace CMCS.CarTransport.Weighter.Frms
{
	public partial class FrmSetting : DevComponents.DotNetBar.Metro.MetroForm
	{
		CommonDAO commonDAO = CommonDAO.GetInstance();

		CommonAppConfig commonAppConfig = CommonAppConfig.GetInstance();

		public FrmSetting()
		{
			InitializeComponent();
		}

		void InitForm()
		{
			InitComPortComboBoxs(cmbIocerCom, cmbWberCom);
			InitBandrateComboBoxs(cmbIocerBandrate, cmbWberBandrate);
			InitNumberAscComboBoxs(5, 8, cmbIocerDataBits, cmbWberDataBits);
			InitNumberAscComboBoxs(1, 15, cmbInductorCoil1Port, cmbInductorCoil2Port, cmbInductorCoil3Port, cmbInductorCoil4Port, cmbInfraredSensor1Port, cmbInfraredSensor2Port, cmbGate1UpPort, cmbGate1DownPort, cmbGate2UpPort, cmbGate2DownPort, cmbSignalLight1Port, cmbSignalLight2Port);
			InitStopBitsComboBoxs(cmbIocerStopBits, cmbWberStopBits);
			InitParityComboBoxs(cmbIocerParity, cmbWberParity);
			InitLEDTypeComboBox(cmbLEDType);
		}

		private void FrmSetting_Load(object sender, EventArgs e)
		{

		}

		private void FrmSetting_Shown(object sender, EventArgs e)
		{
			InitForm();

			LoadAppConfig();
		}

		/// <summary>
		/// 测试数据库连接
		/// </summary>
		/// <returns></returns>
		private bool TestDBConnect()
		{
			if (string.IsNullOrEmpty(txtSelfConnStr.Text.Trim()))
			{
				MessageBoxEx.Show("请先输入数据库连接字符串", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}
			try
			{
				OracleDapperDber dber = new OracleDapperDber(txtSelfConnStr.Text.Trim());
				if (dber.Connection.State != ConnectionState.Open)
				{
					dber.Connection.Open();
					dber.Connection.Close();
				}
				return true;
			}
			catch (Exception ex)
			{
				MessageBoxEx.Show("数据库连接失败，" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return false;
			}
		}

		/// <summary>
		/// 加载配置
		/// </summary>
		void LoadAppConfig()
		{
			txtAppIdentifier.Text = commonAppConfig.AppIdentifier;
			txtSelfConnStr.Text = commonAppConfig.SelfConnStr;
			chbStartup.Checked = (commonDAO.GetAppletConfigString("开机启动") == "1");
			chbBothWay.Checked = (commonDAO.GetAppletConfigString("双向磅") == "1");
			cmbInFactoryType.Text = commonDAO.GetAppletConfigString("过磅类型");
			// IO控制器
			SelectedComboBoxItem(cmbIocerCom, commonDAO.GetAppletConfigInt32("IO控制器_串口").ToString());
			SelectedComboBoxItem(cmbIocerBandrate, commonDAO.GetAppletConfigInt32("IO控制器_波特率").ToString());
			SelectedComboBoxItem(cmbIocerDataBits, commonDAO.GetAppletConfigInt32("IO控制器_数据位").ToString());
			SelectedComboBoxItem(cmbIocerStopBits, commonDAO.GetAppletConfigInt32("IO控制器_停止位").ToString());
			SelectedComboBoxItem(cmbIocerParity, commonDAO.GetAppletConfigInt32("IO控制器_校验位").ToString());
			SelectedComboBoxItem(cmbInductorCoil1Port, commonDAO.GetAppletConfigInt32("IO控制器_地感1端口").ToString());
			SelectedComboBoxItem(cmbInductorCoil2Port, commonDAO.GetAppletConfigInt32("IO控制器_地感2端口").ToString());
			SelectedComboBoxItem(cmbInductorCoil3Port, commonDAO.GetAppletConfigInt32("IO控制器_地感3端口").ToString());
			SelectedComboBoxItem(cmbInductorCoil4Port, commonDAO.GetAppletConfigInt32("IO控制器_地感4端口").ToString());
			SelectedComboBoxItem(cmbInfraredSensor1Port, commonDAO.GetAppletConfigInt32("IO控制器_对射1端口").ToString());
			SelectedComboBoxItem(cmbInfraredSensor2Port, commonDAO.GetAppletConfigInt32("IO控制器_对射2端口").ToString());
			SelectedComboBoxItem(cmbGate1UpPort, commonDAO.GetAppletConfigInt32("IO控制器_道闸1升杆端口").ToString());
			SelectedComboBoxItem(cmbGate1DownPort, commonDAO.GetAppletConfigInt32("IO控制器_道闸1降杆端口").ToString());
			SelectedComboBoxItem(cmbGate2UpPort, commonDAO.GetAppletConfigInt32("IO控制器_道闸2升杆端口").ToString());
			SelectedComboBoxItem(cmbGate2DownPort, commonDAO.GetAppletConfigInt32("IO控制器_道闸2降杆端口").ToString());
			SelectedComboBoxItem(cmbSignalLight1Port, commonDAO.GetAppletConfigInt32("IO控制器_信号灯1端口").ToString());
			SelectedComboBoxItem(cmbSignalLight2Port, commonDAO.GetAppletConfigInt32("IO控制器_信号灯2端口").ToString());

			// 地磅仪表
			SelectedComboBoxItem(cmbWberCom, commonDAO.GetAppletConfigInt32("地磅仪表_串口").ToString());
			SelectedComboBoxItem(cmbWberBandrate, commonDAO.GetAppletConfigInt32("地磅仪表_波特率").ToString());
			SelectedComboBoxItem(cmbWberDataBits, commonDAO.GetAppletConfigInt32("地磅仪表_数据位").ToString());
			SelectedComboBoxItem(cmbWberStopBits, commonDAO.GetAppletConfigInt32("地磅仪表_停止位").ToString());
			SelectedComboBoxItem(cmbWberParity, commonDAO.GetAppletConfigInt32("地磅仪表_校验位").ToString());
			dbtxtMinWeight.Value = commonDAO.GetAppletConfigDouble("地磅仪表_最小称重");
			dbtxtSteady.Value = commonDAO.GetAppletConfigDouble("地磅仪表_稳定时间");
			dbtxtGrossLossTare.Value = Convert.ToDouble(commonDAO.GetAppletConfigString(GlobalVars.CommonAppletConfigName, "地磅仪表_毛皮差"));
			// 读卡器
			iptxtRwer1.Value = commonDAO.GetAppletConfigString("车号识别1_IP地址");
			iptxtRwer2.Value = commonDAO.GetAppletConfigString("车号识别2_IP地址");

			// LED显示屏
			iptxtLED1IP.Value = commonDAO.GetAppletConfigString("LED显示屏1_IP地址");
			iptxtLED2IP.Value = commonDAO.GetAppletConfigString("LED显示屏2_IP地址");
			SelectedComboBoxItem(cmbLEDType, commonDAO.GetAppletConfigString("LED显示屏类型").ToString());
		}

		/// <summary>
		/// 保存配置
		/// </summary>
		bool SaveAppConfig()
		{
			if (!TestDBConnect()) return false;

			commonAppConfig.AppIdentifier = txtAppIdentifier.Text.Trim();
			commonAppConfig.SelfConnStr = txtSelfConnStr.Text;
			commonAppConfig.Save();
			commonDAO.SetAppletConfig("开机启动", Convert.ToInt16(chbStartup.Checked).ToString());
			commonDAO.SetAppletConfig("双向磅", Convert.ToInt16(chbBothWay.Checked).ToString());
			commonDAO.SetAppletConfig("过磅类型", cmbInFactoryType.Text);

			try
			{
#if DEBUG

#else
                // 添加、取消开机启动
                if (chbStartup.Checked)
                    StartUpUtil.InsertStartUp(Application.ProductName, Application.ExecutablePath);
                else
                    StartUpUtil.DeleteStartUp(Application.ProductName);
#endif
			}
			catch { }

			// IO控制器
			commonDAO.SetAppletConfig("IO控制器_串口", (cmbIocerCom.SelectedItem as DataItem).Value);
			commonDAO.SetAppletConfig("IO控制器_波特率", (cmbIocerBandrate.SelectedItem as DataItem).Value);
			commonDAO.SetAppletConfig("IO控制器_数据位", (cmbIocerDataBits.SelectedItem as DataItem).Value);
			commonDAO.SetAppletConfig("IO控制器_停止位", (cmbIocerStopBits.SelectedItem as DataItem).Value);
			commonDAO.SetAppletConfig("IO控制器_校验位", (cmbIocerParity.SelectedItem as DataItem).Value);
			commonDAO.SetAppletConfig("IO控制器_地感1端口", (cmbInductorCoil1Port.SelectedItem as DataItem).Value);
			commonDAO.SetAppletConfig("IO控制器_地感2端口", (cmbInductorCoil2Port.SelectedItem as DataItem).Value);
			commonDAO.SetAppletConfig("IO控制器_地感3端口", (cmbInductorCoil3Port.SelectedItem as DataItem).Value);
			commonDAO.SetAppletConfig("IO控制器_地感4端口", (cmbInductorCoil4Port.SelectedItem as DataItem).Value);
			commonDAO.SetAppletConfig("IO控制器_对射1端口", (cmbInfraredSensor1Port.SelectedItem as DataItem).Value);
			commonDAO.SetAppletConfig("IO控制器_对射2端口", (cmbInfraredSensor2Port.SelectedItem as DataItem).Value);
			commonDAO.SetAppletConfig("IO控制器_道闸1升杆端口", (cmbGate1UpPort.SelectedItem as DataItem).Value);
			commonDAO.SetAppletConfig("IO控制器_道闸1降杆端口", (cmbGate1DownPort.SelectedItem as DataItem).Value);
			commonDAO.SetAppletConfig("IO控制器_道闸2升杆端口", (cmbGate2UpPort.SelectedItem as DataItem).Value);
			commonDAO.SetAppletConfig("IO控制器_道闸2降杆端口", (cmbGate2DownPort.SelectedItem as DataItem).Value);
			commonDAO.SetAppletConfig("IO控制器_信号灯1端口", (cmbSignalLight1Port.SelectedItem as DataItem).Value);
			commonDAO.SetAppletConfig("IO控制器_信号灯2端口", (cmbSignalLight2Port.SelectedItem as DataItem).Value);

			// 地磅仪表
			commonDAO.SetAppletConfig("地磅仪表_串口", (cmbWberCom.SelectedItem as DataItem).Value);
			commonDAO.SetAppletConfig("地磅仪表_波特率", (cmbWberBandrate.SelectedItem as DataItem).Value);
			commonDAO.SetAppletConfig("地磅仪表_数据位", (cmbWberDataBits.SelectedItem as DataItem).Value);
			commonDAO.SetAppletConfig("地磅仪表_停止位", (cmbWberStopBits.SelectedItem as DataItem).Value);
			commonDAO.SetAppletConfig("地磅仪表_校验位", (cmbWberParity.SelectedItem as DataItem).Value);
			commonDAO.SetAppletConfig("地磅仪表_最小称重", dbtxtMinWeight.Value.ToString());
			commonDAO.SetAppletConfig("地磅仪表_稳定时间", dbtxtSteady.Value.ToString());
			commonDAO.SetAppletConfig(GlobalVars.CommonAppletConfigName, "地磅仪表_毛皮差", dbtxtGrossLossTare.Value.ToString());
			// 读卡器
			commonDAO.SetAppletConfig("车号识别1_IP地址", iptxtRwer1.Value);
			commonDAO.SetAppletConfig("车号识别2_IP地址", iptxtRwer2.Value);

			// LED显示屏
			commonDAO.SetAppletConfig("LED显示屏1_IP地址", iptxtLED1IP.Value);
			commonDAO.SetAppletConfig("LED显示屏2_IP地址", iptxtLED2IP.Value);
			commonDAO.SetAppletConfig("LED显示屏类型", (cmbLEDType.SelectedItem as DataItem).Value);
			return true;
		}

		private void btnSubmit_Click(object sender, EventArgs e)
		{
			if (!ValidateInputEmpty(new List<string> { "程序唯一标识符", "数据库连接字符串" }, new List<Control> { txtAppIdentifier, txtSelfConnStr })) return;

			if (!SaveAppConfig()) return;

			if (MessageBoxEx.Show("更改的配置需要重启程序才能生效，是否立刻重启？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
				Application.Restart();
			else
				this.Close();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.Close();
		}

		#region 其他函数

		/// <summary>
		/// 验证批量控件为空，并提示
		/// </summary>
		/// <param name="tipsNames"></param>
		/// <param name="controls"></param>
		/// <returns></returns>
		public static bool ValidateInputEmpty(List<string> tipsNames, List<Control> controls)
		{
			for (int i = 0; i < controls.Count; i++)
			{
				Control control = controls[i];

				if (control is TextBoxX && string.IsNullOrEmpty(((TextBoxX)control).Text))
				{
					control.Focus();
					MessageBoxEx.Show("请输入" + tipsNames[i] + "！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);

					return false;
				}
			}

			return true;
		}

		/// <summary>
		/// 选中下拉框选项
		/// </summary>
		/// <param name="cmb"></param>
		/// <param name="text"></param>
		private void SelectedComboBoxItem(ComboBoxEx cmb, string value)
		{
			foreach (DataItem dataItem in cmb.Items)
			{
				if (dataItem.Value == value) cmb.SelectedItem = dataItem;
			}
		}

		/// <summary>
		/// 初始化串口下拉框
		/// </summary>
		/// <param name="cmb"></param>
		void InitComPortComboBox(ComboBoxEx cmb)
		{
			cmb.Items.Clear();

			cmb.DisplayMember = "Text";
			cmb.ValueMember = "Value";

			for (int i = 1; i < 20; i++)
			{
				cmb.Items.Add(new DataItem("COM" + i.ToString(), i.ToString()));
			}

			cmb.SelectedIndex = 0;
		}

		/// <summary>
		/// 初始化串口下拉框
		/// </summary>
		/// <param name="cmbs"></param>
		void InitComPortComboBoxs(params ComboBoxEx[] cmbs)
		{
			foreach (ComboBoxEx cmb in cmbs)
			{
				InitComPortComboBox(cmb);
			}
		}

		/// <summary>
		/// 初始化波特率下拉框
		/// </summary>
		/// <param name="cmb"></param>
		private void InitBandrateComboBox(ComboBoxEx cmb)
		{
			cmb.Items.Clear();

			cmb.DisplayMember = "Text";
			cmb.ValueMember = "Value";

			cmb.Items.Add(new DataItem("1200"));
			cmb.Items.Add(new DataItem("4800"));
			cmb.Items.Add(new DataItem("9600"));
			cmb.Items.Add(new DataItem("14400"));
			cmb.Items.Add(new DataItem("19200"));
			cmb.Items.Add(new DataItem("38400"));
			cmb.Items.Add(new DataItem("56000"));
			cmb.Items.Add(new DataItem("57600"));
			cmb.Items.Add(new DataItem("115200"));

			cmb.SelectedIndex = 0;
		}

		/// <summary>
		/// 初始化波特率下拉框
		/// </summary>
		/// <param name="cmbs"></param>
		void InitBandrateComboBoxs(params ComboBoxEx[] cmbs)
		{
			foreach (ComboBoxEx cmb in cmbs)
			{
				InitBandrateComboBox(cmb);
			}
		}

		/// <summary>
		/// 初始化数字下拉框
		/// </summary>
		/// <param name="cmb"></param>
		/// <param name="start"></param>
		/// <param name="end"></param>
		void InitNumberAscComboBox(int start, int end, ComboBoxEx cmb)
		{
			cmb.Items.Clear();

			cmb.DisplayMember = "Text";
			cmb.ValueMember = "Value";

			for (int i = start; i <= end; i++)
			{
				cmb.Items.Add(new DataItem(i.ToString()));
			}

			if (cmb.Items.Count > 0) cmb.SelectedIndex = 0;
		}

		/// <summary>
		/// 初始化数字下拉框
		/// </summary>
		/// <param name="cmb"></param>
		/// <param name="start"></param>
		/// <param name="end"></param>
		void InitNumberAscComboBoxs(int start, int end, params ComboBoxEx[] cmbs)
		{
			foreach (ComboBoxEx cmb in cmbs)
			{
				InitNumberAscComboBox(start, end, cmb);
			}
		}

		/// <summary>
		/// 初始化停止位下拉框
		/// </summary>
		/// <param name="cmb"></param>
		void InitStopBitsComboBox(ComboBoxEx cmb)
		{
			cmb.Items.Clear();

			cmb.DisplayMember = "Text";
			cmb.ValueMember = "Value";

			cmb.Items.Add(new DataItem(StopBits.None.ToString(), ((int)StopBits.None).ToString()));
			cmb.Items.Add(new DataItem(StopBits.One.ToString(), ((int)StopBits.One).ToString()));
			cmb.Items.Add(new DataItem(StopBits.OnePointFive.ToString(), ((int)StopBits.OnePointFive).ToString()));
			cmb.Items.Add(new DataItem(StopBits.Two.ToString(), ((int)StopBits.Two).ToString()));

			cmb.SelectedIndex = 0;
		}

		/// <summary>
		/// 初始化停止位下拉框
		/// </summary>
		/// <param name="cmbs"></param>
		void InitStopBitsComboBoxs(params ComboBoxEx[] cmbs)
		{
			foreach (ComboBoxEx cmb in cmbs)
			{
				InitStopBitsComboBox(cmb);
			}
		}

		/// <summary>
		/// 初始化校验位下拉框
		/// </summary>
		/// <param name="cmb"></param>
		void InitParityComboBox(ComboBoxEx cmb)
		{
			cmb.Items.Clear();

			cmb.DisplayMember = "Text";
			cmb.ValueMember = "Value";

			cmb.Items.Add(new DataItem(Parity.None.ToString(), ((int)Parity.None).ToString()));
			cmb.Items.Add(new DataItem(Parity.Odd.ToString(), ((int)Parity.Odd).ToString()));
			cmb.Items.Add(new DataItem(Parity.Even.ToString(), ((int)Parity.Even).ToString()));
			cmb.Items.Add(new DataItem(Parity.Mark.ToString(), ((int)Parity.Mark).ToString()));
			cmb.Items.Add(new DataItem(Parity.Space.ToString(), ((int)Parity.Space).ToString()));

			cmb.SelectedIndex = 0;
		}

		/// <summary>
		/// 初始化校验位下拉框
		/// </summary>
		/// <param name="cmbs"></param>
		void InitParityComboBoxs(params ComboBoxEx[] cmbs)
		{
			foreach (ComboBoxEx cmb in cmbs)
			{
				InitParityComboBox(cmb);
			}
		}

		/// <summary>
		/// 初始化LED类型下拉框
		/// </summary>
		/// <param name="cmb"></param>
		void InitLEDTypeComboBox(ComboBoxEx cmb)
		{
			cmb.Items.Clear();

			cmb.DisplayMember = "Text";
			cmb.ValueMember = "Value";

			cmb.Items.Add(new DataItem("上海仰邦", "上海仰邦"));
			cmb.Items.Add(new DataItem("上海灵信", "上海灵信"));

			cmb.SelectedIndex = 0;
		}
		#endregion
	}
}