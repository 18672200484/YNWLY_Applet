using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.Entities;
using CMCS.Common.Entities.Sys;
using System.ComponentModel;

namespace CMCS.DumblyConcealer.Tasks.PneumaticTransfer.Entities
{
    /// <summary>
    ///控制信息表
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("Trans_Cmd_Tb")]
    public class Trans_Cmd_Tb
    {
        private String _Transno;
        /// <summary>
        /// Id
        /// </summary>
        [CMCS.DapperDber.Attrs.DapperPrimaryKey]
        public String Transno
        {
            get { return _Transno; }
            set { _Transno = value; }
        }

        private Int32 _MachineCode;
        /// <summary>
        /// 总体设备编号 
        /// </summary>
        public Int32 MachineCode
        {
            get { return _MachineCode; }
            set { _MachineCode = value; }
        }

        private Int32 _TransPriority;
        /// <summary>
        /// 命令传送优先级
        /// </summary>
        public Int32 TransPriority
        {
            get { return _TransPriority; }
            set { _TransPriority = value; }
        }

        private String _TransPackCode;
        /// <summary>
        /// 命令传送封装码
        /// </summary>
        public String TransPackCode
        {
            get { return _TransPackCode; }
            set { _TransPackCode = value; }
        }

        private Int32 _CommandCode;
        /// <summary>
        /// 命令代码
        /// </summary>
        public Int32 CommandCode
        {
            get { return _CommandCode; }
            set { _CommandCode = value; }
        }

        private String _CmdOriginStation;
        /// <summary>
        /// 命令传送发送站
        /// </summary>
        public String CmdOriginStation
        {
            get { return _CmdOriginStation; }
            set { _CmdOriginStation = value; }
        }

        private String _CmdDestinationStation;
        /// <summary>
        /// 命令传送接收站
        /// </summary>
        public String CmdDestinationStation
        {
            get { return _CmdDestinationStation; }
            set { _CmdDestinationStation = value; }
        }

        private String _SampleId;
        /// <summary>
        /// 制样码
        /// </summary>
        public String SampleId
        {
            get { return _SampleId; }
            set { _SampleId = value; }
        }

        private DateTime _SendCommandTime;
        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime SendCommandTime
        {
            get { return _SendCommandTime; }
            set { _SendCommandTime = value; }
        }

        private Int32 _DataStatus;
        /// <summary>
        /// 数据发送状态
        /// </summary>
        public Int32 DataStatus
        {
            get { return _DataStatus; }
            set { _DataStatus = value; }
        }
    }
}
