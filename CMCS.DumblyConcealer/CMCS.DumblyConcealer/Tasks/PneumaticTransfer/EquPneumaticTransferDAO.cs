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
using CMCS.DumblyConcealer.Tasks.PneumaticTransfer.Entities;

namespace CMCS.DumblyConcealer.Tasks.PneumaticTransfer
{
    public class EquPneumaticTransferDAO
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="machineCode">设备编码</param>
        /// <param name="equDber">第三方数据库访问对象</param>
        public EquPneumaticTransferDAO(string machineCode, SqlServerDapperDber equDber)
        {
            this.MachineCode = machineCode;
            this.EquDber = equDber;
        }
        #region 数据转换方法

        #endregion
        /// <summary>
        /// 第三方数据库访问对象
        /// </summary>
        public SqlServerDapperDber EquDber;
        /// <summary>
        /// 设备编码
        /// </summary>
        public string MachineCode;

        /// <summary>
        /// 煤样类型转换
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public string SampleTypeChange(string type)
        {
            string sampleType = string.Empty;
            if (type == "1")
                sampleType = "13mm全水分样";
            else if (type == "2")
                sampleType = "6mm全水分样";
            else if (type == "3")
                sampleType = "3mm存查样";
            else if (type == "4")
                sampleType = "0.2mm一般试验分析样";
            else if (type == "5")
                sampleType = "0.2mm存查样";
            return sampleType;
        }

        #region 同步气送命令
        public int SyncQDCSCmd(Action<string, eOutputType> output)
        {
            int res = 0;
            foreach (InfQDCSCmd item in Dbers.GetInstance().SelfDber.Entities<InfQDCSCmd>("where DataFlag=0 order by CreateDate desc"))
            {
                string Transno = DateTime.Now.ToString("yyyyMMdd") + "001";
                Trans_Cmd_Tb cmd_Previous = null;
                do
                {
                    cmd_Previous = this.EquDber.Entity<Trans_Cmd_Tb>(string.Format("where Transno ={0}", Transno));

                    if (cmd_Previous != null) Transno = DateTime.Now.ToString("yyyyMMdd") + (Convert.ToInt32(cmd_Previous.Transno.Substring(8, 3)) + 1).ToString().PadLeft(3, '0');
                } while (cmd_Previous != null);

                Trans_Cmd_Tb cmd = new Trans_Cmd_Tb();
                cmd.Transno = Transno;
                cmd.MachineCode = 1;
                cmd.TransPriority = 0;
                cmd.TransPackCode = DateTime.Now.ToString("yyyyMMdd");
                cmd.SampleId = item.MakeCode;
                cmd.CommandCode = 1;//直传
                cmd.CmdOriginStation = item.OpStartIP;
                cmd.CmdDestinationStation = item.OpEndIP;
                cmd.SendCommandTime = DateTime.Now;
                cmd.DataStatus = 0;
                if (this.EquDber.Insert(cmd) > 0)
                {
                    item.DataFlag = 1;
                    Dbers.GetInstance().SelfDber.Update(item);
                    res++;
                }
            }
            output(string.Format("气动传输-同步命令 {0} 条", res), eOutputType.Normal);
            res = 0;
            foreach (Trans_Status_Tb item in this.EquDber.Entities<Trans_Status_Tb>("where SamReady=3"))
            {
                InfQDCSCmd entity = Dbers.GetInstance().SelfDber.Entity<InfQDCSCmd>("where DataFlag=1 and ResultCode='默认' order by CreateDate desc");
                if (entity != null)
                {
                    entity.ResultCode = eEquInfCmdResultCode.成功.ToString();
                    entity.DataFlag = 3;
                    if (Dbers.GetInstance().SelfDber.Update(entity) > 0)
                        res++;
                }
            }
            output(string.Format("气动传输-同步结果 {0} 条", res), eOutputType.Normal);
            return res;
        }
        #endregion

        #region 同步气送记录数据
        /// <summary>
        /// 同步气动传输数据
        /// </summary>
        /// <param name="output"></param>
        /// <returns></returns>
        public bool SyncQDCSRecord(Action<string, eOutputType> output)
        {
            int res = 0;
            foreach (Trans_Record_Tb item in this.EquDber.Entities<Trans_Record_Tb>("where DataStatus=0"))
            {
                eSendType sendtype;
                Enum.TryParse(item.TransType.ToString(), out sendtype);
                InfQDCSRecord record = new InfQDCSRecord()
                {
                    MachineCode = this.MachineCode,
                    MakeCode = item.PackeageCode,
                    SendType = sendtype.ToString(),
                    SampleType = !string.IsNullOrEmpty(item.SampleType) ? SampleTypeChange(item.SampleType) : "",
                    StartTime = item.StartTime,
                    EndTime = item.EndTime,
                    Oper = item.UserName,
                    DataFlag = 0
                };
                if (Dbers.GetInstance().SelfDber.Insert(record) > 0)
                {
                    item.DataStatus = 1;
                    this.EquDber.Update(item);
                    res++;
                }
                CommonDAO.GetInstance().SetSignalDataValue(this.MachineCode, "制样码", item.PackeageCode);
                CommonDAO.GetInstance().SetSignalDataValue(this.MachineCode, "传送结果", item.Issuccessed == 2 ? "成功" : "失败");
            }
            output(string.Format("同步数据气动传送记录数据{0}条)", res), eOutputType.Normal);
            return res > 0;
        }
        #endregion

        #region 同步实时信号到集中管控
        /// <summary>
        /// 同步实时信号到集中管控
        /// </summary>
        /// <param name="output"></param>
        /// <returns></returns>
        public int SyncSignalDatal(Action<string, eOutputType> output)
        {
            int res = 0;

            foreach (Trans_State_Tb entity in this.EquDber.Entities<Trans_State_Tb>(" where DataStatus=0"))
            {
                res += CommonDAO.GetInstance().SetSignalDataValue(MachineCode, entity.DeviceName, entity.DeviceStatus.ToString()) ? 1 : 0;
                entity.DataStatus = 1;
                this.EquDber.Update(entity);
            }
            output(string.Format("气动传输-同步实时信号 {0} 条", res), eOutputType.Normal);

            return res;
        }
        #endregion

        #region 同步故障信息到集中管控
        /// <summary>
        /// 同故障信息到集中管控
        /// </summary>
        /// <param name="output"></param>
        /// <returns></returns>
        public void SyncCYGError(Action<string, eOutputType> output)
        {
            int res = 0;

            foreach (Trans_ERR_Tb entity in this.EquDber.Entities<Trans_ERR_Tb>("where DataStatus=0"))
            {
                if (CommonDAO.GetInstance().SaveEquInfHitch(MachineCode, entity.ErrorTime, entity.ErrorDec, entity.ErrorCode + entity.Id))
                {
                    entity.DataStatus = 1;
                    this.EquDber.Update(entity);

                    res++;
                }
            }

            output(string.Format("气动传输-同步故障信息记录 {0} 条", res), eOutputType.Normal);
        }
        #endregion
    }
}
