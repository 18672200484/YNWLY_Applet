using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Sockets;
using CMCS.DumblyConcealer.Tasks.TemperatureHumidityInstrument;
using CMCS.DumblyConcealer.Tasks.TemperatureTest;

namespace CMCS.DumblyConcealer.Win.Core
{
    /// <summary>
    /// 测温仪设备
    /// </summary>
    public class ITemperGraber
    {
        /// <summary>
        /// 标识
        /// </summary>
        public string FacilityNumber { get; set; }
        /// <summary>
        /// Com口
        /// </summary>
        public string Com { get; set; }
        /// <summary>
        /// 设备地址
        /// </summary>
        public int Addr { get; set; }
        /// <summary>
        /// CRC8校验码
        /// </summary>
        public int CRC8 { get; set; }
        /// <summary>
        /// 数据保留天数
        /// </summary>
        public int DelDays { get; set; }
        /// <summary>
        /// 数据同步间隔
        /// </summary>
        public int Milliseconds { get; set; }

        /// <summary>
        /// 串口访问对象
        /// </summary>
        /// <returns></returns>
        public TemperatureDAO ComMethod { get; set; }
    }
}
