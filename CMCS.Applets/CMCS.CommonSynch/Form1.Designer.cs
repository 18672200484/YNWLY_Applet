﻿namespace CMCS.CommonSynch
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.metroStatusBar1 = new DevComponents.DotNetBar.Metro.MetroStatusBar();
            this.lblVersion = new DevComponents.DotNetBar.LabelItem();
            this.btnOpenFrmConfigSet = new DevComponents.DotNetBar.ButtonItem();
            this.styleManager1 = new DevComponents.DotNetBar.StyleManager(this.components);
            this.rtxtOutput = new System.Windows.Forms.RichTextBox();
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
            this.lblVersion,
            this.btnOpenFrmConfigSet});
            this.metroStatusBar1.Location = new System.Drawing.Point(2, 542);
            this.metroStatusBar1.Name = "metroStatusBar1";
            this.metroStatusBar1.Size = new System.Drawing.Size(839, 22);
            this.metroStatusBar1.TabIndex = 3;
            this.metroStatusBar1.Text = "metroStatusBar1";
            // 
            // lblVersion
            // 
            this.lblVersion.BorderSide = DevComponents.DotNetBar.eBorderSide.Left;
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Text = "版本：1.0.0.0";
            // 
            // btnOpenFrmConfigSet
            // 
            this.btnOpenFrmConfigSet.Name = "btnOpenFrmConfigSet";
            this.btnOpenFrmConfigSet.Text = "参数设置";
            this.btnOpenFrmConfigSet.Click += new System.EventHandler(this.btnOpenFrmConfigSet_Click);
            // 
            // styleManager1
            // 
            this.styleManager1.ManagerStyle = DevComponents.DotNetBar.eStyle.Metro;
            this.styleManager1.MetroColorParameters = new DevComponents.DotNetBar.Metro.ColorTables.MetroColorGeneratorParameters(System.Drawing.Color.FromArgb(((int)(((byte)(43)))), ((int)(((byte)(47)))), ((int)(((byte)(51))))), System.Drawing.Color.DarkTurquoise);
            // 
            // rtxtOutput
            // 
            this.rtxtOutput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.rtxtOutput.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.rtxtOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxtOutput.ForeColor = System.Drawing.Color.White;
            this.rtxtOutput.Location = new System.Drawing.Point(2, 2);
            this.rtxtOutput.Name = "rtxtOutput";
            this.rtxtOutput.Size = new System.Drawing.Size(839, 540);
            this.rtxtOutput.TabIndex = 4;
            this.rtxtOutput.Text = "";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(843, 566);
            this.Controls.Add(this.rtxtOutput);
            this.Controls.Add(this.metroStatusBar1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Name = "Form1";
            this.Text = "数据同步通用程序";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.Shown += new System.EventHandler(this.Form1_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.Metro.MetroStatusBar metroStatusBar1;
        private DevComponents.DotNetBar.LabelItem lblVersion;
        private DevComponents.DotNetBar.StyleManager styleManager1;
        private DevComponents.DotNetBar.ButtonItem btnOpenFrmConfigSet;
        private System.Windows.Forms.RichTextBox rtxtOutput;
    }
}

