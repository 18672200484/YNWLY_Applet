using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using ThinkCameraSDK.Core.SDK;

namespace ThinkCameraSDK.Core
{
    public class ThinkCameraData
    {
        /// <summary>
        /// 当前连接设备句柄
        /// </summary>
        IntPtr pHandle;

        /// <summary>
        /// 视频预览返回句柄
        /// </summary>
        IntPtr hVideoHandle;

        /// <summary>
        /// 当前设备IP
        /// </summary>
        string Ip;

        /// <summary>
        /// 抓拍路径
        /// </summary>
        static string CaptureFilePath;

        /// <summary>
        /// 当前车牌号
        /// </summary>
        public string CarNumber = "无车牌";

        /// <summary>
        /// 当前错误消息
        /// </summary>
        public string ErrorStr;

        /// <summary>
        /// 数据回传方法
        /// </summary>
        public RealdataCallBack RealdataFuc = null;

        /// <summary>
        /// /响应设备状态回调函数
        /// </summary>
        public DeviceStatusCallback DeviceStatusFuc = null;
        /// <summary>
        /// 响应网络状态回调函数
        /// </summary>
        public NetStatusCallback NetStatusFuc = null;
        public DeviceRunStatusCallback RunStatusFuc = null;

        public Action<string> OnActionReadSuccess;

        public Action<bool> OnActionStatusChange;

        public Action<Exception> OnActionScanError;

        public ThinkCameraData()
        {
            RealdataFuc = new RealdataCallBack(Realdata);
            DeviceStatusFuc = new DeviceStatusCallback(DeviceStatus);
            NetStatusFuc = new NetStatusCallback(NetStatus);
        }
        /// <summary>
        /// 连接设备
        /// </summary>
        /// <param name="sIP">设备IP</param>
        /// <returns></returns>
        public bool ConnectCamera(string sIP, IntPtr UserHandle)
        {
            try
            {
                StringBuilder strIP = new StringBuilder(sIP);
                pHandle = IntPtr.Zero;
                Ip = sIP;
                uint uRes = DeviceInterface.HWTC_ConnectCamera(strIP, ref pHandle);
                if ((DeviceInterface.FEEKBACK_TYPE)uRes == DeviceInterface.FEEKBACK_TYPE.RESULT_OK)
                {
                    DeviceInterface.HWTC_RecordLog(false);
                    DeviceInterface.HWTC_SetRecRealtimeDataMode(pHandle, IntPtr.Zero, RealdataFuc, UserHandle, 0, 0);
                    //设置事件
                    DeviceInterface.HWTC_RegStatusCallback(pHandle, IntPtr.Zero, DeviceStatusFuc, NetStatusFuc, RunStatusFuc);
                    AddDeviceInfo Info = new AddDeviceInfo();
                    Info.intCameraIP = Until.IP2Int(sIP);
                    Info.ptrCameraHandle = pHandle;
                    Info.u32ArmBootTime = 0;
                    Info.u32DspBootTime = 0;
                    Info.u32DspBootCnt = 0;
                    Info.ptrCamControlHandle = IntPtr.Zero;
                    Info.OnReadSuccess = this.OnActionReadSuccess;
                    Info.OnScanError = this.OnActionScanError;
                    Info.OnStatusChange = this.OnActionStatusChange;

                    if (ForGlobal.g_DeviceTable[sIP] == null)
                        ForGlobal.g_DeviceTable.Add(sIP, Info);
                    if (OnActionStatusChange != null) OnActionStatusChange(true);
                    return true;
                }
                else
                {
                    ErrorStr = uRes.ToString();
                    if (OnActionStatusChange != null) OnActionStatusChange(false);
                    return false;
                }
            }
            catch (Exception)
            {
                if (OnActionStatusChange != null) OnActionStatusChange(false);
            }
            return false;
        }

        /// <summary>
        /// 关闭设备
        /// </summary>
        /// <returns></returns>
        public bool Close()
        {
            uint uRes = DeviceInterface.HWTC_DisconnectCamera(this.pHandle);
            if ((DeviceInterface.FEEKBACK_TYPE)uRes == DeviceInterface.FEEKBACK_TYPE.RESULT_OK)
            {
                //关闭视频预览
                HWPlayer.HWPlayer_Quit();
                ForGlobal.g_DeviceTable = null;
                return true;
            }
            else
            {
                ErrorStr = uRes.ToString();
                return false;
            }
        }

