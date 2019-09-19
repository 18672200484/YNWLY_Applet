using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common.Entities.Sys;

namespace CMCS.Common.Entities.Fuel
{
    /// <summary>
    /// 入厂煤车次明细表
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("fultbtransport")]
    public class CmcsTransport : EntityBase1
    {
        /// <summary>
        /// 第三方主键
        /// </summary>
        public string PKID { get; set; }

        /// <summary>
        /// 运输方式
        /// </summary>
        public String TransportStyle { get; set; }

        /// <summary>
        /// 运输类型
        /// </summary>
        public String TransportType { get; set; }

        /// <summary>
        /// 数据来源
        /// </summary>
        public String DataSource { get; set; }

        /// <summary>
        /// 车船号
        /// </summary>
        public String TransportNo { get; set; }

        /// <summary>
        /// 矿发量(吨)
        /// </summary>
        public Decimal TicketWeight { get; set; }

        /// <summary>
        /// 净重(吨)
        /// </summary>
        public Decimal StandardWeight { get; set; }

        /// <summary>
        /// 验收量(吨)
        /// </summary>
        public Decimal CheckQty { get; set; }

        /// <summary>
        /// 盈亏(吨)
        /// </summary>
        public Decimal MarginWeight { get; set; }

        /// <summary>
        /// 路损(吨)
        /// </summary>
        public Decimal RailLost { get; set; }

        /// <summary>
        /// 毛重(吨)
        /// </summary>
        public Decimal GrossWeight { get; set; }

        /// <summary>
        /// 皮重(吨)
        /// </summary>
        public Decimal SkinWeight { get; set; }

        /// <summary>
        /// MesureMan
        /// </summary>
        public String MesureMan { get; set; }

        /// <summary>
        /// 入厂顺序
        /// </summary>
        public Int32 OrderNumber { get; set; }

        /// <summary>
        /// 毛重时间
        /// </summary>
        public DateTime ArriveDate { get; set; }

        /// <summary>
        /// 皮重时间
        /// </summary>
        public DateTime TareDate { get; set; }

        /// <summary>
        /// 出厂时间
        /// </summary>
        public DateTime OutFactoryTime { get; set; }

        /// <summary>
        /// 入厂时间
        /// </summary>
        public DateTime InFactoryTime { get; set; }

        /// <summary>
        /// 卸车时间
        /// </summary>
        public DateTime DisboardTime { get; set; }

        /// <summary>
        /// 备注
        /// </summary>
        public String Remark { get; set; }

        /// <summary>
        /// 关联：批次
        /// </summary>
        public string InFactoryBatchId { get; set; }

        /// <summary>
        /// 扣矸(吨)
        /// </summary>
        public Decimal KGWEIGHT { get; set; }

        /// <summary>
        /// 扣水(吨)
        /// </summary>
        public Decimal KSWEIGHT { get; set; }

        /// <summary>
        /// 剩余量(吨)
        /// </summary>
        public Decimal QtyHave { get; set; }

    }
}
