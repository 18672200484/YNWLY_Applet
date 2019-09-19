using System;
using System.Windows.Forms;
using System.Linq;
//
using DevComponents.DotNetBar.Metro;
using System.Collections.Generic;
using CMCS.Common.Entities.CarTransport;
using CMCS.Common;
using DevComponents.DotNetBar.SuperGrid;
using DevComponents.DotNetBar;
using CMCS.Common.Entities.Fuel;
using CMCS.Common.Enums;
using CMCS.Common.Utilities;
using CMCS.Common.Entities.BaseInfo;
using DevComponents.DotNetBar.Controls;
using CMCS.CarTransport.DAO;
using CMCS.Common.DAO;
using CMCS.MobilePad.Win.Core;
using System.Data;
using CMCS.Common.Views;
using System.Drawing;
using CMCS.CarTransport.Queue.Enums;

namespace CMCS.MobilePad.Win.Frms.Queue
{
    public partial class FrmMobileQueue : MetroAppForm
    {
        /// <summary>
        /// ����Ψһ��ʶ��
        /// </summary>
        public static string UniqueKey = "FrmMobileQueue";

        public List<FulUnLoadPlanDetail> CurrentList;
        string SqlWhere = string.Empty;

        public FrmMobileQueue()
        {
            InitializeComponent();
        }

        private void FrmHome_Load(object sender, EventArgs e)
        {
            this.CurrentList = new List<FulUnLoadPlanDetail>();
            InitFrom();
        }

        public void InitFrom()
        {
            //����ú��
            LoadFuelkind(new ComboBoxEx[] { cmbFuelName_BuyFuel, cmbFuelName_SaleFuel });
            //���ز�����ʽ
            LoadSampleType(new ComboBoxEx[] { cmbSamplingType_BuyFuel, cmbSamplingType_SaleFuel });
            //�����볧ú����
            LoadBuyFuelType(new ComboBoxEx[] { cmbBuyFuelType });
            //���س���ú����
            LoadSaleType(new ComboBoxEx[] { cmbSalesType });
            //���س�Ʒ��
            LoadCPC(new ComboBoxEx[] { cmb_CPC });
            try
            {
                this.IsUseYB = commonDAO.GetAppletConfigBoolen("�Ƿ�����Ԥ��");
            }
            catch { }
            timer3_Tick(null, null);
        }

        #region ҵ������
        CarTransportDAO carTransportDAO = CarTransportDAO.GetInstance();
        QueuerDAO queuerDAO = QueuerDAO.GetInstance();
        CommonDAO commonDAO = CommonDAO.GetInstance();
        #endregion

        #region ����Vars

        private string queueType;
        /// <summary>
        /// �Ŷ�����
        /// </summary>
        public string QueueType
        {
            get { return queueType; }
            set
            {
                queueType = value;
                if (value == eTransportType.ԭ��ú�볡.ToString() || value == eTransportType.�ִ�ú�볡.ToString() || value == eTransportType.��תú�볡.ToString())
                    this.superTabControl2.SelectedTab = superTabItem_BuyFuel;
                else if (value == eTransportType.�ִ�ú����.ToString() || value == eTransportType.��תú����.ToString() || value == eTransportType.���۲���ú.ToString() || value == eTransportType.����ֱ��ú.ToString())
                    this.superTabControl2.SelectedTab = superTabItem_SaleFuel;
                else if (value == eTransportType.��������.ToString())
                    this.superTabControl2.SelectedTab = superTabItem_Goods;
                else if (value == eTransportType.���ó���.ToString())
                    this.superTabControl2.SelectedTab = superTabItem_Visit;
            }
        }

        #region Vars1
        public static PassCarQueuer passCarQueuer = new PassCarQueuer();

