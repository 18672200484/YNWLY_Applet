using System;
using System.Collections.Generic;
using CMCS.Common;
using CMCS.Common.DAO;
using CMCS.Common.Entities.CarTransport;
using CMCS.Common.Entities.Fuel;
using CMCS.Common.Entities.Inf;
using CMCS.Common.Enums;
using CMCS.DapperDber.Dbs.SqlServerDb;
using CMCS.DumblyConcealer.Enums;
using CMCS.DumblyConcealer.Tasks.CarJXSampler.Entities;
using CMCS.DumblyConcealer.Tasks.CarJXSampler.Enums;
using CMCS.DumblyConcealer.Tasks.AutoMaker.Entities;

namespace CMCS.DumblyConcealer.Tasks.CarJXSampler
{
    /// <summary>
    /// 汽车机械采样机接口业务
    /// </summary>
    public class EquCarJXSamplerDAO
    {
        /// <summary>
        /// EquCarJXSamplerDAO
        /// </summary>
        /// <param name="machineCode">设备编码</param>
        /// <param name="equDber">第三方数据库访问对象</param>
        public EquCarJXSamplerDAO(string machineCode, SqlServerDapperDber equDber)
        {
            this.MachineCode = machineCode;
            this.EquDber = equDber;
        }

        CommonDAO commonDAO = CommonDAO.GetInstance();

        /// <summary>
        /// 第三方数据库访问对象
        /// </summary>
        public SqlServerDapperDber EquDber;
        /// <summary>
        /// 设备编码
        /// </summary>
        string MachineCode;

        #region 数据转换方法

        /// <summary>
        /// 开元编码转换为标准设备编码
        /// </summary>
        /// <param name="machine"></param>
        /// <returns></returns>
        public string KYMachineToData(string machine)
        {
            if (machine == GlobalVars.MachineCode_QCJXCYJ_KY_1)
                return GlobalVars.MachineCode_QC_JxSampler_1;
            else if (machine == GlobalVars.MachineCode_QCJXCYJ_KY_2)
                return GlobalVars.MachineCode_QC_JxSampler_1;
            return string.Empty;
        }

        /// <summary>
        /// 标准设备编码转换为开元编码
        /// </summary>
        /// <param name="machine"></param>
        /// <returns></returns>
        public string DataToKYMachine(string machine)
        {
            if (machine == GlobalVars.MachineCode_QC_JxSampler_1)
                return GlobalVars.MachineCode_QCJXCYJ_KY_1;
            else if (machine == GlobalVars.MachineCode_QC_JxSampler_2)
                return GlobalVars.MachineCode_QCJXCYJ_KY_2;
            return string.Empty;
        }

        #endregion

