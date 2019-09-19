using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.Entities;
using CMCS.Common.Entities.Sys;

namespace CMCS.DumblyConcealer.Tasks.AutoMaker.Entities
{
    /// <summary>
    /// 汽车制样机接口 - 故障信息表
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("ZY_ERR_Tb")]
    public class ZY_Error_Tb
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

        private String _Errorcode;
        /// <summary>
        /// 故障代码
        /// </summary>
        public String Errorcode
        {
            get { return _Errorcode; }
            set { _Errorcode = value; }
        }

        private DateTime _ErrorTime;
        /// <summary>
        /// 故障日期时间
        /// </summary>
        [CMCS.DapperDber.Attrs.DapperPrimaryKey]
        public DateTime ErrorTime
        {
            get { return _ErrorTime; }
            set { _ErrorTime = value; }
        }

        private String _ErrorDec;
        /// <summary>
        /// 故障信息
        /// </summary>
        public String ErrorDec
        {
            get { return _ErrorDec; }
            set { _ErrorDec = value; }
        }

        private Int32 _DataStatus;
        /// <summary>
        /// 数据发送状态 0：未读取；1：已读取（接口读取，读完写1）
        /// </summary>
        public Int32 DataStatus
        {
            get { return _DataStatus; }
            set { _DataStatus = value; }
        }
    }
}
