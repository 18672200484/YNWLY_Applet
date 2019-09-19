using System;
//
using System.Data;
using CMCS.DapperDber.Dbs.AccessDb;
using CMCS.DapperDber.Attrs;

namespace CMCS.DumblyConcealer.Tasks.AssayDevice.Entities
{
    /// <summary>
    /// 开元 5E-CH2200碳氢
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("HYTBTQY")]
    public class HAD_CH2200 
    {
        [DapperPrimaryKey]
        public string PKID { get; set; }
        
        /// <summary>
        /// 化验编码
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// 化验员
        /// </summary>
        public string Operator { get; set; }
        /// <summary>
        /// 测试时间
        /// </summary>	 
        public DateTime Date_Ex { get; set; }
        /// <summary>
        /// 样重
        /// </summary>		
        public decimal Weight { get; set; }
        /// <summary>
        /// 空干基氢
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
        /// 设备编号
        /// </summary>
        public string MachineCode { get; set; }
    }
}
