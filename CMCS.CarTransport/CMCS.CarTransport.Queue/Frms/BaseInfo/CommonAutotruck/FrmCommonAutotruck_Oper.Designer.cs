namespace CMCS.CarTransport.Queue.Frms.BaseInfo.CommonAutotruck
{
    partial class FrmCommonAutotruck_Oper
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
            this.txt_CarNumber = new DevComponents.DotNetBar.Controls.TextBoxDropDown();
            this.flpanProvinceAbbreviation = new System.Windows.Forms.FlowLayoutPanel();
            this.txt_ReMark = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.chb_IsUse = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.labelX8 = new DevComponents.DotNetBar.LabelX();
            this.txt_CellPhoneNumber = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.txt_Driver = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(625, 257);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.btnSubmit);
            this.panelEx1.Controls.Add(this.btnCancel);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(3, 220);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(619, 34);
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
            this.btnSubmit.Location = new System.Drawing.Point(454, 5);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSubmit.TabIndex = 3;
            this.btnSubmit.Text = "保  存";
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.btnCancel.Location = new System.Drawing.Point(535, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnCancel.TabIndex = 2;
            this.btnCancel.Text = "取  消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.txt_CarNumber);
            this.panelEx2.Controls.Add(this.flpanProvinceAbbreviation);
            this.panelEx2.Controls.Add(this.txt_ReMark);
            this.panelEx2.Controls.Add(this.chb_IsUse);
            this.panelEx2.Controls.Add(this.labelX8);
            this.panelEx2.Controls.Add(this.txt_CellPhoneNumber);
            this.panelEx2.Controls.Add(this.labelX5);
            this.panelEx2.Controls.Add(this.txt_Driver);
            this.panelEx2.Controls.Add(this.labelX6);
            this.panelEx2.Controls.Add(this.labelX1);
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx2.Location = new System.Drawing.Point(3, 3);
            this.panelEx2.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(619, 214);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 1;
            // 
            // txt_CarNumber
            // 
            this.txt_CarNumber.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
            // 
            // 
            // 
            this.txt_CarNumber.BackgroundStyle.Class = "TextBoxBorder";
            this.txt_CarNumber.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txt_CarNumber.ButtonDropDown.Visible = true;
            this.txt_CarNumber.DropDownControl = this.flpanProvinceAbbreviation;
            this.txt_CarNumber.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.txt_CarNumber.Location = new System.Drawing.Point(103, 25);
            this.txt_CarNumber.Name = "txt_CarNumber";
            this.txt_CarNumber.Size = new System.Drawing.Size(180, 27);
            this.txt_CarNumber.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.txt_CarNumber.TabIndex = 228;
            this.txt_CarNumber.Text = "";
            this.txt_CarNumber.ButtonDropDownClick += new System.ComponentModel.CancelEventHandler(this.txt_CarNumber_ButtonDropDownClick);
            // 
            // flpanProvinceAbbreviation
            // 
            this.flpanProvinceAbbreviation.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.flpanProvinceAbbreviation.AutoScroll = true;
            this.flpanProvinceAbbreviation.BackColor = System.Drawing.Color.White;
            this.flpanProvinceAbbreviation.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flpanProvinceAbbreviation.Location = new System.Drawing.Point(103, 32);
            this.flpanProvinceAbbreviation.Margin = new System.Windows.Forms.Padding(0);
            this.flpanProvinceAbbreviation.MinimumSize = new System.Drawing.Size(174, 2);
            this.flpanProvinceAbbreviation.Name = "flpanProvinceAbbreviation";
            this.flpanProvinceAbbreviation.Padding = new System.Windows.Forms.Padding(3);
            this.flpanProvinceAbbreviation.Size = new System.Drawing.Size(174, 135);
            this.flpanProvinceAbbreviation.TabIndex = 227;
            // 
            // txt_ReMark
            // 
            this.txt_ReMark.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
            // 
            // 
            // 
            this.txt_ReMark.Border.Class = "TextBoxBorder";
            this.txt_ReMark.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txt_ReMark.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_ReMark.ForeColor = System.Drawing.Color.White;
            this.txt_ReMark.Location = new System.Drawing.Point(103, 91);
            this.txt_ReMark.MaxLength = 128;
            this.txt_ReMark.Multiline = true;
            this.txt_ReMark.Name = "txt_ReMark";
            this.txt_ReMark.Size = new System.Drawing.Size(489, 106);
            this.txt_ReMark.TabIndex = 13;
            // 
            // chb_IsUse
            // 
            this.chb_IsUse.AutoSize = true;
            // 
            // 
            // 
            this.chb_IsUse.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chb_IsUse.Checked = true;
            this.chb_IsUse.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chb_IsUse.CheckValue = "Y";
            this.chb_IsUse.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chb_IsUse.ForeColor = System.Drawing.Color.White;
            this.chb_IsUse.Location = new System.Drawing.Point(412, 61);
            this.chb_IsUse.Name = "chb_IsUse";
            this.chb_IsUse.Size = new System.Drawing.Size(60, 24);
            this.chb_IsUse.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chb_IsUse.TabIndex = 38;
            this.chb_IsUse.Text = "启用";
            // 
            // labelX8
            // 
            this.labelX8.AutoSize = true;
            // 
            // 
            // 
            this.labelX8.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX8.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX8.ForeColor = System.Drawing.Color.White;
            this.labelX8.Location = new System.Drawing.Point(58, 93);
            this.labelX8.Name = "labelX8";
            this.labelX8.Size = new System.Drawing.Size(40, 24);
            this.labelX8.TabIndex = 12;
            this.labelX8.Text = "备注";
            // 
            // txt_CellPhoneNumber
            // 
            this.txt_CellPhoneNumber.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
            // 
            // 
            // 
            this.txt_CellPhoneNumber.Border.Class = "TextBoxBorder";
            this.txt_CellPhoneNumber.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txt_CellPhoneNumber.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_CellPhoneNumber.ForeColor = System.Drawing.Color.White;
            this.txt_CellPhoneNumber.Location = new System.Drawing.Point(103, 58);
            this.txt_CellPhoneNumber.Name = "txt_CellPhoneNumber";
            this.txt_CellPhoneNumber.Size = new System.Drawing.Size(180, 27);
            this.txt_CellPhoneNumber.TabIndex = 11;
            // 
            // labelX5
            // 
            this.labelX5.AutoSize = true;
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX5.ForeColor = System.Drawing.Color.White;
            this.labelX5.Location = new System.Drawing.Point(58, 60);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(40, 24);
            this.labelX5.TabIndex = 10;
            this.labelX5.Text = "电话";
            // 
            // txt_Driver
            // 
            this.txt_Driver.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
            // 
            // 
            // 
            this.txt_Driver.Border.Class = "TextBoxBorder";
            this.txt_Driver.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txt_Driver.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_Driver.ForeColor = System.Drawing.Color.White;
            this.txt_Driver.Location = new System.Drawing.Point(412, 24);
            this.txt_Driver.Name = "txt_Driver";
            this.txt_Driver.Size = new System.Drawing.Size(180, 27);
            this.txt_Driver.TabIndex = 9;
            // 
            // labelX6
            // 
            this.labelX6.AutoSize = true;
            // 
            // 
            // 
            this.labelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX6.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX6.ForeColor = System.Drawing.Color.White;
            this.labelX6.Location = new System.Drawing.Point(367, 26);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(40, 24);
            this.labelX6.TabIndex = 8;
            this.labelX6.Text = "司机";
            // 
            // labelX1
            // 
            this.labelX1.AutoSize = true;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX1.ForeColor = System.Drawing.Color.White;
            this.labelX1.Location = new System.Drawing.Point(43, 28);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(56, 24);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "车牌号";
            // 
            // FrmCommonAutotruck_Oper
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(625, 257);
            this.Controls.Add(this.tableLayoutPanel1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximizeBox = false;
            this.Name = "FrmCommonAutotruck_Oper";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "车辆管理";
            this.Load += new System.EventHandler(this.FrmAutotruck_Oper_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panelEx1.ResumeLayout(false);
            this.panelEx2.ResumeLayout(false);
            this.panelEx2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.TextBoxX txt_ReMark;
        private DevComponents.DotNetBar.LabelX labelX8;
        private DevComponents.DotNetBar.Controls.TextBoxX txt_CellPhoneNumber;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.Controls.TextBoxX txt_Driver;
        private DevComponents.DotNetBar.LabelX labelX6;
        private DevComponents.DotNetBar.Controls.CheckBoxX chb_IsUse;
        private DevComponents.DotNetBar.ButtonX btnSubmit;
        private DevComponents.DotNetBar.ButtonX btnCancel;
        private System.Windows.Forms.FlowLayoutPanel flpanProvinceAbbreviation;
        private DevComponents.DotNetBar.Controls.TextBoxDropDown txt_CarNumber;
    }
}