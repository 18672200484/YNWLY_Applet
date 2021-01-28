using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;

using CMCS.Common;
using CMCS.Common.DAO;
using CMCS.Common.Entities.BeltWeight;
using CMCS.Common.Utilities;
using CMCS.DumblyConcealer.Enums;

namespace CMCS.DumblyConcealer.Tasks.HighBeltWeight
{
	/// <summary>
	/// 皮带秤接口业务
	/// </summary>
	public class EquHighBeltWeightDAO
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

		private bool isListener = false;
		/// <summary>
		/// 监听状态 true 正在监听中 false 监听已断开
		/// </summary>
		public bool IsListener
		{
			get { return isListener; }
			set { isListener = value; }
		}

		private IPEndPoint endPoint = null;
		/// <summary>
		/// 当前连接目标
		/// </summary>
		public IPEndPoint EndPoint
		{
			get { return endPoint; }
			set { endPoint = value; }
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
			try
			{
				this.IsListener = state;
				if (!state && Listener != null)
				{
					timer1.Enabled = false;
					this.Listener.Close();
					this.Listener.Dispose();
				}
			}
			catch (Exception ex)
			{
				if (this.OutPut != null) OutPut(string.Format("{0}设置连接状态失败：{1}", this.FacilityNumber, ex), eOutputType.Error);
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
		public void InitListening(string ip, int port, int localport, string facilityNumber, Action<string, eOutputType> output)
		{
			IPAddress ipAddress = IPAddress.Parse(ip);
			EndPoint = new IPEndPoint(ipAddress, port);

			this.FacilityNumber = facilityNumber;
			this.Ip = ip;
			this.Port = port;
			this.output = output;
			if (this.OutPut != null) OutPut(string.Format("{0}初始化成功", this.FacilityNumber), eOutputType.Important);
		}

		/// <summary>
		/// 开始监听
		/// </summary>
		/// <param name="listener"></param>
		/// <param name="millsecon"></param>
		/// <param name="delday"></param>
		public void StartListening(int millsecon, int delday)
		{
			this.MillSecon = millsecon;
			this.DelDay = delday;

			timer2.Interval = millsecon;
			timer2.Elapsed += new System.Timers.ElapsedEventHandler(timer2_Elapsed);
			timer2.Enabled = true;

			timer1.Interval = 2000;
			timer1.Elapsed += new System.Timers.ElapsedEventHandler(timer1_Elapsed);
			timer1.Start();
			timer2_Elapsed(null, null);
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
				timer1.Stop();
				Socketoutput socketoutput = new Socketoutput();
				socketoutput.socket = this.Listener;
				socketoutput.Output = this.OutPut;
				StateObject state = new StateObject();
				state.workSocket = this.Listener;
				socketoutput.stateobject = state;
				this.Listener.BeginReceive(state.buffer, 0, StateObject.BufferSize, 0, new AsyncCallback(ReadCallback), socketoutput);
			}
			catch
			{
				SetListenerState(false);
			}
			//timer1.Start();
		}

		/// <summary>
		/// 发送请求事件
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		void timer2_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
		{
			Send();//先发送预警状态的命令 再发送温湿度的命令
			if (DateTime.Now.Hour == 12 && DateTime.Now.Minute < 30)//清除数据
				DelData(this.DelDay, this.FacilityNumber);
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

					PrintRecvMssg(content);
				}
			}
			catch (Exception ex)
			{
				SetListenerState(false);
				if (this.OutPut != null) OutPut(string.Format("ReadCallback,原因:{0}", ex.ToString()), eOutputType.Error);
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
				decimal momentInstrument = 0, speed = 0, sumWeight = 0;

				string[] content = str.Split(' ');
				if (content.Length < 21) return;
				momentInstrument = Convert.ToDecimal(CodeChange.Hex16ToHex10(content[11].ToString() + content[12].ToString() + content[9].ToString() + content[10].ToString())) / 100m;//顺时流量
				sumWeight = Convert.ToDecimal(CodeChange.Hex16ToHex10(content[15].ToString() + content[16].ToString() + content[13].ToString() + content[14].ToString())) / 1000;//累计流量
				speed = Convert.ToDecimal(CodeChange.Hex16ToHex10(content[19].ToString() + content[20].ToString() + content[17].ToString() + content[18].ToString())) / 100m;//皮带速度
				CommonDAO.GetInstance().SetSignalDataValue(this.FacilityNumber, "累计流量", sumWeight.ToString());
				CommonDAO.GetInstance().SetSignalDataValue(this.FacilityNumber, "瞬时流量", momentInstrument.ToString());
				CommonDAO.GetInstance().SetSignalDataValue(this.FacilityNumber, "皮带速度", speed.ToString());
				if (DateTime.Now.Minute % 10 == 5)
				{
					res += Dbers.GetInstance().SelfDber.Insert<BeltWeightHistory>(
					   new BeltWeightHistory
					   {
						   FacilityNumber = this.FacilityNumber,
						   MomentInstrument = momentInstrument,
						   BeltSpeed = speed,
						   SumWeight = sumWeight,
						   RecordTime = DateTime.Now
					   }
					   );
				}
				if (this.OutPut != null) OutPut(string.Format("{0} 累计流量：{1} 瞬时流量：{2} 皮带速度：{3}", this.FacilityNumber, sumWeight, momentInstrument, speed), eOutputType.Normal);
			}
			catch (Exception ex)
			{
				if (this.OutPut != null) OutPut(string.Format("解析数据失败,原因:{0}", ex.ToString()), eOutputType.Error);
			}
			finally
			{
				timer1.Enabled = true;
			}
			//this.Listener.Close();
		}

		/// <summary>
		/// 发送请求
		/// </summary>
		public void Send()
		{
			timer2.Enabled = false;
			try
			{
				if (!this.IsListener)
				{
					this.listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
					listener.Connect(EndPoint);
					SetListenerState(true);
					if (this.OutPut != null) OutPut(string.Format("{0}连接成功", this.FacilityNumber), eOutputType.Important);
				}
				string str = "00 00 00 00 00 06 01 03 03 EE 00 06";//瞬时流量 仪表累计量 皮带速度
				listener.Send(CodeChange.StrToHex16Byte(str));
			}
			catch (Exception ex)
			{
				SetListenerState(false);
				if (this.OutPut != null) OutPut(string.Format("{0}连接断开:{1}", this.FacilityNumber, ex.Message), eOutputType.Error);
				//if (this.OutPut != null) OutPut(string.Format("发送请求失败,原因:{0}", ex.ToString()), eOutputType.Error);
			}
			finally
			{
				timer2.Enabled = true;
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
				List<BeltWeightHistory> list = Dbers.GetInstance().SelfDber.Entities<BeltWeightHistory>("where CreateDate<:CreateDate and FacilityNumber=:FacilityNumber", new { CreateDate = DateTime.Now.AddDays(-day), FacilityNumber = FacilityNumber });
				if (list != null && list.Count > 0)
				{
					foreach (BeltWeightHistory item in list)
					{
						res += Dbers.GetInstance().SelfDber.Delete<BeltWeightHistory>(item.Id);
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
			this.Listener.Close();
			this.Listener.Dispose();
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
