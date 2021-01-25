using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;
//
using WB.XiangPing.Balance;
using CMCS.Common;
using CMCS.Common.DAO;
using CMCS.Common.Entities.AutoMaker;
using CMCS.Common.Entities.BaseInfo;
using CMCS.Common.Entities.Fuel;
using CMCS.Common.Enums;
using CMCS.Common.Utilities;
using CMCS.Forms.UserControls;
using CMCS.WeighCheck.DAO;
using CMCS.WeighCheck.SampleMake.Enums;
using CMCS.WeighCheck.SampleMake.Utilities;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using DevComponents.DotNetBar.Metro;
using CMCS.WeighCheck.SampleMake.Core;
namespace CMCS.WeighCheck.SampleMake.Frms.Make
{
	public partial class FrmMakeTake : MetroForm
	{
		public enum eOutputType
		{
			[System.ComponentModel.Description("#BD86FA")]
			Normal,
			[System.ComponentModel.Description("#A50081")]
			Important,
			[System.ComponentModel.Description("#F9C916")]
			Warn,
			[System.ComponentModel.Description("#DB2606")]
			Error
		}

		public static string UniqueKey = "FrmMakeTake";

		private CodePrinterSample _CodePrinter;

		private CommonDAO commonDAO = CommonDAO.GetInstance();

		private CZYHandlerDAO czyHandlerDAO = CZYHandlerDAO.GetInstance();

		private eFlowFlag currentFlowFlag;

		private CmcsCMEquipment autoMaker;

		private CmcsRCMake rCMake;

		private CmcsRCSampleBarrel rCSampleBarrel;

		private System.Collections.Generic.List<CmcsRCSampleBarrel> brotherRCSampleBarrels = new System.Collections.Generic.List<CmcsRCSampleBarrel>();

		private System.Collections.Generic.List<string> isScanedRCSampleBarrelId = new System.Collections.Generic.List<string>();

		private string resMessage = string.Empty;

		private double currentWeight;

		private bool isUseWeight = true;

		private bool wbSteady;

		private double wbMinWeight;

		public eFlowFlag CurrentFlowFlag
		{
			get
			{
				return this.currentFlowFlag;
			}
			set
			{
				this.currentFlowFlag = value;
				this.lblCurrentFlowFlag.Text = value.ToString();
			}
		}

		public CmcsCMEquipment AutoMaker
		{
			get
			{
				return this.autoMaker;
			}
			set
			{
				this.autoMaker = value;
				this.lblAutoMakerName.Text = ((value != null) ? value.EquipmentName : "δ����");
				this.btnSendMakePlan.Visible = (value != null);
				this.timer2.Enabled = (value != null);
			}
		}

		public CmcsRCMake RCMake
		{
			get
			{
				return this.rCMake;
			}
			set
			{
				this.rCMake = value;
				this.btnSendMakePlan.Enabled = (value != null);
				this.btnPrintMakeCode.Enabled = (value != null);
				this.IsScanedRCSampleBarrelId.Clear();
			}
		}

		public CmcsRCSampleBarrel RCSampleBarrel
		{
			get
			{
				return this.rCSampleBarrel;
			}
			set
			{
				this.rCSampleBarrel = value;
			}
		}

		public System.Collections.Generic.List<string> IsScanedRCSampleBarrelId
		{
			get
			{
				return this.isScanedRCSampleBarrelId;
			}
			set
			{
				this.isScanedRCSampleBarrelId = value;
			}
		}

		public double CurrentWeight
		{
			get
			{
				return this.currentWeight;
			}
			set
			{
				this.currentWeight = value;
				this.lbl_CurrentWeight.Text = value.ToString() + "KG";
			}
		}

		public bool IsUseWeight
		{
			get
			{
				return this.isUseWeight;
			}
			set
			{
				this.isUseWeight = value;
				this.lblweight.Visible = value;
				this.lbl_CurrentWeight.Visible = value;
				this.lblWber.Visible = value;
				this.slightWber.Visible = value;
			}
		}

		public bool WbSteady
		{
			get
			{
				return this.wbSteady;
			}
			set
			{
				this.wbSteady = value;
			}
		}

		public double WbMinWeight
		{
			get
			{
				return this.wbMinWeight;
			}
			set
			{
				this.wbMinWeight = value;
			}
		}

		public FrmMakeTake()
		{
			this.InitializeComponent();
		}

		public void InitFrom()
		{
			this.IsUseWeight = System.Convert.ToBoolean(this.commonDAO.GetAppletConfigInt32("���ó���"));
			this._CodePrinter = new CodePrinterSample(this.printDocument1);
			this.AutoMaker = this.commonDAO.GetCMEquipmentByMachineCode(this.commonDAO.GetCommonAppletConfigString(CommonAppConfig.GetInstance().AppIdentifier + "��Ӧ������"));
		}

