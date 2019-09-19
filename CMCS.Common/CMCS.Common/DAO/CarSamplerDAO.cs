﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//
using CMCS.Common.Entities;
using CMCS.DapperDber.Util;
using System.Data;
using CMCS.Common.Enums;
using CMCS.Common.Entities.CarTransport;

namespace CMCS.Common.DAO
{
    /// <summary>
    /// 汽车采样机业务
    /// </summary>
    public class CarSamplerDAO
    {
        private static CarSamplerDAO instance;

        public static CarSamplerDAO GetInstance()
        {
            if (instance == null)
            {
                instance = new CarSamplerDAO();
            }

            return instance;
        }

        private CarSamplerDAO()
        { }

        /// <summary>
        /// 发送制样计划
        /// </summary>
        public bool SaveMakerPlan(InfQCJXCYSampleCMD entity, out string message)
        {
            try
            {
                message = "制样计划发送成功";
                if (Dbers.GetInstance().SelfDber.Insert<InfQCJXCYSampleCMD>(entity) > 0)
                    return true;
                message = "制样计划发送失败";
                return false;
            }
            catch (Exception ex)
            {
                message = "制样计划发送失败!" + ex.Message;
                return false;
            }
        }

        /// <summary>
        /// 获取待同步到第三方接口的采样命令
        /// </summary>
        /// <param name="MachineCode">设备编码</param>
        /// <returns></returns>
        public List<InfQCJXCYSampleCMD> GetWaitForSyncSampleCMD(string MachineCode)
        {
            return Dbers.GetInstance().SelfDber.Entities<InfQCJXCYSampleCMD>("where MachineCode=:MachineCode and SyncFlag=0", new { MachineCode = MachineCode });
        }

        /// <summary>
        /// 获取待同步到第三方接口的卸样命令
        /// </summary>
        /// <param name="MachineCode">设备编码</param>
        /// <returns></returns>
        public List<InfQCJXCYUnLoadCMD> GetWaitForSyncJXCYSampleUnloadCmd(string MachineCode)
        {
            return Dbers.GetInstance().SelfDber.Entities<InfQCJXCYUnLoadCMD>("where MachineCode=:MachineCode and SyncFlag=0", new { MachineCode = MachineCode });
        }

        /// <summary>
        /// 获取待同步到第三方接口的卸样命令
        /// </summary>
        /// <returns></returns>
        public List<InfQCJXCYUnLoadCMD> GetWaitForSyncJXCYSampleUnloadCmd()
        {
            return Dbers.GetInstance().SelfDber.Entities<InfQCJXCYUnLoadCMD>("where SyncFlag=0");
        }
    }
}