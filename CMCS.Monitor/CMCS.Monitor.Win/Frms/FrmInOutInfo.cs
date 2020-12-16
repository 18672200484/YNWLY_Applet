﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using CMCS.Common;
//
using CMCS.Common.Entities;
using CMCS.Common.Entities.AutoMaker;
using CMCS.Common.Entities.BaseInfo;
using CMCS.Common.Entities.Inf;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Metro;
using DevComponents.DotNetBar.SuperGrid;
using System.Data;
using CMCS.Common.Utilities;
using CMCS.Common.Entities.Fuel;
using CMCS.Common.Entities.CarTransport;

namespace CMCS.Monitor.Win.Frms
{
    public partial class FrmInOutInfo : MetroForm
    {
        /// <summary>
        /// 窗体唯一标识符
        /// </summary>
        public static string UniqueKey = "FrmInOutInfo";

        public FrmInOutInfo()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 每页显示行数
        /// </summary>
        int PageSize = 28;

        /// <summary>
        /// 总页数
        /// </summary>
        int PageCount = 0;

        /// <summary>
        /// 总记录数
        /// </summary>
        int TotalCount = 0;

        /// <summary>
        /// 当前页索引
        /// </summary>
        int CurrentIndex = 0;

        public string SqlWhere1 = string.Empty;
        public string SqlWhere2 = string.Empty;
        public static string Device = string.Empty;

        private void FrmPoundInfo_Load(object sender, EventArgs e)
        {
            InitForm();

            BindData();
            //btnSearch_Click(null, null);
        }

        /// <summary>
        /// 窗体初始化
        /// </summary>
        private void InitForm()
        {
            dateTimeInput1.Value = DateTime.Now.Date.AddDays(-2);
            dateTimeInput2.Value = DateTime.Now.Date.AddDays(1).AddMilliseconds(-1);

            //加载识别设备

            List<CmcsCMEquipment> list = new List<CmcsCMEquipment>();
            CmcsCMEquipment model1 = new CmcsCMEquipment();
            model1.EquipmentName = "汽车出口";
            list.Add(model1);

            CmcsCMEquipment model2 = new CmcsCMEquipment();
            model2.EquipmentName = "重车入场";
            list.Add(model2);

            CmcsCMEquipment model3 = new CmcsCMEquipment();
            model3.EquipmentName = "轻车入场";
            list.Add(model3);

            cmbPoundName.DisplayMember = "EquipmentName";
            cmbPoundName.ValueMember = "EquipmentName";
            cmbPoundName.DataSource = list;
            cmbPoundName.SelectedIndex = 0;

            if (!string.IsNullOrEmpty(Device))
            {
                cmbPoundName.SelectedValue = Device;
            }

        }

        public void BindData()
        {
            TotalCount = 0;
            PageCount = 0;

            string tempSqlWhere = "";
            List<CmcsBuyFuelTransport> list = new List<CmcsBuyFuelTransport>();

            if (cmbPoundName.SelectedValue.ToString() == "汽车出口")
            {
                GetOutDatas(tempSqlWhere, list);
            }
            else if (cmbPoundName.SelectedValue.ToString() == "重车入场")
            {
                GetBuyDatas(tempSqlWhere, list);
            }
            else if (cmbPoundName.SelectedValue.ToString() == "轻车入场")
            {
                GetSaleDatas(tempSqlWhere, list);
            }
            else
            {
                list = Dbers.GetInstance().SelfDber.ExecutePager<CmcsBuyFuelTransport>(PageSize, CurrentIndex, tempSqlWhere + " order by GROSSTIME desc");
            }

            superGridControl1.PrimaryGrid.DataSource = list;
            PagerControlStatue();

            lblPagerInfo.Text = string.Format("共 {0} 条记录，每页 {1} 条，共 {2} 页，当前第 {3} 页", TotalCount, PageSize, PageCount, CurrentIndex + 1);
        }