        /// <summary>
        /// 同步实时信号到集中管控
        /// </summary>
        /// <param name="output"></param>
        /// <param name="MachineCode">设备编码</param>
        /// <returns></returns>
        public int SyncSignal(Action<string, eOutputType> output)
        {
            int res = 0;

            //采样机基本状态
            KY_CYJ_Status entity = this.EquDber.Entity<KY_CYJ_Status>(" order by CYJ_Machine");
            if (entity != null)
            {
                res += commonDAO.SetSignalDataValue(this.MachineCode, "超声波状态", entity.CSStatus == 1 ? "阻挡" : "正常") ? 1 : 0;
                res += commonDAO.SetSignalDataValue(this.MachineCode, "小车超声波状态", entity.XCSSStatus == 1 ? "阻挡" : "正常") ? 1 : 0;
                res += commonDAO.SetSignalDataValue(this.MachineCode, "采样头", entity.CYTStatus.ToString()) ? 1 : 0;
                res += commonDAO.SetSignalDataValue(this.MachineCode, "道闸2升杆", entity.DZStatus.ToString()) ? 1 : 0;
                res += commonDAO.SetSignalDataValue(this.MachineCode, eSignalDataName.设备状态.ToString(), entity.CYJStatus == 1 ? eEquInfSamplerSystemStatus.就绪待机.ToString() : eEquInfSamplerSystemStatus.发生故障.ToString()) ? 1 : 0;
            }
            //采样机状态
            foreach (KY_CYJ_OutRun item in this.EquDber.Entities<KY_CYJ_OutRun>("where CYJ_Machine=@CYJ_Machine", new { CYJ_Machine = DataToKYMachine(this.MachineCode) }))
            {
                if (item.CY_State == (int)eEquInfSamplerSystemStatus.就绪待机 || item.CY_State == (int)eEquInfSamplerSystemStatus.采样完成)
                    res += commonDAO.SetSignalDataValue(this.MachineCode, eSignalDataName.设备状态.ToString(), eEquInfSystemStatus.就绪待机.ToString()) ? 1 : 0;
                else if (item.CY_State == (int)eEquInfSamplerSystemStatus.正在运行 || item.CY_State == (int)eEquInfSamplerSystemStatus.正在卸样)
                    res += commonDAO.SetSignalDataValue(this.MachineCode, eSignalDataName.设备状态.ToString(), eEquInfSystemStatus.正在运行.ToString()) ? 1 : 0;
                else if (item.CY_State == (int)eEquInfSamplerSystemStatus.发生故障)
                    res += commonDAO.SetSignalDataValue(this.MachineCode, eSignalDataName.设备状态.ToString(), eEquInfSystemStatus.发生故障.ToString()) ? 1 : 0;
                else if (item.CY_State == (int)eEquInfSamplerSystemStatus.急停 || item.CY_State == (int)eEquInfSamplerSystemStatus.停止)
                    res += commonDAO.SetSignalDataValue(this.MachineCode, eSignalDataName.设备状态.ToString(), eEquInfSystemStatus.系统停止.ToString()) ? 1 : 0;

                //res += commonDAO.SetSignalDataValue(this.MachineCode, eSignalDataName.设备状态.ToString(), ((eEquInfSamplerSystemStatus)item.CY_State).ToString()) ? 1 : 0;
            }

            //卸料机状态

            IList<KY_CYJ_Down> down_list = this.EquDber.Entities<KY_CYJ_Down>("where CYJ_Machine=@CYJ_Machine and XL_Start!=0", new { CYJ_Machine = DataToKYMachine(this.MachineCode) });
            if (down_list != null && down_list.Count > 0)
            {
                foreach (KY_CYJ_Down item in down_list)
                {
                    res += commonDAO.SetSignalDataValue(this.MachineCode + GlobalVars.XLJ_Machine, eSignalDataName.设备状态.ToString(), ((eUnLoadState)item.XL_Start).ToString()) ? 1 : 0;
                }
            }
            else
            {
                res += commonDAO.SetSignalDataValue(this.MachineCode + GlobalVars.XLJ_Machine, eSignalDataName.设备状态.ToString(), eUnLoadState.就绪待机.ToString()) ? 1 : 0;
            }
            foreach (EquQCJXCYJSignal item in this.EquDber.Entities<EquQCJXCYJSignal>("where DataStatus=0 order by LastUpdateTime desc"))
            {
                res += commonDAO.SetSignalDataValue(this.MachineCode, item.DeviceName, item.DeviceStatus) ? 1 : 0;
                item.DataStatus = 1;
                this.EquDber.Update(item);
            }
            output(string.Format("同步实时信号 {0} 条-{1}", res, this.MachineCode), eOutputType.Normal);

            return res;
        }

