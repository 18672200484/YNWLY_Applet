using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.IO;
//
using CMCS.Common.DAO;
using CMCS.WeighCheck.DAO;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using DevComponents.DotNetBar.Metro;
using CMCS.Common.Utilities;
using CMCS.Common.Entities.Fuel;
using DevComponents.DotNetBar.SuperGrid;
using RW.HFReader;
using CMCS.WeighCheck.MakeChange.Enums;

using ThoughtWorks.QRCode.Codec;
using CMCS.Common.Entities.AutoCupboard;
using CMCS.WeighCheck.MakeChange.Utilities;
using System.Threading;
using CMCS.Common.Enums;
using CMCS.Common.Entities.BaseInfo;
using CMCS.Common;


namespace CMCS.WeighCheck.MakeChange.Frms
{
    public partial class FrmAutoCupboard : MetroForm
    {
        public FrmAutoCupboard()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体唯一标识符
        /// </summary>
        public static string UniqueKey = "FrmAutoCupboard";

        #region 业务处理类

        CommonDAO commonDAO = CommonDAO.GetInstance();
        CZYHandlerDAO czyHandlerDAO = CZYHandlerDAO.GetInstance();
        PneumaticTransferDAO pneumaticTransferDAO = PneumaticTransferDAO.GetInstance();
        AutoCupboardDAO autoCupboardDAO = AutoCupboardDAO.GetInstance();

        TaskSimpleScheduler taskSimpleScheduler = new TaskSimpleScheduler();

        System.Threading.AutoResetEvent autoResetEvent = new AutoResetEvent(false);

        #endregion

        #region Vars
        bool IsWorking;

        string resMessage = string.Empty;
        string sqlWhere = "where 1=1 ";
        #endregion

        /// <summary>
        /// 初始化
        /// </summary>
        public void InitFrom()
        {
            // 生成取样按钮
            GridButtonXEditControl btnNewCode = superGridControl1.PrimaryGrid.Columns["gclmTakeOut"].EditControl as GridButtonXEditControl;
            btnNewCode.ColorTable = eButtonColor.BlueWithBackground;
            btnNewCode.Click += new EventHandler(btnTake);
            // 生成弃样按钮
            GridButtonXEditControl btnPrintCode = superGridControl1.PrimaryGrid.Columns["gclmPutAway"].EditControl as GridButtonXEditControl;
            btnNewCode.ColorTable = eButtonColor.BlueWithBackground;
            btnPrintCode.Click += new EventHandler(btnPut);
        }

        private void FrmMakeWeight_Load(object sender, EventArgs e)
        {
            // 初始化
            InitFrom();
            btnSearch_Click(null, null);
            timer1_Tick(null, null);
        }

        private void FrmMakeWeight_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        /// <summary>
        /// 加载数据
        /// </summary>
        private void BindData()
        {
            IList<InfCYGSam> cyg = commonDAO.SelfDber.Entities<InfCYGSam>(sqlWhere + " and Code is not null order by MachineCode,CellIndex,ColumnIndex ");
            superGridControl1.PrimaryGrid.DataSource = cyg;
        }

