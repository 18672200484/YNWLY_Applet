using System;
using System.Collections.Generic;
using System.Linq;
using CMCS.DumblyConcealer.Tasks.AutoCupboard.Entities;
using CMCS.DumblyConcealer.Tasks.AutoCupboard.Enums;
using CMCS.Common;
using CMCS.DumblyConcealer.Enums;
using CMCS.Common.DAO;
using CMCS.Common.Enums;
using CMCS.Common.Entities.AutoCupboard;
using CMCS.DapperDber.Dbs.SqlServerDb;

namespace CMCS.DumblyConcealer.Tasks.AutoCupboard
{
    public class EquAutoCupboardDAO
    {
        /// <summary>
        /// 线程锁
        /// </summary>
        protected object objLock = new object();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="machineCode">设备编码</param>
        /// <param name="equDber">第三方数据库访问对象</param>
        public EquAutoCupboardDAO(string machineCode, SqlServerDapperDber equDber)
        {
            this.MachineCode = machineCode;
            this.EquDber = equDber;
        }
        #region 数据转换方法

        /// <summary>
        /// 开元编码转换为标准设备编码
        /// </summary>
        /// <param name="machine"></param>
        /// <returns></returns>
        public string KYMachineToData(string machine)
        {
            if (machine == GlobalVars.MachineCode_CYG1_KY)
                return GlobalVars.MachineCode_CYG1;
            else if (machine == GlobalVars.MachineCode_CYG2_KY)
                return GlobalVars.MachineCode_CYG2;
            return string.Empty;
        }

        /// <summary>
        /// 标准设备编码转换为开元编码
        /// </summary>
        /// <param name="machine"></param>
        /// <returns></returns>
        public string DataToKYMachine(string machine)
        {
            if (machine == GlobalVars.MachineCode_CYG1)
                return GlobalVars.MachineCode_CYG1_KY;
            else if (machine == GlobalVars.MachineCode_CYG2)
                return GlobalVars.MachineCode_CYG2_KY;
            return string.Empty;
        }

        #endregion

        /// <summary>
        /// 第三方数据库访问对象
        /// </summary>
        public SqlServerDapperDber EquDber;
        /// <summary>
        /// 设备编码
        /// </summary>
        public string MachineCode;
        private static DateTime LastConnectTime = DateTime.Now.AddMinutes(-1);
        private static String connectValue = "-9999";

        /// <summary>
        /// 发送存样柜命令
        /// </summary>
        /// <param name="code"></param>
        /// <param name="machinecode"></param>
        /// <param name="operateCode"></param>
        /// <returns></returns>
        public bool SendCupboardCmd(string code, string machinecode, eOperateCode operateCode)
        {
            Tb_PreAction preAction = new Tb_PreAction();
            preAction.SSName = machinecode;
            preAction.Sample_Id = code;
            preAction.Operate_Code = Convert.ToInt32(operateCode);
            preAction.Person_Id = 0;
            preAction.Priority = 7;
            preAction.DoneState = 0;
            return false;
        }

