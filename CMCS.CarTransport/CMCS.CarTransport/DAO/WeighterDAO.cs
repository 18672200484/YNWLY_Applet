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
using CMCS.Common.Enums;
using CMCS.Common.Utilities;
using CMCS.Common.Entities.Fuel;

namespace CMCS.CarTransport.DAO
{
    /// <summary>
    /// 汽车过衡业务
    /// </summary>
    public class WeighterDAO
    {
        private static WeighterDAO instance;

        public static WeighterDAO GetInstance()
        {
            if (instance == null)
            {
                instance = new WeighterDAO();
            }

            return instance;
        }

        private WeighterDAO()
        { }

        public OracleDapperDber SelfDber
        {
            get { return Dbers.GetInstance().SelfDber; }
        }

        CommonDAO commonDAO = CommonDAO.GetInstance();
        CarTransportDAO carTransportDAO = CarTransportDAO.GetInstance();

        #region 入厂煤业务

        /// <summary>
        /// 获取指定日期已完成的入厂煤运输记录
        /// </summary>
        /// <param name="dtStart"></param>
        /// <param name="dtEnd"></param>
        /// <returns></returns>
        public List<View_BuyFuelTransport> GetFinishedBuyFuelTransport(DateTime dtStart, DateTime dtEnd)
        {
            return SelfDber.Entities<View_BuyFuelTransport>("where SuttleWeight!=0 and TareTime>=:dtStart and TareTime<:dtEnd order by TareTime desc", new { dtStart = dtStart, dtEnd = dtEnd });
        }

        /// <summary>
        /// 获取未完成的入厂煤运输记录
        /// </summary>
        /// <returns></returns>
        public List<View_BuyFuelTransport> GetUnFinishBuyFuelTransport()
        {
            return SelfDber.Entities<View_BuyFuelTransport>("where SuttleWeight=0 and IsUse=1 and UnFinishTransportId is not null order by InFactoryTime desc");
        }

        /// <summary>
        /// 保存入厂煤运输记录
        /// </summary>
        /// <param name="transportId"></param>
        /// <param name="weight">重量</param>
        /// <param name="place"></param>
        /// <returns></returns>
        public bool SaveBuyFuelTransport(string transportId, decimal weight, DateTime dt, string place)
        {
            CmcsBuyFuelTransport transport = SelfDber.Get<CmcsBuyFuelTransport>(transportId);
            if (transport == null) return false;
            transport.IsSynch = "0";

            if (transport.GrossWeight == 0)
            {
                transport.StepName = eTruckInFactoryStep.重车.ToString();
                transport.GrossWeight = weight;
                transport.GrossPlace = place;
                transport.GrossTime = dt;
                Log4Neter.Info(string.Format("车牌号:{0} 毛重:{1}", transport.CarNumber, transport.GrossWeight));
            }
            else if (transport.TareWeight == 0)
            {
                transport.StepName = eTruckInFactoryStep.轻车.ToString();
                transport.TareWeight = weight;
                transport.TarePlace = place;
                transport.TareTime = dt;
                transport.SuttleWeight = transport.GrossWeight - transport.TareWeight;
                ////验收量大于票重时多余的量算到扣吨
                if (transport.TicketWeight != 0)
                {
                    decimal deduct = transport.SuttleWeight > transport.TicketWeight ? (transport.SuttleWeight - transport.TicketWeight) : 0;
                    decimal letterdeduct = 0;//抹去的小数位
                    //transport.SuttleWeight -= deduct;
                    transport.CheckWeight = OneDigit(transport.SuttleWeight - deduct - transport.KsWeight - transport.KgWeight, ref letterdeduct);
                    deduct += letterdeduct;
                    transport.AutoKsWeight = deduct;
                    transport.DeductWeight = transport.AutoKsWeight + transport.KsWeight + transport.KgWeight;
                }
                else
                {
                    transport.DeductWeight = transport.KsWeight + transport.KgWeight;
                    transport.CheckWeight = transport.SuttleWeight - transport.DeductWeight;
                }
                transport.ProfitAndLossWeight = transport.CheckWeight - transport.TicketWeight;

                // 回皮即完结
                transport.IsFinish = 1;
                carTransportDAO.DelUnFinishTransport(transport.Id);
                commonDAO.InsertWaitForHandleEvent("汽车智能化_同步入厂煤运输记录到批次", transport.Id);
                commonDAO.InsertWaitForHandleEvent("汽车智能化_删除未完成运输记录", transport.Id);
                Log4Neter.Info(string.Format("车牌号:{0} 毛重:{1} 皮重:{2} 扣重:{3}", transport.CarNumber, transport.GrossWeight, transport.TareWeight, transport.DeductWeight));

                if (IsOverCalcWaylose(transport.SuttleWeight, transport.TicketWeight))
                {
                    commonDAO.SaveSysMessage(place, string.Format("车牌号:{0}净重异常", transport.CarNumber));
                }
            }
            else
                return false;
            return SelfDber.Update(transport) > 0;
        }


