using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing.Imaging;
using System.Drawing;
using System.IO;
using System.Threading;

namespace ZAZ.Finger
{
    /// <summary>
    /// 指纹识别器 
    /// 指纹识别正常流程：打开设备→获取指纹图像→生成特征1→获取指纹图像→生成特征2→合成指纹模版→存储特征1到指纹数据库
    /// </summary>
    public class FingerMachine
    {
        private Int32 errorCode;
        /// <summary>
        /// 当前错误代码
        /// </summary>
        public Int32 ErrorCode
        {
            get { return errorCode; }
            set { errorCode = value; }
        }

        private string messageStr = string.Empty;
        /// <summary>
        /// 当前输出信息
        /// </summary>
        public string MessageStr
        {
            get { return messageStr; }
            set { messageStr = value; }
        }

        private bool status = false;
        /// <summary>
        /// 连接状态
        /// </summary>
        public bool Status
        {
            get { return status; }
        }

        public delegate void ScanErrorEventHandler(Exception ex);
        public event ScanErrorEventHandler OnScanError;

        public delegate void StatusChangeHandler(bool status);
        public event StatusChangeHandler OnStatusChange;

        /// <summary>
        /// 设置连接状态
        /// </summary>
        /// <param name="status"></param>
        public void SetStatus(bool status)
        {
            if (this.status != status && this.OnStatusChange != null) this.OnStatusChange(status);
            this.status = status;
        }

        /// <summary>
        /// 当前连接句柄
        /// </summary>
        IntPtr hHandle = new IntPtr();

        /// <summary>
        /// 是否USB连接
        /// </summary>
        bool IsUsb = true;

        /// <summary>
        /// 图像大小
        /// </summary>
        public const int Image_Size = (256 * 288);

        /// <summary>
        /// 设备地址
        /// </summary>
        public UInt32 nDevAddr = 0xffffffff;

        /// <summary>
        /// 当前预览图像
        /// </summary>
        public Image CurrentImage = null;

        private Int32 fingerId;
        /// <summary>
        /// 当前存储指纹Id号
        /// </summary>
        public Int32 FingerId
        {
            get { return fingerId; }
            set { fingerId = value; }
        }

        public bool zAZGenChar1 = false;
        /// <summary>
        /// 特征1生成状态
        /// </summary>
        public bool ZAZGenChar1
        {
            get { return zAZGenChar1; }
            set { zAZGenChar1 = value; }
        }

        public bool zAZGenChar2 = false;
        /// <summary>
        /// 特征2生成状态
        /// </summary>
        public bool ZAZGenChar2
        {
            get { return zAZGenChar2; }
            set { zAZGenChar2 = value; }
        }

        private Int32 ret;
        /// <summary>
        /// 当前执行结果
        /// </summary>
        public Int32 Ret
        {
            get { return ret; }
            set { ret = value; }
        }

        private Int32 timeOut;
        /// <summary>
        /// 记时
        /// </summary>
        public Int32 TimeOut
        {
            get { return timeOut; }
            set { timeOut = value; }
        }

        public FingerMachine()
        {

        }

        /// <summary>
        /// 打开设备
        /// </summary>
        /// <returns></returns>
        public bool OpenDeviceEx()
        {
            int ret = 0;
            int devce_usb = 0;
            byte[] iPwd = new byte[4];
            uint nDevAddr = 0xffffffff;
            ret = Fingerdll.ZAZOpenDeviceEx(ref hHandle, 2, 0, 0, 2, 0);//设备句柄，2： 无驱 UDISK 设备，无驱 UDISK 设备该参数为 0，波特率，通讯包大小（默认:2），通讯端口号(默认 0)
            if (ret == 0)
            {
                devce_usb = 2;  //无驱
            }
            if (devce_usb == 1)//有驱动 需要验证
            {
                if (Fingerdll.ZAZVfyPwd(hHandle, nDevAddr, iPwd) == 0)
                {
                    this.MessageStr = "打开USB设备成功";
                    SetStatus(true);
                    return true;
                }
                else
                {
                    this.ErrorCode = ret;
                    this.MessageStr = "打开USB设备失败,请查看设备是否连接";
                    return false;
                }
            }
            else if (devce_usb == 2)
            {
                this.MessageStr = "打开USB设备成功";
                SetStatus(true);
                return true;
            }
            else
            {
                this.ErrorCode = ret;
                this.MessageStr = "打开USB设备失败,请查看设备是否连接";
                return false;
            }
        }

        /// <summary>
        /// 关闭设备
        /// </summary>
        /// <returns></returns>
        public bool CloseDeviceEx()
        {
            if (Fingerdll.ZAZCloseDeviceEx(hHandle) == 0)
            {
                SetStatus(false);
                return true;
            }
            return false;
        }