        #region 连锁确定存样柜
        /// <summary>
        /// 连锁确定存样柜
        /// </summary>
        /// <param name="czplx"></param>
        /// <param name="yplx"></param>
        /// <returns></returns>
        private bool AutoCheckSentInfCYGBill(InfCYGControlCMDDetail cmcscygcontrolcmddetail, Action<string, eOutputType> output)
        {
            bool returnvalue = false;
            if (cmcscygcontrolcmddetail.Status == "正在处理")
            {
                EquCYGCmd equcygcmd = new EquCYGCmd();
                EquCYGSample equcygsample = this.EquDber.Entity<EquCYGSample>(" where SampleCode='" + cmcscygcontrolcmddetail.MakeCode + "'");
                if (equcygsample == null)
                {
                    cmcscygcontrolcmddetail.ResultCode = eEquInfCmdResultCode.默认.ToString();
                    cmcscygcontrolcmddetail.Status = "处理完成";
                    cmcscygcontrolcmddetail.Errors = "存样柜无此样：请处理!";
                    returnvalue = false;
                }
                else
                {
                    equcygcmd.MachineCode = equcygsample.MachineCode;
                    equcygcmd.SampleCode = cmcscygcontrolcmddetail.MakeCode;
                    cmcscygcontrolcmddetail.Status = "等待结果";
                    if (this.EquDber.Insert(equcygcmd) > 0 && Dbers.GetInstance().SelfDber.Update(cmcscygcontrolcmddetail) > 0)
                    {
                        returnvalue = true;
                        output(string.Format("成功发送命令等待结果" + cmcscygcontrolcmddetail.MakeCode), eOutputType.Normal);
                    }
                }
            }
            if (cmcscygcontrolcmddetail.Status == "等待结果")
            {
                EquCYGCmd equcygcmds = this.EquDber.Entity<EquCYGCmd>(String.Format(" where SampleCode='{0}' order by createdate desc", cmcscygcontrolcmddetail.MakeCode));
                if (equcygcmds.ResultCode == 1)
                {
                    cmcscygcontrolcmddetail.ResultCode = eEquInfCmdResultCode.默认.ToString();
                    cmcscygcontrolcmddetail.Status = "处理完成";
                    Dbers.GetInstance().SelfDber.Update(cmcscygcontrolcmddetail);
                    output(string.Format("取样成功" + cmcscygcontrolcmddetail.MakeCode), eOutputType.Normal);
                }
                else if (equcygcmds.ResultCode == 2)
                {
                    cmcscygcontrolcmddetail.ResultCode = eEquInfCmdResultCode.默认.ToString();
                    cmcscygcontrolcmddetail.Status = "处理完成";
                    cmcscygcontrolcmddetail.Errors = "存样柜系统异常：请处理!";
                    Dbers.GetInstance().SelfDber.Update(cmcscygcontrolcmddetail);
                    output(string.Format("取样失败" + cmcscygcontrolcmddetail.MakeCode), eOutputType.Normal);
                }
                else if (DateTime.Now - equcygcmds.CreateDate > TimeSpan.FromMinutes(15))
                {
                    cmcscygcontrolcmddetail.ResultCode = eEquInfCmdResultCode.默认.ToString();
                    cmcscygcontrolcmddetail.Status = "处理完成";
                    cmcscygcontrolcmddetail.Errors = "存样柜超时异常：请处理!";
                    Dbers.GetInstance().SelfDber.Update(cmcscygcontrolcmddetail);
                    output(string.Format("取样失败" + cmcscygcontrolcmddetail.MakeCode), eOutputType.Normal);
                }
            }
            return returnvalue;
        }
        #endregion

        #region 转换成位置
        /// <summary>
        /// 转换成第三方接口-位置
        /// </summary>
        /// <param name="CmdCode">位置</param>
        /// <returns></returns>
        private int ConvertToCmdCode(string CmdCode)
        {
            eCmdCode enumResulr;
            if (Enum.TryParse(CmdCode, out enumResulr))
                return (int)enumResulr;
            else
                return (int)eCmdCode.存样柜;
        }
        #endregion

        /// <summary>
        /// 同步存样柜操作记录
        /// </summary>
        /// <param name="output"></param>
        /// <returns></returns>
        public int SyncCYGRecord(Action<string, eOutputType> output)
        {
            int res = 0;
            foreach (Tb_Action item in this.EquDber.Entities<Tb_Action>("where operate_time >=GETDATE() order by operate_time desc", new { MachineCode = DataToKYMachine(this.MachineCode) }))
            {
                eOperateCode eOperCode;
                Enum.TryParse<eOperateCode>(item.Operate_Code.ToString(), false, out eOperCode);
                InfCYGRecord record = Dbers.GetInstance().SelfDber.Entity<InfCYGRecord>("where Code=:Code and OperType=:OperType", new { Code = item.Sample_Id, OperType = eOperCode.ToString() });
                if (record == null)
                {
                    record = new InfCYGRecord();
                    record.MachineCode = this.MachineCode;
                    record.Code = item.Sample_Id;
                    record.Bolt_Id = item.Bolt_Id;
                    record.OperType = eOperCode.ToString();
                    record.UpdateTime = item.Operate_Time;
                    record.OperName = item.PersonName;
                    if (Dbers.GetInstance().SelfDber.Insert(record) > 0)
                        res++;
                }
            }

            output(string.Format("同步存样柜操作记录:{0}条", res), eOutputType.Normal);
            return res;
        }

