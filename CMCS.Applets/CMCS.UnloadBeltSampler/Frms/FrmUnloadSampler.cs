using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using CMCS.Common;
using CMCS.Common.DAO;
using CMCS.Common.Entities;
using CMCS.Common.Entities.AutoMaker;
using CMCS.Common.Entities.BaseInfo;
using CMCS.Common.Entities.BeltSampler;
using CMCS.Common.Entities.Fuel;
using CMCS.Common.Entities.Inf;
using CMCS.Common.Enums;
using CMCS.Common.Utilities;
using CMCS.Forms.UserControls;
using CMCS.Monitor.DAO;
using CMCS.UnloadSampler.DAO;
using CMCS.UnloadSampler.Enums;
using CMCS.UnloadSampler.Frms;
using CMCS.UnloadSampler.Utilities;
//
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Metro;
using DevComponents.DotNetBar.SuperGrid;

namespace CMCS.UnloadSampler.Frms
{
	public partial class FrmUnloadSampler : MetroForm
	{
		public FrmUnloadSampler()
		{
			InitializeComponent();
		}

		/// <summary>
		/// 窗体唯一标识符
		/// </summary>
		public static string UniqueKey = "FrmUnloadSampler";

		#region Vars

		CommonDAO commonDAO = CommonDAO.GetInstance();
		BeltSamplerDAO beltSamplerDAO = BeltSamplerDAO.GetInstance();

		bool isWorking = false;
		/// <summary>
		/// 正在工作
		/// </summary>
		public bool IsWorking
		{
			get { return isWorking; }
			set
			{
				isWorking = value;

				ChangeUIEnabled(!value);
			}
		}

		CmcsCMEquipment currentSampler;
		/// <summary>
		/// 当前选中的采样机
		/// </summary>
		public CmcsCMEquipment CurrentSampler
		{
			get { return currentSampler; }
			set
			{
				currentSampler = value;

				if (value != null)
				{
					LoadBeltSampleBarrel(superGridControl1, value.EquipmentCode);
					LoadLatestSampleUnloadCmd(value.EquipmentCode);
					if (value.EquipmentCode.Contains("入场"))
						CurrentMaker = makerMachineCodes[0];
					else if (value.EquipmentCode.Contains("出场"))
						CurrentMaker = makerMachineCodes[1];
					this.currentSampleCode = string.Empty;
				}
			}
		}

		/// <summary>
		/// 当前选中的制样机
		/// </summary>
		public string CurrentMaker { get; set; }

		/// <summary>
		/// 当前选中的采样码(与样罐保持一致的采样码)
		/// </summary>
		string currentSampleCode;

		CmcsRCSampling currentRCSampling;
		/// <summary>
		/// 当前选中的采样单
		/// </summary>
		public CmcsRCSampling CurrentRCSampling
		{
			get { return currentRCSampling; }
			set { currentRCSampling = value; }
		}

		/// <summary>
		/// 当前卸样命令id
		/// </summary>
		string currentUnloadCmdId;
		/// <summary>
		/// 返回消息
		/// </summary>
		string currentMessage;
		/// <summary>
		/// 当前选择的样罐信息
		/// </summary>
		public List<InfEquInfSampleBarrel> currentEquInfSampleBarrels = new List<InfEquInfSampleBarrel>();

		/// <summary>
		/// 采样机编码 默认#1采样机
		/// </summary>
		string[] samplerMachineCodes = new string[] { GlobalVars.MachineCode_InPDCYJ_1 };

		/// <summary>
		/// 制样机编码 默认#1全自动制样机
		/// </summary>
		string[] makerMachineCodes = new string[] { GlobalVars.MachineCode_QZDZYJ_2, GlobalVars.MachineCode_QZDZYJ_3 };

		Color[] CellColors = new Color[] { ColorTranslator.FromHtml("#7D00FFFF"), ColorTranslator.FromHtml("#7DFFFF00"), ColorTranslator.FromHtml("#7D7CFC00"), ColorTranslator.FromHtml("#7DFF69B4"), ColorTranslator.FromHtml("#7DFF00FF"), ColorTranslator.FromHtml("#7DADD8E6"), ColorTranslator.FromHtml("#7D00FF00"), ColorTranslator.FromHtml("#7DFFC0CB") };
		/// <summary>
		/// 分配的颜色
		/// </summary>
		Dictionary<string, Color> dicCellColors = new Dictionary<string, Color>();