        private void GetSaleDatas(string tempSqlWhere, List<CmcsBuyFuelTransport> list)
        {
            tempSqlWhere = " WHERE 1=1 ";
            if (!String.IsNullOrEmpty((String)dateTimeInput1.Text))
            {
                tempSqlWhere += " and INFACTORYTIME>= to_date('" + dateTimeInput1.Value.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss') ";
            }
            if (!String.IsNullOrEmpty((String)dateTimeInput2.Text))
            {
                tempSqlWhere += " and INFACTORYTIME< to_date('" + dateTimeInput2.Value.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss') ";
            }
            List<CmcsSaleFuelTransport> list1 = Dbers.GetInstance().SelfDber.ExecutePager<CmcsSaleFuelTransport>(PageSize, CurrentIndex, tempSqlWhere + " order by INFACTORYTIME desc");

            TotalCount = Dbers.GetInstance().SelfDber.Count<CmcsSaleFuelTransport>(tempSqlWhere);

            foreach (CmcsSaleFuelTransport item in list1)
            {
                CmcsBuyFuelTransport model = new CmcsBuyFuelTransport();
                model.CarNumber = item.CarNumber;
                model.SupplierName = item.TheSupplier != null ? item.TheSupplier.Name : "";
                model.PASSTIME = item.InFactoryTime;
                model.InFactoryType = item.OutFactoryType;
                list.Add(model);
            }
           
            list = list.OrderByDescending(t => t.PASSTIME).ToList();
            if (TotalCount % PageSize != 0)
                PageCount += TotalCount / PageSize + 1;
            else
                PageCount += TotalCount / PageSize;
        }

        private void GetOutDatas(string tempSqlWhere, List<CmcsBuyFuelTransport> list)
        {
            tempSqlWhere = " WHERE 1=1";
                //" WHERE OUTFACTORYPLACE = '" + cmbPoundName.SelectedValue + "'";
            if (!String.IsNullOrEmpty((String)dateTimeInput1.Text))
            {
                tempSqlWhere += " and OUTFACTORYTIME>= to_date('" + dateTimeInput1.Value.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss') ";
            }
            if (!String.IsNullOrEmpty((String)dateTimeInput2.Text))
            {
                tempSqlWhere += " and OUTFACTORYTIME< to_date('" + dateTimeInput2.Value.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss') ";
            }
            List<CmcsBuyFuelTransport> list1 = Dbers.GetInstance().SelfDber.ExecutePager<CmcsBuyFuelTransport>(PageSize, CurrentIndex, tempSqlWhere + " order by OUTFACTORYTIME desc");

            TotalCount = Dbers.GetInstance().SelfDber.Count<CmcsBuyFuelTransport>(tempSqlWhere);

            tempSqlWhere = " WHERE 1=1";
            //" WHERE INFACTORYPLACE = '" + cmbPoundName.SelectedValue + "'";
            if (!String.IsNullOrEmpty((String)dateTimeInput1.Text))
            {
                tempSqlWhere += " and OUTFACTORYTIME>= to_date('" + dateTimeInput1.Value.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss') ";
            }
            if (!String.IsNullOrEmpty((String)dateTimeInput2.Text))
            {
                tempSqlWhere += " and OUTFACTORYTIME< to_date('" + dateTimeInput2.Value.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss') ";
            }
            List<CmcsSaleFuelTransport> list2 = Dbers.GetInstance().SelfDber.ExecutePager<CmcsSaleFuelTransport>(PageSize, CurrentIndex, tempSqlWhere + " order by OUTFACTORYTIME desc");
            
            TotalCount = Dbers.GetInstance().SelfDber.Count<CmcsSaleFuelTransport>(tempSqlWhere);

            foreach (CmcsBuyFuelTransport item in list1)
            {
                CmcsBuyFuelTransport model = new CmcsBuyFuelTransport();
                model.CarNumber = item.CarNumber;
                model.SupplierName = item.SupplierName;
                model.PASSTIME = item.InFactoryTime;
                model.InFactoryType = item.InFactoryType;
                list.Add(model);
            }
            foreach (CmcsSaleFuelTransport item in list2)
            {
                CmcsBuyFuelTransport model = new CmcsBuyFuelTransport();
                model.CarNumber = item.CarNumber;
                model.SupplierName = item.TheSupplier != null ? item.TheSupplier.Name : "";
                model.PASSTIME = item.OutFactoryTime;
                model.InFactoryType = item.OutFactoryType;
                list.Add(model);
            }
            list = list.OrderByDescending(t => t.PASSTIME).ToList();
            if (TotalCount % PageSize != 0)
                PageCount += TotalCount / PageSize + 1;
            else
                PageCount += TotalCount / PageSize;
        }

