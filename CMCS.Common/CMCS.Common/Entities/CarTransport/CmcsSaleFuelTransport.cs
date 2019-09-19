// 此代码由 NhGenerator v1.0.9.0 工具生成。

using System;
using System.Collections;
using CMCS.Common.Entities.Sys;
using CMCS.Common.Entities.BaseInfo;
using CMCS.Common.DAO;

namespace CMCS.Common.Entities.CarTransport
{
    /// <summary>
    /// 汽车智能化-出场煤运输记录
    /// </summary>
    [Serializable]
    [CMCS.DapperDber.Attrs.DapperBind("CmcsTbSaleFuelTransport")]
    public class CmcsSaleFuelTransport : EntityBase1
    {
        private String _AutotruckId;
        /// <summary>
        /// 关联：车辆管理
        /// </summary>
        public virtual String AutotruckId { get { return _AutotruckId; } set { _AutotruckId = value; } }

        private String _InOutBatchId;
        /// <summary>
        /// 销售煤批次Id
        /// </summary>
        public virtual String InOutBatchId { get { return _InOutBatchId; } set { _InOutBatchId = value; } }

        private String _SamplingIdd;
        /// <summary>
        /// 采样Id
        /// </summary>
        public virtual String SamplingId { get { return _SamplingIdd; } set { _SamplingIdd = value; } }

        private String _TransportSalesId;
        /// <summary>
        /// 预报Id
        /// </summary>
        public virtual String TransportSalesId { get { return _TransportSalesId; } set { _TransportSalesId = value; } }

        private String _TransportSalesNum;
        /// <summary>
        /// 预报编号
        /// </summary>
        public virtual String TransportSalesNum { get { return _TransportSalesNum; } set { _TransportSalesNum = value; } }

        private string _SerialNumber;
        /// <summary>
        /// 流水号
        /// </summary>
        public virtual string SerialNumber { get { return _SerialNumber; } set { _SerialNumber = value; } }

        private String _CarNumber;
        /// <summary>
        /// 车号
        /// </summary>
        public virtual String CarNumber { get { return _CarNumber; } set { _CarNumber = value; } }

        private Decimal _GrossWeight;
        /// <summary>
        /// 毛重(吨)
        /// </summary>
        public virtual Decimal GrossWeight { get { return _GrossWeight; } set { _GrossWeight = value; } }

        private Decimal _TareWeight;
        /// <summary>
        /// 皮重(吨)
        /// </summary>
        public virtual Decimal TareWeight { get { return _TareWeight; } set { _TareWeight = value; } }

        private Decimal _SuttleWeight;
        /// <summary>
        /// 净重(吨)
        /// </summary>
        public virtual Decimal SuttleWeight { get { return _SuttleWeight; } set { _SuttleWeight = value; } }


        private DateTime _InFactoryTime;
        /// <summary>
        /// 入厂时间
        /// </summary>
        public virtual DateTime InFactoryTime { get { return _InFactoryTime; } set { _InFactoryTime = value; } }

        private DateTime _TareTime;
        /// <summary>
        /// 皮重时间
        /// </summary>
        public virtual DateTime TareTime { get { return _TareTime; } set { _TareTime = value; } }

        private DateTime _LoadTime;
        /// <summary>
        /// 接煤时间
        /// </summary>
        public virtual DateTime LoadTime { get { return _LoadTime; } set { _LoadTime = value; } }

        private DateTime _GrossTime;
        /// <summary>
        /// 毛重时间
        /// </summary>
        public virtual DateTime GrossTime { get { return _GrossTime; } set { _GrossTime = value; } }

        private DateTime _OutFactoryTime;
        /// <summary>
        /// 出厂时间
        /// </summary>
        public virtual DateTime OutFactoryTime { get { return _OutFactoryTime; } set { _OutFactoryTime = value; } }

        private string _SupplierId;
        /// <summary>
        /// 接收单位Id
        /// </summary>
        public virtual string SupplierId { get { return _SupplierId; } set { _SupplierId = value; } }

        /// <summary>
        /// 接收单位
        /// </summary>
        [CMCS.DapperDber.Attrs.DapperIgnore]
        public virtual CmcsSupplier TheSupplier
        {
            get { return CommonDAO.GetInstance().SelfDber.Get<CmcsSupplier>(this.SupplierId); }
        }

        private string _TransportCompanyName;
        /// <summary>
        /// 运输单位名称
        /// </summary>
        public virtual string TransportCompanyName { get { return _TransportCompanyName; } set { _TransportCompanyName = value; } }