		RTxtOutputer rTxtOutputer;

		#endregion

		/// <summary>
		/// 窗体初始化
		/// </summary>
		private void FormInit()
		{
			rTxtOutputer = new RTxtOutputer(rTxTMessageInfo);

			// 采样机设备编码，跟卸样程序一一对应
			samplerMachineCodes = commonDAO.GetAppletConfigString("采样机设备编码").Split('|');
			makerMachineCodes[0] = commonDAO.GetAppletConfigString("入厂制样机设备编码");
			makerMachineCodes[1] = commonDAO.GetAppletConfigString("出厂制样机设备编码");

			CreateSamplerButton();
			CreateEquStatus();

			// 触发选择第一台采样机
			if (flpanSamplerButton.Controls.Count > 0) (flpanSamplerButton.Controls[0] as RadioButton).Checked = true;
		}

		private void FrmUnloadSampler_Load(object sender, EventArgs e)
		{
			FormInit();
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{

		}

		#region 卸样业务

		/// <summary>
		/// 绑定集样罐信息
		/// </summary>
		/// <param name="superGridControl"></param>
		/// <param name="machineCode">采样机设备编码</param>
		private void LoadBeltSampleBarrel(SuperGridControl superGridControl, string machineCode)
		{
			superGridControl2.PrimaryGrid.Rows.Clear();

			List<InfEquInfSampleBarrel> list = commonDAO.SelfDber.Entities<InfEquInfSampleBarrel>("where MachineCode=:MachineCode order by BarrelType,BarrelNumber asc", new { MachineCode = machineCode });
			superGridControl.PrimaryGrid.DataSource = list;

			dicCellColors.Clear();

			foreach (InfEquInfSampleBarrel equInfSampleBarrel in list)
			{
				if (string.IsNullOrEmpty(equInfSampleBarrel.SampleCode)) continue;
				string key = equInfSampleBarrel.SampleCode;

				if (!dicCellColors.ContainsKey(key) && dicCellColors.Count < CellColors.Length) dicCellColors.Add(key, CellColors[dicCellColors.Count]);
			}
		}

		/// <summary>
		/// 检查集样罐信息是否已更新
		/// </summary>
		private bool CheckBeltSampleBarrelUpdated()
		{
			foreach (GridRow gridRow in superGridControl1.PrimaryGrid.Rows)
			{
				if (!gridRow.Checked) continue;

				InfEquInfSampleBarrel equInfSampleBarrel = gridRow.DataItem as InfEquInfSampleBarrel;
				InfEquInfSampleBarrel equInfSampleBarrelNew = commonDAO.SelfDber.Get<InfEquInfSampleBarrel>(equInfSampleBarrel.Id);
				if (equInfSampleBarrelNew != null)
				{
					if (String.IsNullOrEmpty(equInfSampleBarrelNew.SampleCode))
						return false;
				}
				else
					return false;
			}
			return true;
		}

		/// <summary>
		/// 选中采样码一致的记录
		/// </summary>
		/// <param name="superGridControl"></param>
		/// <param name="equInfSampleBarrel"></param> 
		private void CheckedSameBarrelRow(SuperGridControl superGridControl, InfEquInfSampleBarrel equInfSampleBarrel)
		{
			if (equInfSampleBarrel == null) return;
			if (string.IsNullOrWhiteSpace(equInfSampleBarrel.SampleCode)) return;

			this.currentEquInfSampleBarrels.Clear();
			this.currentSampleCode = equInfSampleBarrel.SampleCode;
			this.currentEquInfSampleBarrels.Add(equInfSampleBarrel);

			foreach (GridRow gridRow in superGridControl.PrimaryGrid.Rows)
			{
				InfEquInfSampleBarrel thisEquInfSampleBarrel = gridRow.DataItem as InfEquInfSampleBarrel;
				if (thisEquInfSampleBarrel == null || thisEquInfSampleBarrel.Id == equInfSampleBarrel.Id) continue;

				gridRow.Checked = (thisEquInfSampleBarrel != null && !string.IsNullOrWhiteSpace(thisEquInfSampleBarrel.SampleCode)
				   && thisEquInfSampleBarrel.SampleCode == equInfSampleBarrel.SampleCode && thisEquInfSampleBarrel.BarrelType == equInfSampleBarrel.BarrelType);

				if (gridRow.Checked) this.currentEquInfSampleBarrels.Add(thisEquInfSampleBarrel);
			}
		}

		/// <summary>
		/// 根据批次Id加载采样单列表
		/// </summary>
		/// <param name="superGridControl"></param>
		/// <param name="batchId"></param>
		private void LoadRCSamplingList(SuperGridControl superGridControl, string batchId)
		{
			this.CurrentRCSampling = null;

			List<CmcsRCSampling> list = MonitorDAO.GetInstance().GetSamplings(batchId);
			superGridControl.PrimaryGrid.DataSource = list;
		}

		/// <summary>
		/// 加载采样机最近几天的卸样记录
		/// </summary>
		/// <param name="samplerMachineCode">采样机编码</param>
		private void LoadLatestSampleUnloadCmd(string samplerMachineCode)
		{
			superGridControl3.PrimaryGrid.DataSource = commonDAO.SelfDber.TopEntities<InfBeltSampleUnloadCmd>(3, " where MachineCode='" + samplerMachineCode + "' order by createdate desc");
		}

		/// <summary>
		/// 发送制样计划
		/// </summary>
		/// <param name="rCSamplingId">采样单Id</param>
		/// <param name="infactoryBatchId">批次Id</param>
		/// <returns></returns>
		private bool SendMakePlan(string rCSamplingCode, string infactoryBatchId)
		{
			CmcsRCMake rcMake = AutoMakerDAO.GetInstance().GetRCMakeBySampleCode(rCSamplingCode);
			if (rcMake != null)
			{
				string fuelKindName = string.Empty;

				CmcsInFactoryBatch inFactoryBatch = commonDAO.GetBatchByRCSamplingCode(rCSamplingCode);
				if (inFactoryBatch != null)
				{
					CmcsFuelKind fuelKind = commonDAO.SelfDber.Get<CmcsFuelKind>(inFactoryBatch.FuelKindId);
					if (fuelKind != null) fuelKindName = fuelKind.FuelName;
				}

				// 需调整：发送的制样计划中煤种、颗粒度、水分等相关信息视接口而定
				InfMakerPlan makerPlan = new InfMakerPlan()
				{
					InterfaceType = commonDAO.GetMachineInterfaceTypeByCode(this.CurrentMaker),
					MachineCode = this.CurrentMaker,
					InFactoryBatchId = infactoryBatchId,
					MakeCode = rcMake.MakeCode,
					FuelKindName = fuelKindName,
					//Mt = "湿煤",
					MakeType = "在线制样",
					//CoalSize = "小粒度",
					SyncFlag = 0
				};
				AutoMakerDAO.GetInstance().SaveMakerPlanAndStartCmd(makerPlan, out currentMessage);

				rTxtOutputer.Output(currentMessage, eOutputType.Normal);

				return true;
			}
			else
				rTxtOutputer.Output("制样计划发送失败：未找到制样主记录信息", eOutputType.Error);

			return false;
		}

		#endregion

		#region 信号业务

		/// <summary>
		/// 生成采样机选项
		/// </summary>
		private void CreateSamplerButton()
		{
			foreach (string machineCode in samplerMachineCodes)
			{
				CmcsCMEquipment cMEquipment = commonDAO.GetCMEquipmentByMachineCode(machineCode);
				if (cMEquipment == null) continue;

				RadioButton rbtnSampler = new RadioButton();
				rbtnSampler.Font = new Font("Segoe UI", 13f, FontStyle.Regular);
				rbtnSampler.Text = cMEquipment.EquipmentName;
				rbtnSampler.Tag = cMEquipment;
				rbtnSampler.AutoSize = true;
				rbtnSampler.Padding = new System.Windows.Forms.Padding(10, 0, 0, 0);
				rbtnSampler.CheckedChanged += new EventHandler(rbtnSampler_CheckedChanged);

				flpanSamplerButton.Controls.Add(rbtnSampler);
			}
		}

		/// <summary>
		/// 创建皮带采样机、全自动制样机状态
		/// </summary>
		private void CreateEquStatus()
		{
			flpanEquState.SuspendLayout();

			foreach (string machineCode in samplerMachineCodes)
			{
				CmcsCMEquipment cMEquipment = commonDAO.GetCMEquipmentByMachineCode(machineCode);
				if (cMEquipment == null) continue;

				UCtrlSignalLight uCtrlSignalLight = new UCtrlSignalLight()
				{
					Anchor = AnchorStyles.Left,
					Tag = cMEquipment,
					Size = new System.Drawing.Size(20, 20),
					Padding = new System.Windows.Forms.Padding(10, 0, 0, 0)
				};
				SetSystemStatusToolTip(uCtrlSignalLight);

				flpanEquState.Controls.Add(uCtrlSignalLight);

				LabelX lblMachineName = new LabelX()
				{
					Text = cMEquipment.EquipmentName,
					Tag = cMEquipment,
					AutoSize = true,
					Anchor = AnchorStyles.Left,
					Font = new Font("Segoe UI", 12f, FontStyle.Regular)
				};

				flpanEquState.Controls.Add(lblMachineName);
			}
			foreach (string machineCode in makerMachineCodes)
			{

				// 制样机
				CmcsCMEquipment cMEquipmentMaker = commonDAO.GetCMEquipmentByMachineCode(machineCode);
				if (cMEquipmentMaker != null)
				{
					UCtrlSignalLight uCtrlSignalLightMaker = new UCtrlSignalLight()
					{
						Anchor = AnchorStyles.Left,
						Tag = cMEquipmentMaker,
						Size = new System.Drawing.Size(20, 20),
						Padding = new System.Windows.Forms.Padding(10, 0, 0, 0)
					};
					SetSystemStatusToolTip(uCtrlSignalLightMaker);

					flpanEquState.Controls.Add(uCtrlSignalLightMaker);

					LabelX lblMachineNameMaker = new LabelX()
					{
						Text = cMEquipmentMaker.EquipmentName,
						Tag = cMEquipmentMaker,
						AutoSize = true,
						Anchor = AnchorStyles.Left,
						Font = new Font("Segoe UI", 12f, FontStyle.Regular)
					};

					flpanEquState.Controls.Add(lblMachineNameMaker);
				}
			}

			flpanEquState.ResumeLayout();

			if (this.flpanEquState.Controls.Count == 0) MessageBoxEx.Show("皮带采样机或制样机参数未设置！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
		}

		/// <summary>
		/// 更新设备状态
		/// </summary>
		private void RefreshEquStatus()
		{
			foreach (UCtrlSignalLight uCtrlSignalLight in flpanEquState.Controls.OfType<UCtrlSignalLight>())
			{
				if (uCtrlSignalLight.Tag == null) continue;

				string machineCode = (uCtrlSignalLight.Tag as CmcsCMEquipment).EquipmentCode;
				if (string.IsNullOrEmpty(machineCode)) continue;

				string systemStatus = commonDAO.GetSignalDataValue(machineCode, eSignalDataName.设备状态.ToString());
				if (systemStatus == eEquInfSamplerSystemStatus.就绪待机.ToString())
					uCtrlSignalLight.LightColor = EquipmentStatusColors.BeReady;
				else if (systemStatus == eEquInfSamplerSystemStatus.正在运行.ToString() || systemStatus == eEquInfSamplerSystemStatus.正在卸样.ToString())
					uCtrlSignalLight.LightColor = EquipmentStatusColors.Working;
				else if (systemStatus == eEquInfSamplerSystemStatus.发生故障.ToString())
					uCtrlSignalLight.LightColor = EquipmentStatusColors.Breakdown;
			}
		}

		/// <summary>
		/// 设置ToolTip提示
		/// </summary>
		private void SetSystemStatusToolTip(Control control)
		{
			this.toolTip1.SetToolTip(control, "<绿色> 就绪待机\r\n<红色> 正在运行\r\n<黄色> 发生故障");
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			// 更新设备状态
			RefreshEquStatus();
		}

		#endregion

		#region 操作

		void rbtnSampler_CheckedChanged(object sender, EventArgs e)
		{
			RadioButton rbtnSampler = sender as RadioButton;
			this.CurrentSampler = rbtnSampler.Tag as CmcsCMEquipment;
		}

		TaskSimpleScheduler taskSimpleScheduler = new TaskSimpleScheduler();

		System.Threading.AutoResetEvent autoResetEvent = new AutoResetEvent(false);

		/// <summary>
		/// 发送卸样命令
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSendLoadCmd_Click(object sender, EventArgs e)
		{
			if (this.currentEquInfSampleBarrels.Count == 0 || string.IsNullOrEmpty(this.currentSampleCode))
			{
				MessageBoxEx.Show("请选择集样罐再发送", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			if (!CheckBeltSampleBarrelUpdated())
			{
				MessageBoxEx.Show("集样罐已更新，请刷新样罐信息", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				LoadBeltSampleBarrel(superGridControl1, this.currentSampler.EquipmentCode);
				return;
			}
			if (currentRCSampling == null)
			{
				MessageBoxEx.Show("请勾选绑定的采样单后再发送", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			// 检测采样机系统的状态
			string samplerSystemStatue = commonDAO.GetSignalDataValue(this.currentSampler.EquipmentCode, eSignalDataName.设备状态.ToString());
			if (samplerSystemStatue != eEquInfSamplerSystemStatus.就绪待机.ToString())
			{
				MessageBoxEx.Show("采样机系统未就绪，禁止卸样", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			// 检测制样机系统状态
			string makerSystemStatus = commonDAO.GetSignalDataValue(this.CurrentMaker, eSignalDataName.设备状态.ToString());
			if (rbtnToMaker.Checked && makerSystemStatus != eEquInfSamplerSystemStatus.就绪待机.ToString())
			{
				MessageBoxEx.Show("制样机系统未就绪，禁止卸样", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			string message = string.Empty;
			if (!beltSamplerDAO.CanSendSampleUnloadCmd(this.currentSampler.EquipmentCode, out message))
			{
				MessageBoxEx.Show(message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			SendSamplerUnloadCmd();
		}

		/// <summary>
		/// 监听采样机返回结果
		/// </summary> 
		/// <returns></returns>
		private void SendSamplerUnloadCmd()
		{
			taskSimpleScheduler = new TaskSimpleScheduler();

			autoResetEvent.Reset();

			taskSimpleScheduler.StartNewTask("卸样业务逻辑", () =>
			{
				this.IsWorking = true;

				if (rbtnToMaker.Checked)
				{
					#region 启动制样进料皮带
					if (beltSamplerDAO.SendPDUnLoadCmd(this.CurrentMaker, "开始卸料"))
					{
						rTxtOutputer.Output("卸样皮带命令发送成功，等待制样机执行", eOutputType.Normal);

						int waitCount = 0;
						eEquInfCmdResultCode equInfCmdResultCode;
						do
						{
							Thread.Sleep(10000);
							if (waitCount % 5 == 0) rTxtOutputer.Output("正在等待制样机返回结果", eOutputType.Normal);

							waitCount++;

							// 获取卸样命令的执行结果
							equInfCmdResultCode = UnloadSamplerDAO.GetInstance().GetPDUnloadState();
						}
						while (equInfCmdResultCode == eEquInfCmdResultCode.默认);
						if (equInfCmdResultCode == eEquInfCmdResultCode.成功)
						{
							rTxtOutputer.Output("制样机返回：皮带启动成功", eOutputType.Normal);
						}
						else if (equInfCmdResultCode == eEquInfCmdResultCode.失败)
						{
							rTxtOutputer.Output("制样机返回：执行失败", eOutputType.Error);
						}
					}
					else
					{
						rTxtOutputer.Output("卸样皮带命令发送失败", eOutputType.Error);
					}
					#endregion
				}

				#region 发送采样机卸样命令
				// 发送卸样命令
				if (beltSamplerDAO.SendSampleUnloadCmd(this.currentSampler.EquipmentCode, this.CurrentRCSampling.Id, this.currentSampleCode, (eEquInfSamplerUnloadType)Convert.ToInt16(flpanUnloadType.Controls.OfType<RadioButton>().First(a => a.Checked).Tag), out this.currentUnloadCmdId))
				{
					rTxtOutputer.Output("卸样命令发送成功，等待采样机执行", eOutputType.Normal);

					int waitCount = 0;
					eEquInfCmdResultCode equInfCmdResultCode;
					do
					{
						Thread.Sleep(10000);
						if (waitCount % 5 == 0) rTxtOutputer.Output("正在等待采样机返回结果", eOutputType.Normal);

						waitCount++;

						// 获取卸样命令的执行结果
						equInfCmdResultCode = UnloadSamplerDAO.GetInstance().GetUnloadSamplerState(this.currentUnloadCmdId);
					}
					while (equInfCmdResultCode == eEquInfCmdResultCode.默认);

					if (equInfCmdResultCode == eEquInfCmdResultCode.成功)
					{
						rTxtOutputer.Output("采样机返回：卸样成功", eOutputType.Normal);
					}
					else if (equInfCmdResultCode == eEquInfCmdResultCode.失败)
					{
						rTxtOutputer.Output("采样机返回：卸样失败", eOutputType.Error);
						return;
					}
				}
				else
				{
					rTxtOutputer.Output("卸样命令发送失败", eOutputType.Error);
					return;
				}
				#endregion

				#region 停止制样机进料皮带
				if (beltSamplerDAO.SendPDUnLoadCmd(this.CurrentMaker, "停止卸料"))//卸样完成后 卸料皮带停止
				{
					rTxtOutputer.Output("卸样皮带停止命令发送成功，等待制样机执行", eOutputType.Normal);
					int waitCount = 0;
					eEquInfCmdResultCode equInfCmdResultCode;
					do
					{
						Thread.Sleep(10000);
						if (waitCount % 5 == 0) rTxtOutputer.Output("正在等待制样机返回结果", eOutputType.Normal);

						waitCount++;

						// 获取卸样命令的执行结果
						equInfCmdResultCode = UnloadSamplerDAO.GetInstance().GetPDUnloadState();
					}
					while (equInfCmdResultCode == eEquInfCmdResultCode.默认);
					if (equInfCmdResultCode == eEquInfCmdResultCode.成功)
					{
						rTxtOutputer.Output("制样机返回：皮带停止成功", eOutputType.Normal);
					}
					else if (equInfCmdResultCode == eEquInfCmdResultCode.失败)
					{
						rTxtOutputer.Output("制样机返回：执行失败", eOutputType.Error);
						return;
					}
				}
				else
				{
					rTxtOutputer.Output("卸样皮带命令发送失败", eOutputType.Error);
					return;
				}
				#endregion

				#region 检测制样机状态，若已就绪提示发送制样计划
				// 检测制样机系统状态
				if (rbtnToMaker.Checked && this.currentEquInfSampleBarrels != null && this.currentEquInfSampleBarrels[0].BarrelType == "底卸式")
				{
					string makerSystemStatus = commonDAO.GetSignalDataValue(this.CurrentMaker, eSignalDataName.设备状态.ToString());
					if (makerSystemStatus == eEquInfSamplerSystemStatus.就绪待机.ToString())
					{
						if (MessageBoxEx.Show("卸样成功，检测到制样机已就绪，立刻开始制样？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
						{
							if (SendMakePlan(this.CurrentRCSampling.SampleCode, this.CurrentRCSampling.InFactoryBatchId))
								rTxtOutputer.Output("制样命令发送成功", eOutputType.Normal);
							else
								rTxtOutputer.Output("制样命令发送失败", eOutputType.Error);
						}
					}
				}
				#endregion

				this.IsWorking = false;

				LoadBeltSampleBarrel(superGridControl1, this.CurrentSampler.EquipmentCode);
				LoadLatestSampleUnloadCmd(this.CurrentSampler.EquipmentCode);

				autoResetEvent.Set();
			});
		}

		/// <summary>
		/// 更改界面控件的可用属性
		/// </summary>
		/// <param name="enabled"></param>
		private void ChangeUIEnabled(bool enabled)
		{
			this.InvokeEx(() =>
			{
				btnSendLoadCmd.Enabled = enabled;
				btnSendMakeCmd.Enabled = enabled;

				superGridControl1.PrimaryGrid.ReadOnly = !enabled;
				superGridControl2.PrimaryGrid.ReadOnly = !enabled;
				superGridControl3.PrimaryGrid.ReadOnly = !enabled;

				rbtnToMaker.Enabled = enabled;
				rbtnToSubway.Enabled = enabled;

				foreach (RadioButton radioButton in flpanSamplerButton.Controls.OfType<RadioButton>())
				{
					radioButton.Enabled = enabled;
				}
			});
		}

		/// <summary>
		/// 发送制样计划
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSendMakeCmd_Click(object sender, EventArgs e)
		{
			InfBeltSampleUnloadCmd beltSampleUnloadCmd = null;
			foreach (GridRow gridRow in superGridControl3.PrimaryGrid.Rows)
			{
				if (gridRow.Checked)
					beltSampleUnloadCmd = gridRow.DataItem as InfBeltSampleUnloadCmd;
			}
			if (beltSampleUnloadCmd == null)
			{
				MessageBoxEx.Show("请选择卸样记录", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			CmcsRCSampleBarrel rCSampleBarrel = commonDAO.SelfDber.Entity<CmcsRCSampleBarrel>(" where SampleCode='" + beltSampleUnloadCmd.SampleCode + "'");
			if (rCSampleBarrel == null)
			{
				MessageBoxEx.Show("未找到采样单明细记录", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			if (MessageBoxEx.Show("确定要发送制样命令？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
				SendMakePlan(rCSampleBarrel.SampleCode, rCSampleBarrel.InFactoryBatchId);
		}

		#endregion

		#region SuperGridControl

		private void superGridControl1_BeforeCheck(object sender, GridBeforeCheckEventArgs e)
		{
			GridRow gridRow = (e.Item as GridRow);
			if (gridRow == null || gridRow.Checked) { e.Cancel = true; return; }

			InfEquInfSampleBarrel equInfSampleBarrel = gridRow.DataItem as InfEquInfSampleBarrel;

			if (!string.IsNullOrEmpty(equInfSampleBarrel.SampleCode))
			{
				// 选中其他相同采样码的样罐记录
				CheckedSameBarrelRow(sender as SuperGridControl, equInfSampleBarrel);
				// 加载采样单
				LoadRCSamplingList(superGridControl2, equInfSampleBarrel.InFactoryBatchId);
			}
			else e.Cancel = true;
		}

		private void superGridControl2_BeforeCheck(object sender, GridBeforeCheckEventArgs e)
		{
			GridRow gridRow = (e.Item as GridRow);
			if (gridRow == null || gridRow.Checked) { e.Cancel = true; return; }

			this.CurrentRCSampling = gridRow.DataItem as CmcsRCSampling;
			e.Cancel = string.IsNullOrEmpty(this.CurrentRCSampling.SampleCode);

			// 取消其他行的选中状态
			foreach (GridRow gridRowItem in superGridControl2.PrimaryGrid.Rows)
			{
				CmcsRCSampling rCSampling = gridRowItem.DataItem as CmcsRCSampling;
				if (rCSampling.Id == this.CurrentRCSampling.Id) continue;

				gridRowItem.Checked = false;
			}
		}

		private void superGridControl3_CellClick(object sender, GridCellClickEventArgs e)
		{
			InfBeltSampleUnloadCmd sampleUnloadCmd = e.GridCell.GridRow.DataItem as InfBeltSampleUnloadCmd;

			foreach (GridRow gridRow in superGridControl3.PrimaryGrid.Rows)
			{
				InfBeltSampleUnloadCmd beltSampleUnloadCmd = gridRow.DataItem as InfBeltSampleUnloadCmd;
				gridRow.Checked = (beltSampleUnloadCmd != null && sampleUnloadCmd.Id == beltSampleUnloadCmd.Id);
			}
		}

		private void superGridControl1_BeginEdit(object sender, GridEditEventArgs e)
		{
			// 取消编辑
			e.Cancel = true;
		}

		private void superGridControl2_BeginEdit(object sender, GridEditEventArgs e)
		{
			// 取消编辑
			e.Cancel = true;
		}

		private void superGridControl3_BeginEdit(object sender, GridEditEventArgs e)
		{
			// 取消编辑
			e.Cancel = true;
		}

		/// <summary>
		/// 设置行号
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void superGridControl_GetRowHeaderText(object sender, GridGetRowHeaderTextEventArgs e)
		{
			e.Text = (e.GridRow.RowIndex + 1).ToString();
		}

		private void superGridControl1_GetCellStyle(object sender, GridGetCellStyleEventArgs e)
		{
			if (e.GridCell.GridColumn.DataPropertyName == "SampleCode")
			{
				InfEquInfSampleBarrel equInfSampleBarrel = e.GridCell.GridRow.DataItem as InfEquInfSampleBarrel;
				if (equInfSampleBarrel != null && !string.IsNullOrEmpty(equInfSampleBarrel.SampleCode)) e.Style.Background.Color1 = this.dicCellColors[equInfSampleBarrel.SampleCode];
			}
		}
		#endregion

		#region 其他函数

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
