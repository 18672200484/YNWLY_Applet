namespace CMCS.MobilePad.Win.Frms.CarBreakRules
{
    partial class FrmCarBreakRules_Confirm
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.btnSave = new DevComponents.DotNetBar.ButtonX();
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.txtKGWeight = new DevComponents.Editors.DoubleInput();
            this.txtKSWeight = new DevComponents.Editors.DoubleInput();
            this.lbl_KsWeight = new DevComponents.DotNetBar.LabelX();
            this.lbl_KgWeight = new DevComponents.DotNetBar.LabelX();
            this.pictureBox3 = new System.Windows.Forms.PictureBox();
            this.pictureBox2 = new System.Windows.Forms.PictureBox();
            this.btn_Select = new DevComponents.DotNetBar.ButtonX();
            this.btn_Shoot = new DevComponents.DotNetBar.ButtonX();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.cmbBreakRulesResult = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cmbBreakRules = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX6 = new DevComponents.DotNetBar.LabelX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.txtCarNum = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.imageList1 = new System.Windows.Forms.ImageList(this.components);
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.tableLayoutPanel1.SuspendLayout();
            this.panelEx1.SuspendLayout();
            this.panelEx2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtKGWeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtKSWeight)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
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
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(6);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 77F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1027, 588);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.btnSave);
            this.panelEx1.Controls.Add(this.btnCancel);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(6, 517);
            this.panelEx1.Margin = new System.Windows.Forms.Padding(6);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(1015, 65);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 0;
            // 
            // btnSave
            // 
            this.btnSave.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSave.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 14.25F);
            this.btnSave.Location = new System.Drawing.Point(722, 17);
            this.btnSave.Margin = new System.Windows.Forms.Padding(6);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(135, 35);
            this.btnSave.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "±£¥Ê";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 14.25F);
            this.btnCancel.Location = new System.Drawing.Point(881, 17);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(125, 35);
            this.btnCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "»°  œ˚";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.txtKGWeight);
            this.panelEx2.Controls.Add(this.txtKSWeight);
            this.panelEx2.Controls.Add(this.lbl_KsWeight);
            this.panelEx2.Controls.Add(this.lbl_KgWeight);
            this.panelEx2.Controls.Add(this.pictureBox3);
            this.panelEx2.Controls.Add(this.pictureBox2);
            this.panelEx2.Controls.Add(this.btn_Select);
            this.panelEx2.Controls.Add(this.btn_Shoot);
            this.panelEx2.Controls.Add(this.pictureBox1);
            this.panelEx2.Controls.Add(this.cmbBreakRulesResult);
            this.panelEx2.Controls.Add(this.cmbBreakRules);
            this.panelEx2.Controls.Add(this.labelX2);
            this.panelEx2.Controls.Add(this.labelX6);
            this.panelEx2.Controls.Add(this.labelX4);
            this.panelEx2.Controls.Add(this.txtCarNum);
            this.panelEx2.Controls.Add(this.labelX1);
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx2.Location = new System.Drawing.Point(6, 6);
            this.panelEx2.Margin = new System.Windows.Forms.Padding(6, 6, 6, 0);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(1015, 505);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 1;
            // 
            // txtKGWeight
            // 
            this.txtKGWeight.AllowEmptyState = false;
            // 
            // 
            // 
            this.txtKGWeight.BackgroundStyle.BorderColor = System.Drawing.SystemColors.Control;
            this.txtKGWeight.BackgroundStyle.Class = "DateTimeInputBackground";
            this.txtKGWeight.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtKGWeight.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.txtKGWeight.Font = new System.Drawing.Font("Segoe UI", 14.25F);
            this.txtKGWeight.ForeColor = System.Drawing.Color.White;
            this.txtKGWeight.Increment = 1D;
            this.txtKGWeight.InputHorizontalAlignment = DevComponents.Editors.eHorizontalAlignment.Left;
            this.txtKGWeight.Location = new System.Drawing.Point(649, 73);
            this.txtKGWeight.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtKGWeight.MaxValue = 1000D;
            this.txtKGWeight.MinValue = 0D;
            this.txtKGWeight.Name = "txtKGWeight";
            this.txtKGWeight.Size = new System.Drawing.Size(267, 33);
            this.txtKGWeight.TabIndex = 288;
            this.txtKGWeight.Visible = false;
            // 
            // txtKSWeight
            // 
            this.txtKSWeight.AllowEmptyState = false;
            // 
            // 
            // 
            this.txtKSWeight.BackgroundStyle.BorderColor = System.Drawing.SystemColors.Control;
            this.txtKSWeight.BackgroundStyle.Class = "DateTimeInputBackground";
            this.txtKSWeight.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtKSWeight.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.txtKSWeight.Font = new System.Drawing.Font("Segoe UI", 14.25F);
            this.txtKSWeight.ForeColor = System.Drawing.Color.White;
            this.txtKSWeight.Increment = 1D;
            this.txtKSWeight.InputHorizontalAlignment = DevComponents.Editors.eHorizontalAlignment.Left;
            this.txtKSWeight.Location = new System.Drawing.Point(649, 116);
            this.txtKSWeight.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtKSWeight.MaxValue = 1000D;
            this.txtKSWeight.MinValue = 0D;
            this.txtKSWeight.Name = "txtKSWeight";
            this.txtKSWeight.Size = new System.Drawing.Size(267, 33);
            this.txtKSWeight.TabIndex = 287;
            this.txtKSWeight.Visible = false;
            // 
            // lbl_KsWeight
            // 
            this.lbl_KsWeight.AutoSize = true;
            // 
            // 
            // 
            this.lbl_KsWeight.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lbl_KsWeight.Font = new System.Drawing.Font("Segoe UI", 14.25F);
            this.lbl_KsWeight.ForeColor = System.Drawing.Color.White;
            this.lbl_KsWeight.Location = new System.Drawing.Point(526, 117);
            this.lbl_KsWeight.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lbl_KsWeight.Name = "lbl_KsWeight";
            this.lbl_KsWeight.Size = new System.Drawing.Size(111, 32);
            this.lbl_KsWeight.TabIndex = 286;
            this.lbl_KsWeight.Text = "ø€ÀÆ£®∂÷£©";
            this.lbl_KsWeight.Visible = false;
            // 
            // lbl_KgWeight
            // 
            this.lbl_KgWeight.AutoSize = true;
            // 
            // 
            // 
            this.lbl_KgWeight.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lbl_KgWeight.Font = new System.Drawing.Font("Segoe UI", 14.25F);
            this.lbl_KgWeight.ForeColor = System.Drawing.Color.White;
            this.lbl_KgWeight.Location = new System.Drawing.Point(526, 75);
            this.lbl_KgWeight.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.lbl_KgWeight.Name = "lbl_KgWeight";
            this.lbl_KgWeight.Size = new System.Drawing.Size(111, 32);
            this.lbl_KgWeight.TabIndex = 285;
            this.lbl_KgWeight.Text = "ø€Ì∑£®∂÷£©";
            this.lbl_KgWeight.Visible = false;
            // 
            // pictureBox3
            // 
            this.pictureBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox3.ForeColor = System.Drawing.Color.White;
            this.pictureBox3.Location = new System.Drawing.Point(692, 205);
            this.pictureBox3.Margin = new System.Windows.Forms.Padding(6);
            this.pictureBox3.Name = "pictureBox3";
            this.pictureBox3.Size = new System.Drawing.Size(224, 164);
            this.pictureBox3.TabIndex = 284;
            this.pictureBox3.TabStop = false;
            this.pictureBox3.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDoubleClick);
            // 
            // pictureBox2
            // 
            this.pictureBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox2.ForeColor = System.Drawing.Color.White;
            this.pictureBox2.Location = new System.Drawing.Point(442, 205);
            this.pictureBox2.Margin = new System.Windows.Forms.Padding(6);
            this.pictureBox2.Name = "pictureBox2";
            this.pictureBox2.Size = new System.Drawing.Size(232, 164);
            this.pictureBox2.TabIndex = 283;
            this.pictureBox2.TabStop = false;
            this.pictureBox2.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDoubleClick);
            // 
            // btn_Select
            // 
            this.btn_Select.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btn_Select.Font = new System.Drawing.Font("Segoe UI", 14.25F);
            this.btn_Select.Location = new System.Drawing.Point(361, 161);
            this.btn_Select.Margin = new System.Windows.Forms.Padding(6);
            this.btn_Select.Name = "btn_Select";
            this.btn_Select.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor(2);
            this.btn_Select.Size = new System.Drawing.Size(104, 32);
            this.btn_Select.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btn_Select.TabIndex = 1;
            this.btn_Select.Text = "—°  ‘Ò";
            this.btn_Select.Click += new System.EventHandler(this.btn_Select_Click);
            // 
            // btn_Shoot
            // 
            this.btn_Shoot.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btn_Shoot.Font = new System.Drawing.Font("Segoe UI", 14.25F);
            this.btn_Shoot.Location = new System.Drawing.Point(195, 161);
            this.btn_Shoot.Margin = new System.Windows.Forms.Padding(6);
            this.btn_Shoot.Name = "btn_Shoot";
            this.btn_Shoot.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor(2);
            this.btn_Shoot.Size = new System.Drawing.Size(108, 32);
            this.btn_Shoot.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btn_Shoot.TabIndex = 1;
            this.btn_Shoot.Text = "≈ƒ  ’’";
            this.btn_Shoot.Click += new System.EventHandler(this.btn_Shoot_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.pictureBox1.ForeColor = System.Drawing.Color.White;
            this.pictureBox1.Location = new System.Drawing.Point(191, 205);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(6);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(237, 164);
            this.pictureBox1.TabIndex = 282;
            this.pictureBox1.TabStop = false;
            this.pictureBox1.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.pictureBox1_MouseDoubleClick);
            // 
            // cmbBreakRulesResult
            // 
            this.cmbBreakRulesResult.DisplayMember = "Text";
            this.cmbBreakRulesResult.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbBreakRulesResult.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBreakRulesResult.Font = new System.Drawing.Font("Segoe UI", 14.25F);
            this.cmbBreakRulesResult.ForeColor = System.Drawing.Color.White;
            this.cmbBreakRulesResult.FormattingEnabled = true;
            this.cmbBreakRulesResult.ItemHeight = 27;
            this.cmbBreakRulesResult.Location = new System.Drawing.Point(195, 118);
            this.cmbBreakRulesResult.Margin = new System.Windows.Forms.Padding(6);
            this.cmbBreakRulesResult.Name = "cmbBreakRulesResult";
            this.cmbBreakRulesResult.Size = new System.Drawing.Size(267, 33);
            this.cmbBreakRulesResult.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbBreakRulesResult.TabIndex = 281;
            this.cmbBreakRulesResult.SelectedIndexChanged += new System.EventHandler(this.cmbBreakRulesResult_SelectedIndexChanged);
            // 
            // cmbBreakRules
            // 
            this.cmbBreakRules.DisplayMember = "Text";
            this.cmbBreakRules.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbBreakRules.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBreakRules.Font = new System.Drawing.Font("Segoe UI", 14.25F);
            this.cmbBreakRules.ForeColor = System.Drawing.Color.White;
            this.cmbBreakRules.FormattingEnabled = true;
            this.cmbBreakRules.ItemHeight = 27;
            this.cmbBreakRules.Location = new System.Drawing.Point(195, 73);
            this.cmbBreakRules.Margin = new System.Windows.Forms.Padding(6);
            this.cmbBreakRules.Name = "cmbBreakRules";
            this.cmbBreakRules.Size = new System.Drawing.Size(267, 33);
            this.cmbBreakRules.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbBreakRules.TabIndex = 280;
            // 
            // labelX2
            // 
            this.labelX2.AutoSize = true;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Font = new System.Drawing.Font("Segoe UI", 14.25F);
            this.labelX2.ForeColor = System.Drawing.Color.White;
            this.labelX2.Location = new System.Drawing.Point(92, 161);
            this.labelX2.Margin = new System.Windows.Forms.Padding(6);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(91, 32);
            this.labelX2.TabIndex = 234;
            this.labelX2.Text = "Œ•’¬≈ƒ’’";
            // 
            // labelX6
            // 
            this.labelX6.AutoSize = true;
            // 
            // 
            // 
            this.labelX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX6.Font = new System.Drawing.Font("Segoe UI", 14.25F);
            this.labelX6.ForeColor = System.Drawing.Color.White;
            this.labelX6.Location = new System.Drawing.Point(92, 117);
            this.labelX6.Margin = new System.Windows.Forms.Padding(6);
            this.labelX6.Name = "labelX6";
            this.labelX6.Size = new System.Drawing.Size(91, 32);
            this.labelX6.TabIndex = 8;
            this.labelX6.Text = "Œ•’¬¥¶¿Ì";
            // 
            // labelX4
            // 
            this.labelX4.AutoSize = true;
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Font = new System.Drawing.Font("Segoe UI", 14.25F);
            this.labelX4.ForeColor = System.Drawing.Color.White;
            this.labelX4.Location = new System.Drawing.Point(92, 73);
            this.labelX4.Margin = new System.Windows.Forms.Padding(6);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(91, 32);
            this.labelX4.TabIndex = 4;
            this.labelX4.Text = "Œ•’¬¿‡–Õ";
            // 
            // txtCarNum
            // 
            this.txtCarNum.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
            // 
            // 
            // 
            this.txtCarNum.Border.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(52)))));
            this.txtCarNum.Border.Class = "TextBoxBorder";
            this.txtCarNum.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtCarNum.Font = new System.Drawing.Font("Segoe UI", 14.25F);
            this.txtCarNum.ForeColor = System.Drawing.Color.White;
            this.txtCarNum.Location = new System.Drawing.Point(195, 28);
            this.txtCarNum.Margin = new System.Windows.Forms.Padding(6);
            this.txtCarNum.Name = "txtCarNum";
            this.txtCarNum.ReadOnly = true;
            this.txtCarNum.Size = new System.Drawing.Size(267, 33);
            this.txtCarNum.TabIndex = 1;
            // 
            // labelX1
            // 
            this.labelX1.AutoSize = true;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Font = new System.Drawing.Font("Segoe UI", 14.25F);
            this.labelX1.ForeColor = System.Drawing.Color.White;
            this.labelX1.Location = new System.Drawing.Point(113, 29);
            this.labelX1.Margin = new System.Windows.Forms.Padding(6);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(70, 32);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "≥µ≈∆∫≈";
            // 
            // imageList1
            // 
            this.imageList1.ColorDepth = System.Windows.Forms.ColorDepth.Depth8Bit;
            this.imageList1.ImageSize = new System.Drawing.Size(16, 16);
            this.imageList1.TransparentColor = System.Drawing.Color.Transparent;
            // 
            // FrmCarBreakRules_Confirm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(11F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.CaptionFont = new System.Drawing.Font("Segoe UI", 11.25F);
            this.ClientSize = new System.Drawing.Size(1027, 588);
            this.Controls.Add(this.tableLayoutPanel1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 14.25F);
            this.Margin = new System.Windows.Forms.Padding(6);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmCarBreakRules_Confirm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Œ•’¬»∑»œ";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panelEx1.ResumeLayout(false);
            this.panelEx2.ResumeLayout(false);
            this.panelEx2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtKGWeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtKSWeight)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.ButtonX btnCancel;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private DevComponents.DotNetBar.Controls.TextBoxX txtCarNum;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX labelX6;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.ButtonX btnSave;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbBreakRulesResult;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbBreakRules;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.ImageList imageList1;
        private DevComponents.DotNetBar.ButtonX btn_Shoot;
        private DevComponents.DotNetBar.ButtonX btn_Select;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.PictureBox pictureBox3;
        private System.Windows.Forms.PictureBox pictureBox2;
        private DevComponents.Editors.DoubleInput txtKGWeight;
        private DevComponents.Editors.DoubleInput txtKSWeight;
        private DevComponents.DotNetBar.LabelX lbl_KsWeight;
        private DevComponents.DotNetBar.LabelX lbl_KgWeight;
    }
}