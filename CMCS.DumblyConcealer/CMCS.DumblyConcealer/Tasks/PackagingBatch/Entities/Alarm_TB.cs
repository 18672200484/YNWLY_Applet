using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.Entities;
using CMCS.Common.Entities.Sys;
using System.ComponentModel;

namespace CMCS.DumblyConcealer.Tasks.AutoMaker.Entities
{
    /// <summary>
    /// 封装归批机接口 - 故障记录表
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("Alarm_TB")]
    public class Alarm_TB
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

        private String _Error_Record;
        /// <summary>
        /// 故障信息
        /// </summary>
        public String Error_Record
        {
            get { return _Error_Record; }
            set { _Error_Record = value; }
        }

        private String _GroupName;
        /// <summary>
        /// 设备
        /// </summary>
        public String GroupName
        {
            get { return _GroupName; }
            set { _GroupName = value; }
        }

        private DateTime _DateTime;
        /// <summary>
        /// 插入时间
        /// </summary>
        public DateTime DateTime
        {
            get { return _DateTime; }
            set { _DateTime = value; }
        }
    }
}
