namespace CMCS.UnloadSampler.Frms
{
    partial class FrmUnloadSampler
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
            DevComponents.DotNetBar.SuperGrid.Style.Background background2 = new DevComponents.DotNetBar.SuperGrid.Style.Background();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn12 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn13 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn14 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn15 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn16 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn17 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.Style.Background background3 = new DevComponents.DotNetBar.SuperGrid.Style.Background();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmUnloadSampler));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.btnSendMakeCmd = new DevComponents.DotNetBar.ButtonX();
            this.pnlExMain = new DevComponents.DotNetBar.PanelEx();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flpanSamplerButton = new System.Windows.Forms.FlowLayoutPanel();
            this.flpanEquState = new System.Windows.Forms.FlowLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.rTxTMessageInfo = new DevComponents.DotNetBar.Controls.RichTextBoxEx();
            this.panelEx4 = new DevComponents.DotNetBar.PanelEx();
            this.flpanUnloadType = new System.Windows.Forms.FlowLayoutPanel();
            this.rbtnToMaker = new System.Windows.Forms.RadioButton();
            this.rbtnToSubway = new System.Windows.Forms.RadioButton();
            this.btnFallCode = new DevComponents.DotNetBar.ButtonX();
            this.btnSendLoadCmd = new DevComponents.DotNetBar.ButtonX();
            this.superGridControl1 = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
            this.superGridControl2 = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
            this.superGridControl3 = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.dtStartTime = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.dtEndTime = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.btnPreviousDay = new DevComponents.DotNetBar.ButtonX();
            this.btnSearch = new DevComponents.DotNetBar.ButtonX();
            this.btnNextDay = new DevComponents.DotNetBar.ButtonX();
            this.panelEx3 = new DevComponents.DotNetBar.PanelEx();
            this.txt_CoordSampleCode = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btnSearch_BatchCoord = new DevComponents.DotNetBar.ButtonX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.styleManager1 = new DevComponents.DotNetBar.StyleManager(this.components);
            this.pnlExMain.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.panelEx2.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
            this.panelEx4.SuspendLayout();
            this.flpanUnloadType.SuspendLayout();
            this.panelEx1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtStartTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtEndTime)).BeginInit();
            this.panelEx3.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 5000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // timer2
            // 
            this.timer2.Interval = 1000;
            // 
            // btnSendMakeCmd
            // 
            this.btnSendMakeCmd.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSendMakeCmd.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSendMakeCmd.Font = new System.Drawing.Font("Segoe UI", 15.75F);
            this.btnSendMakeCmd.Location = new System.Drawing.Point(191, 59);
            this.btnSendMakeCmd.Name = "btnSendMakeCmd";
            this.btnSendMakeCmd.Size = new System.Drawing.Size(145, 40);
            this.btnSendMakeCmd.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSendMakeCmd.TabIndex = 26;
            this.btnSendMakeCmd.Text = "开 始 制 样";
            this.toolTip1.SetToolTip(this.btnSendMakeCmd, "自动发送制样计划失败时采用人工发送");
            this.btnSendMakeCmd.Click += new System.EventHandler(this.btnSendMakeCmd_Click);
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
            this.pnlExMain.Size = new System.Drawing.Size(1244, 701);
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
            this.tableLayoutPanel1.Controls.Add(this.flpanEquState, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.ForeColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1244, 701);
            this.tableLayoutPanel1.TabIndex = 42;
            // 
            // flpanSamplerButton
            // 
            this.flpanSamplerButton.BackColor = System.Drawing.Color.Transparent;
            this.flpanSamplerButton.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flpanSamplerButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpanSamplerButton.ForeColor = System.Drawing.Color.White;
            this.flpanSamplerButton.Location = new System.Drawing.Point(3, 3);
            this.flpanSamplerButton.Name = "flpanSamplerButton";
            this.flpanSamplerButton.Size = new System.Drawing.Size(1238, 34);
            this.flpanSamplerButton.TabIndex = 30;
            // 
            // flpanEquState
            // 
            this.flpanEquState.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flpanEquState.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpanEquState.ForeColor = System.Drawing.Color.White;
            this.flpanEquState.Location = new System.Drawing.Point(3, 664);
            this.flpanEquState.Name = "flpanEquState";
            this.flpanEquState.Size = new System.Drawing.Size(1238, 34);
            this.flpanEquState.TabIndex = 32;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Controls.Add(this.panelEx2, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.superGridControl1, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.superGridControl2, 1, 2);
            this.tableLayoutPanel2.Controls.Add(this.superGridControl3, 0, 2);
            this.tableLayoutPanel2.Controls.Add(this.panelEx1, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.panelEx3, 1, 1);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.ForeColor = System.Drawing.Color.White;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 43);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 3;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1238, 615);
            this.tableLayoutPanel2.TabIndex = 33;
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.tableLayoutPanel3);
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx2.Location = new System.Drawing.Point(622, 3);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(613, 281);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 39;
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel3.Controls.Add(this.rTxTMessageInfo, 0, 1);
            this.tableLayoutPanel3.Controls.Add(this.panelEx4, 0, 0);
            this.tableLayoutPanel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel3.ForeColor = System.Drawing.Color.White;
            this.tableLayoutPanel3.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(613, 281);
            this.tableLayoutPanel3.TabIndex = 0;
            // 
            // rTxTMessageInfo
            // 
            // 
            // 
            // 
            this.rTxTMessageInfo.BackgroundStyle.Class = "RichTextBoxBorder";
            this.rTxTMessageInfo.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.rTxTMessageInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rTxTMessageInfo.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rTxTMessageInfo.ForeColor = System.Drawing.Color.White;
            this.rTxTMessageInfo.Location = new System.Drawing.Point(3, 143);
            this.rTxTMessageInfo.Name = "rTxTMessageInfo";
            this.rTxTMessageInfo.Size = new System.Drawing.Size(607, 135);
            this.rTxTMessageInfo.TabIndex = 0;
            // 
            // panelEx4
            // 
            this.panelEx4.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx4.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx4.Controls.Add(this.btnSendMakeCmd);
            this.panelEx4.Controls.Add(this.flpanUnloadType);
            this.panelEx4.Controls.Add(this.btnFallCode);
            this.panelEx4.Controls.Add(this.btnSendLoadCmd);
            this.panelEx4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx4.Location = new System.Drawing.Point(3, 3);
            this.panelEx4.Name = "panelEx4";
            this.panelEx4.Size = new System.Drawing.Size(607, 134);
            this.panelEx4.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx4.Style.BackColor1.Color = System.Drawing.Color.Transparent;
            this.panelEx4.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx4.Style.BorderColor.Color = System.Drawing.Color.Gray;
            this.panelEx4.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx4.Style.GradientAngle = 90;
            this.panelEx4.TabIndex = 36;
            // 
            // flpanUnloadType
            // 
            this.flpanUnloadType.BackColor = System.Drawing.Color.Transparent;
            this.flpanUnloadType.Controls.Add(this.rbtnToMaker);
            this.flpanUnloadType.Controls.Add(this.rbtnToSubway);
            this.flpanUnloadType.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flpanUnloadType.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.flpanUnloadType.ForeColor = System.Drawing.Color.White;
            this.flpanUnloadType.Location = new System.Drawing.Point(15, 13);
            this.flpanUnloadType.Name = "flpanUnloadType";
            this.flpanUnloadType.Size = new System.Drawing.Size(157, 83);
            this.flpanUnloadType.TabIndex = 8;
            // 
            // rbtnToMaker
            // 
            this.rbtnToMaker.AutoSize = true;
            this.rbtnToMaker.Checked = true;
            this.rbtnToMaker.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnToMaker.ForeColor = System.Drawing.Color.White;
            this.rbtnToMaker.Location = new System.Drawing.Point(3, 3);
            this.rbtnToMaker.Name = "rbtnToMaker";
            this.rbtnToMaker.Size = new System.Drawing.Size(97, 34);
            this.rbtnToMaker.TabIndex = 1;
            this.rbtnToMaker.TabStop = true;
            this.rbtnToMaker.Tag = "1";
            this.rbtnToMaker.Text = "制样机";
            this.rbtnToMaker.UseVisualStyleBackColor = true;
            this.rbtnToMaker.CheckedChanged += new System.EventHandler(this.rbtnUnLoad_CheckedChanged);
            // 
            // rbtnToSubway
            // 
            this.rbtnToSubway.AutoSize = true;
            this.rbtnToSubway.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnToSubway.ForeColor = System.Drawing.Color.White;
            this.rbtnToSubway.Location = new System.Drawing.Point(3, 43);
            this.rbtnToSubway.Name = "rbtnToSubway";
            this.rbtnToSubway.Size = new System.Drawing.Size(97, 34);
            this.rbtnToSubway.TabIndex = 0;
            this.rbtnToSubway.Tag = "2";
            this.rbtnToSubway.Text = "归批机";
            this.rbtnToSubway.UseVisualStyleBackColor = true;
            // 
            // btnFallCode
            // 
            this.btnFallCode.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnFallCode.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnFallCode.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnFallCode.Location = new System.Drawing.Point(355, 13);
            this.btnFallCode.Name = "btnFallCode";
            this.btnFallCode.Size = new System.Drawing.Size(145, 37);
            this.btnFallCode.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnFallCode.TabIndex = 9;
            this.btnFallCode.Text = "归批机倒料";
            this.btnFallCode.Tooltip = "从归批机倒料到制样机";
            this.btnFallCode.Click += new System.EventHandler(this.btnFallCode_Click);
            // 
            // btnSendLoadCmd
            // 
            this.btnSendLoadCmd.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSendLoadCmd.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSendLoadCmd.Font = new System.Drawing.Font("Segoe UI", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSendLoadCmd.Location = new System.Drawing.Point(191, 13);
            this.btnSendLoadCmd.Name = "btnSendLoadCmd";
            this.btnSendLoadCmd.Size = new System.Drawing.Size(145, 40);
            this.btnSendLoadCmd.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSendLoadCmd.TabIndex = 9;
            this.btnSendLoadCmd.Text = "开 始 卸 样";
            this.btnSendLoadCmd.Tooltip = "选择制样机或归批机后勾选集样罐";
            this.btnSendLoadCmd.Click += new System.EventHandler(this.btnSendLoadCmd_Click);
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
            this.superGridControl1.PrimaryGrid.Caption.Text = "集 样 罐 信 息";
            this.superGridControl1.PrimaryGrid.CheckBoxes = true;
            gridColumn1.Name = "";
            gridColumn1.Width = 20;
            gridColumn2.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gridColumn2.DataPropertyName = "BarrelNumber";
            gridColumn2.HeaderText = "罐号";
            gridColumn2.MinimumWidth = 50;
            gridColumn2.Name = "";
            gridColumn2.Width = 50;
            gridColumn3.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gridColumn3.DataPropertyName = "SampleCode";
            gridColumn3.HeaderText = "采样码";
            gridColumn3.MinimumWidth = 110;
            gridColumn3.Name = "";
            gridColumn3.Width = 150;
            gridColumn4.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gridColumn4.DataPropertyName = "SampleCount";
            gridColumn4.HeaderText = "子样数";
            gridColumn4.MinimumWidth = 80;
            gridColumn4.Name = "";
            gridColumn4.Width = 80;
            gridColumn5.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gridColumn5.DataPropertyName = "BarrelStatus";
            gridColumn5.HeaderText = "桶满";
            gridColumn5.MinimumWidth = 60;
            gridColumn5.Name = "";
            gridColumn5.Width = 60;
            gridColumn6.DataPropertyName = "BarrelType";
            gridColumn6.HeaderText = "样罐类型";
            gridColumn6.Name = "";
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
            this.superGridControl1.Size = new System.Drawing.Size(613, 281);
            this.superGridControl1.TabIndex = 29;
            this.superGridControl1.Text = "superGridControl1";
            this.superGridControl1.BeforeCheck += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridBeforeCheckEventArgs>(this.superGridControl1_BeforeCheck);
            this.superGridControl1.AfterCheck += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridAfterCheckEventArgs>(this.superGridControl1_AfterCheck);
            this.superGridControl1.DataBindingComplete += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridDataBindingCompleteEventArgs>(this.superGridControl1_DataBindingComplete);
            this.superGridControl1.BeginEdit += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridEditEventArgs>(this.superGridControl1_BeginEdit);
            this.superGridControl1.GetCellStyle += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridGetCellStyleEventArgs>(this.superGridControl1_GetCellStyle);
            this.superGridControl1.GetRowHeaderText += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridGetRowHeaderTextEventArgs>(this.superGridControl_GetRowHeaderText);
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
            this.superGridControl2.Location = new System.Drawing.Point(622, 330);
            this.superGridControl2.Name = "superGridControl2";
            this.superGridControl2.PrimaryGrid.AutoGenerateColumns = false;
            this.superGridControl2.PrimaryGrid.Caption.Text = "归 批 机 样 桶 信 息";
            this.superGridControl2.PrimaryGrid.CheckBoxes = true;
            gridColumn7.MinimumWidth = 20;
            gridColumn7.Name = "";
            gridColumn7.SortIndicator = DevComponents.DotNetBar.SuperGrid.SortIndicator.None;
            gridColumn7.Width = 20;
            gridColumn8.DataPropertyName = "UpdateTime";
            gridColumn8.HeaderText = "卸样时间";
            gridColumn8.Name = "";
            gridColumn8.Width = 150;
            gridColumn9.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gridColumn9.DataPropertyName = "Coord";
            gridColumn9.FillWeight = 80;
            gridColumn9.HeaderText = "位置";
            gridColumn9.MinimumWidth = 80;
            gridColumn9.Name = "";
            gridColumn9.SortIndicator = DevComponents.DotNetBar.SuperGrid.SortIndicator.None;
            gridColumn9.Width = 80;
            gridColumn10.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gridColumn10.DataPropertyName = "SampleCode";
            gridColumn10.HeaderText = "采样码";
            gridColumn10.MinimumWidth = 100;
            gridColumn10.Name = "";
            gridColumn10.SortIndicator = DevComponents.DotNetBar.SuperGrid.SortIndicator.None;
            gridColumn10.Width = 150;
            gridColumn11.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gridColumn11.DataPropertyName = "State";
            gridColumn11.HeaderText = "是否有桶";
            gridColumn11.MinimumWidth = 130;
            gridColumn11.Name = "";
            gridColumn11.SortIndicator = DevComponents.DotNetBar.SuperGrid.SortIndicator.None;
            gridColumn11.Width = 80;
            this.superGridControl2.PrimaryGrid.Columns.Add(gridColumn7);
            this.superGridControl2.PrimaryGrid.Columns.Add(gridColumn8);
            this.superGridControl2.PrimaryGrid.Columns.Add(gridColumn9);
            this.superGridControl2.PrimaryGrid.Columns.Add(gridColumn10);
            this.superGridControl2.PrimaryGrid.Columns.Add(gridColumn11);
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
            this.superGridControl2.Size = new System.Drawing.Size(613, 282);
            this.superGridControl2.TabIndex = 33;
            this.superGridControl2.Text = "superGridControl2";
            this.superGridControl2.BeforeCheck += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridBeforeCheckEventArgs>(this.superGridControl2_BeforeCheck);
            this.superGridControl2.DataBindingComplete += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridDataBindingCompleteEventArgs>(this.superGridControl2_DataBindingComplete);
            this.superGridControl2.BeginEdit += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridEditEventArgs>(this.superGridControl2_BeginEdit);
            this.superGridControl2.GetCellStyle += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridGetCellStyleEventArgs>(this.superGridControl2_GetCellStyle);
            this.superGridControl2.GetRowHeaderText += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridGetRowHeaderTextEventArgs>(this.superGridControl_GetRowHeaderText);
            // 
            // superGridControl3
            // 
            this.superGridControl3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
            this.superGridControl3.DefaultVisualStyles.CaptionStyles.Default.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.superGridControl3.DefaultVisualStyles.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            this.superGridControl3.DefaultVisualStyles.ColumnHeaderStyles.Default.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.superGridControl3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.superGridControl3.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
            this.superGridControl3.Font = new System.Drawing.Font("Segoe UI", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.superGridControl3.ForeColor = System.Drawing.Color.White;
            this.superGridControl3.Location = new System.Drawing.Point(3, 330);
            this.superGridControl3.Name = "superGridControl3";
            this.superGridControl3.PrimaryGrid.AutoGenerateColumns = false;
            this.superGridControl3.PrimaryGrid.Caption.Text = "最 近 卸 样 记 录";
            this.superGridControl3.PrimaryGrid.CheckBoxes = true;
            gridColumn12.MinimumWidth = 20;
            gridColumn12.Name = "";
            gridColumn12.SortIndicator = DevComponents.DotNetBar.SuperGrid.SortIndicator.None;
            gridColumn12.Width = 20;
            gridColumn13.DataPropertyName = "CreateDate";
            gridColumn13.HeaderText = "卸样时间";
            gridColumn13.Name = "";
            gridColumn13.Width = 150;
            gridColumn14.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gridColumn14.DataPropertyName = "SampleCode";
            gridColumn14.FillWeight = 70;
            gridColumn14.HeaderText = "采样码";
            gridColumn14.MinimumWidth = 70;
            gridColumn14.Name = "";
            gridColumn14.SortIndicator = DevComponents.DotNetBar.SuperGrid.SortIndicator.None;
            gridColumn14.Width = 130;
            gridColumn15.DataPropertyName = "ResultCode";
            gridColumn15.HeaderText = "结果";
            gridColumn15.Name = "";
            gridColumn15.Width = 70;
            gridColumn16.DataPropertyName = "UnLoadType";
            gridColumn16.HeaderText = "卸样类型";
            gridColumn16.Name = "";
            gridColumn17.DataPropertyName = "BarrelNumber";
            gridColumn17.HeaderText = "罐号";
            gridColumn17.Name = "";
            this.superGridControl3.PrimaryGrid.Columns.Add(gridColumn12);
            this.superGridControl3.PrimaryGrid.Columns.Add(gridColumn13);
            this.superGridControl3.PrimaryGrid.Columns.Add(gridColumn14);
            this.superGridControl3.PrimaryGrid.Columns.Add(gridColumn15);
            this.superGridControl3.PrimaryGrid.Columns.Add(gridColumn16);
            this.superGridControl3.PrimaryGrid.Columns.Add(gridColumn17);
            background3.BackFillType = DevComponents.DotNetBar.SuperGrid.Style.BackFillType.Radial;
            background3.Color1 = System.Drawing.Color.DarkTurquoise;
            background3.Color2 = System.Drawing.Color.White;
            this.superGridControl3.PrimaryGrid.DefaultVisualStyles.CaptionStyles.Default.Background = background3;
            this.superGridControl3.PrimaryGrid.DefaultVisualStyles.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            this.superGridControl3.PrimaryGrid.DefaultVisualStyles.CellStyles.Default.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.superGridControl3.PrimaryGrid.DefaultVisualStyles.CellStyles.ReadOnly.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            this.superGridControl3.PrimaryGrid.DefaultVisualStyles.CellStyles.ReadOnly.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.superGridControl3.PrimaryGrid.ShowRowGridIndex = true;
            this.superGridControl3.Size = new System.Drawing.Size(613, 282);
            this.superGridControl3.TabIndex = 37;
            this.superGridControl3.Text = "superGridControl3";
            this.superGridControl3.CellClick += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridCellClickEventArgs>(this.superGridControl3_CellClick);
            this.superGridControl3.BeginEdit += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridEditEventArgs>(this.superGridControl3_BeginEdit);
            this.superGridControl3.GetRowHeaderText += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridGetRowHeaderTextEventArgs>(this.superGridControl_GetRowHeaderText);
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.dtStartTime);
            this.panelEx1.Controls.Add(this.labelX1);
            this.panelEx1.Controls.Add(this.dtEndTime);
            this.panelEx1.Controls.Add(this.btnPreviousDay);
            this.panelEx1.Controls.Add(this.btnSearch);
            this.panelEx1.Controls.Add(this.btnNextDay);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(3, 290);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(613, 34);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 38;
            // 
            // dtStartTime
            // 
            this.dtStartTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.dtStartTime.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dtStartTime.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtStartTime.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.dtStartTime.ButtonDropDown.Visible = true;
            this.dtStartTime.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.dtStartTime.ForeColor = System.Drawing.Color.White;
            this.dtStartTime.IsPopupCalendarOpen = false;
            this.dtStartTime.Location = new System.Drawing.Point(114, 4);
            // 
            // 
            // 
            this.dtStartTime.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtStartTime.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtStartTime.MonthCalendar.CalendarDimensions = new System.Drawing.Size(1, 1);
            this.dtStartTime.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.dtStartTime.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.dtStartTime.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.dtStartTime.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.dtStartTime.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.dtStartTime.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.dtStartTime.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.dtStartTime.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtStartTime.MonthCalendar.DisplayMonth = new System.DateTime(2018, 5, 1, 0, 0, 0, 0);
            this.dtStartTime.MonthCalendar.FirstDayOfWeek = System.DayOfWeek.Monday;
            this.dtStartTime.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.dtStartTime.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtStartTime.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.dtStartTime.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.dtStartTime.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.dtStartTime.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtStartTime.MonthCalendar.TodayButtonVisible = true;
            this.dtStartTime.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.dtStartTime.Name = "dtStartTime";
            this.dtStartTime.Size = new System.Drawing.Size(147, 27);
            this.dtStartTime.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.dtStartTime.TabIndex = 38;
            // 
            // labelX1
            // 
            this.labelX1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.labelX1.ForeColor = System.Drawing.Color.White;
            this.labelX1.Location = new System.Drawing.Point(37, 6);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(76, 23);
            this.labelX1.TabIndex = 40;
            this.labelX1.Text = "卸样时间";
            // 
            // dtEndTime
            // 
            this.dtEndTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.dtEndTime.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dtEndTime.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtEndTime.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.dtEndTime.ButtonDropDown.Visible = true;
            this.dtEndTime.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.dtEndTime.ForeColor = System.Drawing.Color.White;
            this.dtEndTime.IsPopupCalendarOpen = false;
            this.dtEndTime.Location = new System.Drawing.Point(267, 4);
            // 
            // 
            // 
            this.dtEndTime.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtEndTime.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtEndTime.MonthCalendar.CalendarDimensions = new System.Drawing.Size(1, 1);
            this.dtEndTime.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.dtEndTime.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.dtEndTime.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.dtEndTime.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.dtEndTime.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.dtEndTime.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.dtEndTime.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.dtEndTime.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtEndTime.MonthCalendar.DisplayMonth = new System.DateTime(2018, 5, 1, 0, 0, 0, 0);
            this.dtEndTime.MonthCalendar.FirstDayOfWeek = System.DayOfWeek.Monday;
            this.dtEndTime.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.dtEndTime.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtEndTime.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.dtEndTime.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.dtEndTime.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.dtEndTime.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtEndTime.MonthCalendar.TodayButtonVisible = true;
            this.dtEndTime.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.dtEndTime.Name = "dtEndTime";
            this.dtEndTime.Size = new System.Drawing.Size(141, 27);
            this.dtEndTime.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.dtEndTime.TabIndex = 38;
            // 
            // btnPreviousDay
            // 
            this.btnPreviousDay.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnPreviousDay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPreviousDay.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnPreviousDay.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.btnPreviousDay.Location = new System.Drawing.Point(482, 6);
            this.btnPreviousDay.Name = "btnPreviousDay";
            this.btnPreviousDay.Size = new System.Drawing.Size(59, 23);
            this.btnPreviousDay.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnPreviousDay.TabIndex = 39;
            this.btnPreviousDay.Text = "上一天";
            this.btnPreviousDay.Click += new System.EventHandler(this.btnPreviousDay_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.btnSearch.Location = new System.Drawing.Point(413, 6);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(62, 23);
            this.btnSearch.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSearch.TabIndex = 39;
            this.btnSearch.Text = "搜  索";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnNextDay
            // 
            this.btnNextDay.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnNextDay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNextDay.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnNextDay.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.btnNextDay.Location = new System.Drawing.Point(549, 6);
            this.btnNextDay.Name = "btnNextDay";
            this.btnNextDay.Size = new System.Drawing.Size(64, 23);
            this.btnNextDay.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnNextDay.TabIndex = 39;
            this.btnNextDay.Text = "下一天";
            this.btnNextDay.Click += new System.EventHandler(this.btnNextDay_Click);
            // 
            // panelEx3
            // 
            this.panelEx3.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx3.Controls.Add(this.txt_CoordSampleCode);
            this.panelEx3.Controls.Add(this.btnSearch_BatchCoord);
            this.panelEx3.Controls.Add(this.labelX2);
            this.panelEx3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx3.Location = new System.Drawing.Point(622, 290);
            this.panelEx3.Name = "panelEx3";
            this.panelEx3.Size = new System.Drawing.Size(613, 34);
            this.panelEx3.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx3.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx3.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx3.Style.GradientAngle = 90;
            this.panelEx3.TabIndex = 40;
            // 
            // txt_CoordSampleCode
            // 
            this.txt_CoordSampleCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txt_CoordSampleCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
            // 
            // 
            // 
            this.txt_CoordSampleCode.Border.Class = "TextBoxBorder";
            this.txt_CoordSampleCode.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txt_CoordSampleCode.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.txt_CoordSampleCode.ForeColor = System.Drawing.Color.White;
            this.txt_CoordSampleCode.Location = new System.Drawing.Point(385, 3);
            this.txt_CoordSampleCode.Name = "txt_CoordSampleCode";
            this.txt_CoordSampleCode.Size = new System.Drawing.Size(158, 27);
            this.txt_CoordSampleCode.TabIndex = 41;
            // 
            // btnSearch_BatchCoord
            // 
            this.btnSearch_BatchCoord.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSearch_BatchCoord.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch_BatchCoord.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSearch_BatchCoord.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.btnSearch_BatchCoord.Location = new System.Drawing.Point(548, 4);
            this.btnSearch_BatchCoord.Name = "btnSearch_BatchCoord";
            this.btnSearch_BatchCoord.Size = new System.Drawing.Size(62, 23);
            this.btnSearch_BatchCoord.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSearch_BatchCoord.TabIndex = 39;
            this.btnSearch_BatchCoord.Text = "搜  索";
            this.btnSearch_BatchCoord.Click += new System.EventHandler(this.btnSearch_BatchCoord_Click);
            // 
            // labelX2
            // 
            this.labelX2.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.labelX2.ForeColor = System.Drawing.Color.White;
            this.labelX2.Location = new System.Drawing.Point(320, 4);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(60, 23);
            this.labelX2.TabIndex = 40;
            this.labelX2.Text = "采样码";
            // 
            // styleManager1
            // 
            this.styleManager1.ManagerStyle = DevComponents.DotNetBar.eStyle.Metro;
            this.styleManager1.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51))))), System.Drawing.Color.DarkTurquoise);
            // 
            // FrmUnloadSampler
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1244, 701);
            this.Controls.Add(this.pnlExMain);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmUnloadSampler";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "武汉博晟燃料集中管控-卸样控制程序";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.FrmUnloadSampler_Load);
            this.pnlExMain.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.panelEx2.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.panelEx4.ResumeLayout(false);
            this.flpanUnloadType.ResumeLayout(false);
            this.flpanUnloadType.PerformLayout();
            this.panelEx1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtStartTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtEndTime)).EndInit();
            this.panelEx3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.ToolTip toolTip1;
        private DevComponents.DotNetBar.PanelEx pnlExMain;
        private DevComponents.DotNetBar.SuperGrid.SuperGridControl superGridControl3;
        private DevComponents.DotNetBar.PanelEx panelEx4;
        private DevComponents.DotNetBar.ButtonX btnSendMakeCmd;
        private System.Windows.Forms.FlowLayoutPanel flpanUnloadType;
        private System.Windows.Forms.RadioButton rbtnToSubway;
        private System.Windows.Forms.RadioButton rbtnToMaker;
        private DevComponents.DotNetBar.ButtonX btnSendLoadCmd;
        private System.Windows.Forms.FlowLayoutPanel flpanSamplerButton;
        private DevComponents.DotNetBar.SuperGrid.SuperGridControl superGridControl2;
        private DevComponents.DotNetBar.Controls.RichTextBoxEx rTxTMessageInfo;
        private DevComponents.DotNetBar.SuperGrid.SuperGridControl superGridControl1;
        private System.Windows.Forms.FlowLayoutPanel flpanEquState;
        private DevComponents.DotNetBar.StyleManager styleManager1;
        private DevComponents.DotNetBar.ButtonX btnFallCode;
        private DevComponents.DotNetBar.ButtonX btnSearch;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput dtEndTime;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput dtStartTime;
        private DevComponents.DotNetBar.ButtonX btnPreviousDay;
        private DevComponents.DotNetBar.ButtonX btnNextDay;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.ButtonX btnSearch_BatchCoord;
        private DevComponents.DotNetBar.Controls.TextBoxX txt_CoordSampleCode;
        private DevComponents.DotNetBar.LabelX labelX2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private DevComponents.DotNetBar.PanelEx panelEx3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
    }
}