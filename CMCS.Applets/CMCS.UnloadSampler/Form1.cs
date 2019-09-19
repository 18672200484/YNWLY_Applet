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
using CMCS.Monitor.DAO;
using CMCS.UnloadSampler.Frms;
//
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Metro;
using DevComponents.DotNetBar.SuperGrid;
using CMCS.Common.Utilities;
using CMCS.UnloadSampler.Enums;
using CMCS.UnloadSampler.Utilities;
using CMCS.UnloadSampler.DAO;
using CMCS.UnloadSampler.UserControls;

namespace CMCS.UnloadSampler
{
    public partial class Form1 : MetroForm
    {
        /// <summary>
        /// ��ǰѡ�еĲ���������
        /// </summary>
        string currentSampleMachineCode;
        /// <summary>
        /// ��ǰѡ�еĲ�����
        /// </summary>
        string currentSampleCode;
        /// <summary>
        /// ��ǰж������id
        /// </summary>
        string currentUnloadSampleId;
        /// <summary>
        /// ������Ϣ
        /// </summary>
        string currentMessage;

        /// <summary>
        /// ��ǰѡ��Ĳ�����
        /// </summary>
        public CmcsRCSampling currentCmcsRCSampling;
        /// <summary>
        /// ��ǰѡ���������Ϣ
        /// </summary>
        public List<CmcsEquInfSampleBarrel> currentEquInfSampleBarrels = new List<CmcsEquInfSampleBarrel>();

        /// <summary>
        /// ���������� Ĭ��#1������
        /// </summary>
        string[] sampleMachineCodes = new string[] { GlobalVars.MachineCode_NCGM_HCPDCYJ_1 };

        /// <summary>
        /// ���������� Ĭ��#1ȫ�Զ�������
        /// </summary>
        string[] makeMachineCodes = new string[] { GlobalVars.MachineCode_NCGM_QZDZYJ_1 };

        Color[] CellColors = new Color[] { Color.Aqua, Color.Yellow, Color.LawnGreen, Color.HotPink, Color.Fuchsia, Color.LightBlue, Color.Lime, Color.Pink };
        /// <summary>
        /// �������ɫ
        /// </summary>
        Dictionary<string, Color> dicCellColors = new Dictionary<string, Color>();

        RTxtOutputer rTxtOutputer;

        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// �����ʼ��
        /// </summary>
        private void FormInit()
        {
            superGridControl1.PrimaryGrid.AutoGenerateColumns = false;
            superGridControl2.PrimaryGrid.AutoGenerateColumns = false;
            superGridControl3.PrimaryGrid.AutoGenerateColumns = false;

            rTxtOutputer = new RTxtOutputer(rTxTMessageInfo);

            // �������豸���룬��ж������һһ��Ӧ
            sampleMachineCodes = CommonDAO.GetInstance().GetAppletConfigString("�������豸����").Split('|');
            makeMachineCodes = CommonDAO.GetInstance().GetAppletConfigString("�������豸����").Split('|');
        }

