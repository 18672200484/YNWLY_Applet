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

namespace CMCS.DataTester.Frms
{
    public partial class FrmPLCTest : Form
    {
        OPCServer KepServer = new OPCServer();

        #region 私有变量
        OPCGroups KepGroups;
        OPCGroup KepGroup;
        OPCItems KepItems;
        #endregion

        public FrmPLCTest()
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

        private void btnOpenNet_Click(object sender, EventArgs e)
        {
            Start();
        }
        /// <summary>
        /// OPC服务器
        /// 开始抓取OPC数据
        /// </summary>
        public void Start()
        {
            try
            {
                KepServer.Connect("Matrikon.OPC.Simulation.1", txtIp.Text);


                //判断连接状态
                if (KepServer.ServerState == (int)OPCServerState.OPCRunning)
                {
                    ShowMessage("已连接到-" + KepServer.ServerName);
                }
                else
                {
                    ShowMessage("状态：" + KepServer.ServerState.ToString());
                    return;
                }

                KepGroups = KepServer.OPCGroups;

                Task.Factory.StartNew(CreateGroup);

                //this.GatherData = true;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        /// <summary>
        /// 创建组
        /// </summary>
        private void CreateGroup()
        {
            try
            {
                OPCGroups groups = KepServer.OPCGroups;
                OPCGroup KepGroup = groups.Add("OpcGroup");
                SetGroupProperty();
                KepGroup.DataChange += new DIOPCGroupEvent_DataChangeEventHandler(KepGroup_DataChange);
                //KepGroup.AsyncWriteComplete += new DIOPCGroupEvent_AsyncWriteCompleteEventHandler(KepGroup_AsyncWriteComplete);

                KepItems = KepGroup.OPCItems;
                AddOpcItem();
            }
            catch (Exception err)
            {
                ShowMessage("枚举本地OPC创建组出现错误：" + err.Message);
            }
        }

        private void AddOpcItem()
        {
            KepItems.AddItem("a1.22.1", 1);
            KepItems.AddItem("a2.22.2", 2);
            KepItems.AddItem("a3.22.3", 3);
        }

        /// <summary>
        /// 设置组属性
        /// </summary>
        private void SetGroupProperty()
        {
            KepServer.OPCGroups.DefaultGroupIsActive = true;
            KepServer.OPCGroups.DefaultGroupDeadband = 0;
            KepGroup.UpdateRate = 3000;
            KepGroup.IsActive = true;
            KepGroup.IsSubscribed = true;
        }

        /// <summary>
        /// 每当项数据有变化时执行的事件
        /// </summary>
        /// <param name="TransactionID">处理ID</param>
        /// <param name="NumItems">项个数</param>
        /// <param name="ClientHandles">项客户端句柄</param>
        /// <param name="ItemValues">TAG值</param>
        /// <param name="Qualities">品质</param>
        /// <param name="TimeStamps">时间戳</param>
        private void KepGroup_DataChange(int TransactionID, int NumItems, ref Array ClientHandles, ref Array ItemValues, ref Array Qualities, ref Array TimeStamps)
        {
            try
            {
                for (int i = 1; i <= NumItems; i++)
                {
                    string temp = string.Concat(ClientHandles.GetValue(i), "-", ItemValues.GetValue(i), "-", Qualities.GetValue(i), "-", TimeStamps.GetValue(i));
                    ShowMessage(temp);
                }
            }
            catch (Exception e)
            {
                ShowMessage("KepGroup_DataChange" + e.Message);
            }
        }
    }
}
