using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//
using Xilium.CefGlue;
using System.Windows.Forms;
using CMCS.Monitor.Win.Frms;
using CMCS.Monitor.Win.Frms.Sys;
using CMCS.Monitor.Win.Core;
using CMCS.Common.DAO;
using CMCS.Common.Entities.BaseInfo;
using System.Data;
using CMCS.Common.Utilities;
using CMCS.Monitor.Win.Html;

namespace CMCS.Monitor.Win.CefGlue
{
    /// <summary>
    /// 集中管控首页 CefV8Handler
    /// </summary>
    public class HomeYNWLYCefV8Handler : CefV8Handler
    {
        protected override bool Execute(string name, CefV8Value obj, CefV8Value[] arguments, out CefV8Value returnValue, out string exception)
        {
            exception = null;
            returnValue = null;
            string paramSampler = string.Empty;
             string paramSampler1 = string.Empty;
             string paramSampler2 = string.Empty;
            if (arguments.Length > 0)
            {
                paramSampler = arguments[0].GetStringValue();
                paramSampler1 = arguments.Length > 1 ? arguments[1].GetStringValue() : "";
                paramSampler2 = arguments.Length > 2 ? arguments[2].GetStringValue() : "";
            }
            switch (name)
            {
                // 打开皮带采样机监控界面
                case "OpenTrainBeltSampler":
                    CefProcessMessage OpenTrainBeltSampler = CefProcessMessage.Create("OpenTrainBeltSampler");
                    int b = 0;
                    foreach (CefV8Value item in arguments)
                    {
                        CefValue model = CefValue.Create();
                        model.SetString(item.GetStringValue());
                        OpenTrainBeltSampler.Arguments.SetValue(b, model);
                        b++;
                    }

                    CefV8Context.GetCurrentContext().GetBrowser().SendProcessMessage(CefProcessId.Browser, OpenTrainBeltSampler);
                    break;
                // 打开皮带采样机监控界面
                case "OpenOutTrainBeltSampler":
                    CefProcessMessage OpenOutBeltSampler = CefProcessMessage.Create("OpenOutTrainBeltSampler");
                    int c = 0;
                    foreach (CefV8Value item in arguments)
                    {
                        CefValue model = CefValue.Create();
                        model.SetString(item.GetStringValue());
                        OpenOutBeltSampler.Arguments.SetValue(c, model);
                        c++;
                    }

                    CefV8Context.GetCurrentContext().GetBrowser().SendProcessMessage(CefProcessId.Browser, OpenOutBeltSampler);
                    break;

                //  打开火车机械采样机监控界面
                case "OpenTrainMachinerySampler":
                  // CefV8Context.GetCurrentContext().GetBrowser().SendProcessMessage(CefProcessId.Browser, CefProcessMessage.Create("OpenTrainMachinerySampler"));
                    break;
                // 打开全自动制样机监控
                case "OpenAutoMaker":
                    CefProcessMessage OpenAutoMaker = CefProcessMessage.Create("OpenAutoMaker");
                    int f = 0;
                    foreach (CefV8Value item in arguments)
                    {
                        CefValue model = CefValue.Create();
                        model.SetString(item.GetStringValue());
                        OpenAutoMaker.Arguments.SetValue(f, model);
                        f++;
                    }

                    CefV8Context.GetCurrentContext().GetBrowser().SendProcessMessage(CefProcessId.Browser, OpenAutoMaker);
                    break;
                //  打开火车入厂翻车机监控
                case "OpenTrainTipper":
                    CefV8Context.GetCurrentContext().GetBrowser().SendProcessMessage(CefProcessId.Browser, CefProcessMessage.Create("OpenTrainTipper"));
                    break;
                //  打开火车入厂记录查询
                case "OpenWeightBridgeLoadToday":
                    CefProcessMessage OpenWeightBridgeLoadToday = CefProcessMessage.Create("OpenWeightBridgeLoadToday");
                    int d = 0;
                    foreach (CefV8Value item in arguments)
                    {
                        CefValue model = CefValue.Create();
                        model.SetString(item.GetStringValue());
                        OpenWeightBridgeLoadToday.Arguments.SetValue(d, model);
                        d++;
                    }

                    CefV8Context.GetCurrentContext().GetBrowser().SendProcessMessage(CefProcessId.Browser, OpenWeightBridgeLoadToday);
                    break;
                //  打开汽车入厂重车衡监控
                case "OpenTruckWeighter":
                    CefV8Context.GetCurrentContext().GetBrowser().SendProcessMessage(CefProcessId.Browser, CefProcessMessage.Create("OpenTruckWeighter"));
                    break;
                //  打开汽车成品仓监控
                case "OpenTruckOrder": 
                    CefV8Context.GetCurrentContext().GetBrowser().SendProcessMessage(CefProcessId.Browser, CefProcessMessage.Create("OpenTruckOrder"));
                    break;
                //  打开汽车机械采样机监控
                case "OpenTruckMachinerySampler":
                    CefProcessMessage SamplerMessage = CefProcessMessage.Create("OpenTruckMachinerySampler");
                    int a = 0;
                    foreach (CefV8Value item in arguments)
                    {
                        CefValue model = CefValue.Create();
                        model.SetString(item.GetStringValue());
                        SamplerMessage.Arguments.SetValue(a, model);
                        a++;
                    }

                    CefV8Context.GetCurrentContext().GetBrowser().SendProcessMessage(CefProcessId.Browser, SamplerMessage);
                    break;
                //  打开智能存样柜与气动传输监控
                case "OpenAutoCupboard":
                    CefV8Context.GetCurrentContext().GetBrowser().SendProcessMessage(CefProcessId.Browser, CefProcessMessage.Create("OpenAutoCupboardPneumaticTransfer"));
                    break;
                //  打开化验室监控
                case "OpenLaboratory":
                    CefV8Context.GetCurrentContext().GetBrowser().SendProcessMessage(CefProcessId.Browser, CefProcessMessage.Create("OpenAssayManage"));
                    break;
                case "OpenCarMonitor":
                    CefProcessMessage OpenCarMonitor = CefProcessMessage.Create("OpenCarMonitor");
                    int e = 0;
                    foreach (CefV8Value item in arguments)
                    {
                        CefValue model = CefValue.Create();
                        model.SetString(item.GetStringValue());
                        OpenCarMonitor.Arguments.SetValue(e, model);
                        e++;
                    }
                    CefV8Context.GetCurrentContext().GetBrowser().SendProcessMessage(CefProcessId.Browser, OpenCarMonitor);
                    break;
                case "OpenPoundInfo":
                    CefProcessMessage message = CefProcessMessage.Create("OpenPoundInfo");
                    int i = 0;
                    foreach (CefV8Value item in arguments)
                    {
                        CefValue model = CefValue.Create();
                        model.SetString(item.GetStringValue());
                        message.Arguments.SetValue(i, model);
                        i++;
                    }
                    CefV8Context.GetCurrentContext().GetBrowser().SendProcessMessage(CefProcessId.Browser, message);
                    break;
                case "GetHitchs":
                    List<HitchsEntityTemp> Hitchslist = GetHitchsData(paramSampler);
                    exception = null;
                    returnValue = CefV8Value.CreateString(Newtonsoft.Json.JsonConvert.SerializeObject(Hitchslist));
                    break;
                // 成品仓信息
                case "getStorageInfo":
                    List<StorageEntityTemp> list = getStorageInfoData(paramSampler);
                    exception = null;
                    returnValue = CefV8Value.CreateString(Newtonsoft.Json.JsonConvert.SerializeObject(list));
                    break;
                // 成品仓信息
                case "getStorageAreaInfo":
                    string sql = string.Format(@"select a.fuelstorageid,a.startpoint,a.endpoint from stgtbfuelstoragearea a where a.AREANAME = '{0}'",paramSampler);
                    DataTable dt = CommonDAO.GetInstance().SelfDber.ExecuteDataTable(sql);
                    if(dt != null && dt.Rows.Count >0 )
                    {
                        STGSelectStorageData StorageArea = GetStorageData(dt.Rows[0]["fuelstorageid"].ToString(), 
                            dt.Rows[0]["startpoint"].ToString(), 
                            dt.Rows[0]["endpoint"].ToString());
                        exception = null;
                        string sql1 = string.Format(@"select a.pointx,a.pointy,a.unitname,a.TEMPERATURE,a.polecode from stgtbstoragetemperature a where a.unitname = '{0}'", paramSampler);

                        DataTable dt1 = CommonDAO.GetInstance().SelfDber.ExecuteDataTable(sql1);

                        if (dt1 != null && dt1.Rows.Count > 0)
                        {
                            foreach (DataRow dr in dt1.Rows)
                            {
                                StorageArea.TEMPERATURE += dr["polecode"].ToString() + "," + dr["TEMPERATURE"].ToString() + "|";
                            }
                        }
                        List<STGSelectStorageData> StorageDataList = new List<STGSelectStorageData>();
                        StorageDataList.Add(StorageArea);
                        returnValue = CefV8Value.CreateString(Newtonsoft.Json.JsonConvert.SerializeObject(StorageDataList));
                    }
                    break;
                case "getStorageStackInfo":
                    STGSelectStorageData StorageArea1 = GetStorageData(paramSampler,
                            paramSampler1,
                            paramSampler2);
                        exception = null;
                        List<STGSelectStorageData> StorageDataList1 = new List<STGSelectStorageData>();
                        StorageDataList1.Add(StorageArea1);
                        returnValue = CefV8Value.CreateString(Newtonsoft.Json.JsonConvert.SerializeObject(StorageDataList1));
                    break;
                case "GetStorageBox":
                    List<StorageBoxTemp> Boxlist = GetStorageBoxData();
                    exception = null;
                    returnValue = CefV8Value.CreateString(Newtonsoft.Json.JsonConvert.SerializeObject(Boxlist));
                    break;
                case "OpenInOutInfo":
                    CefProcessMessage messageDoorInfo = CefProcessMessage.Create("OpenInOutInfo");
                    int j = 0;
                    foreach (CefV8Value item in arguments)
                    {
                        CefValue model = CefValue.Create();
                        model.SetString(item.GetStringValue());
                        messageDoorInfo.Arguments.SetValue(j, model);
                        j++;
                    }
                    CefV8Context.GetCurrentContext().GetBrowser().SendProcessMessage(CefProcessId.Browser, messageDoorInfo);
                    break;
                default:
                    returnValue = null;
                    break;
            }

            return true;
        }

       

