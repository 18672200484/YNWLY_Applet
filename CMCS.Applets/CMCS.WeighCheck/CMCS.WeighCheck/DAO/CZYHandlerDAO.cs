using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Reflection;
//
using CMCS.Common;
using CMCS.Common.Entities;
using CMCS.DapperDber.Util;
using CMCS.Common.Enums;
using CMCS.Common.Entities.Fuel;
using CMCS.Common.DAO;
using CMCS.Common.Entities.iEAA;
using CMCS.CarTransport.DAO;


namespace CMCS.WeighCheck.DAO
{
	public class CZYHandlerDAO
	{
		private static CZYHandlerDAO instance;

		public static CZYHandlerDAO GetInstance()
		{
			if (instance == null)
			{
				instance = new CZYHandlerDAO();
			}

			return instance;
		}

		private CZYHandlerDAO()
		{ }

		#region 获取配置信息
		CommonDAO commonDAO = CommonDAO.GetInstance();
		#endregion

		#region 公共
		/// <summary>
		/// 更改制样送样人
		/// </summary>
		/// <param name="rCMakeDetail"></param>
		/// <param name="userName"></param>
		/// <returns></returns>
		public bool UpdateSendPle(CmcsRCMakeDetail rCMakeDetail, string userName)
		{
			CmcsRCAssay assay = Dbers.GetInstance().SelfDber.Entity<CmcsRCAssay>("where MakeId=:MakeId order by CreateDate desc", new { MakeId = rCMakeDetail.MakeId });
			if (assay != null)
			{
				assay.SendPle = userName;
				//更新制样人
				CmcsRCMake make = Dbers.GetInstance().SelfDber.Entity<CmcsRCMake>("where SamplingId=:SamplingId order by CreateDate desc", new { SamplingId = rCMakeDetail.TheRCMake.SamplingId });
				if (make != null)
				{
					make.MakePle = userName;
					Dbers.GetInstance().SelfDber.Update(make);
				}
				return Dbers.GetInstance().SelfDber.Update(assay) > 0;
			}
			return false;
		}
		#endregion

		#region 采样前样桶登记
		/// <summary>
		/// 新增采样明细
		/// </summary>
		/// <param name="sampleId"></param>
		/// <returns></returns>
		public bool SaveSampleDetail(string sampleId, double weight)
		{
			CmcsRCSampling sample = Dbers.GetInstance().SelfDber.Get<CmcsRCSampling>(sampleId);
			if (sample != null)
			{
				CmcsRCSampleBarrel sampleBarrel = new CmcsRCSampleBarrel();
				sampleBarrel.SamplingId = sampleId;
				sampleBarrel.BarrellingTime = DateTime.Now;
				sampleBarrel.SampSecondCode = CommonDAO.GetInstance().CreateSampleDetailCode(sample.SampleCode);
				sampleBarrel.BarrelWeight = weight;
				sampleBarrel.SampleCode = sample.SampleCode;
				sampleBarrel.SampleType = "人工采样";
				sampleBarrel.SampleMachine = "人工";
				return Dbers.GetInstance().SelfDber.Insert(sampleBarrel) > 0;
			}
			return false;
		}

		public bool UpdateRCSampleBarrelSampleWeight(string sampleBarrelId, double weight)
		{
			return Dbers.GetInstance().SelfDber.Execute("update " + EntityReflectionUtil.GetTableName<CmcsRCSampleBarrel>() + " set BarrelWeight=:BarrelWeight where Id=:Id", new { BarrelWeight = weight, Id = sampleBarrelId }) > 0;
		}

		/// <summary>
		/// 获取指定日期的批次
		/// </summary>
		/// <param name="startDate"></param>
		/// <param name="endDate"></param>
		/// <returns></returns>
		public IList<CmcsInFactoryBatch> GetInFactoryBatchByDate(DateTime startDate, DateTime endDate)
		{
			return Dbers.GetInstance().SelfDber.Entities<CmcsInFactoryBatch>("where BACKBATCHDATE>=:StartDate and BACKBATCHDATE<:EndDate", new { StartDate = startDate, EndDate = endDate });
		}

		/// <summary>
		/// 根据批次Id获取采样单
		/// </summary>
		/// <param name="batchId"></param>
		/// <returns></returns>
		public IList<CmcsRCSampling> GetSamplingByBatchId(string batchId)
		{
			return Dbers.GetInstance().SelfDber.Entities<CmcsRCSampling>("where InFactoryBatchId=:InFactoryBatchId order by CreateDate desc", new { InFactoryBatchId = batchId });
		}