        /// <summary>
        /// 同步集样罐信息到集中管控
        /// </summary>
        /// <param name="output"></param> 
        /// <returns></returns>
        public void SyncBarrel(Action<string, eOutputType> output)
        {
            int res = 0;

            foreach (KY_CYJ_BarrelStatus item in this.EquDber.Entities<KY_CYJ_BarrelStatus>(" where CYJ_Machine=@CYJ_Machine and DataFlag=0 order by Barrel_Code", new { CYJ_Machine = DataToKYMachine(this.MachineCode) }))
            {
                if (item.Barrel_Code != "9")
                {
                    InfEquInfSampleBarrel infSampleBarrel = new InfEquInfSampleBarrel();
                    infSampleBarrel.BarrelNumber = item.Barrel_Code;
                    infSampleBarrel.BarrelStatus = item.Down_Full == 0 ? "未满" : "已满";
                    infSampleBarrel.MachineCode = this.MachineCode;
                    infSampleBarrel.InterfaceType = commonDAO.GetMachineInterfaceTypeByCode(this.MachineCode);
                    infSampleBarrel.SampleCode = !string.IsNullOrEmpty(item.CY_Code) ? item.CY_Code.Trim() : "";
                    if (string.IsNullOrEmpty(item.CY_Code)) infSampleBarrel.BarrelStatus = "";
                    infSampleBarrel.InFactoryBatchId = !string.IsNullOrEmpty(item.CY_Code) ? commonDAO.GetBatchIdByRCSamplingCode(item.CY_Code) : "";
                    infSampleBarrel.SampleCount = item.Down_Cont;
                    infSampleBarrel.UpdateTime = item.EditDate;
                    infSampleBarrel.BarrelType = "底卸式";
                    if (commonDAO.SaveEquInfSampleBarrel(infSampleBarrel))
                    {
                        item.DataFlag = 1;
                        this.EquDber.Update(item);
                        res++;
                    }
                }
                else
                {
                    //当前进料桶
                    InfEquInfSampleBarrel JLBarrel = commonDAO.SelfDber.Entity<InfEquInfSampleBarrel>("where MachineCode=:MachineCode and BarrelNumber=:BarrelNumber order by BarrelNumber", new { MachineCode = KYMachineToData(item.CYJ_Machine), BarrelNumber = item.JL_Code });
                    if (JLBarrel != null)
                    {
                        JLBarrel.IsCurrent = 1;
                        commonDAO.SelfDber.Update(JLBarrel);
                    }
                    //当前卸料桶
                }
            }

            output(string.Format("同步集样罐记录 {0} 条-{1}", res, this.MachineCode), eOutputType.Normal);
        }

        /// <summary>
        /// 同步故障信息到集中管控
        /// </summary>
        /// <param name="output"></param>
        /// <returns></returns>
        public void SyncQCJXCYJError(Action<string, eOutputType> output)
        {
            int res = 0;

            //故障信息分为：报警信息  报警恢复信息 
            foreach (Alarm item in this.EquDber.Entities<Alarm>(" where Convert(char(10),Convert(datetime,AlarmDate),120)=Convert(char(10),GetDate(),120)"))
            {
                if (item.EventType.Trim() == "报警")
                {
                    if (commonDAO.SaveEquInfHitch(this.MachineCode, Convert.ToDateTime(item.AlarmDate + " " + item.AlarmTime.Substring(0, 8)), string.Format("故障代码:{0},故障值:{1},故障描述:{2}", item.VarName, item.AlarmValue, item.VarComment), (item.AlarmDate + item.AlarmTime).Replace(" ", "")))
                    {
                        res++;
                    }
                }
                //报警恢复信息预留 后期需要再接入
                else
                {

                }
            }

            //foreach (var item in this.EquDber.Entities<KY_CYJ_Log>("where CYJ_Machine=@CYJ_Machine and Msg_Type!=0 ", new { CYJ_Machine = DataToKYMachine(this.MachineCode) }))
            //{
            //    if (commonDAO.SaveEquInfHitch(this.MachineCode, item.Creat_Time, string.Format("故障代码:{0},故障值:{1},故障描述:{2}", item.Machine_Code, item.Msg_Code, item.Msg_Content)))
            //    {
            //        this.EquDber.Delete<KY_CYJ_Log>(item.Id.ToString());
            //        res++;
            //    }
            //}
            output(string.Format("同步故障信息记录 {0} 条-{1}", res, this.MachineCode), eOutputType.Normal);
        }

