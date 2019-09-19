using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CMCS.Common;
using CMCS.Common.DAO;
using CMCS.Common.Entities.AutoMaker;
using CMCS.Common.Entities.Inf;
using CMCS.Monitor.Win.Core;
using CMCS.Monitor.Win.Frms;
using CMCS.Monitor.Win.Frms.Sys;
using CMCS.Monitor.Win.Html;
//
using Xilium.CefGlue;

namespace CMCS.Monitor.Win.CefGlue
{
    /// <summary>
    /// 皮带采样监控 CefV8Handler
    /// </summary>
    public class TrainBeltSamplerCefV8Handler : CefV8Handler
    {
        List<InfEquInfHitch> equInfHitchs = new List<InfEquInfHitch>();
        protected override bool Execute(string name, CefV8Value obj, CefV8Value[] arguments, out CefV8Value returnValue, out string exception)
        {
            exception = null;
            returnValue = null;
            string paramSampler1 = "", paramSampler2 = "";
            if (arguments.Length > 0)
                paramSampler1 = arguments[0].GetStringValue();
            if (arguments.Length > 1)
                paramSampler2 = arguments[1].GetStringValue();

            switch (name)
            {
                // 设置
                case "SubmitSet":
                    bool res = SetConfig(paramSampler1, paramSampler2);
                    returnValue = CefV8Value.CreateString(res.ToString());
                    break;
                //获取异常信息
                case "GetHitchs":
                    //异常信息
                    string machineCode = string.Empty;
                    if (paramSampler1 == "#1")
                        machineCode = GlobalVars.MachineCode_PDCYJ_1;
                    else if (paramSampler1 == "#2")
                        machineCode = GlobalVars.MachineCode_PDCYJ_2;
                    equInfHitchs = CommonDAO.GetInstance().GetEquInfHitchsByTime(machineCode, DateTime.Now);
                    returnValue = CefV8Value.CreateString(Newtonsoft.Json.JsonConvert.SerializeObject(equInfHitchs.Select(a => new { MachineCode = a.MachineCode, HitchTime = a.HitchTime.ToString("yyyy-MM-dd HH:mm"), HitchDescribe = a.HitchDescribe })));
                    break;
                default:
                    returnValue = null;
                    break;
            }

            return true;
        }

        bool SetConfig(string paramSampler1, string paramSampler2)
        {
            bool success = false;
            CommonDAO commonDAO = CommonDAO.GetInstance();
            if (paramSampler1 == "集样1")
                success = commonDAO.SetAppletConfig(GlobalVars.CommonAppletConfigName, "#1皮带采样机集样方式", paramSampler2);
            else if (paramSampler1 == "集样2")
                success = commonDAO.SetAppletConfig(GlobalVars.CommonAppletConfigName, "#2皮带采样机集样方式", paramSampler2);
            else if (paramSampler1 == "翻车1")
                success = commonDAO.SetAppletConfig(GlobalVars.CommonAppletConfigName, "#1翻车机对应皮带采样机", paramSampler2);
            else if (paramSampler1 == "翻车2")
                success = commonDAO.SetAppletConfig(GlobalVars.CommonAppletConfigName, "#2翻车机对应皮带采样机", paramSampler2);
            return success;
        }
    }
}
