using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;
using BasisPlatform;
using CMCS.AddNative.DAO;
using CMCS.Common.Enums;
using CMCS.Common.Utilities;

namespace CMCS.AddNative
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            // 检测更新
            AU.Updater updater = new AU.Updater();
            if (updater.NeedUpdate())
            {
                Process.Start("AutoUpdater.exe");
                Environment.Exit(0);
            }

            // BasisPlatform:应用程序初始化
            Basiser basiser = Basiser.GetInstance();
            basiser.EnabledEbiaSupport = true;
            basiser.InitBasisPlatform(CommonAppConfig.GetInstance().AppIdentifier, PlatformType.Winform);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            bool notRunning;
            using (Mutex mutex = new Mutex(true, Application.ProductName, out notRunning))
            {
                if (notRunning)
                {
                    //int minute = 5, surplus = minute;

                    //while (minute > 0)
                    //{
                    //    double d = minute - Environment.TickCount / 1000 / 60;
                    //    if (Environment.TickCount < 0 || d <= 0) break;

                    //    Log4Neter.Info("开机延迟启动,剩余" + d + "分钟");

                    //    Thread.Sleep(60000);

                    //    surplus--;
                    //}
                    Application.Run(new Form1());
                }
            }
        }
    }
}
