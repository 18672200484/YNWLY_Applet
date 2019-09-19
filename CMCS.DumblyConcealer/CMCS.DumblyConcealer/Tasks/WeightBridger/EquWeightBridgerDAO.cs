using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.DumblyConcealer.Enums;
using CMCS.DumblyConcealer.Tasks.WeightBridger.Entities;
using CMCS.Common;
using CMCS.Common.Entities;
using CMCS.Common.DAO;
using CMCS.Common.Enums;
using CMCS.Common.Entities.TrainInFactory;
using CMCS.Common.Entities.Fuel;
using System.IO;
using CMCS.CarTransport.DAO;

namespace CMCS.DumblyConcealer.Tasks.WeightBridger
{
    public class EquWeightBridgerDAO
    {
        private static EquWeightBridgerDAO instance;

        public static EquWeightBridgerDAO GetInstance()
        {
            if (instance == null)
            {
                instance = new EquWeightBridgerDAO();
            }
            return instance;
        }

        private EquWeightBridgerDAO()
        {

        }

        /// <summary>
        /// 同步轨道衡数据
        /// </summary>
        /// <param name="output"></param>
        /// <returns></returns>
        public int SyncTrainWeightInfo(Action<string, eOutputType> output)
        {
            int res = 0;
            List<CmcsTrainWeightRecord> list = GetTrainWeightInfo();
            foreach (CmcsTrainWeightRecord item in list)
            {
                if (item.InFactoryDirection == "入厂")
                {
                    CmcsTrainWeightRecord train = Dbers.GetInstance().SelfDber.Entity<CmcsTrainWeightRecord>("where PKID=:PKID", new { PKID = item.PKID });
                    if (train == null)
                    {
                        item.OperDate = DateTime.Now;
                        res += Dbers.GetInstance().SelfDber.Insert(item);
                    }
                    else
                    {
                        train.SupplierName = item.SupplierName;
                        train.MineName = item.MineName;
                        train.FuelKind = item.FuelKind;
                        train.StationName = item.StationName;
                        train.MachineCode = item.MachineCode;
                        train.OrderNumber = item.OrderNumber;
                        train.TrainNumber = item.TrainNumber;
                        train.TrainType = item.TrainType;
                        train.TicketWeight = item.TicketWeight;
                        train.GrossWeight = item.GrossWeight;
                        train.SkinWeight = item.SkinWeight;
                        train.StandardWeight = item.StandardWeight;
                        train.Speed = item.Speed;
                        train.MesureMan = item.MesureMan;
                        train.ArriveTime = item.GrossTime;
                        train.GrossTime = item.GrossTime;
                        train.SkinTime = item.SkinTime;
                        train.LeaveTime = item.LeaveTime;
                        train.UnloadTime = item.UnloadTime;
                        train.DataFlag = 0;
                        res += Dbers.GetInstance().SelfDber.Update(train);
                        CommonDAO.GetInstance();
                    }
                }
                else if (item.InFactoryDirection == "出厂")
                {
                    int dayago = CommonDAO.GetInstance().GetCommonAppletConfigInt32("轨道衡回皮间隔天数");
                    CmcsTrainWeightRecord train_Old = Dbers.GetInstance().SelfDber.Entity<CmcsTrainWeightRecord>("where TrainNumber=:TrainNumber and IsSyncTareWeight=0 and GrossTime>:GrossTime1 and GrossTime<:GrossTime2 order by CreateDate desc", new { TrainNumber = item.TrainNumber, GrossTime1 = item.SkinTime.AddDays(-dayago), GrossTime2 = item.SkinTime });
                    if (train_Old != null)
                    {
                        train_Old.SkinTime = item.GrossTime;
                        train_Old.SkinWeight_Real = item.GrossWeight;
                        train_Old.LeaveTime = train_Old.SkinTime;
                        train_Old.UnloadTime = item.UnloadTime;
                        train_Old.StandardWeight_Real = train_Old.GrossWeight - train_Old.SkinWeight_Real;
                        train_Old.IsSyncTareWeight = 1;
                        res += Dbers.GetInstance().SelfDber.Update(train_Old);
                    }
                }
            }
            output(string.Format("同步轨道衡数据 {0} 条（ 第三方 > 集中管控 ）", res), eOutputType.Normal);
            return res;
        }

