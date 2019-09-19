namespace CMCS.WeighCheck.MakeChange.Frms
{
    partial class FrmSpotCheck
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmSpotCheck));
            this.printDocument1 = new System.Drawing.Printing.PrintDocument();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.pnlExMain = new DevComponents.DotNetBar.PanelEx();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.btnPrint = new DevComponents.DotNetBar.ButtonX();
            this.btn_SpotCheck = new DevComponents.DotNetBar.ButtonX();
            this.btnReset = new DevComponents.DotNetBar.ButtonX();
            this.txtInputAssayCode = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.rtxtMakeWeightInfo = new DevComponents.DotNetBar.Controls.RichTextBoxEx();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.picEncode = new System.Windows.Forms.PictureBox();
            this.superGridControl1 = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.txtInputSpotAssayCode = new DevComponents.DotNetBar.Controls.TextBoxX();
            this.pnlExMain.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panelEx2.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picEncode)).BeginInit();
            this.SuspendLayout();
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
            this.pnlExMain.Size = new System.Drawing.Size(1501, 610);
            this.pnlExMain.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.pnlExMain.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(53)))), ((int)(((byte)(58)))), ((int)(((byte)(63)))));
            this.pnlExMain.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.pnlExMain.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.pnlExMain.Style.GradientAngle = 90;
            this.pnlExMain.TabIndex = 0;
            this.pnlExMain.Text = "制样转码";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panelEx2, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.rtxtMakeWeightInfo, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 0, 1);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.ForeColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 3;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 90F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1501, 610);
            this.tableLayoutPanel1.TabIndex = 231;
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.txtInputSpotAssayCode);
            this.panelEx2.Controls.Add(this.btnPrint);
            this.panelEx2.Controls.Add(this.btn_SpotCheck);
            this.panelEx2.Controls.Add(this.btnReset);
            this.panelEx2.Controls.Add(this.txtInputAssayCode);
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx2.Location = new System.Drawing.Point(3, 3);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(1495, 94);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 1;
            // 
            // btnPrint
            // 
            this.btnPrint.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnPrint.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnPrint.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.btnPrint.Location = new System.Drawing.Point(1006, 54);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(96, 38);
            this.btnPrint.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnPrint.TabIndex = 228;
            this.btnPrint.Text = "打   印";
            this.btnPrint.Tooltip = "快捷键 F2";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // btn_SpotCheck
            // 
            this.btn_SpotCheck.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btn_SpotCheck.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btn_SpotCheck.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.btn_SpotCheck.Location = new System.Drawing.Point(1006, 5);
            this.btn_SpotCheck.Name = "btn_SpotCheck";
            this.btn_SpotCheck.Size = new System.Drawing.Size(108, 39);
            this.btn_SpotCheck.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btn_SpotCheck.TabIndex = 227;
            this.btn_SpotCheck.Text = "生成抽查样";
            this.btn_SpotCheck.Click += new System.EventHandler(this.btn_SpotCheck_Click);
            // 
            // btnReset
            // 
            this.btnReset.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnReset.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btnReset.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.btnReset.Location = new System.Drawing.Point(1123, 5);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(86, 39);
            this.btnReset.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnReset.TabIndex = 224;
            this.btnReset.Text = "重   置";
            this.btnReset.Tooltip = "快捷键 Esc";
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // txtInputAssayCode
            // 
            this.txtInputAssayCode.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtInputAssayCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
            // 
            // 
            // 
            this.txtInputAssayCode.Border.Class = "TextBoxBorder";
            this.txtInputAssayCode.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtInputAssayCode.ButtonCustom.Text = "清空";
            this.txtInputAssayCode.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.txtInputAssayCode.ForeColor = System.Drawing.Color.White;
            this.txtInputAssayCode.Location = new System.Drawing.Point(282, 5);
            this.txtInputAssayCode.Name = "txtInputAssayCode";
            this.txtInputAssayCode.Size = new System.Drawing.Size(711, 39);
            this.txtInputAssayCode.TabIndex = 0;
            this.txtInputAssayCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtInputAssayCode.WatermarkText = "化验码. . .";
            this.txtInputAssayCode.TextChanged += new System.EventHandler(this.txtInputMakeCode_TextChanged);
            this.txtInputAssayCode.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtInputMakeCode_KeyUp);
            this.txtInputAssayCode.MouseMove += new System.Windows.Forms.MouseEventHandler(this.txtInputMakeCode_MouseMove);
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
            this.rtxtMakeWeightInfo.Location = new System.Drawing.Point(3, 523);
            this.rtxtMakeWeightInfo.Name = "rtxtMakeWeightInfo";
            this.rtxtMakeWeightInfo.ReadOnly = true;
            this.rtxtMakeWeightInfo.Size = new System.Drawing.Size(1495, 84);
            this.rtxtMakeWeightInfo.TabIndex = 213;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 220F));
            this.tableLayoutPanel2.Controls.Add(this.picEncode, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.superGridControl1, 0, 0);
            this.tableLayoutPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel2.ForeColor = System.Drawing.Color.White;
            this.tableLayoutPanel2.Location = new System.Drawing.Point(3, 103);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 1;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(1495, 414);
            this.tableLayoutPanel2.TabIndex = 214;
            // 
            // picEncode
            // 
            this.picEncode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
            this.picEncode.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picEncode.ForeColor = System.Drawing.Color.White;
            this.picEncode.Location = new System.Drawing.Point(1278, 3);
            this.picEncode.Name = "picEncode";
            this.picEncode.Size = new System.Drawing.Size(200, 200);
            this.picEncode.TabIndex = 225;
            this.picEncode.TabStop = false;
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
            this.superGridControl1.Location = new System.Drawing.Point(0, 0);
            this.superGridControl1.Margin = new System.Windows.Forms.Padding(0);
            this.superGridControl1.Name = "superGridControl1";
            this.superGridControl1.PrimaryGrid.AutoGenerateColumns = false;
            gridColumn1.DataPropertyName = "BackBatchDate";
            gridColumn1.HeaderText = "归批时间";
            gridColumn1.Name = "";
            gridColumn1.Width = 200;
            gridColumn2.DataPropertyName = "AssayCode";
            gridColumn2.HeaderText = "原化验码";
            gridColumn2.Name = "";
            gridColumn2.Width = 150;
            gridColumn3.DataPropertyName = "SpotAssayCode";
            gridColumn3.HeaderText = "抽查样化验码";
            gridColumn3.Name = "clmSpotCode";
            gridColumn3.Width = 150;
            gridColumn4.DataPropertyName = "CheckUser";
            gridColumn4.HeaderText = "操作人";
            gridColumn4.Name = "";
            gridColumn4.Width = 200;
            gridColumn5.DataPropertyName = "SpotCount";
            gridColumn5.HeaderText = "抽查次数";
            gridColumn5.Name = "";
            gridColumn5.Width = 80;
            gridColumn6.AutoSizeMode = DevComponents.DotNetBar.SuperGrid.ColumnAutoSizeMode.AllCells;
            gridColumn6.DataPropertyName = "AssayPoint";
            gridColumn6.HeaderText = "测试项目";
            gridColumn6.Name = "clmAssayPoint";
            gridColumn6.Width = 200;
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn1);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn2);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn3);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn4);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn5);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn6);
            this.superGridControl1.PrimaryGrid.EnterKeySelectsNextRow = false;
            this.superGridControl1.PrimaryGrid.InitialSelection = DevComponents.DotNetBar.SuperGrid.RelativeSelection.Row;
            this.superGridControl1.PrimaryGrid.MultiSelect = false;
            this.superGridControl1.PrimaryGrid.NoRowsText = "";
            this.superGridControl1.PrimaryGrid.SelectionGranularity = DevComponents.DotNetBar.SuperGrid.SelectionGranularity.Row;
            this.superGridControl1.Size = new System.Drawing.Size(1275, 414);
            this.superGridControl1.TabIndex = 223;
            this.superGridControl1.Text = "superGridControl1";
            this.superGridControl1.BeginEdit += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridEditEventArgs>(this.superGridControl1_BeginEdit);
            this.superGridControl1.GetRowHeaderText += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridGetRowHeaderTextEventArgs>(this.superGridControl_GetRowHeaderText);
            // 
            // timer1
            // 
            this.timer1.Interval = 2000;
            // 
            // txtInputSpotAssayCode
            // 
            this.txtInputSpotAssayCode.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txtInputSpotAssayCode.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
            // 
            // 
            // 
            this.txtInputSpotAssayCode.Border.Class = "TextBoxBorder";
            this.txtInputSpotAssayCode.Border.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.txtInputSpotAssayCode.ButtonCustom.Text = "清空";
            this.txtInputSpotAssayCode.Font = new System.Drawing.Font("Segoe UI", 18F);
            this.txtInputSpotAssayCode.ForeColor = System.Drawing.Color.White;
            this.txtInputSpotAssayCode.Location = new System.Drawing.Point(282, 52);
            this.txtInputSpotAssayCode.Name = "txtInputSpotAssayCode";
            this.txtInputSpotAssayCode.Size = new System.Drawing.Size(711, 39);
            this.txtInputSpotAssayCode.TabIndex = 229;
            this.txtInputSpotAssayCode.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.txtInputSpotAssayCode.WatermarkText = "对应抽查样化验码. . .";
            // 
            // FrmSpotCheck
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
            this.ClientSize = new System.Drawing.Size(1501, 610);
            this.Controls.Add(this.pnlExMain);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 8.25F);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MinimumSize = new System.Drawing.Size(938, 443);
            this.Name = "FrmSpotCheck";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "化验抽查样";
            this.Load += new System.EventHandler(this.FrmSpotCheck_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.FrmMakeChange_Paint);
            this.pnlExMain.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panelEx2.ResumeLayout(false);
            this.tableLayoutPanel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picEncode)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Drawing.Printing.PrintDocument printDocument1;
        private System.Windows.Forms.ToolTip toolTip1;
        private DevComponents.DotNetBar.PanelEx pnlExMain;
        private DevComponents.DotNetBar.Controls.RichTextBoxEx rtxtMakeWeightInfo;
        private DevComponents.DotNetBar.Controls.TextBoxX txtInputAssayCode;
        private DevComponents.DotNetBar.SuperGrid.SuperGridControl superGridControl1;
        private System.Windows.Forms.PictureBox picEncode;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private DevComponents.DotNetBar.ButtonX btn_SpotCheck;
        private DevComponents.DotNetBar.ButtonX btnReset;
        private DevComponents.DotNetBar.ButtonX btnPrint;
        private DevComponents.DotNetBar.Controls.TextBoxX txtInputSpotAssayCode;
    }
}

