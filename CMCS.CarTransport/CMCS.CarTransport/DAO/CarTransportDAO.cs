using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.DAO;
using CMCS.Common.Entities;
using CMCS.DapperDber.Dbs.OracleDb;
using CMCS.Common;
using CMCS.Common.Utilities;
using CMCS.Common.Entities.CarTransport;
using CMCS.DapperDber.Util;
using CMCS.Common.Enums;
using CMCS.Common.Views;
using CMCS.CarTransport.Views;
using CMCS.Common.Entities.Fuel;
using CMCS.Common.Entities.iEAA;
using CMCS.Common.Entities.BaseInfo;

namespace CMCS.CarTransport.DAO
{
    /// <summary>
    /// 汽车智能化业务
    /// </summary>
    public class CarTransportDAO
    {
        private static CarTransportDAO instance;

        public static CarTransportDAO GetInstance()
        {
            if (instance == null)
            {
                instance = new CarTransportDAO();
            }

            return instance;
        }

        private CarTransportDAO()
        { }

        public OracleDapperDber SelfDber
        {
            get { return Dbers.GetInstance().SelfDber; }
        }

        CommonDAO commonDAO = CommonDAO.GetInstance();

        #region 车辆管理

        /// <summary>
        /// 根据车牌号获取车辆信息
        /// </summary>
        /// <param name="carNumber"></param>
        /// <returns></returns>
        public CmcsAutotruck GetAutotruckByCarNumber(string carNumber)
        {
            return SelfDber.Entity<CmcsAutotruck>("where CarNumber=:CarNumber", new { CarNumber = carNumber });
        }

        #endregion

        #region 省份简称

        /// <summary>
        /// 获取省份简称，并按照使用次数降序
        /// </summary>
        /// <returns></returns>
        public List<CmcsProvinceAbbreviation> GetProvinceAbbreviationsInOrder()
        {
            return Dbers.GetInstance().SelfDber.Entities<CmcsProvinceAbbreviation>("order by UseCount desc");
        }

        /// <summary>
        /// 增加省份简称的使用次数
        /// </summary>
        /// <param name="paName">省份简称</param>
        public void AddProvinceAbbreviationUseCount(string paName)
        {
            Dbers.GetInstance().SelfDber.Execute("update " + EntityReflectionUtil.GetTableName<CmcsProvinceAbbreviation>() + " set UseCount=UseCount+1 where PaName=:PaName", new { PaName = paName });
        }

        #endregion

        #region 批次与采制化

        #region 入场批次

        /// <summary>
        /// 根据入场运输记录判断批次是否已生成，并返回。
        /// 根据入场时间（实际到厂时间）、供煤单位、煤种判断
        /// </summary>
        /// <param name="buyFuelTransport"></param>
        /// <returns></returns>
        public CmcsInFactoryBatch HasInFactoryBatch(CmcsBuyFuelTransport buyFuelTransport)
        {
            DateTime dt = buyFuelTransport.GrossTime.Year < 2000 ? buyFuelTransport.InFactoryTime.AddHours(-commonDAO.GetCommonAppletConfigInt32("汽车分批时间点")) : buyFuelTransport.GrossTime.AddHours(-commonDAO.GetCommonAppletConfigInt32("汽车分批时间点"));
            return SelfDber.Entity<CmcsInFactoryBatch>("where Batch like '%-'|| to_char(:CreateDate,'YYYYMMDD') ||'-%' and SupplierId=:SupplierId and MineId=:MineId and FuelKindId=:FuelKindId and InFactoryType=:InFactoryType", new { CreateDate = dt, SupplierId = buyFuelTransport.SupplierId, MineId = buyFuelTransport.MineId, FuelKindId = buyFuelTransport.FuelKindId, InFactoryType = buyFuelTransport.InFactoryType });
        }

        /// <summary>
        /// 生成制定日期的批次编码
        /// </summary>
        /// <param name="prefix">前缀</param>
        /// <param name="dtFactArriveDate">实际到厂时间</param>
        /// <returns></returns>
        public string CreateNewBatchNumber(string prefix, DateTime dtFactArriveDate)
        {
            DateTime dt = dtFactArriveDate.AddHours(-commonDAO.GetCommonAppletConfigInt32("汽车分批时间点"));
            CmcsInFactoryBatch entity = SelfDber.Entity<CmcsInFactoryBatch>("where Batch like '%-'||to_char(:CreateDate,'YYYYMMDD')||'-%' and Batch like :Prefix || '%' order by Batch desc", new { CreateDate = dt, Prefix = prefix + "-" + dt.ToString("yyyyMMdd") });

            if (entity == null)
                return string.Format("{0}-{1}-01", prefix, dt.ToString("yyyyMMdd"));
            else
                return string.Format("{0}-{1}-{2}", prefix, dt.ToString("yyyyMMdd"), (Convert.ToInt16(entity.Batch.Replace(string.Format("{0}-{1}-", prefix, dt.ToString("yyyyMMdd")), "")) + 1).ToString().PadLeft(2, '0'));
        }