        ImperfectCar currentImperfectCar;
        /// <summary>
        /// ʶ���ѡ��ĳ���ƾ֤
        /// </summary>
        public ImperfectCar CurrentImperfectCar
        {
            get { return currentImperfectCar; }
            set
            {
                currentImperfectCar = value;
            }
        }

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
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.��ǰ��Id.ToString(), value.Id);
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.��ǰ����.ToString(), value.CarNumber);

                    if (superTabControl2.SelectedTab == superTabItem_BuyFuel)
                        txtCarNumber_BuyFuel.Text = value.CarNumber;
                    else if (superTabControl2.SelectedTab == superTabItem_SaleFuel)
                        txtCarNumber_SaleFuel.Text = value.CarNumber;
                    else if (superTabControl2.SelectedTab == superTabItem_Goods)
                        txtCarNumber_Goods.Text = value.CarNumber;
                    else if (superTabControl2.SelectedTab == superTabItem_Visit)
                        txtCarNumber_Visit.Text = value.CarNumber;
                }
                else
                {
                    //���ó�����Ϣ
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.��ǰ��Id.ToString(), string.Empty);
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.��ǰ����.ToString(), string.Empty);

                    txtCarNumber_BuyFuel.ResetText();
                    txtCarNumber_SaleFuel.ResetText();
                    txtCarNumber_Goods.ResetText();
                    txtCarNumber_Visit.ResetText();
                }
            }
        }

        private string carnumber1;
        /// <summary>
        /// ��ǰ����1
        /// </summary>
        public string CarNumber1
        {
            get { return carnumber1; }
            set
            {
                carnumber1 = value;
            }
        }

        private eCarType currentCarType;
        /// <summary>
        /// ��ǰ������
        /// </summary>
        public eCarType CurrentCarType
        {
            get { return currentCarType; }
            set
            {
                currentCarType = value;
            }
        }

        private bool isUseYB = false;
        /// <summary>
        /// �Ƿ�����Ԥ��
        /// </summary>
        public bool IsUseYB
        {
            get { return isUseYB; }
            set
            {
                isUseYB = value;

                //�볡ú
                btnSelectTransportCompany_BuyFuel.Enabled = !value;
                btnSelectSupplier_BuyFuel.Enabled = !value;
                btnSelectMine_BuyFuel.Enabled = !value;
                cmbFuelName_BuyFuel.Enabled = !value;
                cmbSamplingType_BuyFuel.Enabled = !value;
                cmbBuyFuelType.Enabled = !value;

                //����ú
                btnSelectTransportCompany_SaleFuel.Enabled = !value;
                btnSelectSupplyReceive_SaleFuel.Enabled = !value;
                cmbFuelName_SaleFuel.Enabled = !value;
                cmbSalesType.Enabled = !value;
                cmbSamplingType_SaleFuel.Enabled = !value;
                //cmb_CPC.Enabled = !value;
                //cmb_Storage.Enabled = !value;
            }
        }

        #endregion

        #endregion

        #region ����ҵ��

        /// <summary>
        /// ��������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer3_Tick(object sender, EventArgs e)
        {
            try
            {
                // �볧ú
                LoadTodayUnFinishBuyFuelTransport();
                LoadTodayFinishBuyFuelTransport();
                LoadTitleBuyFuelTransport();
                // ����ú 
                LoadTodayUnFinishSaleFuelTransport();
                LoadTodayFinishSaleFuelTransport();

                // ��������
                LoadTodayUnFinishGoodsTransport();
                LoadTodayFinishGoodsTransport();

                // ���ó���
                LoadTodayUnFinishVisitTransport();
                LoadTodayFinishVisitTransport();
            }
            catch (Exception ex)
            {
                Log4Neter.Error("timer3_Tick", ex);
            }
            finally
            {
            }
        }

        /// <summary>
        /// ����ú��
        /// </summary>
        void LoadFuelkind(ComboBoxEx[] comboBoxEx)
        {
            IList<CmcsFuelKind> FuelKindList = Dbers.GetInstance().SelfDber.Entities<CmcsFuelKind>("where Valid='��Ч' and ParentId is not null");
            for (int i = 0; i < comboBoxEx.Length; i++)
            {
                comboBoxEx[i].DisplayMember = "FuelName";
                comboBoxEx[i].ValueMember = "Id";
                comboBoxEx[i].DataSource = FuelKindList;
            }
        }

        /// <summary>
        /// ���ز�����ʽ
        /// </summary>
        void LoadSampleType(ComboBoxEx[] comboBoxEx)
        {
            List<CMCS.Common.Entities.iEAA.CodeContent> contentList = commonDAO.GetCodeContentByKind("������ʽ");
            for (int i = 0; i < comboBoxEx.Length; i++)
            {
                comboBoxEx[i].DisplayMember = "Content";
                comboBoxEx[i].ValueMember = "Code";
                comboBoxEx[i].DataSource = contentList;

                comboBoxEx[i].Text = eSamplingType.��е����.ToString();
            }
        }

        /// <summary>
        /// �����볧ú����
        /// </summary>
        void LoadBuyFuelType(ComboBoxEx[] comboBoxEx)
        {
            for (int i = 0; i < comboBoxEx.Length; i++)
            {
                comboBoxEx[i].Items.Add(new ComboBoxItem(eTransportType.ԭ��ú�볡.ToString(), eTransportType.ԭ��ú�볡.ToString()));
                comboBoxEx[i].Items.Add(new ComboBoxItem(eTransportType.�ִ�ú�볡.ToString(), eTransportType.�ִ�ú�볡.ToString()));
                comboBoxEx[i].Items.Add(new ComboBoxItem(eTransportType.��תú�볡.ToString(), eTransportType.��תú�볡.ToString()));
                comboBoxEx[i].SelectedIndex = -1;
            }
        }

        /// <summary>
        /// ���س���ú����
        /// </summary>
        void LoadSaleType(ComboBoxEx[] comboBoxEx)
        {
            for (int i = 0; i < comboBoxEx.Length; i++)
            {
                comboBoxEx[i].Items.Add(new ComboBoxItem(eTransportType.�ִ�ú����.ToString(), eTransportType.�ִ�ú����.ToString()));
                comboBoxEx[i].Items.Add(new ComboBoxItem(eTransportType.��תú����.ToString(), eTransportType.��תú����.ToString()));
                comboBoxEx[i].Items.Add(new ComboBoxItem(eTransportType.����ֱ��ú.ToString(), eTransportType.����ֱ��ú.ToString()));
                comboBoxEx[i].Items.Add(new ComboBoxItem(eTransportType.���۲���ú.ToString(), eTransportType.���۲���ú.ToString()));
                comboBoxEx[i].SelectedIndex = -1;
            }
        }

        /// <summary>
        /// ���س�Ʒ��
        /// </summary>
        void LoadCPC(ComboBoxEx[] comboBoxEx)
        {
            DataTable data = commonDAO.SelfDber.ExecuteDataTable("select Id,PotName from fultbcoalpot where PotName like '%��Ʒ��%' order by PotName");
            if (data != null && data.Rows.Count > 0)
            {
                for (int i = 0; i < comboBoxEx.Length; i++)
                {
                    comboBoxEx[i].Items.Clear();
                    foreach (DataRow item in data.Rows)
                    {
                        comboBoxEx[i].Items.Add(new ComboBoxItem(item["Id"].ToString(), item["PotName"].ToString()));
                    }
                    comboBoxEx[i].SelectedIndex = -1;
                }
            }
        }

        /// <summary>
        /// �����Զ�������
        /// </summary>
        void LoadSource(ComboBoxEx[] comboBoxEx, string dataSourceValue, string dataSourceText, char split = '|', char split2 = '|')
        {
            string[] dataValue = dataSourceValue.Split(split);
            string[] dataText = dataSourceText.Split(split2);
            for (int i = 0; i < comboBoxEx.Length; i++)
            {
                comboBoxEx[i].Items.Clear();
                for (int j = 0; j < dataValue.Length; j++)
                {
                    comboBoxEx[i].Items.Add(new ComboBoxItem(dataValue[j], dataText[j]));
                }
                comboBoxEx[i].SelectedIndex = 0;
            }
        }

        #endregion

        #region �볡úҵ��

        #region Vars1

        private CmcsSupplier selectedSupplier_BuyFuel;
        /// <summary>
        /// ѡ��Ĺ�ú��λ
        /// </summary>
        public CmcsSupplier SelectedSupplier_BuyFuel
        {
            get { return selectedSupplier_BuyFuel; }
            set
            {
                selectedSupplier_BuyFuel = value;

                if (value != null)
                {
                    txtSupplierName_BuyFuel.Text = value.Name;
                }
                else
                {
                    txtSupplierName_BuyFuel.ResetText();
                }
            }
        }

        private CmcsTransportCompany selectedTransportCompany_BuyFuel;
        /// <summary>
        /// ѡ������䵥λ
        /// </summary>
        public CmcsTransportCompany SelectedTransportCompany_BuyFuel
        {
            get { return selectedTransportCompany_BuyFuel; }
            set
            {
                selectedTransportCompany_BuyFuel = value;

                if (value != null)
                {
                    txtTransportCompanyName_BuyFuel.Text = value.Name;
                }
                else
                {
                    txtTransportCompanyName_BuyFuel.ResetText();
                }
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

                if (value != null)
                {
                    txtMineName_BuyFuel.Text = value.Name;
                }
                else
                {
                    txtMineName_BuyFuel.ResetText();
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
                if (value != null)
                {
                    selectedFuelKind_BuyFuel = value;
                    cmbFuelName_BuyFuel.Text = value.FuelName;
                }
                else
                {
                    cmbFuelName_BuyFuel.SelectedIndex = 0;
                }
            }
        }

        private CmcsLMYB selectedLMYB_BuyFuel;
        /// <summary>
        /// ѡ�����úԤ��
        /// </summary>
        public CmcsLMYB SelectedLMYB_BuyFuel
        {
            get { return selectedLMYB_BuyFuel; }
            set
            {
                selectedLMYB_BuyFuel = value;
                if (this.CurrentAutotruck != null) txtCarNumber_BuyFuel.Text = this.CurrentAutotruck.CarNumber;
                try
                {
                    if (value != null)
                    {
                        this.SelectedFuelKind_BuyFuel = commonDAO.SelfDber.Get<CmcsFuelKind>(value.FuelKindId);
                        this.SelectedMine_BuyFuel = commonDAO.SelfDber.Get<CmcsMine>(value.MineId);
                        this.SelectedSupplier_BuyFuel = commonDAO.SelfDber.Get<CmcsSupplier>(value.SupplierId);
                        this.SelectedTransportCompany_BuyFuel = commonDAO.SelfDber.Get<CmcsTransportCompany>(value.TransportCompanyId);
                        this.cmbBuyFuelType.Text = value.InFactoryType;
                        this.cmbSamplingType_BuyFuel.Text = value.SamplingType;
                    }
                    else
                    {
                        //this.SelectedFuelKind_BuyFuel = null;
                        //this.SelectedMine_BuyFuel = null;
                        //this.SelectedSupplier_BuyFuel = null;
                        //this.SelectedTransportCompany_BuyFuel = null;
                    }
                }
                catch { }
            }
        }

        #endregion

        #region ѡ���¼�

        /// <summary>
        /// ѡ��ú��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbFuelName_BuyFuel_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SelectedFuelKind_BuyFuel = cmbFuelName_BuyFuel.SelectedItem as CmcsFuelKind;
        }

        /// <summary>
        /// ѡ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectAutotruck_BuyFuel_Click(object sender, EventArgs e)
        {
            FrmMobileAutotruck_Select frm = new FrmMobileAutotruck_Select(" and IsUse=1 order by CarNumber asc");
            if (frm.ShowDialog() == DialogResult.OK)
            {
                //passCarQueuer.Enqueue(ePassWay.Way1, frm.Output.CarNumber, false);
                //���豸����� ֱ����֤
                this.CurrentAutotruck = carTransportDAO.GetAutotruckByCarNumber(frm.Output.CarNumber);
            }
        }

        /// <summary>
        /// ѡ��ú��λ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectSupplier_BuyFuel_Click(object sender, EventArgs e)
        {
            FrmMobileSupplier_Select frm = new FrmMobileSupplier_Select("where Valid='��Ч' order by Name asc");
            if (frm.ShowDialog() == DialogResult.OK)
            {
                this.SelectedSupplier_BuyFuel = frm.Output;
            }
        }

        /// <summary>
        /// ѡ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectMine_BuyFuel_Click(object sender, EventArgs e)
        {
            FrmMobileMine_Select frm = new FrmMobileMine_Select("where Valid='��Ч' order by Name asc");
            if (frm.ShowDialog() == DialogResult.OK)
            {
                this.SelectedMine_BuyFuel = frm.Output;
            }
        }

        /// <summary>
        /// ѡ�����䵥λ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectTransportCompany_BuyFuel_Click(object sender, EventArgs e)
        {
            FrmMobileTransportCompany_Select frm = new FrmMobileTransportCompany_Select("where IsUse=1 order by Name asc");
            if (frm.ShowDialog() == DialogResult.OK)
            {
                this.SelectedTransportCompany_BuyFuel = frm.Output;
            }
        }

        /// <summary>
        /// �³��Ǽ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNewAutotruck_BuyFuel_Click(object sender, EventArgs e)
        {
            new FrmCarManage_Confirm("").Show();
        }

        /// <summary>
        /// ѡ���볧ú��úԤ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectForecast_BuyFuel_Click(object sender, EventArgs e)
        {
            FrmMobileBuyFuelForecast_Select frm = new FrmMobileBuyFuelForecast_Select(string.Format("where (InFactoryType='{0}' or InFactoryType='{1}' or InFactoryType='{2}') ", eTransportType.ԭ��ú�볡, eTransportType.�ִ�ú�볡, eTransportType.��תú�볡));
            if (frm.ShowDialog() == DialogResult.OK)
            {
                this.SelectedLMYB_BuyFuel = frm.Output;
            }
        }

        #endregion

        #region ���������¼

        /// <summary>
        /// �����볧ú�����¼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveTransport_BuyFuel_Click(object sender, EventArgs e)
        {
            SaveBuyFuelTransport();
        }

        /// <summary>
        /// ���������¼
        /// </summary>
        /// <returns></returns>
        bool SaveBuyFuelTransport()
        {
            //if (IsUseYB && this.SelectedLMYB_BuyFuel == null)
            //{
            //    MessageBoxEx.Show("��ѡ��Ԥ����Ϣ", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return false;
            //}

            if (this.CurrentAutotruck == null)
            {
                MessageBoxEx.Show("��ѡ����", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrEmpty(this.cmbFuelName_BuyFuel.Text))
            {
                MessageBoxEx.Show("��ѡ��ú��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (this.SelectedMine_BuyFuel == null)
            {
                MessageBoxEx.Show("��ѡ����", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (this.SelectedSupplier_BuyFuel == null)
            {
                MessageBoxEx.Show("��ѡ��ú��λ", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (this.SelectedTransportCompany_BuyFuel == null)
            {
                MessageBoxEx.Show("��ѡ�����䵥λ", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            //if (txtTicketWeight_BuyFuel.Value <= 0)
            //{
            //    MessageBoxEx.Show("��������Ч�Ŀ���", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return false;
            //}
            if (string.IsNullOrEmpty(cmbBuyFuelType.Text))
            {
                MessageBoxEx.Show("��ѡ���볡ú����", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            try
            {
                // �����볧ú�ŶӼ�¼��ͬʱ����������Ϣ�Լ����ƻ���������
                if (queuerDAO.JoinQueueBuyFuelTransport(this.CurrentAutotruck, this.SelectedSupplier_BuyFuel, this.SelectedMine_BuyFuel, this.SelectedTransportCompany_BuyFuel, this.cmbFuelName_BuyFuel.SelectedItem as CmcsFuelKind, (decimal)txtTicketWeight_BuyFuel.Value, DateTime.Now, txtRemark_BuyFuel.Text, cmbSamplingType_BuyFuel.Text, this.SelectedLMYB_BuyFuel, cmbBuyFuelType.Text))
                {
                    btnSaveTransport_BuyFuel.Enabled = false;

                    MessageBoxEx.Show("�Ŷӳɹ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadTodayUnFinishBuyFuelTransport();
                    LoadTodayFinishBuyFuelTransport();

                    ResetBuyFuel();
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

        #endregion

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
            this.CurrentAutotruck = null;
            this.SelectedLMYB_BuyFuel = null;
            //this.SelectedMine_BuyFuel = null;
            //this.SelectedSupplier_BuyFuel = null;
            //this.SelectedTransportCompany_BuyFuel = null;

            txtTicketWeight_BuyFuel.Value = 0;
            txtRemark_BuyFuel.ResetText();

            btnSaveTransport_BuyFuel.Enabled = true;
            // �������
            this.CurrentImperfectCar = null;
        }

        /// <summary>
        /// ��ȡδ��ɵ��볧ú��¼
        /// </summary>
        void LoadTodayUnFinishBuyFuelTransport()
        {
            superGridControl1_BuyFuel.PrimaryGrid.DataSource = queuerDAO.GetUnFinishBuyFuelTransport();
        }

        /// <summary>
        /// ��ȡָ����������ɵ��볧ú��¼
        /// </summary>
        void LoadTodayFinishBuyFuelTransport()
        {
            superGridControl2_BuyFuel.PrimaryGrid.DataSource = queuerDAO.GetFinishedBuyFuelTransport(DateTime.Now.Date, DateTime.Now.Date.AddDays(1));
        }

        void LoadTitleBuyFuelTransport()
        {
            List<CmcsBuyFuelTransport> tran = queuerDAO.GetBuyFuelTransportByDate(DateTime.Now.Date, DateTime.Now.Date.AddDays(1));
            txtTitle_BuyFuel.Text = string.Format("�ѵǼ�������{0}   �ѳ��أ�{1}  δ���أ�{2}   �ѳ���δ��Ƥ��{3}   �ѻ�Ƥ��{4}", tran.Count(), tran.Where(a => a.GrossWeight > 0).Count(), tran.Where(a => a.GrossWeight == 0).Count(), tran.Where(a => a.GrossWeight > 0 && a.TareWeight == 0).Count(), tran.Where(a => a.GrossWeight > 0 && a.TareWeight > 0).Count());
        }

        #region DataGridView

        /// <summary>
        /// ˫����ʱ���Զ���乩ú��λ��������Ϣ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void superGridControl_BuyFuel_CellDoubleClick(object sender, DevComponents.DotNetBar.SuperGrid.GridCellDoubleClickEventArgs e)
        {
            GridRow gridRow = (sender as SuperGridControl).PrimaryGrid.ActiveRow as GridRow;
            if (gridRow == null) return;

            View_BuyFuelTransport entity = (gridRow.DataItem as View_BuyFuelTransport);
            if (entity != null)
            {
                this.SelectedFuelKind_BuyFuel = commonDAO.SelfDber.Get<CmcsFuelKind>(entity.FuelKindId);
                this.SelectedMine_BuyFuel = commonDAO.SelfDber.Get<CmcsMine>(entity.MineId);
                this.SelectedSupplier_BuyFuel = commonDAO.SelfDber.Get<CmcsSupplier>(entity.SupplierId);
                this.SelectedTransportCompany_BuyFuel = commonDAO.SelfDber.Get<CmcsTransportCompany>(entity.TransportCompanyId);

                cmbSamplingType_BuyFuel.Text = entity.SamplingType;
            }
        }

        private void superGridControl1_BuyFuel_CellClick(object sender, GridCellClickEventArgs e)
        {
            View_BuyFuelTransport entity = e.GridCell.GridRow.DataItem as View_BuyFuelTransport;
            if (entity == null) return;

            // ������Ч״̬
            if (e.GridCell.GridColumn.Name == "ChangeIsUse") queuerDAO.ChangeBuyFuelTransportToInvalid(entity.Id, Convert.ToBoolean(e.GridCell.Value));
        }

        private void superGridControl1_BuyFuel_DataBindingComplete(object sender, GridDataBindingCompleteEventArgs e)
        {
            foreach (GridRow gridRow in e.GridPanel.Rows)
            {
                View_BuyFuelTransport entity = gridRow.DataItem as View_BuyFuelTransport;
                if (entity == null) return;

                // �����Ч״̬
                gridRow.Cells["ChangeIsUse"].Value = Convert.ToBoolean(entity.IsUse);
                if (entity.GrossWeight > 0 && entity.TareWeight > 0)
                    gridRow.CellStyles.Default.TextColor = Color.Green;
                else if (entity.GrossWeight == 0 && entity.TareWeight == 0)
                    gridRow.CellStyles.Default.TextColor = Color.Red;
            }
        }

        private void superGridControl2_BuyFuel_CellClick(object sender, GridCellClickEventArgs e)
        {
            View_BuyFuelTransport entity = e.GridCell.GridRow.DataItem as View_BuyFuelTransport;
            if (entity == null) return;

            // ������Ч״̬
            if (e.GridCell.GridColumn.Name == "ChangeIsUse") queuerDAO.ChangeBuyFuelTransportToInvalid(entity.Id, Convert.ToBoolean(e.GridCell.Value));
        }

        private void superGridControl2_BuyFuel_DataBindingComplete(object sender, GridDataBindingCompleteEventArgs e)
        {
            foreach (GridRow gridRow in e.GridPanel.Rows)
            {
                View_BuyFuelTransport entity = gridRow.DataItem as View_BuyFuelTransport;
                if (entity == null) return;

                // �����Ч״̬
                gridRow.Cells["ChangeIsUse"].Value = Convert.ToBoolean(entity.IsUse);
                if (entity.GrossWeight > 0 && entity.TareWeight > 0)
                    gridRow.CellStyles.Default.TextColor = Color.Green;
                else if (entity.GrossWeight == 0 && entity.TareWeight == 0)
                    gridRow.CellStyles.Default.TextColor = Color.Red;
            }
        }

        #endregion

        #endregion

        #region ����úҵ��

        #region Var1

        private CmcsLMYB selectedCmcsTransportSales;
        List<String> StorageNames = new List<string>();
        /// <summary>
        /// ѡ�������úԤ��
        /// </summary>
        public CmcsLMYB SelectedCmcsTransportSales
        {
            get { return selectedCmcsTransportSales; }
            set
            {
                selectedCmcsTransportSales = value;

                if (value != null)
                {
                    if (this.CurrentAutotruck != null) this.txtCarNumber_SaleFuel.Text = this.CurrentAutotruck.CarNumber;
                    this.txt_YBNumber1.Text = value.YbNum;
                    this.txt_Consignee1.Text = value.SupplierName;
                    this.SelectedReceive_SaleFuel = commonDAO.SelfDber.Get<CmcsSupplier>(value.SupplierId);
                    this.SelectedTransportCompany_SaleFuel = Dbers.GetInstance().SelfDber.Get<CmcsTransportCompany>(value.TransportCompanyId);
                    this.cmbFuelName_SaleFuel.Text = value.FuelKindName;
                    this.cmbSalesType.Text = value.InFactoryType;
                    this.cmbSamplingType_SaleFuel.Text = value.SamplingType;
                    if (value.InFactoryType == eTransportType.����ֱ��ú.ToString() || value.InFactoryType == eTransportType.���۲���ú.ToString())
                    {
                        LoadSource(new ComboBoxEx[] { cmb_CPC }, value.CPCId, value.CPCName);
                        LoadSource(new ComboBoxEx[] { cmb_Storage }, value.StorageId, value.StorageName);
                    }
                    ChangeCPCVisible(value.InFactoryType);
                }
                else
                {
                    this.txt_YBNumber1.ResetText();
                    this.txt_Consignee1.ResetText();
                }
            }
        }

        private CmcsSupplier selectedReceive_SaleFuel;
        /// <summary>
        /// ѡ����ջ���λ
        /// </summary>
        public CmcsSupplier SelectedReceive_SaleFuel
        {
            get { return selectedReceive_SaleFuel; }
            set
            {
                selectedReceive_SaleFuel = value;

                if (value != null)
                {
                    txt_Consignee1.Text = value.Name;
                }
                else
                {
                    txt_Consignee1.ResetText();
                }
            }
        }

        private CmcsTransportCompany selectedTransportCompany_SaleFuel;
        /// <summary>
        /// ѡ������䵥λ
        /// </summary>
        public CmcsTransportCompany SelectedTransportCompany_SaleFuel
        {
            get { return selectedTransportCompany_SaleFuel; }
            set
            {
                selectedTransportCompany_SaleFuel = value;

                if (value != null)
                {
                    txt_TransportCompayName1.Text = value.Name;
                }
                else
                {
                    txt_TransportCompayName1.ResetText();
                }
            }
        }

        void ChangeCPCVisible(string outFactoryType)
        {
            if (outFactoryType == eTransportType.���۲���ú.ToString() || outFactoryType == eTransportType.����ֱ��ú.ToString())
            {
                lab_CPC.Visible = true;
                lab_Storage.Visible = true;
                cmb_Storage.Visible = true;
                cmb_CPC.Visible = true;
            }
            else
            {
                lab_CPC.Visible = false;
                lab_Storage.Visible = false;
                cmb_Storage.Visible = false;
                cmb_CPC.Visible = false;
            }
        }
        #endregion

        #region �¼�

        private void cmbSalesType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeCPCVisible(cmbSalesType.Text);
        }
        #endregion

        #region ��������

        /// <summary>
        /// ѡ����úԤ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectForecast_SaleFuel_Click(object sender, EventArgs e)
        {
            FrmMobileSaleFuelForecast_Select frm = new FrmMobileSaleFuelForecast_Select(string.Format(" where (InFactoryType='{0}' or InFactoryType='{1}' or InFactoryType='{2}' or InFactoryType='{3}')", eTransportType.�ִ�ú����.ToString(), eTransportType.��תú����.ToString(), eTransportType.���۲���ú.ToString(), eTransportType.����ֱ��ú.ToString()));
            if (frm.ShowDialog() == DialogResult.OK)
            {
                SelectedCmcsTransportSales = frm.Output;
            }
        }

        /// <summary>
        /// ѡ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectAutotruck_SaleFuel_Click(object sender, EventArgs e)
        {
            FrmMobileAutotruck_Select frm = new FrmMobileAutotruck_Select(" and IsUse=1 order by CarNumber asc");
            if (frm.ShowDialog() == DialogResult.OK)
            {
                //passCarQueuer.Enqueue(ePassWay.Way1, frm.Output.CarNumber, false);
                this.CurrentAutotruck = frm.Output;
            }
        }

        /// <summary>
        /// ѡ�����䵥λ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectTransportCompany_SaleFuel_Click(object sender, EventArgs e)
        {
            FrmMobileTransportCompany_Select frm = new FrmMobileTransportCompany_Select("where IsUse=1 order by Name asc");
            if (frm.ShowDialog() == DialogResult.OK)
            {
                this.SelectedTransportCompany_SaleFuel = frm.Output;
            }
        }

        /// <summary>
        /// ѡ���ջ���λ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectSupplyReceive_SaleFuel_Click(object sender, EventArgs e)
        {
            FrmMobileSupplier_Select frm = new FrmMobileSupplier_Select("where Valid='��Ч' order by Name asc");
            if (frm.ShowDialog() == DialogResult.OK)
            {
                this.SelectedReceive_SaleFuel = frm.Output;
            }
        }

        /// <summary>
        /// �³��Ǽ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNewAutotruck_SaleFuel_Click(object sender, EventArgs e)
        {
            new FrmCarManage_Confirm("").Show();
        }

        /// <summary>
        /// ��Ʒ�ֵ�ѡ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void CheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if ((sender as CheckBoxX).Checked == true)
            {
                foreach (CheckBoxX chk in (sender as CheckBoxX).Parent.Controls)
                {
                    if (chk != sender)
                    {
                        chk.Checked = false;
                    }
                }
            }
        }

        /// <summary>
        /// ����δ��������¼
        /// </summary>
        void LoadTodayUnFinishSaleFuelTransport()
        {
            superGridControl1_SaleFuel.PrimaryGrid.DataSource = queuerDAO.GetUnFinishSaleFuelTransport();
        }

        /// <summary>
        /// ���ؽ�������������¼
        /// </summary>
        void LoadTodayFinishSaleFuelTransport()
        {
            superGridControl2_SaleFuel.PrimaryGrid.DataSource = queuerDAO.GetFinishedSaleFuelTransport(DateTime.Now.Date, DateTime.Now.Date.AddDays(1));
        }

        #endregion

        #region ���������¼

        /// <summary>
        /// �Ǽ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveTransport_SaleFuel_Click(object sender, EventArgs e)
        {
            SaveSaleFuelTransport();
        }

        /// <summary>
        /// ���������¼
        /// </summary>
        /// <returns></returns>
        bool SaveSaleFuelTransport()
        {
            if (this.CurrentAutotruck == null)
            {
                MessageBoxEx.Show("��ѡ����", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (this.SelectedTransportCompany_SaleFuel == null)
            {
                MessageBoxEx.Show("��ѡ�����䵥λ", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (this.SelectedReceive_SaleFuel == null)
            {
                MessageBoxEx.Show("��ѡ���ջ���λ", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrEmpty(this.cmbFuelName_SaleFuel.Text))
            {
                MessageBoxEx.Show("��ѡ��ú��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            //if (this.SelectedCmcsTransportSales == null)
            //{
            //    MessageBoxEx.Show("��ѡ������ú����", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return false;
            //}
            //if (string.IsNullOrEmpty(cmbCPC.Text))
            //{
            //    MessageBoxEx.Show("��ѡ���Ӧ��Ʒ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return false;
            //}
            if (string.IsNullOrEmpty(cmbSalesType.Text))
            {
                MessageBoxEx.Show("��ѡ���������", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            try
            {
                ComboBoxItem storageItem = (ComboBoxItem)cmb_Storage.SelectedItem;
                ComboBoxItem cPCItem = (ComboBoxItem)cmb_CPC.SelectedItem;
                // ��������ú�ŶӼ�¼ ͬʱ����������Ϣ
                if (queuerDAO.JoinQueueSaleFuelTransport(this.CurrentAutotruck, this.SelectedCmcsTransportSales, this.SelectedReceive_SaleFuel, this.SelectedTransportCompany_SaleFuel, (this.cmbFuelName_SaleFuel.SelectedItem as CmcsFuelKind), DateTime.Now, txt_ReMark1.Text, CommonAppConfig.GetInstance().AppIdentifier, cmb_CPC.Text, cmbSalesType.Text, cmbSamplingType_SaleFuel.Text, new Tuple<string, string>(cPCItem != null ? cPCItem.Name : "", cPCItem != null ? cPCItem.Text : ""), new Tuple<string, string>(storageItem != null ? storageItem.Name : "", storageItem != null ? storageItem.Text : "")))
                {
                    btnSaveTransport_SaleFuel.Enabled = false;

                    MessageBoxEx.Show("�Ŷӳɹ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadTodayUnFinishSaleFuelTransport();
                    LoadTodayFinishSaleFuelTransport();

                    ResetSaleFuel();
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

        #endregion

        #region ����

        private void btnReset_SaleFuel_Click(object sender, EventArgs e)
        {
            ResetSaleFuel();
        }

        void ResetSaleFuel()
        {

            this.CurrentAutotruck = null;

            //this.SelectedCmcsTransportSales = null;

            txtRemark_BuyFuel.ResetText();

            btnSaveTransport_SaleFuel.Enabled = true;

            // �������
            this.CurrentImperfectCar = null;
            LoadCPC(new ComboBoxEx[] { cmb_CPC });
            cmb_Storage.Items.Clear();
        }

        #endregion

        #region DataGridView

        /// <summary>
        /// ˫����ʱ���Զ���乩ú��λ��������Ϣ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void superGridControl2_SaleFuel_CellDoubleClick(object sender, DevComponents.DotNetBar.SuperGrid.GridCellDoubleClickEventArgs e)
        {
            GridRow gridRow = (sender as SuperGridControl).PrimaryGrid.ActiveRow as GridRow;
            if (gridRow == null) return;

            View_SaleFuelTransport entity = (gridRow.DataItem as View_SaleFuelTransport);
            if (entity != null)
            {
                this.cmbFuelName_SaleFuel.Text = entity.FuelKindName;
                this.SelectedReceive_SaleFuel = commonDAO.SelfDber.Get<CmcsSupplier>(entity.SupplierId);
                this.SelectedTransportCompany_SaleFuel = commonDAO.SelfDber.Get<CmcsTransportCompany>(entity.TransportCompanyId);
                this.cmbSalesType.Text = entity.OutFactoryType;
                this.cmbFuelName_SaleFuel.Text = commonDAO.SelfDber.Get<CmcsFuelKind>(entity.FuelKindId).FuelName;
            }
        }

        private void superGridControl1_SaleFuel_DataBindingComplete(object sender, GridDataBindingCompleteEventArgs e)
        {
            foreach (GridRow gridRow in e.GridPanel.Rows)
            {
                View_SaleFuelTransport entity = gridRow.DataItem as View_SaleFuelTransport;
                if (entity == null) return;

                // �����Ч״̬
                gridRow.Cells["ChangeIsUse"].Value = Convert.ToBoolean(entity.IsUse);
                if (entity.TareWeight > 0 && entity.GrossWeight > 0)
                    gridRow.CellStyles.Default.TextColor = Color.Green;
                else if (entity.TareWeight == 0 && entity.GrossWeight == 0)
                    gridRow.CellStyles.Default.TextColor = Color.Red;
            }
        }

        private void superGridControl2_SaleFuel_DataBindingComplete(object sender, GridDataBindingCompleteEventArgs e)
        {

            foreach (GridRow gridRow in e.GridPanel.Rows)
            {
                View_SaleFuelTransport entity = gridRow.DataItem as View_SaleFuelTransport;
                if (entity == null) return;
                // �����Ч״̬
                gridRow.Cells["ChangeIsUse"].Value = Convert.ToBoolean(entity.IsUse);
                if (entity.TareWeight > 0 && entity.GrossWeight > 0)
                    gridRow.CellStyles.Default.TextColor = Color.Green;
                else if (entity.TareWeight == 0 && entity.GrossWeight == 0)
                    gridRow.CellStyles.Default.TextColor = Color.Red;
            }
        }

        #endregion

        #endregion

        #region ��������ҵ��

        #region Vars1

        private CmcsSupplyReceive selectedSupplyUnit_Goods;
        /// <summary>
        /// ѡ��Ĺ�����λ
        /// </summary>
        public CmcsSupplyReceive SelectedSupplyUnit_Goods
        {
            get { return selectedSupplyUnit_Goods; }
            set
            {
                selectedSupplyUnit_Goods = value;

                if (value != null)
                {
                    txtSupplyUnitName_Goods.Text = value.UnitName;
                }
                else
                {
                    txtSupplyUnitName_Goods.ResetText();
                }
            }
        }

        private CmcsSupplyReceive selectedReceiveUnit_Goods;
        /// <summary>
        /// ѡ����ջ���λ
        /// </summary>
        public CmcsSupplyReceive SelectedReceiveUnit_Goods
        {
            get { return selectedReceiveUnit_Goods; }
            set
            {
                selectedReceiveUnit_Goods = value;

                if (value != null)
                {
                    txtReceiveUnitName_Goods.Text = value.UnitName;
                }
                else
                {
                    txtReceiveUnitName_Goods.ResetText();
                }
            }
        }

        private CmcsGoodsType selectedGoodsType_Goods;
        /// <summary>
        /// ѡ�����������
        /// </summary>
        public CmcsGoodsType SelectedGoodsType_Goods
        {
            get { return selectedGoodsType_Goods; }
            set
            {
                selectedGoodsType_Goods = value;

                if (value != null)
                {
                    txtGoodsTypeName_Goods.Text = value.GoodsName;
                }
                else
                {
                    txtGoodsTypeName_Goods.ResetText();
                }
            }
        }

        #endregion

        #region �¼�

        /// <summary>
        /// ѡ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectAutotruck_Goods_Click(object sender, EventArgs e)
        {
            FrmMobileAutotruck_Select frm = new FrmMobileAutotruck_Select(" IsUse=1 order by CarNumber asc");
            if (frm.ShowDialog() == DialogResult.OK)
            {
                //passCarQueuer.Enqueue(ePassWay.Way1, frm.Output.CarNumber, false);
                this.CurrentAutotruck = frm.Output;
            }
        }

        /// <summary>
        /// ѡ�񹩻���λ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnbtnSelectSupply_Goods_Click(object sender, EventArgs e)
        {
            FrmMobileSupplyReceive_Select frm = new FrmMobileSupplyReceive_Select("where IsValid=1 order by UnitName asc");
            if (frm.ShowDialog() == DialogResult.OK)
            {
                this.SelectedSupplyUnit_Goods = frm.Output;
            }
        }

        /// <summary>
        /// ѡ���ջ���λ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectReceive_Goods_Click(object sender, EventArgs e)
        {
            FrmMobileSupplyReceive_Select frm = new FrmMobileSupplyReceive_Select("where IsValid=1 order by UnitName asc");
            if (frm.ShowDialog() == DialogResult.OK)
            {
                this.SelectedReceiveUnit_Goods = frm.Output;
            }
        }

        /// <summary>
        /// ѡ����������
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectGoodsType_Goods_Click(object sender, EventArgs e)
        {
            FrmMobileGoodsType_Select frm = new FrmMobileGoodsType_Select();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                this.SelectedGoodsType_Goods = frm.Output;
            }
        }

        /// <summary>
        /// �³��Ǽ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNewAutotruck_Goods_Click(object sender, EventArgs e)
        {
            new FrmCarManage_Confirm("").Show();
        }

        #endregion

        /// <summary>
        /// �����ŶӼ�¼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveTransport_Goods_Click(object sender, EventArgs e)
        {
            SaveGoodsTransport();
        }

        /// <summary>
        /// ���������¼
        /// </summary>
        /// <returns></returns>
        bool SaveGoodsTransport()
        {
            if (this.CurrentAutotruck == null)
            {
                MessageBoxEx.Show("��ѡ����", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (this.SelectedSupplyUnit_Goods == null)
            {
                MessageBoxEx.Show("��ѡ�񹩻���λ", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (this.SelectedReceiveUnit_Goods == null)
            {
                MessageBoxEx.Show("��ѡ���ջ���λ", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (this.SelectedGoodsType_Goods == null)
            {
                MessageBoxEx.Show("��ѡ����������", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            try
            {
                // �����ŶӼ�¼
                if (queuerDAO.JoinQueueGoodsTransport(this.CurrentAutotruck, this.SelectedSupplyUnit_Goods, this.SelectedReceiveUnit_Goods, this.SelectedGoodsType_Goods, DateTime.Now, txtRemark_Goods.Text, CommonAppConfig.GetInstance().AppIdentifier))
                {
                    btnSaveTransport_Goods.Enabled = false;

                    MessageBoxEx.Show("�Ŷӳɹ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadTodayUnFinishGoodsTransport();
                    LoadTodayFinishGoodsTransport();
                    ResetGoods();
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
        /// ������Ϣ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_Goods_Click(object sender, EventArgs e)
        {
            ResetGoods();
        }

        /// <summary>
        /// ������Ϣ
        /// </summary>
        void ResetGoods()
        {
            this.CurrentAutotruck = null;
            this.SelectedSupplyUnit_Goods = null;
            this.SelectedReceiveUnit_Goods = null;
            this.txtGoodsTypeName_Goods = null;

            txtRemark_Goods.ResetText();

            btnSaveTransport_Goods.Enabled = true;

            // �������
            this.CurrentImperfectCar = null;
        }

        /// <summary>
        /// ��ȡδ��ɵ��������ʼ�¼
        /// </summary>
        void LoadTodayUnFinishGoodsTransport()
        {
            superGridControl1_Goods.PrimaryGrid.DataSource = queuerDAO.GetUnFinishGoodsTransport();
        }

        /// <summary>
        /// ��ȡָ����������ɵ��������ʼ�¼
        /// </summary>
        void LoadTodayFinishGoodsTransport()
        {
            superGridControl2_Goods.PrimaryGrid.DataSource = queuerDAO.GetFinishedGoodsTransport(DateTime.Now.Date, DateTime.Now.Date.AddDays(1));
        }

        #region DataGridView

        /// <summary>
        /// ˫����ʱ���Զ����¼����Ϣ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void superGridControl_Goods_CellDoubleClick(object sender, DevComponents.DotNetBar.SuperGrid.GridCellDoubleClickEventArgs e)
        {
            GridRow gridRow = (sender as SuperGridControl).PrimaryGrid.ActiveRow as GridRow;
            if (gridRow == null) return;

            CmcsGoodsTransport entity = (gridRow.DataItem as CmcsGoodsTransport);
            if (entity != null)
            {
                this.SelectedSupplyUnit_Goods = commonDAO.SelfDber.Get<CmcsSupplyReceive>(entity.SupplyUnitId);
                this.SelectedReceiveUnit_Goods = commonDAO.SelfDber.Get<CmcsSupplyReceive>(entity.ReceiveUnitId);
                this.SelectedGoodsType_Goods = commonDAO.SelfDber.Get<CmcsGoodsType>(entity.GoodsTypeId);
            }
        }

        private void superGridControl1_Goods_CellClick(object sender, GridCellClickEventArgs e)
        {
            CmcsGoodsTransport entity = e.GridCell.GridRow.DataItem as CmcsGoodsTransport;
            if (entity == null) return;

            // ������Ч״̬
            if (e.GridCell.GridColumn.Name == "ChangeIsUse") queuerDAO.ChangeGoodsTransportToInvalid(entity.Id, Convert.ToBoolean(e.GridCell.Value));
        }

        private void superGridControl1_Goods_DataBindingComplete(object sender, GridDataBindingCompleteEventArgs e)
        {
            foreach (GridRow gridRow in e.GridPanel.Rows)
            {
                CmcsGoodsTransport entity = gridRow.DataItem as CmcsGoodsTransport;
                if (entity == null) return;

                // �����Ч״̬
                gridRow.Cells["ChangeIsUse"].Value = Convert.ToBoolean(entity.IsUse);
                CmcsSupplyReceive supplyunit = Dbers.GetInstance().SelfDber.Get<CmcsSupplyReceive>(entity.SupplyUnitId);
                if (supplyunit != null)
                {
                    gridRow.Cells["SupplyUnitName"].Value = supplyunit.UnitName;
                }
                CmcsSupplyReceive receiveunit = Dbers.GetInstance().SelfDber.Get<CmcsSupplyReceive>(entity.ReceiveUnitId);
                if (receiveunit != null)
                {
                    gridRow.Cells["ReceiveUnitName"].Value = receiveunit.UnitName;
                }
                CmcsGoodsType goodstype = Dbers.GetInstance().SelfDber.Get<CmcsGoodsType>(entity.GoodsTypeId);
                if (goodstype != null)
                {
                    gridRow.Cells["GoodsTypeName"].Value = goodstype.GoodsName;
                }
                if (entity.FirstWeight > 0 && entity.SecondWeight > 0)
                    gridRow.CellStyles.Default.TextColor = Color.Green;
                else if (entity.FirstWeight == 0 && entity.SecondWeight == 0)
                    gridRow.CellStyles.Default.TextColor = Color.Red;
            }
        }

        private void superGridControl2_Goods_CellClick(object sender, GridCellClickEventArgs e)
        {
            CmcsGoodsTransport entity = e.GridCell.GridRow.DataItem as CmcsGoodsTransport;
            if (entity == null) return;

            // ������Ч״̬
            if (e.GridCell.GridColumn.Name == "ChangeIsUse") queuerDAO.ChangeGoodsTransportToInvalid(entity.Id, Convert.ToBoolean(e.GridCell.Value));
        }

        private void superGridControl2_Goods_DataBindingComplete(object sender, GridDataBindingCompleteEventArgs e)
        {
            foreach (GridRow gridRow in e.GridPanel.Rows)
            {
                CmcsGoodsTransport entity = gridRow.DataItem as CmcsGoodsTransport;
                if (entity == null) return;

                // �����Ч״̬
                gridRow.Cells["ChangeIsUse"].Value = Convert.ToBoolean(entity.IsUse);

                CmcsSupplyReceive supplyunit = Dbers.GetInstance().SelfDber.Get<CmcsSupplyReceive>(entity.SupplyUnitId);
                if (supplyunit != null)
                {
                    gridRow.Cells["SupplyUnitName"].Value = supplyunit.UnitName;
                }
                CmcsSupplyReceive receiveunit = Dbers.GetInstance().SelfDber.Get<CmcsSupplyReceive>(entity.ReceiveUnitId);
                if (receiveunit != null)
                {
                    gridRow.Cells["ReceiveUnitName"].Value = receiveunit.UnitName;
                }
                CmcsGoodsType goodstype = Dbers.GetInstance().SelfDber.Get<CmcsGoodsType>(entity.GoodsTypeId);
                if (goodstype != null)
                {
                    gridRow.Cells["GoodsTypeName"].Value = goodstype.GoodsName;
                }
                if (entity.FirstWeight > 0 && entity.SecondWeight > 0)
                    gridRow.CellStyles.Default.TextColor = Color.Green;
                else if (entity.FirstWeight == 0 && entity.SecondWeight == 0)
                    gridRow.CellStyles.Default.TextColor = Color.Red;
            }
        }

        #endregion

        #endregion

        #region ���ó���ҵ��

        /// <summary>
        /// ѡ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectAutotruck_Visit_Click(object sender, EventArgs e)
        {
            FrmMobileAutotruck_Select frm = new FrmMobileAutotruck_Select("and CarType='" + eCarType.���ó���.ToString() + "' and IsUse=1 order by CarNumber asc");
            if (frm.ShowDialog() == DialogResult.OK)
            {
                //passCarQueuer.Enqueue(ePassWay.UnKnow, frm.Output.CarNumber, false);
                this.CurrentAutotruck = frm.Output;
            }
        }

        /// <summary>
        /// �³��Ǽ�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNewAutotruck_Visit_Click(object sender, EventArgs e)
        {
            new FrmCarManage_Confirm("").Show();
        }

        /// <summary>
        /// �����ŶӼ�¼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveTransport_Visit_Click(object sender, EventArgs e)
        {
            SaveVisitTransport();
        }

        /// <summary>
        /// ���������¼
        /// </summary>
        /// <returns></returns>
        bool SaveVisitTransport()
        {
            if (this.CurrentAutotruck == null)
            {
                MessageBoxEx.Show("��ѡ����", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            try
            {
                // �����볧ú�ŶӼ�¼��ͬʱ����������Ϣ�Լ����ƻ���������
                if (queuerDAO.JoinQueueVisitTransport(this.CurrentAutotruck, DateTime.Now, txtRemark_Visit.Text, CommonAppConfig.GetInstance().AppIdentifier))
                {
                    MessageBoxEx.Show("�Ŷӳɹ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadTodayUnFinishVisitTransport();
                    LoadTodayFinishVisitTransport();
                    ResetVisit();
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
        /// ������Ϣ
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_Visit_Click(object sender, EventArgs e)
        {
            ResetVisit();
        }

        /// <summary>
        /// ������Ϣ
        /// </summary>
        void ResetVisit()
        {
            this.CurrentAutotruck = null;

            txtRemark_Visit.ResetText();

            // �������
            this.CurrentImperfectCar = null;
        }

        /// <summary>
        /// ��ȡδ��ɵ����ó�����¼
        /// </summary>
        void LoadTodayUnFinishVisitTransport()
        {
            superGridControl1_Visit.PrimaryGrid.DataSource = queuerDAO.GetUnFinishVisitTransport();
        }

        /// <summary>
        /// ��ȡָ����������ɵ����ó�����¼
        /// </summary>
        void LoadTodayFinishVisitTransport()
        {
            superGridControl2_Visit.PrimaryGrid.DataSource = queuerDAO.GetFinishedVisitTransport(DateTime.Now.Date, DateTime.Now.Date.AddDays(1));
        }

        private void superGridControl1_Visit_CellClick(object sender, GridCellClickEventArgs e)
        {
            CmcsVisitTransport entity = e.GridCell.GridRow.DataItem as CmcsVisitTransport;
            if (entity == null) return;

            // ������Ч״̬
            if (e.GridCell.GridColumn.Name == "ChangeIsUse") queuerDAO.ChangeVisitTransportToInvalid(entity.Id, Convert.ToBoolean(e.GridCell.Value));
        }

        private void superGridControl1_Visit_DataBindingComplete(object sender, GridDataBindingCompleteEventArgs e)
        {
            foreach (GridRow gridRow in e.GridPanel.Rows)
            {
                CmcsVisitTransport entity = gridRow.DataItem as CmcsVisitTransport;
                if (entity == null) return;

                // �����Ч״̬
                gridRow.Cells["ChangeIsUse"].Value = Convert.ToBoolean(entity.IsUse);
            }
        }

        private void superGridControl2_Visit_CellClick(object sender, GridCellClickEventArgs e)
        {
            CmcsVisitTransport entity = e.GridCell.GridRow.DataItem as CmcsVisitTransport;
            if (entity == null) return;

            // ������Ч״̬
            if (e.GridCell.GridColumn.Name == "ChangeIsUse") queuerDAO.ChangeVisitTransportToInvalid(entity.Id, Convert.ToBoolean(e.GridCell.Value));
        }

        private void superGridControl2_Visit_DataBindingComplete(object sender, GridDataBindingCompleteEventArgs e)
        {
            foreach (GridRow gridRow in e.GridPanel.Rows)
            {
                CmcsVisitTransport entity = gridRow.DataItem as CmcsVisitTransport;
                if (entity == null) return;

                // �����Ч״̬
                gridRow.Cells["ChangeIsUse"].Value = Convert.ToBoolean(entity.IsUse);
            }
        }

        #endregion

        #region ��������

        private void superGridControl_BeginEdit(object sender, DevComponents.DotNetBar.SuperGrid.GridEditEventArgs e)
        {
            if (e.GridCell.GridColumn.DataPropertyName != "IsUse")
            {
                // ȡ������༭
                e.Cancel = true;
            }
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
