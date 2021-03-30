using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.ADGS.Win.Entities;
using CMCS.ADGS.Win.Entities.CSKY_Clims;
using CMCS.Common.Entities.Fuel;
using CMCS.Common;
using CMCS.Common.DAO;
using System.Xml.Linq;
using CMCS.DapperDber.Dbs.OracleDb;
using CMCS.Common.Entities.BaseInfo;

namespace CMCS.ADGS.Win
{
	public class EquAssayDeviceDAO
	{
		private static EquAssayDeviceDAO instance;

		public static EquAssayDeviceDAO GetInstance()
		{
			if (instance == null)
			{
				instance = new EquAssayDeviceDAO();
			}
			return instance;
		}
		ADGSAppConfig _ADGSAppConfig;
		OracleDapperDber selfDber;
		private EquAssayDeviceDAO()
		{
			_ADGSAppConfig = ADGSAppConfig.GetInstance();

			selfDber = new OracleDapperDber(this._ADGSAppConfig.SelfConnStr);
		}

		#region Event

		public delegate void OutputInfoEventHandler(string info);
		public event OutputInfoEventHandler OutputInfo;

		public delegate void OutputErrorEventHandler(string describe, Exception ex);
		public event OutputErrorEventHandler OutputError;

		#endregion

		#region 生成多个设备的标准数据

		public string GetAppletConfig(string configName)
		{
			CmcsAppletConfig config = selfDber.Entity<CmcsAppletConfig>("where AppIdentifier=:AppIdentifier and ConfigName=:ConfigName ", new { AppIdentifier = "后台业务处理程序", ConfigName = configName });
			if (config != null) return config.ConfigValue;
			return string.Empty;
		}

		public string GetCommonAppletConfig(string configName)
		{
			CmcsAppletConfig config = selfDber.Entity<CmcsAppletConfig>("where AppIdentifier=:AppIdentifier and ConfigName=:ConfigName ", new { AppIdentifier = "公共配置", ConfigName = configName });
			if (config != null) return config.ConfigValue;
			return string.Empty;
		}

		/// <summary>
		/// 生成标准量热仪数据
		/// </summary>
		/// <param name="output"></param>
		/// <returns></returns>
		public int CreateToHeatStdAssay()
		{
			int res = 0;

			// .量热仪 型号：5E-C5500A双控
			foreach (LRY_5EC5500 entity in selfDber.Entities<LRY_5EC5500>("where TestTime>= :TestTime and Mancoding is not null", new { TestTime = DateTime.Now.AddDays(-Convert.ToInt32(GetAppletConfig("化验设备数据读取天数"))).Date }))
			{
				res += SaveToHeatStdAssay(entity);
			}

			OutputInfo(string.Format("生成标准量热仪数据 {0} 条", res));

			return res;
		}

		/// <summary>
		/// 生成标准测硫仪数据
		/// </summary>
		/// <param name="output"></param>
		/// <returns></returns>
		public int CreateToSulfurStdAssay()
		{
			int res = 0;

			// .测硫仪 型号：5E-8SAII
			foreach (CLY_5E8SAII entity in selfDber.Entities<CLY_5E8SAII>("where CSRQ>= :TestTime and SYMC is not null", new { TestTime = DateTime.Now.AddDays(-Convert.ToInt32(GetAppletConfig("化验设备数据读取天数"))).Date }))
			{
				res += SaveToSulfurStdAssay(entity);
			}

			OutputInfo(string.Format("生成标准测硫仪数据 {0} 条", res));
			return res;
		}

		/// <summary>
		///  生成标准水分仪数据
		/// </summary>
		/// <param name="output"></param>
		/// <returns></returns>
		public int CreateToMoistureStdAssay()
		{
			int res = 0;
			// .水分仪 型号：5E-MW6510
			foreach (SFY_5EMW6510 entity in selfDber.Entities<SFY_5EMW6510>("where BeginDate>= :TestTime and SampleName is not null", new { TestTime = DateTime.Now.AddDays(-Convert.ToInt32(GetAppletConfig("化验设备数据读取天数"))).Date }))
			{
				res += SaveToMoistureStdAssay(entity);
			}

			OutputInfo(string.Format("生成标准水分仪数据 {0} 条", res));
			return res;
		}

