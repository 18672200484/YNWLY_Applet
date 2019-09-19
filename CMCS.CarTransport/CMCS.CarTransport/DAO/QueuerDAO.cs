using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.DAO;
using CMCS.Common.Entities.CarTransport;
using CMCS.DapperDber.Dbs.OracleDb;
using CMCS.Common;
using CMCS.Common.Entities;
using CMCS.Common.Views;
using CMCS.DapperDber.Util;
using CMCS.Common.Entities.BaseInfo;
using CMCS.Common.Entities.Fuel;
using CMCS.Common.Enums;

namespace CMCS.CarTransport.DAO
{
    /// <summary>
    /// 汽车入厂排队业务
    /// </summary>
    public class QueuerDAO
    {
        private static QueuerDAO instance;

        public static QueuerDAO GetInstance()
        {
            if (instance == null)
            {
                instance = new QueuerDAO();
            }

            return instance;
        }

        private QueuerDAO()
        { }

        public OracleDapperDber SelfDber
        {
            get { return Dbers.GetInstance().SelfDber; }
        }

        CommonDAO commonDAO = CommonDAO.GetInstance();
        CarTransportDAO carTransportDAO = CarTransportDAO.GetInstance();

        #region 公共业务
        /// <summary>
        /// 检测车辆是否有未完成运输记录
        /// </summary>
        /// <param name="autotruck"></param>
        /// <returns></returns>
        public bool CheckHasUnFinishTransport(CmcsAutotruck autotruck)
        {
            if (autotruck == null) return false;
            return commonDAO.SelfDber.Count<CmcsUnFinishTransport>("where AutotruckId=:AutotruckId order by CreateDate desc", new { AutotruckId = autotruck.Id }) > 0;
        }
        #endregion

        #region 入厂煤业务

        /// <summary>
        ///  生成入厂煤运输排队记录，同时生成批次信息以及采制化三级编码
        /// </summary>
        /// <param name="autotruck"></param>
        /// <param name="supplier"></param>
        /// <param name="mine"></param>
        /// <param name="transportCompany"></param>
        /// <param name="fuelKind"></param>
        /// <param name="ticketWeight"></param>
        /// <param name="inFactoryTime"></param>
        /// <param name="remark"></param>
        /// <param name="samplingType"></param>
        /// <param name="lmyb"></param>
        /// <param name="inFactoryType"></param>
        /// <returns></returns>
        public bool JoinQueueBuyFuelTransport(CmcsAutotruck autotruck, CmcsSupplier supplier, CmcsMine mine, CmcsTransportCompany transportCompany, CmcsFuelKind fuelKind, decimal ticketWeight, DateTime inFactoryTime, string remark, string samplingType, CmcsLMYB lmyb, string inFactoryType)
        {
            eTransportType TransportType;
            Enum.TryParse(inFactoryType, out TransportType);
            CmcsBuyFuelTransport transport = new CmcsBuyFuelTransport
            {
                SerialNumber = carTransportDAO.CreateNewTransportSerialNumber(TransportType, inFactoryTime),
                AutotruckId = autotruck.Id,
                CarNumber = autotruck.CarNumber,
                SupplierId = supplier.Id,
                SupplierName = supplier.Name,
                InFactoryPlace = CommonAppConfig.GetInstance().AppIdentifier,
                MineId = mine.Id,
                MineName = mine.Name,
                TransportCompanyId = (transportCompany == null ? null : transportCompany.Id),
                FuelKindId = fuelKind.Id,
                FuelKindName = fuelKind.FuelName,
                TicketWeight = ticketWeight,
                InFactoryTime = inFactoryTime,
                IsFinish = 0,
                IsUse = 1,
                SamplingType = samplingType,
                InFactoryType = inFactoryType.ToString(),
                StepName = eTruckInFactoryStep.入厂.ToString(),
                Remark = remark,
                LMYBId = lmyb != null ? lmyb.Id : "",
                YbNum = lmyb != null ? lmyb.YbNum : ""
            };

            // 生成批次以及采制化三级编码数据 
            CmcsInFactoryBatch inFactoryBatch = carTransportDAO.GCQCInFactoryBatchByBuyFuelTransport(transport, lmyb);
            if (inFactoryBatch != null)
            {
                if (SelfDber.Insert(transport) > 0)
                {
                    // 插入未完成运输记录
                    return SelfDber.Insert(new CmcsUnFinishTransport
                    {
                        TransportId = transport.Id,
                        CarType = inFactoryType.ToString(),
                        AutotruckId = autotruck.Id,
                        PrevPlace = CommonAppConfig.GetInstance().AppIdentifier,
                    }) > 0;
                }
            }

            return false;
        }

