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

namespace CMCS.DumblyConcealer.Tasks.AutoMaker
{
	/// <summary>
	/// 全自动制样机接口业务
	/// </summary>
	public class EquAutoMakerDAO
	{
		/// <summary>
		/// EquAutoMakerDAO
		/// </summary>
		/// <param name="machineCode">制样机编码</param>
		/// <param name="equDber">第三方数据库访问对象</param>
		public EquAutoMakerDAO(string machineCode, SqlServerDapperDber equDber)
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
		public string MachineCode;
		/// <summary>
		/// 是否处于故障状态
		/// </summary>
		bool IsHitch = false;
		/// <summary>
		/// 上一次上位机心跳值
		/// </summary>
		string PrevHeartbeat = string.Empty;

		#region 数据转换方法（此处有点麻烦，后期调整接口方案）

		/// <summary>
		/// 开元编码转换为标准设备编码
		/// </summary>
		/// <param name="machine"></param>
		/// <returns></returns>
		public string KYMachineToData(string machine)
		{
			if (machine == GlobalVars.MachineCode_QZDZYJ_KY_1)
				return GlobalVars.MachineCode_QZDZYJ_1;
			else if (machine == GlobalVars.MachineCode_QZDZYJ_KY_2)
				return GlobalVars.MachineCode_QZDZYJ_2;
			else if (machine == GlobalVars.MachineCode_QZDZYJ_KY_3)
				return GlobalVars.MachineCode_QZDZYJ_3;
			return string.Empty;
		}

		/// <summary>
		/// 标准设备编码转换为开元编码
		/// </summary>
		/// <param name="machine"></param>
		/// <returns></returns>
		public string DataToKYMachine(string machine)
		{
			if (machine == GlobalVars.MachineCode_QZDZYJ_1)
				return GlobalVars.MachineCode_QZDZYJ_KY_1;
			else if (machine == GlobalVars.MachineCode_QZDZYJ_2)
				return GlobalVars.MachineCode_QZDZYJ_KY_2;
			else if (machine == GlobalVars.MachineCode_QZDZYJ_3)
				return GlobalVars.MachineCode_QZDZYJ_KY_3;
			return string.Empty;
		}
		#endregion

