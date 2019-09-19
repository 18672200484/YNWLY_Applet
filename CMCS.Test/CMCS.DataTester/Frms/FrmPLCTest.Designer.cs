namespace CMCS.DataTester.Frms
{
    partial class FrmPLCTest
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
            this.label1 = new System.Windows.Forms.Label();
            this.btn14443ARWWrite = new System.Windows.Forms.Button();
            this.btn14443ARWRead = new System.Windows.Forms.Button();
            this.btnOpenNet = new System.Windows.Forms.Button();
            this.btnCloseNet = new System.Windows.Forms.Button();
            this.rtxtMakeWeightInfo = new System.Windows.Forms.TextBox();
            this.txtAddress = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtIp
            // 
            this.txtIp.Location = new System.Drawing.Point(99, 12);
            this.txtIp.Name = "txtIp";
            this.txtIp.Size = new System.Drawing.Size(107, 21);
            this.txtIp.TabIndex = 0;
            this.txtIp.Text = "127.0.0.1";
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
            // btn14443ARWWrite
            // 
            this.btn14443ARWWrite.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btn14443ARWWrite.Location = new System.Drawing.Point(369, 54);
            this.btn14443ARWWrite.Name = "btn14443ARWWrite";
            this.btn14443ARWWrite.Size = new System.Drawing.Size(77, 23);
            this.btn14443ARWWrite.TabIndex = 8;
            this.btn14443ARWWrite.Text = "读离散地址";
            this.btn14443ARWWrite.UseVisualStyleBackColor = true;
            // 
            // btn14443ARWRead
            // 
            this.btn14443ARWRead.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btn14443ARWRead.Location = new System.Drawing.Point(268, 54);
            this.btn14443ARWRead.Name = "btn14443ARWRead";
            this.btn14443ARWRead.Size = new System.Drawing.Size(77, 23);
            this.btn14443ARWRead.TabIndex = 7;
            this.btn14443ARWRead.Text = "读线圈";
            this.btn14443ARWRead.UseVisualStyleBackColor = true;
            // 
            // btnOpenNet
            // 
            this.btnOpenNet.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnOpenNet.Location = new System.Drawing.Point(66, 54);
            this.btnOpenNet.Name = "btnOpenNet";
            this.btnOpenNet.Size = new System.Drawing.Size(77, 23);
            this.btnOpenNet.TabIndex = 7;
            this.btnOpenNet.Text = "打开连接";
            this.btnOpenNet.UseVisualStyleBackColor = true;
            this.btnOpenNet.Click += new System.EventHandler(this.btnOpenNet_Click);
            // 
            // btnCloseNet
            // 
            this.btnCloseNet.ImeMode = System.Windows.Forms.ImeMode.NoControl;
            this.btnCloseNet.Location = new System.Drawing.Point(166, 54);
            this.btnCloseNet.Name = "btnCloseNet";
            this.btnCloseNet.Size = new System.Drawing.Size(77, 23);
            this.btnCloseNet.TabIndex = 7;
            this.btnCloseNet.Text = "关闭网口";
            this.btnCloseNet.UseVisualStyleBackColor = true;
            // 
            // rtxtMakeWeightInfo
            // 
            this.rtxtMakeWeightInfo.Location = new System.Drawing.Point(56, 153);
            this.rtxtMakeWeightInfo.Multiline = true;
            this.rtxtMakeWeightInfo.Name = "rtxtMakeWeightInfo";
            this.rtxtMakeWeightInfo.Size = new System.Drawing.Size(582, 102);
            this.rtxtMakeWeightInfo.TabIndex = 13;
            // 
            // txtAddress
            // 
            this.txtAddress.Location = new System.Drawing.Point(263, 12);
            this.txtAddress.Name = "txtAddress";
            this.txtAddress.Size = new System.Drawing.Size(107, 21);
            this.txtAddress.TabIndex = 0;
            this.txtAddress.Text = "8";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(216, 15);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(41, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "地址：";
            // 
            // FrmPLCTest
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(667, 367);
            this.Controls.Add(this.rtxtMakeWeightInfo);
            this.Controls.Add(this.btn14443ARWWrite);
            this.Controls.Add(this.btnCloseNet);
            this.Controls.Add(this.btnOpenNet);
            this.Controls.Add(this.btn14443ARWRead);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtAddress);
            this.Controls.Add(this.txtIp);
            this.Name = "FrmPLCTest";
            this.Text = "FrmThinkCamera";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtIp;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btn14443ARWWrite;
        private System.Windows.Forms.Button btn14443ARWRead;
        private System.Windows.Forms.Button btnOpenNet;
        private System.Windows.Forms.Button btnCloseNet;
        private System.Windows.Forms.TextBox rtxtMakeWeightInfo;
        private System.Windows.Forms.TextBox txtAddress;
        private System.Windows.Forms.Label label2;
    }
}