		/// <summary>
		/// 根据批次Id获取人工采样单
		/// </summary>
		/// <param name="batchId"></param>
		/// <returns></returns>
		public IList<CmcsRCSampling> GetRGSamplingByBatchId(string batchId)
		{
			return Dbers.GetInstance().SelfDber.Entities<CmcsRCSampling>("where InFactoryBatchId=:InFactoryBatchId and SamplingType='人工采样' order by CreateDate desc", new { InFactoryBatchId = batchId });
		}
		#endregion

		#region 采样后样桶交样
		/// <summary>
		/// 查找样桶登记记录
		/// </summary>
		/// <param name="BarrelCode">样桶编码</param>
		public CmcsRCSampleBarrel GetRCSampleBarrel(string BarrelCode, out string message)
		{
			message = string.Empty;
			CmcsRCSampleBarrel entity = Dbers.GetInstance().SelfDber.Entity<CmcsRCSampleBarrel>(string.Format(" where (SampseCondCode='{0}' or SampleCode='{0}') order by BarrellingTime desc", BarrelCode));
			if (entity == null)
				message = "未找到编码【" + BarrelCode + "】的样桶登记记录";
			return entity;
		}

		/// <summary>
		/// 保存样桶登记记录(人工样 采样第一次称重)
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		public bool SaveRCSampleBarrel(CmcsRCSampleBarrel entity)
		{
			return Dbers.GetInstance().SelfDber.Insert<CmcsRCSampleBarrel>(entity) > 0;
		}

		/// <summary>
		/// 更新样桶已使用状态
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		public bool UpdateSampleBarrel(String code)
		{
			CmcsSampleBarrel entity = Dbers.GetInstance().SelfDber.Entity<CmcsSampleBarrel>(" where BarrelCode='" + code + "' ");
			if (entity != null)
			{
				entity.IsUse = 1;
				return Dbers.GetInstance().SelfDber.Update<CmcsSampleBarrel>(entity) > 0;
			}
			else
			{
				return false;
			}
		}

		/// <summary>
		/// 记录样桶交样记录
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		public bool UpdateRCSampleBarrelHandWeight(string rCSampleBarrelId, double weight, string user)
		{
			return Dbers.GetInstance().SelfDber.Execute("update " + EntityReflectionUtil.GetTableName<CmcsRCSampleBarrel>() + " set HandWeight=:HandWeight,HandDate=sysdate,HandUser=:HandUser where Id=:Id", new { HandWeight = weight, HandUser = user, Id = rCSampleBarrelId }) > 0;
		}

		/// <summary>
		/// 获取采样单信息
		/// </summary>
		/// <param name="dtStart">开始时间</param>
		/// <param name="dtEnd">结束时间</param>
		/// <returns></returns>
		public DataTable GetSampleInfo(DateTime dtStart, DateTime dtEnd)
		{
			string sql = @" select a.batch,a.id as batchid,a.infactorytype,
                                 b.name as suppliername,
                                 f.name as fuelsuppliername,
                                 c.name as minename,
                                 d.fuelname as kindname,
                                 e.name as stationname,
                                 a.factarrivedate,
                                 t.id,
                                 t.samplecode,
                                 t.samplingdate,
                                 t.samplingtype
                            from cmcstbrcsampling t
                            left join fultbinfactorybatch a on t.infactorybatchid = a.id
                            left join fultbsupplier b on a.supplierid = b.id
                            left join fultbmine c on a.mineid = c.id
                            left join fultbfuelkind d on a.fuelkindid = d.id
                            left join fultbstationinfo e on a.stationid = e.id
                            left join fultbsupplier f on a.fuelsupplierid=f.id
                       where a.id is not null and t.samplingdate >= '" + dtStart + "' and t.samplingdate < '" + dtEnd + "'";
			return Dbers.GetInstance().SelfDber.ExecuteDataTable(sql);
		}

		/// <summary>
		/// 获取采样单信息
		/// </summary>
		/// <param name="sampleId">采样Id</param>
		/// <returns></returns>
		public DataTable GetSampleInfo(string sampleId)
		{
			string sql = @" select a.batch,a.id as batchid,a.infactorytype,
                                 b.name as suppliername,
                                 f.name as fuelsuppliername,
                                 c.name as minename,
                                 d.fuelname as kindname,
                                 e.name as stationname,
                                 a.factarrivedate,
                                 t.id,
                                 t.samplecode,
                                 t.samplingdate,
                                 t.samplingtype
                            from cmcstbrcsampling t
                            left join fultbinfactorybatch a on t.infactorybatchid = a.id
                            left join fultbsupplier b on a.supplierid = b.id
                            left join fultbmine c on a.mineid = c.id
                            left join fultbfuelkind d on a.fuelkindid = d.id
                            left join fultbstationinfo e on a.stationid = e.id
                            left join fultbsupplier f on a.fuelsupplierid=f.id
                       where a.id is not null and t.Id= '" + sampleId + "'";
			return Dbers.GetInstance().SelfDber.ExecuteDataTable(sql);
		}

