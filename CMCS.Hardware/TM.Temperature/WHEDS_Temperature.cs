using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading;

namespace TM.Temperature
{
    /// <summary>
    /// 武汉恩德斯温度测试仪
    /// </summary>
    public class WHEDS_Temperature
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sendDatavalue">指令</param>
        public WHEDS_Temperature(string sendDatavalue, byte[] cmd_byte)
        {
            this.SendDataValue = sendDatavalue;
            this.SendDataValue_byte = cmd_byte;
            timer1 = new System.Timers.Timer(1000)
            {
                AutoReset = true
            };
            timer1.Elapsed += new System.Timers.ElapsedEventHandler(timer1_Elapsed);
        }

        private SerialPort serialPort = new SerialPort();
        private System.Timers.Timer timer1;

        //public delegate void SteadyChangeEventHandler(bool steady);
        //public event SteadyChangeEventHandler OnSteadyChange;
        public delegate void StatusChangeHandler(bool status);
        public event StatusChangeHandler OnStatusChange;
        //public delegate void WeightChangeEventHandler(double weight);
        //public event WeightChangeEventHandler OnWeightChange;

        /// <summary>
        /// 接收数据成功
        /// </summary>
        public Action<List<byte>> OnReceiveSuccess;

        private bool status = false;
        /// <summary>
        /// 连接状态
        /// </summary>
        public bool Status
        {
            get { return status; }
        }

        private bool dataing = false;
        /// <summary>
        /// 是否数据处理中
        /// </summary>
        public bool Dataing
        {
            get { return dataing; }
        }

        /// <summary>
        /// 发送数据指令
        /// </summary>
        private string SendDataValue;

        /// <summary>
        /// 发送数据指令
        /// </summary>
        private byte[] SendDataValue_byte;

        /// <summary>
        /// 数据接收次数
        /// </summary>
        private int ReceiveCount = 0;

        /// <summary>
        /// 临时数据
        /// </summary>
        private List<byte> ReceiveList = new List<byte>();

        /// <summary>
        /// 打开串口
        /// 成功返回True;失败返回False;
        /// </summary>
        /// <param name="com">串口号</param>
        /// <param name="bandrate">波特率</param>
        /// <returns></returns>
        public bool OpenCom(int com, int bandrate)
        {
            try
            {
                if (!serialPort.IsOpen)
                {
                    serialPort.PortName = "COM" + com.ToString();
                    serialPort.BaudRate = bandrate;
                    serialPort.DataBits = 8;
                    serialPort.StopBits = StopBits.One;
                    serialPort.Parity = Parity.None;
                    serialPort.ReceivedBytesThreshold = 1;
                    serialPort.RtsEnable = true;
                    serialPort.Open();
                    serialPort.DataReceived += new SerialDataReceivedEventHandler(serialPort_DataReceived);

                    timer1.Enabled = true;

                    SetStatus(true);
                }
            }
            catch (Exception)
            {
                this.status = false;
                if (this.OnStatusChange != null) this.OnStatusChange(status);
            }

            return this.status;
        }

        /// <summary>
        /// 关闭串口
        /// 成功返回True;失败返回False;
        /// </summary>
        /// <returns></returns>
        public void CloseCom()
        {
            try
            {
                timer1.Enabled = false;

                serialPort.DataReceived -= new SerialDataReceivedEventHandler(serialPort_DataReceived);
                serialPort.Close();

                SetStatus(false);
            }
            catch { }
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

        /// <summary>
        /// 串口接收数据
        /// 数据示例：55 68 01 00 C9 00 05 00 03 2F 00 5B 00 07 2F 00 D2 00 0D 2F 00 E6 00 15 2F 00 79 00 17 2F 00 77 BD 0D 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            dataing = true;
            this.ReceiveCount++;

            if (serialPort.IsOpen)
            {
                int bytesToRead = serialPort.BytesToRead;
                byte[] buffer = new byte[bytesToRead];
                serialPort.Read(buffer, 0, bytesToRead);
                this.ReceiveList.AddRange(buffer);

                try
                {
                    if (OnReceiveSuccess != null) OnReceiveSuccess(this.ReceiveList);
                }
                catch
                {
                    ReceiveList.Clear();
                }
                ReceiveList.Clear();
                this.ReceiveCount = 0;
            }
        }

        /// <summary>
        /// 间隔事件 发送请求指令
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            timer1.Enabled = false;
            try
            {
                //SendData();
            }
            catch (Exception)
            {
                SetStatus(false);
            }
            finally
            {
                timer1.Enabled = true;
            }
        }

        /// <summary>
        /// 发送指令
        /// </summary>
        /// <param name="data"></param>
        public void SendData()
        {
            //string text = string.Format("68 55 01 00 C9 87 0D");
            if (serialPort.IsOpen)
            {
                //serialPort.Write(SendDataValue);
                serialPort.Write(SendDataValue_byte, 0, SendDataValue_byte.Length);
            }
        }

    }
}