        /// <summary>
        /// 根据入场煤运输记录生成批次并返回。
        /// 根据入场时间（实际到厂时间）、供煤单位、煤种生成，已存在则不创建
        /// </summary>
        /// <param name="buyFuelTransport"></param>
        /// <returns></returns>
        public CmcsInFactoryBatch GCQCInFactoryBatchByBuyFuelTransport(CmcsBuyFuelTransport buyFuelTransport, CmcsLMYB lmyb)
        {
            bool isSuccess = false;

            CmcsInFactoryBatch entity = HasInFactoryBatch(buyFuelTransport);

            if (entity == null)
            {
                //记录运输方式Id
                CodeContent transporttype = new CodeContent();
                List<CodeContent> par = commonDAO.GetCodeContentByKind("运输方式");
                if (par != null) transporttype = par.Where(a => a.Content == "汽车").FirstOrDefault();

                entity = new CmcsInFactoryBatch()
                {
                    Batch = CreateNewBatchNumber("QC", buyFuelTransport.InFactoryTime),
                    BatchType = "汽车",
                    TransportTypeId = transporttype != null ? transporttype.Id : "",
                    TransportTypeName = "汽车",
                    PlanArriveDate = buyFuelTransport.InFactoryTime,
                    FactArriveDate = buyFuelTransport.InFactoryTime,
                    FuelKindId = buyFuelTransport.FuelKindId,
                    SupplierId = buyFuelTransport.SupplierId,
                    SentSupplierId = buyFuelTransport.SupplierId,
                    SendSupplierId = buyFuelTransport.SupplierId,
                    MineId = buyFuelTransport.MineId,
                    RunDate = buyFuelTransport.InFactoryTime,
                    TransportCompanyId = buyFuelTransport.TransportCompanyId,
                    Remark = "由汽车煤智能化自动创建",
                    IsFinish = 0,
                    IsCheck = 0,
                    IsCTAutoCreate = 1,
                    IsScale = 0,
                    BACKBATCHDATE = buyFuelTransport.InFactoryTime,
                    InFactoryType = buyFuelTransport.InFactoryType
                };
                if (lmyb != null)
                {
                    entity.LMYBID = lmyb.Id;
                    entity.PlanArriveDate = lmyb.InFactoryTime;
                    entity.QCal = lmyb.Q;
                    entity.Stad = lmyb.S;
                    entity.Vad = lmyb.V;
                }
                // 创建新批次
                isSuccess = SelfDber.Insert(entity) > 0;
            }

            if (buyFuelTransport.SamplingType != eSamplingType.人工采样.ToString())
            {
                // 生成采制化数据记录
                CmcsRCSampling rCSampling = commonDAO.GCSamplingMakeAssay(entity, buyFuelTransport.SamplingType, "由汽车煤智能化自动创建", eAssayType.三级编码化验);
                buyFuelTransport.SamplingId = rCSampling.Id;
            }

            buyFuelTransport.InFactoryBatchId = entity.Id;
            return entity;
        }

        #endregion

        #region 出场批次

        /// <summary>
        /// 根据出场运输记录判断批次是否已生成，并返回。
        /// 根据入厂时间（实际到厂时间）、订单
        /// </summary>
        /// <param name="buyFuelTransport"></param>
        /// <returns></returns>
        public CmcsInFactoryBatch HasOutBatch(CmcsSaleFuelTransport SaleFuelTransport)
        {
            DateTime dt = SaleFuelTransport.InFactoryTime.AddHours(-commonDAO.GetCommonAppletConfigInt32("汽车分批时间点"));
            return SelfDber.Entity<CmcsInFactoryBatch>("where Batch like '%-'|| to_char(:CreateDate,'YYYYMMDD') ||'-%' and FuelSupplierId=:SupplierId and FuelKindId=:FuelKindId and InFactoryType=:InFactoryType", new { CreateDate = dt, SupplierId = SaleFuelTransport.SupplierId, FuelKindId = SaleFuelTransport.FuelKindId, InFactoryType = SaleFuelTransport.OutFactoryType });
        }

