namespace CMCS.WeighCheck.SampleMake.Frms.Sample
{
    partial class FrmSampleCheck
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSampleCheck));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.slightWber = new CMCS.Forms.UserControls.UCtrlSignalLight();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.pnlExMain = new DevComponents.DotNetBar.PanelEx();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.rtxtOutputInfo = new DevComponents.DotNetBar.Controls.RichTextBoxEx();
            this.panSampleBarrels = new DevComponents.DotNetBar.PanelEx();
            this.buttonX3 = new DevComponents.DotNetBar.ButtonX();
            this.buttonX4 = new DevComponents.DotNetBar.ButtonX();
            this.buttonX8 = new DevComponents.DotNetBar.ButtonX();
            this.buttonX5 = new DevComponents.DotNetBar.ButtonX();
            this.buttonX9 = new DevComponents.DotNetBar.ButtonX();
            this.buttonX6 = new DevComponents.DotNetBar.ButtonX();
            this.buttonX10 = new DevComponents.DotNetBar.ButtonX();
            this.buttonX7 = new DevComponents.DotNetBar.ButtonX();
            this.buttonX11 = new DevComponents.DotNetBar.ButtonX();
            this.buttonX12 = new DevComponents.DotNetBar.ButtonX();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.lbl_CurrentWeight = new System.Windows.Forms.Label();
            this.lblweight = new System.Windows.Forms.Label();
            this.lblWber = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblCurrentFlowFlag = new System.Windows.Forms.Label();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.btnReset = new DevComponents.DotNetBar.ButtonX();
            this.buttonX13 = new DevComponents.DotNetBar.ButtonX();
            this.label13 = new System.Windows.Forms.Label();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.buttonX2 = new DevComponents.DotNetBar.ButtonX();
            this.txtInputSampleCode = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.pnlExMain.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panSampleBarrels.SuspendLayout();
            this.panelEx1.SuspendLayout();
            this.panelEx2.SuspendLayout();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Interval = 2000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // slightWber
            // 
            this.slightWber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.slightWber.BackColor = System.Drawing.Color.Transparent;
            this.slightWber.ForeColor = System.Drawing.Color.White;
            this.slightWber.LightColor = System.Drawing.Color.Gray;
            this.slightWber.Location = new System.Drawing.Point(1059, 1);
            this.slightWber.Name = "slightWber";
            this.slightWber.Size = new System.Drawing.Size(20, 20);
            this.slightWber.TabIndex = 219;
            this.toolTip1.SetToolTip(this.slightWber, "<绿色> 已连接\r\n<红色> 未连接");
            // 
            // timer2
            // 
            this.timer2.Interval = 5000;
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
            this.pnlExMain.Size = new System.Drawing.Size(1147, 645);
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
            this.tableLayoutPanel1.Controls.Add(this.rtxtOutputInfo, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.panSampleBarrels, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.panelEx1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.panelEx2, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.txtInputSampleCode, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.ForeColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 5;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1147, 645);
            this.tableLayoutPanel1.TabIndex = 221;
            // 
            // rtxtOutputInfo
            // 
            // 
            // 
            // 
            this.rtxtOutputInfo.BackgroundStyle.Class = "RichTextBoxBorder";
            this.rtxtOutputInfo.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.rtxtOutputInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxtOutputInfo.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.rtxtOutputInfo.ForeColor = System.Drawing.Color.White;
            this.rtxtOutputInfo.Location = new System.Drawing.Point(3, 193);
            this.rtxtOutputInfo.Name = "rtxtOutputInfo";
            this.rtxtOutputInfo.ReadOnly = true;
            this.rtxtOutputInfo.Size = new System.Drawing.Size(1141, 449);
            this.rtxtOutputInfo.TabIndex = 202;
            // 
            // panSampleBarrels
            // 
            this.panSampleBarrels.CanvasColor = System.Drawing.SystemColors.Control;
            this.panSampleBarrels.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panSampleBarrels.Controls.Add(this.buttonX3);
            this.panSampleBarrels.Controls.Add(this.buttonX4);
            this.panSampleBarrels.Controls.Add(this.buttonX8);
            this.panSampleBarrels.Controls.Add(this.buttonX5);
            this.panSampleBarrels.Controls.Add(this.buttonX9);
            this.panSampleBarrels.Controls.Add(this.buttonX6);
            this.panSampleBarrels.Controls.Add(this.buttonX10);
            this.panSampleBarrels.Controls.Add(this.buttonX7);
            this.panSampleBarrels.Controls.Add(this.buttonX11);
            this.panSampleBarrels.Controls.Add(this.buttonX12);
            this.panSampleBarrels.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panSampleBarrels.Location = new System.Drawing.Point(3, 113);
            this.panSampleBarrels.Name = "panSampleBarrels";
            this.panSampleBarrels.Size = new System.Drawing.Size(1141, 74);
            this.panSampleBarrels.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panSampleBarrels.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panSampleBarrels.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panSampleBarrels.Style.BorderColor.Color = System.Drawing.Color.Gray;
            this.panSampleBarrels.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panSampleBarrels.Style.GradientAngle = 90;
            this.panSampleBarrels.TabIndex = 216;
            // 
            // buttonX3
            // 
            this.buttonX3.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX3.BackColor = System.Drawing.Color.Gainsboro;
            this.buttonX3.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.buttonX3.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonX3.Location = new System.Drawing.Point(9, 7);
            this.buttonX3.Margin = new System.Windows.Forms.Padding(6, 10, 6, 6);
            this.buttonX3.Name = "buttonX3";
            this.buttonX3.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor(10);
            this.buttonX3.Size = new System.Drawing.Size(60, 60);
            this.buttonX3.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX3.TabIndex = 175;
            this.buttonX3.Tag = "Btn";
            this.buttonX3.Text = "1";
            this.buttonX3.TextColor = System.Drawing.Color.Black;
            // 
            // buttonX4
            // 
            this.buttonX4.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX4.BackColor = System.Drawing.Color.Gainsboro;
            this.buttonX4.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.buttonX4.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonX4.Location = new System.Drawing.Point(81, 7);
            this.buttonX4.Margin = new System.Windows.Forms.Padding(6, 10, 6, 6);
            this.buttonX4.Name = "buttonX4";
            this.buttonX4.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor(10);
            this.buttonX4.Size = new System.Drawing.Size(60, 60);
            this.buttonX4.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX4.TabIndex = 176;
            this.buttonX4.Tag = "Btn";
            this.buttonX4.Text = "2";
            this.buttonX4.TextColor = System.Drawing.Color.Black;
            // 
            // buttonX8
            // 
            this.buttonX8.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX8.BackColor = System.Drawing.Color.Gainsboro;
            this.buttonX8.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.buttonX8.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonX8.Location = new System.Drawing.Point(657, 7);
            this.buttonX8.Margin = new System.Windows.Forms.Padding(6, 10, 6, 6);
            this.buttonX8.Name = "buttonX8";
            this.buttonX8.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor(10);
            this.buttonX8.Size = new System.Drawing.Size(60, 60);
            this.buttonX8.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX8.TabIndex = 184;
            this.buttonX8.Tag = "Btn";
            this.buttonX8.Text = "10";
            this.buttonX8.TextColor = System.Drawing.Color.Black;
            // 
            // buttonX5
            // 
            this.buttonX5.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX5.BackColor = System.Drawing.Color.Gainsboro;
            this.buttonX5.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.buttonX5.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonX5.Location = new System.Drawing.Point(153, 7);
            this.buttonX5.Margin = new System.Windows.Forms.Padding(6, 10, 6, 6);
            this.buttonX5.Name = "buttonX5";
            this.buttonX5.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor(10);
            this.buttonX5.Size = new System.Drawing.Size(60, 60);
            this.buttonX5.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX5.TabIndex = 177;
            this.buttonX5.Tag = "Btn";
            this.buttonX5.Text = "3";
            this.buttonX5.TextColor = System.Drawing.Color.Black;
            // 
            // buttonX9
            // 
            this.buttonX9.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX9.BackColor = System.Drawing.Color.Gainsboro;
            this.buttonX9.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.buttonX9.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonX9.Location = new System.Drawing.Point(585, 7);
            this.buttonX9.Margin = new System.Windows.Forms.Padding(6, 10, 6, 6);
            this.buttonX9.Name = "buttonX9";
            this.buttonX9.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor(10);
            this.buttonX9.Size = new System.Drawing.Size(60, 60);
            this.buttonX9.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX9.TabIndex = 183;
            this.buttonX9.Tag = "Btn";
            this.buttonX9.Text = "9";
            this.buttonX9.TextColor = System.Drawing.Color.Black;
            // 
            // buttonX6
            // 
            this.buttonX6.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX6.BackColor = System.Drawing.Color.Gainsboro;
            this.buttonX6.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.buttonX6.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonX6.Location = new System.Drawing.Point(225, 7);
            this.buttonX6.Margin = new System.Windows.Forms.Padding(6, 10, 6, 6);
            this.buttonX6.Name = "buttonX6";
            this.buttonX6.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor(10);
            this.buttonX6.Size = new System.Drawing.Size(60, 60);
            this.buttonX6.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX6.TabIndex = 178;
            this.buttonX6.Tag = "Btn";
            this.buttonX6.Text = "4";
            this.buttonX6.TextColor = System.Drawing.Color.Black;
            // 
            // buttonX10
            // 
            this.buttonX10.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX10.BackColor = System.Drawing.Color.Gainsboro;
            this.buttonX10.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.buttonX10.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonX10.Location = new System.Drawing.Point(513, 7);
            this.buttonX10.Margin = new System.Windows.Forms.Padding(6, 10, 6, 6);
            this.buttonX10.Name = "buttonX10";
            this.buttonX10.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor(10);
            this.buttonX10.Size = new System.Drawing.Size(60, 60);
            this.buttonX10.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX10.TabIndex = 182;
            this.buttonX10.Tag = "Btn";
            this.buttonX10.Text = "8";
            this.buttonX10.TextColor = System.Drawing.Color.Black;
            // 
            // buttonX7
            // 
            this.buttonX7.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX7.BackColor = System.Drawing.Color.Gainsboro;
            this.buttonX7.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.buttonX7.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonX7.Location = new System.Drawing.Point(297, 7);
            this.buttonX7.Margin = new System.Windows.Forms.Padding(6, 10, 6, 6);
            this.buttonX7.Name = "buttonX7";
            this.buttonX7.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor(10);
            this.buttonX7.Size = new System.Drawing.Size(60, 60);
            this.buttonX7.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX7.TabIndex = 179;
            this.buttonX7.Tag = "Btn";
            this.buttonX7.Text = "5";
            this.buttonX7.TextColor = System.Drawing.Color.Black;
            // 
            // buttonX11
            // 
            this.buttonX11.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX11.BackColor = System.Drawing.Color.Gainsboro;
            this.buttonX11.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.buttonX11.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonX11.Location = new System.Drawing.Point(441, 7);
            this.buttonX11.Margin = new System.Windows.Forms.Padding(6, 10, 6, 6);
            this.buttonX11.Name = "buttonX11";
            this.buttonX11.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor(10);
            this.buttonX11.Size = new System.Drawing.Size(60, 60);
            this.buttonX11.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX11.TabIndex = 181;
            this.buttonX11.Tag = "Btn";
            this.buttonX11.Text = "7";
            this.buttonX11.TextColor = System.Drawing.Color.Black;
            // 
            // buttonX12
            // 
            this.buttonX12.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX12.BackColor = System.Drawing.Color.Gainsboro;
            this.buttonX12.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.buttonX12.Font = new System.Drawing.Font("Segoe UI", 24F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonX12.Location = new System.Drawing.Point(369, 7);
            this.buttonX12.Margin = new System.Windows.Forms.Padding(6, 10, 6, 6);
            this.buttonX12.Name = "buttonX12";
            this.buttonX12.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor(10);
            this.buttonX12.Size = new System.Drawing.Size(60, 60);
            this.buttonX12.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX12.TabIndex = 180;
            this.buttonX12.Tag = "Btn";
            this.buttonX12.Text = "6";
            this.buttonX12.TextColor = System.Drawing.Color.Black;
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.lbl_CurrentWeight);
            this.panelEx1.Controls.Add(this.lblweight);
            this.panelEx1.Controls.Add(this.slightWber);
            this.panelEx1.Controls.Add(this.lblWber);
            this.panelEx1.Controls.Add(this.label2);
            this.panelEx1.Controls.Add(this.lblCurrentFlowFlag);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(3, 3);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(1141, 24);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 203;
            // 
            // lbl_CurrentWeight
            // 
            this.lbl_CurrentWeight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lbl_CurrentWeight.AutoSize = true;
            this.lbl_CurrentWeight.Font = new System.Drawing.Font("Segoe UI", 14.25F);
            this.lbl_CurrentWeight.ForeColor = System.Drawing.Color.White;
            this.lbl_CurrentWeight.Location = new System.Drawing.Point(993, -1);
            this.lbl_CurrentWeight.Name = "lbl_CurrentWeight";
            this.lbl_CurrentWeight.Size = new System.Drawing.Size(22, 25);
            this.lbl_CurrentWeight.TabIndex = 222;
            this.lbl_CurrentWeight.Text = "0";
            // 
            // lblweight
            // 
            this.lblweight.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblweight.AutoSize = true;
            this.lblweight.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.lblweight.ForeColor = System.Drawing.Color.White;
            this.lblweight.Location = new System.Drawing.Point(865, 1);
            this.lblweight.Name = "lblweight";
            this.lblweight.Size = new System.Drawing.Size(137, 20);
            this.lblweight.TabIndex = 221;
            this.lblweight.Text = "电子秤当前重量：";
            // 
            // lblWber
            // 
            this.lblWber.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblWber.AutoSize = true;
            this.lblWber.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWber.ForeColor = System.Drawing.Color.White;
            this.lblWber.Location = new System.Drawing.Point(1084, 1);
            this.lblWber.Name = "lblWber";
            this.lblWber.Size = new System.Drawing.Size(57, 20);
            this.lblWber.TabIndex = 220;
            this.lblWber.Text = "电子秤";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(3, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(89, 20);
            this.label2.TabIndex = 213;
            this.label2.Text = "当前流程：";
            // 
            // lblCurrentFlowFlag
            // 
            this.lblCurrentFlowFlag.AutoSize = true;
            this.lblCurrentFlowFlag.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.lblCurrentFlowFlag.ForeColor = System.Drawing.Color.White;
            this.lblCurrentFlowFlag.Location = new System.Drawing.Point(88, 3);
            this.lblCurrentFlowFlag.Name = "lblCurrentFlowFlag";
            this.lblCurrentFlowFlag.Size = new System.Drawing.Size(73, 20);
            this.lblCurrentFlowFlag.TabIndex = 214;
            this.lblCurrentFlowFlag.Text = "等待扫码";
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.btnReset);
            this.panelEx2.Controls.Add(this.buttonX13);
            this.panelEx2.Controls.Add(this.label13);
            this.panelEx2.Controls.Add(this.buttonX1);
            this.panelEx2.Controls.Add(this.label8);
            this.panelEx2.Controls.Add(this.label9);
            this.panelEx2.Controls.Add(this.buttonX2);
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx2.Location = new System.Drawing.Point(3, 73);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(1141, 34);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 204;
            // 
            // btnReset
            // 
            this.btnReset.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnReset.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnReset.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.btnReset.Location = new System.Drawing.Point(1059, 3);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(82, 33);
            this.btnReset.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnReset.TabIndex = 218;
            this.btnReset.Text = "重 置";
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // buttonX13
            // 
            this.buttonX13.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX13.BackColor = System.Drawing.Color.WhiteSmoke;
            this.buttonX13.ColorTable = DevComponents.DotNetBar.eButtonColor.Blue;
            this.buttonX13.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.buttonX13.Location = new System.Drawing.Point(7, 13);
            this.buttonX13.Name = "buttonX13";
            this.buttonX13.Shape = new DevComponents.DotNetBar.RoundRectangleShapeDescriptor(10, 5, 5, 5);
            this.buttonX13.Size = new System.Drawing.Size(18, 18);
            this.buttonX13.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX13.TabIndex = 210;
            this.buttonX13.Tag = "Btn";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.label13.ForeColor = System.Drawing.Color.White;
            this.label13.Location = new System.Drawing.Point(26, 10);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(41, 20);
            this.label13.TabIndex = 211;
            this.label13.Text = "默认";
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(157)))), ((int)(((byte)(218)))));
            this.buttonX1.ColorTable = DevComponents.DotNetBar.eButtonColor.Flat;
            this.buttonX1.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonX1.Location = new System.Drawing.Point(79, 12);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(18, 18);
            this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX1.TabIndex = 206;
            this.buttonX1.Tag = "Btn";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.label8.ForeColor = System.Drawing.Color.White;
            this.label8.Location = new System.Drawing.Point(103, 10);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(57, 20);
            this.label8.TabIndex = 207;
            this.label8.Text = "待验证";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.label9.ForeColor = System.Drawing.Color.White;
            this.label9.Location = new System.Drawing.Point(197, 10);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(57, 20);
            this.label9.TabIndex = 209;
            this.label9.Text = "已验证";
            // 
            // buttonX2
            // 
            this.buttonX2.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX2.BackColor = System.Drawing.Color.Red;
            this.buttonX2.ColorTable = DevComponents.DotNetBar.eButtonColor.Blue;
            this.buttonX2.Font = new System.Drawing.Font("Segoe UI", 14F);
            this.buttonX2.Location = new System.Drawing.Point(173, 12);
            this.buttonX2.Name = "buttonX2";
            this.buttonX2.Size = new System.Drawing.Size(18, 18);
            this.buttonX2.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX2.TabIndex = 208;
            this.buttonX2.Tag = "Btn";
            // 
            // txtInputSampleCode
            // 
            this.txtInputSampleCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
            // 
            // 
            // 
            this.txtInputSampleCode.Border.Class = "TextBoxBorder";
            this.txtInputSampleCode.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtInputSampleCode.ButtonCustom.Text = "重置";
            this.txtInputSampleCode.ButtonCustom2.Enabled = false;
            this.txtInputSampleCode.ButtonCustom2.Text = "打印";
            this.txtInputSampleCode.Dock = System.Windows.Forms.DockStyle.Fill;
            this.txtInputSampleCode.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.txtInputSampleCode.ForeColor = System.Drawing.Color.White;
            this.txtInputSampleCode.Location = new System.Drawing.Point(3, 33);
            this.txtInputSampleCode.Name = "txtInputSampleCode";
            this.txtInputSampleCode.Size = new System.Drawing.Size(1141, 39);
            this.txtInputSampleCode.TabIndex = 203;
            this.txtInputSampleCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtInputSampleCode.WatermarkText = "扫描采样桶编码. . .";
            this.txtInputSampleCode.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtInputSampleCode_KeyUp);
            // 
            // FrmSampleCheck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
            this.ClientSize = new System.Drawing.Size(1147, 645);
            this.Controls.Add(this.pnlExMain);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "FrmSampleCheck";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "采样桶验证";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmSampleCheck_FormClosing);
            this.Load += new System.EventHandler(this.FrmSampleCheck_Load);
            this.pnlExMain.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panSampleBarrels.ResumeLayout(false);
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            this.panelEx2.ResumeLayout(false);
            this.panelEx2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Timer timer2;
        private DevComponents.DotNetBar.PanelEx pnlExMain;
        private System.Windows.Forms.Label label13;
        private DevComponents.DotNetBar.ButtonX buttonX13;
        private System.Windows.Forms.Label label9;
        private DevComponents.DotNetBar.ButtonX buttonX2;
        private System.Windows.Forms.Label label8;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        private DevComponents.DotNetBar.ButtonX buttonX3;
        private DevComponents.DotNetBar.ButtonX buttonX4;
        private DevComponents.DotNetBar.ButtonX buttonX8;
        private DevComponents.DotNetBar.ButtonX buttonX5;
        private DevComponents.DotNetBar.ButtonX buttonX9;
        private DevComponents.DotNetBar.ButtonX buttonX6;
        private DevComponents.DotNetBar.ButtonX buttonX10;
        private DevComponents.DotNetBar.ButtonX buttonX7;
        private DevComponents.DotNetBar.ButtonX buttonX11;
        private DevComponents.DotNetBar.ButtonX buttonX12;
        private DevComponents.DotNetBar.Controls.TextBoxX txtInputSampleCode;
        private DevComponents.DotNetBar.Controls.RichTextBoxEx rtxtOutputInfo;
        private System.Windows.Forms.Label lblCurrentFlowFlag;
        private System.Windows.Forms.Label label2;
        private DevComponents.DotNetBar.PanelEx panSampleBarrels;
        private DevComponents.DotNetBar.ButtonX btnReset;
        private System.Windows.Forms.Label lblWber;
        private Forms.UserControls.UCtrlSignalLight slightWber;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private System.Windows.Forms.Label lbl_CurrentWeight;
        private System.Windows.Forms.Label lblweight;
    }
}

