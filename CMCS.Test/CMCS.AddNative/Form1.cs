using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CMCS.AddNative.DAO;
using CMCS.Common.Utilities;
using System.Diagnostics;
using System.Xml.Linq;
using System.IO;
using CMCS.AddNative.Core;
using CMCS.AddNative.Enums;
using System.Xml;
using System.Threading;
using CMCS.Common.DAO;
using CMCS.Common.Enums;

namespace CMCS.AddNative
{
    public partial class Form1 : BasisPlatform.Forms.FrmBasis
    {
        public Form1()
        {
            InitializeComponent();
        }
        RTxtOutputer rTxtOutputer;
        CommonAppConfig commonAppConfig = CommonAppConfig.GetInstance();
        private void Form1_Load(object sender, EventArgs e)
        {
            VerifyBeforeClose = false;
            this.rTxtOutputer = new RTxtOutputer(rtxtOutput);
            this.ShowInTaskbar = false;
            this.WindowState = FormWindowState.Minimized;
            try
            {
                if (LoginDAO.CreateCred(commonAppConfig.IP, commonAppConfig.User, commonAppConfig.Pass))
                {
                    this.rTxtOutputer.Output("凭据添加成功");
                    Log4Neter.Info("凭据添加成功");
                }
                else
                {
                    this.rTxtOutputer.Output("凭据添加失败", eOutputType.Warn);
                    Log4Neter.Info("凭据添加失败");
                }
            }
            catch (Exception ex)
            {
                this.rTxtOutputer.Output("凭据添加失败", eOutputType.Error);
                Log4Neter.Error("添加凭据", ex);
            }

            //try
            //{
            //    // 添加、取消开机启动
            //    StartUpUtil.InsertStartUp(Application.ProductName, Application.ExecutablePath);
            //    this.rTxtOutputer.Output("添加开机启动成功", eOutputType.Normal);
            //    //StartUpUtil.DeleteStartUp(Application.ProductName);
            //}
            //catch (Exception ex)
            //{
            //    this.rTxtOutputer.Output("添加开机启动失败", eOutputType.Warn);
            //    Log4Neter.Error("添加开机启动", ex);
            //}

            TestFileSystemWatcher(commonAppConfig.DataPath);

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            try
            {
                //检测程序进程
                Process[] app = Process.GetProcessesByName(commonAppConfig.ProcessName);
                if (app == null) return;
                this.rTxtOutputer.Output("进程检测状态：" + (app.Length > 0 ? "正在运行" : "停止运行"), eOutputType.Normal);

                #region 读取XML
                ////将XML文件加载进来
                //XDocument document = XDocument.Load(commonAppConfig.XmlPath);
                ////获取到XML的根元素进行操作
                //XElement root = document.Root;
                //XElement ele = root.Element("CommonAppConfig");
                ////获取标签的值
                //XElement shuxing = ele.Element(commonAppConfig.AppIdentifier);
                //Console.WriteLine(shuxing.Value);
                ////获取根元素下的所有子元素
                //IEnumerable<XElement> enumerable = root.Elements();
                //foreach (XElement item in enumerable)
                //{
                //    foreach (XElement item1 in item.Elements())
                //    {
                //        Console.WriteLine(item1.Name);   //输出 name  name1            
                //    }
                //    Console.WriteLine(item.Attribute("id").Value);  //输出20
                //}
                #endregion

                //string appIdentifier = commonAppConfig.AppIdentifier.Replace("#", "").Substring(1, commonAppConfig.AppIdentifier.Length - 2) + commonAppConfig.AppIdentifier.Replace("#", "").Substring(0, 1);
                string appIdentifier = commonAppConfig.AppIdentifier;
                CommonDAO.GetInstance().SetSignalDataValue("化验室网络管理", appIdentifier + "_运行状态", app.Length.ToString());

                #region 写入XML
                //XmlDocument document = new XmlDocument();
                //document.Load(commonAppConfig.XmlPath);
                //XmlNode xns = document.SelectSingleNode("CommonAppConfig");
                //foreach (XmlNode item in xns.ChildNodes)
                //{
                //    if (item.Name == "#comment") continue;
                //    XmlElement xe = (XmlElement)item;
                //    if (xe.Name == commonAppConfig.XmlElement)
                //    {
                //        xe.InnerText = app.Length > 0 ? "1" : "0";
                //        break;
                //    }
                //}
                //document.Save(commonAppConfig.XmlPath);
                this.rTxtOutputer.Output("进程检测状态已上传");
                #endregion
            }
            catch (Exception ex)
            {
                Log4Neter.Error("进程检测", ex);
            }
            finally
            {
                timer1.Start();
            }
        }

        void TestFileSystemWatcher(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                this.rTxtOutputer.Output("文件监测路径为空", eOutputType.Normal);
                Log4Neter.Info("文件监测路径为空");
                return;
            }
            FileSystemWatcher watcher = new FileSystemWatcher();
            try
            {
                watcher.Path = path.Substring(0, path.LastIndexOf(@"\"));
            }
            catch (ArgumentException ex)
            {
                Log4Neter.Error("监测文件", ex);
                return;
            }

            //设置监视文件的哪些修改行为
            watcher.NotifyFilter = NotifyFilters.LastAccess
                | NotifyFilters.LastWrite | NotifyFilters.FileName | NotifyFilters.DirectoryName;


            watcher.Filter = "*.mdb";

            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.Created += new FileSystemEventHandler(OnChanged);
            watcher.Deleted += new FileSystemEventHandler(OnChanged);
            watcher.Renamed += new RenamedEventHandler(OnRenamed);

            watcher.EnableRaisingEvents = true;

            //while (Console.Read() != 'q') ;
        }

        void OnChanged(object source, FileSystemEventArgs e)
        {
            try
            {
                string sourceFile = commonAppConfig.DataPath;
                string destinationFile = commonAppConfig.DataCopyPath;
                bool isrewrite = true; // true=覆盖已存在的同名文件,false则反之

                FileInfo file = new FileInfo(sourceFile);
                if (file.Exists)
                {
                    file.CopyTo(destinationFile, isrewrite);
                    this.rTxtOutputer.Output("数据库已复制：" + sourceFile);
                }
            }
            catch (Exception ex)
            {
                this.rTxtOutputer.Output("复制数据库：" + ex.Message, eOutputType.Error);
                Log4Neter.Error("复制数据库", ex);
            }
        }

        void OnRenamed(object source, RenamedEventArgs e)
        {
            try
            {
                string sourceFile = commonAppConfig.DataPath;
                string destinationFile = commonAppConfig.DataCopyPath;
                bool isrewrite = true; // true=覆盖已存在的同名文件,false则反之

                FileInfo file = new FileInfo(sourceFile);
                if (file.Exists)
                {
                    file.CopyTo(destinationFile, isrewrite);
                    this.rTxtOutputer.Output("数据库已复制：" + sourceFile);
                }
            }
            catch (Exception ex)
            {
                this.rTxtOutputer.Output("复制数据库：" + ex.Message, eOutputType.Error);
                Log4Neter.Error("复制数据库", ex);
            }
        }
    }
}
