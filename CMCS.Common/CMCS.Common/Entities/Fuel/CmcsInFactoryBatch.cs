using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.Entities.Sys;
using CMCS.Common.Entities.BaseInfo;

namespace CMCS.Common.Entities.Fuel
{
    /// <summary>
    /// 入厂煤批次表
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("FULTBINFACTORYBATCH")]
    public class CmcsInFactoryBatch : EntityBase1
    {
        /// <summary>
        /// 是否加权审核
        /// </summary>
        public virtual int IsScale { get; set; }

        /// <summary>
        /// 入厂批次号
        /// </summary>
        public virtual String Batch { get; set; }

        /// <summary>
        /// 实际到达时间
        /// </summary>
        public virtual DateTime FactArriveDate { get; set; }

        /// <summary>
        /// 接车人员
        /// </summary>
        public virtual String Runner { get; set; }

        /// <summary>
        /// 接车时间
        /// </summary>
        public virtual DateTime RunDate { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public virtual String Remark { get; set; }

        /// <summary>
        /// 量审核
        /// </summary>
        public virtual Int32 IsCheck { get; set; }

        /// <summary>
        /// 化验审核
        /// </summary>
        public virtual Int32 IsFinish { get; set; }

        /// <summary>
        /// 批次类型 汽车、火车、船运
        /// </summary>
        public virtual String BatchType { get; set; }


        /// <summary>
        /// 运输类型 汽车、火车、船运
        /// </summary>
        public virtual String TransportTypeName { get; set; }


        /// <summary>
        /// 运输类型 汽车、火车、船运
        /// </summary>
        public virtual String TransportTypeId { get; set; }

        /// <summary>
        /// 父批次
        /// </summary>
        public virtual string ParentId { get; set; }

        /// <summary>
        /// 供煤单位
        /// </summary>
        public virtual string SupplierId { get; set; }

        /// <summary>
        /// 供煤单位
        /// </summary>
        [CMCS.DapperDber.Attrs.DapperIgnore]
        public CmcsSupplier TheSupplier
        {
            get { return Dbers.GetInstance().SelfDber.Get<CmcsSupplier>(this.SupplierId); }
        }

        /// <summary>
        /// 托运单位
        /// </summary>
        public virtual string SentSupplierId { get; set; }

        /// <summary>
        /// 发货单位
        /// </summary>
        public virtual string SendSupplierId { get; set; }

        /// <summary>
        /// 发站 多对一
        /// </summary>
        public virtual string StationId { get; set; }

        /// <summary>
        /// 矿点 多对一
        /// </summary>
        public virtual string MineId { get; set; }

        /// <summary>
        /// 矿点
        /// </summary>
        [CMCS.DapperDber.Attrs.DapperIgnore]
        public CmcsMine TheMine
        {
            get { return Dbers.GetInstance().SelfDber.Get<CmcsMine>(this.MineId); }
        }

        /// <summary>
        /// 关联：煤种
        /// </summary>
        public virtual string FuelKindId { get; set; }

        /// <summary>
        /// 煤种名称
        /// </summary>
        public virtual string FuelKindName { get; set; }

        /// <summary>
        /// 煤种
        /// </summary>
        [CMCS.DapperDber.Attrs.DapperIgnore]
        public CmcsFuelKind TheFuelKind
        {
            get { return Dbers.GetInstance().SelfDber.Get<CmcsFuelKind>(this.FuelKindId); }
        }

        /// <summary>
        /// 关联：运输单位
        /// </summary>
        public virtual string TransportCompanyId { get; set; }

        /// <summary>
        /// 是否汽车智能化创建
        /// </summary>
        public virtual Int32 IsCTAutoCreate { get; set; }

        /// <summary>
        /// 归批时间
        /// </summary>
        public virtual DateTime BACKBATCHDATE { get; set; }


        /// <summary>
        /// 入厂煤类型 入厂煤  仓储煤
        /// </summary>
        public virtual string InFactoryType { get; set; }

        /// <summary>
        /// 仓储煤销售发货单位
        /// </summary>
        public virtual string StoSendUnitName { get; set; }

        /// <summary>
        /// 收货单位id
        /// </summary>
        public virtual string FuelSupplierId { get; set; }

        /// <summary>
        /// 收货单位
        /// </summary>
        [CMCS.DapperDber.Attrs.DapperIgnore]
        public CmcsSupplier TheFuelSupplier
        {
            get { return Dbers.GetInstance().SelfDber.Get<CmcsSupplier>(this.FuelSupplierId); }
        }

        /// <summary>
        /// 销售订单Id
        /// </summary>
        public virtual string SalesOrderId { get; set; }

        #region 非自能化冗余字段

        private DateTime _PlanArriveDate;
        /// <summary>
        /// 通知到达时间
        /// </summary>
        public virtual DateTime PlanArriveDate { get { return _PlanArriveDate; } set { _PlanArriveDate = value; } }

        /// <summary>
        /// 票重
        /// </summary>
        public virtual Decimal Ticketqty { get; set; }
        /// <summary>
        /// 净重
        /// </summary>
        public virtual Decimal Suttleweight { get; set; }
        /// <summary>
        /// 验收量
        /// </summary>
        public virtual Decimal Checkqty { get; set; }
        /// <summary>
        /// 盈亏量
        /// </summary>
        public virtual Decimal Marginqty { get; set; }

        /// <summary>
        /// 扣矸
        /// </summary>
        public virtual Decimal KGWEIGHT { get; set; }

        /// <summary>
        /// 扣水
        /// </summary>
        public virtual Decimal KSWEIGHT { get; set; }

        /// <summary>
        /// 车辆数
        /// </summary>
        public virtual Decimal TRANSPORTNUMBER { get; set; }

        private Decimal _QCal;
        /// <summary>
        /// 预估煤质-低位热值
        /// </summary>
        public virtual Decimal QCal { get { return _QCal; } set { _QCal = value; } }

        private Decimal _Stad;
        /// <summary>
        /// 预估煤质-空气干燥基硫分
        /// </summary>
        public virtual Decimal Stad { get { return _Stad; } set { _Stad = value; } }

        private Decimal _Vad;
        /// <summary>
        /// 预估煤质-空气干燥基挥发分
        /// </summary>
        public virtual Decimal Vad { get { return _Vad; } set { _Vad = value; } }

        private Decimal _Mt;
        /// <summary>
        /// 预估煤质-全水
        /// </summary>
        public virtual Decimal Mt { get { return _Mt; } set { _Mt = value; } }

        /// <summary>
        /// 来煤预报Id
        /// </summary>
        public virtual String LMYBID { get; set; }

        private string isSynch = "0";
        /// <summary>
        /// 同步标识
        /// </summary>
        public string IsSynch
        {
            get { return isSynch; }
            set { isSynch = value; }
        }
    
        #endregion
    }
}
