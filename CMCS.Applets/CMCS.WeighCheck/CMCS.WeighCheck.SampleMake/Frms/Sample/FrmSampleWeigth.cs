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
using CMCS.Common.Entities.BaseInfo;
using CMCS.WeighCheck.SampleMake.Utilities;
using CMCS.WeighCheck.SampleMake.Core;

namespace CMCS.WeighCheck.SampleMake.Frms.Sample
{
    public partial class FrmSampleWeigth : MetroForm
    {
        public FrmSampleWeigth()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体唯一标识符
        /// </summary>
        public static string UniqueKey = "FrmSampleWeigth";

        #region Vars

        CommonDAO commonDAO = CommonDAO.GetInstance();
        CZYHandlerDAO cZYHandlerDAO = CZYHandlerDAO.GetInstance();
        CodePrinterSample _CodePrinter = null;
        eFlowFlag currentFlowFlag = eFlowFlag.选择采样单;
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

        private SampleInfo currentSampleInfo = null;
        /// <summary>
        /// 当前选择的采样单信息
        /// </summary>
        public SampleInfo CurrentSampleInfo
        {
            get { return currentSampleInfo; }
            set
            {
                currentSampleInfo = value;
                this.MachineRCSampleBarrelId.Clear();

                if (value != null)
                {
                    this.CurrentFlowFlag = eFlowFlag.选择采样单;
                    txtBatch.Text = value.Batch;
                    if (value.BatchType == eTransportType.仓储煤入场.ToString() || value.BatchType == eTransportType.原料煤入场.ToString() || value.BatchType == eTransportType.中转煤入场.ToString())
                    {
                        lab_SupplierName.Text = "供货单位";
                        txtSupplierName.Text = value.SupplierName;
                    }
                    else
                    {
                        lab_SupplierName.Text = "收货单位";
                        txtSupplierName.Text = value.FuelSupplierName;
                    }
                    txtSampleCode.Text = value.SampleCode;
                    txtSampleType.Text = value.SamplingType;
                    txtMineName.Text = value.MineName;
                    txtFuelKindName.Text = value.KindName;
                }
                else
                {
                    txtBatch.ResetText();
                    txtSupplierName.ResetText();
                    txtSampleCode.ResetText();
                    txtSampleType.ResetText();
                    txtMineName.ResetText();
                    txtFuelKindName.ResetText();
                }
            }
        }

        List<CmcsRCSampleBarrel> currentRCSampleBarrels = new List<CmcsRCSampleBarrel>();
        /// <summary>
        /// 当前登记的样桶记录
        /// </summary>
        public List<CmcsRCSampleBarrel> CurrentRCSampleBarrels
        {
            get { return currentRCSampleBarrels; }
            set
            {
                currentRCSampleBarrels = value;
                superGridControl1.PrimaryGrid.DataSource = value;
            }
        }

        List<string> machineRCSampleBarrelId = new List<string>();
        /// <summary>
        /// 在数据库中已存在的机器采样桶Id
        /// </summary>
        public List<string> MachineRCSampleBarrelId
        {
            get { return machineRCSampleBarrelId; }
            set { machineRCSampleBarrelId = value; }
        }

        #endregion