        /// <summary>
        /// 抓拍图片
        /// </summary>
        /// <returns></returns>
        public bool Capture(string filePath)
        {
            CaptureFilePath = filePath;
            uint uRes = DeviceInterface.HWTC_Capture(this.pHandle);
            if ((DeviceInterface.FEEKBACK_TYPE)uRes == DeviceInterface.FEEKBACK_TYPE.RESULT_OK)
            {
                return true;
            }
            else
            {
                ErrorStr = uRes.ToString();
                return false;
            }
        }

        /// <summary>
        /// 开始预览
        /// </summary>
        /// <param name="Handle"></param>
        /// <returns></returns>
        public bool StartPreview(IntPtr Handle)
        {
            if (string.IsNullOrEmpty(this.Ip)) return false;
            HWPlayer.HWPlayer_Init(true);
            hVideoHandle = HWPlayer.HWPlayer_Open(new StringBuilder(string.Format("rtsp://{0}:8557/PSIA/Streaming/channels/2?videoCodecType=H.264", this.Ip)), 1);
            if (hVideoHandle != IntPtr.Zero)
            {
                HWPlayer.HWPlayer_SetPlayWnd(hVideoHandle, Handle);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 停止预览
        /// </summary>
        /// <returns></returns>
        public bool StopPreview()
        {
            return HWPlayer.HWPlayer_Close(hVideoHandle);
        }

        /// <summary>
        /// 重新预览
        /// </summary>
        /// <param name="Handle"></param>
        /// <returns></returns>
        public bool Preview(IntPtr Handle)
        {
            StopPreview();
            return StartPreview(Handle);
        }

        //响应实时数据回调函数
        public void Realdata(ref DeviceInterface.AnalysisData pAnalyData)
        {
            string sIP = string.Empty;
            AddDeviceInfo info = new AddDeviceInfo();
            foreach (DictionaryEntry de in ForGlobal.g_DeviceTable)
            {
                info = (AddDeviceInfo)ForGlobal.g_DeviceTable[de.Key];
                if (info.intCameraIP == pAnalyData.CamInfo.netInfo.intCameraIP)
                {
                    sIP = de.Key.ToString();
                    break;
                }
            }
            sIP = Until.Int2IP(pAnalyData.CamInfo.netInfo.intCameraIP);
            for (int i = 0; i < pAnalyData.chObjNum; i++)
            {
                DealData(sIP, pAnalyData.pDataInfo[i], info);
            }
        }

        /// <summary>
        /// 处理数据
        /// </summary>
        /// <param name="sIP"></param>
        /// <param name="ptrData"></param>
        public void DealData(string sIP, IntPtr ptrData, AddDeviceInfo info)
        {
            if (ptrData == IntPtr.Zero)
                return;

            DeviceInterface.TCData pData = (DeviceInterface.TCData)Marshal.PtrToStructure(ptrData, typeof(DeviceInterface.TCData));

            //存储数据
            string sPlate = Encoding.Default.GetString(pData.chPlate);
            sPlate = sPlate.Substring(0, sPlate.IndexOf("\0"));

            string sColor = string.Empty;
            if (sPlate == "")
            {
                sPlate = "无车牌";
            }
            else
            {
                if (info.OnReadSuccess != null) info.OnReadSuccess(sPlate);
                if (sPlate != "无车牌") CarNumber = sPlate;
                switch (pData.PlateColor)
                {
                    case DeviceInterface.PLATE_COLOR.YELLOW_COLOR:
                        sColor = "黄";
                        break;
                    case DeviceInterface.PLATE_COLOR.BLUE_COLOR:
                        sColor = "蓝";
                        break;
                    case DeviceInterface.PLATE_COLOR.WHITE_COLOR:
                        sColor = "白";
                        break;
                    case DeviceInterface.PLATE_COLOR.BLACK_COLOR:
                        sColor = "黑";
                        break;
                    default:
                        sColor = "未知";
                        break;
                }
            }

            //if (!Directory.Exists(CaptureFilePath))
            //{
            //    Directory.CreateDirectory(CaptureFilePath);
            //}
            FileStream fs = null;
            string strFile = string.Empty;
            //判断图像类型
            for (int i = 0; i < pData.usImageCount; i++)
            {
                switch (pData.ImgAttrs[i].imgType)
                {
                    //全景图或者二值化图
                    case DeviceInterface.IMAGE_TYPE.FULL_IMAGE:
                        {
                            switch (pData.ImgAttrs[i].imgFormat)
                            {
                                case DeviceInterface.IMAGE_FORMAT.IMGFORMAT_JPG:
                                    {
                                        if (pData.ImgAttrs[i].uiImgLen > 0)
                                        {
                                            strFile = CaptureFilePath;
                                            fs = new FileStream(strFile, FileMode.Create, FileAccess.Write);
                                            if (fs != null)
                                            {
                                                byte[] bytData = new byte[pData.ImgAttrs[i].uiImgLen];
                                                Marshal.Copy(pData.ImgAttrs[i].pImgData, bytData, 0, (int)pData.ImgAttrs[i].uiImgLen);
                                                fs.Write(bytData, 0, (int)pData.ImgAttrs[i].uiImgLen);
                                                fs.Flush();
                                                fs.Close();
                                            }
                                        }

                                    }
                                    break;
                                case DeviceInterface.IMAGE_FORMAT.IMGFORMAT_BIN:
                                    {
                                        if (pData.ImgAttrs[i].uiImgLen > 0)
                                        {
                                            strFile = CaptureFilePath;
                                            StringBuilder sbFile = new StringBuilder(strFile);
                                            DeviceInterface.HWTC_BinaryTOBmp(sbFile, pData.ImgAttrs[i].pImgData, pData.ImgAttrs[i].usWidth,
                                               pData.ImgAttrs[i].usHeight);

                                        }
                                    }
                                    break;
                                default:
                                    break;
                            }
                        }
                        break;
                    //车牌图
                    case DeviceInterface.IMAGE_TYPE.SPECIAL_IMAGE:
                        {
                            switch (pData.ImgAttrs[i].imgFormat)
                            {
                                case DeviceInterface.IMAGE_FORMAT.IMGFORMAT_JPG:
                                    {
                                        if (pData.ImgAttrs[i].uiImgLen > 0)
                                        {
                                            strFile = CaptureFilePath;
                                            fs = new FileStream(strFile, FileMode.Create, FileAccess.Write);
                                            if (fs != null)
                                            {
                                                byte[] bytData = new byte[pData.ImgAttrs[i].uiImgLen];
                                                Marshal.Copy(pData.ImgAttrs[i].pImgData, bytData, 0, (int)pData.ImgAttrs[i].uiImgLen);
                                                fs.Write(bytData, 0, (int)pData.ImgAttrs[i].uiImgLen);
                                                fs.Flush();
                                                fs.Close();
                                            }
                                        }
                                    }
                                    break;
                                case DeviceInterface.IMAGE_FORMAT.IMGFORMAT_BIN:
                                    break;
                                default:
                                    break;
                            }
                        }
                        break;
                    default:
                        break;
                }
            }

        }

        /// <summary>
        /// 响应设备状态回调函数
        /// </summary>
        /// <param name="pHandle"></param>
        /// <param name="pUserData"></param>
        /// <param name="pDeviceStatus"></param>
        public void DeviceStatus(IntPtr pHandle, IntPtr pUserData, ref DeviceInterface.StDeviceStatus pDeviceStatus)
        {
            string sIP = string.Empty;
            foreach (DictionaryEntry de in ForGlobal.g_DeviceTable)
            {

                AddDeviceInfo info = (AddDeviceInfo)ForGlobal.g_DeviceTable[de.Key];
                if (info.ptrCameraHandle == pHandle)
                {
                    sIP = de.Key.ToString();
                    info.u32ArmBootTime = pDeviceStatus.u32ArmBootTime;
                    info.u32DspBootCnt = pDeviceStatus.u32DspBootCnt;
                    info.u32DspBootTime = pDeviceStatus.u32DspBootTime;
                    //交给外部程序处理
                    this.DealDeviceStatus(sIP, info);
                    break;
                }
            }
        }
        /// <summary>
        /// 响应控制通道网络状态回调函数
        /// </summary>
        /// <param name="pUserData"></param>
        /// <param name="pHandle"></param>
        /// <param name="ConnStatus"></param>
        public void NetControlStatus(IntPtr pUserData, IntPtr pHandle, DeviceInterface.CAMERA_STATUS ConnStatus)
        {
            string sIP = string.Empty;
            foreach (DictionaryEntry de in ForGlobal.g_DeviceTable)
            {

                AddDeviceInfo info = (AddDeviceInfo)ForGlobal.g_DeviceTable[de.Key];
                if (info.ptrCamControlHandle == pHandle)
                {
                    sIP = de.Key.ToString();

                    //交给外部程序处理

                    break;
                }
            }
        }
        /// <summary>
        /// 响应网络状态回调函数
        /// </summary>
        /// <param name="pHandle"></param>
        /// <param name="pUserData"></param>
        /// <param name="ConnStatus"></param>
        public void NetStatus(IntPtr pHandle, IntPtr pUserData, DeviceInterface.CAMERA_STATUS ConnStatus)
        {
            string sIP = string.Empty;
            foreach (DictionaryEntry de in ForGlobal.g_DeviceTable)
            {

                AddDeviceInfo info = (AddDeviceInfo)ForGlobal.g_DeviceTable[de.Key];
                if (info.ptrCameraHandle == pHandle)
                {
                    sIP = de.Key.ToString();

                    //交给外部程序处理
                    this.DealNetStatus(sIP, ConnStatus);
                    break;
                }
            }

            //交给外部程序处理
            this.DealNetStatus(Ip, ConnStatus);
        }
        /// <summary>
        /// 设备运行状态回调
        /// </summary>
        /// <param name="pHandle"></param>
        /// <param name="pUserData"></param>
        /// <param name="ui64AlarmInfoData"></param>
        /// <param name="DevRunInfo"></param>
        public void RunStatus(IntPtr pHandle, IntPtr pUserData, UInt64 ui64AlarmInfoData, ref DeviceInterface.stDevRunInfo DevRunInfo)
        {
            string sIP = string.Empty;
            foreach (DictionaryEntry de in ForGlobal.g_DeviceTable)
            {

                AddDeviceInfo info = (AddDeviceInfo)ForGlobal.g_DeviceTable[de.Key];
                if (info.intCameraIP == DevRunInfo.CamInfo.netInfo.intCameraIP)
                {
                    sIP = de.Key.ToString();

                    break;
                }
            }
        }

        /// <summary>
        /// 处理网络状态
        /// </summary>
        /// <param name="sIP"></param>
        /// <param name="ConnStatus"></param>
        public void DealNetStatus(string sIP, DeviceInterface.CAMERA_STATUS ConnStatus)
        {
            if (ConnStatus == DeviceInterface.CAMERA_STATUS.CONNECT_SUCCESS)
            {
                Console.WriteLine("网络正常");
            }
            else if (ConnStatus == DeviceInterface.CAMERA_STATUS.ABNORMALNET_ERROR)
            {
                Console.WriteLine("网络异常");
            }
        }


        /// <summary>
        /// 处理设备工作状态
        /// </summary>
        /// <param name="sIP"></param>
        /// <param name="info"></param>
        public void DealDeviceStatus(string sIP, AddDeviceInfo info)
        {
            DateTime dt_1970 = new DateTime(1970, 1, 1, 0, 0, 0);//1970年的时间
            const int TICKS_UNIT = 10000000;//时刻单位

            Int64 ArmBootTime = Convert.ToInt64(info.u32ArmBootTime) * TICKS_UNIT;
            Int64 tricks_1970 = dt_1970.Ticks + ArmBootTime;
            DateTime dtArmBootTime = new DateTime(tricks_1970);

            string strArmBootTime = dtArmBootTime.ToString();//设备重启时间

            Int64 DspBootTime = Convert.ToInt64(info.u32DspBootTime) * TICKS_UNIT;
            tricks_1970 = dt_1970.Ticks + DspBootTime;
            DateTime dtDspBootTime = new DateTime(tricks_1970);
            string strDspBootTime = dtDspBootTime.ToString();//dsp复位时间

            UInt32 nDspBootCount = info.u32DspBootCnt;    //dsp启动次数
        }

    }

    //已添加设备信息结构体
    [StructLayout(LayoutKind.Sequential, Pack = 0, CharSet = CharSet.Ansi)]
    public struct AddDeviceInfo
    {
        public UInt32 intCameraIP; //相机IP
        //NET层
        public UInt32 u32ArmBootTime;		//系统启动时间	通过time函数读取自1970年至今的秒数 无时区
        //AVS端信息
        public UInt32 u32DspBootTime;		//DSP启动时间 	通过time函数读取自1970年至今的秒数 无时区
        public UInt32 u32DspBootCnt;		//DSP启动次数
        public IntPtr ptrCameraHandle;//相机数据句柄
        public IntPtr ptrCamControlHandle;//相机控制句柄

        public Action<string> OnReadSuccess;

        public Action<bool> OnStatusChange;

        public Action<Exception> OnScanError;
    }

}
