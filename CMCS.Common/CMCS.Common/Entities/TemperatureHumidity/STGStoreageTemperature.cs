using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.Entities.Sys;

namespace CMCS.Common.Entities.TemperatureHumidity
{
    /// <summary>
    /// 温湿度仪
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("CMCSTBHUMITURE")]
    public class Humiture : EntityBase1
    {
        private string recordPle;
        /// <summary>
        /// 记录人员
        /// </summary>
        public string RecordPle
        {
            get { return recordPle; }
            set { recordPle = value; }
        }

        private string equipment;
        /// <summary>
        /// 设备编号
        /// </summary>
        public string Equipment
        {
            get { return equipment; }
            set { equipment = value; }
        }

        private DateTime recordDate;
        /// <summary>
        /// 记录时间
        /// </summary>
        public DateTime RecordDate
        {
            get { return recordDate; }
            set { recordDate = value; }
        }

        private decimal temperature;
        /// <summary>
        /// 温度
        /// </summary>
        public decimal Temperature
        {
            get { return temperature; }
            set { temperature = value; }
        }

        private decimal humidity;
        /// <summary>
        /// 湿度
        /// </summary>
        public decimal Humidity
        {
            get { return humidity; }
            set { humidity = value; }
        }

        private string remark;
        /// <summary>
        /// 备注
        /// </summary>
        public string Remark
        {
            get { return remark; }
            set { remark = value; }
        }

        private int dataFlag;
        /// <summary>
        /// 数据类型：0手动录入，1设备同步
        /// </summary>
        public int DataFlag
        {
            get { return dataFlag; }
            set { dataFlag = value; }
        }
    }
}
