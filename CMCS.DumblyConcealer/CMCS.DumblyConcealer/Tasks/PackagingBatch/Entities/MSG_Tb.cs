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
    /// 封装归批机接口 - 设备运行记录表
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("MSG_Tb")]
    public class MSG_Tb
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

        private String _Msg;
        /// <summary>
        /// 信息
        /// </summary>
        public String Msg
        {
            get { return _Msg; }
            set { _Msg = value; }
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