        private string _TransportCompanyId;
        /// <summary>
        /// 运输单位
        /// </summary>
        public virtual string TransportCompanyId { get { return _TransportCompanyId; } set { _TransportCompanyId = value; } }

        /// <summary>
        /// 运输单位
        /// </summary>
        [CMCS.DapperDber.Attrs.DapperIgnore]
        public virtual CmcsTransportCompany TheTransportCompany
        {
            get { return CommonDAO.GetInstance().SelfDber.Get<CmcsTransportCompany>(this.TransportCompanyId); }
        }

        private string _FuelKindId;
        /// <summary>
        /// 煤种
        /// </summary>
        public virtual string FuelKindId { get { return _FuelKindId; } set { _FuelKindId = value; } }

        /// <summary>
        /// 煤种
        /// </summary>
        [CMCS.DapperDber.Attrs.DapperIgnore]
        public virtual CmcsFuelKind TheFuelKind
        {
            get { return CommonDAO.GetInstance().SelfDber.Get<CmcsFuelKind>(this.FuelKindId); }
        }

        private Decimal _IsUse;
        /// <summary>
        /// 有效
        /// </summary>
        public virtual Decimal IsUse { get { return _IsUse; } set { _IsUse = value; } }

        private Decimal _IsFinish;
        /// <summary>
        /// 已完结
        /// </summary>
        public virtual Decimal IsFinish { get { return _IsFinish; } set { _IsFinish = value; } }

        private string _GrossPlace;
        /// <summary>
        /// 毛重地点
        /// </summary>
        public virtual string GrossPlace { get { return _GrossPlace; } set { _GrossPlace = value; } }

        private string _TarePlace;
        /// <summary>
        /// 皮重地点
        /// </summary>
        public virtual string TarePlace { get { return _TarePlace; } set { _TarePlace = value; } }


        private string _Remark;
        /// <summary>
        /// 备注
        /// </summary>
        public virtual string Remark { get { return _Remark; } set { _Remark = value; } }

        private string _StepName;
        /// <summary>
        /// 所处流程的步骤  入厂、重车、采样、轻车、出厂
        /// </summary>
        public virtual string StepName { get { return _StepName; } set { _StepName = value; } }

        /// <summary>
        /// 接煤地点
        /// </summary>
        public virtual String LoadArea { get; set; }
        /// <summary>
        /// 接煤人
        /// </summary>
        public virtual String Loader { get; set; }
        /// <summary>
        /// 运输单号
        /// </summary>
        public virtual String TransportNo { get; set; }

        /// <summary>
        /// 出量
        /// </summary>
        public virtual Decimal Outweight { get; set; }

        private string isSynch = "0";
        /// <summary>
        /// 同步标识
        /// </summary>
        public string IsSynch
        {
            get { return isSynch; }
            set { isSynch = value; }
        }

        private string outFactoryType;
        /// <summary>
        /// 出厂类型
        /// </summary>
        public string OutFactoryType
        {
            get { return outFactoryType; }
            set { outFactoryType = value; }
        }

        private DateTime _SamplingTime;
        /// <summary>
        /// 采样时间
        /// </summary>
        public DateTime SamplingTime
        {
            get { return _SamplingTime; }
            set { _SamplingTime = value; }
        }

        private string _SamplePlace;
        /// <summary>
        /// 采样地点
        /// </summary>
        public string SamplePlace
        {
            get { return _SamplePlace; }
            set { _SamplePlace = value; }
        }

        private string _SampleType;
        /// <summary>
        /// 采样方式
        /// </summary>
        public string SampleType
        {
            get { return _SampleType; }
            set { _SampleType = value; }
        }

        /// <summary>
        /// 预报明细Id
        /// </summary>
        public string LMYBDetailId { get; set; }

        /// <summary>
        /// 供应商名称
        /// </summary>
        public string SupplierName { get; set; }

        /// <summary>
        /// 成品仓ID
        /// </summary>
        public string CPCId { get; set; }

        /// <summary>
        /// 成品仓名称
        /// </summary>
        public string CPCName { get; set; }

        /// <summary>
        /// 煤场ID
        /// </summary>
        public string StorageId { get; set; }

        /// <summary>
        /// 煤场名称
        /// </summary>
        public string StorageName { get; set; }

        /// <summary>
        /// 来煤预报ID
        /// </summary>
        public string LMYBId { get; set; }
    }
}
