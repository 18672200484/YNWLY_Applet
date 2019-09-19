using System;
using System.Text;
using DevComponents.DotNetBar.Metro;
using System.Data;
//
using CMCS.Common.DAO;
using CMCS.Common.Entities.Fuel;
using CMCS.Common.Entities.CarTransport;
using CMCS.MobilePad.Win.Frms.Sys;
using CMCS.Common.Enums;

namespace CMCS.MobilePad.Win.Frms.DataTarget
{
    public partial class FrmHome : MetroAppForm
    {
        /// <summary>
        /// 窗体唯一标识符
        /// </summary>
        public static string UniqueKey = "FrmHome";

        string SqlWhere = string.Empty;

        bool hasManagePower = false;
        /// <summary>
        /// 对否有维护权限
        /// </summary>
        public bool HasManagePower
        {
            get
            {
                return hasManagePower;
            }
            set
            {
                hasManagePower = value;
            }
        }

        public FrmHome()
        {
            InitializeComponent();
        }

        private void FrmHome_Load(object sender, EventArgs e)
        {
            timer1_Tick(null, null);
        }

        public void BindData()
        {
            #region 入场
            //绑定原料煤入场数据
            int inFactoryPlanCount = CommonDAO.GetInstance().SelfDber.Query<CmcsLMYB>("select t.*, t.rowid from FultbLMYBDetail t inner join FultbLMYB b on t.lmybid=b.id where b.Infactorytype = '原料煤入场' and trunc(b.infactorytime)=trunc(sysdate)").AsList().Count;
            int inFactoryRealCount = CommonDAO.GetInstance().SelfDber.Count<CmcsBuyFuelTransport>("where Trunc(InFactoryTime)=trunc(Sysdate) and InFactoryType='原料煤入场'");
            int inFactoryGrossCount = CommonDAO.GetInstance().SelfDber.Count<CmcsBuyFuelTransport>("where Trunc(GrossTime)=Trunc(Sysdate) and InFactoryType='原料煤入场'");
            int inFactorySampleCount = CommonDAO.GetInstance().SelfDber.Count<CmcsBuyFuelTransport>("where Trunc(SamplingTime)=Trunc(Sysdate) and InFactoryType='原料煤入场'");
            int inFactoryTareCount = CommonDAO.GetInstance().SelfDber.Count<CmcsBuyFuelTransport>("where Trunc(TareTime)=Trunc(Sysdate) and InFactoryType='原料煤入场'");
            int inFactoryOutCount = CommonDAO.GetInstance().SelfDber.Count<CmcsBuyFuelTransport>("where Trunc(OutFactoryTime)=Trunc(Sysdate) and InFactoryType='原料煤入场'");
            //int inFactoryUnloadCount = CommonDAO.GetInstance().SelfDber.Count<CmcsBuyFuelTransport>("where Trunc(UploadTime)=Trunc(Sysdate) and InFactoryType='原料煤入场'");
            int inFactoryUnloadCount = inFactoryGrossCount - inFactoryTareCount;//重车-轻车
            //int inFactoryNoCount = CommonDAO.GetInstance().SelfDber.Query<CmcsLMYB>("select t.*, t.rowid from FultbLMYBDetail t inner join FultbLMYB b on t.lmybid=b.id where b.Infactorytype = '原料煤入场' and trunc(b.infactorytime)=trunc(sysdate) and t.IsFinish='未完成'").AsList().Count;
            int inFactoryNoCount = inFactoryRealCount - inFactoryTareCount;//排队-轻车

            btn_InFactoryPlanCount.Text = btn_InFactoryPlanCount.Tag + Environment.NewLine + inFactoryPlanCount;
            btn_InFactoryRealCount.Text = btn_InFactoryRealCount.Tag + Environment.NewLine + inFactoryRealCount;
            btn_InFactoryGrossCount.Text = btn_InFactoryGrossCount.Tag + Environment.NewLine + inFactoryGrossCount;
            btn_InFactorySampleCount.Text = btn_InFactorySampleCount.Tag + Environment.NewLine + inFactorySampleCount;
            btn_InFactoryUnloadCount.Text = btn_InFactoryUnloadCount.Tag + Environment.NewLine + inFactoryUnloadCount;
            btn_InFactoryTareCount.Text = btn_InFactoryTareCount.Tag + Environment.NewLine + inFactoryTareCount;
            btn_InFactoryOutCount.Text = btn_InFactoryOutCount.Tag + Environment.NewLine + inFactoryOutCount;
            lbl_InFacoryNoCount.Text = lbl_InFacoryNoCount.Tag + inFactoryNoCount.ToString();

            //绑定仓储煤入场数据
            int inFactorySavePlanCount = CommonDAO.GetInstance().SelfDber.Query<CmcsLMYB>("select t.*, t.rowid from FultbLMYBDetail t inner join FultbLMYB b on t.lmybid=b.id where b.Infactorytype = '仓储煤入场' and trunc(b.infactorytime)=trunc(sysdate)").AsList().Count;
            int inFactorySaveRealCount = CommonDAO.GetInstance().SelfDber.Count<CmcsBuyFuelTransport>("where Trunc(InFactoryTime)=trunc(Sysdate) and InFactoryType='仓储煤入场'");
            int inFactorySaveGrossCount = CommonDAO.GetInstance().SelfDber.Count<CmcsBuyFuelTransport>("where Trunc(GrossTime)=Trunc(Sysdate) and InFactoryType='仓储煤入场'");
            int inFactorySaveSampleCount = CommonDAO.GetInstance().SelfDber.Count<CmcsBuyFuelTransport>("where Trunc(SamplingTime)=Trunc(Sysdate) and InFactoryType='仓储煤入场'");
            int inFactorySaveTareCount = CommonDAO.GetInstance().SelfDber.Count<CmcsBuyFuelTransport>("where Trunc(TareTime)=Trunc(Sysdate) and InFactoryType='仓储煤入场'");
            int inFactorySaveOutCount = CommonDAO.GetInstance().SelfDber.Count<CmcsBuyFuelTransport>("where Trunc(OutFactoryTime)=Trunc(Sysdate) and InFactoryType='仓储煤入场'");
            //int inFactorySaveUnloadCount = CommonDAO.GetInstance().SelfDber.Count<CmcsBuyFuelTransport>("where Trunc(UploadTime)=Trunc(Sysdate) and InFactoryType='仓储煤入场'");
            int inFactorySaveUnloadCount = inFactoryGrossCount - inFactoryTareCount;//重车-轻车
            //int inFactorySaveNoCount = CommonDAO.GetInstance().SelfDber.Query<CmcsLMYB>("select t.*, t.rowid from FultbLMYBDetail t inner join FultbLMYB b on t.lmybid=b.id where b.Infactorytype = '仓储煤入场' and trunc(b.infactorytime)=trunc(sysdate) and t.IsFinish='未完成'").AsList().Count;
            int inFactorySaveNoCount = inFactorySaveRealCount - inFactorySaveTareCount;

            btn_InFactorySavePlanCount.Text = btn_InFactorySavePlanCount.Tag + Environment.NewLine + inFactorySavePlanCount;
            btn_InFactorySaveRealCount.Text = btn_InFactorySaveRealCount.Tag + Environment.NewLine + inFactorySaveRealCount;
            btn_InFactorySaveGrossCount.Text = btn_InFactorySaveGrossCount.Tag + Environment.NewLine + inFactorySaveGrossCount;
            btn_InFactorySaveSampleCount.Text = btn_InFactorySaveSampleCount.Tag + Environment.NewLine + inFactorySaveSampleCount;
            btn_InFactorySaveUnloadCount.Text = btn_InFactorySaveUnloadCount.Tag + Environment.NewLine + inFactorySaveUnloadCount;
            btn_InFactorySaveTareCount.Text = btn_InFactorySaveTareCount.Tag + Environment.NewLine + inFactorySaveTareCount;
            btn_InFactorySaveOutCount.Text = btn_InFactorySaveOutCount.Tag + Environment.NewLine + inFactorySaveOutCount;
            lbl_InFactorySaveNoCount.Text = lbl_InFactorySaveNoCount.Tag + inFactorySaveNoCount.ToString();

            #endregion

            #region 出场
            //绑定销售煤出场数据
            int outFactoryPlanCount = CommonDAO.GetInstance().SelfDber.Query<CmcsLMYB>("select t.*, t.rowid from FultbLMYBDetail t inner join FultbLMYB b on t.lmybid=b.id where b.Infactorytype like '%销售%' and trunc(b.infactorytime)=trunc(sysdate)").AsList().Count;
            int outFactoryRealCount = CommonDAO.GetInstance().SelfDber.Count<CmcsSaleFuelTransport>("where Trunc(InFactoryTime)=Trunc(Sysdate) and (OutFactoryType='销售直销煤' or OutFactoryType='销售掺配煤')");
            int outFactoryGrossCount = CommonDAO.GetInstance().SelfDber.Count<CmcsSaleFuelTransport>("where Trunc(GrossTime)=Trunc(Sysdate) and (OutFactoryType='销售直销煤' or OutFactoryType='销售掺配煤')");
            int outFactorySampleCount = CommonDAO.GetInstance().SelfDber.Count<CmcsSaleFuelTransport>("where Trunc(SamplingTime)=Trunc(Sysdate) and (OutFactoryType='销售直销煤' or OutFactoryType='销售掺配煤')");
            int outFactoryTareCount = CommonDAO.GetInstance().SelfDber.Count<CmcsSaleFuelTransport>("where Trunc(TareTime)=Trunc(Sysdate) and (OutFactoryType='销售直销煤' or OutFactoryType='销售掺配煤')");
            int outFactoryOutCount = CommonDAO.GetInstance().SelfDber.Count<CmcsSaleFuelTransport>("where Trunc(OutFactoryTime)=Trunc(Sysdate) and (OutFactoryType='销售直销煤' or OutFactoryType='销售掺配煤')");
            int outFactoryOrderCount = CommonDAO.GetInstance().SelfDber.Count<CmcsSaleFuelTransport>("where Trunc(LoadTime)=Trunc(Sysdate) and (OutFactoryType='销售直销煤' or OutFactoryType='销售掺配煤')");
            //int outFactoryOrderCount = outFactoryTareCount - outFactoryGrossCount;//轻车-重车
            //int outFactoryNoCount = CommonDAO.GetInstance().SelfDber.Query<CmcsLMYB>("select t.*, t.rowid from FultbLMYBDetail t inner join FultbLMYB b on t.lmybid=b.id where b.Infactorytype like '%销售%' and trunc(b.infactorytime)=trunc(sysdate) and t.IsFinish='未完成'").AsList().Count;
            int outFactoryNoCount = outFactoryRealCount - outFactoryGrossCount;

            btn_OutFactoryPlanCount.Text = btn_OutFactoryPlanCount.Tag + Environment.NewLine + outFactoryPlanCount;
            btn_OutFactoryRealCount.Text = btn_OutFactoryRealCount.Tag + Environment.NewLine + outFactoryRealCount;
            btn_OutFactoryGrossCount.Text = btn_OutFactoryGrossCount.Tag + Environment.NewLine + outFactoryGrossCount;
            btn_OutFactorySampleCount.Text = btn_OutFactorySampleCount.Tag + Environment.NewLine + outFactorySampleCount;
            btn_OutFactoryOrderCount.Text = btn_OutFactoryOrderCount.Tag + Environment.NewLine + outFactoryOrderCount;
            btn_OutFactoryTareCount.Text = btn_OutFactoryTareCount.Tag + Environment.NewLine + outFactoryTareCount;
            btn_OutFactoryOutCount.Text = btn_OutFactoryOutCount.Tag + Environment.NewLine + outFactoryOutCount;
            lbl_OutFactoryNoCount.Text = lbl_OutFactoryNoCount.Tag + outFactoryNoCount.ToString();

            //绑定仓储煤出场数据
            int outFactorySavePlanCount = CommonDAO.GetInstance().SelfDber.Query<CmcsLMYB>("select t.*, t.rowid from FultbLMYBDetail t inner join FultbLMYB b on t.lmybid=b.id where b.Infactorytype = '仓储煤出场' and trunc(b.infactorytime)=trunc(sysdate)").AsList().Count;
            int outFactorySaveRealCount = CommonDAO.GetInstance().SelfDber.Count<CmcsSaleFuelTransport>("where Trunc(InFactoryTime)=Trunc(Sysdate) and OutFactoryType='仓储煤出场'");
            int outFactorySaveGrossCount = CommonDAO.GetInstance().SelfDber.Count<CmcsSaleFuelTransport>("where Trunc(GrossTime)=Trunc(Sysdate) and OutFactoryType='仓储煤出场'");
            int outFactorySaveSampleCount = CommonDAO.GetInstance().SelfDber.Count<CmcsSaleFuelTransport>("where Trunc(SamplingTime)=Trunc(Sysdate) and OutFactoryType='仓储煤出场'");
            int outFactorySaveTareCount = CommonDAO.GetInstance().SelfDber.Count<CmcsSaleFuelTransport>("where Trunc(TareTime)=Trunc(Sysdate) and OutFactoryType='仓储煤出场'");
            int outFactorySaveOutCount = CommonDAO.GetInstance().SelfDber.Count<CmcsSaleFuelTransport>("where Trunc(OutFactoryTime)=Trunc(Sysdate) and OutFactoryType='仓储煤出场'");
            int outFactorySaveOrderCount = CommonDAO.GetInstance().SelfDber.Count<CmcsSaleFuelTransport>("where Trunc(LoadTime)=Trunc(Sysdate) and OutFactoryType='仓储煤出场'");
            //int outFactorySaveOrderCount = outFactorySaveTareCount - outFactorySaveGrossCount;//轻车-重车
            //int outFactorySaveNoCount = CommonDAO.GetInstance().SelfDber.Query<CmcsLMYB>("select t.*, t.rowid from FultbLMYBDetail t inner join FultbLMYB b on t.lmybid=b.id where b.Infactorytype = '仓储煤出场' and trunc(b.infactorytime)=trunc(sysdate) and t.IsFinish='未完成'").AsList().Count;
            int outFactorySaveNoCount = outFactorySaveRealCount - outFactorySaveGrossCount;//排队-重车

            btn_OutFactorySavePlanCount.Text = btn_OutFactorySavePlanCount.Tag + Environment.NewLine + outFactorySavePlanCount;
            btn_OutFactorySaveRealCount.Text = btn_OutFactorySaveRealCount.Tag + Environment.NewLine + outFactorySaveRealCount;
            btn_OutFactorySaveGrossCount.Text = btn_OutFactorySaveGrossCount.Tag + Environment.NewLine + outFactorySaveGrossCount;
            btn_OutFactorySaveSampleCount.Text = btn_OutFactorySaveSampleCount.Tag + Environment.NewLine + outFactorySaveSampleCount;
            btn_OutFactorySaveOrderCount.Text = btn_OutFactorySaveOrderCount.Tag + Environment.NewLine + outFactorySaveOrderCount;
            btn_OutFactorySaveTareCount.Text = btn_OutFactorySaveTareCount.Tag + Environment.NewLine + outFactorySaveTareCount;
            btn_OutFactorySaveOutCount.Text = btn_OutFactorySaveOutCount.Tag + Environment.NewLine + outFactorySaveOutCount;
            lbl_OutFactorySaveNoCount.Text = lbl_OutFactorySaveNoCount.Tag + outFactorySaveNoCount.ToString();
            #endregion
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            try
            {
                BindData();
            }
            catch (Exception)
            {

            }
            finally
            {
                timer1.Start();
            }
        }

