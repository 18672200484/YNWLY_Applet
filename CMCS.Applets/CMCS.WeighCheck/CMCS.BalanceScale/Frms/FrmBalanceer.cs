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
        /// ����Ψһ��ʶ��
        /// </summary>
        public static string UniqueKey = "FrmBalanceer";

        #region Vars

        CommonDAO commonDAO = CommonDAO.GetInstance();
        BeltSamplerDAO beltSamplerDAO = BeltSamplerDAO.GetInstance();

        string machineCode;
        /// <summary>
        /// ��ǰ�豸���
        /// </summary>
        public string MachineCode
        {
            get { return machineCode; }
            set { machineCode = value; }
        }

        RTxtOutputer rTxtOutputer;

        #endregion

        /// <summary>
        /// �����ʼ��
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

        #region ������ƽ

        double currentWeight = 0;
        /// <summary>
        /// ���ӳӵ�ǰ����
        /// </summary>
        public double CurrentWeight
        {
            get { return currentWeight; }
            set { currentWeight = value; }
        }

        bool isUseWeight = true;
        /// <summary>
        /// ���õ��ӳ�
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
        /// ���ӳ�����״̬
        /// </summary>
        public bool WbRunStatus
        {
            get { return wbRunStatus; }
            set { wbRunStatus = value; }
        }

        /// <summary>
        /// ���ӳ�״̬�仯
        /// </summary>
        /// <param name="status"></param>
        void wber_OnStatusChange(string machinCode, bool status)
        {
            // �����豸״̬ 
            InvokeEx(() =>
            {
                this.WbRunStatus = status;
                switch (machinCode)
                {
                    case "��ƽ1":
                        slightWber1.LightColor = (status ? Color.Green : Color.Red);
                        break;
                    case "��ƽ2":
                        slightWber2.LightColor = (status ? Color.Green : Color.Red);
                        break;
                    case "��ƽ3":
                        slightWber3.LightColor = (status ? Color.Green : Color.Red);
                        break;
                    case "��ƽ4":
                        slightWber4.LightColor = (status ? Color.Green : Color.Red);
                        break;
                    case "��ƽ5":
                        slightWber5.LightColor = (status ? Color.Green : Color.Red);
                        break;
                    case "��ƽ6":
                        slightWber6.LightColor = (status ? Color.Green : Color.Red);
                        break;
                }
            });
        }

        /// <summary>
        /// ���ӳ������仯
        /// </summary>
        /// <param name="status"></param>
        void wber_OnWeightChange(string machinCode, double weight)
        {
            // �������� 
            InvokeEx(() =>
            {
                if (weight > 0 && !string.IsNullOrEmpty(this.txtInputAssayCode.Text))
                    JoinBalance(this.txtInputAssayCode.Text += weight.ToString());
            });
        }

        #endregion

        #region �豸��ʼ����ж��

        /// <summary>
        /// ��ʼ������豸
        /// </summary>
        private void InitHardware()
        {
            try
            {
                bool success = false;
                string[] carriageRecognitionerMachineCodes = new string[] { "��ƽ1", "��ƽ2", "��ƽ3", "��ƽ4", "��ƽ5", "��ƽ6" };
                Hardwarer.Wber1.OnStatusChange += new WB.Sartorius.Balance.Sartorius_Balance.StatusChangeHandler(wber_OnStatusChange);
                Hardwarer.Wber1.OnWeightChange += new WB.Sartorius.Balance.Sartorius_Balance.WeightChangeEventHandler(wber_OnWeightChange);
                MachineCode = carriageRecognitionerMachineCodes[0];
                success = Hardwarer.Wber1.OpenCom(commonDAO.GetAppletConfigInt32(string.Format("{0}����", MachineCode)), commonDAO.GetAppletConfigInt32(string.Format("{0}������", MachineCode)), commonDAO.GetAppletConfigInt32(string.Format("{0}����λ", MachineCode)), commonDAO.GetAppletConfigInt32(string.Format("{0}У��λ", MachineCode)), MachineCode);

                Hardwarer.Wber2.OnStatusChange += new WB.Sartorius.Balance.Sartorius_Balance.StatusChangeHandler(wber_OnStatusChange);
                Hardwarer.Wber2.OnWeightChange += new WB.Sartorius.Balance.Sartorius_Balance.WeightChangeEventHandler(wber_OnWeightChange);
                MachineCode = carriageRecognitionerMachineCodes[1];
                success = Hardwarer.Wber2.OpenCom(commonDAO.GetAppletConfigInt32(string.Format("{0}����", MachineCode)), commonDAO.GetAppletConfigInt32(string.Format("{0}������", MachineCode)), commonDAO.GetAppletConfigInt32(string.Format("{0}����λ", MachineCode)), commonDAO.GetAppletConfigInt32(string.Format("{0}У��λ", MachineCode)), MachineCode);

                Hardwarer.Wber3.OnStatusChange += new WB.Sartorius.Balance.Sartorius_Balance.StatusChangeHandler(wber_OnStatusChange);
                Hardwarer.Wber3.OnWeightChange += new WB.Sartorius.Balance.Sartorius_Balance.WeightChangeEventHandler(wber_OnWeightChange);
                MachineCode = carriageRecognitionerMachineCodes[2];
                success = Hardwarer.Wber3.OpenCom(commonDAO.GetAppletConfigInt32(string.Format("{0}����", MachineCode)), commonDAO.GetAppletConfigInt32(string.Format("{0}������", MachineCode)), commonDAO.GetAppletConfigInt32(string.Format("{0}����λ", MachineCode)), commonDAO.GetAppletConfigInt32(string.Format("{0}У��λ", MachineCode)), MachineCode);

                Hardwarer.Wber4.OnStatusChange += new WB.Sartorius.Balance.Sartorius_Balance.StatusChangeHandler(wber_OnStatusChange);
                Hardwarer.Wber4.OnWeightChange += new WB.Sartorius.Balance.Sartorius_Balance.WeightChangeEventHandler(wber_OnWeightChange);
                MachineCode = carriageRecognitionerMachineCodes[3];
                success = Hardwarer.Wber4.OpenCom(commonDAO.GetAppletConfigInt32(string.Format("{0}����", MachineCode)), commonDAO.GetAppletConfigInt32(string.Format("{0}������", MachineCode)), commonDAO.GetAppletConfigInt32(string.Format("{0}����λ", MachineCode)), commonDAO.GetAppletConfigInt32(string.Format("{0}У��λ", MachineCode)), MachineCode);

                Hardwarer.Wber5.OnStatusChange += new WB.Sartorius.Balance.Sartorius_Balance.StatusChangeHandler(wber_OnStatusChange);
                Hardwarer.Wber5.OnWeightChange += new WB.Sartorius.Balance.Sartorius_Balance.WeightChangeEventHandler(wber_OnWeightChange);
                MachineCode = carriageRecognitionerMachineCodes[4];
                success = Hardwarer.Wber5.OpenCom(commonDAO.GetAppletConfigInt32(string.Format("{0}����", MachineCode)), commonDAO.GetAppletConfigInt32(string.Format("{0}������", MachineCode)), commonDAO.GetAppletConfigInt32(string.Format("{0}����λ", MachineCode)), commonDAO.GetAppletConfigInt32(string.Format("{0}У��λ", MachineCode)), MachineCode);

                Hardwarer.Wber6.OnStatusChange += new WB.Sartorius.Balance.Sartorius_Balance.StatusChangeHandler(wber_OnStatusChange);
                Hardwarer.Wber6.OnWeightChange += new WB.Sartorius.Balance.Sartorius_Balance.WeightChangeEventHandler(wber_OnWeightChange);
                MachineCode = carriageRecognitionerMachineCodes[5];
                success = Hardwarer.Wber6.OpenCom(commonDAO.GetAppletConfigInt32(string.Format("{0}����", MachineCode)), commonDAO.GetAppletConfigInt32(string.Format("{0}������", MachineCode)), commonDAO.GetAppletConfigInt32(string.Format("{0}����λ", MachineCode)), commonDAO.GetAppletConfigInt32(string.Format("{0}У��λ", MachineCode)), MachineCode);

                timer1.Enabled = true;
            }
            catch (Exception ex)
            {
                Log4Neter.Error("�豸��ʼ��", ex);
            }
        }

        /// <summary>
        /// ж���豸
        /// </summary>
        private void UnloadHardware()
        {
            // ע��˶δ���
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

        #region ҵ��

        private void txtInputAssayCode_TextChanged(object sender, EventArgs e)
        {
            //#1��ƽ|HY2019053198|#1����|29.09
            if (this.txtInputAssayCode.Text.Contains("��ƽ") && this.txtInputAssayCode.Text.EndsWith("��ƽ"))
                this.txtInputAssayCode.Text = this.txtInputAssayCode.Text.Substring(this.txtInputAssayCode.Text.Length - 4, 4);
            if (this.txtInputAssayCode.Text.EndsWith("��ƽ") || this.txtInputAssayCode.Text.EndsWith("����"))
                this.txtInputAssayCode.Text += "|";
            if (this.txtInputAssayCode.Text.Contains("��ƽ") && this.txtInputAssayCode.Text.ToUpper().Contains("HY") && this.txtInputAssayCode.Text.Length == 17)
                this.txtInputAssayCode.Text += "|";
            this.txtInputAssayCode.SelectionStart = this.txtInputAssayCode.Text.Length;
        }

        /// <summary>
        /// ���ؽ�����¼�뻯������
        /// </summary>
        /// <param name="superGridControl"></param>
        private void LoadBalanceList(SuperGridControl superGridControl)
        {
            List<InfBalanceRecord> list = commonDAO.SelfDber.Entities<InfBalanceRecord>("where CreateDate>=:CreateDate order by CreateDate asc", new { CreateDate = DateTime.Now.Date });
            superGridControl.PrimaryGrid.DataSource = list;
        }

        /// <summary>
        /// ¼�뻯������
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
            //��һ�μ���ʱѡ�е�һ��
            foreach (GridRow item in superGridControl1.PrimaryGrid.Rows)
            {
                InfBalanceRecord entity = item.DataItem as InfBalanceRecord;

            }
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
