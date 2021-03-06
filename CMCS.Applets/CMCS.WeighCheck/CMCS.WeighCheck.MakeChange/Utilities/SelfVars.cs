﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.Entities;
using CMCS.Common.Entities.iEAA;
using CMCS.WeighCheck.MakeChange.Frms.Sys;

namespace CMCS.WeighCheck.MakeChange.Utilities
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
        /// 当前登录用户
        /// </summary>
        public static User LoginUser2;

        /// <summary>
        /// 当前登陆用户名称
        /// </summary>
        public static string LoginUserNames;

        /// <summary>
        /// 当前登陆用户账号
        /// </summary>
        public static string LoginUserAccounts;

        /// <summary>
        /// 主窗体引用
        /// </summary>
        public static FrmMainFrame MainFrameForm;
    }
}
