using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.Entities;
using CMCS.Common.Entities.Sys;
using System.ComponentModel;

namespace CMCS.DumblyConcealer.Tasks.PneumaticTransfer.Entities
{
    /// <summary>
    ///气送设备状态表
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("Trans_Status_Tb")]
    public class Trans_Status_Tb
    {
        private Int32 _Id;
        /// <summary>
        /// Id
        /// </summary>
        [CMCS.DapperDber.Attrs.DapperPrimaryKey]
        public Int32 Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        private String _MachineCode;
        /// <summary>
        /// 总体设备编号 
        /// </summary>
        public String MachineCode
        {
            get { return _MachineCode; }
            set { _MachineCode = value; }
        }

        private String _TransStation;
        /// <summary>
        /// 当前传送发送站
        /// </summary>
        public String TransStation
        {
            get { return _TransStation; }
            set { _TransStation = value; }
        }

        private String _SamReady;
        /// <summary>
        /// 气送系统状态
        /*
         4=急停状态(需管控下发急停复位或就地复位气送系统
         3=就绪状态(可以传送一批样)
         2=故障(不可以传送下一批样)
         1=正在运行中（不可以传送下一批样）
         0=停止
        */
        /// </summary>
        public String SamReady
        {
            get { return _SamReady; }
            set { _SamReady = value; }
        }

        private Int32 _DataStatus;
        /// <summary>
        /// 数据发送状态
        /// </summary>
        public Int32 DataStatus
        {
            get { return _DataStatus; }
            set { _DataStatus = value; }
        }

    }
}
