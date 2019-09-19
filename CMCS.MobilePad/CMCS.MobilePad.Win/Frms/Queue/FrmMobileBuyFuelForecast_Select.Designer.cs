namespace CMCS.MobilePad.Win.Frms.Queue
{
    partial class FrmMobileBuyFuelForecast_Select
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
            DevComponents.DotNetBar.SuperGrid.Style.Background background4 = new DevComponents.DotNetBar.SuperGrid.Style.Background();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn31 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn32 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn33 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn34 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn35 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn36 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn37 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn38 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn39 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn40 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
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
            background4.Color1 = System.Drawing.Color.DarkTurquoise;
            this.superGridControl1.DefaultVisualStyles.RowStyles.Selected.Background = background4;
            this.superGridControl1.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
            this.superGridControl1.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.superGridControl1.ForeColor = System.Drawing.Color.White;
            this.superGridControl1.Location = new System.Drawing.Point(12, 51);
            this.superGridControl1.Name = "superGridControl1";
            this.superGridControl1.PrimaryGrid.AutoGenerateColumns = false;
            gridColumn31.DataPropertyName = "InFactoryType";
            gridColumn31.HeaderText = "入场类型";
            gridColumn31.Name = "";
            gridColumn32.DataPropertyName = "InFactoryTime";
            gridColumn32.HeaderText = "预计到厂时间";
            gridColumn32.Name = "";
            gridColumn32.Width = 150;
            gridColumn33.AutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.AllCells;
            gridColumn33.DataPropertyName = "SupplierName";
            gridColumn33.HeaderText = "供煤单位";
            gridColumn33.Name = "";
            gridColumn33.Width = 150;
            gridColumn34.AutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.AllCells;
            gridColumn34.DataPropertyName = "TransportCompanyName";
            gridColumn34.HeaderText = "运输单位";
            gridColumn34.Name = "clmTransportCompanyName";
            gridColumn34.Width = 150;
            gridColumn35.DataPropertyName = "MineName";
            gridColumn35.HeaderText = "矿点";
            gridColumn35.Name = "";
            gridColumn36.DataPropertyName = "FuelKindName";
            gridColumn36.HeaderText = "煤种";
            gridColumn36.Name = "";
            gridColumn37.DataPropertyName = "TransportNumber";
            gridColumn37.HeaderText = "车数";
            gridColumn37.Name = "";
            gridColumn37.Width = 80;
            gridColumn38.DataPropertyName = "CoalNumber";
            gridColumn38.HeaderText = "来煤量";
            gridColumn38.Name = "";
            gridColumn38.Width = 80;
            gridColumn39.AutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.AllCells;
            gridColumn39.DataPropertyName = "YbNum";
            gridColumn39.HeaderText = "预报编号";
            gridColumn39.Name = "";
            gridColumn39.Width = 120;
            gridColumn40.AutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.AllCells;
            gridColumn40.DataPropertyName = "Remark";
            gridColumn40.HeaderText = "备注";
            gridColumn40.Name = "";
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn31);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn32);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn33);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn34);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn35);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn36);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn37);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn38);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn39);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn40);
            this.superGridControl1.PrimaryGrid.DefaultRowHeight = 30;
            this.superGridControl1.PrimaryGrid.EnterKeySelectsNextRow = false;
            this.superGridControl1.PrimaryGrid.InitialSelection = DevComponents.DotNetBar.SuperGrid.RelativeSelection.Row;
            this.superGridControl1.PrimaryGrid.MultiSelect = false;
            this.superGridControl1.PrimaryGrid.SelectionGranularity = DevComponents.DotNetBar.SuperGrid.SelectionGranularity.Row;
            this.superGridControl1.Size = new System.Drawing.Size(1042, 259);
            this.superGridControl1.TabIndex = 4;
            this.superGridControl1.Text = "superGridControl1";
            this.superGridControl1.CellDoubleClick += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridCellDoubleClickEventArgs>(this.superGridControl1_CellDoubleClick);
            this.superGridControl1.DataBindingComplete += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridDataBindingCompleteEventArgs>(this.superGridControl1_DataBindingComplete);
            this.superGridControl1.BeginEdit += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridEditEventArgs>(this.superGridControl1_BeginEdit);
            this.superGridControl1.GetRowHeaderText += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridGetRowHeaderTextEventArgs>(this.superGridControl_GetRowHeaderText);
            this.superGridControl1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.superGridControl1_KeyUp);
            // 
            // btnNextDay
            // 
            this.btnNextDay.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnNextDay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnNextDay.Font = new System.Drawing.Font("Segoe UI", 13.25F);
            this.btnNextDay.Location = new System.Drawing.Point(970, 11);
            this.btnNextDay.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnNextDay.Name = "btnNextDay";
            this.btnNextDay.Size = new System.Drawing.Size(83, 28);
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
            this.btnToday.Location = new System.Drawing.Point(869, 11);
            this.btnToday.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnToday.Name = "btnToday";
            this.btnToday.Size = new System.Drawing.Size(80, 28);
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
            this.btnPrevDay.Location = new System.Drawing.Point(773, 11);
            this.btnPrevDay.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnPrevDay.Name = "btnPrevDay";
            this.btnPrevDay.Size = new System.Drawing.Size(75, 28);
            this.btnPrevDay.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnPrevDay.TabIndex = 20;
            this.btnPrevDay.Text = "上一天";
            this.btnPrevDay.Click += new System.EventHandler(this.btnPrevDay_Click);
            // 
            // timer1
            // 
            this.timer1.Interval = 500;
            // 
            // FrmMobileBuyFuelForecast_Select
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CaptionFont = new System.Drawing.Font("Segoe UI", 11.25F);
            this.ClientSize = new System.Drawing.Size(1065, 321);
            this.Controls.Add(this.btnPrevDay);
            this.Controls.Add(this.btnToday);
            this.Controls.Add(this.btnNextDay);
            this.Controls.Add(this.superGridControl1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "FrmMobileBuyFuelForecast_Select";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "汽车入场煤预报选择页";
            this.Shown += new System.EventHandler(this.FrmBuyFuelForecast_Select_Shown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FrmBuyFuelForecast_Select_KeyUp);
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