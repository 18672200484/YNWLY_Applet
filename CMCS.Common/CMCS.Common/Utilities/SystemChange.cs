using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMCS.Common.Utilities
{
    /// <summary>
    /// 进制转换
    /// </summary>
    public static class SystemChange
    {
        /// <summary>
        /// 字符串转换为16进制
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string StringTo16Byte(string data)
        {
            string result = string.Empty;
            char[] values = data.ToCharArray();
            foreach (char letter in values)
            {
                // Get the integral value of the character.
                int value = Convert.ToInt32(letter);
                // Convert the decimal value to a hexadecimal value in string form.
                string hexOutput = String.Format("{0:X}", value);
                result += hexOutput + " ";
                //Console.WriteLine("Hexadecimal value of {0} is {1}", letter, hexOutput);
            }
            return result;
        }

        /// <summary>
        /// 16进制转换为字符串
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string Byte16ToString(string data)
        {
            string result = string.Empty;
            data.Replace(" ", "");
            data = InsertFormat(data, 2, " ");
            string[] hexValuesSplit = data.Split(' ');
            foreach (String hex in hexValuesSplit)
            {
                int value = Convert.ToInt32(hex, 16);
                string stringValue = Char.ConvertFromUtf32(value);
                char charValue = (char)value;
                result += charValue.ToString();
            }
            return result;
        }

        /// <summary>  
        /// 每隔n个字符插入一个字符  
        /// </summary>  
        /// <param name="input">源字符串</param>  
        /// <param name="interval">间隔字符数</param>  
        /// <param name="value">待插入值</param>  
        /// <returns>返回新生成字符串</returns>  
        public static string InsertFormat(string input, int interval, string value)
        {
            for (int i = interval; i < input.Length; i += interval + 1)
                input = input.Insert(i, value);
            return input;
        }
    }
}