		/// <summary>
		/// 保存交接样记录的交样
		/// </summary>
		/// <returns></returns>
		public bool SaveHandSamplingSend(string sampleId, string samplingSendPle, DateTime samplingSendDate)
		{
			CmcsRCHandSampling handSampling = Dbers.GetInstance().SelfDber.Entity<CmcsRCHandSampling>("where SamplingId=:SamplingId order by CreateDate desc", new { SamplingId = sampleId });
			if (handSampling == null)
			{
				handSampling = new CmcsRCHandSampling();
				handSampling.SamplingId = sampleId;
				handSampling.SamplingSendPle = samplingSendPle;
				handSampling.SamplingSendDate = samplingSendDate;
				handSampling.Remark = "人工制样间自动生成";
				return Dbers.GetInstance().SelfDber.Insert(handSampling) > 0;
			}
			handSampling.SamplingSendPle = samplingSendPle;
			handSampling.SamplingSendDate = samplingSendDate;
			return Dbers.GetInstance().SelfDber.Update(handSampling) > 0;
		}

		/// <summary>
		/// 保存交接样记录的交样
		/// </summary>
		/// <returns></returns>
		public bool SaveHandSamplingReceive(string sampleId, string makeReceivePle, DateTime makeReceiveDate)
		{
			CmcsRCHandSampling handSampling = Dbers.GetInstance().SelfDber.Entity<CmcsRCHandSampling>("where SamplingId=:SamplingId order by CreateDate desc", new { SamplingId = sampleId });
			if (handSampling == null)
			{
				handSampling = new CmcsRCHandSampling();
				handSampling.SamplingId = sampleId;
				handSampling.MakeReceivePle = makeReceivePle;
				handSampling.MakeReceiveDate = makeReceiveDate;
				CmcsRCMake make = Dbers.GetInstance().SelfDber.Entity<CmcsRCMake>("where SamplingId=:SamplingId order by Createdate desc", new { SamplingId = sampleId });
				if (make != null)
				{
					make.IsHandOver = 1;
					Dbers.GetInstance().SelfDber.Update(make);
				}
				return Dbers.GetInstance().SelfDber.Insert(handSampling) > 0;
			}
			handSampling.MakeReceivePle = makeReceivePle;
			handSampling.MakeReceiveDate = makeReceiveDate;
			return Dbers.GetInstance().SelfDber.Update(handSampling) > 0;
		}

		#endregion

		#region 制样前样桶称重校验（接样）
		/// <summary>
		/// 根据入厂煤采样单Id获取制样记录
		/// </summary>
		/// <param name="sampleId">采样单Id</param>
		/// <returns></returns>
		public CmcsRCMake GetRCMakeBySampleId(string sampleId)
		{
			return Dbers.GetInstance().SelfDber.Entity<CmcsRCMake>("where SamplingId=:SamplingId", new { SamplingId = sampleId });
		}

		/// <summary>
		/// 查找同一采样单的样桶登记记录
		/// </summary>
		/// <param name="BarrelCode"></param>
		/// <returns></returns>
		public List<CmcsRCSampleBarrel> GetRCSampleBarrels(string BarrelCode, out string message)
		{
			message = string.Empty;
			List<CmcsRCSampleBarrel> list = new List<CmcsRCSampleBarrel>();
			CmcsRCSampleBarrel entity = GetRCSampleBarrel(BarrelCode, out message);
			if (entity != null)
			{
				list = Dbers.GetInstance().SelfDber.Entities<CmcsRCSampleBarrel>(" where SamplingId='" + entity.SamplingId + "'");
				message = "扫码成功，该批次采样桶共" + list.Count + "桶";
			}
			return list;
		}

		/// <summary>
		/// 记录样桶校验记录
		/// </summary>
		/// <param name="entity"></param>
		/// <returns></returns>
		public bool UpdateRCSampleBarrelCheckSampleWeight(string rCSampleBarrelId, double weight, string user)
		{
			if (weight > 0)
				return Dbers.GetInstance().SelfDber.Execute("update " + EntityReflectionUtil.GetTableName<CmcsRCSampleBarrel>() + " set CheckSampleWeight=:CheckSampleWeight,CheckDate=sysdate,CheckUser=:CheckUser,SampleWeight=(:CheckSampleWeight-BarrelWeight) where Id=:Id", new { CheckSampleWeight = weight, CheckUser = user, Id = rCSampleBarrelId }) > 0;
			else
				return Dbers.GetInstance().SelfDber.Execute("update " + EntityReflectionUtil.GetTableName<CmcsRCSampleBarrel>() + " set CheckDate=sysdate,CheckUser=:CheckUser where Id=:Id", new { CheckUser = user, Id = rCSampleBarrelId }) > 0;
		}
		#endregion

