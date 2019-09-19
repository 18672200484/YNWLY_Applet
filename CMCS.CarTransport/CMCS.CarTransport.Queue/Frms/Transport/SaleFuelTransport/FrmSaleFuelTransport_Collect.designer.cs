namespace CMCS.CarTransport.Queue.Frms.Transport.SaleFuelTransport
{
    partial class FrmSaleFuelTransport_Collect
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
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn7 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.tsmiPrint = new System.Windows.Forms.ToolStripMenuItem();
            this.btnSearch = new DevComponents.DotNetBar.ButtonX();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.panel1 = new System.Windows.Forms.Panel();
            this.cmbMineName_BuyFuel = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.cmbFuelKindName_BuyFuel = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.chkFuelKindTotal = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.chkFuelSupplierTotal = new DevComponents.DotNetBar.Controls.CheckBoxX();
            this.dtpStartTime = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.dtpEndTime = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.btnPrint = new DevComponents.DotNetBar.ButtonX();
            this.superGridControl1 = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtpStartTime)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpEndTime)).BeginInit();
            this.SuspendLayout();
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiPrint});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(125, 26);
            // 
            // tsmiPrint
            // 
            this.tsmiPrint.Name = "tsmiPrint";
            this.tsmiPrint.Size = new System.Drawing.Size(124, 22);
            this.tsmiPrint.Text = "打印磅单";
            this.tsmiPrint.Click += new System.EventHandler(this.tsmiPrint_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSearch.Location = new System.Drawing.Point(1214, 9);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(64, 23);
            this.btnSearch.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSearch.TabIndex = 13;
            this.btnSearch.Text = "搜 索";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // splitContainer1
            // 
            this.splitContainer1.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.ForeColor = System.Drawing.Color.White;
            this.splitContainer1.Location = new System.Drawing.Point(0, 1);
            this.splitContainer1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.BackColor = System.Drawing.Color.Transparent;
            this.splitContainer1.Panel1.Controls.Add(this.panel1);
            this.splitContainer1.Panel1.ForeColor = System.Drawing.Color.White;
            this.splitContainer1.Panel1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(82)))), ((int)(((byte)(89)))));
            this.splitContainer1.Panel2.Controls.Add(this.superGridControl1);
            this.splitContainer1.Panel2.ForeColor = System.Drawing.Color.White;
            this.splitContainer1.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.splitContainer1.Size = new System.Drawing.Size(1432, 453);
            this.splitContainer1.SplitterDistance = 40;
            this.splitContainer1.SplitterWidth = 1;
            this.splitContainer1.TabIndex = 147;
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.Transparent;
            this.panel1.Controls.Add(this.cmbMineName_BuyFuel);
            this.panel1.Controls.Add(this.cmbFuelKindName_BuyFuel);
            this.panel1.Controls.Add(this.chkFuelKindTotal);
            this.panel1.Controls.Add(this.chkFuelSupplierTotal);
            this.panel1.Controls.Add(this.dtpStartTime);
            this.panel1.Controls.Add(this.dtpEndTime);
            this.panel1.Controls.Add(this.buttonX1);
            this.panel1.Controls.Add(this.btnPrint);
            this.panel1.Controls.Add(this.btnSearch);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.ForeColor = System.Drawing.Color.White;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1432, 40);
            this.panel1.TabIndex = 12;
            // 
            // cmbMineName_BuyFuel
            // 
            this.cmbMineName_BuyFuel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbMineName_BuyFuel.DisplayMember = "Text";
            this.cmbMineName_BuyFuel.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbMineName_BuyFuel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbMineName_BuyFuel.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.cmbMineName_BuyFuel.ForeColor = System.Drawing.Color.White;
            this.cmbMineName_BuyFuel.FormattingEnabled = true;
            this.cmbMineName_BuyFuel.ItemHeight = 23;
            this.cmbMineName_BuyFuel.Location = new System.Drawing.Point(820, 6);
            this.cmbMineName_BuyFuel.Name = "cmbMineName_BuyFuel";
            this.cmbMineName_BuyFuel.Size = new System.Drawing.Size(240, 29);
            this.cmbMineName_BuyFuel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbMineName_BuyFuel.TabIndex = 281;
            this.cmbMineName_BuyFuel.WatermarkText = "请选择收货单位";
            // 
            // cmbFuelKindName_BuyFuel
            // 
            this.cmbFuelKindName_BuyFuel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.cmbFuelKindName_BuyFuel.DisplayMember = "Text";
            this.cmbFuelKindName_BuyFuel.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbFuelKindName_BuyFuel.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbFuelKindName_BuyFuel.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.cmbFuelKindName_BuyFuel.ForeColor = System.Drawing.Color.White;
            this.cmbFuelKindName_BuyFuel.FormattingEnabled = true;
            this.cmbFuelKindName_BuyFuel.ItemHeight = 23;
            this.cmbFuelKindName_BuyFuel.Location = new System.Drawing.Point(1078, 6);
            this.cmbFuelKindName_BuyFuel.Name = "cmbFuelKindName_BuyFuel";
            this.cmbFuelKindName_BuyFuel.Size = new System.Drawing.Size(130, 29);
            this.cmbFuelKindName_BuyFuel.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbFuelKindName_BuyFuel.TabIndex = 277;
            this.cmbFuelKindName_BuyFuel.WatermarkText = "请选择煤种";
            // 
            // chkFuelKindTotal
            // 
            this.chkFuelKindTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.chkFuelKindTotal.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chkFuelKindTotal.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.chkFuelKindTotal.ForeColor = System.Drawing.Color.White;
            this.chkFuelKindTotal.Location = new System.Drawing.Point(418, 10);
            this.chkFuelKindTotal.Name = "chkFuelKindTotal";
            this.chkFuelKindTotal.Size = new System.Drawing.Size(100, 23);
            this.chkFuelKindTotal.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chkFuelKindTotal.TabIndex = 276;
            this.chkFuelKindTotal.Text = "煤种统计";
            // 
            // chkFuelSupplierTotal
            // 
            this.chkFuelSupplierTotal.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.chkFuelSupplierTotal.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.chkFuelSupplierTotal.Checked = true;
            this.chkFuelSupplierTotal.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkFuelSupplierTotal.CheckValue = "Y";
            this.chkFuelSupplierTotal.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.chkFuelSupplierTotal.ForeColor = System.Drawing.Color.White;
            this.chkFuelSupplierTotal.Location = new System.Drawing.Point(283, 10);
            this.chkFuelSupplierTotal.Name = "chkFuelSupplierTotal";
            this.chkFuelSupplierTotal.Size = new System.Drawing.Size(129, 23);
            this.chkFuelSupplierTotal.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.chkFuelSupplierTotal.TabIndex = 276;
            this.chkFuelSupplierTotal.Text = "收货单位统计";
            // 
            // dtpStartTime
            // 
            this.dtpStartTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.dtpStartTime.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dtpStartTime.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtpStartTime.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.dtpStartTime.ButtonDropDown.Visible = true;
            this.dtpStartTime.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.dtpStartTime.ForeColor = System.Drawing.Color.White;
            this.dtpStartTime.IsPopupCalendarOpen = false;
            this.dtpStartTime.Location = new System.Drawing.Point(524, 6);
            this.dtpStartTime.LockUpdateChecked = false;
            // 
            // 
            // 
            this.dtpStartTime.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtpStartTime.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtpStartTime.MonthCalendar.CalendarDimensions = new System.Drawing.Size(1, 1);
            this.dtpStartTime.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.dtpStartTime.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.dtpStartTime.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.dtpStartTime.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.dtpStartTime.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.dtpStartTime.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.dtpStartTime.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.dtpStartTime.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtpStartTime.MonthCalendar.DisplayMonth = new System.DateTime(2016, 7, 1, 0, 0, 0, 0);
            this.dtpStartTime.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.dtpStartTime.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtpStartTime.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.dtpStartTime.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.dtpStartTime.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.dtpStartTime.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtpStartTime.MonthCalendar.TodayButtonVisible = true;
            this.dtpStartTime.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.dtpStartTime.Name = "dtpStartTime";
            this.dtpStartTime.Size = new System.Drawing.Size(136, 29);
            this.dtpStartTime.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.dtpStartTime.TabIndex = 270;
            this.dtpStartTime.WatermarkText = "开始时间";
            // 
            // dtpEndTime
            // 
            this.dtpEndTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.dtpEndTime.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dtpEndTime.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtpEndTime.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.dtpEndTime.ButtonDropDown.Visible = true;
            this.dtpEndTime.Font = new System.Drawing.Font("Segoe UI", 12F);
            this.dtpEndTime.ForeColor = System.Drawing.Color.White;
            this.dtpEndTime.IsPopupCalendarOpen = false;
            this.dtpEndTime.Location = new System.Drawing.Point(666, 6);
            this.dtpEndTime.LockUpdateChecked = false;
            // 
            // 
            // 
            this.dtpEndTime.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtpEndTime.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtpEndTime.MonthCalendar.CalendarDimensions = new System.Drawing.Size(1, 1);
            this.dtpEndTime.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.dtpEndTime.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.dtpEndTime.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.dtpEndTime.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.dtpEndTime.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.dtpEndTime.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.dtpEndTime.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.dtpEndTime.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtpEndTime.MonthCalendar.DisplayMonth = new System.DateTime(2016, 7, 1, 0, 0, 0, 0);
            this.dtpEndTime.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.dtpEndTime.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtpEndTime.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.dtpEndTime.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.dtpEndTime.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.dtpEndTime.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtpEndTime.MonthCalendar.TodayButtonVisible = true;
            this.dtpEndTime.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.dtpEndTime.Name = "dtpEndTime";
            this.dtpEndTime.Size = new System.Drawing.Size(136, 29);
            this.dtpEndTime.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.dtpEndTime.TabIndex = 270;
            this.dtpEndTime.WatermarkText = "结束时间";
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.buttonX1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonX1.Location = new System.Drawing.Point(1354, 9);
            this.buttonX1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(64, 23);
            this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX1.TabIndex = 13;
            this.buttonX1.Text = "导 出";
            this.buttonX1.Click += new System.EventHandler(this.btnXExport_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnPrint.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrint.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnPrint.Location = new System.Drawing.Point(1284, 9);
            this.btnPrint.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(64, 23);
            this.btnPrint.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnPrint.TabIndex = 13;
            this.btnPrint.Text = "打 印";
            this.btnPrint.Click += new System.EventHandler(this.tsmiPrint_Click);
            // 
            // superGridControl1
            // 
            this.superGridControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
            this.superGridControl1.DefaultVisualStyles.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            this.superGridControl1.DefaultVisualStyles.CellStyles.Default.Font = new System.Drawing.Font("Segoe UI", 14.25F);
            this.superGridControl1.DefaultVisualStyles.ColumnHeaderStyles.Default.Font = new System.Drawing.Font("Segoe UI", 14.25F);
            this.superGridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.superGridControl1.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
            this.superGridControl1.ForeColor = System.Drawing.Color.White;
            this.superGridControl1.Location = new System.Drawing.Point(0, 0);
            this.superGridControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.superGridControl1.Name = "superGridControl1";
            this.superGridControl1.PrimaryGrid.AutoGenerateColumns = false;
            this.superGridControl1.PrimaryGrid.Caption.Text = "";
            gridColumn1.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gridColumn1.DataPropertyName = "SupplierName";
            gridColumn1.HeaderText = "收货单位";
            gridColumn1.InfoImageAlignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gridColumn1.Name = "";
            gridColumn1.Width = 250;
            gridColumn2.AutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.AllCells;
            gridColumn2.DataPropertyName = "TransportCompanyName";
            gridColumn2.HeaderText = "运输单位";
            gridColumn2.Name = "";
            gridColumn3.DataPropertyName = "FuelKindId";
            gridColumn3.HeaderText = "煤种";
            gridColumn3.Name = "";
            gridColumn3.Width = 120;
            gridColumn4.DataPropertyName = "IsFinish";
            gridColumn4.HeaderText = "车数";
            gridColumn4.Name = "";
            gridColumn5.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gridColumn5.DataPropertyName = "GrossWeight";
            gridColumn5.HeaderText = "毛重";
            gridColumn5.InfoImageAlignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gridColumn5.Name = "";
            gridColumn5.Width = 80;
            gridColumn6.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gridColumn6.DataPropertyName = "TareWeight";
            gridColumn6.HeaderText = "皮重";
            gridColumn6.InfoImageAlignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gridColumn6.Name = "";
            gridColumn6.Width = 80;
            gridColumn7.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gridColumn7.DataPropertyName = "SuttleWeight";
            gridColumn7.HeaderText = "净重";
            gridColumn7.InfoImageAlignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gridColumn7.Name = "";
            gridColumn7.Width = 80;
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn1);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn2);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn3);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn4);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn5);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn6);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn7);
            this.superGridControl1.PrimaryGrid.DefaultRowHeight = 0;
            this.superGridControl1.PrimaryGrid.InitialSelection = DevComponents.DotNetBar.SuperGrid.RelativeSelection.Row;
            this.superGridControl1.PrimaryGrid.SelectionGranularity = DevComponents.DotNetBar.SuperGrid.SelectionGranularity.Row;
            this.superGridControl1.Size = new System.Drawing.Size(1432, 412);
            this.superGridControl1.TabIndex = 5;
            this.superGridControl1.Text = "superGridControl1";
            this.superGridControl1.GetRowHeaderText += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridGetRowHeaderTextEventArgs>(this.superGridControl_GetRowHeaderText);
            // 
            // FrmSaleFuelTransport_Collect
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1433, 455);
            this.Controls.Add(this.splitContainer1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "FrmSaleFuelTransport_Collect";
            this.Text = "出场煤汇总";
            this.Load += new System.EventHandler(this.FrmSaleFuelTransport_Collect_Load);
            this.contextMenuStrip1.ResumeLayout(false);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtpStartTime)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtpEndTime)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonX btnSearch;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem tsmiPrint;
        private System.Drawing.Printing.PrintDocument printDocument1;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput dtpEndTime;
        private DevComponents.DotNetBar.SuperGrid.SuperGridControl superGridControl1;
        private DevComponents.DotNetBar.ButtonX btnPrint;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkFuelSupplierTotal;
        private DevComponents.DotNetBar.Controls.CheckBoxX chkFuelKindTotal;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbFuelKindName_BuyFuel;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput dtpStartTime;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbMineName_BuyFuel;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
    }
}