		/// <summary>
		/// 同步实时信号到集中管控
		/// </summary>
		/// <param name="output"></param>
		/// <returns></returns>
		public int SyncSignal(Action<string, eOutputType> output)
		{
			int res = 0;

			//同步制样机状态
			foreach (ZY_Status_Tb entity in this.EquDber.Entities<ZY_Status_Tb>("where MachineCode=@MachineCode", new { MachineCode = DataToKYMachine(this.MachineCode) }))
			{
				if (entity.SamReady == (int)eEquInfAutoMakerSystemStatus.就绪待机 || entity.SamReady == (int)eEquInfAutoMakerSystemStatus.可以制样)
					res += commonDAO.SetSignalDataValue(this.MachineCode, eSignalDataName.设备状态.ToString(), eEquInfSystemStatus.就绪待机.ToString()) ? 1 : 0;
				else if (entity.SamReady == (int)eEquInfAutoMakerSystemStatus.正在运行)
					res += commonDAO.SetSignalDataValue(this.MachineCode, eSignalDataName.设备状态.ToString(), eEquInfSystemStatus.正在运行.ToString()) ? 1 : 0;
				else if (entity.SamReady == (int)eEquInfAutoMakerSystemStatus.发生故障)
					res += commonDAO.SetSignalDataValue(this.MachineCode, eSignalDataName.设备状态.ToString(), eEquInfSystemStatus.发生故障.ToString()) ? 1 : 0;
				else if (entity.SamReady == (int)eEquInfAutoMakerSystemStatus.急停 || entity.SamReady == (int)eEquInfAutoMakerSystemStatus.停止)
					res += commonDAO.SetSignalDataValue(this.MachineCode, eSignalDataName.设备状态.ToString(), eEquInfSystemStatus.系统停止.ToString()) ? 1 : 0;
				//res += commonDAO.SetSignalDataValue(this.MachineCode, eSignalDataName.设备状态.ToString(), ((eEquInfAutoMakerSystemStatus)entity.SamReady).ToString()) ? 1 : 0;
			}
			//制样设备状态
			foreach (ZY_State_Tb entity in this.EquDber.Entities<ZY_State_Tb>())
			{
				res += commonDAO.SetSignalDataValue(this.MachineCode, entity.DeviceName, (entity.DeviceStatus).ToString()) ? 1 : 0;
				if (entity.DeviceName == "一号桶")
					res += commonDAO.SetSignalDataValue(this.MachineCode, "一号桶编码", string.IsNullOrEmpty(entity.SampleID) ? "" : entity.SampleID) ? 1 : 0;
				else if (entity.DeviceName == "二号桶")
					res += commonDAO.SetSignalDataValue(this.MachineCode, "二号桶编码", string.IsNullOrEmpty(entity.SampleID) ? "" : entity.SampleID) ? 1 : 0;
				else if (entity.DeviceName == "三号桶")
					res += commonDAO.SetSignalDataValue(this.MachineCode, "三号桶编码", string.IsNullOrEmpty(entity.SampleID) ? "" : entity.SampleID) ? 1 : 0;
				else if (entity.DeviceName == "四号桶")
					res += commonDAO.SetSignalDataValue(this.MachineCode, "四号桶编码", string.IsNullOrEmpty(entity.SampleID) ? "" : entity.SampleID) ? 1 : 0;
				else if (entity.DeviceName == "五号桶")
					res += commonDAO.SetSignalDataValue(this.MachineCode, "五号桶编码", string.IsNullOrEmpty(entity.SampleID) ? "" : entity.SampleID) ? 1 : 0;
				else if (entity.DeviceName == "六号桶")
					res += commonDAO.SetSignalDataValue(this.MachineCode, "六号桶编码", string.IsNullOrEmpty(entity.SampleID) ? "" : entity.SampleID) ? 1 : 0;
				else if (entity.DeviceName == "七号桶")
					res += commonDAO.SetSignalDataValue(this.MachineCode, "七号桶编码", string.IsNullOrEmpty(entity.SampleID) ? "" : entity.SampleID) ? 1 : 0;
				else if (entity.DeviceName == "八号桶")
					res += commonDAO.SetSignalDataValue(this.MachineCode, "八号桶编码", string.IsNullOrEmpty(entity.SampleID) ? "" : entity.SampleID) ? 1 : 0;
				else if (entity.DeviceName == "入料斗")
					res += commonDAO.SetSignalDataValue(this.MachineCode, "入料斗编码", string.IsNullOrEmpty(entity.SampleID) ? "" : entity.SampleID) ? 1 : 0;
				else if (entity.DeviceName == "烘干机A")
					res += commonDAO.SetSignalDataValue(this.MachineCode, "烘干机A编码", string.IsNullOrEmpty(entity.SampleID) ? "" : entity.SampleID) ? 1 : 0;
				else if (entity.DeviceName == "烘干机B")
					res += commonDAO.SetSignalDataValue(this.MachineCode, "烘干机B编码", string.IsNullOrEmpty(entity.SampleID) ? "" : entity.SampleID) ? 1 : 0;
				else if (entity.DeviceName == "烘干机C")
					res += commonDAO.SetSignalDataValue(this.MachineCode, "烘干机C编码", string.IsNullOrEmpty(entity.SampleID) ? "" : entity.SampleID) ? 1 : 0;
				else if (entity.DeviceName == "提升斗")
					res += commonDAO.SetSignalDataValue(this.MachineCode, "提升斗编码", string.IsNullOrEmpty(entity.SampleID) ? "" : entity.SampleID) ? 1 : 0;

				entity.DataStatus = 1;//我方已读
				this.EquDber.Update(entity);
			}

			output(string.Format("同步实时信号 {0} 条", res), eOutputType.Normal);

			return res;
		}

		/// <summary>
		/// 同步制样 故障信息到集中管控
		/// </summary>
		/// <param name="output"></param>
		/// <returns></returns>
		public void SyncError(Action<string, eOutputType> output)
		{
			int res = 0;

			foreach (var entity in this.EquDber.Entities<ZY_Error_Tb>("where DataStatus=0"))
			{
				if (CommonDAO.GetInstance().SaveEquInfHitch(this.MachineCode, entity.ErrorTime, entity.ErrorDec, entity.ErrorTime.ToString().Replace(" ", "")))
				{
					entity.DataStatus = 1;
					this.EquDber.Update(entity);

					res++;
				}
			}

			output(string.Format("同步故障信息记录 {0} 条", res), eOutputType.Normal);
		}

