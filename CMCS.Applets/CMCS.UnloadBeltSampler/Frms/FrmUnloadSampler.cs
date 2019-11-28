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
        /// ����Ψһ��ʶ��
        /// </summary>
        public static string UniqueKey = "FrmUnloadSampler";

        #region Vars

        CommonDAO commonDAO = CommonDAO.GetInstance();
        BeltSamplerDAO beltSamplerDAO = BeltSamplerDAO.GetInstance();

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
                    LoadBeltSampleBarrel(superGridControl1, value.EquipmentCode);
                    LoadLatestSampleUnloadCmd(value.EquipmentCode);

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

        /// <summary>
        /// ��ǰж������id
        /// </summary>
        string currentUnloadCmdId;
        /// <summary>
        /// ������Ϣ
        /// </summary>
        string currentMessage;
        /// <summary>
        /// ��ǰѡ���������Ϣ
        /// </summary>
        public List<InfEquInfSampleBarrel> currentEquInfSampleBarrels = new List<InfEquInfSampleBarrel>();

        /// <summary>
        /// ���������� Ĭ��#1������
        /// </summary>
        string[] samplerMachineCodes = new string[] { GlobalVars.MachineCode_InPDCYJ_1 };

        /// <summary>
        /// ���������� Ĭ��#1ȫ�Զ�������
        /// </summary>
        string makerMachineCode = GlobalVars.MachineCode_QZDZYJ_1;

        Color[] CellColors = new Color[] { ColorTranslator.FromHtml("#7D00FFFF"), ColorTranslator.FromHtml("#7DFFFF00"), ColorTranslator.FromHtml("#7D7CFC00"), ColorTranslator.FromHtml("#7DFF69B4"), ColorTranslator.FromHtml("#7DFF00FF"), ColorTranslator.FromHtml("#7DADD8E6"), ColorTranslator.FromHtml("#7D00FF00"), ColorTranslator.FromHtml("#7DFFC0CB") };
        /// <summary>
        /// �������ɫ
        /// </summary>
        Dictionary<string, Color> dicCellColors = new Dictionary<string, Color>();

        RTxtOutputer rTxtOutputer;

        #endregion

        /// <summary>
        /// �����ʼ��
        /// </summary>
        private void FormInit()
        {
            rTxtOutputer = new RTxtOutputer(rTxTMessageInfo);

            // �������豸���룬��ж������һһ��Ӧ
            samplerMachineCodes = commonDAO.GetAppletConfigString("�������豸����").Split('|');
            makerMachineCode = commonDAO.GetAppletConfigString("�������豸����");

            CreateSamplerButton();
            CreateEquStatus();

            // ����ѡ���һ̨������
            if (flpanSamplerButton.Controls.Count > 0) (flpanSamplerButton.Controls[0] as RadioButton).Checked = true;
        }

        private void FrmUnloadSampler_Load(object sender, EventArgs e)
        {
            FormInit();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        #region ж��ҵ��

        /// <summary>
        /// �󶨼�������Ϣ
        /// </summary>
        /// <param name="superGridControl"></param>
        /// <param name="machineCode">�������豸����</param>
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
        /// ѡ�в�����һ�µļ�¼
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
        /// ��������Id���ز������б�
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
        /// ���ز�������������ж����¼
        /// </summary>
        /// <param name="samplerMachineCode">����������</param>
        private void LoadLatestSampleUnloadCmd(string samplerMachineCode)
        {
            superGridControl3.PrimaryGrid.DataSource = commonDAO.SelfDber.TopEntities<InfBeltSampleUnloadCmd>(3, " where MachineCode='" + samplerMachineCode + "' order by createdate desc");
        }

        /// <summary>
        /// ���������ƻ�
        /// </summary>
        /// <param name="rCSamplingId">������Id</param>
        /// <param name="infactoryBatchId">����Id</param>
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
                if (systemStatus == eEquInfSamplerSystemStatus.��������.ToString())
                    uCtrlSignalLight.LightColor = EquipmentStatusColors.BeReady;
                else if (systemStatus == eEquInfSamplerSystemStatus.��������.ToString() || systemStatus == eEquInfSamplerSystemStatus.����ж��.ToString())
                    uCtrlSignalLight.LightColor = EquipmentStatusColors.Working;
                else if (systemStatus == eEquInfSamplerSystemStatus.��������.ToString())
                    uCtrlSignalLight.LightColor = EquipmentStatusColors.Breakdown;
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
        }

        #endregion

        #region ����

        void rbtnSampler_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rbtnSampler = sender as RadioButton;
            this.CurrentSampler = rbtnSampler.Tag as CmcsCMEquipment;
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
                LoadBeltSampleBarrel(superGridControl1, this.currentSampler.EquipmentCode);
                return;
            }
            if (currentRCSampling == null)
            {
                MessageBoxEx.Show("�빴ѡ�󶨵Ĳ��������ٷ���", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ��������ϵͳ��״̬
            string samplerSystemStatue = commonDAO.GetSignalDataValue(this.currentSampler.EquipmentCode, eSignalDataName.�豸״̬.ToString());
            if (samplerSystemStatue != eEquInfSamplerSystemStatus.��������.ToString())
            {
                MessageBoxEx.Show("������ϵͳδ��������ֹж��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // ���������ϵͳ״̬
            string makerSystemStatus = commonDAO.GetSignalDataValue(this.makerMachineCode, eSignalDataName.�豸״̬.ToString());
            if (rbtnToMaker.Checked && makerSystemStatus != eEquInfSamplerSystemStatus.��������.ToString())
            {
                MessageBoxEx.Show("������ϵͳδ��������ֹж��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string message = string.Empty;
            if (!beltSamplerDAO.CanSendSampleUnloadCmd(this.currentSampler.EquipmentCode, out message))
            {
                MessageBoxEx.Show(message, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SendSamplerUnloadCmd();
        }

        /// <summary>
        /// �������������ؽ��
        /// </summary> 
        /// <returns></returns>
        private void SendSamplerUnloadCmd()
        {
            taskSimpleScheduler = new TaskSimpleScheduler();

            autoResetEvent.Reset();

            taskSimpleScheduler.StartNewTask("ж��ҵ���߼�", () =>
            {
                this.IsWorking = true;

                if (rbtnToMaker.Checked)
                {
                    #region ������������Ƥ��
                    if (beltSamplerDAO.SendPDUnLoadCmd(this.currentSampler.EquipmentCode, "��ʼж��"))
                    {
                        rTxtOutputer.Output("ж��Ƥ������ͳɹ����ȴ�������ִ��", eOutputType.Normal);

                        int waitCount = 0;
                        eEquInfCmdResultCode equInfCmdResultCode;
                        do
                        {
                            Thread.Sleep(10000);
                            if (waitCount % 5 == 0) rTxtOutputer.Output("���ڵȴ����������ؽ��", eOutputType.Normal);

                            waitCount++;

                            // ��ȡж�������ִ�н��
                            equInfCmdResultCode = UnloadSamplerDAO.GetInstance().GetPDUnloadState();
                        }
                        while (equInfCmdResultCode == eEquInfCmdResultCode.Ĭ��);
                        if (equInfCmdResultCode == eEquInfCmdResultCode.�ɹ�)
                        {
                            rTxtOutputer.Output("���������أ�Ƥ�������ɹ�", eOutputType.Normal);
                        }
                        else if (equInfCmdResultCode == eEquInfCmdResultCode.ʧ��)
                        {
                            rTxtOutputer.Output("���������أ�ִ��ʧ��", eOutputType.Error);
                        }
                    }
                    else
                    {
                        rTxtOutputer.Output("ж��Ƥ�������ʧ��", eOutputType.Error);
                    }
                    #endregion
                }

                #region ���Ͳ�����ж������
                // ����ж������
                if (beltSamplerDAO.SendSampleUnloadCmd(this.currentSampler.EquipmentCode, this.CurrentRCSampling.Id, this.currentSampleCode, (eEquInfSamplerUnloadType)Convert.ToInt16(flpanUnloadType.Controls.OfType<RadioButton>().First(a => a.Checked).Tag), out this.currentUnloadCmdId))
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
                        equInfCmdResultCode = UnloadSamplerDAO.GetInstance().GetUnloadSamplerState(this.currentUnloadCmdId);
                    }
                    while (equInfCmdResultCode == eEquInfCmdResultCode.Ĭ��);

                    if (equInfCmdResultCode == eEquInfCmdResultCode.�ɹ�)
                    {
                        rTxtOutputer.Output("���������أ�ж���ɹ�", eOutputType.Normal);
                    }
                    else if (equInfCmdResultCode == eEquInfCmdResultCode.ʧ��)
                    {
                        rTxtOutputer.Output("���������أ�ж��ʧ��", eOutputType.Error);
                        return;
                    }
                }
                else
                {
                    rTxtOutputer.Output("ж�������ʧ��", eOutputType.Error);
                    return;
                }
                #endregion

                #region ֹͣ����������Ƥ��
                if (beltSamplerDAO.SendPDUnLoadCmd(this.currentSampler.EquipmentCode, "ֹͣж��"))//ж����ɺ� ж��Ƥ��ֹͣ
                {
                    rTxtOutputer.Output("ж��Ƥ��ֹͣ����ͳɹ����ȴ�������ִ��", eOutputType.Normal);
                    int waitCount = 0;
                    eEquInfCmdResultCode equInfCmdResultCode;
                    do
                    {
                        Thread.Sleep(10000);
                        if (waitCount % 5 == 0) rTxtOutputer.Output("���ڵȴ����������ؽ��", eOutputType.Normal);

                        waitCount++;

                        // ��ȡж�������ִ�н��
                        equInfCmdResultCode = UnloadSamplerDAO.GetInstance().GetPDUnloadState();
                    }
                    while (equInfCmdResultCode == eEquInfCmdResultCode.Ĭ��);
                    if (equInfCmdResultCode == eEquInfCmdResultCode.�ɹ�)
                    {
                        rTxtOutputer.Output("���������أ�Ƥ��ֹͣ�ɹ�", eOutputType.Normal);
                    }
                    else if (equInfCmdResultCode == eEquInfCmdResultCode.ʧ��)
                    {
                        rTxtOutputer.Output("���������أ�ִ��ʧ��", eOutputType.Error);
                        return;
                    }
                }
                else
                {
                    rTxtOutputer.Output("ж��Ƥ�������ʧ��", eOutputType.Error);
                    return;
                }
                #endregion

                #region ���������״̬�����Ѿ�����ʾ���������ƻ�
                // ���������ϵͳ״̬
                if (rbtnToMaker.Checked && this.currentEquInfSampleBarrels != null && this.currentEquInfSampleBarrels[0].BarrelType == "��жʽ")
                {
                    string makerSystemStatus = commonDAO.GetSignalDataValue(this.makerMachineCode, eSignalDataName.�豸״̬.ToString());
                    if (makerSystemStatus == eEquInfSamplerSystemStatus.��������.ToString())
                    {
                        if (MessageBoxEx.Show("ж���ɹ�����⵽�������Ѿ��������̿�ʼ������", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                        {
                            if (SendMakePlan(this.CurrentRCSampling.SampleCode, this.CurrentRCSampling.InFactoryBatchId))
                                rTxtOutputer.Output("��������ͳɹ�", eOutputType.Normal);
                            else
                                rTxtOutputer.Output("���������ʧ��", eOutputType.Error);
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
        /// ���Ľ���ؼ��Ŀ�������
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
        /// ���������ƻ�
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
                MessageBoxEx.Show("��ѡ��ж����¼", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            CmcsRCSampleBarrel rCSampleBarrel = commonDAO.SelfDber.Entity<CmcsRCSampleBarrel>(" where SampleCode='" + beltSampleUnloadCmd.SampleCode + "'");
            if (rCSampleBarrel == null)
            {
                MessageBoxEx.Show("δ�ҵ���������ϸ��¼", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBoxEx.Show("ȷ��Ҫ�����������", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk) == DialogResult.Yes)
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
                // ѡ��������ͬ����������޼�¼
                CheckedSameBarrelRow(sender as SuperGridControl, equInfSampleBarrel);
                // ���ز�����
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

            // ȡ�������е�ѡ��״̬
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
            // ȡ���༭
            e.Cancel = true;
        }

        private void superGridControl2_BeginEdit(object sender, GridEditEventArgs e)
        {
            // ȡ���༭
            e.Cancel = true;
        }

        private void superGridControl3_BeginEdit(object sender, GridEditEventArgs e)
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
            if (e.GridCell.GridColumn.DataPropertyName == "SampleCode")
            {
                InfEquInfSampleBarrel equInfSampleBarrel = e.GridCell.GridRow.DataItem as InfEquInfSampleBarrel;
                if (equInfSampleBarrel != null && !string.IsNullOrEmpty(equInfSampleBarrel.SampleCode)) e.Style.Background.Color1 = this.dicCellColors[equInfSampleBarrel.SampleCode];
            }
        }
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
    }
}