using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMCS.WeighCheck.SampleMake.Enums
{
    /// <summary>
    /// 流程标识
    /// </summary>
    public enum eFlowFlag
    {
        等待扫码,
        等待校验,
        校验成功,
        打印化验码,

        重量校验,
        发送制样命令,
        等待制样结果,

        选择批次,
        选择采样单,
        样桶称重,
        等待登记,
        完成登记,
        
        样品登记
    }
}