		/// <summary>
		/// 同步制样命令
		/// </summary>
		/// <param name="output"></param>
		/// <returns></returns>
		public void SyncCmd(Action<string, eOutputType> output)
		{
			int res = 0;

			// 集中管控 > 第三方 
			foreach (InfMakerControlCmd entity in AutoMakerDAO.GetInstance().GetWaitForSyncMakerControlCmd(this.MachineCode))
			{
				bool isSuccess = false;

				ZY_Cmd_Tb cmdtb = this.EquDber.Entity<ZY_Cmd_Tb>("where MachineCode=@MachineCode", new { MachineCode = DataToKYMachine(this.MachineCode) });
				if (cmdtb == null)
				{
					isSuccess = this.EquDber.Insert(new ZY_Cmd_Tb
					{
						MachineCode = DataToKYMachine(this.MachineCode),
						CommandCode = 1,
						SampleCode = entity.MakeCode,
						SendCommandTime = DateTime.Now,
						DataStatus = 0
					}) > 0;
				}
				else
				{
					cmdtb.CommandCode = 1;
					cmdtb.SampleCode = entity.MakeCode;
					cmdtb.SendCommandTime = DateTime.Now;
					cmdtb.DataStatus = 0;
					isSuccess = this.EquDber.Update(cmdtb) > 0;
				}

				ZY_Interface_Tb interfacetb = this.EquDber.Entity<ZY_Interface_Tb>();
				if (interfacetb == null)
				{
					interfacetb = new ZY_Interface_Tb();
					interfacetb.SampleID = entity.MakeCode;
					interfacetb.Type = 4;
					interfacetb.Size = 1;
					interfacetb.Water = 1;
					interfacetb.DataStatus = 0;
					isSuccess = this.EquDber.Insert(interfacetb) > 0;
				}
				else
				{
					isSuccess = this.EquDber.Execute(string.Format("update {0} set SampleID ='{1}',Type = 4,Size = 1,Water = 4,DataStatus = 0 ", CMCS.DapperDber.Util.EntityReflectionUtil.GetTableName<ZY_Interface_Tb>(), entity.MakeCode)) > 0;
				}
				if (isSuccess)
				{
					entity.SyncFlag = 1;
					commonDAO.SelfDber.Update(entity);
					res++;
				}
			}
			output(string.Format("同步控制命令 {0} 条（集中管控 > 第三方）", res), eOutputType.Normal);
		}

		/// <summary>
		/// 同步制样 出样明细信息到集中管控
		/// </summary>
		/// <param name="output"></param>
		/// <returns></returns>
		public void SyncMakeDetail(Action<string, eOutputType> output)
		{
			int res = 0;

			foreach (ZY_Record_Tb entity in this.EquDber.Entities<ZY_Record_Tb>("where DataStatus=0 and PackCode!=0 order by StartTime desc"))
			{
				if (SyncToRCMakeDetail(entity))
				{
					InfMakerRecord makeRecord = new InfMakerRecord
					{
						InterfaceType = CommonDAO.GetInstance().GetMachineInterfaceTypeByCode(this.MachineCode),
						MachineCode = this.MachineCode,
						MakeCode = entity.SampleID,
						BarrelCode = entity.PackCode,
						YPType = entity.SampleType, //AutoMakerDAO.GetInstance().GetKYMakeType(entity.SampleType.ToString()),
						YPWeight = entity.SamepleWeight,
						StartTime = entity.StartTime,
						EndTime = entity.EndTime,
						MakeUser = entity.UserName,
						DataFlag = 1
					};
					if (AutoMakerDAO.GetInstance().SaveMakerRecord(makeRecord))
					{
						entity.DataStatus = 1;
						this.EquDber.Update(entity);
						res++;
					}
				}
			}

			output(string.Format("同步出样明细记录 {0} 条", res), eOutputType.Normal);
		}

