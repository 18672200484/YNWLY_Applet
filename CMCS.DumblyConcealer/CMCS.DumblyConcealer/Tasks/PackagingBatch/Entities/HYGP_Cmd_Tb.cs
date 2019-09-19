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
    /// 封装归批机接口 - 燃管对接命令表
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("HYGP_Cmd_Tb")]
    public class HYGP_Cmd_Tb
    {
        private Int32 _Id;
        /// <summary>
        /// Id
        /// </summary>
        [CMCS.DapperDber.Attrs.DapperPrimaryKey]
        [CMCS.DapperDber.Attrs.DapperPrimaryKeyAdd]
        public Int32 Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        private String _SampleCode;
        /// <summary>
        /// 样桶倒料编码
        /// </summary>
        public String SampleCode
        {
            get { return _SampleCode; }
            set { _SampleCode = value; }
        }

        private Int32 _Command;
        /// <summary>
        /// 燃管命令  1为倒料
        /// </summary>
        public Int32 Command
        {
            get { return _Command; }
            set { _Command = value; }
        }

        private Int32 _DataStatus;
        /// <summary>
        /// 命令特征字  0为管控插入；1为归批机已读取并执行此命令；11为归批机完成此命令，且样瓶全部正常处理；12为归批机完成此命令，有样桶读卡失败；
        /// </summary>
        public Int32 DataStatus
        {
            get { return _DataStatus; }
            set { _DataStatus = value; }
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
