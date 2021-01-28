using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Net.Sockets;
using CMCS.Common;
using System.Threading;
using CMCS.DumblyConcealer.Enums;
using CMCS.Common.Entities.TemperatureHumidity;
using CMCS.Common.DAO;

namespace CMCS.DumblyConcealer.Tasks.TemperatureHumidityInstrument
{
	/// <summary>
	/// 温湿度仪-TCP/IP协议
	/// </summary>
	public class THDiscriminatorTCPIP
	{
		#region Vars
		private string facilityNumber;
		/// <summary>
		/// 标识
		/// </summary>
		string FacilityNumber
		{
			get { return facilityNumber; }
			set { facilityNumber = value; }
		}

		private string ip;
		/// <summary>
		/// IP
		/// </summary>
		string Ip
		{
			get { return ip; }
			set { ip = value; }
		}

		private int port;
		/// <summary>
		/// 端口
		/// </summary>
		int Port
		{
			get { return port; }
			set { port = value; }
		}

		private int millSecond;
		/// <summary>
		/// 数据同步间隔
		/// </summary>
		int MillSecon
		{
			get { return millSecond; }
			set { millSecond = value; }
		}

		private int delDay;
		/// <summary>
		/// 数据保留天数
		/// </summary>
		int DelDay
		{
			get { return delDay; }
			set { delDay = value; }
		}

		//温度 湿度 预警
		string temperaturePre = "正常", humidityPre = "正常", onoffPre = "正常";

		/// <summary>
		/// 监听定时器
		/// </summary>
		System.Timers.Timer timer1 = new System.Timers.Timer();

		/// <summary>
		/// 发送请求定时器
		/// </summary>
		System.Timers.Timer timer2 = new System.Timers.Timer();

		private Socket listener = null;
		/// <summary>
		/// 当前连接
		/// </summary>
		public Socket Listener
		{
			get { return listener; }
		}

		/// <summary>
		/// 等待进程
		/// </summary>
		private ManualResetEvent allDone = new ManualResetEvent(false);

		private Action<string, eOutputType> output = null;
		/// <summary>
		/// 当前输出方法
		/// </summary>
		public Action<string, eOutputType> OutPut
		{
			get { return output; }
		}

		/// <summary>
		/// 设备连接Point
		/// </summary>
		IPEndPoint serverEndPoint;

		private string type = "03";
		/// <summary>
		/// 发送请求类型 03：预警 04：温湿度
		/// </summary>
		public string Type { get { return type; } }

		private bool isListener = false;
		/// <summary>
		/// 监听状态 true 正在监听中 false 监听已断开
		/// </summary>
		public bool IsListener
		{
			get { return isListener; }
			set { isListener = value; }
		}
		#endregion

		#region Class
		private class StateObject
		{
			public Socket workSocket = null;
			public const int BufferSize = 1024;
			public byte[] buffer = new byte[BufferSize];
			public StringBuilder sb = new StringBuilder();
		}
		private class Socketoutput
		{
			public StateObject stateobject;
			public Socket socket;
			public Action<string, eOutputType> Output;
		}

		#endregion

		#region Method

		/// <summary>
		/// 设置当前连接状态
		/// </summary>
		/// <param name="state"></param>
		/// <returns></returns>
		public void SetListenerState(bool state)
		{
			this.IsListener = state;
			if (!state && Listener != null)
			{
				timer1.Enabled = false;
				this.Listener.Close();
				this.Listener.Dispose();
			}
		}

		/// <summary>
		/// 初始化Socket通信对象
		/// </summary>
		/// <param name="ip"></param>
		/// <param name="port"></param>
		/// <param name="facilityNumber"></param>
		/// <param name="output"></param>
		/// <returns></returns>
		public void InitConnect(string ip, int port, int localport, string facilityNumber, int delday, Action<string, eOutputType> output)
		{
			IPAddress ipAddress = IPAddress.Parse(ip);
			serverEndPoint = new IPEndPoint(ipAddress, port);
			//IPEndPoint localEndPoint = new IPEndPoint(IPAddress.Parse(GetIp()), localport);

			this.FacilityNumber = facilityNumber;
			this.Ip = ip;
			this.Port = port;
			this.DelDay = delday;
			this.output = output;
			if (this.OutPut != null) OutPut(string.Format("{0}初始化成功", this.FacilityNumber), eOutputType.Important);
		}

