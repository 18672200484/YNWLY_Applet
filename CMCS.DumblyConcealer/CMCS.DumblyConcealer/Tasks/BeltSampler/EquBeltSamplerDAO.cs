﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CMCS.Common;
using CMCS.Common.DAO;
using CMCS.Common.Entities;
using CMCS.Common.Entities.AutoMaker;
using CMCS.Common.Entities.BeltSampler;
using CMCS.Common.Entities.Fuel;
using CMCS.Common.Entities.Inf;
using CMCS.Common.Enums;
using CMCS.DapperDber.Dbs.OracleDb;
using CMCS.DumblyConcealer.Enums;
using CMCS.DumblyConcealer.Tasks.BeltSampler.Entities;
using CMCS.DumblyConcealer.Tasks.BeltSampler.Enums;
using CMCS.DapperDber.Dbs.SqlServerDb;

namespace CMCS.DumblyConcealer.Tasks.BeltSampler
{
	/// <summary>
	/// 皮带采样机接口业务
	/// </summary>
	public class EquBeltSamplerDAO
	{
		/// <summary>
		/// 第三方数据库访问对象
		/// </summary>
		public SqlServerDapperDber EquDber;
		/// <summary>
		/// 设备编码
		/// </summary>
		public string Type;

		/// <summary>
		/// 
		/// </summary>
		/// <param name="machineCode"></param>
		/// <param name="equDber"></param>
		public EquBeltSamplerDAO(string type, SqlServerDapperDber equDber)
		{
			this.Type = type;
			this.EquDber = equDber;
		}

		CommonDAO commonDAO = CommonDAO.GetInstance();

		/// <summary>
		/// 是否处于故障状态
		/// </summary>
		bool IsHitch = false;
		/// <summary>
		/// 上一次上位机心跳值
		/// </summary>
		string PrevHeartbeat = string.Empty;

		#region 数据转换方法
		/// <summary>
		/// 第三方接口设备编码 转换成 集中管控设备编码
		/// </summary>
		/// <param name="machineCode">接口表设备编码</param>
		/// <returns></returns>
		public string ConvertToCmcsMachineCode(string machineCode)
		{
			if (Type == "入场")
			{
				if (machineCode == ((int)eInfMachineCode.Machine1).ToString())
					return GlobalVars.MachineCode_InPDCYJ_1;
				else if (machineCode == ((int)eInfMachineCode.Machine2).ToString())
					return GlobalVars.MachineCode_InPDCYJ_2;
			}
			else if (Type == "出场")
			{
				if (machineCode == ((int)eInfMachineCode.Machine1).ToString())
					return GlobalVars.MachineCode_OutPDCYJ_1;
				else if (machineCode == ((int)eInfMachineCode.Machine2).ToString())
					return GlobalVars.MachineCode_OutPDCYJ_2;
			}
			return string.Empty;
		}

		/// <summary>
		/// 集中管控设备编码 转换成 第三方接口设备编码
		/// </summary>
		/// <param name="machineCode">集中管控设备编码</param>
		/// <returns></returns>
		public string ConvertToInfMachineCode(string machineCode)
		{
			if (machineCode == GlobalVars.MachineCode_InPDCYJ_1)
				return ((int)eInfMachineCode.Machine1).ToString();
			else if (machineCode == GlobalVars.MachineCode_InPDCYJ_2)
				return ((int)eInfMachineCode.Machine2).ToString();
			else if (machineCode == GlobalVars.MachineCode_OutPDCYJ_1)
				return ((int)eInfMachineCode.Machine1).ToString();
			else if (machineCode == GlobalVars.MachineCode_OutPDCYJ_2)
				return ((int)eInfMachineCode.Machine2).ToString();

			return string.Empty;
		}

		/// <summary>
		/// 转换成第三方接口-煤种
		/// </summary>
		/// <param name="fuelKindName">煤种</param>
		/// <returns></returns>
		public int ConvertToInfFuelKindName(string fuelKindName)
		{
			eFuelKindName enumResulr;
			if (Enum.TryParse(fuelKindName, out enumResulr))
				return (int)enumResulr;
			else
				return (int)eFuelKindName.其它;
		}