		#region 制样后样品称重登记
		/// <summary>
		/// 根据制样码获取制样主表信息
		/// </summary>
		/// <param name="makeCode">制样码</param>
		/// <returns></returns>
		public CmcsRCMake GetRCMake(string makeCode)
		{
			return Dbers.GetInstance().SelfDber.Entity<CmcsRCMake>(" where MakeCode=:MakeCode order by CreateDate desc", new { MakeCode = makeCode });
		}

		/// <summary>
		/// 根据制样码获取制样从表明细集合
		/// </summary>
		/// <param name="makeCode">制样码</param>
		/// <param name="message"></param>
		/// <returns></returns>
		public List<CmcsRCMakeDetail> GetRCMakeDetails(string makeCode, out string message)
		{
			message = "";

			List<CmcsRCMakeDetail> list = new List<CmcsRCMakeDetail>();
			CmcsRCMake rcmake = GetRCMake(makeCode);
			if (rcmake != null)
			{
				list = Dbers.GetInstance().SelfDber.Entities<CmcsRCMakeDetail>(" where MakeId=:MakeId order by CreateDate asc", new { MakeId = rcmake.Id });
				if (list.Count == 0) message = "未找到制样明细记录";
			}
			else
				message = "未找到制样记录，制样码：" + makeCode;

			return list;
		}

		/// <summary> 
		/// 更新制样明细记录的样重和样罐编码
		/// </summary>
		/// <param name="rCMakeDetailId">制样明细记录Id</param>
		/// <param name="weight">重量</param>
		/// <param name="barrelCode">样罐编码</param>
		/// <returns></returns>
		public bool UpdateMakeDetailWeightAndBarrelCode(string rCMakeDetailId, double weight, string barrelCode)
		{
			return Dbers.GetInstance().SelfDber.Execute("update " + EntityReflectionUtil.GetTableName<CmcsRCMakeDetail>() + " set Weight=:Weight,BarrelCode=:BarrelCode where Id=:Id", new { Id = rCMakeDetailId, Weight = weight, BarrelCode = barrelCode }) > 0;
		}

		/// <summary>
		/// 生成制样及化验
		/// </summary>
		/// <param name="makeCode"></param>
		/// <param name="assayType"></param>
		/// <param name="user"></param>
		/// <param name="assayTarget"></param>
		/// <returns></returns>
		public bool CreateMakeAndAssay(ref string makeCode, string assayType, string user, string assayTarget)
		{
			bool isSuccess = false;
			CmcsRCMake make = Dbers.GetInstance().SelfDber.Entity<CmcsRCMake>("where MakeCode=:MakeCode order by CreateDate desc ", new { MakeCode = makeCode });
			if (make != null)
			{
				CmcsRCSampling rCSampling = Dbers.GetInstance().SelfDber.Get<CmcsRCSampling>(make.SamplingId);
				if (rCSampling != null)
				{
					// 入厂煤制样
					CmcsRCMake rCMake = new CmcsRCMake()
					{
						SamplingId = rCSampling.Id,
						MakeStyle = "机器制样",
						MakeType = assayType,
						MakeStartTime = DateTime.Now,
						MakeEndTime = DateTime.Now,
						MakeCode = CommonDAO.GetInstance().CreateNewMakeCode(DateTime.Now),
						MakePle = user,
						ParentMakeId = make.Id
					};

					isSuccess = Dbers.GetInstance().SelfDber.Insert(rCMake) > 0;
					makeCode = rCMake.MakeCode;
					//入厂煤制样明细  
					foreach (CodeContent item in CommonDAO.GetInstance().GetCodeContentByKind("制样类型"))
					{
						CmcsRCMakeDetail rCMakeDetail = Dbers.GetInstance().SelfDber.Entity<CmcsRCMakeDetail>("where MakeId=:MakeId and SampleType=:SampleType", new { MakeId = rCMake.Id, SampleType = item.Content });
						if (rCMakeDetail == null)
						{
							rCMakeDetail = new CmcsRCMakeDetail();
							rCMakeDetail.MakeId = rCMake.Id;
							rCMakeDetail.BarrelCode = CommonDAO.GetInstance().CreateNewMakeBarrelCodeByMakeCode(rCMake.MakeCode, item.Content);
							rCMakeDetail.SampleType = item.Content;
							isSuccess = Dbers.GetInstance().SelfDber.Insert(rCMakeDetail) > 0;
						}
					}
					// 入厂煤化验
					CmcsRCAssay rCAssay = Dbers.GetInstance().SelfDber.Entity<CmcsRCAssay>("where MakeId=:MakeId", new { MakeId = rCMake.Id });
					if (rCAssay == null)
					{
						// 入厂煤煤质

						CmcsFuelQuality fuelQuality = new CmcsFuelQuality()
						{
							Id = Guid.NewGuid().ToString()
						};

						if (Dbers.GetInstance().SelfDber.Insert(fuelQuality) > 0)
						{
							rCAssay = new CmcsRCAssay()
							{
								MakeId = rCMake.Id,
								AssayType = assayType,
								AssayWay = assayType,
								AssayCode = CommonDAO.GetInstance().CreateNewAssayCode(rCMake.CreateDate),
								InFactoryBatchId = rCSampling.InFactoryBatchId,
								FuelQualityId = fuelQuality.Id,
								AssayDate = rCMake.CreateDate,
								WfStatus = 0,
								AssayPoint = assayTarget
							};

							isSuccess = Dbers.GetInstance().SelfDber.Insert(rCAssay) > 0;
						}
					}
				}
			}
			return isSuccess;
		}
		#endregion