        /// <summary>
        /// 根据出场煤运输记录生成批次并返回。
        /// 根据入场时间（实际到厂时间）、供煤单位、煤种生成，已存在则不创建
        /// </summary>
        /// <param name="buyFuelTransport"></param>
        /// <returns></returns>
        public CmcsInFactoryBatch GCQCInFactoryBatchBySaleFuelTransport(CmcsSaleFuelTransport buyFuelTransport, CmcsLMYB lmyb)
        {
            bool isSuccess = false;

            CmcsInFactoryBatch entity = HasOutBatch(buyFuelTransport);
            if (entity == null)
            {
                //记录运输方式Id
                CodeContent transporttype = new CodeContent();
                List<CodeContent> par = commonDAO.GetCodeContentByKind("运输方式");
                if (par != null) transporttype = par.Where(a => a.Content == "汽车").FirstOrDefault();

                entity = new CmcsInFactoryBatch()
                {
                    Batch = CreateNewBatchNumber("QC", buyFuelTransport.InFactoryTime),
                    BatchType = "汽车",
                    TransportTypeId = transporttype != null ? transporttype.Id : "",
                    TransportTypeName = "汽车",
                    FactArriveDate = buyFuelTransport.InFactoryTime,
                    FuelKindId = buyFuelTransport.FuelKindId,
                    FuelSupplierId = buyFuelTransport.SupplierId,
                    SentSupplierId = buyFuelTransport.SupplierId,
                    RunDate = buyFuelTransport.InFactoryTime,
                    TransportCompanyId = buyFuelTransport.TransportCompanyId,
                    Remark = "由汽车煤智能化自动创建",
                    IsFinish = 0,
                    IsCheck = 0,
                    IsCTAutoCreate = 1,
                    IsScale = 0,
                    BACKBATCHDATE = buyFuelTransport.InFactoryTime,
                    InFactoryType = buyFuelTransport.OutFactoryType
                };
                if (lmyb != null)
                {
                    entity.LMYBID = lmyb.Id;
                    entity.PlanArriveDate = lmyb.InFactoryTime;
                    entity.QCal = lmyb.Q;
                    entity.Stad = lmyb.S;
                    entity.Vad = lmyb.V;
                }
                // 创建新批次
                isSuccess = SelfDber.Insert(entity) > 0;
            }

            if (buyFuelTransport.SampleType != eSamplingType.人工采样.ToString())
            {
                // 生成采制化数据记录
                CmcsRCSampling rCSampling = commonDAO.GCSamplingMakeAssay(entity, "机械采样", "由汽车煤智能化自动创建", eAssayType.三级编码化验);
                buyFuelTransport.SamplingId = rCSampling.Id;
            }
            buyFuelTransport.InOutBatchId = entity.Id;
            return entity;
        }
        #endregion

        #region 采样

        /// <summary>
        /// 根据采样单id获取采样单
        /// </summary>
        /// <param name="samplingId">采样单id</param>
        /// <returns></returns>
        public CmcsRCSampling GetRCSamplingById(string samplingId)
        {
            return SelfDber.Get<CmcsRCSampling>(samplingId);
        }
        #endregion

        #endregion

        #region 矿点
        /// <summary>
        /// 获取默认矿点
        /// </summary>
        /// <returns></returns>
        public CmcsMine GetDefaultMine()
        {
            return commonDAO.SelfDber.Entity<CmcsMine>("where Name=:Name", new { Name = "未知" });
        }