        /// <summary>
        /// 计算拉筋数量
        /// </summary>
        /// <param name="autotruck"></param>
        /// <returns></returns>
        public int SumObstacle(CmcsAutotruck autotruck)
        {
            if (autotruck.LeftObstacle1 > 0)
            {
                if (autotruck.LeftObstacle2 > 0)
                {
                    if (autotruck.LeftObstacle3 > 0)
                    {
                        if (autotruck.LeftObstacle4 > 0)
                        {
                            if (autotruck.LeftObstacle5 > 0)
                            {
                                if (autotruck.LeftObstacle6 > 0)
                                    return 6;
                                return 5;
                            }
                            return 4;
                        }
                        return 3;
                    }
                    return 2;
                }
                return 1;
            }
            return 0;
        }

        /// <summary>
        /// 同步采样命令
        /// </summary>
        /// <param name="output"></param>
        public void SyncSampleCmd(Action<string, eOutputType> output)
        {
            string CarNumber = string.Empty;
            int res = 0;

            // 集中管控 > 第三方 
            foreach (InfQCJXCYSampleCMD entity in CarSamplerDAO.GetInstance().GetWaitForSyncSampleCMD(this.MachineCode))
            {
                bool isSuccess = false;
                CmcsAutotruck AutoTruck = commonDAO.SelfDber.Entity<CmcsAutotruck>(" where CarNumber=:CarNumber", new { CarNumber = entity.CarNumber });
                if (AutoTruck != null)
                {
                    CarNumber = AutoTruck.CarNumber;
                    //先同步车辆信息
                    KY_CarInfo car = this.EquDber.Get<KY_CarInfo>(entity.CarNumber);
                    if (car == null)
                    {
                        car = new KY_CarInfo();
                        car.Car_Number = AutoTruck.CarNumber;
                        car.Car_Long = AutoTruck.CarriageLength;
                        car.Car_Width = AutoTruck.CarriageWidth;
                        car.Car_Height = AutoTruck.CarriageBottomToFloor;
                        car.LJ_Sum = SumObstacle(AutoTruck);
                        car.LJ_0 = 0;
                        car.LJ_1 = AutoTruck.LeftObstacle1;
                        car.LJ_2 = AutoTruck.LeftObstacle2;
                        car.LJ_3 = AutoTruck.LeftObstacle3;
                        car.LJ_4 = AutoTruck.LeftObstacle4;
                        car.LJ_5 = AutoTruck.LeftObstacle5;
                        car.LJ_6 = AutoTruck.LeftObstacle6;
                        car.UserName = AutoTruck.Driver;
                        car.EditDate = DateTime.Now;
                        this.EquDber.Insert(car);
                    }
                    else
                    {
                        car.Car_Number = AutoTruck.CarNumber;
                        car.Car_Long = AutoTruck.CarriageLength;
                        car.Car_Width = AutoTruck.CarriageWidth;
                        car.Car_Height = AutoTruck.CarriageBottomToFloor;
                        car.LJ_Sum = SumObstacle(AutoTruck);
                        car.LJ_0 = 0;
                        car.LJ_1 = AutoTruck.LeftObstacle1;
                        car.LJ_2 = AutoTruck.LeftObstacle2;
                        car.LJ_3 = AutoTruck.LeftObstacle3;
                        car.LJ_4 = AutoTruck.LeftObstacle4;
                        car.LJ_5 = AutoTruck.LeftObstacle5;
                        car.LJ_6 = AutoTruck.LeftObstacle6;
                        car.UserName = AutoTruck.Driver;
                        car.EditDate = DateTime.Now;
                        this.EquDber.Update(car);
                    }

                    //同步采样命令
                    KY_CYJ_OutRun outrun = this.EquDber.Entity<KY_CYJ_OutRun>(" where CYJ_Machine=@CYJ_Machine", new { CYJ_Machine = DataToKYMachine(this.MachineCode) });
                    if (outrun == null)
                    {
                        outrun = new KY_CYJ_OutRun();
                        outrun.CYJ_Machine = DataToKYMachine(this.MachineCode);
                        outrun.Car_Number = AutoTruck.CarNumber;
                        //outrun.Rcd_Code = commonDAO.SelfDber.Entity<CmcsBuyFuelTransport>(" where CarNumber=:CarNumber", new { CarNumber = AutoTruck.CarNumber }).SerialNumber;
                        outrun.CY_Code = entity.SampleCode;
                        outrun.CY_Type = "机械采样";
                        outrun.CY_Point = entity.PointCount;
                        outrun.Batch_Number = entity.InFactoryBatchId;
                        outrun.CY_Control = (int)eSamplingControl.采样指令;
                        outrun.CY_State = (int)eSamplingState.等待采样;
                        outrun.Send_Time = DateTime.Now;
                        //outrun.Clean_Flag = 0;
                        isSuccess = this.EquDber.Insert(outrun) > 0;
                    }
                    else
                    {
                        //outrun.CYJ_Machine = DataToKYMachine(this.MachineCode);
                        outrun.Car_Number = AutoTruck.CarNumber;
                        //outrun.Rcd_Code = commonDAO.SelfDber.Entity<CmcsBuyFuelTransport>(" where CarNumber=:CarNumber", new { CarNumber = AutoTruck.CarNumber }).SerialNumber;
                        outrun.CY_Code = entity.SampleCode;
                        outrun.CY_Type = "机械采样";
                        outrun.CY_Point = entity.PointCount;
                        outrun.Batch_Number = entity.InFactoryBatchId;
                        outrun.CY_Control = (int)eSamplingControl.采样指令;
                        outrun.CY_State = (int)eSamplingState.等待采样;
                        outrun.Send_Time = DateTime.Now;
                        //outrun.Clean_Flag = 0;
                        isSuccess = this.EquDber.Update(outrun) > 0;
                    }
                }

                if (isSuccess)
                {
                    entity.SyncFlag = 1;
                    Dbers.GetInstance().SelfDber.Update(entity);

                    res++;
                }
            }
            output(string.Format("同步采样计划 {0} 条（集中管控 > 第三方）-{1}", res, this.MachineCode), eOutputType.Normal);


            res = 0;
            // 第三方 > 集中管控 同步采样结果
            foreach (KY_CYJ_Record entity in this.EquDber.Entities<KY_CYJ_Record>("where CYJ_Machine=@CYJ_Machine and datediff(dd,Begin_Date,getdate())=0", new { CYJ_Machine = DataToKYMachine(this.MachineCode) }))
            {
                InfQCJXCYSampleCMD samplecmdInf = Dbers.GetInstance().SelfDber.Entity<InfQCJXCYSampleCMD>("where MachineCode=:MachineCode and SampleCode=:SampleCode and CarNumber=:CarNumber and DataFlag=0 order by createdate desc", new { MachineCode = this.MachineCode, SampleCode = entity.CY_Code, CarNumber = entity.Car_Number });
                if (samplecmdInf == null) continue;

                samplecmdInf.StartTime = entity.Begin_Date;
                samplecmdInf.EndTime = entity.End_Date;
                samplecmdInf.SampleUser = entity.CY_User;

                if (Dbers.GetInstance().SelfDber.Update(samplecmdInf) > 0)
                {
                    res++;
                }
            }
            //采样结果 采样完成后采样指令为0 采样状态为采样完成  开元接收到采样命令后会把采样状态改为0 等待采样
            foreach (KY_CYJ_OutRun entity in this.EquDber.Entities<KY_CYJ_OutRun>(" where CYJ_Machine=@CYJ_Machine and CY_Control=0 and CY_State!=0 and Convert(varchar(10),SEND_TIME,120)>=convert(varchar(10),GETDATE(),120)", new { CYJ_Machine = DataToKYMachine(this.MachineCode) }))
            {
                InfQCJXCYSampleCMD samplecmdInf = Dbers.GetInstance().SelfDber.Entity<InfQCJXCYSampleCMD>("where MachineCode=:MachineCode and SampleCode=:SampleCode and CarNumber=:CarNumber and DataFlag=0 order by createdate desc", new { MachineCode = this.MachineCode, SampleCode = entity.CY_Code, CarNumber = entity.Car_Number });
                if (samplecmdInf == null) continue;
                if (entity.CY_State == 2)//采样完成
                {
                    samplecmdInf.ResultCode = eEquInfCmdResultCode.成功.ToString();
                }
                else if (entity.CY_State == 3)
                    samplecmdInf.ResultCode = eEquInfCmdResultCode.失败.ToString();
                samplecmdInf.DataFlag = 1;
                if (Dbers.GetInstance().SelfDber.Update(samplecmdInf) > 0)
                {
                    res++;
                }
            }
            output(string.Format("同步采样计划 {0} 条（第三方 > 集中管控）-{1}", res, this.MachineCode), eOutputType.Normal);
        }