		/// <summary>
		/// �Դ��弰�������ӿؼ�����˫�ػ���
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

		private void FrmSampleCheck_Load(object sender, System.EventArgs e)
		{
			this.InitFrom();
			this.InitHardware();
		}

		private void FrmSampleCheck_FormClosing(object sender, System.Windows.Forms.FormClosingEventArgs e)
		{
			this.UnloadHardware();
		}

		private void Wber_OnSteadyChange(bool steady)
		{
			this.InvokeEx(delegate
			{
				this.WbSteady = steady;
			});
		}

		private void Wber_OnStatusChange(bool status)
		{
			this.InvokeEx(delegate
			{
				this.slightWber.LightColor = (status ? System.Drawing.Color.Green : System.Drawing.Color.Red);
			});
		}

		private void wber_OnWeightChange(double weight)
		{
			this.InvokeEx(delegate
			{
				this.CurrentWeight = weight;
			});
		}

		private void InitHardware()
		{
			try
			{
				if (this.IsUseWeight)
				{
					this.WbMinWeight = this.commonDAO.GetAppletConfigDouble("���ӳ���С����");
					Hardwarer.Wber.OnStatusChange += new XiangPing_Balance.StatusChangeHandler(this.Wber_OnStatusChange);
					Hardwarer.Wber.OnSteadyChange += new XiangPing_Balance.SteadyChangeEventHandler(this.Wber_OnSteadyChange);
					Hardwarer.Wber.OnWeightChange += new XiangPing_Balance.WeightChangeEventHandler(this.wber_OnWeightChange);
					if (!SelfVars.WeightOpen)
					{
						bool weightOpen = Hardwarer.Wber.OpenCom(this.commonDAO.GetAppletConfigInt32("���ӳӴ���"), this.commonDAO.GetAppletConfigInt32("���ӳӲ�����"), this.commonDAO.GetAppletConfigInt32("���ӳ�����λ"), this.commonDAO.GetAppletConfigInt32("���ӳ�ֹͣλ"));
						SelfVars.WeightOpen = weightOpen;
					}
				}
				this.timer1.Enabled = true;
			}
			catch (System.Exception ex)
			{
				Log4Neter.Error("�豸��ʼ��", ex);
			}
		}

		private void UnloadHardware()
		{
			System.Windows.Forms.Application.DoEvents();
			try
			{
				Hardwarer.Wber.CloseCom();
			}
			catch
			{
			}
		}

		private void timer1_Tick(object sender, System.EventArgs e)
		{
			this.timer1.Stop();
			try
			{
				switch (this.CurrentFlowFlag)
				{
					case eFlowFlag.�ȴ�У��:
						if (this.czyHandlerDAO.UpdateRCSampleBarrelCheckSampleWeight(this.rCSampleBarrel.Id, Hardwarer.Wber.Weight, SelfVars.LoginUser.UserName))
						{
							this.ShowMessage("У��ɹ���������" + Hardwarer.Wber.Weight.ToString() + "KG", eOutputType.Normal);
							if (this.IsScanedRCSampleBarrelId.Count == this.brotherRCSampleBarrels.Count)
							{
								this.ShowMessage("�û�����Ͱ��ȫ��У�����!", FrmMakeTake.eOutputType.Normal);
								this.txtInputSampleCode.ResetText();
								this.RCMake = this.czyHandlerDAO.GetRCMakeBySampleId(this.brotherRCSampleBarrels[0].SamplingId);
								if (this.RCMake != null)
								{
									this.txtInputSampleCode.ResetText();
									this.CurrentFlowFlag = eFlowFlag.�ȴ�ɨ��;
								}
								else
								{
									this.ShowMessage("δ�ҵ�������", FrmMakeTake.eOutputType.Error);
								}
							}
							else
							{
								this.txtInputSampleCode.ResetText();
								this.CurrentFlowFlag = eFlowFlag.�ȴ�ɨ��;
							}
						}
						else
						{
							this.ShowMessage("У��ʧ�ܻ�����У�飬����ϵ����Ա", FrmMakeTake.eOutputType.Error);
							this.CurrentFlowFlag = eFlowFlag.�ȴ�У��;
						}
						break;
					case eFlowFlag.����У��:
						if (Hardwarer.Wber.Status && Hardwarer.Wber.Weight > this.WbMinWeight && this.WbSteady)
						{
							this.CurrentFlowFlag = eFlowFlag.�ȴ�У��;
						}
						break;
				}
			}
			catch (System.Exception ex)
			{
				this.ShowMessage("Timer1�����쳣" + ex.Message, FrmMakeTake.eOutputType.Error);
			}
			this.timer1.Start();
		}

