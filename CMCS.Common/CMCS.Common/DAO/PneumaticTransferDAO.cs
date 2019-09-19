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
using CMCS.DumblyConcealer.Tasks.PneumaticTransfer.Entities;
using CMCS.Common.Entities.BaseInfo;
using CMCS.DapperDber.Dbs.SqlServerDb;

namespace CMCS.Common.DAO
{
    /// <summary>
    /// 气动传输业务
    /// </summary>
    public class PneumaticTransferDAO
    {
        private static PneumaticTransferDAO instance;

        public static PneumaticTransferDAO GetInstance()
        {
            if (instance == null)
            {
                instance = new PneumaticTransferDAO();
            }

            return instance;
        }

        private PneumaticTransferDAO()
        { }

        /// <summary>
        /// 发送气送命令
        /// </summary>
        public bool SaveQDCmd(string makeCode, CmcsCMEquipment startOpt, CmcsCMEquipment endOpt, out string message)
        {
            try
            {
                InfQDCSCmd entity = new InfQDCSCmd();
                entity.MachineCode = GlobalVars.MachineCode_QD;
                entity.MakeCode = makeCode;
                entity.OpStart = startOpt.EquipmentName;
                entity.OpStartIP = startOpt.EquipmentCode;
                entity.OpEnd = endOpt.EquipmentName;
                entity.OpEndIP = endOpt.EquipmentCode;
                entity.ResultCode = eEquInfCmdResultCode.默认.ToString();
                entity.DataFlag = 0;
                message = "气送命令发送成功";
                if (Dbers.GetInstance().SelfDber.Insert<InfQDCSCmd>(entity) > 0)
                {
                    return true;
                }
                message = "气送命令发送失败";
                return false;
            }
            catch (Exception ex)
            {
                message = "气送命令发送失败" + ex.Message;
                return false;
            }
        }

        /// <summary>
        /// 发送气送命令
        /// </summary>
        public bool SaveQDCmd(InfQDCSCmd entity, out string message)
        {
            try
            {
                message = "气送命令发送成功";
                if (Dbers.GetInstance().SelfDber.Insert<InfQDCSCmd>(entity) > 0)
                {
                    return true;
                }
                message = "气送命令发送失败";
                return false;
            }
            catch (Exception ex)
            {
                message = "气送命令发送失败" + ex.Message;
                return false;
            }
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
        /// 获取气动传输执行结果
        /// </summary>
        /// <param name="makeCode"></param>
        /// <returns></returns>
        public eEquInfCmdResultCode GetQDCSResult(string makeCode)
        {
            eEquInfCmdResultCode equInfCmdResult = eEquInfCmdResultCode.默认;
            InfQDCSCmd cmdDetail = Dbers.GetInstance().SelfDber.Entity<InfQDCSCmd>("where MakeCode=:MakeCode order by createdate desc", new { MakeCode = makeCode });
            if (cmdDetail != null)
            {
                Enum.TryParse(cmdDetail.ResultCode, out equInfCmdResult);
            }
            return equInfCmdResult;
        }

    }
}
