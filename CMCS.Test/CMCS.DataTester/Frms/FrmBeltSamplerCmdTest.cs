using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CMCS.DumblyConcealer.Tasks.BeltSampler.Entities;
using CMCS.DumblyConcealer;
using CMCS.DapperDber.Dbs.SqlServerDb;
using CMCS.DapperDber.Util;
using CMCS.Common.Utilities;
using System.Threading;
using System.Threading.Tasks;
using CMCS.Common.Enums;
using CMCS.Common;
using CMCS.Common.DAO;
using CMCS.Common.Entities.BaseInfo;

namespace CMCS.DataTester.Frms
{
    public partial class FrmBeltSamplerCmdTest : Form
    {
        SqlServerDapperDber dber = null;

        public FrmBeltSamplerCmdTest()
        {
            InitializeComponent();
        }

        private void FrmBeltSamplerSimulator_Load(object sender, EventArgs e)
        {

        }

        #region Util

        /// <summary>
        /// 输出信息类型
        /// </summary>
        public enum eOutputType
        {
            /// <summary>
            /// 普通
            /// </summary>
            [Description("#FFFFFF")]
            Normal,
            /// <summary>
            /// 错误
            /// </summary>
            [Description("#DB2606")]
            Error
        }

        /// <summary>
        /// 输出运行信息
        /// </summary>
        /// <param name="richTextBox"></param>
        /// <param name="text"></param>
        /// <param name="outputType"></param>
        private void OutputRunInfo(RichTextBox richTextBox, string text, eOutputType outputType = eOutputType.Normal)
        {
            this.InvokeEx(() =>
            {
                if (richTextBox.TextLength > 100000) richTextBox.Clear();

                text = string.Format(" # {0} - {1}", DateTime.Now.ToString("HH:mm:ss"), text);

                richTextBox.SelectionStart = richTextBox.TextLength;

                switch (outputType)
                {
                    case eOutputType.Normal:
                        richTextBox.SelectionColor = Color.Black;
                        break;
                    case eOutputType.Error:
                        richTextBox.SelectionColor = ColorTranslator.FromHtml("#DB2606");
                        break;
                    default:
                        richTextBox.SelectionColor = Color.White;
                        break;
                }

                richTextBox.AppendText(string.Format("{0}\r", text));

                richTextBox.ScrollToCaret();
            });
        }

        /// <summary>
        /// 输出异常信息
        /// </summary>
        /// <param name="text"></param>
        /// <param name="ex"></param>
        private void OutputErrorInfo(string text, Exception ex)
        {
            this.InvokeEx(() =>
            {
                text = string.Format("# {0} - {1}\r\n{2}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), text, ex.Message);

                OutputRunInfo(rtxtOutput, text + "", eOutputType.Error);
            });
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

        private void btn_Db_Click(object sender, EventArgs e)
        {
            try
            {
                this.dber = new SqlServerDapperDber(this.txt_DB.Text);
                if (CommonDAO.GetInstance().TestPing(this.dber.Connection.DataSource))
                    OutputRunInfo(rtxtOutput, "连接成功");
            }
            catch (Exception ex)
            {
                OutputRunInfo(rtxtOutput, "连接失败：" + ex.Message, eOutputType.Error);
            }
        }

        private void btnSendPlan_Click(object sender, EventArgs e)
        {
            try
            {
                EquPDCYJPlan plan = this.dber.Entity<EquPDCYJPlan>("where SampleCode=@SampleCode order by CreateDate desc", new { SampleCode = this.txt_SampleCode.Text });
                if (plan != null)
                {
                    OutputRunInfo(rtxtOutput, "该采样编码计划已存在");
                    return;
                }
                plan = new EquPDCYJPlan();
                plan.SampleCode = txt_SampleCode.Text;
                plan.GatherType = cmbSampleType.Text;
                plan.StartTime = DateTime.Now;
                plan.SampleUser = "测试";
                plan.DataFlag = 0;
                if (this.dber.Insert(plan) > 0)
                    OutputRunInfo(rtxtOutput, "发送成功");
            }
            catch (Exception ex)
            {
                OutputRunInfo(rtxtOutput, "发送失败：" + ex.Message, eOutputType.Error);
            }
        }

        private void btnStartCmd_Click(object sender, EventArgs e)
        {
            try
            {
                EquPDCYJCmd cmd = new EquPDCYJCmd();
                cmd.MachineCode = cmbBeltSampler.Text;
                cmd.CmdCode = "开始采样";
                cmd.SampleCode = txt_SampleCode.Text;
                cmd.ResultCode = "默认";
                cmd.DataFlag = 0;
                if (this.dber.Insert(cmd) > 0)
                    OutputRunInfo(rtxtOutput, "发送成功");
            }
            catch (Exception ex)
            {
                OutputRunInfo(rtxtOutput, "发送失败：" + ex.Message, eOutputType.Error);
            }
        }

        private void btnStopCmd_Click(object sender, EventArgs e)
        {
            try
            {
                EquPDCYJCmd cmd = new EquPDCYJCmd();
                cmd.MachineCode = cmbBeltSampler.Text;
                cmd.CmdCode = "结束采样";
                cmd.SampleCode = txt_SampleCode.Text;
                cmd.ResultCode = "默认";
                cmd.DataFlag = 0;
                if (this.dber.Insert(cmd) > 0)
                    OutputRunInfo(rtxtOutput, "发送成功");
            }
            catch (Exception ex)
            {
                OutputRunInfo(rtxtOutput, "发送失败：" + ex.Message, eOutputType.Error);
            }
        }

        private void btnUnLoad_Click(object sender, EventArgs e)
        {
            try
            {
                EquPDCYJUnloadCmd cmd = new EquPDCYJUnloadCmd();
                cmd.MachineCode = cmbBeltSampler.Text;
                cmd.SampleCode = txt_SampleCode.Text;
                cmd.ResultCode = "默认";
                cmd.DataFlag = 0;
                if (this.dber.Insert(cmd) > 0)
                    OutputRunInfo(rtxtOutput, "发送成功");
            }
            catch (Exception ex)
            {
                OutputRunInfo(rtxtOutput, "发送失败：" + ex.Message, eOutputType.Error);
            }
        }

    }
}