        /// <summary>
        /// 同步卸样命令
        /// </summary>
        /// <param name="output"></param>
        /// <returns></returns>
        public void SyncJXCYControlUnloadCMD(Action<string, eOutputType> output)
        {
            int res = 0;

            // 集中管控 > 第三方
            foreach (InfQCJXCYUnLoadCMD entity in CarSamplerDAO.GetInstance().GetWaitForSyncJXCYSampleUnloadCmd(MachineCode))
            {
                bool isSuccess = false;

                //同步卸样命令
                KY_CYJ_OutRun outrun = this.EquDber.Entity<KY_CYJ_OutRun>(" where CYJ_Machine=@CYJ_Machine", new { CYJ_Machine = DataToKYMachine(this.MachineCode) });
                if (outrun == null)
                {
                    outrun = new KY_CYJ_OutRun();
                    outrun.CYJ_Machine = DataToKYMachine(this.MachineCode);
                    outrun.CY_Code = entity.SampleCode;
                    outrun.CY_Type = "机械采样";
                    outrun.CY_Control = (int)eSamplingControl.卸料指令;
                    outrun.Send_Time = DateTime.Now;
                    isSuccess = this.EquDber.Insert(outrun) > 0;
                }
                else
                {
                    outrun.CY_Code = entity.SampleCode;
                    outrun.CY_Type = "机械采样";
                    outrun.CY_Control = (int)eSamplingControl.卸料指令;
                    outrun.Send_Time = DateTime.Now;
                    isSuccess = this.EquDber.Update(outrun) > 0;
                }
                string SqlWhere = "where CY_Code=@CY_Code";
                if (entity.UnLoadType != "到制样机")
                    SqlWhere += " and Barrel_Code=@Barrel_Code";
                IList<KY_CYJ_BarrelStatus> barrel_codes = this.EquDber.Entities<KY_CYJ_BarrelStatus>(SqlWhere, new { CY_Code = entity.SampleCode, Barrel_Code = entity.BarrelNumber });
                foreach (var item in barrel_codes)
                {
                    KY_CYJ_Down down = this.EquDber.Entity<KY_CYJ_Down>("where Barrel_Code=@Barrel_Code", new { Barrel_Code = item.Barrel_Code });
                    if (down != null)
                    {
                        down.XL_Start = 1;
                        down.XL_Finish = 0;
                        down.Down_Type = entity.UnLoadType == "到制样机" ? "0" : "1";
                        this.EquDber.Update(down);
                    }
                }
                if (isSuccess)
                {
                    entity.SyncFlag = 1;
                    Dbers.GetInstance().SelfDber.Update(entity);

                    res++;
                }
            }
            output(string.Format("同步卸样命令 {0} 条（集中管控 > 第三方）-{1}", res, this.MachineCode), eOutputType.Normal);
        }

