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
using CMCS.BalanceScale.DAO;
using CMCS.BalanceScale.Enums;
using CMCS.BalanceScale.Frms;
//
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Metro;
using DevComponents.DotNetBar.SuperGrid;
using CMCS.Common.Entities.Balance;
using CMCS.BalanceScale.Core;
using CMCS.BalanceScale.Utilities;

namespace CMCS.BalanceScale.Frms
{
    public partial class FrmBalanceer : MetroForm
    {
        public FrmBalanceer()
        {
            InitializeComponent();
        }
        public FrmBalanceer(string machineCode)
        {
            InitializeComponent();
            this.MachineCode = machineCode;
        }

        /// <summary>
        /// 窗体唯一标识符
        /// </summary>
        public static string UniqueKey = "FrmBalanceer";

        #region Vars

        CommonDAO commonDAO = CommonDAO.GetInstance();
        BeltSamplerDAO beltSamplerDAO = BeltSamplerDAO.GetInstance();

        string machineCode;
        /// <summary>
        /// 当前设备编号
        /// </summary>
        public string MachineCode
        {
            get { return machineCode; }
            set { machineCode = value; }
        }

        RTxtOutputer rTxtOutputer;

        #endregion

        /// <summary>
        /// 窗体初始化
        /// </summary>
        private void FormInit()
        {
            rTxtOutputer = new RTxtOutputer(rTxTMessageInfo);
        }

