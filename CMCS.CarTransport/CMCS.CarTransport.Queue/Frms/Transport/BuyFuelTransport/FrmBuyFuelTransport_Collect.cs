using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Reflection;
using Microsoft.Office.Interop.Excel;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Metro;
//
using CMCS.Common;
using CMCS.Common.Entities.CarTransport;
using DevComponents.DotNetBar.SuperGrid;
using CMCS.Common.Entities;
using CMCS.Common.Entities.Fuel;
using CMCS.CarTransport.Queue.Frms.Transport.Print;
using CMCS.Common.Enums;
using System.Linq;
using DevComponents.DotNetBar.Controls;
using CMCS.Common.Entities.BaseInfo;
using CMCS.CarTransport.Queue.Utilities;
using NPOI.HSSF.UserModel;


namespace CMCS.CarTransport.Queue.Frms.Transport.BuyFuelTransport
{
    public partial class FrmBuyFuelTransport_Collect : MetroAppForm
    {
        /// <summary>
        /// 窗体唯一标识符
        /// </summary>
        public static string UniqueKey = "FrmBuyFuelTransport_Collect";

        WagonPrinterCollect wagonPrinter = null;
        List<CmcsBuyFuelTransport> listCount = new List<CmcsBuyFuelTransport>();

        string SqlWhere = string.Empty;

        public FrmBuyFuelTransport_Collect()
        {
            InitializeComponent();
        }

        private void FrmBuyFuelTransport_List_Load(object sender, EventArgs e)
        {
            superGridControl1.PrimaryGrid.AutoGenerateColumns = false;

            dtpStartTime.Value = DateTime.Now;
            dtpEndTime.Value = DateTime.Now;
            LoadMine(cmbMineName_BuyFuel);
            LoadFuelkind(cmbFuelKindName_BuyFuel);
            this.wagonPrinter = new WagonPrinterCollect(printDocument1);

            btnSearch_Click(null, null);
        }
        /// <summary>
        /// 加载矿点
        /// </summary>
        void LoadMine(ComboBoxEx comboBoxEx)
        {
            IList<CmcsMine> list = Dbers.GetInstance().SelfDber.Entities<CmcsMine>("where Valid='有效' and ParentId is not null order by Sequence");
            foreach (CmcsMine item in list)
            {
                comboBoxEx.Items.Add(new ComboBoxItem(item.Id, item.Name));
            }
            comboBoxEx.Items.Insert(0, new ComboBoxItem("", ""));
        }
        /// <summary>
        /// 加载煤种
        /// </summary>
        void LoadFuelkind(ComboBoxEx comboBoxEx)
        {
            IList<CmcsFuelKind> list = Dbers.GetInstance().SelfDber.Entities<CmcsFuelKind>("where Valid='有效' and ParentId is not null order by Sequence");
            foreach (CmcsFuelKind item in list)
            {
                comboBoxEx.Items.Add(new ComboBoxItem(item.Id, item.FuelName));
            }
            comboBoxEx.Items.Insert(0, new ComboBoxItem("", ""));
        }

