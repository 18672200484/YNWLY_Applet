﻿// 此代码由 NhGenerator v1.0.9.0 工具生成。

using System;
using System.Collections;
using CMCS.Common.Entities.Sys;
using CMCS.Common.Entities.BaseInfo;
using CMCS.Common.DAO;

namespace CMCS.Common.Entities.CarTransport
{
	/// <summary>
	/// 汽车智能化-入厂煤运输记录
	/// </summary>
	[Serializable]
	[CMCS.DapperDber.Attrs.DapperBind("CmcsTbBuyFuelTransport")]
	public class CmcsBuyFuelTransport : EntityBase1
	{
		private String _AutotruckId;
		/// <summary>
		/// 关联：车辆管理
		/// </summary>
		public virtual String AutotruckId { get { return _AutotruckId; } set { _AutotruckId = value; } }

		private String _InFactoryBatchId;
		/// <summary>
		/// 入厂煤批次Id
		/// </summary>
		public virtual String InFactoryBatchId { get { return _InFactoryBatchId; } set { _InFactoryBatchId = value; } }

		private String _SamplingId;
		/// <summary>
		/// 入厂煤采样Id
		/// </summary>
		public virtual String SamplingId { get { return _SamplingId; } set { _SamplingId = value; } }

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

		private Decimal _DeductWeight;
		/// <summary>
		/// 扣吨(吨)
		/// </summary>
		public virtual Decimal DeductWeight { get { return _DeductWeight; } set { _DeductWeight = value; } }

		private Decimal _KgWeight;
		/// <summary>
		/// 扣矸(吨)
		/// </summary>
		public virtual Decimal KgWeight { get { return _KgWeight; } set { _KgWeight = value; } }

		private Decimal _KsWeight;
		/// <summary>
		/// 扣水(吨)
		/// </summary>
		public virtual Decimal KsWeight { get { return _KsWeight; } set { _KsWeight = value; } }

		private Decimal _AutoKsWeight;
		/// <summary>
		/// 自动扣水(吨)
		/// </summary>
		public virtual Decimal AutoKsWeight { get { return _AutoKsWeight; } set { _AutoKsWeight = value; } }

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

		private Decimal _CheckWeight;
		/// <summary>
		/// 验收量(吨)
		/// </summary>
		public virtual Decimal CheckWeight { get { return _CheckWeight; } set { _CheckWeight = value; } }

		private Decimal _ProfitAndLossWeight;
		/// <summary>
		/// 盈亏量(吨)
		/// </summary>
		public virtual Decimal ProfitAndLossWeight { get { return _ProfitAndLossWeight; } set { _ProfitAndLossWeight = value; } }

		private Decimal _TicketWeight;
		/// <summary>
		/// 矿发量(吨)
		/// </summary>
		public virtual Decimal TicketWeight { get { return _TicketWeight; } set { _TicketWeight = value; } }

		private DateTime _InFactoryTime;
		/// <summary>
		/// 入厂时间
		/// </summary>
		public virtual DateTime InFactoryTime { get { return _InFactoryTime; } set { _InFactoryTime = value; } }

		private DateTime _GrossTime;

		/// <summary>
		/// 入厂位置
		/// </summary>
		public virtual String InFactoryPlace { get; set; }
		/// <summary>
		/// 毛重时间
		/// </summary>
		public virtual DateTime GrossTime { get { return _GrossTime; } set { _GrossTime = value; } }

		private DateTime _TareTime;
		/// <summary>
		/// 皮重时间
		/// </summary>
		public virtual DateTime TareTime { get { return _TareTime; } set { _TareTime = value; } }

		private DateTime _SamplingTime;
		/// <summary>
		/// 采样时间
		/// </summary>
		public virtual DateTime SamplingTime { get { return _SamplingTime; } set { _SamplingTime = value; } }

		private DateTime _UploadTime;
		/// <summary>
		/// 卸煤时间
		/// </summary>
		public virtual DateTime UploadTime { get { return _UploadTime; } set { _UploadTime = value; } }

		private DateTime _OutFactoryTime;
		/// <summary>
		/// 出厂时间
		/// </summary>
		public virtual DateTime OutFactoryTime { get { return _OutFactoryTime; } set { _OutFactoryTime = value; } }

		/// <summary>
		/// 出厂位置
		/// </summary>
		public virtual String OutFactoryPlace { get; set; }

