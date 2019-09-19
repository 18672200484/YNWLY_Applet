using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//
using CMCS.Common.Entities;
using CMCS.DapperDber.Util;
using System.Data;
using CMCS.Common.Enums;
using CMCS.Common.Entities.AutoMaker;
using CMCS.Common.Entities.Fuel;
using CMCS.Common.Entities.AutoCupboard;
using CMCS.Common.Entities.iEAA;

namespace CMCS.Common.DAO
{
    /// <summary>
    /// 存样柜业务业务
    /// </summary>
    public class AutoCupboardDAO
    {
        private static AutoCupboardDAO instance;

        public static AutoCupboardDAO GetInstance()
        {
            if (instance == null)
            {
                instance = new AutoCupboardDAO();
            }

            return instance;
        }

        private AutoCupboardDAO()
        { }

        /// <summary>
        /// 添加存样柜命令
        /// </summary>
        /// <param name="MakeCodes">制样码</param>
        /// <param name="OperType">操作类型 存样 取样</param>
        /// <param name="eopstart">操作人</param>
        /// <returns></returns>
        public String AddAutoCupboard(List<String> MakeCodes, String OperType, string OperUser, string machineCode)
        {
            InfCYGControlCMD cmcscygcontrolcmd = new InfCYGControlCMD();
            int newbill = 1;
            try
            {
                newbill = Convert.ToInt32(Dbers.GetInstance().SelfDber.Entities<InfCYGControlCMD>(" where Bill like '" + "CYG" + DateTime.Now.ToString("yyMMdd") + "%'").Max(a => a.Bill).Substring(9, 3)) + 1;
            }
            catch (Exception)
            {
            }
            cmcscygcontrolcmd.Bill = "CYG" + DateTime.Now.ToString("yyMMdd") + newbill.ToString().PadLeft(3, '0');
            cmcscygcontrolcmd.DataFlag = 0;
            cmcscygcontrolcmd.OperType = OperType;
            cmcscygcontrolcmd.OperPerson = OperUser;
            cmcscygcontrolcmd.CanWorking = "0";
            cmcscygcontrolcmd.MachineCode = machineCode;
            Dbers.GetInstance().SelfDber.Insert(cmcscygcontrolcmd);
            foreach (string item in MakeCodes)
            {
                InfCYGControlCMDDetail cmcscygcontrolcmddetail = new InfCYGControlCMDDetail();
                cmcscygcontrolcmddetail.CYGControlCMDId = cmcscygcontrolcmd.Id;
                cmcscygcontrolcmddetail.MakeCode = item;
                cmcscygcontrolcmddetail.MachineCode = machineCode;
                cmcscygcontrolcmddetail.ResultCode = eEquInfCmdResultCode.默认.ToString();
                cmcscygcontrolcmddetail.Status = "0";
                cmcscygcontrolcmddetail.Bolt_Id = "";
                Dbers.GetInstance().SelfDber.Insert(cmcscygcontrolcmddetail);
            }
            return cmcscygcontrolcmd.Id;
        }

        /// <summary>
        /// 获取存样柜执行结果
        /// </summary>
        /// <param name="makeCode"></param>
        /// <returns></returns>
        public eEquInfCmdResultCode GetAutoCupboardResult(string makeCode)
        {
            eEquInfCmdResultCode equInfCmdResult = eEquInfCmdResultCode.默认;
            InfCYGControlCMDDetail cmdDetail = Dbers.GetInstance().SelfDber.Entity<InfCYGControlCMDDetail>("where MakeCode=:MakeCode order by createdate desc", new { MakeCode = makeCode });
            if (cmdDetail != null)
            {
                Enum.TryParse(cmdDetail.ResultCode, out equInfCmdResult);
            }
            return equInfCmdResult;
        }

        /// <summary>
        /// 根据制样明细码获取化验码
        /// </summary>
        /// <param name="makeCode"></param>
        /// <returns></returns>
        public string GetAssayCodeByMakeDetailCode(string makedetailCode)
        {
            string assayCode = string.Empty;
            CmcsRCMakeDetail makedetail = CommonDAO.GetInstance().SelfDber.Entity<CmcsRCMakeDetail>("where BarrelCode=:BarrelCode order by CreateDate desc", new { BarrelCode = makedetailCode });
            if (makedetail != null)
            {
                string makeid = makedetail.TheRCMake.Id;
                CmcsRCAssay assay = CommonDAO.GetInstance().SelfDber.Entity<CmcsRCAssay>("where MakeId=:MakeId", new { MakeId = makeid });
                if (assay != null)
                    assayCode = assay.AssayCode;
            }
            return assayCode;
        }
    }
}
