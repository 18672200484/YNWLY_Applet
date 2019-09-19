using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CMCS.DapperDber.Dbs.AccessDb;
// 
using CMCS.DapperDber.Dbs.OracleDb;
using CMCS.DapperDber.Dbs.SqlServerDb;
using CMCS.Common;
using CMCS.Common.DAO;
using CMCS.Common.Utilities;

namespace CMCS.DumblyConcealer
{
    /// <summary>
    /// 数据库访问
    /// </summary>
    public class DcDbers
    {
        /// <summary>
        /// 线程锁
        /// </summary>
        protected static object objLock = new object();

        private static DcDbers instance;

        public static DcDbers GetInstance()
        {
            lock (objLock)
            {
                if (instance == null)
                {
                    instance = new DcDbers();
                }

                return instance;
            }
        }

        CommonDAO commonDAO = CommonDAO.GetInstance();

        private DcDbers()
        {

            try
            {
                BuyFuel_SelfDber_IP = commonDAO.GetCommonAppletConfigString("入场煤数据库IP");
            }
            catch { }
            try
            {
                Sale_SelfDber_IP = commonDAO.GetCommonAppletConfigString("销售煤数据库IP");
            }
            catch { }
        }

        void Dber_SqlWatch(string type, string sql)
        {
            Log4Neter.Info(sql);
        }

        /// <summary>
        /// 开元化验程序数据库连接
        /// </summary>
        public SqlServerDapperDber CSKY_Clims_SelfDber;

        /// <summary>
        /// 入厂煤运输记录程序IP
        /// </summary>
        public String BuyFuel_SelfDber_IP;

        /// <summary>
        /// 销售煤运输记录程序IP
        /// </summary>
        public String Sale_SelfDber_IP;

    }
}
