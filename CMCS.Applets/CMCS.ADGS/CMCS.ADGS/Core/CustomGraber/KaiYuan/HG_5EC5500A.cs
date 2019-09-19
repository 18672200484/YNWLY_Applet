using System;
//
using System.Data;
using CMCS.DapperDber.Dbs.AccessDb;

namespace CMCS.ADGS.Core.CustomGraber.KaiYuan
{
    /// <summary>
    /// 自动量热仪 5E-C5500A双控
    /// </summary>
    public class HG_5EC5500A : AssayGraber
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
            return accessDber.ExecuteDataTable("select * from win5emdb where Testtime > #" + DateTime.Now.AddDays(-DayRange).ToString("yyyy-MM-dd") + "#");
        }

        #endregion
    }
}
