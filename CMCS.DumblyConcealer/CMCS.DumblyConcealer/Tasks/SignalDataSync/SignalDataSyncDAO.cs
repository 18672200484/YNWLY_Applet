using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
//
using CMCS.Common;
using CMCS.Common.Entities.Sys;
using CMCS.DumblyConcealer.Tasks.CarSynchronous.Enums;
using CMCS.DapperDber.Dbs.OracleDb;
using CMCS.Common.Entities.CarTransport;
using CMCS.Common.Entities.Fuel;
using CMCS.DumblyConcealer.Enums;
using CMCS.Common.DAO;
using CMCS.Common.Entities.BaseInfo;
using CMCS.CarTransport.DAO;
using CMCS.Common.Enums;

namespace CMCS.DumblyConcealer.Tasks.SignalDataSync
{
    /// <summary>
    /// 实时信号同步
    /// </summary>
    public class SignalDataSyncDAO
    {
        private static SignalDataSyncDAO instance;

        public static SignalDataSyncDAO GetInstance()
        {
            if (instance == null)
            {
                instance = new SignalDataSyncDAO();
            }
            return instance;
        }

        CommonDAO commonDAO = CommonDAO.GetInstance();
        Action<string, eOutputType> OutPut;
        private SignalDataSyncDAO()
        { }

        /// <summary>
        /// 同步斗轮机信号
        /// </summary>
        /// <param name="output"></param>
        /// <returns></returns>
        public int SyncDLJSignal(Action<string, eOutputType> output)
        {
            int res = 0;
            DataTable data = commonDAO.SelfDber.ExecuteDataTable("select UpdateDate,StackerName,Walk from runtbstackerreal ");
            if (data != null && data.Rows.Count > 0)
            {
                foreach (DataRow item in data.Rows)
                {
                    res += commonDAO.SetSignalDataValue(item["StackerName"].ToString(), eSignalDataName.大车行走位置.ToString(), item["Walk"].ToString()) ? 1 : 0;
                }
            }

            output(string.Format("同步斗轮机实时信号{0}条", res), eOutputType.Normal);
            return res;
        }

    }
}