		/// <summary>
		/// 转换成第三方接口-水分
		/// </summary>
		/// <param name="mt">水分</param>
		/// <returns></returns>
		public int ConvertToInfMt(double mt)
		{
			// TODO

			return (int)eMt.干煤;
		}

		/// <summary>
		/// 转换成第三方接口-采样方式
		/// </summary>
		/// <param name="sampleType">采样方式</param>
		/// <returns></returns>
		public int ConvertToInfSampleType(string sampleType)
		{
			eEquInfSampleType enumResulr;
			if (Enum.TryParse(sampleType, out enumResulr))
				return (int)enumResulr;
			else
				return (int)eEquInfSampleType.到集样罐;
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

			foreach (EquPDCYJSignal entity in this.EquDber.Entities<EquPDCYJSignal>("where DataFlag=0"))
			{
				if (entity.TagName == GlobalVars.EquHeartbeatName) continue;

				// 当心跳检测为故障时，则不更新系统状态，保持 eSampleSystemStatus.发生故障
				if (entity.TagName == "系统" && IsHitch) continue;
				if (entity.TagName == "系统") entity.TagName = eSignalDataName.设备状态.ToString();
				if (commonDAO.SetSignalDataValue(ConvertToCmcsMachineCode(entity.MachineCode), entity.TagName, entity.TagValue))
				{
					res++;
					entity.DataFlag = 1;
					this.EquDber.Update(entity);
				}
			}
			output(string.Format("同步实时信号 {0} 条", res), eOutputType.Normal);

			return res;
		}

		/// <summary>
		/// 同步上位机运行状态 - 心跳值
		/// 每隔30s读取该值，如果数值不变化则表示设备上位机出现故障
		/// </summary>
		/// <returns></returns>
		public void SyncHeartbeatSignal()
		{
			EquPDCYJSignal pDCYSignal = this.EquDber.Entity<EquPDCYJSignal>("where TagName=@TagName", new { TagName = GlobalVars.EquHeartbeatName });
			ChangeSystemHitchStatus((pDCYSignal != null && pDCYSignal.TagValue == this.PrevHeartbeat));
		}

		/// <summary>
		/// 改变系统状态值
		/// </summary>
		/// <param name="isHitch">是否故障</param>
		public void ChangeSystemHitchStatus(bool isHitch)
		{
			IsHitch = isHitch;

			if (IsHitch)
			{
				commonDAO.SetSignalDataValue(ConvertToCmcsMachineCode(eInfMachineCode.Machine1.ToString()), eSignalDataName.设备状态.ToString(), eEquInfSamplerSystemStatus.发生故障.ToString());
				commonDAO.SetSignalDataValue(ConvertToCmcsMachineCode(eInfMachineCode.Machine2.ToString()), eSignalDataName.设备状态.ToString(), eEquInfSamplerSystemStatus.发生故障.ToString());
			}
		}

		/// <summary>
		/// 同步集样罐信息到集中管控
		/// </summary>
		/// <param name="output"></param>
		/// <returns></returns>
		public void SyncBarrel(Action<string, eOutputType> output)
		{
			int res = 0;

			List<EquPDCYJBarrel> infpdcybarrels = this.EquDber.Entities<EquPDCYJBarrel>("where DataFlag=0");
			foreach (EquPDCYJBarrel entity in infpdcybarrels)
			{
				string machineName = ConvertToCmcsMachineCode(entity.MachineCode);

				if (commonDAO.SaveEquInfSampleBarrel(new InfEquInfSampleBarrel
				{
					BarrelNumber = entity.BarrelNumber,
					BarrelStatus = entity.BarrelStatus,
					MachineCode = machineName,
					InFactoryBatchId = entity.InFactoryBatchId,
					InterfaceType = commonDAO.GetMachineInterfaceTypeByCode(machineName),
					IsCurrent = entity.IsCurrent,
					SampleCode = entity.SampleCode,
					SampleCount = entity.SampleCount,
					UpdateTime = entity.UpdateTime,
					BarrelType = entity.BarrelType
				}))
				{
					entity.DataFlag = 1;
					this.EquDber.Update(entity);

					res++;
				}
			}

			//// 生成集样罐提醒消息
			//int emptyBarrelCount = this.EquDber.Count<EquPDCYJBarrel>("where BarrelStatus=@BarrelStatus", new { BarrelStatus = eSampleBarrelStatus.空桶.ToString() });
			//if (emptyBarrelCount <= 2)
			//    commonDAO.SaveSysMessage(eMessageType.皮带采样机.ToString(), "皮带采样机集样罐空桶不超过2个!", eMessageType.皮带采样机.ToString());
			//else if (emptyBarrelCount == 0)
			//    commonDAO.SaveSysMessage(eMessageType.皮带采样机.ToString(), "皮带采样机集样罐已满!", eMessageType.皮带采样机.ToString(), "查看|取消", false);

			output(string.Format("同步集样罐记录 {0} 条", res), eOutputType.Normal);
		}