		#region 化验前样品称重校验
		/// <summary>
		/// 获取制样从表明细记录
		/// </summary>
		/// <param name="MakeCode">制样码</param>
		/// <returns></returns>
		public CmcsRCMakeDetail GetRCMakeDetail(string barrelCode, out string message)
		{
			CmcsRCMakeDetail rcMakeDetail = null;
			try
			{
				rcMakeDetail = Dbers.GetInstance().SelfDber.Entity<CmcsRCMakeDetail>(" where BarrelCode=:BarrelCode order by CreateDate desc", new { BarrelCode = barrelCode });
				if (rcMakeDetail == null)
				{
					message = "未找到【" + barrelCode + "】制样登记记录";
					return null;
				}
			}
			catch (Exception ex)
			{
				message = "程序异常！" + ex.Message;
				return null;
			}
			message = "扫码成功";
			return rcMakeDetail;
		}

		/// <summary>
		/// 根据制样明细Id查找化验记录
		/// </summary>
		/// <param name="rCMakeDetailId">制样明细Id</param>
		/// <returns></returns>
		public CmcsRCAssay GetRCAssayByMakeCode(string rCMakeDetailId)
		{
			CmcsRCMakeDetail rCMakeDetail = Dbers.GetInstance().SelfDber.Get<CmcsRCMakeDetail>(rCMakeDetailId);
			if (rCMakeDetail != null)
			{
				// 三级编码化验查询
				if (rCMakeDetail.SampleType == eMakeSampleType.Type1 || rCMakeDetail.SampleType == eMakeSampleType.Type3)
					return Dbers.GetInstance().SelfDber.Entity<CmcsRCAssay>("where AssayType=:AssayType and MakeId=:MakeId order by CreateDate desc", new { AssayType = eAssayType.三级编码化验.ToString(), MakeId = rCMakeDetail.MakeId });
				// 不同类型的化验查询
				//else if(rCMakeDetail.SampleType==eMakeSampleType.Type2)
			}

			return null;
		}

		/// <summary> 
		/// 更新制样明细记录的校验样重
		/// </summary>
		/// <param name="rCMakeDetailId">制样明细记录Id</param>
		/// <param name="weight">重量</param>
		/// <returns></returns>
		public bool UpdateMakeDetailCheckWeight(string rCMakeDetailId, double weight)
		{
			return Dbers.GetInstance().SelfDber.Execute("update " + EntityReflectionUtil.GetTableName<CmcsRCMakeDetail>() + " set CheckWeight=:CheckWeight where Id=:Id", new { Id = rCMakeDetailId, CheckWeight = weight }) > 0;
		}
		#endregion

		#region 编码转换
		/// <summary>
		/// 制样码转换化验码
		/// </summary>
		/// <param name="makeCode"></param>
		/// <returns></returns>
		public string MakeCodeToAssayCode(string makeCode)
		{
			CmcsRCMake make = Dbers.GetInstance().SelfDber.Entity<CmcsRCMake>("where MakeCode=:MakeCode", new { MakeCode = makeCode });
			if (make == null) return string.Empty;
			CmcsRCAssay assay = Dbers.GetInstance().SelfDber.Entity<CmcsRCAssay>("where MakeId=:MakeId", new { MakeId = make.Id });
			if (assay != null) return assay.AssayCode;
			return string.Empty;
		}

