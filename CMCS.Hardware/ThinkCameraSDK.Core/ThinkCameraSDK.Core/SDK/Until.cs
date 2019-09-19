using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ThinkCameraSDK.Core.SDK
{
    /// <summary>
    /// 帮助类
    /// </summary>
   public class Until
    {
        //Uint32 转Ip地址
        public static string Int2IP(UInt32 ipCode)
        {
            byte a = (byte)(ipCode & 0x000000FF);
            byte b = (byte)((ipCode & 0x0000FF00) >> 8);
            byte c = (byte)((ipCode & 0x00FF0000) >> 16);
            byte d = (byte)((ipCode & 0xFF000000) >> 24);
            string ipStr = String.Format("{0}.{1}.{2}.{3}", a, b, c, d);
            return ipStr;
        }

        //ip地址字符串转整数
        public static UInt32 IP2Int(string ipStr)
        {
            string[] ip = ipStr.Split('.');
            UInt32 a = (UInt32)byte.Parse(ip[0]);
            UInt32 b = (UInt32)byte.Parse(ip[1]) << 8;
            UInt32 c = (UInt32)byte.Parse(ip[2]) << 16;
            UInt32 d = (UInt32)byte.Parse(ip[3]) << 24;
            UInt32 ipCode = (UInt32)(0x000000FF & a);
            ipCode = ipCode | (UInt32)(0x0000FF00 & b);
            ipCode = ipCode | (UInt32)(0x00FF0000 & c);
            ipCode = ipCode | (UInt32)(0xFF000000 & d);
            return ipCode;
        }
    }
}
