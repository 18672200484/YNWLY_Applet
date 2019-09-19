using CMCS.DapperDber.Attrs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMCS.ADGS.Win.Entities.CSKY_Clims
{
    /// <summary>
    /// 开元化验数据明细视图（全硫）
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("V_STAD")]
    public class V_STAD
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
        public string APP_Name { get; set; }

        /// <summary>
        /// 化验员
        /// </summary>
        public string TEST_Man { get; set; }

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
        /// 空干基硫
        /// </summary>
        public decimal STAD{ get; set; }
        /// <summary>
        /// 空干基水
        /// </summary>
        public decimal MAD { get; set; }
    }
}
