using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;

namespace CMCS.DumblyConcealer.Win
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            bool notRunning;
            using (Mutex mutex = new Mutex(true, Application.ProductName, out notRunning))
            {
                if (notRunning) Application.Run(new MDIParent1());
            }
        }
    }
}
