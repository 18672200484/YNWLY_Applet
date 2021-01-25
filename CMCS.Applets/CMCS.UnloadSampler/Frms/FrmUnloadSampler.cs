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
		/// ����Ψһ��ʶ��
		/// </summary>
		public static string UniqueKey = "FrmUnloadSampler";

		#region ҵ������
		CommonDAO commonDAO = CommonDAO.GetInstance();
		BeltSamplerDAO beltSamplerDAO = BeltSamplerDAO.GetInstance();
		QCJXCYSamplerDAO qcjxcySamplerDAO = QCJXCYSamplerDAO.GetInstance();

		#endregion

		#region ����Vars

		bool isWorking = false;
		/// <summary>
		/// ���ڹ���
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
		/// ��ǰѡ�еĲ�����
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
		/// ��ǰѡ�еĲ�����(�����ޱ���һ�µĲ�����)
		/// </summary>
		string currentSampleCode;

		CmcsRCSampling currentRCSampling;
		/// <summary>
		/// ��ǰѡ�еĲ�����
		/// </summary>
		public CmcsRCSampling CurrentRCSampling
		{
			get { return currentRCSampling; }
			set { currentRCSampling = value; }
		}

		InfPackingBatchCoord currentCoord;
		/// <summary>
		/// ��ǰѡ�еĹ�������Ͱ
		/// </summary>
		public InfPackingBatchCoord CurrentCoord
		{
			get { return currentCoord; }
			set { currentCoord = value; }
		}

		/// <summary>
		/// ��ǰж������id
		/// </summary>
		string currentUnloadCmdId;
		/// <summary>
		/// ������Ϣ
		/// </summary>
		string currentMessage;
		/// <summary>
		/// ��ǰѡ��Ĳ�����������Ϣ
		/// </summary>
		public List<InfEquInfSampleBarrel> currentEquInfSampleBarrels = new List<InfEquInfSampleBarrel>();

		/// <summary>
		/// ���������� Ĭ��#1������
		/// </summary>
		string[] samplerMachineCodes = new string[] { GlobalVars.MachineCode_QC_JxSampler_1 };

		/// <summary>
		/// ���������� Ĭ��#1ȫ�Զ�������
		/// </summary>
		string makerMachineCode = GlobalVars.MachineCode_QZDZYJ_1;

		/// <summary>
		/// ����������
		/// </summary>
		string packerMachineCode = GlobalVars.MachineCode_PackingBatch_KY;

		Color[] CellColors = new Color[] { ColorTranslator.FromHtml("#7D00FFFF"), ColorTranslator.FromHtml("#7DFFFF00"), ColorTranslator.FromHtml("#7D7CFC00"), ColorTranslator.FromHtml("#7DFF69B4"), ColorTranslator.FromHtml("#7DFF00FF"), ColorTranslator.FromHtml("#7DADD8E6"), ColorTranslator.FromHtml("#7D00FF00"), ColorTranslator.FromHtml("#7DFFC0CB") };

		/// <summary>
		/// ���������޷������ɫ
		/// </summary>
		Dictionary<string, Color> dicCellColors = new Dictionary<string, Color>();

		/// <summary>
		/// ��������Ͱ�������ɫ
		/// </summary>
		Dictionary<string, Color> dicCellColors_batchCoord = new Dictionary<string, Color>();

		RTxtOutputer rTxtOutputer;

		/// <summary>
		/// ���ж����¼��������
		/// </summary>
		string StrWhere_UnLoad = "";

		/// <summary>
		///��������Ͱ��������
		/// </summary>
		string StrWhere_PackBath = "";

		#endregion

		/// <summary>
		/// �����ʼ��
		/// </summary>
		private void FormInit()
		{
			rTxtOutputer = new RTxtOutputer(rTxTMessageInfo);

			// �������豸���룬��ж������һһ��Ӧ
			samplerMachineCodes = commonDAO.GetAppletConfigString("�������豸����").Split('|');
			makerMachineCode = commonDAO.GetCommonAppletConfigString(CommonAppConfig.GetInstance().AppIdentifier + "��Ӧ������");

			CreateSamplerButton();
			CreateEquStatus();

			// ����ѡ���һ̨������
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

		#region ������
		/// <summary>
		/// ���ع���������
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

		#region ж��ҵ��

		/// <summary>
		/// �󶨲�������������Ϣ
		/// </summary>
		/// <param name="superGridControl"></param>
		/// <param name="machineCode">�������豸����</param>
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
		/// ��鼯������Ϣ�Ƿ��Ѹ���
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
		/// ���ز�������������ж����¼
		/// </summary>
		/// <param name="samplerMachineCode">����������</param>
		private void LoadLatestSampleUnloadCmd(CmcsCMEquipment cmcsCMEquipment)
		{
			if (Dbers.GetInstance().SelfDber.Get<CmcsCMEquipment>(cmcsCMEquipment.Parentid).EquipmentCode == "������е������")
				superGridControl3.PrimaryGrid.DataSource = commonDAO.SelfDber.TopEntities<InfQCJXCYUnLoadCMD>(3, " where MachineCode='" + cmcsCMEquipment.EquipmentCode + "' order by createdate desc");
			else
				rTxtOutputer.Output("δ�ҵ��ƻ�", eOutputType.Error);
		}

		/// <summary>
		/// ѡ�в�����һ�µļ�¼
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
		/// ȡ��ѡ�в�����һ�µļ�¼
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
		/// ���������ƻ�
		/// </summary>
		/// <param name="rCSamplingId">������Id</param>
		/// <param name="infactoryBatchId">����Id</param>
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

				// ����������͵������ƻ���ú�֡������ȡ�ˮ�ֵ������Ϣ�ӽӿڶ���
				InfMakerPlan makerPlan = new InfMakerPlan()
				{
					InterfaceType = commonDAO.GetMachineInterfaceTypeByCode(this.makerMachineCode),
					MachineCode = this.makerMachineCode,
					InFactoryBatchId = infactoryBatchId,
					MakeCode = rcMake.MakeCode,
					FuelKindName = fuelKindName,
					//Mt = "ʪú",
					MakeType = "��������",
					//CoalSize = "С����",
					SyncFlag = 0
				};
				AutoMakerDAO.GetInstance().SaveMakerPlanAndStartCmd(makerPlan, out currentMessage);

				rTxtOutputer.Output(currentMessage, eOutputType.Normal);

				return true;
			}
			else
				rTxtOutputer.Output("�����ƻ�����ʧ�ܣ�δ�ҵ���������¼��Ϣ", eOutputType.Error);

			return false;
		}

		#endregion

		#region �ź�ҵ��

		/// <summary>
		/// ���ɲ�����ѡ��
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
		/// ����Ƥ����������ȫ�Զ�������״̬
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

			// ������
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

			// ������
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

			if (this.flpanEquState.Controls.Count == 0) MessageBoxEx.Show("Ƥ��������������������δ���ã�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
		}

		/// <summary>
		/// �����豸״̬
		/// </summary>
		private void RefreshEquStatus()
		{
			foreach (UCtrlSignalLight uCtrlSignalLight in flpanEquState.Controls.OfType<UCtrlSignalLight>())
			{
				if (uCtrlSignalLight.Tag == null) continue;

				string machineCode = (uCtrlSignalLight.Tag as CmcsCMEquipment).EquipmentCode;
				if (string.IsNullOrEmpty(machineCode)) continue;

				string systemStatus = commonDAO.GetSignalDataValue(machineCode, eSignalDataName.�豸״̬.ToString());

				if (systemStatus == eEquInfSystemStatus.��������.ToString())
					uCtrlSignalLight.LightColor = EquipmentStatusColors.BeReady;
				else if (systemStatus == eEquInfSystemStatus.��������.ToString())
					uCtrlSignalLight.LightColor = EquipmentStatusColors.Working;
				else if (systemStatus == eEquInfSystemStatus.��������.ToString())
					uCtrlSignalLight.LightColor = EquipmentStatusColors.Breakdown;
				else if (systemStatus == eEquInfSystemStatus.ϵͳֹͣ.ToString())
					uCtrlSignalLight.LightColor = EquipmentStatusColors.Forbidden;
			}
		}

		/// <summary>
		/// ����ToolTip��ʾ
		/// </summary>
		private void SetSystemStatusToolTip(Control control)
		{
			this.toolTip1.SetToolTip(control, "<��ɫ> ��������\r\n<��ɫ> ��������\r\n<��ɫ> ��������");
		}

		private void timer1_Tick(object sender, EventArgs e)
		{
			// �����豸״̬
			RefreshEquStatus();
			LoadBatchCoord(superGridControl2);
			LoadSampleBarrel(superGridControl1, this.CurrentSampler.EquipmentCode);
		}

		#endregion

		#region ����

		/// <summary>
		/// ѡ��ͬ�Ĳ�����
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void rbtnSampler_CheckedChanged(object sender, EventArgs e)
		{
			RadioButton rbtnSampler = sender as RadioButton;
			this.CurrentSampler = rbtnSampler.Tag as CmcsCMEquipment;
		}

		/// <summary>
		/// ѡ��ͬ��ж��λ��  1 ������ 0 ������
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void rbtnUnLoad_CheckedChanged(object sender, EventArgs e)
		{
			// ȡ�������е�ѡ��״̬
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
		/// ����ж������
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSendLoadCmd_Click(object sender, EventArgs e)
		{
			if (this.currentEquInfSampleBarrels.Count == 0 || string.IsNullOrEmpty(this.currentSampleCode))
			{
				MessageBoxEx.Show("��ѡ�������ٷ���", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			if (!CheckBeltSampleBarrelUpdated())
			{
				MessageBoxEx.Show("�������Ѹ��£���ˢ��������Ϣ", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				LoadSampleBarrel(superGridControl1, this.currentSampler.EquipmentCode);
				return;
			}
			if (currentRCSampling == null)
			{
				MessageBoxEx.Show("�빴ѡ�󶨵Ĳ��������ٷ���", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			// ��������ϵͳ��״̬
			string samplerSystemStatue = commonDAO.GetSignalDataValue(this.currentSampler.EquipmentCode, eSignalDataName.�豸״̬.ToString());
			if (samplerSystemStatue != eEquInfSamplerSystemStatus.��������.ToString() && samplerSystemStatue != eEquInfSamplerSystemStatus.�������.ToString())
			{
				MessageBoxEx.Show("������ϵͳδ��������ֹж��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			// ���ж�ϻ�ϵͳ��״̬
			//string UnLoadSystemStatue = commonDAO.GetSignalDataValue(this.currentSampler.EquipmentCode + GlobalVars.XLJ_Machine, eSignalDataName.�豸״̬.ToString());
			//if (UnLoadSystemStatue != eUnLoadState.��������.ToString())
			//{
			//	MessageBoxEx.Show("ж����ϵͳδ��������ֹж��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			//	return;
			//}

			// ���������ϵͳ״̬
			string makerSystemStatus = commonDAO.GetSignalDataValue(this.makerMachineCode, eSignalDataName.�豸״̬.ToString());
			if (rbtnToMaker.Checked && makerSystemStatus != eEquInfSamplerSystemStatus.��������.ToString())
			{
				MessageBoxEx.Show("������ϵͳδ��������ֹж��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			string message = string.Empty;
			if (Dbers.GetInstance().SelfDber.Get<CmcsCMEquipment>(this.currentSampler.Parentid).EquipmentCode == "������е������")
			{
				if (!qcjxcySamplerDAO.CanSendSampleUnloadCmd(this.currentSampler.EquipmentCode, out message))
				{
					MessageBoxEx.Show(message, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}
				SendJxSamplerUnloadCmd();
			}
			else
			{
				MessageBoxEx.Show("�޴˱������ͣ����֤\"Ƥ��������\"��\"������е������\"���ͣ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
		}

		/// <summary>
		/// ������е���������ؽ��
		/// </summary> 
		/// <returns></returns>
		private void SendJxSamplerUnloadCmd()
		{
			taskSimpleScheduler = new TaskSimpleScheduler();

			autoResetEvent.Reset();

			taskSimpleScheduler.StartNewTask("ж��ҵ���߼�", () =>
			{
				this.IsWorking = true;

				// ����ж������
				if (qcjxcySamplerDAO.SendSampleUnloadCmd(this.currentSampler.EquipmentCode, this.CurrentRCSampling.Id, this.currentSampleCode, (eEquInfSamplerUnloadType)Convert.ToInt16(flpanUnloadType.Controls.OfType<RadioButton>().First(a => a.Checked).Tag), this.currentEquInfSampleBarrels[0].BarrelNumber, out this.currentUnloadCmdId))
				{
					rTxtOutputer.Output("ж������ͳɹ����ȴ�������ִ��", eOutputType.Normal);

					int waitCount = 0;
					eEquInfCmdResultCode equInfCmdResultCode;
					do
					{
						Thread.Sleep(10000);
						if (waitCount % 5 == 0) rTxtOutputer.Output("���ڵȴ����������ؽ��", eOutputType.Normal);

						waitCount++;

						// ��ȡж�������ִ�н��
						equInfCmdResultCode = UnloadSamplerDAO.GetInstance().GetQCJXCYUnloadSamplerState(this.currentUnloadCmdId);
					}
					while (equInfCmdResultCode == eEquInfCmdResultCode.Ĭ��);

					if (equInfCmdResultCode == eEquInfCmdResultCode.�ɹ�)
					{
						string barrelNumbers = string.Empty;
						foreach (string item in this.currentEquInfSampleBarrels.Select(a => a.BarrelNumber).ToList())
						{
							barrelNumbers += item + " ";
						}
						rTxtOutputer.Output("���������أ�ж���ɹ�����Ͱ��ţ�" + barrelNumbers, eOutputType.Normal);

						// ���������ϵͳ״̬
						if (rbtnToMaker.Checked)
						{
							string makerSystemStatus = commonDAO.GetSignalDataValue(this.makerMachineCode, eSignalDataName.�豸״̬.ToString());
							if (makerSystemStatus == eEquInfSamplerSystemStatus.��������.ToString())
							{
								//if (MessageBoxEx.Show("ж���ɹ�����⵽�������Ѿ��������̿�ʼ������", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
								//{
								//    if (SendMakePlan(this.CurrentRCSampling.Id, this.CurrentRCSampling.InFactoryBatchId))
								//        rTxtOutputer.Output("��������ͳɹ�", eOutputType.Normal);
								//    else
								//        rTxtOutputer.Output("���������ʧ��", eOutputType.Error);
								//}
								MessageBoxEx.Show("ж���ɹ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
							}
						}
					}
					else if (equInfCmdResultCode == eEquInfCmdResultCode.ʧ��)
					{
						rTxtOutputer.Output("���������أ�ж��ʧ��", eOutputType.Error);
					}
				}
				else
				{
					rTxtOutputer.Output("ж�������ʧ��", eOutputType.Error);
				}

				this.IsWorking = false;

				LoadSampleBarrel(superGridControl1, this.CurrentSampler.EquipmentCode);
				LoadLatestSampleUnloadCmd(this.CurrentSampler);

				autoResetEvent.Set();
			});
		}

		/// <summary>
		/// �������������ؽ��
		/// </summary>
		private void SendJxPackingBatchCmd(CmcsRCSampleBarrel rCSampleBarrel)
		{
			taskSimpleScheduler = new TaskSimpleScheduler();

			autoResetEvent.Reset();

			taskSimpleScheduler.StartNewTask("����ҵ���߼�", () =>
			{
				this.IsWorking = true;

				// ���͵�������
				if (PackingBatchDAO.GetInstance().SendPackingBatch(this.currentSampler.EquipmentCode, this.currentSampleCode, out currentMessage))
				{
					rTxtOutputer.Output("��������ͳɹ����ȴ�������ִ��", eOutputType.Normal);

					int waitCount = 0;
					eEquInfCmdResultCode equInfCmdResultCode;
					do
					{
						Thread.Sleep(10000);
						if (waitCount % 5 == 0) rTxtOutputer.Output("���ڵȴ����������ؽ��", eOutputType.Normal);

						waitCount++;

						// ��ȡж�������ִ�н��
						equInfCmdResultCode = PackingBatchDAO.GetInstance().GetQCJXPackingBatchState(this.currentSampleCode);
					}
					while (equInfCmdResultCode == eEquInfCmdResultCode.Ĭ��);

					if (equInfCmdResultCode == eEquInfCmdResultCode.�ɹ�)
					{
						rTxtOutputer.Output("���������أ����ϳɹ�", eOutputType.Normal);

						// ���������ϵͳ״̬
						if (rbtnToMaker.Checked)
						{
							string makerSystemStatus = commonDAO.GetSignalDataValue(this.makerMachineCode, eSignalDataName.�豸״̬.ToString());
							if (makerSystemStatus == eEquInfSamplerSystemStatus.��������.ToString())
							{
								//if (MessageBoxEx.Show("���ϳɹ�����⵽�������Ѿ��������̿�ʼ������", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
								//{
								//    if (SendMakePlan(rCSampleBarrel.SamplingId, rCSampleBarrel.InFactoryBatchId))
								//        //if (SendMakePlan(this.CurrentRCSampling.Id, this.CurrentRCSampling.InFactoryBatchId))
								//        rTxtOutputer.Output("��������ͳɹ�", eOutputType.Normal);
								//    else
								//        rTxtOutputer.Output("���������ʧ��", eOutputType.Error);
								//}
								MessageBoxEx.Show("���ϳɹ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
							}
						}
					}
					else if (equInfCmdResultCode == eEquInfCmdResultCode.ʧ��)
					{
						rTxtOutputer.Output("���������أ�ж��ʧ��", eOutputType.Error);
					}
				}
				else
				{
					rTxtOutputer.Output("ж�������ʧ��", eOutputType.Error);
				}

				this.IsWorking = false;

				LoadSampleBarrel(superGridControl1, this.CurrentSampler.EquipmentCode);
				LoadLatestSampleUnloadCmd(this.CurrentSampler);

				autoResetEvent.Set();
			});
		}

		/// <summary>
		/// ���Ľ���ؼ��Ŀ�������
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
		/// ������������
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnSendMakeCmd_Click(object sender, EventArgs e)
		{
			// ���������ϵͳ״̬
			string makerSystemStatus = commonDAO.GetSignalDataValue(this.makerMachineCode, eSignalDataName.�豸״̬.ToString());
			if (rbtnToMaker.Checked && makerSystemStatus != eEquInfSystemStatus.��������.ToString())
			{
				MessageBoxEx.Show("������ϵͳδ��������ֹ����", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			String tempSampleCode = String.Empty;
			if (Dbers.GetInstance().SelfDber.Get<CmcsCMEquipment>(this.currentSampler.Parentid).EquipmentCode == "������е������")
			{
				InfQCJXCYUnLoadCMD qcjxcyUnLoadCMD = null;
				foreach (GridRow gridRow in superGridControl3.PrimaryGrid.Rows)
				{
					if (gridRow.Checked)
						qcjxcyUnLoadCMD = gridRow.DataItem as InfQCJXCYUnLoadCMD;
				}
				if (qcjxcyUnLoadCMD == null)
				{
					MessageBoxEx.Show("��ѡ��ж����¼", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}
				tempSampleCode = qcjxcyUnLoadCMD.SampleCode.Trim();
			}

			CmcsRCSampleBarrel rCSampleBarrel = commonDAO.SelfDber.Entity<CmcsRCSampleBarrel>(" where BarrelCode='" + tempSampleCode + "'");
			if (rCSampleBarrel == null)
			{
				MessageBoxEx.Show("δ�ҵ���������ϸ��¼", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}

			if (MessageBoxEx.Show("ȷ��Ҫ�����������", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
				SendMakePlan(rCSampleBarrel.SamplingId, rCSampleBarrel.InFactoryBatchId);
		}

		/// <summary>
		/// ���͵�������
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void btnFallCode_Click(object sender, EventArgs e)
		{

			// ��������ϵͳ״̬
			string makerSystemStatus = commonDAO.GetSignalDataValue(GlobalVars.MachineCode_PackingBatch_KY, eSignalDataName.�豸״̬.ToString());
			if (makerSystemStatus != eEquInfSamplerSystemStatus.��������.ToString())
			{
				MessageBoxEx.Show("������ϵͳδ��������ֹ����", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			// ���������ϵͳ״̬
			makerSystemStatus = commonDAO.GetSignalDataValue(this.makerMachineCode, eSignalDataName.�豸״̬.ToString());
			if (makerSystemStatus != eEquInfSamplerSystemStatus.��������.ToString())
			{
				MessageBoxEx.Show("������ϵͳδ��������ֹ����", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			string tempSampleCode = string.Empty, tempMakeCode = string.Empty;
			CmcsRCSampleBarrel rCSampleBarrel = null;
			if (Dbers.GetInstance().SelfDber.Get<CmcsCMEquipment>(this.currentSampler.Parentid).EquipmentCode == "������е������")
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
					MessageBoxEx.Show("��ѡ���������Ͱ", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}
				tempSampleCode = qcjxcyUnLoadCMD.SampleCode;
				this.currentSampleCode = tempSampleCode;
				rCSampleBarrel = commonDAO.SelfDber.Entity<CmcsRCSampleBarrel>(" where BarrelCode='" + tempSampleCode + "'");
				if (rCSampleBarrel == null)
				{
					MessageBoxEx.Show("δ�ҵ���������ϸ��¼", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
					return;
				}
			}
			if (MessageBoxEx.Show("ȷ��Ҫ���͵������", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
				SendJxPackingBatchCmd(rCSampleBarrel);
		}

		#endregion

		#region SuperGridControl

		#region superGridControl1 ������������

		/// <summary>
		/// ѡ��֮ǰ
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void superGridControl1_BeforeCheck(object sender, GridBeforeCheckEventArgs e)
		{
			GridRow gridRow = (e.Item as GridRow);
			if (gridRow == null) { return; }
			InfEquInfSampleBarrel equInfSampleBarrel = gridRow.DataItem as InfEquInfSampleBarrel;
			if (gridRow.Checked && !e.Cancel)//ȡ��ѡ��
			{
				this.currentSampleCode = "";
				this.CurrentRCSampling = null;
				this.currentEquInfSampleBarrels.Clear();
				if (rbtnToMaker.Checked)//������
				{
					UnCheckedSameBarrelRow(sender as SuperGridControl, equInfSampleBarrel);
				}
				else if (rbtnToSubway.Checked)
				{
					this.currentEquInfSampleBarrels.Clear();
				}
			}
			else//ѡ��
			{
				this.currentSampleCode = equInfSampleBarrel.SampleCode;
				this.CurrentRCSampling = commonDAO.GetSamplingBySamplingCode(equInfSampleBarrel.SampleCode);
				if (rbtnToMaker.Checked)//������
				{
					// ѡ��������ͬ����������޼�¼
					CheckedSameBarrelRow(sender as SuperGridControl, equInfSampleBarrel);
				}
				else if (rbtnToSubway.Checked)
				{
					this.currentEquInfSampleBarrels.Clear();
					this.currentEquInfSampleBarrels.Add(equInfSampleBarrel);
					// ȡ�������е�ѡ��״̬
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
			//    // ȡ�������е�ѡ��״̬
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
			//        // ѡ��������ͬ����������޼�¼
			//        CheckedSameBarrelRow(sender as SuperGridControl, equInfSampleBarrel);
			//    }
			//    this.CurrentRCSampling = commonDAO.GetSamplingBySamplingCode(equInfSampleBarrel.SampleCode);
			//    // ���ز�����
			//    //LoadRCSamplingList(superGridControl2, equInfSampleBarrel.InFactoryBatchId);
			//}
			//else e.Cancel = true;
		}

		private void superGridControl1_AfterCheck(object sender, GridAfterCheckEventArgs e)
		{

		}

		private void superGridControl1_BeginEdit(object sender, GridEditEventArgs e)
		{
			// ȡ���༭
			e.Cancel = true;
		}

		/// <summary>
		/// �����к�
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
		/// �������
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

		#region superGridControl2 ������������
		/// <summary>
		/// ѡ����Ͱ
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

			// ȡ�������е�ѡ��״̬
			foreach (GridRow gridRowItem in superGridControl2.PrimaryGrid.Rows)
			{
				InfPackingBatchCoord rCSampling = gridRowItem.DataItem as InfPackingBatchCoord;
				if (rCSampling.Id == this.CurrentCoord.Id) continue;

				gridRowItem.Checked = false;
			}
		}

		private void superGridControl2_BeginEdit(object sender, GridEditEventArgs e)
		{
			// ȡ���༭
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

		#region superGridControl3 ���ж����¼

		private void superGridControl3_CellClick(object sender, GridCellClickEventArgs e)
		{
			if (Dbers.GetInstance().SelfDber.Get<CmcsCMEquipment>(this.currentSampler.Parentid).EquipmentCode == "Ƥ��������")
			{
				InfBeltSampleUnloadCmd sampleUnloadCmd = e.GridCell.GridRow.DataItem as InfBeltSampleUnloadCmd;

				foreach (GridRow gridRow in superGridControl3.PrimaryGrid.Rows)
				{
					InfBeltSampleUnloadCmd beltSampleUnloadCmd = gridRow.DataItem as InfBeltSampleUnloadCmd;
					gridRow.Checked = (beltSampleUnloadCmd != null && sampleUnloadCmd.Id == beltSampleUnloadCmd.Id);
				}
			}
			else if (Dbers.GetInstance().SelfDber.Get<CmcsCMEquipment>(this.currentSampler.Parentid).EquipmentCode == "������е������")
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
			// ȡ���༭
			e.Cancel = true;
		}

		#endregion

		#endregion

		#region ��������

		/// <summary>
		/// Invoke��װ
		/// </summary>
		/// <param name="action"></param>
		public void InvokeEx(Action action)
		{
			if (this.IsDisposed || !this.IsHandleCreated) return;

			this.Invoke(action);
		}

		#endregion

		#region ���ж����¼����
		private void BindData()
		{
			if (Dbers.GetInstance().SelfDber.Get<CmcsCMEquipment>(this.CurrentSampler.Parentid).EquipmentCode == "Ƥ��������")
				superGridControl3.PrimaryGrid.DataSource = commonDAO.SelfDber.Entities<InfBeltSampleUnloadCmd>(" where MachineCode=:MachineCode and CreateDate>=:StartTime and CreateDate<:EndTime order by createdate desc", new { MachineCode = this.CurrentSampler.EquipmentCode, StartTime = dtStartTime.Value.Date, EndTime = dtEndTime.Value.AddDays(1).Date });
			else if (Dbers.GetInstance().SelfDber.Get<CmcsCMEquipment>(this.CurrentSampler.Parentid).EquipmentCode == "������е������")
				superGridControl3.PrimaryGrid.DataSource = commonDAO.SelfDber.Entities<InfQCJXCYUnLoadCMD>(" where MachineCode=:MachineCode and CreateDate>=:StartTime and CreateDate<:EndTime order by createdate desc", new { MachineCode = this.CurrentSampler.EquipmentCode, StartTime = dtStartTime.Value.Date, EndTime = dtEndTime.Value.AddDays(1).Date });
		}

		private void btnSearch_Click(object sender, EventArgs e)
		{
			if (dtStartTime.Value.Year < 2000)
			{
				MessageBoxEx.Show("��ѡ��ʼʱ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			if (dtEndTime.Value.Year < 2000)
			{
				MessageBoxEx.Show("��ѡ�����ʱ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

		#region ��������Ͱ����

		private void btnSearch_BatchCoord_Click(object sender, EventArgs e)
		{
			LoadBatchCoord(superGridControl2);
		}
		#endregion


	}
}
