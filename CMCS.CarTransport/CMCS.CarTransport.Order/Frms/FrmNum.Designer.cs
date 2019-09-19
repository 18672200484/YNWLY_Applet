namespace CMCS.CarTransport.Order.Frms
{
    partial class FrmNum
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtWeight = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX14 = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.btnSaveTransport_SaleFuel0 = new DevComponents.DotNetBar.ButtonX();
            this.SuspendLayout();
            // 
            // txtWeight
            // 
            this.txtWeight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
            // 
            // 
            // 
            this.txtWeight.Border.Class = "TextBoxBorder";
            this.txtWeight.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtWeight.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.txtWeight.ForeColor = System.Drawing.Color.White;
            this.txtWeight.Location = new System.Drawing.Point(77, 28);
            this.txtWeight.Name = "txtWeight";
            this.txtWeight.Size = new System.Drawing.Size(155, 29);
            this.txtWeight.TabIndex = 65;
            this.txtWeight.TabStop = false;
            // 
            // labelX14
            // 
            this.labelX14.AutoSize = true;
            // 
            // 
            // 
            this.labelX14.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX14.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.labelX14.ForeColor = System.Drawing.Color.White;
            this.labelX14.Location = new System.Drawing.Point(30, 31);
            this.labelX14.Name = "labelX14";
            this.labelX14.Size = new System.Drawing.Size(35, 22);
            this.labelX14.TabIndex = 64;
            this.labelX14.Text = "取煤";
            // 
            // labelX1
            // 
            this.labelX1.AutoSize = true;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Font = new System.Drawing.Font("Segoe UI", 10F);
            this.labelX1.ForeColor = System.Drawing.Color.White;
            this.labelX1.Location = new System.Drawing.Point(240, 32);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(21, 22);
            this.labelX1.TabIndex = 66;
            this.labelX1.Text = "吨";
            // 
            // btnSaveTransport_SaleFuel0
            // 
            this.btnSaveTransport_SaleFuel0.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSaveTransport_SaleFuel0.Font = new System.Drawing.Font("Segoe UI", 14.5F);
            this.btnSaveTransport_SaleFuel0.Location = new System.Drawing.Point(77, 82);
            this.btnSaveTransport_SaleFuel0.Name = "btnSaveTransport_SaleFuel0";
            this.btnSaveTransport_SaleFuel0.Size = new System.Drawing.Size(155, 36);
            this.btnSaveTransport_SaleFuel0.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSaveTransport_SaleFuel0.TabIndex = 63;
            this.btnSaveTransport_SaleFuel0.Text = "结束取煤";
            this.btnSaveTransport_SaleFuel0.Click += new System.EventHandler(this.btnSaveTransport_SaleFuel0_Click);
            // 
            // FrmNum
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(295, 145);
            this.Controls.Add(this.labelX1);
            this.Controls.Add(this.txtWeight);
            this.Controls.Add(this.labelX14);
            this.Controls.Add(this.btnSaveTransport_SaleFuel0);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "FrmNum";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private DevComponents.DotNetBar.Controls.TextBoxX txtWeight;
        private DevComponents.DotNetBar.LabelX labelX14;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.ButtonX btnSaveTransport_SaleFuel0;
    }
}