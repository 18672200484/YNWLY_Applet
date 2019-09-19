namespace CMCS.DataTester.Frms
{
    partial class FrmAutoCupBoard_Test
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
            this.btnStart = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btnReset = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.btnSystemStatus_FSGZ = new System.Windows.Forms.Button();
            this.btnSystemStatus_JXDJ = new System.Windows.Forms.Button();
            this.btnSystemStatus_ZZYX = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rtxtOutput = new System.Windows.Forms.RichTextBox();
            this.eventLog1 = new System.Diagnostics.EventLog();
            this.btnInsert = new System.Windows.Forms.Button();
            this.txttype = new System.Windows.Forms.TextBox();
            this.txtcode = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.txtmechine = new System.Windows.Forms.TextBox();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.eventLog1)).BeginInit();
            this.SuspendLayout();
            // 
            // btnStart
            // 
            this.btnStart.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnStart.Location = new System.Drawing.Point(25, 27);
            this.btnStart.Name = "btnStart";
            this.btnStart.Size = new System.Drawing.Size(93, 27);
            this.btnStart.TabIndex = 3;
            this.btnStart.Text = "开始模拟";
            this.btnStart.UseVisualStyleBackColor = true;
            this.btnStart.Click += new System.EventHandler(this.btnStart_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.txtmechine);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txtcode);
            this.groupBox2.Controls.Add(this.txttype);
            this.groupBox2.Controls.Add(this.btnInsert);
            this.groupBox2.Controls.Add(this.btnReset);
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.btnStart);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(5, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(690, 100);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = " 操作 ";
            // 
            // btnReset
            // 
            this.btnReset.Font = new System.Drawing.Font("宋体", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(134)));
            this.btnReset.Location = new System.Drawing.Point(25, 60);
            this.btnReset.Name = "btnReset";
            this.btnReset.Size = new System.Drawing.Size(93, 27);
            this.btnReset.TabIndex = 9;
            this.btnReset.Text = "重置数据";
            this.btnReset.UseVisualStyleBackColor = true;
            this.btnReset.Click += new System.EventHandler(this.btnReset_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.btnSystemStatus_FSGZ);
            this.groupBox3.Controls.Add(this.btnSystemStatus_JXDJ);
            this.groupBox3.Controls.Add(this.btnSystemStatus_ZZYX);
            this.groupBox3.Location = new System.Drawing.Point(411, 27);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(268, 53);
            this.groupBox3.TabIndex = 8;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = " 设置系统状态 ";
            // 
            // btnSystemStatus_FSGZ
            // 
            this.btnSystemStatus_FSGZ.Location = new System.Drawing.Point(179, 20);
            this.btnSystemStatus_FSGZ.Name = "btnSystemStatus_FSGZ";
            this.btnSystemStatus_FSGZ.Size = new System.Drawing.Size(78, 23);
            this.btnSystemStatus_FSGZ.TabIndex = 7;
            this.btnSystemStatus_FSGZ.Text = "发生故障";
            this.btnSystemStatus_FSGZ.UseVisualStyleBackColor = true;
            this.btnSystemStatus_FSGZ.Click += new System.EventHandler(this.btnSystemStatus_FSGZ_Click);
            // 
            // btnSystemStatus_JXDJ
            // 
            this.btnSystemStatus_JXDJ.Location = new System.Drawing.Point(17, 20);
            this.btnSystemStatus_JXDJ.Name = "btnSystemStatus_JXDJ";
            this.btnSystemStatus_JXDJ.Size = new System.Drawing.Size(78, 23);
            this.btnSystemStatus_JXDJ.TabIndex = 4;
            this.btnSystemStatus_JXDJ.Text = "就绪待机";
            this.btnSystemStatus_JXDJ.UseVisualStyleBackColor = true;
            this.btnSystemStatus_JXDJ.Click += new System.EventHandler(this.btnSystemStatus_JXDJ_Click);
            // 
            // btnSystemStatus_ZZYX
            // 
            this.btnSystemStatus_ZZYX.Location = new System.Drawing.Point(98, 20);
            this.btnSystemStatus_ZZYX.Name = "btnSystemStatus_ZZYX";
            this.btnSystemStatus_ZZYX.Size = new System.Drawing.Size(78, 23);
            this.btnSystemStatus_ZZYX.TabIndex = 5;
            this.btnSystemStatus_ZZYX.Text = "正在运行";
            this.btnSystemStatus_ZZYX.UseVisualStyleBackColor = true;
            this.btnSystemStatus_ZZYX.Click += new System.EventHandler(this.btnSystemStatus_ZZYX_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rtxtOutput);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(5, 105);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(690, 307);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = " 输出 ";
            // 
            // rtxtOutput
            // 
            this.rtxtOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxtOutput.Location = new System.Drawing.Point(3, 17);
            this.rtxtOutput.Name = "rtxtOutput";
            this.rtxtOutput.Size = new System.Drawing.Size(684, 287);
            this.rtxtOutput.TabIndex = 2;
            this.rtxtOutput.Text = "";
            // 
            // eventLog1
            // 
            this.eventLog1.SynchronizingObject = this;
            // 
            // btnInsert
            // 
            this.btnInsert.Location = new System.Drawing.Point(327, 47);
            this.btnInsert.Name = "btnInsert";
            this.btnInsert.Size = new System.Drawing.Size(78, 23);
            this.btnInsert.TabIndex = 10;
            this.btnInsert.Text = "模拟存样";
            this.btnInsert.UseVisualStyleBackColor = true;
            this.btnInsert.Click += new System.EventHandler(this.btnInsert_Click);
            // 
            // txttype
            // 
            this.txttype.Location = new System.Drawing.Point(221, 47);
            this.txttype.Name = "txttype";
            this.txttype.Size = new System.Drawing.Size(100, 21);
            this.txttype.TabIndex = 11;
            this.txttype.Text = "3mm备查样";
            // 
            // txtcode
            // 
            this.txtcode.Location = new System.Drawing.Point(221, 20);
            this.txtcode.Name = "txtcode";
            this.txtcode.Size = new System.Drawing.Size(100, 21);
            this.txtcode.TabIndex = 12;
            this.txtcode.Text = "zy123456";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(162, 23);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(53, 12);
            this.label1.TabIndex = 13;
            this.label1.Text = "样品编码";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(162, 50);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(53, 12);
            this.label2.TabIndex = 14;
            this.label2.Text = "样品类型";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(162, 77);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 16;
            this.label3.Text = "存样柜";
            // 
            // txtmechine
            // 
            this.txtmechine.Location = new System.Drawing.Point(221, 74);
            this.txtmechine.Name = "txtmechine";
            this.txtmechine.Size = new System.Drawing.Size(100, 21);
            this.txtmechine.TabIndex = 15;
            this.txtmechine.Text = "#1智能存样柜";
            // 
            // FrmAutoCupBoard_Test
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(700, 417);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Name = "FrmAutoCupBoard_Test";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Text = "全自动存样柜模拟";
            this.Load += new System.EventHandler(this.FrmBeltSamplerSimulator_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.eventLog1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnStart;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox rtxtOutput;
        private System.Windows.Forms.Button btnSystemStatus_ZZYX;
        private System.Windows.Forms.Button btnSystemStatus_JXDJ;
        private System.Windows.Forms.Button btnSystemStatus_FSGZ;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Button btnReset;
        private System.Diagnostics.EventLog eventLog1;
        private System.Windows.Forms.Button btnInsert;
        private System.Windows.Forms.TextBox txtcode;
        private System.Windows.Forms.TextBox txttype;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtmechine;
    }
}