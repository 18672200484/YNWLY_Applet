// 此代码由 NhGenerator v1.0.9.0 工具生成。

using System;
using System.Collections;
using CMCS.Common.Entities.Sys;

namespace CMCS.Common.Entities.CarTransport
{
    /// <summary>
    /// 汽车智能化-违章信息
    /// </summary>
    [Serializable]
    [CMCS.DapperDber.Attrs.DapperBind("CmcsTbBreakRules")]
    public class CmcsBreakRules : EntityBase1
    {
        private String _TransportId;
        /// <summary>
        /// 关联 运输记录ID
        /// </summary>
        public virtual String TransportId { get { return _TransportId; } set { _TransportId = value; } }

        private String _TransportType;
        /// <summary>
        /// 运输记录类型
        /// </summary>
        public virtual String TransportType { get { return _TransportType; } set { _TransportType = value; } }

        private String _BreakRulesType;
        /// <summary>
        /// 违章类型
        /// </summary>
        public virtual String BreakRulesType { get { return _BreakRulesType; } set { _BreakRulesType = value; } }

        private string _BreakRulesResult;
        /// <summary>
        /// 违章处理结果
        /// </summary>
        public virtual string BreakRulesResult { get { return _BreakRulesResult; } set { _BreakRulesResult = value; } }

        private string _Operater;
        /// <summary>
        /// 操作人
        /// </summary>
        public virtual string Operater { get { return _Operater; } set { _Operater = value; } }

    }
}
