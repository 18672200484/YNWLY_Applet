using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Start.Forms
{
    public partial class Splash : Form
    {
        public Splash()
        {
            InitializeComponent();
        }
        //关闭自身
        public void KillMe(object o, EventArgs e)
        {
            this.Close();
        }
        /// <summary>
        /// 加载并显示主窗体
        /// </summary>
        /// <param name="form">主窗体</param>
        public static void LoadAndRun(Form form)
        {
            //订阅主窗体的句柄创建事件
            form.HandleCreated += delegate
            {
                //启动新线程来显示Splash窗体
                new Thread(new ThreadStart(delegate
                {
                    Splash splash = new Splash();
                    //订阅主窗体的Shown事件
                    form.Load += delegate
                    {
                        //通知Splash窗体关闭自身
                        splash.Invoke(new EventHandler(splash.KillMe));
                        splash.Dispose();
                    };
                    //显示Splash窗体
                    Application.Run(splash);
                })).Start();
            };
            //显示主窗体
            Application.Run(form);
        }
    }
}
