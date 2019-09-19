// 此代码由 NhGenerator v1.0.9.0 工具生成。

using System;
using System.Collections;
using CMCS.Common.Entities.Sys;
using CMCS.Common.Entities.BaseInfo;

namespace CMCS.Common.Entities.Fuel
{
    /// <summary>
    /// 入厂煤来煤预报
    /// </summary>
    [Serializable]
    [CMCS.DapperDber.Attrs.DapperBind("FultbLMYB")]
    public class CmcsLMYB : EntityBase1
    {
        private string _SupplierName;
        /// <summary>
        /// 供煤单位
        /// </summary>
        public virtual string SupplierName { get { return _SupplierName; } set { _SupplierName = value; } }

        private string _SupplierId;
        /// <summary>
        /// 供煤单位
        /// </summary>
        public virtual string SupplierId { get { return _SupplierId; } set { _SupplierId = value; } }

        private string _TransportCompanyName;
        /// <summary>
        /// 运输单位
        /// </summary>
        public virtual string TransportCompanyName { get { return _TransportCompanyName; } set { _TransportCompanyName = value; } }

        private string _TransportCompanyId;
        /// <summary>
        /// 运输单位
        /// </summary>
        public virtual string TransportCompanyId { get { return _TransportCompanyId; } set { _TransportCompanyId = value; } }

        private string _FuelKindName;
        /// <summary>
        /// 煤种
        /// </summary>
        public virtual string FuelKindName { get { return _FuelKindName; } set { _FuelKindName = value; } }

        private string _FuelKindId;
        /// <summary>
        /// 煤种
        /// </summary>
        public virtual string FuelKindId { get { return _FuelKindId; } set { _FuelKindId = value; } }

        private string _MineName;
        /// <summary>
        /// 矿点
        /// </summary>
        public virtual string MineName { get { return _MineName; } set { _MineName = value; } }

        private string _MineId;
        /// <summary>
        /// 矿点
        /// </summary>
        public virtual string MineId { get { return _MineId; } set { _MineId = value; } }

        private String _TransportTypeName;
        /// <summary>
        /// 运输方式
        /// </summary>
        public virtual String TransportTypeName { get { return _TransportTypeName; } set { _TransportTypeName = value; } }

        private String _YbNum;
        /// <summary>
        /// 预报编号
        /// </summary>
        public virtual String YbNum { get { return _YbNum; } set { _YbNum = value; } }

        private String _ZcDate;
        /// <summary>
        /// 装车日期
        /// </summary>
        public virtual String ZcDate { get { return _ZcDate; } set { _ZcDate = value; } }

        private Decimal _TransportNumber;
        /// <summary>
        /// 车数
        /// </summary>
        public virtual Decimal TransportNumber { get { return _TransportNumber; } set { _TransportNumber = value; } }

        private Decimal _CoalNumber;
        /// <summary>
        /// 来煤量
        /// </summary>
        public virtual Decimal CoalNumber { get { return _CoalNumber; } set { _CoalNumber = value; } }

        private DateTime _InFactoryTime;
        /// <summary>
        /// 预计到厂时间
        /// </summary>
        public virtual DateTime InFactoryTime { get { return _InFactoryTime; } set { _InFactoryTime = value; } }

        private String _InFactoryType;
        /// <summary>
        /// 入厂类型
        /// </summary>
        public virtual String InFactoryType { get { return _InFactoryType; } set { _InFactoryType = value; } }

        private String _Remark;
        /// <summary>
        /// 备注
        /// </summary>
        public virtual String Remark { get { return _Remark; } set { _Remark = value; } }

        private Decimal _HistoryYq;
        /// <summary>
        /// 历史热值（Kcal/Kg）
        /// </summary>
        public virtual Decimal HistoryYq { get { return _HistoryYq; } set { _HistoryYq = value; } }

        private Decimal _HistoryYs;
        /// <summary>
        /// 历史硫分(%)
        /// </summary>
        public virtual Decimal HistoryYs { get { return _HistoryYs; } set { _HistoryYs = value; } }

