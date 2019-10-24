using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.Entities.Sys;

namespace CMCS.Common.Entities.Balance
{
    /// <summary>
    /// 天平数据录入从表
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("InfTbBalanceRecordDetail")]
    public class InfBalanceRecordDetail : EntityBase1
    {
        private String _MachineCode;
        /// <summary>
        /// 总体设备编号 
        /// </summary>
        public String MachineCode
        {
            get { return _MachineCode; }
            set { _MachineCode = value; }
        }

        private Double _Weight;
        /// <summary>
        /// 重量
        /// </summary>
        public Double Weight
        {
            get { return _Weight; }
            set { _Weight = value; }
        }

        private String _AssayCode;
        /// <summary>
        /// 化验码
        /// </summary>
        public String AssayCode
        {
            get { return _AssayCode; }
            set { _AssayCode = value; }
        }

        private String _BalanceRecordId;
        /// <summary>
        /// 主表Id
        /// </summary>
        public String BalanceRecordId
        {
            get { return _BalanceRecordId; }
            set { _BalanceRecordId = value; }
        }

		/// <summary>
		/// 坩埚架号
		/// </summary>
		public string GGJCode { get; set; }

		/// <summary>
		/// 坩埚号
		/// </summary>
		public string GGCode { get; set; }

        private Int32 _SyncFlag;
        /// <summary>
        /// 同步标识
        /// </summary>
        public Int32 SyncFlag
        {
            get { return _SyncFlag; }
            set { _SyncFlag = value; }
        }

        /// <summary>
        /// 主记录
        /// </summary>
        [CMCS.DapperDber.Attrs.DapperIgnore]
        public InfBalanceRecord TheBalanceRecord
        {
            get { return Dbers.GetInstance().SelfDber.Get<InfBalanceRecord>(this.BalanceRecordId); }
        }
    }
}
