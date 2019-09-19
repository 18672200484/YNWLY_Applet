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
using CMCS.Common.DAO;
using CMCS.Common.Utilities;
using CMCS.CarTransport.DAO;
using CMCS.Common.Enums;
using DevComponents.DotNetBar.Controls;

namespace CMCS.CarTransport.Queue.Frms.Transport.BuyFuelTransport
{
    public partial class FrmBuyFuelTransport_Oper : DevComponents.DotNetBar.Metro.MetroForm
    {
        String id = String.Empty;
        bool edit = false;
        CmcsBuyFuelTransport cmcsBuyFuelTransport;
        CmcsTransportCompany cmcsTransportCompany;
        CmcsMine cmcsMine;
        CmcsSupplier cmcsSupplier;
        CmcsFuelKind cmcsFuelKind;
        List<CmcsBuyFuelTransportDeduct> cmcsbuyfueltransportdeducts;
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
            this.cmcsFuelKind = cmbFuelName_BuyFuel.SelectedItem as CmcsFuelKind;
        }

        /// <summary>
        /// 加载采样方式
        /// </summary>
        void LoadSampleType(ComboBoxEx comboBoxEx)
        {
            comboBoxEx.DisplayMember = "Content";
            comboBoxEx.ValueMember = "Code";
            comboBoxEx.DataSource = CommonDAO.GetInstance().GetCodeContentByKind("采样方式");

            comboBoxEx.Text = eSamplingType.机械采样.ToString();
        }

