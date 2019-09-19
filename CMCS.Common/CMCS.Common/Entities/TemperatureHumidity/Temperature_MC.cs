using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.Entities.Sys;

namespace CMCS.Common.Entities.TemperatureHumidity
{
    /// <summary>
    /// 煤场测温仪
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("cmcstbTemperature_MC")]
    public class Temperature_MC : EntityBase1
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