		private string _SupplierId;
		/// <summary>
		/// 供煤单位
		/// </summary>
		public virtual string SupplierId { get { return _SupplierId; } set { _SupplierId = value; } }

		private string _SupplierName;
		/// <summary>
		/// 供煤单位
		/// </summary>
		public virtual string SupplierName { get { return _SupplierName; } set { _SupplierName = value; } }

		/// <summary>
		/// 供煤单位
		/// </summary>
		[CMCS.DapperDber.Attrs.DapperIgnore]
		public CmcsSupplier TheSupplier
		{
			get { return CommonDAO.GetInstance().SelfDber.Get<CmcsSupplier>(this.SupplierId); }
		}

		private string _FuelKindId;
		/// <summary>
		/// 煤种
		/// </summary>
		public virtual string FuelKindId { get { return _FuelKindId; } set { _FuelKindId = value; } }

		private string _FuelKindName;
		/// <summary>
		/// 煤种
		/// </summary>
		public virtual string FuelKindName { get { return _FuelKindName; } set { _FuelKindName = value; } }

		/// <summary>
		/// 煤种
		/// </summary>
		[CMCS.DapperDber.Attrs.DapperIgnore]
		public CmcsFuelKind TheFuelKind
		{
			get { return CommonDAO.GetInstance().SelfDber.Get<CmcsFuelKind>(this.FuelKindId); }
		}

		private string _MineId;
		/// <summary>
		/// 矿点
		/// </summary>
		public virtual string MineId { get { return _MineId; } set { _MineId = value; } }


		private string _MineName;
		/// <summary>
		/// 矿点
		/// </summary>
		public virtual string MineName { get { return _MineName; } set { _MineName = value; } }

		/// <summary>
		/// 矿点
		/// </summary>
		[CMCS.DapperDber.Attrs.DapperIgnore]
		public CmcsMine TheMine
		{
			get { return CommonDAO.GetInstance().SelfDber.Get<CmcsMine>(this.MineId); }
		}

		private string _TransportCompanyId;
		/// <summary>
		/// 运输单位
		/// </summary>
		public virtual string TransportCompanyId { get { return _TransportCompanyId; } set { _TransportCompanyId = value; } }

		/// <summary>
		/// 运输单位
		/// </summary>
		[CMCS.DapperDber.Attrs.DapperIgnore]
		public CmcsTransportCompany TheTransportCompany
		{
			get { return CommonDAO.GetInstance().SelfDber.Get<CmcsTransportCompany>(this.TransportCompanyId); }
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

		private string _SamplePlace;
		/// <summary>
		/// 采样地点
		/// </summary>
		public virtual string SamplePlace { get { return _SamplePlace; } set { _SamplePlace = value; } }

		private string _SamplingType;
		/// <summary>
		/// 采样方式
		/// </summary>
		public virtual string SamplingType { get { return _SamplingType; } set { _SamplingType = value; } }

		private string _InFactoryType;
		/// <summary>
		/// 入场煤类型 入场煤  入场仓储煤
		/// </summary>
		public virtual string InFactoryType { get { return _InFactoryType; } set { _InFactoryType = value; } }

		private string _Remark;
		/// <summary>
		/// 备注
		/// </summary>
		public virtual string Remark { get { return _Remark; } set { _Remark = value; } }

		private string _StepName;
		/// <summary>
		/// 所处流程的步骤  入厂、重车、采样、轻车、出厂、成品仓
		/// </summary>
		public virtual string StepName { get { return _StepName; } set { _StepName = value; } }


		public virtual String UnLoadArea { get; set; }

		private string isSynch = "0";
		/// <summary>
		/// 同步标识
		/// </summary>
		public string IsSynch
		{
			get { return isSynch; }
			set { isSynch = value; }
		}

		/// <summary>
		/// 预报ID
		/// </summary>
		public virtual String LMYBId { get; set; }

		/// <summary>
		/// 预报编号
		/// </summary>
		public virtual String YbNum { get; set; }

		/// <summary>
		/// 预报明细ID
		/// </summary>
		public virtual String LMYBDetailId { get; set; }

		private int _PrintCount;
		/// <summary>
		/// 打印次数
		/// </summary>
		public int PrintCount { get { return _PrintCount; } set { _PrintCount = value; } }

		private DateTime _PrintTime;
		/// <summary>
		/// 打印时间
		/// </summary>
		public DateTime PrintTime { get { return _PrintTime; } set { _PrintTime = value; } }


	}
}
