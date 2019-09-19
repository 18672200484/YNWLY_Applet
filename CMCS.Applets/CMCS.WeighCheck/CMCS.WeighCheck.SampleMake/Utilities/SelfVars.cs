using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.Entities;
using CMCS.Common.Entities.iEAA;
using CMCS.WeighCheck.SampleMake.Frms.Sys;

namespace CMCS.WeighCheck.SampleMake.Utilities
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

        /// <summary>
        /// 电子秤是否已连接
        /// </summary>
        public static bool WeightOpen;

        /// <summary>
        /// 电子天平是否已连接
        /// </summary>
        public static bool WeightMinOpen;

        /// <summary>
        /// 读卡器是否已连接
        /// </summary>
        public static bool RfReadOpen;
    }
}
