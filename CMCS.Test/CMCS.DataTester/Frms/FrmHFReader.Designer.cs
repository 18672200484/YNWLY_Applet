namespace CMCS.DataTester.Frms
{
    partial class FrmHFReader
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
            this.txtIp = new System.Windows.Forms.TextBox();
            this.txtPort = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtReadData = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtReadKey = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cbb14443ARWBlockNumber = new System.Windows.Forms.ComboBox();
            this.cbb14443ARWSector = new System.Windows.Forms.ComboBox();
            this.label27 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
            this.btn14443ARWWrite = new System.Windows.Forms.Button();
            this.btn14443ARWRead = new System.Windows.Forms.Button();
            this.btnOpenNet = new System.Windows.Forms.Button();
            this.btnOpenRf = new System.Windows.Forms.Button();
            this.btnCloseNet = new System.Windows.Forms.Button();
            this.benCloseRf = new System.Windows.Forms.Button();
            this.rtxtMakeWeightInfo = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtIp
            // 
            this.txtIp.Location = new System.Drawing.Point(99, 12);
            this.txtIp.Name = "txtIp";
            this.txtIp.Size = new System.Drawing.Size(107, 21);
            this.txtIp.TabIndex = 0;
            this.txtIp.Text = "10.90.20.45";
            // 
            // txtPort
            // 
            this.txtPort.Location = new System.Drawing.Point(99, 41);
            this.txtPort.Name = "txtPort";
            this.txtPort.Size = new System.Drawing.Size(107, 21);
            this.txtPort.TabIndex = 3;
            this.txtPort.Text = "6000";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(64, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(29, 12);
            this.label1.TabIndex = 4;
            this.label1.Text = "IP：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(54, 44);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "端口：";
            // 
            // txtReadData
            // 
            this.txtReadData.Location = new System.Drawing.Point(99, 68);
            this.txtReadData.Name = "txtReadData";
            this.txtReadData.Size = new System.Drawing.Size(259, 21);
            this.txtReadData.TabIndex = 3;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(43, 71);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(53, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "数据块：";
            // 
            // txtReadKey
            // 
            this.txtReadKey.Location = new System.Drawing.Point(99, 95);
            this.txtReadKey.Name = "txtReadKey";
            this.txtReadKey.Size = new System.Drawing.Size(259, 21);
            this.txtReadKey.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(43, 98);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(53, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "密钥块：";
            // 
            // cbb14443ARWBlockNumber
            // 
            this.cbb14443ARWBlockNumber.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbb14443ARWBlockNumber.FormattingEnabled = true;
            this.cbb14443ARWBlockNumber.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15"});
            this.cbb14443ARWBlockNumber.Location = new System.Drawing.Point(255, 40);
            this.cbb14443ARWBlockNumber.Name = "cbb14443ARWBlockNumber";
            this.cbb14443ARWBlockNumber.Size = new System.Drawing.Size(98, 20);
            this.cbb14443ARWBlockNumber.TabIndex = 12;
            // 
            // cbb14443ARWSector
            // 
            this.cbb14443ARWSector.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cbb14443ARWSector.FormattingEnabled = true;
            this.cbb14443ARWSector.Items.AddRange(new object[] {
            "0",
            "1",
            "2",
            "3",
            "4",
            "5",
            "6",
            "7",
            "8",
            "9",
            "10",
            "11",
            "12",
            "13",
            "14",
            "15",
            "16",
            "17",
            "18",
            "19",
            "20",
            "21",
            "22",
            "23",
            "24",
            "25",
            "26",
            "27",
            "28",
            "29",
            "30",
            "31",
            "32",
            "33",
            "34",
            "35",
            "36",
            "37",
            "38",
            "39"});
            this.cbb14443ARWSector.Location = new System.Drawing.Point(255, 10);
            this.cbb14443ARWSector.Name = "cbb14443ARWSector";
            this.cbb14443ARWSector.Size = new System.Drawing.Size(98, 20);
            this.cbb14443ARWSector.TabIndex = 11;
            // 
            // label27
            // 
            this.label27.AutoSize = true;
            this.label27.Location = new System.Drawing.Point(214, 44);
            this.label27.Name = "label27";
            this.label27.Size = new System.Drawing.Size(35, 12);
            this.label27.TabIndex = 10;
            this.label27.Text = "块号:";
            // 
            // label26
            // 
            this.label26.AutoSize = true;
            this.label26.Location = new System.Drawing.Point(210, 15);
            this.label26.Name = "label26";
            this.label26.Size = new System.Drawing.Size(47, 12);
            this.label26.TabIndex = 9;
            this.label26.Text = "扇区号:";
            // 
            // btn14443ARWWrite
            // 
            this.btn14443ARWWrite.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btn14443ARWWrite.Location = new System.Drawing.Point(578, 35);
            this.btn14443ARWWrite.Name = "btn14443ARWWrite";
            this.btn14443ARWWrite.Size = new System.Drawing.Size(77, 23);
            this.btn14443ARWWrite.TabIndex = 8;
            this.btn14443ARWWrite.Text = "写";
            this.btn14443ARWWrite.UseVisualStyleBackColor = true;
            this.btn14443ARWWrite.Click += new System.EventHandler(this.btn14443ARWWrite_Click);
            // 
            // btn14443ARWRead
            // 
            this.btn14443ARWRead.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btn14443ARWRead.Location = new System.Drawing.Point(578, 8);
            this.btn14443ARWRead.Name = "btn14443ARWRead";
            this.btn14443ARWRead.Size = new System.Drawing.Size(77, 23);
            this.btn14443ARWRead.TabIndex = 7;
            this.btn14443ARWRead.Text = "读";
            this.btn14443ARWRead.UseVisualStyleBackColor = true;
            this.btn14443ARWRead.Click += new System.EventHandler(this.btn14443ARWRead_Click);
            // 
            // btnOpenNet
            // 
            this.btnOpenNet.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnOpenNet.Location = new System.Drawing.Point(380, 10);
            this.btnOpenNet.Name = "btnOpenNet";
            this.btnOpenNet.Size = new System.Drawing.Size(77, 23);
            this.btnOpenNet.TabIndex = 7;
            this.btnOpenNet.Text = "打开网口";
            this.btnOpenNet.UseVisualStyleBackColor = true;
            this.btnOpenNet.Click += new System.EventHandler(this.btnOpenNet_Click);
            // 
            // btnOpenRf
            // 
            this.btnOpenRf.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnOpenRf.Location = new System.Drawing.Point(380, 38);
            this.btnOpenRf.Name = "btnOpenRf";
            this.btnOpenRf.Size = new System.Drawing.Size(77, 23);
            this.btnOpenRf.TabIndex = 7;
            this.btnOpenRf.Text = "打开读卡器";
            this.btnOpenRf.UseVisualStyleBackColor = true;
            this.btnOpenRf.Click += new System.EventHandler(this.btnOpenRf_Click);
            // 
            // btnCloseNet
            // 
            this.btnCloseNet.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCloseNet.Location = new System.Drawing.Point(480, 10);
            this.btnCloseNet.Name = "btnCloseNet";
            this.btnCloseNet.Size = new System.Drawing.Size(77, 23);
            this.btnCloseNet.TabIndex = 7;
            this.btnCloseNet.Text = "关闭网口";
            this.btnCloseNet.UseVisualStyleBackColor = true;
            this.btnCloseNet.Click += new System.EventHandler(this.btnCloseNet_Click);
            // 
            // benCloseRf
            // 
            this.benCloseRf.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.benCloseRf.Location = new System.Drawing.Point(480, 37);
            this.benCloseRf.Name = "benCloseRf";
            this.benCloseRf.Size = new System.Drawing.Size(77, 23);
            this.benCloseRf.TabIndex = 7;
            this.benCloseRf.Text = "关闭读卡器";
            this.benCloseRf.UseVisualStyleBackColor = true;
            this.benCloseRf.Click += new System.EventHandler(this.benCloseRf_Click);
            // 
            // rtxtMakeWeightInfo
            // 
            this.rtxtMakeWeightInfo.Location = new System.Drawing.Point(56, 153);
            this.rtxtMakeWeightInfo.Multiline = true;
            this.rtxtMakeWeightInfo.Name = "rtxtMakeWeightInfo";
            this.rtxtMakeWeightInfo.Size = new System.Drawing.Size(582, 102);
            this.rtxtMakeWeightInfo.TabIndex = 13;
            // 
            // FrmHFReader
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(667, 367);
            this.Controls.Add(this.rtxtMakeWeightInfo);
            this.Controls.Add(this.cbb14443ARWBlockNumber);
            this.Controls.Add(this.cbb14443ARWSector);
            this.Controls.Add(this.label27);
            this.Controls.Add(this.label26);
            this.Controls.Add(this.btn14443ARWWrite);
            this.Controls.Add(this.benCloseRf);
            this.Controls.Add(this.btnOpenRf);
            this.Controls.Add(this.btnCloseNet);
            this.Controls.Add(this.btnOpenNet);
            this.Controls.Add(this.btn14443ARWRead);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtReadKey);
            this.Controls.Add(this.txtReadData);
            this.Controls.Add(this.txtPort);
            this.Controls.Add(this.txtIp);
            this.Name = "FrmHFReader";
            this.Text = "FrmThinkCamera";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtIp;
        private System.Windows.Forms.TextBox txtPort;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtReadData;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtReadKey;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cbb14443ARWBlockNumber;
        private System.Windows.Forms.ComboBox cbb14443ARWSector;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.Button btn14443ARWWrite;
        private System.Windows.Forms.Button btn14443ARWRead;
        private System.Windows.Forms.Button btnOpenNet;
        private System.Windows.Forms.Button btnOpenRf;
        private System.Windows.Forms.Button btnCloseNet;
        private System.Windows.Forms.Button benCloseRf;
        private System.Windows.Forms.TextBox rtxtMakeWeightInfo;
    }
}