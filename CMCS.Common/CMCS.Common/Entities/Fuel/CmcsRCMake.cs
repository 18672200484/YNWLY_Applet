using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.Entities.Sys;

namespace CMCS.Common.Entities.Fuel
{
    /// <summary>
    /// 入厂煤制样表
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("CMCSTBRCMAKE")]
    public class CmcsRCMake : EntityBase1
    {
        private string _SamplingId;

        /// <summary>
        /// 入厂煤采样Id
        /// </summary>
        public string SamplingId
        {
            get { return _SamplingId; }
            set { _SamplingId = value; }
        }
        private string _GetPle;

        /// <summary>
        /// 接样人
        /// </summary>
        public string GetPle
        {
            get { return _GetPle; }
            set { _GetPle = value; }
        }
        private DateTime _GetDate;

        /// <summary>
        /// 接样时间
        /// </summary>
        public DateTime GetDate
        {
            get { return _GetDate; }
            set { _GetDate = value; }
        }
        private int _GetBarrelCount;

        /// <summary>
        /// 接样桶数
        /// </summary>
        public int GetBarrelCount
        {
            get { return _GetBarrelCount; }
            set { _GetBarrelCount = value; }
        }
        private DateTime _MakeStartTime;

        /// <summary>
        /// 制样开始时间
        /// </summary>
        public DateTime MakeStartTime
        {
            get { return _MakeStartTime; }
            set { _MakeStartTime = value; }
        }
        private DateTime _MakeEndTime;

        /// <summary>
        /// 制样结束时间
        /// </summary>
        public DateTime MakeEndTime
        {
            get { return _MakeEndTime; }
            set { _MakeEndTime = value; }
        }

        private DateTime _UseTime;

        /// <summary>
        /// 实际采样时间
        /// </summary>
        public DateTime UseTime
        {
            get { return _UseTime; }
            set { _UseTime = value; }
        }

        private string _MakePle;

        /// <summary>
        /// 制样人
        /// </summary>
        public string MakePle
        {
            get { return _MakePle; }
            set { _MakePle = value; }
        }
        private string _MakeStyle;

        /// <summary>
        /// 制样方式
        /// </summary>
        public string MakeStyle
        {
            get { return _MakeStyle; }
            set { _MakeStyle = value; }
        }

        private string _MakeType;
        /// <summary>
        /// 制样方式
        /// </summary>
        public string MakeType
        {
            get { return _MakeType; }
            set { _MakeType = value; }
        }

        private string _MakeCode;

        /// <summary>
        /// 制样码
        /// </summary>
        public string MakeCode
        {
            get { return _MakeCode; }
            set { _MakeCode = value; }
        }
        private string _BackupCode;

        /// <summary>
        /// 备用码
        /// </summary>
        public string BackupCode
        {
            get { return _BackupCode; }
            set { _BackupCode = value; }
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

        private int isHandOver = 0;
        /// <summary>
        /// 是否手动解绑
        /// </summary>
        public int IsHandOver
        {
            get { return isHandOver; }
            set { isHandOver = value; }
        }

        /// <summary>
        /// 送样单位
        /// </summary>
        public string SendUnit { get; set; }

        /// <summary>
        /// 接样重量
        /// </summary>
        public decimal GetBarrelWeight { get; set; }

        private string isSynch = "0";
        /// <summary>
        /// 同步标识
        /// </summary>
        public string IsSynch
        {
            get { return isSynch; }
            set { isSynch = value; }
        }

		private string parentMakeId;
		/// <summary>
		/// 父ID
		/// </summary>
		public string ParentMakeId
		{
			get { return parentMakeId; }
			set { parentMakeId = value; }
		}

		/// <summary>
		/// 合/留样 0 无 1 留样 2 合样
		/// </summary>
		public int StayStatus { get; set; }

        /// <summary>
        /// 采样记录
        /// </summary>
        [CMCS.DapperDber.Attrs.DapperIgnore]
        public CmcsRCSampling TheRcSampling
        {
            get { return Dbers.GetInstance().SelfDber.Get<CmcsRCSampling>(this.SamplingId); }
        }
    }
}
