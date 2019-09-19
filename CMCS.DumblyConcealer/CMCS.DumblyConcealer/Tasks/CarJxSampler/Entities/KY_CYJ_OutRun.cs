using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.Entities;
using CMCS.Common.Entities.Sys;

namespace CMCS.DumblyConcealer.Tasks.CarJXSampler.Entities
{
    /// <summary>
    /// 汽车机械采样机接口 - 采样机操作表
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("KY_CYJ_OUTRUN")]
    public class KY_CYJ_OutRun
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
        /// 采样机号
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
        /// 入厂流水号  入厂流水号，能唯一关联是哪一辆车的记录
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

        private String _CY_Type;
        /// <summary>
        /// 采样类型
        /// </summary>
        public String CY_Type
        {
            get { return _CY_Type; }
            set { _CY_Type = value; }
        }

        private Int32 _CY_Point;
        /// <summary>
        /// 采样点数
        /// </summary>
        public Int32 CY_Point
        {
            get { return _CY_Point; }
            set { _CY_Point = value; }
        }

        private String _CY_Area;
        /// <summary>
        /// 采样区域  采样区域列表，格式为1;5;18
        /// </summary>
        public String CY_Area
        {
            get { return _CY_Area; }
            set { _CY_Area = value; }
        }

        private String _CY_CoorDinate;
        /// <summary>
        /// 采样坐标  格式为：X1,Y1,Z1,0,0;X2,Y2,Z2,0,0;
        ///X代表横坐标
        ///Y代表纵坐标
        ///Z代表采样深度
        /// </summary>
        public String CY_CoorDinate
        {
            get { return _CY_CoorDinate; }
            set { _CY_CoorDinate = value; }
        }

        private String _Batch_Number;
        /// <summary>
        /// 批次号
        /// </summary>
        public String Batch_Number
        {
            get { return _Batch_Number; }
            set { _Batch_Number = value; }
        }

        private Int32 _CY_Control;
        /// <summary>
        /// 指令 0空指令 1采样指令 2卸料指令
        /// </summary>
        public Int32 CY_Control
        {
            get { return _CY_Control; }
            set { _CY_Control = value; }
        }

        private Int32 _CY_State;
        /// <summary>
        /// 采样状态
        /// 0代表等待采样
        /// 1代表正在采样
        /// 2代表采样完成
        /// 3代表采样异常
        /// </summary>
        public Int32 CY_State
        {
            get { return _CY_State; }
            set { _CY_State = value; }
        }

        private DateTime _Send_Time;
        /// <summary>
        /// 发送时间
        /// </summary>
        public DateTime Send_Time
        {
            get { return _Send_Time; }
            set { _Send_Time = value; }
        }

        private Int32 _Clean_Flag;
        /// <summary>
        /// 清洗标记 当仅当为1时，采样机进行清洗样操作，否则不清洗
        /// </summary>
        public Int32 Clean_Flag
        {
            get { return _Clean_Flag; }
            set { _Clean_Flag = value; }
        }
    }
}
