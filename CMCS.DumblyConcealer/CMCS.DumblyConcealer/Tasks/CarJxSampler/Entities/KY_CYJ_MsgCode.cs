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
    /// 汽车机械采样机接口 - 采样机常见信息代码
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("KY_CYJ_MsgCode")]
    public class KY_CYJ_MsgCode
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
    }
}