        private void FrmTrainBeltSamplerLoad_Load(object sender, EventArgs e)
        {
            FormInit();

            CreateSamplerButton();
            CreateEquStatus();
            // ������һ����ť
            if (lypanSamplerButton.Controls.Count > 0) (lypanSamplerButton.Controls[0] as RadioButton).Checked = true;
            //Ĭ��#1ȫ�Զ�������
            if (makeMachineCodes.Length > 0) lblCurrMakeMachine.Text = makeMachineCodes[0];
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!String.IsNullOrEmpty(this.currentUnloadSampleId))
            {
                if (MessageBoxEx.Show("ж������δ��ɣ��Ƿ��˳�ϵͳ", "ϵͳ��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    e.Cancel = false;
                else
                    e.Cancel = true;
            }
            else
                e.Cancel = false;
        }

        /// <summary>
        /// ���ɲ�����ѡ��
        /// </summary>
        private void CreateSamplerButton()
        {
            foreach (string machineCode in sampleMachineCodes)
            {
                RadioButton rbtnSampler = new RadioButton();
                rbtnSampler.Text = CommonDAO.GetInstance().GetMachineNameByCode(machineCode);
                rbtnSampler.Tag = machineCode;
                rbtnSampler.AutoSize = true;
                rbtnSampler.Padding = new System.Windows.Forms.Padding(8);
                rbtnSampler.CheckedChanged += new EventHandler(rbtnSampler_CheckedChanged);

                lypanSamplerButton.Controls.Add(rbtnSampler);
            }
        }

        /// <summary>
        /// ������Ƥ����������ȫ�Զ�������״̬
        /// </summary>
        private void CreateEquStatus()
        {
            flpanEquState.SuspendLayout();

            foreach (string cMEquipmentCode in sampleMachineCodes)
            {
                UCtrlSignalLight uCtrlSignalLight = new UCtrlSignalLight()
                {
                    Anchor = AnchorStyles.Left,
                    Tag = cMEquipmentCode,
                    Padding = new System.Windows.Forms.Padding(10, 0, 0, 0)
                };
                SetSystemStatusToolTip(uCtrlSignalLight);

                flpanEquState.Controls.Add(uCtrlSignalLight);

                LabelX lblMachineName = new LabelX()
                {
                    Text = cMEquipmentCode,
                    Tag = cMEquipmentCode,
                    AutoSize = true,
                    Anchor = AnchorStyles.Left,
                    Font = new Font("Segoe UI", 14.25f, FontStyle.Bold)
                };

                flpanEquState.Controls.Add(lblMachineName);
            }

            foreach (string cMEquipmentCode in makeMachineCodes)
            {
                UCtrlSignalLight uCtrlSignalLight = new UCtrlSignalLight()
                {
                    Anchor = AnchorStyles.Left,
                    Tag = cMEquipmentCode,
                    Padding = new System.Windows.Forms.Padding(10, 0, 0, 0)
                };
                SetSystemStatusToolTip(uCtrlSignalLight);

                flpanEquState.Controls.Add(uCtrlSignalLight);

                LabelX lblMachineName = new LabelX()
                {
                    Text = CommonDAO.GetInstance().GetMachineNameByCode(cMEquipmentCode),
                    Tag = cMEquipmentCode,
                    AutoSize = true,
                    Anchor = AnchorStyles.Left,
                    Font = new Font("Segoe UI", 14.25f, FontStyle.Bold)
                };

                flpanEquState.Controls.Add(lblMachineName);
            }

            flpanEquState.ResumeLayout();

            if (this.flpanEquState.Controls.Count == 0)
                MessageBoxEx.Show("��Ƥ��������������������δ���ã�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// ���»�Ƥ��������״̬
        /// </summary>
        private void RefreshEquStatus()
        {
            foreach (UCtrlSignalLight uCtrlSignalLight in flpanEquState.Controls.OfType<UCtrlSignalLight>())
            {
                if (uCtrlSignalLight.Tag == null) continue;

                string machineCode = uCtrlSignalLight.Tag.ToString();
                if (string.IsNullOrEmpty(machineCode)) continue;

                string systemStatus = CommonDAO.GetInstance().GetSignalDataValue(machineCode, GlobalVars.EquSystemStatueName);
                if (systemStatus == eEquInfAutoMakerSystemStatus.��������.ToString())
                    uCtrlSignalLight.LightColor = EquipmentStatusColors.BeReady;
                else if (systemStatus == eEquInfAutoMakerSystemStatus.��������.ToString())
                    uCtrlSignalLight.LightColor = EquipmentStatusColors.Working;
                else if (systemStatus == eEquInfAutoMakerSystemStatus.��������.ToString())
                    uCtrlSignalLight.LightColor = EquipmentStatusColors.Breakdown;
            }
        }

        /// <summary>
        /// ����ToolTip��ʾ
        /// </summary>
        private void SetSystemStatusToolTip(Control control)
        {
            this.toolTip1.SetToolTip(control, "<��ɫ> ��������\r\n<��ɫ> ��������\r\n<��ɫ> ���͹���");
        }

        void rbtnSampler_CheckedChanged(object sender, EventArgs e)
        {
            this.currentSampleCode = string.Empty;

            RadioButton rbtnSampler = sender as RadioButton;
            this.currentSampleMachineCode = rbtnSampler.Tag.ToString();
            BindBeltSampleBarrel(superGridControl1, currentSampleMachineCode);
            LoadLatestSampleUnloadCmd();
        }

        /// <summary>
        /// �󶨼�������Ϣ
        /// </summary>
        /// <param name="superGridControl"></param>
        /// <param name="machineCode">�豸����</param>
        private void BindBeltSampleBarrel(SuperGridControl superGridControl, string machineCode)
        {
            List<CmcsEquInfSampleBarrel> list = MonitorDAO.GetInstance().GetEquInfSampleBarrels(machineCode);
            superGridControl.PrimaryGrid.DataSource = list;

            dicCellColors.Clear();

            foreach (CmcsEquInfSampleBarrel cmcsequinfsamplebarrel in list)
            {
                if (string.IsNullOrEmpty(cmcsequinfsamplebarrel.SampleCode)) continue;
                string key = cmcsequinfsamplebarrel.SampleCode;

                if (!dicCellColors.ContainsKey(key) && dicCellColors.Count < CellColors.Length) dicCellColors.Add(key, CellColors[dicCellColors.Count]);
            }
        }

        /// <summary>
        /// ����������Ϣ�Ƿ��Ѹ���
        /// </summary>
        private bool CheckBeltSampleBarrel()
        {
            foreach (GridRow gridRow in superGridControl1.PrimaryGrid.Rows)
            {
                if (gridRow.Checked)
                {
                    CmcsEquInfSampleBarrel cmcsEquInfSampleBarrel = gridRow.DataItem as CmcsEquInfSampleBarrel;
                    CmcsEquInfSampleBarrel cmcsEquInfSampleBarrelNew = CommonDAO.GetInstance().GetDber().Get<CmcsEquInfSampleBarrel>(cmcsEquInfSampleBarrel.Id);
                    if (cmcsEquInfSampleBarrelNew != null)
                    {
                        if (String.IsNullOrEmpty(cmcsEquInfSampleBarrelNew.SampleCode))
                            return false;
                    }
                    else
                        return false;
                }
            }
            return true;
        }

        private void superGridControl1_CellClick(object sender, GridCellClickEventArgs e)
        {
            string sampleCode = (e.GridCell.GridRow.DataItem as CmcsEquInfSampleBarrel).SampleCode;
            string batchId = (e.GridCell.GridRow.DataItem as CmcsEquInfSampleBarrel).InFactoryBatchId;
            CheckedSameBarrelRow(sender as SuperGridControl, sampleCode);
            CheckedSampleInfo(superGridControl2, batchId);
            this.currentCmcsRCSampling = null;
            lblSampleCode.Text = "####";
            lblSampleType.Text = "####";
        }

        private void superGridControl2_CellClick(object sender, GridCellClickEventArgs e)
        {
            currentCmcsRCSampling = e.GridCell.GridRow.DataItem as CmcsRCSampling;
            string sampleCode = currentCmcsRCSampling.SampleCode;
            lblSampleCode.Text = sampleCode;
            lblSampleType.Text = currentCmcsRCSampling.SamplingType;

            foreach (GridRow gridRow in superGridControl2.PrimaryGrid.Rows)
            {
                CmcsRCSampling cmcsRCSampling = gridRow.DataItem as CmcsRCSampling;
                gridRow.Checked = (cmcsRCSampling != null && !string.IsNullOrWhiteSpace(cmcsRCSampling.SampleCode) && !string.IsNullOrWhiteSpace(sampleCode) && cmcsRCSampling.SampleCode == sampleCode);
            }
        }

        private void superGridControl3_CellClick(object sender, GridCellClickEventArgs e)
        {
            CmcsBeltSampleUnloadCmd sampleUnloadCmd = e.GridCell.GridRow.DataItem as CmcsBeltSampleUnloadCmd;

            foreach (GridRow gridRow in superGridControl3.PrimaryGrid.Rows)
            {
                CmcsBeltSampleUnloadCmd cmcsSampleUnloadCmd = gridRow.DataItem as CmcsBeltSampleUnloadCmd;
                gridRow.Checked = (cmcsSampleUnloadCmd != null && sampleUnloadCmd.Id == cmcsSampleUnloadCmd.Id);
            }
        }

        /// <summary>
        /// ѡ�в�����һ�µļ�¼
        /// </summary>
        /// <param name="superGridControl"></param>
        /// <param name="sampleCode">������</param>
        private void CheckedSameBarrelRow(SuperGridControl superGridControl, string sampleCode)
        {
            currentEquInfSampleBarrels.Clear();
            this.currentSampleCode = sampleCode;

            foreach (GridRow gridRow in superGridControl.PrimaryGrid.Rows)
            {
                CmcsEquInfSampleBarrel cmcsEquInfSampleBarrel = gridRow.DataItem as CmcsEquInfSampleBarrel;
                gridRow.Checked = (cmcsEquInfSampleBarrel != null && !string.IsNullOrWhiteSpace(cmcsEquInfSampleBarrel.SampleCode) && !string.IsNullOrWhiteSpace(sampleCode) && cmcsEquInfSampleBarrel.SampleCode == sampleCode);
                if (gridRow.Checked)
                    currentEquInfSampleBarrels.Add(cmcsEquInfSampleBarrel);
            }
        }

        private void CheckedSampleInfo(SuperGridControl superGridControl, string batchId)
        {
            List<CmcsRCSampling> list = MonitorDAO.GetInstance().GetSamplings(batchId);
            superGridControl.PrimaryGrid.DataSource = list;
        }

        /// <summary>
        /// ����ж������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSendLoadCmd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.currentSampleCode))
            {
                MessageBoxEx.Show("��ѡ�������ٷ���", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!CheckBeltSampleBarrel())
            {
                MessageBoxEx.Show("�������Ѹ��£���ˢ��������Ϣ", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                BindBeltSampleBarrel(superGridControl1, this.currentSampleMachineCode);
                return;
            }

            if (rbtnToMaker.Checked)
            {//ж����������
                if (currentCmcsRCSampling == null)
                {
                    MessageBoxEx.Show("��ѡ����������ٷ���", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // ������ϵͳ״̬
                string makerSystemStatus = CommonDAO.GetInstance().GetSignalDataValue(GlobalVars.MachineCode_NCGM_QZDZYJ_1, GlobalVars.EquSystemStatueName);
                if (rbtnToMaker.Checked && makerSystemStatus != eEquInfSamplerSystemStatus.��������.ToString())
                {
                    MessageBoxEx.Show("������ϵͳδ����", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string message = string.Empty;
                if (BeltSamplerDAO.GetInstance().CanSendSampleLoadCmd(this.currentSampleMachineCode, out message))
                {
                    if (BeltSamplerDAO.GetInstance().SendSampleUnloadCmd(this.currentSampleMachineCode, this.currentSampleCode, out this.currentUnloadSampleId, (eEquInfSamplerUnloadType)Convert.ToInt16(flpanUnloadType.Controls.OfType<RadioButton>().First(a => a.Checked).Tag)))
                    {
                        //�����������ϸ��¼��������Ϣ��
                        BeltSamplerDAO.GetInstance().SaveSampleBarrel(currentEquInfSampleBarrels, currentCmcsRCSampling);

                        MessageBoxEx.Show("����ͳɹ����ȴ�������ϵͳִ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        rTxtOutputer.Output("�ȴ�������ϵͳִ��ж��������", eOutputType.Normal);

                        timer1_Tick(null, null);
                        timer2.Enabled = true;
                        btnSendLoadCmd.Enabled = false;
                        btnSendMakeCmd.Enabled = false;
                    }
                }
                else
                {
                    MessageBoxEx.Show(message, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {//ж������·
                string message = string.Empty;
                if (BeltSamplerDAO.GetInstance().CanSendSampleLoadCmd(this.currentSampleMachineCode, out message))
                {
                    if (BeltSamplerDAO.GetInstance().SendSampleUnloadCmd(this.currentSampleMachineCode, this.currentSampleCode, out this.currentUnloadSampleId, (eEquInfSamplerUnloadType)Convert.ToInt16(flpanUnloadType.Controls.OfType<RadioButton>().First(a => a.Checked).Tag)))
                    {
                        MessageBoxEx.Show("����ͳɹ����ȴ�������ϵͳִ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        rTxtOutputer.Output("�ȴ�������ϵͳִ��ж��������", eOutputType.Normal);

                        timer1_Tick(null, null);
                    }
                }
                else
                {
                    MessageBoxEx.Show(message, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        /// <summary>
        /// ���������ƻ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSendMakeCmd_Click(object sender, EventArgs e)
        {
            CmcsBeltSampleUnloadCmd entity = null;
            foreach (GridRow gridRow in superGridControl3.PrimaryGrid.Rows)
            {
                if (gridRow.Checked)
                    entity = gridRow.DataItem as CmcsBeltSampleUnloadCmd;
            }
            if (entity == null)
            {
                MessageBoxEx.Show("����ѡ��ж����¼", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            CmcsRCSampleBarrel RCSampleBarrel = CommonDAO.GetInstance().GetDber().Entity<CmcsRCSampleBarrel>(" where BarrelCode='" + entity.SampleCode + "'");
            if (RCSampleBarrel == null)
            {
                MessageBoxEx.Show("δ�ҵ���������ϸ��¼", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            SendMakePlan(RCSampleBarrel.SamplingId, RCSampleBarrel.InFactoryBatchId);
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
        /// ���ص�ǰж������
        /// </summary>
        /// <param name="machineCode">�豸����</param>
        private void LoadCurrSampleUnloadCmd()
        {
            if (string.IsNullOrEmpty(currentUnloadSampleId)) return;

            CmcsBeltSampleUnloadCmd beltSampleUnloadCmd = CommonDAO.GetInstance().GetDber().Get<CmcsBeltSampleUnloadCmd>(currentUnloadSampleId);

            lblCurrSamplerName.Text = CommonDAO.GetInstance().GetMachineNameByCode(beltSampleUnloadCmd.MachineCode);

            if (beltSampleUnloadCmd != null)
            {
                lblCurrResultCode.Text = beltSampleUnloadCmd.ResultCode;
                lblCurrSampleCode.Text = beltSampleUnloadCmd.SampleCode;
                lblCurrSendTime.Text = beltSampleUnloadCmd.CreateDate.ToString("yyyy-MM-dd HH:mm:ss");
                lblCurrUnloadType.Text = beltSampleUnloadCmd.UnloadType;
            }
            else
            {
                lblCurrResultCode.Text = "����";
                lblCurrSampleCode.Text = "����";
                lblCurrSendTime.Text = "����";
                lblCurrUnloadType.Text = "����";
            }

            if (lblCurrResultCode.Text == eEquInfCmdResultCode.�ɹ�.ToString())
                lblCurrResultCode.ForeColor = Color.Green;
            else if (lblCurrResultCode.Text == eEquInfCmdResultCode.ʧ��.ToString())
                lblCurrResultCode.ForeColor = Color.Red;
            else
                lblCurrResultCode.ForeColor = Color.Black;
        }

        /// <summary>
        /// ���������ж����¼
        /// </summary>
        private void LoadLatestSampleUnloadCmd()
        {
            List<CmcsBeltSampleUnloadCmd> beltSampleUnloadCmds = CommonDAO.GetInstance().GetDber().TopEntities<CmcsBeltSampleUnloadCmd>(5, " where MachineCode='" + this.currentSampleMachineCode + "' order by createdate desc");

            superGridControl3.PrimaryGrid.DataSource = beltSampleUnloadCmds;
        }

        /// <summary>
        /// ��ʼж������
        /// </summary>
        /// <param name="UnloadSampleId">��ǰж������id</param>
        private bool StartUnloadSample(string UnloadSampleId)
        {
            if (UnloadSampleId != string.Empty)
            {
                if (UnloadSamplerDAO.GetInstance().GetUnloadSamplerState(UnloadSampleId) == eEquInfCmdResultCode.�ɹ�)
                {
                    rTxtOutputer.Output("ж����ɣ�", eOutputType.Normal);
                    if (MessageBoxEx.Show("ж����ɣ��Ƿ��������ƻ�", "ϵͳ��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    {
                        SendMakePlan(this.currentCmcsRCSampling.Id, this.currentCmcsRCSampling.InfactoryBatchId);

                        currentUnloadSampleId = string.Empty;
                        btnSendLoadCmd.Enabled = true;
                        btnSendMakeCmd.Enabled = true;
                        return false;
                    }
                    else
                    {
                        currentUnloadSampleId = string.Empty;
                        btnSendLoadCmd.Enabled = true;
                        btnSendMakeCmd.Enabled = true;
                        return false;
                    }
                }
                else if (UnloadSamplerDAO.GetInstance().GetUnloadSamplerState(UnloadSampleId) == eEquInfCmdResultCode.ʧ��)
                {
                    rTxtOutputer.Output("ж��ʧ�ܣ�", eOutputType.Error);
                    currentUnloadSampleId = string.Empty;
                    btnSendLoadCmd.Enabled = true;
                    btnSendMakeCmd.Enabled = true;
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// ���������ƻ�
        /// </summary>
        /// <returns></returns>
        private bool SendMakePlan(string RCSamplingId, string infactoryBatchId)
        {
            CmcsRCMake rcmake = MakerDAO.GetInstance().GetRCMakeBySampleId(RCSamplingId);
            if (rcmake != null)
            {
                CmcsMakerPlan makerPlan = new CmcsMakerPlan()
                {
                    InterfaceType = GlobalVars.InterfaceType_NCGM_QZDZYJ,
                    MachineCode = GlobalVars.MachineCode_NCGM_QZDZYJ_1,
                    InFactoryBatchId = infactoryBatchId,
                    MakeCode = rcmake.MakeCode,
                    FuelKindName = "��ú",
                    Mt = "ʪú",
                    MakeType = "��������",
                    CoalSize = "С����"
                };
                MakerDAO.GetInstance().SaveMakerPlan(makerPlan, out currentMessage);
                rTxtOutputer.Output(currentMessage, eOutputType.Normal);
                return true;
            }
            else
                rTxtOutputer.Output("�����ƻ�����ʧ�ܣ�δ�ҵ���������¼��Ϣ��", eOutputType.Error);
            return false;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            LoadCurrSampleUnloadCmd();
            RefreshEquStatus();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            timer2.Stop();
            if (!StartUnloadSample(this.currentUnloadSampleId))
                return;
            timer2.Start();
        }

        private void superGridControl1_GetCellStyle(object sender, GridGetCellStyleEventArgs e)
        {
            if (e.GridCell.GridColumn.DataPropertyName == "SampleCode")
            {
                CmcsEquInfSampleBarrel cmcsequinfsamplebarrel = e.GridCell.GridRow.DataItem as CmcsEquInfSampleBarrel;
                if (cmcsequinfsamplebarrel != null && !string.IsNullOrEmpty(cmcsequinfsamplebarrel.SampleCode)) e.Style.Background.Color1 = this.dicCellColors[cmcsequinfsamplebarrel.SampleCode];
            }
        }
    }
}