        /// <summary>
        /// 生成运输记录
        /// </summary>
        /// <param name="autotruck"></param>
        /// <param name="mine"></param>
        /// <param name="fuelKind"></param>
        /// <param name="ticketWeight"></param>
        /// <param name="inFactoryTime"></param>
        /// <param name="remark"></param>
        /// <returns></returns>
        public bool JoinQueueBuyFuelTransport(CmcsAutotruck autotruck, CmcsMine mine, CmcsFuelKind fuelKind, decimal ticketWeight, DateTime inFactoryTime, string remark, ref CmcsBuyFuelTransport transport)
        {
            transport = new CmcsBuyFuelTransport
           {
               SerialNumber = carTransportDAO.CreateNewTransportSerialNumber(eTransportType.原料煤入场, inFactoryTime),
               AutotruckId = autotruck.Id,
               CarNumber = autotruck.CarNumber,
               InFactoryPlace = CommonAppConfig.GetInstance().AppIdentifier,
               MineId = mine.Id,
               MineName = mine.Name,
               FuelKindId = fuelKind.Id,
               FuelKindName = fuelKind.FuelName,
               TicketWeight = ticketWeight,
               InFactoryTime = inFactoryTime,
               IsFinish = 0,
               IsUse = 1,
               StepName = eTruckInFactoryStep.入厂.ToString(),
               Remark = remark
           };
            CmcsInFactoryBatch inFactoryBatch = carTransportDAO.GCQCInFactoryBatchByBuyFuelTransport(transport, null);
            if (SelfDber.Insert(transport) > 0)
            {
                // 插入未完成运输记录
                return SelfDber.Insert(new CmcsUnFinishTransport
                {
                    TransportId = transport.Id,
                    CarType = eCarType.入场煤.ToString(),
                    AutotruckId = autotruck.Id,
                    PrevPlace = CommonAppConfig.GetInstance().AppIdentifier
                }) > 0;
            }
            return false;
        }

        /// <summary>
        /// 根据来煤预报生成入厂煤运输排队记录，同时生成批次信息以及采制化三级编码
        /// </summary>
        /// <param name="autotruck">车辆信息</param>
        /// <param name="lmyb">来煤预报</param>
        /// <param name="inFactoryTime">入厂时间</param>
        /// <returns></returns>
        public bool JoinQueueBuyFuelTransport(CmcsAutotruck autotruck, CmcsLMYB lmyb, DateTime inFactoryTime)
        {
            CmcsLMYBDetail lmybDetail = commonDAO.SelfDber.Entity<CmcsLMYBDetail>("where CarNumber=:CarNumber and LMYBId=:LMYBId", new { CarNumber = autotruck.CarNumber, LMYBId = lmyb.Id });
            if (lmybDetail == null) return false;
            CmcsBuyFuelTransport transport = new CmcsBuyFuelTransport
            {
                SerialNumber = carTransportDAO.CreateNewTransportSerialNumber(eTransportType.原料煤入场, inFactoryTime),
                AutotruckId = autotruck.Id,
                CarNumber = autotruck.CarNumber,
                SupplierId = lmyb.SupplierId,
                SupplierName = lmyb.SupplierName,
                InFactoryPlace = CommonAppConfig.GetInstance().AppIdentifier,
                MineId = lmyb.MineId,
                MineName = lmyb.MineName,
                TransportCompanyId = lmyb.TransportCompanyId,
                FuelKindId = lmyb.FuelKindId,
                FuelKindName = lmyb.FuelKindName,
                TicketWeight = lmybDetail.TicketWeight,
                InFactoryTime = inFactoryTime,
                IsFinish = 0,
                IsUse = 1,
                SamplingType = eSamplingType.机械采样.ToString(),
                InFactoryType = lmyb.InFactoryType,
                StepName = eTruckInFactoryStep.入厂.ToString(),
                Remark = "根据来煤预报自动生成",
            };

            // 生成批次以及采制化三级编码数据 
            CmcsInFactoryBatch inFactoryBatch = carTransportDAO.GCQCInFactoryBatchByBuyFuelTransport(transport, lmyb);
            if (inFactoryBatch != null)
            {
                if (SelfDber.Insert(transport) > 0)
                {
                    // 插入未完成运输记录
                    return SelfDber.Insert(new CmcsUnFinishTransport
                    {
                        TransportId = transport.Id,
                        CarType = eCarType.入场煤.ToString(),
                        AutotruckId = autotruck.Id,
                        PrevPlace = CommonAppConfig.GetInstance().AppIdentifier,
                    }) > 0;
                }
            }

            return false;
        }