        private void FrmBuyFuelTransport_Oper_Load(object sender, EventArgs e)
        {
            LoadSampleType(cmb_SampingType);
            cmbFuelName_BuyFuel.DisplayMember = "FuelName";
            cmbFuelName_BuyFuel.ValueMember = "Id";
            cmbFuelName_BuyFuel.DataSource = Dbers.GetInstance().SelfDber.Entities<CmcsFuelKind>("where Valid='有效' and ParentId is not null");
            cmbFuelName_BuyFuel.SelectedIndex = 0;
            if (!String.IsNullOrEmpty(id))
            {
                this.cmcsBuyFuelTransport = Dbers.GetInstance().SelfDber.Get<CmcsBuyFuelTransport>(this.id);
                txt_SerialNumber.Text = cmcsBuyFuelTransport.SerialNumber;
                txt_CarNumber.Text = cmcsBuyFuelTransport.CarNumber;
                CmcsInFactoryBatch cmcsinfactorybatch = Dbers.GetInstance().SelfDber.Get<CmcsInFactoryBatch>(cmcsBuyFuelTransport.InFactoryBatchId);
                if (cmcsinfactorybatch != null)
                {
                    txt_InFactoryBatchNumber.Text = cmcsinfactorybatch.Batch;
                }
                if (cmcsBuyFuelTransport.SupplierId != null)
                {
                    CmcsSupplier supplier = Dbers.GetInstance().SelfDber.Get<CmcsSupplier>(cmcsBuyFuelTransport.SupplierId);
                    if (supplier != null)
                        txt_SupplierName.Text = supplier.Name;
                }
                if (cmcsBuyFuelTransport.TransportCompanyId != null)
                {
                    CmcsTransportCompany company = Dbers.GetInstance().SelfDber.Get<CmcsTransportCompany>(cmcsBuyFuelTransport.TransportCompanyId);
                    if (company != null)
                        txt_TransportCompanyName.Text = company.Name;
                }
                if (cmcsBuyFuelTransport.MineId != null)
                {
                    CmcsMine mine = Dbers.GetInstance().SelfDber.Get<CmcsMine>(cmcsBuyFuelTransport.MineId);
                    if (mine != null)
                        txt_MineName.Text = mine.Name;
                }
                if (cmcsBuyFuelTransport.FuelKindId != null)
                {
                    CmcsFuelKind fuelkind = Dbers.GetInstance().SelfDber.Get<CmcsFuelKind>(cmcsBuyFuelTransport.FuelKindId);
                    if (fuelkind != null)
                        cmbFuelName_BuyFuel.Text = fuelkind.FuelName;
                }
                cmb_SampingType.SelectedItem = cmcsBuyFuelTransport.SamplingType;
                dbi_TicketWeight.Value = (double)cmcsBuyFuelTransport.TicketWeight;
                dbi_GrossWeight.Value = (double)cmcsBuyFuelTransport.GrossWeight;
                dbi_TareWeight.Value = (double)cmcsBuyFuelTransport.TareWeight;
                dbi_DeductWeight.Value = (double)cmcsBuyFuelTransport.DeductWeight;
                dbi_SuttleWeight.Value = (double)cmcsBuyFuelTransport.SuttleWeight;
                dbi_CheckWeight.Value = (double)cmcsBuyFuelTransport.CheckWeight;
                dbi_ProfitWeight.Value = (double)cmcsBuyFuelTransport.ProfitAndLossWeight;
                dbi_KgWeight.Value = (double)cmcsBuyFuelTransport.KgWeight;
                dbi_KsWeight.Value = (double)cmcsBuyFuelTransport.KsWeight;
                dbi_AutoKsWeight.Value = (double)cmcsBuyFuelTransport.AutoKsWeight;
                txt_UnloadArea.Text = cmcsBuyFuelTransport.UnLoadArea;
                txt_InFactoryTime.Text = cmcsBuyFuelTransport.InFactoryTime.Year == 1 ? "" : cmcsBuyFuelTransport.InFactoryTime.ToString();
                txt_SamplingTime.Text = cmcsBuyFuelTransport.SamplingTime.Year == 1 ? "" : cmcsBuyFuelTransport.SamplingTime.ToString();
                txt_GrossTime.Text = cmcsBuyFuelTransport.GrossTime.Year == 1 ? "" : cmcsBuyFuelTransport.GrossTime.ToString();
                txt_UploadTime.Text = cmcsBuyFuelTransport.UploadTime.Year == 1 ? "" : cmcsBuyFuelTransport.UploadTime.ToString();
                txt_TareTime.Text = cmcsBuyFuelTransport.TareTime.Year == 1 ? "" : cmcsBuyFuelTransport.TareTime.ToString();
                txt_OutFactoryTime.Text = cmcsBuyFuelTransport.OutFactoryTime.Year == 1 ? "" : cmcsBuyFuelTransport.OutFactoryTime.ToString();
                txt_Remark.Text = cmcsBuyFuelTransport.Remark;
                chb_IsFinish.Checked = (cmcsBuyFuelTransport.IsFinish == 1);
                chb_IsUse.Checked = (cmcsBuyFuelTransport.IsUse == 1);
                cmb_SampingType.Text = cmcsBuyFuelTransport.SamplingType;
                ShowDeduct(this.cmcsBuyFuelTransport.Id);
            }
            if (!edit)
            {
                btnSubmit.Enabled = false;
                CMCS.CarTransport.Queue.Utilities.Helper.ControlReadOnly(panelEx2);
            }
        }

