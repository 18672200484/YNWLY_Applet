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
using CMCS.DumblyConcealer.Tasks.AutoMaker.Entities;
using CMCS.Common.Entities.iEAA;
using CMCS.DumblyConcealer.Tasks.AutoCupboard.Entities;

namespace CMCS.DataTester.Frms
{
    public partial class FrmAutoCupBoard_Test : Form
    {
        SqlServerDapperDber AutoCupboard_Dber = new SqlServerDapperDber(CommonDAO.GetInstance().GetCommonAppletConfigString("智能存样柜接口连接字符串"));

        bool isStartSimulator = false;
        /// <summary>
        /// 是否开始模拟
        /// </summary>
        public bool IsStartSimulator
        {
            get { return isStartSimulator; }
            set
            {
                isStartSimulator = value;

                //rbtnMachineCode1.Enabled = !isStartSimulator;
                //rbtnMachineCode2.Enabled = !isStartSimulator;

                btnStart.Text = value ? "停止模拟" : "开始模拟";
            }
        }

        public FrmAutoCupBoard_Test()
        {
            InitializeComponent();
        }

        private void FrmBeltSamplerSimulator_Load(object sender, EventArgs e)
        {
            txtcode.Text = Guid.NewGuid().ToString().Substring(1, 8);
            CreateMainTask();
        }

        SqlServerDapperDber EquDber
        {
            get
            {
                //if (rbtnMachineCode1.Checked)
                return AutoCupboard_Dber;
                //else
                //    return dber2;
            }
        }

        /// <summary>
        /// 开始模拟
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnStart_Click(object sender, EventArgs e)
        {
            this.IsStartSimulator = !IsStartSimulator;
        }

        TaskSimpleScheduler taskSimpleScheduler = new TaskSimpleScheduler();

        System.Threading.AutoResetEvent autoResetEvent = new AutoResetEvent(false);

        private void CreateMainTask()
        {
            taskSimpleScheduler = new TaskSimpleScheduler();

            autoResetEvent.Reset();

            taskSimpleScheduler.StartNewTask("模拟业务", () =>
            {
                if (!this.IsStartSimulator) return;

                // 心跳
                this.EquDber.Execute("update " + EntityReflectionUtil.GetTableName<EquCYGSignal>() + " set TagValue=@TagValue where TagName=@TagName", new { TagName = GlobalVars.EquHeartbeatName, TagValue = DateTime.Now.ToString() });

                // 控制命令
                EquCYGCmd pCYGCmd = this.EquDber.Entity<EquCYGCmd>("where DataFlag=0 order by CreateDate desc");
                if (pCYGCmd != null)
                {
                    CmdHandle(pCYGCmd);

                    autoResetEvent.WaitOne();
                }

            }, 3000);
        }

        private void CmdHandle(EquCYGCmd input)
        {
            Task task = new Task((state) =>
            {
                EquCYGCmd pCYGCmd = state as EquCYGCmd;
                OutputRunInfo(rtxtOutput, "处理命令，命令代码：" + pCYGCmd.CmdCode + "  编码：" + pCYGCmd.SampleCode);

                Thread.Sleep(5000);
                pCYGCmd.ResultCode = 1;
                pCYGCmd.FinishTime = DateTime.Now;
                pCYGCmd.DataFlag = 1;
                this.EquDber.Update(pCYGCmd);
                OutputRunInfo(rtxtOutput, "取样成功,编码：" + pCYGCmd.SampleCode);
                EquCYGSample equcygsample = this.EquDber.Entity<EquCYGSample>(" where SampleCode='" + pCYGCmd.SampleCode + "'");
                if (equcygsample != null)
                {
                    equcygsample.SampleCode = "";
                    equcygsample.DataFlag = 0;
                    this.EquDber.Update(equcygsample);
                }
                OutputRunInfo(rtxtOutput, "清理样柜成功,编码：" + pCYGCmd.SampleCode);
                autoResetEvent.Set();

            }, input);
            task.Start();
        }

        /// <summary>
        /// 重置所有接口表
        /// </summary>
        void ResetAll()
        {
            this.EquDber.Execute("update " + EntityReflectionUtil.GetTableName<EquCYGCmd>() + " set DataFlag=3");
        }

        #region 改变系统状态

        private void btnSystemStatus_JXDJ_Click(object sender, EventArgs e)
        {
            OutputRunInfo(rtxtOutput, "系统状态更改为" + eEquInfSamplerSystemStatus.就绪待机.ToString());
            this.EquDber.Execute("update " + EntityReflectionUtil.GetTableName<EquCYGSignal>() + " set TagValue=@TagValue where TagName=@TagName", new { TagName = eSignalDataName.设备状态.ToString(), TagValue = eEquInfSamplerSystemStatus.就绪待机.ToString() });
        }

        private void btnSystemStatus_ZZYX_Click(object sender, EventArgs e)
        {
            OutputRunInfo(rtxtOutput, "系统状态更改为" + eEquInfSamplerSystemStatus.正在运行.ToString());
            this.EquDber.Execute("update " + EntityReflectionUtil.GetTableName<EquCYGSignal>() + " set TagValue=@TagValue where TagName=@TagName", new { TagName = eSignalDataName.设备状态.ToString(), TagValue = eEquInfSamplerSystemStatus.正在运行.ToString() });
        }

        private void btnSystemStatus_FSGZ_Click(object sender, EventArgs e)
        {
            OutputRunInfo(rtxtOutput, "系统状态更改为" + eEquInfSamplerSystemStatus.发生故障.ToString());
            this.EquDber.Execute("update " + EntityReflectionUtil.GetTableName<EquCYGSignal>() + " set TagValue=@TagValue where TagName=@TagName", new { TagName = eSignalDataName.设备状态.ToString(), TagValue = eEquInfSamplerSystemStatus.发生故障.ToString() });
        }

        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetAll();
        }

        #endregion

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

        private void btnInsert_Click(object sender, EventArgs e)
        {
            try
            {
                EquCYGSample equcygsample = new EquCYGSample();
                equcygsample.SampleCode = txtcode.Text;
                equcygsample.MachineCode = txtmechine.Text;
                equcygsample.DepositTime = DateTime.Now;
                equcygsample.DataFlag = 0;
                equcygsample.SampleType = txttype.Text;
                this.EquDber.Insert(equcygsample);
                OutputRunInfo(rtxtOutput, "处理命令，模拟手动发送站存入编码：" + equcygsample.SampleCode);
            }
            catch (Exception)
            {

                throw;
            }
            txtcode.Text = Guid.NewGuid().ToString().Substring(1, 8);
        }
    }
}
