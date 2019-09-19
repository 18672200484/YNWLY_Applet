﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.Entities.Sys;

namespace CMCS.Common.Entities.Fuel
{
    /// <summary>
    /// 入厂煤采样表
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("CMCSTBRCSAMPLING")]
    public class CmcsRCSampling : EntityBase1
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
        private DateTime _SamplingDate;

        /// <summary>
        /// 采样时间
        /// </summary>
        public DateTime SamplingDate
        {
            get { return _SamplingDate; }
            set { _SamplingDate = value; }
        }
        private string _SamplingPle;

        /// <summary>
        /// 采样人
        /// </summary>
        public string SamplingPle
        {
            get { return _SamplingPle; }
            set { _SamplingPle = value; }
        }
        private string _SamplingType;

        /// <summary>
        /// 采样方式
        /// </summary>
        public string SamplingType
        {
            get { return _SamplingType; }
            set { _SamplingType = value; }
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
        private string _ParentId;

        /// <summary>
        /// 留样合样采样单Id
        /// </summary>
        public string ParentId
        {
            get { return _ParentId; }
            set { _ParentId = value; }
        }
        /// <summary>
        /// 采样机
        /// </summary>
        public string SAMPLER { get; set; }

        /// <summary>
        /// 天气
        /// </summary>
        public string WEATH { get; set; }

        /// <summary>
        /// 车号（直接查询批次的车号，防止拆批后不准确）
        /// </summary>
        public string CARNUMS { get; set; }

        /// <summary>
        /// 采样方案
        /// </summary>
        public string SAMPLINGSCHEME { get; set; }

        /// <summary>
        /// 对比样
        /// </summary>
        public string COMPARESAMPLING { get; set; }

        /// <summary>
        /// 总车数（直接查询批次的总车数，防止拆批后不准确）
        /// </summary>
        public int TOTALNUM { get; set; }
        
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
        /// 批次记录
        /// </summary>
        [CMCS.DapperDber.Attrs.DapperIgnore]
        public CmcsInFactoryBatch TheInFactoryBatch
        {
            get { return Dbers.GetInstance().SelfDber.Get<CmcsInFactoryBatch>(this.InFactoryBatchId); }
        }
    }
}
