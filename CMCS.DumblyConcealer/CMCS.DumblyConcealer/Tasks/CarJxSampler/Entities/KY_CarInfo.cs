using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.Entities;
using CMCS.Common.Entities.Sys;

namespace CMCS.DumblyConcealer.Tasks.CarJXSampler.Entities
{
    /// <summary>
    /// 汽车机械采样机接口 - 车辆信息表
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("KY_CarInfo")]
    public class KY_CarInfo
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

        private String _Car_Number;
        /// <summary>
        /// 车牌号 该值为全表唯一值，若有套牌车请从车牌号做区分
        /// </summary>
        [CMCS.DapperDber.Attrs.DapperPrimaryKey]
        public String Car_Number
        {
            get { return _Car_Number; }
            set { _Car_Number = value; }
        }

        private Int32 _Car_Long;
        /// <summary>
        /// 车长 单位毫米
        /// </summary>
        public Int32 Car_Long
        {
            get { return _Car_Long; }
            set { _Car_Long = value; }
        }

        private Int32 _Car_Width;
        /// <summary>
        /// 车宽 单位毫米
        /// </summary>
        public Int32 Car_Width
        {
            get { return _Car_Width; }
            set { _Car_Width = value; }
        }

        private Int32 _Car_Height;
        /// <summary>
        /// 车底板高 单位毫米
        /// </summary>
        public Int32 Car_Height
        {
            get { return _Car_Height; }
            set { _Car_Height = value; }
        }

        private Int32 _LJ_Sum;
        /// <summary>
        /// 拉筋数 最多6个，没有时为0
        /// </summary>
        public Int32 LJ_Sum
        {
            get { return _LJ_Sum; }
            set { _LJ_Sum = value; }
        }

        private Int32 _LJ_0;
        /// <summary>
        /// 拉筋0 该拉筋一般默认为0
        /// </summary>
        public Int32 LJ_0
        {
            get { return _LJ_0; }
            set { _LJ_0 = value; }
        }

        private Int32 _LJ_1;
        /// <summary>
        /// 拉筋1
        /// </summary>
        public Int32 LJ_1
        {
            get { return _LJ_1; }
            set { _LJ_1 = value; }
        }

        private Int32 _LJ_2;
        /// <summary>
        /// 拉筋2
        /// </summary>
        public Int32 LJ_2
        {
            get { return _LJ_2; }
            set { _LJ_2 = value; }
        }

        private Int32 _LJ_3;
        /// <summary>
        /// 拉筋3
        /// </summary>
        public Int32 LJ_3
        {
            get { return _LJ_3; }
            set { _LJ_3 = value; }
        }

        private Int32 _LJ_4;
        /// <summary>
        /// 拉筋4
        /// </summary>
        public Int32 LJ_4
        {
            get { return _LJ_4; }
            set { _LJ_4 = value; }
        }

        private Int32 _LJ_5;
        /// <summary>
        /// 拉筋5
        /// </summary>
        public Int32 LJ_5
        {
            get { return _LJ_5; }
            set { _LJ_5 = value; }
        }

        private Int32 _LJ_6;
        /// <summary>
        /// 拉筋6
        /// </summary>
        public Int32 LJ_6
        {
            get { return _LJ_6; }
            set { _LJ_6 = value; }
        }

        private Int32 _G_Start;
        /// <summary>
        /// 挂车开始位置
        /// </summary>
        public Int32 G_Start
        {
            get { return _G_Start; }
            set { _G_Start = value; }
        }

        private Int32 _G_End;
        /// <summary>
        /// 挂车结束位置
        /// </summary>
        public Int32 G_End
        {
            get { return _G_End; }
            set { _G_End = value; }
        }

        private Int32 _Car_Hei2_Long;
        /// <summary>
        /// 
        /// </summary>
        public Int32 Car_Hei2_Long
        {
            get { return _Car_Hei2_Long; }
            set { _Car_Hei2_Long = value; }
        }

        private Int32 _Car_Hei2;
        /// <summary>
        /// 底板高2
        /// </summary>
        public Int32 Car_Hei2
        {
            get { return _Car_Hei2; }
            set { _Car_Hei2 = value; }
        }

        private String _UserName;
        /// <summary>
        /// 司机
        /// </summary>
        public String UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        private DateTime _EditDate;
        /// <summary>
        /// 最后编辑时间
        /// </summary>
        public DateTime EditDate
        {
            get { return _EditDate; }
            set { _EditDate = value; }
        }

    }
}
