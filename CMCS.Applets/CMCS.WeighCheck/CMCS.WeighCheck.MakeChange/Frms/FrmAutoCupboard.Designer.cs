﻿namespace CMCS.WeighCheck.MakeChange.Frms
{
    partial class FrmAutoCupboard
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
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn5 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn6 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn7 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn8 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn9 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn10 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmAutoCupboard));
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.slightCYG = new CMCS.Forms.UserControls.UCtrlSignalLight();
            this.slightCYG2 = new CMCS.Forms.UserControls.UCtrlSignalLight();
            this.pnlExMain = new DevComponents.DotNetBar.PanelEx();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.lblWber = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.superGridControl1 = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.txtMakeCode = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.btnAll = new DevComponents.DotNetBar.ButtonX();
            this.btnSearch = new DevComponents.DotNetBar.ButtonX();
            this.rtxtMakeWeightInfo = new DevComponents.DotNetBar.Controls.RichTextBoxEx();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.pnlExMain.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panelEx1.SuspendLayout();
            this.panelEx2.SuspendLayout();
            this.SuspendLayout();
            // 
            // slightCYG
            // 
            this.slightCYG.BackColor = System.Drawing.Color.Transparent;
            this.slightCYG.ForeColor = System.Drawing.Color.White;
            this.slightCYG.LightColor = System.Drawing.Color.Gray;
            this.slightCYG.Location = new System.Drawing.Point(0, 1);
            this.slightCYG.Name = "slightCYG";
            this.slightCYG.Size = new System.Drawing.Size(20, 20);
            this.slightCYG.TabIndex = 221;
            this.toolTip1.SetToolTip(this.slightCYG, "<绿色> 已连接\r\n<红色> 未连接");
            // 
            // slightCYG2
            // 
            this.slightCYG2.BackColor = System.Drawing.Color.Transparent;
            this.slightCYG2.ForeColor = System.Drawing.Color.White;
            this.slightCYG2.LightColor = System.Drawing.Color.Gray;
            this.slightCYG2.Location = new System.Drawing.Point(104, 1);
            this.slightCYG2.Name = "slightCYG2";
            this.slightCYG2.Size = new System.Drawing.Size(20, 20);
            this.slightCYG2.TabIndex = 229;
            this.toolTip1.SetToolTip(this.slightCYG2, "<绿色> 已连接\r\n<红色> 未连接");
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
            this.pnlExMain.Size = new System.Drawing.Size(943, 430);
            this.pnlExMain.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.pnlExMain.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(58)))), ((int)(((byte)(63)))));
            this.pnlExMain.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.pnlExMain.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.pnlExMain.Style.GradientAngle = 90;
            this.pnlExMain.TabIndex = 0;
            this.pnlExMain.Text = "存样柜";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Controls.Add(this.panelEx1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.superGridControl1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.panelEx2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.rtxtMakeWeightInfo, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.ForeColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 30F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 40F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 80F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(943, 430);
            this.tableLayoutPanel1.TabIndex = 233;
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.slightCYG);
            this.panelEx1.Controls.Add(this.lblWber);
            this.panelEx1.Controls.Add(this.slightCYG2);
            this.panelEx1.Controls.Add(this.label1);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(3, 3);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(937, 24);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 0;
            // 
            // lblWber
            // 
            this.lblWber.AutoSize = true;
            this.lblWber.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblWber.ForeColor = System.Drawing.Color.White;
            this.lblWber.Location = new System.Drawing.Point(25, 1);
            this.lblWber.Name = "lblWber";
            this.lblWber.Size = new System.Drawing.Size(74, 20);
            this.lblWber.TabIndex = 222;
            this.lblWber.Text = "#1存样柜";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.White;
            this.label1.Location = new System.Drawing.Point(130, 1);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 20);
            this.label1.TabIndex = 230;
            this.label1.Text = "#2存样柜";
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
            this.superGridControl1.Location = new System.Drawing.Point(0, 70);
            this.superGridControl1.Margin = new System.Windows.Forms.Padding(0);
            this.superGridControl1.Name = "superGridControl1";
            this.superGridControl1.PrimaryGrid.AutoGenerateColumns = false;
            gridColumn1.DataPropertyName = "Code";
            gridColumn1.HeaderText = "样品编码";
            gridColumn1.Name = "";
            gridColumn1.Width = 160;
            gridColumn2.HeaderText = "化验码";
            gridColumn2.Name = "clmAssayCode";
            gridColumn2.Width = 160;
            gridColumn3.DataPropertyName = "SamType";
            gridColumn3.HeaderText = "样瓶类型";
            gridColumn3.Name = "";
            gridColumn3.Width = 80;
            gridColumn4.DataPropertyName = "MachineCode";
            gridColumn4.HeaderText = "存样柜";
            gridColumn4.Name = "";
            gridColumn5.DataPropertyName = "CellIndex";
            gridColumn5.HeaderText = "层";
            gridColumn5.Name = "";
            gridColumn5.Width = 60;
            gridColumn6.DataPropertyName = "ColumnIndex";
            gridColumn6.HeaderText = "列";
            gridColumn6.Name = "";
            gridColumn6.Width = 60;
            gridColumn7.DataPropertyName = "AreaNumber";
            gridColumn7.HeaderText = "区域";
            gridColumn7.Name = "";
            gridColumn7.Width = 60;
            gridColumn8.DataPropertyName = "UpdateTime";
            gridColumn8.HeaderText = "存样时间";
            gridColumn8.Name = "";
            gridColumn8.Width = 120;
            gridColumn9.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridButtonXEditControl);
            gridColumn9.HeaderText = "";
            gridColumn9.Name = "gclmTakeOut";
            gridColumn9.NullString = "取样";
            gridColumn9.RenderType = typeof(DevComponents.DotNetBar.SuperGrid.GridButtonXEditControl);
            gridColumn9.Width = 80;
            gridColumn10.EditorType = typeof(DevComponents.DotNetBar.SuperGrid.GridButtonXEditControl);
            gridColumn10.HeaderText = "";
            gridColumn10.Name = "gclmPutAway";
            gridColumn10.NullString = "弃样";
            gridColumn10.RenderType = typeof(DevComponents.DotNetBar.SuperGrid.GridButtonXEditControl);
            gridColumn10.Visible = false;
            gridColumn10.Width = 80;
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
            this.superGridControl1.PrimaryGrid.EnterKeySelectsNextRow = false;
            this.superGridControl1.PrimaryGrid.InitialSelection = DevComponents.DotNetBar.SuperGrid.RelativeSelection.Row;
            this.superGridControl1.PrimaryGrid.MultiSelect = false;
            this.superGridControl1.PrimaryGrid.NoRowsText = "";
            this.superGridControl1.PrimaryGrid.SelectionGranularity = DevComponents.DotNetBar.SuperGrid.SelectionGranularity.Row;
            this.superGridControl1.Size = new System.Drawing.Size(943, 280);
            this.superGridControl1.TabIndex = 223;
            this.superGridControl1.Text = "superGridControl1";
            this.superGridControl1.DataBindingComplete += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridDataBindingCompleteEventArgs>(this.superGridControl1_DataBindingComplete);
            this.superGridControl1.GetRowHeaderText += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridGetRowHeaderTextEventArgs>(this.superGridControl1_GetRowHeaderText);
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.txtMakeCode);
            this.panelEx2.Controls.Add(this.btnAll);
            this.panelEx2.Controls.Add(this.btnSearch);
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx2.Location = new System.Drawing.Point(3, 33);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(937, 34);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 1;
            // 
            // txtMakeCode
            // 
            this.txtMakeCode.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.txtMakeCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
            // 
            // 
            // 
            this.txtMakeCode.Border.Class = "TextBoxBorder";
            this.txtMakeCode.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtMakeCode.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMakeCode.ForeColor = System.Drawing.Color.White;
            this.txtMakeCode.Location = new System.Drawing.Point(592, 4);
            this.txtMakeCode.Name = "txtMakeCode";
            this.txtMakeCode.Size = new System.Drawing.Size(171, 27);
            this.txtMakeCode.TabIndex = 232;
            this.txtMakeCode.WatermarkText = "制样码......";
            // 
            // btnAll
            // 
            this.btnAll.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnAll.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnAll.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.btnAll.Location = new System.Drawing.Point(854, 4);
            this.btnAll.Name = "btnAll";
            this.btnAll.Size = new System.Drawing.Size(79, 27);
            this.btnAll.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnAll.TabIndex = 231;
            this.btnAll.Text = "全  部";
            this.btnAll.Click += new System.EventHandler(this.btnAll_Click);
            // 
            // btnSearch
            // 
            this.btnSearch.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSearch.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSearch.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.btnSearch.Location = new System.Drawing.Point(769, 4);
            this.btnSearch.Name = "btnSearch";
            this.btnSearch.Size = new System.Drawing.Size(79, 27);
            this.btnSearch.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSearch.TabIndex = 231;
            this.btnSearch.Text = "搜  索";
            this.btnSearch.Click += new System.EventHandler(this.btnSearch_Click);
            // 
            // rtxtMakeWeightInfo
            // 
            // 
            // 
            // 
            this.rtxtMakeWeightInfo.BackgroundStyle.Class = "RichTextBoxBorder";
            this.rtxtMakeWeightInfo.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.rtxtMakeWeightInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxtMakeWeightInfo.Font = new System.Drawing.Font("微软雅黑", 10.5F);
            this.rtxtMakeWeightInfo.ForeColor = System.Drawing.Color.White;
            this.rtxtMakeWeightInfo.Location = new System.Drawing.Point(3, 353);
            this.rtxtMakeWeightInfo.Name = "rtxtMakeWeightInfo";
            this.rtxtMakeWeightInfo.ReadOnly = true;
            this.rtxtMakeWeightInfo.Size = new System.Drawing.Size(937, 74);
            this.rtxtMakeWeightInfo.TabIndex = 213;
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 10000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // FrmAutoCupboard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
            this.ClientSize = new System.Drawing.Size(943, 430);
            this.Controls.Add(this.pnlExMain);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.Name = "FrmAutoCupboard";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "存样柜";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmMakeWeight_FormClosing);
            this.Load += new System.EventHandler(this.FrmMakeWeight_Load);
            this.pnlExMain.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            this.panelEx2.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.ToolTip toolTip1;
        private DevComponents.DotNetBar.PanelEx pnlExMain;
        private DevComponents.DotNetBar.Controls.RichTextBoxEx rtxtMakeWeightInfo;
        private System.Windows.Forms.Label lblWber;
        private Forms.UserControls.UCtrlSignalLight slightCYG;
        private DevComponents.DotNetBar.SuperGrid.SuperGridControl superGridControl1;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label1;
        private Forms.UserControls.UCtrlSignalLight slightCYG2;
        private DevComponents.DotNetBar.ButtonX btnSearch;
        private DevComponents.DotNetBar.Controls.TextBoxX txtMakeCode;
        private DevComponents.DotNetBar.ButtonX btnAll;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private DevComponents.DotNetBar.PanelEx panelEx2;
    }
}

