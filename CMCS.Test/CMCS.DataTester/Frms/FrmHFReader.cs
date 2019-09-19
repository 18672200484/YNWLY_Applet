using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//
using ThinkCameraSDK.Core;
using RW.HFReader;

namespace CMCS.DataTester.Frms
{
    public partial class FrmHFReader : Form
    {
        HFReaderRwer hfReader = new HFReaderRwer();
        public FrmHFReader()
        {
            InitializeComponent();
            cbb14443ARWSector.SelectedIndex = 2;
            cbb14443ARWBlockNumber.SelectedIndex = 0;
        }

        private void btnOpenNet_Click(object sender, EventArgs e)
        {
            if (hfReader.OpenNetPort(txtIp.Text, Convert.ToInt32(txtPort.Text)))
                ShowMessage("连接成功");
            else
                ShowMessage("连接失败");
        }

        private void btnOpenRf_Click(object sender, EventArgs e)
        {
            if (hfReader.OpenRF())
                ShowMessage("连接成功");
            else
                ShowMessage("连接失败");
        }

        private void btn14443ARWRead_Click(object sender, EventArgs e)
        {
            if (hfReader.ChangeToISO14443A())
                ShowMessage("ChangeToISO14443A成功");
            else
                ShowMessage("ChangeToISO14443A失败");

            if (hfReader.Request14443A())
                ShowMessage("Request成功");
            else
                ShowMessage("Request失败");

            if (hfReader.Anticoll14443A())
                ShowMessage("Anticoll成功");
            else
                ShowMessage("Anticoll失败");

            if (hfReader.Select14443A())
                ShowMessage("Select成功");
            else
                ShowMessage("Select失败");

            if (hfReader.AuthKey14443A(Convert.ToByte(cbb14443ARWSector.SelectedIndex), Convert.ToByte(cbb14443ARWBlockNumber.SelectedIndex)))
                ShowMessage("AuthKey成功");
            else
                ShowMessage("AuthKey失败");

            if (hfReader.RWRead14443A(Convert.ToInt32(cbb14443ARWSector.SelectedIndex), Convert.ToInt32(cbb14443ARWBlockNumber.SelectedIndex)) != string.Empty)
            {
                txtReadData.Text = hfReader.ReadData;
                txtReadKey.Text = hfReader.ReadKey;
            }
        }

        private void btnCloseNet_Click(object sender, EventArgs e)
        {
            if (hfReader.CloseNetPort())
                ShowMessage("关闭成功");
            else
                ShowMessage("关闭失败");
        }

        private void benCloseRf_Click(object sender, EventArgs e)
        {
            if (hfReader.CloseRF())
                ShowMessage("关闭成功");
            else
                ShowMessage("关闭失败");
        }

        private void btn14443ARWWrite_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtReadData.Text.Trim()) || txtReadData.Text.Trim().Length != 16)
            {
                MessageBox.Show("请输入正确的数据");
                return;
            }
            if (hfReader.ChangeToISO14443A())
                ShowMessage("ChangeToISO14443A成功");
            else
                ShowMessage("ChangeToISO14443A失败");

            if (hfReader.Request14443A())
                ShowMessage("Request成功");
            else
                ShowMessage("Request失败");

            if (hfReader.Anticoll14443A())
                ShowMessage("Anticoll成功");
            else
                ShowMessage("Anticoll失败");

            if (hfReader.Select14443A())
                ShowMessage("Select成功");
            else
                ShowMessage("Select失败");

            if (hfReader.AuthKey14443A(Convert.ToByte(cbb14443ARWSector.SelectedIndex), Convert.ToByte(cbb14443ARWBlockNumber.SelectedIndex)))
                ShowMessage("AuthKey成功");
            else
                ShowMessage("AuthKey失败");

            if (hfReader.Write14443(txtReadData.Text.Trim(), cbb14443ARWSector.SelectedIndex, cbb14443ARWBlockNumber.SelectedIndex))
                ShowMessage("写卡成功");
            else
                ShowMessage("写卡失败");
        }

        #region 其他

        private void ShowMessage(string info, eOutputType outputType = eOutputType.Normal)
        {
            OutputRunInfo(rtxtMakeWeightInfo, info, outputType);
        }

        /// <summary>
        /// 输出运行信息
        /// </summary>
        /// <param name="richTextBox"></param>
        /// <param name="text"></param>
        /// <param name="outputType"></param>
        private void OutputRunInfo(TextBox richTextBox, string text, eOutputType outputType = eOutputType.Normal)
        {
            this.Invoke((EventHandler)(delegate
            {
                if (richTextBox.TextLength > 100000) richTextBox.Clear();

                text = string.Format("{0}  {1}", DateTime.Now.ToString("HH:mm:ss"), text);

                richTextBox.SelectionStart = richTextBox.TextLength;

                switch (outputType)
                {
                    case eOutputType.Normal:
                        richTextBox.ForeColor = ColorTranslator.FromHtml("#BD86FA");
                        break;
                    case eOutputType.Important:
                        richTextBox.ForeColor = ColorTranslator.FromHtml("#A50081");
                        break;
                    case eOutputType.Warn:
                        richTextBox.ForeColor = ColorTranslator.FromHtml("#F9C916");
                        break;
                    case eOutputType.Error:
                        richTextBox.ForeColor = ColorTranslator.FromHtml("#DB2606");
                        break;
                    default:
                        richTextBox.ForeColor = Color.White;
                        break;
                }

                richTextBox.AppendText(string.Format("{0}\r", text));

                richTextBox.ScrollToCaret();

            }));
        }

        /// <summary>
        /// 输出信息类型
        /// </summary>
        public enum eOutputType
        {
            /// <summary>
            /// 普通
            /// </summary>
            [Description("#BD86FA")]
            Normal,
            /// <summary>
            /// 重要
            /// </summary>
            [Description("#A50081")]
            Important,
            /// <summary>
            /// 警告
            /// </summary>
            [Description("#F9C916")]
            Warn,
            /// <summary>
            /// 错误
            /// </summary>
            [Description("#DB2606")]
            Error
        }

        /// <summary>
        /// Invoke封装
        /// </summary>
        /// <param name="action"></param>
        public void InvokeEx(Action action)
        {
            if (this.IsDisposed || !this.IsHandleCreated) return;

            this.Invoke(action);
        }

        #endregion
    }
}
