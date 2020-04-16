namespace CMCS.CarTransport.Queue.Frms.Transport.Print
{
    partial class FrmPrintRemark
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
			this.printDocument1 = new System.Drawing.Printing.PrintDocument();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
			this.btnCancel = new DevComponents.DotNetBar.ButtonX();
			this.btnSubmit = new DevComponents.DotNetBar.ButtonX();
			this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
			this.panel1 = new System.Windows.Forms.Panel();
			this.txt_PrintTime = new DevComponents.DotNetBar.Controls.TextBoxX();
			this.labelX14 = new DevComponents.DotNetBar.LabelX();
			this.txt_PrintCount = new DevComponents.DotNetBar.Controls.TextBoxX();
			this.labelX12 = new DevComponents.DotNetBar.LabelX();
			this.txt_Remark = new DevComponents.DotNetBar.Controls.TextBoxX();
			this.labelX8 = new DevComponents.DotNetBar.LabelX();
			this.printDocument2 = new System.Drawing.Printing.PrintDocument();
			this.tableLayoutPanel1.SuspendLayout();
			this.panelEx1.SuspendLayout();
			this.panelEx2.SuspendLayout();
			this.panel1.SuspendLayout();
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
			this.tableLayoutPanel1.Size = new System.Drawing.Size(566, 175);
			this.tableLayoutPanel1.TabIndex = 156;
			// 
			// panelEx1
			// 
			this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
			this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.panelEx1.Controls.Add(this.btnCancel);
			this.panelEx1.Controls.Add(this.btnSubmit);
			this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelEx1.Location = new System.Drawing.Point(3, 138);
			this.panelEx1.Name = "panelEx1";
			this.panelEx1.Size = new System.Drawing.Size(560, 34);
			this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelEx1.Style.GradientAngle = 90;
			this.panelEx1.TabIndex = 0;
			// 
			// btnCancel
			// 
			this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
			this.btnCancel.Location = new System.Drawing.Point(476, 6);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Size = new System.Drawing.Size(75, 23);
			this.btnCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.btnCancel.TabIndex = 30;
			this.btnCancel.Text = "取  消";
			this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
			// 
			// btnSubmit
			// 
			this.btnSubmit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnSubmit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSubmit.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
			this.btnSubmit.Location = new System.Drawing.Point(372, 6);
			this.btnSubmit.Name = "btnSubmit";
			this.btnSubmit.Size = new System.Drawing.Size(75, 23);
			this.btnSubmit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.btnSubmit.TabIndex = 29;
			this.btnSubmit.Text = "打  印";
			this.btnSubmit.Click += new System.EventHandler(this.tsmiPrint_Click);
			// 
			// panelEx2
			// 
			this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
			this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.panelEx2.Controls.Add(this.panel1);
			this.panelEx2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelEx2.Location = new System.Drawing.Point(3, 3);
			this.panelEx2.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
			this.panelEx2.Name = "panelEx2";
			this.panelEx2.Size = new System.Drawing.Size(560, 132);
			this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelEx2.Style.GradientAngle = 90;
			this.panelEx2.TabIndex = 1;
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
			this.panel1.Controls.Add(this.txt_PrintTime);
			this.panel1.Controls.Add(this.labelX14);
			this.panel1.Controls.Add(this.txt_PrintCount);
			this.panel1.Controls.Add(this.labelX12);
			this.panel1.Controls.Add(this.txt_Remark);
			this.panel1.Controls.Add(this.labelX8);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.ForeColor = System.Drawing.Color.White;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(560, 132);
			this.panel1.TabIndex = 156;
			// 
			// txt_PrintTime
			// 
			this.txt_PrintTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
			// 
			// 
			// 
			this.txt_PrintTime.Border.BorderColor = System.Drawing.Color.White;
			this.txt_PrintTime.Border.Class = "TextBoxBorder";
			this.txt_PrintTime.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.txt_PrintTime.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txt_PrintTime.ForeColor = System.Drawing.Color.White;
			this.txt_PrintTime.Location = new System.Drawing.Point(325, 14);
			this.txt_PrintTime.Name = "txt_PrintTime";
			this.txt_PrintTime.ReadOnly = true;
			this.txt_PrintTime.Size = new System.Drawing.Size(180, 27);
			this.txt_PrintTime.TabIndex = 247;
			// 
			// labelX14
			// 
			this.labelX14.AutoSize = true;
			this.labelX14.BackColor = System.Drawing.Color.Transparent;
			// 
			// 
			// 
			this.labelX14.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX14.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelX14.ForeColor = System.Drawing.Color.White;
			this.labelX14.Location = new System.Drawing.Point(249, 16);
			this.labelX14.Name = "labelX14";
			this.labelX14.Size = new System.Drawing.Size(72, 24);
			this.labelX14.TabIndex = 246;
			this.labelX14.Text = "打印时间";
			// 
			// txt_PrintCount
			// 
			this.txt_PrintCount.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
			// 
			// 
			// 
			this.txt_PrintCount.Border.BorderColor = System.Drawing.Color.White;
			this.txt_PrintCount.Border.Class = "TextBoxBorder";
			this.txt_PrintCount.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.txt_PrintCount.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txt_PrintCount.ForeColor = System.Drawing.Color.Transparent;
			this.txt_PrintCount.Location = new System.Drawing.Point(85, 14);
			this.txt_PrintCount.Name = "txt_PrintCount";
			this.txt_PrintCount.ReadOnly = true;
			this.txt_PrintCount.Size = new System.Drawing.Size(85, 27);
			this.txt_PrintCount.TabIndex = 245;
			// 
			// labelX12
			// 
			this.labelX12.AutoSize = true;
			this.labelX12.BackColor = System.Drawing.Color.Transparent;
			// 
			// 
			// 
			this.labelX12.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX12.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelX12.ForeColor = System.Drawing.Color.White;
			this.labelX12.Location = new System.Drawing.Point(9, 15);
			this.labelX12.Name = "labelX12";
			this.labelX12.Size = new System.Drawing.Size(72, 24);
			this.labelX12.TabIndex = 244;
			this.labelX12.Text = "打印次数";
			// 
			// txt_Remark
			// 
			this.txt_Remark.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
			// 
			// 
			// 
			this.txt_Remark.Border.BorderColor = System.Drawing.Color.Fuchsia;
			this.txt_Remark.Border.Class = "TextBoxBorder";
			this.txt_Remark.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.txt_Remark.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txt_Remark.ForeColor = System.Drawing.Color.White;
			this.txt_Remark.Location = new System.Drawing.Point(85, 53);
			this.txt_Remark.Multiline = true;
			this.txt_Remark.Name = "txt_Remark";
			this.txt_Remark.Size = new System.Drawing.Size(420, 67);
			this.txt_Remark.TabIndex = 15;
			// 
			// labelX8
			// 
			this.labelX8.AutoSize = true;
			this.labelX8.BackColor = System.Drawing.Color.Transparent;
			// 
			// 
			// 
			this.labelX8.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX8.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.labelX8.ForeColor = System.Drawing.Color.White;
			this.labelX8.Location = new System.Drawing.Point(40, 55);
			this.labelX8.Name = "labelX8";
			this.labelX8.Size = new System.Drawing.Size(40, 24);
			this.labelX8.TabIndex = 14;
			this.labelX8.Text = "备注";
			// 
			// FrmPrintRemark
			// 
			this.AcceptButton = this.btnSubmit;
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.CancelButton = this.btnCancel;
			this.ClientSize = new System.Drawing.Size(566, 175);
			this.Controls.Add(this.tableLayoutPanel1);
			this.DoubleBuffered = true;
			this.MaximizeBox = false;
			this.MinimizeBox = false;
			this.Name = "FrmPrintRemark";
			this.ShowIcon = false;
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
			this.Text = "打印确认";
			this.Load += new System.EventHandler(this.FrmPrintWeb_Load);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.panelEx1.ResumeLayout(false);
			this.panelEx2.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private System.Windows.Forms.Panel panel1;
        private DevComponents.DotNetBar.ButtonX btnSubmit;
        private DevComponents.DotNetBar.ButtonX btnCancel;
		private DevComponents.DotNetBar.Controls.TextBoxX txt_Remark;
		private DevComponents.DotNetBar.LabelX labelX8;
		private DevComponents.DotNetBar.Controls.TextBoxX txt_PrintTime;
		private DevComponents.DotNetBar.LabelX labelX14;
		private DevComponents.DotNetBar.Controls.TextBoxX txt_PrintCount;
		private DevComponents.DotNetBar.LabelX labelX12;
		private System.Drawing.Printing.PrintDocument printDocument2;
	}
}