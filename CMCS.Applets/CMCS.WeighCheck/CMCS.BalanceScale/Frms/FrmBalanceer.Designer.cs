namespace CMCS.BalanceScale.Frms
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmBalanceer));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.slightWber1 = new CMCS.Forms.UserControls.UCtrlSignalLight();
            this.slightWber2 = new CMCS.Forms.UserControls.UCtrlSignalLight();
            this.slightWber3 = new CMCS.Forms.UserControls.UCtrlSignalLight();
            this.slightWber4 = new CMCS.Forms.UserControls.UCtrlSignalLight();
            this.slightWber5 = new CMCS.Forms.UserControls.UCtrlSignalLight();
            this.slightWber6 = new CMCS.Forms.UserControls.UCtrlSignalLight();
            this.pnlExMain = new DevComponents.DotNetBar.PanelEx();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.rTxTMessageInfo = new DevComponents.DotNetBar.Controls.RichTextBoxEx();
            this.txtInputAssayCode = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.superGridControl1 = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
            this.flpanSamplerButton = new System.Windows.Forms.FlowLayoutPanel();
            this.lblWber = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.label1 = new System.Windows.Forms.Label();
            this.styleManager1 = new DevComponents.DotNetBar.StyleManager(this.components);
            this.pnlExMain.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.flpanSamplerButton.SuspendLayout();
            this.panelEx1.SuspendLayout();
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
            // slightWber1
            // 
            this.slightWber1.BackColor = System.Drawing.Color.Transparent;
            this.slightWber1.ForeColor = System.Drawing.Color.White;
            this.slightWber1.LightColor = System.Drawing.Color.Gray;
            this.slightWber1.Location = new System.Drawing.Point(3, 6);
            this.slightWber1.Margin = new System.Windows.Forms.Padding(3, 6, 3, 6);
            this.slightWber1.Name = "slightWber1";
            this.slightWber1.Size = new System.Drawing.Size(24, 24);
            this.slightWber1.TabIndex = 231;
            this.toolTip1.SetToolTip(this.slightWber1, "<绿色> 已连接\r\n<红色> 未连接");
            // 
            // slightWber2
            // 
            this.slightWber2.BackColor = System.Drawing.Color.Transparent;
            this.slightWber2.ForeColor = System.Drawing.Color.White;
            this.slightWber2.LightColor = System.Drawing.Color.Gray;
            this.slightWber2.Location = new System.Drawing.Point(120, 8);
            this.slightWber2.Margin = new System.Windows.Forms.Padding(3, 8, 3, 8);
            this.slightWber2.Name = "slightWber2";
            this.slightWber2.Size = new System.Drawing.Size(24, 24);
            this.slightWber2.TabIndex = 238;
            this.toolTip1.SetToolTip(this.slightWber2, "<绿色> 已连接\r\n<红色> 未连接");
            // 
            // slightWber3
            // 
            this.slightWber3.BackColor = System.Drawing.Color.Transparent;
            this.slightWber3.ForeColor = System.Drawing.Color.White;
            this.slightWber3.LightColor = System.Drawing.Color.Gray;
            this.slightWber3.Location = new System.Drawing.Point(237, 8);
            this.slightWber3.Margin = new System.Windows.Forms.Padding(3, 8, 3, 8);
            this.slightWber3.Name = "slightWber3";
            this.slightWber3.Size = new System.Drawing.Size(24, 24);
            this.slightWber3.TabIndex = 239;
            this.toolTip1.SetToolTip(this.slightWber3, "<绿色> 已连接\r\n<红色> 未连接");
            // 
            // slightWber4
            // 
            this.slightWber4.BackColor = System.Drawing.Color.Transparent;
            this.slightWber4.ForeColor = System.Drawing.Color.White;
            this.slightWber4.LightColor = System.Drawing.Color.Gray;
            this.slightWber4.Location = new System.Drawing.Point(354, 8);
            this.slightWber4.Margin = new System.Windows.Forms.Padding(3, 8, 3, 8);
            this.slightWber4.Name = "slightWber4";
            this.slightWber4.Size = new System.Drawing.Size(24, 24);
            this.slightWber4.TabIndex = 240;
            this.toolTip1.SetToolTip(this.slightWber4, "<绿色> 已连接\r\n<红色> 未连接");
            // 
            // slightWber5
            // 
            this.slightWber5.BackColor = System.Drawing.Color.Transparent;
            this.slightWber5.ForeColor = System.Drawing.Color.White;
            this.slightWber5.LightColor = System.Drawing.Color.Gray;
            this.slightWber5.Location = new System.Drawing.Point(471, 8);
            this.slightWber5.Margin = new System.Windows.Forms.Padding(3, 8, 3, 8);
            this.slightWber5.Name = "slightWber5";
            this.slightWber5.Size = new System.Drawing.Size(24, 24);
            this.slightWber5.TabIndex = 241;
            this.toolTip1.SetToolTip(this.slightWber5, "<绿色> 已连接\r\n<红色> 未连接");
            // 
            // slightWber6
            // 
            this.slightWber6.BackColor = System.Drawing.Color.Transparent;
            this.slightWber6.ForeColor = System.Drawing.Color.White;
            this.slightWber6.LightColor = System.Drawing.Color.Gray;
            this.slightWber6.Location = new System.Drawing.Point(588, 8);
            this.slightWber6.Margin = new System.Windows.Forms.Padding(3, 8, 3, 8);
            this.slightWber6.Name = "slightWber6";
            this.slightWber6.Size = new System.Drawing.Size(24, 24);
            this.slightWber6.TabIndex = 242;
            this.toolTip1.SetToolTip(this.slightWber6, "<绿色> 已连接\r\n<红色> 未连接");
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
            this.pnlExMain.Size = new System.Drawing.Size(1052, 546);
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
            this.tableLayoutPanel1.Controls.Add(this.rTxTMessageInfo, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.txtInputAssayCode, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.flpanSamplerButton, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panelEx1, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.ForeColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 45F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1052, 546);
            this.tableLayoutPanel1.TabIndex = 35;
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
            this.rTxTMessageInfo.Location = new System.Drawing.Point(3, 449);
            this.rTxTMessageInfo.Name = "rTxTMessageInfo";
            this.rTxTMessageInfo.Size = new System.Drawing.Size(1046, 94);
            this.rTxTMessageInfo.TabIndex = 0;
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
            this.txtInputAssayCode.Location = new System.Drawing.Point(3, 88);
            this.txtInputAssayCode.Name = "txtInputAssayCode";
            this.txtInputAssayCode.Size = new System.Drawing.Size(1046, 39);
            this.txtInputAssayCode.TabIndex = 34;
            this.txtInputAssayCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtInputAssayCode.WatermarkText = "请扫描编码. . .";
            this.txtInputAssayCode.TextChanged += new System.EventHandler(this.txtInputAssayCode_TextChanged);
            this.txtInputAssayCode.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtInputAssayCode_KeyUp);
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 1;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel2.Controls.Add(this.superGridControl1, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.ForeColor = System.Drawing.Color.White;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 128);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1046, 315);
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
            this.superGridControl1.PrimaryGrid.Caption.Text = "扫描记录";
            gridColumn1.DataPropertyName = "OperDate";
            gridColumn1.HeaderText = "操作时间";
            gridColumn1.Name = "";
            gridColumn1.Width = 180;
            gridColumn2.DataPropertyName = "OperUser";
            gridColumn2.HeaderText = "操作人";
            gridColumn2.Name = "";
            gridColumn3.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gridColumn3.DataPropertyName = "MachineCode";
            gridColumn3.HeaderText = "设备";
            gridColumn3.MinimumWidth = 110;
            gridColumn3.Name = "";
            gridColumn4.DataPropertyName = "AssayCode";
            gridColumn4.HeaderText = "化验码";
            gridColumn4.Name = "";
            gridColumn4.Width = 200;
            gridColumn5.DataPropertyName = "GGCode";
            gridColumn5.HeaderText = "坩埚号";
            gridColumn5.Name = "";
            gridColumn6.DataPropertyName = "Weight";
            gridColumn6.HeaderText = "重量";
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
            this.superGridControl1.Size = new System.Drawing.Size(1040, 309);
            this.superGridControl1.TabIndex = 29;
            this.superGridControl1.Text = "superGridControl1";
            this.superGridControl1.DataBindingComplete += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridDataBindingCompleteEventArgs>(this.superGridControl1_DataBindingComplete);
            this.superGridControl1.BeginEdit += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridEditEventArgs>(this.superGridControl1_BeginEdit);
            this.superGridControl1.GetRowHeaderText += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridGetRowHeaderTextEventArgs>(this.superGridControl_GetRowHeaderText);
            // 
            // flpanSamplerButton
            // 
            this.flpanSamplerButton.BackColor = System.Drawing.Color.Transparent;
            this.flpanSamplerButton.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flpanSamplerButton.Controls.Add(this.slightWber1);
            this.flpanSamplerButton.Controls.Add(this.lblWber);
            this.flpanSamplerButton.Controls.Add(this.slightWber2);
            this.flpanSamplerButton.Controls.Add(this.label2);
            this.flpanSamplerButton.Controls.Add(this.slightWber3);
            this.flpanSamplerButton.Controls.Add(this.label3);
            this.flpanSamplerButton.Controls.Add(this.slightWber4);
            this.flpanSamplerButton.Controls.Add(this.label4);
            this.flpanSamplerButton.Controls.Add(this.slightWber5);
            this.flpanSamplerButton.Controls.Add(this.label5);
            this.flpanSamplerButton.Controls.Add(this.slightWber6);
            this.flpanSamplerButton.Controls.Add(this.label6);
            this.flpanSamplerButton.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpanSamplerButton.ForeColor = System.Drawing.Color.White;
            this.flpanSamplerButton.Location = new System.Drawing.Point(3, 3);
            this.flpanSamplerButton.Name = "flpanSamplerButton";
            this.flpanSamplerButton.Size = new System.Drawing.Size(1046, 39);
            this.flpanSamplerButton.TabIndex = 30;
            // 
            // lblWber
            // 
            this.lblWber.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.lblWber.AutoSize = true;
            this.lblWber.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWber.ForeColor = System.Drawing.Color.White;
            this.lblWber.Location = new System.Drawing.Point(33, 10);
            this.lblWber.Name = "lblWber";
            this.lblWber.Size = new System.Drawing.Size(81, 20);
            this.lblWber.TabIndex = 232;
            this.lblWber.Text = "电子天平1";
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(150, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(81, 20);
            this.label2.TabIndex = 233;
            this.label2.Text = "电子天平2";
            // 
            // label3
            // 
            this.label3.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.White;
            this.label3.Location = new System.Drawing.Point(267, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(81, 20);
            this.label3.TabIndex = 234;
            this.label3.Text = "电子天平3";
            // 
            // label4
            // 
            this.label4.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(384, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(81, 20);
            this.label4.TabIndex = 235;
            this.label4.Text = "电子天平4";
            // 
            // label5
            // 
            this.label5.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(501, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(81, 20);
            this.label5.TabIndex = 236;
            this.label5.Text = "电子天平5";
            // 
            // label6
            // 
            this.label6.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(618, 10);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(81, 20);
            this.label6.TabIndex = 237;
            this.label6.Text = "电子天平6";
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.label1);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(3, 48);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(1046, 34);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 36;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(3, 8);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(455, 20);
            this.label1.TabIndex = 233;
            this.label1.Text = "操作流程：扫描天平编码>扫描化验编码>扫描坩埚号>输出重量";
            // 
            // styleManager1
            // 
            this.styleManager1.ManagerStyle = DevComponents.DotNetBar.eStyle.Metro;
            this.styleManager1.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51))))), System.Drawing.Color.DarkTurquoise);
            // 
            // FrmBalanceer
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1052, 546);
            this.Controls.Add(this.pnlExMain);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmBalanceer";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "天平称重";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.FrmUnloadSampler_Load);
            this.pnlExMain.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            this.flpanSamplerButton.ResumeLayout(false);
            this.flpanSamplerButton.PerformLayout();
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.ToolTip toolTip1;
        private DevComponents.DotNetBar.PanelEx pnlExMain;
        private System.Windows.Forms.FlowLayoutPanel flpanSamplerButton;
        private DevComponents.DotNetBar.Controls.RichTextBoxEx rTxTMessageInfo;
        private DevComponents.DotNetBar.SuperGrid.SuperGridControl superGridControl1;
        private DevComponents.DotNetBar.StyleManager styleManager1;
        private Forms.UserControls.UCtrlSignalLight slightWber1;
        private DevComponents.DotNetBar.Controls.TextBoxX txtInputAssayCode;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.Label label1;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private System.Windows.Forms.Label lblWber;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
        private Forms.UserControls.UCtrlSignalLight slightWber2;
        private Forms.UserControls.UCtrlSignalLight slightWber3;
        private Forms.UserControls.UCtrlSignalLight slightWber4;
        private Forms.UserControls.UCtrlSignalLight slightWber5;
        private Forms.UserControls.UCtrlSignalLight slightWber6;
    }
}