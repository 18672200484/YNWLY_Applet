using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CMCS.Common;
using CMCS.Common.DAO;
using CMCS.Common.Entities;
using CMCS.Common.Enums;
using CMCS.WeighCheck.DAO;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using DevComponents.DotNetBar.Metro;
using CMCS.WeighCheck.SampleMake.Enums;
using CMCS.Common.Utilities;
using CMCS.WeighCheck.SampleMake.Frms;
using CMCS.Forms.UserControls;
using CMCS.Common.Entities.Fuel;
using DevComponents.DotNetBar.SuperGrid;
using CMCS.Common.Entities.iEAA;
using RW.HFReader;
using CMCS.WeighCheck.SampleMake.Utilities;
using CMCS.WeighCheck.SampleMake.Core;

namespace CMCS.WeighCheck.SampleMake.Frms.Make
{
	public partial class FrmMakeWeight : MetroForm
	{
		public FrmMakeWeight()
		{
			InitializeComponent();
		}

		/// <summary>
		/// 窗体唯一标识符
		/// </summary>
		public static string UniqueKey = "FrmMakeWeight";

		#region Vars

		CommonDAO commonDAO = CommonDAO.GetInstance();
		CZYHandlerDAO czyHandlerDAO = CZYHandlerDAO.GetInstance();
		CodePrinterMake _CodePrinter = null;

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

		string resMessage = string.Empty;

		string assayTarget = string.Empty;
		#endregion

		/// <summary>
		/// 初始化
		/// </summary>
		public void InitFrom()
		{
			cmbAssayType.Items.Add("三级编码化验");
			cmbAssayType.Items.Add("抽查样化验");
			cmbAssayType.Items.Add("复查样化验");
			cmbAssayType.SelectedIndex = 0;

			this.IsUseWeight = Convert.ToBoolean(commonDAO.GetAppletConfigInt32("电子天平启用称重"));
			this._CodePrinter = new CodePrinterMake(printDocument1);

			// 生成编码按钮
			GridButtonXEditControl btnNewCode = superGridControl1.PrimaryGrid.Columns["gclmNewCode"].EditControl as GridButtonXEditControl;
			btnNewCode.ColorTable = eButtonColor.BlueWithBackground;
			btnNewCode.Click += new EventHandler(btnNewCode_Click);
			// 打印编码按钮
			GridButtonXEditControl btnPrintCode = superGridControl1.PrimaryGrid.Columns["gclmPrintCode"].EditControl as GridButtonXEditControl;
			btnPrintCode.ColorTable = eButtonColor.BlueWithBackground;
			btnPrintCode.Click += new EventHandler(btnPrintCode_Click);
			// 写入编码按钮
			GridButtonXEditControl btnWriteCode = superGridControl1.PrimaryGrid.Columns["gclmWriteCode"].EditControl as GridButtonXEditControl;
			btnWriteCode.ColorTable = eButtonColor.BlueWithBackground;
			btnWriteCode.Click += new EventHandler(btnWriteCode_Click);
		}

		/// <summary>
		/// 对窗体及其所有子控件进行双重缓冲
		/// </summary>
		protected override CreateParams CreateParams
		{
			get
			{
				CreateParams cp = base.CreateParams;
				cp.ExStyle |= 0x02000000;
				return cp;
			}
		}

		private void FrmMakeWeight_Load(object sender, EventArgs e)
		{
			// 初始化
			InitFrom();
			// 初始化设备
			InitHardware();
		}

		private void FrmMakeWeight_FormClosing(object sender, FormClosingEventArgs e)
		{
			Hardwarer.ReadRwer.OnStatusChange -= new HFReaderRwer.StatusChangeHandler(Rwer_OnStatusChange);
			UnloadHardware();
		}

		#region 电子天平

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
		/// 启用电子天平
		/// </summary>
		public bool IsUseWeight
		{
			get { return isUseWeight; }
			set
			{
				isUseWeight = value;
				lblweight.Visible = value;
				lbl_CurrentWeight.Visible = value;
				lblWber.Visible = value;
				slightWber.Visible = value;
			}
		}

