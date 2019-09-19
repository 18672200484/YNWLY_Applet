using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Metro;
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
using System.IO;
using NPOI.HSSF.UserModel;

namespace CMCS.CarTransport.Queue.Frms.Transport.BuyFuelTransport
{
    public partial class FrmBuyFuelTransport_Detail : MetroAppForm
    {
        /// <summary>
        /// 窗体唯一标识符
        /// </summary>
        public static string UniqueKey = "FrmBuyFuelTransport_Detail";

        WagonPrinterDetail wagonPrinter = null;
        List<CmcsBuyFuelTransport> listCount = new List<CmcsBuyFuelTransport>();

        string SqlWhere = string.Empty;

        public FrmBuyFuelTransport_Detail()
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
            this.wagonPrinter = new WagonPrinterDetail(printDocument1);

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
            listCount = Dbers.GetInstance().SelfDber.Entities<CmcsBuyFuelTransport>(tempSqlWhere + " order by SerialNumber desc", param);

            listCount.OrderBy(a => a.MineName);
            CmcsBuyFuelTransport listTotal1 = new CmcsBuyFuelTransport();
            listTotal1.MineName = "合计";
            listTotal1.TicketWeight = listCount.Sum(a => a.TicketWeight);
            listTotal1.GrossWeight = listCount.Sum(a => a.GrossWeight);
            listTotal1.TareWeight = listCount.Sum(a => a.TareWeight);
            listTotal1.SuttleWeight = listCount.Sum(a => a.SuttleWeight);
            listTotal1.DeductWeight = listCount.Sum(a => a.DeductWeight);
            listTotal1.KsWeight = listCount.Sum(a => a.KsWeight);
            listTotal1.AutoKsWeight = listCount.Sum(a => a.AutoKsWeight);
            listTotal1.KgWeight = listCount.Sum(a => a.KgWeight);
            listTotal1.CheckWeight = listCount.Sum(a => a.CheckWeight);
            listTotal1.FuelKindName = listCount.Count.ToString();//车数
            listCount.Add(listTotal1);

            superGridControl1.PrimaryGrid.DataSource = listCount;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.SqlWhere = " where 1=1";
            if (dtpStartTime.Value != DateTime.MinValue) this.SqlWhere += " and GrossTime>=:StartTime";
            if (dtpEndTime.Value != DateTime.MinValue) this.SqlWhere += " and GrossTime <:EndTime";
            if (!string.IsNullOrEmpty(txtMineName_Ser.Text)) this.SqlWhere += " and CarNumber like '%'||" + txtMineName_Ser.Text + "||'%'";
            if (!string.IsNullOrEmpty(cmbFuelKindName_BuyFuel.Text)) this.SqlWhere += " and FuelKindName = '" + cmbFuelKindName_BuyFuel.Text + "'";
            if (!string.IsNullOrEmpty(cmbMineName_BuyFuel.Text)) this.SqlWhere += " and MineName = '" + cmbMineName_BuyFuel.Text + "'";
            BindData();
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            this.SqlWhere = string.Empty;
            txtMineName_Ser.Text = string.Empty;
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
                gridRow.Cells["clmKsWeight"].Value = entity.AutoKsWeight + entity.KsWeight;
                if (entity.MineName == "合计")
                {
                    gridRow.Cells["clmFuelKind"].Visible = false;
                    gridRow.Cells["clmGrossTime"].Visible = false;
                    gridRow.Cells["clmTareTime"].Visible = false;
                }
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

        #region 导出Excel

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                FileStream file = new FileStream("河南煤炭储配交易中心鹤壁园区汽运煤计量统计明细报表.xls", FileMode.Open, FileAccess.Read);
                HSSFWorkbook hssfworkbook = new HSSFWorkbook(file);
                HSSFSheet sheetl = (HSSFSheet)hssfworkbook.GetSheet("sheet1");

                if (this.listCount.Count == 0)
                {
                    MessageBox.Show("请先查询数据", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (folderBrowserDialog1.ShowDialog() != DialogResult.OK)
                    return;
                Mysheet1(sheetl, 1, 11, "日期：" + DateTime.Now.ToString("yyyy-MM-dd"));
                for (int i = 0; i < listCount.Count; i++)
                {
                    CmcsBuyFuelTransport entity = listCount[i];

                    Mysheet1(sheetl, i + 3, 0, entity.CarNumber);
                    Mysheet1(sheetl, i + 3, 1, entity.MineName);
                    Mysheet1(sheetl, i + 3, 2, entity.FuelKindName);
                    Mysheet1(sheetl, i + 3, 3, entity.GrossTime.Year > 2000 ? entity.GrossTime.ToString("yyyy/MM/dd HH:mm:ss") : "");
                    Mysheet1(sheetl, i + 3, 4, entity.TareTime.Year > 2000 ? entity.TareTime.ToString("yyyy/MM/dd HH:mm:ss") : "");
                    Mysheet1(sheetl, i + 3, 5, entity.TicketWeight.ToString("f2"));
                    Mysheet1(sheetl, i + 3, 6, entity.GrossWeight.ToString("f2"));
                    Mysheet1(sheetl, i + 3, 7, entity.TareWeight.ToString("f2"));
                    Mysheet1(sheetl, i + 3, 8, entity.SuttleWeight.ToString("f2"));
                    Mysheet1(sheetl, i + 3, 9, (entity.KsWeight + entity.KgWeight).ToString("f2"));
                    Mysheet1(sheetl, i + 3, 10, entity.KgWeight.ToString("f2"));
                    Mysheet1(sheetl, i + 3, 11, entity.KsWeight.ToString("f2"));
                    Mysheet1(sheetl, i + 3, 12, entity.CheckWeight.ToString("f2"));
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
                    sheetl.GetRow(i + 3).GetCell(9).CellStyle = sheetl.GetRow(cellStyle).GetCell(9).CellStyle;
                    sheetl.GetRow(i + 3).GetCell(10).CellStyle = sheetl.GetRow(cellStyle).GetCell(10).CellStyle;
                    sheetl.GetRow(i + 3).GetCell(11).CellStyle = sheetl.GetRow(cellStyle).GetCell(11).CellStyle;
                    sheetl.GetRow(i + 3).GetCell(12).CellStyle = sheetl.GetRow(cellStyle).GetCell(12).CellStyle;
                }
                Mysheet1(sheetl, listCount.Count + 3, 0, "客户：");
                Mysheet1(sheetl, listCount.Count + 3, 11, "交易中心：");
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
                string fileName = "河南煤炭储配交易中心鹤壁园区汽运煤计量统计明细报表_" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";
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