        /// <summary>
        /// 初始化
        /// </summary>
        public void InitFrom()
        {
            this.IsUseWeight = Convert.ToBoolean(commonDAO.GetAppletConfigInt32("启用称重"));

            this._CodePrinter = new CodePrinterSample(printDocument1);

            // 打印编码按钮
            GridButtonXEditControl btnPrintCode = superGridControl1.PrimaryGrid.Columns["gclmPrint"].EditControl as GridButtonXEditControl;
            btnPrintCode.ColorTable = eButtonColor.BlueWithBackground;
            btnPrintCode.Click += new EventHandler(btnPrintCode_Click);
            // 删除按钮
            GridButtonXEditControl btnWriteCode = superGridControl1.PrimaryGrid.Columns["gclmDel"].EditControl as GridButtonXEditControl;
            btnWriteCode.ColorTable = eButtonColor.BlueWithBackground;
            btnWriteCode.Click += new EventHandler(btnDel_Click);
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

		private void FrmSampleWeigth_Load(object sender, EventArgs e)
        {
            InitFrom();
            // 初始化设备
            InitHardware();
        }

        private void FrmSampleWeigth_FormClosing(object sender, FormClosingEventArgs e)
        {
            UnloadHardware();
        }

        #region 电子秤

        double currentWeight = 0;
        /// <summary>
        /// 电子秤当前重量
        /// </summary>
        public double CurrentWeight
        {
            get { return currentWeight; }
            set
            {
                currentWeight = value;
                this.lbl_CurrentWeight.Text = value.ToString() + "KG";
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
                lblweight.Visible = value;
                lbl_CurrentWeight.Visible = value;
                lblWber.Visible = value;
                slightWber.Visible = value;
            }
        }

        bool wbRunStatus = false;
        /// <summary>
        /// 电子秤连接状态
        /// </summary>
        public bool WbRunStatus
        {
            get { return wbRunStatus; }
            set { wbRunStatus = value; }
        }

        bool wbSteady = false;
        /// <summary>
        /// 电子秤稳定状态
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
        /// 电子秤最小称重 单位：KG
        /// </summary>
        public double WbMinWeight
        {
            get { return wbMinWeight; }
            set
            {
                wbMinWeight = value;
            }
        }

        double barrelWeight = 0;
        /// <summary>
        /// 人工采样桶重量 KG
        /// </summary>
        public double BarrelWeight
        {
            get { return barrelWeight; }
            set
            {
                barrelWeight = value;
            }
        }

        /// <summary>
        /// 电子秤状态变化
        /// </summary>
        /// <param name="status"></param>
        void wber_OnStatusChange(bool status)
        {
            // 接收设备状态 
            InvokeEx(() =>
            {
                this.WbRunStatus = status;

                slightWber.LightColor = (status ? Color.Green : Color.Red);
            });
        }

        /// <summary>
        /// 电子秤稳定状态变化
        /// </summary>
        /// <param name="status"></param>
        void wber_OnSteadyChange(bool steady)
        {
            // 接收设备状态 
            InvokeEx(() =>
            {
                this.WbSteady = steady;
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

                // 初始化-电子秤
                if (IsUseWeight)
                {
                    this.WbMinWeight = commonDAO.GetAppletConfigDouble("电子秤最小重量");
                    this.BarrelWeight = commonDAO.GetAppletConfigDouble("人工样桶重量");

                    Hardwarer.Wber.OnStatusChange += new WB.XiangPing.Balance.XiangPing_Balance.StatusChangeHandler(wber_OnStatusChange);
                    Hardwarer.Wber.OnSteadyChange += new WB.XiangPing.Balance.XiangPing_Balance.SteadyChangeEventHandler(wber_OnSteadyChange);
                    Hardwarer.Wber.OnWeightChange += new WB.XiangPing.Balance.XiangPing_Balance.WeightChangeEventHandler(wber_OnWeightChange);

                    success = Hardwarer.Wber.OpenCom(commonDAO.GetAppletConfigInt32("电子秤串口"), commonDAO.GetAppletConfigInt32("电子秤波特率"), commonDAO.GetAppletConfigInt32("电子秤数据位"), commonDAO.GetAppletConfigInt32("电子秤停止位"));
                    SelfVars.WeightOpen = success;
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
                Hardwarer.Wber.CloseCom();
            }
            catch { }
        }

        #endregion

        #region 业务

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();

            try
            {
                switch (this.CurrentFlowFlag)
                {
                    case eFlowFlag.选择采样单:
                        #region

                        if (this.CurrentSampleInfo == null)
                            break;
                        else
                        {
                            LoadSampleDetail();
                            this.CurrentFlowFlag = eFlowFlag.样桶称重;
                        }

                        #endregion
                        break;
                    case eFlowFlag.样桶称重:
                        #region
                        if (IsUseWeight)
                        {
                            if (Hardwarer.Wber.Status && Hardwarer.Wber.Weight > this.WbMinWeight && WbSteady)
                            {
                                this.CurrentFlowFlag = eFlowFlag.等待登记;
                            }
                        }
                        else
                        {
                            this.CurrentWeight = this.BarrelWeight;
                            this.CurrentFlowFlag = eFlowFlag.等待登记;
                        }

                        #endregion
                        break;
                    case eFlowFlag.等待登记:
                        #region

                        #endregion
                        break;
                    case eFlowFlag.完成登记:

                        this.CurrentRCSampleBarrels = currentRCSampleBarrels;
                        this.CurrentFlowFlag = eFlowFlag.样桶称重;

                        break;
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Timer1运行异常" + ex.Message, eOutputType.Error);
            }

            timer1.Start();
        }

        /// <summary>
        /// 加载样桶明细
        /// </summary>
        private void LoadSampleDetail()
        {
            List<CmcsRCSampleBarrel> cmcsrcsamplebarrel = commonDAO.SelfDber.Entities<CmcsRCSampleBarrel>("where SamplingId=:SamplingId order by CreateDate desc", new { SamplingId = this.CurrentSampleInfo.Id });
            //加载已绑定数据
            CurrentRCSampleBarrels = cmcsrcsamplebarrel;
            this.MachineRCSampleBarrelId = cmcsrcsamplebarrel.Select(a => a.Id).ToList();
        }

        /// <summary>
        /// 重置
        /// </summary>
        private void Restet()
        {
            this.CurrentSampleInfo = null;
            this.CurrentRCSampleBarrels = new List<CmcsRCSampleBarrel>();
            this.CurrentFlowFlag = eFlowFlag.选择采样单;
        }

        #endregion

        #region 操作

        /// <summary>
        /// 保存样桶登记信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveSampleBarrel_Click(object sender, EventArgs e)
        {
            if (this.CurrentRCSampleBarrels.Count == 0)
            {
                MessageBoxEx.Show("采样桶列表为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            foreach (CmcsRCSampleBarrel item in this.CurrentRCSampleBarrels)
            {
                if (this.MachineRCSampleBarrelId.Contains(item.Id))
                    // 在数据库已存在的样桶，只是没有关联采样单
                    cZYHandlerDAO.UpdateRCSampleBarrelSampleWeight(item.Id, item.SampleWeight);
                else
                {
                    cZYHandlerDAO.UpdateSampleBarrel(item.BarrelCode);
                    // 人工登记桶
                    cZYHandlerDAO.SaveRCSampleBarrel(item);
                }
            }

            MessageBoxEx.Show("样桶登记成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
            Restet();
        }

        /// <summary>
        /// 选择批次信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelSampleInfo_Click(object sender, EventArgs e)
        {
            FrmBatchSelect frm = new FrmBatchSelect();
            frm.Owner = this;
            frm.ShowDialog();
        }

        /// <summary>
        /// 新增采样桶
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (this.CurrentSampleInfo == null)
            {
                MessageBoxEx.Show("请先选择采样单", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (cZYHandlerDAO.SaveSampleDetail(this.CurrentSampleInfo.Id, this.CurrentWeight == 0 ? this.BarrelWeight : this.CurrentWeight))
            {
                commonDAO.SaveAppletLog(eAppletLogLevel.Info, "新增采样桶", string.Format("采样主码:{0};操作人:{1};", CurrentSampleInfo.SampleCode, SelfVars.LoginUser.UserName));
                LoadSampleDetail();
                this.CurrentFlowFlag = eFlowFlag.完成登记;
            }
        }

        /// <summary>
        /// 重置信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_Click(object sender, EventArgs e)
        {
            Restet();
        }

        /// <summary>
        /// 打印采样码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (this.CurrentSampleInfo != null)
            {
                commonDAO.SaveAppletLog(eAppletLogLevel.Warn, "删除采样主码", string.Format("采样主码:{0};操作人:{1};", CurrentSampleInfo.SampleCode, SelfVars.LoginUser.UserName));
                this._CodePrinter.Print(this.CurrentSampleInfo.SampleCode);
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

            CmcsRCSampleBarrel sampleBarrel = btn.EditorCell.GridRow.DataItem as CmcsRCSampleBarrel;
            if (sampleBarrel == null) return;

            if (!string.IsNullOrEmpty(sampleBarrel.SampSecondCode))
            {
                if (sampleBarrel.PrintCount > 0)
                {
                    if (MessageBoxEx.Show("该采样桶已打印，是否再次打印？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) != DialogResult.Yes)
                        return;
                }
                sampleBarrel.PrintCount++;
                sampleBarrel.PrintDate = DateTime.Now;
                sampleBarrel.PrintUser = SelfVars.LoginUser.UserName;
                commonDAO.SelfDber.Update(sampleBarrel);
                commonDAO.SaveAppletLog(eAppletLogLevel.Info, "打印采样桶明细", string.Format("采样次码:{0};采样主码:{1};操作人:{2};", sampleBarrel.SampSecondCode, sampleBarrel.SampleCode, SelfVars.LoginUser.UserName));
                _CodePrinter.Print(sampleBarrel.SampleCode);
                _CodePrinter.Print(sampleBarrel.SampSecondCode);
            }
        }

        /// <summary>
        /// 删除明细
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnDel_Click(object sender, EventArgs e)
        {
            GridButtonXEditControl btn = sender as GridButtonXEditControl;
            if (btn == null) return;

            CmcsRCSampleBarrel sampleBarrel = btn.EditorCell.GridRow.DataItem as CmcsRCSampleBarrel;
            if (sampleBarrel == null) return;
            if (MessageBoxEx.Show("确定删除该采样桶？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes)
            {
                commonDAO.SaveAppletLog(eAppletLogLevel.Warn, "删除采样桶明细", string.Format("采样次码:{0};采样主码:{1};操作人:{2};", sampleBarrel.SampSecondCode, sampleBarrel.SampleCode, SelfVars.LoginUser.UserName));
                if (Dbers.GetInstance().SelfDber.Delete<CmcsRCSampleBarrel>(sampleBarrel.Id) > 0)
                    LoadSampleDetail();
            }
        }
        #endregion

        #region DataGridView

        private void superGridControl1_GetRowHeaderText(object sender, DevComponents.DotNetBar.SuperGrid.GridGetRowHeaderTextEventArgs e)
        {
            e.Text = (e.GridRow.RowIndex + 1).ToString();
        }

        private void superGridControl1_BeginEdit(object sender, GridEditEventArgs e)
        {
            // 取消编辑
            e.Cancel = true;
        }

        private void superGridControl1_DataBindingComplete(object sender, GridDataBindingCompleteEventArgs e)
        {
            foreach (GridRow grid in e.GridPanel.Rows)
            {
                CmcsRCSampleBarrel entity = grid.DataItem as CmcsRCSampleBarrel;
                if (entity == null) return;
                if (entity.PrintCount > 0)
                    grid.CellStyles.Default.TextColor = Color.Green;
            }
        }
        #endregion

        #region 其他

        private void ShowMessage(string info, eOutputType outputType)
        {
            OutputRunInfo(rtxtOutputInfo, info, outputType);
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