		/// <summary>
		///  生成标准工分仪数据
		/// </summary>
		/// <param name="output"></param>
		/// <returns></returns>
		public int CreateToProximateStdAssay()
		{
			int res = 0;
			// .工分仪 型号：5E-MAG6700
			foreach (HYTBPAG_5EMAG6700 entity in selfDber.Entities<HYTBPAG_5EMAG6700>("where Date_Ex >=:Date_Ex  and SampleName is not null order by Date_Ex", new { Date_Ex = DateTime.Now.AddDays(-Convert.ToInt32(GetAppletConfig("化验设备数据读取天数"))).Date }))
			{
				res += SaveToProximateStdAssay(entity);
			}

			OutputInfo(string.Format("生成标准工分仪数据 {0} 条", res));
			return res;
		}

		/// <summary>
		///  生成标准灰熔融数据
		/// </summary>
		/// <param name="output"></param>
		/// <returns></returns>
		public int CreateToAshStdAssay()
		{
			int res = 0;
			// .灰熔融 型号：5E-AF
			foreach (ASH_5EAF entity in selfDber.Entities<ASH_5EAF>("where TestDate >=:TestDate  and SampleName is not null", new { TestDate = DateTime.Now.AddDays(-Convert.ToInt32(GetAppletConfig("化验设备数据读取天数"))).Date }))
			{
				res += SaveToAshStdAssay(entity);
			}

			OutputInfo(string.Format("生成标准灰熔融数据 {0} 条", res));
			return res;
		}

		/// <summary>
		///  生成标准碳氢仪数据
		/// </summary>
		/// <param name="output"></param>
		/// <returns></returns>
		public int CreateToHadStdAssay()
		{
			int res = 0;
			// .碳氢仪 型号：5E-CH2200
			foreach (HAD_CH2200 entity in selfDber.Entities<HAD_CH2200>("where Date_Ex >=:Date_Ex  and Name is not null", new { Date_Ex = DateTime.Now.AddDays(-Convert.ToInt32(GetAppletConfig("化验设备数据读取天数"))).Date }))
			{
				res += SaveToHadStdAssay(entity);
			}

			OutputInfo(string.Format("生成标准碳氢仪数据 {0} 条", res));
			return res;
		}

		#endregion

		#region 保存标准数据
		/// <summary>
		/// 生成标准测硫仪数据
		/// </summary>
		/// <param name="output"></param>
		/// <returns></returns>
		public int SaveToSulfurStdAssay(CLY_5E8SAII entity)
		{
			int res = 0;
			if (entity == null) return res;
			CmcsSulfurStdAssay item = selfDber.Entity<CmcsSulfurStdAssay>("where PKID=:PKID", new { PKID = entity.PKID });
			if (item == null)
			{
				item = new CmcsSulfurStdAssay();
				item.SampleNumber = entity.SYMC;
				item.FacilityNumber = entity.MachineCode;
				item.ContainerWeight = entity.GGZ;
				item.SampleWeight = entity.SYZL;
				item.Stad = entity.KGJQL;
				item.Std = entity.GJQL;
				item.AssayUser = entity.HYY;
				item.AssayTime = entity.CSRQ;
				item.OrderNumber = 0;
				item.IsEffective = 0;
				item.PKID = entity.PKID;
				item.DataType = "原始数据";
				res += selfDber.Insert<CmcsSulfurStdAssay>(item);
			}
			else
			{
				if (item.IsEffective == 1) return res;
				item.FacilityNumber = entity.MachineCode;
				item.ContainerWeight = entity.GGZ;
				item.SampleWeight = entity.SYZL;
				item.Stad = entity.KGJQL;
				item.Std = entity.GJQL;
				if (item.IsHandModify != "1")
				{
					item.SampleNumber = entity.SYMC;
					item.AssayUser = entity.HYY;
				}
				item.AssayTime = entity.CSRQ;
				item.OrderNumber = 0;
				item.DataType = "原始数据";
				res += selfDber.Update<CmcsSulfurStdAssay>(item);
			}
			return res;
		}

