namespace CMCS.Monitor.Win.Frms
{
    partial class FrmAutoMakerRecord
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
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn1 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn2 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn3 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn4 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn5 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn6 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn7 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn8 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAutoMakerRecord));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.plnTop = new System.Windows.Forms.Panel();
            this.dtInputEnd = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.lblto = new DevComponents.DotNetBar.LabelX();
            this.dtInputStart = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.btnToday = new DevComponents.DotNetBar.ButtonX();
            this.btnNextDay = new DevComponents.DotNetBar.ButtonX();
            this.btnPreDay = new DevComponents.DotNetBar.ButtonX();
            this.txtBarrelCode_Ser = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btnSearch = new DevComponents.DotNetBar.ButtonX();
            this.btnAll = new DevComponents.DotNetBar.ButtonX();
            this.plnBottom = new System.Windows.Forms.Panel();
            this.lblPagerInfo = new DevComponents.DotNetBar.LabelX();
            this.btnFirst = new DevComponents.DotNetBar.ButtonX();
            this.btnPrevious = new DevComponents.DotNetBar.ButtonX();
            this.btnNext = new DevComponents.DotNetBar.ButtonX();
            this.btnLast = new DevComponents.DotNetBar.ButtonX();
            this.plnMain = new System.Windows.Forms.Panel();
            this.superGridControl1 = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
            this.tableLayoutPanel1.SuspendLayout();
            this.plnTop.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtInputEnd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtInputStart)).BeginInit();
            this.plnBottom.SuspendLayout();
            this.plnMain.SuspendLayout();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.plnTop, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.plnBottom, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.plnMain, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 32F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 16F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(809, 418);
            this.tableLayoutPanel1.TabIndex = 0;
            // 
            // plnTop
            // 
            this.plnTop.Controls.Add(this.dtInputEnd);
            this.plnTop.Controls.Add(this.lblto);
            this.plnTop.Controls.Add(this.dtInputStart);
            this.plnTop.Controls.Add(this.btnToday);
            this.plnTop.Controls.Add(this.btnNextDay);
            this.plnTop.Controls.Add(this.btnPreDay);
            this.plnTop.Controls.Add(this.txtBarrelCode_Ser);
            this.plnTop.Controls.Add(this.btnSearch);
            this.plnTop.Controls.Add(this.btnAll);
            this.plnTop.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plnTop.Location = new System.Drawing.Point(3, 2);
            this.plnTop.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.plnTop.Name = "plnTop";
            this.plnTop.Size = new System.Drawing.Size(803, 28);
            this.plnTop.TabIndex = 0;
            // 
            // dtInputEnd
            // 
            // 
            // 
            // 
            this.dtInputEnd.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dtInputEnd.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtInputEnd.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.dtInputEnd.ButtonDropDown.Visible = true;
            this.dtInputEnd.CustomFormat = "yyyy-MM-dd";
            this.dtInputEnd.ForeColor = System.Drawing.Color.Black;
            this.dtInputEnd.Format = DevComponents.Editors.eDateTimePickerFormat.Custom;
            this.dtInputEnd.IsPopupCalendarOpen = false;
            this.dtInputEnd.Location = new System.Drawing.Point(383, 4);
            this.dtInputEnd.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            // 
            // 
            // 
            this.dtInputEnd.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtInputEnd.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtInputEnd.MonthCalendar.CalendarDimensions = new System.Drawing.Size(1, 1);
            this.dtInputEnd.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.dtInputEnd.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.dtInputEnd.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.dtInputEnd.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.dtInputEnd.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.dtInputEnd.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.dtInputEnd.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.dtInputEnd.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtInputEnd.MonthCalendar.DisplayMonth = new System.DateTime(2016, 3, 1, 0, 0, 0, 0);
            this.dtInputEnd.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.dtInputEnd.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtInputEnd.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.dtInputEnd.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.dtInputEnd.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.dtInputEnd.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtInputEnd.MonthCalendar.TodayButtonVisible = true;
            this.dtInputEnd.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.dtInputEnd.Name = "dtInputEnd";
            this.dtInputEnd.Size = new System.Drawing.Size(110, 21);
            this.dtInputEnd.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.dtInputEnd.TabIndex = 179;
            this.dtInputEnd.TimeSelectorTimeFormat = DevComponents.Editors.DateTimeAdv.eTimeSelectorFormat.Time24H;
            // 
            // lblto
            // 
            // 
            // 
            // 
            this.lblto.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblto.ForeColor = System.Drawing.Color.Black;
            this.lblto.Location = new System.Drawing.Point(360, 5);
            this.lblto.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lblto.Name = "lblto";
            this.lblto.Size = new System.Drawing.Size(18, 18);
            this.lblto.TabIndex = 178;
            this.lblto.Text = "至";
            // 
            // dtInputStart
            // 
            // 
            // 
            // 
            this.dtInputStart.BackgroundStyle.Class = "DateTimeInputBackground";
            this.dtInputStart.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtInputStart.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
            this.dtInputStart.ButtonDropDown.Visible = true;
            this.dtInputStart.CustomFormat = "yyyy-MM-dd";
            this.dtInputStart.ForeColor = System.Drawing.Color.Black;
            this.dtInputStart.Format = DevComponents.Editors.eDateTimePickerFormat.Custom;
            this.dtInputStart.IsPopupCalendarOpen = false;
            this.dtInputStart.Location = new System.Drawing.Point(244, 4);
            this.dtInputStart.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            // 
            // 
            // 
            this.dtInputStart.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtInputStart.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtInputStart.MonthCalendar.CalendarDimensions = new System.Drawing.Size(1, 1);
            this.dtInputStart.MonthCalendar.ClearButtonVisible = true;
            // 
            // 
            // 
            this.dtInputStart.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
            this.dtInputStart.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
            this.dtInputStart.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
            this.dtInputStart.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
            this.dtInputStart.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
            this.dtInputStart.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
            this.dtInputStart.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtInputStart.MonthCalendar.DisplayMonth = new System.DateTime(2016, 3, 1, 0, 0, 0, 0);
            this.dtInputStart.MonthCalendar.MarkedDates = new System.DateTime[0];
            this.dtInputStart.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
            // 
            // 
            // 
            this.dtInputStart.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
            this.dtInputStart.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
            this.dtInputStart.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.dtInputStart.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.dtInputStart.MonthCalendar.TodayButtonVisible = true;
            this.dtInputStart.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
            this.dtInputStart.Name = "dtInputStart";
            this.dtInputStart.Size = new System.Drawing.Size(110, 21);
            this.dtInputStart.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.dtInputStart.TabIndex = 177;
            this.dtInputStart.TimeSelectorTimeFormat = DevComponents.Editors.DateTimeAdv.eTimeSelectorFormat.Time24H;
            // 
            // btnToday
            // 
            this.btnToday.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnToday.CommandParameter = "Today";
            this.btnToday.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnToday.Location = new System.Drawing.Point(159, 5);
            this.btnToday.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnToday.Name = "btnToday";
            this.btnToday.Size = new System.Drawing.Size(64, 18);
            this.btnToday.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnToday.TabIndex = 176;
            this.btnToday.Text = "今天";
            this.btnToday.Click += new System.EventHandler(this.btnDay_Click);
            // 
            // btnNextDay
            // 
            this.btnNextDay.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnNextDay.CommandParameter = "Next";
            this.btnNextDay.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnNextDay.Location = new System.Drawing.Point(86, 5);
            this.btnNextDay.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnNextDay.Name = "btnNextDay";
            this.btnNextDay.Size = new System.Drawing.Size(64, 18);
            this.btnNextDay.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnNextDay.TabIndex = 175;
            this.btnNextDay.Text = "下一天";
            this.btnNextDay.Click += new System.EventHandler(this.btnDay_Click);
            // 
            // btnPreDay
            // 
            this.btnPreDay.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnPreDay.CommandParameter = "Last";
            this.btnPreDay.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPreDay.Location = new System.Drawing.Point(12, 5);
            this.btnPreDay.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnPreDay.Name = "btnPreDay";
            this.btnPreDay.Size = new System.Drawing.Size(64, 18);
            this.btnPreDay.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnPreDay.TabIndex = 174;
            this.btnPreDay.Text = "上一天";
            this.btnPreDay.Click += new System.EventHandler(this.btnDay_Click);
            // 
            // txtBarrelCode_Ser
            // 
            this.txtBarrelCode_Ser.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtBarrelCode_Ser.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
            // 
            // 
            // 
            this.txtBarrelCode_Ser.Border.Class = "TextBoxBorder";
            this.txtBarrelCode_Ser.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtBarrelCode_Ser.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.txtBarrelCode_Ser.ForeColor = System.Drawing.Color.Black;
            this.txtBarrelCode_Ser.Location = new System.Drawing.Point(571, 3);
            this.txtBarrelCode_Ser.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtBarrelCode_Ser.Name = "txtBarrelCode_Ser";
            this.txtBarrelCode_Ser.Size = new System.Drawing.Size(104, 23);
            this.txtBarrelCode_Ser.TabIndex = 173;
            this.txtBarrelCode_Ser.WatermarkText = "样罐编码...";
            // 
            // btnSearch
            // 
            this.btnSearch.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSearch.Location = new System.Drawing.Point(680, 5);
            this.btnSearch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(51, 18);
            this.btnSearch.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSearch.TabIndex = 172;
            this.btnSearch.Text = "搜索";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // btnAll
            // 
            this.btnAll.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAll.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnAll.Location = new System.Drawing.Point(737, 5);
            this.btnAll.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnAll.Name = "btnAll";
            this.btnAll.Size = new System.Drawing.Size(51, 18);
            this.btnAll.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnAll.TabIndex = 171;
            this.btnAll.Text = "全部";
            this.btnAll.Click += new System.EventHandler(this.btnAll_Click);
            // 
            // plnBottom
            // 
            this.plnBottom.Controls.Add(this.lblPagerInfo);
            this.plnBottom.Controls.Add(this.btnFirst);
            this.plnBottom.Controls.Add(this.btnPrevious);
            this.plnBottom.Controls.Add(this.btnNext);
            this.plnBottom.Controls.Add(this.btnLast);
            this.plnBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plnBottom.Location = new System.Drawing.Point(3, 388);
            this.plnBottom.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.plnBottom.Name = "plnBottom";
            this.plnBottom.Size = new System.Drawing.Size(803, 28);
            this.plnBottom.TabIndex = 1;
            // 
            // lblPagerInfo
            // 
            this.lblPagerInfo.AutoSize = true;
            // 
            // 
            // 
            this.lblPagerInfo.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblPagerInfo.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lblPagerInfo.ForeColor = System.Drawing.Color.Black;
            this.lblPagerInfo.Location = new System.Drawing.Point(9, 6);
            this.lblPagerInfo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.lblPagerInfo.Name = "lblPagerInfo";
            this.lblPagerInfo.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.lblPagerInfo.Size = new System.Drawing.Size(266, 20);
            this.lblPagerInfo.TabIndex = 99;
            this.lblPagerInfo.Text = "共 0 条记录，每页20 条，共 0 页，当前第 0 页";
            // 
            // btnFirst
            // 
            this.btnFirst.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnFirst.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnFirst.CommandParameter = "First";
            this.btnFirst.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnFirst.Location = new System.Drawing.Point(507, 5);
            this.btnFirst.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnFirst.Name = "btnFirst";
            this.btnFirst.Size = new System.Drawing.Size(64, 18);
            this.btnFirst.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnFirst.TabIndex = 98;
            this.btnFirst.Text = "|<";
            this.btnFirst.Click += new System.EventHandler(this.btnPagerCommand_Click);
            // 
            // btnPrevious
            // 
            this.btnPrevious.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnPrevious.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrevious.CommandParameter = "Previous";
            this.btnPrevious.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPrevious.Location = new System.Drawing.Point(581, 5);
            this.btnPrevious.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnPrevious.Name = "btnPrevious";
            this.btnPrevious.Size = new System.Drawing.Size(64, 18);
            this.btnPrevious.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnPrevious.TabIndex = 97;
            this.btnPrevious.Text = "<";
            this.btnPrevious.Click += new System.EventHandler(this.btnPagerCommand_Click);
            // 
            // btnNext
            // 
            this.btnNext.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNext.CommandParameter = "Next";
            this.btnNext.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnNext.Location = new System.Drawing.Point(655, 5);
            this.btnNext.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnNext.Name = "btnNext";
            this.btnNext.Size = new System.Drawing.Size(64, 18);
            this.btnNext.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnNext.TabIndex = 96;
            this.btnNext.Text = ">";
            this.btnNext.Click += new System.EventHandler(this.btnPagerCommand_Click);
            // 
            // btnLast
            // 
            this.btnLast.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnLast.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnLast.CommandParameter = "Last";
            this.btnLast.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnLast.Location = new System.Drawing.Point(728, 5);
            this.btnLast.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnLast.Name = "btnLast";
            this.btnLast.Size = new System.Drawing.Size(64, 18);
            this.btnLast.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnLast.TabIndex = 95;
            this.btnLast.Text = ">|";
            this.btnLast.Click += new System.EventHandler(this.btnPagerCommand_Click);
            // 
            // plnMain
            // 
            this.plnMain.Controls.Add(this.superGridControl1);
            this.plnMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.plnMain.Location = new System.Drawing.Point(3, 34);
            this.plnMain.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.plnMain.Name = "plnMain";
            this.plnMain.Size = new System.Drawing.Size(803, 350);
            this.plnMain.TabIndex = 2;
            // 
            // superGridControl1
            // 
            this.superGridControl1.BackColor = System.Drawing.Color.White;
            this.superGridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.superGridControl1.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
            this.superGridControl1.ForeColor = System.Drawing.Color.Black;
            this.superGridControl1.Location = new System.Drawing.Point(0, 0);
            this.superGridControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.superGridControl1.Name = "superGridControl1";
            this.superGridControl1.PrimaryGrid.Caption.Text = "";
            this.superGridControl1.PrimaryGrid.ColumnHeader.RowHeight = 30;
            gridColumn1.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gridColumn1.DataPropertyName = "MachineCode";
            gridColumn1.HeaderText = "设备编号";
            gridColumn1.MinimumWidth = 120;
            gridColumn1.Name = "";
            gridColumn1.Width = 120;
            gridColumn2.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gridColumn2.DataPropertyName = "BarrelCode";
            gridColumn2.HeaderText = "样罐编码";
            gridColumn2.MinimumWidth = 120;
            gridColumn2.Name = "";
            gridColumn2.Width = 120;
            gridColumn3.AutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.AllCells;
            gridColumn3.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gridColumn3.DataPropertyName = "YPType";
            gridColumn3.FillWeight = 80;
            gridColumn3.HeaderText = "样品类型";
            gridColumn3.MinimumWidth = 80;
            gridColumn3.Name = "";
            gridColumn3.Width = 80;
            gridColumn4.DataPropertyName = "YPWeight";
            gridColumn4.HeaderText = "样品重量";
            gridColumn4.Name = "";
            gridColumn4.Width = 80;
            gridColumn5.DataPropertyName = "MakeType";
            gridColumn5.HeaderText = "制样方式";
            gridColumn5.Name = "";
            gridColumn6.DataPropertyName = "StartTime";
            gridColumn6.HeaderText = "制样开始时间";
            gridColumn6.Name = "";
            gridColumn6.Width = 160;
            gridColumn7.DataPropertyName = "EndTime";
            gridColumn7.HeaderText = "制样结束时间";
            gridColumn7.Name = "";
            gridColumn7.Width = 160;
            gridColumn8.DataPropertyName = "MakeUser";
            gridColumn8.HeaderText = "制样员";
            gridColumn8.Name = "";
            gridColumn8.Width = 80;
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn1);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn2);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn3);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn4);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn5);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn6);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn7);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn8);
            this.superGridControl1.PrimaryGrid.SelectionGranularity = DevComponents.DotNetBar.SuperGrid.SelectionGranularity.Row;
            this.superGridControl1.Size = new System.Drawing.Size(803, 350);
            this.superGridControl1.TabIndex = 5;
            this.superGridControl1.Text = "superGridControl1";
            this.superGridControl1.BeginEdit += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridEditEventArgs>(this.superGridControl1_BeginEdit);
            // 
            // FrmAutoMakerRecord
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(809, 418);
            this.Controls.Add(this.tableLayoutPanel1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(825, 456);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(825, 456);
            this.Name = "FrmAutoMakerRecord";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "历史制样记录";
            this.Load += new System.EventHandler(this.FrmAutoMakerRecord_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.plnTop.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtInputEnd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtInputStart)).EndInit();
            this.plnBottom.ResumeLayout(false);
            this.plnBottom.PerformLayout();
            this.plnMain.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel plnTop;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput dtInputEnd;
        private DevComponents.DotNetBar.LabelX lblto;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput dtInputStart;
        private DevComponents.DotNetBar.ButtonX btnToday;
        private DevComponents.DotNetBar.ButtonX btnNextDay;
        private DevComponents.DotNetBar.ButtonX btnPreDay;
        private DevComponents.DotNetBar.Controls.TextBoxX txtBarrelCode_Ser;
        private DevComponents.DotNetBar.ButtonX btnSearch;
        private DevComponents.DotNetBar.ButtonX btnAll;
        private System.Windows.Forms.Panel plnBottom;
        private DevComponents.DotNetBar.LabelX lblPagerInfo;
        private DevComponents.DotNetBar.ButtonX btnFirst;
        private DevComponents.DotNetBar.ButtonX btnPrevious;
        private DevComponents.DotNetBar.ButtonX btnNext;
        private DevComponents.DotNetBar.ButtonX btnLast;
        private System.Windows.Forms.Panel plnMain;
        private DevComponents.DotNetBar.SuperGrid.SuperGridControl superGridControl1;
    }
}