        /// <summary>
        /// 根据煤场ID，开始点，结束点，批次ID查询范围内煤量及煤质
        /// </summary>
        /// <param name="storage"></param>
        /// <param name="startPoint"></param>
        /// <param name="endPoint"></param>
        /// <param name="batchID"></param>
        /// <returns></returns>
        public virtual STGSelectStorageData GetStorageData(string storageId, string startPoint, string endPoint)
        {
            String SQL = string.Format(@"
                        SELECT SUM(A.QTYHAVE) VALUE,
                        ROUND(CASE WHEN SUM(CASE WHEN QCAL>0 THEN A.QTYHAVE ELSE 0 END)>0 THEN SUM(QCAL*A.QTYHAVE)/SUM(CASE WHEN QCAL>0 THEN A.QTYHAVE ELSE 0 END) ELSE 0 END,6) QCAL,
                        ROUND(CASE WHEN SUM(CASE WHEN QJ>0 THEN A.QTYHAVE ELSE 0 END)>0 THEN SUM(QJ*A.QTYHAVE)/SUM(CASE WHEN QJ>0 THEN A.QTYHAVE ELSE 0 END) ELSE 0 END,6) QJ,
                        ROUND(CASE WHEN SUM(CASE WHEN MAR>0 THEN A.QTYHAVE ELSE 0 END)>0 THEN SUM(MAR*A.QTYHAVE)/SUM(CASE WHEN MAR>0 THEN A.QTYHAVE ELSE 0 END) ELSE 0 END,6) MAR,
                        ROUND(CASE WHEN SUM(CASE WHEN MAD>0 THEN A.QTYHAVE ELSE 0 END)>0 THEN SUM(MAD*A.QTYHAVE)/SUM(CASE WHEN MAD>0 THEN A.QTYHAVE ELSE 0 END) ELSE 0 END,6) MAD,
                        ROUND(CASE WHEN SUM(CASE WHEN VAR>0 THEN A.QTYHAVE ELSE 0 END)>0 THEN SUM(VAR*A.QTYHAVE)/SUM(CASE WHEN VAR>0 THEN A.QTYHAVE ELSE 0 END) ELSE 0 END,6) VAR,
                        ROUND(CASE WHEN SUM(CASE WHEN VAD >0 THEN A.QTYHAVE ELSE 0 END)>0 THEN SUM(VAD *A.QTYHAVE)/SUM(CASE WHEN VAD >0 THEN A.QTYHAVE ELSE 0 END) ELSE 0 END,6) VAD ,
                        ROUND(CASE WHEN SUM(CASE WHEN VD  >0 THEN A.QTYHAVE ELSE 0 END)>0 THEN SUM(VD  *A.QTYHAVE)/SUM(CASE WHEN VD  >0 THEN A.QTYHAVE ELSE 0 END) ELSE 0 END,6) VD  ,
                        ROUND(CASE WHEN SUM(CASE WHEN VDAF>0 THEN A.QTYHAVE ELSE 0 END)>0 THEN SUM(VDAF*A.QTYHAVE)/SUM(CASE WHEN VDAF>0 THEN A.QTYHAVE ELSE 0 END) ELSE 0 END,6) VDAF,
                        ROUND(CASE WHEN SUM(CASE WHEN STAR>0 THEN A.QTYHAVE ELSE 0 END)>0 THEN SUM(STAR*A.QTYHAVE)/SUM(CASE WHEN STAR>0 THEN A.QTYHAVE ELSE 0 END) ELSE 0 END,6) STAR,
                        ROUND(CASE WHEN SUM(CASE WHEN STAD>0 THEN A.QTYHAVE ELSE 0 END)>0 THEN SUM(STAD*A.QTYHAVE)/SUM(CASE WHEN STAD>0 THEN A.QTYHAVE ELSE 0 END) ELSE 0 END,6) STAD,
                        ROUND(CASE WHEN SUM(CASE WHEN STD >0 THEN A.QTYHAVE ELSE 0 END)>0 THEN SUM(STD *A.QTYHAVE)/SUM(CASE WHEN STD >0 THEN A.QTYHAVE ELSE 0 END) ELSE 0 END,6) STD ,
                        ROUND(CASE WHEN SUM(CASE WHEN AAR>0 THEN A.QTYHAVE ELSE 0 END)>0 THEN SUM(AAR*A.QTYHAVE)/SUM(CASE WHEN AAR>0 THEN A.QTYHAVE ELSE 0 END) ELSE 0 END,6) AAR,
                        ROUND(CASE WHEN SUM(CASE WHEN AAD>0 THEN A.QTYHAVE ELSE 0 END)>0 THEN SUM(AAD*A.QTYHAVE)/SUM(CASE WHEN AAD>0 THEN A.QTYHAVE ELSE 0 END) ELSE 0 END,6) AAD,
                        ROUND(CASE WHEN SUM(CASE WHEN AD >0 THEN A.QTYHAVE ELSE 0 END)>0 THEN SUM(AD *A.QTYHAVE)/SUM(CASE WHEN AD >0 THEN A.QTYHAVE ELSE 0 END) ELSE 0 END,6) AD ,
                        ROUND(CASE WHEN SUM(CASE WHEN ST>0 THEN A.QTYHAVE ELSE 0 END)>0 THEN SUM(ST*A.QTYHAVE)/SUM(CASE WHEN ST>0 THEN A.QTYHAVE ELSE 0 END) ELSE 0 END,6) ST,
                        ROUND(CASE WHEN SUM(CASE WHEN fuelprice>0 THEN A.QTYHAVE ELSE 0 END)>0 THEN SUM(FUELPRICE*A.QTYHAVE)/SUM(CASE WHEN FUELPRICE>0 THEN A.QTYHAVE ELSE 0 END) ELSE 0 END,6) FUELPRICE,
                        MIN(A.STARTPOINT) STARTPOINT ,MAX(A.ENDPOINT) ENDPOINT 
                        FROM STGTBINMINQTY A 
                        LEFT JOIN STGTBALLINMIDDLE B ON A.INMIDDLEID = B.ID 
                        LEFT JOIN FULTBFUELQUALITY C ON B.FUELQUALITYID = C.ID 
                        WHERE A.FUELSTORAGEID ='{0}' AND A.STARTPOINT >= {1} AND A.ENDPOINT <= {2} AND A.QTYHAVE>0 ", storageId, startPoint, endPoint);

            DataTable dt = CommonDAO.GetInstance().SelfDber.ExecuteDataTable(SQL);
               IList<STGSelectStorageData> list  = new List<STGSelectStorageData> ();
            foreach (DataRow dr in dt.Rows)
            {
                STGSelectStorageData model = new STGSelectStorageData();
                model.Qcal = ToDecimal(dr["QCAL"],0);
                model.Stad = ToDecimal(dr["STAD"], 2);
                model.Vdaf = ToDecimal(dr["VDAF"], 2);
                model.Value = ToDecimal(dr["VALUE"], 2);
                model.StartPoint = Convert.ToDecimal(startPoint);
                model.EndPoint = Convert.ToDecimal(endPoint);
                list.Add(model);
            }
       
            STGSelectStorageData entity = null;
            if (list != null && list.Count > 0)
            {
                entity = list.FirstOrDefault();
                if (entity.StartPoint == 0 && entity.EndPoint == 0)
                {
                    entity.StartPoint = Convert.ToDecimal(startPoint);
                    entity.EndPoint = Convert.ToDecimal(endPoint);
                }
            }
            else
            {
                entity = new STGSelectStorageData();
            }
            return entity;

        }

        private decimal ToDecimal(object obj, int i)
        {
            decimal value = 0.00m;
            try
            {
                value = Math.Round(Convert.ToDecimal(obj), i);
                return value;
            }
            catch (Exception)
            {
                return value;
            }
        }


        private static List<HitchsEntityTemp> GetHitchsData(string paramSampler)
        {
            List<HitchsEntityTemp> Hitchslist = new List<HitchsEntityTemp>();
            HitchsEntityTemp Hitchs1 = new HitchsEntityTemp();
            Hitchs1.MachineCode = paramSampler;
            Hitchs1.HitchTime = DateTime.Now.ToString();
            if (paramSampler.Contains("磅"))
            {
                Hitchs1.HitchDescribe = "地磅设备连接失败";
            }
            else if (paramSampler == "#2采样机")
            {
                Hitchs1.HitchDescribe = "采样机无法启动";
            }
            else if (paramSampler == "#3乙皮带")
            {
                Hitchs1.HitchDescribe = "#3乙皮带故障";
            }
            Hitchslist.Add(Hitchs1);
            return Hitchslist;
        }

        private static List<StorageEntityTemp> getStorageInfoData(string paramSampler)
        {
            List<StorageEntityTemp> list = new List<StorageEntityTemp>();
            StorageEntityTemp model1 = new StorageEntityTemp();
            StorageEntityTemp model2 = new StorageEntityTemp();
            model1.name = "#1";
            model1.per = "0";
            model1.speed = "0";

            if (paramSampler == "#1")
            {
                model1.name = "#1";
                model1.per = "0";
                model1.speed = "0";
            }
            else if (paramSampler == "#2")
            {
                model1.name = "#2";
                model1.per = "30";
                model1.speed = "100";
            }
            else if (paramSampler == "#3")
            {
                model1.name = "#3";
                model1.per = "50";
                model1.speed = "200";
            }
            else
            {
                model1.name = "#4";
                model1.per = "100";
                model1.speed = "250";
            }

            list.Add(model1);
            return list;
        }

        private List<StorageBoxTemp> GetStorageBoxData()
        {
            List<CmcsSignalData> boxList1 = CommonDAO.GetInstance().GetSignalDataValueByLike("智能存样柜");
            List<StorageBoxTemp> Boxlist = new List<StorageBoxTemp>();
            StorageBoxTemp box1 = new StorageBoxTemp();
            box1.XH = "1";
            box1.Name = "#1智能存样柜";
            box1.MaxBoxNumber = GetSignValue("#1智能存样柜", "共有仓位", boxList1);
            box1.UseNumber = GetSignValue("#1智能存样柜", "已存仓位", boxList1);
            box1.NoUseNumber = GetSignValue("#1智能存样柜", "未存仓位", boxList1);
            box1.UsePer = GetSignValue("#1智能存样柜", "存样率", boxList1);

            string sql1 = @"select (b.configvalue) from cmcstbappletconfig b where b.configname = '存样柜超期天数'";
            DataTable dt1 = CommonDAO.GetInstance().SelfDber.ExecuteDataTable(sql1);
            int configvalue = 90;
            if (dt1 != null && dt1.Rows.Count > 0)
            {
                configvalue = Convert.ToInt32(dt1.Rows[0][0].ToString());
            }
            string sql2 = string.Format(@"select count(0) from inftbcygsam d where d.machinecode = '#1智能存样柜' and d.isnew = 1 and d.updatetime + {0} < sysdate", configvalue);
            DataTable dt2 = CommonDAO.GetInstance().SelfDber.ExecuteDataTable(sql2);
            if (dt2 != null && dt2.Rows.Count > 0)
            {
                box1.LimitNumber = dt2.Rows[0][0].ToString();
            }
            Boxlist.Add(box1);

            StorageBoxTemp box2 = new StorageBoxTemp();
            box2.XH = "2";
            box2.Name = "#2智能存样柜";
            box2.MaxBoxNumber = GetSignValue("#2智能存样柜", "共有仓位", boxList1);
            box2.UseNumber = GetSignValue("#2智能存样柜", "已存仓位", boxList1);
            box2.NoUseNumber = GetSignValue("#2智能存样柜", "未存仓位", boxList1);
            box2.UsePer = GetSignValue("#2智能存样柜", "存样率", boxList1);

            string sql3 = string.Format(@"select count(0) from inftbcygsam d where d.machinecode = '#2智能存样柜' and d.isnew = 1 and d.updatetime + {0} < sysdate", configvalue);
            DataTable dt3 = CommonDAO.GetInstance().SelfDber.ExecuteDataTable(sql3);
            if (dt3 != null && dt3.Rows.Count > 0)
            {
                box2.LimitNumber = dt3.Rows[0][0].ToString();
            }

            Boxlist.Add(box2);
            return Boxlist;
        }

        private List<StorageAreaEntityTemp> GetStorageAreaData(string str)
        {
            List<CmcsSignalData> boxList1 = CommonDAO.GetInstance().GetSignalDataValueByLike("智能存样柜");
            List<StorageAreaEntityTemp> Boxlist = new List<StorageAreaEntityTemp>();
            StorageAreaEntityTemp box1 = new StorageAreaEntityTemp();
            Boxlist.Add(box1);
            return Boxlist;
        }

        #region MyRegion
        public string GetSignValueByLike(string SignalPrefix, string SignalName, List<CmcsSignalData> boxList1)
        {
            var d = boxList1.Where(t => t.SignalPrefix == SignalPrefix && t.SignalName.Contains(SignalName)).Select(t => t.SignalValue).ToList();
            int i = 0;
            foreach (var item in d)
            {
                i += Convert.ToInt32(item);
            }
            return i.ToString();
        }

        public string GetSignValue(string SignalPrefix, string SignalName, List<CmcsSignalData> boxList1)
        {
            var d = boxList1.Where(t => t.SignalPrefix == SignalPrefix && t.SignalName == SignalName).FirstOrDefault();
            if (d != null)
            {
                return d.SignalValue;
            }
            else
            {
                return "";
            }
        } 
        #endregion

        public class DoorInfoTemp
        {
            /// <summary>
            /// 序号
            /// </summary>
            public string XH { get; set; }
            /// <summary>
            /// 存样柜
            /// </summary>
            public string Name { get; set; }
            /// <summary>
            /// 共有仓位
            /// </summary>
            public string MaxBoxNumber { get; set; }
            /// <summary>
            /// 已使用样数
            /// </summary>
            public string UseNumber { get; set; }
            /// <summary>
            /// 未使用样数
            /// </summary>
            public string NoUseNumber { get; set; }
            /// <summary>
            /// 使用比例
            /// </summary>
            public string UsePer { get; set; }
            /// <summary>
            /// 超期数量
            /// </summary>
            public string LimitNumber { get; set; }
        }

        public class StorageBoxTemp
        {
            /// <summary>
            /// 序号
            /// </summary>
            public string XH { get; set; }
            /// <summary>
            /// 存样柜
            /// </summary>
            public string Name { get; set; }
            /// <summary>
            /// 共有仓位
            /// </summary>
            public string MaxBoxNumber { get; set; }
            /// <summary>
            /// 已使用样数
            /// </summary>
            public string UseNumber { get; set; }
            /// <summary>
            /// 未使用样数
            /// </summary>
            public string NoUseNumber { get; set; }
            /// <summary>
            /// 使用比例
            /// </summary>
            public string UsePer { get; set; }
            /// <summary>
            /// 超期数量
            /// </summary>
            public string LimitNumber { get; set; }
        }

        public class StorageAreaEntityTemp
        {
            /// <summary>
            /// 区域名称
            /// </summary>
            public string name { get; set; }
            /// <summary>
            /// 煤场存量
            /// </summary>
            public string qty { get; set; }
            /// <summary>
            /// qcal
            /// </summary>
            public string qcal { get; set; }
            /// <summary>
            /// stad
            /// </summary>
            public string stad { get; set; }
            /// <summary>
            /// aad
            /// </summary>
            public string aad { get; set; }
            /// <summary>
            /// vdaf
            /// </summary>
            public string vdaf { get; set; }
        }

        public class StorageEntityTemp
        {
            /// <summary>
            /// 仓位
            /// </summary>
            public string name { get; set; }
            /// <summary>
            /// 料位
            /// </summary>
            public string per { get; set; }
            /// <summary>
            /// 实时转速
            /// </summary>
            public string speed { get; set; }
        }

        public class HitchsEntityTemp
        {
            /// <summary>
            /// 故障设备名称
            /// </summary>
            public string MachineCode { get; set; }
            /// <summary>
            /// 故障时间
            /// </summary>
            public string HitchTime { get; set; }
            /// <summary>
            /// 故障内容
            /// </summary>
            public string HitchDescribe { get; set; }
        }
        /// <summary>
        /// 煤场选择页面临时类
        /// </summary>
        [Serializable]
        public class STGSelectStorageData 
        {
            /// <summary>
            /// 煤量
            /// </summary>
            public Decimal Value { get; set; }

            /// <summary>
            /// 起始点
            /// </summary>
            public Decimal StartPoint { get; set; }

            /// <summary>
            /// 结束点
            /// </summary>
            public Decimal EndPoint { get; set; }

            /// <summary>
            /// 宽度
            /// </summary>
            public Decimal Width { get; set; }

            /// <summary>
            /// 批次号
            /// </summary>
            public String Batch { get; set; }

            /// <summary>
            /// 热值(Kcal/kg)
            /// </summary>
            public Decimal Qcal { get; set; }

            /// <summary>
            /// 热值(MJ/kg)
            /// </summary>
            public Decimal QJ { get; set; }

            /// <summary>
            /// 全水(MT)
            /// </summary>
            public Decimal Mar { get; set; }

            /// <summary>
            /// 空干基水分
            /// </summary>
            public Decimal Mad { get; set; }

            /// <summary>
            /// 硫分(Star)
            /// </summary>
            public Decimal Star { get; set; }

            /// <summary>
            /// 硫分(Stad)
            /// </summary>
            public Decimal Stad { get; set; }

            /// <summary>
            /// 硫分(Std)
            /// </summary>
            public Decimal Std { get; set; }

            /// <summary>
            /// 挥发分(Var)
            /// </summary>
            public Decimal Var { get; set; }

            /// <summary>
            /// 挥发分(Vad)
            /// </summary>
            public Decimal Vad { get; set; }

            /// <summary>
            /// 挥发分(Vd)
            /// </summary>
            public Decimal Vd { get; set; }

            /// <summary>
            /// 挥发分(Vdaf)
            /// </summary>
            public Decimal Vdaf { get; set; }

            /// <summary>
            /// 灰分(Aar)
            /// </summary>
            public Decimal Aar { get; set; }

            /// <summary>
            /// 灰分(Aad)
            /// </summary>
            public Decimal Aad { get; set; }

            /// <summary>
            /// 灰分(Ad)
            /// </summary>
            public Decimal Ad { get; set; }

            /// <summary>
            /// 灰熔点(℃)
            /// </summary>
            public Decimal ST { get; set; }

            /// <summary>
            /// 颜色
            /// </summary>
            public String Color { get; set; }
            /// <summary>
            /// 煤价
            /// </summary>
            public Decimal FuelPrice { get; set; }

            /// <summary>
            /// 煤场ID
            /// </summary>
            public String FuelStorageID { get; set; }

            /// <summary>
            /// 顺序
            /// </summary>
            public Int32 UnitOrder { get; set; }

            /// <summary>
            /// 比例（临时）
            /// </summary>
            public Decimal Scale { get; set; }

            /// <summary>
            /// 温度（临时）
            /// </summary>
            public String TEMPERATURE { get; set; }

            /// <summary>
            /// 温度（临时）
            /// </summary>
            public String polecode { get; set; }
            
            
        }
    }
}