        /// <summary>
        /// 解析报文数据
        /// </summary>
        /// <returns></returns>
        public List<CmcsTrainWeightRecord> GetTrainWeightInfo()
        {
            String interface_path = CommonDAO.GetInstance().GetCommonAppletConfigString("轨道衡报文路径");
            int interface_day = CommonDAO.GetInstance().GetCommonAppletConfigInt32("轨道衡数据读取天数");

            List<CmcsTrainWeightRecord> items = new List<CmcsTrainWeightRecord>();
            List<string> st = new List<string>();
            for (int i = 0; i <= interface_day; i++)
            {
                String[] directory = Directory.GetFiles(interface_path, String.Format("{0}_*", DateTime.Now.AddDays(-i).ToString("yyMMdd")));
                st.AddRange(directory);
            }
            foreach (var item in st)
            {
                String bw = File.ReadAllText(item, Encoding.GetEncoding("gb2312"));
                //if (bw.Contains("*"))
                //    continue;
                String[] strs = bw.Split(new string[] { "\r\n" }, StringSplitOptions.None);
                DateTime dt = new DateTime(2000, 1, 1);
                String fx = "";
                String name = "";
                for (int i = 0; i < strs.Count(); i++)
                {
                    if (i == 1)
                    {
                        if (strs[i].Contains("<--"))
                        {
                            fx = "出厂";
                        }
                        else if (strs[i].Contains("-->"))
                        {
                            fx = "入厂";
                        }
                    }
                    if (i == 3)
                    {
                        string ss = DateTime.Now.ToString("yyyy").Substring(0, 2) + strs[i].Substring(strs[i].IndexOf(':') + 1, strs[i].Length - strs[i].IndexOf(':') - 1);
                        ss = ss.Substring(0, 4) + "-" + ss.Substring(4, 2) + "-" + ss.Substring(6, 2) + " " + ss.Substring(9, 2) + ":" + ss.Substring(11, 2) + ":" + ss.Substring(13, 2);
                        DateTime.TryParse(ss, out dt);
                    }
                    if (i >= 5)
                    {
                        String[] ss = strs[i].Split(',');
                        if (ss.Count() >= 10)
                        {
                            CmcsTrainWeightRecord tw = new CmcsTrainWeightRecord();
                            int twOrderNumber;
                            if (int.TryParse(ss[0].Trim(), out twOrderNumber))
                            {
                                tw.OrderNumber = twOrderNumber;
                            }
                            tw.TrainNumber = ss[1].Trim();
                            Decimal twGrossWeight;
                            if (Decimal.TryParse(ss[2].Trim(), out twGrossWeight))
                            {
                                if ((twGrossWeight < 25 && fx == "入厂") || (twGrossWeight > 25 && fx == "出厂") || twGrossWeight == 0)
                                    continue;
                                tw.GrossWeight = twGrossWeight;
                            }
                            Decimal twTareWeight;
                            if (Decimal.TryParse(ss[3].Trim(), out twTareWeight))
                            {
                                tw.SkinWeight = twTareWeight;//不同步皮重
                            }
                            Decimal twSuttleWeight;
                            if (Decimal.TryParse(ss[4].Trim(), out twSuttleWeight))
                            {
                                tw.StandardWeight = twSuttleWeight;
                            }
                            tw.SupplierName = ss[6].Trim();
                            decimal ticketWeight;
                            if (Decimal.TryParse(ss[8].Trim(), out ticketWeight))
                            {
                                tw.TicketWeight = ticketWeight;
                            }
                            Decimal twSpeed;
                            if (Decimal.TryParse(ss[9].Trim(), out twSpeed))
                            {
                                tw.Speed = twSpeed;
                            }
                            tw.TrainType = ss[10].Trim();
                            tw.PKID = dt.ToString("yyyyMMddHHmmss") + "-" + tw.OrderNumber;
                            tw.GrossTime = dt;
                            tw.ArriveTime = tw.GrossTime;
                            tw.SkinTime = dt;
                            tw.LeaveTime = dt;
                            tw.MesureMan = name;
                            tw.InFactoryDirection = fx;
                            tw.MachineCode = "轨道衡";
                            items.Add(tw);
                        }
                    }
                }
            }
            return items;
        }