        /// <summary>
        /// 同步存样柜命令
        /// </summary>
        /// <param name="output"></param>
        /// <returns></returns>
        public int SyncCYGCmd(Action<string, eOutputType> output)
        {
            int res = 0, resdetail = 0;
            foreach (InfCYGControlCMD item in Dbers.GetInstance().SelfDber.Entities<InfCYGControlCMD>("where DataFlag=0 and MachineCode=:MachineCode", new { MachineCode = this.MachineCode }))
            {
                foreach (InfCYGControlCMDDetail detail in item.CmdDetails)
                {
                    Tb_Bolt bolt = this.EquDber.Entity<Tb_Bolt>("where Sample_Id=@Sample_Id", new { Sample_Id = detail.MakeCode });
                    if (bolt == null) continue;
                    Tb_PreAction cmd = new Tb_PreAction();
                    cmd.SSName = DataToKYMachine(detail.MachineCode);
                    cmd.Sample_Id = detail.MakeCode;
                    cmd.Operate_Code = item.OperType == "取样" ? 2 : 3;
                    cmd.Priority = 4;
                    cmd.Bolt_Id = bolt.Bolt_Id;
                    cmd.DoneState = 0;
                    if (this.EquDber.Insert(cmd) > 0)
                    {
                        resdetail++;
                    }
                }
                res++;
                item.DataFlag = 1;
                Dbers.GetInstance().SelfDber.Update(item);
            }

            output(string.Format("同步存样柜命令:{0}条,明细命令:{1}条)", res, resdetail), eOutputType.Normal);
            return res;
        }

        /// <summary>
        /// 同步存样柜执行结果
        /// </summary>
        /// <param name="output"></param>
        /// <returns></returns>
        public int SyncCYGResult(Action<string, eOutputType> output)
        {
            int res = 0;
            foreach (Tb_PreAction item in this.EquDber.Entities<Tb_PreAction>("where DoneState=2 or DoneState=3 order by Sample_Id desc"))
            {
                InfCYGControlCMDDetail cygdetail = Dbers.GetInstance().SelfDber.Entity<InfCYGControlCMDDetail>("where MakeCode=:MakeCode and MachineCode=:MachineCode order by CreateDate desc", new { MakeCode = item.Sample_Id, MachineCode = KYMachineToData(item.SSName) });
                if (cygdetail == null) continue;
                if (item.DoneState == 2)
                    cygdetail.ResultCode = eEquInfCmdResultCode.失败.ToString();
                else if (item.DoneState == 3)
                    cygdetail.ResultCode = eEquInfCmdResultCode.成功.ToString();
                cygdetail.Status = "1";
                if (Dbers.GetInstance().SelfDber.Update(cygdetail) > 0)
                    res++;
                //给主表的status赋值  0 默认 1成功 2 失败
            }
            output(string.Format("同步存样柜命令结果:{0}条)", res), eOutputType.Normal);
            return res;
        }
        /// <summary>
        /// 同步智能存样柜数据
        /// </summary>
        /// <param name="output"></param>
        /// <returns></returns>
        public bool SyncCYGInfo(Action<string, eOutputType> output)
        {
            int resu = 0;//修改
            int resi = 0;//新增
            bool returnresult = false;

            foreach (Tb_Bolt item in this.EquDber.Entities<Tb_Bolt>("where DataStatus=0"))
            {
                InfCYGSamHistory infcygsamhistory = new InfCYGSamHistory();
                InfCYGSam infcygsam = Dbers.GetInstance().SelfDber.Entity<InfCYGSam>(" where MachineCode=:MachineCode and CellIndex=:CellIndex and ColumnIndex=:ColumnIndex and AreaNumber=:AreaNumber", new { MachineCode = MachineCode, CellIndex = item.RowNo, ColumnIndex = item.ColumnNo, AreaNumber = item.RotateNo });

                if (infcygsam == null)
                {
                    infcygsam = new InfCYGSam();
                    infcygsam.UpdateTime = DateTime.Now;
                    infcygsam.Code = item.Sample_Id;
                    infcygsam.SamType = item.Big == 1 ? "大瓶" : "小瓶";
                    infcygsam.CellIndex = item.RowNo;
                    infcygsam.ColumnIndex = item.ColumnNo;
                    infcygsam.AreaNumber = item.RotateNo;
                    infcygsam.MachineCode = MachineCode;
                    if (!String.IsNullOrEmpty(item.Sample_Id)) { infcygsam.IsNew = 1; } else { infcygsam.IsNew = 0; }
                    if (Dbers.GetInstance().SelfDber.Insert(infcygsam) > 0)
                    {
                        resi++;
                        item.DataStatus = 1;
                        this.EquDber.Update(item);
                    }

                    //历史存样信息
                    //if (!String.IsNullOrEmpty(item.Sample_Id))
                    //{
                    //    infcygsamhistory.MachineCode = MachineCode;
                    //    infcygsamhistory.UpdateTime = DateTime.Now;
                    //    infcygsamhistory.Code = item.Sample_Id;
                    //    infcygsam.SamType = item.Big == 1 ? "大瓶" : "小瓶";
                    //    infcygsamhistory.CellIndex = item.RowNo;
                    //    infcygsamhistory.ColumnIndex = item.ColumnNo;
                    //    infcygsamhistory.AreaNumber = item.RotateNo;
                    //    infcygsamhistory.IsNew = 1;
                    //    resu += Dbers.GetInstance().SelfDber.Insert(infcygsamhistory);
                    //}
                }
                else
                {
                    #region 历史存样信息
                    //if (!String.IsNullOrEmpty(item.Sample_Id))
                    //{
                    //    infcygsamhistory.UpdateTime = DateTime.Now;
                    //    infcygsamhistory.Code = item.Sample_Id;
                    //    //infcygsamhistory.SamType = item.SampleType;
                    //    infcygsamhistory.CellIndex = item.RowNo;
                    //    infcygsamhistory.ColumnIndex = item.ColumnNo;
                    //    infcygsamhistory.AreaNumber = item.RotateNo;
                    //    infcygsamhistory.MachineCode = MachineCode;
                    //    infcygsamhistory.IsNew = 1;
                    //    resu += Dbers.GetInstance().SelfDber.Insert(infcygsamhistory);
                    //}
                    //else
                    //{
                    //    infcygsamhistory.UpdateTime = DateTime.Now;
                    //    infcygsamhistory.Code = infcygsam.Code;
                    //    infcygsamhistory.SamType = infcygsam.SamType;
                    //    infcygsamhistory.CellIndex = infcygsam.CellIndex;
                    //    infcygsamhistory.ColumnIndex = infcygsam.ColumnIndex;
                    //    infcygsamhistory.AreaNumber = infcygsam.AreaNumber;
                    //    infcygsamhistory.MachineCode = MachineCode;
                    //    infcygsamhistory.IsNew = 0;
                    //    resu += Dbers.GetInstance().SelfDber.Insert(infcygsamhistory);
                    //}

                    #endregion

                    infcygsam.UpdateTime = DateTime.Now;
                    infcygsam.Code = item.Sample_Id;
                    infcygsam.SamType = item.Big == 1 ? "大瓶" : "小瓶";
                    infcygsam.CellIndex = item.RowNo;
                    infcygsam.ColumnIndex = item.ColumnNo;
                    infcygsam.AreaNumber = item.RotateNo;
                    infcygsam.MachineCode = MachineCode;
                    if (!String.IsNullOrEmpty(item.Sample_Id)) { infcygsam.IsNew = 1; } else { infcygsam.IsNew = 0; }
                    if (Dbers.GetInstance().SelfDber.Update(infcygsam) > 0)
                    {
                        resi++;
                        item.DataStatus = 1;
                        this.EquDber.Update(item);
                    }
                }
            }

            if (Dbers.GetInstance().SelfDber.Entities<InfCYGSam>(" where IsNew!=1").Count <= 50)
            {
                CommonDAO.GetInstance().SaveSysMessage(eMessageType.存样柜.ToString(), "智能存样柜空闲个数小于50个!", eMessageType.存样柜.ToString());
            }

            if (Dbers.GetInstance().SelfDber.Entities<InfCYGSam>(" where IsNew=1 and UpdateTime+90>sysdate").Count <= 50)
            {
                CommonDAO.GetInstance().SaveSysMessage(eMessageType.存样柜.ToString(), "智能存样柜样品已超期!", eMessageType.存样柜.ToString());
            }

            output(string.Format("同步数据:(实时数据同步:{0},历史数据同步:{1})", resi, resu), eOutputType.Normal);
            return returnresult;
        }

