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
using OPCAutomation;
using System.Threading.Tasks;
using ModBus.Tcp;

namespace CMCS.DataTester.Frms
{
    public partial class FrmModBusTest : Form
    {
        ModBusTcp_Net modBus = new ModBusTcp_Net();

        public FrmModBusTest()
        {
            InitializeComponent();
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

                text = string.Format(Environment.NewLine + "{0}  {1}", DateTime.Now.ToString("HH:mm:ss"), text);

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

        private void btnOpenNet_Click(object sender, EventArgs e)
        {
            if (modBus.Connect(txtIp.Text))
                ShowMessage("连接成功");
            else
                ShowMessage("连接失败");
        }

        private void btnCloseNet_Click(object sender, EventArgs e)
        {
            if (modBus.CloseConnect())
                ShowMessage("关闭成功");
            else
                ShowMessage("关闭失败");
        }

        private void btn14443ARWRead_Click(object sender, EventArgs e)
        {
            int result = modBus.ReadCoil(txtAddress.Text);
            ShowMessage(string.Format("[{0}]:{1}", txtAddress.Text, result));
        }

        private void btn14443ARWWrite_Click(object sender, EventArgs e)
        {
            int result = modBus.ReadDisCrete(txtAddress.Text);
            ShowMessage(string.Format("[{0}]:{1}", txtAddress.Text, result));
        }
    }
}
