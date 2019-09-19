using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMCS.CarTransport.Order.Enums
{
    /// <summary>
    /// 流程标识
    /// </summary>
    public enum eFlowFlag
    {
        等待车辆,
        识别车辆,
        验证信息,
        等待进入,
        正在取煤,
        取煤完成,
        保存信息,
        等待离开
    }
}
