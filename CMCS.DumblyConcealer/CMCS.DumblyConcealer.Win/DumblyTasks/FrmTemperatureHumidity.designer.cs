namespace CMCS.DumblyConcealer.Win.DumblyTasks
{
    partial class FrmTemperatureHumidity
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

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
			this.rtxtOutput = new System.Windows.Forms.RichTextBox();
			this.SuspendLayout();
			// 
			// rtxtOutput
			// 
			this.rtxtOutput.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(64)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
			this.rtxtOutput.BorderStyle = System.Windows.Forms.BorderStyle.None;
			this.rtxtOutput.Dock = System.Windows.Forms.DockStyle.Fill;
			this.rtxtOutput.ForeColor = System.Drawing.Color.White;
			this.rtxtOutput.Location = new System.Drawing.Point(0, 0);
			this.rtxtOutput.Name = "rtxtOutput";
			this.rtxtOutput.Size = new System.Drawing.Size(620, 323);
			this.rtxtOutput.TabIndex = 3;
			this.rtxtOutput.Text = "";
			// 
			// FrmTemperatureHumidity
			// 
			this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
			this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
			this.ClientSize = new System.Drawing.Size(620, 323);
			this.Controls.Add(this.rtxtOutput);
			this.Name = "FrmTemperatureHumidity";
			this.Text = "TemperatureHumidity";
			this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.FrmAutoCupboard_NCGM_FormClosed);
			this.Load += new System.EventHandler(this.TemperatureHumidity_Load);
			this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox rtxtOutput;
    }
}
