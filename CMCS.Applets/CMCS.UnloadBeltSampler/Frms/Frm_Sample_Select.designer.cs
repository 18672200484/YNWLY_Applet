namespace CMCS.UnloadSampler.Frms
{
    partial class Frm_Sample_Select
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Frm_Sample_Select));
            this.lvwInfactoryBatch = new DevComponents.DotNetBar.Controls.ListViewEx();
            this.clmBatch = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmMineName = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmSamplingDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmTransportNumber = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmFuelKind = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.clmFactarriveDate = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.metroShell1 = new DevComponents.DotNetBar.Metro.MetroShell();
            this.splitMain = new System.Windows.Forms.SplitContainer();
            this.btnToday = new DevComponents.DotNetBar.ButtonX();
            this.btnNextDay = new DevComponents.DotNetBar.ButtonX();
            this.btnPreDay = new DevComponents.DotNetBar.ButtonX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.dtInputEnd = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.lblto = new DevComponents.DotNetBar.LabelX();
            this.dtInputStart = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
            this.btnSearch = new DevComponents.DotNetBar.ButtonX();
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).BeginInit();
            this.splitMain.Panel1.SuspendLayout();
            this.splitMain.Panel2.SuspendLayout();
            this.splitMain.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtInputEnd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtInputStart)).BeginInit();
            this.SuspendLayout();
            // 
            // lvwInfactoryBatch
            // 
            this.lvwInfactoryBatch.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.lvwInfactoryBatch.Border.Class = "ListViewBorder";
            this.lvwInfactoryBatch.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lvwInfactoryBatch.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.clmBatch,
            this.clmName,
            this.clmMineName,
            this.clmSamplingDate,
            this.clmTransportNumber,
            this.clmFuelKind,
            this.clmFactarriveDate});
            this.lvwInfactoryBatch.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lvwInfactoryBatch.Font = new System.Drawing.Font("微软雅黑", 13F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.lvwInfactoryBatch.ForeColor = System.Drawing.Color.Black;
            this.lvwInfactoryBatch.FullRowSelect = true;
            this.lvwInfactoryBatch.GridLines = true;
            this.lvwInfactoryBatch.Location = new System.Drawing.Point(0, 0);
            this.lvwInfactoryBatch.MultiSelect = false;
            this.lvwInfactoryBatch.Name = "lvwInfactoryBatch";
            this.lvwInfactoryBatch.Size = new System.Drawing.Size(920, 336);
            this.lvwInfactoryBatch.TabIndex = 173;
            this.lvwInfactoryBatch.UseCompatibleStateImageBehavior = false;
            this.lvwInfactoryBatch.View = System.Windows.Forms.View.Details;
            this.lvwInfactoryBatch.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.lvwInfactoryBatch_MouseDoubleClick);
            // 
            // clmBatch
            // 
            this.clmBatch.Text = "批次号";
            this.clmBatch.Width = 120;
            // 
            // clmName
            // 
            this.clmName.Text = "供应商名称";
            this.clmName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clmName.Width = 250;
            // 
            // clmMineName
            // 
            this.clmMineName.Text = "矿点";
            this.clmMineName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clmMineName.Width = 120;
            // 
            // clmSamplingDate
            // 
            this.clmSamplingDate.Text = "采样日期";
            this.clmSamplingDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clmSamplingDate.Width = 140;
            // 
            // clmTransportNumber
            // 
            this.clmTransportNumber.Text = "车数";
            this.clmTransportNumber.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clmTransportNumber.Width = 70;
            // 
            // clmFuelKind
            // 
            this.clmFuelKind.Text = "煤种";
            this.clmFuelKind.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clmFuelKind.Width = 80;
            // 
            // clmFactarriveDate
            // 
            this.clmFactarriveDate.Text = "入厂日期";
            this.clmFactarriveDate.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.clmFactarriveDate.Width = 140;
            // 
            // metroShell1
            // 
            this.metroShell1.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.metroShell1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.metroShell1.CaptionVisible = true;
            this.metroShell1.Dock = System.Windows.Forms.DockStyle.Top;
            this.metroShell1.ForeColor = System.Drawing.Color.Black;
            this.metroShell1.HelpButtonText = null;
            this.metroShell1.HelpButtonVisible = false;
            this.metroShell1.KeyTipsFont = new System.Drawing.Font("Tahoma", 7F);
            this.metroShell1.Location = new System.Drawing.Point(1, 1);
            this.metroShell1.Name = "metroShell1";
            this.metroShell1.SettingsButtonVisible = false;
            this.metroShell1.Size = new System.Drawing.Size(920, 32);
            this.metroShell1.SystemText.MaximizeRibbonText = "&Maximize the Ribbon";
            this.metroShell1.SystemText.MinimizeRibbonText = "Mi&nimize the Ribbon";
            this.metroShell1.SystemText.QatAddItemText = "&Add to Quick Access Toolbar";
            this.metroShell1.SystemText.QatCustomizeMenuLabel = "<b>Customize Quick Access Toolbar</b>";
            this.metroShell1.SystemText.QatCustomizeText = "&Customize Quick Access Toolbar...";
            this.metroShell1.SystemText.QatDialogAddButton = "&Add >>";
            this.metroShell1.SystemText.QatDialogCancelButton = "Cancel";
            this.metroShell1.SystemText.QatDialogCaption = "Customize Quick Access Toolbar";
            this.metroShell1.SystemText.QatDialogCategoriesLabel = "&Choose commands from:";
            this.metroShell1.SystemText.QatDialogOkButton = "OK";
            this.metroShell1.SystemText.QatDialogPlacementCheckbox = "&Place Quick Access Toolbar below the Ribbon";
            this.metroShell1.SystemText.QatDialogRemoveButton = "&Remove";
            this.metroShell1.SystemText.QatPlaceAboveRibbonText = "&Place Quick Access Toolbar above the Ribbon";
            this.metroShell1.SystemText.QatPlaceBelowRibbonText = "&Place Quick Access Toolbar below the Ribbon";
            this.metroShell1.SystemText.QatRemoveItemText = "&Remove from Quick Access Toolbar";
            this.metroShell1.TabIndex = 174;
            this.metroShell1.TabStripFont = new System.Drawing.Font("Segoe UI", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.metroShell1.Text = "metroShell1";
            // 
            // splitMain
            // 
            this.splitMain.BackColor = System.Drawing.Color.Transparent;
            this.splitMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.splitMain.ForeColor = System.Drawing.Color.Black;
            this.splitMain.Location = new System.Drawing.Point(1, 33);
            this.splitMain.Name = "splitMain";
            this.splitMain.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitMain.Panel1
            // 
            this.splitMain.Panel1.BackColor = System.Drawing.Color.Transparent;
            this.splitMain.Panel1.Controls.Add(this.btnToday);
            this.splitMain.Panel1.Controls.Add(this.btnNextDay);
            this.splitMain.Panel1.Controls.Add(this.btnPreDay);
            this.splitMain.Panel1.Controls.Add(this.labelX1);
            this.splitMain.Panel1.Controls.Add(this.dtInputEnd);
            this.splitMain.Panel1.Controls.Add(this.lblto);
            this.splitMain.Panel1.Controls.Add(this.dtInputStart);
            this.splitMain.Panel1.Controls.Add(this.btnSearch);
            this.splitMain.Panel1.ForeColor = System.Drawing.Color.Black;
            // 
            // splitMain.Panel2
            // 
            this.splitMain.Panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(216)))), ((int)(((byte)(216)))), ((int)(((byte)(216)))));
            this.splitMain.Panel2.Controls.Add(this.lvwInfactoryBatch);
            this.splitMain.Panel2.ForeColor = System.Drawing.Color.Black;
            this.splitMain.Size = new System.Drawing.Size(920, 379);
            this.splitMain.SplitterDistance = 42;
            this.splitMain.SplitterWidth = 1;
            this.splitMain.TabIndex = 175;
            // 
            // btnToday
            // 
            this.btnToday.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnToday.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnToday.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnToday.Location = new System.Drawing.Point(246, 3);
            this.btnToday.Name = "btnToday";
            this.btnToday.Size = new System.Drawing.Size(92, 36);
            this.btnToday.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnToday.TabIndex = 177;
            this.btnToday.Text = "今天";
            this.btnToday.Click += new System.EventHandler(this.btnToday_Click);
            // 
            // btnNextDay
            // 
            this.btnNextDay.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnNextDay.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnNextDay.Font = new System.Drawing.Font("微软雅黑", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnNextDay.Location = new System.Drawing.Point(129, 3);
            this.btnNextDay.Name = "btnNextDay";
            this.btnNextDay.Size = new System.Drawing.Size(92, 36);
            this.btnNextDay.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnNextDay.TabIndex = 176;
            this.btnNextDay.Text = "下一天";
            this.btnNextDay.Click += new System.EventHandler(this.btnNextDay_Click);
            // 
            // btnPreDay
            // 
            this.btnPreDay.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnPreDay.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnPreDay.Font = new System.Drawing.Font("微软雅黑", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnPreDay.Location = new System.Drawing.Point(14, 3);
            this.btnPreDay.Name = "btnPreDay";
            this.btnPreDay.Size = new System.Drawing.Size(92, 36);
            this.btnPreDay.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnPreDay.TabIndex = 175;
            this.btnPreDay.Text = "上一天";
            this.btnPreDay.Click += new System.EventHandler(this.btnPreDay_Click);
            // 
            // labelX1
            // 
            this.labelX1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.labelX1.ForeColor = System.Drawing.Color.Black;
            this.labelX1.Location = new System.Drawing.Point(473, 10);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(89, 23);
            this.labelX1.TabIndex = 174;
            this.labelX1.Text = "采样日期：";
            this.labelX1.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // dtInputEnd
            // 
            this.dtInputEnd.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
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
            this.dtInputEnd.Location = new System.Drawing.Point(708, 11);
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
            this.dtInputEnd.Size = new System.Drawing.Size(102, 21);
            this.dtInputEnd.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.dtInputEnd.TabIndex = 173;
            this.dtInputEnd.TimeSelectorTimeFormat = DevComponents.Editors.DateTimeAdv.eTimeSelectorFormat.Time24H;
            // 
            // lblto
            // 
            this.lblto.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            // 
            // 
            // 
            this.lblto.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lblto.ForeColor = System.Drawing.Color.Black;
            this.lblto.Location = new System.Drawing.Point(688, 10);
            this.lblto.Name = "lblto";
            this.lblto.Size = new System.Drawing.Size(13, 23);
            this.lblto.TabIndex = 172;
            this.lblto.Text = "至";
            // 
            // dtInputStart
            // 
            this.dtInputStart.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
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
            this.dtInputStart.Location = new System.Drawing.Point(568, 11);
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
            this.dtInputStart.Size = new System.Drawing.Size(102, 21);
            this.dtInputStart.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.dtInputStart.TabIndex = 171;
            this.dtInputStart.TimeSelectorTimeFormat = DevComponents.Editors.DateTimeAdv.eTimeSelectorFormat.Time24H;
            // 
            // btnSearch
            // 
            this.btnSearch.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSearch.Font = new System.Drawing.Font("微软雅黑", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnSearch.Location = new System.Drawing.Point(833, 10);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(75, 23);
            this.btnSearch.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSearch.TabIndex = 5;
            this.btnSearch.Text = "搜索";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // Frm_Sample_Select
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(922, 413);
            this.Controls.Add(this.splitMain);
            this.Controls.Add(this.metroShell1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(922, 413);
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(922, 413);
            this.Name = "Frm_Sample_Select";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "入厂煤采样选择";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Frm_SupplierUnit_Selet_FormClosing);
            this.Load += new System.EventHandler(this.Frm_Batch_Select_Load);
            this.splitMain.Panel1.ResumeLayout(false);
            this.splitMain.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitMain)).EndInit();
            this.splitMain.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtInputEnd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtInputStart)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Controls.ListViewEx lvwInfactoryBatch;
        private System.Windows.Forms.ColumnHeader clmBatch;
        private System.Windows.Forms.ColumnHeader clmMineName;
        private DevComponents.DotNetBar.Metro.MetroShell metroShell1;
        private System.Windows.Forms.ColumnHeader clmName;
        private System.Windows.Forms.SplitContainer splitMain;
        private DevComponents.DotNetBar.ButtonX btnSearch;
        private System.Windows.Forms.ColumnHeader clmSamplingDate;
        private System.Windows.Forms.ColumnHeader clmTransportNumber;
        private System.Windows.Forms.ColumnHeader clmFuelKind;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput dtInputEnd;
        private DevComponents.DotNetBar.LabelX lblto;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput dtInputStart;
        private DevComponents.DotNetBar.ButtonX btnToday;
        private DevComponents.DotNetBar.ButtonX btnNextDay;
        private DevComponents.DotNetBar.ButtonX btnPreDay;
        private System.Windows.Forms.ColumnHeader clmFactarriveDate;
    }
}