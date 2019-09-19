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
        /// 窗体唯一标识符
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
        /// 语音播报
        /// </summary>
        VoiceSpeaker voiceSpeaker = new VoiceSpeaker();

        bool wbSteady = false;
        /// <summary>
        /// 地磅仪表稳定状态
        /// </summary>
        public bool WbSteady
        {
            get { return wbSteady; }
            set
            {
                wbSteady = value;

                this.panCurrentWeight.Style.ForeColor.Color = (value ? Color.Lime : Color.Red);

                panCurrentWeight.Refresh();

                commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.地磅仪表_稳定.ToString(), value ? "1" : "0");
            }
        }

        double wbMinWeight = 0;
        /// <summary>
        /// 地磅仪表最小称重 单位：吨
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
                    txt_CarNumber.Text = value.CarNumber;

                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.当前车Id.ToString(), value.Id);
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.当前车号.ToString(), value.CarNumber);
                }
                else
                {
                    txt_CarNumber.Text = "";
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.当前车Id.ToString(), string.Empty);
                    commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.当前车号.ToString(), string.Empty);
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
                selectedFuelKind_BuyFuel = value;
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
            }
        }
        #endregion

        /// <summary>
        /// 窗体初始化
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
            // 卸载设备
            UnloadHardware();
        }

        #region 设备相关

        #region 地磅仪表

        /// <summary>
        /// 重量稳定事件
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
        /// 地磅仪表状态变化
        /// </summary>
        /// <param name="status"></param>
        void Wber_OnStatusChange(bool status)
        {
            // 接收设备状态 
            InvokeEx(() =>
            {
                slightWber.LightColor = (status ? Color.Green : Color.Red);

                commonDAO.SetSignalDataValue(CommonAppConfig.GetInstance().AppIdentifier, eSignalDataName.地磅仪表_连接状态.ToString(), status ? "1" : "0");
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

        #region 设备初始化与卸载

        /// <summary>
        /// 初始化外接设备
        /// </summary>
        private void InitHardware()
        {
            try
            {
                bool success = false;

                this.WbMinWeight = commonDAO.GetAppletConfigDouble("地磅仪表_最小称重");

                // 地磅仪表
                Hardwarer.Wber.OnStatusChange += new WB.TOLEDO.YAOHUA.TOLEDO_YAOHUAWber.StatusChangeHandler(Wber_OnStatusChange);
                Hardwarer.Wber.OnSteadyChange += new WB.TOLEDO.YAOHUA.TOLEDO_YAOHUAWber.SteadyChangeEventHandler(Wber_OnSteadyChange);
                Hardwarer.Wber.OnWeightChange += new WB.TOLEDO.YAOHUA.TOLEDO_YAOHUAWber.WeightChangeEventHandler(Wber_OnWeightChange);
                success = Hardwarer.Wber.OpenCom(commonDAO.GetAppletConfigInt32("地磅仪表_串口"), commonDAO.GetAppletConfigInt32("地磅仪表_波特率"), commonDAO.GetAppletConfigInt32("地磅仪表_数据位"), commonDAO.GetAppletConfigInt32("地磅仪表_停止位"));

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
        }

        #endregion

        #endregion

        #region 公共业务

        /// <summary>
        /// 慢速任务
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer2_Tick(object sender, EventArgs e)
        {
            timer2.Stop();
            // 三分钟执行一次
            timer2.Interval = 180000;

            try
            {
                // 入厂煤
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

        #region 入厂煤业务

        CmcsBuyFuelTransport currentBuyFuelTransport;
        /// <summary>
        /// 当前运输记录
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
        /// 加载矿点
        /// </summary>
        void LoadMine(ComboBoxEx comboBoxEx)
        {
            comboBoxEx.DisplayMember = "Name";
            comboBoxEx.ValueMember = "Id";
            comboBoxEx.DataSource = Dbers.GetInstance().SelfDber.Entities<CmcsMine>("where Valid='有效' and ParentId is not null order by Sequence");
            this.SelectedMine_BuyFuel = comboBoxEx.SelectedItem as CmcsMine;
        }

        /// <summary>
        /// 加载煤种
        /// </summary>
        void LoadFuelkind(ComboBoxEx comboBoxEx)
        {
            comboBoxEx.DisplayMember = "FuelName";
            comboBoxEx.ValueMember = "Id";
            comboBoxEx.DataSource = Dbers.GetInstance().SelfDber.Entities<CmcsFuelKind>("where Valid='有效' and ParentId is not null order by Sequence");

            this.SelectedFuelKind_BuyFuel = comboBoxEx.SelectedItem as CmcsFuelKind;
        }

        /// <summary>
        /// 保存入厂煤运输记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSaveTransport_BuyFuel_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.txt_CarNumber.Text)) { MessageBoxEx.Show("请填写车牌号", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            if (this.SelectedMine_BuyFuel == null) { MessageBoxEx.Show("请选择矿点", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            if (this.SelectedFuelKind_BuyFuel == null) { MessageBoxEx.Show("请选择煤种", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }

            if ((decimal)Hardwarer.Wber.Weight <= 0) { MessageBoxEx.Show("重量不能小于0", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning); return; }
            if (!SaveBuyFuelTransport())
                MessageBoxEx.Show("保存失败", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
            {
                MessageBoxEx.Show("保存成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                ResetBuyFuel();
            }
        }

        /// <summary>
        /// 保存运输记录
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
                    // 查找该车未完成的运输记录
                    CmcsUnFinishTransport unFinishTransport = carTransportDAO.GetUnFinishTransportByAutotruckId(this.CurrentAutotruck.Id, eCarType.入场煤.ToString());

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
                MessageBoxEx.Show("保存失败\r\n" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);

                Log4Neter.Error("保存运输记录", ex);
            }

            return false;
        }

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
        /// 获取未完成的入厂煤记录
        /// </summary>
        void LoadTodayUnFinishBuyFuelTransport()
        {
            superGridControl1_BuyFuel.PrimaryGrid.DataSource = weighterDAO.GetUnFinishBuyFuelTransport();
        }

        /// <summary>
        /// 获取指定日期已完成的入厂煤记录
        /// </summary>
        void LoadTodayFinishBuyFuelTransport()
        {
            superGridControl2_BuyFuel.PrimaryGrid.DataSource = weighterDAO.GetFinishedBuyFuelTransport(DateTime.Now.Date, DateTime.Now.Date.AddDays(1));
        }

        #endregion

        #region 其他函数
        /// <summary>
        /// 选择煤种
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbFuelName_BuyFuel_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SelectedFuelKind_BuyFuel = cmbFuelKindName_BuyFuel.SelectedItem as CmcsFuelKind;
        }

        /// <summary>
        /// 选择矿点
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmbMineName_BuyFuel_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SelectedMine_BuyFuel = cmbMineName_BuyFuel.SelectedItem as CmcsMine;
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

        #region 创建省份选择按钮

        /// <summary>
        /// 创建省份简称按钮
        /// </summary>
        private void CreateProvinceAbbreviationButton()
        {
            flpanProvinceAbbreviation.Controls.Clear();

            foreach (CmcsProvinceAbbreviation provinceAbbreviation in CarTransportDAO.GetInstance().GetProvinceAbbreviationsInOrder())
            {
                ButtonX btnProvinceAbbreviation = new ButtonX();
                btnProvinceAbbreviation.Text = provinceAbbreviation.PaName;
                btnProvinceAbbreviation.Style = eDotNetBarStyle.Metro;
                btnProvinceAbbreviation.Font = new Font("微软雅黑", 10.8f, FontStyle.Bold);
                btnProvinceAbbreviation.Size = new Size(26, 26);
                btnProvinceAbbreviation.Margin = new System.Windows.Forms.Padding(3);
                btnProvinceAbbreviation.Click += BtnProvinceAbbreviation_Click;

                flpanProvinceAbbreviation.Controls.Add(btnProvinceAbbreviation);
            }
        }

        /// <summary>
        /// 点击省份简称按钮
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
        /// 省份事件
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
        /// 选择回皮车辆
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSelectAutotruck_BuyFuel_Click(object sender, EventArgs e)
        {
            FrmUnFinishTransport_Select frm = new FrmUnFinishTransport_Select("where CarType='" + eCarType.入场煤.ToString() + "' order by CreateDate desc");
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
        /// 打印磅单
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
            // 取消进入编辑
            e.Cancel = true;
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
        /// 刷新数据
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