		/// <summary>
		/// 制样明细码转换化验码
		/// </summary>
		/// <param name="makeCode"></param>
		/// <returns></returns>
		public string MakeDetailCodeToAssayCode(string makeDetailCode)
		{
			CmcsRCMakeDetail makeDetail = Dbers.GetInstance().SelfDber.Entity<CmcsRCMakeDetail>("where BarrelCode=:BarrelCode order by CreateDate desc", new { BarrelCode = makeDetailCode });
			if (makeDetail != null)
			{
				CmcsRCAssay assay = Dbers.GetInstance().SelfDber.Entity<CmcsRCAssay>("where MakeId=:MakeId", new { MakeId = makeDetail.MakeId });
				if (assay != null) return assay.AssayCode;
			}

			return string.Empty;
		}

		/// <summary>
		/// 制样码(主码/明细码)转换化验码
		/// </summary>
		/// <param name="makeCode"></param>
		/// <returns></returns>
		public string MakeBillNumberToAssayCode(string makeCode)
		{
			if (makeCode.Length == 16)
			{
				return MakeDetailCodeToAssayCode(makeCode);
			}
			else
			{
				return MakeCodeToAssayCode(makeCode);
			}
		}

		/// <summary>
		/// 根据制样码获取制样明细
		/// </summary>
		/// <param name="makeCode"></param>
		/// <returns></returns>
		public IList<CmcsRCMakeDetail> GetMakeDetailsByMakeBillNumber(string makeCode, out string message)
		{
			message = "";

			IList<CmcsRCMakeDetail> details = Dbers.GetInstance().SelfDber.Entities<CmcsRCMakeDetail>("where BarrelCode=:BarrelCode order by CreateDate desc ", new { BarrelCode = makeCode });
			if (details == null || details.Count == 0)
			{
				CmcsRCMake make = Dbers.GetInstance().SelfDber.Entity<CmcsRCMake>("where MakeCode=:MakeCode", new { MakeCode = makeCode });
				if (make != null)
				{
					details = Dbers.GetInstance().SelfDber.Entities<CmcsRCMakeDetail>("where MakeId=:MakeId", new { MakeId = make.Id });
					if (details.Count == 0) message = "未找到制样明细记录";
				}
				else
					message = "未找到制样记录，制样码：" + makeCode;
			}
			return details;
		}

		/// <summary>
		/// 根据制样明细码获取制样明细
		/// </summary>
		/// <param name="makeCode"></param>
		/// <returns></returns>
		public CmcsRCMakeDetail GetMakeDetailByMakeBillNumber(string makeCode)
		{
			return Dbers.GetInstance().SelfDber.Entity<CmcsRCMakeDetail>("where BarrelCode=:BarrelCode order by CreateDate desc ", new { BarrelCode = makeCode });
		}

		/// <summary>
		/// 解绑化验单及化验操作
		/// </summary>
		/// <param name="makeCode"></param>
		/// <returns></returns>
		public bool RelieveAssay(CmcsRCMakeDetail makeDetail, string makeCreateUser, string assayCheckUser)
		{
			CmcsRCAssay assay = Dbers.GetInstance().SelfDber.Entity<CmcsRCAssay>("where MakeId=:MakeId order by CreateDate desc", new { MakeId = makeDetail.MakeId });
			if (assay != null)
			{
				assay.IsRelieve = 1;

				assay.GetPle = assayCheckUser;
				assay.SendPle = makeCreateUser;
				if (assay.AssayType == "三级编码化验" && makeDetail.SampleType.Contains("6mm"))
					assay.SendDate = GlobalVars.ServerNowDateTime;
				else if ((assay.AssayType == "复查样化验" || assay.AssayType == "抽查样化验") && makeDetail.SampleType.Contains("0.2mm"))
					assay.SendDate = DateTime.Now;
				if (makeDetail.SampleType.Contains("0.2mm") && assay.GetDate.Year < 2000)
				{
					assay.GetDate = GlobalVars.ServerNowDateTime;
				}
				if (makeDetail.PrintCount == 0)
				{
					if (string.IsNullOrEmpty(assay.GetPle))
						assay.GetPle = assayCheckUser;
					else
						assay.GetPle += "," + assayCheckUser;
				}
				return Dbers.GetInstance().SelfDber.Update<CmcsRCAssay>(assay) > 0;
			}
			return false;
		}

		/// <summary>
		/// 根据制样码获取化验记录
		/// </summary>
		/// <param name="makeId"></param>
		/// <returns></returns>
		public CmcsRCAssay GetAssayByMakeId(string makeId)
		{
			return Dbers.GetInstance().SelfDber.Entity<CmcsRCAssay>("where MakeId=:MakeId", new { MakeId = makeId });
		}

		/// <summary>
		/// 根据制样码获取化验记录
		/// </summary>
		/// <param name="makeId"></param>
		/// <returns></returns>
		public IList<CmcsRCAssay> GetAssaysByMakeId(string makeId)
		{
			return Dbers.GetInstance().SelfDber.Entities<CmcsRCAssay>("where MakeId=:MakeId order by CreateDate desc", new { MakeId = makeId });
		}

