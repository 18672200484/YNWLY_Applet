namespace CMCS.WeighCheck.SampleMake.Frms.JDYMake
{
    partial class FrmJDYMake_List
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
			DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn9 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
			DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn10 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
			DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn11 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
			DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn12 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
			this.superGridControl1 = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
			this.btnPrevious = new DevComponents.DotNetBar.ButtonX();
			this.btnFirst = new DevComponents.DotNetBar.ButtonX();
			this.btnLast = new DevComponents.DotNetBar.ButtonX();
			this.btnNext = new DevComponents.DotNetBar.ButtonX();
			this.btnSearch = new DevComponents.DotNetBar.ButtonX();
			this.panel2 = new System.Windows.Forms.Panel();
			this.lblPagerInfo = new DevComponents.DotNetBar.LabelX();
			this.superGridControl2 = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
			this.splitContainer2 = new System.Windows.Forms.SplitContainer();
			this.splitContainer1 = new System.Windows.Forms.SplitContainer();
			this.panel1 = new System.Windows.Forms.Panel();
			this.slightRwer = new CMCS.Forms.UserControls.UCtrlSignalLight();
			this.label1 = new System.Windows.Forms.Label();
			this.labelX1 = new DevComponents.DotNetBar.LabelX();
			this.dt_StartTime = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
			this.dt_EndTime = new DevComponents.Editors.DateTimeAdv.DateTimeInput();
			this.BtnAdd = new DevComponents.DotNetBar.ButtonX();
			this.txt_SearchMakeCode = new DevComponents.DotNetBar.Controls.TextBoxX();
			this.txt_SearchSendUnitName = new DevComponents.DotNetBar.Controls.TextBoxX();
			this.btn_All = new DevComponents.DotNetBar.ButtonX();
			this.panel2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).BeginInit();
			this.splitContainer2.Panel1.SuspendLayout();
			this.splitContainer2.Panel2.SuspendLayout();
			this.splitContainer2.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
			this.splitContainer1.Panel1.SuspendLayout();
			this.splitContainer1.Panel2.SuspendLayout();
			this.splitContainer1.SuspendLayout();
			this.panel1.SuspendLayout();
			((System.ComponentModel.ISupportInitialize)(this.dt_StartTime)).BeginInit();
			((System.ComponentModel.ISupportInitialize)(this.dt_EndTime)).BeginInit();
			this.SuspendLayout();
			// 
			// superGridControl1
			// 
			this.superGridControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
			this.superGridControl1.DefaultVisualStyles.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
			this.superGridControl1.DefaultVisualStyles.CellStyles.Default.Font = new System.Drawing.Font("Segoe UI", 11.25F);
			this.superGridControl1.DefaultVisualStyles.ColumnHeaderStyles.Default.Font = new System.Drawing.Font("Segoe UI", 11.25F);
			this.superGridControl1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.superGridControl1.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
			this.superGridControl1.ForeColor = System.Drawing.Color.White;
			this.superGridControl1.Location = new System.Drawing.Point(0, 0);
			this.superGridControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.superGridControl1.Name = "superGridControl1";
			this.superGridControl1.PrimaryGrid.AutoGenerateColumns = false;
			this.superGridControl1.PrimaryGrid.Caption.Text = "";
			gridColumn1.CellStyles.Default.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Underline, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
			gridColumn1.DefaultNewRowCellValue = "查看";
			gridColumn1.HeaderText = "";
			gridColumn1.InfoImageAlignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
			gridColumn1.Name = "clmShow";
			gridColumn1.NullString = "查看";
			gridColumn1.Width = 32;
			gridColumn2.CellStyles.Default.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Underline);
			gridColumn2.DefaultNewRowCellValue = "修改";
			gridColumn2.HeaderText = "";
			gridColumn2.InfoImageAlignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
			gridColumn2.Name = "clmEdit";
			gridColumn2.NullString = "修改";
			gridColumn2.Width = 32;
			gridColumn3.CellStyles.Default.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Underline);
			gridColumn3.DefaultNewRowCellValue = "删除";
			gridColumn3.HeaderText = "";
			gridColumn3.InfoImageAlignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
			gridColumn3.Name = "clmDelete";
			gridColumn3.NullString = "删除";
			gridColumn3.Width = 32;
			gridColumn4.AutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.AllCells;
			gridColumn4.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
			gridColumn4.DataPropertyName = "MakeCode";
			gridColumn4.HeaderText = "制样码";
			gridColumn4.InfoImageAlignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
			gridColumn4.Name = "clmMakeCode";
			gridColumn4.Width = 80;
			gridColumn5.AutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.AllCells;
			gridColumn5.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
			gridColumn5.DataPropertyName = "GetPle";
			gridColumn5.HeaderText = "接样人";
			gridColumn5.InfoImageAlignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
			gridColumn5.Name = "";
			gridColumn5.Width = 80;
			gridColumn6.AutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.AllCells;
			gridColumn6.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
			gridColumn6.DataPropertyName = "SendUnit";
			gridColumn6.HeaderText = "送样单位";
			gridColumn6.InfoImageAlignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
			gridColumn6.Name = "";
			gridColumn6.Width = 80;
			gridColumn7.AutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.AllCells;
			gridColumn7.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
			gridColumn7.DataPropertyName = "GetDate";
			gridColumn7.HeaderText = "接样时间";
			gridColumn7.InfoImageAlignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
			gridColumn7.Name = "";
			gridColumn7.Width = 80;
			gridColumn8.AutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.AllCells;
			gridColumn8.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
			gridColumn8.DataPropertyName = "MakeStartTime";
			gridColumn8.HeaderText = "制样开始时间";
			gridColumn8.InfoImageAlignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
			gridColumn8.Name = "";
			gridColumn8.Width = 80;
			gridColumn9.AutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.AllCells;
			gridColumn9.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
			gridColumn9.DataPropertyName = "UseTime";
			gridColumn9.HeaderText = "实际采样时间";
			gridColumn9.InfoImageAlignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
			gridColumn9.Name = "";
			gridColumn9.Width = 80;
			gridColumn10.AutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.AllCells;
			gridColumn10.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
			gridColumn10.DataPropertyName = "GetBarrelWeight";
			gridColumn10.HeaderText = "接样重量";
			gridColumn10.InfoImageAlignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
			gridColumn10.Name = "";
			gridColumn10.Width = 80;
			gridColumn11.AutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.Fill;
			gridColumn11.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
			gridColumn11.DataPropertyName = "Remark";
			gridColumn11.HeaderText = "备注";
			gridColumn11.InfoImageAlignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
			gridColumn11.Name = "";
			gridColumn11.Width = 80;
			gridColumn12.DataPropertyName = "Id";
			gridColumn12.Name = "clmId";
			gridColumn12.Visible = false;
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
			this.superGridControl1.PrimaryGrid.InitialSelection = DevComponents.DotNetBar.SuperGrid.RelativeSelection.Row;
			this.superGridControl1.PrimaryGrid.SelectionGranularity = DevComponents.DotNetBar.SuperGrid.SelectionGranularity.Row;
			this.superGridControl1.Size = new System.Drawing.Size(1035, 571);
			this.superGridControl1.TabIndex = 4;
			this.superGridControl1.Text = "superGridControl1";
			this.superGridControl1.CellMouseDown += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridCellMouseEventArgs>(this.superGridControl1_CellMouseDown);
			this.superGridControl1.BeginEdit += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridEditEventArgs>(this.superGridControl1_BeginEdit);
			// 
			// btnPrevious
			// 
			this.btnPrevious.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnPrevious.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnPrevious.CommandParameter = "Previous";
			this.btnPrevious.Font = new System.Drawing.Font("Segoe UI", 11.25F);
			this.btnPrevious.Location = new System.Drawing.Point(820, 9);
			this.btnPrevious.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.btnPrevious.Name = "btnPrevious";
			this.btnPrevious.Size = new System.Drawing.Size(64, 23);
			this.btnPrevious.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.btnPrevious.TabIndex = 104;
			this.btnPrevious.Text = "<";
			this.btnPrevious.Click += new System.EventHandler(this.btnPagerCommand_Click);
			// 
			// btnFirst
			// 
			this.btnFirst.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnFirst.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnFirst.CommandParameter = "First";
			this.btnFirst.Font = new System.Drawing.Font("Segoe UI", 11.25F);
			this.btnFirst.Location = new System.Drawing.Point(750, 9);
			this.btnFirst.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.btnFirst.Name = "btnFirst";
			this.btnFirst.Size = new System.Drawing.Size(64, 23);
			this.btnFirst.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.btnFirst.TabIndex = 103;
			this.btnFirst.Text = "|<";
			this.btnFirst.Click += new System.EventHandler(this.btnPagerCommand_Click);
			// 
			// btnLast
			// 
			this.btnLast.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnLast.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnLast.CommandParameter = "Last";
			this.btnLast.Font = new System.Drawing.Font("Segoe UI", 11.25F);
			this.btnLast.Location = new System.Drawing.Point(959, 9);
			this.btnLast.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.btnLast.Name = "btnLast";
			this.btnLast.Size = new System.Drawing.Size(64, 23);
			this.btnLast.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.btnLast.TabIndex = 101;
			this.btnLast.Text = ">|";
			this.btnLast.Click += new System.EventHandler(this.btnPagerCommand_Click);
			// 
			// btnNext
			// 
			this.btnNext.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnNext.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnNext.CommandParameter = "Next";
			this.btnNext.Font = new System.Drawing.Font("Segoe UI", 11.25F);
			this.btnNext.Location = new System.Drawing.Point(889, 9);
			this.btnNext.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.btnNext.Name = "btnNext";
			this.btnNext.Size = new System.Drawing.Size(64, 23);
			this.btnNext.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.btnNext.TabIndex = 100;
			this.btnNext.Text = ">";
			this.btnNext.Click += new System.EventHandler(this.btnPagerCommand_Click);
			// 
			// btnSearch
			// 
			this.btnSearch.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btnSearch.Location = new System.Drawing.Point(899, 44);
			this.btnSearch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.btnSearch.Name = "btnSearch";
			this.btnSearch.Size = new System.Drawing.Size(64, 23);
			this.btnSearch.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.btnSearch.TabIndex = 13;
			this.btnSearch.Text = "搜 索";
			this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
			// 
			// panel2
			// 
			this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(82)))), ((int)(((byte)(89)))));
			this.panel2.Controls.Add(this.btnPrevious);
			this.panel2.Controls.Add(this.btnFirst);
			this.panel2.Controls.Add(this.btnLast);
			this.panel2.Controls.Add(this.btnNext);
			this.panel2.Controls.Add(this.lblPagerInfo);
			this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel2.ForeColor = System.Drawing.Color.White;
			this.panel2.Location = new System.Drawing.Point(0, 0);
			this.panel2.Margin = new System.Windows.Forms.Padding(2);
			this.panel2.Name = "panel2";
			this.panel2.Size = new System.Drawing.Size(1035, 40);
			this.panel2.TabIndex = 1;
			// 
			// lblPagerInfo
			// 
			this.lblPagerInfo.AutoSize = true;
			this.lblPagerInfo.BackColor = System.Drawing.Color.Transparent;
			// 
			// 
			// 
			this.lblPagerInfo.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.lblPagerInfo.Font = new System.Drawing.Font("Segoe UI", 11.25F);
			this.lblPagerInfo.ForeColor = System.Drawing.Color.White;
			this.lblPagerInfo.Location = new System.Drawing.Point(7, 8);
			this.lblPagerInfo.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.lblPagerInfo.Name = "lblPagerInfo";
			this.lblPagerInfo.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
			this.lblPagerInfo.Size = new System.Drawing.Size(338, 24);
			this.lblPagerInfo.TabIndex = 99;
			this.lblPagerInfo.Text = "共 0 条记录，每页20 条，共 0 页，当前第 0 页";
			// 
			// superGridControl2
			// 
			this.superGridControl2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
			this.superGridControl2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.superGridControl2.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
			this.superGridControl2.ForeColor = System.Drawing.Color.White;
			this.superGridControl2.Location = new System.Drawing.Point(0, 0);
			this.superGridControl2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.superGridControl2.Name = "superGridControl2";
			this.superGridControl2.PrimaryGrid.Caption.Text = "";
			this.superGridControl2.Size = new System.Drawing.Size(1035, 571);
			this.superGridControl2.TabIndex = 3;
			this.superGridControl2.Text = "superGridControl1";
			// 
			// splitContainer2
			// 
			this.splitContainer2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
			this.splitContainer2.Dock = System.Windows.Forms.DockStyle.Fill;
			this.splitContainer2.FixedPanel = System.Windows.Forms.FixedPanel.Panel2;
			this.splitContainer2.ForeColor = System.Drawing.Color.White;
			this.splitContainer2.IsSplitterFixed = true;
			this.splitContainer2.Location = new System.Drawing.Point(0, 0);
			this.splitContainer2.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.splitContainer2.Name = "splitContainer2";
			this.splitContainer2.Orientation = System.Windows.Forms.Orientation.Horizontal;
			// 
			// splitContainer2.Panel1
			// 
			this.splitContainer2.Panel1.BackColor = System.Drawing.Color.Transparent;
			this.splitContainer2.Panel1.Controls.Add(this.superGridControl1);
			this.splitContainer2.Panel1.Controls.Add(this.superGridControl2);
			this.splitContainer2.Panel1.ForeColor = System.Drawing.Color.White;
			// 
			// splitContainer2.Panel2
			// 
			this.splitContainer2.Panel2.BackColor = System.Drawing.Color.Transparent;
			this.splitContainer2.Panel2.Controls.Add(this.panel2);
			this.splitContainer2.Panel2.ForeColor = System.Drawing.Color.White;
			this.splitContainer2.Panel2MinSize = 40;
			this.splitContainer2.Size = new System.Drawing.Size(1035, 612);
			this.splitContainer2.SplitterDistance = 571;
			this.splitContainer2.SplitterWidth = 1;
			this.splitContainer2.TabIndex = 0;
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
			this.splitContainer1.Panel2.Controls.Add(this.splitContainer2);
			this.splitContainer1.Panel2.ForeColor = System.Drawing.Color.White;
			this.splitContainer1.Panel2.RightToLeft = System.Windows.Forms.RightToLeft.No;
			this.splitContainer1.Size = new System.Drawing.Size(1035, 693);
			this.splitContainer1.SplitterDistance = 80;
			this.splitContainer1.SplitterWidth = 1;
			this.splitContainer1.TabIndex = 147;
			// 
			// panel1
			// 
			this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(82)))), ((int)(((byte)(89)))));
			this.panel1.Controls.Add(this.slightRwer);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Controls.Add(this.labelX1);
			this.panel1.Controls.Add(this.dt_StartTime);
			this.panel1.Controls.Add(this.dt_EndTime);
			this.panel1.Controls.Add(this.BtnAdd);
			this.panel1.Controls.Add(this.txt_SearchMakeCode);
			this.panel1.Controls.Add(this.txt_SearchSendUnitName);
			this.panel1.Controls.Add(this.btn_All);
			this.panel1.Controls.Add(this.btnSearch);
			this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.panel1.ForeColor = System.Drawing.Color.White;
			this.panel1.Location = new System.Drawing.Point(0, 0);
			this.panel1.Margin = new System.Windows.Forms.Padding(2);
			this.panel1.Name = "panel1";
			this.panel1.Size = new System.Drawing.Size(1035, 80);
			this.panel1.TabIndex = 12;
			// 
			// slightRwer
			// 
			this.slightRwer.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.slightRwer.BackColor = System.Drawing.Color.Transparent;
			this.slightRwer.ForeColor = System.Drawing.Color.White;
			this.slightRwer.LightColor = System.Drawing.Color.Gray;
			this.slightRwer.Location = new System.Drawing.Point(948, 9);
			this.slightRwer.Name = "slightRwer";
			this.slightRwer.Size = new System.Drawing.Size(20, 20);
			this.slightRwer.TabIndex = 260;
			// 
			// label1
			// 
			this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.label1.AutoSize = true;
			this.label1.BackColor = System.Drawing.Color.Transparent;
			this.label1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.label1.ForeColor = System.Drawing.Color.White;
			this.label1.Location = new System.Drawing.Point(973, 9);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(57, 20);
			this.label1.TabIndex = 261;
			this.label1.Text = "读卡器";
			// 
			// labelX1
			// 
			this.labelX1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.labelX1.BackColor = System.Drawing.Color.Transparent;
			// 
			// 
			// 
			this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.labelX1.Font = new System.Drawing.Font("Segoe UI", 11.25F);
			this.labelX1.ForeColor = System.Drawing.Color.White;
			this.labelX1.Location = new System.Drawing.Point(431, 44);
			this.labelX1.Name = "labelX1";
			this.labelX1.Size = new System.Drawing.Size(90, 23);
			this.labelX1.TabIndex = 259;
			this.labelX1.Text = "制样日期：";
			// 
			// dt_StartTime
			// 
			this.dt_StartTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.dt_StartTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
			// 
			// 
			// 
			this.dt_StartTime.BackgroundStyle.Class = "DateTimeInputBackground";
			this.dt_StartTime.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.dt_StartTime.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
			this.dt_StartTime.ButtonDropDown.Visible = true;
			this.dt_StartTime.CustomFormat = "yyyy-MM-dd";
			this.dt_StartTime.Font = new System.Drawing.Font("Segoe UI", 11.25F);
			this.dt_StartTime.ForeColor = System.Drawing.Color.White;
			this.dt_StartTime.Format = DevComponents.Editors.eDateTimePickerFormat.Custom;
			this.dt_StartTime.IsPopupCalendarOpen = false;
			this.dt_StartTime.Location = new System.Drawing.Point(527, 42);
			// 
			// 
			// 
			this.dt_StartTime.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
			// 
			// 
			// 
			this.dt_StartTime.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.dt_StartTime.MonthCalendar.CalendarDimensions = new System.Drawing.Size(1, 1);
			this.dt_StartTime.MonthCalendar.ClearButtonVisible = true;
			// 
			// 
			// 
			this.dt_StartTime.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
			this.dt_StartTime.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
			this.dt_StartTime.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
			this.dt_StartTime.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
			this.dt_StartTime.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
			this.dt_StartTime.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
			this.dt_StartTime.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.dt_StartTime.MonthCalendar.DisplayMonth = new System.DateTime(2019, 3, 1, 0, 0, 0, 0);
			this.dt_StartTime.MonthCalendar.FirstDayOfWeek = System.DayOfWeek.Monday;
			this.dt_StartTime.MonthCalendar.MarkedDates = new System.DateTime[0];
			this.dt_StartTime.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
			// 
			// 
			// 
			this.dt_StartTime.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
			this.dt_StartTime.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
			this.dt_StartTime.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.dt_StartTime.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.dt_StartTime.MonthCalendar.TodayButtonVisible = true;
			this.dt_StartTime.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
			this.dt_StartTime.Name = "dt_StartTime";
			this.dt_StartTime.Size = new System.Drawing.Size(180, 27);
			this.dt_StartTime.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.dt_StartTime.TabIndex = 258;
			// 
			// dt_EndTime
			// 
			this.dt_EndTime.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.dt_EndTime.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
			// 
			// 
			// 
			this.dt_EndTime.BackgroundStyle.Class = "DateTimeInputBackground";
			this.dt_EndTime.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.dt_EndTime.ButtonDropDown.Shortcut = DevComponents.DotNetBar.eShortcut.AltDown;
			this.dt_EndTime.ButtonDropDown.Visible = true;
			this.dt_EndTime.CustomFormat = "yyyy-MM-dd";
			this.dt_EndTime.Font = new System.Drawing.Font("Segoe UI", 11.25F);
			this.dt_EndTime.ForeColor = System.Drawing.Color.White;
			this.dt_EndTime.Format = DevComponents.Editors.eDateTimePickerFormat.Custom;
			this.dt_EndTime.IsPopupCalendarOpen = false;
			this.dt_EndTime.Location = new System.Drawing.Point(713, 42);
			// 
			// 
			// 
			this.dt_EndTime.MonthCalendar.AnnuallyMarkedDates = new System.DateTime[0];
			// 
			// 
			// 
			this.dt_EndTime.MonthCalendar.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.dt_EndTime.MonthCalendar.CalendarDimensions = new System.Drawing.Size(1, 1);
			this.dt_EndTime.MonthCalendar.ClearButtonVisible = true;
			// 
			// 
			// 
			this.dt_EndTime.MonthCalendar.CommandsBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground2;
			this.dt_EndTime.MonthCalendar.CommandsBackgroundStyle.BackColorGradientAngle = 90;
			this.dt_EndTime.MonthCalendar.CommandsBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarBackground;
			this.dt_EndTime.MonthCalendar.CommandsBackgroundStyle.BorderTop = DevComponents.DotNetBar.eStyleBorderType.Solid;
			this.dt_EndTime.MonthCalendar.CommandsBackgroundStyle.BorderTopColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.BarDockedBorder;
			this.dt_EndTime.MonthCalendar.CommandsBackgroundStyle.BorderTopWidth = 1;
			this.dt_EndTime.MonthCalendar.CommandsBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.dt_EndTime.MonthCalendar.DisplayMonth = new System.DateTime(2019, 3, 1, 0, 0, 0, 0);
			this.dt_EndTime.MonthCalendar.FirstDayOfWeek = System.DayOfWeek.Monday;
			this.dt_EndTime.MonthCalendar.MarkedDates = new System.DateTime[0];
			this.dt_EndTime.MonthCalendar.MonthlyMarkedDates = new System.DateTime[0];
			// 
			// 
			// 
			this.dt_EndTime.MonthCalendar.NavigationBackgroundStyle.BackColor2SchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground2;
			this.dt_EndTime.MonthCalendar.NavigationBackgroundStyle.BackColorGradientAngle = 90;
			this.dt_EndTime.MonthCalendar.NavigationBackgroundStyle.BackColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
			this.dt_EndTime.MonthCalendar.NavigationBackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.dt_EndTime.MonthCalendar.TodayButtonVisible = true;
			this.dt_EndTime.MonthCalendar.WeeklyMarkedDays = new System.DayOfWeek[0];
			this.dt_EndTime.Name = "dt_EndTime";
			this.dt_EndTime.Size = new System.Drawing.Size(180, 27);
			this.dt_EndTime.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.dt_EndTime.TabIndex = 258;
			// 
			// BtnAdd
			// 
			this.BtnAdd.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.BtnAdd.Font = new System.Drawing.Font("Segoe UI", 11.25F);
			this.BtnAdd.Location = new System.Drawing.Point(10, 45);
			this.BtnAdd.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.BtnAdd.Name = "BtnAdd";
			this.BtnAdd.Size = new System.Drawing.Size(64, 23);
			this.BtnAdd.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.BtnAdd.TabIndex = 18;
			this.BtnAdd.Text = "新 增";
			this.BtnAdd.Click += new System.EventHandler(this.BtnAdd_Click);
			// 
			// txt_SearchMakeCode
			// 
			this.txt_SearchMakeCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.txt_SearchMakeCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
			// 
			// 
			// 
			this.txt_SearchMakeCode.Border.Class = "TextBoxBorder";
			this.txt_SearchMakeCode.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.txt_SearchMakeCode.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txt_SearchMakeCode.ForeColor = System.Drawing.Color.White;
			this.txt_SearchMakeCode.Location = new System.Drawing.Point(272, 41);
			this.txt_SearchMakeCode.Name = "txt_SearchMakeCode";
			this.txt_SearchMakeCode.Size = new System.Drawing.Size(150, 27);
			this.txt_SearchMakeCode.TabIndex = 14;
			this.txt_SearchMakeCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txt_SearchMakeCode.WatermarkImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
			this.txt_SearchMakeCode.WatermarkText = "请扫描制样码...";
			// 
			// txt_SearchSendUnitName
			// 
			this.txt_SearchSendUnitName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.txt_SearchSendUnitName.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
			// 
			// 
			// 
			this.txt_SearchSendUnitName.Border.Class = "TextBoxBorder";
			this.txt_SearchSendUnitName.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
			this.txt_SearchSendUnitName.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.txt_SearchSendUnitName.ForeColor = System.Drawing.Color.White;
			this.txt_SearchSendUnitName.Location = new System.Drawing.Point(116, 41);
			this.txt_SearchSendUnitName.Name = "txt_SearchSendUnitName";
			this.txt_SearchSendUnitName.Size = new System.Drawing.Size(150, 27);
			this.txt_SearchSendUnitName.TabIndex = 14;
			this.txt_SearchSendUnitName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
			this.txt_SearchSendUnitName.WatermarkImageAlignment = System.Drawing.ContentAlignment.MiddleCenter;
			this.txt_SearchSendUnitName.WatermarkText = "请输入送样单位...";
			// 
			// btn_All
			// 
			this.btn_All.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
			this.btn_All.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
			this.btn_All.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.btn_All.Location = new System.Drawing.Point(966, 44);
			this.btn_All.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
			this.btn_All.Name = "btn_All";
			this.btn_All.Size = new System.Drawing.Size(64, 23);
			this.btn_All.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
			this.btn_All.TabIndex = 13;
			this.btn_All.Text = "全 部";
			this.btn_All.Click += new System.EventHandler(this.btnAll_Click);
			// 
			// FrmJDYMake_List
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(1036, 695);
			this.Controls.Add(this.splitContainer1);
			this.DoubleBuffered = true;
			this.Font = new System.Drawing.Font("Segoe UI", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
			this.Name = "FrmJDYMake_List";
			this.Text = "监管样制样";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmJDYMake_List_FormClosed);
			this.Load += new System.EventHandler(this.FrmJDYMake_List_Load);
			this.Shown += new System.EventHandler(this.FrmJDYMake_List_Shown);
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.splitContainer2.Panel1.ResumeLayout(false);
			this.splitContainer2.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer2)).EndInit();
			this.splitContainer2.ResumeLayout(false);
			this.splitContainer1.Panel1.ResumeLayout(false);
			this.splitContainer1.Panel2.ResumeLayout(false);
			((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
			this.splitContainer1.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			((System.ComponentModel.ISupportInitialize)(this.dt_StartTime)).EndInit();
			((System.ComponentModel.ISupportInitialize)(this.dt_EndTime)).EndInit();
			this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.SuperGrid.SuperGridControl superGridControl1;
        private DevComponents.DotNetBar.ButtonX btnPrevious;
        private DevComponents.DotNetBar.ButtonX btnFirst;
        private DevComponents.DotNetBar.ButtonX btnLast;
        private DevComponents.DotNetBar.ButtonX btnNext;
        private DevComponents.DotNetBar.ButtonX btnSearch;
        private System.Windows.Forms.Panel panel2;
        private DevComponents.DotNetBar.LabelX lblPagerInfo;
        private DevComponents.DotNetBar.SuperGrid.SuperGridControl superGridControl2;
        private System.Windows.Forms.SplitContainer splitContainer2;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Panel panel1;
        private DevComponents.DotNetBar.ButtonX BtnAdd;
        private DevComponents.DotNetBar.Controls.TextBoxX txt_SearchSendUnitName;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput dt_StartTime;
        private DevComponents.Editors.DateTimeAdv.DateTimeInput dt_EndTime;
        private DevComponents.DotNetBar.Controls.TextBoxX txt_SearchMakeCode;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.ButtonX btn_All;
		private Forms.UserControls.UCtrlSignalLight slightRwer;
		private System.Windows.Forms.Label label1;
	}
}