        /// <summary>
        /// 根据来煤预报生成入厂煤运输排队记录，同时生成批次信息以及采制化三级编码
        /// </summary>
        /// <param name="autotruck">车辆信息</param>
        /// <param name="lmyb">来煤预报</param>
        /// <param name="inFactoryTime">入厂时间</param>
        /// <returns></returns>
        public bool JoinQueueBuyFuelTransport(CmcsAutotruck autotruck, CmcsLMYBDetail lmybdetail, DateTime inFactoryTime)
        {
            CmcsLMYB lmyb = lmybdetail.TheLMYB;
            if (lmyb == null) return false;
            eTransportType transportType;
            Enum.TryParse(lmyb.InFactoryType, out transportType);
            CmcsBuyFuelTransport transport = new CmcsBuyFuelTransport
            {
                SerialNumber = carTransportDAO.CreateNewTransportSerialNumber(transportType, inFactoryTime),
                AutotruckId = autotruck.Id,
                CarNumber = autotruck.CarNumber,
                SupplierId = lmyb.SupplierId,
                SupplierName = lmyb.SupplierName,
                InFactoryPlace = CommonAppConfig.GetInstance().AppIdentifier,
                MineId = lmyb.MineId,
                MineName = lmyb.MineName,
                TransportCompanyId = lmyb.TransportCompanyId,
                FuelKindId = lmyb.FuelKindId,
                FuelKindName = lmyb.FuelKindName,
                TicketWeight = lmybdetail.TicketWeight,
                InFactoryTime = inFactoryTime,
                IsFinish = 0,
                IsUse = 1,
                SamplingType = eSamplingType.机械采样.ToString(),
                InFactoryType = lmyb.InFactoryType,
                StepName = eTruckInFactoryStep.入厂.ToString(),
                Remark = "根据来煤预报自动生成",
                LMYBDetailId = lmybdetail.Id
            };

            // 生成批次以及采制化三级编码数据 
            CmcsInFactoryBatch inFactoryBatch = carTransportDAO.GCQCInFactoryBatchByBuyFuelTransport(transport, lmyb);
            if (inFactoryBatch != null)
            {
                if (SelfDber.Insert(transport) > 0)
                {
                    // 插入未完成运输记录
                    return SelfDber.Insert(new CmcsUnFinishTransport
                    {
                        TransportId = transport.Id,
                        CarType = eCarType.入场煤.ToString(),
                        AutotruckId = autotruck.Id,
                        PrevPlace = CommonAppConfig.GetInstance().AppIdentifier,
                    }) > 0;
                }
            }

            return false;
        }

        /// <summary>
        /// 获取指定日期已完成的入厂煤运输记录
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<View_BuyFuelTransport> GetFinishedBuyFuelTransport(DateTime dtStart, DateTime dtEnd)
        {
            return SelfDber.Entities<View_BuyFuelTransport>("where IsFinish=1 and InFactoryTime>=:dtStart and InFactoryTime<:dtEnd order by InFactoryTime desc", new { dtStart = dtStart, dtEnd = dtEnd });
        }

        /// <summary>
        /// 获取未完成的入厂煤运输记录
        /// </summary>
        /// <returns></returns>
        public List<View_BuyFuelTransport> GetUnFinishBuyFuelTransport()
        {
            return SelfDber.Entities<View_BuyFuelTransport>("where IsFinish=0 and IsUse=1 and UnFinishTransportId is not null order by InFactoryTime desc");
        }

