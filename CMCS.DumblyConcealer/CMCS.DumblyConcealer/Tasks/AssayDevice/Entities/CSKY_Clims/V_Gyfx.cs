using CMCS.DapperDber.Attrs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMCS.DumblyConcealer.Tasks.AssayDevice.Entities.CSKY_Clims
{
    /// <summary>
    /// 开元化验数据明细视图（工分仪）
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("V_Gyfx")]
    public class V_Gyfx
    {
        /// <summary>
        /// 自增量（主键）	
        /// </summary>
        [DapperPrimaryKey]
        public decimal mID { get; set; }
        /// <summary>
        /// 仪器数据库自增量	
        /// </summary>
        public decimal mPF_ID { get; set; }
        /// <summary>
        /// 仪器自动编号
        /// </summary>
        public string mPF_KEY { get; set; }
        /// <summary>
        /// 同步基准时间	
        /// </summary>
        public DateTime mPF_DATE { get; set; }
        /// <summary>
        /// 数据创建时间
        /// </summary>
        public DateTime mPF_CREATE_TIME { get; set; }
        /// <summary>
        /// 数据上传时间
        /// </summary>
        public DateTime mPF_UPLOAD_TIME { get; set; }
        /// <summary>
        /// 样品编号
        /// </summary>
        public string mPF_SAMPLE_NO { get; set; }
        /// <summary>
        /// 样品编号
        /// </summary>
        public string mSAMPLE_NO { get; set; }
        /// <summary>
        /// 仪器编号
        /// </summary>
        public string mAPP_CODE { get; set; }
        /// <summary>
        /// 仪器名称
        /// </summary>
        public string mAPP_Name{get;set;}
       
        /// <summary>
        /// 测试开始时间
        /// </summary>
        public DateTime mTEST_TIME_START { get; set; }
        /// <summary>
        /// 测试结束时间
        /// </summary>
        public DateTime mTEST_TIME_END { get; set; }
        /// <summary>
        /// 试样重量
        /// </summary>
        public decimal mSAMPLE_WEIGHT { get; set; }
        /// <summary>
        ///坩埚重量
        /// </summary>
        public decimal mM_WEIGHT { get; set; }
        
          /// <summary>
        /// 
        /// </summary>
        public decimal Mad { get; set; }

          /// <summary>
        /// 
        /// </summary>
        public decimal Aad { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal Vad { get; set; }

        /// <summary>
        /// 状态	-1	待汇总	0	移除	2	已汇总（包含）
        /// </summary>
        public int mSTATE { get; set; }
       
          /// <summary>
        /// 测试人
        /// </summary>
        public string mTEST_MAN { get; set; }
    }
}