        /// <summary>
        /// 更新矿点
        /// </summary>
        /// <param name="video"></param>
        /// <returns></returns>
        public bool InsertMine(CmcsMine video)
        {
            CmcsMine entity = commonDAO.SelfDber.Entity<CmcsMine>("where Id=:Id", new { Id = video.Id });
            if (entity == null)
                return commonDAO.SelfDber.Insert(video) > 0;
            else
                return commonDAO.SelfDber.Update(video) > 0;
        }
        /// <summary>
        /// 删除矿点
        /// </summary>
        /// <param name="video"></param>
        /// <returns></returns>
        public bool DelMine(CmcsMine video)
        {
            int res = 0;
            foreach (var item in commonDAO.SelfDber.Entities<CmcsMine>("where ParentId=:ParentId ", new { ParentId = video.Id }))
            {
                DelMine(item);
            }
            try
            {
                if (commonDAO.SelfDber.Delete<CmcsMine>(video.Id) > 0)
                    res++;
            }
            catch (Exception)
            {
                return false;
            }
            return res > 0;
        }
        /// <summary>
        /// 根据父编码获取下级节点编码（父编码+2位逐级递增的数值）
        /// </summary>
        /// <param name="strCode"></param>
        /// <returns></returns>
        public string GetMineNewChildCode(string strCode)
        {
            string strNewCode = strCode;
            CmcsMine mine = new CmcsMine();
            if (strCode == "00")
            {
                mine = commonDAO.SelfDber.Entity<CmcsMine>("where ParentId=:ParentId order by Code desc ", new { ParentId = "-1" });
            }
            else
            {
                mine = commonDAO.SelfDber.Entity<CmcsMine>("where Code like :Code ||'%' and Code !=:Code order by Code desc ", new { Code = strCode });
            }
            if (mine != null)
            {
                strNewCode = mine.Code.Replace(strCode, "");
                strNewCode = strCode + (Convert.ToInt32(strNewCode) + 1).ToString().PadLeft(2, '0');
            }
            else
            {
                if (strCode == "00")
                    strNewCode = "0001";
                else
                {
                    strNewCode = strCode + "01";
                }
            }

            return strNewCode;
        }
        /// <summary>
        /// 获取排序号
        /// </summary>
        /// <param name="parentMine"></param>
        /// <returns></returns>
        public int GetMineOrderNumBer(CmcsMine parentMine)
        {
            CmcsMine mine = commonDAO.SelfDber.Entity<CmcsMine>(" where ParentId=:ParentId order by Sequence desc", new { ParentId = parentMine.Id });
            if (mine != null)
            {
                mine.Sequence++;
                return mine.Sequence;
            }
            return 0;
        }
        #endregion

        #region 煤种
        /// <summary>
        /// 获取默认煤种
        /// </summary>
        /// <returns></returns>
        public CmcsFuelKind GetDefaultFuelKind()
        {
            return commonDAO.SelfDber.Entity<CmcsFuelKind>("where FuelName=:FuelName", new { FuelName = "未知" });
        }

        /// <summary>
        /// 更新煤种
        /// </summary>
        /// <param name="video"></param>
        /// <returns></returns>
        public bool InsertFuelKind(CmcsFuelKind video)
        {
            CmcsFuelKind entity = commonDAO.SelfDber.Entity<CmcsFuelKind>("where Id=:Id", new { Id = video.Id });
            if (entity == null)
                return commonDAO.SelfDber.Insert(video) > 0;
            else
                return commonDAO.SelfDber.Update(video) > 0;
        }

        /// <summary>
        /// 删除煤种
        /// </summary>
        /// <param name="video"></param>
        /// <returns></returns>
        public bool DelFuelKind(CmcsFuelKind video)
        {
            int res = 0;

            foreach (var item in commonDAO.SelfDber.Entities<CmcsFuelKind>("where ParentId=:ParentId", new { ParentId = video.Id }))
            {
                DelFuelKind(item);
            }
            try
            {
                if (commonDAO.SelfDber.Delete<CmcsFuelKind>(video.Id) > 0)
                    res++;
            }
            catch (Exception)
            {
                return false;
            }
            return res > 0;
        }

        /// <summary>
        /// 根据父编码获取下级节点编码（父编码+2位逐级递增的数值）
        /// </summary>
        /// <param name="strCode"></param>
        /// <returns></returns>
        public string GetFuelKindNewChildCode(string strCode)
        {
            string strNewCode = strCode;
            CmcsFuelKind mine = new CmcsFuelKind();
            if (strCode == "00")
            {
                mine = commonDAO.SelfDber.Entity<CmcsFuelKind>("where ParentId=:ParentId order by FuelCode desc ", new { ParentId = "-1" });
            }
            else
            {
                mine = commonDAO.SelfDber.Entity<CmcsFuelKind>("where FuelCode like :Code ||'%' and Code !=:Code order by FuelCode desc ", new { Code = strCode });
            }
            if (mine != null)
            {
                strNewCode = mine.FuelCode.Replace(strCode, "");
                strNewCode = strCode + (Convert.ToInt32(strNewCode) + 1).ToString().PadLeft(2, '0');
            }
            else
            {
                if (strCode == "00")
                    strNewCode = "0001";
                else
                {
                    strNewCode = strCode + "01";
                }
            }

            return strNewCode;
        }

        /// <summary>
        /// 获取排序号
        /// </summary>
        /// <param name="parentMine"></param>
        /// <returns></returns>
        public int GetFuelOrderNumBer(CmcsFuelKind parentMine)
        {
            CmcsFuelKind mine = commonDAO.SelfDber.Entity<CmcsFuelKind>(" where ParentId=:ParentId order by Sequence desc", new { ParentId = parentMine.Id });
            if (mine != null)
            {
                mine.Sequence++;
                return mine.Sequence;
            }
            return 0;
        }
        #endregion

        #region 运输记录

