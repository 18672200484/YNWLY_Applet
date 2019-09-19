
namespace CMCS.BalanceScale.Core
{
    /// <summary>
    /// 硬件设备类
    /// </summary>
    public class Hardwarer
    {
        static WB.Sartorius.Balance.Sartorius_Balance wber1 = new WB.Sartorius.Balance.Sartorius_Balance();

        /// <summary>
        /// 电子天平1
        /// </summary>
        public static WB.Sartorius.Balance.Sartorius_Balance Wber1
        {
            get { return wber1; }
        }

        static WB.Sartorius.Balance.Sartorius_Balance wber2 = new WB.Sartorius.Balance.Sartorius_Balance();

        /// <summary>
        /// 电子天平2
        /// </summary>
        public static WB.Sartorius.Balance.Sartorius_Balance Wber2
        {
            get { return wber2; }
        }

        static WB.Sartorius.Balance.Sartorius_Balance wber3 = new WB.Sartorius.Balance.Sartorius_Balance();

        /// <summary>
        /// 电子天平3
        /// </summary>
        public static WB.Sartorius.Balance.Sartorius_Balance Wber3
        {
            get { return wber3; }
        }

        static WB.Sartorius.Balance.Sartorius_Balance wber4 = new WB.Sartorius.Balance.Sartorius_Balance();

        /// <summary>
        /// 电子天平4
        /// </summary>
        public static WB.Sartorius.Balance.Sartorius_Balance Wber4
        {
            get { return wber4; }
        }

        static WB.Sartorius.Balance.Sartorius_Balance wber5 = new WB.Sartorius.Balance.Sartorius_Balance();

        /// <summary>
        /// 电子天平5
        /// </summary>
        public static WB.Sartorius.Balance.Sartorius_Balance Wber5
        {
            get { return wber5; }
        }

        static WB.Sartorius.Balance.Sartorius_Balance wber6 = new WB.Sartorius.Balance.Sartorius_Balance();

        /// <summary>
        /// 电子天平6
        /// </summary>
        public static WB.Sartorius.Balance.Sartorius_Balance Wber6
        {
            get { return wber6; }
        }

    }
}
