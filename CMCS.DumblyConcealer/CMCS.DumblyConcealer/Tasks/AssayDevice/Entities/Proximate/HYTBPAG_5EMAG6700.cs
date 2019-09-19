using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.Entities;
using CMCS.DapperDber.Attrs;

namespace CMCS.DumblyConcealer.Tasks.AssayDevice.Entities
{
    /// <summary>
    /// 工分仪
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("HYTBGFY")]
    public class HYTBPAG_5EMAG6700
    {
        [DapperPrimaryKey]
        public string PKID { get; set; }
        /// <summary>
        /// 化验编码
        /// </summary>
        public string SampleName { get; set; }
        /// <summary>
        /// 样品顺序
        /// </summary>
        public int ObjCode { get; set; }
        /// <summary>
        /// 测试时间
        /// </summary>	 
        public DateTime Date_Ex { get; set; }
        /// <summary>
        /// 内水含量
        /// </summary>		
        public decimal Mad { get; set; }
        /// <summary>
        /// 空干基灰分
        /// </summary>		
        public decimal Aad { get; set; }
        /// <summary>
        /// 空干基挥发分
        /// </summary>		
        public decimal Vad { get; set; }
        /// <summary>
        /// 水灰空坩埚重
        /// </summary>		
        public decimal EmptyGGWeight { get; set; }
        /// <summary>
        /// 水灰煤样重
        /// </summary>		
        public decimal ColeWeight { get; set; } 
        /// <summary>
        /// 热值
        /// </summary>		
        public decimal Qbad { get; set; }
        /// <summary>
        /// 测试人员
        /// </summary>		
        public string Operator { get; set; }
        /// <summary>
        /// 设备编号
        /// </summary>
        public string MachineCode { get; set; }
    }
}