		private void Restet()
		{
			this.CurrentFlowFlag = eFlowFlag.�ȴ�ɨ��;
			this.RCMake = null;
			this.RCSampleBarrel = null;
			this.brotherRCSampleBarrels.Clear();
			this.IsScanedRCSampleBarrelId.Clear();
			this.CurrentWeight = 0.0;
			this.btnSendMakePlan.Enabled = false;
			this.txtInputSampleCode.ResetText();
			this.rtxtOutputInfo.ResetText();
			this.txtInputSampleCode.Focus();
			this.CreateButtonX(10);
			this.ShowButton(0, "Clear", 0);
		}

		private void SendMakePlanAndStart()
		{
			if (this.brotherRCSampleBarrels.Count == 0)
			{
				this.ShowMessage("������ɲ���ͰУ��", FrmMakeTake.eOutputType.Error);
				return;
			}
			if (this.RCMake == null)
			{
				this.ShowMessage("δ�ҵ���������¼", FrmMakeTake.eOutputType.Error);
				return;
			}
			if (this.commonDAO.GetSignalDataValue(this.AutoMaker.EquipmentCode, eSignalDataName.�豸״̬.ToString()) != eEquInfSamplerSystemStatus.��������.ToString())
			{
				MessageBoxEx.Show("������δ��������ֹ������������!", "��ʾ", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Exclamation);
				return;
			}
			if (MessageBoxEx.Show("׼����������������ȷ��ú����ȫ�������������϶�!", "��ʾ", System.Windows.Forms.MessageBoxButtons.OKCancel, System.Windows.Forms.MessageBoxIcon.Asterisk) == System.Windows.Forms.DialogResult.OK)
			{
				InfMakerPlan entity = new InfMakerPlan
				{
					InterfaceType = this.commonDAO.GetMachineInterfaceTypeByCode(this.AutoMaker.EquipmentCode),
					MachineCode = this.AutoMaker.EquipmentCode,
					InFactoryBatchId = this.brotherRCSampleBarrels[0].InFactoryBatchId,
					MakeCode = this.RCMake.MakeCode,
					FuelKindName = "��ú",
					Mt = "ʪú",
					MakeType = "��������",
					CoalSize = "С����"
				};
				if (AutoMakerDAO.GetInstance().SaveMakerPlanAndStartCmd(entity, out this.resMessage))
				{
					this.CurrentFlowFlag = eFlowFlag.�ȴ��������;
					this.ShowMessage(this.resMessage, FrmMakeTake.eOutputType.Normal);
					return;
				}
				this.ShowMessage(this.resMessage, FrmMakeTake.eOutputType.Error);
			}
		}

