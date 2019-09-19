using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using CMCS.DumblyConcealer.Tasks.TemperatureHumidityInstrument;

namespace CMCS.DumblyConcealer.Win.Core
{
    public class IAssayGraber
    {
        /// <summary>
        /// 标识
        /// </summary>
        public string FacilityNumber { get; set; }
        /// <summary>
        /// IP
        /// </summary>
        public string Ip { get; set; }
        /// <summary>
        /// 端口
        /// </summary>
        public int Port { get; set; }
        /// <summary>
        /// 本机端口
        /// </summary>
        public int LocalPort { get; set; }
        /// <summary>
        /// 数据保留天数
        /// </summary>
        public int DelDays { get; set; }
        /// <summary>
        /// 数据同步间隔
        /// </summary>
        public int Milliseconds { get; set; }
        /// <summary>
        /// 监听
        /// </summary>
        /// <returns></returns>
        public Socket Socket { get; set; }

        /// <summary>
        /// 监听对象
        /// </summary>
        /// <returns></returns>
        public THDiscriminatorTCPIP SocketMethod { get; set; }
    }
}
