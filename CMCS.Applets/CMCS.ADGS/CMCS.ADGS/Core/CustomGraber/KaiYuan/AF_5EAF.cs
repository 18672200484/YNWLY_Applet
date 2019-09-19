using System;
//
using System.Data;
using CMCS.DapperDber.Dbs.AccessDb;

namespace CMCS.ADGS.Core.CustomGraber.KaiYuan
{
    /// <summary>
    /// 开元 5E-AF灰融仪
    /// </summary>
    public class AF_5EAF : AssayGraber
    {
        /// <summary>
        /// 提取范围 单位：天
        /// </summary>~
        public int DayRange
        {
            get { return Convert.ToInt32(Parameters["DayRange"]); }
        }

        public override DataTable ExecuteGrab()
        {
            AccessDapperDber accessDber = new AccessDapperDber(ConnStr);
            return accessDber.ExecuteDataTable("select * from exp where TestDate > #" + DateTime.Now.AddDays(-DayRange).ToString("yyyy-MM-dd") + "#");
        }

    }
}
