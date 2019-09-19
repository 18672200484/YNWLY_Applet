using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Data;
using System.IO.Ports;
//
using CMCS.Common;
using CMCS.Common.Entities.Sys;
using CMCS.DumblyConcealer.Tasks.CarSynchronous.Enums;
using CMCS.DapperDber.Dbs.OracleDb;
using CMCS.Common.Entities.CarTransport;
using CMCS.Common.Entities.Fuel;
using CMCS.DumblyConcealer.Enums;
using CMCS.Common.DAO;
using CMCS.Common.Entities.BaseInfo;
using CMCS.CarTransport.DAO;
using System.IO.Ports;
using TM.Temperature;
using CMCS.Common.Utilities;
using CMCS.Common.Entities.TemperatureHumidity;


namespace CMCS.DumblyConcealer.Tasks.TemperatureTest
{
    /// <summary>
    /// 煤场温度测试接口
    /// </summary>
    public class TemperatureDAO
    {
        #region Vars
        CommonDAO commonDAO = CommonDAO.GetInstance();

        /// <summary>
        /// 临时数据
        /// </summary>
        private List<byte> ReceiveList = new List<byte>();

        int Temp_Com1, Temp_Addr1, Temp_Addr2, Temp_Com2;

        private SerialPort serialPort = new SerialPort();
        private System.Timers.Timer timer1;

        /// <summary>
        /// 标识
        /// </summary>
        string FacilityNumber;

        /// <summary>
        /// 串口号
        /// </summary>
        string Com;

        /// <summary>
        /// 目标地址
        /// </summary>
        int Addr;

        /// <summary>
        /// CRC校验码
        /// </summary>
        int CRC8;

        /// <summary>
        /// 是否正在处理数据
        /// </summary>
        bool Dataing;

        /// <summary>
        /// 查询指令
        /// </summary>
        byte[] Send_Cmd;

        /// <summary>
        /// 数据保留天数
        /// </summary>
        int DelDays;
        private Action<string, eOutputType> output = null;
        /// <summary>
        /// 当前输出方法
        /// </summary>
        public Action<string, eOutputType> OutPut
        {
            get { return output; }
        }

        #endregion

        public TemperatureDAO(string com, int addr, int crc8, int deldays, string facilityNumber)
        {
            this.Com = com;
            this.Addr = addr;
            this.CRC8 = crc8;
            this.DelDays = deldays;
            this.FacilityNumber = facilityNumber;

            string cmd = string.Format("68 55 01 00 {0} {1} 0D", CodeChange.Hex10ToHex16(addr.ToString()), crc8);//查询命令
            Send_Cmd = CodeChange.StrToHex16Byte(cmd);
        }

        /// <summary>
        /// 开始接口业务
        /// </summary>
        /// <param name="output"></param>
        /// <param name="milliseconds"></param>
        public void SyncTemp(Action<string, eOutputType> output, int milliseconds)
        {
            this.output = output;
            OpenCom(this.Com, 9600);
            timer1 = new System.Timers.Timer();
            timer1.Interval = milliseconds * 1000;
            timer1.Elapsed += new System.Timers.ElapsedEventHandler(timer1_Elapsed);
            timer1.Enabled = true;
            timer1_Elapsed(null, null);
        }

        /// <summary>
        /// 数据处理
        /// </summary>
        /// <param name="data"></param>
        void ReceiveData(List<byte> data)
        {
            //55 68 01 00 C9 00 03 00 07 2F 00 D2 00 0D 2F 00 E6 00 15 2F 00 79 71 0D
            int Length = 5;
            int startindex = 7;
            for (int i = 0; i < data[6]; i++)
            {
                string machineCode = data[startindex].ToString().Trim('0') + data[startindex + 1].ToString().Trim('0');//测温杆编号

                string temper = ((data[startindex + 3] + data[startindex + 4]) / 8m).ToString("f1");//温度计算
                startindex += Length;
                commonDAO.SetSignalDataValue(GlobalVars.MachineCode_MCCW, machineCode, temper);
                OutPut("————————", eOutputType.Normal);
                OutPut(string.Format("设备:{0} 温度{1}", machineCode, temper), eOutputType.Normal);
                if (DateTime.Now.Minute == 0)
                {
                    Temperature_MC temp = new Temperature_MC();
                    temp.FacilityNumber = machineCode;
                    temp.Temperature = Convert.ToDecimal(temper);
                    temp.IsUse = "0";
                    temp.IsUpload = "0";
                    commonDAO.SelfDber.Insert(temp);
                }
            }
        }

