using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMCS.Common.Utilities
{
    /// <summary>
    /// 进制转换
    /// </summary>
    public static class CodeChange
    {
        #region 数组信息
        /// <summary> 
        /// CRC8位校验表 
        /// </summary> 
        //private byte[] CRC8Table = new byte[] { 
        //0,94,188,226,97,63,221,131,194,156,126,32,163,253,31,65, 
        //157,195,33,127,252,162,64,30, 95,1,227,189,62,96,130,220,
        //35,125,159,193,66,28,254,160,225,191,93,3,128,222,60,98,
        //190,224,2,92,223,129,99,61,124,34,192,158,29,67,161,255,
        //70,24,250,164,39,121,155,197,132,218,56,102,229,187,89,7,
        //219,133,103,57,186,228,6,88,25,71,165,251,120,38,196,154,
        //101,59,217,135,4,90,184,230,167,249,27,69,198,152,122,36,
        //248,166,68,26,153,199,37,123,58,100,134,216,91,5,231,185,
        //140,210,48,110,237,179,81,15,78,16,242,172,47,113,147,205,
        //17,79,173,243,112,46,204,146,211,141,111,49,178,236,14,80,
        //175,241,19,77,206,144,114,44,109,51,209,143,12,82,176,238,
        //50,108,142,208,83,13,239,177,240,174,76,18,145,207,45,115,
        //202,148,118,40,171,245,23,73,8,86,180,234,105,55,213,139,
        //87,9,235,181,54,104,138,212,149,203, 41,119,244,170,72,22,
        //233,183,85,11,136,214,52,106,43,117,151,201,74,20,246,168,
        //116,42,200,150,21,75,169,247,182,232,10,84,215,137,107,53 };
        #endregion

        #region CRC8
        /// <summary>
        /// CRC8校验码
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="off"></param>
        /// <param name="len"></param>
        /// <returns></returns>
        //public static byte CRC8(byte[] buffer, int off, int len)
        //{
        //    byte crc = 0;
        //    if (buffer == null)
        //    {
        //        throw new ArgumentNullException("buffer");
        //    }
        //    if (off < 0 || len < 0 || off + len > buffer.Length)
        //    {
        //        throw new ArgumentOutOfRangeException();
        //    }

        //    for (int i = off; i < len; i++)
        //    {
        //        crc = CRC8Table[crc ^ buffer[i]];
        //    }
        //    return crc;
        //}

        public static byte CRC8_1(byte[] buffer)
        {
            byte crc = 0;
            for (int j = 0; j < buffer.Length; j++)
            {
                crc ^= buffer[j];
                for (int i = 0; i < 8; i++)
                {
                    if ((crc & 0x01) != 0)
                    {
                        crc >>= 1;
                        crc ^= 0x8c;
                    }
                    else
                    {
                        crc >>= 1;
                    }
                }
            }
            return crc;
        }


        public static byte CRC8_2(byte[] buffer)
        {
            byte crc = 0;
            for (int j = 0; j < buffer.Length; j++)
            {
                for (int i = 1; i != 0; i *= 2)
                {
                    if ((crc & 1) != 0)
                    {
                        crc /= 2;
                        crc ^= 0x8C;
                    }
                    else
                    {
                        crc /= 2;
                    }
                    if ((buffer[j] & i) != 0) crc ^= 0x8C;
                }
            }
            return crc;
        }

        /// <summary>
        /// CRC8CCITT式校验
        /// </summary>
        /// <param name="crcPoly">校验多项式 byte</param>
        /// <param name="data">数据</param>
        /// <returns>返回原数组+CRC校验字节</returns>
        public static int CRC8CCITT(byte[] data)
        {
            byte CRCPoly = 0x85;//CRC多项式，当做除数
            byte[] Data = data;
            byte CRCTempResult = 0x00;//CRC结果运算的得数，但不是最后的值
            byte CRCResult = 0x00;//CRC结果运算最后的值

            CRCTempResult = (byte)(Data[0] ^ CRCPoly);

            for (int arrayLength = 1; arrayLength <= (data.Length - 1); arrayLength++)
            {
                for (int i = 0; i < 8; i++)
                {
                    if ((CRCTempResult & 0x80) == 0x00)
                    {
                        CRCTempResult = (byte)(CRCTempResult << 1);
                        CRCTempResult = (byte)(
                                                            CRCTempResult |
                                                            ((Data[arrayLength] & 0x80) == 0x80 ? 0x01 : 0x00)
                                                          );
                        Data[arrayLength] <<= 1;
                    }
                    else
                    {
                        CRCTempResult = (byte)(CRCTempResult ^ CRCPoly);
                        i--;
                    }
                }
            }
            CRCResult = CRCTempResult;
            return CRCResult;
        }
        #endregion

        /// <summary>
        /// 十六进制转换到十进制
        /// </summary>
        /// <param name="hex"></param>
        /// <returns></returns>
        public static int Hex16ToHex10(string hex)
        {
            int ten = 0;
            for (int i = 0, j = hex.Length - 1; i < hex.Length; i++)
            {
                ten += HexChar2Value(hex.Substring(i, 1)) * ((int)Math.Pow(16, j));
                j--;
            }
            return ten;
        }

        /// <summary>
        /// 从十进制转换到十六进制
        /// </summary>
        /// <param name="ten"></param>
        /// <returns></returns>
        public static string Hex10ToHex16(string ten)
        {
            ulong tenValue = Convert.ToUInt64(ten);
            ulong divValue, resValue;
            string hex = "";
            do
            {
                //divValue = (ulong)Math.Floor(tenValue / 16);

                divValue = (ulong)Math.Floor((decimal)(tenValue / 16));

                resValue = tenValue % 16;
                hex = tenValue2Char(resValue) + hex;
                tenValue = divValue;
            }
            while (tenValue >= 16);
            if (tenValue != 0)
                hex = tenValue2Char(tenValue) + hex;
            return hex;
        }

        public static int HexChar2Value(string hexChar)
        {
            switch (hexChar)
            {
                case "0":
                case "1":
                case "2":
                case "3":
                case "4":
                case "5":
                case "6":
                case "7":
                case "8":
                case "9":
                    return Convert.ToInt32(hexChar);
                case "a":
                case "A":
                    return 10;
                case "b":
                case "B":
                    return 11;
                case "c":
                case "C":
                    return 12;
                case "d":
                case "D":
                    return 13;
                case "e":
                case "E":
                    return 14;
                case "f":
                case "F":
                    return 15;
                default:
                    return 0;
            }
        }

        public static string tenValue2Char(ulong ten)
        {
            switch (ten)
            {
                case 0:
                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 6:
                case 7:
                case 8:
                case 9:
                    return ten.ToString();
                case 10:
                    return "A";
                case 11:
                    return "B";
                case 12:
                    return "C";
                case 13:
                    return "D";
                case 14:
                    return "E";
                case 15:
                    return "F";
                default:
                    return "";
            }
        }

        /// <summary>
        /// 字符串转16进制字节数组
        /// </summary>
        /// <param name="hexString"></param>
        /// <returns></returns>
        public static byte[] StrToHex16Byte(string hexString)
        {
            hexString = hexString.Replace(" ", "");
            if ((hexString.Length % 2) != 0)
                hexString += " ";
            byte[] returnBytes = new byte[hexString.Length / 2];
            for (int i = 0; i < returnBytes.Length; i++)
                returnBytes[i] = Convert.ToByte(hexString.Substring(i * 2, 2), 16);
            return returnBytes;
        }

        /// <summary>
        /// 字节数组转16进制字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <returns></returns>
        public static string ByteToHex16Str(byte[] bytes)
        {
            string returnStr = "";
            if (bytes != null)
            {
                for (int i = 0; i < bytes.Length; i++)
                {
                    returnStr += bytes[i].ToString("X2") + " ";
                }
            }
            return returnStr.TrimEnd(' ');
        }

        /// <summary>
        /// 从汉字转换到16进制
        /// </summary>
        /// <param name="s"></param>
        /// <param name="charset">编码,如"utf-8","gb2312"</param>
        /// <param name="fenge">是否每字符用逗号分隔</param>
        /// <returns></returns>
        public static string ToHex(string s, string charset, bool fenge)
        {
            if ((s.Length % 2) != 0)
            {
                s += " ";//空格
                //throw new ArgumentException("s is not valid chinese string!");
            }
            System.Text.Encoding chs = System.Text.Encoding.GetEncoding(charset);
            byte[] bytes = chs.GetBytes(s);
            string str = "";
            for (int i = 0; i < bytes.Length; i++)
            {
                str += string.Format("{0:X}", bytes[i]);
                if (fenge && (i != bytes.Length - 1))
                {
                    str += string.Format("{0}", ",");
                }
            }
            return str.ToLower();
        }

        ///<summary>
        /// 从16进制转换成汉字
        /// </summary>
        /// <param name="hex"></param>
        /// <param name="charset">编码,如"utf-8","gb2312"</param>
        /// <returns></returns>
        public static string UnHex(string hex, string charset)
        {
            if (hex == null)
                throw new ArgumentNullException("hex");
            hex = hex.Replace(",", "");
            hex = hex.Replace("\n", "");
            hex = hex.Replace("\\", "");
            hex = hex.Replace(" ", "");
            if (hex.Length % 2 != 0)
            {
                hex += "20";//空格
            }
            // 需要将 hex 转换成 byte 数组。 
            byte[] bytes = new byte[hex.Length / 2];

            for (int i = 0; i < bytes.Length; i++)
            {
                try
                {
                    // 每两个字符是一个 byte。 
                    bytes[i] = byte.Parse(hex.Substring(i * 2, 2),
                    System.Globalization.NumberStyles.HexNumber);
                }
                catch
                {
                    // Rethrow an exception with custom message. 
                    throw new ArgumentException("hex is not a valid hex number!", "hex");
                }
            }
            System.Text.Encoding chs = System.Text.Encoding.GetEncoding(charset);
            return chs.GetString(bytes);
        }

        /// <summary> 
        /// 十六进制转为负数
        ///</summary>
        ///<param name="strNumber"></param>
        ///<returns></returns>
        public static int HexStringToNegative(string strNumber)
        {
            int iNegate = 0;
            int iNumber = Convert.ToInt32(strNumber, 16);

            if (iNumber > 127)
            {
                int iComplement = iNumber - 1;
                string strNegate = string.Empty;

                char[] binChar = Convert.ToString(iComplement, 2).PadLeft(8, '0').ToArray();

                foreach (char ch in binChar)
                {
                    if (Convert.ToInt32(ch) == 48)
                    {
                        strNegate += "1";
                    }
                    else
                    {
                        strNegate += "0";
                    }
                }

                iNegate = -Convert.ToInt32(strNegate, 2);
            }

            return iNegate;
        }
    }
}
