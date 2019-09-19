using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMCS.Common.Enums
{
    /// <summary>
    /// 第三方设备接口 - 采样机系统状态
    /// </summary>
    public enum eEquInfSamplerSystemStatus
    {
        就绪待机 = 0,
        正在运行 = 1,
        采样完成 = 2,
        发生故障 = 3,
        停止 = 4,
        急停 = 5,
        正在卸样 = 6
    }
}
