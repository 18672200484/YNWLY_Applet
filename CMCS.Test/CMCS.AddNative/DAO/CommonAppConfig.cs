using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace CMCS.AddNative.DAO
{
    /// <summary>
    /// 程序配置
    /// </summary>
    public class CommonAppConfig
    {
        private static string ConfigXmlPath = "Common.AppConfig.xml";

        private static CommonAppConfig instance;

        public static CommonAppConfig GetInstance()
        {
            return instance;
        }

        static CommonAppConfig()
        {
            instance = CMCS.Common.Utilities.XOConverter.LoadConfig<CommonAppConfig>(ConfigXmlPath);
        }

        /// <summary>
        /// 保存配置
        /// </summary>
        public void Save()
        {
            CMCS.Common.Utilities.XOConverter.SaveConfig(instance, ConfigXmlPath);
        }

        private string appIdentifier;
        /// <summary>
        /// 唯一标识
        /// </summary>
        [Description("唯一标识")]
        public string AppIdentifier
        {
            get { return appIdentifier; }
            set { appIdentifier = value; }
        }

        private string iP;
        /// <summary>
        /// IP地址
        /// </summary>
        [Description("IP地址")]
        public string IP
        {
            get { return iP; }
            set { iP = value; }
        }

        private string user;
        /// <summary>
        /// 用户名
        /// </summary>
        [Description("用户名")]
        public string User
        {
            get { return user; }
            set { user = value; }
        }

        private string pass;
        /// <summary>
        /// 密码
        /// </summary>
        [Description("密码")]
        public string Pass
        {
            get { return pass; }
            set { pass = value; }
        }

        private string processName;
        /// <summary>
        /// 进程名
        /// </summary>
        [Description("进程名")]
        public string ProcessName
        {
            get { return processName; }
            set { processName = value; }
        }

        private string xmlPath;
        /// <summary>
        /// 上传XML路径
        /// </summary>
        [Description("上传XML路径")]
        public string XmlPath
        {
            get { return xmlPath; }
            set { xmlPath = value; }
        }

        private string xmlElement;
        /// <summary>
        /// 上传XML标识
        /// </summary>
        [Description("上传XML标识")]
        public string XmlElement
        {
            get { return xmlElement; }
            set { xmlElement = value; }
        }

        private string dataPath;
        /// <summary>
        /// 数据文件路径
        /// </summary>
        [Description("数据文件路径")]
        public string DataPath
        {
            get { return dataPath; }
            set { dataPath = value; }
        }

        private string dataCopyPath;
        /// <summary>
        /// 数据文件复制路径
        /// </summary>
        [Description("数据文件复制路径")]
        public string DataCopyPath
        {
            get { return dataCopyPath; }
            set { dataCopyPath = value; }
        }
    }
}