        #region 入场煤Button
        //原料煤
        private void lbl_InFacoryNoCount_Click(object sender, EventArgs e)
        {
            OpenFinishInFactory(eFlowStatus.未完成.ToString(), eTransportType.原料煤入场.ToString());
        }

        private void btn_InFactoryPlanCount_Click(object sender, EventArgs e)
        {
            OpenYJInFactory("入场煤");
        }

        private void btn_InFactoryRealCount_Click(object sender, EventArgs e)
        {
            OpenFinishInFactory(eFlowStatus.入场.ToString(), eTransportType.原料煤入场.ToString());
        }

        private void btn_InFactoryGrossCount_Click(object sender, EventArgs e)
        {
            OpenFinishInFactory(eFlowStatus.重车.ToString(), eTransportType.原料煤入场.ToString());
        }

        private void btn_InFactorySampleCount_Click(object sender, EventArgs e)
        {
            OpenFinishInFactory(eFlowStatus.采样.ToString(), eTransportType.原料煤入场.ToString());
        }

        private void btn_InFactoryUnloadCount_Click(object sender, EventArgs e)
        {
            OpenFinishInFactory(eFlowStatus.卸煤.ToString(), eTransportType.原料煤入场.ToString());
        }

        private void btn_InFactoryTareCount_Click(object sender, EventArgs e)
        {
            OpenFinishInFactory(eFlowStatus.轻车.ToString(), eTransportType.原料煤入场.ToString());
        }

