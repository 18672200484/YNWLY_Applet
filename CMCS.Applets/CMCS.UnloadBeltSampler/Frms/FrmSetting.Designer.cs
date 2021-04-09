namespace CMCS.UnloadSampler.Frms
{
    partial class FrmSetting
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
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
			this.btnSubmit = new DevComponents.DotNetBar.ButtonX();
			this.btnCancel = new DevComponents.DotNetBar.ButtonX();
			this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
			this.txtSelfConnStr = new DevComponents.DotNetBar.Controls.TextBoxX();
			this.labelX20 = new DevComponents.DotNetBar.LabelX();
			this.txtSampleCode = new DevComponents.DotNetBar.Controls.TextBoxX();
			this.labelX8 = new DevComponents.DotNetBar.LabelX();
			this.txtAppIdentifier = new DevComponents.DotNetBar.Controls.TextBoxX();
			this.labelX3 = new DevComponents.DotNetBar.LabelX();
			this.txtRCMakeCode = new DevComponents.DotNetBar.Controls.TextBoxX();
			this.labelX1 = new DevComponents.DotNetBar.LabelX();
			this.txtCCMakeCode = new DevComponents.DotNetBar.Controls.TextBoxX();
			this.labelX2 = new DevComponents.DotNetBar.LabelX();
			this.tableLayoutPanel1.SuspendLayout();
			this.panelEx1.SuspendLayout();
			this.panelEx2.SuspendLayout();
			this.SuspendLayout();
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(82)))), ((int)(((byte)(89)))));
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.panelEx1, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.panelEx2, 0, 0);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.ForeColor = System.Drawing.Color.White;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 2;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(702, 396);
			this.tableLayoutPanel1.TabIndex = 0;
			// 
			// panelEx1
			// 
			this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
			this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.panelEx1.Controls.Add(this.btnSubmit);
			this.panelEx1.Controls.Add(this.btnCancel);
			this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelEx1.Location = new System.Drawing.Point(3, 359);
			this.panelEx1.Name = "panelEx1";
			this.panelEx1.Size = new System.Drawing.Size(696, 34);
			this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelEx1.Style.GradientAngle = 90;
			this.panelEx1.TabIndex = 0;
			// 
			// btnSubmit
			// 
			this.btnSubmit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnSubmit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSubmit.Font = new System.Drawing.Font("Segoe UI", 11.25F);
			this.btnSubmit.Location = new System.Drawing.Point(529, 6);
			this.btnSubmit.Name = "btnSubmit";
			this.btnSubmit.Size = new System.Drawing.Size(75, 23);
			this.btnSubmit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.btnSubmit.TabIndex = 1;
			this.btnSubmit.Text = "保  存";
			this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
			// 
			// btnCancel
			// 
			this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 11.25F);
			this.btnCancel.Location = new System.Drawing.Point(610, 6);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.btnCancel.TabIndex = 0;
			this.btnCancel.Text = "取  消";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// panelEx2
			// 
			this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
			this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.panelEx2.Controls.Add(this.txtCCMakeCode);
			this.panelEx2.Controls.Add(this.labelX2);
			this.panelEx2.Controls.Add(this.txtRCMakeCode);
			this.panelEx2.Controls.Add(this.labelX1);
			this.panelEx2.Controls.Add(this.txtSelfConnStr);
			this.panelEx2.Controls.Add(this.labelX20);
			this.panelEx2.Controls.Add(this.txtSampleCode);
			this.panelEx2.Controls.Add(this.labelX8);
			this.panelEx2.Controls.Add(this.txtAppIdentifier);
			this.panelEx2.Controls.Add(this.labelX3);
			this.panelEx2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelEx2.Location = new System.Drawing.Point(3, 3);
			this.panelEx2.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
			this.panelEx2.Name = "panelEx2";
			this.panelEx2.Size = new System.Drawing.Size(696, 353);
			this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelEx2.Style.GradientAngle = 90;
			this.panelEx2.TabIndex = 1;
			// 
			// txtSelfConnStr
			// 
			this.txtSelfConnStr.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
			// 
			// 
			// 
			this.txtSelfConnStr.Border.Class = "TextBoxBorder";
			this.txtSelfConnStr.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.txtSelfConnStr.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txtSelfConnStr.ForeColor = System.Drawing.Color.White;
			this.txtSelfConnStr.Location = new System.Drawing.Point(191, 85);
			this.txtSelfConnStr.Multiline = true;
			this.txtSelfConnStr.Name = "txtSelfConnStr";
			this.txtSelfConnStr.Size = new System.Drawing.Size(446, 55);
			this.txtSelfConnStr.TabIndex = 208;
			// 
			// labelX20
			// 
			this.labelX20.AutoSize = true;
			this.labelX20.BackColor = System.Drawing.Color.Transparent;
			// 
			// 
			// 
			this.labelX20.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX20.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelX20.ForeColor = System.Drawing.Color.White;
			this.labelX20.Location = new System.Drawing.Point(37, 85);
			this.labelX20.Name = "labelX20";
			this.labelX20.Size = new System.Drawing.Size(137, 24);
			this.labelX20.TabIndex = 207;
			this.labelX20.Text = "数据库连接字符串";
			// 
			// txtSampleCode
			// 
			this.txtSampleCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
			// 
			// 
			// 
			this.txtSampleCode.Border.Class = "TextBoxBorder";
			this.txtSampleCode.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.txtSampleCode.Font = new System.Drawing.Font("Segoe UI", 11.25F);
			this.txtSampleCode.ForeColor = System.Drawing.Color.White;
			this.txtSampleCode.Location = new System.Drawing.Point(191, 157);
			this.txtSampleCode.Name = "txtSampleCode";
			this.txtSampleCode.Size = new System.Drawing.Size(446, 27);
			this.txtSampleCode.TabIndex = 206;
			// 
			// labelX8
			// 
			this.labelX8.AutoSize = true;
			// 
			// 
			// 
			this.labelX8.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX8.Font = new System.Drawing.Font("Segoe UI", 11.25F);
			this.labelX8.ForeColor = System.Drawing.Color.White;
			this.labelX8.Location = new System.Drawing.Point(115, 157);
			this.labelX8.Name = "labelX8";
			this.labelX8.Size = new System.Drawing.Size(56, 24);
			this.labelX8.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.labelX8.TabIndex = 205;
			this.labelX8.Text = "采样机";
			// 
			// txtAppIdentifier
			// 
			this.txtAppIdentifier.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
			// 
			// 
			// 
			this.txtAppIdentifier.Border.Class = "TextBoxBorder";
			this.txtAppIdentifier.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.txtAppIdentifier.Font = new System.Drawing.Font("Segoe UI", 11.25F);
			this.txtAppIdentifier.ForeColor = System.Drawing.Color.White;
			this.txtAppIdentifier.Location = new System.Drawing.Point(191, 42);
			this.txtAppIdentifier.Name = "txtAppIdentifier";
			this.txtAppIdentifier.Size = new System.Drawing.Size(446, 27);
			this.txtAppIdentifier.TabIndex = 195;
			// 
			// labelX3
			// 
			this.labelX3.AutoSize = true;
			// 
			// 
			// 
			this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX3.Font = new System.Drawing.Font("Segoe UI", 11.25F);
			this.labelX3.ForeColor = System.Drawing.Color.White;
			this.labelX3.Location = new System.Drawing.Point(66, 42);
			this.labelX3.Name = "labelX3";
			this.labelX3.Size = new System.Drawing.Size(105, 24);
			this.labelX3.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.labelX3.TabIndex = 194;
			this.labelX3.Text = "程序唯一标识";
			// 
			// txtRCMakeCode
			// 
			this.txtRCMakeCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
			// 
			// 
			// 
			this.txtRCMakeCode.Border.Class = "TextBoxBorder";
			this.txtRCMakeCode.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.txtRCMakeCode.Font = new System.Drawing.Font("Segoe UI", 11.25F);
			this.txtRCMakeCode.ForeColor = System.Drawing.Color.White;
			this.txtRCMakeCode.Location = new System.Drawing.Point(191, 199);
			this.txtRCMakeCode.Name = "txtRCMakeCode";
			this.txtRCMakeCode.Size = new System.Drawing.Size(227, 27);
			this.txtRCMakeCode.TabIndex = 210;
			// 
			// labelX1
			// 
			this.labelX1.AutoSize = true;
			// 
			// 
			// 
			this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX1.Font = new System.Drawing.Font("Segoe UI", 11.25F);
			this.labelX1.ForeColor = System.Drawing.Color.White;
			this.labelX1.Location = new System.Drawing.Point(86, 199);
			this.labelX1.Name = "labelX1";
			this.labelX1.Size = new System.Drawing.Size(88, 24);
			this.labelX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.labelX1.TabIndex = 209;
			this.labelX1.Text = "入厂制样机";
			// 
			// txtCCMakeCode
			// 
			this.txtCCMakeCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
			// 
			// 
			// 
			this.txtCCMakeCode.Border.Class = "TextBoxBorder";
			this.txtCCMakeCode.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.txtCCMakeCode.Font = new System.Drawing.Font("Segoe UI", 11.25F);
			this.txtCCMakeCode.ForeColor = System.Drawing.Color.White;
			this.txtCCMakeCode.Location = new System.Drawing.Point(191, 241);
			this.txtCCMakeCode.Name = "txtCCMakeCode";
			this.txtCCMakeCode.Size = new System.Drawing.Size(227, 27);
			this.txtCCMakeCode.TabIndex = 212;
			// 
			// labelX2
			// 
			this.labelX2.AutoSize = true;
			// 
			// 
			// 
			this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX2.Font = new System.Drawing.Font("Segoe UI", 11.25F);
			this.labelX2.ForeColor = System.Drawing.Color.White;
			this.labelX2.Location = new System.Drawing.Point(86, 241);
			this.labelX2.Name = "labelX2";
			this.labelX2.Size = new System.Drawing.Size(88, 24);
			this.labelX2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.labelX2.TabIndex = 211;
			this.labelX2.Text = "出厂制样机";
			// 
			// FrmSetting
			// 
			this.AcceptButton = this.btnSubmit;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(702, 396);
			this.Controls.Add(this.tableLayoutPanel1);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.MaximizeBox = false;
			this.Name = "FrmSetting";
			this.ShowIcon = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "参数设置";
			this.Load += new System.EventHandler(this.FrmSetting_Load);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.panelEx1.ResumeLayout(false);
			this.panelEx2.ResumeLayout(false);
			this.panelEx2.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.ButtonX btnCancel;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private DevComponents.DotNetBar.ButtonX btnSubmit;
        private DevComponents.DotNetBar.Controls.TextBoxX txtAppIdentifier;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.TextBoxX txtSampleCode;
        private DevComponents.DotNetBar.LabelX labelX8;
        private DevComponents.DotNetBar.Controls.TextBoxX txtSelfConnStr;
        private DevComponents.DotNetBar.LabelX labelX20;
		private DevComponents.DotNetBar.Controls.TextBoxX txtCCMakeCode;
		private DevComponents.DotNetBar.LabelX labelX2;
		private DevComponents.DotNetBar.Controls.TextBoxX txtRCMakeCode;
		private DevComponents.DotNetBar.LabelX labelX1;
	}
}