using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.Entities.Sys;

namespace CMCS.Common.Entities.Fuel
{
    /// <summary>
    /// 采样桶记录表
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("CmcsTbRcSampleBarrel")]
    public class CmcsRCSampleBarrel : EntityBase1
    {
        private string _InfactoryBatchId;
        private string _SamplingId;

        /// <summary>
        /// 入厂煤采样Id
        /// </summary>
        public string SamplingId
        {
            get { return _SamplingId; }
            set { _SamplingId = value; }
        }

        /// <summary>
        /// 关联批次id
        /// </summary>
        public string InFactoryBatchId
        {
            get { return _InfactoryBatchId; }
            set { _InfactoryBatchId = value; }
        }

        private string _SampleType;

        /// <summary>
        /// 采样方式 人工采样、机械采样、皮带采样
        /// </summary>
        public string SampleType
        {
            get { return _SampleType; }
            set { _SampleType = value; }
        }
        private string _SampleMachine;

        /// <summary>
        /// 采样机
        /// </summary>
        public string SampleMachine
        {
            get { return _SampleMachine; }
            set { _SampleMachine = value; }
        }
        private string _SampleCode;

        /// <summary>
        /// 采样码
        /// </summary>
        public string SampleCode
        {
            get { return _SampleCode; }
            set { _SampleCode = value; }
        }

        /// <summary>
        /// 采样次码
        /// </summary>
        public string SampSecondCode { get; set; }

        private string _BarrelCode;

        /// <summary>
        /// 桶编码
        /// </summary>
        public string BarrelCode
        {
            get { return _BarrelCode; }
            set { _BarrelCode = value; }
        }
        private DateTime _BarrellingTime;

        /// <summary>
        /// 装桶时间
        /// </summary>
        public DateTime BarrellingTime
        {
            get { return _BarrellingTime; }
            set { _BarrellingTime = value; }
        }
        private double _SampleWeight;

        /// <summary>
        /// 样重=校验样重-样桶重量
        /// </summary>
        public double SampleWeight
        {
            get { return _SampleWeight; }
            set { _SampleWeight = value; }
        }

        private double _BarrelWeight;

        /// <summary>
        /// 样桶重量
        /// </summary>
        public double BarrelWeight
        {
            get { return _BarrelWeight; }
            set { _BarrelWeight = value; }
        }

        private double _CheckSampleWeight;

        /// <summary>
        /// 校验样重
        /// </summary>
        public double CheckSampleWeight
        {
            get { return _CheckSampleWeight; }
            set { _CheckSampleWeight = value; }
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
    
        private double _HandWeight;

        /// <summary>
        /// 交样重量
        /// </summary>
        public double HandWeight
        {
            get { return _HandWeight; }
            set { _HandWeight = value; }
        }

        private DateTime _HandDate;

        /// <summary>
        /// 交样时间
        /// </summary>
        public DateTime HandDate
        {
            get { return _HandDate; }
            set { _HandDate = value; }
        }

        private string _HandUser;

        /// <summary>
        /// 交样人
        /// </summary>
        public string HandUser
        {
            get { return _HandUser; }
            set { _HandUser = value; }
        }

        private DateTime _CheckDate;

        /// <summary>
        /// 接样时间（校验时间）
        /// </summary>
        public DateTime CheckDate
        {
            get { return _CheckDate; }
            set { _CheckDate = value; }
        }

        private string _CheckUser;

        /// <summary>
        /// 接样人（校验人）
        /// </summary>
        public string CheckUser
        {
            get { return _CheckUser; }
            set { _CheckUser = value; }
        }

        /// <summary>
        /// 打印时间
        /// </summary>
        public DateTime PrintDate { get; set; }

        /// <summary>
        /// 打印次数
        /// </summary>
        public int PrintCount { get; set; }

        /// <summary>
        /// 打印人
        /// </summary>
        public string PrintUser { get; set; } 

        ///// <summary>
        ///// 临时字段-是否已扫码验证
        ///// </summary> 
        //public bool isScan = false;

        //[DapperDber.Attrs.DapperIgnore]
        //public bool IsExist { get; set; }
    }
}
