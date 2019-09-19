using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using CMCS.CarTransport.WeighterHand.Core;
using System.Threading;
using System.IO;
using CMCS.CarTransport.DAO;
using CMCS.Common.DAO;
using System.IO.Ports;
using CMCS.Common.Utilities;
using CMCS.CarTransport.WeighterHand.Enums;
using CMCS.Common.Entities;
using CMCS.Common.Entities.CarTransport;
using CMCS.Common;
using CMCS.CarTransport.WeighterHand.Frms.Sys;
using DevComponents.DotNetBar.Controls;
using CMCS.Common.Views;
using DevComponents.DotNetBar.SuperGrid;
using CMCS.Common.Enums;
using CMCS.Common.Entities.Sys;
using CMCS.Common.Entities.BaseInfo;
using CMCS.Common.Entities.Fuel;
using CMCS.CarTransport.Views;
using CMCS.CarTransport.WeighterHand.Frms.Transport.Print;

namespace CMCS.CarTransport.WeighterHand.Frms
{
    public partial class FrmWeighter : DevComponents.DotNetBar.Metro.MetroForm
    {
        /// <summary>
        /// ����Ψһ��ʶ��
        /// </summary>
        public static string UniqueKey = "FrmWeighter";

        public FrmWeighter()
        {
            InitializeComponent();
        }

        #region Vars

        CarTransportDAO carTransportDAO = CarTransportDAO.GetInstance();
        WeighterDAO weighterDAO = WeighterDAO.GetInstance();
        CommonDAO commonDAO = CommonDAO.GetInstance();

        /// <summary>
        /// ��������
        /// </summary>
        VoiceSpeaker voiceSpeaker = new VoiceSpeaker();

        bool wbSteady = false;
        /// <summary>
        /// �ذ��Ǳ��ȶ�״̬
        /// </summary>
        public bool WbSteady
        {
            get { return wbSteady; }
            set
            {
                wbSteady = value;

                this.panCurrentWeight.Style.ForeColor.Color = (value ? Color.Lime : Color.Red);

                panCurrentWeight.Refresh();

                commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.�ذ��Ǳ�_�ȶ�.ToString(), value ? "1" : "0");
            }
        }

        double wbMinWeight = 0;
        /// <summary>
        /// �ذ��Ǳ���С���� ��λ����
        /// </summary>
        public double WbMinWeight
        {
            get { return wbMinWeight; }
            set
            {
                wbMinWeight = value;
            }
        }

        public static PassCarQueuer passCarQueuer = new PassCarQueuer();

