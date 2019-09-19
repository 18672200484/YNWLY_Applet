using CMCS.DapperDber.Attrs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMCS.ADGS.Win.Entities.CSKY_Clims
{
    /// <summary>
    /// 开元化验数据汇总表（计算结果）
    /// </summary>
    [CMCS.DapperDber.Attrs.DapperBind("HY_LAST_DATA")]
    public class HY_LAST_DATA
    {
      [DapperPrimaryKey]
       public decimal ID {get;set;}
      public string  SAMPLE_NO {get;set;}
      public DateTime  TEST_TIME_START {get;set;}
      public DateTime TEST_TIME_END { get; set; }
      public decimal  MT {get;set;}
      public decimal  MAD {get;set;}
      public decimal  VAD {get;set;}
      public decimal  AAD {get;set;}
      public decimal  QBAD {get;set;}
      public decimal  STAD {get;set;}
      public decimal  HAD {get;set;}
      public int  MT_C {get;set;}
      public int M_C { get; set; }
      public int A_C { get; set; }
      public int V_C { get; set; }
      public int QB_C { get; set; }
      public int H_C { get; set; }
      public int ST_C { get; set; }
      public int STATE { get; set; }
      public int DT { get; set; }
      public int ST { get; set; }
      public int HT { get; set; }
      public int FT { get; set; }
      public int JZTZ { get; set; }
      public int DT_C { get; set; }
      public int HT_C { get; set; }
      public int FT_C { get; set; }
      public int ST_SC { get; set; }
      public int Mtcount { get; set; }
      public int Mcount { get; set; }
      public int Acount { get; set; }
      public int Vcount { get; set; }
      public int Qbcount { get; set; }
      public int Hcount { get; set; }
      public int Stcount { get; set; }
      public int DTcount { get; set; }
      public int HTcount { get; set; }
      public int FTcount { get; set; }
      public int STScount { get; set; }
      public int CoalWeight { get; set; }
      public decimal  Oad {get;set;}
    }
}