        /// <summary>
        /// 手动保存入厂煤运输记录
        /// </summary>
        /// <param name="transportId"></param>
        /// <param name="weight">重量</param>
        /// <param name="deductweight">扣重</param>
        /// <param name="place"></param>
        /// <returns></returns>
        public bool SaveBuyFuelTransportHand(CmcsBuyFuelTransport transport, decimal weight, DateTime dt, string place)
        {
            if (transport == null) return false;
            transport.IsSynch = "0";

            if (transport.GrossWeight == 0)
            {
                transport.StepName = eTruckInFactoryStep.重车.ToString();
                transport.GrossWeight = weight;
                transport.GrossPlace = place;
                transport.GrossTime = dt;
                // 生成批次以及采制化三级编码数据 
                CmcsInFactoryBatch inFactoryBatch = CarTransportDAO.GetInstance().GCQCInFactoryBatchByBuyFuelTransport(transport, null);
            }
            else if (transport.TareWeight == 0)
            {
                transport.StepName = eTruckInFactoryStep.轻车.ToString();
                transport.TareWeight = weight;
                transport.TarePlace = place;
                transport.TareTime = dt;
                transport.OutFactoryTime = dt;
                transport.SuttleWeight = transport.GrossWeight - transport.TareWeight;
                ////验收量大于票重时多余的量算到扣吨
                if (transport.TicketWeight != 0)
                {
                    decimal deduct = transport.SuttleWeight > transport.TicketWeight ? (transport.SuttleWeight - transport.TicketWeight) : 0;
                    decimal letterdeduct = 0;//抹去的小数位
                    //transport.SuttleWeight -= deduct;
                    transport.CheckWeight = OneDigit(transport.SuttleWeight - deduct - transport.KsWeight - transport.KgWeight, ref letterdeduct);
                    deduct += letterdeduct;
                    transport.AutoKsWeight = deduct;
                    transport.DeductWeight = transport.AutoKsWeight + transport.KsWeight + transport.KgWeight;
                }
                else
                {
                    transport.DeductWeight = transport.KsWeight + transport.KgWeight;
                    transport.CheckWeight = transport.SuttleWeight - transport.DeductWeight;
                }
                transport.ProfitAndLossWeight = transport.CheckWeight - transport.TicketWeight;

                // 回皮即完结
                transport.IsFinish = 1;
                carTransportDAO.DelUnFinishTransport(transport.Id);
                commonDAO.InsertWaitForHandleEvent("汽车智能化_同步入厂煤运输记录到批次", transport.Id);
                commonDAO.InsertWaitForHandleEvent("汽车智能化_删除未完成运输记录", transport.Id);
            }
            else
                return false;

            return SelfDber.Update(transport) > 0;
        }

