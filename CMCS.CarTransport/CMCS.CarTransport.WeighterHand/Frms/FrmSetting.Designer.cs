namespace CMCS.CarTransport.WeighterHand.Frms
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
            this.components = new System.ComponentModel.Container();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.btnSubmit = new DevComponents.DotNetBar.ButtonX();
            this.btnCancel = new DevComponents.DotNetBar.ButtonX();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.tabControl1 = new DevComponents.DotNetBar.TabControl();
            this.tabControlPanel1 = new DevComponents.DotNetBar.TabControlPanel();
            this.txtSelfConnStr = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX20 = new DevComponents.DotNetBar.LabelX();
            this.chbStartup = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.txtAppIdentifier = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.labelX4 = new DevComponents.DotNetBar.LabelX();
            this.tabItem1 = new DevComponents.DotNetBar.TabItem(this.components);
            this.tabControlPanel3 = new DevComponents.DotNetBar.TabControlPanel();
            this.cmbWberParity = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX3 = new DevComponents.DotNetBar.LabelX();
            this.dbtxtMinWeight = new DevComponents.Editors.DoubleInput();
            this.labelX23 = new DevComponents.DotNetBar.LabelX();
            this.cmbWberStopBits = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX24 = new DevComponents.DotNetBar.LabelX();
            this.cmbWberDataBits = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX25 = new DevComponents.DotNetBar.LabelX();
            this.cmbWberBandrate = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX26 = new DevComponents.DotNetBar.LabelX();
            this.cmbWberCom = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.labelX27 = new DevComponents.DotNetBar.LabelX();
            this.tabItem3 = new DevComponents.DotNetBar.TabItem(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            this.panelEx1.SuspendLayout();
            this.panelEx2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.tabControl1)).BeginInit();
            this.tabControl1.SuspendLayout();
            this.tabControlPanel1.SuspendLayout();
            this.tabControlPanel3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dbtxtMinWeight)).BeginInit();
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
            this.tableLayoutPanel1.Size = new System.Drawing.Size(709, 506);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.btnSubmit);
            this.panelEx1.Controls.Add(this.btnCancel);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(3, 469);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(703, 34);
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
            this.btnSubmit.Location = new System.Drawing.Point(538, 6);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(75, 23);
            this.btnSubmit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSubmit.TabIndex = 1;
            this.btnSubmit.Text = "��  ��";
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.btnCancel.Location = new System.Drawing.Point(619, 6);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnCancel.TabIndex = 0;
            this.btnCancel.Text = "ȡ  ��";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.tabControl1);
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx2.Location = new System.Drawing.Point(3, 3);
            this.panelEx2.Margin = new System.Windows.Forms.Padding(3, 3, 3, 0);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(703, 463);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 1;
            // 
            // tabControl1
            // 
            this.tabControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
            this.tabControl1.CanReorderTabs = false;
            this.tabControl1.ColorScheme.TabItemBackgroundColorBlend.AddRange(new DevComponents.DotNetBar.BackgroundColorBlend[] {
            new DevComponents.DotNetBar.BackgroundColorBlend(System.Drawing.Color.Empty, 0F),
            new DevComponents.DotNetBar.BackgroundColorBlend(System.Drawing.Color.Empty, 0.45F),
            new DevComponents.DotNetBar.BackgroundColorBlend(System.Drawing.Color.Empty, 0.45F),
            new DevComponents.DotNetBar.BackgroundColorBlend(System.Drawing.Color.Empty, 1F)});
            this.tabControl1.ColorScheme.TabItemBorder = System.Drawing.Color.Empty;
            this.tabControl1.ColorScheme.TabItemBorderDark = System.Drawing.Color.Empty;
            this.tabControl1.ColorScheme.TabItemHotBackgroundColorBlend.AddRange(new DevComponents.DotNetBar.BackgroundColorBlend[] {
            new DevComponents.DotNetBar.BackgroundColorBlend(System.Drawing.Color.Empty, 0F),
            new DevComponents.DotNetBar.BackgroundColorBlend(System.Drawing.Color.Empty, 0.45F),
            new DevComponents.DotNetBar.BackgroundColorBlend(System.Drawing.Color.Empty, 0.45F),
            new DevComponents.DotNetBar.BackgroundColorBlend(System.Drawing.Color.Empty, 1F)});
            this.tabControl1.ColorScheme.TabItemSelectedBackgroundColorBlend.AddRange(new DevComponents.DotNetBar.BackgroundColorBlend[] {
            new DevComponents.DotNetBar.BackgroundColorBlend(System.Drawing.Color.Empty, 0F),
            new DevComponents.DotNetBar.BackgroundColorBlend(System.Drawing.Color.Empty, 0.45F),
            new DevComponents.DotNetBar.BackgroundColorBlend(System.Drawing.Color.Empty, 0.45F),
            new DevComponents.DotNetBar.BackgroundColorBlend(System.Drawing.Color.Empty, 1F)});
            this.tabControl1.ColorScheme.TabItemSelectedBorder = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(188)))), ((int)(((byte)(204)))));
            this.tabControl1.ColorScheme.TabItemSelectedBorderDark = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(188)))), ((int)(((byte)(204)))));
            this.tabControl1.ColorScheme.TabItemSelectedBorderLight = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(188)))), ((int)(((byte)(204)))));
            this.tabControl1.ColorScheme.TabPanelBorder = System.Drawing.Color.Empty;
            this.tabControl1.Controls.Add(this.tabControlPanel3);
            this.tabControl1.Controls.Add(this.tabControlPanel1);
            this.tabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.ForeColor = System.Drawing.Color.White;
            this.tabControl1.Location = new System.Drawing.Point(0, 0);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedTabFont = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tabControl1.SelectedTabIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(703, 463);
            this.tabControl1.Style = DevComponents.DotNetBar.eTabStripStyle.Metro;
            this.tabControl1.TabIndex = 0;
            this.tabControl1.TabLayoutType = DevComponents.DotNetBar.eTabLayoutType.MultilineNoNavigationBox;
            this.tabControl1.Tabs.Add(this.tabItem1);
            this.tabControl1.Tabs.Add(this.tabItem3);
            this.tabControl1.TabStop = false;
            // 
            // tabControlPanel1
            // 
            this.tabControlPanel1.Controls.Add(this.txtSelfConnStr);
            this.tabControlPanel1.Controls.Add(this.labelX20);
            this.tabControlPanel1.Controls.Add(this.chbStartup);
            this.tabControlPanel1.Controls.Add(this.txtAppIdentifier);
            this.tabControlPanel1.Controls.Add(this.labelX4);
            this.tabControlPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanel1.Location = new System.Drawing.Point(0, 31);
            this.tabControlPanel1.Name = "tabControlPanel1";
            this.tabControlPanel1.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanel1.Size = new System.Drawing.Size(703, 432);
            this.tabControlPanel1.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
            this.tabControlPanel1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.tabControlPanel1.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right) 
            | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanel1.Style.GradientAngle = 90;
            this.tabControlPanel1.TabIndex = 1;
            this.tabControlPanel1.TabItem = this.tabItem1;
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
            this.txtSelfConnStr.Location = new System.Drawing.Point(167, 67);
            this.txtSelfConnStr.Name = "txtSelfConnStr";
            this.txtSelfConnStr.Size = new System.Drawing.Size(446, 27);
            this.txtSelfConnStr.TabIndex = 20;
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
            this.labelX20.Location = new System.Drawing.Point(30, 69);
            this.labelX20.Name = "labelX20";
            this.labelX20.Size = new System.Drawing.Size(132, 24);
            this.labelX20.TabIndex = 19;
            this.labelX20.Text = "���ݿ������ַ���";
            // 
            // chbStartup
            // 
            this.chbStartup.AutoSize = true;
            this.chbStartup.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.chbStartup.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chbStartup.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chbStartup.ForeColor = System.Drawing.Color.White;
            this.chbStartup.Location = new System.Drawing.Point(167, 105);
            this.chbStartup.Name = "chbStartup";
            this.chbStartup.Size = new System.Drawing.Size(90, 24);
            this.chbStartup.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chbStartup.TabIndex = 18;
            this.chbStartup.Text = "��������";
            // 
            // txtAppIdentifier
            // 
            this.txtAppIdentifier.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
            // 
            // 
            // 
            this.txtAppIdentifier.Border.Class = "TextBoxBorder";
            this.txtAppIdentifier.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtAppIdentifier.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtAppIdentifier.ForeColor = System.Drawing.Color.White;
            this.txtAppIdentifier.Location = new System.Drawing.Point(167, 29);
            this.txtAppIdentifier.Name = "txtAppIdentifier";
            this.txtAppIdentifier.Size = new System.Drawing.Size(180, 27);
            this.txtAppIdentifier.TabIndex = 17;
            // 
            // labelX4
            // 
            this.labelX4.AutoSize = true;
            this.labelX4.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX4.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX4.ForeColor = System.Drawing.Color.White;
            this.labelX4.Location = new System.Drawing.Point(61, 31);
            this.labelX4.Name = "labelX4";
            this.labelX4.Size = new System.Drawing.Size(101, 24);
            this.labelX4.TabIndex = 16;
            this.labelX4.Text = "����Ψһ��ʶ";
            // 
            // tabItem1
            // 
            this.tabItem1.AttachedControl = this.tabControlPanel1;
            this.tabItem1.Name = "tabItem1";
            this.tabItem1.Text = "��������";
            // 
            // tabControlPanel3
            // 
            this.tabControlPanel3.Controls.Add(this.cmbWberParity);
            this.tabControlPanel3.Controls.Add(this.labelX3);
            this.tabControlPanel3.Controls.Add(this.dbtxtMinWeight);
            this.tabControlPanel3.Controls.Add(this.labelX23);
            this.tabControlPanel3.Controls.Add(this.cmbWberStopBits);
            this.tabControlPanel3.Controls.Add(this.labelX24);
            this.tabControlPanel3.Controls.Add(this.cmbWberDataBits);
            this.tabControlPanel3.Controls.Add(this.labelX25);
            this.tabControlPanel3.Controls.Add(this.cmbWberBandrate);
            this.tabControlPanel3.Controls.Add(this.labelX26);
            this.tabControlPanel3.Controls.Add(this.cmbWberCom);
            this.tabControlPanel3.Controls.Add(this.labelX27);
            this.tabControlPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControlPanel3.Location = new System.Drawing.Point(0, 31);
            this.tabControlPanel3.Name = "tabControlPanel3";
            this.tabControlPanel3.Padding = new System.Windows.Forms.Padding(1);
            this.tabControlPanel3.Size = new System.Drawing.Size(703, 432);
            this.tabControlPanel3.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
            this.tabControlPanel3.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.tabControlPanel3.Style.BorderSide = ((DevComponents.DotNetBar.eBorderSide)(((DevComponents.DotNetBar.eBorderSide.Left | DevComponents.DotNetBar.eBorderSide.Right) 
            | DevComponents.DotNetBar.eBorderSide.Bottom)));
            this.tabControlPanel3.Style.GradientAngle = 90;
            this.tabControlPanel3.TabIndex = 3;
            this.tabControlPanel3.TabItem = this.tabItem3;
            // 
            // cmbWberParity
            // 
            this.cmbWberParity.DisplayMember = "Text";
            this.cmbWberParity.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbWberParity.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWberParity.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbWberParity.ForeColor = System.Drawing.Color.White;
            this.cmbWberParity.FormattingEnabled = true;
            this.cmbWberParity.ItemHeight = 21;
            this.cmbWberParity.Location = new System.Drawing.Point(167, 105);
            this.cmbWberParity.Name = "cmbWberParity";
            this.cmbWberParity.Size = new System.Drawing.Size(92, 27);
            this.cmbWberParity.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbWberParity.TabIndex = 54;
            // 
            // labelX3
            // 
            this.labelX3.AutoSize = true;
            this.labelX3.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX3.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX3.ForeColor = System.Drawing.Color.White;
            this.labelX3.Location = new System.Drawing.Point(107, 107);
            this.labelX3.Name = "labelX3";
            this.labelX3.Size = new System.Drawing.Size(54, 24);
            this.labelX3.TabIndex = 53;
            this.labelX3.Text = "У��λ";
            // 
            // dbtxtMinWeight
            // 
            this.dbtxtMinWeight.AllowEmptyState = false;
            this.dbtxtMinWeight.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
            // 
            // 
            // 
            this.dbtxtMinWeight.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dbtxtMinWeight.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dbtxtMinWeight.ButtonFreeText.Shortcut = DevComponents.DotNetBar.eShortcut.F2;
            this.dbtxtMinWeight.ForeColor = System.Drawing.Color.White;
            this.dbtxtMinWeight.Increment = 1D;
            this.dbtxtMinWeight.Location = new System.Drawing.Point(464, 105);
            this.dbtxtMinWeight.MaxValue = 10D;
            this.dbtxtMinWeight.MinValue = 0.01D;
            this.dbtxtMinWeight.Name = "dbtxtMinWeight";
            this.dbtxtMinWeight.Size = new System.Drawing.Size(92, 27);
            this.dbtxtMinWeight.TabIndex = 45;
            this.dbtxtMinWeight.Value = 0.01D;
            // 
            // labelX23
            // 
            this.labelX23.AutoSize = true;
            this.labelX23.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX23.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX23.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX23.ForeColor = System.Drawing.Color.White;
            this.labelX23.Location = new System.Drawing.Point(388, 107);
            this.labelX23.Name = "labelX23";
            this.labelX23.Size = new System.Drawing.Size(70, 24);
            this.labelX23.TabIndex = 43;
            this.labelX23.Text = "��С����";
            // 
            // cmbWberStopBits
            // 
            this.cmbWberStopBits.DisplayMember = "Text";
            this.cmbWberStopBits.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbWberStopBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWberStopBits.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbWberStopBits.ForeColor = System.Drawing.Color.White;
            this.cmbWberStopBits.FormattingEnabled = true;
            this.cmbWberStopBits.ItemHeight = 21;
            this.cmbWberStopBits.Location = new System.Drawing.Point(464, 67);
            this.cmbWberStopBits.Name = "cmbWberStopBits";
            this.cmbWberStopBits.Size = new System.Drawing.Size(92, 27);
            this.cmbWberStopBits.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbWberStopBits.TabIndex = 42;
            // 
            // labelX24
            // 
            this.labelX24.AutoSize = true;
            this.labelX24.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX24.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX24.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX24.ForeColor = System.Drawing.Color.White;
            this.labelX24.Location = new System.Drawing.Point(404, 69);
            this.labelX24.Name = "labelX24";
            this.labelX24.Size = new System.Drawing.Size(54, 24);
            this.labelX24.TabIndex = 41;
            this.labelX24.Text = "ֹͣλ";
            // 
            // cmbWberDataBits
            // 
            this.cmbWberDataBits.DisplayMember = "Text";
            this.cmbWberDataBits.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbWberDataBits.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWberDataBits.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbWberDataBits.ForeColor = System.Drawing.Color.White;
            this.cmbWberDataBits.FormattingEnabled = true;
            this.cmbWberDataBits.ItemHeight = 21;
            this.cmbWberDataBits.Location = new System.Drawing.Point(167, 67);
            this.cmbWberDataBits.Name = "cmbWberDataBits";
            this.cmbWberDataBits.Size = new System.Drawing.Size(92, 27);
            this.cmbWberDataBits.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbWberDataBits.TabIndex = 40;
            // 
            // labelX25
            // 
            this.labelX25.AutoSize = true;
            this.labelX25.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX25.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX25.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX25.ForeColor = System.Drawing.Color.White;
            this.labelX25.Location = new System.Drawing.Point(107, 69);
            this.labelX25.Name = "labelX25";
            this.labelX25.Size = new System.Drawing.Size(54, 24);
            this.labelX25.TabIndex = 39;
            this.labelX25.Text = "����λ";
            // 
            // cmbWberBandrate
            // 
            this.cmbWberBandrate.DisplayMember = "Text";
            this.cmbWberBandrate.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbWberBandrate.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWberBandrate.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbWberBandrate.ForeColor = System.Drawing.Color.White;
            this.cmbWberBandrate.FormattingEnabled = true;
            this.cmbWberBandrate.ItemHeight = 21;
            this.cmbWberBandrate.Location = new System.Drawing.Point(464, 29);
            this.cmbWberBandrate.Name = "cmbWberBandrate";
            this.cmbWberBandrate.Size = new System.Drawing.Size(92, 27);
            this.cmbWberBandrate.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbWberBandrate.TabIndex = 38;
            // 
            // labelX26
            // 
            this.labelX26.AutoSize = true;
            this.labelX26.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX26.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX26.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX26.ForeColor = System.Drawing.Color.White;
            this.labelX26.Location = new System.Drawing.Point(404, 31);
            this.labelX26.Name = "labelX26";
            this.labelX26.Size = new System.Drawing.Size(54, 24);
            this.labelX26.TabIndex = 37;
            this.labelX26.Text = "������";
            // 
            // cmbWberCom
            // 
            this.cmbWberCom.DisplayMember = "Text";
            this.cmbWberCom.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbWberCom.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbWberCom.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cmbWberCom.ForeColor = System.Drawing.Color.White;
            this.cmbWberCom.FormattingEnabled = true;
            this.cmbWberCom.ItemHeight = 21;
            this.cmbWberCom.Location = new System.Drawing.Point(167, 29);
            this.cmbWberCom.Name = "cmbWberCom";
            this.cmbWberCom.Size = new System.Drawing.Size(92, 27);
            this.cmbWberCom.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbWberCom.TabIndex = 36;
            // 
            // labelX27
            // 
            this.labelX27.AutoSize = true;
            this.labelX27.BackColor = System.Drawing.Color.Transparent;
            // 
            // 
            // 
            this.labelX27.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX27.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.labelX27.ForeColor = System.Drawing.Color.White;
            this.labelX27.Location = new System.Drawing.Point(122, 31);
            this.labelX27.Name = "labelX27";
            this.labelX27.Size = new System.Drawing.Size(39, 24);
            this.labelX27.TabIndex = 35;
            this.labelX27.Text = "����";
            // 
            // tabItem3
            // 
            this.tabItem3.AttachedControl = this.tabControlPanel3;
            this.tabItem3.Name = "tabItem3";
            this.tabItem3.Text = "�ذ��Ǳ�";
            // 
            // FrmSetting
            // 
            this.AcceptButton = this.btnSubmit;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(709, 506);
            this.Controls.Add(this.tableLayoutPanel1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MaximizeBox = false;
            this.Name = "FrmSetting";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "��������";
            this.Load += new System.EventHandler(this.FrmSetting_Load);
            this.Shown += new System.EventHandler(this.FrmSetting_Shown);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panelEx1.ResumeLayout(false);
            this.panelEx2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.tabControl1)).EndInit();
            this.tabControl1.ResumeLayout(false);
            this.tabControlPanel1.ResumeLayout(false);
            this.tabControlPanel1.PerformLayout();
            this.tabControlPanel3.ResumeLayout(false);
            this.tabControlPanel3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dbtxtMinWeight)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.ButtonX btnCancel;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private DevComponents.DotNetBar.ButtonX btnSubmit;
        private DevComponents.DotNetBar.TabControl tabControl1;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanel1;
        private DevComponents.DotNetBar.TabItem tabItem1;
        private DevComponents.DotNetBar.TabControlPanel tabControlPanel3;
        private DevComponents.DotNetBar.TabItem tabItem3;
        private DevComponents.Editors.DoubleInput dbtxtMinWeight;
        private DevComponents.DotNetBar.LabelX labelX23;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbWberStopBits;
        private DevComponents.DotNetBar.LabelX labelX24;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbWberDataBits;
        private DevComponents.DotNetBar.LabelX labelX25;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbWberBandrate;
        private DevComponents.DotNetBar.LabelX labelX26;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbWberCom;
        private DevComponents.DotNetBar.LabelX labelX27;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbWberParity;
        private DevComponents.DotNetBar.LabelX labelX3;
        private DevComponents.DotNetBar.Controls.CheckBoxX chbStartup;
        private DevComponents.DotNetBar.Controls.TextBoxX txtAppIdentifier;
        private DevComponents.DotNetBar.LabelX labelX4;
        private DevComponents.DotNetBar.Controls.TextBoxX txtSelfConnStr;
        private DevComponents.DotNetBar.LabelX labelX20;
    }
}