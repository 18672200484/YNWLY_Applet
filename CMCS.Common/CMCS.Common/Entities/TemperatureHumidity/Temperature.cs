using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.Entities.Sys;

namespace CMCS.Common.Entities.TemperatureHumidity
{
    /// <summary>
    /// 温湿度仪
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("cmcstbTemperature")]
    public class TemperatureInfo : EntityBase1
    {
        private string facilityNumber;
        /// <summary>
        /// 标识
        /// </summary>
        public string FacilityNumber
        {
            get { return facilityNumber; }
            set { facilityNumber = value; }
        }

        private decimal temperature;
        /// <summary>
        /// 温度
        /// </summary>
        public decimal Temperature
        {
            get { return temperature; }
            set { temperature = value; }
        }

        private decimal humidity;
        /// <summary>
        /// 湿度
        /// </summary>
        public decimal Humidity
        {
            get { return humidity; }
            set { humidity = value; }
        }

        private string temperaturePre;
        /// <summary>
        /// 温度预警
        /// </summary>
        public string TemperaturePre
        {
            get { return temperaturePre; }
            set { temperaturePre = value; }
        }

        private string humidityPre;
        /// <summary>
        /// 湿度预警
        /// </summary>
        public string HumidityPre
        {
            get { return humidityPre; }
            set { humidityPre = value; }
        }

        private string onoffPre;
        /// <summary>
        /// 开关量预警
        /// </summary>
        public string OnOffPre
        {
            get { return onoffPre; }
            set { onoffPre = value; }
        }

        private string isUpload;
        /// <summary>
        /// 是否上传
        /// </summary>
        public string IsUpload
        {
            get { return isUpload; }
            set { isUpload = value; }
        }

        private string isUse;
        /// <summary>
        /// 是否使用
        /// </summary>
        public string IsUse
        {
            get { return isUse; }
            set { isUse = value; }
        }

    }
}
