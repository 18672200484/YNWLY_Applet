using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.Entities;
using CMCS.Common.Entities.Sys;
using System.ComponentModel;

namespace CMCS.DumblyConcealer.Tasks.CarJXSampler.Entities
{
    /// <summary>
    /// 汽车机械采样机接口 - 采样机基本状态表
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("KY_CYJ_Status")]
    public class KY_CYJ_Status
    {
        private String _CYJ_Machine;
        /// <summary>
        /// 采样机号
        /// </summary>
        public String CYJ_Machine
        {
            get { return _CYJ_Machine; }
            set { _CYJ_Machine = value; }
        }

        private Int32 _CSStatus;
        /// <summary>
        /// 超声波状态 1为超声波被阻档，否则为正常状态
        /// </summary>
        public Int32 CSStatus
        {
            get { return _CSStatus; }
            set { _CSStatus = value; }
        }

        private Int32 _XCSSStatus;
        /// <summary>
        /// 小车超声波状态 1为超声波被阻档，否则为正常状态
        /// </summary>
        public Int32 XCSSStatus
        {
            get { return _XCSSStatus; }
            set { _XCSSStatus = value; }
        }

        private Int32 _CYTStatus;
        /// <summary>
        /// 采样头状态 1为采样头下降状态，否则为上升到位状态
        /// </summary>
        public Int32 CYTStatus
        {
            get { return _CYTStatus; }
            set { _CYTStatus = value; }
        }

        private Int32 _DZStatus;
        /// <summary>
        /// 道闸状态 1为道闸抬起状态，否则为关闭状态
        /// </summary>
        public Int32 DZStatus
        {
            get { return _DZStatus; }
            set { _DZStatus = value; }
        }

        private Int32 _CYJStatus;
        /// <summary>
        /// 采样机就绪状态 1为采样机就绪状态，否则为未就绪状态
        /// </summary>
        public Int32 CYJStatus
        {
            get { return _CYJStatus; }
            set { _CYJStatus = value; }
        }

    }
}