		private void txtInputSampleCode_KeyUp(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			if (e.KeyCode == System.Windows.Forms.Keys.Return)
			{
				if (this.CurrentFlowFlag != eFlowFlag.�ȴ�ɨ��)
				{
					return;
				}
				string barrelCode = this.txtInputSampleCode.Text.Trim();
				if (string.IsNullOrWhiteSpace(barrelCode))
				{
					return;
				}
				if (this.brotherRCSampleBarrels.Count == 0)
				{
					this.brotherRCSampleBarrels = this.czyHandlerDAO.GetRCSampleBarrels(barrelCode, out this.resMessage);
					if (this.brotherRCSampleBarrels.Count == 0)
					{
						this.ShowMessage(this.resMessage, FrmMakeTake.eOutputType.Error);
						this.txtInputSampleCode.ResetText();
						return;
					}
					this.ShowMessage(this.resMessage, FrmMakeTake.eOutputType.Normal);
					this.CreateButtonX(this.brotherRCSampleBarrels.Count);
					this.ShowButton(this.brotherRCSampleBarrels.Count, "Sum", 0);
				}
				this.rCSampleBarrel = (from a in this.brotherRCSampleBarrels
									   where a.SampSecondCode == barrelCode
									   select a).FirstOrDefault<CmcsRCSampleBarrel>();
				if (this.rCSampleBarrel != null)
				{
					if (!this.IsScanedRCSampleBarrelId.Contains(this.rCSampleBarrel.Id))
					{
						this.IsScanedRCSampleBarrelId.Add(this.rCSampleBarrel.Id);
						this.ShowButton(this.IsScanedRCSampleBarrelId.Count, "Already", System.Convert.ToInt32(this.rCSampleBarrel.SampSecondCode.Substring(14, 2)));
						if (this.IsScanedRCSampleBarrelId.Count < this.brotherRCSampleBarrels.Count)
						{
							this.ShowMessage(string.Concat(new object[]
							{
								"��Ͱ���룺",
								barrelCode,
								"����ʣ",
								this.brotherRCSampleBarrels.Count - this.IsScanedRCSampleBarrelId.Count,
								"ͰδУ�飬��ɨ�¸���Ͱ"
							}), FrmMakeTake.eOutputType.Normal);
						}
						else
						{
							foreach (CmcsRCSampleBarrel current in this.brotherRCSampleBarrels)
							{
								CmcsSampleBarrel cmcsSampleBarrel = Dbers.GetInstance().SelfDber.Entity<CmcsSampleBarrel>(" where BarrelCode='" + current.BarrelCode + "' ", null);
								if (cmcsSampleBarrel != null)
								{
									cmcsSampleBarrel.IsUse = 0;
									Dbers.GetInstance().SelfDber.Update<CmcsSampleBarrel>(cmcsSampleBarrel);
								}
							}
							this.czyHandlerDAO.SaveHandSamplingReceive(this.brotherRCSampleBarrels[0].SamplingId, SelfVars.LoginUser.UserName, DateTime.Now);
							this.ShowMessage("��Ͱ���룺" + barrelCode + "����������Ͱ��ȫ��У��ɹ�", FrmMakeTake.eOutputType.Normal);
						}
						this.CurrentFlowFlag = eFlowFlag.�ȴ�У��;
						return;
					}
					this.txtInputSampleCode.ResetText();
					this.ShowMessage("��Ͱ���룺" + barrelCode + " ��У�飬��ɨ�¸���Ͱ", FrmMakeTake.eOutputType.Error);
					return;
				}
				else
				{
					this.txtInputSampleCode.ResetText();
					this.ShowMessage("��Ͱ���룺" + barrelCode + " У��ʧ�ܣ���ɨ�¸���Ͱ", FrmMakeTake.eOutputType.Error);
				}
			}
		}

		private void btnSendMakePlan_Click(object sender, System.EventArgs e)
		{
			this.SendMakePlanAndStart();
		}

		private void btnPrintMakeCode_Click(object sender, System.EventArgs e)
		{
			if (this.RCMake != null)
			{
				this._CodePrinter.Print(this.RCMake.MakeCode);
			}
		}

		private void btnReset_Click(object sender, System.EventArgs e)
		{
			this.Restet();
		}

		/// <summary>
		/// ��̬������Ͱ��ť
		/// </summary>
		/// <param name="count"></param>
		private void CreateButtonX(int count)
		{
			this.panSampleBarrels.Controls.Clear();
			this.panSampleBarrels.SuspendLayout();
			int num = 9;
			int num2 = 7;
			int num3 = 1;
			this.tableLayoutPanel1.RowStyles[3].Height = 80f;
			for (int i = 0; i < count; i++)
			{
				ButtonX buttonX = new ButtonX();
				buttonX.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
				buttonX.BackColor = System.Drawing.Color.Gainsboro;
				buttonX.ColorTable = eButtonColor.Flat;
				buttonX.Font = new System.Drawing.Font("Segoe UI", 24f, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, 0);
				buttonX.Location = new System.Drawing.Point(num, num2);
				buttonX.Margin = new System.Windows.Forms.Padding(6, 10, 6, 6);
				buttonX.Name = "CreatebuttonX" + (i + 1).ToString();
				buttonX.Shape = new RoundRectangleShapeDescriptor(10);
				buttonX.Size = new System.Drawing.Size(60, 60);
				buttonX.Style = eDotNetBarStyle.StyleManagerControlled;
				buttonX.TabIndex = 175;
				buttonX.Tag = "Btn";
				buttonX.Text = (i + 1).ToString();
				buttonX.TextColor = System.Drawing.Color.Black;
				this.panSampleBarrels.Controls.Add(buttonX);
				num += 69;
				if (num >= this.panSampleBarrels.Width - 60)
				{
					this.tableLayoutPanel1.RowStyles[3].Height = (float)(80 + 65 * num3);
					num3++;
					num2 += 65;
					num = 9;
				}
			}
		}

