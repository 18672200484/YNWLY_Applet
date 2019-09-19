using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//
using CMCS.Common.Entities;
using CMCS.DapperDber.Util;
using CMCS.Common.Enums;
using CMCS.DapperDber.Dbs.OracleDb;
using System.Data;
using CMCS.Common.Utilities;
using System.Runtime.Serialization.Json;
using CMCS.Common.Entities.BaseInfo;
using CMCS.Common.Entities.Fuel;
using CMCS.Common.Entities.TrainInFactory;

namespace CMCS.Common.DAO
{
    /// <summary>
    /// 火车入厂业务
    /// </summary>
    public class TrainInFactoryDAO
    {
        private static TrainInFactoryDAO instance;

        public static TrainInFactoryDAO GetInstance()
        {
            if (instance == null)
            {
                instance = new TrainInFactoryDAO();
            }

            return instance;
        }

        private TrainInFactoryDAO()
        { }
          
        public OracleDapperDber SelfDber
        {
            get { return Dbers.GetInstance().SelfDber; }
        }

        CommonDAO commonDAO = CommonDAO.GetInstance();

        /// <summary>
        /// 根据翻车机编码获取对应关系的皮带采样机
        /// </summary>
        /// <param name="trainTipperMachineCode">翻车机编码</param>
        /// <returns></returns>
        public CmcsCMEquipment GetTrainTipperLinkBeltSampler(string trainTipperMachineCode)
        {
            string beltSamplerMachineCode = commonDAO.GetCommonAppletConfigString(trainTipperMachineCode + "对应皮带采样机");
            if (string.IsNullOrEmpty(beltSamplerMachineCode)) return null;

            return commonDAO.GetCMEquipmentByMachineCode(beltSamplerMachineCode);
        }

        /// <summary>
        /// 根据火车记录更新批次明细
        /// </summary>
        /// <param name="trainWeightRecord"></param>
        /// <returns></returns>
        public bool UpdateTransportByTrainWeightRecord(CmcsTrainWeightRecord trainWeightRecord)
        {
            CmcsTransport transport = SelfDber.Entity<CmcsTransport>("where PKID=:PKID", new { PKID = trainWeightRecord.PKID });
            if (transport != null)
            {
                transport.SkinWeight = trainWeightRecord.SkinWeight;
                transport.TareDate = trainWeightRecord.SkinTime;
                transport.OutFactoryTime = trainWeightRecord.SkinTime;
                transport.StandardWeight = trainWeightRecord.StandardWeight;
                transport.CheckQty = transport.StandardWeight;
                transport.QtyHave = transport.StandardWeight;
                transport.MarginWeight = transport.StandardWeight - transport.TicketWeight;
                return SelfDber.Update(transport) > 0 && commonDAO.UpdateBatchByTransport(transport.InFactoryBatchId);
            }
            return false;
        }
    }
}