        /// <summary>
        /// 获取指定日期的入厂煤运输记录
        /// </summary>
        /// <returns></returns>
        public List<CmcsBuyFuelTransport> GetBuyFuelTransportByDate(DateTime dtStart, DateTime dtEnd)
        {
            return SelfDber.Entities<CmcsBuyFuelTransport>("where InFactoryTime>=:dtStart and InFactoryTime<:dtEnd order by InFactoryTime desc", new { dtStart = dtStart, dtEnd = dtEnd });
        }

        /// <summary>
        /// 更改入厂煤运输记录的无效状态
        /// </summary>
        /// <param name="buyFuelTransportId"></param>
        /// <param name="isValid">是否有效</param>
        /// <returns></returns>
        public bool ChangeBuyFuelTransportToInvalid(string buyFuelTransportId, bool isValid)
        {
            if (isValid)
            {
                // 设置为有效
                CmcsBuyFuelTransport buyFuelTransport = SelfDber.Get<CmcsBuyFuelTransport>(buyFuelTransportId);
                if (buyFuelTransport != null)
                {
                    if (SelfDber.Execute("update " + EntityReflectionUtil.GetTableName<CmcsBuyFuelTransport>() + " set IsUse=1 where Id=:Id", new { Id = buyFuelTransportId }) > 0)
                    {
                        if (buyFuelTransport.IsFinish == 0)
                        {
                            SelfDber.Insert(new CmcsUnFinishTransport
                            {
                                AutotruckId = buyFuelTransport.AutotruckId,
                                CarType = eCarType.入场煤.ToString(),
                                TransportId = buyFuelTransport.Id,
                                PrevPlace = "未知"
                            });
                        }

                        return true;
                    }
                }
            }
            else
            {
                // 设置为无效

                if (SelfDber.Execute("update " + EntityReflectionUtil.GetTableName<CmcsBuyFuelTransport>() + " set IsUse=0 where Id=:Id", new { Id = buyFuelTransportId }) > 0)
                {
                    SelfDber.DeleteBySQL<CmcsUnFinishTransport>("where TransportId=:TransportId", new { TransportId = buyFuelTransportId });

                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// 根据车牌号获取指定到达日期的入厂煤来煤预报
        /// </summary>
        /// <param name="carNumber">车牌号</param>
        /// <param name="inFactoryTime">预计到达日期</param>
        /// <returns></returns>
        public List<CmcsLMYB> GetBuyFuelForecastByCarNumber(string carNumber, DateTime inFactoryTime)
        {
            return SelfDber.Query<CmcsLMYB>("select l.* from " + EntityReflectionUtil.GetTableName<CmcsLMYBDetail>() + " ld left join " + EntityReflectionUtil.GetTableName<CmcsLMYB>() + " l on l.Id=ld.lmybid where ld.CarNumber=:CarNumber and to_char(InFactoryTime,'yyyymmdd')=to_char(:InFactoryTime,'yyyymmdd') order by l.InFactoryTime asc",
                new { CarNumber = carNumber.Trim(), InFactoryTime = inFactoryTime }).ToList();
        }

        /// <summary>
        /// 根据车号获取预报明细
        /// </summary>
        /// <param name="carNumber"></param>
        /// <param name="inFactoryTime"></param>
        /// <returns></returns>
        public CmcsLMYBDetail GetBuyFuelForecastDetailByCarNumber(string carNumber)
        {
            return SelfDber.Entity<CmcsLMYBDetail>("where CarNumber=:CarNumber and IsFinish='未完成' order by createdate desc ", new { CarNumber = carNumber });
        }

        /// <summary>
        /// 根据车号获取预报明细
        /// </summary>
        /// <param name="carNumber"></param>
        /// <param name="inFactoryTime"></param>
        /// <returns></returns>
        public CmcsLMYB GetLMYBBySupplier(string supplierName, DateTime inFactoryTime)
        {
            return SelfDber.Entity<CmcsLMYB>("where SupplierName=:SupplierName and trunc(InFactoryTime)=trunc(:InFactoryTime) order by InFactoryTime desc ", new { SupplierName = supplierName, InFactoryTime = inFactoryTime });
        }

        /// <summary>
        /// 删除运输记录
        /// </summary>
        /// <param name="transportid"></param>
        /// <returns></returns>
        public bool DeleteTransport(string transportid)
        {
            commonDAO.SelfDber.DeleteBySQL<CmcsBuyFuelTransportDeduct>("where TransportId=:TransportId", new { TransportId = transportid });
            commonDAO.SelfDber.DeleteBySQL<CmcsTransportPicture>("where TransportId=:TransportId", new { TransportId = transportid });
            commonDAO.SelfDber.DeleteBySQL<CmcsUnFinishTransport>("where TransportId=:TransportId", new { TransportId = transportid });
            return commonDAO.SelfDber.Delete<CmcsBuyFuelTransport>(transportid) > 0;
        }
        #endregion

        #region 销售煤业务
        /// <summary>
        /// 根据车牌号获取指定到达日期的销售煤来煤预报
        /// </summary>
        /// <param name="carNumber">车牌号</param>
        /// <param name="inFactoryTime">预计到达日期</param>
        /// <returns></returns>
        public List<CmcsTransportSales> GetSaleFuelForecastByCarNumber(string carNumber, DateTime inFactoryTime)
        {
            return SelfDber.Query<CmcsTransportSales>("select l.* from " + EntityReflectionUtil.GetTableName<CmcsTransportSales>() + " l where l.transportdetails like '%" + carNumber + "%' and to_char(zcdate,'yyyymmdd')='" + inFactoryTime.ToString("yyyyMMdd") + "' and checkstatus=1 order by l.zcdate asc").ToList();
        }

        /// <summary>
        /// 根据车牌号获取指定到达日期的销售煤来煤预报
        /// </summary>
        /// <param name="carNumber">车牌号</param>
        /// <param name="inFactoryTime">预计到达日期</param>
        /// <returns></returns>
        public CmcsTransportSalesDetail GetSaleFuelForecastDetailByCarNumber(string carNumber)
        {
            return SelfDber.Entity<CmcsTransportSalesDetail>("where CarNumber=:CarNumber", new { CarNumber = carNumber });
        }

        /// <summary>
        /// 生成出场煤运输排队记录，同时生成批次信息
        /// </summary>
        /// <param name="autotruck"></param>
        /// <param name="transportsales"></param>
        /// <param name="supplyReceive"></param>
        /// <param name="company"></param>
        /// <param name="fuelKind"></param>
        /// <param name="inFactoryTime"></param>
        /// <param name="remark"></param>
        /// <param name="place"></param>
        /// <param name="loadarea"></param>
        /// <returns></returns>
        public bool JoinQueueSaleFuelTransport(CmcsAutotruck autotruck, CmcsLMYB transportsales, CmcsSupplier supplyReceive, CmcsTransportCompany company, CmcsFuelKind fuelKind, DateTime inFactoryTime, string remark, string place, string loadarea, string transportType, string sampleType, Tuple<string, string> CPC, Tuple<string, string> storageName)
        {
            eTransportType TransportType;
            Enum.TryParse(transportType, out TransportType);
            CmcsSaleFuelTransport transport = new CmcsSaleFuelTransport
            {
                SerialNumber = carTransportDAO.CreateNewTransportSerialNumber(TransportType, inFactoryTime),
                AutotruckId = autotruck.Id,
                CarNumber = autotruck.CarNumber,
                TransportSalesNum = transportsales != null ? transportsales.YbNum : "",
                TransportSalesId = transportsales != null ? transportsales.Id : "",
                TransportCompanyName = company.Name,
                TransportCompanyId = company.Id,
                TransportNo = transportsales != null ? transportsales.TransportNo : "",
                SupplierId = supplyReceive.Id,
                SupplierName = supplyReceive.Name,
                InFactoryTime = inFactoryTime,
                IsFinish = 0,
                IsUse = 1,
                StepName = eTruckInFactoryStep.入厂.ToString(),
                LoadArea = loadarea,
                Remark = remark,
                FuelKindId = fuelKind != null ? fuelKind.Id : "",
                OutFactoryType = transportType,
                SampleType = sampleType,
                CPCId = CPC.Item1,
                CPCName = CPC.Item2,
                StorageId = storageName.Item1,
                StorageName = storageName.Item2,
                LMYBId = transportsales != null ? transportsales.Id : ""
            };

            //生成批次数据 
            CmcsInFactoryBatch inOutBatch = carTransportDAO.GCQCInFactoryBatchBySaleFuelTransport(transport, transportsales);
            if (inOutBatch != null)
            {
                if (SelfDber.Insert(transport) > 0)
                {
                    // 插入未完成运输记录
                    return SelfDber.Insert(new CmcsUnFinishTransport
                    {
                        TransportId = transport.Id,
                        CarType = transportType.ToString(),
                        AutotruckId = autotruck.Id,
                        PrevPlace = place,
                    }) > 0;
                }
            }
            return false;
        }

        /// <summary>
        /// 根据来煤预报生成出场煤运输排队记录，同时生成批次信息
        /// </summary>
        /// <param name="autotruck"></param>
        /// <param name="transportsales"></param>
        /// <param name="supplyReceive"></param>
        /// <param name="company"></param>
        /// <param name="fuelKind"></param>
        /// <param name="inFactoryTime"></param>
        /// <param name="remark"></param>
        /// <param name="place"></param>
        /// <param name="loadarea"></param>
        /// <returns></returns>
        public bool JoinQueueSaleFuelTransport(CmcsAutotruck autotruck, CmcsLMYBDetail lmybdetail, DateTime inFactoryTime)
        {
            CmcsLMYB lmyb = lmybdetail.TheLMYB;
            if (lmyb == null) return false;
            eTransportType transportType;
            Enum.TryParse(lmyb.InFactoryType, out transportType);
            CmcsSaleFuelTransport transport = new CmcsSaleFuelTransport
            {
                SerialNumber = carTransportDAO.CreateNewTransportSerialNumber(transportType, inFactoryTime),
                AutotruckId = autotruck.Id,
                CarNumber = autotruck.CarNumber,
                TransportSalesNum = lmyb.YbNum,
                TransportSalesId = lmybdetail.Id,
                TransportCompanyId = lmyb.TransportCompanyId,
                TransportNo = "",
                SupplierId = lmyb.TheFuelSupplier != null ? lmyb.TheFuelSupplier.Id : "",
                InFactoryTime = inFactoryTime,
                IsFinish = 0,
                IsUse = 1,
                StepName = eTruckInFactoryStep.入厂.ToString(),
                LoadArea = lmybdetail.StorageName,
                Remark = "根据来煤预报自动生成"
            };

            //生成批次数据 
            CmcsInFactoryBatch inOutBatch = carTransportDAO.GCQCInFactoryBatchBySaleFuelTransport(transport, null);
            if (inOutBatch != null)
            {
                if (SelfDber.Insert(transport) > 0)
                {
                    // 插入未完成运输记录
                    return SelfDber.Insert(new CmcsUnFinishTransport
                    {
                        TransportId = transport.Id,
                        CarType = lmyb.InFactoryType,
                        AutotruckId = autotruck.Id,
                        PrevPlace = CommonAppConfig.GetInstance().AppIdentifier,
                    }) > 0;
                }
            }

            //if (SelfDber.Insert(transport) > 0)
            //{
            //    // 插入未完成运输记录
            //    return SelfDber.Insert(new CmcsUnFinishTransport
            //    {
            //        TransportId = transport.Id,
            //        CarType = eCarType.销售煤.ToString(),
            //        AutotruckId = autotruck.Id,
            //        PrevPlace = place,
            //    }) > 0;
            //}
            return false;
        }

        /// <summary>
        /// 获取指定日期已完成的出场煤运输记录
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<View_SaleFuelTransport> GetFinishedSaleFuelTransport(DateTime dtStart, DateTime dtEnd)
        {
            return SelfDber.Entities<View_SaleFuelTransport>("where IsFinish=1 and InFactoryTime>=:dtStart and InFactoryTime<:dtEnd order by InFactoryTime desc", new { dtStart = dtStart, dtEnd = dtEnd });
        }

        /// <summary>
        /// 获取未完成的出场煤运输记录
        /// </summary>
        /// <returns></returns>
        public List<View_SaleFuelTransport> GetUnFinishSaleFuelTransport()
        {
            return SelfDber.Entities<View_SaleFuelTransport>("where IsFinish=0 and IsUse=1 and UnFinishTransportId is not null order by InFactoryTime desc");
        }

        /// <summary>
        /// 删除运输记录
        /// </summary>
        /// <param name="transportid"></param>
        /// <returns></returns>
        public bool DeleteSaleTransport(string transportid)
        {
            commonDAO.SelfDber.DeleteBySQL<CmcsTransportPicture>("where TransportId=:TransportId", new { TransportId = transportid });
            commonDAO.SelfDber.DeleteBySQL<CmcsUnFinishTransport>("where TransportId=:TransportId", new { TransportId = transportid });
            return commonDAO.SelfDber.Delete<CmcsSaleFuelTransport>(transportid) > 0;
        }
        #endregion

        #region 其他物资业务

        /// <summary>
        /// 生成其他物资运输排队记录
        /// </summary>
        /// <param name="autotruck">车辆</param>
        /// <param name="supply">供货单位</param>
        /// <param name="receive">收货单位</param>
        /// <param name="goodsType">物资类型</param>
        /// <param name="inFactoryTime">入厂时间</param>
        /// <param name="remark">备注</param>
        /// <param name="place">地点</param>
        /// <returns></returns>
        public bool JoinQueueGoodsTransport(CmcsAutotruck autotruck, CmcsSupplyReceive supply, CmcsSupplyReceive receive, CmcsGoodsType goodsType, DateTime inFactoryTime, string remark, string place)
        {
            CmcsGoodsTransport transport = new CmcsGoodsTransport
            {
                SerialNumber = carTransportDAO.CreateNewTransportSerialNumber(eTransportType.其他物资, inFactoryTime),
                AutotruckId = autotruck.Id,
                CarNumber = autotruck.CarNumber,
                SupplyUnitId = supply.Id,
                ReceiveUnitId = receive.Id,
                GoodsTypeId = goodsType.Id,
                InFactoryTime = inFactoryTime,
                IsFinish = 0,
                IsUse = 1,
                StepName = eTruckInFactoryStep.入厂.ToString(),
                Remark = remark
            };

            if (SelfDber.Insert(transport) > 0)
            {
                // 插入未完成运输记录
                return SelfDber.Insert(new CmcsUnFinishTransport
                {
                    TransportId = transport.Id,
                    CarType = eCarType.其他物资.ToString(),
                    AutotruckId = autotruck.Id,
                    PrevPlace = place,
                }) > 0;
            }

            return false;
        }

        /// <summary>
        /// 获取指定日期已完成的其他物资运输记录
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<CmcsGoodsTransport> GetFinishedGoodsTransport(DateTime dtStart, DateTime dtEnd)
        {
            return SelfDber.Entities<CmcsGoodsTransport>("where IsFinish=1 and InFactoryTime>=:dtStart and InFactoryTime<:dtEnd order by InFactoryTime desc", new { dtStart = dtStart, dtEnd = dtEnd });
        }

        /// <summary>
        /// 获取未完成的其他物资运输记录
        /// </summary>
        /// <returns></returns>
        public List<CmcsGoodsTransport> GetUnFinishGoodsTransport()
        {
            return SelfDber.Entities<CmcsGoodsTransport>("where IsFinish=0 and IsUse=1 and Id in (select TransportId from " + EntityReflectionUtil.GetTableName<CmcsUnFinishTransport>() + " where CarType=:CarType) order by InFactoryTime desc", new { CarType = eCarType.其他物资.ToString() });
        }

        /// <summary>
        /// 更改其他物资运输记录的无效状态
        /// </summary>
        /// <param name="transportId"></param>
        /// <param name="isValid">是否有效</param>
        /// <returns></returns>
        public bool ChangeGoodsTransportToInvalid(string transportId, bool isValid)
        {
            if (isValid)
            {
                // 设置为有效
                CmcsGoodsTransport buyFuelTransport = SelfDber.Get<CmcsGoodsTransport>(transportId);
                if (buyFuelTransport != null)
                {
                    if (SelfDber.Execute("update " + EntityReflectionUtil.GetTableName<CmcsGoodsTransport>() + " set IsUse=1 where Id=:Id", new { Id = transportId }) > 0)
                    {
                        if (buyFuelTransport.IsFinish == 0)
                        {
                            SelfDber.Insert(new CmcsUnFinishTransport
                            {
                                AutotruckId = buyFuelTransport.AutotruckId,
                                CarType = eCarType.其他物资.ToString(),
                                TransportId = buyFuelTransport.Id,
                                PrevPlace = "未知"
                            });
                        }

                        return true;
                    }
                }
            }
            else
            {
                // 设置为无效

                if (SelfDber.Execute("update " + EntityReflectionUtil.GetTableName<CmcsGoodsTransport>() + " set IsUse=0 where Id=:Id", new { Id = transportId }) > 0)
                {
                    SelfDber.DeleteBySQL<CmcsUnFinishTransport>("where TransportId=:TransportId", new { TransportId = transportId });

                    return true;
                }
            }

            return false;
        }

        #endregion

        #region 来访车辆业务

        /// <summary>
        /// 生成来访车辆运输排队记录
        /// </summary>
        /// <param name="autotruck">车辆</param> 
        /// <param name="inFactoryTime">入厂时间</param>
        /// <param name="remark">备注</param>
        /// <param name="place">地点</param>
        /// <returns></returns>
        public bool JoinQueueVisitTransport(CmcsAutotruck autotruck, DateTime inFactoryTime, string remark, string place)
        {
            CmcsVisitTransport transport = new CmcsVisitTransport
            {
                SerialNumber = carTransportDAO.CreateNewTransportSerialNumber(eTransportType.来访车辆, inFactoryTime),
                AutotruckId = autotruck.Id,
                CarNumber = autotruck.CarNumber,
                InFactoryTime = inFactoryTime,
                IsFinish = 0,
                IsUse = 1,
                StepName = eTruckInFactoryStep.入厂.ToString(),
                Remark = remark
            };

            if (SelfDber.Insert(transport) > 0)
            {
                // 插入未完成运输记录
                return SelfDber.Insert(new CmcsUnFinishTransport
                {
                    TransportId = transport.Id,
                    CarType = eCarType.来访车辆.ToString(),
                    AutotruckId = autotruck.Id,
                    PrevPlace = place,
                }) > 0;
            }

            return false;
        }

        /// <summary>
        /// 获取指定日期已完成的来访车辆运输记录
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<CmcsVisitTransport> GetFinishedVisitTransport(DateTime dtStart, DateTime dtEnd)
        {
            return SelfDber.Entities<CmcsVisitTransport>("where IsFinish=1 and InFactoryTime>=:dtStart and InFactoryTime<:dtEnd order by InFactoryTime desc", new { dtStart = dtStart, dtEnd = dtEnd });
        }

        /// <summary>
        /// 获取未完成的来访车辆运输记录
        /// </summary>
        /// <returns></returns>
        public List<CmcsVisitTransport> GetUnFinishVisitTransport()
        {
            return SelfDber.Entities<CmcsVisitTransport>("where IsFinish=0 and IsUse=1 and Id in (select TransportId from " + EntityReflectionUtil.GetTableName<CmcsUnFinishTransport>() + " where CarType=:CarType) order by InFactoryTime desc", new { CarType = eCarType.来访车辆.ToString() });
        }

        /// <summary>
        /// 更改来访车辆运输记录的无效状态
        /// </summary>
        /// <param name="transportId"></param>
        /// <param name="isValid">是否有效</param>
        /// <returns></returns>
        public bool ChangeVisitTransportToInvalid(string transportId, bool isValid)
        {
            if (isValid)
            {
                // 设置为有效
                CmcsVisitTransport buyFuelTransport = SelfDber.Get<CmcsVisitTransport>(transportId);
                if (buyFuelTransport != null)
                {
                    if (SelfDber.Execute("update " + EntityReflectionUtil.GetTableName<CmcsVisitTransport>() + " set IsUse=1 where Id=:Id", new { Id = transportId }) > 0)
                    {
                        if (buyFuelTransport.IsFinish == 0)
                        {
                            SelfDber.Insert(new CmcsUnFinishTransport
                            {
                                AutotruckId = buyFuelTransport.AutotruckId,
                                CarType = eCarType.来访车辆.ToString(),
                                TransportId = buyFuelTransport.Id,
                                PrevPlace = "未知"
                            });
                        }

                        return true;
                    }
                }
            }
            else
            {
                // 设置为无效

                if (SelfDber.Execute("update " + EntityReflectionUtil.GetTableName<CmcsVisitTransport>() + " set IsUse=0 where Id=:Id", new { Id = transportId }) > 0)
                {
                    SelfDber.DeleteBySQL<CmcsUnFinishTransport>("where TransportId=:TransportId", new { TransportId = transportId });

                    return true;
                }
            }

            return false;
        }

        #endregion
    }
}
