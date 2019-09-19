using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
//
using CMCS.Common;
using CMCS.Common.DAO;
using CMCS.WeighCheck.DAO;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using DevComponents.DotNetBar.Metro;
using CMCS.WeighCheck.SampleMake.Enums;
using CMCS.Common.Utilities;
using CMCS.Common.Entities.Fuel;
using CMCS.WeighCheck.SampleMake.Utilities;
using CMCS.WeighCheck.SampleMake.Core;

namespace CMCS.WeighCheck.SampleMake.Frms.Sample
{
    public partial class FrmSampleCheck : MetroForm
    {
        public FrmSampleCheck()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体唯一标识符
        /// </summary>
        public static string UniqueKey = "FrmSampleCheck";

        #region Vars

        CodePrinterSample _CodePrinter = null;
        CommonDAO commonDAO = CommonDAO.GetInstance();
        CZYHandlerDAO czyHandlerDAO = CZYHandlerDAO.GetInstance();

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

        CmcsRCSampleBarrel rCSampleBarrel = null;
        /// <summary>
        /// 当前扫描的样桶信息
        /// </summary>
        public CmcsRCSampleBarrel RCSampleBarrel
        {
            get { return rCSampleBarrel; }
            set
            {
                rCSampleBarrel = value;
            }
        }
        /// <summary>
        /// 与当前扫描的样桶关联的其他样桶记录
        /// </summary>
        List<CmcsRCSampleBarrel> brotherRCSampleBarrels = new List<CmcsRCSampleBarrel>();

        List<string> isScanedRCSampleBarrelId = new List<string>();
        /// <summary>
        /// 已完成验证的采样桶Id
        /// </summary>
        public List<string> IsScanedRCSampleBarrelId
        {
            get { return isScanedRCSampleBarrelId; }
            set { isScanedRCSampleBarrelId = value; }
        }

        string resMessage = string.Empty;

        #endregion

        public void InitFrom()
        {
            this.IsUseWeight = Convert.ToBoolean(commonDAO.GetAppletConfigInt32("启用称重"));

            this._CodePrinter = new CodePrinterSample(printDocument1);
        }

        private void FrmSampleCheck_Load(object sender, EventArgs e)
        {
            //初始化
            InitFrom();
            //初始化设备
            InitHardware();
        }

        private void FrmSampleCheck_FormClosing(object sender, FormClosingEventArgs e)
        {
            UnloadHardware();
        }

