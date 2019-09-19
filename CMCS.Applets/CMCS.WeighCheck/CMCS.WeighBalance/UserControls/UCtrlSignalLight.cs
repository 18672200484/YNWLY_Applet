using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CMCS.UnloadSampler.UserControls
{
    public partial class UCtrlSignalLight : UserControl
    {
        private Color lightColor = Color.Gray;
        /// <summary>
        /// 显示颜色
        /// </summary>
        public Color LightColor
        {
            set { lightColor = value; this.Refresh(); }
        }

        public UCtrlSignalLight()
        {
            InitializeComponent();
        }

        private void UCtrlSignalLight_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.FillEllipse(new SolidBrush(this.lightColor), this.ClientRectangle);
        }
    }
}
