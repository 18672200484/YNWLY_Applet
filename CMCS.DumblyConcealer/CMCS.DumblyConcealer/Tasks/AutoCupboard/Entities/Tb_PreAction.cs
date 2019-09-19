using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.Entities;
using CMCS.Common.Entities.Sys;

namespace CMCS.DumblyConcealer.Tasks.AutoCupboard.Entities
{
    /// <summary>
    /// 取、弃样接口表
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("tb_preaction")]
    public class Tb_PreAction
    {
        /// <summary>
        /// 自增序号
        /// </summary>
        [CMCS.DapperDber.Attrs.DapperPrimaryKey, CMCS.DapperDber.Attrs.DapperPrimaryKeyAdd]
        public Int32 Id { get; set; }

        /// <summary>
        /// 柜号
        /// </summary>
        public String SSName { get; set; }

        /// <summary>
        /// 瓶底RFID全码
        /// </summary>
        public String Sample_Id { get; set; }

        /// <summary>
        /// 操作类型 2取，3弃
        /// </summary>
        public Int32 Operate_Code { get; set; }

        /// <summary>
        /// 人员编号
        /// </summary>
        public Int32 Person_Id { get; set; }

        /// <summary>
        /// 瓶id
        /// </summary>
        public Int32 Bolt_Id { get; set; }

        /// <summary>
        /// 类型 1  2  3自动存样  4自动取样  7
        /// </summary>
        public Int32 Priority { get; set; }

        /// <summary>
        /// 0未执行  1正在执行  2执行异常   4 执行完成
        /// </summary>
        public Int32 DoneState { get; set; }

    }
}