        /// <summary>
        /// 保存重量
        /// </summary>
        /// <param name="transport"></param>
        /// <returns></returns>
        public bool SaveBuyFuelTransport(CmcsBuyFuelTransport transport)
        {
            if (transport == null) return false;
            transport.IsSynch = "0";

            if (transport.GrossWeight > 0)
            {
                if (transport.GrossTime.Year < 2000) { transport.GrossTime = DateTime.Now; transport.InFactoryTime = DateTime.Now; }
                if (transport.TareWeight > 0)
                {
                    if (transport.TareTime.Year < 2000) transport.TareTime = DateTime.Now;
                    if (transport.OutFactoryTime.Year < 2000) transport.OutFactoryTime = DateTime.Now;

                    transport.SuttleWeight = transport.GrossWeight - transport.TareWeight;
                    ////验收量大于票重时多余的量算到扣吨
                    if (transport.TicketWeight != 0)
                    {
                        decimal deduct = transport.SuttleWeight > transport.TicketWeight ? (transport.SuttleWeight - transport.TicketWeight) : 0;
                        decimal letterdeduct = 0;//抹去的小数位
                        //transport.SuttleWeight -= deduct;
                        transport.CheckWeight = OneDigit(transport.SuttleWeight - deduct - transport.KsWeight - transport.KgWeight, ref letterdeduct);
                        deduct += letterdeduct;
                        transport.AutoKsWeight = deduct;
                        transport.DeductWeight = transport.AutoKsWeight + transport.KsWeight + transport.KgWeight;
                    }
                    else
                    {
                        transport.AutoKsWeight = 0;
                        transport.DeductWeight = transport.KsWeight + transport.KgWeight;
                        transport.CheckWeight = transport.SuttleWeight - transport.DeductWeight;
                    }
                    transport.ProfitAndLossWeight = transport.CheckWeight - transport.TicketWeight;

                    // 回皮即完结
                    transport.IsFinish = 1;
                    if (SelfDber.Get<CmcsBuyFuelTransport>(transport.Id) == null)
                    {
                        if (SelfDber.Insert(transport) > 0)
                        {
                            carTransportDAO.DelUnFinishTransport(transport.Id);
                            commonDAO.InsertWaitForHandleEvent("汽车智能化_同步入厂煤运输记录到批次", transport.Id);
                            commonDAO.InsertWaitForHandleEvent("汽车智能化_删除未完成运输记录", transport.Id);
                            return true;
                        }
                    }
                    else
                    {
                        if (SelfDber.Update(transport) > 0)
                        {
                            carTransportDAO.DelUnFinishTransport(transport.Id);
                            commonDAO.InsertWaitForHandleEvent("汽车智能化_同步入厂煤运输记录到批次", transport.Id);
                            commonDAO.InsertWaitForHandleEvent("汽车智能化_删除未完成运输记录", transport.Id);
                            return true;
                        }
                    }
                }
            }
            return SelfDber.Update(transport) > 0;
        }
        /// <summary>
        /// 舍去第二位小数，无论大小
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        decimal OneDigit(decimal value, ref decimal deductvalue)
        {

            decimal result = Math.Floor(value * 10) / 10m;
            deductvalue = value - result;
            return result;
        }

        /// <summary>
        /// 计算路损
        /// </summary>
        /// <param name="suttleWeight">净重</param>
        /// <param name="tickeWeight">票重</param>
        /// <returns></returns>
        public decimal CalcWaylose(decimal suttleWeight, decimal tickeWeight)
        {
            // 系数=0.012
            // 当 净重>=票重，路损=0
            // 当 净重<票重&&(票重-净重)>=票重*路损系数，路损=票重*路损系数
            // 当 净重<票重&&(票重-净重)<票重*路损系数，路损=票重-净重  

            decimal wayLose = 0, xishu = 0.012m;
            try
            {
                xishu = commonDAO.GetCommonAppletConfigDecimal("路损系数");
            }
            catch { }
            if (suttleWeight > tickeWeight)
                wayLose = 0;
            else if (suttleWeight < tickeWeight && (tickeWeight - suttleWeight) >= tickeWeight * xishu)
                wayLose = tickeWeight * xishu;
            else if (suttleWeight < tickeWeight && (tickeWeight - suttleWeight) < tickeWeight * xishu)
                wayLose = tickeWeight - suttleWeight;

            return Math.Round(wayLose, 2);
        }

