namespace CMCS.MobilePad.Win.Frms
{
    partial class FrmStorageTemperature_Confirm
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
            this.btnSave = new DevComponents.DotNetBar.ButtonX();
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.panel1 = new System.Windows.Forms.Panel();
            this.label7 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtPointY = new DevComponents.Editors.DoubleInput();
            this.txtPointX = new DevComponents.Editors.DoubleInput();
            this.ddlUnitName = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.ddlPoleCode = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX18 = new DevComponents.DotNetBar.LabelX();
            this.labelX19 = new DevComponents.DotNetBar.LabelX();
            this.labelX5 = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.Temperature = new DevComponents.Editors.DoubleInput();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.tableLayoutPanel1.SuspendLayout();
            this.panelEx1.SuspendLayout();
            this.panelEx2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPointY)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPointX)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Temperature)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(216)))), ((int)(((byte)(216)))));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panelEx1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panelEx2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.ForeColor = System.Drawing.Color.Black;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 62F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(899, 471);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.btnSave);
            this.panelEx1.Controls.Add(this.btnCancel);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(4, 414);
            this.panelEx1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(891, 52);
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
            this.btnSave.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.btnSave.Location = new System.Drawing.Point(672, 8);
            this.btnSave.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(100, 38);
            this.btnSave.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSave.TabIndex = 1;
            this.btnSave.Text = "保存";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.btnCancel.Location = new System.Drawing.Point(784, 8);
            this.btnCancel.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 38);
            this.btnCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "取  消";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.Temperature);
            this.panelEx2.Controls.Add(this.labelX2);
            this.panelEx2.Controls.Add(this.panel1);
            this.panelEx2.Controls.Add(this.label7);
            this.panelEx2.Controls.Add(this.label15);
            this.panelEx2.Controls.Add(this.label14);
            this.panelEx2.Controls.Add(this.label13);
            this.panelEx2.Controls.Add(this.label12);
            this.panelEx2.Controls.Add(this.label11);
            this.panelEx2.Controls.Add(this.label10);
            this.panelEx2.Controls.Add(this.label9);
            this.panelEx2.Controls.Add(this.label8);
            this.panelEx2.Controls.Add(this.label6);
            this.panelEx2.Controls.Add(this.label5);
            this.panelEx2.Controls.Add(this.label4);
            this.panelEx2.Controls.Add(this.label3);
            this.panelEx2.Controls.Add(this.label2);
            this.panelEx2.Controls.Add(this.label1);
            this.panelEx2.Controls.Add(this.txtPointY);
            this.panelEx2.Controls.Add(this.txtPointX);
            this.panelEx2.Controls.Add(this.ddlUnitName);
            this.panelEx2.Controls.Add(this.ddlPoleCode);
            this.panelEx2.Controls.Add(this.labelX18);
            this.panelEx2.Controls.Add(this.labelX19);
            this.panelEx2.Controls.Add(this.labelX5);
            this.panelEx2.Controls.Add(this.labelX1);
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx2.Location = new System.Drawing.Point(4, 5);
            this.panelEx2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 0);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(891, 404);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 1;
            // 
            // panel1
            // 
            this.panel1.ForeColor = System.Drawing.Color.Black;
            this.panel1.Location = new System.Drawing.Point(575, 28);
            this.panel1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(259, 278);
            this.panel1.TabIndex = 266;
            this.panel1.MouseClick += new System.Windows.Forms.MouseEventHandler(this.panel1_MouseClick);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(575, 325);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(17, 20);
            this.label7.TabIndex = 282;
            this.label7.Text = "0";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.ForeColor = System.Drawing.Color.Black;
            this.label15.Location = new System.Drawing.Point(541, 282);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(17, 20);
            this.label15.TabIndex = 281;
            this.label15.Text = "0";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.ForeColor = System.Drawing.Color.Black;
            this.label14.Location = new System.Drawing.Point(863, 286);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(18, 20);
            this.label14.TabIndex = 280;
            this.label14.Text = "X";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.ForeColor = System.Drawing.Color.Black;
            this.label13.Location = new System.Drawing.Point(556, 9);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(17, 20);
            this.label13.TabIndex = 279;
            this.label13.Text = "Y";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(819, 325);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(25, 20);
            this.label12.TabIndex = 278;
            this.label12.Text = "50";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.ForeColor = System.Drawing.Color.Black;
            this.label11.Location = new System.Drawing.Point(768, 325);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(25, 20);
            this.label11.TabIndex = 277;
            this.label11.Text = "40";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(719, 325);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(25, 20);
            this.label10.TabIndex = 276;
            this.label10.Text = "30";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(668, 325);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(25, 20);
            this.label9.TabIndex = 275;
            this.label9.Text = "20";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.ForeColor = System.Drawing.Color.Black;
            this.label8.Location = new System.Drawing.Point(617, 325);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(25, 20);
            this.label8.TabIndex = 274;
            this.label8.Text = "10";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(533, 238);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(25, 20);
            this.label6.TabIndex = 272;
            this.label6.Text = "10";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(533, 195);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(25, 20);
            this.label5.TabIndex = 271;
            this.label5.Text = "20";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(533, 152);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(25, 20);
            this.label4.TabIndex = 270;
            this.label4.Text = "30";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(533, 109);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(25, 20);
            this.label3.TabIndex = 269;
            this.label3.Text = "40";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(533, 66);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(25, 20);
            this.label2.TabIndex = 268;
            this.label2.Text = "50";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(533, 23);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(25, 20);
            this.label1.TabIndex = 267;
            this.label1.Text = "60";
            // 
            // txtPointY
            // 
            this.txtPointY.AllowEmptyState = false;
            // 
            // 
            // 
            this.txtPointY.BackgroundStyle.BorderColor = System.Drawing.Color.Fuchsia;
            this.txtPointY.BackgroundStyle.Class = "DateTimeInputBackground";
            this.txtPointY.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtPointY.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.txtPointY.Font = new System.Drawing.Font("Segoe UI", 14.25F);
            this.txtPointY.ForeColor = System.Drawing.Color.Black;
            this.txtPointY.Increment = 1D;
            this.txtPointY.InputHorizontalAlignment = DevComponents.Editors.eHorizontalAlignment.Left;
            this.txtPointY.Location = new System.Drawing.Point(228, 226);
            this.txtPointY.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtPointY.MaxValue = 1000D;
            this.txtPointY.MinValue = 0D;
            this.txtPointY.Name = "txtPointY";
            this.txtPointY.Size = new System.Drawing.Size(253, 33);
            this.txtPointY.TabIndex = 265;
            this.txtPointY.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtPointY_KeyUp);
            // 
            // txtPointX
            // 
            this.txtPointX.AllowEmptyState = false;
            // 
            // 
            // 
            this.txtPointX.BackgroundStyle.BorderColor = System.Drawing.Color.Fuchsia;
            this.txtPointX.BackgroundStyle.Class = "DateTimeInputBackground";
            this.txtPointX.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtPointX.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.txtPointX.Font = new System.Drawing.Font("Segoe UI", 14.25F);
            this.txtPointX.ForeColor = System.Drawing.Color.Black;
            this.txtPointX.Increment = 1D;
            this.txtPointX.InputHorizontalAlignment = DevComponents.Editors.eHorizontalAlignment.Left;
            this.txtPointX.Location = new System.Drawing.Point(228, 162);
            this.txtPointX.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txtPointX.MaxValue = 1000D;
            this.txtPointX.MinValue = 0D;
            this.txtPointX.Name = "txtPointX";
            this.txtPointX.Size = new System.Drawing.Size(253, 33);
            this.txtPointX.TabIndex = 264;
            this.txtPointX.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtPointX_KeyUp);
            // 
            // ddlUnitName
            // 
            this.ddlUnitName.DisplayMember = "Text";
            this.ddlUnitName.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ddlUnitName.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlUnitName.Font = new System.Drawing.Font("Segoe UI", 14.25F);
            this.ddlUnitName.ForeColor = System.Drawing.Color.Black;
            this.ddlUnitName.FormattingEnabled = true;
            this.ddlUnitName.ItemHeight = 27;
            this.ddlUnitName.Location = new System.Drawing.Point(228, 98);
            this.ddlUnitName.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ddlUnitName.Name = "ddlUnitName";
            this.ddlUnitName.Size = new System.Drawing.Size(252, 33);
            this.ddlUnitName.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ddlUnitName.TabIndex = 263;
            // 
            // ddlPoleCode
            // 
            this.ddlPoleCode.DisplayMember = "Text";
            this.ddlPoleCode.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.ddlPoleCode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.ddlPoleCode.Font = new System.Drawing.Font("Segoe UI", 14.25F);
            this.ddlPoleCode.ForeColor = System.Drawing.Color.Black;
            this.ddlPoleCode.FormattingEnabled = true;
            this.ddlPoleCode.ItemHeight = 27;
            this.ddlPoleCode.Location = new System.Drawing.Point(228, 34);
            this.ddlPoleCode.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.ddlPoleCode.Name = "ddlPoleCode";
            this.ddlPoleCode.Size = new System.Drawing.Size(252, 33);
            this.ddlPoleCode.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.ddlPoleCode.TabIndex = 262;
            // 
            // labelX18
            // 
            this.labelX18.AutoSize = true;
            // 
            // 
            // 
            this.labelX18.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX18.Font = new System.Drawing.Font("Segoe UI", 14.25F);
            this.labelX18.ForeColor = System.Drawing.Color.Black;
            this.labelX18.Location = new System.Drawing.Point(77, 229);
            this.labelX18.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.labelX18.Name = "labelX18";
            this.labelX18.Size = new System.Drawing.Size(61, 32);
            this.labelX18.TabIndex = 260;
            this.labelX18.Text = "Y坐标";
            // 
            // labelX19
            // 
            this.labelX19.AutoSize = true;
            // 
            // 
            // 
            this.labelX19.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX19.Font = new System.Drawing.Font("Segoe UI", 14.25F);
            this.labelX19.ForeColor = System.Drawing.Color.Black;
            this.labelX19.Location = new System.Drawing.Point(77, 165);
            this.labelX19.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.labelX19.Name = "labelX19";
            this.labelX19.Size = new System.Drawing.Size(61, 32);
            this.labelX19.TabIndex = 258;
            this.labelX19.Text = "X坐标";
            // 
            // labelX5
            // 
            this.labelX5.AutoSize = true;
            // 
            // 
            // 
            this.labelX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX5.Font = new System.Drawing.Font("Segoe UI", 14.25F);
            this.labelX5.ForeColor = System.Drawing.Color.Black;
            this.labelX5.Location = new System.Drawing.Point(77, 100);
            this.labelX5.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.labelX5.Name = "labelX5";
            this.labelX5.Size = new System.Drawing.Size(132, 32);
            this.labelX5.TabIndex = 236;
            this.labelX5.Text = "煤场分区名称";
            // 
            // labelX1
            // 
            this.labelX1.AutoSize = true;
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Font = new System.Drawing.Font("Segoe UI", 14.25F);
            this.labelX1.ForeColor = System.Drawing.Color.Black;
            this.labelX1.Location = new System.Drawing.Point(77, 35);
            this.labelX1.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(111, 32);
            this.labelX1.TabIndex = 0;
            this.labelX1.Text = "测温杆编号";
            // 
            // Temperature
            // 
            this.Temperature.AllowEmptyState = false;
            // 
            // 
            // 
            this.Temperature.BackgroundStyle.BorderColor = System.Drawing.Color.Fuchsia;
            this.Temperature.BackgroundStyle.Class = "DateTimeInputBackground";
            this.Temperature.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.Temperature.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.Temperature.Font = new System.Drawing.Font("Segoe UI", 14.25F);
            this.Temperature.ForeColor = System.Drawing.Color.Black;
            this.Temperature.Increment = 1D;
            this.Temperature.InputHorizontalAlignment = DevComponents.Editors.eHorizontalAlignment.Left;
            this.Temperature.Location = new System.Drawing.Point(227, 286);
            this.Temperature.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.Temperature.MaxValue = 1000D;
            this.Temperature.MinValue = 0D;
            this.Temperature.Name = "Temperature";
            this.Temperature.Size = new System.Drawing.Size(253, 33);
            this.Temperature.TabIndex = 284;
            // 
            // labelX2
            // 
            this.labelX2.AutoSize = true;
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Font = new System.Drawing.Font("Segoe UI", 14.25F);
            this.labelX2.ForeColor = System.Drawing.Color.Black;
            this.labelX2.Location = new System.Drawing.Point(76, 289);
            this.labelX2.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(84, 32);
            this.labelX2.TabIndex = 283;
            this.labelX2.Text = "温度(℃)";
            // 
            // FrmStorageTemperature_Confirm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 20F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.CaptionFont = new System.Drawing.Font("Segoe UI", 11.25F);
            this.ClientSize = new System.Drawing.Size(899, 471);
            this.Controls.Add(this.tableLayoutPanel1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "FrmStorageTemperature_Confirm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "煤场测温杆设置";
            this.Shown += new System.EventHandler(this.FrmStorageTemperature_Confirm_Shown);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panelEx1.ResumeLayout(false);
            this.panelEx2.ResumeLayout(false);
            this.panelEx2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtPointY)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPointX)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Temperature)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.ButtonX btnCancel;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.ButtonX btnSave;
        private DevComponents.DotNetBar.LabelX labelX5;
        private DevComponents.DotNetBar.LabelX labelX18;
        private DevComponents.DotNetBar.LabelX labelX19;
        private DevComponents.DotNetBar.Controls.ComboBoxEx ddlUnitName;
        private DevComponents.DotNetBar.Controls.ComboBoxEx ddlPoleCode;
        private DevComponents.Editors.DoubleInput txtPointY;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label15;
        private DevComponents.Editors.DoubleInput txtPointX;
        private DevComponents.Editors.DoubleInput Temperature;
        private DevComponents.DotNetBar.LabelX labelX2;
    }
}