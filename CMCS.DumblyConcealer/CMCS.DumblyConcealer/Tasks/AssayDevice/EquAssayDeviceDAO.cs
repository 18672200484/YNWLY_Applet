using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.DumblyConcealer.Enums;
using CMCS.DumblyConcealer.Tasks.AssayDevice.Entities;
using CMCS.DumblyConcealer.Tasks.AssayDevice.Entities.CSKY_Clims;
using CMCS.Common.Entities.Fuel;
using CMCS.Common;
using CMCS.Common.DAO;
using System.Xml.Linq;

namespace CMCS.DumblyConcealer.Tasks.AssayDevice
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

        private EquAssayDeviceDAO()
        {

        }

        CommonDAO commonDAO = CommonDAO.GetInstance();
        //DcDbers dcDbersDAO = DcDbers.GetInstance();

        #region 生成多个设备的标准数据

        /// <summary>
        /// 生成标准量热仪数据
        /// </summary>
        /// <param name="output"></param>
        /// <returns></returns>
        public int CreateToHeatStdAssay(Action<string, eOutputType> output)
        {
            int res = 0;

            // .量热仪 型号：5E-C5500A双控
            foreach (LRY_5EC5500 entity in Dbers.GetInstance().SelfDber.Entities<LRY_5EC5500>("where TestTime>= :TestTime and Mancoding is not null", new { TestTime = DateTime.Now.AddDays(-Convert.ToInt32(commonDAO.GetAppletConfigString("化验设备数据读取天数"))).Date }))
            {
                res += SaveToHeatStdAssay(entity);
            }

            output(string.Format("生成标准量热仪数据 {0} 条", res), eOutputType.Normal);

            return res;
        }

        /// <summary>
        /// 生成标准测硫仪数据
        /// </summary>
        /// <param name="output"></param>
        /// <returns></returns>
        public int CreateToSulfurStdAssay(Action<string, eOutputType> output)
        {
            int res = 0;

            // .测硫仪 型号：5E-8SAII
            foreach (CLY_5E8SAII entity in Dbers.GetInstance().SelfDber.Entities<CLY_5E8SAII>("where CSRQ>= :TestTime and SYMC is not null", new { TestTime = DateTime.Now.AddDays(-Convert.ToInt32(commonDAO.GetAppletConfigString("化验设备数据读取天数"))).Date }))
            {
                res += SaveToSulfurStdAssay(entity);
            }

            //foreach (CLY_5E8SAII entity in Dbers.GetInstance().SelfDber.Entities<CLY_5E8SAII2>("where CSRQ>= :TestTime and SYMC is not null", new { TestTime = DateTime.Now.AddDays(-Convert.ToInt32(commonDAO.GetAppletConfigString("化验设备数据读取天数"))).Date }))
            //{
            //    res += SaveToSulfurStdAssay(entity);
            //}

            //foreach (CLY_5E8SAII entity in Dbers.GetInstance().SelfDber.Entities<CLY_5E8SAII3>("where CSRQ>= :TestTime and SYMC is not null", new { TestTime = DateTime.Now.AddDays(-Convert.ToInt32(commonDAO.GetAppletConfigString("化验设备数据读取天数"))).Date }))
            //{
            //    res += SaveToSulfurStdAssay(entity);
            //}

            output(string.Format("生成标准测硫仪数据 {0} 条", res), eOutputType.Normal);
            return res;
        }

        /// <summary>
        ///  生成标准水分仪数据
        /// </summary>
        /// <param name="output"></param>
        /// <returns></returns>
        public int CreateToMoistureStdAssay(Action<string, eOutputType> output)
        {
            int res = 0;
            // .水分仪 型号：5E-MW6510
            foreach (SFY_5EMW6510 entity in Dbers.GetInstance().SelfDber.Entities<SFY_5EMW6510>("where BeginDate>= :TestTime and SampleName is not null", new { TestTime = DateTime.Now.AddDays(-Convert.ToInt32(commonDAO.GetAppletConfigString("化验设备数据读取天数"))).Date }))
            {
                res += SaveToMoistureStdAssay(entity);
            }

            //foreach (SFY_5EMW6510 entity in Dbers.GetInstance().SelfDber.Entities<SFY_5EMW65102>("where BeginDate>= :TestTime and SampleName is not null", new { TestTime = DateTime.Now.AddDays(-Convert.ToInt32(commonDAO.GetAppletConfigString("化验设备数据读取天数"))).Date }))
            //{
            //    res += SaveToMoistureStdAssay(entity);
            //}

            //foreach (SFY_5EMW6510 entity in Dbers.GetInstance().SelfDber.Entities<SFY_5EMW65103>("where BeginDate>= :TestTime and SampleName is not null", new { TestTime = DateTime.Now.AddDays(-Convert.ToInt32(commonDAO.GetAppletConfigString("化验设备数据读取天数"))).Date }))
            //{
            //    res += SaveToMoistureStdAssay(entity);
            //}
            output(string.Format("生成标准水分仪数据 {0} 条", res), eOutputType.Normal);
            return res;
        }
        /// <summary>
        ///  生成标准工分仪数据
        /// </summary>
        /// <param name="output"></param>
        /// <returns></returns>
        public int CreateToProximateStdAssay(Action<string, eOutputType> output)
        {
            int res = 0;
            // .工分仪 型号：5E-MAG6700
            foreach (HYTBPAG_5EMAG6700 entity in Dbers.GetInstance().SelfDber.Entities<HYTBPAG_5EMAG6700>("where Date_Ex >=:Date_Ex  and SampleName is not null", new { Date_Ex = DateTime.Now.AddDays(-Convert.ToInt32(commonDAO.GetAppletConfigString("化验设备数据读取天数"))).Date }))
            {
                res += SaveToProximateStdAssay(entity);
            }

            //foreach (HYTBPAG_5EMAG6700 entity in Dbers.GetInstance().SelfDber.Entities<HYTBPAG_5EMAG67002>("where Date_Ex >=:Date_Ex  and SampleName is not null", new { Date_Ex = DateTime.Now.AddDays(-Convert.ToInt32(commonDAO.GetAppletConfigString("化验设备数据读取天数"))).Date }))
            //{
            //    res += SaveToProximateStdAssay(entity);
            //}

            //foreach (HYTBPAG_5EMAG6700 entity in Dbers.GetInstance().SelfDber.Entities<HYTBPAG_5EMAG67003>("where Date_Ex >=:Date_Ex  and SampleName is not null", new { Date_Ex = DateTime.Now.AddDays(-Convert.ToInt32(commonDAO.GetAppletConfigString("化验设备数据读取天数"))).Date }))
            //{
            //    res += SaveToProximateStdAssay(entity);
            //}

            //foreach (HYTBPAG_5EMAG6700 entity in Dbers.GetInstance().SelfDber.Entities<HYTBPAG_5EMAG67004>("where Date_Ex >=:Date_Ex  and SampleName is not null", new { Date_Ex = DateTime.Now.AddDays(-Convert.ToInt32(commonDAO.GetAppletConfigString("化验设备数据读取天数"))).Date }))
            //{
            //    res += SaveToProximateStdAssay(entity);
            //}
            output(string.Format("生成标准工分仪数据 {0} 条", res), eOutputType.Normal);
            return res;
        }

        /// <summary>
        ///  生成标准灰熔融数据
        /// </summary>
        /// <param name="output"></param>
        /// <returns></returns>
        public int CreateToAshStdAssay(Action<string, eOutputType> output)
        {
            int res = 0;
            // .灰熔融 型号：5E-AF
            foreach (ASH_5EAF entity in Dbers.GetInstance().SelfDber.Entities<ASH_5EAF>("where TestDate >=:TestDate  and SampleName is not null", new { TestDate = DateTime.Now.AddDays(-Convert.ToInt32(commonDAO.GetAppletConfigString("化验设备数据读取天数"))).Date }))
            {
                res += SaveToAshStdAssay(entity);
            }

            //foreach (ASH_5EAF entity in Dbers.GetInstance().SelfDber.Entities<ASH_5EAF2>("where TestDate >=:TestDate  and SampleName is not null", new { TestDate = DateTime.Now.AddDays(-Convert.ToInt32(commonDAO.GetAppletConfigString("化验设备数据读取天数"))).Date }))
            //{
            //    res += SaveToAshStdAssay(entity);
            //}

            output(string.Format("生成标准灰熔融数据 {0} 条", res), eOutputType.Normal);
            return res;
        }

        /// <summary>
        ///  生成标准碳氢仪数据
        /// </summary>
        /// <param name="output"></param>
        /// <returns></returns>
        public int CreateToHadStdAssay(Action<string, eOutputType> output)
        {
            int res = 0;
            // .碳氢仪 型号：5E-CH2200
            foreach (HAD_CH2200 entity in Dbers.GetInstance().SelfDber.Entities<HAD_CH2200>("where Date_Ex >=:Date_Ex  and Name is not null", new { Date_Ex = DateTime.Now.AddDays(-Convert.ToInt32(commonDAO.GetAppletConfigString("化验设备数据读取天数"))).Date }))
            {
                res += SaveToHadStdAssay(entity);
            }

            //foreach (HAD_CH2200 entity in Dbers.GetInstance().SelfDber.Entities<HAD_CH22002>("where Date_Ex >=:Date_Ex  and Name is not null", new { Date_Ex = DateTime.Now.AddDays(-Convert.ToInt32(commonDAO.GetAppletConfigString("化验设备数据读取天数"))).Date }))
            //{
            //    res += SaveToHadStdAssay(entity);
            //}

            output(string.Format("生成标准碳氢仪数据 {0} 条", res), eOutputType.Normal);
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
            CmcsSulfurStdAssay item = Dbers.GetInstance().SelfDber.Entity<CmcsSulfurStdAssay>("where PKID=:PKID", new { PKID = entity.PKID });
            if (item == null)
            {
                item = new CmcsSulfurStdAssay();
                item.SampleNumber = entity.SYMC;
                item.FacilityNumber = entity.MachineCode;
                item.ContainerWeight = entity.GGZ;
                item.SampleWeight = entity.SYZL;
                item.Stad = entity.KGJQL;
                item.Std = entity.GJQL;
                if (item.IsHandModify != "1") item.AssayUser = entity.HYY;
                item.AssayTime = entity.CSRQ;
                item.OrderNumber = 0;
                item.IsEffective = 0;
                item.PKID = entity.PKID;
                item.DataType = "原始数据";
                res += Dbers.GetInstance().SelfDber.Insert<CmcsSulfurStdAssay>(item);
            }
            else
            {
                if (item.IsEffective == 1) return res;
                item.SampleNumber = entity.SYMC;
                item.FacilityNumber = entity.MachineCode;
                item.ContainerWeight = entity.GGZ;
                item.SampleWeight = entity.SYZL;
                item.Stad = entity.KGJQL;
                item.Std = entity.GJQL;
                if (item.IsHandModify != "1") item.AssayUser = entity.HYY;
                item.AssayTime = entity.CSRQ;
                item.OrderNumber = 0;
                item.DataType = "原始数据";
                res += Dbers.GetInstance().SelfDber.Update<CmcsSulfurStdAssay>(item);
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
            CmcsHeatStdAssay item = Dbers.GetInstance().SelfDber.Entity<CmcsHeatStdAssay>("where PKID=:PKID", new { PKID = entity.PKID });
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
                if (item.IsHandModify != "1") item.AssayUser = entity.Testman;
                item.AssayTime = entity.TestTime;
                item.IsEffective = 0;
                item.PKID = entity.PKID;
                item.DataType = "原始数据";
                res += Dbers.GetInstance().SelfDber.Insert<CmcsHeatStdAssay>(item);
            }
            else
            {
                if (item.IsEffective == 1) return res;
                item.SampleNumber = entity.Mingchen;
                item.FacilityNumber = entity.MachineCode;
                item.ContainerWeight = 0;
                item.SampleWeight = Convert.ToDecimal(entity.Weight);
                item.Qbad = Convert.ToDecimal(entity.Qb);
                if (item.IsHandModify != "1") item.AssayUser = entity.Testman;
                item.AssayTime = entity.TestTime;
                item.DataType = "原始数据";
                res += Dbers.GetInstance().SelfDber.Update<CmcsHeatStdAssay>(item);
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
            CmcsMoistureStdAssay item = Dbers.GetInstance().SelfDber.Entity<CmcsMoistureStdAssay>("where PKID=:PKID", new { PKID = entity.PKID });
            if (item == null)
            {
                item = new CmcsMoistureStdAssay();
                item.SampleNumber = entity.SampleName;
                item.FacilityNumber = entity.MachineCode;
                item.ContainerWeight = 0;
                item.SampleWeight = entity.Sample;
                item.WaterType = entity.Content.Contains("全水") ? "全水分" : "分析水";
                item.WaterPer = entity.Moisture;
                if (item.WaterType == "全水分") item.WaterPer += commonDAO.GetCommonAppletConfigDecimal("全水补正值");
                if (item.IsHandModify != "1") item.AssayUser = entity.Operator;
                item.IsEffective = 0;
                item.PKID = entity.PKID;
                item.AssayTime = entity.BeginDate;
                item.DataType = "原始数据";
                res += Dbers.GetInstance().SelfDber.Insert<CmcsMoistureStdAssay>(item);
            }
            else
            {
                if (item.IsEffective == 1) return res;
                item.SampleNumber = entity.SampleName;
                item.FacilityNumber = entity.MachineCode;
                item.ContainerWeight = 0;
                item.SampleWeight = entity.Sample;
                item.WaterPer = entity.Moisture;
                if (item.IsHandModify != "1") item.AssayUser = entity.Operator;
                item.AssayTime = entity.BeginDate;
                item.WaterType = entity.Content.Contains("全水") ? "全水分" : "分析水";
                item.DataType = "原始数据";
                res += Dbers.GetInstance().SelfDber.Update<CmcsMoistureStdAssay>(item);
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

            CmcsProximateStdAssay present = Dbers.GetInstance().SelfDber.Entity<CmcsProximateStdAssay>("where PKID=:PKID", new { PKID = entity.PKID });
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
                if (present.IsHandModify != "1") present.AssayUser = entity.Operator;
                present.AssayTime = entity.Date_Ex;
                present.FacilityNumber = entity.MachineCode;
                present.OrderNumber = entity.ObjCode;
                present.DataType = "原始数据";

                return Dbers.GetInstance().SelfDber.Insert(present);
            }
            if (present.IsEffective == 1) return res;
            present.SampleNumber = entity.SampleName;
            present.ContainerWeight = entity.EmptyGGWeight;
            present.SampleWeight = entity.ColeWeight;
            present.Vad = entity.Vad;
            present.Mad = entity.Mad;
            present.Aad = entity.Aad;
            if (present.IsHandModify != "1") present.AssayUser = entity.Operator;
            present.AssayTime = entity.Date_Ex;
            present.FacilityNumber = entity.MachineCode;
            present.OrderNumber = entity.ObjCode;
            present.DataType = "原始数据";
            return Dbers.GetInstance().SelfDber.Update(present);
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
            CmcsAshStdAssay item = Dbers.GetInstance().SelfDber.Entity<CmcsAshStdAssay>("where PKID=:PKID", new { PKID = entity.PKID });
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
                if (item.IsHandModify != "1") item.AssayUser = entity.Operator;
                item.IsEffective = 0;
                item.PKID = entity.PKID;
                item.AssayTime = entity.TestDate;
                item.DataType = "原始数据";
                res += Dbers.GetInstance().SelfDber.Insert<CmcsAshStdAssay>(item);
            }
            else
            {
                if (item.IsEffective == 1) return res;
                item.SampleNumber = entity.SampleName;
                item.FacilityNumber = entity.MachineCode;
                item.ContainerWeight = 0;
                item.SampleWeight = 0;
                item.DT = entity.DT;
                item.ST = entity.ST;
                item.FT = entity.FT;
                item.HT = entity.HT;
                if (item.IsHandModify != "1") item.AssayUser = entity.Operator;
                item.IsEffective = 0;
                item.PKID = entity.PKID;
                item.AssayTime = entity.TestDate;
                item.DataType = "原始数据";
                res += Dbers.GetInstance().SelfDber.Update<CmcsAshStdAssay>(item);
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
            CmcsHadStdAssay item = Dbers.GetInstance().SelfDber.Entity<CmcsHadStdAssay>("where PKID=:PKID", new { PKID = entity.PKID });
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
                if (item.IsHandModify != "1") item.AssayUser = entity.Operator;
                item.IsEffective = 0;
                item.PKID = entity.PKID;
                item.AssayTime = entity.Date_Ex;
                item.DataType = "原始数据";
                res += Dbers.GetInstance().SelfDber.Insert<CmcsHadStdAssay>(item);
            }
            else
            {
                if (item.IsEffective == 1) return res;
                item.SampleNumber = entity.Name;
                item.FacilityNumber = entity.MachineCode;
                item.SampleWeight = entity.Weight;
                item.Had = entity.Had;
                item.Hd = entity.Hd;
                item.Cad = entity.Cad;
                item.Cd = entity.Cd;
                if (item.IsHandModify != "1") item.AssayUser = entity.Operator;
                item.IsEffective = 0;
                item.AssayTime = entity.Date_Ex;
                item.DataType = "原始数据";
                res += Dbers.GetInstance().SelfDber.Update<CmcsHadStdAssay>(item);
            }
            return res;
        }
        #endregion

        #region 同步开元数据

        ///// <summary>
        ///// 保存定硫仪数据
        ///// </summary>
        ///// <param name="output"></param>
        ///// <returns></returns>
        //public int SaveToSulfurStdAssay_CSKY(Action<string, eOutputType> output)
        //{
        //    int res = 0;
        //    //定硫仪
        //    foreach (V_STAD entity in dcDbersDAO.CSKY_Clims_SelfDber.Entities<V_STAD>(" where TEST_TIME_END>=@TEST_TIME_END and SAMPLE_NO is not null", new { TEST_TIME_END = DateTime.Now.AddDays(-Convert.ToInt32(commonDAO.GetAppletConfigString("化验设备数据读取天数"))).Date }))
        //    {
        //        CmcsSulfurStdAssay item = Dbers.GetInstance().SelfDber.Entity<CmcsSulfurStdAssay>("where PKID=:PKID", new { PKID = entity.ID + entity.APP_CODE });
        //        if (item == null)
        //        {
        //            item = new CmcsSulfurStdAssay();
        //            item.SampleNumber = entity.SAMPLE_NO;
        //            item.FacilityNumber = entity.APP_Name;
        //            item.ContainerWeight = 0;
        //            item.SampleWeight = 0;
        //            item.Stad = entity.STAD;
        //            item.AssayUser = entity.TEST_Man;
        //            item.AssayTime = entity.PF_DATE;
        //            item.OrderNumber = 0;
        //            item.IsEffective = 0;
        //            item.PKID = entity.ID + entity.APP_CODE;
        //            item.DataType = "有效数据";
        //            res += Dbers.GetInstance().SelfDber.Insert<CmcsSulfurStdAssay>(item);
        //        }
        //        else
        //        {
        //            item.SampleNumber = entity.SAMPLE_NO;
        //            item.FacilityNumber = entity.APP_Name;
        //            item.ContainerWeight = 0;
        //            item.SampleWeight = 0;
        //            item.Stad = entity.STAD;
        //            item.AssayUser = entity.TEST_Man;
        //            item.AssayTime = entity.PF_DATE;
        //            item.OrderNumber = 0;
        //            item.DataType = "有效数据";
        //            res += Dbers.GetInstance().SelfDber.Update<CmcsSulfurStdAssay>(item);
        //        }
        //    }
        //    output(string.Format("同步开元定硫数据 {0} 条", res), eOutputType.Important);
        //    return res;
        //}

        ///// <summary>
        ///// 保存量热仪数据
        ///// </summary>
        ///// <param name="output"></param>
        ///// <returns></returns>
        //public int SaveToHeatAssay_CSKY(Action<string, eOutputType> output)
        //{
        //    int res = 0;
        //    //量热仪
        //    foreach (V_QBAD entity in dcDbersDAO.CSKY_Clims_SelfDber.Entities<V_QBAD>(" where TEST_TIME_END>=@TEST_TIME_END and SAMPLE_NO is not null", new { TEST_TIME_END = DateTime.Now.AddDays(-Convert.ToInt32(commonDAO.GetAppletConfigString("化验设备数据读取天数"))).Date }))
        //    {
        //        CmcsHeatStdAssay item = Dbers.GetInstance().SelfDber.Entity<CmcsHeatStdAssay>("where PKID=:PKID", new { PKID = entity.ID + entity.APP_CODE });
        //        if (item == null)
        //        {
        //            item = new CmcsHeatStdAssay();
        //            item.SampleNumber = entity.SAMPLE_NO;
        //            item.FacilityNumber = entity.APP_Name;
        //            item.ContainerWeight = 0;
        //            item.SampleWeight = entity.SAMPLE_WEIGHT;
        //            item.Qbad = entity.QBAD;
        //            item.AssayUser = entity.TEST_MAN;
        //            item.AssayTime = entity.PF_DATE;
        //            item.IsEffective = 0;
        //            item.PKID = entity.ID + entity.APP_CODE;
        //            item.DataType = "有效数据";
        //            res += Dbers.GetInstance().SelfDber.Insert<CmcsHeatStdAssay>(item);
        //        }
        //        else
        //        {
        //            item.SampleNumber = entity.SAMPLE_NO;
        //            item.FacilityNumber = entity.APP_Name;
        //            item.ContainerWeight = 0;
        //            item.SampleWeight = entity.SAMPLE_WEIGHT;
        //            item.Qbad = entity.QBAD;
        //            item.AssayUser = entity.TEST_MAN;
        //            item.AssayTime = entity.PF_DATE;
        //            item.DataType = "有效数据";
        //            res += Dbers.GetInstance().SelfDber.Update<CmcsHeatStdAssay>(item);
        //        }
        //    }
        //    output(string.Format("同步开元量热数据 {0} 条", res), eOutputType.Important);
        //    return res;
        //}

        ///// <summary>
        ///// 保存水分仪数据
        ///// </summary>
        ///// <param name="output"></param>
        ///// <returns></returns>
        //public int SaveToMoistureAssay_CSKY(Action<string, eOutputType> output)
        //{
        //    int res = 0;
        //    foreach (V_MT entity in dcDbersDAO.CSKY_Clims_SelfDber.Entities<V_MT>(" where TEST_TIME_END>=@TEST_TIME_END and SAMPLE_NO is not null", new { TEST_TIME_END = DateTime.Now.AddDays(-Convert.ToInt32(commonDAO.GetAppletConfigString("化验设备数据读取天数"))).Date }))
        //    {
        //        CmcsMoistureStdAssay item = Dbers.GetInstance().SelfDber.Entity<CmcsMoistureStdAssay>("where PKID=:PKID", new { PKID = entity.ID + entity.APP_CODE });
        //        if (item == null)
        //        {
        //            item = new CmcsMoistureStdAssay();
        //            item.SampleNumber = entity.SAMPLE_NO;
        //            item.FacilityNumber = entity.APP_Name;
        //            item.ContainerWeight = 0;
        //            item.SampleWeight = entity.SAMPLE_WEIGHT;
        //            item.ContainerWeight = entity.C_WEIGHT;
        //            item.WaterPer = entity.MT;
        //            item.AssayUser = entity.TEST_MAN;
        //            item.IsEffective = 0;
        //            item.PKID = entity.ID + entity.APP_CODE;
        //            item.AssayTime = entity.TEST_TIME_START;
        //            item.WaterType = "全水分";
        //            item.DataType = "有效数据";
        //            res += Dbers.GetInstance().SelfDber.Insert<CmcsMoistureStdAssay>(item);
        //        }
        //        else
        //        {
        //            item.SampleNumber = entity.SAMPLE_NO;
        //            item.FacilityNumber = entity.APP_Name;
        //            item.ContainerWeight = 0;
        //            item.SampleWeight = entity.SAMPLE_WEIGHT;
        //            item.ContainerWeight = entity.C_WEIGHT;
        //            item.WaterPer = entity.MT;
        //            item.AssayUser = entity.TEST_MAN;
        //            item.IsEffective = 0;
        //            item.AssayTime = entity.TEST_TIME_START;
        //            item.WaterType = "全水分";
        //            item.DataType = "有效数据";
        //            res += Dbers.GetInstance().SelfDber.Update<CmcsMoistureStdAssay>(item);
        //        }
        //    }
        //    output(string.Format("同步开元全水数据 {0} 条", res), eOutputType.Important);
        //    res = 0;
        //    foreach (V_Mad entity in dcDbersDAO.CSKY_Clims_SelfDber.Entities<V_Mad>(" where TEST_TIME_END>=@TEST_TIME_END and SAMPLE_NO is not null", new { TEST_TIME_END = DateTime.Now.AddDays(-Convert.ToInt32(commonDAO.GetAppletConfigString("化验设备数据读取天数"))).Date }))
        //    {
        //        CmcsMoistureStdAssay item = Dbers.GetInstance().SelfDber.Entity<CmcsMoistureStdAssay>("where PKID=:PKID", new { PKID = entity.ID + entity.APP_CODE });
        //        if (item == null)
        //        {
        //            item = new CmcsMoistureStdAssay();
        //            item.SampleNumber = entity.SAMPLE_NO;
        //            item.FacilityNumber = entity.APP_Name;
        //            item.ContainerWeight = 0;
        //            item.SampleWeight = entity.SAMPLE_WEIGHT;
        //            item.WaterPer = entity.MAD;
        //            item.AssayUser = entity.TEST_MAN;
        //            item.IsEffective = 0;
        //            item.PKID = entity.ID + entity.APP_CODE;
        //            item.AssayTime = entity.TEST_TIME_START;
        //            item.WaterType = "内水";
        //            item.DataType = "有效数据";
        //            res += Dbers.GetInstance().SelfDber.Insert<CmcsMoistureStdAssay>(item);
        //        }
        //        else
        //        {
        //            item.SampleNumber = entity.SAMPLE_NO;
        //            item.FacilityNumber = entity.APP_Name;
        //            item.ContainerWeight = 0;
        //            item.SampleWeight = entity.SAMPLE_WEIGHT;
        //            item.WaterPer = entity.MAD;
        //            item.AssayUser = entity.TEST_MAN;
        //            item.IsEffective = 0;
        //            item.AssayTime = entity.TEST_TIME_START;
        //            item.WaterType = "内水";
        //            item.DataType = "有效数据";
        //            res += Dbers.GetInstance().SelfDber.Update<CmcsMoistureStdAssay>(item);
        //        }
        //    }
        //    output(string.Format("同步开元内水数据 {0} 条", res), eOutputType.Important);

        //    return res;
        //}

        ///// <summary>
        ///// 保存工分仪数据
        ///// </summary>
        ///// <param name="output"></param>
        ///// <returns></returns>
        //public int SaveToProximateAssay_SCKY(Action<string, eOutputType> output)
        //{
        //    int res = 0;
        //    foreach (V_Gyfx entity in dcDbersDAO.CSKY_Clims_SelfDber.Entities<V_Gyfx>(" where mTEST_TIME_END>=@TEST_TIME_END and mSAMPLE_NO is not null", new { TEST_TIME_END = DateTime.Now.AddDays(-Convert.ToInt32(commonDAO.GetAppletConfigString("化验设备数据读取天数"))).Date }))
        //    {
        //        CmcsProximateStdAssay item = Dbers.GetInstance().SelfDber.Entity<CmcsProximateStdAssay>("where PKID=:PKID", new { PKID = entity.mID + entity.mAPP_CODE });
        //        if (item == null)
        //        {
        //            item = new CmcsProximateStdAssay();
        //            item.SampleNumber = entity.mSAMPLE_NO;
        //            item.FacilityNumber = entity.mAPP_Name;
        //            item.ContainerWeight = entity.mM_WEIGHT;
        //            item.SampleWeight = entity.mSAMPLE_WEIGHT;
        //            item.Mad = entity.Mad;
        //            item.Vad = entity.Vad;
        //            item.Aad = entity.Aad;
        //            item.AssayUser = entity.mTEST_MAN;
        //            item.IsEffective = 0;
        //            item.PKID = entity.mID + entity.mAPP_CODE;
        //            item.AssayTime = entity.mTEST_TIME_START;
        //            res += Dbers.GetInstance().SelfDber.Insert<CmcsProximateStdAssay>(item);
        //        }
        //        else
        //        {
        //            item.SampleNumber = entity.mSAMPLE_NO;
        //            item.FacilityNumber = entity.mAPP_Name;
        //            item.ContainerWeight = entity.mM_WEIGHT;
        //            item.SampleWeight = entity.mSAMPLE_WEIGHT;
        //            item.Mad = entity.Mad;
        //            item.Vad = entity.Vad;
        //            item.Aad = entity.Aad;
        //            item.AssayUser = entity.mTEST_MAN;
        //            item.IsEffective = 0;
        //            item.AssayTime = entity.mTEST_TIME_START;
        //            res += Dbers.GetInstance().SelfDber.Update<CmcsProximateStdAssay>(item);
        //        }
        //    }
        //    output(string.Format("同步开元工分仪数据 {0} 条", res), eOutputType.Important);
        //    return res;
        //}

        ///// <summary>
        ///// 保存灰熔融数据
        ///// </summary>
        ///// <param name="output"></param>
        ///// <returns></returns>
        //public int SaveToAshAssay_SCKY(Action<string, eOutputType> output)
        //{
        //    int res = 0;
        //    foreach (V_FCA entity in dcDbersDAO.CSKY_Clims_SelfDber.Entities<V_FCA>(" where TEST_TIME_END>=@TEST_TIME_END and SAMPLE_NO is not null", new { TEST_TIME_END = DateTime.Now.AddDays(-Convert.ToInt32(commonDAO.GetAppletConfigString("化验设备数据读取天数"))).Date }))
        //    {
        //        CmcsAshStdAssay item = Dbers.GetInstance().SelfDber.Entity<CmcsAshStdAssay>("where PKID=:PKID", new { PKID = entity.ID + entity.APP_CODE });
        //        if (item == null)
        //        {
        //            item = new CmcsAshStdAssay();
        //            item.SampleNumber = entity.SAMPLE_NO;
        //            item.FacilityNumber = entity.APP_Name;
        //            item.ContainerWeight = 0;
        //            item.SampleWeight = entity.SAMPLE_WEIGHT;
        //            item.DT = entity.DT;
        //            item.FT = entity.FT;
        //            item.ST = entity.ST;
        //            item.HT = entity.HT;
        //            item.AssayUser = "";
        //            item.IsEffective = 0;
        //            item.PKID = entity.ID + entity.APP_CODE;
        //            item.AssayTime = entity.TEST_TIME_START;
        //            item.DataType = "有效数据";
        //            res += Dbers.GetInstance().SelfDber.Insert<CmcsAshStdAssay>(item);
        //        }
        //        else
        //        {
        //            item.SampleNumber = entity.SAMPLE_NO;
        //            item.FacilityNumber = entity.APP_Name;
        //            item.ContainerWeight = 0;
        //            item.SampleWeight = entity.SAMPLE_WEIGHT;
        //            item.DT = entity.DT;
        //            item.FT = entity.FT;
        //            item.ST = entity.ST;
        //            item.HT = entity.HT;
        //            item.AssayUser = "";
        //            item.IsEffective = 0;
        //            item.AssayTime = entity.TEST_TIME_START;
        //            item.DataType = "有效数据";
        //            res += Dbers.GetInstance().SelfDber.Update<CmcsAshStdAssay>(item);
        //        }
        //    }
        //    output(string.Format("同步开元灰熔融数据 {0} 条", res), eOutputType.Important);
        //    return res;
        //}

        ///// <summary>
        ///// 保存碳氢仪数据
        ///// </summary>
        ///// <param name="output"></param>
        ///// <returns></returns>
        //public int SaveToHadAssay_SCKY(Action<string, eOutputType> output)
        //{
        //    int res = 0;
        //    foreach (V_Had entity in dcDbersDAO.CSKY_Clims_SelfDber.Entities<V_Had>(" where TEST_TIME_END>=@TEST_TIME_END and SAMPLE_NO is not null", new { TEST_TIME_END = DateTime.Now.AddDays(-Convert.ToInt32(commonDAO.GetAppletConfigString("化验设备数据读取天数"))).Date }))
        //    {
        //        CmcsHadStdAssay item = Dbers.GetInstance().SelfDber.Entity<CmcsHadStdAssay>("where PKID=:PKID", new { PKID = entity.ID + entity.APP_CODE });
        //        if (item == null)
        //        {
        //            item = new CmcsHadStdAssay();
        //            item.SampleNumber = entity.SAMPLE_NO;
        //            item.FacilityNumber = entity.APP_Name;
        //            item.SampleWeight = entity.SAMPLE_WEIGHT;
        //            item.Had = entity.HAD;
        //            item.AssayUser = "";
        //            item.IsEffective = 0;
        //            item.PKID = entity.ID + entity.APP_CODE;
        //            item.AssayTime = entity.TEST_TIME_START;
        //            item.DataType = "有效数据";
        //            res += Dbers.GetInstance().SelfDber.Insert<CmcsHadStdAssay>(item);
        //        }
        //        else
        //        {
        //            item.SampleNumber = entity.SAMPLE_NO;
        //            item.FacilityNumber = entity.APP_Name;
        //            item.SampleWeight = entity.SAMPLE_WEIGHT;
        //            item.Had = entity.HAD;
        //            item.AssayUser = "";
        //            item.IsEffective = 0;
        //            item.PKID = entity.ID + entity.APP_CODE;
        //            item.AssayTime = entity.TEST_TIME_START;
        //            item.DataType = "有效数据";
        //            res += Dbers.GetInstance().SelfDber.Update<CmcsHadStdAssay>(item);
        //        }
        //    }
        //    output(string.Format("同步开元碳氢仪数据 {0} 条", res), eOutputType.Important);
        //    return res;
        //}
        #endregion

        #region 读取化验设备运行状态
        public void ReadHyMachine(Action<string, eOutputType> output)
        {
            int res = 0;
            string path = commonDAO.GetCommonAppletConfigString("化验设备状态XML路径");
            #region 读取XML
            //将XML文件加载进来
            XDocument document = XDocument.Load(path);
            //获取到XML的根元素进行操作
            XElement ele = document.Root;
            //XElement ele = root.Element("CommonAppConfig");
            //获取标签的值
            XElement shuxing = ele.Element("LRY1");
            res += commonDAO.SetSignalDataValue("化验室网络管理", "量热仪1_运行状态", shuxing.Value) ? 1 : 0;
            shuxing = ele.Element("LRY2");
            res += commonDAO.SetSignalDataValue("化验室网络管理", "量热仪2_运行状态", shuxing.Value) ? 1 : 0;
            shuxing = ele.Element("LRY3");
            res += commonDAO.SetSignalDataValue("化验室网络管理", "量热仪3_运行状态", shuxing.Value) ? 1 : 0;
            shuxing = ele.Element("LRY4");
            res += commonDAO.SetSignalDataValue("化验室网络管理", "量热仪4_运行状态", shuxing.Value) ? 1 : 0;
            shuxing = ele.Element("DLY1");
            res += commonDAO.SetSignalDataValue("化验室网络管理", "定硫仪1_运行状态", shuxing.Value) ? 1 : 0;
            shuxing = ele.Element("DLY2");
            res += commonDAO.SetSignalDataValue("化验室网络管理", "定硫仪2_运行状态", shuxing.Value) ? 1 : 0;
            shuxing = ele.Element("DLY3");
            res += commonDAO.SetSignalDataValue("化验室网络管理", "定硫仪3_运行状态", shuxing.Value) ? 1 : 0;
            shuxing = ele.Element("GFY1");
            res += commonDAO.SetSignalDataValue("化验室网络管理", "工分仪1_运行状态", shuxing.Value) ? 1 : 0;
            shuxing = ele.Element("GFY2");
            res += commonDAO.SetSignalDataValue("化验室网络管理", "工分仪2_运行状态", shuxing.Value) ? 1 : 0;
            shuxing = ele.Element("GFY3");
            res += commonDAO.SetSignalDataValue("化验室网络管理", "工分仪3_运行状态", shuxing.Value) ? 1 : 0;
            shuxing = ele.Element("GFY4");
            res += commonDAO.SetSignalDataValue("化验室网络管理", "工分仪4_运行状态", shuxing.Value) ? 1 : 0;
            shuxing = ele.Element("SFY1");
            res += commonDAO.SetSignalDataValue("化验室网络管理", "水分仪1_运行状态", shuxing.Value) ? 1 : 0;
            shuxing = ele.Element("SFY2");
            res += commonDAO.SetSignalDataValue("化验室网络管理", "水分仪2_运行状态", shuxing.Value) ? 1 : 0;
            shuxing = ele.Element("SFY3");
            res += commonDAO.SetSignalDataValue("化验室网络管理", "水分仪3_运行状态", shuxing.Value) ? 1 : 0;
            shuxing = ele.Element("HRY1");
            res += commonDAO.SetSignalDataValue("化验室网络管理", "灰融仪1_运行状态", shuxing.Value) ? 1 : 0;
            shuxing = ele.Element("HRY2");
            res += commonDAO.SetSignalDataValue("化验室网络管理", "灰融仪2_运行状态", shuxing.Value) ? 1 : 0;
            shuxing = ele.Element("TQY1");
            res += commonDAO.SetSignalDataValue("化验室网络管理", "碳氢仪1_运行状态", shuxing.Value) ? 1 : 0;
            shuxing = ele.Element("TQY2");
            res += commonDAO.SetSignalDataValue("化验室网络管理", "碳氢仪2_运行状态", shuxing.Value) ? 1 : 0;
            output("同步设备状态" + res.ToString() + "条", eOutputType.Normal);
            #endregion
        }
        #endregion
    }
}