        /// <summary>
        /// 路损是否过大
        /// </summary>
        /// <param name="suttleWeight"></param>
        /// <param name="tickeWeight"></param>
        /// <returns></returns>
        public bool IsOverCalcWaylose(decimal suttleWeight, decimal tickeWeight)
        {
            decimal xishu = 0.012m;
            try
            {
                xishu = commonDAO.GetCommonAppletConfigDecimal("路损系数");
            }
            catch { }
            if (suttleWeight > tickeWeight)
                return false;
            else if (suttleWeight < tickeWeight && (tickeWeight - suttleWeight) >= tickeWeight * xishu)
                return true;
            else if (suttleWeight < tickeWeight && (tickeWeight - suttleWeight) < tickeWeight * xishu)
                return false;
            return false;
        }
        #endregion

        #region 销售煤业务

        /// <summary>
        /// 获取指定日期已完成的销售煤运输记录
        /// </summary>
        /// <param name="dtStart"></param>
        /// <param name="dtEnd"></param>
        /// <returns></returns>
        public List<View_SaleFuelTransport> GetFinishedSaleFuelTransport(DateTime dtStart, DateTime dtEnd)
        {
            return SelfDber.Entities<View_SaleFuelTransport>("where SuttleWeight!=0 and InFactoryTime>=:dtStart and InFactoryTime<:dtEnd order by InFactoryTime desc", new { dtStart = dtStart, dtEnd = dtEnd });
        }

        /// <summary>
        /// 获取未完成的销售煤运输记录
        /// </summary>
        /// <returns></returns>
        public List<View_SaleFuelTransport> GetUnFinishSaleFuelTransport()
        {
            return SelfDber.Entities<View_SaleFuelTransport>("where SuttleWeight=0 and IsUse=1 and UnFinishTransportId is not null order by InFactoryTime desc");
        }

        /// <summary>
        /// 保存销售煤运输记录
        /// </summary>
        /// <param name="transportId"></param>
        /// <param name="weight">重量</param>
        /// <param name="place"></param>
        /// <returns></returns>
        public bool SaveSaleFuelTransport(string transportId, decimal weight, DateTime dt, string place)
        {
            CmcsSaleFuelTransport transport = SelfDber.Get<CmcsSaleFuelTransport>(transportId);
            if (transport == null) return false;
            transport.IsSynch = "0";

            if (transport.TareWeight == 0)
            {
                transport.StepName = eTruckInFactoryStep.轻车.ToString();
                transport.TareWeight = weight;
                transport.TarePlace = place;
                transport.TareTime = dt;
            }
            else if (transport.GrossWeight == 0)
            {
                transport.StepName = eTruckInFactoryStep.重车.ToString();
                transport.GrossWeight = weight;
                transport.GrossPlace = place;
                transport.GrossTime = dt;
                transport.OutFactoryTime = dt;
                transport.SuttleWeight = transport.GrossWeight - transport.TareWeight;

                // 回皮即完结
                transport.IsFinish = 1;
                carTransportDAO.DelUnFinishTransport(transport.Id);
                commonDAO.InsertWaitForHandleEvent("汽车智能化_同步销售煤运输记录到批次", transport.Id);
                commonDAO.InsertWaitForHandleEvent("汽车智能化_删除未完成运输记录", transport.Id);
            }
            else
                return false;

            return SelfDber.Update(transport) > 0;
        }

