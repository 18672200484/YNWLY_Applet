using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common;
using CMCS.Common.Entities.Sys;
using CMCS.DumblyConcealer.Tasks.CarSynchronous.Enums;
using CMCS.DapperDber.Dbs.OracleDb;
using CMCS.Common.Entities.CarTransport;
using CMCS.Common.Entities.Fuel;
using CMCS.DumblyConcealer.Enums;
using CMCS.Common.DAO;
using CMCS.Common.Entities.BaseInfo;
using System.Data;
using CMCS.CarTransport.DAO;

namespace CMCS.DumblyConcealer.Tasks.CarSynchronous
{
    /// <summary>
    /// 综合事件处理
    /// </summary>
    public class DataHandlerDAO
    {
        private static DataHandlerDAO instance;

        public static DataHandlerDAO GetInstance()
        {
            if (instance == null)
            {
                instance = new DataHandlerDAO();
            }
            return instance;
        }

        CommonDAO commonDAO = CommonDAO.GetInstance();
        Action<string, eOutputType> OutPut;
        private DataHandlerDAO()
        { }

        /// <summary>
        /// 开始处理
        /// </summary> 
        /// <returns></returns>
        public void Start(Action<string, eOutputType> output)
        {
            this.OutPut = output;
            foreach (CmcsWaitForHandleEvent item in commonDAO.SelfDber.Entities<CmcsWaitForHandleEvent>("where DataFlag=0"))
            {
                bool isSuccess = false;

                eEventCode eventCode;
                bool a = Enum.TryParse<eEventCode>(item.EventCode, out eventCode);
                if (!Enum.TryParse<eEventCode>(item.EventCode, out eventCode)) continue;

                switch (eventCode)
                {
                    case eEventCode.汽车智能化_同步销售煤运输记录到批次:

                        if (SyncToOutBatch(output, item.ObjectId))
                        {
                            isSuccess = true;

                            output(string.Format("事件：{0}  ObjectId：{1}", eEventCode.汽车智能化_同步销售煤运输记录到批次.ToString(), item.ObjectId), eOutputType.Normal);
                        }

                        break;
                    case eEventCode.汽车智能化_同步入厂煤运输记录到批次:

                        if (SyncToBatch(output, item.ObjectId))
                        {
                            isSuccess = true;

                            output(string.Format("事件：{0}  ObjectId：{1}", eEventCode.汽车智能化_同步入厂煤运输记录到批次.ToString(), item.ObjectId), eOutputType.Normal);
                        }
                        break;
                    case eEventCode.汽车智能化_删除未完成运输记录:
                        if (SyncDelUnFinishTransport(output, item.ObjectId))
                        {
                            isSuccess = true;

                            output(string.Format("事件：{0}  ObjectId：{1}", eEventCode.汽车智能化_删除未完成运输记录.ToString(), item.ObjectId), eOutputType.Normal);
                        }
                        break;
                    case eEventCode.汽车智能化_删除入厂煤运输记录:
                        if (SyncDelBuyFuelTransport(output, item.ObjectId))
                        {
                            isSuccess = true;

                            output(string.Format("事件：{0}  ObjectId：{1}", eEventCode.汽车智能化_删除入厂煤运输记录.ToString(), item.ObjectId), eOutputType.Normal);
                        }
                        break;
                    case eEventCode.汽车智能化_删除出厂煤运输记录:
                        if (SyncDelSalesTransport(output, item.ObjectId))
                        {
                            isSuccess = true;

                            output(string.Format("事件：{0}  ObjectId：{1}", eEventCode.汽车智能化_删除出厂煤运输记录.ToString(), item.ObjectId), eOutputType.Normal);
                        }

                        break;
                }

                if (isSuccess)
                {
                    item.DataFlag = 1;
                    commonDAO.SelfDber.Update(item);
                }
            }
        }

        /// <summary>
        /// 删除未完成运输记录
        /// </summary>
        /// <param name="transportId">汽车入厂煤运输记录Id</param>
        /// <returns></returns>
        private bool SyncDelUnFinishTransport(Action<string, eOutputType> output, string transportId)
        {
            bool res = false;
            CmcsUnFinishTransport transport = commonDAO.SelfDber.Entity<CmcsUnFinishTransport>("where TransportId=:TransportId order by createdate desc", new { TransportId = transportId });
            if (transport == null) return true;
            res = commonDAO.SelfDber.Delete<CmcsUnFinishTransport>(transport.Id) > 0;
            return res;
        }

