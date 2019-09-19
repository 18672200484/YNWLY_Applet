namespace CMCS.DataTester.Frms
{
    partial class FrmCodeChange
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
            this.btnCloseCom = new System.Windows.Forms.Button();
            this.btnOpenCom = new System.Windows.Forms.Button();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnCloseCom
            // 
            this.btnCloseCom.Location = new System.Drawing.Point(312, 44);
            this.btnCloseCom.Name = "btnCloseCom";
            this.btnCloseCom.Size = new System.Drawing.Size(104, 23);
            this.btnCloseCom.TabIndex = 42;
            this.btnCloseCom.Text = "16进制转字符串";
            this.btnCloseCom.UseVisualStyleBackColor = true;
            this.btnCloseCom.Click += new System.EventHandler(this.btnColseCom_Click);
            // 
            // btnOpenCom
            // 
            this.btnOpenCom.Location = new System.Drawing.Point(312, 15);
            this.btnOpenCom.Name = "btnOpenCom";
            this.btnOpenCom.Size = new System.Drawing.Size(104, 23);
            this.btnOpenCom.TabIndex = 41;
            this.btnOpenCom.Text = "字符串转16进制";
            this.btnOpenCom.UseVisualStyleBackColor = true;
            this.btnOpenCom.Click += new System.EventHandler(this.btnOpenCom_Click);
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(66, 15);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(228, 63);
            this.textBox1.TabIndex = 43;
            // 
            // textBox2
            // 
            this.textBox2.Location = new System.Drawing.Point(66, 100);
            this.textBox2.Multiline = true;
            this.textBox2.Name = "textBox2";
            this.textBox2.Size = new System.Drawing.Size(228, 63);
            this.textBox2.TabIndex = 43;
            // 
            // FrmCodeChange
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(539, 243);
            this.Controls.Add(this.textBox2);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.btnCloseCom);
            this.Controls.Add(this.btnOpenCom);
            this.Name = "FrmCodeChange";
            this.Text = "进制转换";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.FrmWBYaoHua_FormClosing);
            this.Load += new System.EventHandler(this.FrmWBYaoHua_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnCloseCom;
        private System.Windows.Forms.Button btnOpenCom;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox2;

    }
}