using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMCS.Common.Enums
{
    /// <summary>
    /// 第三方设备接口 - 设备系统状态
    /// </summary>
    public enum eEquInfSystemStatus
    {
        就绪待机 = 0,
        正在运行 = 1,
        发生故障 = 2,
        系统停止 = 3
    }
}