        /// <summary>
        /// 取样
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnTake(object sender, EventArgs e)
        {
            GridButtonXEditControl btn = sender as GridButtonXEditControl;
            if (btn == null) return;
            InfCYGSam entity = btn.EditorCell.GridRow.DataItem as InfCYGSam;
            if (entity != null)
            {
                if (entity.MachineCode == GlobalVars.MachineCode_CYG1 && slightCYG.LightColor != EquipmentStatusColors.BeReady)
                {
                    MessageBoxEx.Show(string.Format("{0}未就绪!", entity.MachineCode), "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (entity.MachineCode == GlobalVars.MachineCode_CYG2 && slightCYG2.LightColor != EquipmentStatusColors.BeReady)
                {
                    MessageBoxEx.Show(string.Format("{0}未就绪!", entity.MachineCode), "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                List<string> makeCodes = new List<string>();
                makeCodes.Add(entity.Code);
                SendCYGCmd(makeCodes, "取样", SelfVars.LoginUser.UserName, entity.MachineCode);
            }
        }

        /// <summary>
        /// 弃样
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPut(object sender, EventArgs e)
        {
            GridButtonXEditControl btn = sender as GridButtonXEditControl;
            if (btn == null) return;
            InfCYGSam entity = btn.EditorCell.GridRow.DataItem as InfCYGSam;
            if (entity != null)
            {
                List<string> makeCodes = new List<string>();
                makeCodes.Add(entity.Code);
                SendCYGCmd(makeCodes, "弃样", SelfVars.LoginUser.UserName, entity.MachineCode);
            }
        }


        /// <summary>
        /// 发送存样柜命令并监听执行结果
        /// </summary> 
        /// <returns></returns>
        private void SendCYGCmd(List<String> MakeCodes, String OperType, string OperUser, string machineCode)
        {
            taskSimpleScheduler = new TaskSimpleScheduler();

            autoResetEvent.Reset();

            taskSimpleScheduler.StartNewTask("存样柜命令", () =>
            {
                this.IsWorking = true;

                // 发送取样命令
                if (!string.IsNullOrEmpty(autoCupboardDAO.AddAutoCupboard(MakeCodes, OperType, OperUser, machineCode)))
                {
                    //pneumaticTransferDAO.SaveQDCmd();
                    ShowMessage(string.Format("{0}命令发送成功,等待存样柜执行", OperType), eOutputType.Normal);

                    int waitCount = 0;
                    eEquInfCmdResultCode equInfCmdResultCode;
                    do
                    {
                        Thread.Sleep(10000);
                        if (waitCount % 5 == 0) ShowMessage("正在等待存样柜返回结果", eOutputType.Normal);

                        waitCount++;

                        // 获取卸样命令的执行结果
                        equInfCmdResultCode = autoCupboardDAO.GetAutoCupboardResult(MakeCodes[0]);
                    }
                    while (equInfCmdResultCode == eEquInfCmdResultCode.默认);

                    if (equInfCmdResultCode == eEquInfCmdResultCode.成功)
                    {
                        ShowMessage("存样柜返回：执行成功", eOutputType.Normal);
                        if (SendQDCSCmd(MakeCodes[0], machineCode))
                        {
                            do
                            {
                                Thread.Sleep(10000);
                                if (waitCount % 5 == 0) ShowMessage("正在等待气动传输返回结果", eOutputType.Normal);

                                waitCount++;

                                // 获取卸样命令的执行结果
                                equInfCmdResultCode = pneumaticTransferDAO.GetQDCSResult(MakeCodes[0]);
                            }
                            while (equInfCmdResultCode == eEquInfCmdResultCode.默认);
                        }
                        if (equInfCmdResultCode == eEquInfCmdResultCode.成功)
                        {
                            ShowMessage("气动传输返回：执行成功", eOutputType.Normal);
                        }
                    }
                }
                else
                {
                    ShowMessage("存样柜命令发送失败", eOutputType.Error);
                }

                autoResetEvent.Set();
            });
        }

        /// <summary>
        /// 发送气送命令
        /// </summary>
        /// <param name="makeCode"></param>
        /// <param name="machineCode"></param>
        /// <returns></returns>
        public bool SendQDCSCmd(string makeCode, string machineCode)
        {
            CmcsCMEquipment start = machineCode == GlobalVars.MachineCode_CYG1 ? commonDAO.GetCMEquipmentByMachineName("双向收发站1") : commonDAO.GetCMEquipmentByMachineName("双向收发站2");
            CmcsCMEquipment end = commonDAO.GetCMEquipmentByMachineName("化验室接收站");
            if (start != null && end != null)
            {
                string message = string.Empty;
                if (pneumaticTransferDAO.SaveQDCmd(makeCode, start, end, out message))
                {
                    ShowMessage(message, eOutputType.Normal);
                    return true;
                }
            }
            return false;
        }

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
        /// 搜索
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtMakeCode.Text))
                sqlWhere += " and Code like '%" + txtMakeCode.Text + "%'";
            BindData();
        }

        /// <summary>
        /// 全部
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnAll_Click(object sender, EventArgs e)
        {
            sqlWhere = "where 1=1 ";
            txtMakeCode.ResetText();
            BindData();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            BindData();
            RefreshEquStatus();
        }


        /// <summary>
        /// 更新设备状态
        /// </summary>
        private void RefreshEquStatus()
        {
            string systemStatus = commonDAO.GetSignalDataValue(GlobalVars.MachineCode_CYG1, eSignalDataName.设备状态.ToString());
            string s1 = commonDAO.GetSignalDataValue(GlobalVars.MachineCode_CYG1, "机械手空闲标志位");
            string s2 = commonDAO.GetSignalDataValue(GlobalVars.MachineCode_CYG1, "人工取瓶准备好标志位");
            string s3 = commonDAO.GetSignalDataValue(GlobalVars.MachineCode_CYG1, "自动取瓶准备好标志位");
            string s4 = commonDAO.GetSignalDataValue(GlobalVars.MachineCode_CYG1, "自动存瓶准备好标志位");

            if (systemStatus == "自动" && s1 == "1" && s2 == "1" && s3 == "1" && s4 == "1")
            {
                slightCYG.LightColor = EquipmentStatusColors.BeReady;
            }
            else
            {
                slightCYG.LightColor = EquipmentStatusColors.Working;
            }
            systemStatus = commonDAO.GetSignalDataValue(GlobalVars.MachineCode_CYG2, eSignalDataName.设备状态.ToString());
            s1 = commonDAO.GetSignalDataValue(GlobalVars.MachineCode_CYG2, "机械手空闲标志位");
            s2 = commonDAO.GetSignalDataValue(GlobalVars.MachineCode_CYG2, "人工取瓶准备好标志位");
            s3 = commonDAO.GetSignalDataValue(GlobalVars.MachineCode_CYG2, "自动取瓶准备好标志位");
            s4 = commonDAO.GetSignalDataValue(GlobalVars.MachineCode_CYG2, "自动存瓶准备好标志位");

            if (systemStatus == "自动" && s1 == "1" && s2 == "1" && s3 == "1" && s4 == "1")
            {
                slightCYG2.LightColor = EquipmentStatusColors.BeReady;
            }
            else
            {
                slightCYG2.LightColor = EquipmentStatusColors.Working;
            }
        }

        #region DataGridView
        private void superGridControl1_GetRowHeaderText(object sender, GridGetRowHeaderTextEventArgs e)
        {
            e.Text = (e.GridRow.Index + 1).ToString();
        }

        private void superGridControl1_DataBindingComplete(object sender, GridDataBindingCompleteEventArgs e)
        {
            foreach (GridRow gridRow in e.GridPanel.Rows)
            {
                InfCYGSam entity = gridRow.DataItem as InfCYGSam;
                gridRow.Cells["clmAssayCode"].Value = autoCupboardDAO.GetAssayCodeByMakeDetailCode(entity.Code);
            }
        }
        #endregion
    }
}
