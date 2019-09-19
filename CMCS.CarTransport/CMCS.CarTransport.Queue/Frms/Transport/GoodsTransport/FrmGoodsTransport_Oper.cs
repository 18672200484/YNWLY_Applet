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
using CMCS.Common.Utilities;

namespace CMCS.CarTransport.Queue.Frms.Transport.GoodsTransport
{
    public partial class FrmGoodsTransport_Oper : DevComponents.DotNetBar.Metro.MetroForm
    {
        String id = String.Empty;
        bool edit = false;
        CmcsGoodsTransport cmcsGoodsTransport;
        CmcsSupplyReceive supplyUnit;
        CmcsSupplyReceive receiveUnit;
        CmcsGoodsType cmcsGoodsType;

        public FrmGoodsTransport_Oper()
        {
            InitializeComponent();
        }
        public FrmGoodsTransport_Oper(String pId, bool pEdit)
        {
            InitializeComponent();
            id = pId;
            edit = pEdit;
        }


        private void FrmGoodsTransport_Oper_Load(object sender, EventArgs e)
        {
            cmb_GoodsTypeName.DataSource = Dbers.GetInstance().SelfDber.Entities<CmcsGoodsType>(" where ParentId is not null order by OrderNumber");
            cmb_GoodsTypeName.DisplayMember = "GoodsName";
            cmb_GoodsTypeName.ValueMember = "Id";
            cmb_GoodsTypeName.SelectedIndex = 0;

            if (!String.IsNullOrEmpty(id))
            {
                this.cmcsGoodsTransport = Dbers.GetInstance().SelfDber.Get<CmcsGoodsTransport>(this.id);
                txt_SerialNumber.Text = cmcsGoodsTransport.SerialNumber;
                txt_CarNumber.Text = cmcsGoodsTransport.CarNumber;

                txt_SupplyUnitName.Text = Dbers.GetInstance().SelfDber.Get<CmcsSupplyReceive>(cmcsGoodsTransport.SupplyUnitId).UnitName;
                txt_ReceiveUnitName.Text = Dbers.GetInstance().SelfDber.Get<CmcsSupplyReceive>(cmcsGoodsTransport.ReceiveUnitId).UnitName;
                cmb_GoodsTypeName.Text = Dbers.GetInstance().SelfDber.Get<CmcsGoodsType>(cmcsGoodsTransport.GoodsTypeId).GoodsName;
                dbi_FirstWeight.Value = (double)cmcsGoodsTransport.FirstWeight;
                dbi_SecondWeight.Value = (double)cmcsGoodsTransport.SecondWeight;
                dbi_SuttleWeight.Value = (double)cmcsGoodsTransport.SuttleWeight;
                txt_InFactoryTime.Text = cmcsGoodsTransport.InFactoryTime.Year == 1 ? "" : cmcsGoodsTransport.InFactoryTime.ToString();
                txt_OutFactoryTime.Text = cmcsGoodsTransport.OutFactoryTime.Year == 1 ? "" : cmcsGoodsTransport.OutFactoryTime.ToString();
                txt_FirstTime.Text = cmcsGoodsTransport.FirstTime.Year == 1 ? "" : cmcsGoodsTransport.FirstTime.ToString();
                txt_SecondTime.Text = cmcsGoodsTransport.SecondTime.Year == 1 ? "" : cmcsGoodsTransport.SecondTime.ToString();
                txt_Remark.Text = cmcsGoodsTransport.Remark;
                chb_IsFinish.Checked = (cmcsGoodsTransport.IsFinish == 1);
                chb_IsUse.Checked = (cmcsGoodsTransport.IsUse == 1);
            }
            if (!edit)
            {
                btnSubmit.Enabled = false;
                CMCS.CarTransport.Queue.Utilities.Helper.ControlReadOnly(panelEx2);
            }
        }


        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (txt_SerialNumber.Text.Length == 0)
            {
                MessageBoxEx.Show("�ñ공�ƺŲ���Ϊ�գ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            if ((cmcsGoodsTransport == null || cmcsGoodsTransport.CarNumber != txt_SerialNumber.Text) && Dbers.GetInstance().SelfDber.Entities<CmcsGoodsTransport>(" where CarNumber=:CarNumber", new { CarNumber = txt_SerialNumber.Text }).Count > 0)
            {
                MessageBoxEx.Show("�ñ공�ƺŲ����ظ���", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (cmcsGoodsTransport != null)
            {
                if (!CompareClass.CompareClassValue(this.cmcsGoodsTransport, Dbers.GetInstance().SelfDber.Get<CmcsGoodsTransport>(this.id)))
                {
                    MessageBoxEx.Show("�����Ѹ��������´�ҳ���޸ı��棡", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (cmcsGoodsTransport.CarNumber != txt_CarNumber.Text)
                {
                    CmcsAutotruck autotruck = Dbers.GetInstance().SelfDber.Entity<CmcsAutotruck>(" where CarNumber=:CarNumber", new { CarNumber = this.txt_CarNumber.Text });
                    if (autotruck != null)
                    {
                        cmcsGoodsTransport.AutotruckId = autotruck.Id;
                        CmcsUnFinishTransport unfinish = Dbers.GetInstance().SelfDber.Entity<CmcsUnFinishTransport>(" where TransportId=:TransportId", new { TransportId = cmcsGoodsTransport.Id });
                        if (unfinish != null)
                        {
                            unfinish.AutotruckId = autotruck.Id;
                            Dbers.GetInstance().SelfDber.Update(unfinish);
                        }
                    }
                }
                cmcsGoodsTransport.CarNumber = txt_CarNumber.Text;
                cmcsGoodsTransport.FirstWeight = (decimal)dbi_FirstWeight.Value;
                cmcsGoodsTransport.SecondWeight = (decimal)dbi_SecondWeight.Value;
                if (dbi_FirstWeight.Value != 0 && (decimal)dbi_SecondWeight.Value != 0)
                {
                    cmcsGoodsTransport.SuttleWeight = Math.Abs((decimal)dbi_FirstWeight.Value - (decimal)dbi_SecondWeight.Value);
                }
                if (supplyUnit != null)
                {
                    cmcsGoodsTransport.ReceiveUnitId = receiveUnit.Id;
                }
                if (receiveUnit != null)
                {
                    cmcsGoodsTransport.SupplyUnitId = supplyUnit.Id;
                }
                if (cmcsGoodsType != null)
                {
                    cmcsGoodsTransport.GoodsTypeId = cmcsGoodsType.Id;
                }
                txt_Remark.Text = cmcsGoodsTransport.Remark;
                cmcsGoodsTransport.IsFinish = (chb_IsFinish.Checked ? 1 : 0);
                cmcsGoodsTransport.IsUse = (chb_IsUse.Checked ? 1 : 0);
                cmcsGoodsTransport.IsSynch = "0";
                Dbers.GetInstance().SelfDber.Update(cmcsGoodsTransport);
            }
            else
            {
                cmcsGoodsTransport = new CmcsGoodsTransport();
                cmcsGoodsTransport.FirstWeight = (decimal)dbi_FirstWeight.Value;
                cmcsGoodsTransport.SecondWeight = (decimal)dbi_SecondWeight.Value;
                if (dbi_FirstWeight.Value != 0 && (decimal)dbi_SecondWeight.Value != 0)
                {
                    cmcsGoodsTransport.SuttleWeight = Math.Abs((decimal)dbi_FirstWeight.Value - (decimal)dbi_SecondWeight.Value);
                }
                if (supplyUnit != null)
                {
                    cmcsGoodsTransport.ReceiveUnitId = receiveUnit.Id;
                }
                if (receiveUnit != null)
                {
                    cmcsGoodsTransport.SupplyUnitId = supplyUnit.Id;
                }
                if (cmcsGoodsType != null)
                {
                    cmcsGoodsTransport.GoodsTypeId = cmcsGoodsType.Id;
                }
                txt_Remark.Text = cmcsGoodsTransport.Remark;
                cmcsGoodsTransport.IsFinish = (chb_IsFinish.Checked ? 1 : 0);
                cmcsGoodsTransport.IsUse = (chb_IsUse.Checked ? 1 : 0);
                Dbers.GetInstance().SelfDber.Insert(cmcsGoodsTransport);
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


        }




        private void superGridControl1_BeginEdit(object sender, DevComponents.DotNetBar.SuperGrid.GridEditEventArgs e)
        {
            // ȡ���༭
            e.Cancel = true;
        }



        private void btnReceiveUnit_Click(object sender, EventArgs e)
        {

            FrmSupplyReceive_Select Frm = new FrmSupplyReceive_Select();
            Frm.ShowDialog();
            if (Frm.DialogResult == DialogResult.OK)
            {
                receiveUnit = Frm.Output;
            }
        }

        private void btnSupplyUnit_Click(object sender, EventArgs e)
        {

            FrmSupplyReceive_Select Frm = new FrmSupplyReceive_Select();
            Frm.ShowDialog();
            if (Frm.DialogResult == DialogResult.OK)
            {
                supplyUnit = Frm.Output;
            }
        }

        private void cmb_GoodsTypeName_SelectedIndexChanged(object sender, EventArgs e)
        {
            this.cmcsGoodsType = cmb_GoodsTypeName.SelectedItem as CmcsGoodsType;
        }

        private void btnCarNumber_Click(object sender, EventArgs e)
        {
            FrmAutotruck_Select frm = new FrmAutotruck_Select(" and IsUse=1 order by CarNumber asc");
            if (frm.ShowDialog() == DialogResult.OK)
            {
                this.txt_CarNumber.Text = frm.Output.CarNumber;
            }
        }

    }
}