        public void ShowDeduct(String newId)
        {
            cmcsbuyfueltransportdeducts = Dbers.GetInstance().SelfDber.Entities<CmcsBuyFuelTransportDeduct>(" where TransportId=:TransportId", new { TransportId = newId });
            superGridControl1.PrimaryGrid.DataSource = cmcsbuyfueltransportdeducts;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (txt_SerialNumber.Text.Length == 0)
            {
                MessageBoxEx.Show("该标车牌号不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if ((cmcsBuyFuelTransport == null || cmcsBuyFuelTransport.CarNumber != txt_CarNumber.Text) && Dbers.GetInstance().SelfDber.Entities<CmcsBuyFuelTransport>(" where CarNumber=:CarNumber and IsFinish=0", new { CarNumber = txt_CarNumber.Text }).Count > 0)
            {
                MessageBoxEx.Show("该标车牌号不可重复！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (cmcsBuyFuelTransport != null)
            {
                if (!CompareClass.CompareClassValue(this.cmcsBuyFuelTransport, Dbers.GetInstance().SelfDber.Get<CmcsBuyFuelTransport>(this.id)))
                {
                    MessageBoxEx.Show("数据已更改请重新打开页面修改保存！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (cmcsBuyFuelTransport.CarNumber != txt_CarNumber.Text)
                {
                    CmcsAutotruck autotruck = Dbers.GetInstance().SelfDber.Entity<CmcsAutotruck>(" where CarNumber=:CarNumber", new { CarNumber = this.txt_CarNumber.Text });
                    if (autotruck != null)
                    {
                        cmcsBuyFuelTransport.AutotruckId = autotruck.Id;
                        CmcsUnFinishTransport unfinish = Dbers.GetInstance().SelfDber.Entity<CmcsUnFinishTransport>(" where TransportId=:TransportId", new { TransportId = cmcsBuyFuelTransport.Id });
                        if (unfinish != null)
                        {
                            unfinish.AutotruckId = autotruck.Id;
                            Dbers.GetInstance().SelfDber.Update(unfinish);
                        }
                    }
                }
                cmcsBuyFuelTransport.SerialNumber = txt_SerialNumber.Text;
                cmcsBuyFuelTransport.CarNumber = txt_CarNumber.Text;
                if (cmcsSupplier != null)
                {
                    cmcsBuyFuelTransport.SupplierId = cmcsSupplier.Id;
                    cmcsBuyFuelTransport.SupplierName = cmcsSupplier.Name;
                }
                if (cmcsTransportCompany != null)
                {
                    cmcsBuyFuelTransport.TransportCompanyId = cmcsTransportCompany.Id;

                }
                if (cmcsMine != null)
                {
                    cmcsBuyFuelTransport.MineId = cmcsMine.Id;
                    cmcsBuyFuelTransport.MineName = cmcsMine.Name;
                }
                if (cmcsFuelKind != null)
                {
                    cmcsBuyFuelTransport.FuelKindId = cmcsFuelKind.Id;
                    cmcsBuyFuelTransport.FuelKindName = cmcsFuelKind.FuelName;
                }

                cmcsBuyFuelTransport.SamplingType = (string)cmb_SampingType.Text;
                cmcsBuyFuelTransport.TicketWeight = (decimal)dbi_TicketWeight.Value;
                cmcsBuyFuelTransport.GrossWeight = (decimal)dbi_GrossWeight.Value;
                cmcsBuyFuelTransport.KsWeight = (decimal)dbi_KsWeight.Value;
                cmcsBuyFuelTransport.KgWeight = (decimal)dbi_KgWeight.Value;
                cmcsBuyFuelTransport.AutoKsWeight = (decimal)dbi_AutoKsWeight.Value;
                cmcsBuyFuelTransport.DeductWeight = cmcsBuyFuelTransport.KsWeight + cmcsBuyFuelTransport.KgWeight + cmcsBuyFuelTransport.AutoKsWeight;
                cmcsBuyFuelTransport.TareWeight = (decimal)dbi_TareWeight.Value;
                cmcsBuyFuelTransport.SuttleWeight = (decimal)dbi_SuttleWeight.Value;
                txt_Remark.Text = cmcsBuyFuelTransport.Remark;
                cmcsBuyFuelTransport.IsFinish = (chb_IsFinish.Checked ? 1 : 0);
                cmcsBuyFuelTransport.IsUse = (chb_IsUse.Checked ? 1 : 0);

                CmcsInFactoryBatch inFactoryBatch = CarTransportDAO.GetInstance().GCQCInFactoryBatchByBuyFuelTransport(cmcsBuyFuelTransport, null);
                if (inFactoryBatch != null)
                {
                    cmcsBuyFuelTransport.InFactoryBatchId = inFactoryBatch.Id;
                }
                else
                {
                    MessageBoxEx.Show("供应商、矿点、煤种不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                cmcsBuyFuelTransport.IsSynch = "0";
                WeighterDAO.GetInstance().SaveBuyFuelTransport(cmcsBuyFuelTransport);
                SaveAndUpdate(cmcsBuyFuelTransport, cmcsbuyfueltransportdeducts);
            }
            else
            {
                cmcsBuyFuelTransport = new CmcsBuyFuelTransport();
                cmcsBuyFuelTransport.SerialNumber = txt_SerialNumber.Text;
                cmcsBuyFuelTransport.CarNumber = txt_CarNumber.Text;
                if (cmcsSupplier != null)
                {
                    cmcsBuyFuelTransport.SupplierId = cmcsSupplier.Id;
                }
                if (cmcsTransportCompany != null)
                {
                    cmcsBuyFuelTransport.TransportCompanyId = cmcsTransportCompany.Id;
                }
                if (cmcsMine != null)
                {
                    cmcsBuyFuelTransport.MineId = cmcsMine.Id;
                }
                if (cmcsFuelKind != null)
                {
                    cmcsBuyFuelTransport.FuelKindId = cmcsFuelKind.Id;
                }
                cmcsBuyFuelTransport.SamplingType = (string)cmb_SampingType.SelectedItem;
                cmcsBuyFuelTransport.TicketWeight = (decimal)dbi_TicketWeight.Value;
                cmcsBuyFuelTransport.GrossWeight = (decimal)dbi_GrossWeight.Value;
                cmcsBuyFuelTransport.DeductWeight = (decimal)dbi_DeductWeight.Value;
                cmcsBuyFuelTransport.TareWeight = (decimal)dbi_TareWeight.Value;
                txt_Remark.Text = cmcsBuyFuelTransport.Remark;
                cmcsBuyFuelTransport.IsFinish = (chb_IsFinish.Checked ? 1 : 0);
                cmcsBuyFuelTransport.IsUse = (chb_IsUse.Checked ? 1 : 0);

                WeighterDAO.GetInstance().SaveBuyFuelTransport(cmcsBuyFuelTransport);
                SaveAndUpdate(cmcsBuyFuelTransport, cmcsbuyfueltransportdeducts);
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void buttonX2_Click(object sender, EventArgs e)
        {

            FrmBuyFuelTransportDeduct_Oper frmEdit = new FrmBuyFuelTransportDeduct_Oper(String.Empty, true, cmcsbuyfueltransportdeducts);
            if (frmEdit.ShowDialog() == DialogResult.OK)
            {
                cmcsbuyfueltransportdeducts = superGridControl1.PrimaryGrid.DataSource as List<CmcsBuyFuelTransportDeduct>;
                cmcsbuyfueltransportdeducts = cmcsbuyfueltransportdeducts.Where(a => a.Id != frmEdit.cmcsBuyFuelTransportDeduct.Id).ToList();
                cmcsbuyfueltransportdeducts.Add(frmEdit.cmcsBuyFuelTransportDeduct);
                superGridControl1.PrimaryGrid.DataSource = cmcsbuyfueltransportdeducts;
                dbi_DeductWeight.Value = (double)cmcsbuyfueltransportdeducts.Select(a => a.DeductWeight).Sum();
            }
        }

        void SaveAndUpdate(CmcsBuyFuelTransport item, List<CmcsBuyFuelTransportDeduct> details)
        {
            List<CmcsBuyFuelTransportDeduct> olds = Dbers.GetInstance().SelfDber.Entities<CmcsBuyFuelTransportDeduct>(" where TransportId=:TransportId", new { TransportId = item.Id });
            foreach (CmcsBuyFuelTransportDeduct old in olds)
            {
                CmcsBuyFuelTransportDeduct del = details.Where(a => a.Id == old.Id).FirstOrDefault();
                if (del == null)
                {
                    Dbers.GetInstance().SelfDber.Delete<CmcsBuyFuelTransportDeduct>(old.Id);
                }
            }
            foreach (var detail in details)
            {
                detail.TransportId = item.Id;
                CmcsBuyFuelTransportDeduct insertorupdate = olds.Where(a => a.Id == detail.Id).FirstOrDefault();
                if (insertorupdate == null)
                {
                    Dbers.GetInstance().SelfDber.Insert(detail);
                }
                else
                {
                    Dbers.GetInstance().SelfDber.Update(detail);
                }
            }
        }

        private void superGridControl1_CellMouseDown(object sender, DevComponents.DotNetBar.SuperGrid.GridCellMouseEventArgs e)
        {
            CmcsBuyFuelTransportDeduct entity = cmcsbuyfueltransportdeducts.Where(a => a.Id == superGridControl1.PrimaryGrid.GetCell(e.GridCell.GridRow.Index, superGridControl1.PrimaryGrid.Columns["clmId"].ColumnIndex).Value.ToString()).FirstOrDefault();
            switch (superGridControl1.PrimaryGrid.Columns[e.GridCell.ColumnIndex].Name)
            {

                case "clmShow":
                    FrmBuyFuelTransportDeduct_Oper frmShow = new FrmBuyFuelTransportDeduct_Oper(entity.Id, false, cmcsbuyfueltransportdeducts);
                    if (frmShow.ShowDialog() == DialogResult.OK)
                    {
                        superGridControl1.PrimaryGrid.DataSource = cmcsbuyfueltransportdeducts;
                        dbi_DeductWeight.Value = (double)cmcsbuyfueltransportdeducts.Select(a => a.DeductWeight).Sum();
                    }
                    break;
                case "clmEdit":
                    FrmBuyFuelTransportDeduct_Oper frmEdit = new FrmBuyFuelTransportDeduct_Oper(entity.Id, true, cmcsbuyfueltransportdeducts);
                    if (frmEdit.ShowDialog() == DialogResult.OK)
                    {
                        cmcsbuyfueltransportdeducts = cmcsbuyfueltransportdeducts.Where(a => a.Id != frmEdit.cmcsBuyFuelTransportDeduct.Id).ToList();
                        cmcsbuyfueltransportdeducts.Add(frmEdit.cmcsBuyFuelTransportDeduct);
                        superGridControl1.PrimaryGrid.DataSource = cmcsbuyfueltransportdeducts;
                        dbi_DeductWeight.Value = (double)cmcsbuyfueltransportdeducts.Select(a => a.DeductWeight).Sum();
                    }
                    break;
                case "clmDelete":

                    cmcsbuyfueltransportdeducts = cmcsbuyfueltransportdeducts.Where(a => a.Id != entity.Id).ToList();
                    superGridControl1.PrimaryGrid.DataSource = cmcsbuyfueltransportdeducts;
                    dbi_DeductWeight.Value = (double)cmcsbuyfueltransportdeducts.Select(a => a.DeductWeight).Sum();
                    break;
            }
        }


        private void superGridControl1_BeginEdit(object sender, DevComponents.DotNetBar.SuperGrid.GridEditEventArgs e)
        {
            // 取消编辑
            e.Cancel = true;
        }

        private void btnSupplier_Click(object sender, EventArgs e)
        {
            FrmSupplier_Select Frm = new FrmSupplier_Select();
            Frm.ShowDialog();
            if (Frm.DialogResult == DialogResult.OK)
            {
                cmcsSupplier = Frm.Output;
                this.txt_SupplierName.Text = Frm.Output.Name;
            }
        }

        private void btnTransportCompany_Click(object sender, EventArgs e)
        {
            FrmTransportCompany_Select Frm = new FrmTransportCompany_Select();
            Frm.ShowDialog();
            if (Frm.DialogResult == DialogResult.OK)
            {
                cmcsTransportCompany = Frm.Output;
                this.txt_SupplierName.Text = Frm.Output.Name;
            }
        }

        private void BtnMine_Click(object sender, EventArgs e)
        {
            FrmMine_Select Frm = new FrmMine_Select();
            Frm.ShowDialog();
            if (Frm.DialogResult == DialogResult.OK)
            {
                cmcsMine = Frm.Output;
                this.txt_MineName.Text = Frm.Output.Name;
            }
        }

        private void btnFuelKind_Click(object sender, EventArgs e)
        {
            //FrmFuelKind_Select Frm = new FrmFuelKind_Select();
            //Frm.ShowDialog();
            //if (Frm.DialogResult == DialogResult.OK)
            //{
            //    cmcsMine = Frm.Output;
            //}
        }

        private void btnCarNumber_Click(object sender, EventArgs e)
        {
            FrmAutotruck_Select frm = new FrmAutotruck_Select(" and IsUse=1 order by CarNumber asc");
            if (frm.ShowDialog() == DialogResult.OK)
            {
                this.txt_CarNumber.Text = frm.Output.CarNumber;
            }
        }
        /// <summary>
        /// 计算扣吨
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dbi_KgWeight_TextChanged(object sender, EventArgs e)
        {
            this.dbi_DeductWeight.Value = this.dbi_KsWeight.Value + this.dbi_KgWeight.Value + this.dbi_AutoKsWeight.Value;
        }
    }
}