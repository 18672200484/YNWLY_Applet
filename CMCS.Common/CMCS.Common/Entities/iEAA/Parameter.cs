using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.Entities.Sys;

namespace CMCS.Common.Entities.iEAA
{
    /// <summary>
    /// 参数管理
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("Syssmtbparameter")]
    public class Parameter : EntityBase1
    {
        private string _Name;
        /// <summary>
        /// 指标名称
        /// </summary>
        public string Name { get { return _Name; } set { _Name = value; } }

        private string _Value;
        /// <summary>
        /// 指标值
        /// </summary>
        public string Value { get { return _Value; } set { _Value = value; } }

        private string _IsModify;
        /// <summary>
        /// 
        /// </summary>
        public string IsModify { get { return _IsModify; } set { _IsModify = value; } }

        private string _IsDelete;
        /// <summary>
        /// 是否删除
        /// </summary>
        public string IsDelete { get { return _IsDelete; } set { _IsDelete = value; } }

        private string _Remark;
        /// <summary>
        /// 说明
        /// </summary>
        public string Remark { get { return _Remark; } set { _Remark = value; } }

        private string _SubSystem;
        /// <summary>
        /// 
        /// </summary>
        public string SubSystem { get { return _SubSystem; } set { _SubSystem = value; } }

        private string _ModuleName;
        /// <summary>
        /// 模块名称
        /// </summary>
        public string ModuleName { get { return _ModuleName; } set { _ModuleName = value; } }
    }
}
