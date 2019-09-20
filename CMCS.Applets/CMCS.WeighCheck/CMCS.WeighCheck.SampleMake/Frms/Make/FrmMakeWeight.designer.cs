namespace CMCS.WeighCheck.SampleMake.Frms.Make
{
    partial class FrmMakeWeight
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
			this.components = new System.ComponentModel.Container();
			DevComponents.DotNetBar.SuperGrid.Style.Background background1 = new DevComponents.DotNetBar.SuperGrid.Style.Background();
			DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn1 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
			DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn2 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
			DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn3 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
			DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn4 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
			DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn5 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
			DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn6 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMakeWeight));
			this.printDocument1 = new System.Drawing.Printing.PrintDocument();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.slightWber = new CMCS.Forms.UserControls.UCtrlSignalLight();
			this.pnlExMain = new DevComponents.DotNetBar.PanelEx();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
			this.txtInputMakeCode = new DevComponents.DotNetBar.Controls.TextBoxX();
			this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
			this.lbl_CurrentWeight = new System.Windows.Forms.Label();
			this.lblweight = new System.Windows.Forms.Label();
			this.slightRwer = new CMCS.Forms.UserControls.UCtrlSignalLight();
			this.label1 = new System.Windows.Forms.Label();
			this.lblCurrentFlowFlag = new System.Windows.Forms.Label();
			this.lblWber = new System.Windows.Forms.Label();
			this.label10 = new System.Windows.Forms.Label();
			this.rtxtMakeWeightInfo = new DevComponents.DotNetBar.Controls.RichTextBoxEx();
			this.superGridControl1 = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
			this.panelEx3 = new DevComponents.DotNetBar.PanelEx();
			this.panelAssayTargetType = new DevComponents.DotNetBar.PanelEx();
			this.rbtnNormal = new System.Windows.Forms.RadioButton();
			this.rbtnAll = new System.Windows.Forms.RadioButton();
			this.rbtnHandChoose = new System.Windows.Forms.RadioButton();
			this.cmbAssayType = new DevComponents.DotNetBar.Controls.ComboBoxEx();
			this.panelAssayTarget = new DevComponents.DotNetBar.PanelEx();
			this.checkBoxX4 = new DevComponents.DotNetBar.Controls.CheckBoxX();
			this.checkBoxX8 = new DevComponents.DotNetBar.Controls.CheckBoxX();
			this.checkBoxX1 = new DevComponents.DotNetBar.Controls.CheckBoxX();
			this.checkBoxX7 = new DevComponents.DotNetBar.Controls.CheckBoxX();
			this.checkBoxX6 = new DevComponents.DotNetBar.Controls.CheckBoxX();
			this.checkBoxX5 = new DevComponents.DotNetBar.Controls.CheckBoxX();
			this.checkBoxX3 = new DevComponents.DotNetBar.Controls.CheckBoxX();
			this.checkBoxX2 = new DevComponents.DotNetBar.Controls.CheckBoxX();
			this.timer2 = new System.Windows.Forms.Timer(this.components);
			this.btnReset = new DevComponents.DotNetBar.ButtonX();
			this.pnlExMain.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.panelEx2.SuspendLayout();
			this.panelEx1.SuspendLayout();
			this.panelEx3.SuspendLayout();
			this.panelAssayTargetType.SuspendLayout();
			this.panelAssayTarget.SuspendLayout();
			this.SuspendLayout();
			// 
			// slightWber
			// 
			this.slightWber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.slightWber.BackColor = System.Drawing.Color.Transparent;
			this.slightWber.ForeColor = System.Drawing.Color.White;
			this.slightWber.LightColor = System.Drawing.Color.Gray;
			this.slightWber.Location = new System.Drawing.Point(1152, 1);
			this.slightWber.Name = "slightWber";
			this.slightWber.Size = new System.Drawing.Size(20, 20);
			this.slightWber.TabIndex = 221;
			this.toolTip1.SetToolTip(this.slightWber, "<绿色> 已连接\r\n<红色> 未连接");
			// 
			// pnlExMain
			// 
			this.pnlExMain.CanvasColor = System.Drawing.SystemColors.Control;
			this.pnlExMain.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.pnlExMain.Controls.Add(this.tableLayoutPanel1);
			this.pnlExMain.Dock = System.Windows.Forms.DockStyle.Fill;
			this.pnlExMain.Font = new System.Drawing.Font("Segoe UI", 8.25F);
			this.pnlExMain.Location = new System.Drawing.Point(0, 0);
			this.pnlExMain.Name = "pnlExMain";
			this.pnlExMain.Size = new System.Drawing.Size(1326, 554);
			this.pnlExMain.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.pnlExMain.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(58)))), ((int)(((byte)(63)))));
			this.pnlExMain.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.pnlExMain.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.pnlExMain.Style.GradientAngle = 90;
			this.pnlExMain.TabIndex = 0;
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.panelEx2, 0, 2);
			this.tableLayoutPanel1.Controls.Add(this.panelEx1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.rtxtMakeWeightInfo, 0, 4);
			this.tableLayoutPanel1.Controls.Add(this.superGridControl1, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.panelEx3, 0, 1);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.ForeColor = System.Drawing.Color.White;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 5;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 60F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 120F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(1326, 554);
			this.tableLayoutPanel1.TabIndex = 224;
			// 
			// panelEx2
			// 
			this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
			this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.panelEx2.Controls.Add(this.txtInputMakeCode);
			this.panelEx2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelEx2.Location = new System.Drawing.Point(3, 93);
			this.panelEx2.Name = "panelEx2";
			this.panelEx2.Size = new System.Drawing.Size(1320, 39);
			this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelEx2.Style.GradientAngle = 90;
			this.panelEx2.TabIndex = 227;
			// 
			// txtInputMakeCode
			// 
			this.txtInputMakeCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
			// 
			// 
			// 
			this.txtInputMakeCode.Border.Class = "TextBoxBorder";
			this.txtInputMakeCode.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.txtInputMakeCode.ButtonCustom.Text = "清空";
			this.txtInputMakeCode.Dock = System.Windows.Forms.DockStyle.Fill;
			this.txtInputMakeCode.Font = new System.Drawing.Font("Segoe UI", 18F);
			this.txtInputMakeCode.ForeColor = System.Drawing.Color.White;
			this.txtInputMakeCode.Location = new System.Drawing.Point(0, 0);
			this.txtInputMakeCode.Name = "txtInputMakeCode";
			this.txtInputMakeCode.Size = new System.Drawing.Size(1320, 39);
			this.txtInputMakeCode.TabIndex = 229;
			this.txtInputMakeCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtInputMakeCode.WatermarkText = "扫描制样码. . .";
			this.txtInputMakeCode.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtInputMakeCode_KeyUp);
			// 
			// panelEx1
			// 
			this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
			this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.panelEx1.Controls.Add(this.lbl_CurrentWeight);
			this.panelEx1.Controls.Add(this.lblweight);
			this.panelEx1.Controls.Add(this.slightWber);
			this.panelEx1.Controls.Add(this.slightRwer);
			this.panelEx1.Controls.Add(this.label1);
			this.panelEx1.Controls.Add(this.lblCurrentFlowFlag);
			this.panelEx1.Controls.Add(this.lblWber);
			this.panelEx1.Controls.Add(this.label10);
			this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelEx1.Location = new System.Drawing.Point(3, 3);
			this.panelEx1.Name = "panelEx1";
			this.panelEx1.Size = new System.Drawing.Size(1320, 24);
			this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelEx1.Style.GradientAngle = 90;
			this.panelEx1.TabIndex = 0;
			// 
			// lbl_CurrentWeight
			// 
			this.lbl_CurrentWeight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lbl_CurrentWeight.AutoSize = true;
			this.lbl_CurrentWeight.Font = new System.Drawing.Font("Segoe UI", 14.25F);
			this.lbl_CurrentWeight.ForeColor = System.Drawing.Color.White;
			this.lbl_CurrentWeight.Location = new System.Drawing.Point(1095, -1);
			this.lbl_CurrentWeight.Name = "lbl_CurrentWeight";
			this.lbl_CurrentWeight.Size = new System.Drawing.Size(22, 25);
			this.lbl_CurrentWeight.TabIndex = 226;
			this.lbl_CurrentWeight.Text = "0";
			// 
			// lblweight
			// 
			this.lblweight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblweight.AutoSize = true;
			this.lblweight.Font = new System.Drawing.Font("Segoe UI", 11.25F);
			this.lblweight.ForeColor = System.Drawing.Color.White;
			this.lblweight.Location = new System.Drawing.Point(962, 2);
			this.lblweight.Name = "lblweight";
			this.lblweight.Size = new System.Drawing.Size(153, 20);
			this.lblweight.TabIndex = 225;
			this.lblweight.Text = "电子天平当前重量：";
			// 
			// slightRwer
			// 
			this.slightRwer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.slightRwer.BackColor = System.Drawing.Color.Transparent;
			this.slightRwer.ForeColor = System.Drawing.Color.White;
			this.slightRwer.LightColor = System.Drawing.Color.Gray;
			this.slightRwer.Location = new System.Drawing.Point(1237, 1);
			this.slightRwer.Name = "slightRwer";
			this.slightRwer.Size = new System.Drawing.Size(20, 20);
			this.slightRwer.TabIndex = 221;
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.ForeColor = System.Drawing.Color.White;
			this.label1.Location = new System.Drawing.Point(1262, 1);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(57, 20);
			this.label1.TabIndex = 222;
			this.label1.Text = "读卡器";
			// 
			// lblCurrentFlowFlag
			// 
			this.lblCurrentFlowFlag.AutoSize = true;
			this.lblCurrentFlowFlag.Font = new System.Drawing.Font("Segoe UI", 11.25F);
			this.lblCurrentFlowFlag.ForeColor = System.Drawing.Color.White;
			this.lblCurrentFlowFlag.Location = new System.Drawing.Point(75, 0);
			this.lblCurrentFlowFlag.Name = "lblCurrentFlowFlag";
			this.lblCurrentFlowFlag.Size = new System.Drawing.Size(73, 20);
			this.lblCurrentFlowFlag.TabIndex = 210;
			this.lblCurrentFlowFlag.Text = "等待扫码";
			// 
			// lblWber
			// 
			this.lblWber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblWber.AutoSize = true;
			this.lblWber.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblWber.ForeColor = System.Drawing.Color.White;
			this.lblWber.Location = new System.Drawing.Point(1177, 1);
			this.lblWber.Name = "lblWber";
			this.lblWber.Size = new System.Drawing.Size(57, 20);
			this.lblWber.TabIndex = 222;
			this.lblWber.Text = "电子秤";
			// 
			// label10
			// 
			this.label10.AutoSize = true;
			this.label10.Font = new System.Drawing.Font("Segoe UI", 11.25F);
			this.label10.ForeColor = System.Drawing.Color.White;
			this.label10.Location = new System.Drawing.Point(-1, 0);
			this.label10.Name = "label10";
			this.label10.Size = new System.Drawing.Size(89, 20);
			this.label10.TabIndex = 209;
			this.label10.Text = "当前流程：";
			// 
			// rtxtMakeWeightInfo
			// 
			// 
			// 
			// 
			this.rtxtMakeWeightInfo.BackgroundStyle.Class = "RichTextBoxBorder";
			this.rtxtMakeWeightInfo.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.rtxtMakeWeightInfo.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rtxtMakeWeightInfo.Font = new System.Drawing.Font("微软雅黑", 10.5F);
			this.rtxtMakeWeightInfo.ForeColor = System.Drawing.Color.White;
			this.rtxtMakeWeightInfo.Location = new System.Drawing.Point(3, 437);
			this.rtxtMakeWeightInfo.Name = "rtxtMakeWeightInfo";
			this.rtxtMakeWeightInfo.ReadOnly = true;
			this.rtxtMakeWeightInfo.Size = new System.Drawing.Size(1320, 114);
			this.rtxtMakeWeightInfo.TabIndex = 213;
			// 
			// superGridControl1
			// 
			this.superGridControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
			this.superGridControl1.DefaultVisualStyles.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
			this.superGridControl1.DefaultVisualStyles.CellStyles.Default.Font = new System.Drawing.Font("Segoe UI", 11.25F);
			this.superGridControl1.DefaultVisualStyles.ColumnHeaderStyles.Default.Font = new System.Drawing.Font("Segoe UI", 11.25F);
			background1.Color1 = System.Drawing.Color.DarkTurquoise;
			this.superGridControl1.DefaultVisualStyles.RowStyles.Selected.Background = background1;
			this.superGridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.superGridControl1.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
			this.superGridControl1.Font = new System.Drawing.Font("Segoe UI", 8.25F);
			this.superGridControl1.ForeColor = System.Drawing.Color.White;
			this.superGridControl1.Location = new System.Drawing.Point(0, 135);
			this.superGridControl1.Margin = new System.Windows.Forms.Padding(0);
			this.superGridControl1.Name = "superGridControl1";
			this.superGridControl1.PrimaryGrid.AutoGenerateColumns = false;
			gridColumn1.DataPropertyName = "SampleType";
			gridColumn1.HeaderText = "样品类型";
			gridColumn1.Name = "";
			gridColumn1.Width = 170;
			gridColumn2.DataPropertyName = "BarrelCode";
			gridColumn2.HeaderText = "样品编码";
			gridColumn2.Name = "";
			gridColumn2.Width = 200;
			gridColumn3.DataPropertyName = "Weight";
			gridColumn3.HeaderText = "样重(g)";
			gridColumn3.Name = "";
			gridColumn3.Width = 80;
			gridColumn4.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridButtonXEditControl);
			gridColumn4.HeaderText = "";
			gridColumn4.Name = "gclmNewCode";
			gridColumn4.NullString = "生成编码";
			gridColumn4.RenderType = typeof(DevComponents.DotNetBar.SuperGrid.GridButtonXEditControl);
			gridColumn4.Visible = false;
			gridColumn4.Width = 80;
			gridColumn5.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridButtonXEditControl);
			gridColumn5.HeaderText = "";
			gridColumn5.Name = "gclmPrintCode";
			gridColumn5.NullString = "打印编码";
			gridColumn5.RenderType = typeof(DevComponents.DotNetBar.SuperGrid.GridButtonXEditControl);
			gridColumn5.Width = 80;
			gridColumn6.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridButtonXEditControl);
			gridColumn6.HeaderText = "";
			gridColumn6.Name = "gclmWriteCode";
			gridColumn6.NullString = "写入编码";
			gridColumn6.RenderType = typeof(DevComponents.DotNetBar.SuperGrid.GridButtonXEditControl);
			gridColumn6.Width = 80;
			this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn1);
			this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn2);
			this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn3);
			this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn4);
			this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn5);
			this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn6);
			this.superGridControl1.PrimaryGrid.EnterKeySelectsNextRow = false;
			this.superGridControl1.PrimaryGrid.InitialSelection = DevComponents.DotNetBar.SuperGrid.RelativeSelection.Row;
			this.superGridControl1.PrimaryGrid.MultiSelect = false;
			this.superGridControl1.PrimaryGrid.NoRowsText = "";
			this.superGridControl1.PrimaryGrid.SelectionGranularity = DevComponents.DotNetBar.SuperGrid.SelectionGranularity.Row;
			this.superGridControl1.Size = new System.Drawing.Size(1326, 299);
			this.superGridControl1.TabIndex = 223;
			this.superGridControl1.Text = "superGridControl1";
			this.superGridControl1.BeginEdit += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridEditEventArgs>(this.superGridControl1_BeginEdit);
			// 
			// panelEx3
			// 
			this.panelEx3.CanvasColor = System.Drawing.SystemColors.Control;
			this.panelEx3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.panelEx3.Controls.Add(this.btnReset);
			this.panelEx3.Controls.Add(this.panelAssayTargetType);
			this.panelEx3.Controls.Add(this.cmbAssayType);
			this.panelEx3.Controls.Add(this.panelAssayTarget);
			this.panelEx3.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelEx3.Location = new System.Drawing.Point(3, 33);
			this.panelEx3.Name = "panelEx3";
			this.panelEx3.Size = new System.Drawing.Size(1320, 54);
			this.panelEx3.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.panelEx3.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.panelEx3.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.panelEx3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelEx3.Style.GradientAngle = 90;
			this.panelEx3.TabIndex = 228;
			// 
			// panelAssayTargetType
			// 
			this.panelAssayTargetType.CanvasColor = System.Drawing.SystemColors.Control;
			this.panelAssayTargetType.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.panelAssayTargetType.Controls.Add(this.rbtnNormal);
			this.panelAssayTargetType.Controls.Add(this.rbtnAll);
			this.panelAssayTargetType.Controls.Add(this.rbtnHandChoose);
			this.panelAssayTargetType.Location = new System.Drawing.Point(196, 0);
			this.panelAssayTargetType.Name = "panelAssayTargetType";
			this.panelAssayTargetType.Size = new System.Drawing.Size(381, 25);
			this.panelAssayTargetType.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.panelAssayTargetType.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.panelAssayTargetType.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.panelAssayTargetType.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelAssayTargetType.Style.GradientAngle = 90;
			this.panelAssayTargetType.TabIndex = 243;
			// 
			// rbtnNormal
			// 
			this.rbtnNormal.AutoSize = true;
			this.rbtnNormal.Font = new System.Drawing.Font("Segoe UI", 11.25F);
			this.rbtnNormal.ForeColor = System.Drawing.Color.White;
			this.rbtnNormal.Location = new System.Drawing.Point(205, 1);
			this.rbtnNormal.Name = "rbtnNormal";
			this.rbtnNormal.Size = new System.Drawing.Size(91, 24);
			this.rbtnNormal.TabIndex = 245;
			this.rbtnNormal.TabStop = true;
			this.rbtnNormal.Tag = "日常分析";
			this.rbtnNormal.Text = "日常分析";
			this.rbtnNormal.UseVisualStyleBackColor = true;
			this.rbtnNormal.CheckedChanged += new System.EventHandler(this.rbtnHandChoose_CheckedChanged);
			// 
			// rbtnAll
			// 
			this.rbtnAll.AutoSize = true;
			this.rbtnAll.Font = new System.Drawing.Font("Segoe UI", 11.25F);
			this.rbtnAll.ForeColor = System.Drawing.Color.White;
			this.rbtnAll.Location = new System.Drawing.Point(112, 0);
			this.rbtnAll.Name = "rbtnAll";
			this.rbtnAll.Size = new System.Drawing.Size(75, 24);
			this.rbtnAll.TabIndex = 244;
			this.rbtnAll.TabStop = true;
			this.rbtnAll.Tag = "全指标";
			this.rbtnAll.Text = "全指标";
			this.rbtnAll.UseVisualStyleBackColor = true;
			this.rbtnAll.CheckedChanged += new System.EventHandler(this.rbtnHandChoose_CheckedChanged);
			// 
			// rbtnHandChoose
			// 
			this.rbtnHandChoose.AutoSize = true;
			this.rbtnHandChoose.Font = new System.Drawing.Font("Segoe UI", 11.25F);
			this.rbtnHandChoose.ForeColor = System.Drawing.Color.White;
			this.rbtnHandChoose.Location = new System.Drawing.Point(9, 1);
			this.rbtnHandChoose.Name = "rbtnHandChoose";
			this.rbtnHandChoose.Size = new System.Drawing.Size(91, 24);
			this.rbtnHandChoose.TabIndex = 243;
			this.rbtnHandChoose.TabStop = true;
			this.rbtnHandChoose.Tag = "手选指标";
			this.rbtnHandChoose.Text = "手选指标";
			this.rbtnHandChoose.UseVisualStyleBackColor = true;
			this.rbtnHandChoose.CheckedChanged += new System.EventHandler(this.rbtnHandChoose_CheckedChanged);
			// 
			// cmbAssayType
			// 
			this.cmbAssayType.DisplayMember = "Text";
			this.cmbAssayType.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.cmbAssayType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.cmbAssayType.Font = new System.Drawing.Font("Segoe UI", 18F);
			this.cmbAssayType.ForeColor = System.Drawing.Color.White;
			this.cmbAssayType.FormattingEnabled = true;
			this.cmbAssayType.ItemHeight = 33;
			this.cmbAssayType.Location = new System.Drawing.Point(0, 9);
			this.cmbAssayType.Name = "cmbAssayType";
			this.cmbAssayType.Size = new System.Drawing.Size(190, 39);
			this.cmbAssayType.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.cmbAssayType.TabIndex = 231;
			this.cmbAssayType.SelectedIndexChanged += new System.EventHandler(this.cmbAssayType_SelectedIndexChanged);
			// 
			// panelAssayTarget
			// 
			this.panelAssayTarget.CanvasColor = System.Drawing.SystemColors.Control;
			this.panelAssayTarget.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.panelAssayTarget.Controls.Add(this.checkBoxX4);
			this.panelAssayTarget.Controls.Add(this.checkBoxX8);
			this.panelAssayTarget.Controls.Add(this.checkBoxX1);
			this.panelAssayTarget.Controls.Add(this.checkBoxX7);
			this.panelAssayTarget.Controls.Add(this.checkBoxX6);
			this.panelAssayTarget.Controls.Add(this.checkBoxX5);
			this.panelAssayTarget.Controls.Add(this.checkBoxX3);
			this.panelAssayTarget.Controls.Add(this.checkBoxX2);
			this.panelAssayTarget.Location = new System.Drawing.Point(196, 28);
			this.panelAssayTarget.Name = "panelAssayTarget";
			this.panelAssayTarget.Size = new System.Drawing.Size(708, 28);
			this.panelAssayTarget.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.panelAssayTarget.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.panelAssayTarget.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.panelAssayTarget.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelAssayTarget.Style.GradientAngle = 90;
			this.panelAssayTarget.TabIndex = 233;
			this.panelAssayTarget.Visible = false;
			// 
			// checkBoxX4
			// 
			// 
			// 
			// 
			this.checkBoxX4.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.checkBoxX4.Font = new System.Drawing.Font("Segoe UI", 11.5F);
			this.checkBoxX4.ForeColor = System.Drawing.Color.White;
			this.checkBoxX4.Location = new System.Drawing.Point(548, 3);
			this.checkBoxX4.Name = "checkBoxX4";
			this.checkBoxX4.Size = new System.Drawing.Size(83, 23);
			this.checkBoxX4.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.checkBoxX4.TabIndex = 241;
			this.checkBoxX4.Text = "灰熔融";
			this.checkBoxX4.CheckedChanged += new System.EventHandler(this.assayTarget_CheckedChanged);
			// 
			// checkBoxX8
			// 
			// 
			// 
			// 
			this.checkBoxX8.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.checkBoxX8.Font = new System.Drawing.Font("Segoe UI", 11.5F);
			this.checkBoxX8.ForeColor = System.Drawing.Color.White;
			this.checkBoxX8.Location = new System.Drawing.Point(90, 3);
			this.checkBoxX8.Name = "checkBoxX8";
			this.checkBoxX8.Size = new System.Drawing.Size(114, 23);
			this.checkBoxX8.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.checkBoxX8.TabIndex = 240;
			this.checkBoxX8.Text = "空干基水分";
			this.checkBoxX8.CheckedChanged += new System.EventHandler(this.assayTarget_CheckedChanged);
			// 
			// checkBoxX1
			// 
			// 
			// 
			// 
			this.checkBoxX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.checkBoxX1.Font = new System.Drawing.Font("Segoe UI", 11.5F);
			this.checkBoxX1.ForeColor = System.Drawing.Color.White;
			this.checkBoxX1.Location = new System.Drawing.Point(9, 3);
			this.checkBoxX1.Name = "checkBoxX1";
			this.checkBoxX1.Size = new System.Drawing.Size(96, 23);
			this.checkBoxX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.checkBoxX1.TabIndex = 240;
			this.checkBoxX1.Text = "全水分";
			this.checkBoxX1.CheckedChanged += new System.EventHandler(this.assayTarget_CheckedChanged);
			// 
			// checkBoxX7
			// 
			// 
			// 
			// 
			this.checkBoxX7.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.checkBoxX7.Font = new System.Drawing.Font("Segoe UI", 11.5F);
			this.checkBoxX7.ForeColor = System.Drawing.Color.White;
			this.checkBoxX7.Location = new System.Drawing.Point(205, 3);
			this.checkBoxX7.Name = "checkBoxX7";
			this.checkBoxX7.Size = new System.Drawing.Size(65, 23);
			this.checkBoxX7.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.checkBoxX7.TabIndex = 237;
			this.checkBoxX7.Text = "灰分";
			this.checkBoxX7.CheckedChanged += new System.EventHandler(this.assayTarget_CheckedChanged);
			// 
			// checkBoxX6
			// 
			// 
			// 
			// 
			this.checkBoxX6.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.checkBoxX6.Font = new System.Drawing.Font("Segoe UI", 11.5F);
			this.checkBoxX6.ForeColor = System.Drawing.Color.White;
			this.checkBoxX6.Location = new System.Drawing.Point(481, 3);
			this.checkBoxX6.Name = "checkBoxX6";
			this.checkBoxX6.Size = new System.Drawing.Size(76, 23);
			this.checkBoxX6.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.checkBoxX6.TabIndex = 238;
			this.checkBoxX6.Text = "氢值";
			this.checkBoxX6.CheckedChanged += new System.EventHandler(this.assayTarget_CheckedChanged);
			// 
			// checkBoxX5
			// 
			// 
			// 
			// 
			this.checkBoxX5.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.checkBoxX5.Font = new System.Drawing.Font("Segoe UI", 11.5F);
			this.checkBoxX5.ForeColor = System.Drawing.Color.White;
			this.checkBoxX5.Location = new System.Drawing.Point(268, 3);
			this.checkBoxX5.Name = "checkBoxX5";
			this.checkBoxX5.Size = new System.Drawing.Size(74, 23);
			this.checkBoxX5.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.checkBoxX5.TabIndex = 239;
			this.checkBoxX5.Text = "挥发分";
			this.checkBoxX5.CheckedChanged += new System.EventHandler(this.assayTarget_CheckedChanged);
			// 
			// checkBoxX3
			// 
			// 
			// 
			// 
			this.checkBoxX3.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.checkBoxX3.Font = new System.Drawing.Font("Segoe UI", 11.5F);
			this.checkBoxX3.ForeColor = System.Drawing.Color.White;
			this.checkBoxX3.Location = new System.Drawing.Point(417, 3);
			this.checkBoxX3.Name = "checkBoxX3";
			this.checkBoxX3.Size = new System.Drawing.Size(80, 23);
			this.checkBoxX3.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.checkBoxX3.TabIndex = 233;
			this.checkBoxX3.Text = "热值";
			this.checkBoxX3.CheckedChanged += new System.EventHandler(this.assayTarget_CheckedChanged);
			// 
			// checkBoxX2
			// 
			// 
			// 
			// 
			this.checkBoxX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.checkBoxX2.Font = new System.Drawing.Font("Segoe UI", 11.5F);
			this.checkBoxX2.ForeColor = System.Drawing.Color.White;
			this.checkBoxX2.Location = new System.Drawing.Point(348, 3);
			this.checkBoxX2.Name = "checkBoxX2";
			this.checkBoxX2.Size = new System.Drawing.Size(80, 23);
			this.checkBoxX2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.checkBoxX2.TabIndex = 234;
			this.checkBoxX2.Text = "硫份";
			this.checkBoxX2.CheckedChanged += new System.EventHandler(this.assayTarget_CheckedChanged);
			// 
			// timer2
			// 
			this.timer2.Interval = 2000;
			// 
			// btnReset
			// 
			this.btnReset.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnReset.Font = new System.Drawing.Font("Segoe UI", 11.25F);
			this.btnReset.Location = new System.Drawing.Point(1204, 7);
			this.btnReset.Name = "btnReset";
			this.btnReset.Size = new System.Drawing.Size(107, 42);
			this.btnReset.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.btnReset.TabIndex = 244;
			this.btnReset.Text = "重 置";
			this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
			// 
			// FrmMakeWeight
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
			this.ClientSize = new System.Drawing.Size(1326, 554);
			this.Controls.Add(this.pnlExMain);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.Name = "FrmMakeWeight";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "制样明细";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMakeWeight_FormClosing);
			this.Load += new System.EventHandler(this.FrmMakeWeight_Load);
			this.pnlExMain.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.panelEx2.ResumeLayout(false);
			this.panelEx1.ResumeLayout(false);
			this.panelEx1.PerformLayout();
			this.panelEx3.ResumeLayout(false);
			this.panelAssayTargetType.ResumeLayout(false);
			this.panelAssayTargetType.PerformLayout();
			this.panelAssayTarget.ResumeLayout(false);
			this.ResumeLayout(false);

        }

        #endregion

        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.ToolTip toolTip1;
        private DevComponents.DotNetBar.PanelEx pnlExMain;
        private System.Windows.Forms.Timer timer2;
        private DevComponents.DotNetBar.Controls.RichTextBoxEx rtxtMakeWeightInfo;
        private System.Windows.Forms.Label lblCurrentFlowFlag;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblWber;
        private Forms.UserControls.UCtrlSignalLight slightWber;
        private DevComponents.DotNetBar.SuperGrid.SuperGridControl superGridControl1;
        private System.Windows.Forms.Label label1;
        private Forms.UserControls.UCtrlSignalLight slightRwer;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private System.Windows.Forms.Label lbl_CurrentWeight;
        private System.Windows.Forms.Label lblweight;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private DevComponents.DotNetBar.Controls.TextBoxX txtInputMakeCode;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbAssayType;
        private DevComponents.DotNetBar.PanelEx panelEx3;
        private DevComponents.DotNetBar.PanelEx panelAssayTarget;
        private DevComponents.DotNetBar.Controls.CheckBoxX checkBoxX7;
        private DevComponents.DotNetBar.Controls.CheckBoxX checkBoxX6;
        private DevComponents.DotNetBar.Controls.CheckBoxX checkBoxX5;
        private DevComponents.DotNetBar.Controls.CheckBoxX checkBoxX3;
        private DevComponents.DotNetBar.Controls.CheckBoxX checkBoxX2;
        private DevComponents.DotNetBar.Controls.CheckBoxX checkBoxX1;
        private DevComponents.DotNetBar.Controls.CheckBoxX checkBoxX8;
        private DevComponents.DotNetBar.Controls.CheckBoxX checkBoxX4;
        private DevComponents.DotNetBar.PanelEx panelAssayTargetType;
        private System.Windows.Forms.RadioButton rbtnHandChoose;
        private System.Windows.Forms.RadioButton rbtnAll;
        private System.Windows.Forms.RadioButton rbtnNormal;
		private DevComponents.DotNetBar.ButtonX btnReset;
	}
}

