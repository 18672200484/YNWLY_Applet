namespace CMCS.FingerIdentify.Frms
{
    partial class FrmFingerRegist
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmFingerRegist));
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.pnlExMain = new DevComponents.DotNetBar.PanelEx();
            this.btnSubmit = new DevComponents.DotNetBar.ButtonX();
            this.cmbFingerName = new DevComponents.DotNetBar.Controls.ComboBoxEx();
            this.comboItem1 = new DevComponents.Editors.ComboItem();
            this.comboItem2 = new DevComponents.Editors.ComboItem();
            this.comboItem3 = new DevComponents.Editors.ComboItem();
            this.comboItem4 = new DevComponents.Editors.ComboItem();
            this.comboItem5 = new DevComponents.Editors.ComboItem();
            this.comboItem6 = new DevComponents.Editors.ComboItem();
            this.comboItem7 = new DevComponents.Editors.ComboItem();
            this.comboItem8 = new DevComponents.Editors.ComboItem();
            this.comboItem9 = new DevComponents.Editors.ComboItem();
            this.comboItem10 = new DevComponents.Editors.ComboItem();
            this.superGridControl1 = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
            this.lbeUserName = new DevComponents.DotNetBar.LabelX();
            this.labelX2 = new DevComponents.DotNetBar.LabelX();
            this.labelX1 = new DevComponents.DotNetBar.LabelX();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnClose = new DevComponents.DotNetBar.ButtonX();
            this.btnOpen = new DevComponents.DotNetBar.ButtonX();
            this.btnRegistFinger = new DevComponents.DotNetBar.ButtonX();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.rTxTMessageInfo = new DevComponents.DotNetBar.Controls.RichTextBoxEx();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.picFinger = new System.Windows.Forms.PictureBox();
            this.styleManager1 = new DevComponents.DotNetBar.StyleManager(this.components);
            this.labelItem2 = new DevComponents.DotNetBar.LabelItem();
            this.lblLoginUserName = new DevComponents.DotNetBar.LabelItem();
            this.labelItem1 = new DevComponents.DotNetBar.LabelItem();
            this.labelItem3 = new DevComponents.DotNetBar.LabelItem();
            this.labelItem4 = new DevComponents.DotNetBar.LabelItem();
            this.labelItem5 = new DevComponents.DotNetBar.LabelItem();
            this.pnlExMain.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picFinger)).BeginInit();
            this.SuspendLayout();
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 3000;
            // 
            // timer2
            // 
            this.timer2.Interval = 1000;
            // 
            // pnlExMain
            // 
            this.pnlExMain.CanvasColor = System.Drawing.SystemColors.Control;
            this.pnlExMain.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.pnlExMain.Controls.Add(this.btnSubmit);
            this.pnlExMain.Controls.Add(this.cmbFingerName);
            this.pnlExMain.Controls.Add(this.superGridControl1);
            this.pnlExMain.Controls.Add(this.lbeUserName);
            this.pnlExMain.Controls.Add(this.labelX2);
            this.pnlExMain.Controls.Add(this.labelX1);
            this.pnlExMain.Controls.Add(this.groupBox3);
            this.pnlExMain.Controls.Add(this.groupBox2);
            this.pnlExMain.Controls.Add(this.groupBox1);
            this.pnlExMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlExMain.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.pnlExMain.Location = new System.Drawing.Point(0, 0);
            this.pnlExMain.Name = "pnlExMain";
            this.pnlExMain.Size = new System.Drawing.Size(648, 546);
            this.pnlExMain.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.pnlExMain.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.pnlExMain.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.pnlExMain.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.pnlExMain.Style.GradientAngle = 90;
            this.pnlExMain.TabIndex = 0;
            // 
            // btnSubmit
            // 
            this.btnSubmit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSubmit.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btnSubmit.CommandParameter = "First";
            this.btnSubmit.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.btnSubmit.Location = new System.Drawing.Point(565, 511);
            this.btnSubmit.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnSubmit.Name = "btnSubmit";
            this.btnSubmit.Size = new System.Drawing.Size(71, 23);
            this.btnSubmit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSubmit.TabIndex = 104;
            this.btnSubmit.Text = "确  定";
            this.btnSubmit.Click += new System.EventHandler(this.btnSubmit_Click);
            // 
            // cmbFingerName
            // 
            this.cmbFingerName.DisplayMember = "Text";
            this.cmbFingerName.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.cmbFingerName.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.cmbFingerName.ForeColor = System.Drawing.Color.White;
            this.cmbFingerName.FormattingEnabled = true;
            this.cmbFingerName.ItemHeight = 21;
            this.cmbFingerName.Items.AddRange(new object[] {
            this.comboItem1,
            this.comboItem2,
            this.comboItem3,
            this.comboItem4,
            this.comboItem5,
            this.comboItem6,
            this.comboItem7,
            this.comboItem8,
            this.comboItem9,
            this.comboItem10});
            this.cmbFingerName.Location = new System.Drawing.Point(364, 9);
            this.cmbFingerName.Name = "cmbFingerName";
            this.cmbFingerName.Size = new System.Drawing.Size(121, 27);
            this.cmbFingerName.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.cmbFingerName.TabIndex = 7;
            // 
            // comboItem1
            // 
            this.comboItem1.Text = "右大拇指";
            // 
            // comboItem2
            // 
            this.comboItem2.Text = "右食指";
            // 
            // comboItem3
            // 
            this.comboItem3.Text = "右中指";
            // 
            // comboItem4
            // 
            this.comboItem4.Text = "右无名指";
            // 
            // comboItem5
            // 
            this.comboItem5.Text = "右小指";
            // 
            // comboItem6
            // 
            this.comboItem6.Text = "左大拇指";
            // 
            // comboItem7
            // 
            this.comboItem7.Text = "左食指";
            // 
            // comboItem8
            // 
            this.comboItem8.Text = "左中指";
            // 
            // comboItem9
            // 
            this.comboItem9.Text = "左无名指";
            // 
            // comboItem10
            // 
            this.comboItem10.Text = "左小指";
            // 
            // superGridControl1
            // 
            this.superGridControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
            this.superGridControl1.DefaultVisualStyles.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            this.superGridControl1.DefaultVisualStyles.CellStyles.Default.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.superGridControl1.DefaultVisualStyles.ColumnHeaderStyles.Default.Font = new System.Drawing.Font("Segoe UI", 11.25F);
            this.superGridControl1.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
            this.superGridControl1.ForeColor = System.Drawing.Color.White;
            this.superGridControl1.Location = new System.Drawing.Point(15, 41);
            this.superGridControl1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.superGridControl1.Name = "superGridControl1";
            this.superGridControl1.PrimaryGrid.AutoGenerateColumns = false;
            this.superGridControl1.PrimaryGrid.Caption.Text = "";
            gridColumn1.CellStyles.Default.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Underline);
            gridColumn1.DefaultNewRowCellValue = "修改指纹";
            gridColumn1.HeaderText = "";
            gridColumn1.InfoImageAlignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gridColumn1.Name = "clmEdit";
            gridColumn1.NullString = "修改";
            gridColumn1.Visible = false;
            gridColumn1.Width = 32;
            gridColumn2.CellStyles.Default.Font = new System.Drawing.Font("宋体", 9F, System.Drawing.FontStyle.Underline);
            gridColumn2.DefaultNewRowCellValue = "删除";
            gridColumn2.HeaderText = "";
            gridColumn2.Name = "clmDel";
            gridColumn2.NullString = "删除";
            gridColumn2.Width = 32;
            gridColumn3.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gridColumn3.DataPropertyName = "FingerName";
            gridColumn3.HeaderText = "指纹名称";
            gridColumn3.InfoImageAlignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gridColumn3.MinimumWidth = 100;
            gridColumn3.Name = "";
            gridColumn4.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gridColumn4.DataPropertyName = "";
            gridColumn4.HeaderText = "用户账号";
            gridColumn4.InfoImageAlignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gridColumn4.MinimumWidth = 100;
            gridColumn4.Name = "clmUserAccount";
            gridColumn5.HeaderText = "用户名称";
            gridColumn5.Name = "clmUserName";
            gridColumn6.DataPropertyName = "Id";
            gridColumn6.Name = "clmId";
            gridColumn6.Visible = false;
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn1);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn2);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn3);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn4);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn5);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn6);
            this.superGridControl1.PrimaryGrid.InitialSelection = DevComponents.DotNetBar.SuperGrid.RelativeSelection.Row;
            this.superGridControl1.PrimaryGrid.SelectionGranularity = DevComponents.DotNetBar.SuperGrid.SelectionGranularity.Row;
            this.superGridControl1.Size = new System.Drawing.Size(621, 162);
            this.superGridControl1.TabIndex = 6;
            this.superGridControl1.Text = "superGridControl1";
            this.superGridControl1.CellMouseDown += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridCellMouseEventArgs>(this.superGridControl1_CellMouseDown);
            this.superGridControl1.DataBindingComplete += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridDataBindingCompleteEventArgs>(this.superGridControl1_DataBindingComplete);
            this.superGridControl1.BeginEdit += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridEditEventArgs>(this.superGridControl1_BeginEdit);
            this.superGridControl1.GetRowHeaderText += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridGetRowHeaderTextEventArgs>(this.superGridControl_GetRowHeaderText);
            // 
            // lbeUserName
            // 
            // 
            // 
            // 
            this.lbeUserName.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.lbeUserName.Font = new System.Drawing.Font("Segoe UI", 11.5F);
            this.lbeUserName.ForeColor = System.Drawing.Color.White;
            this.lbeUserName.Location = new System.Drawing.Point(127, 12);
            this.lbeUserName.Name = "lbeUserName";
            this.lbeUserName.Size = new System.Drawing.Size(118, 23);
            this.lbeUserName.TabIndex = 4;
            // 
            // labelX2
            // 
            // 
            // 
            // 
            this.labelX2.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX2.Font = new System.Drawing.Font("Segoe UI", 11.5F);
            this.labelX2.ForeColor = System.Drawing.Color.White;
            this.labelX2.Location = new System.Drawing.Point(251, 12);
            this.labelX2.Name = "labelX2";
            this.labelX2.Size = new System.Drawing.Size(121, 23);
            this.labelX2.TabIndex = 4;
            this.labelX2.Text = "当前指纹名称：";
            // 
            // labelX1
            // 
            // 
            // 
            // 
            this.labelX1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.labelX1.Font = new System.Drawing.Font("Segoe UI", 11.5F);
            this.labelX1.ForeColor = System.Drawing.Color.White;
            this.labelX1.Location = new System.Drawing.Point(15, 13);
            this.labelX1.Name = "labelX1";
            this.labelX1.Size = new System.Drawing.Size(121, 23);
            this.labelX1.TabIndex = 4;
            this.labelX1.Text = "当前注册用户：";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnClose);
            this.groupBox3.Controls.Add(this.btnOpen);
            this.groupBox3.Controls.Add(this.btnRegistFinger);
            this.groupBox3.ForeColor = System.Drawing.Color.White;
            this.groupBox3.Location = new System.Drawing.Point(277, 366);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(359, 130);
            this.groupBox3.TabIndex = 3;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "基本操作";
            // 
            // btnClose
            // 
            this.btnClose.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnClose.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnClose.Location = new System.Drawing.Point(87, 19);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnClose.TabIndex = 0;
            this.btnClose.Text = "关闭设备";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnOpen
            // 
            this.btnOpen.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOpen.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnOpen.Location = new System.Drawing.Point(6, 19);
            this.btnOpen.Name = "btnOpen";
            this.btnOpen.Size = new System.Drawing.Size(75, 23);
            this.btnOpen.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnOpen.TabIndex = 0;
            this.btnOpen.Text = "打开设备";
            this.btnOpen.Click += new System.EventHandler(this.btnOpen_Click);
            // 
            // btnRegistFinger
            // 
            this.btnRegistFinger.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnRegistFinger.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnRegistFinger.Location = new System.Drawing.Point(6, 48);
            this.btnRegistFinger.Name = "btnRegistFinger";
            this.btnRegistFinger.Size = new System.Drawing.Size(75, 23);
            this.btnRegistFinger.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnRegistFinger.TabIndex = 0;
            this.btnRegistFinger.Text = "注册指纹";
            this.btnRegistFinger.Click += new System.EventHandler(this.btn_eroll_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.rTxTMessageInfo);
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(277, 208);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(359, 149);
            this.groupBox2.TabIndex = 2;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "操作提示";
            // 
            // rTxTMessageInfo
            // 
            // 
            // 
            // 
            this.rTxTMessageInfo.BackgroundStyle.Class = "RichTextBoxBorder";
            this.rTxTMessageInfo.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.rTxTMessageInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rTxTMessageInfo.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rTxTMessageInfo.ForeColor = System.Drawing.Color.White;
            this.rTxTMessageInfo.Location = new System.Drawing.Point(3, 19);
            this.rTxTMessageInfo.Name = "rTxTMessageInfo";
            this.rTxTMessageInfo.Size = new System.Drawing.Size(353, 127);
            this.rTxTMessageInfo.TabIndex = 1;
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.picFinger);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(15, 208);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(256, 288);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "图像预览";
            // 
            // picFinger
            // 
            this.picFinger.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picFinger.ForeColor = System.Drawing.Color.White;
            this.picFinger.Location = new System.Drawing.Point(3, 19);
            this.picFinger.Name = "picFinger";
            this.picFinger.Size = new System.Drawing.Size(250, 266);
            this.picFinger.TabIndex = 0;
            this.picFinger.TabStop = false;
            // 
            // styleManager1
            // 
            this.styleManager1.ManagerStyle = DevComponents.DotNetBar.eStyle.Metro;
            this.styleManager1.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51))))), System.Drawing.Color.DarkTurquoise);
            // 
            // labelItem2
            // 
            this.labelItem2.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far;
            this.labelItem2.Name = "labelItem2";
            this.labelItem2.Text = "登录用户：";
            // 
            // lblLoginUserName
            // 
            this.lblLoginUserName.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far;
            this.lblLoginUserName.Name = "lblLoginUserName";
            this.lblLoginUserName.Text = "系统管理员";
            this.lblLoginUserName.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // labelItem1
            // 
            this.labelItem1.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far;
            this.labelItem1.Name = "labelItem1";
            this.labelItem1.Text = "登录用户：";
            // 
            // labelItem3
            // 
            this.labelItem3.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far;
            this.labelItem3.Name = "labelItem3";
            this.labelItem3.Text = "系统管理员";
            this.labelItem3.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // labelItem4
            // 
            this.labelItem4.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far;
            this.labelItem4.Name = "labelItem4";
            this.labelItem4.Text = "登录用户：";
            // 
            // labelItem5
            // 
            this.labelItem5.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Far;
            this.labelItem5.Name = "labelItem5";
            this.labelItem5.Text = "系统管理员";
            this.labelItem5.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // FrmFingerRegist
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(648, 546);
            this.Controls.Add(this.pnlExMain);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 11.5F);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "FrmFingerRegist";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "注册指纹";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmFingerRegist_FormClosing);
            this.Load += new System.EventHandler(this.FrmFingerRegist_Load);
            this.pnlExMain.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.picFinger)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Timer timer2;
        private System.Windows.Forms.ToolTip toolTip1;
        private DevComponents.DotNetBar.PanelEx pnlExMain;
        private DevComponents.DotNetBar.StyleManager styleManager1;
        private System.Windows.Forms.PictureBox picFinger;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private DevComponents.DotNetBar.ButtonX btnRegistFinger;
        private DevComponents.DotNetBar.Controls.RichTextBoxEx rTxTMessageInfo;
        private DevComponents.DotNetBar.LabelItem labelItem2;
        private DevComponents.DotNetBar.LabelItem lblLoginUserName;
        private DevComponents.DotNetBar.LabelItem labelItem1;
        private DevComponents.DotNetBar.LabelItem labelItem3;
        private DevComponents.DotNetBar.LabelItem labelItem4;
        private DevComponents.DotNetBar.LabelItem labelItem5;
        private DevComponents.DotNetBar.LabelX labelX1;
        private DevComponents.DotNetBar.LabelX lbeUserName;
        private DevComponents.DotNetBar.ButtonX btnOpen;
        private DevComponents.DotNetBar.ButtonX btnClose;
        private DevComponents.DotNetBar.SuperGrid.SuperGridControl superGridControl1;
        private DevComponents.DotNetBar.LabelX labelX2;
        private DevComponents.DotNetBar.Controls.ComboBoxEx cmbFingerName;
        private DevComponents.Editors.ComboItem comboItem1;
        private DevComponents.Editors.ComboItem comboItem2;
        private DevComponents.Editors.ComboItem comboItem3;
        private DevComponents.Editors.ComboItem comboItem4;
        private DevComponents.Editors.ComboItem comboItem5;
        private DevComponents.Editors.ComboItem comboItem6;
        private DevComponents.Editors.ComboItem comboItem7;
        private DevComponents.Editors.ComboItem comboItem8;
        private DevComponents.Editors.ComboItem comboItem9;
        private DevComponents.Editors.ComboItem comboItem10;
        private DevComponents.DotNetBar.ButtonX btnSubmit;
    }
}