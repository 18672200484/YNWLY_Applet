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
    [CMCS.DapperDber.Attrs.DapperBind("STGTBFUELSTORAGEAREA")]
    public class StorageArea : EntityBase1
    {
        private string _AreaName;
        /// <summary>
        /// 煤场分区名称
        /// </summary>
        public string AreaName
        {
            get { return _AreaName; }
            set { _AreaName = value; }
        }

        private string _StartPoint;
        /// <summary>
        /// 开始节点
        /// </summary>
        public string StartPoint
        {
            get { return _StartPoint; }
            set { _StartPoint = value; }
        }

        private decimal _EndPoint;
        /// <summary>
        /// 结束节点
        /// </summary>
        public decimal EndPoint
        {
            get { return _EndPoint; }
            set { _EndPoint = value; }
        }

        private string _FuelStorageId;
        /// <summary>
        /// 煤场id
        /// </summary>
        public string FuelStorageId
        {
            get { return _FuelStorageId; }
            set { _FuelStorageId = value; }
        }

        
    }
}
