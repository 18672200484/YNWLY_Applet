using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.Entities.Sys;

namespace CMCS.Common.Entities.PackingBatch
{
    /// <summary>
    /// 封装归批机接口-实时样桶表
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("inftbqcjxcyPackingBatchCoord")]
    public class InfPackingBatchCoord : EntityBase1
    {
        private Int32 coord;
        /// <summary>
        /// 位置
        /// </summary>
        public Int32 Coord
        {
            get { return coord; }
            set { coord = value; }
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

        private String _SampleCode;
        /// <summary>
        /// 样桶码
        /// </summary>
        public String SampleCode
        {
            get { return _SampleCode; }
            set { _SampleCode = value; }
        }

        private Int32 _State;
        /// <summary>
        /// 样桶状态  1为有桶0无桶
        /// </summary>
        public Int32 State
        {
            get { return _State; }
            set { _State = value; }
        }

        private DateTime _UpdateTime;
        /// <summary>
        /// 更新时间
        /// </summary>
        public DateTime UpdateTime
        {
            get { return _UpdateTime; }
            set { _UpdateTime = value; }
        }

        private int syncFlag = 0;
        /// <summary>
        /// 同步标识 0=未同步 1=已同步
        /// </summary>
        public int SyncFlag
        {
            get { return syncFlag; }
            set { syncFlag = value; }
        }
    }
}