        /// <summary>
        /// 注册指纹
        /// </summary>
        /// <returns></returns>
        public bool RegistFinger(int fingerId)
        {
            this.FingerId = fingerId;
            int devce_usb = 0;
            UInt32 nDevAddr = 0xffffffff;
            int ImgLen = 0;
            string strFile = "";
            string sPath = "";
            int iBuffer = 0;
            int timeout = 20;   //定义等待超时
            int m_Addr = 1;//本例将指纹存储在设备库位置1中 
            if (this.Status == false)
            {
                this.MessageStr = "请先打开设备";
                return false;
            }
            //获取指纹图像  4.上传指纹图像(可省略) 5.显示指纹图像(可省略)  6.生成特征A  
            if (!ZAZGenChar1)
            {
                if (GetFingerImage(nDevAddr, 1))
                    ZAZGenChar1 = true;
                else
                    return false;
            }
            //获取指纹图像  8.上传指纹图像(可省略) 9.显示指纹图像(可省略) 10.生成特征B
            if (!ZAZGenChar2)
            {
                if (GetFingerImage(nDevAddr, 2))
                    ZAZGenChar2 = true;
                else
                    return false;
            }
            /****************合成模板*********/
            Ret = Fingerdll.ZAZRegModule(hHandle, nDevAddr);  //合并特征
            if (ret != 0)
            {
                this.MessageStr = Fingerdll.ZAZErr2Strt(ret);
                return false;
            }
            else
            {
                this.MessageStr = "合成指纹模板成功";
            }

            //本例以存在在指纹设备库中进行
            ret = Fingerdll.ZAZStoreChar(hHandle, nDevAddr, 1, this.FingerId);    //存放模板
            if (ret != 0)
            {
                this.MessageStr = Fingerdll.ZAZErr2Strt(ret);
                return false;
            }
            else
            {
                this.MessageStr = "存储指纹成功";
                return true;
            }
        }

        /// <summary>
        /// 获取指纹
        /// </summary>
        /// <param name="buffer">指纹在设备中临时存放位置 1 2</param>
        /// <returns></returns>
        public bool GetFinger(int buffer = 1)
        {
            TimeOut = 20;
            int ret = 0;
            byte[] ImgData = new byte[Image_Size];
            int[] ImgLen = new int[1];
            int iBuffer = buffer;

        BEIG1:
            ret = Fingerdll.ZAZGetImage(hHandle, nDevAddr);  //获取图象 
            if (ret == 0)
            {
                ShowInfomation("获取图像成功...");
            }
            else if (ret == 2)
            {
                //超时判断
                ShowInfomation("等待手指平放在传感器上-" + TimeOut.ToString() + "秒");
                if (TimeOut < 0)
                {
                    ShowInfomation("等待超时");
                    return false;
                }
                TimeOut--;
                Thread.Sleep(1000);
                goto BEIG1;
            }
            else
            {
                ShowInfomation(Fingerdll.ZAZErr2Strt(ret));
                return false;
            }

            //////////////////////////////////////////////////////////////////////////
            //不涉及图像，下面可以省略
            /****************上传图像*********/

            ShowInfomation("正在上传图像请等待...");
            ret = Fingerdll.ZAZUpImage(hHandle, nDevAddr, ImgData, ImgLen);  //上传图象
            if (ret != 0)
            {
                ShowInfomation(Fingerdll.ZAZErr2Strt(ret));
                return false;
            }
            //strFile = System.Windows.Forms.Application.StartupPath + "\\ZAZFinger.bmp";
            string strFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ZAZFinger.bmp");
            ret = Fingerdll.ZAZImgData2BMP(ImgData, strFile);
            if (ret != 0)
            {
                ShowInfomation(Fingerdll.ZAZErr2Strt(ret));
                return false;
            }
            ShowImage(strFile);
            //ret = Fingerdll.ZAZShowFingerData(fpbmp.Handle, ref ImgData[0]);
            //////////////////////////////////////////////////////////////////////////
            /****************生成特征 *********/
            ret = Fingerdll.ZAZGenChar(hHandle, nDevAddr, iBuffer);  //生成模板
            if (ret != 0)
            {
                ShowInfomation(Fingerdll.ZAZErr2Strt(ret));
                return false;
            }
            else
            {
                ShowInfomation("生成指纹特征" + buffer.ToString());
            }
            Thread.Sleep(10);
        BEIG2:
            if (ret == 0)
            { //超时判断

                string strTemp;
                ret = Fingerdll.ZAZGetImage(hHandle, nDevAddr);  //获取图象 
                ShowInfomation("等待手指拿开-");
                // if (timeout < 0)
                // { ShowInfomation("等待超时"); return 0; }
                // timeout--;
                // Thread.Sleep(10);
                goto BEIG2;
            }
            else if (ret == 1)
            {
                ShowInfomation(Fingerdll.ZAZErr2Strt(ret));
                return false;
            }
            return true;
        }

        /// <summary>
        /// 将Dat文件的指纹下载到设备的地址2
        /// </summary>
        /// <returns></returns>
        public bool GetDatFinger(string datUrl)
        {
            Ret = Fingerdll.ZAZDownCharFromFile(hHandle, nDevAddr, 2, datUrl);
            if (Ret == 0)
                return true;
            return false;
        }

