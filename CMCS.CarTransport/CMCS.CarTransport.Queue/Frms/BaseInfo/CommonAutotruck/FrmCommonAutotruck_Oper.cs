using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using CMCS.Common;
using CMCS.Common.Entities.CarTransport;
using DevComponents.DotNetBar.Controls;
using DevComponents.Editors;
using CMCS.CarTransport.Queue.Utilities;
using CMCS.CarTransport.DAO;
using CMCS.Common.Enums;

namespace CMCS.CarTransport.Queue.Frms.BaseInfo.CommonAutotruck
{
    public partial class FrmCommonAutotruck_Oper : DevComponents.DotNetBar.Metro.MetroForm
    {
        String id = String.Empty;
        bool edit = false;
        CmcsAutotruck cmcsAutotruck;
        public FrmCommonAutotruck_Oper()
        {
            InitializeComponent();
            edit = true;
        }
        public FrmCommonAutotruck_Oper(String pId, bool pEdit)
        {
            InitializeComponent();
            id = pId;
            edit = pEdit;
        }

        private void FrmAutotruck_Oper_Load(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(id))
            {
                this.cmcsAutotruck = Dbers.GetInstance().SelfDber.Get<CmcsAutotruck>(this.id);
                txt_CarNumber.Text = cmcsAutotruck.CarNumber;
                txt_Driver.Text = cmcsAutotruck.Driver;
                txt_CellPhoneNumber.Text = cmcsAutotruck.CellPhoneNumber;
                chb_IsUse.Checked = (cmcsAutotruck.IsUse == 1);
                txt_ReMark.Text = cmcsAutotruck.ReMark;
            }
            if (!edit)
            {
                btnSubmit.Enabled = false;
                CMCS.CarTransport.Queue.Utilities.Helper.ControlReadOnly(panelEx2);
            }
        }


        private void btnSubmit_Click(object sender, EventArgs e)
        {
            txt_CarNumber.Text = txt_CarNumber.Text.ToUpper();
            if (txt_CarNumber.Text.Length == 0)
            {
                MessageBoxEx.Show("该车牌号不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if ((cmcsAutotruck == null || cmcsAutotruck.CarNumber != txt_CarNumber.Text) && Dbers.GetInstance().SelfDber.Entities<CmcsAutotruck>(" where CarNumber=:CarNumber", new { CarNumber = txt_CarNumber.Text }).Count > 0)
            {
                MessageBoxEx.Show("该车牌号不可重复！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (String.IsNullOrEmpty(id)) { id = Guid.NewGuid().ToString(); }

            if (cmcsAutotruck != null)
            {
                cmcsAutotruck.CarNumber = txt_CarNumber.Text;
                cmcsAutotruck.CarType = eCarType.来访车辆.ToString();
                cmcsAutotruck.Driver = txt_Driver.Text;
                cmcsAutotruck.CellPhoneNumber = txt_CellPhoneNumber.Text;
                cmcsAutotruck.IsUse = (chb_IsUse.Checked ? 1 : 0);
                cmcsAutotruck.IsInner = 1;
                cmcsAutotruck.ReMark = txt_ReMark.Text;
                cmcsAutotruck.IsSynch = "0";
                Dbers.GetInstance().SelfDber.Update(cmcsAutotruck);
            }
            else
            {
                cmcsAutotruck = new CmcsAutotruck();
                cmcsAutotruck.CarNumber = txt_CarNumber.Text;
                cmcsAutotruck.CarType = eCarType.来访车辆.ToString();
                cmcsAutotruck.Driver = txt_Driver.Text;
                cmcsAutotruck.CellPhoneNumber = txt_CellPhoneNumber.Text;
                cmcsAutotruck.IsUse = (chb_IsUse.Checked ? 1 : 0);
                cmcsAutotruck.IsInner = 1;
                cmcsAutotruck.ReMark = txt_ReMark.Text;
                Dbers.GetInstance().SelfDber.Insert(cmcsAutotruck);
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void txt_CarNumber_Leave(object sender, EventArgs e)
        {
            ((TextBoxX)sender).Text = ((TextBoxX)sender).Text.ToUpper();
        }

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

        private void txt_CarNumber_ButtonDropDownClick(object sender, CancelEventArgs e)
        {
            CreateProvinceAbbreviationButton();
        }
    }
}