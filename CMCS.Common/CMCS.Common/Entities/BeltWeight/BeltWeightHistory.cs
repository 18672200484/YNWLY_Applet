using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.Entities.Sys;

namespace CMCS.Common.Entities.BeltWeight
{
    /// <summary>
    /// 皮带秤历史数据
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("cmcstbBeltWeightHistory")]
    public class BeltWeightHistory : EntityBase1
    {
        private string facilityNumber;
        /// <summary>
        /// 设备标识
        /// </summary>
        public string FacilityNumber
        {
            get { return facilityNumber; }
            set { facilityNumber = value; }
        }

        private decimal beltSpeed;
        /// <summary>
        /// 皮带速度
        /// </summary>
        public decimal BeltSpeed
        {
            get { return beltSpeed; }
            set { beltSpeed = value; }
        }

        private decimal momentInstrument;
        /// <summary>
        /// 瞬时流量
        /// </summary>
        public decimal MomentInstrument
        {
            get { return momentInstrument; }
            set { momentInstrument = value; }
        }

        private decimal sumWeight;
        /// <summary>
        /// 累计重量 KG
        /// </summary>
        public decimal SumWeight
        {
            get { return sumWeight; }
            set { sumWeight = value; }
        }

        private DateTime recordTime;
        /// <summary>
        /// 记录时间
        /// </summary>
        public DateTime RecordTime
        {
            get { return recordTime; }
            set { recordTime = value; }
        }

    }
}