        private void btn_InFactoryOutCount_Click(object sender, EventArgs e)
        {
            OpenFinishInFactory(eFlowStatus.出场.ToString(), eTransportType.原料煤入场.ToString());
        }

        //仓储煤
        private void lbl_InFacorySaveNoCount_Click(object sender, EventArgs e)
        {
            OpenFinishInFactory(eFlowStatus.未完成.ToString(), eTransportType.仓储煤入场.ToString());
        }

        private void btn_InFactorySavePlanCount_Click(object sender, EventArgs e)
        {
            OpenYJInFactory("入场煤");
        }

        private void btn_InFactorySaveRealCount_Click(object sender, EventArgs e)
        {
            OpenFinishInFactory(eFlowStatus.入场.ToString(), eTransportType.仓储煤入场.ToString());
        }

        private void btn_InFactorySaveGrossCount_Click(object sender, EventArgs e)
        {
            OpenFinishInFactory(eFlowStatus.重车.ToString(), eTransportType.仓储煤入场.ToString());
        }

        private void btn_InFactorySaveSampleCount_Click(object sender, EventArgs e)
        {
            OpenFinishInFactory(eFlowStatus.采样.ToString(), eTransportType.仓储煤入场.ToString());
        }

        private void btn_InFactorySaveUnloadCount_Click(object sender, EventArgs e)
        {
            OpenFinishInFactory(eFlowStatus.卸煤.ToString(), eTransportType.仓储煤入场.ToString());
        }

