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
    /// 汽车机械采样机接口 - 采样机卸料桶状态
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("KY_CYJ_BarrelStatus")]
    public class KY_CYJ_BarrelStatus
    {
        private Int32 _Id;
        /// <summary>
        /// Id自动增长
        /// </summary>
        [CMCS.DapperDber.Attrs.DapperPrimaryKeyAdd]
        [CMCS.DapperDber.Attrs.DapperPrimaryKey]
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

        private Int32 _Down_Cont;
        /// <summary>
        /// 卸料次数
        /// </summary>
        public Int32 Down_Cont
        {
            get { return _Down_Cont; }
            set { _Down_Cont = value; }
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

        private String _CY_Code;
        /// <summary>
        ///采样编码
        /// </summary>
        public String CY_Code
        {
            get { return _CY_Code; }
            set { _CY_Code = value; }
        }

        private DateTime _EditDate;
        /// <summary>
        ///最后更新时间
        /// </summary>
        public DateTime EditDate
        {
            get { return _EditDate; }
            set { _EditDate = value; }
        }

        private String _JL_Code;
        /// <summary>
        ///当前进料桶号
        /// </summary>
        public String JL_Code
        {
            get { return _JL_Code; }
            set { _JL_Code = value; }
        }

        private String _Down_Code;
        /// <summary>
        ///当前出料桶号
        /// </summary>
        public String Down_Code
        {
            get { return _Down_Code; }
            set { _Down_Code = value; }
        }

        private Int32 _DataFlag;
        /// <summary>
        ///数据标识 0：开元写入 1:博晟读取
        /// </summary>
        public Int32 DataFlag
        {
            get { return _DataFlag; }
            set { _DataFlag = value; }
        }

    }
}
