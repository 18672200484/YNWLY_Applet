using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.Entities;
using CMCS.Common.Entities.Sys;

namespace CMCS.Common.Views
{
    /// <summary>
    /// 视图-出厂煤运输记录视图
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("View_CCSampling")]
    public class View_CCSampling : EntityBase2
    {
        private DateTime _SamplingDate;
        private String _SampleCode;
        private String _SamplingType;
        private String _Batch;
        private String _BatchId;
        private DateTime _FactarriveDate;
        private String _BatchType;
        private String _SupplierName;
        private String _MineName;
        private String _FuelName;
        private String _StationName;

        /// <summary>
        /// 采样码
        /// </summary>
        public String SampleCode
        {
            get { return _SampleCode; }
            set { _SampleCode = value; }
        }

        /// <summary>
        /// 采样方式
        /// </summary>
        public String SamplingType
        {
            get { return _SamplingType; }
            set { _SamplingType = value; }
        }

        /// <summary>
        /// 配煤方案
        /// </summary>
        public String MincoalNo
        {
            get { return _Batch; }
            set { _Batch = value; }
        }

        /// <summary>
        /// 配煤方案ID
        /// </summary>
        public String InCoalPotId
        {
            get { return _BatchId; }
            set { _BatchId = value; }
        }

        /// <summary>
        /// 编制时间
        /// </summary>
        public DateTime DraftTime
        {
            get { return _FactarriveDate; }
            set { _FactarriveDate = value; }
        }

        /// <summary>
        /// 采样日期
        /// </summary>
        public DateTime SamplingDate
        {
            get { return _SamplingDate; }
            set { _SamplingDate = value; }
        }
    }
}