        private void btn_InFactorySaveTareCount_Click(object sender, EventArgs e)
        {
            OpenFinishInFactory(eFlowStatus.轻车.ToString(), eTransportType.仓储煤入场.ToString());
        }

        private void btn_InFactorySaveOutCount_Click(object sender, EventArgs e)
        {
            OpenFinishInFactory(eFlowStatus.出场.ToString(), eTransportType.仓储煤入场.ToString());
        }
        #endregion

        #region 出场煤Button
        //销售煤出场
        private void lbl_OutFactoryNoCount_Click(object sender, EventArgs e)
        {
            OpenFinishOutFactory(eFlowStatus.未完成.ToString(), "销售");
        }

        private void btn_OutFactoryPlanCount_Click(object sender, EventArgs e)
        {
            OpenYJInFactory("销售");
        }

        private void btn_OutFactoryRealCount_Click(object sender, EventArgs e)
        {
            OpenFinishOutFactory(eFlowStatus.入场.ToString(), "销售");
        }

        private void btn_OutFactoryGrossCount_Click(object sender, EventArgs e)
        {
            OpenFinishOutFactory(eFlowStatus.重车.ToString(), "销售");
        }

        private void btn_OutFactorySampleCount_Click(object sender, EventArgs e)
        {
            OpenFinishOutFactory(eFlowStatus.采样.ToString(), "销售");
        }

