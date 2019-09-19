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
    ///气送记录表
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("Trans_Record_Tb")]
    public class Trans_Record_Tb
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
        /// 总体设备编号 制样机编号：01（1#制样机）； 02（2#制样机
        /// </summary>
        public String MachineCode
        {
            get { return _MachineCode; }
            set { _MachineCode = value; }
        }

        private String _PackeageCode;
        /// <summary>
        /// 封装码
        /// </summary>
        public String PackeageCode
        {
            get { return _PackeageCode; }
            set { _PackeageCode = value; }
        }

        private String _SampleType;
        /// <summary>
        /// 煤样类型   =1：13mm全水分样  =2：6mm全水分样  =3：3mm存查样  =4：0.2mm一般试验分析样  =5：0.2mm存查样
        /// </summary>
        public String SampleType
        {
            get { return _SampleType; }
            set { _SampleType = value; }
        }

        private Int32 _TransType;
        /// <summary>
        /// 传送方式  =1：在线自动传送  =2：离线自动传送  =3：人工传送
        /// </summary>
        public Int32 TransType
        {
            get { return _TransType; }
            set { _TransType = value; }
        }

        private DateTime _StartTime;
        /// <summary>
        /// 传送开始时间
        /// </summary>
        public DateTime StartTime
        {
            get { return _StartTime; }
            set { _StartTime = value; }
        }

        private DateTime _EndTime;
        /// <summary>
        /// 传送结束时间
        /// </summary>
        public DateTime EndTime
        {
            get { return _EndTime; }
            set { _EndTime = value; }
        }

        private String _UserName;
        /// <summary>
        /// 工作人员
        /// </summary>
        public String UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        private Int32 _Issuccessed;
        /// <summary>
        /// 是否成功
        /// </summary>
        public Int32 Issuccessed
        {
            get { return _Issuccessed; }
            set { _Issuccessed = value; }
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