		bool wbSteady = false;
		/// <summary>
		/// 电子天平仪表稳定状态
		/// </summary>
		public bool WbSteady
		{
			get { return wbSteady; }
			set
			{
				wbSteady = value;
			}
		}

		double wbMinWeight = 0;
		/// <summary>
		/// 电子天平仪表最小称重 单位：吨
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
		/// 电子天平状态变化
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

		#region 读卡器

		/// <summary>
		/// 读卡成功事件
		/// </summary>
		/// <param name="steady"></param>
		void Rwer_OnReadSuccess(string rfid)
		{
			InvokeEx(() =>
			{
				txtInputMakeCode.Text = rfid.ToUpper();
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
				if (status) ShowMessage("读卡器连接成功", eOutputType.Normal);
				else ShowMessage("读卡器连接失败", eOutputType.Error);
				slightRwer.LightColor = (status ? Color.Green : Color.Red);
			});
		}

		/// <summary>
		/// 读卡
		/// </summary>
		/// <returns></returns>
		private string ReadRf()
		{
			byte SecNumber = Convert.ToByte(commonDAO.GetAppletConfigInt32("读卡器扇区"));
			byte BlockNumber = Convert.ToByte(commonDAO.GetAppletConfigInt32("读卡器块区"));

			if (Hardwarer.ReadRwer.OpenRF())
			{
				ShowMessage("射频打开成功", eOutputType.Normal);
			}
			else
			{
				ShowMessage("射频打开失败", eOutputType.Error);
				return string.Empty;
			}

			if (Hardwarer.ReadRwer.ChangeToISO14443A())
			{
				ShowMessage("切换到1443模式成功", eOutputType.Normal);
			}
			else
			{
				ShowMessage("切换到1443模式失败", eOutputType.Error);
				return string.Empty;
			}

			if (Hardwarer.ReadRwer.Request14443A())
			{
				ShowMessage("获取卡类型成功", eOutputType.Normal);
			}
			else
			{
				ShowMessage("获取卡类型失败", eOutputType.Error);
				return string.Empty;
			}

			if (Hardwarer.ReadRwer.Anticoll14443A())
			{
				ShowMessage("获取卡号成功", eOutputType.Normal);
			}
			else
			{
				ShowMessage("获取卡号失败", eOutputType.Error);
				return string.Empty;
			}

			if (Hardwarer.ReadRwer.Select14443A())
			{
				ShowMessage("获取卡容量成功", eOutputType.Normal);
			}
			else
			{
				ShowMessage("获取卡容量失败", eOutputType.Error);
				return string.Empty;
			}

			if (Hardwarer.ReadRwer.AuthKey14443A(SecNumber, BlockNumber))
			{
				ShowMessage("标签密钥验证成功", eOutputType.Normal);
			}
			else
			{
				ShowMessage("标签密钥验证失败", eOutputType.Error);
				return string.Empty;
			}
			if (Hardwarer.ReadRwer.RWRead14443A(SecNumber, BlockNumber) != string.Empty)
			{
				ShowMessage("读卡成功", eOutputType.Normal);
				return Hardwarer.ReadRwer.Byte16ToString(Hardwarer.ReadRwer.ReadData);
			}
			return string.Empty;
		}

