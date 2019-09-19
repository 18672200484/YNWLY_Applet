using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.Entities;
using CMCS.Common.Entities.Sys;

namespace CMCS.DumblyConcealer.Tasks.AutoMaker.Entities
{
    /// <summary>
    /// 制样机 - 卸料控制命令
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("XL_Cmd_Tb")]
    public class XL_Cmd_Tb
    {
        private string id;
        /// <summary>
        /// Id
        /// </summary>
        [CMCS.DapperDber.Attrs.DapperPrimaryKey, CMCS.DapperDber.Attrs.DapperPrimaryKeyAdd]
        public string Id
        {
            get { return id; }
            set { id = value; }
        }

        private string machineCode;
        /// <summary>
        /// 设备编号
        /// </summary>
        public string MachineCode
        {
            get { return machineCode; }
            set { machineCode = value; }
        }

        private string cmdCode;
        /// <summary>
        /// 命令代码
        /// </summary>
        public string CmdCode
        {
            get { return cmdCode; }
            set { cmdCode = value; }
        }

        private string resultCode;
        /// <summary>
        /// 执行结果
        /// </summary>
        public string ResultCode
        {
            get { return resultCode; }
            set { resultCode = value; }
        }

        private int dataFlag;
        /// <summary>
        /// 标识符
        /// </summary>
        public int DataFlag
        {
            get { return dataFlag; }
            set { dataFlag = value; }
        }
    }
}
