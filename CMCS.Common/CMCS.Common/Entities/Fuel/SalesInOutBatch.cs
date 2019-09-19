using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.Entities.Sys;

namespace CMCS.Common.Entities.Fuel
{
    /// <summary>
    /// 销售煤批次表
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("FULTBSALESINOUTBATCH")]
    public class SalesInOutBatch : EntityBase1
    {
        private String _Batch;
        /// <summary>
        /// 批次号
        /// </summary>
        public virtual String Batch { get { return _Batch; } set { _Batch = value; } }

        private String _InCoalCode;
        /// <summary>
        /// 自编码
        /// </summary>
        public virtual String InCoalCode { get { return _InCoalCode; } set { _InCoalCode = value; } }

        private String _LmybId;
        /// <summary>
        /// 调运计划
        /// </summary>
        public virtual String LmybId { get { return _LmybId; } set { _LmybId = value; } }

        private String _CoalProductId;
        /// <summary>
        /// 配煤入库
        /// </summary>
        public virtual String CoalProductId { get { return _CoalProductId; } set { _CoalProductId = value; } }

        private String _ConsigneeId;
        /// <summary>
        /// 收货单位id
        /// </summary>
        public virtual String ConsigneeId { get { return _ConsigneeId; } set { _ConsigneeId = value; } }


        private String _TransportCompayId;
        /// <summary>
        /// 运输单位id
        /// </summary>
        public virtual String TransportCompayId { get { return _TransportCompayId; } set { _TransportCompayId = value; } }


        private DateTime _DispatchDate;
        /// <summary>
        /// 发货时间
        /// </summary>
        public virtual DateTime DispatchDate { get { return _DispatchDate; } set { _DispatchDate = value; } }

        private DateTime _PlanArriveDate;
        /// <summary>
        /// 预计到达时间
        /// </summary>
        public virtual DateTime PlanArriveDate { get { return _PlanArriveDate; } set { _PlanArriveDate = value; } }

        private DateTime _FactArriveDate;
        /// <summary>
        /// 实际到达时间
        /// </summary>
        public virtual DateTime FactArriveDate { get { return _FactArriveDate; } set { _FactArriveDate = value; } }

        private DateTime _BackBatchDate;
        /// <summary>
        /// 归批时间
        /// </summary>
        public virtual DateTime BackBatchDate { get { return _BackBatchDate; } set { _BackBatchDate = value; } }

        private Int32 _TransportNumber;
        /// <summary>
        /// 总车数
        /// </summary>
        public virtual Int32 TransportNumber { get { return _TransportNumber; } set { _TransportNumber = value; } }

        private Decimal _SuttleWeight;
        /// <summary>
        /// 总重量(吨)
        /// </summary>
        public virtual Decimal SuttleWeight { get { return _SuttleWeight; } set { _SuttleWeight = value; } }

        private String _Runner;
        /// <summary>
        /// 接车人员
        /// </summary>
        public virtual String Runner { get { return _Runner; } set { _Runner = value; } }

        private DateTime _RunDate;
        /// <summary>
        /// 接车时间
        /// </summary>
        public virtual DateTime RunDate { get { return _RunDate; } set { _RunDate = value; } }

        private String _AuditingUserAccount;
        /// <summary>
        /// 审批人
        /// </summary>
        public virtual String AuditingUserAccount { get { return _AuditingUserAccount; } set { _AuditingUserAccount = value; } }

        private DateTime _AuditingDate;
        /// <summary>
        /// 审批时间
        /// </summary>
        public virtual DateTime AuditingDate { get { return _AuditingDate; } set { _AuditingDate = value; } }

        private Int32 _IsQty;
        /// <summary>
        /// 状态 0：初始 1：计量审核结束
        /// </summary>
        public virtual Int32 IsQty { get { return _IsQty; } set { _IsQty = value; } }

        private String _IsFinishFill;
        /// <summary>
        /// 是否上煤完毕
        /// </summary>
        public virtual String IsFinishFill { get { return _IsFinishFill; } set { _IsFinishFill = value; } }

        private Int32 _IsAdjusted;
        /// <summary>
        /// 1,0没生成数据 或 需要更新
        /// </summary>
        public virtual Int32 IsAdjusted { get { return _IsAdjusted; } set { _IsAdjusted = value; } }

        private String _OrderId;
        /// <summary>
        /// 订单
        /// </summary>
        public virtual String OrderId { get { return _OrderId; } set { _OrderId = value; } }

        private String _Remark;
        /// <summary>
        /// 备注
        /// </summary>
        public virtual String Remark { get { return _Remark; } set { _Remark = value; } }

        private Int32 _IsRemove;
        /// <summary>
        /// 是否拆分
        /// </summary>
        public virtual Int32 IsRemove { get { return _IsRemove; } set { _IsRemove = value; } }

        private string _ParentRemoveBatchId;
        /// <summary>
        /// 被拆分的批次id
        /// </summary>
        public virtual string ParentRemoveBatchId { get { return _ParentRemoveBatchId; } set { _ParentRemoveBatchId = value; } }

        private Int32 _IsUpload;
        /// <summary>
        /// 是否上传
        /// </summary>
        public virtual Int32 IsUpload { get { return _IsUpload; } set { _IsUpload = value; } }

        private String _DataFrom;
        /// <summary>
        /// 电厂信息
        /// </summary>
        public virtual String DataFrom { get { return _DataFrom; } set { _DataFrom = value; } }

        private Int32 _IsSign;
        /// <summary>
        /// 是否审核
        /// </summary>
        public virtual Int32 IsSign { get { return _IsSign; } set { _IsSign = value; } }

        private String _OutFactoryType;
        /// <summary>
        /// 出厂类型
        /// </summary>
        public virtual String OutFactoryType { get { return _OutFactoryType; } set { _OutFactoryType = value; } }

    }
}
