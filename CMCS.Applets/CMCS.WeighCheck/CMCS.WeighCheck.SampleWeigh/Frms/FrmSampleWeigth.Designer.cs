namespace CMCS.WeighCheck.SampleWeigh.Frms.SampleWeigth
{
    partial class FrmSampleWeigth
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSampleWeigth));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.slightWber = new CMCS.Forms.UserControls.UCtrlSignalLight();
            this.pnlExMain = new DevComponents.DotNetBar.PanelEx();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panelEx4 = new DevComponents.DotNetBar.PanelEx();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.label10 = new System.Windows.Forms.Label();
            this.txtMineName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.label9 = new System.Windows.Forms.Label();
            this.btnSaveSampleBarrel = new DevComponents.DotNetBar.ButtonX();
            this.btnPrint = new DevComponents.DotNetBar.ButtonX();
            this.btnReset = new DevComponents.DotNetBar.ButtonX();
            this.label8 = new System.Windows.Forms.Label();
            this.btnAdd = new DevComponents.DotNetBar.ButtonX();
            this.label7 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.btnSelSampleInfo = new DevComponents.DotNetBar.ButtonX();
            this.txtBatch = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtSampleCode = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtSupplierName = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.txtSampleType = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.superGridControl1 = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
            this.lblWber = new System.Windows.Forms.Label();
            this.rtxtOutputInfo = new DevComponents.DotNetBar.Controls.RichTextBoxEx();
            this.lblCurrentFlowFlag = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.pnlExMain.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panelEx1.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 2000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // slightWber
            // 
            this.slightWber.BackColor = System.Drawing.Color.Transparent;
            this.slightWber.ForeColor = System.Drawing.Color.White;
            this.slightWber.LightColor = System.Drawing.Color.Gray;
            this.slightWber.Location = new System.Drawing.Point(701, 9);
            this.slightWber.Name = "slightWber";
            this.slightWber.Size = new System.Drawing.Size(20, 20);
            this.slightWber.TabIndex = 211;
            this.toolTip1.SetToolTip(this.slightWber, "<绿色> 已连接\r\n<红色> 未连接");
            // 
            // pnlExMain
            // 
            this.pnlExMain.CanvasColor = System.Drawing.SystemColors.Control;
            this.pnlExMain.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.pnlExMain.Controls.Add(this.tableLayoutPanel1);
            this.pnlExMain.Controls.Add(this.lblWber);
            this.pnlExMain.Controls.Add(this.slightWber);
            this.pnlExMain.Controls.Add(this.rtxtOutputInfo);
            this.pnlExMain.Controls.Add(this.lblCurrentFlowFlag);
            this.pnlExMain.Controls.Add(this.label2);
            this.pnlExMain.Controls.Add(this.label1);
            this.pnlExMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlExMain.Location = new System.Drawing.Point(0, 0);
            this.pnlExMain.Name = "pnlExMain";
            this.pnlExMain.Size = new System.Drawing.Size(825, 455);
            this.pnlExMain.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.pnlExMain.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(58)))), ((int)(((byte)(63)))));
            this.pnlExMain.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.pnlExMain.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.pnlExMain.Style.GradientAngle = 90;
            this.pnlExMain.TabIndex = 0;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 300F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 4F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panelEx4, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.panelEx1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panelEx2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.superGridControl1, 2, 1);
            this.tableLayoutPanel1.ForeColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(13, 35);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(800, 286);
            this.tableLayoutPanel1.TabIndex = 215;
            // 
            // panelEx4
            // 
            this.panelEx4.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx4.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelEx4.Location = new System.Drawing.Point(304, 0);
            this.panelEx4.Margin = new System.Windows.Forms.Padding(0);
            this.panelEx4.Name = "panelEx4";
            this.panelEx4.Size = new System.Drawing.Size(496, 30);
            this.panelEx4.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx4.Style.BackColor1.Color = System.Drawing.Color.DarkTurquoise;
            this.panelEx4.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx4.Style.BorderColor.Color = System.Drawing.Color.Gray;
            this.panelEx4.Style.CornerType = DevComponents.DotNetBar.eCornerType.Diagonal;
            this.panelEx4.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx4.Style.GradientAngle = 90;
            this.panelEx4.TabIndex = 0;
            this.panelEx4.Text = "采样桶列表";
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.label10);
            this.panelEx1.Controls.Add(this.txtMineName);
            this.panelEx1.Controls.Add(this.label9);
            this.panelEx1.Controls.Add(this.btnSaveSampleBarrel);
            this.panelEx1.Controls.Add(this.btnPrint);
            this.panelEx1.Controls.Add(this.btnReset);
            this.panelEx1.Controls.Add(this.label8);
            this.panelEx1.Controls.Add(this.btnAdd);
            this.panelEx1.Controls.Add(this.label7);
            this.panelEx1.Controls.Add(this.label6);
            this.panelEx1.Controls.Add(this.btnSelSampleInfo);
            this.panelEx1.Controls.Add(this.txtBatch);
            this.panelEx1.Controls.Add(this.txtSampleCode);
            this.panelEx1.Controls.Add(this.txtSupplierName);
            this.panelEx1.Controls.Add(this.txtSampleType);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 30);
            this.panelEx1.Margin = new System.Windows.Forms.Padding(0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(300, 256);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.Color = System.Drawing.Color.Gray;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 213;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.label10.ForeColor = System.Drawing.Color.White;
            this.label10.Location = new System.Drawing.Point(40, 153);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(41, 20);
            this.label10.TabIndex = 59;
            this.label10.Text = "矿点";
            // 
            // txtMineName
            // 
            this.txtMineName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
            // 
            // 
            // 
            this.txtMineName.Border.Class = "TextBoxBorder";
            this.txtMineName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtMineName.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMineName.ForeColor = System.Drawing.Color.White;
            this.txtMineName.Location = new System.Drawing.Point(85, 150);
            this.txtMineName.Name = "txtMineName";
            this.txtMineName.ReadOnly = true;
            this.txtMineName.Size = new System.Drawing.Size(199, 27);
            this.txtMineName.TabIndex = 58;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(10, 54);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(73, 20);
            this.label9.TabIndex = 57;
            this.label9.Text = "采样方式";
            // 
            // btnSaveSampleBarrel
            // 
            this.btnSaveSampleBarrel.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSaveSampleBarrel.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnSaveSampleBarrel.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.btnSaveSampleBarrel.Location = new System.Drawing.Point(108, 224);
            this.btnSaveSampleBarrel.Name = "btnSaveSampleBarrel";
            this.btnSaveSampleBarrel.Size = new System.Drawing.Size(61, 23);
            this.btnSaveSampleBarrel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSaveSampleBarrel.TabIndex = 209;
            this.btnSaveSampleBarrel.Text = "保 存";
            this.btnSaveSampleBarrel.Visible = false;
            this.btnSaveSampleBarrel.Click += new System.EventHandler(this.btnSaveSampleBarrel_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnPrint.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnPrint.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.btnPrint.Location = new System.Drawing.Point(202, 195);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(61, 23);
            this.btnPrint.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnPrint.TabIndex = 210;
            this.btnPrint.Text = "打 印";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btnReset
            // 
            this.btnReset.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnReset.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnReset.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.btnReset.Location = new System.Drawing.Point(14, 224);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(61, 23);
            this.btnReset.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnReset.TabIndex = 210;
            this.btnReset.Text = "重 置";
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(10, 120);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(73, 20);
            this.label8.TabIndex = 56;
            this.label8.Text = "供货单位";
            // 
            // btnAdd
            // 
            this.btnAdd.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnAdd.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnAdd.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.btnAdd.Location = new System.Drawing.Point(108, 195);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(88, 23);
            this.btnAdd.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnAdd.TabIndex = 53;
            this.btnAdd.Text = "新增采样桶";
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.label7.ForeColor = System.Drawing.Color.White;
            this.label7.Location = new System.Drawing.Point(25, 20);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(57, 20);
            this.label7.TabIndex = 52;
            this.label7.Text = "采样码";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(10, 86);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(73, 20);
            this.label6.TabIndex = 50;
            this.label6.Text = "批次编码";
            // 
            // btnSelSampleInfo
            // 
            this.btnSelSampleInfo.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSelSampleInfo.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnSelSampleInfo.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.btnSelSampleInfo.Location = new System.Drawing.Point(14, 195);
            this.btnSelSampleInfo.Name = "btnSelSampleInfo";
            this.btnSelSampleInfo.Size = new System.Drawing.Size(88, 23);
            this.btnSelSampleInfo.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSelSampleInfo.TabIndex = 53;
            this.btnSelSampleInfo.Text = "选择采样单";
            this.btnSelSampleInfo.Click += new System.EventHandler(this.btnSelSampleInfo_Click);
            // 
            // txtBatch
            // 
            this.txtBatch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
            // 
            // 
            // 
            this.txtBatch.Border.Class = "TextBoxBorder";
            this.txtBatch.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtBatch.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtBatch.ForeColor = System.Drawing.Color.White;
            this.txtBatch.Location = new System.Drawing.Point(85, 84);
            this.txtBatch.Name = "txtBatch";
            this.txtBatch.ReadOnly = true;
            this.txtBatch.Size = new System.Drawing.Size(199, 27);
            this.txtBatch.TabIndex = 51;
            // 
            // txtSampleCode
            // 
            this.txtSampleCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
            // 
            // 
            // 
            this.txtSampleCode.Border.Class = "TextBoxBorder";
            this.txtSampleCode.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtSampleCode.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSampleCode.ForeColor = System.Drawing.Color.White;
            this.txtSampleCode.Location = new System.Drawing.Point(85, 18);
            this.txtSampleCode.Name = "txtSampleCode";
            this.txtSampleCode.ReadOnly = true;
            this.txtSampleCode.Size = new System.Drawing.Size(199, 27);
            this.txtSampleCode.TabIndex = 55;
            // 
            // txtSupplierName
            // 
            this.txtSupplierName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
            // 
            // 
            // 
            this.txtSupplierName.Border.Class = "TextBoxBorder";
            this.txtSupplierName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtSupplierName.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSupplierName.ForeColor = System.Drawing.Color.White;
            this.txtSupplierName.Location = new System.Drawing.Point(85, 117);
            this.txtSupplierName.Name = "txtSupplierName";
            this.txtSupplierName.ReadOnly = true;
            this.txtSupplierName.Size = new System.Drawing.Size(199, 27);
            this.txtSupplierName.TabIndex = 52;
            // 
            // txtSampleType
            // 
            this.txtSampleType.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
            // 
            // 
            // 
            this.txtSampleType.Border.Class = "TextBoxBorder";
            this.txtSampleType.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtSampleType.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSampleType.ForeColor = System.Drawing.Color.White;
            this.txtSampleType.Location = new System.Drawing.Point(85, 51);
            this.txtSampleType.Name = "txtSampleType";
            this.txtSampleType.ReadOnly = true;
            this.txtSampleType.Size = new System.Drawing.Size(199, 27);
            this.txtSampleType.TabIndex = 56;
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx2.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panelEx2.Location = new System.Drawing.Point(0, 0);
            this.panelEx2.Margin = new System.Windows.Forms.Padding(0);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(300, 30);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.Color = System.Drawing.Color.DarkTurquoise;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.BorderColor.Color = System.Drawing.Color.Gray;
            this.panelEx2.Style.CornerType = DevComponents.DotNetBar.eCornerType.Diagonal;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 1;
            this.panelEx2.Text = "采样单";
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
            this.superGridControl1.Location = new System.Drawing.Point(304, 30);
            this.superGridControl1.Margin = new System.Windows.Forms.Padding(0);
            this.superGridControl1.Name = "superGridControl1";
            this.superGridControl1.PrimaryGrid.AutoGenerateColumns = false;
            gridColumn1.DataPropertyName = "SampseCondCode";
            gridColumn1.HeaderText = "采样次码";
            gridColumn1.Name = "";
            gridColumn1.Width = 160;
            gridColumn2.DataPropertyName = "BarrellingTime";
            gridColumn2.HeaderText = "装桶时间";
            gridColumn2.Name = "";
            gridColumn2.Width = 150;
            gridColumn3.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridButtonXEditControl);
            gridColumn3.HeaderText = "";
            gridColumn3.Name = "gclmPrint";
            gridColumn3.NullString = "打印编码";
            gridColumn3.RenderType = typeof(DevComponents.DotNetBar.SuperGrid.GridButtonXEditControl);
            gridColumn3.Width = 80;
            gridColumn4.DataPropertyName = "";
            gridColumn4.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridButtonXEditControl);
            gridColumn4.HeaderText = "";
            gridColumn4.Name = "gclmDel";
            gridColumn4.NullString = "删除";
            gridColumn4.RenderType = typeof(DevComponents.DotNetBar.SuperGrid.GridButtonXEditControl);
            gridColumn4.Width = 80;
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn1);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn2);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn3);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn4);
            this.superGridControl1.PrimaryGrid.EnterKeySelectsNextRow = false;
            this.superGridControl1.PrimaryGrid.InitialSelection = DevComponents.DotNetBar.SuperGrid.RelativeSelection.Row;
            this.superGridControl1.PrimaryGrid.MultiSelect = false;
            this.superGridControl1.PrimaryGrid.NoRowsText = "";
            this.superGridControl1.PrimaryGrid.SelectionGranularity = DevComponents.DotNetBar.SuperGrid.SelectionGranularity.Row;
            this.superGridControl1.Size = new System.Drawing.Size(496, 256);
            this.superGridControl1.TabIndex = 208;
            this.superGridControl1.Text = "superGridControl1";
            this.superGridControl1.BeginEdit += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridEditEventArgs>(this.superGridControl1_BeginEdit);
            this.superGridControl1.GetRowHeaderText += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridGetRowHeaderTextEventArgs>(this.superGridControl1_GetRowHeaderText);
            // 
            // lblWber
            // 
            this.lblWber.AutoSize = true;
            this.lblWber.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWber.ForeColor = System.Drawing.Color.White;
            this.lblWber.Location = new System.Drawing.Point(726, 9);
            this.lblWber.Name = "lblWber";
            this.lblWber.Size = new System.Drawing.Size(57, 20);
            this.lblWber.TabIndex = 212;
            this.lblWber.Text = "电子秤";
            // 
            // rtxtOutputInfo
            // 
            // 
            // 
            // 
            this.rtxtOutputInfo.BackgroundStyle.Class = "RichTextBoxBorder";
            this.rtxtOutputInfo.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.rtxtOutputInfo.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.rtxtOutputInfo.ForeColor = System.Drawing.Color.White;
            this.rtxtOutputInfo.Location = new System.Drawing.Point(12, 327);
            this.rtxtOutputInfo.Name = "rtxtOutputInfo";
            this.rtxtOutputInfo.ReadOnly = true;
            this.rtxtOutputInfo.Size = new System.Drawing.Size(801, 91);
            this.rtxtOutputInfo.TabIndex = 57;
            // 
            // lblCurrentFlowFlag
            // 
            this.lblCurrentFlowFlag.AutoSize = true;
            this.lblCurrentFlowFlag.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.lblCurrentFlowFlag.ForeColor = System.Drawing.Color.White;
            this.lblCurrentFlowFlag.Location = new System.Drawing.Point(87, 9);
            this.lblCurrentFlowFlag.Name = "lblCurrentFlowFlag";
            this.lblCurrentFlowFlag.Size = new System.Drawing.Size(89, 20);
            this.lblCurrentFlowFlag.TabIndex = 50;
            this.lblCurrentFlowFlag.Text = "选择采样单";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(12, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 20);
            this.label2.TabIndex = 49;
            this.label2.Text = "当前流程：";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(12, 426);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(360, 20);
            this.label1.TabIndex = 47;
            this.label1.Text = "操作说明 : 选择采样单 >>新增采样桶 >> 打印编码";
            // 
            // FrmSampleWeigth
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
            this.ClientSize = new System.Drawing.Size(825, 455);
            this.Controls.Add(this.pnlExMain);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "FrmSampleWeigth";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "样品称重校验程序";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmSampleWeigth_FormClosing);
            this.Load += new System.EventHandler(this.FrmSampleWeigth_Load);
            this.pnlExMain.ResumeLayout(false);
            this.pnlExMain.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.ToolTip toolTip1;
        private DevComponents.DotNetBar.PanelEx pnlExMain;
        private DevComponents.DotNetBar.Controls.RichTextBoxEx rtxtOutputInfo;
        private DevComponents.DotNetBar.Controls.TextBoxX txtSampleType;
        private DevComponents.DotNetBar.Controls.TextBoxX txtSampleCode;
        private DevComponents.DotNetBar.ButtonX btnSelSampleInfo;
        private DevComponents.DotNetBar.Controls.TextBoxX txtSupplierName;
        private DevComponents.DotNetBar.Controls.TextBoxX txtBatch;
        private System.Windows.Forms.Label lblCurrentFlowFlag;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private DevComponents.DotNetBar.SuperGrid.SuperGridControl superGridControl1;
        private DevComponents.DotNetBar.ButtonX btnSaveSampleBarrel;
        private DevComponents.DotNetBar.ButtonX btnReset;
        private System.Windows.Forms.Label lblWber;
        private Forms.UserControls.UCtrlSignalLight slightWber;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label6;
        private DevComponents.DotNetBar.PanelEx panelEx4;
        private System.Windows.Forms.Label label10;
        private DevComponents.DotNetBar.Controls.TextBoxX txtMineName;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private DevComponents.DotNetBar.ButtonX btnPrint;
        private DevComponents.DotNetBar.ButtonX btnAdd;
    }
}

