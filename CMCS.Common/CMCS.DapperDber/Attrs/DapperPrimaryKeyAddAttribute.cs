using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMCS.DapperDber.Attrs
{
    /// <summary>
    /// 自增长Id字段特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class DapperPrimaryKeyAddAttribute : Attribute
    {
    }
}
