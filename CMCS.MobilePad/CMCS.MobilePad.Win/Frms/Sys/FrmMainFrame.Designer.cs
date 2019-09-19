

namespace CMCS.MobilePad.Win.Frms.Sys
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
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.superTabControl1 = new DevComponents.DotNetBar.SuperTabControl();
            this.superTabControlPanel2 = new DevComponents.DotNetBar.SuperTabControlPanel();
            this.superTabItem1 = new DevComponents.DotNetBar.SuperTabItem();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.btnOpenCarShipping = new DevComponents.DotNetBar.ButtonX();
            this.btnOpenTemper = new DevComponents.DotNetBar.ButtonX();
            this.btnOpenCarBreak = new DevComponents.DotNetBar.ButtonX();
            this.btnOpenUnLoad = new DevComponents.DotNetBar.ButtonX();
            this.lblCurrentTime = new System.Windows.Forms.Label();
            this.btnApplicationExit = new DevComponents.DotNetBar.ButtonX();
            this.btnOpenQueue = new DevComponents.DotNetBar.ButtonX();
            this.btnOpenDeduct = new DevComponents.DotNetBar.ButtonX();
            this.btnOpenCarUpdate = new DevComponents.DotNetBar.ButtonX();
            this.btnOpenLMYB = new DevComponents.DotNetBar.ButtonX();
            this.btnOpenHome = new DevComponents.DotNetBar.ButtonX();
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
            this.metroStatusBar1.Location = new System.Drawing.Point(0, 810);
            this.metroStatusBar1.Margin = new System.Windows.Forms.Padding(4);
            this.metroStatusBar1.Name = "metroStatusBar1";
            this.metroStatusBar1.Size = new System.Drawing.Size(1539, 28);
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
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 62F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1539, 810);
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
            this.superTabControl1.Location = new System.Drawing.Point(0, 62);
            this.superTabControl1.Margin = new System.Windows.Forms.Padding(0);
            this.superTabControl1.Name = "superTabControl1";
            this.superTabControl1.ReorderTabsEnabled = true;
            this.superTabControl1.SelectedTabFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.superTabControl1.SelectedTabIndex = 0;
            this.superTabControl1.Size = new System.Drawing.Size(1539, 748);
            this.superTabControl1.TabFont = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.superTabControl1.TabIndex = 10;
            this.superTabControl1.Tabs.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.superTabItem1});
            this.superTabControl1.Text = "superTabControl_Main";
            this.superTabControl1.TextAlignment = DevComponents.DotNetBar.eItemAlignment.Center;
            // 
            // superTabControlPanel2
            // 
            this.superTabControlPanel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.superTabControlPanel2.Location = new System.Drawing.Point(0, 36);
            this.superTabControlPanel2.Margin = new System.Windows.Forms.Padding(4);
            this.superTabControlPanel2.Name = "superTabControlPanel2";
            this.superTabControlPanel2.Size = new System.Drawing.Size(1539, 712);
            this.superTabControlPanel2.TabIndex = 0;
            this.superTabControlPanel2.TabItem = this.superTabItem1;
            // 
            // superTabItem1
            // 
            this.superTabItem1.AttachedControl = this.superTabControlPanel2;
            this.superTabItem1.GlobalItem = false;
            this.superTabItem1.Name = "superTabItem1";
            this.superTabItem1.SelectedTabFont = new System.Drawing.Font("Segoe UI", 12F);
            this.superTabItem1.TabFont = new System.Drawing.Font("Segoe UI", 12F);
            this.superTabItem1.Text = "superTabItem1";
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.btnOpenCarShipping);
            this.panelEx2.Controls.Add(this.btnOpenTemper);
            this.panelEx2.Controls.Add(this.btnOpenCarBreak);
            this.panelEx2.Controls.Add(this.btnOpenUnLoad);
            this.panelEx2.Controls.Add(this.lblCurrentTime);
            this.panelEx2.Controls.Add(this.btnApplicationExit);
            this.panelEx2.Controls.Add(this.btnOpenQueue);
            this.panelEx2.Controls.Add(this.btnOpenDeduct);
            this.panelEx2.Controls.Add(this.btnOpenCarUpdate);
            this.panelEx2.Controls.Add(this.btnOpenLMYB);
            this.panelEx2.Controls.Add(this.btnOpenHome);
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx2.Location = new System.Drawing.Point(0, 0);
            this.panelEx2.Margin = new System.Windows.Forms.Padding(0);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(1539, 62);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 1;
            // 
            // btnOpenCarShipping
            // 
            this.btnOpenCarShipping.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOpenCarShipping.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnOpenCarShipping.AutoExpandOnClick = true;
            this.btnOpenCarShipping.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenCarShipping.Location = new System.Drawing.Point(996, 10);
            this.btnOpenCarShipping.Margin = new System.Windows.Forms.Padding(4);
            this.btnOpenCarShipping.Name = "btnOpenCarShipping";
            this.btnOpenCarShipping.Size = new System.Drawing.Size(130, 39);
            this.btnOpenCarShipping.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnOpenCarShipping.TabIndex = 18;
            this.btnOpenCarShipping.Text = "车辆仓位调整";
            this.btnOpenCarShipping.Click += new System.EventHandler(this.btnOpenCarShipping_Click);
            // 
            // btnOpenTemper
            // 
            this.btnOpenTemper.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOpenTemper.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnOpenTemper.AutoExpandOnClick = true;
            this.btnOpenTemper.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenTemper.Location = new System.Drawing.Point(858, 10);
            this.btnOpenTemper.Margin = new System.Windows.Forms.Padding(4);
            this.btnOpenTemper.Name = "btnOpenTemper";
            this.btnOpenTemper.Size = new System.Drawing.Size(130, 39);
            this.btnOpenTemper.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnOpenTemper.TabIndex = 17;
            this.btnOpenTemper.Text = "测温杆设置";
            this.btnOpenTemper.Click += new System.EventHandler(this.btnOpenTemper_Click);
            // 
            // btnOpenCarBreak
            // 
            this.btnOpenCarBreak.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOpenCarBreak.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnOpenCarBreak.AutoExpandOnClick = true;
            this.btnOpenCarBreak.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenCarBreak.Location = new System.Drawing.Point(720, 10);
            this.btnOpenCarBreak.Margin = new System.Windows.Forms.Padding(4);
            this.btnOpenCarBreak.Name = "btnOpenCarBreak";
            this.btnOpenCarBreak.Size = new System.Drawing.Size(130, 39);
            this.btnOpenCarBreak.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnOpenCarBreak.TabIndex = 16;
            this.btnOpenCarBreak.Text = "违章事件记录";
            this.btnOpenCarBreak.Click += new System.EventHandler(this.btnOpenCarBreak_Click);
            // 
            // btnOpenUnLoad
            // 
            this.btnOpenUnLoad.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOpenUnLoad.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnOpenUnLoad.AutoExpandOnClick = true;
            this.btnOpenUnLoad.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenUnLoad.Location = new System.Drawing.Point(306, 10);
            this.btnOpenUnLoad.Margin = new System.Windows.Forms.Padding(4);
            this.btnOpenUnLoad.Name = "btnOpenUnLoad";
            this.btnOpenUnLoad.Size = new System.Drawing.Size(130, 39);
            this.btnOpenUnLoad.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnOpenUnLoad.TabIndex = 15;
            this.btnOpenUnLoad.Text = "接 卸 方 案";
            this.btnOpenUnLoad.Click += new System.EventHandler(this.btnOpenUnLoad_Click);
            // 
            // lblCurrentTime
            // 
            this.lblCurrentTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCurrentTime.AutoSize = true;
            this.lblCurrentTime.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentTime.ForeColor = System.Drawing.Color.White;
            this.lblCurrentTime.Location = new System.Drawing.Point(1296, 15);
            this.lblCurrentTime.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
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
            this.btnApplicationExit.Location = new System.Drawing.Point(1134, 10);
            this.btnApplicationExit.Margin = new System.Windows.Forms.Padding(4);
            this.btnApplicationExit.Name = "btnApplicationExit";
            this.btnApplicationExit.Size = new System.Drawing.Size(130, 39);
            this.btnApplicationExit.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnApplicationExit.TabIndex = 13;
            this.btnApplicationExit.Text = "退 出 程 序";
            this.btnApplicationExit.Click += new System.EventHandler(this.btnApplicationExit_Click);
            // 
            // btnOpenQueue
            // 
            this.btnOpenQueue.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOpenQueue.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnOpenQueue.AutoExpandOnClick = true;
            this.btnOpenQueue.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenQueue.Location = new System.Drawing.Point(142, 10);
            this.btnOpenQueue.Margin = new System.Windows.Forms.Padding(4);
            this.btnOpenQueue.Name = "btnOpenQueue";
            this.btnOpenQueue.Size = new System.Drawing.Size(74, 39);
            this.btnOpenQueue.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnOpenQueue.TabIndex = 12;
            this.btnOpenQueue.Text = "登  记";
            this.btnOpenQueue.Click += new System.EventHandler(this.btnOpenQueue_Click);
            // 
            // btnOpenDeduct
            // 
            this.btnOpenDeduct.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOpenDeduct.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnOpenDeduct.AutoExpandOnClick = true;
            this.btnOpenDeduct.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenDeduct.Location = new System.Drawing.Point(224, 10);
            this.btnOpenDeduct.Margin = new System.Windows.Forms.Padding(4);
            this.btnOpenDeduct.Name = "btnOpenDeduct";
            this.btnOpenDeduct.Size = new System.Drawing.Size(74, 39);
            this.btnOpenDeduct.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnOpenDeduct.TabIndex = 12;
            this.btnOpenDeduct.Text = "扣  吨";
            this.btnOpenDeduct.Click += new System.EventHandler(this.btnOpenDeduct_Click);
            // 
            // btnOpenCarUpdate
            // 
            this.btnOpenCarUpdate.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOpenCarUpdate.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnOpenCarUpdate.AutoExpandOnClick = true;
            this.btnOpenCarUpdate.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenCarUpdate.Location = new System.Drawing.Point(582, 10);
            this.btnOpenCarUpdate.Margin = new System.Windows.Forms.Padding(4);
            this.btnOpenCarUpdate.Name = "btnOpenCarUpdate";
            this.btnOpenCarUpdate.Size = new System.Drawing.Size(130, 39);
            this.btnOpenCarUpdate.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnOpenCarUpdate.TabIndex = 11;
            this.btnOpenCarUpdate.Text = "车辆信息修改";
            this.btnOpenCarUpdate.Click += new System.EventHandler(this.btnOpenCarUpdate_Click);
            // 
            // btnOpenLMYB
            // 
            this.btnOpenLMYB.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOpenLMYB.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnOpenLMYB.AutoExpandOnClick = true;
            this.btnOpenLMYB.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenLMYB.Location = new System.Drawing.Point(444, 10);
            this.btnOpenLMYB.Margin = new System.Windows.Forms.Padding(4);
            this.btnOpenLMYB.Name = "btnOpenLMYB";
            this.btnOpenLMYB.Size = new System.Drawing.Size(130, 39);
            this.btnOpenLMYB.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnOpenLMYB.TabIndex = 9;
            this.btnOpenLMYB.Text = "调运信息修改";
            this.btnOpenLMYB.Click += new System.EventHandler(this.btnOpenLMYB_Click);
            // 
            // btnOpenHome
            // 
            this.btnOpenHome.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOpenHome.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnOpenHome.AutoExpandOnClick = true;
            this.btnOpenHome.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenHome.Location = new System.Drawing.Point(4, 10);
            this.btnOpenHome.Margin = new System.Windows.Forms.Padding(4);
            this.btnOpenHome.Name = "btnOpenHome";
            this.btnOpenHome.Size = new System.Drawing.Size(130, 39);
            this.btnOpenHome.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnOpenHome.TabIndex = 10;
            this.btnOpenHome.Text = "数据指标统计";
            this.btnOpenHome.Click += new System.EventHandler(this.btnOpenHome_Click);
            // 
            // timer_CurrentTime
            // 
            this.timer_CurrentTime.Enabled = true;
            this.timer_CurrentTime.Interval = 1000;
            this.timer_CurrentTime.Tick += new System.EventHandler(this.timer_CurrentTime_Tick);
            // 
            // FrmMainFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CaptionFont = new System.Drawing.Font("Segoe UI", 11.25F);
            this.ClientSize = new System.Drawing.Size(1539, 838);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.metroStatusBar1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("宋体", 11.25F);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(4);
            this.MinimumSize = new System.Drawing.Size(1538, 828);
            this.Name = "FrmMainFrame";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "武汉博晟汽车智能化-移动端";
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
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private DevComponents.DotNetBar.ButtonX btnApplicationExit;
        private DevComponents.DotNetBar.ButtonX btnOpenCarUpdate;
        private DevComponents.DotNetBar.SuperTabControl superTabControl1;
        private DevComponents.DotNetBar.ButtonX btnOpenLMYB;
        private System.Windows.Forms.Label lblCurrentTime;
        private System.Windows.Forms.Timer timer_CurrentTime;  
        private DevComponents.DotNetBar.SuperTabControlPanel superTabControlPanel2;
        private DevComponents.DotNetBar.SuperTabItem superTabItem1;
        private DevComponents.DotNetBar.ButtonX btnOpenDeduct;
        private DevComponents.DotNetBar.ButtonX btnOpenHome;
        private DevComponents.DotNetBar.ButtonX btnOpenUnLoad;
        private DevComponents.DotNetBar.ButtonX btnOpenCarBreak;
        private DevComponents.DotNetBar.ButtonX btnOpenTemper;
        private DevComponents.DotNetBar.ButtonX btnOpenCarShipping;
        private DevComponents.DotNetBar.ButtonX btnOpenQueue;
    }
}

