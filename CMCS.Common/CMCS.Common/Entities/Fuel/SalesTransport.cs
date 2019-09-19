using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.Entities.Sys;

namespace CMCS.Common.Entities.Fuel
{
    /// <summary>
    /// 销售煤车次表
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("FULTBSALESTRANSPORT")]
    public class SalesTransport : EntityBase1
    {
        private Int32 _OrderNumber;
        /// <summary>
        /// 入厂顺序
        /// </summary>
        public virtual Int32 OrderNumber { get { return _OrderNumber; } set { _OrderNumber = value; } }

        private String _TransportNo;
        /// <summary>
        /// 车号
        /// </summary>
        public virtual String TransportNo { get { return _TransportNo; } set { _TransportNo = value; } }

        private Decimal _GrossWeight;
        /// <summary>
        /// 毛重(吨)
        /// </summary>
        public virtual Decimal GrossWeight { get { return _GrossWeight; } set { _GrossWeight = value; } }

        private Decimal _SkinWeight;
        /// <summary>
        /// 皮重(吨)
        /// </summary>
        public virtual Decimal SkinWeight { get { return _SkinWeight; } set { _SkinWeight = value; } }

        private Decimal _StandardWeight;
        /// <summary>
        /// 净重(吨)
        /// </summary>
        public virtual Decimal StandardWeight { get { return _StandardWeight; } set { _StandardWeight = value; } }

        private DateTime _TareDate;
        /// <summary>
        /// 皮重时间
        /// </summary>
        public virtual DateTime TareDate { get { return _TareDate; } set { _TareDate = value; } }

        private DateTime _LeaveDate;
        /// <summary>
        /// 毛重时间
        /// </summary>
        public virtual DateTime LeaveDate { get { return _LeaveDate; } set { _LeaveDate = value; } }

        private String _MeasureMan;
        /// <summary>
        /// 过衡人
        /// </summary>
        public virtual String MeasureMan { get { return _MeasureMan; } set { _MeasureMan = value; } }

        private DateTime _InFactoryTime;
        /// <summary>
        /// 入厂时间
        /// </summary>
        public virtual DateTime InFactoryTime { get { return _InFactoryTime; } set { _InFactoryTime = value; } }

        private DateTime _OutFactoryTime;
        /// <summary>
        /// 出厂时间
        /// </summary>
        public virtual DateTime OutFactoryTime { get { return _OutFactoryTime; } set { _OutFactoryTime = value; } }

        private DateTime _FillTime;
        /// <summary>
        /// 上煤时间
        /// </summary>
        public virtual DateTime FillTime { get { return _FillTime; } set { _FillTime = value; } }

        private String _InOutBatchId;
        /// <summary>
        /// 批次来煤 多对一
        /// </summary>
        public virtual String InOutBatchId { get { return _InOutBatchId; } set { _InOutBatchId = value; } }

        private string _PKID;
        /// <summary>
        /// 第三方主键
        /// </summary>
        public virtual string PKID { get { return _PKID; } set { _PKID = value; } }

        private Int32 _IsAdjusted;
        /// <summary>
        /// 1已生成日数据(大表),0没生成数据 或 需要更新
        /// </summary>
        public virtual Int32 IsAdjusted { get { return _IsAdjusted; } set { _IsAdjusted = value; } }

        private string _IsFinishFill;
        /// <summary>
        /// 是否上煤完毕
        /// </summary>
        public virtual string IsFinishFill { get { return _IsFinishFill; } set { _IsFinishFill = value; } }

        private Int32 _IsFinish;
        /// <summary>
        /// 是否结束
        /// </summary>
        public virtual Int32 IsFinish { get { return _IsFinish; } set { _IsFinish = value; } }

        private Int32 _IsBalance;
        /// <summary>
        /// 是否结算
        /// </summary>
        public virtual Int32 IsBalance { get { return _IsBalance; } set { _IsBalance = value; } }

        private String _DataSource;
        /// <summary>
        /// 数据源：轨道衡、汽车衡、输入
        /// </summary>
        public virtual String DataSource { get { return _DataSource; } set { _DataSource = value; } }

        private Int32 _IsUpload;
        /// <summary>
        /// 是否上传
        /// </summary>
        public virtual Int32 IsUpload { get { return _IsUpload; } set { _IsUpload = value; } }

        private String _DataFrom;
        /// <summary>
        /// 数据来源
        /// </summary>
        public virtual String DataFrom { get { return _DataFrom; } set { _DataFrom = value; } }

        private Int32 _IsSign;
        /// <summary>
        /// 是否审核
        /// </summary>
        public virtual Int32 IsSign { get { return _IsSign; } set { _IsSign = value; } }

        private string isSynch = "0";
        /// <summary>
        /// 同步标识
        /// </summary>
        public string IsSynch
        {
            get { return isSynch; }
            set { isSynch = value; }
        }
    }
}
