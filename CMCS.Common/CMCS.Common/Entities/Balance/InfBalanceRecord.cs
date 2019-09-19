using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.Entities.Sys;

namespace CMCS.Common.Entities.Balance
{
    /// <summary>
    /// 天平数据录入主表
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("InfTbBalanceRecord")]
    public class InfBalanceRecord : EntityBase1
    {
        private String _MachineCode;
        /// <summary>
        /// 总体设备编号 
        /// </summary>
        public String MachineCode
        {
            get { return _MachineCode; }
            set { _MachineCode = value; }
        }

        private String _AssayCode;
        /// <summary>
        /// 化验码
        /// </summary>
        public String AssayCode
        {
            get { return _AssayCode; }
            set { _AssayCode = value; }
        }

        private String _AssayType;
        /// <summary>
        /// 化验类型
        /// </summary>
        public String AssayType
        {
            get { return _AssayType; }
            set { _AssayType = value; }
        }

        /// <summary>
        /// 坩埚号
        /// </summary>
        public string GGCode { get; set; }

        /// <summary>
        /// 重量
        /// </summary>
        public double Weight { get; set; }

        private Int32 _SyncFlag;
        /// <summary>
        /// 同步标识
        /// </summary>
        public Int32 SyncFlag
        {
            get { return _SyncFlag; }
            set { _SyncFlag = value; }
        }
    }
}
