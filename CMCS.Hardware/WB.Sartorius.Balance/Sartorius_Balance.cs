using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO.Ports;
using System.Threading;

namespace WB.Sartorius.Balance
{
    /// <summary>
    /// 赛多利斯电子天平
    /// </summary>
    public class Sartorius_Balance
    {
        /// <summary>
        /// TOLEDO_IND245Wber
        /// </summary>
        public Sartorius_Balance()
        {
        }

        private SerialPort serialPort = new SerialPort();
        private string MachineCode;

        public delegate void SteadyChangeEventHandler(string machinCode, bool steady);
        public delegate void StatusChangeHandler(string machinCode, bool status);
        public event StatusChangeHandler OnStatusChange;
        public delegate void WeightChangeEventHandler(string machinCode, double weight);
        public event WeightChangeEventHandler OnWeightChange;

        private bool status = false;
        /// <summary>
        /// 连接状态
        /// </summary>
        public bool Status
        {
            get { return status; }
        }

        private double weight;
        /// <summary>
        /// 重量
        /// </summary>
        public double Weight
        {
            get { return weight; }
        }

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
        /// <param name="dataBits">数据位</param>
        /// <param name="parity">校验</param>
        /// <returns></returns>
        public bool OpenCom(int com, int bandrate, int dataBits, int parity, string machineCode = "")
        {
            try
            {
                this.MachineCode = machineCode;
                if (!serialPort.IsOpen)
                {
                    serialPort.PortName = "COM" + com.ToString();
                    serialPort.BaudRate = bandrate;
                    serialPort.DataBits = dataBits;
                    serialPort.StopBits = StopBits.One;
                    serialPort.Parity = (Parity)parity;
                    serialPort.ReceivedBytesThreshold = 1;
                    serialPort.RtsEnable = true;
                    serialPort.Open();
                    serialPort.DataReceived += new SerialDataReceivedEventHandler(serialPort_DataReceived);

                    SetStatus(true);

                    this.Closing = false;
                }
            }
            catch (Exception)
            {
                this.status = false;
                if (this.OnStatusChange != null) this.OnStatusChange(this.MachineCode, status);
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
                this.Closing = true;

                serialPort.DataReceived -= new SerialDataReceivedEventHandler(serialPort_DataReceived);
                serialPort.Close();

                SetStatus(false);
            }
            catch { }
        }

        bool Closing = false;

        /// <summary>
        /// 设置连接状态
        /// </summary>
        /// <param name="status"></param>
        public void SetStatus(bool status)
        {
            if (this.status != status && this.OnStatusChange != null) this.OnStatusChange(this.MachineCode, status);
            this.status = status;
        }

        /// <summary>
        /// 串口接收数据
        /// 数据示例：02 31 30 20 20 20 20 32 30 30 20 20 20 20 30 30 0D 1E 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            if (this.Closing) return;

            this.ReceiveCount++;

            if (serialPort.IsOpen)
            {
                int bytesToRead = serialPort.BytesToRead;
                byte[] buffer = new byte[bytesToRead];
                serialPort.Read(buffer, 0, bytesToRead);

                for (int i = 0; i < bytesToRead; i++)
                {
                    if (buffer[i] == 0x47 || buffer[i] == 0x4E) ReceiveList.Clear();

                    ReceiveList.Add(buffer[i]);

                    try
                    {
                        if (buffer[i] == 0x0A && ReceiveList.Count == 22)
                        {
                            string temp = string.Empty;
                            for (int j = 7; j < 17; j++)
                            {
                                temp += Convert.ToChar(ReceiveList[j]);
                            }

                            if (ReceiveList[6] == 0x2D)
                                this.weight = Convert.ToDouble(temp) / -1d;
                            else
                                this.weight = Convert.ToDouble(temp) / 1d;

                            if (OnWeightChange != null) OnWeightChange(this.MachineCode, this.weight);
                            SetStatus(true);
                            ReceiveList.Clear();
                        }
                    }
                    catch
                    {
                        SetStatus(false);
                    }
                }
            }
        }
    }
}