		private void RefreshSignalStatus()
		{
			string signalDataValue = this.commonDAO.GetSignalDataValue(this.AutoMaker.EquipmentCode, eSignalDataName.�豸״̬.ToString());
			if (signalDataValue == eEquInfSystemStatus.��������.ToString())
			{
				this.slightAutoMaker.LightColor = EquipmentStatusColors.BeReady;
				return;
			}
			if (signalDataValue == eEquInfSystemStatus.��������.ToString())
			{
				this.slightAutoMaker.LightColor = EquipmentStatusColors.Working;
				return;
			}
			if (signalDataValue == eEquInfSystemStatus.��������.ToString())
			{
				this.slightAutoMaker.LightColor = EquipmentStatusColors.Breakdown;
				return;
			}
			if (signalDataValue == eEquInfSystemStatus.ϵͳֹͣ.ToString())
			{
				this.slightAutoMaker.LightColor = EquipmentStatusColors.Forbidden;
			}
		}

		private void timer2_Tick(object sender, System.EventArgs e)
		{
			this.RefreshSignalStatus();
		}

		private void ClearBarrelCode()
		{
			this.txtInputSampleCode.ResetText();
			this.RCSampleBarrel = null;
			this.brotherRCSampleBarrels.Clear();
			this.RCMake = null;
			this.CurrentFlowFlag = eFlowFlag.�ȴ�ɨ��;
		}

		private void ShowMessage(string info, FrmMakeTake.eOutputType outputType)
		{
			this.OutputRunInfo(this.rtxtOutputInfo, info, outputType);
		}

		private void OutputRunInfo(RichTextBoxEx richTextBox, string text, FrmMakeTake.eOutputType outputType = FrmMakeTake.eOutputType.Normal)
		{
			base.Invoke(new System.EventHandler(delegate
			{
				if (richTextBox.TextLength > 100000)
				{
					richTextBox.Clear();
				}
				text = string.Format("{0}  {1}", System.DateTime.Now.ToString("HH:mm:ss"), text);
				richTextBox.SelectionStart = richTextBox.TextLength;
				switch (outputType)
				{
					case FrmMakeTake.eOutputType.Normal:
						richTextBox.SelectionColor = System.Drawing.ColorTranslator.FromHtml("#BD86FA");
						break;
					case FrmMakeTake.eOutputType.Important:
						richTextBox.SelectionColor = System.Drawing.ColorTranslator.FromHtml("#A50081");
						break;
					case FrmMakeTake.eOutputType.Warn:
						richTextBox.SelectionColor = System.Drawing.ColorTranslator.FromHtml("#F9C916");
						break;
					case FrmMakeTake.eOutputType.Error:
						richTextBox.SelectionColor = System.Drawing.ColorTranslator.FromHtml("#DB2606");
						break;
					default:
						richTextBox.SelectionColor = System.Drawing.Color.White;
						break;
				}
				richTextBox.AppendText(string.Format("{0}\r", text));
				richTextBox.ScrollToCaret();
			}));
		}

		private void ShowButton(int count, string type, int index = 0)
		{
			if (type == "Sum")
			{
				System.Collections.IEnumerator enumerator = this.panSampleBarrels.Controls.GetEnumerator();
				try
				{
					while (enumerator.MoveNext())
					{
						System.Windows.Forms.Control control = (System.Windows.Forms.Control)enumerator.Current;
						if (control.Tag != null && control.Tag.ToString() == "Btn")
						{
							for (int i = 1; i <= count; i++)
							{
								if (control.Text == i.ToString())
								{
									control.BackColor = System.Drawing.Color.FromArgb(0, 157, 218);
								}
							}
						}
					}
					return;
				}
				finally
				{
					System.IDisposable disposable = enumerator as System.IDisposable;
					if (disposable != null)
					{
						disposable.Dispose();
					}
				}
			}
			if (type == "Already")
			{
				System.Collections.IEnumerator enumerator2 = this.panSampleBarrels.Controls.GetEnumerator();
				try
				{
					while (enumerator2.MoveNext())
					{
						System.Windows.Forms.Control control2 = (System.Windows.Forms.Control)enumerator2.Current;
						if (control2.Tag != null && control2.Tag.ToString() == "Btn" && control2.Text == index.ToString())
						{
							control2.BackColor = System.Drawing.Color.Red;
						}
					}
					return;
				}
				finally
				{
					System.IDisposable disposable2 = enumerator2 as System.IDisposable;
					if (disposable2 != null)
					{
						disposable2.Dispose();
					}
				}
			}
			if (type == "Clear")
			{
				foreach (System.Windows.Forms.Control control3 in this.panSampleBarrels.Controls)
				{
					if (control3.Tag != null && control3.Tag.ToString() == "Btn")
					{
						control3.BackColor = System.Drawing.Color.DarkGray;
					}
				}
			}
		}

		public void InvokeEx(Action action)
		{
			if (base.IsDisposed || !base.IsHandleCreated)
			{
				return;
			}
			base.Invoke(action);
		}
	}

}
