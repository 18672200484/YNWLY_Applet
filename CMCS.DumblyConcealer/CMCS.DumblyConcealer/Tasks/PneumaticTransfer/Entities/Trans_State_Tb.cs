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
    [CMCS.DapperDber.Attrs.DapperBind("Trans_State_Tb")]
    public class Trans_State_Tb
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

        private String _DeviceCode;
        /// <summary>
        /// 详细设备编号
        /// </summary>
        public String DeviceCode
        {
            get { return _DeviceCode; }
            set { _DeviceCode = value; }
        }

        private String _SampleID;
        /// <summary>
        /// 制样编码
        /// </summary>
        public String SampleID
        {
            get { return _SampleID; }
            set { _SampleID = value; }
        }

        private String _DeviceName;
        /// <summary>
        /// 详细设备名称
        /// </summary>
        public String DeviceName
        {
            get { return _DeviceName; }
            set { _DeviceName = value; }
        }

        private Int32 _DeviceStatus;
        /// <summary>
        /// 工作状态
        /// </summary>
        public Int32 DeviceStatus
        {
            get { return _DeviceStatus; }
            set { _DeviceStatus = value; }
        }

        private DateTime _LastUpdateTime;
        /// <summary>
        /// 最后更新时间
        /// </summary>
        public DateTime LastUpdateTime
        {
            get { return _LastUpdateTime; }
            set { _LastUpdateTime = value; }
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
