using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.Entities.Sys;

namespace CMCS.Common.Entities.TemperatureHumidity
{
    /// <summary>
    /// 煤场测温仪
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("STGTBSTORAGETEMPERATURE")]
    public class STGStoreageTemperature : EntityBase1
    {
        private string unitName;
        /// <summary>
        /// 煤场分区名称
        /// </summary>
        public string UnitName
        {
            get { return unitName; }
            set { unitName = value; }
        }

        private string poleCode;
        /// <summary>
        /// 测温杆编号
        /// </summary>
        public string PoleCode
        {
            get { return poleCode; }
            set { poleCode = value; }
        }

        private decimal pointX;
        /// <summary>
        /// X坐标
        /// </summary>
        public decimal PointX
        {
            get { return pointX; }
            set { pointX = value; }
        }

        private decimal pointY;
        /// <summary>
        /// Y坐标
        /// </summary>
        public decimal PointY
        {
            get { return pointY; }
            set { pointY = value; }
        }

        private string temperature;
        /// <summary>
        /// 温度
        /// </summary>
        public string Temperature
        {
            get { return temperature; }
            set { temperature = value; }
        }
    }
}
