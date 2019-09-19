using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace CMCS.Common.Utilities
{
    public class CompareClass
    {

        /// <summary>
        /// 比较实体数据是否更改
        /// </summary>
        /// <typeparam name="T">实体</typeparam>
        /// <param name="w1">实体1</param>
        /// <param name="w2">实体2</param>
        /// <param name="WithOutValues">不需要比较字段</param>
        /// <returns></returns>
        public static bool CompareClassValue<T>(T w1, T w2, List<String> WithOutValues = null)
        {
            foreach (PropertyInfo item in w1.GetType().GetProperties())
            {
                if (WithOutValues == null || WithOutValues.IndexOf(item.Name) < 0)
                {
                    if (w2.GetType().GetProperty(item.Name).GetValue(w2, null) == null && item.GetValue(w1, null) == null)
                    {
                        continue;
                    }
                    else if (w2.GetType().GetProperty(item.Name).GetValue(w2, null) == null && item.GetValue(w1, null) != null)
                    {
                        return false;
                    }
                    else if (w2.GetType().GetProperty(item.Name).GetValue(w2, null) != null && item.GetValue(w1, null) == null)
                    {
                        return false;
                    }
                    else if (w2.GetType().GetProperty(item.Name).GetValue(w2, null).ToString() != item.GetValue(w1, null).ToString())
                    {
                        return false;
                    }
                }
            }
            return true;
        }
    }
}
