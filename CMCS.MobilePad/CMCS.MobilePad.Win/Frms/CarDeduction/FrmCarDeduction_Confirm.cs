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

namespace CMCS.MobilePad.Win.Frms
{
    public partial class FrmCarDeduction_Confirm : DevComponents.DotNetBar.Metro.MetroForm
    {
        string autotruckId;
        string transportId;
        string carType;

        CommonDAO commonDAO = CommonDAO.GetInstance();

        public FrmCarDeduction_Confirm(string transportId)
        {
            InitializeComponent();

            this.transportId = transportId;
            CmcsBuyFuelTransport transport = commonDAO.SelfDber.Get<CmcsBuyFuelTransport>(this.transportId);
            if (transport != null)
            {
                txtCarNum.Text = transport.CarNumber;
                txtKGWeight.Text = transport.KgWeight.ToString("f2");
                txtKSWeight.Text = transport.KsWeight.ToString("f2");
            }
        } 

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            CmcsBuyFuelTransport transport = commonDAO.SelfDber.Get<CmcsBuyFuelTransport>(this.transportId);
            if (transport != null)
            {
                transport.KgWeight = Convert.ToDecimal(txtKGWeight.Text);
                transport.KsWeight = Convert.ToDecimal(txtKSWeight.Text);
                commonDAO.SelfDber.Update<CmcsBuyFuelTransport>(transport);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }
    }
}