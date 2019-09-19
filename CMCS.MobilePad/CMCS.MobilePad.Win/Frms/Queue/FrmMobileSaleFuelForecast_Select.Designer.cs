namespace CMCS.MobilePad.Win.Frms.Queue
{
    partial class FrmMobileSaleFuelForecast_Select
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
            DevComponents.DotNetBar.SuperGrid.Style.Background background3 = new DevComponents.DotNetBar.SuperGrid.Style.Background();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn19 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn20 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn21 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn22 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn23 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn24 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn25 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn26 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn27 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            this.superGridControl1 = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
            this.btnNextDay = new DevComponents.DotNetBar.ButtonX();
            this.btnToday = new DevComponents.DotNetBar.ButtonX();
            this.btnPrevDay = new DevComponents.DotNetBar.ButtonX();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.SuspendLayout();
            // 
            // superGridControl1
            // 
            this.superGridControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
            this.superGridControl1.DefaultVisualStyles.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            this.superGridControl1.DefaultVisualStyles.CellStyles.Default.Font = new System.Drawing.Font("Segoe UI", 13.25F);
            this.superGridControl1.DefaultVisualStyles.ColumnHeaderStyles.Default.Font = new System.Drawing.Font("Segoe UI", 13.25F);
            background3.Color1 = System.Drawing.Color.DarkTurquoise;
            this.superGridControl1.DefaultVisualStyles.RowStyles.Selected.Background = background3;
            this.superGridControl1.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
            this.superGridControl1.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.superGridControl1.ForeColor = System.Drawing.Color.White;
            this.superGridControl1.Location = new System.Drawing.Point(12, 51);
            this.superGridControl1.Name = "superGridControl1";
            this.superGridControl1.PrimaryGrid.AutoGenerateColumns = false;
            gridColumn19.DataPropertyName = "ZcDate";
            gridColumn19.HeaderText = "预计装车日期";
            gridColumn19.Name = "";
            gridColumn19.Width = 140;
            gridColumn20.DataPropertyName = "InFactoryType";
            gridColumn20.HeaderText = "出场类型";
            gridColumn20.Name = "";
            gridColumn21.DataPropertyName = "SupplierName";
            gridColumn21.HeaderText = "接收单位";
            gridColumn21.Name = "clmFuelSupplierName";
            gridColumn21.Width = 200;
            gridColumn22.DataPropertyName = "TransportCompanyName";
            gridColumn22.HeaderText = "运输单位";
            gridColumn22.Name = "clmTransportCompanyName";
            gridColumn22.Width = 150;
            gridColumn23.DataPropertyName = "TransportNumber";
            gridColumn23.HeaderText = "车数";
            gridColumn23.Name = "";
            gridColumn23.Width = 80;
            gridColumn24.AutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.AllCells;
            gridColumn24.DataPropertyName = "HourStart";
            gridColumn24.HeaderText = "开始时段";
            gridColumn24.Name = "";
            gridColumn24.Width = 70;
            gridColumn25.AutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.AllCells;
            gridColumn25.DataPropertyName = "HourEnd";
            gridColumn25.HeaderText = "结束时段";
            gridColumn25.Name = "";
            gridColumn25.Width = 70;
            gridColumn26.DataPropertyName = "YbNum";
            gridColumn26.HeaderText = "预报编号";
            gridColumn26.Name = "";
            gridColumn26.Width = 200;
            gridColumn27.AutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.AllCells;
            gridColumn27.DataPropertyName = "Remark";
            gridColumn27.HeaderText = "备注";
            gridColumn27.Name = "";
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn19);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn20);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn21);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn22);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn23);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn24);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn25);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn26);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn27);
            this.superGridControl1.PrimaryGrid.DefaultRowHeight = 30;
            this.superGridControl1.PrimaryGrid.EnterKeySelectsNextRow = false;
            this.superGridControl1.PrimaryGrid.InitialSelection = DevComponents.DotNetBar.SuperGrid.RelativeSelection.Row;
            this.superGridControl1.PrimaryGrid.MultiSelect = false;
            this.superGridControl1.PrimaryGrid.SelectionGranularity = DevComponents.DotNetBar.SuperGrid.SelectionGranularity.Row;
            this.superGridControl1.Size = new System.Drawing.Size(1032, 259);
            this.superGridControl1.TabIndex = 4;
            this.superGridControl1.Text = "superGridControl1";
            this.superGridControl1.CellDoubleClick += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridCellDoubleClickEventArgs>(this.superGridControl1_CellDoubleClick);
            this.superGridControl1.DataBindingComplete += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridDataBindingCompleteEventArgs>(this.superGridControl_DataBindingComplete);
            this.superGridControl1.BeginEdit += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridEditEventArgs>(this.superGridControl1_BeginEdit);
            this.superGridControl1.GetRowHeaderText += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridGetRowHeaderTextEventArgs>(this.superGridControl_GetRowHeaderText);
            this.superGridControl1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.superGridControl1_KeyUp);
            // 
            // btnNextDay
            // 
            this.btnNextDay.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnNextDay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNextDay.Font = new System.Drawing.Font("Segoe UI", 13.25F);
            this.btnNextDay.Location = new System.Drawing.Point(966, 11);
            this.btnNextDay.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnNextDay.Name = "btnNextDay";
            this.btnNextDay.Size = new System.Drawing.Size(78, 28);
            this.btnNextDay.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnNextDay.TabIndex = 18;
            this.btnNextDay.Text = "下一天";
            this.btnNextDay.Click += new System.EventHandler(this.btnNextDay_Click);
            // 
            // btnToday
            // 
            this.btnToday.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnToday.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnToday.Font = new System.Drawing.Font("Segoe UI", 13.25F);
            this.btnToday.Location = new System.Drawing.Point(873, 11);
            this.btnToday.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnToday.Name = "btnToday";
            this.btnToday.Size = new System.Drawing.Size(78, 28);
            this.btnToday.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnToday.TabIndex = 19;
            this.btnToday.Text = "今 天";
            this.btnToday.Click += new System.EventHandler(this.btnToday_Click);
            // 
            // btnPrevDay
            // 
            this.btnPrevDay.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnPrevDay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrevDay.Font = new System.Drawing.Font("Segoe UI", 13.25F);
            this.btnPrevDay.Location = new System.Drawing.Point(777, 11);
            this.btnPrevDay.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnPrevDay.Name = "btnPrevDay";
            this.btnPrevDay.Size = new System.Drawing.Size(81, 28);
            this.btnPrevDay.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnPrevDay.TabIndex = 20;
            this.btnPrevDay.Text = "上一天";
            this.btnPrevDay.Click += new System.EventHandler(this.btnPrevDay_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FrmMobileSaleFuelForecast_Select
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CaptionFont = new System.Drawing.Font("Segoe UI", 11.25F);
            this.ClientSize = new System.Drawing.Size(1056, 321);
            this.Controls.Add(this.btnPrevDay);
            this.Controls.Add(this.btnToday);
            this.Controls.Add(this.btnNextDay);
            this.Controls.Add(this.superGridControl1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "FrmMobileSaleFuelForecast_Select";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "汽车出场煤预报选择页";
            this.Load += new System.EventHandler(this.FrmSaleFuelForecast_Select_Load);
            this.Shown += new System.EventHandler(this.FrmSaleFuelForecast_Select_Shown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FrmSaleFuelForecast_Select_KeyUp);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.SuperGrid.SuperGridControl superGridControl1;
        private DevComponents.DotNetBar.ButtonX btnNextDay;
        private DevComponents.DotNetBar.ButtonX btnToday;
        private DevComponents.DotNetBar.ButtonX btnPrevDay;
        private System.Windows.Forms.Timer timer1;

    }
}