        /// <summary>
        /// 根据车Id获取未完成的运输记录
        /// </summary>
        /// <param name="autotruckId">车Id</param>
        /// <returns></returns>
        public CmcsUnFinishTransport GetUnFinishTransportByAutotruckId(string autotruckId, string carType)
        {
            return SelfDber.Entity<CmcsUnFinishTransport>("where AutotruckId=:AutotruckId and CarType=:CarType order by createdate desc", new { AutotruckId = autotruckId, CarType = carType });
        }

        /// <summary>
        /// 根据运输记录Id获取未完成的运输记录
        /// </summary>
        /// <param name="autotruckId">车Id</param>
        /// <returns></returns>
        public CmcsUnFinishTransport GetUnFinishTransportByTransportId(string TransportId)
        {
            return SelfDber.Entity<CmcsUnFinishTransport>("where transportid=:transportid order by createdate desc", new { transportid = TransportId });
        }

        /// <summary>
        /// 根据车辆Id获取未完成的运输记录
        /// </summary>
        /// <param name="autotruckId">车Id</param>
        /// <returns></returns>
        public CmcsUnFinishTransport GetUnFinishTransportByAutotruckId(string AutotruckId)
        {
            return SelfDber.Entity<CmcsUnFinishTransport>("where AutotruckId=:AutotruckId", new { AutotruckId = AutotruckId });
        }

        /// <summary>
        /// 根据车牌号获取未完成的运输记录
        /// </summary>
        /// <param name="autotruckId">车牌号</param>
        /// <returns></returns>
        public List<View_UnFinishTransport> GetUnFinishTransportByCarNumber(string carNumber, string sqlWhere)
        {
            List<View_UnFinishTransport> res = SelfDber.Entities<View_UnFinishTransport>(sqlWhere);
            if (!string.IsNullOrEmpty(carNumber) && res != null)
            {
                return res.Where(a =>
                {
                    if (a.CarNumber != null && a.CarNumber.ToUpper().Contains(carNumber.ToUpper())) return true;

                    return false;
                }).ToList();
            }
            else
                return res;
        }

        /// <summary>
        /// 根据车牌号获取未完成的运输记录(准确搜索)
        /// </summary>
        /// <param name="autotruckId">车牌号</param>
        /// <returns></returns>
        public List<View_UnFinishTransport> GetUnFinishTransportByCarNumberAccurate(string carNumber, string sqlWhere)
        {
            List<View_UnFinishTransport> res = SelfDber.Entities<View_UnFinishTransport>(sqlWhere);
            if (!string.IsNullOrEmpty(carNumber))
            {
                return res.Where(a =>
                {
                    if (a.CarNumber != null && a.CarNumber.ToUpper() == (carNumber.ToUpper())) return true;

                    return false;
                }).ToList();
            }
            else
                return res;
        }

        /// <summary>
        /// 根据运输记录id查找入厂煤运输记录
        /// </summary>
        /// <param name="transportId">未完成的运输记录Id</param>
        /// <returns></returns>
        public CmcsBuyFuelTransport GetBuyFuelTransportById(string transportId)
        {
            return SelfDber.Get<CmcsBuyFuelTransport>(transportId);
        }

        /// <summary>
        /// 根据运输记录id查找出场煤运输记录
        /// </summary>
        /// <param name="transportId">未完成的运输记录Id</param>
        /// <returns></returns>
        public CmcsSaleFuelTransport GetSaleFuelTransportById(string transportId)
        {
            return SelfDber.Get<CmcsSaleFuelTransport>(transportId);
        }

