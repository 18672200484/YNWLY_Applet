using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.Entities.Sys;

namespace CMCS.Common.Entities.Fuel
{
    /// <summary>
    /// 化验数据-碳氢仪
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("CMCSTBHADSTDASSAY")]
    public class CmcsHadStdAssay : EntityBase1
    {

        /// <summary>
        /// 化验编码
        /// </summary>
        public string SampleNumber { get; set; }

        /// <summary>
        /// 设备编号
        /// </summary>
        public string FacilityNumber { get; set; }

        /// <summary>
        /// 样品重量
        /// </summary>
        public decimal SampleWeight { get; set; }

        /// <summary>
        /// 氢值
        /// </summary>
        public decimal Had { get; set; }

        /// <summary>
        /// 氢值
        /// </summary>
        public decimal Hd { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal Cad { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal Cd { get; set; }

        /// <summary>
        /// 化验用户
        /// </summary>
        public string AssayUser { get; set; }

        /// <summary>
        /// 化验日期
        /// </summary>
        public DateTime AssayTime { get; set; }

        /// <summary>
        /// 顺序号
        /// </summary>
        public int OrderNumber { get; set; }

        /// <summary>
        /// 是否有效
        /// </summary>
        public int IsEffective { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string PKID { get; set; }

        /// <summary>
        /// 数据类型 原始数据 有效数据
        /// </summary>
        public string DataType { get; set; }

        /// <summary>
        /// 是否手动修改数据
        /// </summary>
        public string IsHandModify { get; set; }

    }
}