        /// <summary>
        /// 同步实时信号到集中管控
        /// </summary>
        /// <param name="output"></param>
        /// <returns></returns>
        public int SyncSignalDatal(Action<string, eOutputType> output)
        {
            int res = 0;
            foreach (Tb_Status entity in this.EquDber.Entities<Tb_Status>(" where Status=0"))
            {
                res += CommonDAO.GetInstance().SetSignalDataValue(MachineCode, "机械手空闲标志位", entity.S1.ToString()) ? 1 : 0;
                res += CommonDAO.GetInstance().SetSignalDataValue(MachineCode, "人工取瓶准备好标志位", entity.S2.ToString()) ? 1 : 0;
                res += CommonDAO.GetInstance().SetSignalDataValue(MachineCode, "自动取瓶准备好标志位", entity.S3.ToString()) ? 1 : 0;
                res += CommonDAO.GetInstance().SetSignalDataValue(MachineCode, "自动存瓶准备好标志位", entity.S4.ToString()) ? 1 : 0;
                res += CommonDAO.GetInstance().SetSignalDataValue(MachineCode, "人工存瓶准备好标志位", entity.S5.ToString()) ? 1 : 0;
                res += CommonDAO.GetInstance().SetSignalDataValue(MachineCode, "人工存瓶启动中标志位", entity.S6.ToString()) ? 1 : 0;
                res += CommonDAO.GetInstance().SetSignalDataValue(MachineCode, "自动取瓶启动中标志位", entity.S7.ToString()) ? 1 : 0;
                res += CommonDAO.GetInstance().SetSignalDataValue(MachineCode, "人工存瓶启动中标志位", entity.S8.ToString()) ? 1 : 0;
                res += CommonDAO.GetInstance().SetSignalDataValue(MachineCode, "异常存瓶中标志位", entity.S9.ToString()) ? 1 : 0;
                res += CommonDAO.GetInstance().SetSignalDataValue(MachineCode, "自动存瓶中标志位", entity.S10.ToString()) ? 1 : 0;
                res += CommonDAO.GetInstance().SetSignalDataValue(MachineCode, "回零中标志位", entity.S11.ToString()) ? 1 : 0;

                res += CommonDAO.GetInstance().SetSignalDataValue(MachineCode, eSignalDataName.设备状态.ToString(), entity.M_STATUS.ToString()) ? 1 : 0;
                entity.Status = 1;
                this.EquDber.Update(entity);
            }

            foreach (EquCYGSignal entity in this.EquDber.Entities<EquCYGSignal>())
            {
                res += CommonDAO.GetInstance().SetSignalDataValue(MachineCode, "机械手行走", entity.JXS_行走) ? 1 : 0;
                res += CommonDAO.GetInstance().SetSignalDataValue(MachineCode, "机械手升降", entity.JXS_升降) ? 1 : 0;
                res += CommonDAO.GetInstance().SetSignalDataValue(MachineCode, "机械手旋转", entity.JXS_旋转) ? 1 : 0;
            }

            int ready = 0, big = 0, small = 0, bigCW = 624, smallCW = 416, TotalCW = 1040;
            res += CommonDAO.GetInstance().SetSignalDataValue(MachineCode, "共有仓位", TotalCW.ToString()) ? 1 : 0;
            res += CommonDAO.GetInstance().SetSignalDataValue(MachineCode, "大瓶仓位", bigCW.ToString()) ? 1 : 0;
            res += CommonDAO.GetInstance().SetSignalDataValue(MachineCode, "小瓶仓位", smallCW.ToString()) ? 1 : 0;

            IList<Tb_Bolt> bolts = this.EquDber.Entities<Tb_Bolt>(" where Bolt_State=1");
            if (bolts != null)
            {
                ready = bolts.Count;
                big = bolts.Where(a => a.Big == 1).Count();
                small = bolts.Where(a => a.Small == 1).Count();
            }
            res += CommonDAO.GetInstance().SetSignalDataValue(MachineCode, "已存仓位", ready.ToString()) ? 1 : 0;
            res += CommonDAO.GetInstance().SetSignalDataValue(MachineCode, "未存仓位", (TotalCW - ready).ToString()) ? 1 : 0;
            res += CommonDAO.GetInstance().SetSignalDataValue(MachineCode, "大瓶已存仓位", big.ToString()) ? 1 : 0;
            res += CommonDAO.GetInstance().SetSignalDataValue(MachineCode, "小瓶已存仓位", small.ToString()) ? 1 : 0;
            res += CommonDAO.GetInstance().SetSignalDataValue(MachineCode, "存样率", (ready * 100 / TotalCW).ToString("f2") + "%") ? 1 : 0;

            output(string.Format("智能存样柜-同步实时信号 {0} 条", res), eOutputType.Normal);

            return res;
        }

        /// <summary>
        /// 同步存样柜故障信息到集中管控
        /// </summary>
        /// <param name="output"></param>
        /// <returns></returns>
        public void SyncCYGError(Action<string, eOutputType> output)
        {
            int res = 0;

            foreach (Tb_FailLog entity in this.EquDber.Entities<Tb_FailLog>("where Status=0"))
            {
                if (CommonDAO.GetInstance().SaveEquInfHitch(MachineCode, entity.Time, entity.Fail_Log, entity.Dv_Name + entity.Id))
                {
                    entity.Status = 1;
                    this.EquDber.Update(entity);

                    res++;
                }
            }

            output(string.Format("智能存样柜-同步故障信息记录 {0} 条", res), eOutputType.Normal);
        }
    }
}