        /// <summary>
        /// 同步卸样操作结果
        /// </summary>
        /// <param name="output"></param>
        public void SyncResult(Action<string, eOutputType> output)
        {
            int res = 0, res2 = 0;
            // 第三方 > 集中管控 卸样结果
            foreach (InfQCJXCYUnLoadCMD item in Dbers.GetInstance().SelfDber.Entities<InfQCJXCYUnLoadCMD>("where MachineCode=:MachineCode and DataFlag=0", new { MachineCode = this.MachineCode }))
            {
                if (item.UnLoadType == "到制样机")
                {
                    IList<KY_CYJ_BarrelStatus> barrelstatus = this.EquDber.Entities<KY_CYJ_BarrelStatus>("where CY_Code=@CY_Code order by Barrel_Code", new { CY_Code = item.SampleCode });
                    IList<KY_CYJ_Down> downlist = new List<KY_CYJ_Down>();
                    foreach (KY_CYJ_BarrelStatus barrel in barrelstatus)
                    {
                        KY_CYJ_Down down = this.EquDber.Entity<KY_CYJ_Down>("where Barrel_Code=@Barrel_Code and XL_Finish=1", new { Barrel_Code = barrel.Barrel_Code });
                        if (down != null)
                            downlist.Add(down);
                    }
                    if (downlist.Count == barrelstatus.Count)
                    {
                        foreach (KY_CYJ_Down entity in downlist)
                        {
                            //历史卸样结果
                            InfQCJXCYJUnloadResult oldUnloadResult = commonDAO.SelfDber.Entity<InfQCJXCYJUnloadResult>("where SampleCode=:SampleCode and BarrelCode=:BarrelCode and UnLoadType=:UnLoadType and DataFlag=0", new { SampleCode = item.SampleCode, UnLoadType = item.UnLoadType, BarrelCode = entity.Barrel_Code });
                            if (oldUnloadResult == null)
                            {
                                // 生成采样桶记录
                                CmcsRCSampleBarrel rCSampleBarrel = new CmcsRCSampleBarrel();

                                rCSampleBarrel.BarrelCode = item.SampleCode;
                                rCSampleBarrel.BarrellingTime = entity.LastDateTime;
                                rCSampleBarrel.SampleCode = item.SampleCode;
                                rCSampleBarrel.SampleMachine = this.MachineCode.Contains("#1") ? "#1汽车机械采样机" : "#2汽车机械采样机";
                                rCSampleBarrel.SampleType = eSamplingType.机械采样.ToString();
                                rCSampleBarrel.SampSecondCode = commonDAO.CreateSampleDetailCode(rCSampleBarrel.SampleCode);
                                rCSampleBarrel.SamplingId = commonDAO.GetSamplingIdBySamplingCode(item.SampleCode);
                                rCSampleBarrel.InFactoryBatchId = commonDAO.GetBatchIdByRCSamplingId(rCSampleBarrel.SamplingId);

                                if (commonDAO.SelfDber.Insert(rCSampleBarrel) > 0)
                                {
                                    if (commonDAO.SelfDber.Insert(new InfQCJXCYJUnloadResult
                                    {
                                        SampleCode = item.SampleCode,
                                        BarrelCode = entity.Barrel_Code,
                                        UnloadTime = entity.LastDateTime,
                                        UnLoadType = item.UnLoadType,
                                        DataFlag = 0
                                    }) > 0)
                                    {
                                        res++;
                                    }
                                }
                            }
                        }
                        item.ResultCode = eEquInfCmdResultCode.成功.ToString();
                        item.DataFlag = 1;
                        if (Dbers.GetInstance().SelfDber.Update(item) > 0)
                        {
                            // 我方已读
                            this.EquDber.Execute(string.Format("update {0} set XL_Start = 0,XL_Finish = 0,Read_Flag = 0 where XL_Finish=1", CMCS.DapperDber.Util.EntityReflectionUtil.GetTableName<KY_CYJ_Down>()));

                            res2++;
                        }
                    }
                }
                else if (item.UnLoadType == "到归批机")
                {
                    KY_CYJ_Down down = this.EquDber.Entity<KY_CYJ_Down>("where Barrel_Code=@Barrel_Code and XL_Finish=1", new { Barrel_Code = item.BarrelNumber });
                    if (down != null)
                    {
                        //历史卸样结果
                        InfQCJXCYJUnloadResult oldUnloadResult = commonDAO.SelfDber.Entity<InfQCJXCYJUnloadResult>("where SampleCode=:SampleCode and BarrelCode=:BarrelCode and UnLoadType=:UnLoadType and DataFlag=0", new { SampleCode = item.SampleCode, UnLoadType = item.UnLoadType, BarrelCode = item.BarrelNumber });
                        if (oldUnloadResult == null)
                        {
                            // 生成采样桶记录
                            CmcsRCSampleBarrel rCSampleBarrel = new CmcsRCSampleBarrel();

                            rCSampleBarrel.BarrelCode = item.SampleCode;
                            rCSampleBarrel.SampSecondCode = commonDAO.CreateSampleDetailCode(item.SampleCode); ;
                            rCSampleBarrel.BarrellingTime = down.LastDateTime;
                            rCSampleBarrel.SampleCode = item.SampleCode;
                            rCSampleBarrel.SampSecondCode = commonDAO.CreateSampleDetailCode(rCSampleBarrel.SampleCode);
                            rCSampleBarrel.SampleMachine = this.MachineCode.Contains("#1") ? "#1汽车机械采样机" : "#2汽车机械采样机";
                            rCSampleBarrel.SampleType = eSamplingType.机械采样.ToString();
                            rCSampleBarrel.SamplingId = commonDAO.GetSamplingIdBySamplingCode(item.SampleCode);
                            rCSampleBarrel.InFactoryBatchId = commonDAO.GetBatchIdByRCSamplingId(rCSampleBarrel.SamplingId);

                            if (commonDAO.SelfDber.Insert(rCSampleBarrel) > 0)
                            {
                                InfQCJXCYJUnloadResult unload = new InfQCJXCYJUnloadResult
                                   {
                                       SampleCode = item.SampleCode,
                                       BarrelCode = down.Barrel_Code,
                                       UnloadTime = down.LastDateTime,
                                       DataFlag = 0
                                   };
                                if (commonDAO.SelfDber.Insert(unload) > 0)
                                {
                                    res++;
                                }
                            }
                        }
                        item.ResultCode = eEquInfCmdResultCode.成功.ToString();
                        item.DataFlag = 1;
                        if (Dbers.GetInstance().SelfDber.Update(item) > 0)
                        {
                            // 我方已读
                            this.EquDber.Execute(string.Format("update {0} set XL_Start = 0,XL_Finish = 0,Read_Flag = 0 where XL_Finish=1 and Barrel_Code='{1}'", CMCS.DapperDber.Util.EntityReflectionUtil.GetTableName<KY_CYJ_Down>(), item.BarrelNumber));

                            res2++;
                        }
                    }
                }
            }

            output(string.Format("同步历史卸样结果 {0} 条（第三方 > 集中管控）", res), eOutputType.Normal);
            output(string.Format("同步卸样结果 {0} 条（第三方 > 集中管控）", res2), eOutputType.Normal);
        }