        /// <summary>
        /// 创建验证Button按钮
        /// </summary>
        /// <param name="count"></param>
        private void CreateButtonX(int count)
        {
            this.panSampleBarrels.Controls.Clear();
            this.panSampleBarrels.SuspendLayout();
            int PointX = 9;
            int PointY = 7;
            int HeightCount = 1;
            tableLayoutPanel1.RowStyles[3].Height = 80;
            for (int i = 0; i < count; i++)
            {
                ButtonX btn = new ButtonX();
                btn.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
                btn.BackColor = System.Drawing.Color.Gainsboro;
                btn.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
                btn.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                btn.Location = new System.Drawing.Point(PointX, PointY);
                btn.Margin = new System.Windows.Forms.Padding(6, 10, 6, 6);
                btn.Name = "CreatebuttonX" + (i + 1).ToString();
                btn.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor(10);
                btn.Size = new System.Drawing.Size(60, 60);
                btn.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
                btn.TabIndex = 175;
                btn.Tag = "Btn";
                btn.Text = (i + 1).ToString();
                btn.TextColor = System.Drawing.Color.Black;
                this.panSampleBarrels.Controls.Add(btn);
                PointX += 69;
                if (PointX >= this.panSampleBarrels.Width - 60)
                {
                    tableLayoutPanel1.RowStyles[3].Height = 80 + 65 * HeightCount;
                    HeightCount++;
                    PointY += 65;
                    PointX = 9;
                }
            }
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
        /// 电子秤状态变化
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

                // 初始化-电子秤
                if (IsUseWeight)
                {
                    this.WbMinWeight = commonDAO.GetAppletConfigDouble("电子秤最小重量");

                    // 电子秤
                    Hardwarer.Wber.OnStatusChange += new WB.XiangPing.Balance.XiangPing_Balance.StatusChangeHandler(Wber_OnStatusChange);
                    Hardwarer.Wber.OnSteadyChange += new WB.XiangPing.Balance.XiangPing_Balance.SteadyChangeEventHandler(Wber_OnSteadyChange);
                    Hardwarer.Wber.OnWeightChange += new WB.XiangPing.Balance.XiangPing_Balance.WeightChangeEventHandler(wber_OnWeightChange);

                    if (!SelfVars.WeightOpen)
                    {
                        success = Hardwarer.Wber.OpenCom(commonDAO.GetAppletConfigInt32("电子秤串口"), commonDAO.GetAppletConfigInt32("电子秤波特率"), commonDAO.GetAppletConfigInt32("电子秤数据位"), commonDAO.GetAppletConfigInt32("电子秤停止位"));
                        SelfVars.WeightOpen = success;
                    }
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
                    case eFlowFlag.等待扫码:

                        break;
                    case eFlowFlag.重量校验:
                        #region

                        if (Hardwarer.Wber.Status && Hardwarer.Wber.Weight > this.WbMinWeight && WbSteady)
                        {
                            this.CurrentFlowFlag = eFlowFlag.等待校验;
                        }

                        #endregion
                        break;
                    case eFlowFlag.等待校验:
                        #region

                        if (czyHandlerDAO.UpdateRCSampleBarrelHandWeight(this.rCSampleBarrel.Id, Hardwarer.Wber.Weight, SelfVars.LoginUser.UserName))
                        {
                            ShowMessage("校验成功，重量：" + Hardwarer.Wber.Weight.ToString() + "KG", eOutputType.Normal);

                            // 所有桶扫描完后进入下一流程 
                            if (this.IsScanedRCSampleBarrelId.Count == this.brotherRCSampleBarrels.Count)
                            {
                                ShowMessage("该环节样桶已全部校验完毕!", eOutputType.Normal);
                                txtInputSampleCode.ResetText();
                            }
                            else
                            {
                                txtInputSampleCode.ResetText();
                            }
                            this.CurrentFlowFlag = eFlowFlag.等待扫码;
                        }
                        else
                        {
                            ShowMessage("校验失败或者已校验，请联系管理员", eOutputType.Error);
                            this.CurrentFlowFlag = eFlowFlag.等待校验;
                        }

                        #endregion
                        break;
                }
            }
            catch (Exception ex)
            {
                ShowMessage("Timer1运行异常" + ex.Message, eOutputType.Error);
                Log4Neter.Error("Timer1运行异常", ex);
            }

            timer1.Start();
        }

        /// <summary>
        /// 重置
        /// </summary>
        private void Restet()
        {
            this.CurrentFlowFlag = eFlowFlag.等待扫码;
            this.RCSampleBarrel = null;
            this.brotherRCSampleBarrels.Clear();
            this.CurrentWeight = 0;
            this.IsScanedRCSampleBarrelId.Clear();
            txtInputSampleCode.ResetText();
            rtxtOutputInfo.ResetText();

            // 方便客户快速使用，获取焦点
            txtInputSampleCode.Focus();
            CreateButtonX(10);
            ShowButton(0, "Clear");
        }

        #endregion

        #region 操作

        /// <summary>
        /// 键入Enter检测有效性
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtInputSampleCode_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (this.CurrentFlowFlag != eFlowFlag.等待扫码) return;

                string barrelCode = txtInputSampleCode.Text.Trim().ToUpper();
                if (String.IsNullOrWhiteSpace(barrelCode)) return;

                //  根据采样桶编码查找该采样单下所有采样桶记录
                if (this.brotherRCSampleBarrels.Count == 0)
                {
                    this.brotherRCSampleBarrels = czyHandlerDAO.GetRCSampleBarrels(barrelCode, out resMessage);
                    if (this.brotherRCSampleBarrels.Count == 0)
                    {
                        ShowMessage(resMessage, eOutputType.Error);
                        txtInputSampleCode.ResetText();
                        return;
                    }
                    ShowMessage(resMessage, eOutputType.Normal);
                    CreateButtonX(this.brotherRCSampleBarrels.Count);
                    ShowButton(this.brotherRCSampleBarrels.Count, "Sum");
                }