		/// <summary>
		/// 更改验证状态
		/// </summary>
		/// <param name="makeDetailId"></param>
		/// <param name="isCheck"></param>
		/// <param name="CheckUser"></param>
		/// <returns></returns>
		public bool ChangeCheckStatus(string makeDetailId, bool isCheck, string CheckUser)
		{
			CmcsRCMakeDetail rCMakeDetail = Dbers.GetInstance().SelfDber.Get<CmcsRCMakeDetail>(makeDetailId);
			if (rCMakeDetail != null)
			{
				rCMakeDetail.IsCheck = 1;
				rCMakeDetail.CheckUser = CheckUser;
				return Dbers.GetInstance().SelfDber.Update(rCMakeDetail) > 0;
			}
			return false;
		}

		/// <summary>
		/// 根据化验码转换制样码
		/// </summary>
		/// <param name="assayCode"></param>
		/// <returns></returns>
		public string GetMakeCodeByAssayCode(string assayCode)
		{
			string makeCode = string.Empty;
			CmcsRCAssay rCAssay = Dbers.GetInstance().SelfDber.Entity<CmcsRCAssay>("where AssayCode=:AssayCode", new { AssayCode = assayCode });
			if (rCAssay != null)
			{
				CmcsRCMake rCMake = Dbers.GetInstance().SelfDber.Get<CmcsRCMake>(rCAssay.MakeId);
				if (rCMake != null)
					makeCode = rCMake.MakeCode;
			}
			return makeCode;
		}

		/// <summary>
		/// 根据化验码获取化验指标
		/// </summary>
		/// <param name="assayCode"></param>
		/// <returns></returns>
		public string GetAssayPointByAssayCode(string assayCode)
		{
			string assayPoint = string.Empty;
			CmcsRCAssay rCAssay = Dbers.GetInstance().SelfDber.Entity<CmcsRCAssay>("where AssayCode=:AssayCode", new { AssayCode = assayCode });
			if (rCAssay != null)
			{
				assayPoint = rCAssay.AssayPoint;
			}
			return assayPoint;
		}

		#region 抽查样
		/// <summary>
		/// 检测是否能生成抽查样
		/// </summary>
		/// <param name="assayCode"></param>
		/// <param name="message"></param>
		/// <returns></returns>
		public bool CheckSpotAssay(string assayCode, ref string message)
		{
			CmcsRCAssay assay = CommonDAO.GetInstance().SelfDber.Entity<CmcsRCAssay>("where AssayCode=:AssayCode order by CreateDate desc", new { AssayCode = assayCode });
			if (assay == null)
			{
				message = "未找到此化验编码对应的化验记录！";
				return false;
			}
			if (assay.TheRcMake == null)
			{
				message = "未找到此化验编码对应的化验记录！";
				return false;
			}
			if (assay == null)
			{
				message = "未找到此化验编码对应的制样记录！";
				return false;
			}
			if (assay.TheRcMake != null && assay.TheRcMake.StayStatus == 1)
			{
				message = "此化验编码对应的制样记录为留样，不能进行操作！";
				return false;
			}
			if (assay.WfStatus != 2)
			{
				message = "此化验编码对应的化验记录必须流程结束后才可以进行此操作！";
				return false;
			}
			if (assay.AssayWay == "煤场化验")
			{
				message = "此化验编码对应的化验记录为煤场化验，不能进行此操作！";
				return false;
			}
			if (assay.AssayWay == "抽查样化验")
			{
				message = "此记录是抽查样化验，不能进行抽查样操作！";
				return false;
			}

			return true;
		}