		/// <summary>
		/// 同步故障信息到集中管控
		/// </summary>
		/// <param name="output"></param>
		/// <returns></returns>
		public void SyncError(Action<string, eOutputType> output)
		{
			int res = 0;

			foreach (EquPDCYJError entity in this.EquDber.Entities<EquPDCYJError>("where DataFlag=0"))
			{
				if (commonDAO.SaveEquInfHitch(ConvertToCmcsMachineCode(entity.MachineCode), entity.ErrorTime, "故障代码 " + entity.ErrorCode + "，" + entity.ErrorDescribe, ""))
				{
					entity.DataFlag = 1;
					this.EquDber.Update(entity);

					res++;
				}
			}

			output(string.Format("同步故障信息记录 {0} 条", res), eOutputType.Normal);
		}

		/// <summary>
		/// 同步采样计划
		/// </summary>
		/// <param name="output"></param>
		/// <returns></returns>
		public void SyncPlan(Action<string, eOutputType> output)
		{
			int res = 0;

			// 集中管控 > 第三方 
			foreach (InfBeltSamplePlan entity in BeltSamplerDAO.GetInstance().GetWaitForSyncBeltSamplePlan(Type == "入场" ? GlobalVars.InterfaceType_PDCYJ_In : GlobalVars.InterfaceType_PDCYJ_Out))
			{
				bool isSuccess = false;

				EquPDCYJPlan pDCYPlan = this.EquDber.Get<EquPDCYJPlan>(entity.Id);
				if (pDCYPlan == null)
				{
					isSuccess = this.EquDber.Insert(new EquPDCYJPlan
					{
						// 保持相同的Id
						Id = entity.Id,
						CarCount = entity.CarCount,
						DataFlag = 0,
						CreateDate = entity.CreateDate,
						InFactoryBatchId = entity.InFactoryBatchId,
						Mt = ConvertToInfMt(entity.Mt),
						SampleCode = entity.SampleCode,
						GatherType = entity.GatherType,
						TicketWeight = entity.TicketWeight
					}) > 0;
				}
				else
				{
					pDCYPlan.CarCount = entity.CarCount;
					pDCYPlan.DataFlag = 0;
					pDCYPlan.CreateDate = entity.CreateDate;
					pDCYPlan.InFactoryBatchId = entity.InFactoryBatchId;
					pDCYPlan.Mt = ConvertToInfMt(entity.Mt);
					pDCYPlan.SampleCode = entity.SampleCode;
					pDCYPlan.GatherType = entity.GatherType;
					pDCYPlan.TicketWeight = entity.TicketWeight;

					isSuccess = this.EquDber.Update(pDCYPlan) > 0;
				}

				if (isSuccess)
				{
					entity.SyncFlag = 1;
					Dbers.GetInstance().SelfDber.Update(entity);

					res++;
				}
			}
			output(string.Format("同步采样计划 {0} 条（集中管控 > 第三方）", res), eOutputType.Normal);

			res = 0;
			// 第三方 > 集中管控
			foreach (EquPDCYJPlan entity in this.EquDber.Entities<EquPDCYJPlan>("where DataFlag=2 and datediff(dd,CreateDate,getdate())=0"))
			{
				InfBeltSamplePlan beltSamplePlan = Dbers.GetInstance().SelfDber.Get<InfBeltSamplePlan>(entity.Id);
				if (beltSamplePlan == null) continue;

				// 更新采样开始、结束时间、采样员等
				beltSamplePlan.StartTime = entity.StartTime;
				beltSamplePlan.EndTime = entity.EndTime;
				beltSamplePlan.SampleUser = entity.SampleUser;

				if (Dbers.GetInstance().SelfDber.Update(beltSamplePlan) > 0)
				{
					// 我方已读
					entity.DataFlag = 3;
					this.EquDber.Update(entity);

					res++;
				}
			}
			output(string.Format("同步采样计划 {0} 条（第三方 > 集中管控）", res), eOutputType.Normal);
		}