        private void GetBuyDatas(string tempSqlWhere, List<CmcsBuyFuelTransport> list)
        {
            tempSqlWhere = " WHERE 1=1";
            if (!String.IsNullOrEmpty((String)dateTimeInput1.Text))
            {
                tempSqlWhere += " and INFACTORYTIME>= to_date('" + dateTimeInput1.Value.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss') ";
            }
            if (!String.IsNullOrEmpty((String)dateTimeInput2.Text))
            {
                tempSqlWhere += " and INFACTORYTIME< to_date('" + dateTimeInput2.Value.ToString("yyyy-MM-dd HH:mm:ss") + "','yyyy-mm-dd hh24:mi:ss') ";
            }
            List<CmcsBuyFuelTransport> list1 = Dbers.GetInstance().SelfDber.ExecutePager<CmcsBuyFuelTransport>(PageSize, CurrentIndex, tempSqlWhere + " order by INFACTORYTIME desc");
            TotalCount = Dbers.GetInstance().SelfDber.Count<CmcsBuyFuelTransport>(tempSqlWhere);

            foreach (CmcsBuyFuelTransport item in list1)
            {
                CmcsBuyFuelTransport model = new CmcsBuyFuelTransport();
                model.CarNumber = item.CarNumber;
                model.SupplierName = item.SupplierName;
                model.PASSTIME = item.InFactoryTime;
                model.InFactoryType = item.InFactoryType;
                list.Add(model);
            }
           
            list = list.OrderByDescending(t => t.PASSTIME).ToList();
            if (TotalCount % PageSize != 0)
                PageCount += TotalCount / PageSize + 1;
            else
                PageCount += TotalCount / PageSize;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            BindData();
        }

        #region Pager

        private void btnPagerCommand_Click(object sender, EventArgs e)
        {
            ButtonX btn = sender as ButtonX;
            switch (btn.CommandParameter.ToString())
            {
                case "First":
                    CurrentIndex = 0;
                    break;
                case "Previous":
                    CurrentIndex = CurrentIndex - 1;
                    break;
                case "Next":
                    CurrentIndex = CurrentIndex + 1;
                    break;
                case "Last":
                    CurrentIndex = PageCount - 1;
                    break;
            }

            BindData();
        }

        public void PagerControlStatue()
        {
            if (PageCount <= 1)
            {
                btnFirst.Enabled = false;
                btnPrevious.Enabled = false;
                btnLast.Enabled = false;
                btnNext.Enabled = false;

                return;
            }

            if (CurrentIndex == 0)
            {
                // 首页
                btnFirst.Enabled = false;
                btnPrevious.Enabled = false;
                btnLast.Enabled = true;
                btnNext.Enabled = true;
            }

            if (CurrentIndex > 0 && CurrentIndex < PageCount - 1)
            {
                // 上一页/下一页
                btnFirst.Enabled = true;
                btnPrevious.Enabled = true;
                btnLast.Enabled = true;
                btnNext.Enabled = true;
            }

            if (CurrentIndex == PageCount - 1)
            {
                // 末页
                btnFirst.Enabled = true;
                btnPrevious.Enabled = true;
                btnLast.Enabled = false;
                btnNext.Enabled = false;
            }
        }

        private void GetTotalCount1(string sqlWhere, ref int TotalCount, ref int PageCount)
        {
            TotalCount += Dbers.GetInstance().SelfDber.Count<CmcsBuyFuelTransport>(sqlWhere);
           
        }
        #endregion

        #region SuperGridControl

        private void superGridControl1_GetRowHeaderText(object sender, DevComponents.DotNetBar.SuperGrid.GridGetRowHeaderTextEventArgs e)
        {
            e.Text = ((this.CurrentIndex * this.PageSize) + e.GridRow.RowIndex + 1).ToString();
        }

        private void superGridControl1_BeginEdit(object sender, GridEditEventArgs e)
        {
            // 取消编辑
            e.Cancel = true;
        }

        #endregion


    }

    
}
