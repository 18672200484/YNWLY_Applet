namespace CMCS.DataTester.Frms
{
    partial class FrmBeltSamplerCmdTest
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
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.btn_Db = new System.Windows.Forms.Button();
            this.btnUnLoad = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.cmbSampleType = new System.Windows.Forms.ComboBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cmbBeltSampler = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txt_DB = new System.Windows.Forms.TextBox();
            this.txt_SampleCode = new System.Windows.Forms.TextBox();
            this.btnSendPlan = new System.Windows.Forms.Button();
            this.btnStartCmd = new System.Windows.Forms.Button();
            this.btnStopCmd = new System.Windows.Forms.Button();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.rtxtOutput = new System.Windows.Forms.RichTextBox();
            this.groupBox2.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.btn_Db);
            this.groupBox2.Controls.Add(this.btnUnLoad);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.cmbSampleType);
            this.groupBox2.Controls.Add(this.label3);
            this.groupBox2.Controls.Add(this.cmbBeltSampler);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.label1);
            this.groupBox2.Controls.Add(this.txt_DB);
            this.groupBox2.Controls.Add(this.txt_SampleCode);
            this.groupBox2.Controls.Add(this.btnSendPlan);
            this.groupBox2.Controls.Add(this.btnStartCmd);
            this.groupBox2.Controls.Add(this.btnStopCmd);
            this.groupBox2.Dock = System.Windows.Forms.DockStyle.Top;
            this.groupBox2.Location = new System.Drawing.Point(5, 5);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(781, 145);
            this.groupBox2.TabIndex = 6;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = " 操作 ";
            // 
            // btn_Db
            // 
            this.btn_Db.Location = new System.Drawing.Point(697, 18);
            this.btn_Db.Name = "btn_Db";
            this.btn_Db.Size = new System.Drawing.Size(78, 23);
            this.btn_Db.TabIndex = 17;
            this.btn_Db.Text = "确 定";
            this.btn_Db.UseVisualStyleBackColor = true;
            this.btn_Db.Click += new System.EventHandler(this.btn_Db_Click);
            // 
            // btnUnLoad
            // 
            this.btnUnLoad.Location = new System.Drawing.Point(342, 102);
            this.btnUnLoad.Name = "btnUnLoad";
            this.btnUnLoad.Size = new System.Drawing.Size(78, 23);
            this.btnUnLoad.TabIndex = 16;
            this.btnUnLoad.Text = "开始卸样";
            this.btnUnLoad.UseVisualStyleBackColor = true;
            this.btnUnLoad.Click += new System.EventHandler(this.btnUnLoad_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(406, 63);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 15;
            this.label4.Text = "采样方式";
            // 
            // cmbSampleType
            // 
            this.cmbSampleType.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbSampleType.FormattingEnabled = true;
            this.cmbSampleType.Items.AddRange(new object[] {
            "密码罐",
            "底卸式",
            "混合"});
            this.cmbSampleType.Location = new System.Drawing.Point(465, 59);
            this.cmbSampleType.Name = "cmbSampleType";
            this.cmbSampleType.Size = new System.Drawing.Size(126, 20);
            this.cmbSampleType.TabIndex = 14;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(52, 63);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(41, 12);
            this.label3.TabIndex = 13;
            this.label3.Text = "采样机";
            // 
            // cmbBeltSampler
            // 
            this.cmbBeltSampler.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbBeltSampler.FormattingEnabled = true;
            this.cmbBeltSampler.Items.AddRange(new object[] {
            "1",
            "2"});
            this.cmbBeltSampler.Location = new System.Drawing.Point(97, 58);
            this.cmbBeltSampler.Name = "cmbBeltSampler";
            this.cmbBeltSampler.Size = new System.Drawing.Size(126, 20);
            this.cmbBeltSampler.TabIndex = 12;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(65, 12);
            this.label2.TabIndex = 11;
            this.label2.Text = "数据库连接";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(229, 63);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(35, 12);
            this.label1.TabIndex = 11;
            this.label1.Text = "编 码";
            // 
            // txt_DB
            // 
            this.txt_DB.Location = new System.Drawing.Point(97, 18);
            this.txt_DB.Name = "txt_DB";
            this.txt_DB.Size = new System.Drawing.Size(594, 21);
            this.txt_DB.TabIndex = 10;
            this.txt_DB.Text = "Password=123456;Persist Security Info=True;User ID=sa;Initial Catalog=BS3HCPDCYJ;" +
    "Data Source=10.90.20.239";
            // 
            // txt_SampleCode
            // 
            this.txt_SampleCode.Location = new System.Drawing.Point(276, 58);
            this.txt_SampleCode.Name = "txt_SampleCode";
            this.txt_SampleCode.Size = new System.Drawing.Size(120, 21);
            this.txt_SampleCode.TabIndex = 10;
            // 
            // btnSendPlan
            // 
            this.btnSendPlan.Location = new System.Drawing.Point(96, 102);
            this.btnSendPlan.Name = "btnSendPlan";
            this.btnSendPlan.Size = new System.Drawing.Size(78, 23);
            this.btnSendPlan.TabIndex = 4;
            this.btnSendPlan.Text = "发送计划";
            this.btnSendPlan.UseVisualStyleBackColor = true;
            this.btnSendPlan.Click += new System.EventHandler(this.btnSendPlan_Click);
            // 
            // btnStartCmd
            // 
            this.btnStartCmd.Location = new System.Drawing.Point(177, 102);
            this.btnStartCmd.Name = "btnStartCmd";
            this.btnStartCmd.Size = new System.Drawing.Size(78, 23);
            this.btnStartCmd.TabIndex = 5;
            this.btnStartCmd.Text = "开始采样";
            this.btnStartCmd.UseVisualStyleBackColor = true;
            this.btnStartCmd.Click += new System.EventHandler(this.btnStartCmd_Click);
            // 
            // btnStopCmd
            // 
            this.btnStopCmd.Location = new System.Drawing.Point(258, 102);
            this.btnStopCmd.Name = "btnStopCmd";
            this.btnStopCmd.Size = new System.Drawing.Size(78, 23);
            this.btnStopCmd.TabIndex = 6;
            this.btnStopCmd.Text = "停止采样";
            this.btnStopCmd.UseVisualStyleBackColor = true;
            this.btnStopCmd.Click += new System.EventHandler(this.btnStopCmd_Click);
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.rtxtOutput);
            this.groupBox1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.groupBox1.Location = new System.Drawing.Point(5, 150);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(781, 262);
            this.groupBox1.TabIndex = 7;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = " 输出 ";
            // 
            // rtxtOutput
            // 
            this.rtxtOutput.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rtxtOutput.Location = new System.Drawing.Point(3, 17);
            this.rtxtOutput.Name = "rtxtOutput";
            this.rtxtOutput.Size = new System.Drawing.Size(775, 242);
            this.rtxtOutput.TabIndex = 2;
            this.rtxtOutput.Text = "";
            // 
            // FrmBeltSamplerCmdTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(791, 417);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.groupBox2);
            this.Name = "FrmBeltSamplerCmdTest";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.Text = "皮带采样机模拟";
            this.Load += new System.EventHandler(this.FrmBeltSamplerSimulator_Load);
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.RichTextBox rtxtOutput;
        private System.Windows.Forms.Button btnStopCmd;
        private System.Windows.Forms.Button btnStartCmd;
        private System.Windows.Forms.Button btnSendPlan;
        private System.Windows.Forms.TextBox txt_SampleCode;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_DB;
        private System.Windows.Forms.ComboBox cmbBeltSampler;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cmbSampleType;
        private System.Windows.Forms.Button btnUnLoad;
        private System.Windows.Forms.Button btn_Db;
    }
}