		/// <summary>
		/// 开始监听
		/// </summary>
		/// <param name="listener"></param>
		/// <param name="millsecon"></param>
		/// <param name="delday"></param>
		public void StartListening(int millsecon)
		{
			this.MillSecon = millsecon;

			timer2.Interval = millsecon;
			timer2.Elapsed += new System.Timers.ElapsedEventHandler(timer2_Elapsed);
			timer2.Enabled = true;
			timer2_Elapsed(null, null);

			timer1.Interval = 200;
			timer1.Elapsed += new System.Timers.ElapsedEventHandler(timer1_Elapsed);
			//timer1.Enabled = true;
		}

		/// <summary>
		/// 监听事件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void timer1_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			try
			{
				if (!this.IsListener) return;
				timer1.Stop();

				#region 异步监听
				Socketoutput socketoutput = new Socketoutput();
				socketoutput.socket = this.Listener;
				socketoutput.Output = this.OutPut;
				StateObject state = new StateObject();
				state.workSocket = this.Listener;
				socketoutput.stateobject = state;
				this.Listener.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReadCallback), socketoutput);
				#endregion

				#region 同步监听
				//byte[] buffer = new byte[] { };
				//string content = string.Empty;
				//this.Listener.Receive(buffer);
				//if (buffer.Length > 0)
				//{
				//    //转换为16进制
				//    for (int i = 0; i < buffer.Length; i++)
				//    {
				//        content += string.Format("{0:x2} ", buffer[i]);//注意空格
				//    }
				//    this.IsListener = true;
				//    PrintRecvMssg(content.Trim());
				//}
				#endregion
			}
			catch
			{
				SetListenerState(false);
			}
			finally
			{
				timer1.Start();
			}
		}

		/// <summary>
		/// 发送请求事件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void timer2_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			try
			{
				timer2.Stop();
				if (!this.IsListener)
				{
					this.listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
					listener.Connect(serverEndPoint);


					SetListenerState(true);
					if (this.OutPut != null) OutPut(string.Format("{0}连接成功", this.FacilityNumber), eOutputType.Important);
					timer1_Elapsed(null, null);
				}
				Send(this.type);//先发送预警状态的命令 再发送温湿度的命令
				this.type = this.type == "04" ? "03" : "04";
				if (DateTime.Now.Hour == 00 && DateTime.Now.Minute < 5)//清除数据
					DelData(this.DelDay, this.FacilityNumber);
			}
			catch (Exception ex)
			{
				SetListenerState(false);
				if (this.OutPut != null) OutPut(string.Format("{0}连接断开:{1}", this.FacilityNumber, ex.Message), eOutputType.Error);
			}
			finally
			{
				timer2.Start();
			}
		}

		/// <summary>
		/// 发送请求
		/// </summary>
		public void Send(string type)
		{
			string str = string.Empty;
			if (type == "03")//报警状态（保持寄存器）的读取
			{
				str = "00 00 00 00 00 06 00 03 00 00 00 02";
			}
			else if (type == "04")//温湿度数据（输入寄存器）的读取
			{
				str = "00 00 00 00 00 06 00 04 00 00 00 02";
			}
			this.Listener.Send(strToHexByte(str));
		}

		/// <summary>
		/// 监听异步回传
		/// </summary>
		/// <param name="ar"></param>
		private void ReadCallback(IAsyncResult ar)
		{
			String content = String.Empty;
			Socketoutput socketoutput = (Socketoutput)ar.AsyncState;
			StateObject state = (StateObject)socketoutput.stateobject;
			try
			{
				Socket handler = state.workSocket;
				int bytesRead = handler.EndReceive(ar);
				if (bytesRead > 0)
				{
					//转换为16进制
					for (int i = 0; i < bytesRead; i++)
					{
						content += string.Format("{0:x2} ", state.buffer[i]);//注意空格
					}
					PrintRecvMssg(content.Trim());
				}
			}
			catch (Exception ex)
			{
				SetListenerState(false);
				if (this.OutPut != null) OutPut(string.Format("{0}连接断开", this.FacilityNumber), eOutputType.Error);
				//if (this.OutPut != null) OutPut(string.Format("ReadCallback,原因:{0}", ex.ToString()), eOutputType.Error);
			}
		}

		/// <summary>
		/// 数据处理
		/// </summary>
		/// <param name="str"></param>
		/// <param name="output"></param>
		private void PrintRecvMssg(string str)
		{
			timer1.Enabled = false;
			int res = 0;
			try
			{
				decimal temperature = 0, humidity = 0;

				string[] content = str.Split(' ');
				if (content.Length == 0 || content.Length < 13) return;

				if (content[7] == "03")
				{
					temperaturePre = content[9] == "00" ? "正常" : "异常";
					humidityPre = content[10] == "00" ? "正常" : "异常";
					onoffPre = content[11] + content[12] == "0000" ? "正常" : "异常";
				}
				else if (content[7] == "04")
				{
					if (content[9] == "80")//负数 最小 -20
						temperature = Convert.ToDecimal(Int32.Parse(content[10], System.Globalization.NumberStyles.HexNumber)) / -10m;
					else //最大 80
						temperature = Convert.ToDecimal(Int32.Parse((content[9] + content[10]), System.Globalization.NumberStyles.HexNumber)) / 10m;
					humidity = Convert.ToDecimal(Int32.Parse((content[11] + content[12]), System.Globalization.NumberStyles.HexNumber)) / 10m;
					CommonDAO.GetInstance().SetSignalDataValue(this.FacilityNumber, "当前温度", temperature.ToString());
					CommonDAO.GetInstance().SetSignalDataValue(this.FacilityNumber, "当前湿度", humidity.ToString());

					if (DateTime.Now.Minute == 0)
					{
						res += Dbers.GetInstance().SelfDber.Insert<TemperatureInfo>(
						   new TemperatureInfo
						   {
							   FacilityNumber = this.FacilityNumber,
							   Temperature = temperature,
							   Humidity = humidity,
							   TemperaturePre = temperaturePre,
							   HumidityPre = humidityPre,
							   OnOffPre = onoffPre,
							   IsUpload = "0",
							   IsUse = "0"
						   }
						   );
					}
					DateTime time = Convert.ToDateTime(DateTime.Now.ToString("yyyy-MM-dd") + " " + CommonDAO.GetInstance().GetCommonAppletConfigString("温湿度仪写入时间"));
					if (DateTime.Now > time)
					{
						Humiture hum = Dbers.GetInstance().SelfDber.Entity<Humiture>("where RecordDate=:RecordDate order by CreateDate desc", new { RecordDate = time });
						if (hum == null)
						{
							res += Dbers.GetInstance().SelfDber.Insert<Humiture>(
								new Humiture
								{
									Equipment = this.FacilityNumber,
									Temperature = temperature,
									Humidity = humidity,
									RecordDate = time,
									DataFlag = 1,
								}
								);
						}
					}
					if (this.OutPut != null) OutPut(string.Format("成功读取{0}数据 温度:{1} 湿度:{2} 温度预警:{3} 湿度预警:{4} 开关预警:{5}", this.FacilityNumber, temperature, humidity, temperaturePre, humidityPre, onoffPre), eOutputType.Normal);
				}
			}
			catch (Exception ex)
			{
				if (this.OutPut != null) OutPut(string.Format("解析数据失败,原因:{0}", ex.ToString()), eOutputType.Error);
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
		public void DelData(int day, string FacilityNumber)
		{
			try
			{
				int res = 0;
				List<TemperatureInfo> list = Dbers.GetInstance().SelfDber.Entities<TemperatureInfo>("where CreateDate<:CreateDate and FacilityNumber=:FacilityNumber", new { CreateDate = DateTime.Now.AddDays(-day), FacilityNumber = FacilityNumber });
				if (list != null && list.Count > 0)
				{
					foreach (TemperatureInfo item in list)
					{
						res += Dbers.GetInstance().SelfDber.Delete<TemperatureInfo>(item.Id);
					}
				}
				if (this.OutPut != null) OutPut(string.Format("清除{0}数据 {1}条", this.FacilityNumber, res), eOutputType.Normal);
			}
			catch (Exception ex)
			{
				if (this.OutPut != null) OutPut(string.Format("清除历史数据失败,原因:{0}", ex.ToString()), eOutputType.Error);
			}
		}

		/// <summary>
		/// 关闭连接
		/// </summary>
		public void Close()
		{
			this.timer1.Enabled = false;
			this.timer1.Stop();
			this.timer2.Enabled = false;
			this.timer2.Stop();
			SetListenerState(false);
			if (this.OutPut != null) OutPut(string.Format("{0}关闭连接", this.FacilityNumber), eOutputType.Error);
		}

		#endregion

		#region Untils
		/// <summary>
		/// 字符串转16进制
		/// </summary>
		/// <param name="hexString"></param>
		/// <returns></returns>
		private static byte[] strToHexByte(string hexString)
		{
			hexString = hexString.Replace(" ", "");
			if ((hexString.Length % 2) != 0) hexString += "";
			byte[] returnBytes = new byte[hexString.Length / 2];
			for (int i = 0; i < returnBytes.Length; i++)
			{
				returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
			}
			return returnBytes;
		}

		/// <summary>
		/// 获取本机IP
		/// </summary>
		/// <returns></returns>
		private string GetIp()
		{
			IPHostEntry ipHost = Dns.Resolve(Dns.GetHostName());
			IPAddress ipAddr = ipHost.AddressList[0];
			return ipAddr.ToString();
		}

		#endregion
	}
}
