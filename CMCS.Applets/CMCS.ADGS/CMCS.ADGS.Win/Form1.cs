using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using CMCS.ADGS.Core;
using DevComponents.DotNetBar.Controls;
using BasisPlatform.Util;
using CMCS.Common.Utilities;

namespace CMCS.ADGS.Win
{
    public partial class Form1 : BasisPlatform.Forms.FrmBasis
    {
        TaskSimpleScheduler taskSimpleScheduler = new TaskSimpleScheduler();

        GrabPerformer grabPerformer = new GrabPerformer();
        EquAssayDeviceDAO equAssayDAO = EquAssayDeviceDAO.GetInstance();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            lblVersion.Text = "版本：" + System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString();

            grabPerformer.OutputInfo += new GrabPerformer.OutputInfoEventHandler(grabPerformer_OutputInfo);
            grabPerformer.OutputError += new GrabPerformer.OutputErrorEventHandler(grabPerformer_OutputError);

            equAssayDAO.OutputInfo += new EquAssayDeviceDAO.OutputInfoEventHandler(grabPerformer_OutputInfo);
            equAssayDAO.OutputError += new EquAssayDeviceDAO.OutputErrorEventHandler(grabPerformer_OutputError);

#if DEBUG

#else
            this.IsSecretRunning = true;
            this.VerifyBeforeClose = true;
            this.ChangeNotifyIcon(this.Icon);
#endif
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            try
            {
#if DEBUG

#else
                // 添加、取消开机启动
                if (ADGSAppConfig.GetInstance().Startup)
                    StartUpUtil.InsertStartUp(Application.ProductName, Application.ExecutablePath);
                else
                    StartUpUtil.DeleteStartUp(Application.ProductName);
#endif
            }
            catch { }

            grabPerformer.StartGrab();

            ExecuteAllTask();
        }

        /// <summary>
        /// 执行所有任务
        /// </summary>
        void ExecuteAllTask()
        {
            EquAssayDeviceDAO assayDevice_DAO = EquAssayDeviceDAO.GetInstance();

            taskSimpleScheduler.StartNewTask("生成标准测硫仪数据", () =>
            {
                assayDevice_DAO.CreateToSulfurStdAssay();

            }, 30000, grabPerformer_OutputError);

            taskSimpleScheduler.StartNewTask("生成标准量热仪数据", () =>
            {
                assayDevice_DAO.CreateToHeatStdAssay();

            }, 30000, grabPerformer_OutputError);

            taskSimpleScheduler.StartNewTask("生成标准水分仪数据", () =>
            {
                assayDevice_DAO.CreateToMoistureStdAssay();

            }, 30000, grabPerformer_OutputError);

            taskSimpleScheduler.StartNewTask("生成标准工分仪数据", () =>
            {
                assayDevice_DAO.CreateToProximateStdAssay();

            }, 30000, grabPerformer_OutputError);

            taskSimpleScheduler.StartNewTask("生成标准碳氢仪数据", () =>
            {
                assayDevice_DAO.CreateToHadStdAssay();

            }, 30000, grabPerformer_OutputError);

            taskSimpleScheduler.StartNewTask("生成标准灰融仪数据", () =>
            {
                assayDevice_DAO.CreateToAshStdAssay();

            }, 30000, grabPerformer_OutputError);
        }

        void grabPerformer_OutputError(string describe, Exception ex)
        {
            OutputErrorInfo(describe, ex);

            Log4netUtil.Error(describe, ex);
        }

        void grabPerformer_OutputInfo(string info)
        {
            OutputRunInfo(rtxtOutput, info);

            Log4netUtil.Info(info);
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
        /// 输出运行信息
        /// </summary>
        /// <param name="richTextBox"></param>
        /// <param name="text"></param>
        /// <param name="outputType"></param>
        private void OutputRunInfo(RichTextBoxEx richTextBox, string text, eOutputType outputType = eOutputType.Normal)
        {
            this.InvokeEx(() =>
            {
                if (richTextBox.TextLength > 100000) richTextBox.Clear();

                text = string.Format(" # {0} - {1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), text);

                richTextBox.SelectionStart = richTextBox.TextLength;

                switch (outputType)
                {
                    case eOutputType.Normal:
                        richTextBox.SelectionColor = ColorTranslator.FromHtml("#BD86FA");
                        break;
                    case eOutputType.Important:
                        richTextBox.SelectionColor = ColorTranslator.FromHtml("#A50081");
                        break;
                    case eOutputType.Warn:
                        richTextBox.SelectionColor = ColorTranslator.FromHtml("#F9C916");
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
    }
}
