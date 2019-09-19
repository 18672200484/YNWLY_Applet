using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.Entities.Sys;

namespace CMCS.Common.Entities.Fuel
{
    /// <summary>
    /// 采样交接样表
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("Cmcstbrchandoversampling")]
    public class CmcsRCHandSampling : EntityBase1
    {
        /// <summary>
        ///采样送样人
        /// </summary>
        public string SamplingSendPle { get; set; }

        /// <summary>
        /// 采样送样时间
        /// </summary>
        public DateTime SamplingSendDate { get; set; }

        /// <summary>
        /// 制样收样人
        /// </summary>
        public string MakeReceivePle { get; set; }

        /// <summary>
        /// 制样收样时间
        /// </summary>
        public DateTime MakeReceiveDate { get; set; }

        /// <summary>
        /// 是否上传
        /// </summary>
        public int IsUpload { get; set; }

        /// <summary>
        /// 关联采样单
        /// </summary>
        public string SamplingId { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public string Remark { get; set; }

    }
}
