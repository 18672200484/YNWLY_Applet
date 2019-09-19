// 此代码由 NhGenerator v1.0.9.0 工具生成。

using System;
using System.Collections;
using CMCS.Common.Entities.Sys;
using CMCS.Common.Entities.CarTransport;

namespace CMCS.Common.Entities.Fuel
{
    /// <summary>
    /// 入厂煤来煤预报明细
    /// </summary>
    [Serializable]
    [CMCS.DapperDber.Attrs.DapperBind("FultbLMYBDetail")]
    public class CmcsLMYBDetail : EntityBase1
    {
        private String _CarNumber;
        /// <summary>
        /// 车号
        /// </summary>
        public virtual String CarNumber { get { return _CarNumber; } set { _CarNumber = value; } }

        private String _LMYBId;
        /// <summary>
        /// 来煤预报Id
        /// </summary>
        public virtual String LMYBId { get { return _LMYBId; } set { _LMYBId = value; } }

        private Decimal _TicketWeight;
        /// <summary>
        /// 矿发量
        /// </summary>
        public virtual Decimal TicketWeight { get { return _TicketWeight; } set { _TicketWeight = value; } }

        private string isSynch = "0";
        /// <summary>
        /// 同步标识
        /// </summary>
        public string IsSynch
        {
            get { return isSynch; }
            set { isSynch = value; }
        }
    
        private String _IsFinish;
        /// <summary>
        /// 是否完成
        /// </summary>
        public virtual String IsFinish { get { return _IsFinish; } set { _IsFinish = value; } }

        private String _StorageName;
        /// <summary>
        /// 煤场区域名称
        /// </summary>
        public virtual String StorageName { get { return _StorageName; } set { _StorageName = value; } }

        private String _StorageRolad;
        /// <summary>
        /// 成品仓
        /// </summary>
        public virtual String CPCID { get { return _StorageRolad; } set { _StorageRolad = value; } }

        /// <summary>
        /// 成品仓名称
        /// </summary>
        public virtual String CPCName { get; set; }

        [CMCS.DapperDber.Attrs.DapperIgnore]
        public CmcsLMYB TheLMYB
        {
            get { return Dbers.GetInstance().SelfDber.Get<CmcsLMYB>(this.LMYBId); }
        }

        private String _Remark;
        /// <summary>
        /// 备注
        /// </summary>
        public virtual String Remark { get { return _Remark; } set { _Remark = value; } }

        /// <summary>
        /// 入厂煤运输记录
        /// </summary>
        [CMCS.DapperDber.Attrs.DapperIgnore]
        public CmcsBuyFuelTransport TheBuyFuelTransport
        {
            get { return Dbers.GetInstance().SelfDber.Entity<CmcsBuyFuelTransport>("where LMYBDetailId=:LMYBDetailId", new { LMYBDetailId = this.Id }); }
        }

        /// <summary>
        /// 出厂煤运输记录
        /// </summary>
        [CMCS.DapperDber.Attrs.DapperIgnore]
        public CmcsSaleFuelTransport TheSaleFuelTransport
        {
            get { return Dbers.GetInstance().SelfDber.Entity<CmcsSaleFuelTransport>("where LMYBDetailId=:LMYBDetailId", new { LMYBDetailId = this.Id }); }
        }

    }
}