        /// <summary>
        /// 同步车辆信息
        /// </summary>
        /// <param name="output"></param>
        public void SyncCarInfo(Action<string, eOutputType> output)
        {
            int res = 0;
            foreach (KY_CarInfo item in this.EquDber.Entities<KY_CarInfo>("where LJ_1>0"))
            {
                CmcsAutotruck auto = commonDAO.SelfDber.Entity<CmcsAutotruck>("where CarNumber=:CarNumber and LeftObstacle1=0", new { CarNumber = item.Car_Number });
                if (auto != null)
                {
                    auto.CarriageLength = item.Car_Long;
                    auto.CarriageWidth = item.Car_Width;
                    auto.CarriageBottomToFloor = item.Car_Height;
                    auto.LeftObstacle1 = item.LJ_1;
                    auto.LeftObstacle2 = item.LJ_2;
                    auto.LeftObstacle3 = item.LJ_3;
                    auto.LeftObstacle4 = item.LJ_4;
                    auto.LeftObstacle5 = item.LJ_5;
                    auto.LeftObstacle6 = item.LJ_6;

                    auto.RightObstacle1 = item.LJ_1;
                    auto.RightObstacle2 = item.LJ_2;
                    auto.RightObstacle3 = item.LJ_3;
                    auto.RightObstacle4 = item.LJ_4;
                    auto.RightObstacle5 = item.LJ_5;
                    auto.RightObstacle6 = item.LJ_6;

                    if (commonDAO.SelfDber.Update<CmcsAutotruck>(auto) > 0)
                        res++;
                }
            }
            output(string.Format("同步车辆信息 {0} 条（第三方 > 集中管控）-{1}", res, this.MachineCode), eOutputType.Normal);
        }
    }
}
