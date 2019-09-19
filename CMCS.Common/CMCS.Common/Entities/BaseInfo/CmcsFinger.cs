using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.Entities.Sys;
using CMCS.Common.Entities.iEAA;
using CMCS.Common.DAO;

namespace CMCS.Common.Entities.Fuel
{
    /// <summary>
    /// 用户指纹
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("CmcstbUserFinger")]
    public class CmcsFinger : EntityBase1
    {
        private String _UserId;
        /// <summary>
        /// 用户Id
        /// </summary>
        public virtual String UserId { get { return _UserId; } set { _UserId = value; } }

        private String _FingerName;
        /// <summary>
        /// 指纹名称
        /// </summary>
        public virtual String FingerName { get { return _FingerName; } set { _FingerName = value; } }

        /// <summary>
        /// 用户
        /// </summary>
        //public User TheUser { get { return CommonDAO.GetInstance().SelfDber.Get<User>(this.UserId); } }

        private String _FingerUrl;
        /// <summary>
        /// dat文件路径
        /// </summary>
        public virtual String FingerUrl { get { return _FingerUrl; } set { _FingerUrl = value; } }

        private String _Remark;
        /// <summary>
        /// 备注
        /// </summary>
        public virtual String Remark { get { return _Remark; } set { _Remark = value; } }
    }
}
