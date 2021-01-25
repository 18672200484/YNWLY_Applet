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
using CMCS.Common.Entities.CarTransport;
using CMCS.Common.Entities.PackingBatch;

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

		#region 业务处理类
		CommonDAO commonDAO = CommonDAO.GetInstance();
		BeltSamplerDAO beltSamplerDAO = BeltSamplerDAO.GetInstance();
		QCJXCYSamplerDAO qcjxcySamplerDAO = QCJXCYSamplerDAO.GetInstance();

		#endregion

		#region 公共Vars

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
					LoadSampleBarrel(superGridControl1, value.EquipmentCode);
					LoadLatestSampleUnloadCmd(value);

					this.currentSampleCode = string.Empty;
				}
			}
		}

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

		InfPackingBatchCoord currentCoord;
		/// <summary>
		/// 当前选中的归批机样桶
		/// </summary>
		public InfPackingBatchCoord CurrentCoord
		{
			get { return currentCoord; }
			set { currentCoord = value; }
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
		/// 当前选择的采样机样罐信息
		/// </summary>
		public List<InfEquInfSampleBarrel> currentEquInfSampleBarrels = new List<InfEquInfSampleBarrel>();

		/// <summary>
		/// 采样机编码 默认#1采样机
		/// </summary>
		string[] samplerMachineCodes = new string[] { GlobalVars.MachineCode_QC_JxSampler_1 };

		/// <summary>
		/// 制样机编码 默认#1全自动制样机
		/// </summary>
		string makerMachineCode = GlobalVars.MachineCode_QZDZYJ_1;

		/// <summary>
		/// 归批机编码
		/// </summary>
		string packerMachineCode = GlobalVars.MachineCode_PackingBatch_KY;

		Color[] CellColors = new Color[] { ColorTranslator.FromHtml("#7D00FFFF"), ColorTranslator.FromHtml("#7DFFFF00"), ColorTranslator.FromHtml("#7D7CFC00"), ColorTranslator.FromHtml("#7DFF69B4"), ColorTranslator.FromHtml("#7DFF00FF"), ColorTranslator.FromHtml("#7DADD8E6"), ColorTranslator.FromHtml("#7D00FF00"), ColorTranslator.FromHtml("#7DFFC0CB") };

		/// <summary>
		/// 采样机样罐分配的颜色
		/// </summary>
		Dictionary<string, Color> dicCellColors = new Dictionary<string, Color>();

		/// <summary>
		/// 归批机样桶分配的颜色
		/// </summary>
		Dictionary<string, Color> dicCellColors_batchCoord = new Dictionary<string, Color>();

		RTxtOutputer rTxtOutputer;

		/// <summary>
		/// 最近卸样记录搜索条件
		/// </summary>
		string StrWhere_UnLoad = "";

		/// <summary>
		///归批机样桶搜索条件
		/// </summary>
		string StrWhere_PackBath = "";

		#endregion

		/// <summary>
		/// 窗体初始化
		/// </summary>
		private void FormInit()
		{
			rTxtOutputer = new RTxtOutputer(rTxTMessageInfo);

			// 采样机设备编码，跟卸样程序一一对应
			samplerMachineCodes = commonDAO.GetAppletConfigString("采样机设备编码").Split('|');
			makerMachineCode = commonDAO.GetCommonAppletConfigString(CommonAppConfig.GetInstance().AppIdentifier + "对应制样机");

			CreateSamplerButton();
			CreateEquStatus();

			// 触发选择第一台采样机
			if (flpanSamplerButton.Controls.Count > 0) (flpanSamplerButton.Controls[0] as RadioButton).Checked = true;
			dtStartTime.Value = dtEndTime.Value = DateTime.Now;
		}

		private void FrmUnloadSampler_Load(object sender, EventArgs e)
		{
			FormInit();
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{

		}

		#region 归批机
		/// <summary>
		/// 加载归批机数据
		/// </summary>
		/// <param name="superGridControl"></param>
		private void LoadBatchCoord(SuperGridControl superGridControl)
		{
			superGridControl.PrimaryGrid.Rows.Clear();

			StrWhere_PackBath = string.Format(" where State=1 and MachineCode='{0}'", GlobalVars.MachineCode_PackingBatch_KY);
			if (!string.IsNullOrEmpty(txt_CoordSampleCode.Text))
				StrWhere_PackBath += string.Format(" and SampleCode like '%{0}%'", txt_CoordSampleCode.Text);
			List<InfPackingBatchCoord> list = commonDAO.SelfDber.Entities<InfPackingBatchCoord>(StrWhere_PackBath);
			superGridControl.PrimaryGrid.DataSource = list;

			dicCellColors_batchCoord.Clear();

			foreach (InfPackingBatchCoord batchCoord in list)
			{
				if (string.IsNullOrEmpty(batchCoord.SampleCode)) continue;
				string key = batchCoord.SampleCode;

				if (!dicCellColors_batchCoord.ContainsKey(key) && dicCellColors_batchCoord.Count < CellColors.Length) dicCellColors_batchCoord.Add(key, CellColors[dicCellColors_batchCoord.Count]);
			}
		}

		#endregion

		#region 卸样业务

		/// <summary>
		/// 绑定采样机集样罐信息
		/// </summary>
		/// <param name="superGridControl"></param>
		/// <param name="machineCode">采样机设备编码</param>
		private void LoadSampleBarrel(SuperGridControl superGridControl, string machineCode)
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
		/// 加载采样机最近几天的卸样记录
		/// </summary>
		/// <param name="samplerMachineCode">采样机编码</param>
		private void LoadLatestSampleUnloadCmd(CmcsCMEquipment cmcsCMEquipment)
		{
			if (Dbers.GetInstance().SelfDber.Get<CmcsCMEquipment>(cmcsCMEquipment.Parentid).EquipmentCode == "汽车机械采样机")
				superGridControl3.PrimaryGrid.DataSource = commonDAO.SelfDber.TopEntities<InfQCJXCYUnLoadCMD>(3, " where MachineCode='" + cmcsCMEquipment.EquipmentCode + "' order by createdate desc");
			else
				rTxtOutputer.Output("未找到计划", eOutputType.Error);
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
			this.currentEquInfSampleBarrels.Add(equInfSampleBarrel);

			foreach (GridRow gridRow in superGridControl.PrimaryGrid.Rows)
			{
				InfEquInfSampleBarrel thisEquInfSampleBarrel = gridRow.DataItem as InfEquInfSampleBarrel;
				if (thisEquInfSampleBarrel == null || thisEquInfSampleBarrel.Id == equInfSampleBarrel.Id)
				{
					continue;
				}

				gridRow.Checked = (thisEquInfSampleBarrel != null && !string.IsNullOrWhiteSpace(thisEquInfSampleBarrel.SampleCode)
				   && thisEquInfSampleBarrel.SampleCode == equInfSampleBarrel.SampleCode && thisEquInfSampleBarrel.BarrelType == equInfSampleBarrel.BarrelType);

				if (gridRow.Checked) this.currentEquInfSampleBarrels.Add(thisEquInfSampleBarrel);
			}
		}

		/// <summary>
		/// 取消选中采样码一致的记录
		/// </summary>
		/// <param name="superGridControl"></param>
		/// <param name="equInfSampleBarrel"></param> 
		private void UnCheckedSameBarrelRow(SuperGridControl superGridControl, InfEquInfSampleBarrel equInfSampleBarrel)
		{
			if (equInfSampleBarrel == null) return;
			if (string.IsNullOrWhiteSpace(equInfSampleBarrel.SampleCode)) return;

			this.currentEquInfSampleBarrels.Clear();

			foreach (GridRow gridRow in superGridControl.PrimaryGrid.Rows)
			{
				InfEquInfSampleBarrel thisEquInfSampleBarrel = gridRow.DataItem as InfEquInfSampleBarrel;
				if (thisEquInfSampleBarrel != null && thisEquInfSampleBarrel.SampleCode == equInfSampleBarrel.SampleCode && thisEquInfSampleBarrel.Id != equInfSampleBarrel.Id)
				{
					gridRow.Checked = false;
				}
			}
		}

		/// <summary>
		/// 发送制样计划
		/// </summary>
		/// <param name="rCSamplingId">采样单Id</param>
		/// <param name="infactoryBatchId">批次Id</param>
		/// <returns></returns>
		private bool SendMakePlan(string rCSamplingId, string infactoryBatchId)
		{
			CmcsRCMake rcMake = AutoMakerDAO.GetInstance().GetRCMakeBySampleId(rCSamplingId);
			if (rcMake != null)
			{
				string fuelKindName = string.Empty;

				CmcsInFactoryBatch inFactoryBatch = commonDAO.GetBatchByRCSamplingId(rCSamplingId);
				if (inFactoryBatch != null)
				{
					CmcsFuelKind fuelKind = commonDAO.SelfDber.Get<CmcsFuelKind>(inFactoryBatch.FuelKindId);
					if (fuelKind != null) fuelKindName = fuelKind.FuelName;
				}

				// 需调整：发送的制样计划中煤种、颗粒度、水分等相关信息视接口而定
				InfMakerPlan makerPlan = new InfMakerPlan()
				{
					InterfaceType = commonDAO.GetMachineInterfaceTypeByCode(this.makerMachineCode),
					MachineCode = this.makerMachineCode,
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

			// 制样机
			CmcsCMEquipment cMEquipmentMaker = commonDAO.GetCMEquipmentByMachineCode(makerMachineCode);
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

			// 归批机
			CmcsCMEquipment cMEquipmentPacker = commonDAO.GetCMEquipmentByMachineCode(packerMachineCode);
			if (cMEquipmentPacker != null)
			{
				UCtrlSignalLight uCtrlSignalLightMaker = new UCtrlSignalLight()
				{
					Anchor = AnchorStyles.Left,
					Tag = cMEquipmentPacker,
					Size = new System.Drawing.Size(20, 20),
					Padding = new System.Windows.Forms.Padding(10, 0, 0, 0)
				};
				SetSystemStatusToolTip(uCtrlSignalLightMaker);

				flpanEquState.Controls.Add(uCtrlSignalLightMaker);

				LabelX lblMachineNameMaker = new LabelX()
				{
					Text = cMEquipmentPacker.EquipmentName,
					Tag = cMEquipmentPacker,
					AutoSize = true,
					Anchor = AnchorStyles.Left,
					Font = new Font("Segoe UI", 12f, FontStyle.Regular)
				};

				flpanEquState.Controls.Add(lblMachineNameMaker);
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

				if (systemStatus == eEquInfSystemStatus.就绪待机.ToString())
					uCtrlSignalLight.LightColor = EquipmentStatusColors.BeReady;
				else if (systemStatus == eEquInfSystemStatus.正在运行.ToString())
					uCtrlSignalLight.LightColor = EquipmentStatusColors.Working;
				else if (systemStatus == eEquInfSystemStatus.发生故障.ToString())
					uCtrlSignalLight.LightColor = EquipmentStatusColors.Breakdown;
				else if (systemStatus == eEquInfSystemStatus.系统停止.ToString())
					uCtrlSignalLight.LightColor = EquipmentStatusColors.Forbidden;
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
			LoadBatchCoord(superGridControl2);
			LoadSampleBarrel(superGridControl1, this.CurrentSampler.EquipmentCode);
		}

		#endregion

		#region 操作

		/// <summary>
		/// 选择不同的采样机
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void rbtnSampler_CheckedChanged(object sender, EventArgs e)
		{
			RadioButton rbtnSampler = sender as RadioButton;
			this.CurrentSampler = rbtnSampler.Tag as CmcsCMEquipment;
		}

		/// <summary>
		/// 选择不同的卸料位置  1 制样机 0 归批机
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void rbtnUnLoad_CheckedChanged(object sender, EventArgs e)
		{
			// 取消其他行的选中状态
			//foreach (GridRow gridRowItem in superGridControl1.PrimaryGrid.Rows)
			//{
			//    gridRowItem.Checked = false;
			//}
			if (this.currentEquInfSampleBarrels != null && this.currentEquInfSampleBarrels.Count > 0)
			{
				GridBeforeCheckEventArgs before_e = null;
				foreach (GridRow gridRow in superGridControl1.PrimaryGrid.Rows)
				{
					InfEquInfSampleBarrel thisEquInfSampleBarrel = gridRow.DataItem as InfEquInfSampleBarrel;
					if (this.currentEquInfSampleBarrels[0].Id == thisEquInfSampleBarrel.Id)
					{
						before_e = new GridBeforeCheckEventArgs(gridRow.GridPanel, gridRow);
						before_e.Cancel = true;
						break;
					}
				}
				superGridControl1_BeforeCheck(superGridControl1, before_e);
				if (rbtnToMaker.Checked)
				{

				}
				else if (rbtnToSubway.Checked)
				{

				}
			}
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
				LoadSampleBarrel(superGridControl1, this.currentSampler.EquipmentCode);
				return;
			}
			if (currentRCSampling == null)
			{
				MessageBoxEx.Show("请勾选绑定的采样单后再发送", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			// 检测采样机系统的状态
			string samplerSystemStatue = commonDAO.GetSignalDataValue(this.currentSampler.EquipmentCode, eSignalDataName.设备状态.ToString());
			if (samplerSystemStatue != eEquInfSamplerSystemStatus.就绪待机.ToString() && samplerSystemStatue != eEquInfSamplerSystemStatus.采样完成.ToString())
			{
				MessageBoxEx.Show("采样机系统未就绪，禁止卸样", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			// 检测卸料机系统的状态
			//string UnLoadSystemStatue = commonDAO.GetSignalDataValue(this.currentSampler.EquipmentCode + GlobalVars.XLJ_Machine, eSignalDataName.设备状态.ToString());
			//if (UnLoadSystemStatue != eUnLoadState.就绪待机.ToString())
			//{
			//	MessageBoxEx.Show("卸样机系统未就绪，禁止卸样", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			//	return;
			//}

			// 检测制样机系统状态
			string makerSystemStatus = commonDAO.GetSignalDataValue(this.makerMachineCode, eSignalDataName.设备状态.ToString());
			if (rbtnToMaker.Checked && makerSystemStatus != eEquInfSamplerSystemStatus.就绪待机.ToString())
			{
				MessageBoxEx.Show("制样机系统未就绪，禁止卸样", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			string message = string.Empty;
			if (Dbers.GetInstance().SelfDber.Get<CmcsCMEquipment>(this.currentSampler.Parentid).EquipmentCode == "汽车机械采样机")
			{
				if (!qcjxcySamplerDAO.CanSendSampleUnloadCmd(this.currentSampler.EquipmentCode, out message))
				{
					MessageBoxEx.Show(message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}
				SendJxSamplerUnloadCmd();
			}
			else
			{
				MessageBoxEx.Show("无此编码类型，请查证\"皮带采样机\"和\"汽车机械采样机\"类型！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
		}

		/// <summary>
		/// 监听机械采样机返回结果
		/// </summary> 
		/// <returns></returns>
		private void SendJxSamplerUnloadCmd()
		{
			taskSimpleScheduler = new TaskSimpleScheduler();

			autoResetEvent.Reset();

			taskSimpleScheduler.StartNewTask("卸样业务逻辑", () =>
			{
				this.IsWorking = true;

				// 发送卸样命令
				if (qcjxcySamplerDAO.SendSampleUnloadCmd(this.currentSampler.EquipmentCode, this.CurrentRCSampling.Id, this.currentSampleCode, (eEquInfSamplerUnloadType)Convert.ToInt16(flpanUnloadType.Controls.OfType<RadioButton>().First(a => a.Checked).Tag), this.currentEquInfSampleBarrels[0].BarrelNumber, out this.currentUnloadCmdId))
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
						equInfCmdResultCode = UnloadSamplerDAO.GetInstance().GetQCJXCYUnloadSamplerState(this.currentUnloadCmdId);
					}
					while (equInfCmdResultCode == eEquInfCmdResultCode.默认);

					if (equInfCmdResultCode == eEquInfCmdResultCode.成功)
					{
						string barrelNumbers = string.Empty;
						foreach (string item in this.currentEquInfSampleBarrels.Select(a => a.BarrelNumber).ToList())
						{
							barrelNumbers += item + " ";
						}
						rTxtOutputer.Output("采样机返回：卸样成功，样桶编号：" + barrelNumbers, eOutputType.Normal);

						// 检测制样机系统状态
						if (rbtnToMaker.Checked)
						{
							string makerSystemStatus = commonDAO.GetSignalDataValue(this.makerMachineCode, eSignalDataName.设备状态.ToString());
							if (makerSystemStatus == eEquInfSamplerSystemStatus.就绪待机.ToString())
							{
								//if (MessageBoxEx.Show("卸样成功，检测到制样机已就绪，立刻开始制样？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
								//{
								//    if (SendMakePlan(this.CurrentRCSampling.Id, this.CurrentRCSampling.InFactoryBatchId))
								//        rTxtOutputer.Output("制样命令发送成功", eOutputType.Normal);
								//    else
								//        rTxtOutputer.Output("制样命令发送失败", eOutputType.Error);
								//}
								MessageBoxEx.Show("卸样成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
							}
						}
					}
					else if (equInfCmdResultCode == eEquInfCmdResultCode.失败)
					{
						rTxtOutputer.Output("采样机返回：卸样失败", eOutputType.Error);
					}
				}
				else
				{
					rTxtOutputer.Output("卸样命令发送失败", eOutputType.Error);
				}

				this.IsWorking = false;

				LoadSampleBarrel(superGridControl1, this.CurrentSampler.EquipmentCode);
				LoadLatestSampleUnloadCmd(this.CurrentSampler);

				autoResetEvent.Set();
			});
		}

		/// <summary>
		/// 监听归批机返回结果
		/// </summary>
		private void SendJxPackingBatchCmd(CmcsRCSampleBarrel rCSampleBarrel)
		{
			taskSimpleScheduler = new TaskSimpleScheduler();

			autoResetEvent.Reset();

			taskSimpleScheduler.StartNewTask("倒料业务逻辑", () =>
			{
				this.IsWorking = true;

				// 发送倒样命令
				if (PackingBatchDAO.GetInstance().SendPackingBatch(this.currentSampler.EquipmentCode, this.currentSampleCode, out currentMessage))
				{
					rTxtOutputer.Output("倒样命令发送成功，等待归批机执行", eOutputType.Normal);

					int waitCount = 0;
					eEquInfCmdResultCode equInfCmdResultCode;
					do
					{
						Thread.Sleep(10000);
						if (waitCount % 5 == 0) rTxtOutputer.Output("正在等待归批机返回结果", eOutputType.Normal);

						waitCount++;

						// 获取卸样命令的执行结果
						equInfCmdResultCode = PackingBatchDAO.GetInstance().GetQCJXPackingBatchState(this.currentSampleCode);
					}
					while (equInfCmdResultCode == eEquInfCmdResultCode.默认);

					if (equInfCmdResultCode == eEquInfCmdResultCode.成功)
					{
						rTxtOutputer.Output("归批机返回：倒料成功", eOutputType.Normal);

						// 检测制样机系统状态
						if (rbtnToMaker.Checked)
						{
							string makerSystemStatus = commonDAO.GetSignalDataValue(this.makerMachineCode, eSignalDataName.设备状态.ToString());
							if (makerSystemStatus == eEquInfSamplerSystemStatus.就绪待机.ToString())
							{
								//if (MessageBoxEx.Show("倒料成功，检测到制样机已就绪，立刻开始制样？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
								//{
								//    if (SendMakePlan(rCSampleBarrel.SamplingId, rCSampleBarrel.InFactoryBatchId))
								//        //if (SendMakePlan(this.CurrentRCSampling.Id, this.CurrentRCSampling.InFactoryBatchId))
								//        rTxtOutputer.Output("制样命令发送成功", eOutputType.Normal);
								//    else
								//        rTxtOutputer.Output("制样命令发送失败", eOutputType.Error);
								//}
								MessageBoxEx.Show("倒料成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
							}
						}
					}
					else if (equInfCmdResultCode == eEquInfCmdResultCode.失败)
					{
						rTxtOutputer.Output("采样机返回：卸样失败", eOutputType.Error);
					}
				}
				else
				{
					rTxtOutputer.Output("卸样命令发送失败", eOutputType.Error);
				}

				this.IsWorking = false;

				LoadSampleBarrel(superGridControl1, this.CurrentSampler.EquipmentCode);
				LoadLatestSampleUnloadCmd(this.CurrentSampler);

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
				btnFallCode.Enabled = enabled;

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
		/// 发送制样命令
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSendMakeCmd_Click(object sender, EventArgs e)
		{
			// 检测制样机系统状态
			string makerSystemStatus = commonDAO.GetSignalDataValue(this.makerMachineCode, eSignalDataName.设备状态.ToString());
			if (rbtnToMaker.Checked && makerSystemStatus != eEquInfSystemStatus.就绪待机.ToString())
			{
				MessageBoxEx.Show("制样机系统未就绪，禁止制样", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			String tempSampleCode = String.Empty;
			if (Dbers.GetInstance().SelfDber.Get<CmcsCMEquipment>(this.currentSampler.Parentid).EquipmentCode == "汽车机械采样机")
			{
				InfQCJXCYUnLoadCMD qcjxcyUnLoadCMD = null;
				foreach (GridRow gridRow in superGridControl3.PrimaryGrid.Rows)
				{
					if (gridRow.Checked)
						qcjxcyUnLoadCMD = gridRow.DataItem as InfQCJXCYUnLoadCMD;
				}
				if (qcjxcyUnLoadCMD == null)
				{
					MessageBoxEx.Show("请选择卸样记录", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}
				tempSampleCode = qcjxcyUnLoadCMD.SampleCode.Trim();
			}

			CmcsRCSampleBarrel rCSampleBarrel = commonDAO.SelfDber.Entity<CmcsRCSampleBarrel>(" where BarrelCode='" + tempSampleCode + "'");
			if (rCSampleBarrel == null)
			{
				MessageBoxEx.Show("未找到采样单明细记录", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			if (MessageBoxEx.Show("确定要发送制样命令？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
				SendMakePlan(rCSampleBarrel.SamplingId, rCSampleBarrel.InFactoryBatchId);
		}

		/// <summary>
		/// 发送倒料命令
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnFallCode_Click(object sender, EventArgs e)
		{

			// 检测归批机系统状态
			string makerSystemStatus = commonDAO.GetSignalDataValue(GlobalVars.MachineCode_PackingBatch_KY, eSignalDataName.设备状态.ToString());
			if (makerSystemStatus != eEquInfSamplerSystemStatus.就绪待机.ToString())
			{
				MessageBoxEx.Show("归批机系统未就绪，禁止倒料", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			// 检测制样机系统状态
			makerSystemStatus = commonDAO.GetSignalDataValue(this.makerMachineCode, eSignalDataName.设备状态.ToString());
			if (makerSystemStatus != eEquInfSamplerSystemStatus.就绪待机.ToString())
			{
				MessageBoxEx.Show("制样机系统未就绪，禁止倒料", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			string tempSampleCode = string.Empty, tempMakeCode = string.Empty;
			CmcsRCSampleBarrel rCSampleBarrel = null;
			if (Dbers.GetInstance().SelfDber.Get<CmcsCMEquipment>(this.currentSampler.Parentid).EquipmentCode == "汽车机械采样机")
			{
				InfPackingBatchCoord qcjxcyUnLoadCMD = null;
				foreach (GridRow gridRow in superGridControl2.PrimaryGrid.Rows)
				{
					if (gridRow.Checked)
					{
						qcjxcyUnLoadCMD = gridRow.DataItem as InfPackingBatchCoord;
					}
				}
				if (qcjxcyUnLoadCMD == null)
				{
					MessageBoxEx.Show("请选择归批机样桶", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}
				tempSampleCode = qcjxcyUnLoadCMD.SampleCode;
				this.currentSampleCode = tempSampleCode;
				rCSampleBarrel = commonDAO.SelfDber.Entity<CmcsRCSampleBarrel>(" where BarrelCode='" + tempSampleCode + "'");
				if (rCSampleBarrel == null)
				{
					MessageBoxEx.Show("未找到采样单明细记录", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}
			}
			if (MessageBoxEx.Show("确定要发送倒料命令？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
				SendJxPackingBatchCmd(rCSampleBarrel);
		}

		#endregion

		#region SuperGridControl

		#region superGridControl1 采样机集样罐

		/// <summary>
		/// 选中之前
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void superGridControl1_BeforeCheck(object sender, GridBeforeCheckEventArgs e)
		{
			GridRow gridRow = (e.Item as GridRow);
			if (gridRow == null) { return; }
			InfEquInfSampleBarrel equInfSampleBarrel = gridRow.DataItem as InfEquInfSampleBarrel;
			if (gridRow.Checked && !e.Cancel)//取消选中
			{
				this.currentSampleCode = "";
				this.CurrentRCSampling = null;
				this.currentEquInfSampleBarrels.Clear();
				if (rbtnToMaker.Checked)//制样机
				{
					UnCheckedSameBarrelRow(sender as SuperGridControl, equInfSampleBarrel);
				}
				else if (rbtnToSubway.Checked)
				{
					this.currentEquInfSampleBarrels.Clear();
				}
			}
			else//选中
			{
				this.currentSampleCode = equInfSampleBarrel.SampleCode;
				this.CurrentRCSampling = commonDAO.GetSamplingBySamplingCode(equInfSampleBarrel.SampleCode);
				if (rbtnToMaker.Checked)//制样机
				{
					// 选中其他相同采样码的样罐记录
					CheckedSameBarrelRow(sender as SuperGridControl, equInfSampleBarrel);
				}
				else if (rbtnToSubway.Checked)
				{
					this.currentEquInfSampleBarrels.Clear();
					this.currentEquInfSampleBarrels.Add(equInfSampleBarrel);
					// 取消其他行的选中状态
					foreach (GridRow gridRowItem in superGridControl1.PrimaryGrid.Rows)
					{
						if (e.Cancel && gridRowItem == gridRow)
							continue;
						gridRowItem.Checked = false;
					}
				}
			}

			//if (rbtnToSubway.Checked)
			//{
			//    // 取消其他行的选中状态
			//    foreach (GridRow gridRowItem in superGridControl1.PrimaryGrid.Rows)
			//    {
			//        //InfEquInfSampleBarrel rCSampling = gridRowItem.DataItem as InfEquInfSampleBarrel;
			//        //if (this.currentEquInfSampleBarrels != null && this.currentEquInfSampleBarrels.Count > 0 && rCSampling.Id == this.currentEquInfSampleBarrels[0].Id) continue;

			//        gridRowItem.Checked = false;
			//    }
			//}
			//this.currentEquInfSampleBarrels.Clear();
			//this.currentEquInfSampleBarrels.Add(equInfSampleBarrel);
			//if (!string.IsNullOrEmpty(equInfSampleBarrel.SampleCode))
			//{
			//    if (rbtnToSubway.Checked)
			//    {
			//        this.currentSampleCode = equInfSampleBarrel.SampleCode;
			//    }
			//    else
			//    {
			//        // 选中其他相同采样码的样罐记录
			//        CheckedSameBarrelRow(sender as SuperGridControl, equInfSampleBarrel);
			//    }
			//    this.CurrentRCSampling = commonDAO.GetSamplingBySamplingCode(equInfSampleBarrel.SampleCode);
			//    // 加载采样单
			//    //LoadRCSamplingList(superGridControl2, equInfSampleBarrel.InFactoryBatchId);
			//}
			//else e.Cancel = true;
		}

		private void superGridControl1_AfterCheck(object sender, GridAfterCheckEventArgs e)
		{

		}

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
		private void superGridControl_GetRowHeaderText(object sender, GridGetRowHeaderTextEventArgs e)
		{
			e.Text = (e.GridRow.RowIndex + 1).ToString();
		}

		private void superGridControl1_GetCellStyle(object sender, GridGetCellStyleEventArgs e)
		{
			try
			{
				if (e.GridCell.GridColumn.DataPropertyName == "SampleCode")
				{
					InfEquInfSampleBarrel equInfSampleBarrel = e.GridCell.GridRow.DataItem as InfEquInfSampleBarrel;
					if (equInfSampleBarrel != null && !string.IsNullOrEmpty(equInfSampleBarrel.SampleCode) && this.dicCellColors.Count > 0) e.Style.Background.Color1 = this.dicCellColors[equInfSampleBarrel.SampleCode];
				}
			}
			catch { }
		}

		/// <summary>
		/// 加载完成
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void superGridControl1_DataBindingComplete(object sender, GridDataBindingCompleteEventArgs e)
		{
			foreach (GridRow grid in e.GridPanel.Rows)
			{
				InfEquInfSampleBarrel entity = grid.DataItem as InfEquInfSampleBarrel;
				if (entity == null || string.IsNullOrEmpty(entity.SampleCode)) continue;
				if (this.currentEquInfSampleBarrels != null && this.currentEquInfSampleBarrels.Count > 0 && this.currentEquInfSampleBarrels.Select(a => a.Id).Contains(entity.Id))
				{
					grid.Checked = true;
				}
				else
					grid.Checked = false;
			}
		}

		#endregion

		#region superGridControl2 归批机集样罐
		/// <summary>
		/// 选择样桶
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void superGridControl2_BeforeCheck(object sender, GridBeforeCheckEventArgs e)
		{
			GridRow gridRow = (e.Item as GridRow);
			if (gridRow == null) { e.Cancel = true; return; }
			if (gridRow.Checked) gridRow.Checked = false;
			this.CurrentCoord = gridRow.DataItem as InfPackingBatchCoord;
			e.Cancel = string.IsNullOrEmpty(this.CurrentCoord.SampleCode);

			// 取消其他行的选中状态
			foreach (GridRow gridRowItem in superGridControl2.PrimaryGrid.Rows)
			{
				InfPackingBatchCoord rCSampling = gridRowItem.DataItem as InfPackingBatchCoord;
				if (rCSampling.Id == this.CurrentCoord.Id) continue;

				gridRowItem.Checked = false;
			}
		}

		private void superGridControl2_BeginEdit(object sender, GridEditEventArgs e)
		{
			// 取消编辑
			e.Cancel = true;
		}

		private void superGridControl2_GetCellStyle(object sender, GridGetCellStyleEventArgs e)
		{
			try
			{
				if (e.GridCell.GridColumn.DataPropertyName == "SampleCode")
				{
					InfPackingBatchCoord batchCoord = e.GridCell.GridRow.DataItem as InfPackingBatchCoord;
					if (batchCoord != null && !string.IsNullOrEmpty(batchCoord.SampleCode) && this.dicCellColors_batchCoord.Count > 0) e.Style.Background.Color1 = this.dicCellColors_batchCoord[batchCoord.SampleCode];
				}
			}
			catch { }
		}

		private void superGridControl2_DataBindingComplete(object sender, GridDataBindingCompleteEventArgs e)
		{
			foreach (GridRow gridRow in e.GridPanel.Rows)
			{
				InfPackingBatchCoord batchCoord = gridRow.DataItem as InfPackingBatchCoord;
				if (batchCoord != null && this.CurrentCoord != null && batchCoord.Id == this.CurrentCoord.Id)
					gridRow.Checked = true;
			}
		}
		#endregion

		#region superGridControl3 最近卸样记录

		private void superGridControl3_CellClick(object sender, GridCellClickEventArgs e)
		{
			if (Dbers.GetInstance().SelfDber.Get<CmcsCMEquipment>(this.currentSampler.Parentid).EquipmentCode == "皮带采样机")
			{
				InfBeltSampleUnloadCmd sampleUnloadCmd = e.GridCell.GridRow.DataItem as InfBeltSampleUnloadCmd;

				foreach (GridRow gridRow in superGridControl3.PrimaryGrid.Rows)
				{
					InfBeltSampleUnloadCmd beltSampleUnloadCmd = gridRow.DataItem as InfBeltSampleUnloadCmd;
					gridRow.Checked = (beltSampleUnloadCmd != null && sampleUnloadCmd.Id == beltSampleUnloadCmd.Id);
				}
			}
			else if (Dbers.GetInstance().SelfDber.Get<CmcsCMEquipment>(this.currentSampler.Parentid).EquipmentCode == "汽车机械采样机")
			{
				InfQCJXCYUnLoadCMD sampleUnloadCmd = e.GridCell.GridRow.DataItem as InfQCJXCYUnLoadCMD;

				foreach (GridRow gridRow in superGridControl3.PrimaryGrid.Rows)
				{
					InfQCJXCYUnLoadCMD qcjxcySampleUnloadCmd = gridRow.DataItem as InfQCJXCYUnLoadCMD;
					gridRow.Checked = (qcjxcySampleUnloadCmd != null && sampleUnloadCmd.Id == qcjxcySampleUnloadCmd.Id);
				}
			}
		}

		private void superGridControl3_BeginEdit(object sender, GridEditEventArgs e)
		{
			// 取消编辑
			e.Cancel = true;
		}

		#endregion

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

		#region 最近卸样记录搜索
		private void BindData()
		{
			if (Dbers.GetInstance().SelfDber.Get<CmcsCMEquipment>(this.CurrentSampler.Parentid).EquipmentCode == "皮带采样机")
				superGridControl3.PrimaryGrid.DataSource = commonDAO.SelfDber.Entities<InfBeltSampleUnloadCmd>(" where MachineCode=:MachineCode and CreateDate>=:StartTime and CreateDate<:EndTime order by createdate desc", new { MachineCode = this.CurrentSampler.EquipmentCode, StartTime = dtStartTime.Value.Date, EndTime = dtEndTime.Value.AddDays(1).Date });
			else if (Dbers.GetInstance().SelfDber.Get<CmcsCMEquipment>(this.CurrentSampler.Parentid).EquipmentCode == "汽车机械采样机")
				superGridControl3.PrimaryGrid.DataSource = commonDAO.SelfDber.Entities<InfQCJXCYUnLoadCMD>(" where MachineCode=:MachineCode and CreateDate>=:StartTime and CreateDate<:EndTime order by createdate desc", new { MachineCode = this.CurrentSampler.EquipmentCode, StartTime = dtStartTime.Value.Date, EndTime = dtEndTime.Value.AddDays(1).Date });
		}

		private void btnSearch_Click(object sender, EventArgs e)
		{
			if (dtStartTime.Value.Year < 2000)
			{
				MessageBoxEx.Show("请选择开始时间", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			if (dtEndTime.Value.Year < 2000)
			{
				MessageBoxEx.Show("请选择结束时间", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			BindData();
		}

		private void btnPreviousDay_Click(object sender, EventArgs e)
		{
			dtStartTime.Value = dtStartTime.Value.AddDays(-1);
			dtEndTime.Value = dtEndTime.Value.AddDays(-1);
			BindData();
		}

		private void btnNextDay_Click(object sender, EventArgs e)
		{
			dtStartTime.Value = dtStartTime.Value.AddDays(1);
			dtEndTime.Value = dtEndTime.Value.AddDays(1);
			BindData();
		}
		#endregion

		#region 归批机样桶搜索

		private void btnSearch_BatchCoord_Click(object sender, EventArgs e)
		{
			LoadBatchCoord(superGridControl2);
		}
		#endregion


	}
}
