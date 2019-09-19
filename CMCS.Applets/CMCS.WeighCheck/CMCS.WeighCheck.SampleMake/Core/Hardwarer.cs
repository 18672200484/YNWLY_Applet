
namespace CMCS.WeighCheck.SampleMake.Core
{
    /// <summary>
    /// 硬件设备类
    /// </summary>
    public class Hardwarer
    {
        static WB.XiangPing.Balance.XiangPing_Balance wber = new WB.XiangPing.Balance.XiangPing_Balance(3);
        /// <summary>
        /// 电子秤
        /// </summary>
        public static WB.XiangPing.Balance.XiangPing_Balance Wber
        {
            get { return wber; }
        }

        static WB.XiangPing.Balance.XiangPing_Balance wber_min = new WB.XiangPing.Balance.XiangPing_Balance(3);
        /// <summary>
        /// 电子天平
        /// </summary>
        public static WB.XiangPing.Balance.XiangPing_Balance Wber_min
        {
            get { return wber_min; }
        }

        static RW.HFReader.HFReaderRwer readRwer = new RW.HFReader.HFReaderRwer();
        /// <summary>
        /// 读卡器
        /// </summary>
        public static RW.HFReader.HFReaderRwer ReadRwer
        {
            get { return readRwer; }
        }
    }
}