        /// <summary>
        /// 保存重量
        /// </summary>
        /// <param name="transport"></param>
        /// <returns></returns>
        public bool SaveSaleFuelTransport(CmcsSaleFuelTransport transport)
        {
            if (transport == null) return false;
            transport.IsSynch = "0";

            // 生成批次以及采制化三级编码数据 
            CmcsInFactoryBatch inFactoryBatch = CarTransportDAO.GetInstance().GCQCInFactoryBatchBySaleFuelTransport(transport, null);
            if (transport.GrossWeight > 0)
            {
                if (transport.GrossTime.Year < 2000) { transport.GrossTime = DateTime.Now; transport.InFactoryTime = DateTime.Now; }
                if (transport.TareWeight > 0)
                {
                    if (transport.TareTime.Year < 2000) transport.TareTime = DateTime.Now;
                    if (transport.OutFactoryTime.Year < 2000) transport.OutFactoryTime = DateTime.Now;

                    transport.SuttleWeight = transport.GrossWeight - transport.TareWeight;

                    // 回皮即完结
                    transport.IsFinish = 1;
                    if (SelfDber.Get<CmcsSaleFuelTransport>(transport.Id) == null)
                    {
                        if (SelfDber.Insert(transport) > 0)
                        {
                            carTransportDAO.DelUnFinishTransport(transport.Id);
                            commonDAO.InsertWaitForHandleEvent("汽车智能化_同步销售煤运输记录到批次", transport.Id);
                            commonDAO.InsertWaitForHandleEvent("汽车智能化_删除未完成运输记录", transport.Id);
                            return true;
                        }
                    }
                    else
                    {
                        if (SelfDber.Update(transport) > 0)
                        {
                            carTransportDAO.DelUnFinishTransport(transport.Id);
                            commonDAO.InsertWaitForHandleEvent("汽车智能化_同步销售煤运输记录到批次", transport.Id);
                            commonDAO.InsertWaitForHandleEvent("汽车智能化_删除未完成运输记录", transport.Id);
                            return true;
                        }
                    }
                }
            }
            return SelfDber.Update(transport) > 0;
        }
        #endregion

        #region 其他物资业务

        /// <summary>
        /// 获取指定日期已完成的其他物资运输记录
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public List<CmcsGoodsTransport> GetFinishedGoodsTransport(DateTime dtStart, DateTime dtEnd)
        {
            return SelfDber.Entities<CmcsGoodsTransport>("where SuttleWeight>0 and InFactoryTime>=:dtStart and InFactoryTime<:dtEnd order by InFactoryTime desc", new { dtStart = dtStart, dtEnd = dtEnd });
        }

        /// <summary>
        /// 获取未完成的其他物资运输记录
        /// </summary>
        /// <returns></returns>
        public List<CmcsGoodsTransport> GetUnFinishGoodsTransport()
        {
            return SelfDber.Entities<CmcsGoodsTransport>("where SuttleWeight=0 and IsUse=1 and Id in (select TransportId from " + EntityReflectionUtil.GetTableName<CmcsUnFinishTransport>() + " where CarType=:CarType) order by InFactoryTime desc", new { CarType = eCarType.其他物资.ToString() });
        }

        /// <summary>
        /// 保存其他物资运输记录
        /// </summary>
        /// <param name="transportId"></param>
        /// <param name="weight">重量</param>
        /// <param name="place"></param>
        /// <returns></returns>
        public bool SaveGoodsTransport(string transportId, decimal weight, DateTime dt, string place)
        {
            CmcsGoodsTransport transport = SelfDber.Get<CmcsGoodsTransport>(transportId);
            if (transport == null) return false;
            transport.IsSynch = "0";

            if (transport.FirstWeight == 0)
            {
                transport.StepName = eTruckInFactoryStep.重车.ToString();
                transport.FirstWeight = weight;
                transport.FirstPlace = place;
                transport.FirstTime = dt;
            }
            else if (transport.SecondWeight == 0)
            {
                transport.StepName = eTruckInFactoryStep.轻车.ToString();
                transport.SecondWeight = weight;
                transport.SecondPlace = place;
                transport.SecondTime = dt;
                transport.SuttleWeight = Math.Abs(transport.FirstWeight - transport.SecondWeight);

                // 回皮即完结
                transport.IsFinish = 1;
            }
            else
                return false;

            return SelfDber.Update(transport) > 0;
        }


        #endregion
    }
}
