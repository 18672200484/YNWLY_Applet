using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CMCS.Common.DAO;
using CMCS.WeighCheck.SampleMake.Frms;
using DevComponents.DotNetBar.Controls;
using DevComponents.DotNetBar.Metro;
using DevComponents.DotNetBar.SuperGrid;
using CMCS.WeighCheck.DAO;
using CMCS.Common.Entities.Fuel;
using DevComponents.DotNetBar;
using CMCS.Common.Enums;
using CMCS.Common.Entities.BaseInfo;
using CMCS.WeighCheck.SampleMake.Utilities;

namespace CMCS.WeighCheck.SampleMake.Frms.Sample
{
    public partial class FrmBatchSelect : MetroForm
    {
        CZYHandlerDAO cZYHandlerDAO = CZYHandlerDAO.GetInstance();
        public FrmSampleWeigth _Form1;
        bool isExit = true;
        List<SampleInfo> listSampleInfo = new List<SampleInfo>();

        /// <summary>
        /// 当前日期
        /// </summary>
        DateTime CurrentDay = DateTime.Now;


        public FrmBatchSelect()
        {
            InitializeComponent();
        }

        private void Frm_Batch_Select_Load(object sender, EventArgs e)
        {
            _Form1 = this.Owner as FrmSampleWeigth;
            dtInputStart.Value = dtInputEnd.Value = DateTime.Now;

            BindData();
        }

        private void BindData()
        {
            listSampleInfo.Clear();

            IList<CmcsInFactoryBatch> listBatchInfo = cZYHandlerDAO.GetInFactoryBatchByDate(DateTime.Parse(dtInputStart.Text), DateTime.Parse(dtInputEnd.Text).AddDays(1));
            superGridControl1.PrimaryGrid.DataSource = listBatchInfo;
        }

        private void lvwInfactoryBatch_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewEx lvw = sender as ListViewEx;
            if (lvw != null)
            {
                ListViewItem item = lvw.GetItemAt(e.X, e.Y);
            }
        }

        private void Frm_SupplierUnit_Selet_FormClosing(object sender, FormClosingEventArgs e)
        {
            //if (isExit)
            //    _Form1.CurrentSampleInfo = null;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(dtInputStart.Text))
                return;
            if (string.IsNullOrEmpty(dtInputEnd.Text))
                return;

            BindData();
        }

        private void btnPreDay_Click(object sender, EventArgs e)
        {
            CurrentDay = CurrentDay.AddDays(-1);
            ShowDateTime();
            BindData();
        }

        private void btnNextDay_Click(object sender, EventArgs e)
        {
            CurrentDay = CurrentDay.AddDays(1);
            ShowDateTime();
            BindData();
        }

        private void btnToday_Click(object sender, EventArgs e)
        {
            CurrentDay = DateTime.Now;
            ShowDateTime();
            BindData();
        }

        #region DateTime
        private void ShowDateTime()
        {
            dtInputStart.Value = dtInputEnd.Value = DateTime.Parse(CurrentDay.ToString("yyyy-MM-dd"));
        }
        #endregion

        #region DataGridView
        private void superGridControl1_BeginEdit(object sender, DevComponents.DotNetBar.SuperGrid.GridEditEventArgs e)
        {
            // 取消编辑模式
            e.Cancel = true;
        }

        private void superGridControl1_CellDoubleClick(object sender, DevComponents.DotNetBar.SuperGrid.GridCellDoubleClickEventArgs e)
        {
            GridRow gridRow = (sender as SuperGridControl).PrimaryGrid.ActiveRow as GridRow;
            if (gridRow == null) return;

            CmcsInFactoryBatch entity = (gridRow.DataItem as CmcsInFactoryBatch);
            if (entity != null)
            {
                IList<CmcsRCSampling> rCSample = cZYHandlerDAO.GetRGSamplingByBatchId(entity.Id);
                if (rCSample != null && rCSample.Count > 0)
                {
                    DataTable dt = cZYHandlerDAO.GetSampleInfo(rCSample[0].Id);
                    listSampleInfo.Clear();
                    foreach (DataRow drSample in dt.Rows)
                    {
                        listSampleInfo.Add(new SampleInfo()
                        {
                            Id = drSample["Id"].ToString(),
                            Batch = drSample["Batch"].ToString(),
                            BatchId = drSample["BatchId"].ToString(),
                            SupplierName = drSample["SupplierName"].ToString(),
                            MineName = drSample["MineName"].ToString(),
                            KindName = drSample["KindName"].ToString(),
                            StationName = drSample["StationName"].ToString(),
                            FactarriveDate = DateTime.Parse(drSample["FactarriveDate"].ToString()),
                            SampleCode = drSample["SampleCode"].ToString(),
                            SamplingDate = DateTime.Parse(drSample["SamplingDate"].ToString()),
                            SamplingType = drSample["SamplingType"].ToString(),
                            BatchType = drSample["InFactoryType"].ToString(),
                            FuelSupplierName = drSample["FuelSupplierName"].ToString(),
                        });
                        _Form1.CurrentSampleInfo = listSampleInfo[0];
                    }
                }
                else
                {
                    if (MessageBoxEx.Show("该批次没有人工采样单，是否生成人工采样？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    {
                        // 生成采制化数据记录
                        CmcsRCSampling rCSampling = CommonDAO.GetInstance().GCSamplingMakeAssay(entity, eSamplingType.人工采样.ToString(), "由人工制样室自动创建", eAssayType.三级编码化验, SelfVars.LoginUser.UserName);
                        DataTable dt = cZYHandlerDAO.GetSampleInfo(rCSampling.Id);
                        listSampleInfo.Clear();
                        foreach (DataRow drSample in dt.Rows)
                        {
                            listSampleInfo.Add(new SampleInfo()
                            {
                                Id = drSample["Id"].ToString(),
                                Batch = drSample["Batch"].ToString(),
                                BatchId = drSample["BatchId"].ToString(),
                                SupplierName = drSample["SupplierName"].ToString(),
                                MineName = drSample["MineName"].ToString(),
                                KindName = drSample["KindName"].ToString(),
                                StationName = drSample["StationName"].ToString(),
                                FactarriveDate = DateTime.Parse(drSample["FactarriveDate"].ToString()),
                                SampleCode = drSample["SampleCode"].ToString(),
                                SamplingDate = DateTime.Parse(drSample["SamplingDate"].ToString()),
                                SamplingType = drSample["SamplingType"].ToString(),
                                BatchType = drSample["InFactoryType"].ToString(),
                                FuelSupplierName = drSample["FuelSupplierName"].ToString(),
                            });
                            _Form1.CurrentSampleInfo = listSampleInfo[0];
                        }
                    }
                }

                isExit = false;
                this.Close();
            }
        }

        private void superGridControl1_DataBindingComplete(object sender, GridDataBindingCompleteEventArgs e)
        {
            foreach (GridRow gridRow in e.GridPanel.Rows)
            {
                CmcsInFactoryBatch entity = gridRow.DataItem as CmcsInFactoryBatch;
                if (entity == null) return;
                gridRow.Cells["clmSupplierName"].Value = entity.TheSupplier != null ? entity.TheSupplier.Name : "";
                gridRow.Cells["clmMineName"].Value = entity.TheMine != null ? entity.TheMine.Name : "";
                gridRow.Cells["clmFuelKindName"].Value = entity.TheFuelKind != null ? entity.TheFuelKind.FuelName : "";
                gridRow.Cells["clmFuelSupplierName"].Value = entity.TheFuelSupplier != null ? entity.TheFuelSupplier.Name : "";
            }
        }
        #endregion

    }
}
