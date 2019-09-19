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
    /// 汽车机械采样机接口 - 采样机实时状态表
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("KY_CYJ_Log")]
    public class KY_CYJ_Log
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

        private Int32 _Msg_Type;
        /// <summary>
        /// 信息类型
        /// 0为一般信息
        /// 1为警告
        /// 2为故障
        /// </summary>
        public Int32 Msg_Type
        {
            get { return _Msg_Type; }
            set { _Msg_Type = value; }
        }

        private String _Msg_Code;
        /// <summary>
        /// 信息代码 具体名称见表CYJ_MSGCODE。若没有可为空
        /// </summary>
        public String Msg_Code
        {
            get { return _Msg_Code; }
            set { _Msg_Code = value; }
        }

        private String _Msg_Content;
        /// <summary>
        /// 信息内容
        /// </summary>
        [Description("详细的描述")]
        public String Msg_Content
        {
            get { return _Msg_Content; }
            set { _Msg_Content = value; }
        }

        private DateTime _Creat_Time;
        /// <summary>
        /// 创建时间 
        /// </summary>
        public DateTime Creat_Time
        {
            get { return _Creat_Time; }
            set { _Creat_Time = value; }
        }

        private DateTime _Edit_Time;
        /// <summary>
        /// 最后修改时间 
        /// </summary>
        public DateTime Edit_Time
        {
            get { return _Edit_Time; }
            set { _Edit_Time = value; }
        }

        private Int32 _Msg_State;
        /// <summary>
        /// 信息处理状态 主要是平方这边的处理，例如：故障是否已处理。一般处理完后改为1
        /// </summary>
        public Int32 Msg_State
        {
            get { return _Msg_State; }
            set { _Msg_State = value; }
        }
    }
}
