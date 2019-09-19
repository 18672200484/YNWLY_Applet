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
using CMCS.Common;

namespace CMCS.Monitor.Win.CefGlue
{
    /// <summary>
    /// 汽车衡监控 CefV8Handler
    /// </summary>
    public class TruckWeighterCefV8Handler : CefV8Handler
    {
        CommonDAO commonDAO = CommonDAO.GetInstance();

        protected override bool Execute(string name, CefV8Value obj, CefV8Value[] arguments, out CefV8Value returnValue, out string exception)
        {
            exception = null;
            returnValue = null;

            switch (name)
            {
                // 道闸1升杆
                case "Gate1Up":
                    commonDAO.SendAppRemoteControlCmd(GlobalVars.MachineCode_QC_Weighter_1, "控制道闸", "Gate1Up");
                    break;
                // 道闸1降杆
                case "Gate1Down":
                    commonDAO.SendAppRemoteControlCmd(GlobalVars.MachineCode_QC_Weighter_1, "控制道闸", "Gate1Down");
                    break;
                // 道闸2升杆
                case "Gate2Up":
                    commonDAO.SendAppRemoteControlCmd(GlobalVars.MachineCode_QC_Weighter_1, "控制道闸", "Gate2Up");
                    break;
                // 道闸2降杆
                case "Gate2Down":
                    commonDAO.SendAppRemoteControlCmd(GlobalVars.MachineCode_QC_Weighter_1, "控制道闸", "Gate2Down");
                    break;
                default:
                    returnValue = null;
                    break;
            }

            return true;
        }
    }
}