        /// <summary>
        /// 将指定车的未完结运输记录强制更改为无效
        /// </summary>
        /// <param name="autotruckId">车Id</param>
        public void ChangeUnFinishTransportToInvalid(string autotruckId)
        {
            if (string.IsNullOrEmpty(autotruckId)) return;

            foreach (CmcsUnFinishTransport unFinishTransport in SelfDber.Entities<CmcsUnFinishTransport>("where AutotruckId=:AutotruckId", new { AutotruckId = autotruckId }))
            {
                if (unFinishTransport.CarType == eTransportType.原料煤入场.ToString() || unFinishTransport.CarType == eTransportType.仓储煤入场.ToString() || unFinishTransport.CarType == eTransportType.中转煤入场.ToString())
                {
                    SelfDber.Execute("Update " + EntityReflectionUtil.GetTableName<CmcsBuyFuelTransport>() + " set IsUse=0 where Id=:Id", new { Id = unFinishTransport.TransportId });
                    SelfDber.Delete<CmcsUnFinishTransport>(unFinishTransport.Id);
                }
                if (unFinishTransport.CarType == eTransportType.销售直销煤.ToString() || unFinishTransport.CarType == eTransportType.销售掺配煤.ToString() || unFinishTransport.CarType == eTransportType.仓储煤出场.ToString() || unFinishTransport.CarType == eTransportType.中转煤出场.ToString())
                {
                    SelfDber.Execute("Update " + EntityReflectionUtil.GetTableName<CmcsSaleFuelTransport>() + " set IsUse=0 where Id=:Id", new { Id = unFinishTransport.TransportId });
                    SelfDber.Delete<CmcsUnFinishTransport>(unFinishTransport.Id);
                }
                if (unFinishTransport.CarType == eTransportType.其他物资.ToString())
                {
                    SelfDber.Execute("Update " + EntityReflectionUtil.GetTableName<CmcsGoodsTransport>() + " set IsUse=0 where Id=:Id", new { Id = unFinishTransport.TransportId });
                    SelfDber.Delete<CmcsUnFinishTransport>(unFinishTransport.Id);
                }
                if (unFinishTransport.CarType == eTransportType.来访车辆.ToString())
                {
                    SelfDber.Execute("Update " + EntityReflectionUtil.GetTableName<CmcsVisitTransport>() + " set IsUse=0 where Id=:Id", new { Id = unFinishTransport.TransportId });
                    SelfDber.Delete<CmcsUnFinishTransport>(unFinishTransport.Id);
                }
            }
        }

