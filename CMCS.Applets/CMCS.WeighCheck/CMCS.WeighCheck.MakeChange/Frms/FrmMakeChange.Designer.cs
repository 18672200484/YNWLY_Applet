namespace CMCS.WeighCheck.MakeChange.Frms
{
    partial class FrmMakeChange
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
			DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn7 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
			DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn8 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
			DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn9 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
			DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn10 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
			DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn11 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
			DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn12 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
			DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn13 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
			DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn14 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
			DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn15 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
			DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn16 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
			DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn17 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
			System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMakeChange));
			this.printDocument1 = new System.Drawing.Printing.PrintDocument();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.slightRwer = new CMCS.Forms.UserControls.UCtrlSignalLight();
			this.slightWber = new CMCS.Forms.UserControls.UCtrlSignalLight();
			this.pnlExMain = new DevComponents.DotNetBar.PanelEx();
			this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
			this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
			this.label1 = new System.Windows.Forms.Label();
			this.lbl_CurrentWeight = new System.Windows.Forms.Label();
			this.lblweight = new System.Windows.Forms.Label();
			this.lblCurrentFlowFlag = new System.Windows.Forms.Label();
			this.lblWber = new System.Windows.Forms.Label();
			this.label14 = new System.Windows.Forms.Label();
			this.lblRwber = new System.Windows.Forms.Label();
			this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
			this.btnReset = new DevComponents.DotNetBar.ButtonX();
			this.btnReadRf = new DevComponents.DotNetBar.ButtonX();
			this.btnPrint = new DevComponents.DotNetBar.ButtonX();
			this.txtInputMakeCode = new DevComponents.DotNetBar.Controls.TextBoxX();
			this.txtInputAssayCode = new DevComponents.DotNetBar.Controls.TextBoxX();
			this.rtxtMakeWeightInfo = new DevComponents.DotNetBar.Controls.RichTextBoxEx();
			this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
			this.picEncode = new System.Windows.Forms.PictureBox();
			this.superGridControl1 = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
			this.timer2 = new System.Windows.Forms.Timer(this.components);
			this.timer1 = new System.Windows.Forms.Timer(this.components);
			this.btnTestPrint = new DevComponents.DotNetBar.ButtonX();
			this.pnlExMain.SuspendLayout();
			this.tableLayoutPanel1.SuspendLayout();
			this.panelEx1.SuspendLayout();
			this.panelEx2.SuspendLayout();
			this.tableLayoutPanel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.picEncode)).BeginInit();
			this.SuspendLayout();
			// 
			// slightRwer
			// 
			this.slightRwer.BackColor = System.Drawing.Color.Transparent;
			this.slightRwer.ForeColor = System.Drawing.Color.White;
			this.slightRwer.LightColor = System.Drawing.Color.Gray;
			this.slightRwer.Location = new System.Drawing.Point(3, 1);
			this.slightRwer.Name = "slightRwer";
			this.slightRwer.Size = new System.Drawing.Size(20, 20);
			this.slightRwer.TabIndex = 221;
			this.toolTip1.SetToolTip(this.slightRwer, "<绿色> 已连接\r\n<红色> 未连接");
			// 
			// slightWber
			// 
			this.slightWber.BackColor = System.Drawing.Color.Transparent;
			this.slightWber.ForeColor = System.Drawing.Color.White;
			this.slightWber.LightColor = System.Drawing.Color.Gray;
			this.slightWber.Location = new System.Drawing.Point(155, 2);
			this.slightWber.Name = "slightWber";
			this.slightWber.Size = new System.Drawing.Size(20, 20);
			this.slightWber.TabIndex = 229;
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
			this.pnlExMain.Size = new System.Drawing.Size(1501, 610);
			this.pnlExMain.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.pnlExMain.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(58)))), ((int)(((byte)(63)))));
			this.pnlExMain.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.pnlExMain.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.pnlExMain.Style.GradientAngle = 90;
			this.pnlExMain.TabIndex = 0;
			this.pnlExMain.Text = "制样转码";
			// 
			// tableLayoutPanel1
			// 
			this.tableLayoutPanel1.ColumnCount = 1;
			this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.Controls.Add(this.panelEx1, 0, 0);
			this.tableLayoutPanel1.Controls.Add(this.panelEx2, 0, 1);
			this.tableLayoutPanel1.Controls.Add(this.rtxtMakeWeightInfo, 0, 3);
			this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 2);
			this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel1.ForeColor = System.Drawing.Color.White;
			this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
			this.tableLayoutPanel1.Name = "tableLayoutPanel1";
			this.tableLayoutPanel1.RowCount = 4;
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 90F));
			this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
			this.tableLayoutPanel1.Size = new System.Drawing.Size(1501, 610);
			this.tableLayoutPanel1.TabIndex = 231;
			// 
			// panelEx1
			// 
			this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
			this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.panelEx1.Controls.Add(this.label1);
			this.panelEx1.Controls.Add(this.lbl_CurrentWeight);
			this.panelEx1.Controls.Add(this.lblweight);
			this.panelEx1.Controls.Add(this.slightRwer);
			this.panelEx1.Controls.Add(this.lblCurrentFlowFlag);
			this.panelEx1.Controls.Add(this.lblWber);
			this.panelEx1.Controls.Add(this.label14);
			this.panelEx1.Controls.Add(this.lblRwber);
			this.panelEx1.Controls.Add(this.slightWber);
			this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelEx1.Location = new System.Drawing.Point(3, 3);
			this.panelEx1.Name = "panelEx1";
			this.panelEx1.Size = new System.Drawing.Size(1495, 24);
			this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelEx1.Style.GradientAngle = 90;
			this.panelEx1.TabIndex = 0;
			// 
			// label1
			// 
			this.label1.Anchor = System.Windows.Forms.AnchorStyles.Left;
			this.label1.AutoSize = true;
			this.label1.Font = new System.Drawing.Font("Segoe UI", 11.25F);
			this.label1.ForeColor = System.Drawing.Color.White;
			this.label1.Location = new System.Drawing.Point(299, 3);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(1099, 20);
			this.label1.TabIndex = 233;
			this.label1.Text = "颜色说明：整行：蓝色:当前正在接样记录；绿色:已经核对的记录；质量情况：绿色:质量正常；红色:质量异常；打印时间、制样码:蓝色:今日以前的接样记录";
			// 
			// lbl_CurrentWeight
			// 
			this.lbl_CurrentWeight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lbl_CurrentWeight.AutoSize = true;
			this.lbl_CurrentWeight.Font = new System.Drawing.Font("Segoe UI", 14.25F);
			this.lbl_CurrentWeight.ForeColor = System.Drawing.Color.White;
			this.lbl_CurrentWeight.Location = new System.Drawing.Point(1299, 0);
			this.lbl_CurrentWeight.Name = "lbl_CurrentWeight";
			this.lbl_CurrentWeight.Size = new System.Drawing.Size(22, 25);
			this.lbl_CurrentWeight.TabIndex = 232;
			this.lbl_CurrentWeight.Text = "0";
			// 
			// lblweight
			// 
			this.lblweight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblweight.AutoSize = true;
			this.lblweight.Font = new System.Drawing.Font("Segoe UI", 11.25F);
			this.lblweight.ForeColor = System.Drawing.Color.White;
			this.lblweight.Location = new System.Drawing.Point(1157, 3);
			this.lblweight.Name = "lblweight";
			this.lblweight.Size = new System.Drawing.Size(153, 20);
			this.lblweight.TabIndex = 231;
			this.lblweight.Text = "电子天平当前重量：";
			// 
			// lblCurrentFlowFlag
			// 
			this.lblCurrentFlowFlag.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.lblCurrentFlowFlag.AutoSize = true;
			this.lblCurrentFlowFlag.BackColor = System.Drawing.Color.Transparent;
			this.lblCurrentFlowFlag.Font = new System.Drawing.Font("Segoe UI", 11.25F);
			this.lblCurrentFlowFlag.ForeColor = System.Drawing.Color.White;
			this.lblCurrentFlowFlag.Location = new System.Drawing.Point(1419, 4);
			this.lblCurrentFlowFlag.Name = "lblCurrentFlowFlag";
			this.lblCurrentFlowFlag.Size = new System.Drawing.Size(73, 20);
			this.lblCurrentFlowFlag.TabIndex = 228;
			this.lblCurrentFlowFlag.Text = "等待扫码";
			// 
			// lblWber
			// 
			this.lblWber.AutoSize = true;
			this.lblWber.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblWber.ForeColor = System.Drawing.Color.White;
			this.lblWber.Location = new System.Drawing.Point(181, 3);
			this.lblWber.Name = "lblWber";
			this.lblWber.Size = new System.Drawing.Size(121, 20);
			this.lblWber.TabIndex = 230;
			this.lblWber.Text = "电子秤连接状态";
			// 
			// label14
			// 
			this.label14.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label14.AutoSize = true;
			this.label14.BackColor = System.Drawing.Color.Transparent;
			this.label14.Font = new System.Drawing.Font("Segoe UI", 11.25F);
			this.label14.ForeColor = System.Drawing.Color.White;
			this.label14.Location = new System.Drawing.Point(1343, 4);
			this.label14.Name = "label14";
			this.label14.Size = new System.Drawing.Size(89, 20);
			this.label14.TabIndex = 227;
			this.label14.Text = "当前流程：";
			// 
			// lblRwber
			// 
			this.lblRwber.AutoSize = true;
			this.lblRwber.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.lblRwber.ForeColor = System.Drawing.Color.White;
			this.lblRwber.Location = new System.Drawing.Point(28, 2);
			this.lblRwber.Name = "lblRwber";
			this.lblRwber.Size = new System.Drawing.Size(121, 20);
			this.lblRwber.TabIndex = 222;
			this.lblRwber.Text = "读卡器连接状态";
			// 
			// panelEx2
			// 
			this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
			this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.panelEx2.Controls.Add(this.btnTestPrint);
			this.panelEx2.Controls.Add(this.btnReset);
			this.panelEx2.Controls.Add(this.btnReadRf);
			this.panelEx2.Controls.Add(this.btnPrint);
			this.panelEx2.Controls.Add(this.txtInputMakeCode);
			this.panelEx2.Controls.Add(this.txtInputAssayCode);
			this.panelEx2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panelEx2.Location = new System.Drawing.Point(3, 33);
			this.panelEx2.Name = "panelEx2";
			this.panelEx2.Size = new System.Drawing.Size(1495, 94);
			this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
			this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
			this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
			this.panelEx2.Style.GradientAngle = 90;
			this.panelEx2.TabIndex = 1;
			// 
			// btnReset
			// 
			this.btnReset.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnReset.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.btnReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
			this.btnReset.Location = new System.Drawing.Point(1123, 5);
			this.btnReset.Name = "btnReset";
			this.btnReset.Size = new System.Drawing.Size(96, 39);
			this.btnReset.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.btnReset.TabIndex = 224;
			this.btnReset.Text = "重   置";
			this.btnReset.Tooltip = "快捷键 Esc";
			this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
			// 
			// btnReadRf
			// 
			this.btnReadRf.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnReadRf.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.btnReadRf.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
			this.btnReadRf.Location = new System.Drawing.Point(1011, 5);
			this.btnReadRf.Name = "btnReadRf";
			this.btnReadRf.Size = new System.Drawing.Size(96, 39);
			this.btnReadRf.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.btnReadRf.TabIndex = 224;
			this.btnReadRf.Text = "读   卡";
			this.btnReadRf.Tooltip = "快捷键 F1";
			this.btnReadRf.Click += new System.EventHandler(this.btnReadRf_Click);
			// 
			// btnPrint
			// 
			this.btnPrint.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnPrint.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.btnPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
			this.btnPrint.Location = new System.Drawing.Point(1011, 54);
			this.btnPrint.Name = "btnPrint";
			this.btnPrint.Size = new System.Drawing.Size(96, 38);
			this.btnPrint.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.btnPrint.TabIndex = 226;
			this.btnPrint.Text = "打   印";
			this.btnPrint.Tooltip = "快捷键 F2";
			this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
			// 
			// txtInputMakeCode
			// 
			this.txtInputMakeCode.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.txtInputMakeCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
			// 
			// 
			// 
			this.txtInputMakeCode.Border.Class = "TextBoxBorder";
			this.txtInputMakeCode.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.txtInputMakeCode.ButtonCustom.Text = "清空";
			this.txtInputMakeCode.Font = new System.Drawing.Font("Segoe UI", 18F);
			this.txtInputMakeCode.ForeColor = System.Drawing.Color.White;
			this.txtInputMakeCode.Location = new System.Drawing.Point(282, 5);
			this.txtInputMakeCode.Name = "txtInputMakeCode";
			this.txtInputMakeCode.Size = new System.Drawing.Size(711, 39);
			this.txtInputMakeCode.TabIndex = 0;
			this.txtInputMakeCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtInputMakeCode.WatermarkText = "读取制样码/化验码. . .";
			this.txtInputMakeCode.TextChanged += new System.EventHandler(this.txtInputMakeCode_TextChanged);
			this.txtInputMakeCode.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtInputMakeCode_KeyUp);
			this.txtInputMakeCode.MouseMove += new System.Windows.Forms.MouseEventHandler(this.txtInputMakeCode_MouseMove);
			// 
			// txtInputAssayCode
			// 
			this.txtInputAssayCode.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.txtInputAssayCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
			// 
			// 
			// 
			this.txtInputAssayCode.Border.Class = "TextBoxBorder";
			this.txtInputAssayCode.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.txtInputAssayCode.ButtonCustom.Text = "清空";
			this.txtInputAssayCode.Font = new System.Drawing.Font("Segoe UI", 18F);
			this.txtInputAssayCode.ForeColor = System.Drawing.Color.White;
			this.txtInputAssayCode.Location = new System.Drawing.Point(282, 53);
			this.txtInputAssayCode.Name = "txtInputAssayCode";
			this.txtInputAssayCode.Size = new System.Drawing.Size(711, 39);
			this.txtInputAssayCode.TabIndex = 0;
			this.txtInputAssayCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txtInputAssayCode.WatermarkText = "对应化验码/制样码. . .";
			this.txtInputAssayCode.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtInputMakeCode_KeyUp);
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
			this.rtxtMakeWeightInfo.Location = new System.Drawing.Point(3, 523);
			this.rtxtMakeWeightInfo.Name = "rtxtMakeWeightInfo";
			this.rtxtMakeWeightInfo.ReadOnly = true;
			this.rtxtMakeWeightInfo.Size = new System.Drawing.Size(1495, 84);
			this.rtxtMakeWeightInfo.TabIndex = 213;
			// 
			// tableLayoutPanel2
			// 
			this.tableLayoutPanel2.ColumnCount = 2;
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 220F));
			this.tableLayoutPanel2.Controls.Add(this.picEncode, 1, 0);
			this.tableLayoutPanel2.Controls.Add(this.superGridControl1, 0, 0);
			this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.tableLayoutPanel2.ForeColor = System.Drawing.Color.White;
			this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 133);
			this.tableLayoutPanel2.Name = "tableLayoutPanel2";
			this.tableLayoutPanel2.RowCount = 1;
			this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
			this.tableLayoutPanel2.Size = new System.Drawing.Size(1495, 384);
			this.tableLayoutPanel2.TabIndex = 214;
			// 
			// picEncode
			// 
			this.picEncode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
			this.picEncode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.picEncode.ForeColor = System.Drawing.Color.White;
			this.picEncode.Location = new System.Drawing.Point(1278, 3);
			this.picEncode.Name = "picEncode";
			this.picEncode.Size = new System.Drawing.Size(200, 200);
			this.picEncode.TabIndex = 225;
			this.picEncode.TabStop = false;
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
			this.superGridControl1.Location = new System.Drawing.Point(0, 0);
			this.superGridControl1.Margin = new System.Windows.Forms.Padding(0);
			this.superGridControl1.Name = "superGridControl1";
			this.superGridControl1.PrimaryGrid.AutoGenerateColumns = false;
			gridColumn1.DataPropertyName = "BackBatchDate";
			gridColumn1.HeaderText = "归批时间";
			gridColumn1.Name = "";
			gridColumn1.Width = 200;
			gridColumn2.DataPropertyName = "AssayCode";
			gridColumn2.HeaderText = "化验码";
			gridColumn2.Name = "";
			gridColumn2.Width = 150;
			gridColumn3.DataPropertyName = "MakeCode_6mm";
			gridColumn3.HeaderText = "6mm制样码";
			gridColumn3.Name = "clmMakeCode_6mm";
			gridColumn3.Width = 150;
			gridColumn4.DataPropertyName = "Weight_6mm";
			gridColumn4.HeaderText = "样重(g)";
			gridColumn4.Name = "";
			gridColumn4.Visible = false;
			gridColumn4.Width = 80;
			gridColumn5.DataPropertyName = "";
			gridColumn5.HeaderText = "质量情况";
			gridColumn5.Name = "clmIsNormal_6mm";
			gridColumn5.NullString = "";
			gridColumn5.RenderType = typeof(DevComponents.DotNetBar.SuperGrid.GridLabelXEditControl);
			gridColumn6.DataPropertyName = "PrintTime_6mm";
			gridColumn6.HeaderText = "打印时间";
			gridColumn6.Name = "clmPrintTime_6mm";
			gridColumn6.Width = 200;
			gridColumn7.DataPropertyName = "MakeCode_2mm";
			gridColumn7.HeaderText = "0.2mm制样码";
			gridColumn7.Name = "clmMakeCode_2mm";
			gridColumn7.Width = 150;
			gridColumn8.DataPropertyName = "Weight_2mm";
			gridColumn8.HeaderText = "样重(g)";
			gridColumn8.Name = "";
			gridColumn8.Visible = false;
			gridColumn8.Width = 80;
			gridColumn9.HeaderText = "质量情况";
			gridColumn9.Name = "clmIsNormal_2mm";
			gridColumn9.RenderType = typeof(DevComponents.DotNetBar.SuperGrid.GridLabelXEditControl);
			gridColumn10.DataPropertyName = "PrintTime_2mm";
			gridColumn10.HeaderText = "打印时间";
			gridColumn10.Name = "clmPrintTime_2mm";
			gridColumn10.Width = 200;
			gridColumn11.DataPropertyName = "PrintCount";
			gridColumn11.HeaderText = "打印次数";
			gridColumn11.Name = "";
			gridColumn11.Width = 80;
			gridColumn12.DataPropertyName = "CheckUser";
			gridColumn12.HeaderText = "操作人";
			gridColumn12.Name = "";
			gridColumn12.Width = 200;
			gridColumn13.DataPropertyName = "CheckType";
			gridColumn13.HeaderText = "类型";
			gridColumn13.Name = "";
			gridColumn13.Width = 80;
			gridColumn14.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridButtonXEditControl);
			gridColumn14.HeaderText = "";
			gridColumn14.Name = "gclmNewCode";
			gridColumn14.NullString = "生成编码";
			gridColumn14.RenderType = typeof(DevComponents.DotNetBar.SuperGrid.GridButtonXEditControl);
			gridColumn14.Visible = false;
			gridColumn14.Width = 80;
			gridColumn15.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridButtonXEditControl);
			gridColumn15.HeaderText = "";
			gridColumn15.Name = "gclmPrintCode";
			gridColumn15.NullString = "打印编码";
			gridColumn15.RenderType = typeof(DevComponents.DotNetBar.SuperGrid.GridButtonXEditControl);
			gridColumn15.Visible = false;
			gridColumn15.Width = 80;
			gridColumn16.DataPropertyName = "IsCheck";
			gridColumn16.HeaderText = "是否核对";
			gridColumn16.Name = "clmIsCheck";
			gridColumn16.RenderType = typeof(DevComponents.DotNetBar.SuperGrid.GridCheckBoxXEditControl);
			gridColumn17.DataPropertyName = "AssayPoint";
			gridColumn17.HeaderText = "测试项目";
			gridColumn17.Name = "clmAssayPoint";
			gridColumn17.Width = 200;
			this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn1);
			this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn2);
			this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn3);
			this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn4);
			this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn5);
			this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn6);
			this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn7);
			this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn8);
			this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn9);
			this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn10);
			this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn11);
			this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn12);
			this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn13);
			this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn14);
			this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn15);
			this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn16);
			this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn17);
			this.superGridControl1.PrimaryGrid.EnterKeySelectsNextRow = false;
			this.superGridControl1.PrimaryGrid.InitialSelection = DevComponents.DotNetBar.SuperGrid.RelativeSelection.Row;
			this.superGridControl1.PrimaryGrid.MultiSelect = false;
			this.superGridControl1.PrimaryGrid.NoRowsText = "";
			this.superGridControl1.PrimaryGrid.SelectionGranularity = DevComponents.DotNetBar.SuperGrid.SelectionGranularity.Row;
			this.superGridControl1.Size = new System.Drawing.Size(1275, 384);
			this.superGridControl1.TabIndex = 223;
			this.superGridControl1.Text = "superGridControl1";
			this.superGridControl1.CellMouseDown += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridCellMouseEventArgs>(this.superGridControl1_CellMouseDown);
			this.superGridControl1.DataBindingComplete += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridDataBindingCompleteEventArgs>(this.superGridControl1_DataBindingComplete);
			this.superGridControl1.BeginEdit += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridEditEventArgs>(this.superGridControl1_BeginEdit);
			this.superGridControl1.GetRowHeaderText += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridGetRowHeaderTextEventArgs>(this.superGridControl_GetRowHeaderText);
			// 
			// timer2
			// 
			this.timer2.Enabled = true;
			this.timer2.Interval = 2000;
			this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
			// 
			// timer1
			// 
			this.timer1.Interval = 2000;
			this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
			// 
			// btnTestPrint
			// 
			this.btnTestPrint.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnTestPrint.Anchor = System.Windows.Forms.AnchorStyles.Top;
			this.btnTestPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
			this.btnTestPrint.Location = new System.Drawing.Point(1123, 54);
			this.btnTestPrint.Name = "btnTestPrint";
			this.btnTestPrint.Size = new System.Drawing.Size(96, 38);
			this.btnTestPrint.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.btnTestPrint.TabIndex = 227;
			this.btnTestPrint.Text = "测试打印";
			this.btnTestPrint.Tooltip = "只打印化验码 不进行记录";
			this.btnTestPrint.Click += new System.EventHandler(this.btnTestPrint_Click);
			// 
			// FrmMakeChange
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
			this.ClientSize = new System.Drawing.Size(1501, 610);
			this.Controls.Add(this.pnlExMain);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
			this.ForeColor = System.Drawing.Color.White;
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
			this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
			this.KeyPreview = true;
			this.MinimumSize = new System.Drawing.Size(938, 443);
			this.Name = "FrmMakeChange";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "化验转码";
			this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMakeWeight_FormClosing);
			this.Load += new System.EventHandler(this.FrmMakeWeight_Load);
			this.Paint += new System.Windows.Forms.PaintEventHandler(this.FrmMakeChange_Paint);
			this.pnlExMain.ResumeLayout(false);
			this.tableLayoutPanel1.ResumeLayout(false);
			this.panelEx1.ResumeLayout(false);
			this.panelEx1.PerformLayout();
			this.panelEx2.ResumeLayout(false);
			this.tableLayoutPanel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.picEncode)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.ToolTip toolTip1;
        private DevComponents.DotNetBar.PanelEx pnlExMain;
        private System.Windows.Forms.Timer timer2;
        private DevComponents.DotNetBar.Controls.RichTextBoxEx rtxtMakeWeightInfo;
        private DevComponents.DotNetBar.Controls.TextBoxX txtInputMakeCode;
        private System.Windows.Forms.Label lblRwber;
        private Forms.UserControls.UCtrlSignalLight slightRwer;
        private DevComponents.DotNetBar.SuperGrid.SuperGridControl superGridControl1;
        private DevComponents.DotNetBar.ButtonX btnReadRf;
        private DevComponents.DotNetBar.Controls.TextBoxX txtInputAssayCode;
        private System.Windows.Forms.PictureBox picEncode;
        private DevComponents.DotNetBar.ButtonX btnPrint;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label lblCurrentFlowFlag;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label lblWber;
        private Forms.UserControls.UCtrlSignalLight slightWber;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private DevComponents.DotNetBar.ButtonX btnReset;
        private System.Windows.Forms.Label lbl_CurrentWeight;
        private System.Windows.Forms.Label lblweight;
        private System.Windows.Forms.Label label1;
		private DevComponents.DotNetBar.ButtonX btnTestPrint;
	}
}