        /// <summary>
        /// 同步车号识别数据
        /// </summary>
        /// <param name="output"></param>
        /// <returns></returns>
        public void SyncTrainRecognitionInfo(Action<string, eOutputType> output)
        {
            CmcsTrainRecognition train = Dbers.GetInstance().SelfDber.Entity<CmcsTrainRecognition>("where Status=0 and MachineCode=1 order by CrossTime desc");
            CommonDAO.GetInstance().SetSignalDataValue(GlobalVars.MachineCode_Recognition_1, eSignalDataName.当前车号.ToString(), train != null ? train.CarNumber : "");

            CmcsTrainRecognition train2 = Dbers.GetInstance().SelfDber.Entity<CmcsTrainRecognition>("where Status=0 and MachineCode=2 order by CrossTime desc");
            CommonDAO.GetInstance().SetSignalDataValue(GlobalVars.MachineCode_Recognition_2, eSignalDataName.当前车号.ToString(), train2 != null ? train2.CarNumber : "");

            int CrossNumber = Dbers.GetInstance().SelfDber.Count<CmcsTrainRecognition>("where CrossTime>=:CrossTime and MachineCode=1 order by CrossTime desc", new { CrossTime = DateTime.Now.Date });
            CommonDAO.GetInstance().SetSignalDataValue(GlobalVars.MachineCode_Recognition_1, eSignalDataName.当日已过车数.ToString(), CrossNumber.ToString());

            int CrossNumber2 = Dbers.GetInstance().SelfDber.Count<CmcsTrainRecognition>("where CrossTime>=:CrossTime and MachineCode=2 order by CrossTime desc", new { CrossTime = DateTime.Now.Date });
            CommonDAO.GetInstance().SetSignalDataValue(GlobalVars.MachineCode_Recognition_2, eSignalDataName.当日已过车数.ToString(), CrossNumber2.ToString());

            if (train != null)
            {
                CmcsTrainWeightRecord trainWeight = Dbers.GetInstance().SelfDber.Entity<CmcsTrainWeightRecord>("where TrainNumber=:TrainNumber and GrossTime<:GrossTime and GrossWeight=0 order by GrossTime desc", new { TrainNumber = train.CarNumber, GrossTime = train.CrossTime });
                CommonDAO.GetInstance().SetSignalDataValue(GlobalVars.MachineCode_Recognition_2, eSignalDataName.当前记录Id.ToString(), trainWeight != null ? trainWeight.PKID : "");
            }
            if (train2 != null)
            {
                CmcsTrainWeightRecord trainWeight2 = Dbers.GetInstance().SelfDber.Entity<CmcsTrainWeightRecord>("where TrainNumber=:TrainNumber and GrossTime<:GrossTime and GrossWeight=0 order by GrossTime desc", new { TrainNumber = train2.CarNumber, GrossTime = train2.CrossTime });
                CommonDAO.GetInstance().SetSignalDataValue(GlobalVars.MachineCode_Recognition_2, eSignalDataName.当前记录Id.ToString(), trainWeight2 != null ? trainWeight2.PKID : "");
            }

            //入场总车数
            int WeightTotal = CommonDAO.GetInstance().SelfDber.Count<CmcsTrainWeightRecord>("where ArriveTime>=:ArriveTime", new { ArriveTime = DateTime.Now.Date });
            //翻车总车数
            int GrossTotal = CommonDAO.GetInstance().SelfDber.Count<CmcsTrainRecognition>("where CrossTime>=:CrossTime ", new { CrossTime = DateTime.Now.Date });
            //待翻车数
            CommonDAO.GetInstance().SetSignalDataValue(GlobalVars.MachineCode_TrunOver, eSignalDataName.当日待翻车数.ToString(), (WeightTotal - GrossTotal).ToString());

            //检测火车入场时间
            IList<CmcsTrainWeightRecord> list = CommonDAO.GetInstance().SelfDber.Entities<CmcsTrainWeightRecord>("where ArriveTime>=:ArriveTime", new { ArriveTime = DateTime.Now.Date });
            if (list != null)
            {
                IList<CmcsTrainWeightRecord> list_over = list.Where(a => (DateTime.Now - a.ArriveTime).Hours > CommonDAO.GetInstance().GetCommonAppletConfigInt32("火车入场最长时间")).ToList();
                if (list_over != null && list_over.Count > 0)
                {
                    string msgContent = string.Empty;
                    if (list_over.Count < 6)
                    {
                        msgContent = "火车入场超时:";
                        IList<string> TrainNumbers = list_over.Select(a => a.TrainNumber).ToList();
                        foreach (string item in TrainNumbers)
                        {
                            msgContent += item + ",";
                        }
                        msgContent = msgContent.TrimEnd(',');
                    }
                    else
                    {
                        msgContent = string.Format("火车入场：{0}节车厢超时，请到火车入场列表查看", list_over.Count);
                    }
                    CommonDAO.GetInstance().SaveSysMessage("轨道衡", msgContent);
                }
            }
        }
    }
}
