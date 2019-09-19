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
    [CMCS.DapperDber.Attrs.DapperBind("KY_CYJ_Alarm")]
    public class KY_CYJ_Alarm
    {
        private String _CYJ_Machine;
        /// <summary>
        /// 采样机编号 
        /// </summary>
        public String CYJ_Machine
        {
            get { return _CYJ_Machine; }
            set { _CYJ_Machine = value; }
        }

        private DateTime _AlarmDateTime;
        /// <summary>
        /// 报警日期时间
        /// </summary>
        public DateTime AlarmDateTime
        {
            get { return _AlarmDateTime; }
            set { _AlarmDateTime = value; }
        }

        private DateTime _AcrDateTime;
        /// <summary>
        /// 确认或恢复时期时间
        /// </summary>
        public DateTime AcrDateTime
        {
            get { return _AcrDateTime; }
            set { _AcrDateTime = value; }
        }

        private String _VarName;
        /// <summary>
        /// 变量名
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
    }
}
