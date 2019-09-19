using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.Entities.Sys;

namespace CMCS.Common.Entities.Fuel
{
    [CMCS.DapperDber.Attrs.DapperBind("CMCSTBSAMPLEBARREL")]
    [Serializable]
    public class CmcsSampleBarrel : EntityBase1
    {
        private String _BarrelName;
        /// <summary>
        /// 名称
        /// </summary>
        public virtual String BarrelName { get { return _BarrelName; } set { _BarrelName = value; } }

        private String _BarrelCode;
        /// <summary>
        /// 编码
        /// </summary>
        public virtual String BarrelCode { get { return _BarrelCode; } set { _BarrelCode = value; } }

        private Int32 _IsUse;
        /// <summary>
        /// 是否使用
        /// </summary>
        public virtual Int32 IsUse { get { return _IsUse; } set { _IsUse = value; } }

        private String _Remark;
        /// <summary>
        /// 备注
        /// </summary>
        public virtual String Remark { get { return _Remark; } set { _Remark = value; } }

        private string isSynch = "0";
        /// <summary>
        /// 同步标识
        /// </summary>
        public string IsSynch
        {
            get { return isSynch; }
            set { isSynch = value; }
        }
    }
}