        public void BindData()
        {
            listCount.Clear();
            string tempSqlWhere = this.SqlWhere;
            object param = new { StartTime = dtpStartTime.Value.Date, EndTime = dtpEndTime.Value.AddDays(1).Date };
            List<CmcsBuyFuelTransport> list = Dbers.GetInstance().SelfDber.Entities<CmcsBuyFuelTransport>(tempSqlWhere + " order by SerialNumber desc", param);

            var minename = from p in list group p by new { p.MineName } into g select new { MineName = g.Key.MineName };

            foreach (var item in minename)
            {
                List<CmcsBuyFuelTransport> listone = list.Where(a => a.MineName == item.MineName).ToList();
                if (listone != null && listone.Count > 0)
                {
                    CmcsBuyFuelTransport entity = new CmcsBuyFuelTransport();
                    entity.MineName = listone[0].MineName;
                    entity.FuelKindName = listone[0].FuelKindName;
                    entity.TicketWeight = listone.Sum(a => a.TicketWeight);
                    entity.GrossWeight = listone.Sum(a => a.GrossWeight);
                    entity.TareWeight = listone.Sum(a => a.TareWeight);
                    entity.SuttleWeight = listone.Sum(a => a.SuttleWeight);
                    entity.DeductWeight = listone.Sum(a => a.DeductWeight);
                    entity.KsWeight = listone.Sum(a => a.AutoKsWeight + a.KsWeight);
                    entity.AutoKsWeight = listone.Sum(a => a.AutoKsWeight);
                    //entity.KsWeight = listone.Sum(a => a.KsWeight);
                    entity.KgWeight = listone.Sum(a => a.KgWeight);
                    entity.CheckWeight = listone.Sum(a => a.CheckWeight);
                    entity.IsFinish = listone.Count;//把车数放到完成状态，用来过渡数据
                    entity.SamplingType = listone[0].SamplingType;
                    listCount.Add(entity);
                }
            }
            listCount.OrderBy(a => a.MineName);
            if (chkFuelKindTotal.Checked && !chkMineTotal.Checked)
            {
                var fuelname = from p in list group p by new { p.FuelKindName } into g select new { FuelKindName = g.Key.FuelKindName };
                listCount.Clear();
                foreach (var item in fuelname)
                {
                    List<CmcsBuyFuelTransport> listone = list.Where(a => a.FuelKindName == item.FuelKindName).ToList();
                    if (listone != null && listone.Count > 0)
                    {
                        CmcsBuyFuelTransport entity = new CmcsBuyFuelTransport();
                        entity.MineName = listone[0].MineName;
                        entity.FuelKindName = listone[0].FuelKindName;
                        entity.TicketWeight = listone.Sum(a => a.TicketWeight);
                        entity.GrossWeight = listone.Sum(a => a.GrossWeight);
                        entity.TareWeight = listone.Sum(a => a.TareWeight);
                        entity.SuttleWeight = listone.Sum(a => a.SuttleWeight);
                        entity.DeductWeight = listone.Sum(a => a.DeductWeight);
                        entity.KsWeight = listone.Sum(a => a.AutoKsWeight + a.KsWeight);
                        entity.AutoKsWeight = listone.Sum(a => a.AutoKsWeight);
                        //entity.KsWeight = listone.Sum(a => a.KsWeight);
                        entity.KgWeight = listone.Sum(a => a.KgWeight);
                        entity.CheckWeight = listone.Sum(a => a.CheckWeight);
                        entity.IsFinish = listone.Count;//把车数放到完成状态，用来过渡数据
                        entity.SamplingType = listone[0].SamplingType;
                        listCount.Add(entity);
                    }
                }
                listCount.OrderBy(a => a.FuelKindName);
            }
            else if (chkMineTotal.Checked && chkFuelKindTotal.Checked)
            {
                var minefuelname = from p in list group p by new { p.MineName, p.FuelKindName } into g select new { MineName = g.Key.MineName, FuelKindName = g.Key.FuelKindName };
                listCount.Clear();
                foreach (var item in minefuelname)
                {
                    List<CmcsBuyFuelTransport> listone = list.Where(a => a.MineName == item.MineName && a.FuelKindName == item.FuelKindName).ToList();
                    if (listone != null && listone.Count > 0)
                    {
                        CmcsBuyFuelTransport entity = new CmcsBuyFuelTransport();
                        entity.MineName = listone[0].MineName;
                        entity.FuelKindName = listone[0].FuelKindName;
                        entity.TicketWeight = listone.Sum(a => a.TicketWeight);
                        entity.GrossWeight = listone.Sum(a => a.GrossWeight);
                        entity.TareWeight = listone.Sum(a => a.TareWeight);
                        entity.SuttleWeight = listone.Sum(a => a.SuttleWeight);
                        entity.DeductWeight = listone.Sum(a => a.DeductWeight);
                        entity.KsWeight = listone.Sum(a => a.AutoKsWeight + a.KsWeight);
                        entity.AutoKsWeight = listone.Sum(a => a.AutoKsWeight);
                        //entity.KsWeight = listone.Sum(a => a.KsWeight);
                        entity.KgWeight = listone.Sum(a => a.KgWeight);
                        entity.CheckWeight = listone.Sum(a => a.CheckWeight);
                        entity.IsFinish = listone.Count;//把车数放到完成状态，用来过渡数据
                        entity.SamplingType = listone[0].SamplingType;
                        listCount.Add(entity);
                    }
                }
                listCount.OrderBy(a => a.MineName).OrderBy(a => a.FuelKindName);
            }


            CmcsBuyFuelTransport listTotal1 = new CmcsBuyFuelTransport();
            listTotal1.MineName = "合计";
            listTotal1.FuelKindName = "";
            listTotal1.TicketWeight = list.Sum(a => a.TicketWeight);
            listTotal1.GrossWeight = list.Sum(a => a.GrossWeight);
            listTotal1.TareWeight = list.Sum(a => a.TareWeight);
            listTotal1.SuttleWeight = list.Sum(a => a.SuttleWeight);
            listTotal1.DeductWeight = list.Sum(a => a.DeductWeight);
            listTotal1.KsWeight = list.Sum(a => a.AutoKsWeight + a.KsWeight);
            //listTotal1.KsWeight = list.Sum(a => a.KsWeight);
            listTotal1.AutoKsWeight = list.Sum(a => a.AutoKsWeight);
            listTotal1.KgWeight = list.Sum(a => a.KgWeight);
            listTotal1.CheckWeight = list.Sum(a => a.CheckWeight);
            listTotal1.IsFinish = list.Count;//车数
            listCount.Add(listTotal1);

            superGridControl1.PrimaryGrid.DataSource = listCount;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.SqlWhere = " where TareWeight>0";
            if (dtpStartTime.Value != DateTime.MinValue) this.SqlWhere += " and TareTime >=:StartTime";
            if (dtpEndTime.Value != DateTime.MinValue) this.SqlWhere += " and TareTime <:EndTime";
            if (!string.IsNullOrEmpty(cmbMineName_BuyFuel.Text)) this.SqlWhere += " and MineName = '" + cmbMineName_BuyFuel.Text + "'";
            if (!string.IsNullOrEmpty(cmbFuelKindName_BuyFuel.Text)) this.SqlWhere += " and FuelKindName = '" + cmbFuelKindName_BuyFuel.Text + "'";

            BindData();
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            this.SqlWhere = string.Empty;
            cmbMineName_BuyFuel.Text = string.Empty;
            cmbFuelKindName_BuyFuel.Text = string.Empty;
            BindData();
        }

