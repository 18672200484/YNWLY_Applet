namespace CMCS.DataTester
{
    partial class MDIParent1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MDIParent1));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.tsmiData = new System.Windows.Forms.ToolStripMenuItem();
            this.btnOpenFrmBuildTrainWeightRecord = new System.Windows.Forms.ToolStripMenuItem();
            this.btnOpenFrmBuildTrainCarriagePass = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiSimulator = new System.Windows.Forms.ToolStripMenuItem();
            this.btnOpenFrmIOSimulator = new System.Windows.Forms.ToolStripMenuItem();
            this.btnOpenFrmWB231Simulator = new System.Windows.Forms.ToolStripMenuItem();
            this.btnOpenFrmWB245Simulator = new System.Windows.Forms.ToolStripMenuItem();
            this.btnOpenFrmBeltSamplerSimulator = new System.Windows.Forms.ToolStripMenuItem();
            this.btnOpenFrmAutoMakerSimulator = new System.Windows.Forms.ToolStripMenuItem();
            this.btnOpenFrmCarJxSamplerSimulator = new System.Windows.Forms.ToolStripMenuItem();
            this.btnOpenFrmAutoCupBoard_Test = new System.Windows.Forms.ToolStripMenuItem();
            this.btnOpenThinkCamera = new System.Windows.Forms.ToolStripMenuItem();
            this.耀华地磅ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.进制转换ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.HFReader = new System.Windows.Forms.ToolStripMenuItem();
            this.QRCode = new System.Windows.Forms.ToolStripMenuItem();
            this.btnOpenFinger = new System.Windows.Forms.ToolStripMenuItem();
            this.tmisTest = new System.Windows.Forms.ToolStripMenuItem();
            this.pLC测试ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.modBus测试ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.赛多利斯电子天平模拟工具ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.添加管理凭据ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.湘平电子秤ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.赛摩皮带采样机测试ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.windowsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.cascadeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tileVerticalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tileHorizontalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslblVersion = new System.Windows.Forms.ToolStripStatusLabel();
            this.四舍六入测试ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiData,
            this.tsmiSimulator,
            this.windowsMenu});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.MdiWindowListItem = this.windowsMenu;
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(632, 25);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "MenuStrip";
            // 
            // tsmiData
            // 
            this.tsmiData.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnOpenFrmBuildTrainWeightRecord,
            this.btnOpenFrmBuildTrainCarriagePass});
            this.tsmiData.ImageTransparentColor = System.Drawing.SystemColors.ActiveBorder;
            this.tsmiData.Name = "tsmiData";
            this.tsmiData.Size = new System.Drawing.Size(59, 21);
            this.tsmiData.Text = "数据(&T)";
            // 
            // btnOpenFrmBuildTrainWeightRecord
            // 
            this.btnOpenFrmBuildTrainWeightRecord.ImageTransparentColor = System.Drawing.Color.Black;
            this.btnOpenFrmBuildTrainWeightRecord.Name = "btnOpenFrmBuildTrainWeightRecord";
            this.btnOpenFrmBuildTrainWeightRecord.Size = new System.Drawing.Size(172, 22);
            this.btnOpenFrmBuildTrainWeightRecord.Text = "火车入厂数据生成";
            this.btnOpenFrmBuildTrainWeightRecord.Click += new System.EventHandler(this.btnOpenFrmBuildTrainWeightRecord_Click);
            // 
            // btnOpenFrmBuildTrainCarriagePass
            // 
            this.btnOpenFrmBuildTrainCarriagePass.Name = "btnOpenFrmBuildTrainCarriagePass";
            this.btnOpenFrmBuildTrainCarriagePass.Size = new System.Drawing.Size(172, 22);
            this.btnOpenFrmBuildTrainCarriagePass.Text = "车号识别数据生成";
            this.btnOpenFrmBuildTrainCarriagePass.Click += new System.EventHandler(this.btnOpenFrmBuildTrainCarriagePass_Click);
            // 
            // tsmiSimulator
            // 
            this.tsmiSimulator.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.btnOpenFrmIOSimulator,
            this.btnOpenFrmWB231Simulator,
            this.btnOpenFrmWB245Simulator,
            this.btnOpenFrmBeltSamplerSimulator,
            this.btnOpenFrmAutoMakerSimulator,
            this.btnOpenFrmCarJxSamplerSimulator,
            this.btnOpenFrmAutoCupBoard_Test,
            this.btnOpenThinkCamera,
            this.耀华地磅ToolStripMenuItem,
            this.进制转换ToolStripMenuItem,
            this.HFReader,
            this.QRCode,
            this.btnOpenFinger,
            this.tmisTest,
            this.pLC测试ToolStripMenuItem,
            this.modBus测试ToolStripMenuItem,
            this.赛多利斯电子天平模拟工具ToolStripMenuItem,
            this.添加管理凭据ToolStripMenuItem,
            this.湘平电子秤ToolStripMenuItem,
            this.赛摩皮带采样机测试ToolStripMenuItem,
            this.四舍六入测试ToolStripMenuItem});
            this.tsmiSimulator.Name = "tsmiSimulator";
            this.tsmiSimulator.Size = new System.Drawing.Size(59, 21);
            this.tsmiSimulator.Text = "模拟(&S)";
            // 
            // btnOpenFrmIOSimulator
            // 
            this.btnOpenFrmIOSimulator.Name = "btnOpenFrmIOSimulator";
            this.btnOpenFrmIOSimulator.Size = new System.Drawing.Size(240, 22);
            this.btnOpenFrmIOSimulator.Text = "精敏IO控制器模拟";
            this.btnOpenFrmIOSimulator.Click += new System.EventHandler(this.btnOpenFrmIOSimulator_Click);
            // 
            // btnOpenFrmWB231Simulator
            // 
            this.btnOpenFrmWB231Simulator.Name = "btnOpenFrmWB231Simulator";
            this.btnOpenFrmWB231Simulator.Size = new System.Drawing.Size(240, 22);
            this.btnOpenFrmWB231Simulator.Text = "托利多电子秤IND231模拟工具";
            this.btnOpenFrmWB231Simulator.Click += new System.EventHandler(this.btnOpenFrmWB231Simulator_Click);
            // 
            // btnOpenFrmWB245Simulator
            // 
            this.btnOpenFrmWB245Simulator.Name = "btnOpenFrmWB245Simulator";
            this.btnOpenFrmWB245Simulator.Size = new System.Drawing.Size(240, 22);
            this.btnOpenFrmWB245Simulator.Text = "托利多地磅IND245模拟工具";
            this.btnOpenFrmWB245Simulator.Click += new System.EventHandler(this.btnOpenFrmWBSimulator_Click);
            // 
            // btnOpenFrmBeltSamplerSimulator
            // 
            this.btnOpenFrmBeltSamplerSimulator.Name = "btnOpenFrmBeltSamplerSimulator";
            this.btnOpenFrmBeltSamplerSimulator.Size = new System.Drawing.Size(240, 22);
            this.btnOpenFrmBeltSamplerSimulator.Text = "皮带采样机模拟";
            this.btnOpenFrmBeltSamplerSimulator.Click += new System.EventHandler(this.btnOpenFrmBeltSamplerSimulator_Click);
            // 
            // btnOpenFrmAutoMakerSimulator
            // 
            this.btnOpenFrmAutoMakerSimulator.Name = "btnOpenFrmAutoMakerSimulator";
            this.btnOpenFrmAutoMakerSimulator.Size = new System.Drawing.Size(240, 22);
            this.btnOpenFrmAutoMakerSimulator.Text = "全自动制样机模拟";
            this.btnOpenFrmAutoMakerSimulator.Click += new System.EventHandler(this.btnOpenFrmAutoMakerSimulator_Click);
            // 
            // btnOpenFrmCarJxSamplerSimulator
            // 
            this.btnOpenFrmCarJxSamplerSimulator.Name = "btnOpenFrmCarJxSamplerSimulator";
            this.btnOpenFrmCarJxSamplerSimulator.Size = new System.Drawing.Size(240, 22);
            this.btnOpenFrmCarJxSamplerSimulator.Text = "汽车机械采样机模拟";
            this.btnOpenFrmCarJxSamplerSimulator.Click += new System.EventHandler(this.btnOpenFrmCarJxSamplerSimulator_Click);
            // 
            // btnOpenFrmAutoCupBoard_Test
            // 
            this.btnOpenFrmAutoCupBoard_Test.Name = "btnOpenFrmAutoCupBoard_Test";
            this.btnOpenFrmAutoCupBoard_Test.Size = new System.Drawing.Size(240, 22);
            this.btnOpenFrmAutoCupBoard_Test.Text = "智能存样柜模拟工具";
            this.btnOpenFrmAutoCupBoard_Test.Click += new System.EventHandler(this.btnOpenFrmAutoCupBoard_Test_Click);
            // 
            // btnOpenThinkCamera
            // 
            this.btnOpenThinkCamera.Name = "btnOpenThinkCamera";
            this.btnOpenThinkCamera.Size = new System.Drawing.Size(240, 22);
            this.btnOpenThinkCamera.Text = "通通停车视频";
            this.btnOpenThinkCamera.Click += new System.EventHandler(this.btnOpenThinkCamera_Click);
            // 
            // 耀华地磅ToolStripMenuItem
            // 
            this.耀华地磅ToolStripMenuItem.Name = "耀华地磅ToolStripMenuItem";
            this.耀华地磅ToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            this.耀华地磅ToolStripMenuItem.Text = "耀华地磅";
            this.耀华地磅ToolStripMenuItem.Click += new System.EventHandler(this.耀华地磅ToolStripMenuItem_Click);
            // 
            // 进制转换ToolStripMenuItem
            // 
            this.进制转换ToolStripMenuItem.Name = "进制转换ToolStripMenuItem";
            this.进制转换ToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            this.进制转换ToolStripMenuItem.Text = "进制转换";
            this.进制转换ToolStripMenuItem.Click += new System.EventHandler(this.进制转换ToolStripMenuItem_Click);
            // 
            // HFReader
            // 
            this.HFReader.Name = "HFReader";
            this.HFReader.Size = new System.Drawing.Size(240, 22);
            this.HFReader.Text = "开元芯片读卡器";
            this.HFReader.Click += new System.EventHandler(this.HFReader_Click);
            // 
            // QRCode
            // 
            this.QRCode.Name = "QRCode";
            this.QRCode.Size = new System.Drawing.Size(240, 22);
            this.QRCode.Text = "二维码生成";
            this.QRCode.Click += new System.EventHandler(this.QRCode_Click);
            // 
            // btnOpenFinger
            // 
            this.btnOpenFinger.Name = "btnOpenFinger";
            this.btnOpenFinger.Size = new System.Drawing.Size(240, 22);
            this.btnOpenFinger.Text = "指昂指纹识别";
            this.btnOpenFinger.Click += new System.EventHandler(this.btnOpenFinger_Click);
            // 
            // tmisTest
            // 
            this.tmisTest.Name = "tmisTest";
            this.tmisTest.Size = new System.Drawing.Size(240, 22);
            this.tmisTest.Text = "测试";
            this.tmisTest.Click += new System.EventHandler(this.tmisTest_Click);
            // 
            // pLC测试ToolStripMenuItem
            // 
            this.pLC测试ToolStripMenuItem.Name = "pLC测试ToolStripMenuItem";
            this.pLC测试ToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            this.pLC测试ToolStripMenuItem.Text = "PLC测试";
            this.pLC测试ToolStripMenuItem.Click += new System.EventHandler(this.pLC测试ToolStripMenuItem_Click);
            // 
            // modBus测试ToolStripMenuItem
            // 
            this.modBus测试ToolStripMenuItem.Name = "modBus测试ToolStripMenuItem";
            this.modBus测试ToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            this.modBus测试ToolStripMenuItem.Text = "ModBus测试";
            this.modBus测试ToolStripMenuItem.Click += new System.EventHandler(this.modBus测试ToolStripMenuItem_Click);
            // 
            // 赛多利斯电子天平模拟工具ToolStripMenuItem
            // 
            this.赛多利斯电子天平模拟工具ToolStripMenuItem.Name = "赛多利斯电子天平模拟工具ToolStripMenuItem";
            this.赛多利斯电子天平模拟工具ToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            this.赛多利斯电子天平模拟工具ToolStripMenuItem.Text = "赛多利斯电子天平模拟工具";
            this.赛多利斯电子天平模拟工具ToolStripMenuItem.Click += new System.EventHandler(this.赛多利斯电子天平模拟工具ToolStripMenuItem_Click);
            // 
            // 添加管理凭据ToolStripMenuItem
            // 
            this.添加管理凭据ToolStripMenuItem.Name = "添加管理凭据ToolStripMenuItem";
            this.添加管理凭据ToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            this.添加管理凭据ToolStripMenuItem.Text = "添加管理凭据";
            this.添加管理凭据ToolStripMenuItem.Click += new System.EventHandler(this.添加管理凭据ToolStripMenuItem_Click);
            // 
            // 湘平电子秤ToolStripMenuItem
            // 
            this.湘平电子秤ToolStripMenuItem.Name = "湘平电子秤ToolStripMenuItem";
            this.湘平电子秤ToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            this.湘平电子秤ToolStripMenuItem.Text = "湘平电子秤";
            this.湘平电子秤ToolStripMenuItem.Click += new System.EventHandler(this.湘平电子秤ToolStripMenuItem_Click);
            // 
            // 赛摩皮带采样机测试ToolStripMenuItem
            // 
            this.赛摩皮带采样机测试ToolStripMenuItem.Name = "赛摩皮带采样机测试ToolStripMenuItem";
            this.赛摩皮带采样机测试ToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            this.赛摩皮带采样机测试ToolStripMenuItem.Text = "赛摩皮带采样机测试";
            this.赛摩皮带采样机测试ToolStripMenuItem.Click += new System.EventHandler(this.赛摩皮带采样机测试ToolStripMenuItem_Click);
            // 
            // windowsMenu
            // 
            this.windowsMenu.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.cascadeToolStripMenuItem,
            this.tileVerticalToolStripMenuItem,
            this.tileHorizontalToolStripMenuItem});
            this.windowsMenu.Name = "windowsMenu";
            this.windowsMenu.Size = new System.Drawing.Size(64, 21);
            this.windowsMenu.Text = "窗口(&W)";
            // 
            // cascadeToolStripMenuItem
            // 
            this.cascadeToolStripMenuItem.Name = "cascadeToolStripMenuItem";
            this.cascadeToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.cascadeToolStripMenuItem.Text = "层叠(&C)";
            this.cascadeToolStripMenuItem.Click += new System.EventHandler(this.CascadeToolStripMenuItem_Click);
            // 
            // tileVerticalToolStripMenuItem
            // 
            this.tileVerticalToolStripMenuItem.Name = "tileVerticalToolStripMenuItem";
            this.tileVerticalToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.tileVerticalToolStripMenuItem.Text = "垂直平铺(&V)";
            this.tileVerticalToolStripMenuItem.Click += new System.EventHandler(this.TileVerticalToolStripMenuItem_Click);
            // 
            // tileHorizontalToolStripMenuItem
            // 
            this.tileHorizontalToolStripMenuItem.Name = "tileHorizontalToolStripMenuItem";
            this.tileHorizontalToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.tileHorizontalToolStripMenuItem.Text = "水平平铺(&H)";
            this.tileHorizontalToolStripMenuItem.Click += new System.EventHandler(this.TileHorizontalToolStripMenuItem_Click);
            // 
            // statusStrip
            // 
            this.statusStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripStatusLabel,
            this.tsslblVersion});
            this.statusStrip.Location = new System.Drawing.Point(0, 396);
            this.statusStrip.Name = "statusStrip";
            this.statusStrip.Size = new System.Drawing.Size(632, 22);
            this.statusStrip.TabIndex = 2;
            this.statusStrip.Text = "StatusStrip";
            // 
            // toolStripStatusLabel
            // 
            this.toolStripStatusLabel.Name = "toolStripStatusLabel";
            this.toolStripStatusLabel.Size = new System.Drawing.Size(44, 17);
            this.toolStripStatusLabel.Text = "版本：";
            // 
            // tsslblVersion
            // 
            this.tsslblVersion.Name = "tsslblVersion";
            this.tsslblVersion.Size = new System.Drawing.Size(45, 17);
            this.tsslblVersion.Text = "1.0.0.0";
            // 
            // 四舍六入测试ToolStripMenuItem
            // 
            this.四舍六入测试ToolStripMenuItem.Name = "四舍六入测试ToolStripMenuItem";
            this.四舍六入测试ToolStripMenuItem.Size = new System.Drawing.Size(240, 22);
            this.四舍六入测试ToolStripMenuItem.Text = "四舍六入测试";
            this.四舍六入测试ToolStripMenuItem.Click += new System.EventHandler(this.四舍六入测试ToolStripMenuItem_Click);
            // 
            // MDIParent1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(632, 418);
            this.Controls.Add(this.statusStrip);
            this.Controls.Add(this.menuStrip);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.menuStrip;
            this.Name = "MDIParent1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "燃料集中管控模拟测试工具";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MDIParent1_FormClosing);
            this.Load += new System.EventHandler(this.MDIParent1_Load);
            this.Shown += new System.EventHandler(this.MDIParent1_Shown);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.statusStrip.ResumeLayout(false);
            this.statusStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }
        #endregion


        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.StatusStrip statusStrip;
        private System.Windows.Forms.ToolStripStatusLabel toolStripStatusLabel;
        private System.Windows.Forms.ToolStripMenuItem tileHorizontalToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tsmiData;
        private System.Windows.Forms.ToolStripMenuItem btnOpenFrmBuildTrainWeightRecord;
        private System.Windows.Forms.ToolStripMenuItem windowsMenu;
        private System.Windows.Forms.ToolStripMenuItem cascadeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tileVerticalToolStripMenuItem;
        private System.Windows.Forms.ToolStripStatusLabel tsslblVersion;
        private System.Windows.Forms.ToolStripMenuItem btnOpenFrmBuildTrainCarriagePass;
        private System.Windows.Forms.ToolStripMenuItem tsmiSimulator;
        private System.Windows.Forms.ToolStripMenuItem btnOpenFrmIOSimulator;
        private System.Windows.Forms.ToolStripMenuItem btnOpenFrmWB245Simulator;
        private System.Windows.Forms.ToolStripMenuItem btnOpenFrmBeltSamplerSimulator;
        private System.Windows.Forms.ToolStripMenuItem btnOpenFrmAutoMakerSimulator;
        private System.Windows.Forms.ToolStripMenuItem btnOpenFrmCarJxSamplerSimulator;
        private System.Windows.Forms.ToolStripMenuItem btnOpenFrmWB231Simulator;
        private System.Windows.Forms.ToolStripMenuItem btnOpenFrmAutoCupBoard_Test;
        private System.Windows.Forms.ToolStripMenuItem btnOpenThinkCamera;
        private System.Windows.Forms.ToolStripMenuItem 耀华地磅ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 进制转换ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem HFReader;
        private System.Windows.Forms.ToolStripMenuItem QRCode;
        private System.Windows.Forms.ToolStripMenuItem btnOpenFinger;
        private System.Windows.Forms.ToolStripMenuItem tmisTest;
        private System.Windows.Forms.ToolStripMenuItem pLC测试ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem modBus测试ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 赛多利斯电子天平模拟工具ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 添加管理凭据ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 湘平电子秤ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 赛摩皮带采样机测试ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 四舍六入测试ToolStripMenuItem;
    }
}



