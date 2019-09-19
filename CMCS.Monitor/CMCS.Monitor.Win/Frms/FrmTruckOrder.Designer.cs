namespace CMCS.Monitor.Win.Frms
{
    partial class FrmTruckOrder
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
            this.components = new System.ComponentModel.Container();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.panWebBrower = new DevComponents.DotNetBar.PanelEx();
            this.gboxTest = new System.Windows.Forms.GroupBox();
            this.btnRefresh = new DevComponents.DotNetBar.ButtonX();
            this.btnRequestData = new DevComponents.DotNetBar.ButtonX();
            this.panWebBrower.SuspendLayout();
            this.gboxTest.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 3000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // panWebBrower
            // 
            this.panWebBrower.CanvasColor = System.Drawing.SystemColors.Control;
            this.panWebBrower.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panWebBrower.Controls.Add(this.gboxTest);
            this.panWebBrower.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panWebBrower.Location = new System.Drawing.Point(0, 0);
            this.panWebBrower.Margin = new System.Windows.Forms.Padding(0);
            this.panWebBrower.Name = "panWebBrower";
            this.panWebBrower.Size = new System.Drawing.Size(1436, 725);
            this.panWebBrower.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panWebBrower.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panWebBrower.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panWebBrower.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panWebBrower.Style.GradientAngle = 90;
            this.panWebBrower.TabIndex = 2;
            // 
            // gboxTest
            // 
            this.gboxTest.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.gboxTest.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
            this.gboxTest.Controls.Add(this.btnRefresh);
            this.gboxTest.Controls.Add(this.btnRequestData);
            this.gboxTest.ForeColor = System.Drawing.Color.White;
            this.gboxTest.Location = new System.Drawing.Point(1322, 0);
            this.gboxTest.Name = "gboxTest";
            this.gboxTest.Size = new System.Drawing.Size(111, 83);
            this.gboxTest.TabIndex = 8;
            this.gboxTest.TabStop = false;
            this.gboxTest.Text = " �� �� ";
            // 
            // btnRefresh
            // 
            this.btnRefresh.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnRefresh.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnRefresh.Location = new System.Drawing.Point(6, 23);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(98, 23);
            this.btnRefresh.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnRefresh.TabIndex = 4;
            this.btnRefresh.Text = "ˢ��ҳ��";
            // 
            // btnRequestData
            // 
            this.btnRequestData.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnRequestData.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnRequestData.Location = new System.Drawing.Point(6, 51);
            this.btnRequestData.Name = "btnRequestData";
            this.btnRequestData.Size = new System.Drawing.Size(98, 23);
            this.btnRequestData.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnRequestData.TabIndex = 5;
            this.btnRequestData.Text = "���ݸ���";
            // 
            // FrmTruckOrder
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1436, 725);
            this.Controls.Add(this.panWebBrower);
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Name = "FrmTruckOrder";
            this.Text = "MetroForm";
            this.Load += new System.EventHandler(this.FrmTruckOrder_Load);
            this.panWebBrower.ResumeLayout(false);
            this.gboxTest.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private DevComponents.DotNetBar.PanelEx panWebBrower;
        private System.Windows.Forms.GroupBox gboxTest;
        private DevComponents.DotNetBar.ButtonX btnRefresh;
        private DevComponents.DotNetBar.ButtonX btnRequestData;
    }
}