        private void btnInStore_Click(object sender, EventArgs e)
        {
            FrmBuyFuelTransport_Oper frm = new FrmBuyFuelTransport_Oper();
            frm.ShowDialog();

            BindData();
        }

        #region GridView

        private void superGridControl1_DataBindingComplete(object sender, DevComponents.DotNetBar.SuperGrid.GridDataBindingCompleteEventArgs e)
        {
            foreach (GridRow gridRow in e.GridPanel.Rows)
            {
                CmcsBuyFuelTransport entity = gridRow.DataItem as CmcsBuyFuelTransport;
                if (entity == null) return;
                //gridRow.Cells["KsWeight"].Value = entity.KsWeight + entity.AutoKsWeight;
            }
        }

        /// <summary>
        /// 设置行号
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void superGridControl_GetRowHeaderText(object sender, DevComponents.DotNetBar.SuperGrid.GridGetRowHeaderTextEventArgs e)
        {
            e.Text = (e.GridRow.RowIndex + 1).ToString();
        }

        #endregion

        /// <summary>
        /// 打印磅单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiPrint_Click(object sender, EventArgs e)
        {
            this.wagonPrinter.Print(this.listCount, null, dtpStartTime.Value, dtpEndTime.Value);
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            //ImportExcel import = new ImportExcel();
            //import.DataGridViewToExcels(this.superGridControl1);
            btnXExport_Click(null, null);
        }

        #region 导出Excel