        /// <summary>
        /// 删除入厂煤运输记录
        /// </summary>
        /// <param name="transportId">汽车入厂煤运输记录Id</param>
        /// <returns></returns>
        private bool SyncDelBuyFuelTransport(Action<string, eOutputType> output, string transportId)
        {
            bool res = false;
            CmcsUnFinishTransport transport = commonDAO.SelfDber.Entity<CmcsUnFinishTransport>("where TransportId=:TransportId order by createdate desc", new { TransportId = transportId });
            if (transport != null)
            {
                res = commonDAO.SelfDber.Delete<CmcsUnFinishTransport>(transport.Id) > 0;
            }
            res = commonDAO.SelfDber.Delete<CmcsBuyFuelTransport>(transportId) > 0;
            return res;
        }

        /// <summary>
        /// 删除出厂煤运输记录
        /// </summary>
        /// <param name="transportId">汽车出厂煤运输记录Id</param>
        /// <returns></returns>
        private bool SyncDelSalesTransport(Action<string, eOutputType> output, string transportId)
        {
            bool res = true;
            CmcsUnFinishTransport transport = commonDAO.SelfDber.Entity<CmcsUnFinishTransport>("where TransportId=:TransportId order by createdate desc", new { TransportId = transportId });
            if (transport != null)
            {
                res = commonDAO.SelfDber.Delete<CmcsUnFinishTransport>(transport.Id) > 0;
            }
            res = commonDAO.SelfDber.Delete<CmcsSaleFuelTransport>(transportId) > 0;
            return res;
        }

        /// <summary>
        /// 将汽车入厂煤运输记录同步到批次明细中
        /// </summary>
        /// <param name="transportId">汽车入厂煤运输记录Id</param>
        /// <returns></returns>
        private bool SyncToBatch(Action<string, eOutputType> output, string transportId)
        {
            bool res = false;
            CmcsBuyFuelTransport transport = commonDAO.SelfDber.Get<CmcsBuyFuelTransport>(transportId);
            if (transport == null || transport.IsFinish == 0) return false;

            CmcsInFactoryBatch batch = commonDAO.SelfDber.Get<CmcsInFactoryBatch>(transport.InFactoryBatchId);
            if (batch == null) return false;

            CmcsTransport truck = commonDAO.SelfDber.Entity<CmcsTransport>("where InFactoryBatchId=:InFactoryBatchId and PKID=:PKID", new { InFactoryBatchId = batch.Id, PKID = transport.Id });
            if (truck != null)
            {
                truck.TransportNo = transport.CarNumber;
                truck.OperDate = transport.OperDate;
                truck.TransportStyle = "汽车";
                truck.TransportType = "汽车";
                truck.DisboardTime = transport.UploadTime;
                truck.OutFactoryTime = transport.OutFactoryTime;
                truck.InFactoryTime = transport.InFactoryTime;
                truck.TareDate = transport.TareTime;
                truck.ArriveDate = transport.InFactoryTime;
                truck.TicketWeight = transport.TicketWeight;
                truck.GrossWeight = transport.GrossWeight;
                truck.SkinWeight = transport.TareWeight;
                truck.StandardWeight = transport.SuttleWeight;
                truck.KGWEIGHT = transport.KgWeight;
                truck.KSWEIGHT = transport.KsWeight;
                truck.CheckQty = transport.CheckWeight;
                truck.MarginWeight = transport.ProfitAndLossWeight;
                truck.InFactoryBatchId = transport.InFactoryBatchId;
                truck.PKID = transport.Id;
                truck.MesureMan = "汽车智能化";
                truck.DataSource = "同步";
                truck.QtyHave = transport.CheckWeight;
                res = commonDAO.SelfDber.Update(truck) > 0;
            }
            else
            {
                truck = new CmcsTransport()
                {
                    TransportNo = transport.CarNumber,
                    OperDate = transport.OperDate,
                    TransportStyle = "汽车",
                    TransportType = "汽车",
                    ArriveDate = transport.CreateDate,
                    DisboardTime = transport.UploadTime,
                    OutFactoryTime = transport.OutFactoryTime,
                    InFactoryTime = transport.InFactoryTime,
                    TareDate = transport.TareTime,
                    TicketWeight = transport.TicketWeight,
                    GrossWeight = transport.GrossWeight,
                    SkinWeight = transport.TareWeight,
                    StandardWeight = transport.SuttleWeight,
                    KGWEIGHT = transport.KgWeight,
                    KSWEIGHT = transport.KsWeight,
                    CheckQty = transport.CheckWeight,
                    MarginWeight = transport.ProfitAndLossWeight,
                    InFactoryBatchId = transport.InFactoryBatchId,
                    PKID = transport.Id,
                    MesureMan = "汽车智能化",
                    DataSource = "同步",
                    QtyHave = transport.CheckWeight
                };

                res = commonDAO.SelfDber.Insert(truck) > 0;
            }

            if (res)
            {
                // 更新批次的量 

                List<CmcsTransport> trucks = commonDAO.SelfDber.Entities<CmcsTransport>("where InFactoryBatchId=:InFactoryBatchId order by Createdate desc", new { InFactoryBatchId = batch.Id });
                batch.TRANSPORTNUMBER = trucks.Count;
                batch.IsCheck = 0;
                batch.Ticketqty = trucks.Sum(a => a.TicketWeight);
                batch.Suttleweight = trucks.Sum(a => a.StandardWeight);
                batch.Checkqty = trucks.Sum(a => a.CheckQty);
                batch.KGWEIGHT = trucks.Sum(a => a.KGWEIGHT);
                batch.KSWEIGHT = trucks.Sum(a => a.KGWEIGHT);
                batch.Marginqty = trucks.Sum(a => a.MarginWeight);
                batch.BACKBATCHDATE = trucks[0].InFactoryTime;
                batch.FactArriveDate = trucks[0].InFactoryTime;
                commonDAO.SelfDber.Update(batch);
            }

            return res;
        }