		/// <summary>
		/// 生成标准量热仪数据
		/// </summary>
		/// <param name="output"></param>
		/// <returns></returns>
		public int SaveToHeatStdAssay(LRY_5EC5500 entity)
		{
			int res = 0;
			if (entity == null) return res;
			CmcsHeatStdAssay item = selfDber.Entity<CmcsHeatStdAssay>("where PKID=:PKID", new { PKID = entity.PKID });
			if (entity.Mingchen.Contains('-'))
				entity.Mingchen = entity.Mingchen.Substring(0, entity.Mingchen.LastIndexOf('-') - 1);
			if (item == null)
			{
				item = new CmcsHeatStdAssay();
				item.SampleNumber = entity.Mingchen;
				item.FacilityNumber = entity.MachineCode;
				item.ContainerWeight = 0;
				item.SampleWeight = Convert.ToDecimal(entity.Weight);
				item.Qbad = Convert.ToDecimal(entity.Qb);
				item.AssayUser = entity.Testman;
				item.AssayTime = entity.TestTime;
				item.IsEffective = 0;
				item.PKID = entity.PKID;
				item.DataType = "原始数据";
				res += selfDber.Insert<CmcsHeatStdAssay>(item);
			}
			else
			{
				if (item.IsEffective == 1) return res;
				item.FacilityNumber = entity.MachineCode;
				item.ContainerWeight = 0;
				item.SampleWeight = Convert.ToDecimal(entity.Weight);
				item.Qbad = Convert.ToDecimal(entity.Qb);
				if (item.IsHandModify != "1")
				{
					item.SampleNumber = entity.Mingchen;
					item.AssayUser = entity.Testman;
				}
				item.AssayTime = entity.TestTime;
				item.DataType = "原始数据";
				res += selfDber.Update<CmcsHeatStdAssay>(item);
			}

			return res;
		}

		/// <summary>
		/// 保存标准水分仪数据
		/// </summary>
		/// <param name="output"></param>
		/// <returns></returns>
		public int SaveToMoistureStdAssay(SFY_5EMW6510 entity)
		{
			int res = 0;
			if (entity == null) return res;
			CmcsMoistureStdAssay item = selfDber.Entity<CmcsMoistureStdAssay>("where PKID=:PKID", new { PKID = entity.PKID });
			if (item == null)
			{
				item = new CmcsMoistureStdAssay();
				item.SampleNumber = entity.SampleName;
				item.FacilityNumber = entity.MachineCode;
				item.ContainerWeight = 0;
				item.SampleWeight = entity.Sample;
				item.WaterType = entity.Content.Contains("全水") ? "全水分" : "分析水";
				item.WaterPer = entity.Moisture;
				//if (item.WaterType == "全水分") item.WaterPer += Convert.ToDecimal(GetCommonAppletConfig("全水补正值"));
				item.AssayUser = entity.Operator;
				item.IsEffective = 0;
				item.PKID = entity.PKID;
				item.AssayTime = entity.BeginDate;
				item.DataType = "原始数据";
				res += selfDber.Insert<CmcsMoistureStdAssay>(item);
			}
			else
			{
				if (item.IsEffective == 1) return res;

				item.FacilityNumber = entity.MachineCode;
				item.ContainerWeight = 0;
				item.SampleWeight = entity.Sample;
				item.WaterPer = entity.Moisture;
				if (item.IsHandModify != "1")
				{
					item.SampleNumber = entity.SampleName;
					item.AssayUser = entity.Operator;
				}
				item.AssayTime = entity.BeginDate;
				item.WaterType = entity.Content.Contains("全水") ? "全水分" : "分析水";
				item.DataType = "原始数据";
				res += selfDber.Update<CmcsMoistureStdAssay>(item);
			}
			return res;
		}

		/// <summary>
		/// 保存工业分析仪数据
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		public int SaveToProximateStdAssay(HYTBPAG_5EMAG6700 entity)
		{
			int res = 0;
			if (entity == null) return res;
			CmcsRCAssay assay = selfDber.Entity<CmcsRCAssay>("where AssayCode=:AssayCode order by CreateDate", new { AssayCode = entity.SampleName });
			if (assay != null && assay.AssayDate.Year < 2000)
			{
				assay.AssayDate = entity.Date_Ex;
				selfDber.Update(assay);
			}
			CmcsProximateStdAssay present = selfDber.Entity<CmcsProximateStdAssay>("where PKID=:PKID", new { PKID = entity.PKID });
			if (present == null)
			{
				present = new CmcsProximateStdAssay();

				present.PKID = entity.PKID;
				present.SampleNumber = entity.SampleName;
				present.ContainerWeight = entity.EmptyGGWeight;
				present.SampleWeight = entity.ColeWeight;
				present.Vad = entity.Vad;
				present.Mad = entity.Mad;
				present.Aad = entity.Aad;
				present.AssayUser = entity.Operator;
				present.AssayTime = entity.Date_Ex;
				present.FacilityNumber = entity.MachineCode;
				present.OrderNumber = entity.ObjCode;
				present.DataType = "原始数据";

				return selfDber.Insert(present);
			}
			if (present.IsEffective == 1) return res;
			present.ContainerWeight = entity.EmptyGGWeight;
			present.SampleWeight = entity.ColeWeight;
			present.Vad = entity.Vad;
			present.Mad = entity.Mad;
			present.Aad = entity.Aad;
			if (present.IsHandModify != "1")
			{
				present.SampleNumber = entity.SampleName;
				present.AssayUser = entity.Operator;
			}
			present.AssayTime = entity.Date_Ex;
			present.FacilityNumber = entity.MachineCode;
			present.OrderNumber = entity.ObjCode;
			present.DataType = "原始数据";
			return selfDber.Update(present);
		}

