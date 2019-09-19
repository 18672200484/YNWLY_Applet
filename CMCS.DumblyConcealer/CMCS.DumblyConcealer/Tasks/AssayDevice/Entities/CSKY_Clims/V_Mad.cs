using CMCS.DapperDber.Attrs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMCS.DumblyConcealer.Tasks.AssayDevice.Entities.CSKY_Clims
{
    /// <summary>
    /// 开元化验数据明细视图（水分）
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("V_Mad")]
    public class V_Mad
    {
        /// <summary>
        /// 自增量（主键）	
        /// </summary>
        [DapperPrimaryKey]
        public decimal ID { get; set; }
        /// <summary>
        /// 仪器数据库自增量	
        /// </summary>
        public decimal CLID { get; set; }
        
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
        public string APP_Name { get; set; }

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
        /// 称量瓶号
        /// </summary>
        public string M_NO { get; set; }
        /// <summary>
        /// 称量瓶重
        /// </summary>
        public decimal M_WEIGHT { get; set; }

        /// <summary>
        /// 烘干后重
        /// </summary>
        public decimal HG_WEIGHT { get; set; }

        /// <summary>
        /// 烘干后检查性试验重量
        /// </summary>
        public decimal HG2_WEIGHT { get; set; }

        /// <summary>
        /// 试样重量
        /// </summary>
        public decimal HG3_WEIGHT { get; set; }
        
        /// <summary>
        /// 内水
        /// </summary>
        public decimal MAD { get; set; }

        /// <summary>
        /// 状态	-1	待汇总	0	移除	2	已汇总（包含）
        /// </summary>
        public int STATE { get; set; }

        /// <summary>
        /// 加样重
        /// </summary>
        public decimal MADJyz { get; set; }

        /// <summary>
        /// 烘干后减少重量
        /// </summary>
        public decimal Madjz { get; set; }

        /// <summary>
        /// 水校正系数
        /// </summary>
        public decimal KMac { get; set; }

        /// <summary>
        /// 水失重
        /// </summary>
        public decimal M_ShiZhong { get; set; }

        /// <summary>
        /// 加样重
        /// </summary>
        public decimal WAddColeWeight { get; set; }

        /// <summary>
        /// 烘干后检查性试验重量
        /// </summary>
        public decimal AdjustWcanzhong { get; set; }

        /// <summary>
        /// 化验员
        /// </summary>
        public string TEST_MAN { get; set; }

    }
}
