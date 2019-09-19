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
    /// 汽车机械采样机接口 - 采样记录
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("KY_CYJ_Record")]
    public class KY_CYJ_Record
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

        private String _Card_Number;
        /// <summary>
        /// 卡号
        /// </summary>
        public String Card_Number
        {
            get { return _Card_Number; }
            set { _Card_Number = value; }
        }

        private String _Car_Number;
        /// <summary>
        /// 车牌号
        /// </summary>
        public String Car_Number
        {
            get { return _Car_Number; }
            set { _Car_Number = value; }
        }

        private String _Rcd_Code;
        /// <summary>
        /// 入厂流水号
        /// </summary>
        public String Rcd_Code
        {
            get { return _Rcd_Code; }
            set { _Rcd_Code = value; }
        }

        private String _CY_Code;
        /// <summary>
        /// 采样编码
        /// </summary>
        public String CY_Code
        {
            get { return _CY_Code; }
            set { _CY_Code = value; }
        }

        private String _Batch_Number;
        /// <summary>
        /// 桶编码
        /// </summary>
        public String Batch_Number
        {
            get { return _Batch_Number; }
            set { _Batch_Number = value; }
        }

        private String _CY_Type;
        /// <summary>
        /// 采样类型
        /// </summary>
        public String CY_Type
        {
            get { return _CY_Type; }
            set { _CY_Type = value; }
        }

        private String _ZY_Type;
        /// <summary>
        /// 制样方式
        /// </summary>
        public String ZY_Type
        {
            get { return _ZY_Type; }
            set { _ZY_Type = value; }
        }

        private String _CY_CoorDinate;
        /// <summary>
        /// 采样坐标
        /// </summary>
        public String CY_CoorDinate
        {
            get { return _CY_CoorDinate; }
            set { _CY_CoorDinate = value; }
        }

        private String _CY_User;
        /// <summary>
        /// 采样操作人
        /// </summary>
        public String CY_User
        {
            get { return _CY_User; }
            set { _CY_User = value; }
        }

        private DateTime _Begin_Date;
        /// <summary>
        /// 采样开始时间
        /// </summary>
        public DateTime Begin_Date
        {
            get { return _Begin_Date; }
            set { _Begin_Date = value; }
        }

        private DateTime _End_Date;
        /// <summary>
        /// 采样结束时间
        /// </summary>
        public DateTime End_Date
        {
            get { return _End_Date; }
            set { _End_Date = value; }
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

    }
}