        private void FrmUnloadSampler_Load(object sender, EventArgs e)
        {
            FormInit();
            InitHardware();
            LoadBalanceList(superGridControl1);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        #region 电子天平

        double currentWeight = 0;
        /// <summary>
        /// 电子秤当前重量
        /// </summary>
        public double CurrentWeight
        {
            get { return currentWeight; }
            set { currentWeight = value; }
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

                lblWber.Visible = value;
                slightWber1.Visible = value;
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

        /// <summary>
        /// 电子秤状态变化
        /// </summary>
        /// <param name="status"></param>
        void wber_OnStatusChange(string machinCode, bool status)
        {
            // 接收设备状态 
            InvokeEx(() =>
            {
                this.WbRunStatus = status;
                switch (machinCode)
                {
                    case "天平1":
                        slightWber1.LightColor = (status ? Color.Green : Color.Red);
                        break;
                    case "天平2":
                        slightWber2.LightColor = (status ? Color.Green : Color.Red);
                        break;
                    case "天平3":
                        slightWber3.LightColor = (status ? Color.Green : Color.Red);
                        break;
                    case "天平4":
                        slightWber4.LightColor = (status ? Color.Green : Color.Red);
                        break;
                    case "天平5":
                        slightWber5.LightColor = (status ? Color.Green : Color.Red);
                        break;
                    case "天平6":
                        slightWber6.LightColor = (status ? Color.Green : Color.Red);
                        break;
                }
            });
        }

        /// <summary>
        /// 电子秤重量变化
        /// </summary>
        /// <param name="status"></param>
        void wber_OnWeightChange(string machinCode, double weight)
        {
            // 接收重量 
            InvokeEx(() =>
            {
                if (weight > 0 && !string.IsNullOrEmpty(this.txtInputAssayCode.Text))
                    JoinBalance(this.txtInputAssayCode.Text += weight.ToString());
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
                string[] carriageRecognitionerMachineCodes = new string[] { "天平1", "天平2", "天平3", "天平4", "天平5", "天平6" };
                Hardwarer.Wber1.OnStatusChange += new WB.Sartorius.Balance.Sartorius_Balance.StatusChangeHandler(wber_OnStatusChange);
                Hardwarer.Wber1.OnWeightChange += new WB.Sartorius.Balance.Sartorius_Balance.WeightChangeEventHandler(wber_OnWeightChange);
                MachineCode = carriageRecognitionerMachineCodes[0];
                success = Hardwarer.Wber1.OpenCom(commonDAO.GetAppletConfigInt32(string.Format("{0}串口", MachineCode)), commonDAO.GetAppletConfigInt32(string.Format("{0}波特率", MachineCode)), commonDAO.GetAppletConfigInt32(string.Format("{0}数据位", MachineCode)), commonDAO.GetAppletConfigInt32(string.Format("{0}校验位", MachineCode)), MachineCode);

                Hardwarer.Wber2.OnStatusChange += new WB.Sartorius.Balance.Sartorius_Balance.StatusChangeHandler(wber_OnStatusChange);
                Hardwarer.Wber2.OnWeightChange += new WB.Sartorius.Balance.Sartorius_Balance.WeightChangeEventHandler(wber_OnWeightChange);
                MachineCode = carriageRecognitionerMachineCodes[1];
                success = Hardwarer.Wber2.OpenCom(commonDAO.GetAppletConfigInt32(string.Format("{0}串口", MachineCode)), commonDAO.GetAppletConfigInt32(string.Format("{0}波特率", MachineCode)), commonDAO.GetAppletConfigInt32(string.Format("{0}数据位", MachineCode)), commonDAO.GetAppletConfigInt32(string.Format("{0}校验位", MachineCode)), MachineCode);

                Hardwarer.Wber3.OnStatusChange += new WB.Sartorius.Balance.Sartorius_Balance.StatusChangeHandler(wber_OnStatusChange);
                Hardwarer.Wber3.OnWeightChange += new WB.Sartorius.Balance.Sartorius_Balance.WeightChangeEventHandler(wber_OnWeightChange);
                MachineCode = carriageRecognitionerMachineCodes[2];
                success = Hardwarer.Wber3.OpenCom(commonDAO.GetAppletConfigInt32(string.Format("{0}串口", MachineCode)), commonDAO.GetAppletConfigInt32(string.Format("{0}波特率", MachineCode)), commonDAO.GetAppletConfigInt32(string.Format("{0}数据位", MachineCode)), commonDAO.GetAppletConfigInt32(string.Format("{0}校验位", MachineCode)), MachineCode);

                Hardwarer.Wber4.OnStatusChange += new WB.Sartorius.Balance.Sartorius_Balance.StatusChangeHandler(wber_OnStatusChange);
                Hardwarer.Wber4.OnWeightChange += new WB.Sartorius.Balance.Sartorius_Balance.WeightChangeEventHandler(wber_OnWeightChange);
                MachineCode = carriageRecognitionerMachineCodes[3];
                success = Hardwarer.Wber4.OpenCom(commonDAO.GetAppletConfigInt32(string.Format("{0}串口", MachineCode)), commonDAO.GetAppletConfigInt32(string.Format("{0}波特率", MachineCode)), commonDAO.GetAppletConfigInt32(string.Format("{0}数据位", MachineCode)), commonDAO.GetAppletConfigInt32(string.Format("{0}校验位", MachineCode)), MachineCode);

                Hardwarer.Wber5.OnStatusChange += new WB.Sartorius.Balance.Sartorius_Balance.StatusChangeHandler(wber_OnStatusChange);
                Hardwarer.Wber5.OnWeightChange += new WB.Sartorius.Balance.Sartorius_Balance.WeightChangeEventHandler(wber_OnWeightChange);
                MachineCode = carriageRecognitionerMachineCodes[4];
                success = Hardwarer.Wber5.OpenCom(commonDAO.GetAppletConfigInt32(string.Format("{0}串口", MachineCode)), commonDAO.GetAppletConfigInt32(string.Format("{0}波特率", MachineCode)), commonDAO.GetAppletConfigInt32(string.Format("{0}数据位", MachineCode)), commonDAO.GetAppletConfigInt32(string.Format("{0}校验位", MachineCode)), MachineCode);

                Hardwarer.Wber6.OnStatusChange += new WB.Sartorius.Balance.Sartorius_Balance.StatusChangeHandler(wber_OnStatusChange);
                Hardwarer.Wber6.OnWeightChange += new WB.Sartorius.Balance.Sartorius_Balance.WeightChangeEventHandler(wber_OnWeightChange);
                MachineCode = carriageRecognitionerMachineCodes[5];
                success = Hardwarer.Wber6.OpenCom(commonDAO.GetAppletConfigInt32(string.Format("{0}串口", MachineCode)), commonDAO.GetAppletConfigInt32(string.Format("{0}波特率", MachineCode)), commonDAO.GetAppletConfigInt32(string.Format("{0}数据位", MachineCode)), commonDAO.GetAppletConfigInt32(string.Format("{0}校验位", MachineCode)), MachineCode);

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
                Hardwarer.Wber1.CloseCom();
            }
            catch
            { }

            try
            {
                Hardwarer.Wber2.CloseCom();
            }
            catch
            { }

            try
            {
                Hardwarer.Wber3.CloseCom();
            }
            catch
            { }

            try
            {
                Hardwarer.Wber4.CloseCom();
            }
            catch
            { }

            try
            {
                Hardwarer.Wber5.CloseCom();
            }
            catch
            { }

            try
            {
                Hardwarer.Wber6.CloseCom();
            }
            catch
            { }
        }
        #endregion

        #region 业务

        private void txtInputAssayCode_TextChanged(object sender, EventArgs e)
        {
            //#1天平|HY2019053198|#1坩埚|29.09
            if (this.txtInputAssayCode.Text.Contains("天平") && this.txtInputAssayCode.Text.EndsWith("天平"))
                this.txtInputAssayCode.Text = this.txtInputAssayCode.Text.Substring(this.txtInputAssayCode.Text.Length - 4, 4);
            if (this.txtInputAssayCode.Text.EndsWith("天平") || this.txtInputAssayCode.Text.EndsWith("坩埚"))
                this.txtInputAssayCode.Text += "|";
            if (this.txtInputAssayCode.Text.Contains("天平") && this.txtInputAssayCode.Text.ToUpper().Contains("HY") && this.txtInputAssayCode.Text.Length == 17)
                this.txtInputAssayCode.Text += "|";
            this.txtInputAssayCode.SelectionStart = this.txtInputAssayCode.Text.Length;
        }

        /// <summary>
        /// 加载今日已录入化验数据
        /// </summary>
        /// <param name="superGridControl"></param>
        private void LoadBalanceList(SuperGridControl superGridControl)
        {
            List<InfBalanceRecord> list = commonDAO.SelfDber.Entities<InfBalanceRecord>("where CreateDate>=:CreateDate order by CreateDate asc", new { CreateDate = DateTime.Now.Date });
            superGridControl.PrimaryGrid.DataSource = list;
        }

        /// <summary>
        /// 录入化验数据
        /// </summary>
        /// <param name="assayCode"></param>
        /// <returns></returns>
        private bool JoinBalance(string content)
        {
            string[] pars = content.Split('|');
            if (pars.Length != 4) return false;
            InfBalanceRecord entity = commonDAO.SelfDber.Entity<InfBalanceRecord>("where AssayCode=:AssayCode and  MachineCode=:MachineCode and GGCode=:GGCode order by CreateDate desc", new { AssayCode = pars[1], MachineCode = pars[0], GGCode = pars[2] });
            if (entity == null)
            {
                entity = new InfBalanceRecord();
                entity.CreateUser = SelfVars.LoginUser.UserAccount;
                entity.OperUser = SelfVars.LoginUser.UserAccount;
                entity.MachineCode = pars[0];
                entity.AssayCode = pars[1];
                entity.GGCode = pars[2];
                entity.Weight = Convert.ToDouble(pars[3]);
                commonDAO.SelfDber.Insert(entity);
                LoadBalanceList(superGridControl1);
            }
            this.txtInputAssayCode.Text = "";
            LoadBalanceList(superGridControl1);
            return true;
        }

        private void txtInputAssayCode_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!String.IsNullOrEmpty(txtInputAssayCode.Text.Trim()))
                {
                    JoinBalance(txtInputAssayCode.Text.Trim().ToUpper());
                }
            }
        }
        #endregion

        #region SuperGridControl

        private void superGridControl1_DataBindingComplete(object sender, GridDataBindingCompleteEventArgs e)
        {
            //第一次加载时选中第一条
            foreach (GridRow item in superGridControl1.PrimaryGrid.Rows)
            {
                InfBalanceRecord entity = item.DataItem as InfBalanceRecord;

            }
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
