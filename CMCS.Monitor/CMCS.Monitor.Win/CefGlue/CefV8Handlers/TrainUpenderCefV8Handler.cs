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
    public class TrainUpenderCefV8Handler : CefV8Handler
    {

        protected override bool Execute(string name, CefV8Value obj, CefV8Value[] arguments, out CefV8Value returnValue, out string exception)
        {
            exception = null;
            returnValue = null;

            switch (name)
            {
                default:
                    returnValue = null;
                    break;
            }

            return true;
        }
    }
}
