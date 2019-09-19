using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.DapperDber.Dbs.AccessDb;

namespace CMCS.ADGS.Core.CustomGraber.KaiYuan
{
    /// <summary>
    /// 开元 CH2200碳氢仪
    /// </summary>
    public class HAD_CH2200 : AssayGraber
    {
        /// <summary>
        /// 提取范围 单位：天
        /// </summary>~
        public int DayRange
        {
            get { return Convert.ToInt32(Parameters["DayRange"]); }
        }

        public override System.Data.DataTable ExecuteGrab()
        {
            AccessDapperDber accessDber = new AccessDapperDber(ConnStr);
            return accessDber.ExecuteDataTable("select * from sample where Date > #" + DateTime.Now.AddDays(-DayRange).ToString("yyyy-MM-dd") + "#");
        }

    }
}
