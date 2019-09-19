namespace CMCS.DataTester.Frms
{
    partial class FrmThinkCamera
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
            this.panel1 = new System.Windows.Forms.Panel();
            this.txtCarNumber = new System.Windows.Forms.TextBox();
            this.btnCapture = new System.Windows.Forms.Button();
            this.btnClose = new System.Windows.Forms.Button();
            this.btnStartPreview = new System.Windows.Forms.Button();
            this.btnStopPreview = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtIp
            // 
            this.txtIp.Location = new System.Drawing.Point(49, 12);
            this.txtIp.Name = "txtIp";
            this.txtIp.Size = new System.Drawing.Size(188, 21);
            this.txtIp.TabIndex = 0;
            this.txtIp.Text = "192.168.1.99";
            // 
            // panel1
            // 
            this.panel1.Location = new System.Drawing.Point(49, 66);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(541, 289);
            this.panel1.TabIndex = 1;
            // 
            // txtCarNumber
            // 
            this.txtCarNumber.Location = new System.Drawing.Point(49, 41);
            this.txtCarNumber.Name = "txtCarNumber";
            this.txtCarNumber.Size = new System.Drawing.Size(107, 21);
            this.txtCarNumber.TabIndex = 3;
            // 
            // btnCapture
            // 
            this.btnCapture.Location = new System.Drawing.Point(458, 10);
            this.btnCapture.Name = "btnCapture";
            this.btnCapture.Size = new System.Drawing.Size(75, 23);
            this.btnCapture.TabIndex = 2;
            this.btnCapture.Text = "抓   拍";
            this.btnCapture.UseVisualStyleBackColor = true;
            this.btnCapture.Click += new System.EventHandler(this.btnCapture_Click);
            // 
            // btnClose
            // 
            this.btnClose.Location = new System.Drawing.Point(458, 39);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(75, 23);
            this.btnClose.TabIndex = 2;
            this.btnClose.Text = "关闭连接";
            this.btnClose.UseVisualStyleBackColor = true;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnStartPreview
            // 
            this.btnStartPreview.Location = new System.Drawing.Point(539, 10);
            this.btnStartPreview.Name = "btnStartPreview";
            this.btnStartPreview.Size = new System.Drawing.Size(75, 23);
            this.btnStartPreview.TabIndex = 2;
            this.btnStartPreview.Text = "开始预览";
            this.btnStartPreview.UseVisualStyleBackColor = true;
            this.btnStartPreview.Click += new System.EventHandler(this.btnStartPreview_Click);
            // 
            // btnStopPreview
            // 
            this.btnStopPreview.Location = new System.Drawing.Point(539, 37);
            this.btnStopPreview.Name = "btnStopPreview";
            this.btnStopPreview.Size = new System.Drawing.Size(75, 23);
            this.btnStopPreview.TabIndex = 2;
            this.btnStopPreview.Text = "停止预览";
            this.btnStopPreview.UseVisualStyleBackColor = true;
            this.btnStopPreview.Click += new System.EventHandler(this.btnStopPreview_Click);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(377, 10);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 2;
            this.button2.Text = "连  接";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Click += new System.EventHandler(this.button1_Click);
            // 
            // FrmThinkCamera
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(667, 367);
            this.Controls.Add(this.txtCarNumber);
            this.Controls.Add(this.btnClose);
            this.Controls.Add(this.btnStopPreview);
            this.Controls.Add(this.btnStartPreview);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.btnCapture);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.txtIp);
            this.Name = "FrmThinkCamera";
            this.Text = "FrmThinkCamera";
            this.Load += new System.EventHandler(this.FrmThinkCamera_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtIp;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txtCarNumber;
        private System.Windows.Forms.Button btnCapture;
        private System.Windows.Forms.Button btnClose;
        private System.Windows.Forms.Button btnStartPreview;
        private System.Windows.Forms.Button btnStopPreview;
        private System.Windows.Forms.Button button2;
    }
}