		/// <summary>
		/// 同步样品信息到集中管控入厂煤制样明细表
		/// </summary>
		/// <param name="makeDetail"></param>
		private bool SyncToRCMakeDetail(ZY_Record_Tb makeDetail)
		{
			CmcsRCMake rCMake = commonDAO.SelfDber.Entity<CmcsRCMake>("where MakeCode=:MakeCode", new { MakeCode = makeDetail.SampleID });
			if (rCMake != null)
			{
				// 修改制样结束时间
				rCMake.MakeStyle = eMakeType.机器制样.ToString();
				if (rCMake.MakeEndTime < makeDetail.EndTime) rCMake.MakeEndTime = makeDetail.EndTime;
				if (rCMake.MakeStartTime != rCMake.CreateDate && rCMake.MakeStartTime > makeDetail.StartTime)
				{
					rCMake.GetDate = DateTime.Now;
					rCMake.MakeStartTime = makeDetail.StartTime;
				}
				if (rCMake.MakeStartTime < makeDetail.StartTime)
				{
					rCMake.MakeStartTime = makeDetail.StartTime;
				}
				if (rCMake.MakeEndTime < makeDetail.EndTime)
				{
					rCMake.MakeEndTime = makeDetail.EndTime;
				}
				commonDAO.SelfDber.Update(rCMake);

				CmcsRCMakeDetail rCMakeDetail = commonDAO.SelfDber.Entity<CmcsRCMakeDetail>("where MakeId=:MakeId and SampleType=:SampleType", new { MakeId = rCMake.Id, SampleType = makeDetail.SampleType });
				if (rCMakeDetail != null)
				{
					rCMakeDetail.OperDate = DateTime.Now;
					rCMakeDetail.CreateDate = DateTime.Now;
					rCMakeDetail.Weight = makeDetail.SamepleWeight;
					rCMakeDetail.SampleType = makeDetail.SampleType;//AutoMakerDAO.GetInstance().GetKYMakeType(makeDetail.SampleType.ToString());
																	//rCMakeDetail.BarrelCode = makeDetail.PackCode;
					return commonDAO.SelfDber.Update(rCMakeDetail) > 0;
				}
				else
				{
					rCMakeDetail = new CmcsRCMakeDetail();
					rCMakeDetail.MakeId = rCMake.Id;
					rCMakeDetail.OperDate = DateTime.Now;
					rCMakeDetail.CreateDate = DateTime.Now;
					rCMakeDetail.Weight = makeDetail.SamepleWeight;
					rCMakeDetail.SampleType = makeDetail.SampleType;//AutoMakerDAO.GetInstance().GetKYMakeType(makeDetail.SampleType.ToString());
					rCMakeDetail.BarrelCode = makeDetail.PackCode;
					return commonDAO.SelfDber.Insert(rCMakeDetail) > 0;
				}
			}
			else
				return true;

		}

		/// <summary>
		/// 同步进料皮带卸样命令
		/// </summary>
		/// <param name="makeDetail"></param>
		public void SyncXLCmd(Action<string, eOutputType> output)
		{
			int res = 0;
			foreach (InfMakerUnLoad item in Dbers.GetInstance().SelfDber.Entities<InfMakerUnLoad>("where MachineCode=:MachineCode and SyncFlag=0", new { MachineCode = this.MachineCode }))
			{
				XL_Cmd_Tb cmd = this.EquDber.Entity<XL_Cmd_Tb>();
				if (cmd == null)
				{
					cmd.MachineCode = DataToKYMachine(item.MachineCode);
					cmd.CmdCode = item.CmdCode;
					cmd.ResultCode = eEquInfCmdResultCode.默认.ToString();
					cmd.DataFlag = 0;
					if (this.EquDber.Insert(cmd) > 0)
					{
						res++;
						item.SyncFlag = 1;
						Dbers.GetInstance().SelfDber.Update(item);
					}
				}
				else
				{
					cmd.CmdCode = item.CmdCode;
					cmd.ResultCode = eEquInfCmdResultCode.默认.ToString();
					cmd.DataFlag = 0;
					if (this.EquDber.Update(cmd) > 0)
					{
						res++;
						item.SyncFlag = 1;
						Dbers.GetInstance().SelfDber.Update(item);
					}
				}
			}

			output(string.Format("同步卸样皮带命令集控>第三方 {0} 条", res), eOutputType.Normal);

			res = 0;
			foreach (XL_Cmd_Tb item in this.EquDber.Entities<XL_Cmd_Tb>("where DataFlag=2"))
			{
				InfMakerUnLoad entity = Dbers.GetInstance().SelfDber.Entity<InfMakerUnLoad>("where ResultCode='默认' and SyncFlag=1 order by createdate desc");
				if (entity == null) continue;
				entity.ResultCode = item.ResultCode;
				entity.DataFlag = 3;
				if (Dbers.GetInstance().SelfDber.Update(entity) > 0)
				{
					res++;
					item.DataFlag = 3;
					this.EquDber.Update(item);
				}
			}
			output(string.Format("同步卸样皮带命令第三方>集控 {0} 条", res), eOutputType.Normal);
		}
	}
}
