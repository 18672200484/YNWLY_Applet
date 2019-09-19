// 此代码由 NhGenerator v1.0.9.0 工具生成。

using System;
using System.Collections;
using CMCS.Common.Entities.Sys;

namespace CMCS.Common.Entities.CoalPot
{
    /// <summary>
    /// 汽车智能化-成品仓管理
    /// </summary>
    [Serializable]
    [CMCS.DapperDber.Attrs.DapperBind("FulTbCoalPot")]
    public class FulCoalPot : EntityBase1
    {
        private String _PotName;
        /// <summary>
        /// 成品仓名称
        /// </summary>
        public virtual String PotName { get { return _PotName; } set { _PotName = value; } }

        private String _OrderNumber;
        /// <summary>
        /// 顺序
        /// </summary>
        public virtual String OrderNumber { get { return _OrderNumber; } set { _OrderNumber = value; } }

        private String _ParentId;
        /// <summary>
        /// 父ID
        /// </summary>
        public virtual String ParentId { get { return _ParentId; } set { _ParentId = value; } }

        private String _IdPath;
        /// <summary>
        /// 父ID
        /// </summary>
        public virtual String IdPath { get { return _IdPath; } set { _IdPath = value; } }

        private String _SalesOrderId;
        /// <summary>
        /// 订单ID
        /// </summary>
        public virtual String SalesOrderId { get { return _SalesOrderId; } set { _SalesOrderId = value; } }

        private String _ProgramId;
        /// <summary>
        /// 生产计划id 
        /// </summary>
        public virtual String ProgramId { get { return _ProgramId; } set { _ProgramId = value; } }

    }
}
