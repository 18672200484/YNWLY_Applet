using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.Entities.Sys;

namespace CMCS.Common.Entities.Fuel
{
    /// <summary>
    /// 客户
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("FULTBCustomerInfo")]
    public class Customer : EntityBase1
    {
        private String _CustomerCode;
        /// <summary>
        /// 客户编号
        /// </summary>
        public virtual String CustomerCode { get { return _CustomerCode; }  set { _CustomerCode = value; }}

        private String _CustomerName;
        /// <summary>
        /// 客户名称
        /// </summary>
        public virtual String CustomerName { get { return _CustomerName; }  set { _CustomerName = value; }}

        private String _Linker;
        /// <summary>
        /// 联系人
        /// </summary>
        public virtual String Linker { get { return _Linker; }  set { _Linker = value; }}

        private String _LinkPhone;
        /// <summary>
        /// 联系电话
        /// </summary>
        public virtual String LinkPhone { get { return _LinkPhone; }  set { _LinkPhone = value; }}

        private String _Email;
        /// <summary>
        /// Email
        /// </summary>
        public virtual String Email { get { return _Email; }  set { _Email = value; }}

        private String _Address;
        /// <summary>
        /// 地址
        /// </summary>
        public virtual String Address { get { return _Address; }  set { _Address = value; }}

        private String _Remark;
        /// <summary>
        /// 备注
        /// </summary>
        public virtual String Remark { get { return _Remark; }  set { _Remark = value; }}

        private String _Valid;
        /// <summary>
        /// 是否有效
        /// </summary>
        public virtual String Valid { get { return _Valid; } set { _Valid = value; } }

    }
}
