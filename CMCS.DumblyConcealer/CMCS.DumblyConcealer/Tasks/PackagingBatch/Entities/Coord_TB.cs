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
    /// 封装归批机接口 - 样桶信息表
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("Coord_TB")]
    public class Coord_TB
    {
        private Int32 _Coord;
        /// <summary>
        /// 位置
        /// </summary>
        [CMCS.DapperDber.Attrs.DapperPrimaryKey]
        public Int32 Coord
        {
            get { return _Coord; }
            set { _Coord = value; }
        }

        private String _SampleCode;
        /// <summary>
        /// 样桶码
        /// </summary>
        public String SampleCode
        {
            get { return _SampleCode; }
            set { _SampleCode = value; }
        }

        private Int32 _State;
        /// <summary>
        /// 样桶状态  1为有桶0无桶
        /// </summary>
        public Int32 State
        {
            get { return _State; }
            set { _State = value; }
        }

        private DateTime _DateTime;
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime DateTime
        {
            get { return _DateTime; }
            set { _DateTime = value; }
        }
    }
}
