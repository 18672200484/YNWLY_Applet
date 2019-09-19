using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.Entities.Sys;

namespace CMCS.Common.Entities.BaseInfo
{
    /// <summary>
    /// 基础信息-送样单位管理
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("fultbsendunit")]
    public class CmcsSendUnit : EntityBase1
    {
        /// <summary>
        /// 编码
        /// </summary>
        public String SendUnitCode { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        public String SendUnitName { get; set; }

        /// <summary>
        /// 联系人
        /// </summary>
        public String Linker { get; set; }
        /// <summary>
        /// Email
        /// </summary>
        public virtual String Email { get; set; }

        /// <summary>
        /// 地址
        /// </summary>
        public virtual String Address { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public virtual String Remark { get; set; }

        /// <summary>
        /// 是否有效
        /// </summary>
        public virtual String Vaild { get; set; }
    }
}
