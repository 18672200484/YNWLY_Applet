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
    [CMCS.DapperDber.Attrs.DapperBind("Status")]
    public class Status
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

        private String _SystemStop;
        /// <summary>
        /// 系统状态 0 正常  1 故障
        /// </summary>
        public String SystemStop
        {
            get { return _SystemStop; }
            set { _SystemStop = value; }
        }

        private String _YCMS;
        /// <summary>
        /// 远程模式
        /// </summary>
        public String YCMS
        {
            get { return _YCMS; }
            set { _YCMS = value; }
        }
    }
}
