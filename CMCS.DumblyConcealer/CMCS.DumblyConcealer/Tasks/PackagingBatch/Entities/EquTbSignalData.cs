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
    /// 封装归批机接口 - 实时信号表
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("EquTbSignalData")]
    public class EquTbSignalData
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

        private String _TagName;
        /// <summary>
        /// 信号名称
        /// </summary>
        public String TagName
        {
            get { return _TagName; }
            set { _TagName = value; }
        }

        private String _TagValue;
        /// <summary>
        /// 信号值
        /// </summary>
        public String TagValue
        {
            get { return _TagValue; }
            set { _TagValue = value; }
        }

        private DateTime _UpdateTime;
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime
        {
            get { return _UpdateTime; }
            set { _UpdateTime = value; }
        }

        private Int32 _DataFlag;
        /// <summary>
        /// 更新标识
        /// </summary>
        public Int32 DataFlag
        {
            get { return _DataFlag; }
            set { _DataFlag = value; }
        }
    }
}
