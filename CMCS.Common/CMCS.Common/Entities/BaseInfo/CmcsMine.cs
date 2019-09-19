using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.Entities.Sys;

namespace CMCS.Common.Entities.BaseInfo
{
    /// <summary>
    /// 基础信息-矿点
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("fultbmine")]
    public class CmcsMine : EntityBase1
    {
        /// <summary>
        /// 编码
        /// </summary>
        public String Code { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public String Name { get; set; }


        public String Valid { get; set; }
        public String ReMark { get; set; }
        public Int32 Sequence { get; set; }

        public String ParentId { get; set; }

        private string isSynch = "0";
        /// <summary>
        /// 同步标识
        /// </summary>
        public string IsSynch
        {
            get { return isSynch; }
            set { isSynch = value; }
        }
    }
}
