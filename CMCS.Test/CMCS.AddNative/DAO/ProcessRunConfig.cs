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
    public class ProcessRunConfig
    {
        private static string ConfigXmlPath;

        private static CommonAppConfig instance;

        public static CommonAppConfig GetInstance()
        {
            return instance;
        }

        static ProcessRunConfig()
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

        /// <summary>
        /// 量热仪1
        /// </summary>
        public string LRY1 { get; set; }

        /// <summary>
        /// 量热仪2
        /// </summary>
        public string LRY2 { get; set; }

        /// <summary>
        /// 量热仪3
        /// </summary>
        public string LRY3 { get; set; }

        /// <summary>
        /// 量热仪4
        /// </summary>
        public string LRY4 { get; set; }

        /// <summary>
        /// 工分仪1
        /// </summary>
        public string GFY1 { get; set; }

        /// <summary>
        /// 工分仪2
        /// </summary>
        public string GFY2 { get; set; }

        /// <summary>
        /// 工分仪3
        /// </summary>
        public string GFY3 { get; set; }

        /// <summary>
        /// 工分仪4
        /// </summary>
        public string GFY4 { get; set; }

        /// <summary>
        /// 定硫仪4
        /// </summary>
        public string DLY4 { get; set; }

    }
}
