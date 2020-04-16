namespace CMCS.DataTester.Frms
{
    partial class FrmFourTest
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
			this.txt_InPut = new System.Windows.Forms.TextBox();
			this.button1 = new System.Windows.Forms.Button();
			this.txt_Value1 = new System.Windows.Forms.TextBox();
			this.txt_Value2 = new System.Windows.Forms.TextBox();
			this.txt_Value3 = new System.Windows.Forms.TextBox();
			this.groupBox1 = new System.Windows.Forms.GroupBox();
			this.label5 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.label3 = new System.Windows.Forms.Label();
			this.groupBox2 = new System.Windows.Forms.GroupBox();
			this.label2 = new System.Windows.Forms.Label();
			this.label1 = new System.Windows.Forms.Label();
			this.txt_Dts = new System.Windows.Forms.TextBox();
			this.label6 = new System.Windows.Forms.Label();
			this.txt_Value4 = new System.Windows.Forms.TextBox();
			this.groupBox1.SuspendLayout();
			this.groupBox2.SuspendLayout();
			this.SuspendLayout();
			// 
			// txt_InPut
			// 
			this.txt_InPut.Location = new System.Drawing.Point(74, 20);
			this.txt_InPut.Name = "txt_InPut";
			this.txt_InPut.Size = new System.Drawing.Size(120, 21);
			this.txt_InPut.TabIndex = 0;
			// 
			// button1
			// 
			this.button1.Location = new System.Drawing.Point(300, 20);
			this.button1.Name = "button1";
			this.button1.Size = new System.Drawing.Size(75, 23);
			this.button1.TabIndex = 1;
			this.button1.Text = "计算";
			this.button1.UseVisualStyleBackColor = true;
			this.button1.Click += new System.EventHandler(this.button1_Click);
			// 
			// txt_Value1
			// 
			this.txt_Value1.Location = new System.Drawing.Point(74, 20);
			this.txt_Value1.Name = "txt_Value1";
			this.txt_Value1.Size = new System.Drawing.Size(126, 21);
			this.txt_Value1.TabIndex = 0;
			// 
			// txt_Value2
			// 
			this.txt_Value2.Location = new System.Drawing.Point(74, 47);
			this.txt_Value2.Name = "txt_Value2";
			this.txt_Value2.Size = new System.Drawing.Size(126, 21);
			this.txt_Value2.TabIndex = 0;
			// 
			// txt_Value3
			// 
			this.txt_Value3.Location = new System.Drawing.Point(74, 74);
			this.txt_Value3.Name = "txt_Value3";
			this.txt_Value3.Size = new System.Drawing.Size(126, 21);
			this.txt_Value3.TabIndex = 0;
			// 
			// groupBox1
			// 
			this.groupBox1.Controls.Add(this.label6);
			this.groupBox1.Controls.Add(this.txt_Value4);
			this.groupBox1.Controls.Add(this.label5);
			this.groupBox1.Controls.Add(this.label4);
			this.groupBox1.Controls.Add(this.label3);
			this.groupBox1.Controls.Add(this.txt_Value1);
			this.groupBox1.Controls.Add(this.txt_Value2);
			this.groupBox1.Controls.Add(this.txt_Value3);
			this.groupBox1.Location = new System.Drawing.Point(67, 92);
			this.groupBox1.Name = "groupBox1";
			this.groupBox1.Size = new System.Drawing.Size(218, 226);
			this.groupBox1.TabIndex = 2;
			this.groupBox1.TabStop = false;
			this.groupBox1.Text = "结果";
			// 
			// label5
			// 
			this.label5.AutoSize = true;
			this.label5.Location = new System.Drawing.Point(33, 77);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(35, 12);
			this.label5.TabIndex = 7;
			this.label5.Text = "结果3";
			// 
			// label4
			// 
			this.label4.AutoSize = true;
			this.label4.Location = new System.Drawing.Point(33, 50);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(35, 12);
			this.label4.TabIndex = 6;
			this.label4.Text = "结果2";
			// 
			// label3
			// 
			this.label3.AutoSize = true;
			this.label3.Location = new System.Drawing.Point(33, 23);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(35, 12);
			this.label3.TabIndex = 5;
			this.label3.Text = "结果1";
			// 
			// groupBox2
			// 
			this.groupBox2.Controls.Add(this.label2);
			this.groupBox2.Controls.Add(this.label1);
			this.groupBox2.Controls.Add(this.txt_Dts);
			this.groupBox2.Controls.Add(this.txt_InPut);
			this.groupBox2.Location = new System.Drawing.Point(67, 7);
			this.groupBox2.Name = "groupBox2";
			this.groupBox2.Size = new System.Drawing.Size(218, 74);
			this.groupBox2.TabIndex = 3;
			this.groupBox2.TabStop = false;
			this.groupBox2.Text = "待计算值";
			// 
			// label2
			// 
			this.label2.AutoSize = true;
			this.label2.Location = new System.Drawing.Point(7, 51);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(65, 12);
			this.label2.TabIndex = 4;
			this.label2.Text = "保留小数位";
			// 
			// label1
			// 
			this.label1.AutoSize = true;
			this.label1.Location = new System.Drawing.Point(7, 24);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(65, 12);
			this.label1.TabIndex = 4;
			this.label1.Text = "待计算数据";
			// 
			// txt_Dts
			// 
			this.txt_Dts.Location = new System.Drawing.Point(74, 47);
			this.txt_Dts.Name = "txt_Dts";
			this.txt_Dts.Size = new System.Drawing.Size(120, 21);
			this.txt_Dts.TabIndex = 0;
			// 
			// label6
			// 
			this.label6.AutoSize = true;
			this.label6.Location = new System.Drawing.Point(33, 104);
			this.label6.Name = "label6";
			this.label6.Size = new System.Drawing.Size(35, 12);
			this.label6.TabIndex = 9;
			this.label6.Text = "结果4";
			// 
			// txt_Value4
			// 
			this.txt_Value4.Location = new System.Drawing.Point(74, 101);
			this.txt_Value4.Name = "txt_Value4";
			this.txt_Value4.Size = new System.Drawing.Size(126, 21);
			this.txt_Value4.TabIndex = 8;
			// 
			// FrmFourTest
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(527, 382);
			this.Controls.Add(this.groupBox2);
			this.Controls.Add(this.groupBox1);
			this.Controls.Add(this.button1);
			this.Name = "FrmFourTest";
			this.Text = "测试";
			this.Load += new System.EventHandler(this.FrmTest_Load);
			this.groupBox1.ResumeLayout(false);
			this.groupBox1.PerformLayout();
			this.groupBox2.ResumeLayout(false);
			this.groupBox2.PerformLayout();
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txt_InPut;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.TextBox txt_Value1;
        private System.Windows.Forms.TextBox txt_Value2;
        private System.Windows.Forms.TextBox txt_Value3;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txt_Dts;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.TextBox txt_Value4;
	}
}