        private void btn_OutFactoryOrderCount_Click(object sender, EventArgs e)
        {
            OpenFinishOutFactory(eFlowStatus.装车.ToString(), "销售");
        }

        private void btn_OutFactoryTareCount_Click(object sender, EventArgs e)
        {
            OpenFinishOutFactory(eFlowStatus.轻车.ToString(), "销售");
        }

        private void btn_OutFactoryOutCount_Click(object sender, EventArgs e)
        {
            OpenFinishOutFactory(eFlowStatus.出场.ToString(), "销售");
        }

        //仓储煤出场
        private void lbl_OutFactorySaveNoCount_Click(object sender, EventArgs e)
        {
            OpenFinishOutFactory(eFlowStatus.未完成.ToString(), eTransportType.仓储煤出场.ToString());
        }

        private void btn_OutFactorySavePlanCount_Click(object sender, EventArgs e)
        {
            OpenYJInFactory(eTransportType.仓储煤出场.ToString());
        }

        private void btn_OutFactorySaveRealCount_Click(object sender, EventArgs e)
        {
            OpenFinishOutFactory(eFlowStatus.入场.ToString(), eTransportType.仓储煤出场.ToString());
        }

        private void btn_OutFactorySaveGrossCount_Click(object sender, EventArgs e)
        {
            OpenFinishOutFactory(eFlowStatus.重车.ToString(), eTransportType.仓储煤出场.ToString());
        }

