using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.UnloadSampler.Enums;
using CMCS.Common;
using CMCS.Common.Entities;
using CMCS.Common.Enums;
using CMCS.Common.Entities.BeltSampler;
using CMCS.Common.Entities.AutoMaker;

namespace CMCS.UnloadSampler.DAO
{
    public class UnloadSamplerDAO
    {
        private static UnloadSamplerDAO instance;

        public static UnloadSamplerDAO GetInstance()
        {
            if (instance == null)
            {
                instance = new UnloadSamplerDAO();
            }

            return instance;
        }

        private UnloadSamplerDAO()
        { }

        /// <summary>
        /// 获取卸样状态
        /// </summary>
        /// <param name="output"></param>
        /// <returns></returns>
        public eEquInfCmdResultCode GetUnloadSamplerState(string UnloadSamplerId)
        {
            eEquInfCmdResultCode eResult;
            InfBeltSampleUnloadCmd SampleUnloadCmd = Dbers.GetInstance().SelfDber.Get<InfBeltSampleUnloadCmd>(UnloadSamplerId);
            if (SampleUnloadCmd != null)
            {
                if (Enum.TryParse(SampleUnloadCmd.ResultCode, out eResult))
                    return eResult;
                else
                    return eEquInfCmdResultCode.默认;
            }
            else
                return eEquInfCmdResultCode.默认;
        }

        /// <summary>
        /// 获取卸样皮带状态
        /// </summary>
        /// <param name="output"></param>
        /// <returns></returns>
        public eEquInfCmdResultCode GetPDUnloadState()
        {
            eEquInfCmdResultCode eResult;
            InfMakerUnLoad SampleUnloadCmd = Dbers.GetInstance().SelfDber.Entity<InfMakerUnLoad>("where DataFlag=3 order by createdate desc");
            if (SampleUnloadCmd != null)
            {
                if (Enum.TryParse(SampleUnloadCmd.ResultCode, out eResult))
                    return eResult;
                else
                    return eEquInfCmdResultCode.默认;
            }
            else
                return eEquInfCmdResultCode.默认;
        }
    }
}
