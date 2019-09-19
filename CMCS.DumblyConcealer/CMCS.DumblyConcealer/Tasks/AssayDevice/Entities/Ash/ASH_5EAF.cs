using System;
//
using System.Data;
using CMCS.DapperDber.Dbs.AccessDb;
using CMCS.DapperDber.Attrs;

namespace CMCS.DumblyConcealer.Tasks.AssayDevice.Entities
{
    /// <summary>
    /// 开元 5E-AF灰融仪
    /// </summary>
     [CMCS.DapperDber.Attrs.DapperBind("HYTBHRY")]
    public class ASH_5EAF 
    {
        [DapperPrimaryKey]
        public string PKID { get; set; }
        /// <summary>
        /// 化验编码
        /// </summary>
        public string SampleNO { get; set; }
        /// <summary>
        /// 化验编码
        /// </summary>
        public string SampleName { get; set; }
        /// <summary>
        /// 化验员
        /// </summary>
        public string Operator { get; set; }
     
        /// <summary>
        /// 测试时间
        /// </summary>	 
        public DateTime TestDate { get; set; }
        /// <summary>
        /// 
        /// </summary>		
        public decimal DT { get; set; }
        /// <summary>
        /// 
        /// </summary>		
        public decimal ST { get; set; }
        /// <summary>
        /// 
        /// </summary>		
        public decimal HT { get; set; }
        /// <summary>
        /// 
        /// </summary>		
        public decimal FT { get; set; }
        
        /// <summary>
        /// 设备编号
        /// </summary>
        public string MachineCode { get; set; }
    }
}
