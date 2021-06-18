using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.Entities.Sys;

namespace CMCS.Common.Entities.Fuel
{
	/// <summary>
	/// 入厂煤化验表
	/// </summary>
	[CMCS.DapperDber.Attrs.DapperBind("CMCSTBRCASSAY")]
	public class CmcsRCAssay : EntityBase1
	{
		private string _InFactoryBatchId;

		/// <summary>
		/// 关联批次id
		/// </summary>
		public string InFactoryBatchId
		{
			get { return _InFactoryBatchId; }
			set { _InFactoryBatchId = value; }
		}

		private String _AssayType;
		/// <summary>
		/// 化验类型
		/// </summary>
		public virtual String AssayType { get { return _AssayType; } set { _AssayType = value; } }

		private String _AssayCode;
		/// <summary>
		/// 化验编码
		/// </summary>
		public virtual String AssayCode { get { return _AssayCode; } set { _AssayCode = value; } }

		private String _MakeId;
		/// <summary>
		/// 制样记录Id
		/// </summary>
		public virtual String MakeId { get { return _MakeId; } set { _MakeId = value; } }
		private String _AssayPle;
		/// <summary>
		/// 化验人
		/// </summary>
		public virtual String AssayPle { get { return _AssayPle; } set { _AssayPle = value; } }

		private DateTime _AssayDate;
		/// <summary>
		/// 化验时间
		/// </summary>
		public virtual DateTime AssayDate { get { return _AssayDate; } set { _AssayDate = value; } }

		private String _SendPle;
		/// <summary>
		/// 制样送样人
		/// </summary>
		public virtual String SendPle { get { return _SendPle; } set { _SendPle = value; } }

		private DateTime _SendDate;
		/// <summary>
		/// 制样送样时间
		/// </summary>
		public virtual DateTime SendDate { get { return _SendDate; } set { _SendDate = value; } }

		private String _GetPle;
		/// <summary>
		/// 化验收样人
		/// </summary>
		public virtual String GetPle { get { return _GetPle; } set { _GetPle = value; } }

		private DateTime _GetDate;
		/// <summary>
		/// 化验收样时间
		/// </summary>
		public virtual DateTime GetDate { get { return _GetDate; } set { _GetDate = value; } }

		private string _FuelQualityId;

		/// <summary>
		/// 关联煤质id
		/// </summary>
		public string FuelQualityId
		{
			get { return _FuelQualityId; }
			set { _FuelQualityId = value; }
		}
		private string _Remark;
		/// <summary>
		/// 备注
		/// </summary>
		public string Remark
		{
			get { return _Remark; }
			set { _Remark = value; }
		}
		private int _WfStatus;
		/// <summary>
		/// 工作流状态
		/// </summary>
		public int WfStatus
		{
			get { return _WfStatus; }
			set { _WfStatus = value; }
		}

		private string isSynch = "0";
		/// <summary>
		/// 同步标识
		/// </summary>
		public string IsSynch
		{
			get { return isSynch; }
			set { isSynch = value; }
		}

		private int isRelieve = 0;
		/// <summary>
		/// 是否解绑 0 未解绑 1已解绑
		/// </summary>
		public int IsRelieve
		{
			get { return isRelieve; }
			set { isRelieve = value; }
		}

		/// <summary>
		/// 化验指标
		/// </summary>
		public string AssayPoint { get; set; }

		/// <summary>
		/// 打印日期
		/// </summary>
		public DateTime PrintTime { get; set; }

		/// <summary>
		/// 打印次数
		/// </summary>
		public int PrintCount { get; set; }

		/// <summary>
		/// 归批日期
		/// </summary>
		public DateTime BackBatchDate { get; set; }

		/// <summary>
		/// 化验类型
		/// </summary>
		public string AssayWay { get; set; }

		/// <summary>
		/// 父ID
		/// </summary>
		public string ParentId { get; set; }

		/// <summary>
		/// 制样
		/// </summary>
		[CMCS.DapperDber.Attrs.DapperIgnore]
		public CmcsRCMake TheRcMake
		{
			get { return Dbers.GetInstance().SelfDber.Get<CmcsRCMake>(this.MakeId); }
		}

		/// <summary>
		/// 化验煤质
		/// </summary>
		[CMCS.DapperDber.Attrs.DapperIgnore]
		public CmcsFuelQuality TheFuelQuality
		{
			get { return Dbers.GetInstance().SelfDber.Get<CmcsFuelQuality>(this.FuelQualityId); }
		}
	}
}
