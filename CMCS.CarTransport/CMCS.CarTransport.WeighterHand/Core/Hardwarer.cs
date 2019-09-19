
namespace CMCS.CarTransport.WeighterHand.Core
{
    /// <summary>
    /// 硬件设备类
    /// </summary>
    public class Hardwarer
    {
        static WB.TOLEDO.YAOHUA.TOLEDO_YAOHUAWber wber = new WB.TOLEDO.YAOHUA.TOLEDO_YAOHUAWber(4);
        /// <summary>
        /// 地磅仪表
        /// </summary>
        public static WB.TOLEDO.YAOHUA.TOLEDO_YAOHUAWber Wber
        {
            get { return wber; }
        }
    }
}
