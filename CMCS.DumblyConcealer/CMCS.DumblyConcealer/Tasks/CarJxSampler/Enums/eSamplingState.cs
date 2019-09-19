using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMCS.DumblyConcealer.Tasks.CarJXSampler.Enums
{
    /// <summary>
    /// 采样状态
    /// </summary>
    public enum eSamplingState
    {
        等待采样 = 0,
        正在采样 = 1,
        采样完成 = 2,
        采样异常 = 3
    }
}
