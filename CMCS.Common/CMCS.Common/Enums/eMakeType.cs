using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMCS.Common.Enums
{
    /// <summary>
    /// 制样方式
    /// </summary>
    public enum eMakeType
    {
        人工制样,
        机械制样,

        全水分样6mm1 = 1,
        全水分样6mm2 = 2,
        分析样3mm = 3,
        一般试验分析 = 4,
        存查样 = 5
    }
}
