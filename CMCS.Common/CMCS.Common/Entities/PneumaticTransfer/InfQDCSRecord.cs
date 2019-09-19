using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.Entities.Sys;

namespace CMCS.DumblyConcealer.Tasks.PneumaticTransfer.Entities
{
    /// <summary>
    /// 气动传输记录表
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("InftbQDCSRecord")]
    public class InfQDCSRecord : EntityBase1
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

        private String _MakeCode;
        /// <summary>
        /// 制样码
        /// </summary>
        public String MakeCode
        {
            get { return _MakeCode; }
            set { _MakeCode = value; }
        }

        private String _SendType;
        /// <summary>
        /// 传送方式
        /// </summary>
        public String SendType
        {
            get { return _SendType; }
            set { _SendType = value; }
        }

        private String _SampleType;
        /// <summary>
        /// 煤样类型
        /// </summary>
        public String SampleType
        {
            get { return _SampleType; }
            set { _SampleType = value; }
        }

        private DateTime _StartTime;
        /// <summary>
        /// 传输开始时间
        /// </summary>
        public DateTime StartTime
        {
            get { return _StartTime; }
            set { _StartTime = value; }
        }

        private DateTime _EndTime;
        /// <summary>
        /// 传输完成时间
        /// </summary>
        public DateTime EndTime
        {
            get { return _EndTime; }
            set { _EndTime = value; }
        }

        private String _Oper;
        /// <summary>
        /// 操作人员
        /// </summary>
        public String Oper
        {
            get { return _Oper; }
            set { _Oper = value; }
        }

        private Int32 _DataFlag;
        /// <summary>
        /// 标识符
        /// </summary>
        public Int32 DataFlag
        {
            get { return _DataFlag; }
            set { _DataFlag = value; }
        }
    }
}