        CmcsAutotruck currentAutotruck;
        /// <summary>
        /// ��ǰ��
        /// </summary>
        public CmcsAutotruck CurrentAutotruck
        {
            get { return currentAutotruck; }
            set
            {
                currentAutotruck = value;

                if (value != null)
                {
                    txt_CarNumber.Text = value.CarNumber;

                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.��ǰ��Id.ToString(), value.Id);
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.��ǰ����.ToString(), value.CarNumber);
                }
                else
                {
                    txt_CarNumber.Text = "";
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.��ǰ��Id.ToString(), string.Empty);
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.��ǰ����.ToString(), string.Empty);
                }
            }
        }

        private CmcsFuelKind selectedFuelKind_BuyFuel;
        /// <summary>
        /// ѡ���ú��
        /// </summary>
        public CmcsFuelKind SelectedFuelKind_BuyFuel
        {
            get { return selectedFuelKind_BuyFuel; }
            set
            {
                selectedFuelKind_BuyFuel = value;
            }
        }

        private CmcsMine selectedMine_BuyFuel;
        /// <summary>
        /// ѡ��Ŀ��
        /// </summary>
        public CmcsMine SelectedMine_BuyFuel
        {
            get { return selectedMine_BuyFuel; }
            set
            {
                selectedMine_BuyFuel = value;
            }
        }
        #endregion

        /// <summary>
        /// �����ʼ��
        /// </summary>
        private void InitForm()
        {
            LoadMine(cmbMineName_BuyFuel);
            LoadFuelkind(cmbFuelKindName_BuyFuel);
        }

        private void FrmWeighter_Load(object sender, EventArgs e)
        {

        }

        private void FrmWeighter_Shown(object sender, EventArgs e)
        {
            InitHardware();

            InitForm();
        }

        private void FrmQueuer_FormClosing(object sender, FormClosingEventArgs e)
        {
            // ж���豸
            UnloadHardware();
        }

        #region �豸���

        #region �ذ��Ǳ�

        /// <summary>
        /// �����ȶ��¼�
        /// </summary>
        /// <param name="steady"></param>
        void Wber_OnSteadyChange(bool steady)
        {
            InvokeEx(() =>
              {
                  this.WbSteady = steady;
              });
        }

        /// <summary>
        /// �ذ��Ǳ�״̬�仯
        /// </summary>
        /// <param name="status"></param>
        void Wber_OnStatusChange(bool status)
        {
            // �����豸״̬ 
            InvokeEx(() =>
            {
                slightWber.LightColor = (status ? Color.Green : Color.Red);

                commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.�ذ��Ǳ�_����״̬.ToString(), status ? "1" : "0");
            });
        }

        void Wber_OnWeightChange(double weight)
        {
            InvokeEx(() =>
            {
                panCurrentWeight.Text = weight.ToString();
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

                this.WbMinWeight = commonDAO.GetAppletConfigDouble("�ذ��Ǳ�_��С����");

                // �ذ��Ǳ�
                Hardwarer.Wber.OnStatusChange += new WB.TOLEDO.YAOHUA.TOLEDO_YAOHUAWber.StatusChangeHandler(Wber_OnStatusChange);
                Hardwarer.Wber.OnSteadyChange += new WB.TOLEDO.YAOHUA.TOLEDO_YAOHUAWber.SteadyChangeEventHandler(Wber_OnSteadyChange);
                Hardwarer.Wber.OnWeightChange += new WB.TOLEDO.YAOHUA.TOLEDO_YAOHUAWber.WeightChangeEventHandler(Wber_OnWeightChange);
                success = Hardwarer.Wber.OpenCom(commonDAO.GetAppletConfigInt32("�ذ��Ǳ�_����"), commonDAO.GetAppletConfigInt32("�ذ��Ǳ�_������"), commonDAO.GetAppletConfigInt32("�ذ��Ǳ�_����λ"), commonDAO.GetAppletConfigInt32("�ذ��Ǳ�_ֹͣλ"));

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
        }

        #endregion

        #endregion

        #region ����ҵ��

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer2_Tick(object sender, EventArgs e)
        {
            timer2.Stop();
            // ������ִ��һ��
            timer2.Interval = 180000;

            try
            {
                // �볧ú
                LoadTodayUnFinishBuyFuelTransport();
                LoadTodayFinishBuyFuelTransport();
            }
            catch (Exception ex)
            {
                Log4Neter.Error("timer2_Tick", ex);
            }
            finally
            {
                timer2.Start();
            }
        }

        #endregion

        #region �볧úҵ��

        CmcsBuyFuelTransport currentBuyFuelTransport;
        /// <summary>
        /// ��ǰ�����¼
        /// </summary>
        public CmcsBuyFuelTransport CurrentBuyFuelTransport
        {
            get { return currentBuyFuelTransport; }
            set
            {
                currentBuyFuelTransport = value;

                if (value != null)
                {
                    cmbFuelKindName_BuyFuel.Text = value.FuelKindName;
                    cmbMineName_BuyFuel.Text = value.MineName;
                    txt_CarNumber.Text = value.CarNumber;
                    txt_CarNumber.Select(txt_CarNumber.Text.Length, 0);
                    txtGrossWeight_BuyFuel.Text = value.GrossWeight.ToString("F2");
                    txtTicketWeight_BuyFuel.Text = value.TicketWeight.ToString("F2");
                    txtTareWeight_BuyFuel.Text = value.TareWeight.ToString("F2");
                    if (txtKgWeight_BuyFuel.Value == 0) txtKgWeight_BuyFuel.Text = value.KgWeight.ToString("F2");
                    if (txtKsWeight_BuyFuel.Value == 0) txtKsWeight_BuyFuel.Text = value.KsWeight.ToString("F2");
                    txtSuttleWeight_BuyFuel.Text = value.SuttleWeight.ToString("F2");
                }
                else
                {
                    //txt_CarNumber.ResetText();
                    txtGrossWeight_BuyFuel.ResetText();
                    //txtTicketWeight_BuyFuel.Value = 0;
                    txtTareWeight_BuyFuel.ResetText();
                    txtKgWeight_BuyFuel.Value = 0;
                    txtSuttleWeight_BuyFuel.ResetText();
                }
            }
        }

        /// <summary>
        /// ���ؿ��
        /// </summary>
        void LoadMine(ComboBoxEx comboBoxEx)
        {
            comboBoxEx.DisplayMember = "Name";
            comboBoxEx.ValueMember = "Id";
            comboBoxEx.DataSource = Dbers.GetInstance().SelfDber.Entities<CmcsMine>("where Valid='��Ч' and ParentId is not null order by Sequence");
            this.SelectedMine_BuyFuel = comboBoxEx.SelectedItem as CmcsMine;
        }

        /// <summary>
        /// ����ú��
        /// </summary>
        void LoadFuelkind(ComboBoxEx comboBoxEx)
        {
            comboBoxEx.DisplayMember = "FuelName";
            comboBoxEx.ValueMember = "Id";
            comboBoxEx.DataSource = Dbers.GetInstance().SelfDber.Entities<CmcsFuelKind>("where Valid='��Ч' and ParentId is not null order by Sequence");

            this.SelectedFuelKind_BuyFuel = comboBoxEx.SelectedItem as CmcsFuelKind;
        }

        /// <summary>
        /// �����볧ú�����¼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveTransport_BuyFuel_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txt_CarNumber.Text)) { MessageBoxEx.Show("����д���ƺ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            if (this.SelectedMine_BuyFuel == null) { MessageBoxEx.Show("��ѡ����", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            if (this.SelectedFuelKind_BuyFuel == null) { MessageBoxEx.Show("��ѡ��ú��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            if ((decimal)Hardwarer.Wber.Weight <= 0) { MessageBoxEx.Show("��������С��0", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            if (!SaveBuyFuelTransport())
                MessageBoxEx.Show("����ʧ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                MessageBoxEx.Show("����ɹ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ResetBuyFuel();
            }
        }

        /// <summary>
        /// ���������¼
        /// </summary>
        /// <returns></returns>
        bool SaveBuyFuelTransport()
        {
            CmcsAutotruck autoTruck = commonDAO.SelfDber.Entity<CmcsAutotruck>("where CarNumber=:CarNumber", new { CarNumber = this.txt_CarNumber.Text });
            if (autoTruck == null)
            {
                autoTruck = new CmcsAutotruck() { CarNumber = this.txt_CarNumber.Text };
                commonDAO.SelfDber.Insert(autoTruck);
            }
            this.CurrentAutotruck = autoTruck;
            try
            {
                if (this.CurrentBuyFuelTransport == null)
                {
                    // ���Ҹó�δ��ɵ������¼
                    CmcsUnFinishTransport unFinishTransport = carTransportDAO.GetUnFinishTransportByAutotruckId(this.CurrentAutotruck.Id, eCarType.�볡ú.ToString());

                    if (unFinishTransport != null)
                    {
                        this.CurrentBuyFuelTransport = commonDAO.SelfDber.Get<CmcsBuyFuelTransport>(unFinishTransport.TransportId);
                    }
                    else
                    {
                        CmcsBuyFuelTransport transport = new CmcsBuyFuelTransport();
                        QueuerDAO.GetInstance().JoinQueueBuyFuelTransport(autoTruck, this.SelectedMine_BuyFuel, this.SelectedFuelKind_BuyFuel, Convert.ToDecimal(this.txtTicketWeight_BuyFuel.Value), DateTime.Now, "", ref transport);
                        this.CurrentBuyFuelTransport = transport;
                    }
                }
                this.CurrentBuyFuelTransport.KgWeight = (decimal)this.txtKgWeight_BuyFuel.Value;
                this.CurrentBuyFuelTransport.KsWeight = (decimal)this.txtKsWeight_BuyFuel.Value;
                this.CurrentBuyFuelTransport.DeductWeight = (decimal)this.txtKgWeight_BuyFuel.Value + (decimal)this.txtKsWeight_BuyFuel.Value;
                if (weighterDAO.SaveBuyFuelTransportHand(this.CurrentBuyFuelTransport, (decimal)Hardwarer.Wber.Weight, DateTime.Now, CommonAppConfig.GetInstance().AppIdentifier))
                {
                    this.CurrentBuyFuelTransport = commonDAO.SelfDber.Get<CmcsBuyFuelTransport>(this.CurrentBuyFuelTransport.Id);

                    LoadTodayUnFinishBuyFuelTransport();
                    LoadTodayFinishBuyFuelTransport();

                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show("����ʧ��\r\n" + ex.Message, "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Error);

                Log4Neter.Error("���������¼", ex);
            }

            return false;
        }

        /// <summary>
        /// �����볧ú�����¼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_BuyFuel_Click(object sender, EventArgs e)
        {
            ResetBuyFuel();
        }

        /// <summary>
        /// ������Ϣ
        /// </summary>
        void ResetBuyFuel()
        {
            cmbFuelKindName_BuyFuel.SelectedIndex = 0;
            cmbMineName_BuyFuel.SelectedIndex = 0;
            txt_CarNumber.ResetText();
            txtTicketWeight_BuyFuel.Value = 0;
            txtKgWeight_BuyFuel.Value = 0;
            txtKsWeight_BuyFuel.Value = 0;
            this.CurrentAutotruck = null;
            this.CurrentBuyFuelTransport = null;
        }

        /// <summary>
        /// ��ȡδ��ɵ��볧ú��¼
        /// </summary>
        void LoadTodayUnFinishBuyFuelTransport()
        {
            superGridControl1_BuyFuel.PrimaryGrid.DataSource = weighterDAO.GetUnFinishBuyFuelTransport();
        }

        /// <summary>
        /// ��ȡָ����������ɵ��볧ú��¼
        /// </summary>
        void LoadTodayFinishBuyFuelTransport()
        {
            superGridControl2_BuyFuel.PrimaryGrid.DataSource = weighterDAO.GetFinishedBuyFuelTransport(DateTime.Now.Date, DateTime.Now.Date.AddDays(1));
        }

        #endregion

        #region ��������
        /// <summary>
        /// ѡ��ú��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbFuelName_BuyFuel_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SelectedFuelKind_BuyFuel = cmbFuelKindName_BuyFuel.SelectedItem as CmcsFuelKind;
        }

        /// <summary>
        /// ѡ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbMineName_BuyFuel_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SelectedMine_BuyFuel = cmbMineName_BuyFuel.SelectedItem as CmcsMine;
        }

        /// <summary>
        /// Invoke��װ
        /// </summary>
        /// <param name="action"></param>
        public void InvokeEx(Action action)
        {
            if (this.IsDisposed || !this.IsHandleCreated) return;

            this.Invoke(action);
        }

        #region ����ʡ��ѡ��ť

        /// <summary>
        /// ����ʡ�ݼ�ư�ť
        /// </summary>
        private void CreateProvinceAbbreviationButton()
        {
            flpanProvinceAbbreviation.Controls.Clear();

            foreach (CmcsProvinceAbbreviation provinceAbbreviation in CarTransportDAO.GetInstance().GetProvinceAbbreviationsInOrder())
            {
                ButtonX btnProvinceAbbreviation = new ButtonX();
                btnProvinceAbbreviation.Text = provinceAbbreviation.PaName;
                btnProvinceAbbreviation.Style = eDotNetBarStyle.Metro;
                btnProvinceAbbreviation.Font = new Font("΢���ź�", 10.8f, FontStyle.Bold);
                btnProvinceAbbreviation.Size = new Size(26, 26);
                btnProvinceAbbreviation.Margin = new System.Windows.Forms.Padding(3);
                btnProvinceAbbreviation.Click += BtnProvinceAbbreviation_Click;

                flpanProvinceAbbreviation.Controls.Add(btnProvinceAbbreviation);
            }
        }

        /// <summary>
        /// ���ʡ�ݼ�ư�ť
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BtnProvinceAbbreviation_Click(object sender, EventArgs e)
        {
            ButtonX btnProvinceAbbreviation = sender as ButtonX;
            if (btnProvinceAbbreviation != null) CarTransportDAO.GetInstance().AddProvinceAbbreviationUseCount(btnProvinceAbbreviation.Text);

            txt_CarNumber.Text = txt_CarNumber.Text.Insert(0, btnProvinceAbbreviation.Text);
            txt_CarNumber.CloseDropDown();

            txt_CarNumber.Focus();
            txt_CarNumber.Select(txt_CarNumber.Text.Length, 0);
        }

        /// <summary>
        /// ʡ���¼�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txt_CarNumber_ButtonDropDownClick(object sender, CancelEventArgs e)
        {
            CreateProvinceAbbreviationButton();
        }

        #endregion

        private void txt_CarNumber_TextChanged(object sender, EventArgs e)
        {
            Search(this.txt_CarNumber.Text.Trim());
        }

        void Search(string input)
        {
            List<View_UnFinishTransport> list = new List<View_UnFinishTransport>();
            if (!string.IsNullOrEmpty(input))
            {
                list = CarTransportDAO.GetInstance().GetUnFinishTransportByCarNumberAccurate(input.Trim(), " order by CreateDate desc");
                if (list != null && list.Count > 0)
                {
                    this.CurrentBuyFuelTransport = commonDAO.SelfDber.Get<CmcsBuyFuelTransport>(list[0].TransportId);
                }
                else
                {
                    this.CurrentBuyFuelTransport = null;
                }
            }
        }

        /// <summary>
        /// ѡ���Ƥ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectAutotruck_BuyFuel_Click(object sender, EventArgs e)
        {
            FrmUnFinishTransport_Select frm = new FrmUnFinishTransport_Select("where CarType='" + eCarType.�볡ú.ToString() + "' order by CreateDate desc");
            if (frm.ShowDialog() == DialogResult.OK)
            {
                List<View_UnFinishTransport> list = CarTransportDAO.GetInstance().GetUnFinishTransportByCarNumber(frm.Output.CarNumber.Trim(), " order by CreateDate desc");
                if (list != null && list.Count > 0)
                {
                    this.CurrentBuyFuelTransport = commonDAO.SelfDber.Get<CmcsBuyFuelTransport>(list[0].TransportId);
                }
                else
                {
                    this.CurrentBuyFuelTransport = null;
                }
            }
        }

        /// <summary>
        /// ��ӡ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiPrint_Click(object sender, EventArgs e)
        {
            GridRow gridRow = superGridControl2_BuyFuel.PrimaryGrid.ActiveRow as GridRow;
            if (gridRow == null) return;
            View_BuyFuelTransport entity = gridRow.DataItem as View_BuyFuelTransport;
            CmcsBuyFuelTransport entity2 = Dbers.GetInstance().SelfDber.Get<CmcsBuyFuelTransport>(entity.Id);
            FrmPrintWeb frm = new FrmPrintWeb(entity2);
            frm.ShowDialog();
        }
        #endregion

        #region DataGridView
        private void superGridControl_BeginEdit(object sender, DevComponents.DotNetBar.SuperGrid.GridEditEventArgs e)
        {
            // ȡ������༭
            e.Cancel = true;
        }

        /// <summary>
        /// �����к�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void superGridControl_GetRowHeaderText(object sender, DevComponents.DotNetBar.SuperGrid.GridGetRowHeaderTextEventArgs e)
        {
            e.Text = (e.GridRow.RowIndex + 1).ToString();
        }

        private void superGridControl1_DataBindingComplete(object sender, DevComponents.DotNetBar.SuperGrid.GridDataBindingCompleteEventArgs e)
        {
            foreach (GridRow gridRow in e.GridPanel.Rows)
            {
                View_BuyFuelTransport entity = gridRow.DataItem as View_BuyFuelTransport;
                if (entity == null) return;
                CmcsBuyFuelTransport entity2 = Dbers.GetInstance().SelfDber.Get<CmcsBuyFuelTransport>(entity.Id);
                gridRow.Cells["clmKsWeight"].Value = entity2.AutoKsWeight + entity2.KsWeight;
            }
        }

        #endregion
        /// <summary>
        /// ˢ������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefres_Click(object sender, EventArgs e)
        {
            LoadFuelkind(cmbFuelKindName_BuyFuel);
            LoadMine(cmbMineName_BuyFuel);
            LoadTodayUnFinishBuyFuelTransport();
            LoadTodayFinishBuyFuelTransport();
        }
    }
}
