using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.Entities;
using CMCS.Common.Entities.iEAA;
using CMCS.BalanceScale.Frms.Sys;

namespace CMCS.BalanceScale.Utilities
{
    /// <summary>
    /// 变量集合
    /// </summary>
    public static class SelfVars
    {
        /// <summary>
        /// 当前登录用户
        /// </summary>
        public static User LoginUser;

        /// <summary>
        /// 主窗体引用
        /// </summary>
        public static FrmMainFrame MainFrameForm; 
    }
}