        /// <summary>
        /// 数据处理
        /// </summary>
        /// <param name="data"></param>
        void ReceiveData2(List<byte> data)
        {
            try
            {
                //55 68 01 00 C9 00 03 00 07 2F 00 D2 00 0D 2F 00 E6 00 15 2F 00 79 71 0D

                byte[] dataArray = data.ToArray();
                string dataString = CodeChange.ByteToHex16Str(dataArray);
                string[] data16Array = dataString.Split(' ');
                if (data16Array.Length == 0) return;
                int Length = 5;
                int startindex = 7;
                int length = (data16Array.Length - 8) / 5;
                for (int i = 0; i < length; i++)
                {
                    string machineCode = CodeChange.Hex16ToHex10(data16Array[startindex] + data16Array[startindex + 1]).ToString();//测温杆编号

                    string temper = OneDigit(CodeChange.Hex16ToHex10(data16Array[startindex + 3] + data16Array[startindex + 4]) / 8m).ToString("f1");//温度计算
                    startindex += Length;
                    commonDAO.SetSignalDataValue(GlobalVars.MachineCode_MCCW, machineCode, temper);
                    OutPut("————————", eOutputType.Normal);
                    OutPut(string.Format("设备:{0} 温度{1}", machineCode, temper), eOutputType.Normal);
                    STGStoreageTemperature storagetemp = commonDAO.SelfDber.Entity<STGStoreageTemperature>("where PoleCode=:PoleCode", new { PoleCode = machineCode });
                    if (storagetemp != null)
                    {
                        storagetemp.Temperature = temper;
                        Dbers.GetInstance().SelfDber.Update(storagetemp);
                    }
                    if (DateTime.Now.Minute == 0)
                    {
                        Temperature_MC temp = new Temperature_MC();
                        temp.FacilityNumber = machineCode;
                        temp.Temperature = Convert.ToDecimal(temper);
                        temp.IsUse = "0";
                        temp.IsUpload = "0";
                        commonDAO.SelfDber.Insert(temp);
                    }
                }
            }
            catch (Exception ex)
            {
                this.OutPut("数据解析：" + ex.Message, eOutputType.Error);
            }
        }

        /// <summary>
        /// 舍去第二位小数，无论大小
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        decimal OneDigit(decimal value)
        {
            decimal result = Math.Floor(value * 10) / 10m;
            return result;
        }

        /// <summary>
        /// 打开串口
        /// 成功返回True;失败返回False;
        /// </summary>
        /// <param name="com">串口号</param>
        /// <param name="bandrate">波特率</param>
        /// <returns></returns>
        public void OpenCom(string com, int bandrate)
        {
            try
            {
                if (!serialPort.IsOpen)
                {
                    serialPort.PortName = "COM" + com;
                    serialPort.BaudRate = bandrate;
                    serialPort.DataBits = 8;
                    serialPort.StopBits = StopBits.One;
                    serialPort.Parity = Parity.None;
                    serialPort.ReceivedBytesThreshold = 1;
                    serialPort.RtsEnable = true;
                    serialPort.Open();
                    serialPort.DataReceived += new SerialDataReceivedEventHandler(serialPort_DataReceived);
                    OutPut(string.Format("串口{0}打开成功", this.Com), eOutputType.Normal);
                }
            }
            catch (Exception ex)
            {
                this.OutPut(ex.Message, eOutputType.Error);
            }
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
            }
            catch (Exception ex)
            {
                Log4Neter.Error(this.FacilityNumber + "关闭串口", ex);
            }
        }

        /// <summary>
        /// 串口接收数据
        /// 数据示例：55 68 01 00 C9 00 05 00 03 2F 00 5B 00 07 2F 00 D2 00 0D 2F 00 E6 00 15 2F 00 79 00 17 2F 00 77 BD 0D 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e)
        {
            try
            {
                Dataing = true;

                if (serialPort.IsOpen)
                {
                    int bytesToRead = serialPort.BytesToRead;
                    byte[] buffer = new byte[bytesToRead];
                    serialPort.Read(buffer, 0, bytesToRead);
                    this.ReceiveList.AddRange(buffer);
                    if (this.ReceiveList.Count > 0 && this.ReceiveList[0] == 0x55 && this.ReceiveList.Last() == 0x0D)
                    {
                        try
                        {
                            ReceiveData2(this.ReceiveList);
                        }
                        catch
                        {
                            ReceiveList.Clear();
                        }
                    }
                    ReceiveList.Clear();
                }
                Dataing = false;
            }
            catch (Exception ex)
            {
                OutPut("接收数据失败：" + ex.Message, eOutputType.Error);
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
                if (!serialPort.IsOpen)
                    serialPort.Open();
                if (!Dataing && serialPort.IsOpen)
                    serialPort.Write(Send_Cmd, 0, Send_Cmd.Length);
                if (DateTime.Now.Hour == 00 && DateTime.Now.Minute < 5)//清除数据
                    DelData(this.DelDays);
            }
            catch (Exception ex)
            {
                OutPut("发送指令失败:" + ex.Message, eOutputType.Error);
            }
            finally
            {
                timer1.Enabled = true;
            }
        }

        /// <summary>
        /// 清除历史数据
        /// </summary>
        /// <param name="day"></param>
        /// <param name="FacilityNumber"></param>
        /// <returns></returns>
        public void DelData(int day)
        {
            try
            {
                int res = 0;
                List<Temperature_MC> list = Dbers.GetInstance().SelfDber.Entities<Temperature_MC>("where CreateDate<:CreateDate", new { CreateDate = DateTime.Now.AddDays(-day) });
                if (list != null && list.Count > 0)
                {
                    foreach (Temperature_MC item in list)
                    {
                        res += Dbers.GetInstance().SelfDber.Delete<TemperatureInfo>(item.Id);
                    }
                }
                if (this.OutPut != null) OutPut(string.Format("清除历史数据 {0}条", res), eOutputType.Normal);
            }
            catch (Exception ex)
            {
                if (this.OutPut != null) OutPut(string.Format("清除历史数据失败,原因:{0}", ex.ToString()), eOutputType.Error);
            }
        }
    }
}