        private void btn_OutFactorySaveSampleCount_Click(object sender, EventArgs e)
        {
            OpenFinishOutFactory(eFlowStatus.采样.ToString(), eTransportType.仓储煤出场.ToString());
        }

        private void btn_OutFactorySaveOrderCount_Click(object sender, EventArgs e)
        {
            OpenFinishOutFactory(eFlowStatus.装车.ToString(), eTransportType.仓储煤出场.ToString());
        }

        private void btn_OutFactorySaveTareCount_Click(object sender, EventArgs e)
        {
            OpenFinishOutFactory(eFlowStatus.轻车.ToString(), eTransportType.仓储煤出场.ToString());
        }

        private void btn_OutFactorySaveOutCount_Click(object sender, EventArgs e)
        {
            OpenFinishOutFactory(eFlowStatus.出场.ToString(), eTransportType.仓储煤出场.ToString());
        }
        #endregion

        /// <summary>
        /// 打开预计到场
        /// </summary>
        /// <param name="type"></param>
        private void OpenYJInFactory(string type)
        {
            string uniqueKey = "FrmYJInFactory";

            if (FrmMainFrame.superTabControlManager.GetTab(uniqueKey) == null)
            {
                FrmUnFinishInFactory frm = new FrmUnFinishInFactory(type, true);
                FrmMainFrame.superTabControlManager.CreateTab(frm.Text, uniqueKey, frm, true, true);
            }
            else
            {
                FrmMainFrame.superTabControlManager.GetTab(uniqueKey).Close();

                FrmUnFinishInFactory frm = new FrmUnFinishInFactory(type, true);
                FrmMainFrame.superTabControlManager.CreateTab(frm.Text, uniqueKey, frm, true, true);
            }
        }

        /// <summary>
        /// 打开未完成车辆
        /// </summary>
        /// <param name="type"></param>
        private void OpenUnFinish(string type)
        {
            string uniqueKey = FrmUnFinishInFactory.UniqueKey;

            if (FrmMainFrame.superTabControlManager.GetTab(uniqueKey) == null)
            {
                FrmUnFinishInFactory frm = new FrmUnFinishInFactory(type, false);
                FrmMainFrame.superTabControlManager.CreateTab(frm.Text, uniqueKey, frm, true, true);
            }
            else
            {
                FrmMainFrame.superTabControlManager.GetTab(uniqueKey).Close();

                FrmUnFinishInFactory frm = new FrmUnFinishInFactory(type, false);
                FrmMainFrame.superTabControlManager.CreateTab(frm.Text, uniqueKey, frm, true, true);
            }
        }

        /// <summary>
        /// 打开入场已完成
        /// </summary>
        /// <param name="type"></param>
        private void OpenFinishInFactory(string status, string inFactoryType)
        {
            string uniqueKey = FrmFinishInFactory.UniqueKey;

            if (FrmMainFrame.superTabControlManager.GetTab(uniqueKey) == null)
            {
                FrmFinishInFactory frm = new FrmFinishInFactory(status, inFactoryType);
                FrmMainFrame.superTabControlManager.CreateTab(frm.Text, uniqueKey, frm, true, true);
            }
            else
            {
                FrmMainFrame.superTabControlManager.GetTab(uniqueKey).Close();

                FrmFinishInFactory frm = new FrmFinishInFactory(status, inFactoryType);
                FrmMainFrame.superTabControlManager.CreateTab(frm.Text, uniqueKey, frm, true, true);
            }
        }

        /// <summary>
        /// 打开出场已完成
        /// </summary>
        /// <param name="type"></param>
        private void OpenFinishOutFactory(string status, string type)
        {
            string uniqueKey = FrmFinishOutFactory.UniqueKey;

            if (FrmMainFrame.superTabControlManager.GetTab(uniqueKey) == null)
            {
                FrmFinishOutFactory frm = new FrmFinishOutFactory(status, type);
                FrmMainFrame.superTabControlManager.CreateTab(frm.Text, uniqueKey, frm, true, true);
            }
            else
            {
                FrmMainFrame.superTabControlManager.GetTab(uniqueKey).Close();

                FrmFinishOutFactory frm = new FrmFinishOutFactory(status, type);
                FrmMainFrame.superTabControlManager.CreateTab(frm.Text, uniqueKey, frm, true, true);
            }
        }
    }
}
