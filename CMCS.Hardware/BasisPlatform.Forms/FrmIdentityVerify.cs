using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BasisPlatform.Forms
{
    public partial class FrmIdentityVerify : Form
    {
        public FrmIdentityVerify()
        {
            InitializeComponent();
        }

        private void txtPwd_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter && this.txtPwd.Text == DateTime.Now.ToString("yyyyMMdd"))
                DialogResult = DialogResult.Yes;
        }
    }
}