		/// <summary>
		/// 同步控制命令
		/// </summary>
		/// <param name="output"></param>
		/// <returns></returns>
		public void SyncCmd(Action<string, eOutputType> output)
		{
			int res = 0;

			// 集中管控 > 第三方 
			foreach (InfBeltSampleCmd entity in BeltSamplerDAO.GetInstance().GetWaitForSyncBeltSampleCmd(Type == "入场" ? GlobalVars.InterfaceType_PDCYJ_In : GlobalVars.InterfaceType_PDCYJ_Out))
			{
				bool isSuccess = false;

				EquPDCYJCmd pDCYControlCMD = this.EquDber.Get<EquPDCYJCmd>(entity.Id);
				if (pDCYControlCMD == null)
				{
					isSuccess = this.EquDber.Insert(new EquPDCYJCmd
					{
						// 保持相同的Id
						Id = entity.Id,
						DataFlag = 0,
						CreateDate = entity.CreateDate,
						SampleCode = entity.SampleCode,
						MachineCode = ConvertToInfMachineCode(entity.MachineCode),
						CmdCode = entity.CmdCode,
						ResultCode = eEquInfCmdResultCode.默认.ToString()
					}) > 0;
				}
				else isSuccess = true;

				if (isSuccess)
				{
					entity.SyncFlag = 1;
					Dbers.GetInstance().SelfDber.Update(entity);

					res++;
				}
			}
			output(string.Format("同步控制命令 {0} 条（集中管控 > 第三方）", res), eOutputType.Normal);

			res = 0;
			// 第三方 > 集中管控
			foreach (EquPDCYJCmd entity in this.EquDber.Entities<EquPDCYJCmd>("where DataFlag=2"))
			{
				InfBeltSampleCmd beltSampleCmd = Dbers.GetInstance().SelfDber.Get<InfBeltSampleCmd>(entity.Id);
				if (beltSampleCmd == null) continue;

				// 更新执行结果等
				beltSampleCmd.ResultCode = entity.ResultCode;
				beltSampleCmd.DataFlag = 3;

				if (Dbers.GetInstance().SelfDber.Update(beltSampleCmd) > 0)
				{
					// 我方已读
					entity.DataFlag = 3;
					this.EquDber.Update(entity);

					res++;
				}
			}
			output(string.Format("同步控制命令 {0} 条（第三方 > 集中管控）", res), eOutputType.Normal);
		}