		/// <summary>
		/// 生成抽查样
		/// </summary>
		/// <param name="assay">原化验记录</param>
		/// <param name="UserName">操作人</param>
		/// <param name="assayPoint">化验指标</param>
		/// <param name="PreFix">化验类型</param>
		/// <returns></returns>
		public bool CreateSpotAssay(CmcsRCAssay entity, string userName, string userAccount, string assayPoint, string PreFix, ref CmcsRCAssay assay)
		{
			try
			{
				assay = new CmcsRCAssay();
				assay.AssayPoint = assayPoint;
				assay.AssayType = "三级编码化验";
				assay.SendDate = GlobalVars.ServerNowDateTime;
				assay.AssayDate = GlobalVars.ServerNowDateTime;
				CmcsRCMake make = entity.TheRcMake;
				if (make.TheRcSampling != null && make.TheRcSampling.TheInFactoryBatch != null)
					assay.InFactoryBatchId = make.TheRcSampling.TheInFactoryBatch.Id;
				assay.MakeId = make.Id;
				assay.IsRelieve = 1;
				assay.AssayPle = userName;
				assay.AssayCode = CommonDAO.GetInstance().CreateNewAssayCode(DateTime.Now);
				assay.AssayWay = PreFix;
				assay.Remark = "由化验室接样程序手动生成";
				assay.ParentId = entity.Id;
				assay.BackBatchDate = entity.BackBatchDate;
				CmcsFuelQuality quality_new = new CmcsFuelQuality();
				CmcsRCAssay assay_new = CommonDAO.GetInstance().SelfDber.Entity<CmcsRCAssay>("where MakeId=:MakeId order by CreateDate desc", new { MakeId = make.Id });
				if (assay_new != null)
					quality_new = (CmcsFuelQuality)Clone(assay_new.TheFuelQuality);
				quality_new.Id = Guid.NewGuid().ToString();
				quality_new.CreateUser = userAccount;
				quality_new.OperUser = userAccount;

				assay.FuelQualityId = quality_new.Id;
				Dbers.GetInstance().SelfDber.Insert(quality_new);
				Dbers.GetInstance().SelfDber.Insert(assay);

				return true;
			}
			catch (Exception)
			{
				return false;
			}
		}

		/// <summary>
		/// 根据化验编码获取抽查样编码
		/// </summary>
		/// <param name="assayCode"></param>
		/// <returns></returns>
		public string GetSpotAssayCodeByAssayCode(string assayCode)
		{
			CmcsRCAssay entity = commonDAO.SelfDber.Entity<CmcsRCAssay>("where AssayCode=:AssayCode order by CreateDate desc", new { AssayCode = assayCode });
			if (entity != null)
			{
				CmcsRCAssay entity_spot = commonDAO.SelfDber.Entity<CmcsRCAssay>("where MakeId=:MakeId and AssayWay='抽查样化验' order by CreateDate desc", new { MakeId = entity.TheRcMake.Id });
				if (entity_spot != null)
					return entity_spot.AssayCode;
			}
			return "";
		}

		/// <summary>
		/// 获取抽查样记录
		/// </summary>
		/// <param name="date"></param>
		/// <param name="assayCode"></param>
		/// <returns></returns>
		public IList<CmcsRCAssay> GetSpotAssayByDate(DateTime date, string assayCode = "")
		{
			string sqlWhere = "where AssayWay='抽查样化验' ";
			sqlWhere += "and CreateDate>=:CreateDate";
			if (!string.IsNullOrWhiteSpace(assayCode))
				sqlWhere += string.Format(" and AssayCode='{0}'", assayCode);
			sqlWhere += " order by CreateDate desc ";
			return commonDAO.SelfDber.Entities<CmcsRCAssay>(sqlWhere, new { CreateDate = date.Date });
		}

		/// <summary>
		/// 根据制样码获取化验记录
		/// </summary>
		/// <param name="makeId"></param>
		/// <returns></returns>
		public CmcsRCAssay GetAssayBySpotMakeId(string makeId)
		{
			return Dbers.GetInstance().SelfDber.Entity<CmcsRCAssay>("where MakeId=:MakeId and AssayWay !='抽查样化验' order by CreateDate ", new { MakeId = makeId });
		}

		/// <summary>
		/// 根据抽查样制样ID获取抽查样次数
		/// </summary>
		/// <param name="makeId"></param>
		/// <returns></returns>
		public string GetSpotCountBySpotMakeId(string makeId, string spotAssayCode)
		{
			string result = string.Empty;
			string sql = string.Format("select * from {0} where MakeId='{1}' and AssayWay='抽查样化验' order by AssayDate ", CMCS.DapperDber.Util.EntityReflectionUtil.GetTableName<CmcsRCAssay>(), makeId);
			DataTable data = Dbers.GetInstance().SelfDber.ExecuteDataTable(sql);
			int count = 1;
			foreach (DataRow item in data.Rows)
			{
				if (item["AssayCode"].ToString() == spotAssayCode)
				{
					result = string.Format("第{0}次抽查", count.ToString());
				}
				count++;
			}
			return result;
		}

		#endregion
		/// <summary>
		/// 克隆一个对象
		/// </summary>
		/// <param name="sampleObject"></param>
		/// <returns></returns>
		public static object Clone(object sampleObject)
		{
			Type t = sampleObject.GetType();
			PropertyInfo[] properties = t.GetProperties();
			object p = t.InvokeMember("", BindingFlags.CreateInstance, null, sampleObject, null);
			foreach (PropertyInfo pi in properties)
			{
				if (pi.CanWrite)
				{
					object value = pi.GetValue(sampleObject, null);
					pi.SetValue(p, value, null);
				}
			}
			return p;
		}
		#endregion
	}
}
