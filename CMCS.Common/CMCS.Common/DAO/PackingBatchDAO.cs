using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//
using CMCS.Common.Entities;
using CMCS.DapperDber.Util;
using System.Data;
using CMCS.Common.Enums;
using CMCS.Common.Entities.AutoMaker;
using CMCS.Common.Entities.Fuel;
using CMCS.Common.Entities.AutoCupboard;
using CMCS.Common.Entities.iEAA;
using CMCS.Common.Entities.PackingBatch;

namespace CMCS.Common.DAO
{
    /// <summary>
    /// 矩阵合样归批机业务
    /// </summary>
    public class PackingBatchDAO
    {
        private static PackingBatchDAO instance;

        public static PackingBatchDAO GetInstance()
        {
            if (instance == null)
            {
                instance = new PackingBatchDAO();
            }

            return instance;
        }

        private PackingBatchDAO()
        { }

        /// <summary>
        /// 根据采样码获取制样记录
        /// </summary>
        /// <param name="sampleId"></param>
        /// <returns></returns>
        public CmcsRCMake GetRCMakeBySampleCode(string sampleCode)
        {
            CmcsRCSampling rcsampling = Dbers.GetInstance().SelfDber.Entity<CmcsRCSampling>("where SampleCode=:SampleCode", new { SampleCode = sampleCode });
            if (rcsampling == null) return null;
            CmcsRCMake rcmake = Dbers.GetInstance().SelfDber.Entity<CmcsRCMake>("where SamplingId=:SamplingId", new { SamplingId = rcsampling.Id });
            return rcmake;
        }


        /// <summary>
        /// 获取待同步到第三方接口的制样计划
        /// </summary>
        /// <param name="machineCode">设备编码</param>
        /// <returns></returns>
        public List<InfMakerPlan> GetWaitForSyncMakePlan(string machineCode)
        {
            return Dbers.GetInstance().SelfDber.Entities<InfMakerPlan>("where MachineCode=:MachineCode and SyncFlag=0", new { MachineCode = machineCode });
        }

        /// <summary>
        /// 获取待同步到第三方接口的控制命令表
        /// </summary>
        /// <param name="machineCode">设备编码</param>
        /// <returns></returns>
        public List<InfMakerControlCmd> GetWaitForSyncMakerControlCmd(string machineCode)
        {
            return Dbers.GetInstance().SelfDber.Entities<InfMakerControlCmd>("where MachineCode=:MachineCode and SyncFlag=0", new { MachineCode = machineCode });
        }

        /// <summary>
        /// 获取制样出样明细
        /// </summary>
        /// <param name="makecode"></param>
        /// <returns></returns>
        public List<InfMakerRecord> GetMakerRecordByMakeCode(string makecode)
        {
            return Dbers.GetInstance().SelfDber.Entities<InfMakerRecord>("where makecode=:MakeCode order by EndTime desc", new { MakeCode = makecode });
        }

        /// <summary>
        /// 获取制样样品传输状态
        /// </summary>
        /// <param name="barrelcode">样瓶编码</param>
        /// <returns></returns>
        public string GetMakerRecordStatusByBarrelCode(string barrelcode)
        {
            InfCYGControlCMDDetail entity = Dbers.GetInstance().SelfDber.Entity<InfCYGControlCMDDetail>("where code=:BarrelCode order by CreateDate desc", new { BarrelCode = barrelcode });
            if (entity != null)
                if (entity.Status != null)
                    return entity.Status;
            return "";
        }

        /// <summary>
        /// 获取气动传输传输样品信息
        /// </summary>
        /// <param name="dTime"></param>
        /// <returns></returns>
        public List<InfCYGControlCMDDetail> GetCYGControlCMDDetailByTime(DateTime dTime)
        {
            return Dbers.GetInstance().SelfDber.Entities<InfCYGControlCMDDetail>("where CreateDate like '%" + dTime.ToString("yyyy-MM-dd") + "%' order by CreateDate desc");
        }

        /// <summary>
        /// 获取待同步到第三方接口的倒料命令
        /// </summary>
        /// <returns></returns>
        public List<InfPackingBatchCmd> GetWaitForSyncJXCYPackingBatchCmd()
        {
            return Dbers.GetInstance().SelfDber.Entities<InfPackingBatchCmd>("where SyncFlag=0");
        }

        /// <summary>
        /// 发送倒料命令
        /// </summary>
        /// <param name="rCSamplingCode"></param>
        /// <returns></returns>
        public bool SendPackingBatch(string machineCode, string rCSamplingCode, out string currentMessage)
        {
            CmcsRCMake rcMake = AutoMakerDAO.GetInstance().GetRCMakeBySampleCode(rCSamplingCode);
            if (rcMake != null)
            {
                string fuelKindName = string.Empty;

                InfPackingBatchCmd packingbatch = new InfPackingBatchCmd()
                {
                    InterfaceType = CommonDAO.GetInstance().GetMachineInterfaceTypeByCode(machineCode),
                    MachineCode = machineCode,
                    SampleCode = rCSamplingCode,
                    MakeCode = rcMake.MakeCode,
                    ResultCode = eEquInfCmdResultCode.默认.ToString(),
                    SyncFlag = 0,
                    DataFlag = 0
                };
                if (CommonDAO.GetInstance().SelfDber.Insert(packingbatch) > 0)
                {
                    currentMessage = "倒料命令发送成功";
                    return true;
                }
                else
                {
                    currentMessage = "倒料命令发送失败";
                    return false;
                }
            }
            else
            {
                currentMessage = "未找到制样主记录信息";
                return false;
            }
        }

        /// <summary>
        /// 获取倒料状态
        /// </summary>
        /// <param name="output"></param>
        /// <returns></returns>
        public eEquInfCmdResultCode GetQCJXPackingBatchState(string samplingCode)
        {
            eEquInfCmdResultCode eResult;
            InfPackingBatchCmd SampleUnloadCmd = Dbers.GetInstance().SelfDber.Entity<InfPackingBatchCmd>("where SampleCode=:SampleCode order by CreateDate desc", new { SampleCode = samplingCode });
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
        /// 保存归批机样桶信息
        /// </summary>
        /// <param name="coord"></param>
        /// <returns></returns>
        public bool SaveCoord(InfPackingBatchCoord coord)
        {
            InfPackingBatchCoord batchCoord = Dbers.GetInstance().SelfDber.Entity<InfPackingBatchCoord>("where Coord=:Coord and MachineCode=:MachineCode order by Coord", new { Coord = coord.Coord, MachineCode = coord.MachineCode });
            if (batchCoord == null)
            {
                return Dbers.GetInstance().SelfDber.Insert(coord) > 0;
            }
            else
            {
                batchCoord.UpdateTime = coord.UpdateTime;
                batchCoord.SampleCode = coord.SampleCode;
                batchCoord.State = coord.State;
                batchCoord.SyncFlag = 0;
                return Dbers.GetInstance().SelfDber.Update(batchCoord) > 0;
            }
        }
    }
}
