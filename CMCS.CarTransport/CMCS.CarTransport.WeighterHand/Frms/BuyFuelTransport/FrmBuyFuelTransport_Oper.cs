using System;
using System.Linq;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using CMCS.Common;
using CMCS.Common.Entities.CarTransport;
using CMCS.Common.Entities;
using CMCS.Common.Entities.BaseInfo;
using CMCS.Common.Entities.Fuel;
using DevComponents.DotNetBar.Controls;
using CMCS.Common.DAO;
using CMCS.CarTransport.DAO;
using CMCS.CarTransport.WeighterHand.Core;
using CMCS.Common.Enums;
using CMCS.CarTransport.WeighterHand.Utilities;
using CMCS.Common.Utilities;

namespace CMCS.CarTransport.WeighterHand.Frms.Transport.BuyFuelTransport
{
    public partial class FrmBuyFuelTransport_Oper : DevComponents.DotNetBar.Metro.MetroForm
    {
        String id = String.Empty;
        bool edit = false;
        CmcsBuyFuelTransport cmcsBuyFuelTransport;
        CommonDAO commonDAO = CommonDAO.GetInstance();

        #region Vars

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
                    this.txt_CarNumber.Text = value.CarNumber;
                }
                else
                {
                    this.txt_CarNumber.ResetText();
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
                    txt_MineName.Text = value.Name;
                }
                else
                {
                    txt_MineName.ResetText();
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

        bool hasManagePower = false;
        /// <summary>
        /// 对否有维护权限
        /// </summary>
        public bool HasManagePower
        {
            get
            {
                return hasManagePower;
            }

            set
            {
                hasManagePower = value;
                dbi_GrossWeight.IsInputReadOnly = !value;
                dbi_TareWeight.IsInputReadOnly = !value;
            }
        }

        #endregion

        public FrmBuyFuelTransport_Oper()
        {
            InitializeComponent();
        }
        public FrmBuyFuelTransport_Oper(String pId, bool pEdit)
        {
            InitializeComponent();
            id = pId;
            edit = pEdit;
        }

        private void cmbFuelName_BuyFuel_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.SelectedFuelKind_BuyFuel = cmbFuelName_BuyFuel.SelectedItem as CmcsFuelKind;
        }
        private void FrmBuyFuelTransport_Oper_Load(object sender, EventArgs e)
        {
            cmbFuelName_BuyFuel.DisplayMember = "FuelName";
            cmbFuelName_BuyFuel.ValueMember = "Id";
            cmbFuelName_BuyFuel.DataSource = Dbers.GetInstance().SelfDber.Entities<CmcsFuelKind>("where Valid='有效' and ParentId is not null");
            cmbFuelName_BuyFuel.SelectedIndex = 0;
            HasManagePower = CommonDAO.GetInstance().HasResourcePowerByResCode(SelfVars.LoginUser.UserAccount, eUserRoleCodes.汽车智能化信息维护.ToString());

            if (!String.IsNullOrEmpty(id))
            {
                this.cmcsBuyFuelTransport = Dbers.GetInstance().SelfDber.Get<CmcsBuyFuelTransport>(this.id);
                txt_SerialNumber.Text = cmcsBuyFuelTransport.SerialNumber;
                txt_CarNumber.Text = cmcsBuyFuelTransport.CarNumber;

                txt_MineName.Text = cmcsBuyFuelTransport.MineName;
                cmbFuelName_BuyFuel.Text = cmcsBuyFuelTransport.FuelKindName;
                dbi_TicketWeight.Value = (double)cmcsBuyFuelTransport.TicketWeight;
                dbi_GrossWeight.Value = (double)cmcsBuyFuelTransport.GrossWeight;
                dbi_TareWeight.Value = (double)cmcsBuyFuelTransport.TareWeight;
                dbi_AutoKsWeight.Value = (double)cmcsBuyFuelTransport.AutoKsWeight;
                dbi_KsWeight.Value = (double)cmcsBuyFuelTransport.KsWeight;
                dbi_KgWeight.Value = (double)cmcsBuyFuelTransport.KgWeight;
                dbi_DeductWeight.Value = (double)cmcsBuyFuelTransport.DeductWeight;
                dbi_SuttleWeight.Value = (double)cmcsBuyFuelTransport.SuttleWeight;
                dbi_CheckWeight.Value = (double)(cmcsBuyFuelTransport.CheckWeight);
                txt_GrossTime.Text = cmcsBuyFuelTransport.GrossTime.Year == 1 ? "" : cmcsBuyFuelTransport.GrossTime.ToString();
                txt_TareTime.Text = cmcsBuyFuelTransport.TareTime.Year == 1 ? "" : cmcsBuyFuelTransport.TareTime.ToString();
                txt_Remark.Text = cmcsBuyFuelTransport.Remark;
                chb_IsFinish.Checked = (cmcsBuyFuelTransport.IsFinish == 1);
                chb_IsUse.Checked = (cmcsBuyFuelTransport.IsUse == 1);
            }
            if (!edit)
            {
                btnSubmit.Enabled = false;
                CMCS.CarTransport.WeighterHand.Utilities.Helper.ControlReadOnly(panelEx2);
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (cmcsBuyFuelTransport == null)
            {
                MessageBoxEx.Show("当前记录为空,请返回列表页面重试！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (txt_CarNumber.Text.Length == 0)
            {
                MessageBoxEx.Show("车牌号不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            //if ((cmcsBuyFuelTransport == null || cmcsBuyFuelTransport.CarNumber != txt_CarNumber.Text) && Dbers.GetInstance().SelfDber.Entities<CmcsBuyFuelTransport>(" where CarNumber=:CarNumber and IsFinish='1'", new { CarNumber = txt_CarNumber.Text }).Count > 0)
            //{
            //    MessageBoxEx.Show("车牌号不可重复！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
            //if ((decimal)dbi_TicketWeight.Value <= 0)
            //{
            //    MessageBoxEx.Show("请填写正确的矿发量！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //    return;
            //}
            if (cmcsBuyFuelTransport != null)
            {
                CmcsAutotruck autoTruck = commonDAO.SelfDber.Entity<CmcsAutotruck>("where CarNumber=:CarNumber", new { CarNumber = this.txt_CarNumber.Text });
                if (autoTruck == null)
                {
                    autoTruck = new CmcsAutotruck() { CarNumber = this.txt_CarNumber.Text };
                    commonDAO.SelfDber.Insert(autoTruck);
                }
                //车号发生改变
                if (cmcsBuyFuelTransport.CarNumber != txt_CarNumber.Text)
                {
                    cmcsBuyFuelTransport.AutotruckId = autoTruck.Id;
                    CmcsUnFinishTransport unFinishTransprot = CarTransportDAO.GetInstance().GetUnFinishTransportByAutotruckId(cmcsBuyFuelTransport.Id);
                    if (unFinishTransprot != null)
                    {
                        unFinishTransprot.AutotruckId = autoTruck.Id;
                        commonDAO.SelfDber.Update(unFinishTransprot);
                    }
                }
                cmcsBuyFuelTransport.SerialNumber = txt_SerialNumber.Text;
                cmcsBuyFuelTransport.CarNumber = txt_CarNumber.Text;

                if (this.SelectedMine_BuyFuel != null)
                {
                    cmcsBuyFuelTransport.MineId = this.SelectedMine_BuyFuel.Id;
                    cmcsBuyFuelTransport.MineName = this.SelectedMine_BuyFuel.Name;
                }
                if (this.SelectedFuelKind_BuyFuel != null)
                {
                    cmcsBuyFuelTransport.FuelKindId = this.SelectedFuelKind_BuyFuel.Id;
                    cmcsBuyFuelTransport.FuelKindName = this.SelectedFuelKind_BuyFuel.FuelName;
                }
                if (cmcsBuyFuelTransport.GrossWeight != (decimal)dbi_GrossWeight.Value || cmcsBuyFuelTransport.TareWeight != (decimal)dbi_TareWeight.Value)
                {
                    Log4Neter.Info(string.Format("{0}修改,修改前毛重:{1},修改前皮重:{2}", SelfVars.LoginUser.UserName, cmcsBuyFuelTransport.GrossWeight, cmcsBuyFuelTransport.TareWeight));
                    commonDAO.SaveAppletLog(eAppletLogLevel.Info, "修改运输记录", string.Format("{0}修改,修改前毛重:{1},修改前皮重:{2}", SelfVars.LoginUser.UserName, cmcsBuyFuelTransport.GrossWeight, cmcsBuyFuelTransport.TareWeight));
                }
                cmcsBuyFuelTransport.Remark = txt_Remark.Text;
                cmcsBuyFuelTransport.TicketWeight = (decimal)dbi_TicketWeight.Value;
                cmcsBuyFuelTransport.GrossWeight = (decimal)dbi_GrossWeight.Value;
                cmcsBuyFuelTransport.DeductWeight = (decimal)dbi_DeductWeight.Value;
                cmcsBuyFuelTransport.TareWeight = (decimal)dbi_TareWeight.Value;
                cmcsBuyFuelTransport.SuttleWeight = (decimal)dbi_SuttleWeight.Value;
                txt_Remark.Text = cmcsBuyFuelTransport.Remark;
                cmcsBuyFuelTransport.IsFinish = (chb_IsFinish.Checked ? 1 : 0);
                cmcsBuyFuelTransport.IsUse = (chb_IsUse.Checked ? 1 : 0);

                cmcsBuyFuelTransport.KsWeight = (decimal)dbi_KsWeight.Value;
                cmcsBuyFuelTransport.KgWeight = (decimal)dbi_KgWeight.Value;
                WeighterDAO.GetInstance().SaveBuyFuelTransport(cmcsBuyFuelTransport);
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void BtnMine_Click(object sender, EventArgs e)
        {
            FrmMine_Select Frm = new FrmMine_Select("where Valid='有效' and parentId is not null order by Name asc");
            Frm.ShowDialog();
            if (Frm.DialogResult == DialogResult.OK)
            {
                this.SelectedMine_BuyFuel = Frm.Output;
            }
        }

        #region DataGridView

        private void superGridControl1_CellMouseDown(object sender, DevComponents.DotNetBar.SuperGrid.GridCellMouseEventArgs e)
        {

        }

        private void superGridControl1_BeginEdit(object sender, DevComponents.DotNetBar.SuperGrid.GridEditEventArgs e)
        {
            // 取消编辑
            e.Cancel = true;
        }

        #endregion

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
    }
}