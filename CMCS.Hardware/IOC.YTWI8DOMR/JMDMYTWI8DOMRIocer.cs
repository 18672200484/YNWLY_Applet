using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using System.Net;

namespace IOC.JMDMYTWI8DOMR
{
    public class JMDMYTWI8DOMRIocer
    {
        public JMDMYTWI8DOMRIocer()
        {
            timer1 = new System.Timers.Timer(2000)
            {
                AutoReset = true
            };
            timer1.Elapsed += new System.Timers.ElapsedEventHandler(timer1_Elapsed);
        }

        public delegate void ReceivedEventHandler(int[] receiveValue);
        public event ReceivedEventHandler OnReceived;

        public delegate void StatusChangeHandler(bool status);
        public event StatusChangeHandler OnStatusChange;

        /// <summary>
        /// 接收到的数据
        /// </summary>
        public int[] ReceiveValue = new int[20];

        private System.Timers.Timer timer1;

        private UdpClient udpClient;

        public UdpClient UdpClient
        {
            get { return udpClient; }
            set { udpClient = value; }
        }

        /// <summary>
        /// 设置连接状态
        /// </summary>
        /// <param name="status"></param>
        public void SetStatus(bool status)
        {
            if (this.status != status && this.OnStatusChange != null) this.OnStatusChange(status);
            this.status = status;
        }


        private bool status;

        public bool Status
        {
            get { return status; }
            set { status = value; }
        }


        private IPEndPoint remotePoint = null;

        /// <summary>
        /// 打开UDP端口
        /// </summary>
        /// <param name="tcp"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public void OpenUDP(string ip, int port)
        {
            try
            {
                IPAddress remoteIP = IPAddress.Parse(ip);

                remotePoint = new IPEndPoint(remoteIP, port);//实例化一个远程端点 

                UdpClient = new UdpClient();

                UdpClient.Connect(remotePoint);

                SetStatus(true);

                timer1.Enabled = true;
            }
            catch (Exception)
            {
                this.status = false;
                if (this.OnStatusChange != null) this.OnStatusChange(status);
            }

        }

        /// <summary>
        /// 关闭UDP端口
        /// </summary>
        /// <param name="tcp"></param>
        /// <param name="port"></param>
        /// <returns></returns>
        public void ClostUDP()
        {
            this.UdpClient.Close();
            this.status = false;
            SetStatus(false);
            timer1.Enabled = false;
        }

        /// <summary>
        /// 接收数据
        /// </summary>
        public void ReceiveData()
        {
            if (this.status)
            {
                try
                {
                    Output("123456:I");

                    IPEndPoint RemoteIpEndPoint = new IPEndPoint(IPAddress.Any, 0);

                    Byte[] receiveBytes = this.UdpClient.Receive(ref RemoteIpEndPoint);

                    string returnData = Encoding.ASCII.GetString(receiveBytes);

                    returnData = returnData.Replace("I=", "");

                    string[] returnDataArr = new string[12];

                    for (var j = 0; j < returnData.Length; j++)
                    {
                        returnDataArr[j] = returnData.Substring(j, 1);
                    }

                    for (int i = 0; i < returnDataArr.Length; i++)
                    {
                        this.ReceiveValue[i] = int.Parse(returnDataArr[i]);
                    }

                    if (OnReceived != null) OnReceived(this.ReceiveValue);
                }
                catch { }
            }
        }


        /// <summary>
        /// 间隔事件
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            ReceiveData();
        }

        /// <summary>
        /// 输入
        /// </summary>
        /// <param name="pnum">通道号</param>
        /// <param name="type"></param>
        public void Output(int pnum, bool type)
        {
            if (this.status)
            {
                string str = "";

                for (int i = 1; i <= 8; i++)
                {
                    if (i == pnum)
                    {
                        str += type == true ? "1" : "0";
                    }
                    else
                    {
                        str += "2";
                    }
                }

                string sendCmd = string.Format("123456:({0})", str);

                byte[] buffer = System.Text.Encoding.Default.GetBytes(sendCmd);

                this.UdpClient.Send(buffer, buffer.Length);//将数据发送到远程端点 
            }
        }

        /// <summary>
        /// 输入命令
        /// </summary>
        /// <param name="sendCmd"></param>
        public void Output(string sendCmd)
        {
            if (this.status)
            {
                byte[] buffer = System.Text.Encoding.Default.GetBytes(sendCmd);

                this.UdpClient.Send(buffer, buffer.Length);//将数据发送到远程端点 
            }
        }
    }
}
