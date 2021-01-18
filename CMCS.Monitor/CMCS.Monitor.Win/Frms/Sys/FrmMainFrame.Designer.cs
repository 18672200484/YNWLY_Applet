using CMCS.Monitor.Win.Core;

namespace CMCS.Monitor.Win.Frms.Sys
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
            this.buttonItem2 = new DevComponents.DotNetBar.ButtonItem();
            this.buttonItem1 = new DevComponents.DotNetBar.ButtonItem();
            this.btnOpenMainPage = new DevComponents.DotNetBar.ButtonX();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.lblCurrentTime = new System.Windows.Forms.Label();
            this.lblSystemName = new System.Windows.Forms.Label();
            this.btnCZGK = new DevComponents.DotNetBar.ButtonX();
            this.btnOpenTrainBeltSampler = new DevComponents.DotNetBar.ButtonItem();
            this.btnOpenOutBeltSampler = new DevComponents.DotNetBar.ButtonItem();
            this.btnOpenAutoMaker = new DevComponents.DotNetBar.ButtonItem();
            this.btnOpenCarSampler = new DevComponents.DotNetBar.ButtonItem();
            this.buttonX6 = new DevComponents.DotNetBar.ButtonX();
            this.btnOpenWeightBridgeLoad = new DevComponents.DotNetBar.ButtonItem();
            this.btnOpenBuyFuelLoad = new DevComponents.DotNetBar.ButtonItem();
            this.btnOpenPoundInfo = new DevComponents.DotNetBar.ButtonItem();
            this.btnOpenInOutInfo = new DevComponents.DotNetBar.ButtonItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.btnJoinBacthManage = new DevComponents.DotNetBar.ButtonX();
            this.buttonX1 = new DevComponents.DotNetBar.ButtonX();
            this.btnOpenGuardInfo = new DevComponents.DotNetBar.ButtonX();
            this.btnOpenEquInfHitch = new DevComponents.DotNetBar.ButtonX();
            this.btnCarMonitor = new DevComponents.DotNetBar.ButtonX();
            this.btnOpenAssayManage = new DevComponents.DotNetBar.ButtonX();
            this.btnOpenTrainTipper = new DevComponents.DotNetBar.ButtonX();
            this.btnOpenAutoCupboard = new DevComponents.DotNetBar.ButtonX();
            this.superTabControl1 = new DevComponents.DotNetBar.SuperTabControl();
            this.superTabControlPanel1 = new DevComponents.DotNetBar.SuperTabControlPanel();
            this.superTabItem1 = new DevComponents.DotNetBar.SuperTabItem();
            this.flpanEquipments = new System.Windows.Forms.FlowLayoutPanel();
            this.timer_CurrentTime = new System.Windows.Forms.Timer(this.components);
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.timer_EquipmentStatus = new System.Windows.Forms.Timer(this.components);
            this.timer_MsgTime = new System.Windows.Forms.Timer(this.components);
            this.btnOpenSaleFuelLoad = new DevComponents.DotNetBar.ButtonItem();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.panelEx1.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panelEx2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.superTabControl1)).BeginInit();
            this.superTabControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // buttonItem2
            // 
            this.buttonItem2.Name = "buttonItem2";
            // 
            // buttonItem1
            // 
            this.buttonItem1.Name = "buttonItem1";
            this.buttonItem1.Text = "buttonItem1";
            // 
            // btnOpenMainPage
            // 
            this.btnOpenMainPage.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOpenMainPage.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnOpenMainPage.AutoExpandOnClick = true;
            this.btnOpenMainPage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnOpenMainPage.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenMainPage.Location = new System.Drawing.Point(3, 7);
            this.btnOpenMainPage.Name = "btnOpenMainPage";
            this.btnOpenMainPage.Size = new System.Drawing.Size(108, 31);
            this.btnOpenMainPage.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnOpenMainPage.TabIndex = 6;
            this.btnOpenMainPage.Text = "集 控 首 页";
            this.btnOpenMainPage.TextColor = System.Drawing.Color.Black;
            this.btnOpenMainPage.Click += new System.EventHandler(this.btnOpenMainPage_Click);
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.lblCurrentTime);
            this.panelEx1.Controls.Add(this.lblSystemName);
            this.panelEx1.Controls.Add(this.pictureBox1);
            this.panelEx1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx1.Location = new System.Drawing.Point(0, 0);
            this.panelEx1.Margin = new System.Windows.Forms.Padding(0);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(1291, 56);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.Color = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(82)))), ((int)(((byte)(89)))));
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 7;
            // 
            // lblCurrentTime
            // 
            this.lblCurrentTime.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblCurrentTime.AutoSize = true;
            this.lblCurrentTime.Font = new System.Drawing.Font("Segoe UI", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrentTime.ForeColor = System.Drawing.Color.White;
            this.lblCurrentTime.Location = new System.Drawing.Point(963, 14);
            this.lblCurrentTime.Name = "lblCurrentTime";
            this.lblCurrentTime.Size = new System.Drawing.Size(239, 28);
            this.lblCurrentTime.TabIndex = 2;
            this.lblCurrentTime.Text = "2017年02月14日 09:24:39";
            // 
            // lblSystemName
            // 
            this.lblSystemName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.lblSystemName.Font = new System.Drawing.Font("Segoe UI", 20F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSystemName.ForeColor = System.Drawing.Color.White;
            this.lblSystemName.Location = new System.Drawing.Point(0, 2);
            this.lblSystemName.Name = "lblSystemName";
            this.lblSystemName.Size = new System.Drawing.Size(1288, 47);
            this.lblSystemName.TabIndex = 1;
            this.lblSystemName.Text = "鹤壁煤炭产业园质检智能化集中管控平台";
            this.lblSystemName.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // btnCZGK
            // 
            this.btnCZGK.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCZGK.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnCZGK.AutoExpandOnClick = true;
            this.btnCZGK.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnCZGK.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCZGK.Location = new System.Drawing.Point(371, 7);
            this.btnCZGK.Name = "btnCZGK";
            this.btnCZGK.Size = new System.Drawing.Size(108, 31);
            this.btnCZGK.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnCZGK.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnOpenTrainBeltSampler,
            this.btnOpenOutBeltSampler,
            this.btnOpenAutoMaker,
            this.btnOpenCarSampler});
            this.btnCZGK.TabIndex = 9;
            this.btnCZGK.Text = "采 制 管 控";
            this.btnCZGK.TextColor = System.Drawing.Color.Black;
            // 
            // btnOpenTrainBeltSampler
            // 
            this.btnOpenTrainBeltSampler.GlobalItem = false;
            this.btnOpenTrainBeltSampler.Name = "btnOpenTrainBeltSampler";
            this.btnOpenTrainBeltSampler.Text = "入场皮带采样监控";
            this.btnOpenTrainBeltSampler.Click += new System.EventHandler(this.btnOpenTrainBeltSampler_Click);
            // 
            // btnOpenOutBeltSampler
            // 
            this.btnOpenOutBeltSampler.Name = "btnOpenOutBeltSampler";
            this.btnOpenOutBeltSampler.Text = "出场皮带采样监控";
            this.btnOpenOutBeltSampler.Click += new System.EventHandler(this.btnOpenOutBeltSampler_Click);
            // 
            // btnOpenAutoMaker
            // 
            this.btnOpenAutoMaker.GlobalItem = false;
            this.btnOpenAutoMaker.Name = "btnOpenAutoMaker";
            this.btnOpenAutoMaker.Text = "全自动制样机监控";
            this.btnOpenAutoMaker.Click += new System.EventHandler(this.btnOpenAutoMaker_Click);
            // 
            // btnOpenCarSampler
            // 
            this.btnOpenCarSampler.GlobalItem = false;
            this.btnOpenCarSampler.Name = "btnOpenCarSampler";
            this.btnOpenCarSampler.Text = "汽车机械采样监控";
            this.btnOpenCarSampler.Click += new System.EventHandler(this.btnOpenCarSampler_Click);
            // 
            // buttonX6
            // 
            this.buttonX6.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX6.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.buttonX6.AutoExpandOnClick = true;
            this.buttonX6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.buttonX6.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonX6.Location = new System.Drawing.Point(117, 7);
            this.buttonX6.Name = "buttonX6";
            this.buttonX6.Size = new System.Drawing.Size(108, 31);
            this.buttonX6.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX6.SubItems.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.btnOpenWeightBridgeLoad,
            this.btnOpenBuyFuelLoad,
            this.btnOpenSaleFuelLoad,
            this.btnOpenPoundInfo,
            this.btnOpenInOutInfo});
            this.buttonX6.TabIndex = 10;
            this.buttonX6.Text = "计 量 管 控";
            this.buttonX6.TextColor = System.Drawing.Color.Black;
            // 
            // btnOpenWeightBridgeLoad
            // 
            this.btnOpenWeightBridgeLoad.GlobalItem = false;
            this.btnOpenWeightBridgeLoad.Name = "btnOpenWeightBridgeLoad";
            this.btnOpenWeightBridgeLoad.Text = "火车入场记录查询";
            this.btnOpenWeightBridgeLoad.Click += new System.EventHandler(this.btnOpenWeightBridgeLoad_Click);
            // 
            // btnOpenBuyFuelLoad
            // 
            this.btnOpenBuyFuelLoad.GlobalItem = false;
            this.btnOpenBuyFuelLoad.Name = "btnOpenBuyFuelLoad";
            this.btnOpenBuyFuelLoad.Text = "汽车入场记录查询";
            this.btnOpenBuyFuelLoad.Click += new System.EventHandler(this.btnOpenBuyFuelLoad_Click);
            // 
            // btnOpenPoundInfo
            // 
            this.btnOpenPoundInfo.GlobalItem = false;
            this.btnOpenPoundInfo.Name = "btnOpenPoundInfo";
            this.btnOpenPoundInfo.Text = "地磅记录";
            this.btnOpenPoundInfo.Click += new System.EventHandler(this.btnOpenPoundInfo_Click);
            // 
            // btnOpenInOutInfo
            // 
            this.btnOpenInOutInfo.GlobalItem = false;
            this.btnOpenInOutInfo.Name = "btnOpenInOutInfo";
            this.btnOpenInOutInfo.Text = "进出场记录";
            this.btnOpenInOutInfo.Click += new System.EventHandler(this.btnOpenInOutInfo_Click);
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(82)))), ((int)(((byte)(89)))));
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.panelEx2, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panelEx1, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.superTabControl1, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.flpanEquipments, 0, 3);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.ForeColor = System.Drawing.Color.White;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 4;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 56F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 50F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 34F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1291, 780);
            this.tableLayoutPanel1.TabIndex = 10;
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.btnJoinBacthManage);
            this.panelEx2.Controls.Add(this.buttonX1);
            this.panelEx2.Controls.Add(this.btnOpenGuardInfo);
            this.panelEx2.Controls.Add(this.btnOpenEquInfHitch);
            this.panelEx2.Controls.Add(this.btnCarMonitor);
            this.panelEx2.Controls.Add(this.btnOpenAssayManage);
            this.panelEx2.Controls.Add(this.btnOpenTrainTipper);
            this.panelEx2.Controls.Add(this.btnOpenAutoCupboard);
            this.panelEx2.Controls.Add(this.btnCZGK);
            this.panelEx2.Controls.Add(this.btnOpenMainPage);
            this.panelEx2.Controls.Add(this.buttonX6);
            this.panelEx2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panelEx2.Location = new System.Drawing.Point(3, 59);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(1285, 44);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelBackground;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 0;
            // 
            // btnJoinBacthManage
            // 
            this.btnJoinBacthManage.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnJoinBacthManage.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnJoinBacthManage.AutoExpandOnClick = true;
            this.btnJoinBacthManage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnJoinBacthManage.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnJoinBacthManage.Location = new System.Drawing.Point(1077, 7);
            this.btnJoinBacthManage.Name = "btnJoinBacthManage";
            this.btnJoinBacthManage.Size = new System.Drawing.Size(108, 31);
            this.btnJoinBacthManage.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnJoinBacthManage.TabIndex = 21;
            this.btnJoinBacthManage.Text = "合样归批机";
            this.btnJoinBacthManage.TextColor = System.Drawing.Color.Black;
            this.btnJoinBacthManage.Click += new System.EventHandler(this.btnJoinBacthManage_Click);
            // 
            // buttonX1
            // 
            this.buttonX1.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.buttonX1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.buttonX1.AutoExpandOnClick = true;
            this.buttonX1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.buttonX1.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.buttonX1.Location = new System.Drawing.Point(1191, 7);
            this.buttonX1.Name = "buttonX1";
            this.buttonX1.Size = new System.Drawing.Size(108, 31);
            this.buttonX1.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.buttonX1.TabIndex = 22;
            this.buttonX1.Text = "退出";
            this.buttonX1.TextColor = System.Drawing.Color.Black;
            this.buttonX1.Click += new System.EventHandler(this.btnApplicationExit_Click);
            // 
            // btnOpenGuardInfo
            // 
            this.btnOpenGuardInfo.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOpenGuardInfo.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnOpenGuardInfo.AutoExpandOnClick = true;
            this.btnOpenGuardInfo.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnOpenGuardInfo.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenGuardInfo.Location = new System.Drawing.Point(963, 7);
            this.btnOpenGuardInfo.Name = "btnOpenGuardInfo";
            this.btnOpenGuardInfo.Size = new System.Drawing.Size(108, 31);
            this.btnOpenGuardInfo.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnOpenGuardInfo.TabIndex = 17;
            this.btnOpenGuardInfo.Text = "门禁记录";
            this.btnOpenGuardInfo.TextColor = System.Drawing.Color.Black;
            this.btnOpenGuardInfo.Click += new System.EventHandler(this.btnOpenGuardInfo_Click);
            // 
            // btnOpenEquInfHitch
            // 
            this.btnOpenEquInfHitch.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOpenEquInfHitch.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnOpenEquInfHitch.AutoExpandOnClick = true;
            this.btnOpenEquInfHitch.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnOpenEquInfHitch.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenEquInfHitch.Location = new System.Drawing.Point(849, 7);
            this.btnOpenEquInfHitch.Name = "btnOpenEquInfHitch";
            this.btnOpenEquInfHitch.Size = new System.Drawing.Size(108, 31);
            this.btnOpenEquInfHitch.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnOpenEquInfHitch.TabIndex = 16;
            this.btnOpenEquInfHitch.Text = "设备异常";
            this.btnOpenEquInfHitch.TextColor = System.Drawing.Color.Black;
            this.btnOpenEquInfHitch.Click += new System.EventHandler(this.btnOpenEquInfHitch_Click);
            // 
            // btnCarMonitor
            // 
            this.btnCarMonitor.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnCarMonitor.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnCarMonitor.AutoExpandOnClick = true;
            this.btnCarMonitor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnCarMonitor.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCarMonitor.Location = new System.Drawing.Point(713, 7);
            this.btnCarMonitor.Name = "btnCarMonitor";
            this.btnCarMonitor.Size = new System.Drawing.Size(130, 31);
            this.btnCarMonitor.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnCarMonitor.TabIndex = 15;
            this.btnCarMonitor.Text = "汽车设备监控";
            this.btnCarMonitor.TextColor = System.Drawing.Color.Black;
            this.btnCarMonitor.Click += new System.EventHandler(this.btnCarMonitor_Click);
            // 
            // btnOpenAssayManage
            // 
            this.btnOpenAssayManage.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOpenAssayManage.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnOpenAssayManage.AutoExpandOnClick = true;
            this.btnOpenAssayManage.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnOpenAssayManage.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenAssayManage.Location = new System.Drawing.Point(599, 7);
            this.btnOpenAssayManage.Name = "btnOpenAssayManage";
            this.btnOpenAssayManage.Size = new System.Drawing.Size(108, 31);
            this.btnOpenAssayManage.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnOpenAssayManage.TabIndex = 13;
            this.btnOpenAssayManage.Text = "化验室管理";
            this.btnOpenAssayManage.TextColor = System.Drawing.Color.Black;
            this.btnOpenAssayManage.Click += new System.EventHandler(this.btnOpenAssayManage_Click);
            // 
            // btnOpenTrainTipper
            // 
            this.btnOpenTrainTipper.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOpenTrainTipper.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnOpenTrainTipper.AutoExpandOnClick = true;
            this.btnOpenTrainTipper.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnOpenTrainTipper.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenTrainTipper.Location = new System.Drawing.Point(231, 7);
            this.btnOpenTrainTipper.Name = "btnOpenTrainTipper";
            this.btnOpenTrainTipper.Size = new System.Drawing.Size(134, 31);
            this.btnOpenTrainTipper.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnOpenTrainTipper.TabIndex = 12;
            this.btnOpenTrainTipper.Text = "翻 车 机 监 控";
            this.btnOpenTrainTipper.TextColor = System.Drawing.Color.Black;
            this.btnOpenTrainTipper.Click += new System.EventHandler(this.btnOpenTrainTipper_Click);
            // 
            // btnOpenAutoCupboard
            // 
            this.btnOpenAutoCupboard.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnOpenAutoCupboard.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.btnOpenAutoCupboard.AutoExpandOnClick = true;
            this.btnOpenAutoCupboard.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnOpenAutoCupboard.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnOpenAutoCupboard.Location = new System.Drawing.Point(485, 7);
            this.btnOpenAutoCupboard.Name = "btnOpenAutoCupboard";
            this.btnOpenAutoCupboard.Size = new System.Drawing.Size(108, 31);
            this.btnOpenAutoCupboard.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnOpenAutoCupboard.TabIndex = 11;
            this.btnOpenAutoCupboard.Text = "气 动 传 输";
            this.btnOpenAutoCupboard.TextColor = System.Drawing.Color.Black;
            this.btnOpenAutoCupboard.Click += new System.EventHandler(this.btnOpenAutoCupboard_Click);
            // 
            // superTabControl1
            // 
            this.superTabControl1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51)))));
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
            this.superTabControl1.Controls.Add(this.superTabControlPanel1);
            this.superTabControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.superTabControl1.ForeColor = System.Drawing.Color.White;
            this.superTabControl1.Location = new System.Drawing.Point(0, 106);
            this.superTabControl1.Margin = new System.Windows.Forms.Padding(0);
            this.superTabControl1.Name = "superTabControl1";
            this.superTabControl1.ReorderTabsEnabled = true;
            this.superTabControl1.SelectedTabFont = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.superTabControl1.SelectedTabIndex = 0;
            this.superTabControl1.Size = new System.Drawing.Size(1291, 640);
            this.superTabControl1.TabFont = new System.Drawing.Font("Segoe UI", 9F);
            this.superTabControl1.TabIndex = 9;
            this.superTabControl1.Tabs.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.superTabItem1});
            this.superTabControl1.TabsVisible = false;
            this.superTabControl1.Text = "superTabControl_Main";
            // 
            // superTabControlPanel1
            // 
            this.superTabControlPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.superTabControlPanel1.Location = new System.Drawing.Point(0, 30);
            this.superTabControlPanel1.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.superTabControlPanel1.Name = "superTabControlPanel1";
            this.superTabControlPanel1.Size = new System.Drawing.Size(1291, 610);
            this.superTabControlPanel1.TabIndex = 1;
            this.superTabControlPanel1.TabItem = this.superTabItem1;
            // 
            // superTabItem1
            // 
            this.superTabItem1.AttachedControl = this.superTabControlPanel1;
            this.superTabItem1.GlobalItem = false;
            this.superTabItem1.Name = "superTabItem1";
            this.superTabItem1.Text = "superTabItem1";
            // 
            // flpanEquipments
            // 
            this.flpanEquipments.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(82)))), ((int)(((byte)(89)))));
            this.flpanEquipments.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpanEquipments.ForeColor = System.Drawing.Color.White;
            this.flpanEquipments.Location = new System.Drawing.Point(3, 749);
            this.flpanEquipments.Name = "flpanEquipments";
            this.flpanEquipments.Size = new System.Drawing.Size(1285, 28);
            this.flpanEquipments.TabIndex = 10;
            // 
            // timer_CurrentTime
            // 
            this.timer_CurrentTime.Enabled = true;
            this.timer_CurrentTime.Interval = 1000;
            this.timer_CurrentTime.Tick += new System.EventHandler(this.timer_CurrentTime_Tick);
            // 
            // timer_EquipmentStatus
            // 
            this.timer_EquipmentStatus.Interval = 30000;
            this.timer_EquipmentStatus.Tick += new System.EventHandler(this.timer_EquipmentStatus_Tick);
            // 
            // timer_MsgTime
            // 
            this.timer_MsgTime.Enabled = true;
            this.timer_MsgTime.Interval = 1000;
            this.timer_MsgTime.Tick += new System.EventHandler(this.timer_MsgTime_Tick);
            // 
            // btnOpenSaleFuelLoad
            // 
            this.btnOpenSaleFuelLoad.Name = "btnOpenSaleFuelLoad";
            this.btnOpenSaleFuelLoad.Text = "汽车出场记录查询";
            this.btnOpenSaleFuelLoad.Click += new System.EventHandler(this.btnOpenSaleFuelLoad_Click);
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.pictureBox1.ForeColor = System.Drawing.Color.White;
            this.pictureBox1.Image = global::CMCS.Monitor.Win.Properties.Resources.CompanyLogo;
            this.pictureBox1.Location = new System.Drawing.Point(6, 3);
            this.pictureBox1.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(234, 53);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox1.TabIndex = 0;
            this.pictureBox1.TabStop = false;
            // 
            // FrmMainFrame
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1291, 780);
            this.Controls.Add(this.tableLayoutPanel1);
            this.ForeColor = System.Drawing.Color.White;
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(1022, 726);
            this.Name = "FrmMainFrame";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "武汉博晟燃料集中管控系统";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panelEx2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.superTabControl1)).EndInit();
            this.superTabControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.ButtonItem buttonItem2;
        private DevComponents.DotNetBar.ButtonItem buttonItem1;
        private DevComponents.DotNetBar.ButtonX btnOpenMainPage;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label lblSystemName;
        private System.Windows.Forms.Label lblCurrentTime;
        private DevComponents.DotNetBar.ButtonX btnCZGK;
        private DevComponents.DotNetBar.ButtonX buttonX6;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private DevComponents.DotNetBar.SuperTabControl superTabControl1;
        private DevComponents.DotNetBar.SuperTabControlPanel superTabControlPanel1;
        private DevComponents.DotNetBar.SuperTabItem superTabItem1;
        private DevComponents.DotNetBar.ButtonItem btnOpenTrainBeltSampler;
        private DevComponents.DotNetBar.ButtonItem btnOpenAutoMaker;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private System.Windows.Forms.Timer timer_CurrentTime;
        private DevComponents.DotNetBar.ButtonItem btnOpenWeightBridgeLoad;
        private DevComponents.DotNetBar.ButtonX btnOpenAutoCupboard;
        private System.Windows.Forms.FlowLayoutPanel flpanEquipments;
        private System.Windows.Forms.ToolTip toolTip1;
        private System.Windows.Forms.Timer timer_EquipmentStatus;
        private System.Windows.Forms.Timer timer_MsgTime;
        private DevComponents.DotNetBar.ButtonX btnOpenTrainTipper;
        private DevComponents.DotNetBar.ButtonItem btnOpenCarSampler;
        private DevComponents.DotNetBar.ButtonX btnOpenAssayManage;
        private DevComponents.DotNetBar.ButtonX btnCarMonitor;
        private DevComponents.DotNetBar.ButtonItem btnOpenBuyFuelLoad;
        private DevComponents.DotNetBar.ButtonX btnOpenEquInfHitch;
        private DevComponents.DotNetBar.ButtonX btnOpenGuardInfo;
        private DevComponents.DotNetBar.ButtonX buttonX1;
        //private DevComponents.DotNetBar.ButtonX btnPoundInfo;
        //private DevComponents.DotNetBar.ButtonX btnInOutInfo;
        private DevComponents.DotNetBar.ButtonX btnJoinBacthManage;
        private DevComponents.DotNetBar.ButtonItem btnOpenPoundInfo;
        private DevComponents.DotNetBar.ButtonItem btnOpenInOutInfo;
        private DevComponents.DotNetBar.ButtonItem btnOpenOutBeltSampler;
        private DevComponents.DotNetBar.ButtonItem btnOpenSaleFuelLoad;
        //private DevComponents.DotNetBar.ButtonItem btnOpenPoundInfo;
        //private DevComponents.DotNetBar.ButtonItem btnOpenInOutInfo;
        
    }
}

