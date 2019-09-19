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

namespace CMCS.Monitor.Win.CefGlue
{
    /// <summary>
    /// 集中管控首页 CefV8Handler
    /// </summary>
    public class HomePageCefV8Handler : CefV8Handler
    {
        protected override bool Execute(string name, CefV8Value obj, CefV8Value[] arguments, out CefV8Value returnValue, out string exception)
        {
            exception = null;
            returnValue = null;

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
                //  打开汽车机械采样机监控
                case "OpenTruckMachinerySampler":
                    CefV8Context.GetCurrentContext().GetBrowser().SendProcessMessage(CefProcessId.Browser, CefProcessMessage.Create("OpenTruckMachinerySampler"));
                    break;
                //  打开智能存样柜与气动传输监控
                case "OpenAutoCupboardPneumaticTransfer":
                    CefV8Context.GetCurrentContext().GetBrowser().SendProcessMessage(CefProcessId.Browser, CefProcessMessage.Create("OpenAutoCupboardPneumaticTransfer"));
                    break;
                //  打开化验室监控
                case "OpenAssayManage":
                    CefV8Context.GetCurrentContext().GetBrowser().SendProcessMessage(CefProcessId.Browser, CefProcessMessage.Create("OpenAssayManage"));
                    break;
                default:
                    returnValue = null;
                    break;
            }

            return true;
        }
    }
}
