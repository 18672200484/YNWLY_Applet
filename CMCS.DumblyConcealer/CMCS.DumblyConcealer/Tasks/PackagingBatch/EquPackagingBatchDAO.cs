using System;
using CMCS.Common;
using CMCS.Common.DAO;
using CMCS.Common.Enums;
using CMCS.DumblyConcealer.Enums;
using CMCS.DumblyConcealer.Tasks.AutoCupboard;
using CMCS.DumblyConcealer.Tasks.AutoMaker.Entities;
using CMCS.Common.Entities.AutoMaker;
using CMCS.Common.Entities.Fuel;
using CMCS.DapperDber.Dbs.SqlServerDb;
using CMCS.DumblyConcealer.Tasks.AutoCupboard.Enums;
using CMCS.DumblyConcealer.Tasks.AutoMaker.Enums;
using CMCS.Common.Entities.CarTransport;
using CMCS.DumblyConcealer.Tasks.PackagingBatch.Enums;
using CMCS.Common.Entities.PackingBatch;

namespace CMCS.DumblyConcealer.Tasks.PackagingBatch
{
    /// <summary>
    /// 封装归批机接口业务
    /// </summary>
    public class EquPackagingBatchDAO
    {
        /// <summary>
        /// EquAutoMakerDAO
        /// </summary>
        /// <param name="equDber">第三方数据库访问对象</param>
        public EquPackagingBatchDAO(SqlServerDapperDber equDber)
        {
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
       public string MachineCode = GlobalVars.MachineCode_PackingBatch_KY;

        /// <summary>
        /// 同步实时信号到集中管控
        /// </summary>
        /// <param name="output"></param>
        /// <returns></returns>
        public int SyncSignal(Action<string, eOutputType> output)
        {
            int res = 0;
            foreach (Status item in this.EquDber.Entities<Status>())
            {
                res += commonDAO.SetSignalDataValue(this.MachineCode, eSignalDataName.设备状态.ToString(), item.SystemStop.Trim() == "0" ? eEquInfSystemStatus.就绪待机.ToString() : eEquInfSystemStatus.发生故障.ToString()) ? 1 : 0;
                res += commonDAO.SetSignalDataValue(this.MachineCode, eSignalDataName.操作模式.ToString(), item.YCMS.Trim() == "0" ? "本地" : "远程") ? 1 : 0;
            }
            foreach (HYGP_Cmd_Tb item in this.EquDber.Entities<HYGP_Cmd_Tb>("where DataStatus=1"))
            {
                res += commonDAO.SetSignalDataValue(this.MachineCode, eSignalDataName.设备状态.ToString(), eEquInfSamplerSystemStatus.正在运行.ToString()) ? 1 : 0;
            }

            foreach (EquTbSignalData item in this.EquDber.Entities<EquTbSignalData>("where DataFlag=0"))
            {
                res += commonDAO.SetSignalDataValue(this.MachineCode, item.TagName, item.TagValue) ? 1 : 0;
            }
            output(string.Format("同步实时信号 {0} 条", res), eOutputType.Normal);

            return res;
        }

        /// <summary>
        /// 同步故障信息到集中管控
        /// </summary>
        /// <param name="output"></param>
        /// <returns></returns>
        public void SyncError(Action<string, eOutputType> output)
        {
            int res = 0;

            foreach (var entity in this.EquDber.Entities<Alarm_TB>("where CONVERT(char(10),DateTime,120)=CONVERT(char(10),GETDATE(),120)"))
            {
                if (CommonDAO.GetInstance().SaveEquInfHitch(this.MachineCode, entity.DateTime, entity.Error_Record, entity.Id.ToString()))
                {
                    res++;
                }
            }

            output(string.Format("同步故障信息记录 {0} 条", res), eOutputType.Normal);
        }

        /// <summary>
        /// 同步倒料命令
        /// </summary>
        /// <param name="output"></param>
        /// <returns></returns>
        public void SyncCmd(Action<string, eOutputType> output)
        {
            int res = 0;

            // 集中管控 > 第三方 
            foreach (InfPackingBatchCmd entity in PackingBatchDAO.GetInstance().GetWaitForSyncJXCYPackingBatchCmd())
            {
                bool isSuccess = false;

                isSuccess = this.EquDber.Insert(new HYGP_Cmd_Tb
                    {
                        Command = 1,
                        SampleCode = entity.SampleCode,
                        DateTime = DateTime.Now,
                        DataStatus = 0
                    }) > 0;

                if (isSuccess)
                {
                    entity.SyncFlag = 1;
                    commonDAO.SelfDber.Update(entity);
                    res++;
                }
            }
            output(string.Format("同步控制命令 {0} 条（集中管控 > 第三方）", res), eOutputType.Normal);


            res = 0;
            // 第三方 > 集中管控
            foreach (HYGP_Cmd_Tb entity in this.EquDber.Entities<HYGP_Cmd_Tb>("where DataStatus=11 or DataStatus=12"))
            {
                InfPackingBatchCmd makerControlCmd = commonDAO.SelfDber.Entity<InfPackingBatchCmd>("where SampleCode=:SampleCode and DataFlag=0 order by CreateDate desc", new { SampleCode = entity.SampleCode });
                if (makerControlCmd == null) continue;

                // 更新执行结果等
                makerControlCmd.ResultCode = ((eDataStatus)entity.DataStatus).ToString();
                makerControlCmd.DataFlag = 3;
                if (commonDAO.SelfDber.Update(makerControlCmd) > 0)
                {
                    res++;
                }
            }
            output(string.Format("同步控制命令 {0} 条（第三方 > 集中管控）", res), eOutputType.Normal);
        }

        /// <summary>
        /// 同步样桶信息
        /// </summary>
        /// <param name="output"></param>
        /// <returns></returns>
        public void SyncCoord(Action<string, eOutputType> output)
        {
            int res = 0;

            // 集中管控 > 第三方 
            foreach (Coord_TB entity in this.EquDber.Entities<Coord_TB>(""))
            {
                bool isSuccess = false;
                InfPackingBatchCoord batchcoord = new InfPackingBatchCoord();
                batchcoord.MachineCode = this.MachineCode;
                batchcoord.SampleCode = entity.SampleCode;
                batchcoord.State = entity.State;
                batchcoord.UpdateTime = entity.DateTime;
                batchcoord.Coord = entity.Coord;
                isSuccess = PackingBatchDAO.GetInstance().SaveCoord(batchcoord);
                if (isSuccess)
                {
                    res++;
                }
            }
            output(string.Format("同步样桶信息 {0} 条（第三方 > 集中管控）", res), eOutputType.Normal);
        }
    }
}
