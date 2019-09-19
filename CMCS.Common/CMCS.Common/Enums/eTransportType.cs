using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMCS.Common.Enums
{
    /// <summary>
    /// 运输记录类型
    /// </summary>
    public enum eTransportType
    {
        原料煤入场,
        仓储煤入场,
        中转煤入场,

        仓储煤出场,
        中转煤出场,

        销售直销煤,
        销售掺配煤,

        其他物资,

        来访车辆
    }
}
