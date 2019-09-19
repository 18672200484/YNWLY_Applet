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
        /// 窗体唯一标识符
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
            //加载煤种
            LoadFuelkind(new ComboBoxEx[] { cmbFuelName_BuyFuel, cmbFuelName_SaleFuel });
            //加载采样方式
            LoadSampleType(new ComboBoxEx[] { cmbSamplingType_BuyFuel, cmbSamplingType_SaleFuel });
            //加载入厂煤类型
            LoadBuyFuelType(new ComboBoxEx[] { cmbBuyFuelType });
            //加载出厂煤类型
            LoadSaleType(new ComboBoxEx[] { cmbSalesType });
            //加载成品仓
            LoadCPC(new ComboBoxEx[] { cmb_CPC });
            try
            {
                this.IsUseYB = commonDAO.GetAppletConfigBoolen("是否启用预报");
            }
            catch { }
            timer3_Tick(null, null);
        }

        #region 业务处理类
        CarTransportDAO carTransportDAO = CarTransportDAO.GetInstance();
        QueuerDAO queuerDAO = QueuerDAO.GetInstance();
        CommonDAO commonDAO = CommonDAO.GetInstance();
        #endregion

        #region 公共Vars

        private string queueType;
        /// <summary>
        /// 排队类型
        /// </summary>
        public string QueueType
        {
            get { return queueType; }
            set
            {
                queueType = value;
                if (value == eTransportType.原料煤入场.ToString() || value == eTransportType.仓储煤入场.ToString() || value == eTransportType.中转煤入场.ToString())
                    this.superTabControl2.SelectedTab = superTabItem_BuyFuel;
                else if (value == eTransportType.仓储煤出场.ToString() || value == eTransportType.中转煤出场.ToString() || value == eTransportType.销售掺配煤.ToString() || value == eTransportType.销售直销煤.ToString())
                    this.superTabControl2.SelectedTab = superTabItem_SaleFuel;
                else if (value == eTransportType.其他物资.ToString())
                    this.superTabControl2.SelectedTab = superTabItem_Goods;
                else if (value == eTransportType.来访车辆.ToString())
                    this.superTabControl2.SelectedTab = superTabItem_Visit;
            }
        }

        #region Vars1
        public static PassCarQueuer passCarQueuer = new PassCarQueuer();

        ImperfectCar currentImperfectCar;
        /// <summary>
        /// 识别或选择的车辆凭证
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
        /// 当前车
        /// </summary>
        public CmcsAutotruck CurrentAutotruck
        {
            get { return currentAutotruck; }
            set
            {
                currentAutotruck = value;

                if (value != null)
                {
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.当前车Id.ToString(), value.Id);
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.当前车号.ToString(), value.CarNumber);

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
                    //重置车辆信息
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.当前车Id.ToString(), string.Empty);
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.当前车号.ToString(), string.Empty);

                    txtCarNumber_BuyFuel.ResetText();
                    txtCarNumber_SaleFuel.ResetText();
                    txtCarNumber_Goods.ResetText();
                    txtCarNumber_Visit.ResetText();
                }
            }
        }

        private string carnumber1;
        /// <summary>
        /// 当前车号1
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
        /// 当前车类型
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
        /// 是否启用预报
        /// </summary>
        public bool IsUseYB
        {
            get { return isUseYB; }
            set
            {
                isUseYB = value;

                //入场煤
                btnSelectTransportCompany_BuyFuel.Enabled = !value;
                btnSelectSupplier_BuyFuel.Enabled = !value;
                btnSelectMine_BuyFuel.Enabled = !value;
                cmbFuelName_BuyFuel.Enabled = !value;
                cmbSamplingType_BuyFuel.Enabled = !value;
                cmbBuyFuelType.Enabled = !value;

                //出场煤
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

        #region 公共业务

        /// <summary>
        /// 慢速任务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer3_Tick(object sender, EventArgs e)
        {
            try
            {
                // 入厂煤
                LoadTodayUnFinishBuyFuelTransport();
                LoadTodayFinishBuyFuelTransport();
                LoadTitleBuyFuelTransport();
                // 销售煤 
                LoadTodayUnFinishSaleFuelTransport();
                LoadTodayFinishSaleFuelTransport();

                // 其他物资
                LoadTodayUnFinishGoodsTransport();
                LoadTodayFinishGoodsTransport();

                // 来访车辆
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
        /// 加载煤种
        /// </summary>
        void LoadFuelkind(ComboBoxEx[] comboBoxEx)
        {
            IList<CmcsFuelKind> FuelKindList = Dbers.GetInstance().SelfDber.Entities<CmcsFuelKind>("where Valid='有效' and ParentId is not null");
            for (int i = 0; i < comboBoxEx.Length; i++)
            {
                comboBoxEx[i].DisplayMember = "FuelName";
                comboBoxEx[i].ValueMember = "Id";
                comboBoxEx[i].DataSource = FuelKindList;
            }
        }

        /// <summary>
        /// 加载采样方式
        /// </summary>
        void LoadSampleType(ComboBoxEx[] comboBoxEx)
        {
            List<CMCS.Common.Entities.iEAA.CodeContent> contentList = commonDAO.GetCodeContentByKind("采样方式");
            for (int i = 0; i < comboBoxEx.Length; i++)
            {
                comboBoxEx[i].DisplayMember = "Content";
                comboBoxEx[i].ValueMember = "Code";
                comboBoxEx[i].DataSource = contentList;

                comboBoxEx[i].Text = eSamplingType.机械采样.ToString();
            }
        }

        /// <summary>
        /// 加载入厂煤类型
        /// </summary>
        void LoadBuyFuelType(ComboBoxEx[] comboBoxEx)
        {
            for (int i = 0; i < comboBoxEx.Length; i++)
            {
                comboBoxEx[i].Items.Add(new ComboBoxItem(eTransportType.原料煤入场.ToString(), eTransportType.原料煤入场.ToString()));
                comboBoxEx[i].Items.Add(new ComboBoxItem(eTransportType.仓储煤入场.ToString(), eTransportType.仓储煤入场.ToString()));
                comboBoxEx[i].Items.Add(new ComboBoxItem(eTransportType.中转煤入场.ToString(), eTransportType.中转煤入场.ToString()));
                comboBoxEx[i].SelectedIndex = -1;
            }
        }

        /// <summary>
        /// 加载出厂煤类型
        /// </summary>
        void LoadSaleType(ComboBoxEx[] comboBoxEx)
        {
            for (int i = 0; i < comboBoxEx.Length; i++)
            {
                comboBoxEx[i].Items.Add(new ComboBoxItem(eTransportType.仓储煤出场.ToString(), eTransportType.仓储煤出场.ToString()));
                comboBoxEx[i].Items.Add(new ComboBoxItem(eTransportType.中转煤出场.ToString(), eTransportType.中转煤出场.ToString()));
                comboBoxEx[i].Items.Add(new ComboBoxItem(eTransportType.销售直销煤.ToString(), eTransportType.销售直销煤.ToString()));
                comboBoxEx[i].Items.Add(new ComboBoxItem(eTransportType.销售掺配煤.ToString(), eTransportType.销售掺配煤.ToString()));
                comboBoxEx[i].SelectedIndex = -1;
            }
        }

        /// <summary>
        /// 加载成品仓
        /// </summary>
        void LoadCPC(ComboBoxEx[] comboBoxEx)
        {
            DataTable data = commonDAO.SelfDber.ExecuteDataTable("select Id,PotName from fultbcoalpot where PotName like '%成品仓%' order by PotName");
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
        /// 加载自定义数据
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

        #region 入场煤业务

        #region Vars1

        private CmcsSupplier selectedSupplier_BuyFuel;
        /// <summary>
        /// 选择的供煤单位
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
        /// 选择的运输单位
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
        /// 选择的矿点
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
        /// 选择的煤种
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
        /// 选择的来煤预报
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

        #region 选择事件

        /// <summary>
        /// 选择煤种
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbFuelName_BuyFuel_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SelectedFuelKind_BuyFuel = cmbFuelName_BuyFuel.SelectedItem as CmcsFuelKind;
        }

        /// <summary>
        /// 选择车辆
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectAutotruck_BuyFuel_Click(object sender, EventArgs e)
        {
            FrmMobileAutotruck_Select frm = new FrmMobileAutotruck_Select(" and IsUse=1 order by CarNumber asc");
            if (frm.ShowDialog() == DialogResult.OK)
            {
                //passCarQueuer.Enqueue(ePassWay.Way1, frm.Output.CarNumber, false);
                //无设备情况下 直接验证
                this.CurrentAutotruck = carTransportDAO.GetAutotruckByCarNumber(frm.Output.CarNumber);
            }
        }

        /// <summary>
        /// 选择供煤单位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectSupplier_BuyFuel_Click(object sender, EventArgs e)
        {
            FrmMobileSupplier_Select frm = new FrmMobileSupplier_Select("where Valid='有效' order by Name asc");
            if (frm.ShowDialog() == DialogResult.OK)
            {
                this.SelectedSupplier_BuyFuel = frm.Output;
            }
        }

        /// <summary>
        /// 选择矿点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectMine_BuyFuel_Click(object sender, EventArgs e)
        {
            FrmMobileMine_Select frm = new FrmMobileMine_Select("where Valid='有效' order by Name asc");
            if (frm.ShowDialog() == DialogResult.OK)
            {
                this.SelectedMine_BuyFuel = frm.Output;
            }
        }

        /// <summary>
        /// 选择运输单位
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
        /// 新车登记
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNewAutotruck_BuyFuel_Click(object sender, EventArgs e)
        {
            new FrmCarManage_Confirm("").Show();
        }

        /// <summary>
        /// 选择入厂煤来煤预报
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectForecast_BuyFuel_Click(object sender, EventArgs e)
        {
            FrmMobileBuyFuelForecast_Select frm = new FrmMobileBuyFuelForecast_Select(string.Format("where (InFactoryType='{0}' or InFactoryType='{1}' or InFactoryType='{2}') ", eTransportType.原料煤入场, eTransportType.仓储煤入场, eTransportType.中转煤入场));
            if (frm.ShowDialog() == DialogResult.OK)
            {
                this.SelectedLMYB_BuyFuel = frm.Output;
            }
        }

        #endregion

        #region 保存运输记录

        /// <summary>
        /// 保存入厂煤运输记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveTransport_BuyFuel_Click(object sender, EventArgs e)
        {
            SaveBuyFuelTransport();
        }

        /// <summary>
        /// 保存运输记录
        /// </summary>
        /// <returns></returns>
        bool SaveBuyFuelTransport()
        {
            //if (IsUseYB && this.SelectedLMYB_BuyFuel == null)
            //{
            //    MessageBoxEx.Show("请选择预报信息", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return false;
            //}

            if (this.CurrentAutotruck == null)
            {
                MessageBoxEx.Show("请选择车辆", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrEmpty(this.cmbFuelName_BuyFuel.Text))
            {
                MessageBoxEx.Show("请选择煤种", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (this.SelectedMine_BuyFuel == null)
            {
                MessageBoxEx.Show("请选择矿点", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (this.SelectedSupplier_BuyFuel == null)
            {
                MessageBoxEx.Show("请选择供煤单位", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (this.SelectedTransportCompany_BuyFuel == null)
            {
                MessageBoxEx.Show("请选择运输单位", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            //if (txtTicketWeight_BuyFuel.Value <= 0)
            //{
            //    MessageBoxEx.Show("请输入有效的矿发量", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return false;
            //}
            if (string.IsNullOrEmpty(cmbBuyFuelType.Text))
            {
                MessageBoxEx.Show("请选择入场煤类型", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            try
            {
                // 生成入厂煤排队记录，同时生成批次信息以及采制化三级编码
                if (queuerDAO.JoinQueueBuyFuelTransport(this.CurrentAutotruck, this.SelectedSupplier_BuyFuel, this.SelectedMine_BuyFuel, this.SelectedTransportCompany_BuyFuel, this.cmbFuelName_BuyFuel.SelectedItem as CmcsFuelKind, (decimal)txtTicketWeight_BuyFuel.Value, DateTime.Now, txtRemark_BuyFuel.Text, cmbSamplingType_BuyFuel.Text, this.SelectedLMYB_BuyFuel, cmbBuyFuelType.Text))
                {
                    btnSaveTransport_BuyFuel.Enabled = false;

                    MessageBoxEx.Show("排队成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadTodayUnFinishBuyFuelTransport();
                    LoadTodayFinishBuyFuelTransport();

                    ResetBuyFuel();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show("保存失败\r\n" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);

                Log4Neter.Error("保存运输记录", ex);
            }

            return false;
        }

        #endregion

        /// <summary>
        /// 重置入厂煤运输记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_BuyFuel_Click(object sender, EventArgs e)
        {
            ResetBuyFuel();
        }

        /// <summary>
        /// 重置信息
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
            // 最后重置
            this.CurrentImperfectCar = null;
        }

        /// <summary>
        /// 获取未完成的入厂煤记录
        /// </summary>
        void LoadTodayUnFinishBuyFuelTransport()
        {
            superGridControl1_BuyFuel.PrimaryGrid.DataSource = queuerDAO.GetUnFinishBuyFuelTransport();
        }

        /// <summary>
        /// 获取指定日期已完成的入厂煤记录
        /// </summary>
        void LoadTodayFinishBuyFuelTransport()
        {
            superGridControl2_BuyFuel.PrimaryGrid.DataSource = queuerDAO.GetFinishedBuyFuelTransport(DateTime.Now.Date, DateTime.Now.Date.AddDays(1));
        }

        void LoadTitleBuyFuelTransport()
        {
            List<CmcsBuyFuelTransport> tran = queuerDAO.GetBuyFuelTransportByDate(DateTime.Now.Date, DateTime.Now.Date.AddDays(1));
            txtTitle_BuyFuel.Text = string.Format("已登记总量：{0}   已称重：{1}  未称重：{2}   已称重未回皮：{3}   已回皮：{4}", tran.Count(), tran.Where(a => a.GrossWeight > 0).Count(), tran.Where(a => a.GrossWeight == 0).Count(), tran.Where(a => a.GrossWeight > 0 && a.TareWeight == 0).Count(), tran.Where(a => a.GrossWeight > 0 && a.TareWeight > 0).Count());
        }

        #region DataGridView

        /// <summary>
        /// 双击行时，自动填充供煤单位、矿点等信息
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

            // 更改有效状态
            if (e.GridCell.GridColumn.Name == "ChangeIsUse") queuerDAO.ChangeBuyFuelTransportToInvalid(entity.Id, Convert.ToBoolean(e.GridCell.Value));
        }

        private void superGridControl1_BuyFuel_DataBindingComplete(object sender, GridDataBindingCompleteEventArgs e)
        {
            foreach (GridRow gridRow in e.GridPanel.Rows)
            {
                View_BuyFuelTransport entity = gridRow.DataItem as View_BuyFuelTransport;
                if (entity == null) return;

                // 填充有效状态
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

            // 更改有效状态
            if (e.GridCell.GridColumn.Name == "ChangeIsUse") queuerDAO.ChangeBuyFuelTransportToInvalid(entity.Id, Convert.ToBoolean(e.GridCell.Value));
        }

        private void superGridControl2_BuyFuel_DataBindingComplete(object sender, GridDataBindingCompleteEventArgs e)
        {
            foreach (GridRow gridRow in e.GridPanel.Rows)
            {
                View_BuyFuelTransport entity = gridRow.DataItem as View_BuyFuelTransport;
                if (entity == null) return;

                // 填充有效状态
                gridRow.Cells["ChangeIsUse"].Value = Convert.ToBoolean(entity.IsUse);
                if (entity.GrossWeight > 0 && entity.TareWeight > 0)
                    gridRow.CellStyles.Default.TextColor = Color.Green;
                else if (entity.GrossWeight == 0 && entity.TareWeight == 0)
                    gridRow.CellStyles.Default.TextColor = Color.Red;
            }
        }

        #endregion

        #endregion

        #region 出场煤业务

        #region Var1

        private CmcsLMYB selectedCmcsTransportSales;
        List<String> StorageNames = new List<string>();
        /// <summary>
        /// 选择的销售煤预报
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
                    if (value.InFactoryType == eTransportType.销售直销煤.ToString() || value.InFactoryType == eTransportType.销售掺配煤.ToString())
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
        /// 选择的收货单位
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
        /// 选择的运输单位
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
            if (outFactoryType == eTransportType.销售掺配煤.ToString() || outFactoryType == eTransportType.销售直销煤.ToString())
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

        #region 事件

        private void cmbSalesType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ChangeCPCVisible(cmbSalesType.Text);
        }
        #endregion

        #region 公共方法

        /// <summary>
        /// 选择来煤预报
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectForecast_SaleFuel_Click(object sender, EventArgs e)
        {
            FrmMobileSaleFuelForecast_Select frm = new FrmMobileSaleFuelForecast_Select(string.Format(" where (InFactoryType='{0}' or InFactoryType='{1}' or InFactoryType='{2}' or InFactoryType='{3}')", eTransportType.仓储煤出场.ToString(), eTransportType.中转煤出场.ToString(), eTransportType.销售掺配煤.ToString(), eTransportType.销售直销煤.ToString()));
            if (frm.ShowDialog() == DialogResult.OK)
            {
                SelectedCmcsTransportSales = frm.Output;
            }
        }

        /// <summary>
        /// 选择车辆
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
        /// 选择运输单位
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
        /// 选择收货单位
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectSupplyReceive_SaleFuel_Click(object sender, EventArgs e)
        {
            FrmMobileSupplier_Select frm = new FrmMobileSupplier_Select("where Valid='有效' order by Name asc");
            if (frm.ShowDialog() == DialogResult.OK)
            {
                this.SelectedReceive_SaleFuel = frm.Output;
            }
        }

        /// <summary>
        /// 新车登记
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNewAutotruck_SaleFuel_Click(object sender, EventArgs e)
        {
            new FrmCarManage_Confirm("").Show();
        }

        /// <summary>
        /// 成品仓单选
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
        /// 加载未完成运输记录
        /// </summary>
        void LoadTodayUnFinishSaleFuelTransport()
        {
            superGridControl1_SaleFuel.PrimaryGrid.DataSource = queuerDAO.GetUnFinishSaleFuelTransport();
        }

        /// <summary>
        /// 加载今日已完成运输记录
        /// </summary>
        void LoadTodayFinishSaleFuelTransport()
        {
            superGridControl2_SaleFuel.PrimaryGrid.DataSource = queuerDAO.GetFinishedSaleFuelTransport(DateTime.Now.Date, DateTime.Now.Date.AddDays(1));
        }

        #endregion

        #region 保存运输记录

        /// <summary>
        /// 登记
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveTransport_SaleFuel_Click(object sender, EventArgs e)
        {
            SaveSaleFuelTransport();
        }

        /// <summary>
        /// 保存运输记录
        /// </summary>
        /// <returns></returns>
        bool SaveSaleFuelTransport()
        {
            if (this.CurrentAutotruck == null)
            {
                MessageBoxEx.Show("请选择车辆", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (this.SelectedTransportCompany_SaleFuel == null)
            {
                MessageBoxEx.Show("请选择运输单位", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (this.SelectedReceive_SaleFuel == null)
            {
                MessageBoxEx.Show("请选择收货单位", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrEmpty(this.cmbFuelName_SaleFuel.Text))
            {
                MessageBoxEx.Show("请选择煤种", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            //if (this.SelectedCmcsTransportSales == null)
            //{
            //    MessageBoxEx.Show("请选择销售煤订单", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return false;
            //}
            //if (string.IsNullOrEmpty(cmbCPC.Text))
            //{
            //    MessageBoxEx.Show("请选择对应成品仓", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return false;
            //}
            if (string.IsNullOrEmpty(cmbSalesType.Text))
            {
                MessageBoxEx.Show("请选择出场类型", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            try
            {
                ComboBoxItem storageItem = (ComboBoxItem)cmb_Storage.SelectedItem;
                ComboBoxItem cPCItem = (ComboBoxItem)cmb_CPC.SelectedItem;
                // 生成销售煤排队记录 同时生成批次信息
                if (queuerDAO.JoinQueueSaleFuelTransport(this.CurrentAutotruck, this.SelectedCmcsTransportSales, this.SelectedReceive_SaleFuel, this.SelectedTransportCompany_SaleFuel, (this.cmbFuelName_SaleFuel.SelectedItem as CmcsFuelKind), DateTime.Now, txt_ReMark1.Text, CommonAppConfig.GetInstance().AppIdentifier, cmb_CPC.Text, cmbSalesType.Text, cmbSamplingType_SaleFuel.Text, new Tuple<string, string>(cPCItem != null ? cPCItem.Name : "", cPCItem != null ? cPCItem.Text : ""), new Tuple<string, string>(storageItem != null ? storageItem.Name : "", storageItem != null ? storageItem.Text : "")))
                {
                    btnSaveTransport_SaleFuel.Enabled = false;

                    MessageBoxEx.Show("排队成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadTodayUnFinishSaleFuelTransport();
                    LoadTodayFinishSaleFuelTransport();

                    ResetSaleFuel();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show("保存失败\r\n" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);

                Log4Neter.Error("保存运输记录", ex);
            }

            return false;
        }

        #endregion

        #region 重置

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

            // 最后重置
            this.CurrentImperfectCar = null;
            LoadCPC(new ComboBoxEx[] { cmb_CPC });
            cmb_Storage.Items.Clear();
        }

        #endregion

        #region DataGridView

        /// <summary>
        /// 双击行时，自动填充供煤单位、矿点等信息
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

                // 填充有效状态
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
                // 填充有效状态
                gridRow.Cells["ChangeIsUse"].Value = Convert.ToBoolean(entity.IsUse);
                if (entity.TareWeight > 0 && entity.GrossWeight > 0)
                    gridRow.CellStyles.Default.TextColor = Color.Green;
                else if (entity.TareWeight == 0 && entity.GrossWeight == 0)
                    gridRow.CellStyles.Default.TextColor = Color.Red;
            }
        }

        #endregion

        #endregion

        #region 其他物资业务

        #region Vars1

        private CmcsSupplyReceive selectedSupplyUnit_Goods;
        /// <summary>
        /// 选择的供货单位
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
        /// 选择的收货单位
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
        /// 选择的物资类型
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

        #region 事件

        /// <summary>
        /// 选择车辆
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
        /// 选择供货单位
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
        /// 选择收货单位
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
        /// 选择物资类型
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
        /// 新车登记
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNewAutotruck_Goods_Click(object sender, EventArgs e)
        {
            new FrmCarManage_Confirm("").Show();
        }

        #endregion

        /// <summary>
        /// 保存排队记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveTransport_Goods_Click(object sender, EventArgs e)
        {
            SaveGoodsTransport();
        }

        /// <summary>
        /// 保存运输记录
        /// </summary>
        /// <returns></returns>
        bool SaveGoodsTransport()
        {
            if (this.CurrentAutotruck == null)
            {
                MessageBoxEx.Show("请选择车辆", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (this.SelectedSupplyUnit_Goods == null)
            {
                MessageBoxEx.Show("请选择供货单位", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (this.SelectedReceiveUnit_Goods == null)
            {
                MessageBoxEx.Show("请选择收货单位", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (this.SelectedGoodsType_Goods == null)
            {
                MessageBoxEx.Show("请选择物资类型", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }

            try
            {
                // 生成排队记录
                if (queuerDAO.JoinQueueGoodsTransport(this.CurrentAutotruck, this.SelectedSupplyUnit_Goods, this.SelectedReceiveUnit_Goods, this.SelectedGoodsType_Goods, DateTime.Now, txtRemark_Goods.Text, CommonAppConfig.GetInstance().AppIdentifier))
                {
                    btnSaveTransport_Goods.Enabled = false;

                    MessageBoxEx.Show("排队成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadTodayUnFinishGoodsTransport();
                    LoadTodayFinishGoodsTransport();
                    ResetGoods();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show("保存失败\r\n" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);

                Log4Neter.Error("保存运输记录", ex);
            }

            return false;
        }

        /// <summary>
        /// 重置信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_Goods_Click(object sender, EventArgs e)
        {
            ResetGoods();
        }

        /// <summary>
        /// 重置信息
        /// </summary>
        void ResetGoods()
        {
            this.CurrentAutotruck = null;
            this.SelectedSupplyUnit_Goods = null;
            this.SelectedReceiveUnit_Goods = null;
            this.txtGoodsTypeName_Goods = null;

            txtRemark_Goods.ResetText();

            btnSaveTransport_Goods.Enabled = true;

            // 最后重置
            this.CurrentImperfectCar = null;
        }

        /// <summary>
        /// 获取未完成的其他物资记录
        /// </summary>
        void LoadTodayUnFinishGoodsTransport()
        {
            superGridControl1_Goods.PrimaryGrid.DataSource = queuerDAO.GetUnFinishGoodsTransport();
        }

        /// <summary>
        /// 获取指定日期已完成的其他物资记录
        /// </summary>
        void LoadTodayFinishGoodsTransport()
        {
            superGridControl2_Goods.PrimaryGrid.DataSource = queuerDAO.GetFinishedGoodsTransport(DateTime.Now.Date, DateTime.Now.Date.AddDays(1));
        }

        #region DataGridView

        /// <summary>
        /// 双击行时，自动填充录入信息
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

            // 更改有效状态
            if (e.GridCell.GridColumn.Name == "ChangeIsUse") queuerDAO.ChangeGoodsTransportToInvalid(entity.Id, Convert.ToBoolean(e.GridCell.Value));
        }

        private void superGridControl1_Goods_DataBindingComplete(object sender, GridDataBindingCompleteEventArgs e)
        {
            foreach (GridRow gridRow in e.GridPanel.Rows)
            {
                CmcsGoodsTransport entity = gridRow.DataItem as CmcsGoodsTransport;
                if (entity == null) return;

                // 填充有效状态
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

            // 更改有效状态
            if (e.GridCell.GridColumn.Name == "ChangeIsUse") queuerDAO.ChangeGoodsTransportToInvalid(entity.Id, Convert.ToBoolean(e.GridCell.Value));
        }

        private void superGridControl2_Goods_DataBindingComplete(object sender, GridDataBindingCompleteEventArgs e)
        {
            foreach (GridRow gridRow in e.GridPanel.Rows)
            {
                CmcsGoodsTransport entity = gridRow.DataItem as CmcsGoodsTransport;
                if (entity == null) return;

                // 填充有效状态
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

        #region 来访车辆业务

        /// <summary>
        /// 选择车辆
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectAutotruck_Visit_Click(object sender, EventArgs e)
        {
            FrmMobileAutotruck_Select frm = new FrmMobileAutotruck_Select("and CarType='" + eCarType.来访车辆.ToString() + "' and IsUse=1 order by CarNumber asc");
            if (frm.ShowDialog() == DialogResult.OK)
            {
                //passCarQueuer.Enqueue(ePassWay.UnKnow, frm.Output.CarNumber, false);
                this.CurrentAutotruck = frm.Output;
            }
        }

        /// <summary>
        /// 新车登记
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnNewAutotruck_Visit_Click(object sender, EventArgs e)
        {
            new FrmCarManage_Confirm("").Show();
        }

        /// <summary>
        /// 保存排队记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveTransport_Visit_Click(object sender, EventArgs e)
        {
            SaveVisitTransport();
        }

        /// <summary>
        /// 保存运输记录
        /// </summary>
        /// <returns></returns>
        bool SaveVisitTransport()
        {
            if (this.CurrentAutotruck == null)
            {
                MessageBoxEx.Show("请选择车辆", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            try
            {
                // 生成入厂煤排队记录，同时生成批次信息以及采制化三级编码
                if (queuerDAO.JoinQueueVisitTransport(this.CurrentAutotruck, DateTime.Now, txtRemark_Visit.Text, CommonAppConfig.GetInstance().AppIdentifier))
                {
                    MessageBoxEx.Show("排队成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Information);

                    LoadTodayUnFinishVisitTransport();
                    LoadTodayFinishVisitTransport();
                    ResetVisit();
                    return true;
                }
            }
            catch (Exception ex)
            {
                MessageBoxEx.Show("保存失败\r\n" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);

                Log4Neter.Error("保存运输记录", ex);
            }

            return false;
        }

        /// <summary>
        /// 重置信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReset_Visit_Click(object sender, EventArgs e)
        {
            ResetVisit();
        }

        /// <summary>
        /// 重置信息
        /// </summary>
        void ResetVisit()
        {
            this.CurrentAutotruck = null;

            txtRemark_Visit.ResetText();

            // 最后重置
            this.CurrentImperfectCar = null;
        }

        /// <summary>
        /// 获取未完成的来访车辆记录
        /// </summary>
        void LoadTodayUnFinishVisitTransport()
        {
            superGridControl1_Visit.PrimaryGrid.DataSource = queuerDAO.GetUnFinishVisitTransport();
        }

        /// <summary>
        /// 获取指定日期已完成的来访车辆记录
        /// </summary>
        void LoadTodayFinishVisitTransport()
        {
            superGridControl2_Visit.PrimaryGrid.DataSource = queuerDAO.GetFinishedVisitTransport(DateTime.Now.Date, DateTime.Now.Date.AddDays(1));
        }

        private void superGridControl1_Visit_CellClick(object sender, GridCellClickEventArgs e)
        {
            CmcsVisitTransport entity = e.GridCell.GridRow.DataItem as CmcsVisitTransport;
            if (entity == null) return;

            // 更改有效状态
            if (e.GridCell.GridColumn.Name == "ChangeIsUse") queuerDAO.ChangeVisitTransportToInvalid(entity.Id, Convert.ToBoolean(e.GridCell.Value));
        }

        private void superGridControl1_Visit_DataBindingComplete(object sender, GridDataBindingCompleteEventArgs e)
        {
            foreach (GridRow gridRow in e.GridPanel.Rows)
            {
                CmcsVisitTransport entity = gridRow.DataItem as CmcsVisitTransport;
                if (entity == null) return;

                // 填充有效状态
                gridRow.Cells["ChangeIsUse"].Value = Convert.ToBoolean(entity.IsUse);
            }
        }

        private void superGridControl2_Visit_CellClick(object sender, GridCellClickEventArgs e)
        {
            CmcsVisitTransport entity = e.GridCell.GridRow.DataItem as CmcsVisitTransport;
            if (entity == null) return;

            // 更改有效状态
            if (e.GridCell.GridColumn.Name == "ChangeIsUse") queuerDAO.ChangeVisitTransportToInvalid(entity.Id, Convert.ToBoolean(e.GridCell.Value));
        }

        private void superGridControl2_Visit_DataBindingComplete(object sender, GridDataBindingCompleteEventArgs e)
        {
            foreach (GridRow gridRow in e.GridPanel.Rows)
            {
                CmcsVisitTransport entity = gridRow.DataItem as CmcsVisitTransport;
                if (entity == null) return;

                // 填充有效状态
                gridRow.Cells["ChangeIsUse"].Value = Convert.ToBoolean(entity.IsUse);
            }
        }

        #endregion

        #region 其他函数

        private void superGridControl_BeginEdit(object sender, DevComponents.DotNetBar.SuperGrid.GridEditEventArgs e)
        {
            if (e.GridCell.GridColumn.DataPropertyName != "IsUse")
            {
                // 取消进入编辑
                e.Cancel = true;
            }
        }

        /// <summary>
        /// 设置行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void superGridControl_GetRowHeaderText(object sender, DevComponents.DotNetBar.SuperGrid.GridGetRowHeaderTextEventArgs e)
        {
            e.Text = (e.GridRow.RowIndex + 1).ToString();
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
