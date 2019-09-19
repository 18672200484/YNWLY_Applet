namespace CMCS.CarTransport.Queue.Frms
{
    partial class FrmSaleFuelForecast_Select
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
            this.superGridControl1.DefaultVisualStyles.CellStyles.Default.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.superGridControl1.DefaultVisualStyles.ColumnHeaderStyles.Default.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            background1.Color1 = System.Drawing.Color.DarkTurquoise;
            this.superGridControl1.DefaultVisualStyles.RowStyles.Selected.Background = background1;
            this.superGridControl1.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
            this.superGridControl1.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.superGridControl1.ForeColor = System.Drawing.Color.White;
            this.superGridControl1.Location = new System.Drawing.Point(12, 51);
            this.superGridControl1.Name = "superGridControl1";
            this.superGridControl1.PrimaryGrid.AutoGenerateColumns = false;
            gridColumn1.DataPropertyName = "ZcDate";
            gridColumn1.HeaderText = "预计装车日期";
            gridColumn1.Name = "";
            gridColumn1.Width = 140;
            gridColumn2.DataPropertyName = "InFactoryType";
            gridColumn2.HeaderText = "出场类型";
            gridColumn2.Name = "";
            gridColumn3.DataPropertyName = "SupplierName";
            gridColumn3.HeaderText = "接收单位";
            gridColumn3.Name = "clmFuelSupplierName";
            gridColumn3.Width = 200;
            gridColumn4.DataPropertyName = "TransportCompanyName";
            gridColumn4.HeaderText = "运输单位";
            gridColumn4.Name = "clmTransportCompanyName";
            gridColumn4.Width = 150;
            gridColumn5.DataPropertyName = "TransportNumber";
            gridColumn5.HeaderText = "车数";
            gridColumn5.Name = "";
            gridColumn5.Width = 80;
            gridColumn6.DataPropertyName = "HourStart";
            gridColumn6.HeaderText = "开始时段";
            gridColumn6.Name = "";
            gridColumn6.Width = 70;
            gridColumn7.DataPropertyName = "HourEnd";
            gridColumn7.HeaderText = "结束时段";
            gridColumn7.Name = "";
            gridColumn7.Width = 70;
            gridColumn8.DataPropertyName = "YbNum";
            gridColumn8.HeaderText = "预报编号";
            gridColumn8.Name = "";
            gridColumn8.Width = 200;
            gridColumn9.AutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.AllCells;
            gridColumn9.DataPropertyName = "Remark";
            gridColumn9.HeaderText = "备注";
            gridColumn9.Name = "";
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn1);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn2);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn3);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn4);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn5);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn6);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn7);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn8);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn9);
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
            this.btnNextDay.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.btnNextDay.Location = new System.Drawing.Point(980, 16);
            this.btnNextDay.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnNextDay.Name = "btnNextDay";
            this.btnNextDay.Size = new System.Drawing.Size(64, 23);
            this.btnNextDay.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnNextDay.TabIndex = 18;
            this.btnNextDay.Text = "下一天";
            this.btnNextDay.Click += new System.EventHandler(this.btnNextDay_Click);
            // 
            // btnToday
            // 
            this.btnToday.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnToday.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnToday.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.btnToday.Location = new System.Drawing.Point(905, 16);
            this.btnToday.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnToday.Name = "btnToday";
            this.btnToday.Size = new System.Drawing.Size(64, 23);
            this.btnToday.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnToday.TabIndex = 19;
            this.btnToday.Text = "今 天";
            this.btnToday.Click += new System.EventHandler(this.btnToday_Click);
            // 
            // btnPrevDay
            // 
            this.btnPrevDay.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnPrevDay.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnPrevDay.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.btnPrevDay.Location = new System.Drawing.Point(830, 16);
            this.btnPrevDay.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnPrevDay.Name = "btnPrevDay";
            this.btnPrevDay.Size = new System.Drawing.Size(64, 23);
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
            // FrmSaleFuelForecast_Select
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1056, 321);
            this.Controls.Add(this.btnPrevDay);
            this.Controls.Add(this.btnToday);
            this.Controls.Add(this.btnNextDay);
            this.Controls.Add(this.superGridControl1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.Name = "FrmSaleFuelForecast_Select";
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