        private Decimal _HistoryYv;
        /// <summary>
        /// 历史挥发分(%)
        /// </summary>
        public virtual Decimal HistoryYv { get { return _HistoryYv; } set { _HistoryYv = value; } }

        private Decimal _Q;
        /// <summary>
        /// 预估热值（Kcal/Kg）
        /// </summary>
        public virtual Decimal Q { get { return _Q; } set { _Q = value; } }

        private Decimal _S;
        /// <summary>
        /// 预估硫分(%)
        /// </summary>
        public virtual Decimal S { get { return _S; } set { _S = value; } }

        private Decimal _V;
        /// <summary>
        /// 预估挥发分(%)
        /// </summary>
        public virtual Decimal V { get { return _V; } set { _V = value; } }

        private string isSynch = "0";
        /// <summary>
        /// 同步标识
        /// </summary>
        public string IsSynch
        {
            get { return isSynch; }
            set { isSynch = value; }
        }

        private String _SalesOrderId;
        /// <summary>
        /// 销售订单Id
        /// </summary>
        public virtual String SalesOrderId { get { return _SalesOrderId; } set { _SalesOrderId = value; } }

        private String _FuelSupplierId;
        /// <summary>
        /// 收货单位Id
        /// </summary>
        public virtual String FuelSupplierId { get { return _FuelSupplierId; } set { _FuelSupplierId = value; } }

        /// <summary>
        /// 收货单位
        /// </summary>
        [CMCS.DapperDber.Attrs.DapperIgnore]
        public CmcsSupplier TheFuelSupplier
        {
            get { return Dbers.GetInstance().SelfDber.Get<CmcsSupplier>(this.FuelSupplierId); }
        }

        /// <summary>
        /// 运输单位
        /// </summary>
        [CMCS.DapperDber.Attrs.DapperIgnore]
        public CmcsTransportCompany TheTransportCompany
        {
            get { return Dbers.GetInstance().SelfDber.Get<CmcsTransportCompany>(this.TransportCompanyId); }
        }

        private Decimal _ProductNumbering;
        /// <summary>
        /// 正在排产量
        /// </summary>
        public virtual Decimal ProductNumbering { get { return _ProductNumbering; } set { _ProductNumbering = value; } }

        private Decimal _ReadyProductNumber;
        /// <summary>
        /// 已排产量
        /// </summary>
        public virtual Decimal ReadyProductNumber { get { return _ReadyProductNumber; } set { _ReadyProductNumber = value; } }

        private Decimal _ActualReadyNumber;
        /// <summary>
        /// 实际已排产
        /// </summary>
        public virtual Decimal ActualReadyNumber { get { return _ActualReadyNumber; } set { _ActualReadyNumber = value; } }

        private Decimal _ReadySalesNumber;
        /// <summary>
        /// 实际已销售
        /// </summary>
        public virtual Decimal ReadySalesNumber { get { return _ReadySalesNumber; } set { _ReadySalesNumber = value; } }

        private String _TransportNo;
        /// <summary>
        /// 运输单号
        /// </summary>
        public virtual String TransportNo { get { return _TransportNo; } set { _TransportNo = value; } }

        private Int32 _HourStart;
        /// <summary>
        /// 装车时段1
        /// </summary>
        public virtual Int32 HourStart { get { return _HourStart; } set { _HourStart = value; } }

        private Int32 _HourEnd;
        /// <summary>
        /// 装车时段2
        /// </summary>
        public virtual Int32 HourEnd { get { return _HourEnd; } set { _HourEnd = value; } }

        /// <summary>
        /// 成品仓ID
        /// </summary>
        public virtual string CPCId { get; set; }

        /// <summary>
        /// 成品仓名称
        /// </summary>
        public virtual string CPCName { get; set; }

        /// <summary>
        /// 煤场区域ID
        /// </summary>
        public virtual string StorageId { get; set; }

        /// <summary>
        /// 煤场区域名称
        /// </summary>
        public virtual string StorageName { get; set; }

        /// <summary>
        /// 采样方式
        /// </summary>
        public virtual string SamplingType { get; set; }
    }
}
