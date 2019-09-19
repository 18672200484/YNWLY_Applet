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
    /// 汽车机械采样机接口 - 分样机卸料表
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("KY_FYJ_Down")]
    public class KY_CYJ_Down
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

        private String _Barrel_Code;
        /// <summary>
        /// 桶号  
        /// </summary>
        public String Barrel_Code
        {
            get { return _Barrel_Code; }
            set { _Barrel_Code = value; }
        }

        private Int32 _XL_Start;
        /// <summary>
        /// 卸料开始  当为1时，采样机开始准备卸料，进入卸料状态时将状态改为2，卸料故障时为3，卸料完成时燃管将其改为0 
        /// </summary>
        public Int32 XL_Start
        {
            get { return _XL_Start; }
            set { _XL_Start = value; }
        }

        private Int32 _XL_Finish;
        /// <summary>
        /// 卸料完成  采样机完成操作后，将值改为1，燃管读到1时改写为0
        /// </summary>
        public Int32 XL_Finish
        {
            get { return _XL_Finish; }
            set { _XL_Finish = value; }
        }

        private DateTime _LastDateTime;
        /// <summary>
        /// 最后更新时间  
        /// </summary>
        public DateTime LastDateTime
        {
            get { return _LastDateTime; }
            set { _LastDateTime = value; }
        }

        private String _CYJ_Machine;
        /// <summary>
        /// 采样编号 
        /// </summary>
        public String CYJ_Machine
        {
            get { return _CYJ_Machine; }
            set { _CYJ_Machine = value; }
        }

        private String _Down_Type;
        /// <summary>
        /// 卸样方式  0到制样机   1旁路归批机
        /// </summary>
        public String Down_Type
        {
            get { return _Down_Type; }
            set { _Down_Type = value; }
        }

        private Int32 _Read_Flag;
        /// <summary>
        /// 状态 采样机完成操作后，将值改为1，燃管读到1时改写为0
        /// </summary>
        public Int32 Read_Flag
        {
            get { return _Read_Flag; }
            set { _Read_Flag = value; }
        }

        private String _Remark;
        /// <summary>
        /// 状态 读取完之后写 0 
        /// </summary>
        public String Remark
        {
            get { return _Remark; }
            set { _Remark = value; }
        }
    }
}
