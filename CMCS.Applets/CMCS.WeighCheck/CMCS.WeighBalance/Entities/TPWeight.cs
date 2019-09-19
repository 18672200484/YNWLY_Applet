using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMCS.WeighBalance.Entities
{
    [CMCS.DapperDber.Attrs.DapperBind("TP_Weight")]
    public class TPWeight
    {
        [CMCS.DapperDber.Attrs.DapperPrimaryKeyAdd, CMCS.DapperDber.Attrs.DapperPrimaryKey]
        public string Id { get; set; }

        public DateTime CreateTime { get; set; }

        /// <summary>
        /// 重量
        /// </summary>
        public double Weight { get; set; }

        /// <summary>
        /// 唯一编号
        /// </summary>
        public string S_NO { get; set; }

        /// <summary>
        /// 类型
        /// </summary>
        public string TP_Type { get; set; }

        public string Creator { get; set; }

        /// <summary>
        /// 天平编号
        /// </summary>
        public string TP_NO { get; set; }

        /// <summary>
        /// 是否启用
        /// </summary>
        public int Stateop { get; set; }

        /// <summary>
        /// 排序号
        /// </summary>
        public string SortNumber { get; set; }

        /// <summary>
        /// 坩埚号
        /// </summary>
        public string GG_NO { get; set; }

        /// <summary>
        /// 化验编码
        /// </summary>
        public string Sample_NO { get; set; }
    }
}
