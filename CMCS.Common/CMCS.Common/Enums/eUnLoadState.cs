using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMCS.Common.Enums
{
    /// <summary>
    /// 卸料机状态
    /// </summary>
    public enum eUnLoadState
    {
        就绪待机 = 0,
        准备卸料 = 1,
        正在卸料 = 2,
        发生故障 = 3
    }
}