        private void btnXExport_Click(object sender, EventArgs e)
        {
            try
            {
                FileStream file = new FileStream("河南煤炭储配交易中心鹤壁园区汽运煤计量统计日报表.xls", FileMode.Open, FileAccess.Read);
                HSSFWorkbook hssfworkbook = new HSSFWorkbook(file);
                HSSFSheet sheetl = (HSSFSheet)hssfworkbook.GetSheet("sheet1");

                if (this.listCount.Count == 0)
                {
                    MessageBox.Show("请先查询数据", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (folderBrowserDialog1.ShowDialog() != DialogResult.OK)
                    return;
                Mysheet1(sheetl, 1, 7, "日期：" + DateTime.Now.ToString("yyyy-MM-dd"));
                for (int i = 0; i < listCount.Count; i++)
                {
                    CmcsBuyFuelTransport entity = listCount[i];

                    Mysheet1(sheetl, i + 3, 0, entity.MineName);
                    Mysheet1(sheetl, i + 3, 1, entity.FuelKindName);
                    Mysheet1(sheetl, i + 3, 2, entity.IsFinish.ToString());
                    Mysheet1(sheetl, i + 3, 3, entity.TicketWeight.ToString("f2"));
                    Mysheet1(sheetl, i + 3, 4, entity.GrossWeight.ToString("f2"));
                    Mysheet1(sheetl, i + 3, 5, entity.TareWeight.ToString("f2"));
                    Mysheet1(sheetl, i + 3, 6, (entity.KsWeight).ToString("f2"));//导出的报表只显示人工扣水
                    Mysheet1(sheetl, i + 3, 7, entity.KgWeight.ToString("f2"));
                    Mysheet1(sheetl, i + 3, 8, entity.CheckWeight.ToString("f2"));
                    Mysheet1(sheetl, i + 3, 9, entity.SamplingType);
                    Mysheet1(sheetl, i + 3, 10, "");
                    sheetl.GetRow(i + 3).Height = sheetl.GetRow(3).Height;
                    int cellStyle = 3;//合计第2行，非合计第3行
                    if (entity.MineName == "合计")
                    {
                        cellStyle = 2;
                    }
                    sheetl.GetRow(i + 3).GetCell(0).CellStyle = sheetl.GetRow(cellStyle).GetCell(0).CellStyle;
                    sheetl.GetRow(i + 3).GetCell(1).CellStyle = sheetl.GetRow(cellStyle).GetCell(1).CellStyle;
                    sheetl.GetRow(i + 3).GetCell(2).CellStyle = sheetl.GetRow(cellStyle).GetCell(2).CellStyle;
                    sheetl.GetRow(i + 3).GetCell(3).CellStyle = sheetl.GetRow(cellStyle).GetCell(3).CellStyle;
                    sheetl.GetRow(i + 3).GetCell(4).CellStyle = sheetl.GetRow(cellStyle).GetCell(4).CellStyle;
                    sheetl.GetRow(i + 3).GetCell(5).CellStyle = sheetl.GetRow(cellStyle).GetCell(5).CellStyle;
                    sheetl.GetRow(i + 3).GetCell(6).CellStyle = sheetl.GetRow(cellStyle).GetCell(6).CellStyle;
                    sheetl.GetRow(i + 3).GetCell(7).CellStyle = sheetl.GetRow(cellStyle).GetCell(7).CellStyle;
                    sheetl.GetRow(i + 3).GetCell(8).CellStyle = sheetl.GetRow(cellStyle).GetCell(8).CellStyle;

                }
                Mysheet1(sheetl, listCount.Count + 3, 0, "客户：");
                Mysheet1(sheetl, listCount.Count + 3, 7, "交易中心：");
                sheetl.GetRow(listCount.Count + 3).Height = sheetl.GetRow(3).Height;
                #region 合计
                //Mysheet1(sheetl, _CurrExportData.Count + 1, 0, "合计");
                //Mysheet1(sheetl, _CurrExportData.Count + 1, 2, _CurrExportData.Count + "车");
                //Mysheet1(sheetl, _CurrExportData.Count + 1, 9, Math.Round(_CurrExportData.Sum(a => a.TZ), 2).ToString());
                //Mysheet1(sheetl, _CurrExportData.Count + 1, 10, Math.Round(_CurrExportData.Sum(a => a.JZ), 2).ToString());
                //Mysheet1(sheetl, _CurrExportData.Count + 1, 11, Math.Round(_CurrExportData.Sum(a => a.KZ), 2).ToString());
                //Mysheet1(sheetl, _CurrExportData.Count + 1, 13, Math.Round(_CurrExportData.Sum(a => a.MZ), 2).ToString());
                //Mysheet1(sheetl, _CurrExportData.Count + 1, 19, Math.Round(_CurrExportData.Sum(a => a.PZ), 2).ToString());
                #endregion

                sheetl.ForceFormulaRecalculation = true;
                string fileName = "河南煤炭储配交易中心鹤壁园区汽运煤计量统计日报表_" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";
                GC.Collect();

                FileStream fs = File.OpenWrite(folderBrowserDialog1.SelectedPath + "\\" + fileName);
                hssfworkbook.Write(fs);   //向打开的这个xls文件中写入表并保存。  
                fs.Close();
                MessageBox.Show("导出成功", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void Mysheet1(HSSFSheet sheet1, int x, int y, String Value)
        {
            if (sheet1.GetRow(x) == null)
            {
                sheet1.CreateRow(x);
            }
            if (sheet1.GetRow(x).GetCell(y) == null)
            {
                sheet1.GetRow(x).CreateCell(y);
            }
            sheet1.GetRow(x).GetCell(y).SetCellValue(Value);

        }

        #endregion

    }
}
