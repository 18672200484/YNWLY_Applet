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
    [CMCS.DapperDber.Attrs.DapperBind("Trans_Cmd_Tb")]
    public class EquTbQDCSCmd : EntityBase1
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

        private String _OpStart;
        /// <summary>
        /// 起始站
        /// </summary>
        public String OpStart
        {
            get { return _OpStart; }
            set { _OpStart = value; }
        }

        private String _OpEnd;
        /// <summary>
        /// 操作人员
        /// </summary>
        public String OpEnd
        {
            get { return _OpEnd; }
            set { _OpEnd = value; }
        }

        private String _OperUser;
        /// <summary>
        /// 目的站
        /// </summary>
        public String OperUser
        {
            get { return _OperUser; }
            set { _OperUser = value; }
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
