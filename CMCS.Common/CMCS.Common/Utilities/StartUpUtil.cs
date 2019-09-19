using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//
using System.IO;

namespace CMCS.Common.Utilities
{
    /// <summary>
    /// 开机启动类
    /// </summary>
    public static class StartUpUtil
    {
        #region 注册表开机启动项
        /// <summary>
        /// 添加开机启动
        /// </summary>
        /// <param name="name">键</param>
        /// <param name="filepath">程序路径</param>
        public static void InsertStartUp(string name, string filepath)
        {
            if (!File.Exists(filepath))
                return;

            Microsoft.Win32.RegistryKey Rkey = Microsoft.Win32.Registry.LocalMachine.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (Rkey == null)
                Rkey = Microsoft.Win32.Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run");

            Rkey.SetValue(name, filepath);
        }

        /// <summary>
        /// 删除开机启动
        /// </summary>
        /// <param name="name">键</param>
        public static void DeleteStartUp(string name)
        {
            Microsoft.Win32.RegistryKey Rkey = Microsoft.Win32.Registry.LocalMachine.CreateSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run");
            Rkey.DeleteValue(name, false);
        }
        #endregion

        #region 开始菜单启动
        /// <summary>
        /// 向开始菜单启动添加开机启动项
        /// </summary>
        /// <param name="directory"></param>
        /// <param name="shortcutName"></param>
        /// <param name="targetPath"></param>
        /// <param name="description"></param>
        /// <param name="iconLocation"></param>
        /// <returns></returns>
        //public static bool StartUp(string directory, string shortcutName, string targetPath, string description = null, string iconLocation = null)
        //{
        //    // 获取全局 开始 文件夹位置
        //    Environment.GetFolderPath(Environment.SpecialFolder.CommonStartup);
        //    // 获取当前登录用户的 开始 文件夹位置
        //    directory = Environment.GetFolderPath(Environment.SpecialFolder.Startup);
        //    try
        //    {
        //        if (!Directory.Exists(directory))
        //        {
        //            Directory.CreateDirectory(directory);
        //        }

        //        //添加引用 Com 中搜索 Windows Script Host Object Model
        //        string shortcutPath = Path.Combine(directory, string.Format("{0}.lnk", shortcutName));
        //        IWshRuntimeLibrary.WshShell shell = new IWshRuntimeLibrary.WshShell();
        //        IWshRuntimeLibrary.IWshShortcut shortcut = (IWshRuntimeLibrary.IWshShortcut)shell.CreateShortcut(shortcutPath);//创建快捷方式对象
        //        shortcut.TargetPath = targetPath;//指定目标路径
        //        shortcut.WorkingDirectory = Path.GetDirectoryName(targetPath);//设置起始位置
        //        shortcut.WindowStyle = 1;//设置运行方式，默认为常规窗口
        //        shortcut.Description = description;//设置备注
        //        shortcut.IconLocation = string.IsNullOrWhiteSpace(iconLocation) ? targetPath : iconLocation;//设置图标路径
        //        shortcut.Save();//保存快捷方式
        //        return true;
        //    }
        //    catch
        //    { }
        //    return false;
        //}
        #endregion
    }
}