		/// <summary>
		/// 同步卸样命令
		/// </summary>
		/// <param name="output"></param>
		/// <returns></returns>
		public void SyncUnloadCmd(Action<string, eOutputType> output)
		{
			int res = 0;

			// 集中管控 > 第三方
			foreach (InfBeltSampleUnloadCmd entity in BeltSamplerDAO.GetInstance().GetWaitForSyncBeltSampleUnloadCmd(Type == "入场" ? GlobalVars.InterfaceType_PDCYJ_In : GlobalVars.InterfaceType_PDCYJ_Out))
			{
				bool isSuccess = false;

				EquPDCYJUnloadCmd pDCYUnloadCMD = this.EquDber.Get<EquPDCYJUnloadCmd>(entity.Id);
				if (pDCYUnloadCMD == null)
				{
					isSuccess = this.EquDber.Insert(new EquPDCYJUnloadCmd
					{
						// 保持相同的Id
						Id = entity.Id,
						DataFlag = 0,
						CreateDate = entity.CreateDate,
						SampleCode = entity.SampleCode,
						MachineCode = ConvertToInfMachineCode(entity.MachineCode),
						ResultCode = eEquInfCmdResultCode.默认.ToString(),
						SamplingId = entity.SamplingId
					}) > 0;
				}
				else isSuccess = true;

				if (isSuccess)
				{
					entity.SyncFlag = 1;
					Dbers.GetInstance().SelfDber.Update(entity);

					res++;
				}
			}
			output(string.Format("同步卸样命令 {0} 条（集中管控 > 第三方）", res), eOutputType.Normal);

			res = 0;
			// 第三方 > 集中管控
			foreach (EquPDCYJUnloadCmd entity in this.EquDber.Entities<EquPDCYJUnloadCmd>("where DataFlag=2 and datediff(dd,CreateDate,getdate())=0"))
			{
				InfBeltSampleUnloadCmd beltSampleUnloadCmd = commonDAO.SelfDber.Get<InfBeltSampleUnloadCmd>(entity.Id);
				if (beltSampleUnloadCmd == null) continue;

				// 更新执行结果等
				beltSampleUnloadCmd.ResultCode = entity.ResultCode;
				beltSampleUnloadCmd.DataFlag = 3;

				if (Dbers.GetInstance().SelfDber.Update(beltSampleUnloadCmd) > 0)
				{
					// 我方已读
					entity.DataFlag = 3;
					this.EquDber.Update(entity);

					res++;
				}
			}
			output(string.Format("同步卸样命令 {0} 条（第三方 > 集中管控）", res), eOutputType.Normal);
		}

		/// <summary>
		/// 同步卸样结果，根据卸样信息生成集控采样桶记录
		/// </summary>
		/// <param name="output"></param>
		/// <returns></returns>
		public void SyncUnloadResult(Action<string, eOutputType> output)
		{
			int res = 0;

			foreach (EquPDCYJUnloadResult pDCYJUnloadResult in this.EquDber.Entities<EquPDCYJUnloadResult>("where DataFlag=0"))
			{
				InfBeltSamplerUnloadResult oldUnloadResult = commonDAO.SelfDber.Get<InfBeltSamplerUnloadResult>(pDCYJUnloadResult.Id);
				if (oldUnloadResult == null)
				{
					// 查找采样计划
					EquPDCYJPlan pDCYJPlan = this.EquDber.Entity<EquPDCYJPlan>("where SampleCode=@SampleCode", new { SampleCode = pDCYJUnloadResult.SampleCode });
					if (pDCYJPlan != null)
					{
						// 生成采样桶记录
						CmcsRCSampleBarrel rCSampleBarrel = new CmcsRCSampleBarrel()
						{
							SampleCode = pDCYJUnloadResult.SampleCode,
							BarrelCode = pDCYJUnloadResult.BarrelCode,
							BarrellingTime = pDCYJUnloadResult.UnloadTime,
							InFactoryBatchId = pDCYJPlan.InFactoryBatchId,
							SampleMachine = commonDAO.GetMachineNameByCode(ConvertToCmcsMachineCode(pDCYJUnloadResult.MachineCode)),
							SampleType = eSamplingType.皮带采样.ToString(),
							SamplingId = pDCYJUnloadResult.SamplingId
						};

						if (commonDAO.SelfDber.Insert(rCSampleBarrel) > 0)
						{
							// 同步卸样结果
							if (commonDAO.SelfDber.Insert(new InfBeltSamplerUnloadResult
							{
								MachineCode = ConvertToCmcsMachineCode(pDCYJUnloadResult.MachineCode),
								SampleCode = pDCYJUnloadResult.SampleCode,
								DataFlag = pDCYJUnloadResult.DataFlag,
								BarrelCode = pDCYJUnloadResult.BarrelCode,
								UnloadTime = pDCYJUnloadResult.UnloadTime,
								SamplingId = pDCYJUnloadResult.SamplingId
							}) > 0)
							{
								pDCYJUnloadResult.DataFlag = 1;
								this.EquDber.Update(pDCYJUnloadResult);

								res++;
							}
						}
					}
				}
			}

			output(string.Format("同步卸样结果 {0} 条", res), eOutputType.Normal);
		}
	}
}