		/// <summary>
		/// 保存灰熔融数据
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		public int SaveToAshStdAssay(ASH_5EAF entity)
		{
			int res = 0;
			if (entity == null) return res;
			CmcsAshStdAssay item = selfDber.Entity<CmcsAshStdAssay>("where PKID=:PKID", new { PKID = entity.PKID });
			if (item == null)
			{
				item = new CmcsAshStdAssay();
				item.SampleNumber = entity.SampleName;
				item.FacilityNumber = entity.MachineCode;
				item.ContainerWeight = 0;
				item.SampleWeight = 0;
				item.DT = entity.DT;
				item.ST = entity.ST;
				item.FT = entity.FT;
				item.HT = entity.HT;
				item.AssayUser = entity.Operator;
				item.IsEffective = 0;
				item.PKID = entity.PKID;
				item.AssayTime = entity.TestDate;
				item.DataType = "原始数据";
				res += selfDber.Insert<CmcsAshStdAssay>(item);
			}
			else
			{
				if (item.IsEffective == 1) return res;
				item.FacilityNumber = entity.MachineCode;
				item.ContainerWeight = 0;
				item.SampleWeight = 0;
				item.DT = entity.DT;
				item.ST = entity.ST;
				item.FT = entity.FT;
				item.HT = entity.HT;
				if (item.IsHandModify != "1")
				{
					item.SampleNumber = entity.SampleName;
					item.AssayUser = entity.Operator;
				}
				item.IsEffective = 0;
				item.PKID = entity.PKID;
				item.AssayTime = entity.TestDate;
				item.DataType = "原始数据";
				res += selfDber.Update<CmcsAshStdAssay>(item);
			}
			return res;
		}


		/// <summary>
		/// 保存碳氢仪数据
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		public int SaveToHadStdAssay(HAD_CH2200 entity)
		{
			int res = 0;
			if (entity == null) return res;
			CmcsHadStdAssay item = selfDber.Entity<CmcsHadStdAssay>("where PKID=:PKID", new { PKID = entity.PKID });
			if (item == null)
			{
				item = new CmcsHadStdAssay();
				item.SampleNumber = entity.Name;
				item.FacilityNumber = entity.MachineCode;
				item.SampleWeight = entity.Weight;
				item.Had = entity.Had;
				item.Hd = entity.Hd;
				item.Cad = entity.Cad;
				item.Cd = entity.Cd;
				item.AssayUser = entity.Operator;
				item.IsEffective = 0;
				item.PKID = entity.PKID;
				item.AssayTime = entity.Date_Ex;
				item.DataType = "原始数据";
				res += selfDber.Insert<CmcsHadStdAssay>(item);
			}
			else
			{
				if (item.IsEffective == 1) return res;
				item.FacilityNumber = entity.MachineCode;
				item.SampleWeight = entity.Weight;
				item.Had = entity.Had;
				item.Hd = entity.Hd;
				item.Cad = entity.Cad;
				item.Cd = entity.Cd;
				if (item.IsHandModify != "1")
				{
					item.SampleNumber = entity.Name;
					item.AssayUser = entity.Operator;
				}
				item.IsEffective = 0;
				item.AssayTime = entity.Date_Ex;
				item.DataType = "原始数据";
				res += selfDber.Update<CmcsHadStdAssay>(item);
			}
			return res;
		}
		#endregion
	}
}
