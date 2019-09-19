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
    /// 汽车机械采样机接口 - 采样机报警记录表
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("Alarm")]
    public class Alarm
    {
        private Int32 _Id;
        /// <summary>
        /// Id 
        /// </summary>
        [CMCS.DapperDber.Attrs.DapperPrimaryKey]
        [CMCS.DapperDber.Attrs.DapperPrimaryKeyAdd]
        public Int32 Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        private String _AlarmDate;
        /// <summary>
        /// 报警日期 
        /// </summary>
        public String AlarmDate
        {
            get { return _AlarmDate; }
            set { _AlarmDate = value; }
        }

        private String _AlarmTime;
        /// <summary>
        /// 报警时间
        /// </summary>
        public String AlarmTime
        {
            get { return _AlarmTime; }
            set { _AlarmTime = value; }
        }

        private String _AlarmType;
        /// <summary>
        /// 报警状态 开  关 ？？ 
        /// </summary>
        public String AlarmType
        {
            get { return _AlarmType; }
            set { _AlarmType = value; }
        }

        private String _AcrDate;
        /// <summary>
        /// 确认或恢复时期
        /// </summary>
        public String AcrDate
        {
            get { return _AcrDate; }
            set { _AcrDate = value; }
        }

        private String _AcrTime;
        /// <summary>
        /// 确认或恢复时间
        /// </summary>
        public String AcrTime
        {
            get { return _AcrTime; }
            set { _AcrTime = value; }
        }

        private String _EventType;
        /// <summary>
        /// 报警类型  报警   报警恢复
        /// </summary>
        public String EventType
        {
            get { return _EventType; }
            set { _EventType = value; }
        }

        private String _VarName;
        /// <summary>
        /// 报警名称
        /// </summary>
        public String VarName
        {
            get { return _VarName; }
            set { _VarName = value; }
        }

        private String _AlarmValue;
        /// <summary>
        /// 报警值
        /// </summary>
        public String AlarmValue
        {
            get { return _AlarmValue; }
            set { _AlarmValue = value; }
        }

        private String _LimitValue;
        /// <summary>
        /// 限值
        /// </summary>
        public String LimitValue
        {
            get { return _LimitValue; }
            set { _LimitValue = value; }
        }

        private String _ResumeValue;
        /// <summary>
        /// 恢复值
        /// </summary>
        public String ResumeValue
        {
            get { return _ResumeValue; }
            set { _ResumeValue = value; }
        }

        private String _OperatorName;
        /// <summary>
        /// 操作员名
        /// </summary>
        public String OperatorName
        {
            get { return _OperatorName; }
            set { _OperatorName = value; }
        }

        private String _VarComment;
        /// <summary>
        /// 变量描述
        /// </summary>
        public String VarComment
        {
            get { return _VarComment; }
            set { _VarComment = value; }
        }

        private String _MachineName;
        /// <summary>
        /// 采样机编码
        /// </summary>
        public String MachineName
        {
            get { return _MachineName; }
            set { _MachineName = value; }
        }
    }
}
