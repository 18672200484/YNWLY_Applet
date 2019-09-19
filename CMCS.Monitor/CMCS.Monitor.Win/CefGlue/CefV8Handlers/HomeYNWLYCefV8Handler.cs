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
            if (arguments.Length > 0)
            {
                paramSampler = arguments[0].GetStringValue();
            }
            switch (name)
            {
                // 打开皮带采样机监控界面
                case "OpenTrainBeltSampler":
                    CefV8Context.GetCurrentContext().GetBrowser().SendProcessMessage(CefProcessId.Browser, CefProcessMessage.Create("OpenTrainBeltSampler"));
                    break;
                //  打开火车机械采样机监控界面
                case "OpenTrainMachinerySampler":
                    //CefV8Context.GetCurrentContext().GetBrowser().SendProcessMessage(CefProcessId.Browser, CefProcessMessage.Create("OpenTrainMachinerySampler"));
                    break;
                // 打开全自动制样机监控
                case "OpenAutoMaker":
                    CefV8Context.GetCurrentContext().GetBrowser().SendProcessMessage(CefProcessId.Browser, CefProcessMessage.Create("OpenAutoMaker"));
                    break;
                //  打开火车入厂翻车机监控
                case "OpenTrainTipper":
                    CefV8Context.GetCurrentContext().GetBrowser().SendProcessMessage(CefProcessId.Browser, CefProcessMessage.Create("OpenTrainTipper"));
                    break;
                //  打开火车入厂记录查询
                case "OpenWeightBridgeLoadToday":
                    CefV8Context.GetCurrentContext().GetBrowser().SendProcessMessage(CefProcessId.Browser, CefProcessMessage.Create("OpenWeightBridgeLoadToday"));
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
                    CefV8Context.GetCurrentContext().GetBrowser().SendProcessMessage(CefProcessId.Browser, CefProcessMessage.Create("OpenTruckMachinerySampler"));
                    break;
                //  打开智能存样柜与气动传输监控
                case "OpenAutoCupboard":
                    CefV8Context.GetCurrentContext().GetBrowser().SendProcessMessage(CefProcessId.Browser, CefProcessMessage.Create("OpenAutoCupboardPneumaticTransfer"));
                    break;
                //  打开化验室监控
                case "OpenLaboratory":
                    CefV8Context.GetCurrentContext().GetBrowser().SendProcessMessage(CefProcessId.Browser, CefProcessMessage.Create("OpenAssayManage"));
                    break;
                case "GetHitchs":
                    List<HitchsEntityTemp> Hitchslist = new List<HitchsEntityTemp>();
                    HitchsEntityTemp Hitchs1 = new HitchsEntityTemp();
                    if (paramSampler == "#1磅")
                    {
                        Hitchs1.MachineCode = paramSampler;
                        Hitchs1.HitchTime = DateTime.Now.ToString();
                        Hitchs1.HitchDescribe = "称重数据异常";
                    }
                    else if (paramSampler == "#2采样机")
                    {
                        Hitchs1.MachineCode = paramSampler;
                        Hitchs1.HitchTime = DateTime.Now.ToString();
                        Hitchs1.HitchDescribe = "采样机无法启动";
                    }
                    else if (paramSampler == "#3乙皮带")
                    {
                        Hitchs1.MachineCode = paramSampler;
                        Hitchs1.HitchTime = DateTime.Now.ToString();
                        Hitchs1.HitchDescribe = "#3乙皮带故障";
                    }
                    Hitchslist.Add(Hitchs1);

                    exception = null;
                    returnValue = CefV8Value.CreateString(Newtonsoft.Json.JsonConvert.SerializeObject(Hitchslist));
                    break;
                // 成品仓信息
                case "getStorageInfo":

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

                    exception = null;
                    returnValue = CefV8Value.CreateString(Newtonsoft.Json.JsonConvert.SerializeObject(list));
                    break;
                default:
                    returnValue = null;
                    break;
            }

            return true;
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

    }
}
