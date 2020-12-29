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
	public partial class FrmMakeChange : MetroForm
	{
		public FrmMakeChange()
		{
			InitializeComponent();
		}

		/// <summary>
		/// 窗体唯一标识符
		/// </summary>
		public static string UniqueKey = "FrmMakeChange";

		#region 业务处理类

		CommonDAO commonDAO = CommonDAO.GetInstance();
		CZYHandlerDAO czyHandlerDAO = CZYHandlerDAO.GetInstance();

		#endregion

		#region Vars

		CodePrinter _CodePrinter = null;
		QRCodePrinter _QRCodePrinter = null;

		eFlowFlag currentFlowFlag = eFlowFlag.等待扫码;
		/// <summary>
		/// 当前流程标识
		/// </summary>
		public eFlowFlag CurrentFlowFlag
		{
			get { return currentFlowFlag; }
			set
			{
				currentFlowFlag = value;
				lblCurrentFlowFlag.Text = value.ToString();
			}
		}

		// 当前制样明细记录
		CmcsRCMakeDetail CurrentMakeDetail = null;

		// 当前制样明细记录
		MakeDetail CurrentChangeMakeDetail = null;

		// 当前制样明细记录
		IList<CmcsRCMakeDetail> CurrentMakeDetails = null;

		/// <summary>
		/// 化验记录
		/// </summary>
		CmcsRCAssay RCAssay;

		string resMessage = string.Empty;

		/// <summary>
		/// 2mm超差重
		/// </summary>
		double OverWeight_2mm = 0;

		/// <summary>
		/// 6mm超差重
		/// </summary>
		double OverWeight_6mm = 0;

		/// <summary>
		/// 验证方式
		/// </summary>
		string CheckType = "扫码";

		/// <summary>
		/// 是否验证完毕 2mm 0.6mm都进行过打印操作
		/// </summary>
		bool IsChecked = false;
		#endregion

		/// <summary>
		/// 初始化
		/// </summary>
		public void InitFrom()
		{
			superGridControl1.PrimaryGrid.ColumnAutoSizeMode = ColumnAutoSizeMode.AllCells;

			this._CodePrinter = new CodePrinter(printDocument1);
			this._QRCodePrinter = new QRCodePrinter(printDocument1);

			OverWeight_2mm = commonDAO.GetAppletConfigDouble("0.2mm超差重");
			OverWeight_6mm = commonDAO.GetAppletConfigDouble("6mm超差重");
			LoadRCMakeDetail();
		}

		private void FrmMakeWeight_Load(object sender, EventArgs e)
		{
			// 初始化
			InitFrom();
			// 初始化设备
			InitHardware();
			this.Focus();
			btnReset_Click(null, null);
		}

		private void FrmMakeWeight_FormClosing(object sender, FormClosingEventArgs e)
		{
			UnloadHardware();
		}

		#region 读卡器

		/// <summary>
		/// 读卡器
		/// </summary>
		HFReaderRwer rwer = new HFReaderRwer();

		/// <summary>
		/// 读卡成功事件
		/// </summary>
		/// <param name="steady"></param>
		void Rwer_OnReadSuccess(string rfid)
		{
			InvokeEx(() =>
			{
				txtInputMakeCode.Text = rfid;
			});
		}

		/// <summary>
		///  读卡器状态变化
		/// </summary>
		/// <param name="status"></param>
		void Rwer_OnStatusChange(bool status)
		{
			// 接收设备状态 
			InvokeEx(() =>
			{
				slightRwer.LightColor = (status ? Color.Green : Color.Red);
			});
		}

		#endregion

		#region 电子秤

		/// <summary>
		/// 电子秤
		/// </summary>
		WB.XiangPing.Balance.XiangPing_Balance wber = new WB.XiangPing.Balance.XiangPing_Balance(3);

		double currentWeight = 0;
		/// <summary>
		/// 电子天平当前重量
		/// </summary>
		public double CurrentWeight
		{
			get { return currentWeight; }
			set
			{
				currentWeight = value;
				this.lbl_CurrentWeight.Text = value.ToString() + "g";
			}
		}

		bool isUseWeight = true;
		/// <summary>
		/// 启用电子秤
		/// </summary>
		public bool IsUseWeight
		{
			get { return isUseWeight; }
			set
			{
				isUseWeight = value;

				lblWber.Visible = value;
				slightWber.Visible = value;
			}
		}

		bool wbSteady = false;
		/// <summary>
		/// 电子秤稳定状态
		/// </summary>
		public bool WbSteady
		{
			get { return wbSteady; }
			set { wbSteady = value; }
		}

		double wbMinWeight = 0;
		/// <summary>
		/// 电子秤最小称重 单位：吨
		/// </summary>
		public double WbMinWeight
		{
			get { return wbMinWeight; }
			set
			{
				wbMinWeight = value;
			}
		}

		/// <summary>
		/// 重量稳定事件
		/// </summary>
		/// <param name="steady"></param>
		void Wber_OnSteadyChange(bool steady)
		{
			// 仪表稳定状态 
			InvokeEx(() =>
			{
				this.WbSteady = steady;
			});
		}

		/// <summary>
		/// 电子秤仪表状态变化
		/// </summary>
		/// <param name="status"></param>
		void Wber_OnStatusChange(bool status)
		{
			// 接收设备状态 
			InvokeEx(() =>
			{
				slightWber.LightColor = (status ? Color.Green : Color.Red);
			});
		}

		/// <summary>
		/// 实时重量
		/// </summary>
		/// <param name="status"></param>
		void wber_OnWeightChange(double weight)
		{
			// 接收设备状态 
			InvokeEx(() =>
			{
				this.CurrentWeight = weight;
			});
		}
		#endregion

		#region 设备初始化与卸载

		/// <summary>
		/// 初始化外接设备
		/// </summary>
		private void InitHardware()
		{
			try
			{
				bool success = false;
				// 初始化-读卡器
				rwer.OnStatusChange += new HFReaderRwer.StatusChangeHandler(Rwer_OnStatusChange);
				success = rwer.OpenNetPort(commonDAO.GetAppletConfigString("读卡器IP"), commonDAO.GetAppletConfigInt32("读卡器端口"));
				if (success) ShowMessage("读卡器连接成功", eOutputType.Normal);
				else ShowMessage("读卡器连接失败", eOutputType.Error);
				IsUseWeight = commonDAO.GetAppletConfigString("启用称重") == "1" ? true : false;
				// 初始化-电子秤 
				if (IsUseWeight)
				{
					this.WbMinWeight = commonDAO.GetAppletConfigDouble("电子秤最小重量");

					// 电子秤仪表1
					wber.OnStatusChange += new WB.XiangPing.Balance.XiangPing_Balance.StatusChangeHandler(Wber_OnStatusChange);
					wber.OnSteadyChange += new WB.XiangPing.Balance.XiangPing_Balance.SteadyChangeEventHandler(Wber_OnSteadyChange);
					wber.OnWeightChange += new WB.XiangPing.Balance.XiangPing_Balance.WeightChangeEventHandler(wber_OnWeightChange);

					success = wber.OpenCom(commonDAO.GetAppletConfigInt32("电子秤串口"), commonDAO.GetAppletConfigInt32("电子秤波特率"), commonDAO.GetAppletConfigInt32("电子秤数据位"), commonDAO.GetAppletConfigInt32("电子秤停止位"));
					if (success) ShowMessage("电子秤连接成功", eOutputType.Normal);
					else ShowMessage("电子秤连接失败", eOutputType.Error);
				}

				timer1.Enabled = true;
			}
			catch (Exception ex)
			{
				Log4Neter.Error("设备初始化", ex);
			}
		}

		/// <summary>
		/// 卸载设备
		/// </summary>
		private void UnloadHardware()
		{
			// 注意此段代码
			Application.DoEvents();

			try
			{
				rwer.CloseRF();
				rwer.CloseNetPort();
			}
			catch { }
			try
			{
				wber.CloseCom();
			}
			catch
			{ }
		}
		#endregion

		#region 称重校验业务

		private void timer1_Tick(object sender, EventArgs e)
		{
			timer1.Stop();
			try
			{
				#region 制样称重校验
				switch (this.CurrentFlowFlag)
				{
					case eFlowFlag.等待校验:
						#region
						// 重量大于最小称重且稳定
						if (wber.Status && wber.Weight > WbMinWeight && WbSteady)
						{
							czyHandlerDAO.UpdateMakeDetailCheckWeight(this.CurrentMakeDetail.Id, wber.Weight);
							ShowMessage("校验完成，重量为：" + wber.Weight.ToString(), eOutputType.Normal);

							if (WeightCheck())
								this.CurrentFlowFlag = eFlowFlag.校验成功;
						}
						#endregion
						break;
					case eFlowFlag.校验成功:
						PrintAssayCode();
						break;
				}
				#endregion
			}
			catch (Exception ex)
			{
				ShowMessage("Timer1运行异常" + ex.Message, eOutputType.Error);
			}
			timer1.Start();
		}

		/// <summary>
		/// 重量验证
		/// </summary>
		/// <returns></returns>
		private bool WeightCheck()
		{
			bool check = false;
			switch (this.CurrentMakeDetail.SampleType)
			{
				case "0.2mm分析样":
					if (Math.Abs(this.CurrentMakeDetail.Weight - wber.Weight) <= commonDAO.GetCommonAppletConfigDouble("0.2mm超差重"))
						check = true;
					break;
				case "0.2mm存查样":
					if (Math.Abs(this.CurrentMakeDetail.Weight - wber.Weight) <= commonDAO.GetCommonAppletConfigDouble("0.2mm超差重"))
						check = true;
					break;
				case "3mm存查样":
					if (Math.Abs(this.CurrentMakeDetail.Weight - wber.Weight) <= commonDAO.GetCommonAppletConfigDouble("3mm超差重"))
						check = true;
					break;
				case "6mm全水样":
					if (Math.Abs(this.CurrentMakeDetail.Weight - wber.Weight) <= commonDAO.GetCommonAppletConfigDouble("6mm超差重"))
						check = true;
					break;
				default:
					break;
			}
			if (check)
			{
				ShowMessage("校验成功，重量为：" + wber.Weight.ToString(), eOutputType.Normal);
				btnPrint.Enabled = true;
			}
			else
			{
				ShowMessage("校验失败，重量为：" + wber.Weight.ToString(), eOutputType.Normal);
				btnPrint.Enabled = false;
			}
			return check;
		}

		/// <summary>
		/// 重置
		/// </summary>
		private void Restet()
		{
			this.CurrentFlowFlag = eFlowFlag.等待扫码;

			this.CurrentMakeDetail = null;
			this.RCAssay = null;

			txtInputMakeCode.ButtonCustom.Enabled = false;
			txtInputMakeCode.ResetText();
			txtInputAssayCode.ResetText();
			picEncode.Image = null;
			superGridControl1.PrimaryGrid.DataSource = null;
			FoucsAndSelect();

			LoadRCMakeDetail();
		}

		/// <summary>
		/// 打印化验码
		/// </summary>
		private void PrintAssayCode()
		{
			this.CurrentFlowFlag = eFlowFlag.打印化验码;

			if (this.picEncode.Image == null) return;

			if (MessageBoxEx.Show("样品类型：" + this.CurrentMakeDetail.SampleType + "，立刻打印化验码？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
			{
				btnPrint_Click(null, null);

				Restet();
			}
			else
				Restet();
		}

		#endregion

		#region 业务

		/// <summary>
		/// 根据制样码加载化验码及制样明细
		/// </summary>
		/// <param name="makeCode"></param>
		private void LoadCode(string makeCode)
		{
			this.txtInputAssayCode.Text = czyHandlerDAO.MakeBillNumberToAssayCode(makeCode);

			#region DotNetBar
			this.picEncode.Image = QRCodeBar(this.txtInputAssayCode.Text.Trim());
			this.picEncode.Width = 200;
			this.picEncode.Height = 200;
			#endregion

			#region 第三方插件
			//this.picEncode.Image = QRCode(this.txtInputAssayCode.Text.Trim(), 4);
			//picEncode.Width = this.picEncode.Image.Width;
			//picEncode.Height = this.picEncode.Image.Height;
			#endregion

			LoadRCMakeDetail();
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
				LoadRCMakeDetail();
			}
			else if (e.KeyCode == Keys.F2)//打印
			{
				btnPrint_Click(null, null);
			}
			else if (e.KeyCode == Keys.F1)//读卡
			{
				btnReadRf_Click(null, null);
			}
			else if (e.KeyCode == Keys.Escape)//重置
			{
				btnReset_Click(null, null);
			}
			else
				this.CheckType = "扫码";
		}

		/// <summary>
		/// 文本改变事件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void txtInputMakeCode_TextChanged(object sender, EventArgs e)
		{
			txtInputMakeCode.Text = txtInputMakeCode.Text.Trim().ToUpper();
			txtInputMakeCode.SelectionStart = txtInputMakeCode.Text.Length;
			//if (txtInputMakeCode.Text.Trim().Length > 16)
			//{
			//    txtInputMakeCode.Text = txtInputMakeCode.Text.Trim().Substring(txtInputMakeCode.Text.Trim().Length - 16, 16);
			//    txtInputMakeCode.SelectionStart = 16;
			//}
			if (txtInputMakeCode.Text.Trim().Length == 16)//制样明细码
				LoadCode(txtInputMakeCode.Text.Trim());
			else if (txtInputMakeCode.Text.Trim().Length == 12)//化验码
			{
				txtInputAssayCode.Text = czyHandlerDAO.GetMakeCodeByAssayCode(txtInputMakeCode.Text);
			}
			else
			{
				txtInputAssayCode.ResetText();
				picEncode.Image = null;
			}
		}

		private void txtInputMakeCode_MouseMove(object sender, MouseEventArgs e)
		{
			// 方便客户快速使用，获取焦点
			if (!txtInputMakeCode.Focused)
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

		private void timer2_Tick(object sender, EventArgs e)
		{
			// 方便客户快速使用，获取焦点
			//FoucsAndSelect();
		}

		/// <summary>
		/// 获取焦点并选中
		/// </summary>
		private void FoucsAndSelect()
		{
			txtInputMakeCode.Focus();
			txtInputMakeCode.SelectAll();
		}
		#endregion

		/// <summary>
		/// 加载制样明细记录
		/// </summary>
		private void LoadRCMakeDetail()
		{
			string barrelCode = txtInputMakeCode.Text.Length == 16 ? txtInputMakeCode.Text.Substring(0, 13) : "";
			IList<CmcsRCMakeDetail> rCMakeDetails = commonDAO.GetPrintMakeDetail(DateTime.Now, barrelCode);
			IList<MakeDetail> MakeDetails = DataChange(rCMakeDetails);
			superGridControl1.PrimaryGrid.DataSource = MakeDetails;
			if (string.IsNullOrEmpty(txtInputMakeCode.Text)) return;
			this.CurrentMakeDetail = czyHandlerDAO.GetMakeDetailByMakeBillNumber(txtInputMakeCode.Text.Trim());
			if (this.CurrentMakeDetail == null)
			{
				ShowMessage("未找到制样明细记录", eOutputType.Error);
				return;
			}
			this.CurrentChangeMakeDetail = DataChange(this.CurrentMakeDetail);
			if (MakeDetails.Select(a => a.AssayId).ToList().IndexOf(this.CurrentChangeMakeDetail.AssayId) == -1)
			{
				MakeDetails.Insert(0, CurrentChangeMakeDetail);
				superGridControl1.PrimaryGrid.DataSource = MakeDetails;
			}
			else
			{
				rCMakeDetails.Add(this.CurrentMakeDetail);
				MakeDetails = DataChange(rCMakeDetails);
				superGridControl1.PrimaryGrid.DataSource = MakeDetails;
				int index = MakeDetails.Select(a => a.AssayId).ToList().IndexOf(this.CurrentChangeMakeDetail.AssayId);
				//((GridRow)superGridControl1.PrimaryGrid.Rows[index]).CellStyles.Default.TextColor = Color.Blue;
			}
			this.CurrentFlowFlag = eFlowFlag.等待校验;
			//开始重量验证
			timer1_Tick(null, null);
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

		#region 读卡器
		/// <summary>
		/// 读卡
		/// </summary>
		/// <returns></returns>
		private string ReadRf()
		{
			byte SecNumber = Convert.ToByte(commonDAO.GetAppletConfigInt32("读卡器扇区"));
			byte BlockNumber = Convert.ToByte(commonDAO.GetAppletConfigInt32("读卡器块区"));

			//if (rwer.OpenRF())
			//{
			//    ShowMessage("射频打开成功", eOutputType.Normal);
			//}
			//else
			//{
			//    ShowMessage("射频打开失败", eOutputType.Error);
			//    return string.Empty;
			//}

			//if (rwer.ChangeToISO14443A())
			//{
			//    ShowMessage("切换到1443模式成功", eOutputType.Normal);
			//}
			//else
			//{
			//    ShowMessage("切换到1443模式失败", eOutputType.Error);
			//    return string.Empty;
			//}

			//if (rwer.Request14443A())
			//{
			//    ShowMessage("获取卡类型成功", eOutputType.Normal);
			//}
			//else
			//{
			//    ShowMessage("获取卡类型失败", eOutputType.Error);
			//    return string.Empty;
			//}

			//if (rwer.Anticoll14443A())
			//{
			//    ShowMessage("获取卡号成功", eOutputType.Normal);
			//}
			//else
			//{
			//    ShowMessage("获取卡号失败", eOutputType.Error);
			//    return string.Empty;
			//}

			//if (rwer.Select14443A())
			//{
			//    ShowMessage("获取卡容量成功", eOutputType.Normal);
			//}
			//else
			//{
			//    ShowMessage("获取卡容量失败", eOutputType.Error);
			//    return string.Empty;
			//}

			//if (rwer.AuthKey14443A(SecNumber, BlockNumber))
			//{
			//    ShowMessage("标签密钥验证成功", eOutputType.Normal);
			//}
			//else
			//{
			//    ShowMessage("标签密钥验证失败", eOutputType.Error);
			//    return string.Empty;
			//}
			if (rwer.RWRead14443A(SecNumber, BlockNumber) != string.Empty)
			{
				this.CheckType = "读卡";
				ShowMessage("读卡成功", eOutputType.Normal);
				return rwer.Byte16ToString(rwer.ReadData).ToUpper();
			}
			else
			{
				ShowMessage("读卡失败：" + rwer.ErrorStr, eOutputType.Normal);
			}
			//rwer.CloseRF();
			//rwer.CloseNetPort();
			return string.Empty;
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
		/// 读卡
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnReadRf_Click(object sender, EventArgs e)
		{
			string RfTag = ReadRf();
			if (!string.IsNullOrEmpty(RfTag))
			{
				txtInputMakeCode.Text = RfTag;
				//LoadCode(RfTag);
			}
			else
			{
				btnReset_Click(null, null);
			}
		}

		/// <summary>
		/// 打印二维码
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnPrint_Click(object sender, EventArgs e)
		{
			try
			{
				if (!string.IsNullOrEmpty(this.txtInputAssayCode.Text.Trim()))
				{
					if (this.IsChecked)
					{
						if (MessageBoxEx.Show("该化验码已审核，是否再次进行打印！", "提示", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning) == DialogResult.Cancel)
							return;
					}
					if (this.CurrentMakeDetail != null)
					{
						this.CurrentMakeDetail.PrintCount++;
						this.CurrentMakeDetail.PrintTime = DateTime.Now;
						this.CurrentMakeDetail.CheckType = this.CheckType;
						this.CurrentMakeDetail.CheckUser = SelfVars.LoginUserNames;
						this.CurrentMakeDetail.IsCheck = 1;

						commonDAO.SelfDber.Update(this.CurrentMakeDetail);
						czyHandlerDAO.RelieveAssay(this.CurrentMakeDetail, this.CurrentMakeDetail.TheRCMake.MakePle, SelfVars.LoginUserNames);
					}
					_CodePrinter.Print(this.txtInputAssayCode.Text.Trim());
					LoadRCMakeDetail();
					FoucsAndSelect();
				}
				else
					MessageBoxEx.Show("请先转换出化验码", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
			catch (Exception ex)
			{
				Log4Neter.Error("打印事件", ex);
			}
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
			if (string.IsNullOrEmpty(txtInputMakeCode.Text))
			{
				MessageBoxEx.Show("请先输入化验码", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

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

		/// <summary>
		/// 数据加载完成事件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void superGridControl1_DataBindingComplete(object sender, GridDataBindingCompleteEventArgs e)
		{
			foreach (GridRow gridRow in e.GridPanel.Rows)
			{
				MakeDetail entity = gridRow.DataItem as MakeDetail;
				if (entity == null) return;

				if (StringToDouble(entity.CheckWeight_6mm) > 0 && StringToDouble(entity.Weight_6mm) > 0 && Math.Abs(StringToDouble(entity.CheckWeight_6mm) - StringToDouble(entity.Weight_6mm)) <= OverWeight_6mm)
				{
					gridRow.Cells["clmIsNormal_6mm"].Value = "正常";
					gridRow.Cells["clmIsNormal_6mm"].CellStyles.Default.TextColor = Color.Green;
				}
				else
				{
					gridRow.Cells["clmIsNormal_6mm"].Value = "异常";
					gridRow.Cells["clmIsNormal_6mm"].CellStyles.Default.TextColor = Color.Red;
				}

				if (StringToDouble(entity.CheckWeight_2mm) > 0 && StringToDouble(entity.Weight_2mm) > 0 && Math.Abs(StringToDouble(entity.CheckWeight_2mm) - StringToDouble(entity.Weight_2mm)) <= OverWeight_2mm)
				{
					gridRow.Cells["clmIsNormal_2mm"].Value = "正常";
					gridRow.Cells["clmIsNormal_2mm"].CellStyles.Default.TextColor = Color.Green;
				}
				else
				{
					gridRow.Cells["clmIsNormal_2mm"].Value = "异常";
					gridRow.Cells["clmIsNormal_2mm"].CellStyles.Default.TextColor = Color.Red;
				}

				if (entity.IsCheck == 1)
				{
					gridRow.CellStyles.Default.TextColor = Color.Green;
				}
				if (StringToDate(entity.PrintTime_6mm).Date < DateTime.Now.Date && StringToDate(entity.PrintTime_6mm).Date > DateTime.MinValue)
				{
					gridRow.Cells["clmPrintTime_6mm"].CellStyles.Default.TextColor = Color.Blue;
					gridRow.Cells["clmMakeCode_6mm"].CellStyles.Default.TextColor = Color.Blue;
				}
				if (StringToDate(entity.PrintTime_2mm).Date < DateTime.Now.Date && StringToDate(entity.PrintTime_2mm).Date > DateTime.MinValue)
				{
					gridRow.Cells["clmPrintTime_2mm"].CellStyles.Default.TextColor = Color.Blue;
					gridRow.Cells["clmMakeCode_2mm"].CellStyles.Default.TextColor = Color.Blue;
				}
			}
		}

		private void superGridControl1_CellMouseDown(object sender, GridCellMouseEventArgs e)
		{
			MakeDetail entity = e.GridCell.GridRow.DataItem as MakeDetail;
			if (entity == null) return;

			// 更改验证状态
			if (e.GridCell.GridColumn.Name == "clmIsCheck")
			{
				czyHandlerDAO.ChangeCheckStatus(entity.MakeDetialId_6mm, Convert.ToBoolean(e.GridCell.Value), SelfVars.LoginUserNames);
				czyHandlerDAO.ChangeCheckStatus(entity.MakeDetialId_2mm, Convert.ToBoolean(e.GridCell.Value), SelfVars.LoginUserNames);
				LoadRCMakeDetail();
			}
			else if (e.GridCell.GridColumn.Name == "clmIsNormal_6mm")
			{
				FrmOverWeight frm = new FrmOverWeight(entity.MakeDetialId_6mm);
				frm.ShowDialog();
			}
			else if (e.GridCell.GridColumn.Name == "clmIsNormal_2mm")
			{
				FrmOverWeight frm = new FrmOverWeight(entity.MakeDetialId_2mm);
				frm.ShowDialog();
			}
		}
		#endregion

		#region 数据转换

		/// <summary>
		/// 临时类转换
		/// </summary>
		/// <param name="rCMakeDetail"></param>
		/// <returns></returns>
		private IList<MakeDetail> DataChange(IList<CmcsRCMakeDetail> rCMakeDetail)
		{
			IList<MakeDetail> makeDetails = new List<MakeDetail>();
			var MakeIds = from p in rCMakeDetail group p by new { p.MakeId, p.TheRCMake.MakeType } into g select new { g.Key.MakeId, g.Key.MakeType };
			foreach (var item in MakeIds)
			{
				CmcsRCMakeDetail makeDetail_2mm = rCMakeDetail.Where(a => (a.SampleType == "0.2mm分析样" || a.SampleType == "0.2mm存查样") && a.MakeId == item.MakeId).FirstOrDefault();
				CmcsRCMakeDetail makeDetail_6mm = rCMakeDetail.Where(a => a.SampleType == "6mm全水样" && a.MakeId == item.MakeId).FirstOrDefault();
				IList<CmcsRCAssay> assays = czyHandlerDAO.GetAssaysByMakeId(item.MakeId);
				string makeType = string.Empty;
				makeType = makeDetail_2mm != null && makeDetail_2mm.TheRCMake != null ? makeDetail_2mm.TheRCMake.MakeType : "";
				makeType = makeDetail_6mm != null && makeDetail_6mm.TheRCMake != null ? makeDetail_6mm.TheRCMake.MakeType : "";
				foreach (CmcsRCAssay assay in assays)
				{
					MakeDetail makeDetail = new MakeDetail();
					CmcsInFactoryBatch batch = commonDAO.SelfDber.Get<CmcsInFactoryBatch>(assay.InFactoryBatchId);
					makeDetail.AssayId = assay.Id;
					if (!string.IsNullOrEmpty(assay.AssayPoint)) makeDetail.AssayPoint = assay.AssayPoint;
					if (batch != null)
						makeDetail.BackBatchDate = batch.BACKBATCHDATE.ToString("yyyy-MM-dd");
					else
					{
						CmcsRCMakeDetail entity = rCMakeDetail.Where(a => a.MakeId == item.MakeId).FirstOrDefault();
						if (entity != null && entity.TheRCMake.TheRcAssay.AssayType == "外样化验")
							makeDetail.BackBatchDate = entity.TheRCMake.GetDate.ToString("yyyy-MM-dd");
						else
							makeDetail.BackBatchDate = entity.TheRCMake.UseTime.ToString("yyyy-MM-dd");
					}

					makeDetail.AssayCode = assay.AssayCode;
					makeDetail.MakeCode_2mm = makeDetail_2mm != null ? makeDetail_2mm.BarrelCode : "";
					makeDetail.CheckWeight_2mm = makeDetail_2mm != null ? makeDetail_2mm.CheckWeight.ToString() : "";
					makeDetail.Weight_2mm = makeDetail_2mm != null ? makeDetail_2mm.Weight.ToString() : "";
					makeDetail.MakeDetialId_2mm = makeDetail_2mm != null ? makeDetail_2mm.Id : "";
					makeDetail.PrintTime_2mm = makeDetail_2mm != null ? makeDetail_2mm.PrintTime.ToString() : "";

					if (makeType == "复查样化验")
					{
						makeDetail.MakeCode_6mm = "/";
						makeDetail.CheckWeight_6mm = "/";
						makeDetail.Weight_6mm = "/";
						makeDetail.MakeDetialId_6mm = "/";
						makeDetail.PrintTime_6mm = "/";
						makeDetail.IsCheck = makeDetail_2mm.IsCheck;
					}
					else
					{
						makeDetail.MakeCode_6mm = makeDetail_6mm != null ? makeDetail_6mm.BarrelCode : "";
						makeDetail.CheckWeight_6mm = makeDetail_6mm != null ? makeDetail_6mm.CheckWeight.ToString() : "";
						makeDetail.Weight_6mm = makeDetail_6mm != null ? makeDetail_6mm.Weight.ToString() : "";
						makeDetail.MakeDetialId_6mm = makeDetail_6mm != null ? makeDetail_6mm.Id : "";
						makeDetail.PrintTime_6mm = makeDetail_6mm != null ? makeDetail_6mm.PrintTime.ToString() : "";
						makeDetail.IsCheck = (makeDetail_6mm != null && makeDetail_6mm.IsCheck == 1 && makeDetail_2mm != null && makeDetail_2mm.IsCheck == 1) ? 1 : 0;
					}

					makeDetail.PrintCount = (makeDetail_6mm != null ? makeDetail_6mm.PrintCount.ToString() : "0") + "," + (makeDetail_2mm != null ? makeDetail_2mm.PrintCount.ToString() : "0");
					makeDetail.CheckType = (makeDetail_6mm != null ? makeDetail_6mm.CheckType : "") + " " + ((makeDetail_2mm != null ? makeDetail_2mm.CheckType : ""));
					makeDetail.CheckUser = (makeDetail_6mm != null ? makeDetail_6mm.CheckUser : "") + " " + ((makeDetail_2mm != null ? makeDetail_2mm.CheckUser : ""));
					makeDetails.Add(makeDetail);
				}
			}

			return makeDetails;
		}

		/// <summary>
		/// 临时类转换
		/// </summary>
		/// <param name="rCMakeDetail"></param>
		/// <returns></returns>
		private MakeDetail DataChange(CmcsRCMakeDetail rCMakeDetail)
		{
			this.IsChecked = false;
			MakeDetail makeDetail = new MakeDetail();
			CmcsRCAssay assay = czyHandlerDAO.GetAssayByMakeId(rCMakeDetail.MakeId);
			if (assay != null)
			{
				CmcsInFactoryBatch batch = commonDAO.SelfDber.Get<CmcsInFactoryBatch>(assay.InFactoryBatchId);
				makeDetail.AssayId = assay.Id;
				if (!string.IsNullOrEmpty(assay.AssayPoint)) makeDetail.AssayPoint = assay.AssayPoint;
				makeDetail.BackBatchDate = batch != null ? batch.BACKBATCHDATE.ToString("yyyy-MM-dd") : rCMakeDetail.TheRCMake.UseTime.ToString("yyyy-MM-dd");
				makeDetail.AssayCode = assay.AssayCode;
				if (rCMakeDetail.SampleType == "0.2mm分析样" || rCMakeDetail.SampleType == "0.2mm存查样")
				{
					makeDetail.MakeCode_2mm = rCMakeDetail.BarrelCode;
					makeDetail.CheckWeight_2mm = rCMakeDetail.CheckWeight.ToString();
					makeDetail.Weight_2mm = rCMakeDetail.Weight.ToString();
					makeDetail.MakeDetialId_2mm = rCMakeDetail.Id;
					makeDetail.PrintTime_2mm = rCMakeDetail.PrintTime.ToString();

					CmcsRCMakeDetail makeDetail_6mm = commonDAO.SelfDber.Entity<CmcsRCMakeDetail>("where MakeId=:MakeId and SampleType=:SampleType order by CreateDate desc", new { MakeId = rCMakeDetail.MakeId, SampleType = "6mm全水样" });
					if (makeDetail_6mm != null)
						makeDetail.IsCheck = (rCMakeDetail.IsCheck == 1 && makeDetail_6mm.IsCheck == 1) ? 1 : 0;
					else
						makeDetail.IsCheck = (rCMakeDetail.IsCheck == 1) ? 1 : 0;
				}
				else if (rCMakeDetail.SampleType == "6mm全水样")
				{
					makeDetail.MakeCode_6mm = rCMakeDetail.BarrelCode;
					makeDetail.CheckWeight_6mm = rCMakeDetail.CheckWeight.ToString();
					makeDetail.Weight_6mm = rCMakeDetail.Weight.ToString();
					makeDetail.MakeDetialId_6mm = rCMakeDetail.Id;
					makeDetail.PrintTime_6mm = rCMakeDetail.PrintTime.ToString();

					CmcsRCMakeDetail makeDetail_2mm = commonDAO.SelfDber.Entity<CmcsRCMakeDetail>("where MakeId=:MakeId and SampleType=:SampleType order by CreateDate desc", new { MakeId = rCMakeDetail.MakeId, SampleType = "0.2mm分析样" });
					if (makeDetail_2mm != null)
						makeDetail.IsCheck = (rCMakeDetail.IsCheck == 1 && makeDetail_2mm.IsCheck == 1) ? 1 : 0;
					else
						makeDetail.IsCheck = (rCMakeDetail.IsCheck == 1) ? 1 : 0;
				}
				this.IsChecked = makeDetail.IsCheck == 1;
				makeDetail.PrintCount = rCMakeDetail.PrintCount.ToString();

				makeDetail.CheckType = rCMakeDetail.CheckType;
				makeDetail.CheckUser = rCMakeDetail.CheckUser;
			}

			return makeDetail;
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

	class MakeDetail
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
		/// 6mm制样码
		/// </summary>
		public string MakeCode_6mm { get; set; }

		/// <summary>
		/// 6mm原始样重
		/// </summary>
		public string Weight_6mm { get; set; }

		/// <summary>
		/// 6mm校验样重
		/// </summary>
		public string CheckWeight_6mm { get; set; }

		/// <summary>
		/// 6mm制样Id
		/// </summary>
		public string MakeDetialId_6mm { get; set; }

		/// <summary>
		/// 0.2mm制样码
		/// </summary>
		public string MakeCode_2mm { get; set; }

		/// <summary>
		/// 0.2mm原始样重
		/// </summary>
		public string Weight_2mm { get; set; }

		/// <summary>
		/// 0.2mm校验样重
		/// </summary>
		public string CheckWeight_2mm { get; set; }

		/// <summary>
		/// 2mm制样Id
		/// </summary>
		public string MakeDetialId_2mm { get; set; }

		/// <summary>
		/// 打印次数
		/// </summary>
		public string PrintCount { get; set; }

		/// <summary>
		/// 6mm打印时间
		/// </summary>
		public string PrintTime_6mm { get; set; }

		/// <summary>
		/// 3mm打印时间
		/// </summary>
		public string PrintTime_2mm { get; set; }

		/// <summary>
		/// 操作人
		/// </summary>
		public string CheckUser { get; set; }

		/// <summary>
		/// 操作类型
		/// </summary>
		public string CheckType { get; set; }

		/// <summary>
		/// 是否核对
		/// </summary>
		public int IsCheck { get; set; }

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