        /// <summary>
        /// 将汽车出场运输记录同步到批次明细中
        /// </summary>
        /// <param name="transportId">汽车销售煤运输记录Id</param>
        /// <returns></returns>
        private bool SyncToOutBatch(Action<string, eOutputType> output, string transportId)
        {
            bool res = false;
            CmcsSaleFuelTransport transport = commonDAO.SelfDber.Get<CmcsSaleFuelTransport>(transportId);
            if (transport == null || transport.IsFinish == 0) return false;

            CmcsInFactoryBatch batch = commonDAO.SelfDber.Get<CmcsInFactoryBatch>(transport.InOutBatchId);
            if (batch == null) return false;
            CmcsTransport truck = commonDAO.SelfDber.Entity<CmcsTransport>("where InFactoryBatchId=:InOutBatchId and PKID=:PKID", new { InOutBatchId = batch.Id, PKID = transport.Id });
            if (truck != null)
            {
                truck.TransportNo = transport.CarNumber;
                truck.OperDate = transport.OperDate;
                truck.TransportStyle = "汽车";
                truck.TransportType = "汽车";
                truck.ArriveDate = transport.GrossTime;
                truck.OutFactoryTime = transport.OutFactoryTime;
                truck.InFactoryTime = transport.InFactoryTime;
                truck.TareDate = transport.TareTime;
                truck.ArriveDate = transport.InFactoryTime;
                truck.TicketWeight = transport.Outweight;
                truck.GrossWeight = transport.GrossWeight;
                truck.SkinWeight = transport.TareWeight;
                truck.StandardWeight = transport.SuttleWeight;
                truck.CheckQty = transport.SuttleWeight;
                truck.InFactoryBatchId = transport.InOutBatchId;
                truck.PKID = transport.Id;
                truck.MesureMan = "汽车智能化";
                truck.QtyHave = transport.SuttleWeight;

                res = commonDAO.SelfDber.Update(truck) > 0;
            }
            else
            {
                truck = new CmcsTransport()
                {
                    TransportNo = transport.CarNumber,
                    OperDate = transport.OperDate,
                    TransportStyle = "汽车",
                    TransportType = "汽车",
                    ArriveDate = transport.GrossTime,
                    OutFactoryTime = transport.OutFactoryTime,
                    InFactoryTime = transport.InFactoryTime,
                    TareDate = transport.TareTime,
                    TicketWeight = transport.Outweight,
                    GrossWeight = transport.GrossWeight,
                    SkinWeight = transport.TareWeight,
                    StandardWeight = transport.SuttleWeight,
                    CheckQty = transport.Outweight,
                    InFactoryBatchId = transport.InOutBatchId,
                    PKID = transport.Id,
                    MesureMan = "汽车智能化",
                    DataSource = "同步",
                    QtyHave = transport.SuttleWeight
                };

                res = commonDAO.SelfDber.Insert(truck) > 0;
            }

            if (res)
            {
                // 更新批次的量 

                List<CmcsTransport> trucks = commonDAO.SelfDber.Entities<CmcsTransport>("where InFactoryBatchId=:InFactoryBatchId order by Createdate desc", new { InFactoryBatchId = batch.Id });
                batch.TRANSPORTNUMBER = trucks.Count;
                batch.IsCheck = 0;
                batch.Ticketqty = trucks.Sum(a => a.TicketWeight);
                batch.Suttleweight = trucks.Sum(a => a.StandardWeight);
                batch.Checkqty = trucks.Sum(a => a.CheckQty);
                batch.KGWEIGHT = trucks.Sum(a => a.KGWEIGHT);
                batch.KSWEIGHT = trucks.Sum(a => a.KGWEIGHT);
                batch.Marginqty = trucks.Sum(a => a.MarginWeight);
                batch.BACKBATCHDATE = trucks[0].InFactoryTime;
                batch.FactArriveDate = trucks[0].InFactoryTime;
                commonDAO.SelfDber.Update(batch);
            }

            return res;
        }

    }
}