        /// <summary>
        /// 生成运输记录流水号
        /// </summary>
        /// <param name="carType">车类型</param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public string CreateNewTransportSerialNumber(eTransportType carType, DateTime dt)
        {
            string prefix = "Null";

            if (carType == eTransportType.原料煤入场)
            {
                prefix = "YLMRC";

                CmcsBuyFuelTransport entity = SelfDber.Entity<CmcsBuyFuelTransport>("where to_char(CreateDate,'yyyymmdd')=to_char(:CreateDate,'yyyymmdd') and SerialNumber like :Prefix || '%' order by InFactoryTime desc", new { CreateDate = dt, Prefix = prefix });
                if (entity == null)
                    return prefix + dt.ToString("yyMMdd") + "001";
                else
                    return prefix + dt.ToString("yyMMdd") + (Convert.ToInt16(entity.SerialNumber.Replace(prefix + dt.ToString("yyMMdd"), "")) + 1).ToString().PadLeft(3, '0');
            }
            else if (carType == eTransportType.仓储煤入场)
            {
                prefix = "CCMRC";

                CmcsBuyFuelTransport entity = SelfDber.Entity<CmcsBuyFuelTransport>("where to_char(CreateDate,'yyyymmdd')=to_char(:CreateDate,'yyyymmdd') and SerialNumber like :Prefix || '%' order by InFactoryTime desc", new { CreateDate = dt, Prefix = prefix });
                if (entity == null)
                    return prefix + dt.ToString("yyMMdd") + "001";
                else
                    return prefix + dt.ToString("yyMMdd") + (Convert.ToInt16(entity.SerialNumber.Replace(prefix + dt.ToString("yyMMdd"), "")) + 1).ToString().PadLeft(3, '0');
            }
            else if (carType == eTransportType.中转煤入场)
            {
                prefix = "ZZMRC";

                CmcsBuyFuelTransport entity = SelfDber.Entity<CmcsBuyFuelTransport>("where to_char(CreateDate,'yyyymmdd')=to_char(:CreateDate,'yyyymmdd') and SerialNumber like :Prefix || '%' order by InFactoryTime desc", new { CreateDate = dt, Prefix = prefix });
                if (entity == null)
                    return prefix + dt.ToString("yyMMdd") + "001";
                else
                    return prefix + dt.ToString("yyMMdd") + (Convert.ToInt16(entity.SerialNumber.Replace(prefix + dt.ToString("yyMMdd"), "")) + 1).ToString().PadLeft(3, '0');
            }
            else if (carType == eTransportType.仓储煤出场)
            {
                prefix = "CCMCC";

                CmcsSaleFuelTransport entity = SelfDber.Entity<CmcsSaleFuelTransport>("where to_char(CreateDate,'yyyymmdd')=to_char(:CreateDate,'yyyymmdd') and SerialNumber like :Prefix || '%' order by InFactoryTime desc", new { CreateDate = dt, Prefix = prefix });
                if (entity == null)
                    return prefix + dt.ToString("yyMMdd") + "001";
                else
                    return prefix + dt.ToString("yyMMdd") + (Convert.ToInt16(entity.SerialNumber.Replace(prefix + dt.ToString("yyMMdd"), "")) + 1).ToString().PadLeft(3, '0');
            }
            else if (carType == eTransportType.中转煤出场)
            {
                prefix = "ZZMCC";

                CmcsSaleFuelTransport entity = SelfDber.Entity<CmcsSaleFuelTransport>("where to_char(CreateDate,'yyyymmdd')=to_char(:CreateDate,'yyyymmdd') and SerialNumber like :Prefix || '%' order by InFactoryTime desc", new { CreateDate = dt, Prefix = prefix });
                if (entity == null)
                    return prefix + dt.ToString("yyMMdd") + "001";
                else
                    return prefix + dt.ToString("yyMMdd") + (Convert.ToInt16(entity.SerialNumber.Replace(prefix + dt.ToString("yyMMdd"), "")) + 1).ToString().PadLeft(3, '0');
            }
            else if (carType == eTransportType.销售直销煤 || carType == eTransportType.销售掺配煤)
            {
                prefix = "XSM";

                CmcsSaleFuelTransport entity = SelfDber.Entity<CmcsSaleFuelTransport>("where to_char(CreateDate,'yyyymmdd')=to_char(:CreateDate,'yyyymmdd') and SerialNumber like :Prefix || '%' order by InFactoryTime desc", new { CreateDate = dt, Prefix = prefix });
                if (entity == null)
                    return prefix + dt.ToString("yyMMdd") + "001";
                else
                    return prefix + dt.ToString("yyMMdd") + (Convert.ToInt16(entity.SerialNumber.Replace(prefix + dt.ToString("yyMMdd"), "")) + 1).ToString().PadLeft(3, '0');
            }
            else if (carType == eTransportType.其他物资)
            {
                prefix = "WZ";

                CmcsGoodsTransport entity = SelfDber.Entity<CmcsGoodsTransport>("where to_char(CreateDate,'yyyymmdd')=to_char(:CreateDate,'yyyymmdd') and SerialNumber like :Prefix || '%' order by InFactoryTime desc", new { CreateDate = dt, Prefix = prefix });
                if (entity == null)
                    return prefix + dt.ToString("yyMMdd") + "001";
                else
                    return prefix + dt.ToString("yyMMdd") + (Convert.ToInt16(entity.SerialNumber.Replace(prefix + dt.ToString("yyMMdd"), "")) + 1).ToString().PadLeft(3, '0');
            }
            else if (carType == eTransportType.来访车辆)
            {
                prefix = "LF";

                CmcsVisitTransport entity = SelfDber.Entity<CmcsVisitTransport>("where to_char(CreateDate,'yyyymmdd')=to_char(:CreateDate,'yyyymmdd') and SerialNumber like :Prefix || '%' order by InFactoryTime desc", new { CreateDate = dt, Prefix = prefix });
                if (entity == null)
                    return prefix + dt.ToString("yyMMdd") + "001";
                else
                    return prefix + dt.ToString("yyMMdd") + (Convert.ToInt16(entity.SerialNumber.Replace(prefix + dt.ToString("yyMMdd"), "")) + 1).ToString().PadLeft(3, '0');
            }

            return prefix + dt.ToString("yyMMdd") + DateTime.Now.Second.ToString().PadLeft(3, '0');
        }

        /// <summary>
        /// 根据汽车入厂路线设置，判断当前是否准许通过，不通过则返回下一地点的位置
        /// </summary>
        /// <param name="carType">车类型</param>
        /// <param name="currentStepName">此车当前所处步骤</param>
        /// <param name="thisSetpName">当前地点代表的步骤</param>
        /// <param name="thisPlaceName">当前地点</param>
        /// <param name="nextPlace">返回下一地点的位置</param>
        /// <returns></returns>
        public bool CheckNextTruckInFactoryWay(string carType, string currentStepName, string thisSetpName, string thisPlaceName, out string nextPlace)
        {
            nextPlace = string.Empty;

            // 查找启用的路线，若没有启用的线路则通过
            CmcsTruckInFactoryWay truckInFactoryWay = SelfDber.Entity<CmcsTruckInFactoryWay>("where WayType=:WayType", new { WayType = carType });
            if (truckInFactoryWay == null) return true;

            // 查找当前所处的步骤
            CmcsTruckInFactoryWayDetail currentTruckInFactoryWayDetail = SelfDber.Entity<CmcsTruckInFactoryWayDetail>("where StepName=:StepName and TruckInFactoryWayId=:TruckInFactoryWayId", new { StepName = currentStepName, TruckInFactoryWayId = truckInFactoryWay.Id });
            if (currentTruckInFactoryWayDetail == null) return false;

            // 查找下一步骤
            CmcsTruckInFactoryWayDetail nextTruckInFactoryWayDetail = SelfDber.Entity<CmcsTruckInFactoryWayDetail>("where TruckInFactoryWayId=:TruckInFactoryWayId and StepNumber=:StepNumber", new { TruckInFactoryWayId = truckInFactoryWay.Id, StepNumber = currentTruckInFactoryWayDetail.StepNumber + 1 });
            if (nextTruckInFactoryWayDetail == null) return false;

            // 首先判断步骤是否符合条件
            if (!thisSetpName.Contains(nextTruckInFactoryWayDetail.StepName) || !("|" + nextTruckInFactoryWayDetail.WayPalce + "|").Contains("|" + thisPlaceName + "|"))
            {
                // 步骤不符合

                string[] nextPlaces = nextTruckInFactoryWayDetail.WayPalce.Split('|');
                nextPlace = (nextPlaces.Length > 0) ? nextPlaces[0] : string.Empty;
                return false;
            }
            //else
            //{
            //    // 步骤符合，再判断地点是否符合条件

            //    if (!("|" + nextTruckInFactoryWayDetail.WayPalce + "|").Contains("|" + thisPlaceName + "|")) return false;
            //}

            return true;
        }

