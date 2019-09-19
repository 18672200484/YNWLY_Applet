﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.Entities.Sys;

namespace CMCS.Common.Entities.BaseInfo
{
    /// <summary>
    /// 基础信息-煤种
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("fultbfuelkind")]
    public class CmcsFuelKind : EntityBase1
    {
        private string _FuelCode;
        /// <summary>
        /// 编码
        /// </summary>
        public string FuelCode
        {
            get { return _FuelCode; }
            set { _FuelCode = value; }
        }

        private string _FuelName;
        /// <summary>
        /// 名称
        /// </summary>
        public string FuelName
        {
            get { return _FuelName; }
            set { _FuelName = value; }
        }


        private string _ParentId;
        /// <summary>
        /// 父Id
        /// </summary>
        public string ParentId
        {
            get { return _ParentId; }
            set { _ParentId = value; }
        }


        public int Sequence { get; set; }


        public string ReMark { get; set; }
        public string Valid { get; set; }

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
