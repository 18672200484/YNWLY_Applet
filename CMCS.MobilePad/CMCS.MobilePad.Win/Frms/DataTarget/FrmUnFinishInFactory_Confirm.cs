using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using CMCS.Common.DAO;
using CMCS.Common.Entities.CarTransport;
using CMCS.Common.Enums;
using CMCS.Common.Entities.Sys;
using CMCS.Common.Entities.Fuel;

namespace CMCS.MobilePad.Win.Frms.DataTarget
{
    public partial class FrmUnFinishInFactory_Confirm : DevComponents.DotNetBar.Metro.MetroForm
    {
        CmcsLMYBDetail LMYB;

        CommonDAO commonDAO = CommonDAO.GetInstance();

        public FrmUnFinishInFactory_Confirm(CmcsLMYBDetail lmyb)
        {
            InitializeComponent();

            LoadResult();
            LMYB = lmyb;
            if (LMYB != null)
            {
                txtCarNum.Text = LMYB.CarNumber;
                ddlIsUse.Text = LMYB.IsFinish;
                txtReMark.Text = LMYB.Remark;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            LMYB.Remark = txtReMark.Text;
            LMYB.IsFinish = ddlIsUse.Text;
            commonDAO.SelfDber.Update<CmcsLMYBDetail>(LMYB);
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void LoadResult()
        {
            ddlIsUse.Items.Add("未完成");
            ddlIsUse.Items.Add("已完成");
            ddlIsUse.SelectedIndex = 0;
        }
    }
}