		/// <summary>
		/// 写卡
		/// </summary>
		/// <returns></returns>
		private string WriteRf(string rf)
		{
			byte SecNumber = Convert.ToByte(commonDAO.GetAppletConfigInt32("读卡器扇区"));
			byte BlockNumber = Convert.ToByte(commonDAO.GetAppletConfigInt32("读卡器块区"));

			if (Hardwarer.ReadRwer.OpenRF())
			{
				ShowMessage("射频打开成功", eOutputType.Normal);
			}
			else
			{
				ShowMessage("射频打开失败", eOutputType.Error);
				return string.Empty;
			}

			if (Hardwarer.ReadRwer.ChangeToISO14443A())
			{
				ShowMessage("切换到1443模式成功", eOutputType.Normal);
			}
			else
			{
				ShowMessage("切换到1443模式失败", eOutputType.Error);
				return string.Empty;
			}

			if (Hardwarer.ReadRwer.Request14443A())
			{
				ShowMessage("获取卡类型成功", eOutputType.Normal);
			}
			else
			{
				ShowMessage("获取卡类型失败", eOutputType.Error);
				return string.Empty;
			}

			if (Hardwarer.ReadRwer.Anticoll14443A())
			{
				ShowMessage("获取卡号成功", eOutputType.Normal);
			}
			else
			{
				ShowMessage("获取卡号失败", eOutputType.Error);
				return string.Empty;
			}

			if (Hardwarer.ReadRwer.Select14443A())
			{
				ShowMessage("获取卡容量成功", eOutputType.Normal);
			}
			else
			{
				ShowMessage("获取卡容量失败", eOutputType.Error);
				return string.Empty;
			}

			if (Hardwarer.ReadRwer.AuthKey14443A(SecNumber, BlockNumber))
			{
				ShowMessage("标签密钥验证成功", eOutputType.Normal);
			}
			else
			{
				ShowMessage("标签密钥验证失败", eOutputType.Error);
				return string.Empty;
			}
			if (Hardwarer.ReadRwer.Write14443(rf, Convert.ToInt32(SecNumber), Convert.ToInt32(BlockNumber)))
			{
				ShowMessage("编码：" + rf + "写卡成功", eOutputType.Normal);
				string rf_new = Hardwarer.ReadRwer.Byte16ToString(Hardwarer.ReadRwer.RWRead14443A(SecNumber, BlockNumber));
				if (rf == rf_new)
				{
					ShowMessage("编码：" + rf + "读卡验证成功", eOutputType.Normal);
					return rf_new;
				}
			}

			return string.Empty;
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
				Hardwarer.ReadRwer.OnStatusChange += new HFReaderRwer.StatusChangeHandler(Rwer_OnStatusChange);
				Rwer_OnStatusChange(Hardwarer.ReadRwer.Status);
				if (!SelfVars.RfReadOpen)
				{
					// 初始化-读卡器
					success = Hardwarer.ReadRwer.OpenNetPort(commonDAO.GetAppletConfigString("读卡器IP"), commonDAO.GetAppletConfigInt32("读卡器端口"));
					SelfVars.RfReadOpen = success;
				}
				// 初始化-电子天平
				if (IsUseWeight)
				{
					this.WbMinWeight = commonDAO.GetAppletConfigDouble("电子天平最小重量");

					Hardwarer.Wber_min.OnStatusChange += new WB.XiangPing.Balance.XiangPing_Balance.StatusChangeHandler(Wber_OnStatusChange);
					Hardwarer.Wber_min.OnSteadyChange += new WB.XiangPing.Balance.XiangPing_Balance.SteadyChangeEventHandler(Wber_OnSteadyChange);
					Hardwarer.Wber_min.OnWeightChange += new WB.XiangPing.Balance.XiangPing_Balance.WeightChangeEventHandler(wber_OnWeightChange);

					if (!SelfVars.WeightMinOpen)
					{
						success = Hardwarer.Wber_min.OpenCom(commonDAO.GetAppletConfigInt32("电子天平串口"), commonDAO.GetAppletConfigInt32("电子天平波特率"), commonDAO.GetAppletConfigInt32("电子天平数据位"), commonDAO.GetAppletConfigInt32("电子天平停止位"));
						SelfVars.WeightMinOpen = success;
					}
				}
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
				Hardwarer.Wber_min.CloseCom();
			}
			catch { }
		}
		#endregion

		#region 业务

		/// <summary>
		///  重置流程信息
		/// </summary>
		private void Restet()
		{
			this.CurrentFlowFlag = eFlowFlag.等待扫码;

			txtInputMakeCode.ResetText();
			rtxtMakeWeightInfo.ResetText();

			// 方便客户快速使用，获取焦点
			txtInputMakeCode.Focus();
		}

		#endregion

		#region 操作

