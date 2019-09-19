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
    /// 汽车机械采样机接口 - 采样机卸料桶状态(每次卸样会生成一条记录)
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("KY_CYJ_Barrel")]
    public class KY_CYJ_Barrel
    {
        private Int32 _Id;
        /// <summary>
        /// Id自动增长
        /// </summary>
        [CMCS.DapperDber.Attrs.DapperPrimaryKeyAdd]
        public Int32 Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        private String _CYJ_Machine;
        /// <summary>
        /// 采样机编号 
        /// </summary>
        public String CYJ_Machine
        {
            get { return _CYJ_Machine; }
            set { _CYJ_Machine = value; }
        }

        private String _Barrel_Code;
        /// <summary>
        /// 桶号
        /// </summary>
        public String Barrel_Code
        {
            get { return _Barrel_Code; }
            set { _Barrel_Code = value; }
        }

        private Int32 _Down_Count;
        /// <summary>
        /// 卸料次数
        /// </summary>
        public Int32 Down_Count
        {
            get { return _Down_Count; }
            set { _Down_Count = value; }
        }

        private Int32 _Down_Full;
        /// <summary>
        /// 是否满了
        /// </summary>
        public Int32 Down_Full
        {
            get { return _Down_Full; }
            set { _Down_Full = value; }
        }

        private String _Barrel_Number;
        /// <summary>
        /// 桶编码 该编码由采样机系统生成，当电厂有合样要求时，每清空一次桶，重新生成一次，永远唯一，当电厂无合样要求时，可以与 CY_CODE保持一致
        /// </summary>
        public String Barrel_Number
        {
            get { return _Barrel_Number; }
            set { _Barrel_Number = value; }
        }

        private String _CY_Code;
        /// <summary>
        ///采样编码
        /// </summary>
        public String CY_Code
        {
            get { return _CY_Code; }
            set { _CY_Code = value; }
        }

        private DateTime _Edit_Time;
        /// <summary>
        ///最后更新时间
        /// </summary>
        public DateTime Edit_Time
        {
            get { return _Edit_Time; }
            set { _Edit_Time = value; }
        }
    }
}