                // 采样桶编码属于同一采样单下则验证通过，直到全部验证完毕
                this.rCSampleBarrel = this.brotherRCSampleBarrels.Where(a => a.SampSecondCode == barrelCode).FirstOrDefault();
                if (this.rCSampleBarrel != null)
                {
                    if (!this.IsScanedRCSampleBarrelId.Contains(this.rCSampleBarrel.Id))
                    {
                        this.IsScanedRCSampleBarrelId.Add(this.rCSampleBarrel.Id);

                        //ShowButton(this.IsScanedRCSampleBarrelId.Count, "Already");
                        ShowButton(this.IsScanedRCSampleBarrelId.Count, "Already", Convert.ToInt32(this.rCSampleBarrel.SampSecondCode.Substring(14, 2)));

                        if (this.IsScanedRCSampleBarrelId.Count < this.brotherRCSampleBarrels.Count)
                            ShowMessage("样桶编码：" + barrelCode + "，还剩" + (this.brotherRCSampleBarrels.Count - this.IsScanedRCSampleBarrelId.Count) + "桶未交样，请扫下个样桶", eOutputType.Normal);
                        else
                        {
                            foreach (CmcsRCSampleBarrel item in this.brotherRCSampleBarrels)
                            {
                                CmcsSampleBarrel entity = commonDAO.SelfDber.Entity<CmcsSampleBarrel>(" where BarrelCode='" + item.BarrelCode + "' ");
                                if (entity != null)
                                {
                                    entity.IsUse = 0;
                                    Dbers.GetInstance().SelfDber.Update(entity);
                                }
                            }
                            czyHandlerDAO.SaveHandSamplingSend(this.brotherRCSampleBarrels[0].SamplingId, SelfVars.LoginUser.UserName, DateTime.Now);
                            ShowMessage("样桶编码：" + barrelCode + "，该批次样桶已全部交样成功", eOutputType.Normal);
                        }
                        this.CurrentFlowFlag = eFlowFlag.等待校验;
                    }
                    else
                    {
                        txtInputSampleCode.ResetText();
                        ShowMessage("样桶编码：" + barrelCode + " 已校验，请扫下个样桶", eOutputType.Error);
                    }
                }
                else
                {
                    txtInputSampleCode.ResetText();
                    ShowMessage("样桶编码：" + barrelCode + " 交样失败，请扫下个样桶", eOutputType.Error);
                }
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

        #endregion

        #region 其他

        private void ClearBarrelCode()
        {
            txtInputSampleCode.ResetText();
            this.RCSampleBarrel = null;
            this.brotherRCSampleBarrels.Clear();
            this.CurrentFlowFlag = eFlowFlag.等待扫码;
        }

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
        /// 设置按钮显示状态
        /// </summary>
        /// <param name="count">总个数</param>
        /// <param name="type">类型</param> 
        /// <param name="index">标记位</param>
        private void ShowButton(int count, string type, int index = 0)
        {
            if (type == "Sum")
            {
                foreach (Control control in panSampleBarrels.Controls)
                {
                    if (control.Tag == null) continue;
                    if (control.Tag.ToString() == "Btn")
                    {
                        for (int i = 1; i <= count; i++)
                        {
                            if (control.Text == i.ToString())
                                control.BackColor = Color.FromArgb(0, 157, 218);
                        }
                    }
                }
            }
            else if (type == "Already")
            {
                foreach (Control control in panSampleBarrels.Controls)
                {
                    if (control.Tag == null) continue;
                    if (control.Tag.ToString() == "Btn")
                    {
                        //for (int i = 1; i <= count; i++)
                        //{
                        if (control.Text == index.ToString())
                            control.BackColor = Color.Red;
                        //}
                    }
                }
            }
            else if (type == "Clear")
            {
                foreach (Control control in panSampleBarrels.Controls)
                {
                    if (control.Tag == null) continue;
                    if (control.Tag.ToString() == "Btn")
                        control.BackColor = System.Drawing.Color.DarkGray;
                }
            }
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