		/// <summary>
		/// 键入Enter检测有效性
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void txtInputMakeCode_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				if (!String.IsNullOrEmpty(txtInputMakeCode.Text.Trim()))
				{
					if (cmbAssayType.Text == "三级编码化验")
						LoadRCMakeDetail();
					else
					{
						if (string.IsNullOrEmpty(assayTarget))
						{
							MessageBoxEx.Show("请选择化验指标", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
							return;
						}
						string makeCode = txtInputMakeCode.Text.Trim();
						czyHandlerDAO.CreateMakeAndAssay(ref makeCode, cmbAssayType.Text, SelfVars.LoginUser.UserName, assayTarget.TrimEnd(','));
						txtInputMakeCode.Text = makeCode;
						LoadRCMakeDetail();
					}
				}
			}
		}

		/// <summary>
		/// 选择化验类型
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void cmbAssayType_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (cmbAssayType.Text == "三级编码化验")
			{
				this.panelAssayTarget.Visible = false;
				this.panelAssayTargetType.Visible = false;
			}
			else
			{
				this.panelAssayTarget.Visible = true;
				this.panelAssayTargetType.Visible = true;
			}
		}

		/// <summary>
		/// 清空
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void txtMakeWeightCode_ButtonCustomClick(object sender, EventArgs e)
		{
			Restet();
		}

		private void btnReset_Click(object sender, EventArgs e)
		{
			Restet();
		}
		/// <summary>
		/// 加载制样明细记录
		/// </summary>
		private void LoadRCMakeDetail()
		{
			CmcsRCMake rCMake = czyHandlerDAO.GetRCMake(txtInputMakeCode.Text.Trim().ToUpper());
			if (rCMake != null)
			{
				List<CmcsRCMakeDetail> rCMakeDetails = czyHandlerDAO.GetRCMakeDetails(txtInputMakeCode.Text.Trim().ToUpper(), out resMessage);
				if (rCMakeDetails.Count == 0)
				{
					ShowMessage(resMessage, eOutputType.Error);
					if (MessageBoxEx.Show("该制样单无制样明细，是否生成制样明细？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
					{
						rCMakeDetails = CreateRcMakeDetail(rCMake);
					}
				}
				else
				{
					this.CurrentFlowFlag = eFlowFlag.样品登记;
				}
				superGridControl1.PrimaryGrid.DataSource = rCMakeDetails;
			}
			else
			{
				ShowMessage("未找到制样信息", eOutputType.Error);
			}
		}

		/// <summary>
		/// 生成制样明细
		/// </summary>
		/// <param name="rcMake"></param>
		/// <returns></returns>
		public List<CmcsRCMakeDetail> CreateRcMakeDetail(CmcsRCMake rcMake)
		{
			IList<CodeContent> maketype = commonDAO.GetCodeContentByKind("样品类型");
			foreach (CodeContent item in maketype)
			{
				CmcsRCMakeDetail makedetail = commonDAO.SelfDber.Entity<CmcsRCMakeDetail>("where MakeId=:MakeId and SampleType=:SampleType", new { MakeId = rcMake.Id, SampleType = item.Content });
				if (makedetail == null)
				{
					makedetail = new CmcsRCMakeDetail();
					makedetail.MakeId = rcMake.Id;
					makedetail.SampleType = item.Content;
					makedetail.BarrelCode = commonDAO.CreateNewMakeBarrelCodeByMakeCode(rcMake.MakeCode, item.Content);
					commonDAO.SelfDber.Insert(makedetail);
				}
			}
			return commonDAO.SelfDber.Entities<CmcsRCMakeDetail>("where MakeId=:MakeId", new { MakeId = rcMake.Id });
		}

		/// <summary>
		/// 生成编码
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void btnNewCode_Click(object sender, EventArgs e)
		{
			GridButtonXEditControl btn = sender as GridButtonXEditControl;
			if (btn == null) return;

			CmcsRCMakeDetail rCMakeDetail = btn.EditorCell.GridRow.DataItem as CmcsRCMakeDetail;
			if (rCMakeDetail == null) return;

			if (!string.IsNullOrEmpty(rCMakeDetail.BarrelCode) && MessageBoxEx.Show("样罐编码已存在，确定要重新生成？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
				return;

			// 生成随机样罐编码
			string newBarrelCode = commonDAO.CreateNewMakeBarrelCodeByMakeCode(rCMakeDetail.TheRCMake.MakeCode, rCMakeDetail.SampleType);
			// 称重校验
			if (IsUseWeight)
			{
				if (Hardwarer.Wber_min.Status && Hardwarer.Wber_min.Weight > 0 && Hardwarer.Wber_min.Weight > WbMinWeight)
				{
					rCMakeDetail.BarrelCode = newBarrelCode;
					rCMakeDetail.Weight = Hardwarer.Wber_min.Weight;

					czyHandlerDAO.UpdateMakeDetailWeightAndBarrelCode(rCMakeDetail.Id, Hardwarer.Wber_min.Weight, newBarrelCode);
				}
				else
					MessageBoxEx.Show("未检测到重量", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
			// 不称重校验
			else
			{
				rCMakeDetail.BarrelCode = newBarrelCode;

				czyHandlerDAO.UpdateMakeDetailWeightAndBarrelCode(rCMakeDetail.Id, 0, newBarrelCode);
			}
		}

		/// <summary>
		/// 打印编码
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void btnPrintCode_Click(object sender, EventArgs e)
		{
			GridButtonXEditControl btn = sender as GridButtonXEditControl;
			if (btn == null) return;

			CmcsRCMakeDetail rCMakeDetail = btn.EditorCell.GridRow.DataItem as CmcsRCMakeDetail;
			if (rCMakeDetail == null) return;
			czyHandlerDAO.UpdateSendPle(rCMakeDetail, SelfVars.LoginUser.UserName);
			if (!string.IsNullOrEmpty(rCMakeDetail.BarrelCode))
				_CodePrinter.Print(rCMakeDetail.BarrelCode, rCMakeDetail.SampleType);
			else
				MessageBoxEx.Show("请先点击[生成]按钮生成样罐编码", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
		}

		/// <summary>
		/// 写入编码
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void btnWriteCode_Click(object sender, EventArgs e)
		{
			GridButtonXEditControl btn = sender as GridButtonXEditControl;
			if (btn == null) return;

			CmcsRCMakeDetail rCMakeDetail = btn.EditorCell.GridRow.DataItem as CmcsRCMakeDetail;
			if (rCMakeDetail == null) return;
			czyHandlerDAO.UpdateSendPle(rCMakeDetail, SelfVars.LoginUser.UserName);
			if (!string.IsNullOrEmpty(rCMakeDetail.BarrelCode))
				WriteRf(rCMakeDetail.BarrelCode);
			else
				MessageBoxEx.Show("请先点击[生成]按钮生成样罐编码", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
		}


		private void assayTarget_CheckedChanged(object sender, EventArgs e)
		{
			CheckBoxX check = (CheckBoxX)sender;
			if (check.Checked && !this.assayTarget.Contains(check.Text))
			{
				this.assayTarget += check.Text + ",";
			}
			else
			{
				this.assayTarget = this.assayTarget.Replace(check.Text + ",", "");
			}
		}

		/// <summary>
		/// 选择化验指标
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void rbtnHandChoose_CheckedChanged(object sender, EventArgs e)
		{
			RadioButton rbtn = (RadioButton)sender;
			if (rbtn.Tag.ToString() == "手选指标")
			{
				foreach (Control item in this.panelAssayTarget.Controls)
				{
					CheckBoxX checkother = (CheckBoxX)item;
					checkother.Checked = false;
				}
			}
			else if (rbtn.Tag.ToString() == "全指标")
			{
				foreach (Control item in this.panelAssayTarget.Controls)
				{
					CheckBoxX checkother = (CheckBoxX)item;
					checkother.Checked = true;
					//this.assayTarget += checkother.Text + ",";
				}
			}
			else if (rbtn.Tag.ToString() == "日常分析")
			{
				foreach (Control item in this.panelAssayTarget.Controls)
				{
					CheckBoxX checkother = (CheckBoxX)item;
					if (checkother.Text == "氢值" || checkother.Text == "灰熔融")
					{
						checkother.Checked = false;
						continue;
					}
					checkother.Checked = true;
					//this.assayTarget += checkother.Text + ",";
				}
			}
		}

		#region DataGridView
		private void superGridControl1_BeginEdit(object sender, GridEditEventArgs e)
		{
			// 取消编辑
			e.Cancel = true;
		}
		#endregion

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

	}
}
