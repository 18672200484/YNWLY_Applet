
namespace CMCS.CarTransport.Out.Core
{
    /// <summary>
    /// 硬件设备类
    /// </summary>
    public class Hardwarer
    {
        static IOC.JMDM20DIOV2.JMDM20DIOV2Iocer iocer = new IOC.JMDM20DIOV2.JMDM20DIOV2Iocer();
        /// <summary>
        /// IO控制器
        /// </summary>
        public static IOC.JMDM20DIOV2.JMDM20DIOV2Iocer Iocer
        {
            get { return iocer; }
        }

        static ThinkCameraSDK.Core.ThinkCameraData rwer1 = new ThinkCameraSDK.Core.ThinkCameraData();
        /// <summary>
        /// 车号识别1
        /// </summary>
        public static ThinkCameraSDK.Core.ThinkCameraData Rwer1
        {
            get { return rwer1; }
        }

        static ThinkCameraSDK.Core.ThinkCameraData rwer2 = new ThinkCameraSDK.Core.ThinkCameraData();
        /// <summary>
        /// 车号识别2
        /// </summary>
        public static ThinkCameraSDK.Core.ThinkCameraData Rwer2
        {
            get { return rwer2; }
        }
    }
}
