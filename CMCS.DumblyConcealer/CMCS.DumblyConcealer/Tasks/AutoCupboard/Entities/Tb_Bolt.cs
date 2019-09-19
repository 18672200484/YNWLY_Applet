using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.Entities;
using CMCS.Common.Entities.Sys;

namespace CMCS.DumblyConcealer.Tasks.AutoCupboard.Entities
{
    /// <summary>
    /// 瓶子信息表
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("tb_bolt")]
    public class Tb_Bolt
    {
        /// <summary>
        /// 瓶唯一编码
        /// </summary>
        [CMCS.DapperDber.Attrs.DapperPrimaryKey, CMCS.DapperDber.Attrs.DapperPrimaryKeyAdd]
        public Int32 Bolt_Id { get; set; }

        /// <summary>
        /// 柜号
        /// </summary>
        public Int32 SSName { get; set; }

        /// <summary>
        /// 层
        /// </summary>
        public Int32 RowNo { get; set; }

        /// <summary>
        /// 列
        /// </summary>
        public Int32 ColumnNo { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32 PositionNo { get; set; }

        /// <summary>
        /// 0左1右
        /// </summary>
        public Int32 RotateNo { get; set; }

        /// <summary>
        /// 0无瓶1有瓶
        /// </summary>
        public Int32 Bolt_State { get; set; }

        /// <summary>
        /// 瓶底RFID全码
        /// </summary>
        public String Sample_Id { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32 Big { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32 Small { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public Int32 DataStatus { get; set; }

    }
}
