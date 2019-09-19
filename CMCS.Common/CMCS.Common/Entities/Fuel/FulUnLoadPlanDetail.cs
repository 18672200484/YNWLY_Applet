// 此代码由 NhGenerator v1.0.9.0 工具生成。

using System;
using System.Collections;
using CMCS.Common.Entities.Sys;

namespace CMCS.Common.Entities.Fuel
{
    /// <summary>
    /// 卸煤计划明细
    /// </summary>
    [Serializable]
    [CMCS.DapperDber.Attrs.DapperBind("fultbunloadareaplandetail")]
    public class FulUnLoadPlanDetail : EntityBase1
    {
        private String _UnLoadAreaPlanId;
        /// <summary>
        /// 卸煤计划主记录ID
        /// </summary>
        public virtual String UnLoadAreaPlanId { get { return _UnLoadAreaPlanId; } set { _UnLoadAreaPlanId = value; } }

        private String _LMYBDetailId;
        /// <summary>
        /// 来煤预报明细Id
        /// </summary>
        public virtual String LMYBDetailId { get { return _LMYBDetailId; } set { _LMYBDetailId = value; } }

        private String _AutoTruckId;
        /// <summary>
        /// 车辆ID
        /// </summary>
        public virtual String AutoTruckId { get { return _AutoTruckId; } set { _AutoTruckId = value; } }

        private String _CarNumber;
        /// <summary>
        /// 车号
        /// </summary>
        public virtual String CarNumber { get { return _CarNumber; } set { _CarNumber = value; } }

        private String _UnLoadArea;
        /// <summary>
        /// 卸煤区域
        /// </summary>
        public virtual String UnLoadArea { get { return _UnLoadArea; } set { _UnLoadArea = value; } }

        private String _UnLoadTime;
        /// <summary>
        /// 卸煤时间
        /// </summary>
        public virtual String UnLoadTime { get { return _UnLoadTime; } set { _UnLoadTime = value; } }

        private String _IsUnLoad;
        /// <summary>
        /// 是否卸煤
        /// </summary>
        public virtual String IsUnLoad { get { return _IsUnLoad; } set { _IsUnLoad = value; } }

        private String _OrderNo;
        /// <summary>
        /// 顺序
        /// </summary>
        public virtual String OrderNo { get { return _OrderNo; } set { _OrderNo = value; } }

        private String _Remark;
        /// <summary>
        /// 备注
        /// </summary>
        public virtual String Remark { get { return _Remark; } set { _Remark = value; } }

        /// <summary>
        /// 预报明细
        /// </summary>
        [CMCS.DapperDber.Attrs.DapperIgnore]
        public CmcsLMYBDetail TheLMYBDetail
        {
            get { return Dbers.GetInstance().SelfDber.Get<CmcsLMYBDetail>(this.LMYBDetailId); }
        }
    }
}
