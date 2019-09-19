using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.Entities.Sys;

namespace CMCS.DumblyConcealer.Tasks.PneumaticTransfer.Entities
{
    /// <summary>
    /// 气动命令表
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("InftbQDCSCmd")]
    public class InfQDCSCmd : EntityBase1
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

        private String _OpStartIP;
        /// <summary>
        /// 起始站IP
        /// </summary>
        public String OpStartIP
        {
            get { return _OpStartIP; }
            set { _OpStartIP = value; }
        }

        private String _OpEndIP;
        /// <summary>
        /// 目的站IP
        /// </summary>
        public String OpEndIP
        {
            get { return _OpEndIP; }
            set { _OpEndIP = value; }
        }

        private String _OpStart;
        /// <summary>
        /// 起始站名称
        /// </summary>
        public String OpStart
        {
            get { return _OpStart; }
            set { _OpStart = value; }
        }

        private String _OpEnd;
        /// <summary>
        /// 目的站名称
        /// </summary>
        public String OpEnd
        {
            get { return _OpEnd; }
            set { _OpEnd = value; }
        }

        private String _ResultCode;
        /// <summary>
        /// 结果
        /// </summary>
        public String ResultCode
        {
            get { return _ResultCode; }
            set { _ResultCode = value; }
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
