using System;
//
using System.Data;
using CMCS.DapperDber.Dbs.AccessDb;

namespace CMCS.ADGS.Core.CustomGraber.KaiYuan
{
    /// <summary>
    /// 自动工业分析仪 5E-MAG6700
    /// </summary>
    public class PAG_5EMAG6700 : AssayGraber
    {
        /// <summary>
        /// 提取范围 单位：天
        /// </summary>~
        public int DayRange
        {
            get { return Convert.ToInt32(Parameters["DayRange"]); }
        }

        #region Method

        public override DataTable ExecuteGrab()
        {
            AccessDapperDber accessDber = new AccessDapperDber(ConnStr);
            return accessDber.ExecuteDataTable("select * from TestResult where Date > #" + DateTime.Now.AddDays(-DayRange).ToString("yyyy-MM-dd") + "#");
        }

        #endregion
    }
}
