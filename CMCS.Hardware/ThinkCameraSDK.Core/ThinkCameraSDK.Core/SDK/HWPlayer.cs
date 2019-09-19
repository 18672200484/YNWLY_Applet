using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;

namespace ThinkCameraSDK.Core.SDK
{
    /// <summary>
    /// 视频流
    /// </summary>
    public class HWPlayer
    {
        public enum VIDEOFORMAT : uint
        {
            RGB_FORMAT = 1,
            H264_FORMAT = 2,
            YUV_FORMAT = 3
        }
         /*********************************************************************************
        函数：RealStreamCallBack
        描述：获取实时视频流的回调函数
        参数：
        pUserData 用户设置回调函数时，传入的用户数据
        hVideoHandle 视频流句柄 
        pBuffer 视频数据
        nSize   视频数据长度
        返回值：无
        **********************************************************************************/

        public delegate void RealStreamCallBack(IntPtr pUserData,IntPtr hVideoHandle,IntPtr pBuffer,UInt32 nSize);
        /*********************************************************************************
    函数：HWPlayer_Init
    描述：初始化
    参数：
	    bShowVideo false为不显示,true为显示，默认为不显示。当该参数为false时，调用此
	    函数HWPlayer_SetPlayWnd无效，并且不支持RGB_FORMAT、YUV_FORMAT格式的数据输出

    返回值：
	    true 初始化成功，false初始化失败
    **********************************************************************************/
        [DllImport("HWPlayerDLL.dll", CharSet = CharSet.Ansi)]
        public static extern bool HWPlayer_Init(bool bShowVideo);
        /*********************************************************************************
    函数：HWPlayer_Open
    描述：打开视频流
    参数：
    chVideoFileName 视频文件名称，如果是网络流，输入rtsp地址；如果是视频文件，则输入视频文件名称
    wVideoType 视频类型，1表示网络视频流；2表示文件视频流

    返回值：视频流句柄，如果执行失败，则返回INVALID_VIDEOHANLE
    **********************************************************************************/
        [DllImport("HWPlayerDLL.dll", CharSet = CharSet.Ansi)]
        public static extern IntPtr HWPlayer_Open(StringBuilder chVideoFileName,UInt16 wVideoType);

                /*********************************************************************************
        函数：HWPlayer_SetPlayWnd
        描述：设置播放视频窗体句柄
        参数：
        hVideoHandle 要显示的视频流句柄
        hPlayWnd 显示视频窗体句柄，如果为NULL，则不显示

        返回值：true表示设置成功；false表示设置失败
        **********************************************************************************/

        [DllImport("HWPlayerDLL.dll", CharSet = CharSet.Ansi)]
        public static extern bool HWPlayer_SetPlayWnd(IntPtr hVideoHandle, IntPtr hPlayWnd);


        /*********************************************************************************
        函数：HWPlayer_Pause
        描述：控制视频的暂停、继续
        参数：
        hVideoHandle 要控制的视频流句柄
        bPause true表示暂停、false表示继续

        返回值：true表示设置成功；false表示设置失败
        **********************************************************************************/
        [DllImport("HWPlayerDLL.dll", CharSet = CharSet.Ansi)]
        public static extern bool HWPlayer_Pause(IntPtr hVideoHandle,bool bPause);

        /*********************************************************************************
        函数：HWPlayer_Close
        描述：关闭视频接收或停止播放
        参数：
        hVideoHandle 要控制的视频流句柄
        返回值：true表示设置成功；false表示设置失败
        **********************************************************************************/
        [DllImport("HWPlayerDLL.dll", CharSet = CharSet.Ansi)]
        public static extern bool HWPlayer_Close(IntPtr hVideoHandle);


        /*********************************************************************************
        函数：HWPlayer_SetCallback
描述：设置视频接收的回调函数
参数：
hVideoHandle 要响应的视频流句柄
pUserData   传入用户数据，回调时作为第一个参数传出
fucCallback 转换数据回调函数
fucCallbackH264 264数据回调函数
返回值：true表示设置成功；false表示设置失败
        **********************************************************************************/
        [DllImport("HWPlayerDLL.dll", CharSet = CharSet.Ansi,CallingConvention = CallingConvention.StdCall, EntryPoint = "HWPlayer_SetCallback", SetLastError = true)]
        public static extern bool HWPlayer_SetCallback(IntPtr hVideoHandle, IntPtr pUserData, VIDEOFORMAT videoFormat, RealStreamCallBack fucCallback, RealStreamCallBack fucCallbackH264);

        /*********************************************************************************
        函数：HWPlayer_Quit
        描述：退出程序时，释放所有资源
        参数：
        无参数
        返回值：无返回值
        **********************************************************************************/
        [DllImport("HWPlayerDLL.dll", CharSet = CharSet.Ansi)]
        public static extern void  HWPlayer_Quit();

        /*********************************************************************************
        函数：ConvertH264ToAvi
        描述：将264文件转为AVI
        参数：
        chH264File  264文件
        chAVIFile   avi文件
        nFrameRate
        返回值：false表示失败、true表示成功
        **********************************************************************************/
        [DllImport("HWPlayerDLL.dll", CharSet = CharSet.Ansi)]
        public static extern bool ConvertH264ToAvi(StringBuilder chH264File, StringBuilder chAVIFile, int nFrameRate);

       

        /*********************************************************************************
        函数：HWPlayer_GetFrameRate
        描述：获取帧率
        参数：
        hVideoHandle 要获取的视频流句柄
        fFrameRate  返回帧率
        返回值：false表示失败、true表示成功

        **********************************************************************************/
        [DllImport("HWPlayerDLL.dll", CharSet = CharSet.Ansi)]
        public static extern bool  HWPlayer_GetFrameRate(IntPtr hVideoHandle,ref float fFrameRate);

        /*********************************************************************************
       函数：CaptureImage
       描述：抓拍图像
       参数：
       hVideoHandle 要获取的视频流句柄
       pImageData  一帧图像数据，格式是RGB
       返回值：false表示失败、true表示成功
       **********************************************************************************/

        [DllImport("HWPlayerDLL.dll", CharSet = CharSet.Ansi)]
        public static extern bool GetImageSize(IntPtr hVideoHandle, ref Int32 nImageWidth,ref Int32  nImageHeight);


        /*********************************************************************************
        函数：CaptureImage
        描述：抓拍图像
        参数：
        hVideoHandle 要获取的视频流句柄
        pImageData  一帧图像数据，格式是RGB
        返回值：false表示失败、true表示成功


        **********************************************************************************/
        [DllImport("HWPlayerDLL.dll", CharSet = CharSet.Ansi)]
        public static extern bool CaptureImage(IntPtr hVideoHandle,IntPtr pImageData);

       
    }
}
