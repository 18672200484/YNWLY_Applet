using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMCS.Common.Enums
{
    /// <summary>
    /// 第三方设备接口 - 全自动制样机系统状态
    /// </summary>
    public enum eEquInfAutoMakerSystemStatus
    {
        停止 = 0,//就地模式 远程不能下发
        正在运行 = 1,//不可以制下一批样
        发生故障 = 2,//不可以制下一批样
        就绪待机 = 3,//可以卸料状态
        急停 = 4,//需管控下发急停复位或就地复位制样机
        可以制样 = 5
    }
}
