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
    /// 智能存样柜及气动传输监控界面 CefV8Handler
    /// </summary>
    public class AutoCupboardCefV8Handler : CefV8Handler
    {
        List<InfEquInfHitch> equInfHitchs = new List<InfEquInfHitch>();
        protected override bool Execute(string name, CefV8Value obj, CefV8Value[] arguments, out CefV8Value returnValue, out string exception)
        {
            exception = null;
            returnValue = null;
            string paramSampler = arguments[0].GetStringValue();

            switch (name)
            {
                //获取异常信息
                case "GetHitchs":
                    //异常信息
                    string machineCode = string.Empty;
                    if (paramSampler == "#1CYG")
                        machineCode = GlobalVars.MachineCode_CYG1;
                    else if (paramSampler == "#2CYG")
                        machineCode = GlobalVars.MachineCode_CYG2;
                    else if (paramSampler == "#1QD")
                        machineCode = GlobalVars.MachineCode_QD;

                    equInfHitchs = CommonDAO.GetInstance().GetEquInfHitchsByTime(machineCode, DateTime.Now);
                    returnValue = CefV8Value.CreateString(Newtonsoft.Json.JsonConvert.SerializeObject(equInfHitchs.Select(a => new { MachineCode = a.MachineCode, HitchTime = a.HitchTime.ToString("yyyy-MM-dd HH:mm"), HitchDescribe = a.HitchDescribe })));
                    break;
                default:
                    returnValue = null;
                    break;
            }

            return true;
        }
    }
}
