using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.Entities.Sys;

namespace CMCS.Common.Entities.Fuel
{
	/// <summary>
	/// 入厂煤制样明细表
	/// </summary>
	[CMCS.DapperDber.Attrs.DapperBind("CMCSTBRCMakeDetail")]
	public class CmcsRCMakeDetail : EntityBase1
	{
		private string _MakeId;

		/// <summary>
		/// 入厂煤制样Id
		/// </summary>
		public string MakeId
		{
			get { return _MakeId; }
			set { _MakeId = value; }
		}
		private string _BarrelCode;

		/// <summary>
		/// 样罐编码
		/// </summary>
		public string BarrelCode
		{
			get { return _BarrelCode; }
			set { _BarrelCode = value; }
		}

		/// <summary>
		/// 装罐时间
		/// </summary>
		public DateTime BarrelTime { get; set; }

		private string _BackupCode;

		/// <summary>
		/// 备用码
		/// </summary>
		public string BackupCode
		{
			get { return _BackupCode; }
			set { _BackupCode = value; }
		}
		private string _SampleType;

		/// <summary>
		/// 样品类型
		/// </summary>
		public string SampleType
		{
			get { return _SampleType; }
			set { _SampleType = value; }
		}
		private decimal _Weight;

		/// <summary>
		/// 样重（克）
		/// </summary>
		public decimal Weight
		{
			get { return _Weight; }
			set { _Weight = value; }
		}

		private decimal _CheckWeight;

		/// <summary>
		/// 校验样重
		/// </summary>
		public decimal CheckWeight
		{
			get { return _CheckWeight; }
			set { _CheckWeight = value; }
		}

		private DateTime _CheckTime;
		/// <summary>
		/// 校验时间
		/// </summary>
		public DateTime CheckTime
		{
			get { return _CheckTime; }
			set { _CheckTime = value; }
		}

		private int _PrintCount;
		/// <summary>
		/// 打印次数
		/// </summary>
		public int PrintCount
		{
			get { return _PrintCount; }
			set { _PrintCount = value; }
		}

		private DateTime _PrintTime;
		/// <summary>
		/// 打印时间
		/// </summary>
		public DateTime PrintTime
		{
			get { return _PrintTime; }
			set { _PrintTime = value; }
		}

		private string _CheckType;
		/// <summary>
		/// 校验类型
		/// </summary>
		public string CheckType
		{
			get { return _CheckType; }
			set { _CheckType = value; }
		}

		private string _CheckUser;
		/// <summary>
		/// 验证人
		/// </summary>
		public string CheckUser
		{
			get { return _CheckUser; }
			set { _CheckUser = value; }
		}

		/// <summary>
		/// 是否验证
		/// </summary>
		public int IsCheck { get; set; }

		[DapperDber.Attrs.DapperIgnore]
		public CmcsRCMake TheRCMake
		{
			get
			{
				return Dbers.GetInstance().SelfDber.Get<CmcsRCMake>(this.MakeId);
			}
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

	}
}
