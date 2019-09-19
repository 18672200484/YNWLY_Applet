using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;

namespace CMCS.CarTransport.Order.Frms
{
    public partial class FrmNum : DevComponents.DotNetBar.Metro.MetroForm
    {
        public FrmNum()
        {
            InitializeComponent();
        }
        private decimal outWeight;

        public decimal OutWeight
        {
            get { return outWeight; }
            set { outWeight = value; }
        }
        private void btnSaveTransport_SaleFuel0_Click(object sender, EventArgs e)
        {
            decimal weight;
            if (decimal.TryParse(txtWeight.Text,out weight))
            {
                this.OutWeight = weight;
                this.DialogResult = DialogResult.OK;
            }
            else
            {
                MessageBoxEx.Show("煤量不正确", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }
    }
}
