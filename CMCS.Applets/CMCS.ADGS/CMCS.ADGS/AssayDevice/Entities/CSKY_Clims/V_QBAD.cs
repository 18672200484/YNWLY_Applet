using CMCS.DapperDber.Attrs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMCS.ADGS.Win.Entities.CSKY_Clims
{
    /// <summary>
    /// 开元化验数据明细视图（弹筒热值）
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("V_QBAD")]
    public class V_QBAD
    {
        /// <summary>
        /// 自增量（主键）	
        /// </summary>
        [DapperPrimaryKey]
        public decimal ID { get; set; }
        /// <summary>
        /// 仪器数据库自增量	
        /// </summary>
        public decimal PF_ID { get; set; }
        /// <summary>
        /// 仪器自动编号
        /// </summary>
        public string PF_KEY { get; set; }
        /// <summary>
        /// 同步基准时间	
        /// </summary>
        public DateTime PF_DATE { get; set; }
        /// <summary>
        /// 数据创建时间
        /// </summary>
        public DateTime PF_CREATE_TIME { get; set; }
        /// <summary>
        /// 数据上传时间
        /// </summary>
        public DateTime PF_UPLOAD_TIME { get; set; }
        /// <summary>
        /// 样品编号
        /// </summary>
        public string PF_SAMPLE_NO { get; set; }
        /// <summary>
        /// 样品编号
        /// </summary>
        public string SAMPLE_NO { get; set; }
        /// <summary>
        /// 仪器编号
        /// </summary>
        public string APP_CODE { get; set; }
        /// <summary>
        /// 仪器名称
        /// </summary>
        public string APP_Name{get;set;}
       
        /// <summary>
        /// 测试开始时间
        /// </summary>
        public DateTime TEST_TIME_START { get; set; }
        /// <summary>
        /// 测试结束时间
        /// </summary>
        public DateTime TEST_TIME_END { get; set; }
        /// <summary>
        /// 试样重量
        /// </summary>
        public decimal SAMPLE_WEIGHT { get; set; }
        
          /// <summary>
        /// 弹筒热值
        /// </summary>
        public decimal QBAD { get; set; }

          /// <summary>
        /// 点火丝热值
        /// </summary>
        public decimal DianRZ { get; set; }

        /// <summary>
        /// 状态	-1	待汇总	0	移除	2	已汇总（包含）
        /// </summary>
        public int STATE { get; set; }
          /// <summary>
        /// 热容量
        /// </summary>
        public string RRL { get; set; }

          /// <summary>
        /// 主期温升
        /// </summary>
        public string ZQWS { get; set; }

          /// <summary>
        /// 氧弹号
        /// </summary>
        public string YDH { get; set; }
        
          /// <summary>
        /// 测试人
        /// </summary>
        public string TEST_MAN { get; set; }
        
          /// <summary>
        /// 冷却校正值C
        /// </summary>
        public string F_C { get; set; }
        
          /// <summary>
        /// 添加热
        /// </summary>
        public string Tian { get; set; }
        
        /// <summary>
        /// 样品类型
        /// </summary>
        public string SAMPLE_TYPE { get; set; }
    }
}