        /// <summary>
        /// 删除未完成运输记录
        /// </summary>
        /// <param name="transportid"></param>
        /// <returns></returns>
        public bool DelUnFinishTransport(string transportid)
        {
            return SelfDber.DeleteBySQL<CmcsUnFinishTransport>("where TransportId=:TransportId", new { transportid }) > 0;
        }
        #endregion

        #region 其它物资

        /// <summary>
        /// 更新物资
        /// </summary>
        /// <param name="video"></param>
        /// <returns></returns>
        public bool InsertGoodsType(CmcsGoodsType video)
        {
            CmcsGoodsType entity = commonDAO.SelfDber.Entity<CmcsGoodsType>("where Id=:Id", new { Id = video.Id });
            if (entity == null)
                return commonDAO.SelfDber.Insert(video) > 0;
            else
                return commonDAO.SelfDber.Update(video) > 0;
        }


        /// <summary>
        /// 删除物资
        /// </summary>
        /// <param name="video"></param>
        /// <returns></returns>
        public bool DelGoodsType(CmcsGoodsType video)
        {
            int res = 0;
            foreach (var item in commonDAO.SelfDber.Entities<CmcsGoodsType>("where ParentId=:ParentId", new { ParentId = video.Id }))
            {
                DelGoodsType(item);
            }
            try
            {
                if (commonDAO.SelfDber.Delete<CmcsGoodsType>(video.Id) > 0)
                    res++;
            }
            catch (Exception)
            {
                return false;
            }
            return res > 0;
        }

        /// <summary>
        /// 根据父编码获取下级节点编码（父编码+2位逐级递增的数值）
        /// </summary>
        /// <param name="strCode"></param>
        /// <returns></returns>
        public string GetGoodsTypeNewChildCode(string strCode)
        {
            string strNewCode = strCode;
            CmcsGoodsType mine = new CmcsGoodsType();
            if (strCode == "00")
            {
                mine = commonDAO.SelfDber.Entity<CmcsGoodsType>("where ParentId=:ParentId order by TreeCode desc ", new { ParentId = "-1" });
            }
            else
            {
                mine = commonDAO.SelfDber.Entity<CmcsGoodsType>("where TreeCode like :TreeCode ||'%' and TreeCode !=:TreeCode order by TreeCode desc ", new { TreeCode = strCode });
            }
            if (mine != null)
            {
                strNewCode = mine.TreeCode.Replace(strCode, "");
                strNewCode = strCode + (Convert.ToInt32(strNewCode) + 1).ToString().PadLeft(2, '0');
            }
            else
            {
                if (strCode == "00")
                    strNewCode = "0001";
                else
                {
                    strNewCode = strCode + "01";
                }
            }

            return strNewCode;
        }

        /// <summary>
        /// 获取排序号
        /// </summary>
        /// <param name="parentMine"></param>
        /// <returns></returns>
        public int GetGoodsTypeOrderNumBer(CmcsGoodsType parentMine)
        {
            CmcsGoodsType goodstype = commonDAO.SelfDber.Entity<CmcsGoodsType>(" where ParentId=:ParentId order by OrderNumber desc", new { ParentId = parentMine.Id });
            if (goodstype != null)
            {
                goodstype.OrderNumber++;
                return goodstype.OrderNumber;
            }
            return 0;
        }
        #endregion
    }
}