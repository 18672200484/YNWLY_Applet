﻿

namespace CMCS.CarTransport.WeighterHand.Frms.Sys
{
    partial class FrmMainFrame
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmMainFrame));
            this.metroStatusBar1 = new DevComponents.DotNetBar.Metro.MetroStatusBar();
            this.labelItem1 = new DevComponents.DotNetBar.LabelItem();
            this.lblVersion = new DevComponents.DotNetBar.LabelItem();
            this.labelItem2 = new DevComponents.DotNetBar.LabelItem();
            this.lblLoginUserName = new DevComponents.DotNetBar.LabelItem();
            this.styleManager1 = new DevComponents.DotNetBar.StyleManager(this.components);
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.superTabControl1 = new DevComponents.DotNetBar.SuperTabControl();
            this.superTabControlPanel2 = new DevComponents.DotNetBar.SuperTabControlPanel();
            this.superTabItem2 = new DevComponents.DotNetBar.SuperTabItem();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.btnOpenChangePassword = new DevComponents.DotNetBar.ButtonX();
            this.btnOpenBaseInfo = new DevComponents.DotNetBar.ButtonX();
            this.btnOpenMineLoad = new DevComponents.DotNetBar.ButtonItem();
            this.btnOpenFuelKindlLoad = new DevComponents.DotNetBar.ButtonItem();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.buttonItem1 = new DevComponents.DotNetBar.ButtonItem();
            this.btnBuyFuelDetail = new DevComponents.DotNetBar.ButtonItem();
            this.btnOpenTransport = new DevComponents.DotNetBar.ButtonX();
            this.btnOpenBuyFuelTransportLoad = new DevComponents.DotNetBar.ButtonItem();
            this.lblCurrentTime = new System.Windows.Forms.Label();
            this.btnApplicationExit = new DevComponents.DotNetBar.ButtonX();
            this.btnOpenSetting = new DevComponents.DotNetBar.ButtonX();
            this.timer_CurrentTime = new System.Windows.Forms.Timer(this.components);
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.superTabControl1)).BeginInit();
            this.superTabControl1.SuspendLayout();
            this.panelEx2.SuspendLayout();
            this.SuspendLayout();
            // 
            // metroStatusBar1
            // 
            this.metroStatusBar1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
            // 
            // 
            // 
            this.metroStatusBar1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.metroStatusBar1.ContainerControlProcessDialogKey = true;
            this.metroStatusBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.metroStatusBar1.Font = new System.Drawing.Font("Segoe UI", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.metroStatusBar1.ForeColor = System.Drawing.Color.White;
            this.metroStatusBar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.labelItem1,
            this.lblVersion,
            this.labelItem2,
            this.lblLoginUserName});
            this.metroStatusBar1.Location = new System.Drawing.Point(0, 790);
            this.metroStatusBar1.Name = "metroStatusBar1";
            this.metroStatusBar1.Size = new System.Drawing.Size(1424, 22);
            this.metroStatusBar1.TabIndex = 6;
            this.metroStatusBar1.Text = "metroStatusBar1";
            // 
            // labelItem1
            // 
            this.labelItem1.Name = "labelItem1";
            this.labelItem1.Text = "版本：";
            // 
            // lblVersion
            // 
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Text = "1.0.0.0";
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
            // styleManager1
            // 
            this.styleManager1.ManagerStyle = DevComponents.DotNetBar.eStyle.Metro;
            this.styleManager1.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51))))), System.Drawing.Color.DarkTurquoise);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(82)))), ((int)(((byte)(89)))));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.superTabControl1, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panelEx2, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.ForeColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1424, 790);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // superTabControl1
            // 
            this.superTabControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
            this.superTabControl1.CloseButtonOnTabsVisible = true;
            // 
            // 
            // 
            // 
            // 
            // 
            this.superTabControl1.ControlBox.CloseBox.Name = "";
            // 
            // 
            // 
            this.superTabControl1.ControlBox.MenuBox.Name = "";
            this.superTabControl1.ControlBox.Name = "";
            this.superTabControl1.ControlBox.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.superTabControl1.ControlBox.MenuBox,
            this.superTabControl1.ControlBox.CloseBox});
            this.superTabControl1.ControlBox.Visible = false;
            this.superTabControl1.Controls.Add(this.superTabControlPanel2);
            this.superTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.superTabControl1.ForeColor = System.Drawing.Color.White;
            this.superTabControl1.Location = new System.Drawing.Point(0, 50);
            this.superTabControl1.Margin = new System.Windows.Forms.Padding(0);
            this.superTabControl1.Name = "superTabControl1";
            this.superTabControl1.ReorderTabsEnabled = true;
            this.superTabControl1.SelectedTabFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.superTabControl1.SelectedTabIndex = 0;
            this.superTabControl1.Size = new System.Drawing.Size(1424, 740);
            this.superTabControl1.TabFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.superTabControl1.TabIndex = 10;
            this.superTabControl1.Tabs.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.superTabItem2});
            this.superTabControl1.Text = "superTabControl_Main";
            this.superTabControl1.TextAlignment = DevComponents.DotNetBar.eItemAlignment.Center;
            // 
            // superTabControlPanel2
            // 
            this.superTabControlPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.superTabControlPanel2.Location = new System.Drawing.Point(0, 36);
            this.superTabControlPanel2.Name = "superTabControlPanel2";
            this.superTabControlPanel2.Size = new System.Drawing.Size(1424, 704);
            this.superTabControlPanel2.TabIndex = 0;
            this.superTabControlPanel2.TabItem = this.superTabItem2;
            // 
            // superTabItem2
            // 
            this.superTabItem2.AttachedControl = this.superTabControlPanel2;
            this.superTabItem2.GlobalItem = false;
            this.superTabItem2.Name = "superTabItem2";
            this.superTabItem2.SelectedTabFont = new System.Drawing.Font("Segoe UI", 12F);
            this.superTabItem2.TabFont = new System.Drawing.Font("Segoe UI", 12F);
            this.superTabItem2.Text = "superTabItem2";
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.btnOpenChangePassword);
            this.panelEx2.Controls.Add(this.btnOpenBaseInfo);
            this.panelEx2.Controls.Add(this.buttonX1);
            this.panelEx2.Controls.Add(this.btnOpenTransport);
            this.panelEx2.Controls.Add(this.lblCurrentTime);
            this.panelEx2.Controls.Add(this.btnApplicationExit);
            this.panelEx2.Controls.Add(this.btnOpenSetting);
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx2.Location = new System.Drawing.Point(0, 0);
            this.panelEx2.Margin = new System.Windows.Forms.Padding(0);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(1424, 50);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 1;
            // 
            // btnOpenChangePassword
            // 
            this.btnOpenChangePassword.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOpenChangePassword.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnOpenChangePassword.AutoExpandOnClick = true;
            this.btnOpenChangePassword.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenChangePassword.Location = new System.Drawing.Point(470, 8);
            this.btnOpenChangePassword.Name = "btnOpenChangePassword";
            this.btnOpenChangePassword.Size = new System.Drawing.Size(108, 31);
            this.btnOpenChangePassword.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnOpenChangePassword.TabIndex = 20;
            this.btnOpenChangePassword.Text = "修 改 密 码";
            this.btnOpenChangePassword.Click += new System.EventHandler(this.btnOpenChangePassword_Click);
            // 
            // btnOpenBaseInfo
            // 
            this.btnOpenBaseInfo.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOpenBaseInfo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnOpenBaseInfo.AutoExpandOnClick = true;
            this.btnOpenBaseInfo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenBaseInfo.Location = new System.Drawing.Point(12, 8);
            this.btnOpenBaseInfo.Name = "btnOpenBaseInfo";
            this.btnOpenBaseInfo.Size = new System.Drawing.Size(108, 31);
            this.btnOpenBaseInfo.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnOpenBaseInfo.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnOpenMineLoad,
            this.btnOpenFuelKindlLoad});
            this.btnOpenBaseInfo.TabIndex = 19;
            this.btnOpenBaseInfo.Text = "基 础 信 息";
            // 
            // btnOpenMineLoad
            // 
            this.btnOpenMineLoad.GlobalItem = false;
            this.btnOpenMineLoad.Name = "btnOpenMineLoad";
            this.btnOpenMineLoad.Text = "矿点管理";
            this.btnOpenMineLoad.Click += new System.EventHandler(this.btnOpenMineLoad_Click);
            // 
            // btnOpenFuelKindlLoad
            // 
            this.btnOpenFuelKindlLoad.GlobalItem = false;
            this.btnOpenFuelKindlLoad.Name = "btnOpenFuelKindlLoad";
            this.btnOpenFuelKindlLoad.Text = "煤种管理";
            this.btnOpenFuelKindlLoad.Click += new System.EventHandler(this.btnOpenFuelKindlLoad_Click);
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.buttonX1.AutoExpandOnClick = true;
            this.buttonX1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonX1.Location = new System.Drawing.Point(240, 8);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(108, 31);
            this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX1.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.buttonItem1,
            this.btnBuyFuelDetail});
            this.buttonX1.TabIndex = 18;
            this.buttonX1.Text = "报 表 汇 总";
            // 
            // buttonItem1
            // 
            this.buttonItem1.GlobalItem = false;
            this.buttonItem1.Name = "buttonItem1";
            this.buttonItem1.Text = "入厂煤报表";
            this.buttonItem1.Click += new System.EventHandler(this.btnOpenBuyFuelTransportCollectLoad_Click);
            // 
            // btnBuyFuelDetail
            // 
            this.btnBuyFuelDetail.GlobalItem = false;
            this.btnBuyFuelDetail.Name = "btnBuyFuelDetail";
            this.btnBuyFuelDetail.Text = "入厂煤明细";
            this.btnBuyFuelDetail.Click += new System.EventHandler(this.btnBuyFuelDetail_Click);
            // 
            // btnOpenTransport
            // 
            this.btnOpenTransport.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOpenTransport.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnOpenTransport.AutoExpandOnClick = true;
            this.btnOpenTransport.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenTransport.Location = new System.Drawing.Point(126, 8);
            this.btnOpenTransport.Name = "btnOpenTransport";
            this.btnOpenTransport.Size = new System.Drawing.Size(108, 31);
            this.btnOpenTransport.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnOpenTransport.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnOpenBuyFuelTransportLoad});
            this.btnOpenTransport.TabIndex = 17;
            this.btnOpenTransport.Text = "运 输 记 录";
            // 
            // btnOpenBuyFuelTransportLoad
            // 
            this.btnOpenBuyFuelTransportLoad.GlobalItem = false;
            this.btnOpenBuyFuelTransportLoad.Name = "btnOpenBuyFuelTransportLoad";
            this.btnOpenBuyFuelTransportLoad.Text = "入厂煤运输记录";
            this.btnOpenBuyFuelTransportLoad.Click += new System.EventHandler(this.btnOpenBuyFuelTransportLoad_Click);
            // 
            // lblCurrentTime
            // 
            this.lblCurrentTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCurrentTime.AutoSize = true;
            this.lblCurrentTime.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentTime.ForeColor = System.Drawing.Color.White;
            this.lblCurrentTime.Location = new System.Drawing.Point(1177, 11);
            this.lblCurrentTime.Name = "lblCurrentTime";
            this.lblCurrentTime.Size = new System.Drawing.Size(239, 28);
            this.lblCurrentTime.TabIndex = 14;
            this.lblCurrentTime.Text = "2017年02月14日 09:24:39";
            // 
            // btnApplicationExit
            // 
            this.btnApplicationExit.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnApplicationExit.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnApplicationExit.AutoExpandOnClick = true;
            this.btnApplicationExit.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnApplicationExit.Location = new System.Drawing.Point(584, 8);
            this.btnApplicationExit.Name = "btnApplicationExit";
            this.btnApplicationExit.Size = new System.Drawing.Size(108, 31);
            this.btnApplicationExit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnApplicationExit.TabIndex = 13;
            this.btnApplicationExit.Text = "退 出 程 序";
            this.btnApplicationExit.Click += new System.EventHandler(this.btnApplicationExit_Click);
            // 
            // btnOpenSetting
            // 
            this.btnOpenSetting.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOpenSetting.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnOpenSetting.AutoExpandOnClick = true;
            this.btnOpenSetting.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenSetting.Location = new System.Drawing.Point(354, 8);
            this.btnOpenSetting.Name = "btnOpenSetting";
            this.btnOpenSetting.Size = new System.Drawing.Size(108, 31);
            this.btnOpenSetting.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnOpenSetting.TabIndex = 9;
            this.btnOpenSetting.Text = "参 数 设 置";
            this.btnOpenSetting.Click += new System.EventHandler(this.btnOpenSetting_Click);
            // 
            // timer_CurrentTime
            // 
            this.timer_CurrentTime.Enabled = true;
            this.timer_CurrentTime.Interval = 1000;
            this.timer_CurrentTime.Tick += new System.EventHandler(this.timer_CurrentTime_Tick);
            // 
            // FrmMainFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1424, 812);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.metroStatusBar1);
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1440, 850);
            this.Name = "FrmMainFrame";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "武汉博晟汽车智能化-过衡";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.tableLayoutPanel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.superTabControl1)).EndInit();
            this.superTabControl1.ResumeLayout(false);
            this.panelEx2.ResumeLayout(false);
            this.panelEx2.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion  

        private DevComponents.DotNetBar.Metro.MetroStatusBar metroStatusBar1;
        private DevComponents.DotNetBar.LabelItem labelItem1;
        private DevComponents.DotNetBar.LabelItem lblVersion;
        private DevComponents.DotNetBar.LabelItem labelItem2;
        private DevComponents.DotNetBar.LabelItem lblLoginUserName;
        private DevComponents.DotNetBar.StyleManager styleManager1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private DevComponents.DotNetBar.ButtonX btnApplicationExit;
        private DevComponents.DotNetBar.SuperTabControl superTabControl1;
        private DevComponents.DotNetBar.ButtonX btnOpenSetting;
        private System.Windows.Forms.Label lblCurrentTime;
        private System.Windows.Forms.Timer timer_CurrentTime;  
        private DevComponents.DotNetBar.SuperTabControlPanel superTabControlPanel2;
        private DevComponents.DotNetBar.SuperTabItem superTabItem2;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        private DevComponents.DotNetBar.ButtonItem buttonItem1;
        private DevComponents.DotNetBar.ButtonItem btnBuyFuelDetail;
        private DevComponents.DotNetBar.ButtonX btnOpenTransport;
        private DevComponents.DotNetBar.ButtonItem btnOpenBuyFuelTransportLoad;
        private DevComponents.DotNetBar.ButtonX btnOpenChangePassword;
        private DevComponents.DotNetBar.ButtonX btnOpenBaseInfo;
        private DevComponents.DotNetBar.ButtonItem btnOpenMineLoad;
        private DevComponents.DotNetBar.ButtonItem btnOpenFuelKindlLoad;
    }
}

