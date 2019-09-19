using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.Entities;
using CMCS.Common.Entities.Sys;

namespace CMCS.DumblyConcealer.Tasks.AutoCupboard.Entities
{
    /// <summary>
    /// 故障信息表
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("tb_failLog")]
    public class Tb_FailLog
    {
        /// <summary>
        /// 自增主键
        /// </summary>
        [CMCS.DapperDber.Attrs.DapperPrimaryKey, CMCS.DapperDber.Attrs.DapperPrimaryKeyAdd]
        public Int32 Id { get; set; }

        /// <summary>
        /// 存查柜名
        /// </summary>
        public Int32 SSName { get; set; }

        /// <summary>
        /// 设备名称
        /// </summary>
        public String Dv_Name { get; set; }

        /// <summary>
        /// 故障代码
        /// </summary>
        public Int32 Dv_Code { get; set; }

        /// <summary>
        /// 故障内容
        /// </summary>
        public String Fail_Log { get; set; }

        /// <summary>
        /// 煤样编码
        /// </summary>
        public String Sample_Id { get; set; }

        /// <summary>
        /// 时间
        /// </summary>
        public DateTime Time { get; set; }

        public Int32 Status { get; set; }

        /// <summary>
        /// 故障发生时状态
        /// </summary>
        public String M_Status { get; set; }

    }
}
