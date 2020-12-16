namespace CMCS.WeighBalance.Frms
{
    partial class FrmBalanceer
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
			DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn1 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
			DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn2 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
			DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn3 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
			DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn4 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
			DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn5 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
			DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn6 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
			DevComponents.DotNetBar.SuperGrid.Style.Background background1 = new DevComponents.DotNetBar.SuperGrid.Style.Background();
			DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn7 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
			DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn8 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
			DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn9 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
			DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn10 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
			DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn11 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
			DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn12 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
			DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn13 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
			DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn14 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
			DevComponents.DotNetBar.SuperGrid.Style.Background background2 = new DevComponents.DotNetBar.SuperGrid.Style.Background();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBalanceer));
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.timer2 = new System.Windows.Forms.Timer(this.components);
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.slightWber = new CMCS.Forms.UserControls.UCtrlSignalLight();
			this.pnlExMain = new DevComponents.DotNetBar.PanelEx();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.flpanSamplerButton = new System.Windows.Forms.FlowLayoutPanel();
			this.lblWber = new System.Windows.Forms.Label();
			this.labelX1 = new DevComponents.DotNetBar.LabelX();
			this.cmbType = new DevComponents.DotNetBar.Controls.ComboBoxEx();
			this.comboItem34 = new DevComponents.Editors.ComboItem();
			this.comboItem35 = new DevComponents.Editors.ComboItem();
			this.comboItem36 = new DevComponents.Editors.ComboItem();
			this.comboItem37 = new DevComponents.Editors.ComboItem();
			this.comboItem38 = new DevComponents.Editors.ComboItem();
			this.comboItem39 = new DevComponents.Editors.ComboItem();
			this.comboItem40 = new DevComponents.Editors.ComboItem();
			this.comboItem41 = new DevComponents.Editors.ComboItem();
			this.comboItem42 = new DevComponents.Editors.ComboItem();
			this.comboItem43 = new DevComponents.Editors.ComboItem();
			this.comboItem44 = new DevComponents.Editors.ComboItem();
			this.comboItem45 = new DevComponents.Editors.ComboItem();
			this.comboItem46 = new DevComponents.Editors.ComboItem();
			this.comboItem47 = new DevComponents.Editors.ComboItem();
			this.comboItem48 = new DevComponents.Editors.ComboItem();
			this.labelX2 = new DevComponents.DotNetBar.LabelX();
			this.txtGGJ = new DevComponents.DotNetBar.Controls.TextBoxX();
			this.btnSubmit = new DevComponents.DotNetBar.ButtonX();
			this.txtInputAssayCode = new DevComponents.DotNetBar.Controls.TextBoxX();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.superGridControl1 = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
			this.superGridControl2 = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
			this.rTxTMessageInfo = new System.Windows.Forms.RichTextBox();
			this.styleManager1 = new DevComponents.DotNetBar.StyleManager(this.components);
			this.pnlExMain.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.flpanSamplerButton.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			this.SuspendLayout();
			// 
			// timer1
			// 
			this.timer1.Enabled = true;
			this.timer1.Interval = 3000;
			// 
			// timer2
			// 
			this.timer2.Interval = 1000;
			// 
			// slightWber
			// 
			this.slightWber.BackColor = System.Drawing.Color.Transparent;
			this.slightWber.ForeColor = System.Drawing.Color.White;
			this.slightWber.LightColor = System.Drawing.Color.Gray;
			this.slightWber.Location = new System.Drawing.Point(3, 6);
			this.slightWber.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
			this.slightWber.Name = "slightWber";
			this.slightWber.Size = new System.Drawing.Size(24, 24);
			this.slightWber.TabIndex = 231;
			this.toolTip1.SetToolTip(this.slightWber, "<绿色> 已连接\r\n<红色> 未连接");
			// 
			// pnlExMain
			// 
			this.pnlExMain.CanvasColor = System.Drawing.SystemColors.Control;
			this.pnlExMain.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.pnlExMain.Controls.Add(this.tableLayoutPanel1);
			this.pnlExMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlExMain.Font = new System.Drawing.Font("Segoe UI", 9F);
			this.pnlExMain.Location = new System.Drawing.Point(0, 0);
			this.pnlExMain.Name = "pnlExMain";
			this.pnlExMain.Size = new System.Drawing.Size(1100, 575);
			this.pnlExMain.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.pnlExMain.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.pnlExMain.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.pnlExMain.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.pnlExMain.Style.GradientAngle = 90;
			this.pnlExMain.TabIndex = 0;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.flpanSamplerButton, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.txtInputAssayCode, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.rTxTMessageInfo, 0, 3);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.ForeColor = System.Drawing.Color.White;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 4;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(1100, 575);
			this.tableLayoutPanel1.TabIndex = 35;
			// 
			// flpanSamplerButton
			// 
			this.flpanSamplerButton.BackColor = System.Drawing.Color.Transparent;
			this.flpanSamplerButton.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.flpanSamplerButton.Controls.Add(this.slightWber);
			this.flpanSamplerButton.Controls.Add(this.lblWber);
			this.flpanSamplerButton.Controls.Add(this.labelX1);
			this.flpanSamplerButton.Controls.Add(this.cmbType);
			this.flpanSamplerButton.Controls.Add(this.labelX2);
			this.flpanSamplerButton.Controls.Add(this.txtGGJ);
			this.flpanSamplerButton.Controls.Add(this.btnSubmit);
			this.flpanSamplerButton.Dock = System.Windows.Forms.DockStyle.Fill;
			this.flpanSamplerButton.ForeColor = System.Drawing.Color.White;
			this.flpanSamplerButton.Location = new System.Drawing.Point(3, 3);
			this.flpanSamplerButton.Name = "flpanSamplerButton";
			this.flpanSamplerButton.Size = new System.Drawing.Size(1094, 34);
			this.flpanSamplerButton.TabIndex = 30;
			// 
			// lblWber
			// 
			this.lblWber.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.lblWber.AutoSize = true;
			this.lblWber.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblWber.ForeColor = System.Drawing.Color.White;
			this.lblWber.Location = new System.Drawing.Point(33, 8);
			this.lblWber.Name = "lblWber";
			this.lblWber.Size = new System.Drawing.Size(73, 20);
			this.lblWber.TabIndex = 232;
			this.lblWber.Text = "电子天平";
			// 
			// labelX1
			// 
			// 
			// 
			// 
			this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX1.Font = new System.Drawing.Font("Segoe UI", 11.25F);
			this.labelX1.ForeColor = System.Drawing.Color.White;
			this.labelX1.Location = new System.Drawing.Point(112, 3);
			this.labelX1.Name = "labelX1";
			this.labelX1.Size = new System.Drawing.Size(59, 23);
			this.labelX1.TabIndex = 233;
			this.labelX1.Text = "类型：";
			// 
			// cmbType
			// 
			this.cmbType.DisplayMember = "Text";
			this.cmbType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.cmbType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbType.Font = new System.Drawing.Font("Segoe UI", 11.25F);
			this.cmbType.ForeColor = System.Drawing.Color.White;
			this.cmbType.FormattingEnabled = true;
			this.cmbType.ItemHeight = 21;
			this.cmbType.Items.AddRange(new object[] {
            this.comboItem34,
            this.comboItem35,
            this.comboItem36,
            this.comboItem37,
            this.comboItem38,
            this.comboItem39,
            this.comboItem40,
            this.comboItem41,
            this.comboItem42,
            this.comboItem43,
            this.comboItem44,
            this.comboItem45,
            this.comboItem46,
            this.comboItem47,
            this.comboItem48});
			this.cmbType.Location = new System.Drawing.Point(177, 3);
			this.cmbType.Name = "cmbType";
			this.cmbType.Size = new System.Drawing.Size(98, 27);
			this.cmbType.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.cmbType.TabIndex = 234;
			// 
			// comboItem34
			// 
			this.comboItem34.Text = "COM1";
			// 
			// comboItem35
			// 
			this.comboItem35.Text = "COM2";
			// 
			// comboItem36
			// 
			this.comboItem36.Text = "COM3";
			// 
			// comboItem37
			// 
			this.comboItem37.Text = "COM4";
			// 
			// comboItem38
			// 
			this.comboItem38.Text = "COM5";
			// 
			// comboItem39
			// 
			this.comboItem39.Text = "COM6";
			// 
			// comboItem40
			// 
			this.comboItem40.Text = "COM7";
			// 
			// comboItem41
			// 
			this.comboItem41.Text = "COM8";
			// 
			// comboItem42
			// 
			this.comboItem42.Text = "COM9";
			// 
			// comboItem43
			// 
			this.comboItem43.Text = "COM10";
			// 
			// comboItem44
			// 
			this.comboItem44.Text = "COM11";
			// 
			// comboItem45
			// 
			this.comboItem45.Text = "COM12";
			// 
			// comboItem46
			// 
			this.comboItem46.Text = "COM13";
			// 
			// comboItem47
			// 
			this.comboItem47.Text = "COM14";
			// 
			// comboItem48
			// 
			this.comboItem48.Text = "COM15";
			// 
			// labelX2
			// 
			// 
			// 
			// 
			this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX2.Font = new System.Drawing.Font("Segoe UI", 11.25F);
			this.labelX2.ForeColor = System.Drawing.Color.White;
			this.labelX2.Location = new System.Drawing.Point(281, 3);
			this.labelX2.Name = "labelX2";
			this.labelX2.Size = new System.Drawing.Size(72, 23);
			this.labelX2.TabIndex = 237;
			this.labelX2.Text = "坩埚架：";
			// 
			// txtGGJ
			// 
			this.txtGGJ.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
			// 
			// 
			// 
			this.txtGGJ.Border.Class = "TextBoxBorder";
			this.txtGGJ.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.txtGGJ.Font = new System.Drawing.Font("Segoe UI", 11.25F);
			this.txtGGJ.ForeColor = System.Drawing.Color.White;
			this.txtGGJ.Location = new System.Drawing.Point(359, 3);
			this.txtGGJ.Name = "txtGGJ";
			this.txtGGJ.Size = new System.Drawing.Size(100, 27);
			this.txtGGJ.TabIndex = 236;
			// 
			// btnSubmit
			// 
			this.btnSubmit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnSubmit.Font = new System.Drawing.Font("Segoe UI", 11.25F);
			this.btnSubmit.Location = new System.Drawing.Point(465, 3);
			this.btnSubmit.Name = "btnSubmit";
			this.btnSubmit.Size = new System.Drawing.Size(75, 23);
			this.btnSubmit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.btnSubmit.TabIndex = 235;
			this.btnSubmit.Text = "提交数据";
			this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
			// 
			// txtInputAssayCode
			// 
			this.txtInputAssayCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
			// 
			// 
			// 
			this.txtInputAssayCode.Border.Class = "TextBoxBorder";
			this.txtInputAssayCode.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.txtInputAssayCode.ButtonCustom.Text = "清空";
			this.txtInputAssayCode.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtInputAssayCode.Font = new System.Drawing.Font("Segoe UI", 18F);
			this.txtInputAssayCode.ForeColor = System.Drawing.Color.White;
			this.txtInputAssayCode.Location = new System.Drawing.Point(3, 43);
			this.txtInputAssayCode.Name = "txtInputAssayCode";
			this.txtInputAssayCode.Size = new System.Drawing.Size(1094, 39);
			this.txtInputAssayCode.TabIndex = 34;
			this.txtInputAssayCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtInputAssayCode.WatermarkText = "请输入化验码. . .";
			this.txtInputAssayCode.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtInputAssayCode_KeyUp);
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 2;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
			this.tableLayoutPanel2.Controls.Add(this.superGridControl1, 0, 0);
			this.tableLayoutPanel2.Controls.Add(this.superGridControl2, 1, 0);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.ForeColor = System.Drawing.Color.White;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 83);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 1;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(1094, 389);
			this.tableLayoutPanel2.TabIndex = 35;
			// 
			// superGridControl1
			// 
			this.superGridControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
			this.superGridControl1.DefaultVisualStyles.CaptionStyles.Default.Font = new System.Drawing.Font("Segoe UI", 11.25F);
			this.superGridControl1.DefaultVisualStyles.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
			this.superGridControl1.DefaultVisualStyles.ColumnHeaderStyles.Default.Font = new System.Drawing.Font("Segoe UI", 11.25F);
			this.superGridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.superGridControl1.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
			this.superGridControl1.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.superGridControl1.ForeColor = System.Drawing.Color.White;
			this.superGridControl1.Location = new System.Drawing.Point(3, 3);
			this.superGridControl1.Name = "superGridControl1";
			this.superGridControl1.PrimaryGrid.AutoGenerateColumns = false;
			this.superGridControl1.PrimaryGrid.Caption.Text = "化 验 码";
			gridColumn1.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridCheckBoxXEditControl);
			gridColumn1.HeaderText = "";
			gridColumn1.Name = "clmCheck";
			gridColumn1.Width = 30;
			gridColumn2.DefaultNewRowCellValue = "删除";
			gridColumn2.HeaderText = "";
			gridColumn2.Name = "clmDelete";
			gridColumn2.NullString = "删除";
			gridColumn2.Width = 40;
			gridColumn3.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
			gridColumn3.DataPropertyName = "AssayCode";
			gridColumn3.HeaderText = "化验码";
			gridColumn3.MinimumWidth = 110;
			gridColumn3.Name = "";
			gridColumn3.Width = 200;
			gridColumn4.DataPropertyName = "AssayType";
			gridColumn4.HeaderText = "类型";
			gridColumn4.Name = "";
			gridColumn5.DataPropertyName = "CreateUser";
			gridColumn5.HeaderText = "操作人";
			gridColumn5.Name = "";
			gridColumn6.DataPropertyName = "Id";
			gridColumn6.Name = "clmId";
			gridColumn6.Visible = false;
			this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn1);
			this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn2);
			this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn3);
			this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn4);
			this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn5);
			this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn6);
			background1.BackFillType = DevComponents.DotNetBar.SuperGrid.Style.BackFillType.Radial;
			background1.Color1 = System.Drawing.Color.DarkTurquoise;
			background1.Color2 = System.Drawing.Color.White;
			this.superGridControl1.PrimaryGrid.DefaultVisualStyles.CaptionStyles.Default.Background = background1;
			this.superGridControl1.PrimaryGrid.DefaultVisualStyles.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
			this.superGridControl1.PrimaryGrid.DefaultVisualStyles.CellStyles.Default.Font = new System.Drawing.Font("Segoe UI", 11.25F);
			this.superGridControl1.PrimaryGrid.DefaultVisualStyles.CellStyles.ReadOnly.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
			this.superGridControl1.PrimaryGrid.DefaultVisualStyles.CellStyles.ReadOnly.Font = new System.Drawing.Font("Segoe UI", 11.25F);
			this.superGridControl1.PrimaryGrid.InitialSelection = DevComponents.DotNetBar.SuperGrid.RelativeSelection.Row;
			this.superGridControl1.PrimaryGrid.MultiSelect = false;
			this.superGridControl1.PrimaryGrid.SelectionGranularity = DevComponents.DotNetBar.SuperGrid.SelectionGranularity.Row;
			this.superGridControl1.PrimaryGrid.ShowRowGridIndex = true;
			this.superGridControl1.Size = new System.Drawing.Size(541, 383);
			this.superGridControl1.TabIndex = 29;
			this.superGridControl1.Text = "superGridControl1";
			this.superGridControl1.CellMouseDown += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridCellMouseEventArgs>(this.superGridControl1_CellMouseDown);
			this.superGridControl1.DataBindingComplete += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridDataBindingCompleteEventArgs>(this.superGridControl1_DataBindingComplete);
			this.superGridControl1.BeginEdit += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridEditEventArgs>(this.superGridControl1_BeginEdit);
			this.superGridControl1.GetRowHeaderText += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridGetRowHeaderTextEventArgs>(this.superGridControl_GetRowHeaderText);
			this.superGridControl1.Click += new System.EventHandler(this.superGridControl1_Click);
			// 
			// superGridControl2
			// 
			this.superGridControl2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
			this.superGridControl2.DefaultVisualStyles.CaptionStyles.Default.Font = new System.Drawing.Font("Segoe UI", 11.25F);
			this.superGridControl2.DefaultVisualStyles.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
			this.superGridControl2.DefaultVisualStyles.ColumnHeaderStyles.Default.Font = new System.Drawing.Font("Segoe UI", 11.25F);
			this.superGridControl2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.superGridControl2.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
			this.superGridControl2.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.superGridControl2.ForeColor = System.Drawing.Color.White;
			this.superGridControl2.Location = new System.Drawing.Point(550, 3);
			this.superGridControl2.Name = "superGridControl2";
			this.superGridControl2.PrimaryGrid.AutoGenerateColumns = false;
			this.superGridControl2.PrimaryGrid.Caption.Text = "重 量 信 息";
			gridColumn7.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridCheckBoxXEditControl);
			gridColumn7.HeaderText = "";
			gridColumn7.Name = "clmDetailCheck";
			gridColumn7.Width = 40;
			gridColumn8.DefaultNewRowCellValue = "删除";
			gridColumn8.HeaderText = "";
			gridColumn8.Name = "clmDelete";
			gridColumn8.NullString = "删除";
			gridColumn8.Width = 40;
			gridColumn9.DataPropertyName = "GGJCode";
			gridColumn9.HeaderText = "坩埚架";
			gridColumn9.Name = "";
			gridColumn9.Width = 60;
			gridColumn10.DataPropertyName = "GGCode";
			gridColumn10.HeaderText = "坩埚号";
			gridColumn10.Name = "";
			gridColumn10.Width = 60;
			gridColumn11.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
			gridColumn11.DataPropertyName = "Weight";
			gridColumn11.HeaderText = "重量(g)";
			gridColumn11.MinimumWidth = 80;
			gridColumn11.Name = "";
			gridColumn11.SortIndicator = DevComponents.DotNetBar.SuperGrid.SortIndicator.None;
			gridColumn11.Width = 80;
			gridColumn12.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
			gridColumn12.DataPropertyName = "CreateDate";
			gridColumn12.HeaderText = "录入时间";
			gridColumn12.MinimumWidth = 130;
			gridColumn12.Name = "";
			gridColumn12.SortIndicator = DevComponents.DotNetBar.SuperGrid.SortIndicator.None;
			gridColumn12.Width = 150;
			gridColumn13.HeaderText = "状态";
			gridColumn13.Name = "clmDetailStatus";
			gridColumn13.Width = 60;
			gridColumn14.DataPropertyName = "Id";
			gridColumn14.Name = "clmId";
			gridColumn14.Visible = false;
			this.superGridControl2.PrimaryGrid.Columns.Add(gridColumn7);
			this.superGridControl2.PrimaryGrid.Columns.Add(gridColumn8);
			this.superGridControl2.PrimaryGrid.Columns.Add(gridColumn9);
			this.superGridControl2.PrimaryGrid.Columns.Add(gridColumn10);
			this.superGridControl2.PrimaryGrid.Columns.Add(gridColumn11);
			this.superGridControl2.PrimaryGrid.Columns.Add(gridColumn12);
			this.superGridControl2.PrimaryGrid.Columns.Add(gridColumn13);
			this.superGridControl2.PrimaryGrid.Columns.Add(gridColumn14);
			background2.BackFillType = DevComponents.DotNetBar.SuperGrid.Style.BackFillType.Radial;
			background2.Color1 = System.Drawing.Color.DarkTurquoise;
			background2.Color2 = System.Drawing.Color.White;
			this.superGridControl2.PrimaryGrid.DefaultVisualStyles.CaptionStyles.Default.Background = background2;
			this.superGridControl2.PrimaryGrid.DefaultVisualStyles.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
			this.superGridControl2.PrimaryGrid.DefaultVisualStyles.CellStyles.Default.Font = new System.Drawing.Font("Segoe UI", 11.25F);
			this.superGridControl2.PrimaryGrid.DefaultVisualStyles.CellStyles.ReadOnly.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
			this.superGridControl2.PrimaryGrid.DefaultVisualStyles.CellStyles.ReadOnly.Font = new System.Drawing.Font("Segoe UI", 11.25F);
			this.superGridControl2.PrimaryGrid.InitialSelection = DevComponents.DotNetBar.SuperGrid.RelativeSelection.Row;
			this.superGridControl2.PrimaryGrid.SelectionGranularity = DevComponents.DotNetBar.SuperGrid.SelectionGranularity.Row;
			this.superGridControl2.PrimaryGrid.ShowRowGridIndex = true;
			this.superGridControl2.Size = new System.Drawing.Size(541, 383);
			this.superGridControl2.TabIndex = 33;
			this.superGridControl2.Text = "superGridControl2";
			this.superGridControl2.CellMouseDown += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridCellMouseEventArgs>(this.superGridControl2_CellMouseDown);
			this.superGridControl2.DataBindingComplete += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridDataBindingCompleteEventArgs>(this.superGridControl2_DataBindingComplete);
			this.superGridControl2.BeginEdit += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridEditEventArgs>(this.superGridControl2_BeginEdit);
			this.superGridControl2.GetRowHeaderText += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridGetRowHeaderTextEventArgs>(this.superGridControl_GetRowHeaderText);
			// 
			// rTxTMessageInfo
			// 
			this.rTxTMessageInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
			this.rTxTMessageInfo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rTxTMessageInfo.ForeColor = System.Drawing.Color.White;
			this.rTxTMessageInfo.Location = new System.Drawing.Point(3, 478);
			this.rTxTMessageInfo.Name = "rTxTMessageInfo";
			this.rTxTMessageInfo.Size = new System.Drawing.Size(1094, 94);
			this.rTxTMessageInfo.TabIndex = 36;
			this.rTxTMessageInfo.Text = "";
			// 
			// styleManager1
			// 
			this.styleManager1.ManagerStyle = DevComponents.DotNetBar.eStyle.Metro;
			this.styleManager1.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51))))), System.Drawing.Color.DarkTurquoise);
			// 
			// FrmBalanceer
			// 
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
			this.ClientSize = new System.Drawing.Size(1100, 575);
			this.Controls.Add(this.pnlExMain);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.ForeColor = System.Drawing.Color.White;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.MaximizeBox = false;
			this.MaximumSize = new System.Drawing.Size(1100, 575);
			this.Name = "FrmBalanceer";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "武汉博晟燃料集中管控-卸样控制程序";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
			this.Load += new System.EventHandler(this.FrmUnloadSampler_Load);
			this.pnlExMain.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.flpanSamplerButton.ResumeLayout(false);
			this.flpanSamplerButton.PerformLayout();
			this.tableLayoutPanel2.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.ToolTip toolTip1;
        private DevComponents.DotNetBar.PanelEx pnlExMain;
        private System.Windows.Forms.FlowLayoutPanel flpanSamplerButton;
        private DevComponents.DotNetBar.SuperGrid.SuperGridControl superGridControl2;
        private DevComponents.DotNetBar.SuperGrid.SuperGridControl superGridControl1;
        private DevComponents.DotNetBar.StyleManager styleManager1;
        private Forms.UserControls.UCtrlSignalLight slightWber;
        private System.Windows.Forms.Label lblWber;
        private DevComponents.DotNetBar.Controls.TextBoxX txtInputAssayCode;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbType;
        private DevComponents.Editors.ComboItem comboItem34;
        private DevComponents.Editors.ComboItem comboItem35;
        private DevComponents.Editors.ComboItem comboItem36;
        private DevComponents.Editors.ComboItem comboItem37;
        private DevComponents.Editors.ComboItem comboItem38;
        private DevComponents.Editors.ComboItem comboItem39;
        private DevComponents.Editors.ComboItem comboItem40;
        private DevComponents.Editors.ComboItem comboItem41;
        private DevComponents.Editors.ComboItem comboItem42;
        private DevComponents.Editors.ComboItem comboItem43;
        private DevComponents.Editors.ComboItem comboItem44;
        private DevComponents.Editors.ComboItem comboItem45;
        private DevComponents.Editors.ComboItem comboItem46;
        private DevComponents.Editors.ComboItem comboItem47;
        private DevComponents.Editors.ComboItem comboItem48;
        private DevComponents.DotNetBar.ButtonX btnSubmit;
		private DevComponents.DotNetBar.Controls.TextBoxX txtGGJ;
		private DevComponents.DotNetBar.LabelX labelX2;
		private System.Windows.Forms.RichTextBox rTxTMessageInfo;
	}
}