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
    public class Trans_Cmd_Tb2
    {
        private Int32 _Id;
        /// <summary>
        /// Id
        /// </summary>
        [CMCS.DapperDber.Attrs.DapperPrimaryKey, CMCS.DapperDber.Attrs.DapperPrimaryKeyAdd]
        public Int32 Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        private String _MachineCode;
        /// <summary>
        /// 总体设备编号 
        /// </summary>
        public String MachineCode
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

        private Int32 _Cmdtype;
        /// <summary>
        /// 命令传送方式
        /// </summary>
        public Int32 Cmdtype
        {
            get { return _Cmdtype; }
            set { _Cmdtype = value; }
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

        private String _OriginStationCode;
        /// <summary>
        /// 命令传送发送站编码
        /// </summary>
        public String OriginStationCode
        {
            get { return _OriginStationCode; }
            set { _OriginStationCode = value; }
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

        private String _DestinationStationCode;
        /// <summary>
        /// 命令传送接收站编码
        /// </summary>
        public String DestinationStationCode
        {
            get { return _DestinationStationCode; }
            set { _DestinationStationCode = value; }
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
