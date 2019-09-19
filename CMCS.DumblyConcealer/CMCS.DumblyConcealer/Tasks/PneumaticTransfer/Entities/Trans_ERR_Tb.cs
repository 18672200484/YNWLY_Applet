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
    ///故障信息表
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("Trans_ERR_Tb")]
    public class Trans_ERR_Tb
    {
        private Int32 _Id;
        /// <summary>
        /// Id
        /// </summary>
        [CMCS.DapperDber.Attrs.DapperPrimaryKey]
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

        private String _ErrorCode;
        /// <summary>
        /// 详细设备编号
        /// </summary>
        public String ErrorCode
        {
            get { return _ErrorCode; }
            set { _ErrorCode = value; }
        }

        private String _ErrorDec;
        /// <summary>
        /// 详细设备名称
        /// </summary>
        public String ErrorDec
        {
            get { return _ErrorDec; }
            set { _ErrorDec = value; }
        }

        private DateTime _ErrorTime;
        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime ErrorTime
        {
            get { return _ErrorTime; }
            set { _ErrorTime = value; }
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
