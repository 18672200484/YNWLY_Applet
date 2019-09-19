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
        /// 当前选中的采样机编码
        /// </summary>
        string currentSampleMachineCode;
        /// <summary>
        /// 当前选中的采样码
        /// </summary>
        string currentSampleCode;
        /// <summary>
        /// 当前卸样命令id
        /// </summary>
        string currentUnloadSampleId;
        /// <summary>
        /// 返回消息
        /// </summary>
        string currentMessage;

        /// <summary>
        /// 当前选择的采样单
        /// </summary>
        public CmcsRCSampling currentCmcsRCSampling;
        /// <summary>
        /// 当前选择的样罐信息
        /// </summary>
        public List<CmcsEquInfSampleBarrel> currentEquInfSampleBarrels = new List<CmcsEquInfSampleBarrel>();

        /// <summary>
        /// 采样机编码 默认#1采样机
        /// </summary>
        string[] sampleMachineCodes = new string[] { GlobalVars.MachineCode_NCGM_HCPDCYJ_1 };

        /// <summary>
        /// 制样机编码 默认#1全自动制样机
        /// </summary>
        string[] makeMachineCodes = new string[] { GlobalVars.MachineCode_NCGM_QZDZYJ_1 };

        Color[] CellColors = new Color[] { Color.Aqua, Color.Yellow, Color.LawnGreen, Color.HotPink, Color.Fuchsia, Color.LightBlue, Color.Lime, Color.Pink };
        /// <summary>
        /// 分配的颜色
        /// </summary>
        Dictionary<string, Color> dicCellColors = new Dictionary<string, Color>();

        RTxtOutputer rTxtOutputer;

        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体初始化
        /// </summary>
        private void FormInit()
        {
            superGridControl1.PrimaryGrid.AutoGenerateColumns = false;
            superGridControl2.PrimaryGrid.AutoGenerateColumns = false;
            superGridControl3.PrimaryGrid.AutoGenerateColumns = false;

            rTxtOutputer = new RTxtOutputer(rTxTMessageInfo);

            // 采样机设备编码，跟卸样程序一一对应
            sampleMachineCodes = CommonDAO.GetInstance().GetAppletConfigString("采样机设备编码").Split('|');
            makeMachineCodes = CommonDAO.GetInstance().GetAppletConfigString("制样机设备编码").Split('|');
        }

        private void FrmTrainBeltSamplerLoad_Load(object sender, EventArgs e)
        {
            FormInit();

            CreateSamplerButton();
            CreateEquStatus();
            // 触发第一个按钮
            if (lypanSamplerButton.Controls.Count > 0) (lypanSamplerButton.Controls[0] as RadioButton).Checked = true;
            //默认#1全自动制样机
            if (makeMachineCodes.Length > 0) lblCurrMakeMachine.Text = makeMachineCodes[0];
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!String.IsNullOrEmpty(this.currentUnloadSampleId))
            {
                if (MessageBoxEx.Show("卸样流程未完成，是否退出系统", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
                    e.Cancel = false;
                else
                    e.Cancel = true;
            }
            else
                e.Cancel = false;
        }

        /// <summary>
        /// 生成采样机选项
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
        /// 创建火车皮带采样机、全自动制样机状态
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
                MessageBoxEx.Show("火车皮带采样机或制样机参数未设置！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// 更新火车皮带采样机状态
        /// </summary>
        private void RefreshEquStatus()
        {
            foreach (UCtrlSignalLight uCtrlSignalLight in flpanEquState.Controls.OfType<UCtrlSignalLight>())
            {
                if (uCtrlSignalLight.Tag == null) continue;

                string machineCode = uCtrlSignalLight.Tag.ToString();
                if (string.IsNullOrEmpty(machineCode)) continue;

                string systemStatus = CommonDAO.GetInstance().GetSignalDataValue(machineCode, GlobalVars.EquSystemStatueName);
                if (systemStatus == eEquInfAutoMakerSystemStatus.就绪待机.ToString())
                    uCtrlSignalLight.LightColor = EquipmentStatusColors.BeReady;
                else if (systemStatus == eEquInfAutoMakerSystemStatus.正在运行.ToString())
                    uCtrlSignalLight.LightColor = EquipmentStatusColors.Working;
                else if (systemStatus == eEquInfAutoMakerSystemStatus.发生故障.ToString())
                    uCtrlSignalLight.LightColor = EquipmentStatusColors.Breakdown;
            }
        }

        /// <summary>
        /// 设置ToolTip提示
        /// </summary>
        private void SetSystemStatusToolTip(Control control)
        {
            this.toolTip1.SetToolTip(control, "<绿色> 就绪待机\r\n<红色> 正在运行\r\n<黄色> 发送故障");
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
        /// 绑定集样罐信息
        /// </summary>
        /// <param name="superGridControl"></param>
        /// <param name="machineCode">设备编码</param>
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
        /// 检查采样罐信息是否已更新
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
        /// 选中采样码一致的记录
        /// </summary>
        /// <param name="superGridControl"></param>
        /// <param name="sampleCode">采样码</param>
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
        /// 发送卸样命令
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSendLoadCmd_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.currentSampleCode))
            {
                MessageBoxEx.Show("请选择集样罐再发送", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (!CheckBeltSampleBarrel())
            {
                MessageBoxEx.Show("集样罐已更新，请刷新样罐信息", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                BindBeltSampleBarrel(superGridControl1, this.currentSampleMachineCode);
                return;
            }

            if (rbtnToMaker.Checked)
            {//卸样到制样机
                if (currentCmcsRCSampling == null)
                {
                    MessageBoxEx.Show("请选择采样单后再发送", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                // 制样机系统状态
                string makerSystemStatus = CommonDAO.GetInstance().GetSignalDataValue(GlobalVars.MachineCode_NCGM_QZDZYJ_1, GlobalVars.EquSystemStatueName);
                if (rbtnToMaker.Checked && makerSystemStatus != eEquInfSamplerSystemStatus.就绪待机.ToString())
                {
                    MessageBoxEx.Show("制样机系统未就绪", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                string message = string.Empty;
                if (BeltSamplerDAO.GetInstance().CanSendSampleLoadCmd(this.currentSampleMachineCode, out message))
                {
                    if (BeltSamplerDAO.GetInstance().SendSampleUnloadCmd(this.currentSampleMachineCode, this.currentSampleCode, out this.currentUnloadSampleId, (eEquInfSamplerUnloadType)Convert.ToInt16(flpanUnloadType.Controls.OfType<RadioButton>().First(a => a.Checked).Tag)))
                    {
                        //保存采样单明细记录【样罐信息】
                        BeltSamplerDAO.GetInstance().SaveSampleBarrel(currentEquInfSampleBarrels, currentCmcsRCSampling);

                        MessageBoxEx.Show("命令发送成功，等待采样机系统执行", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        rTxtOutputer.Output("等待采样机系统执行卸样操作！", eOutputType.Normal);

                        timer1_Tick(null, null);
                        timer2.Enabled = true;
                        btnSendLoadCmd.Enabled = false;
                        btnSendMakeCmd.Enabled = false;
                    }
                }
                else
                {
                    MessageBoxEx.Show(message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            else
            {//卸样到旁路
                string message = string.Empty;
                if (BeltSamplerDAO.GetInstance().CanSendSampleLoadCmd(this.currentSampleMachineCode, out message))
                {
                    if (BeltSamplerDAO.GetInstance().SendSampleUnloadCmd(this.currentSampleMachineCode, this.currentSampleCode, out this.currentUnloadSampleId, (eEquInfSamplerUnloadType)Convert.ToInt16(flpanUnloadType.Controls.OfType<RadioButton>().First(a => a.Checked).Tag)))
                    {
                        MessageBoxEx.Show("命令发送成功，等待采样机系统执行", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        rTxtOutputer.Output("等待采样机系统执行卸样操作！", eOutputType.Normal);

                        timer1_Tick(null, null);
                    }
                }
                else
                {
                    MessageBoxEx.Show(message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        /// <summary>
        /// 发送制样计划
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
                MessageBoxEx.Show("请先选择卸样记录", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            CmcsRCSampleBarrel RCSampleBarrel = CommonDAO.GetInstance().GetDber().Entity<CmcsRCSampleBarrel>(" where BarrelCode='" + entity.SampleCode + "'");
            if (RCSampleBarrel == null)
            {
                MessageBoxEx.Show("未找到采样单明细记录", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            SendMakePlan(RCSampleBarrel.SamplingId, RCSampleBarrel.InFactoryBatchId);
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
        /// 加载当前卸样任务
        /// </summary>
        /// <param name="machineCode">设备编码</param>
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
                lblCurrResultCode.Text = "暂无";
                lblCurrSampleCode.Text = "暂无";
                lblCurrSendTime.Text = "暂无";
                lblCurrUnloadType.Text = "暂无";
            }

            if (lblCurrResultCode.Text == eEquInfCmdResultCode.成功.ToString())
                lblCurrResultCode.ForeColor = Color.Green;
            else if (lblCurrResultCode.Text == eEquInfCmdResultCode.失败.ToString())
                lblCurrResultCode.ForeColor = Color.Red;
            else
                lblCurrResultCode.ForeColor = Color.Black;
        }

        /// <summary>
        /// 加载最近的卸样记录
        /// </summary>
        private void LoadLatestSampleUnloadCmd()
        {
            List<CmcsBeltSampleUnloadCmd> beltSampleUnloadCmds = CommonDAO.GetInstance().GetDber().TopEntities<CmcsBeltSampleUnloadCmd>(5, " where MachineCode='" + this.currentSampleMachineCode + "' order by createdate desc");

            superGridControl3.PrimaryGrid.DataSource = beltSampleUnloadCmds;
        }

        /// <summary>
        /// 开始卸样操作
        /// </summary>
        /// <param name="UnloadSampleId">当前卸样命令id</param>
        private bool StartUnloadSample(string UnloadSampleId)
        {
            if (UnloadSampleId != string.Empty)
            {
                if (UnloadSamplerDAO.GetInstance().GetUnloadSamplerState(UnloadSampleId) == eEquInfCmdResultCode.成功)
                {
                    rTxtOutputer.Output("卸样完成！", eOutputType.Normal);
                    if (MessageBoxEx.Show("卸样完成，是否发送制样计划", "系统提示", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk, MessageBoxDefaultButton.Button1) == DialogResult.Yes)
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
                else if (UnloadSamplerDAO.GetInstance().GetUnloadSamplerState(UnloadSampleId) == eEquInfCmdResultCode.失败)
                {
                    rTxtOutputer.Output("卸样失败！", eOutputType.Error);
                    currentUnloadSampleId = string.Empty;
                    btnSendLoadCmd.Enabled = true;
                    btnSendMakeCmd.Enabled = true;
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// 发送制样计划
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
                    FuelKindName = "褐煤",
                    Mt = "湿煤",
                    MakeType = "在线制样",
                    CoalSize = "小粒度"
                };
                MakerDAO.GetInstance().SaveMakerPlan(makerPlan, out currentMessage);
                rTxtOutputer.Output(currentMessage, eOutputType.Normal);
                return true;
            }
            else
                rTxtOutputer.Output("制样计划发送失败：未找到制样主记录信息！", eOutputType.Error);
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
