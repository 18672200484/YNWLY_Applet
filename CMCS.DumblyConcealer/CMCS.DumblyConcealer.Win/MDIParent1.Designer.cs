﻿namespace CMCS.DumblyConcealer.Win
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
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MDIParent1));
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.tsmiTasks = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOpenFrmWeightBridger = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOpenFrmBeltSampler_NCGM = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOpenFrmAutoMaker_NCGM = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOpenFrmAutoCupboard_NCGM = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOpenFrmAssayDevice = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOpenFrmCarSampler_CSKY = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOpenFrmDataHandler = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOpenPackingBatch = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOpenPneumaticTransfer = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOpenPLC = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOpenTemperator = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOpenTemperatureHumidity = new System.Windows.Forms.ToolStripMenuItem();
            this.tsmiOpenHighBeltWeight = new System.Windows.Forms.ToolStripMenuItem();
            this.tsiOpenSignalData = new System.Windows.Forms.ToolStripMenuItem();
            this.windowsMenu = new System.Windows.Forms.ToolStripMenuItem();
            this.cascadeToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tileVerticalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tileHorizontalToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip = new System.Windows.Forms.StatusStrip();
            this.toolStripStatusLabel = new System.Windows.Forms.ToolStripStatusLabel();
            this.tsslblVersion = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolTip = new System.Windows.Forms.ToolTip(this.components);
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.menuStrip.SuspendLayout();
            this.statusStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiTasks,
            this.windowsMenu});
            this.menuStrip.Location = new System.Drawing.Point(0, 0);
            this.menuStrip.MdiWindowListItem = this.windowsMenu;
            this.menuStrip.Name = "menuStrip";
            this.menuStrip.Size = new System.Drawing.Size(632, 25);
            this.menuStrip.TabIndex = 0;
            this.menuStrip.Text = "MenuStrip";
            // 
            // tsmiTasks
            // 
            this.tsmiTasks.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsmiOpenFrmWeightBridger,
            this.tsmiOpenFrmBeltSampler_NCGM,
            this.tsmiOpenFrmAutoMaker_NCGM,
            this.tsmiOpenFrmAutoCupboard_NCGM,
            this.tsmiOpenFrmAssayDevice,
            this.tsmiOpenFrmCarSampler_CSKY,
            this.tsmiOpenFrmDataHandler,
            this.tsmiOpenPackingBatch,
            this.tsmiOpenPneumaticTransfer,
            this.tsmiOpenPLC,
            this.tsmiOpenTemperator,
            this.tsmiOpenTemperatureHumidity,
            this.tsmiOpenHighBeltWeight,
            this.tsiOpenSignalData});
            this.tsmiTasks.ImageTransparentColor = System.Drawing.SystemColors.ActiveBorder;
            this.tsmiTasks.Name = "tsmiTasks";
            this.tsmiTasks.Size = new System.Drawing.Size(59, 21);
            this.tsmiTasks.Text = "任务(&T)";
            // 
            // tsmiOpenFrmWeightBridger
            // 
            this.tsmiOpenFrmWeightBridger.ImageTransparentColor = System.Drawing.Color.Black;
            this.tsmiOpenFrmWeightBridger.Name = "tsmiOpenFrmWeightBridger";
            this.tsmiOpenFrmWeightBridger.Size = new System.Drawing.Size(285, 22);
            this.tsmiOpenFrmWeightBridger.Text = "01.同步轨道衡数据、入厂车号识别数据";
            this.tsmiOpenFrmWeightBridger.Click += new System.EventHandler(this.tsmiOpenFrmWeightBridger_Click);
            // 
            // tsmiOpenFrmBeltSampler_NCGM
            // 
            this.tsmiOpenFrmBeltSampler_NCGM.Name = "tsmiOpenFrmBeltSampler_NCGM";
            this.tsmiOpenFrmBeltSampler_NCGM.Size = new System.Drawing.Size(285, 22);
            this.tsmiOpenFrmBeltSampler_NCGM.Text = "02.皮带采样机接口";
            this.tsmiOpenFrmBeltSampler_NCGM.Click += new System.EventHandler(this.tsmiOpenFrmBeltSampler_Click);
            // 
            // tsmiOpenFrmAutoMaker_NCGM
            // 
            this.tsmiOpenFrmAutoMaker_NCGM.Name = "tsmiOpenFrmAutoMaker_NCGM";
            this.tsmiOpenFrmAutoMaker_NCGM.Size = new System.Drawing.Size(285, 22);
            this.tsmiOpenFrmAutoMaker_NCGM.Text = "03.全自动制样机接口";
            this.tsmiOpenFrmAutoMaker_NCGM.Click += new System.EventHandler(this.tsmiOpenFrmAutoMaker_NCGM_Click);
            // 
            // tsmiOpenFrmAutoCupboard_NCGM
            // 
            this.tsmiOpenFrmAutoCupboard_NCGM.Name = "tsmiOpenFrmAutoCupboard_NCGM";
            this.tsmiOpenFrmAutoCupboard_NCGM.Size = new System.Drawing.Size(285, 22);
            this.tsmiOpenFrmAutoCupboard_NCGM.Text = "04.智能存样柜、气动传输接口";
            this.tsmiOpenFrmAutoCupboard_NCGM.Click += new System.EventHandler(this.tsmiOpenFrmAutoCupboard_NCGM_Click);
            // 
            // tsmiOpenFrmAssayDevice
            // 
            this.tsmiOpenFrmAssayDevice.Name = "tsmiOpenFrmAssayDevice";
            this.tsmiOpenFrmAssayDevice.Size = new System.Drawing.Size(285, 22);
            this.tsmiOpenFrmAssayDevice.Text = "05.生成标准化验数据";
            this.tsmiOpenFrmAssayDevice.Click += new System.EventHandler(this.tsmiOpenFrmAssayDevice_Click);
            // 
            // tsmiOpenFrmCarSampler_CSKY
            // 
            this.tsmiOpenFrmCarSampler_CSKY.Name = "tsmiOpenFrmCarSampler_CSKY";
            this.tsmiOpenFrmCarSampler_CSKY.Size = new System.Drawing.Size(285, 22);
            this.tsmiOpenFrmCarSampler_CSKY.Text = "07.汽车机械采样机接口";
            this.tsmiOpenFrmCarSampler_CSKY.Click += new System.EventHandler(this.tsmiOpenFrmCarSampler_CSKY_Click);
            // 
            // tsmiOpenFrmDataHandler
            // 
            this.tsmiOpenFrmDataHandler.Name = "tsmiOpenFrmDataHandler";
            this.tsmiOpenFrmDataHandler.Size = new System.Drawing.Size(285, 22);
            this.tsmiOpenFrmDataHandler.Text = "08.综合事件处理";
            this.tsmiOpenFrmDataHandler.Click += new System.EventHandler(this.tsmiOpenFrmCarSynchronous_Click);
            // 
            // tsmiOpenPackingBatch
            // 
            this.tsmiOpenPackingBatch.Name = "tsmiOpenPackingBatch";
            this.tsmiOpenPackingBatch.Size = new System.Drawing.Size(285, 22);
            this.tsmiOpenPackingBatch.Text = "09.矩阵合样归批机接口";
            this.tsmiOpenPackingBatch.Click += new System.EventHandler(this.tsmiOpenPackingBatch_Click);
            // 
            // tsmiOpenPneumaticTransfer
            // 
            this.tsmiOpenPneumaticTransfer.Name = "tsmiOpenPneumaticTransfer";
            this.tsmiOpenPneumaticTransfer.Size = new System.Drawing.Size(285, 22);
            this.tsmiOpenPneumaticTransfer.Text = "10.气动传输接口";
            this.tsmiOpenPneumaticTransfer.Click += new System.EventHandler(this.tsmiOpenPneumaticTransfer_Click);
            // 
            // tsmiOpenPLC
            // 
            this.tsmiOpenPLC.Name = "tsmiOpenPLC";
            this.tsmiOpenPLC.Size = new System.Drawing.Size(285, 22);
            this.tsmiOpenPLC.Text = "11.PLC下位机接口";
            this.tsmiOpenPLC.Click += new System.EventHandler(this.tsmiOpenPLC_Click);
            // 
            // tsmiOpenTemperator
            // 
            this.tsmiOpenTemperator.Name = "tsmiOpenTemperator";
            this.tsmiOpenTemperator.Size = new System.Drawing.Size(285, 22);
            this.tsmiOpenTemperator.Text = "12.煤场测温仪接口";
            this.tsmiOpenTemperator.Click += new System.EventHandler(this.tsmiOpenTemperator_Click);
            // 
            // tsmiOpenTemperatureHumidity
            // 
            this.tsmiOpenTemperatureHumidity.Name = "tsmiOpenTemperatureHumidity";
            this.tsmiOpenTemperatureHumidity.Size = new System.Drawing.Size(285, 22);
            this.tsmiOpenTemperatureHumidity.Text = "13.化验室温湿度仪接口";
            this.tsmiOpenTemperatureHumidity.Click += new System.EventHandler(this.tsmiOpenTemperatureHumidity_Click);
            // 
            // tsmiOpenHighBeltWeight
            // 
            this.tsmiOpenHighBeltWeight.Name = "tsmiOpenHighBeltWeight";
            this.tsmiOpenHighBeltWeight.Size = new System.Drawing.Size(285, 22);
            this.tsmiOpenHighBeltWeight.Text = "14.高精度皮带秤接口";
            this.tsmiOpenHighBeltWeight.Click += new System.EventHandler(this.tsmiOpenHighBeltWeight_Click);
            // 
            // tsiOpenSignalData
            // 
            this.tsiOpenSignalData.Name = "tsiOpenSignalData";
            this.tsiOpenSignalData.Size = new System.Drawing.Size(285, 22);
            this.tsiOpenSignalData.Text = "15.实时信号同步";
            this.tsiOpenSignalData.Click += new System.EventHandler(this.tsiOpenSignalData_Click);
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
            // timer1
            // 
            this.timer1.Interval = 2000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
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
            this.Text = "燃料集中管控后台处理程序";
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
        private System.Windows.Forms.ToolStripMenuItem tsmiTasks;
        private System.Windows.Forms.ToolStripMenuItem tsmiOpenFrmWeightBridger;
        private System.Windows.Forms.ToolStripMenuItem windowsMenu;
        private System.Windows.Forms.ToolStripMenuItem cascadeToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem tileVerticalToolStripMenuItem;
        private System.Windows.Forms.ToolTip toolTip;
        private System.Windows.Forms.ToolStripStatusLabel tsslblVersion;
        private System.Windows.Forms.ToolStripMenuItem tsmiOpenFrmBeltSampler_NCGM;
        private System.Windows.Forms.ToolStripMenuItem tsmiOpenFrmAutoMaker_NCGM;
        private System.Windows.Forms.ToolStripMenuItem tsmiOpenFrmAutoCupboard_NCGM;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.ToolStripMenuItem tsmiOpenFrmAssayDevice;
        private System.Windows.Forms.ToolStripMenuItem tsmiOpenFrmCarSampler_CSKY;
        private System.Windows.Forms.ToolStripMenuItem tsmiOpenFrmDataHandler;
        private System.Windows.Forms.ToolStripMenuItem tsmiOpenPackingBatch;
        private System.Windows.Forms.ToolStripMenuItem tsmiOpenPneumaticTransfer;
        private System.Windows.Forms.ToolStripMenuItem tsmiOpenPLC;
        private System.Windows.Forms.ToolStripMenuItem tsmiOpenTemperator;
        private System.Windows.Forms.ToolStripMenuItem tsmiOpenTemperatureHumidity;
        private System.Windows.Forms.ToolStripMenuItem tsmiOpenHighBeltWeight;
        private System.Windows.Forms.ToolStripMenuItem tsiOpenSignalData;
    }
}