        /// <summary>
        /// 比对指纹  比对之前保证设备地址1中为当前获取到的指纹 地址2为要比对的指纹
        /// </summary>
        /// <returns></returns>
        public bool ZAZMatch()
        {
            int[] nScore = new int[0];
            ret = Fingerdll.ZAZMatch(hHandle, nDevAddr, nScore);  //比对模板
            if (nScore[0] > 50)
                return true;
            return false;
        }

        /// <summary>
        /// 显示指纹图像
        /// </summary>
        /// <param name="bmpFileName"></param>
        private void ShowImage(string bmpFileName)
        {
            FileStream pFileStream = new FileStream(bmpFileName, FileMode.Open, FileAccess.Read);
            this.CurrentImage = Image.FromStream(pFileStream);
        }


        /// <summary>
        /// 生成指纹图像
        /// </summary>
        /// <returns></returns>
        public bool GetFingerImage(UInt32 nDevAddr, int iBuffer)
        {
            byte[] ImgData = new byte[Image_Size];
            int[] ImgLen = new int[1];
            int timeout = 0;
            //while (Ret == 2)
            //{
            //    Ret = Fingerdll.ZAZGetImage(hHandle, nDevAddr);  //获取图象 
            //    this.MessageStr = "请将手指平放在传感器上...";
            //    Thread.Sleep(100);
            //    timeout++;
            //    if (timeout > 100)
            //        return false;
            //}

            Ret = Fingerdll.ZAZGetImage(hHandle, nDevAddr);  //获取图象 
            if (Ret == 2)
            {
                this.MessageStr = "请将手指平放在传感器上...";
            }
            else if (Ret == 0)
            {
                this.MessageStr = "获取图像成功...";
                if (IsUsb)
                {
                    this.MessageStr = "正在上传图像请等待...";
                    Ret = Fingerdll.ZAZUpImage(hHandle, nDevAddr, ImgData, ImgLen);  //上传图象
                    if (Ret != 0)
                    {
                        this.MessageStr = Fingerdll.ZAZErr2Strt(Ret);
                        return false;
                    }
                    string strFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ZAZFinger.bmp");
                    Ret = Fingerdll.ZAZImgData2BMP(ImgData, strFile);
                    if (Ret != 0)
                    {
                        this.MessageStr = Fingerdll.ZAZErr2Strt(Ret);
                        return false;
                    }
                    FileStream pFileStream = new FileStream(strFile, FileMode.Open, FileAccess.Read);
                    this.CurrentImage = Image.FromStream(pFileStream);
                    return true;
                }

                //////////////////////////////////////////////////////////////////////////
                /****************生成特征 *********/
                Ret = Fingerdll.ZAZGenChar(hHandle, nDevAddr, iBuffer);  //生成模板
                if (Ret != 0)
                {
                    this.MessageStr = Fingerdll.ZAZErr2Strt(Ret);
                    return false;
                }
                else
                {
                    this.MessageStr = "生成指纹特征" + iBuffer.ToString();
                }
            BEIG2:
                if (ret == 0)
                { //超时判断

                    Ret = Fingerdll.ZAZGetImage(hHandle, nDevAddr);  //获取图象 
                    this.MessageStr = "等待手指拿开-";
                    goto BEIG2;
                }
                if (Ret == 1)
                {
                    this.MessageStr = Fingerdll.ZAZErr2Strt(Ret);
                    return false;
                }
            }
            else
            {
                this.MessageStr = Fingerdll.ZAZErr2Strt(ret);
            }
            return false;
        }

        /// <summary>
        /// 读取指纹页地址
        /// </summary>
        /// <returns></returns>
        public string ReadIndex()
        {
            uint nDevAddr = 0xffffffff;
            byte[] indextable = new byte[32];

            string tmp = "";
            int temp, ttt;
            for (int moban = 0; moban < 4; moban++)
            {
                Ret = Fingerdll.ZAZReadIndexTable(hHandle, nDevAddr, moban, indextable);
                if (Ret == 0)
                {
                    for (int ari = 0; ari < 32; ari++)
                    {
                        for (int i = 0; i < 8; i++)
                        {
                            temp = indextable[ari];
                            ttt = (0x01 << i);
                            temp &= ttt;
                            if (temp != 0)
                            {
                                int m_Addr;
                                m_Addr = moban * 32 * 8 + ari * 8 + i;
                                tmp += m_Addr.ToString() + ",";
                            }
                        }
                    }
                }
            }
            return tmp;
        }

        /// <summary>
        /// 删除指纹
        /// </summary>
        /// <param name="FingerId">指纹Id</param>
        /// <returns></returns>
        public bool DelFinger(int FingerId)
        {
            Ret = Fingerdll.ZAZDelChar(hHandle, nDevAddr, FingerId, 1);
            if (Ret == 0) return true;
            else
            {
                this.MessageStr = GetErrorStr(Ret);
                return false;
            }
        }

        /// <summary>
        /// 根据错误代码获取错误信息
        /// </summary>
        /// <param name="nErrorCode"></param>
        /// <returns></returns>
        public string GetErrorStr(int nErrorCode)
        {
            return Fingerdll.ZAZErr2Strt(nErrorCode);
        }

        public void ShowInfomation(string message)
        {
            this.MessageStr = message;
        }
    }
}
