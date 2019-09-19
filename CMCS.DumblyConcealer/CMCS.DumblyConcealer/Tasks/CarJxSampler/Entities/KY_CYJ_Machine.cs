using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.Entities;
using CMCS.Common.Entities.Sys;
using System.ComponentModel;

namespace CMCS.DumblyConcealer.Tasks.CarJXSampler.Entities
{
    /// <summary>
    /// 汽车机械采样机接口 - 采样机基本状态表
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("KY_CYJ_Machine")]
    public class KY_CYJ_Machine
    {
        private Int32 _Id;
        /// <summary>
        /// Id
        /// </summary>
        [CMCS.DapperDber.Attrs.DapperPrimaryKeyAdd]
        public Int32 Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        private String _CYJ_Machine;
        /// <summary>
        /// 采样机号
        /// </summary>
        public String CYJ_Machine
        {
            get { return _CYJ_Machine; }
            set { _CYJ_Machine = value; }
        }

        private String _Machine_Code;
        /// <summary>
        /// 部件编号
        /// </summary>
        public String Machine_Code
        {
            get { return _Machine_Code; }
            set { _Machine_Code = value; }
        }

        private String _Machine_Name;
        /// <summary>
        /// 信息类型
        /// </summary>
        public String Machine_Name
        {
            get { return _Machine_Name; }
            set { _Machine_Name = value; }
        }

    }
}
