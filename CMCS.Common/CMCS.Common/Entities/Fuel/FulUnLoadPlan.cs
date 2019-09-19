// 此代码由 NhGenerator v1.0.9.0 工具生成。

using System;
using System.Collections;
using CMCS.Common.Entities.Sys;
using CMCS.Common.Entities.BaseInfo;

namespace CMCS.Common.Entities.Fuel
{
    /// <summary>
    /// 卸煤计划
    /// </summary>
    [Serializable]
    [CMCS.DapperDber.Attrs.DapperBind("fultbunloadareaplan")]
    public class FulUnLoadPlan : EntityBase1
    {
        private string _LMYBId;
        /// <summary>
        /// 预报ID
        /// </summary>
        public virtual string LMYBId { get { return _LMYBId; } set { _LMYBId = value; } }

        private CmcsLMYB _TheLMYB;
        /// <summary>
        /// 来煤预报
        /// </summary>
        [CMCS.DapperDber.Attrs.DapperIgnore]
        public CmcsLMYB TheLMYB
        {
            get { return Dbers.GetInstance().SelfDber.Get<CmcsLMYB>(this.LMYBId); }
        }

    }
}
