using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.Entities.Sys;

namespace CMCS.Common.Entities.Storage
{
    /// <summary>
    /// 煤场测温杆
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("STGTBSTORAGETEMPERATURE")]
    public class StorageTemperature : EntityBase1
    {
        private string _UnitName;
        /// <summary>
        /// 煤场分区名称
        /// </summary>
        public string UnitName
        {
            get { return _UnitName; }
            set { _UnitName = value; }
        }

        private string _PoleCode;
        /// <summary>
        /// 测温杆编号
        /// </summary>
        public string PoleCode
        {
            get { return _PoleCode; }
            set { _PoleCode = value; }
        }

        private decimal _PointX;
        /// <summary>
        /// X
        /// </summary>
        public decimal PointX
        {
            get { return _PointX; }
            set { _PointX = value; }
        }

        private decimal _PointY;
        /// <summary>
        /// Y
        /// </summary>
        public decimal PointY
        {
            get { return _PointY; }
            set { _PointY = value; }
        }

        private decimal _Temperature;
        /// <summary>
        /// 实时温度
        /// </summary>
        public decimal Temperature
        {
            get { return _Temperature; }
            set { _Temperature = value; }
        }
    }
}
