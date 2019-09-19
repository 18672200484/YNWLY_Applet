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

namespace CMCS.CarTransport.Queue.Frms.Transport.SaleFuelTransport
{
    public partial class FrmSaleFuelTransport_Detail : MetroAppForm
    {
        /// <summary>
        /// ����Ψһ��ʶ��
        /// </summary>
        public static string UniqueKey = "FrmSaleFuelTransport_Detail";

        WagonPrinterDetail wagonPrinter = null;
        List<CmcsSaleFuelTransport> listCount = new List<CmcsSaleFuelTransport>();

        string SqlWhere = string.Empty;

        public FrmSaleFuelTransport_Detail()
        {
            InitializeComponent();
        }

        private void FrmSaleFuelTransport_Detail_Load(object sender, EventArgs e)
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
        /// �����ջ���λ
        /// </summary>
        void LoadMine(ComboBoxEx comboBoxEx)
        {
            IList<CmcsSupplier> list = Dbers.GetInstance().SelfDber.Entities<CmcsSupplier>("where IsStop=0 order by Name");
            foreach (CmcsSupplier item in list)
            {
                comboBoxEx.Items.Add(new ComboBoxItem(item.Id, item.Name));
            }
            comboBoxEx.Items.Insert(0, new ComboBoxItem("", ""));
        }
        /// <summary>
        /// ����ú��
        /// </summary>
        void LoadFuelkind(ComboBoxEx comboBoxEx)
        {
            IList<CmcsFuelKind> list = Dbers.GetInstance().SelfDber.Entities<CmcsFuelKind>("where Valid='��Ч' and ParentId is not null order by Sequence");
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
            listCount = Dbers.GetInstance().SelfDber.Entities<CmcsSaleFuelTransport>(tempSqlWhere + " order by SerialNumber desc", param);

            listCount.OrderBy(a => a.SupplierId);
            CmcsSaleFuelTransport listTotal1 = new CmcsSaleFuelTransport();
            listTotal1.SupplierId = "�ϼ�";
            listTotal1.GrossWeight = listCount.Sum(a => a.GrossWeight);
            listTotal1.TareWeight = listCount.Sum(a => a.TareWeight);
            listTotal1.SuttleWeight = listCount.Sum(a => a.SuttleWeight);
            listTotal1.IsFinish = listCount.Count;//����
            listCount.Add(listTotal1);

            superGridControl1.PrimaryGrid.DataSource = listCount;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.SqlWhere = " where 1=1";
            if (!string.IsNullOrEmpty(dtpStartTime.Text)) this.SqlWhere += " and TareTime >=:StartTime";
            if (!string.IsNullOrEmpty(dtpEndTime.Text)) this.SqlWhere += " and TareTime<:EndTime";
            if (!string.IsNullOrEmpty(txtMineName_Ser.Text)) this.SqlWhere += " and CarNumber like '%'||" + txtMineName_Ser.Text + "||'%'";
            if (!string.IsNullOrEmpty(cmbFuelKindName_BuyFuel.Text)) this.SqlWhere += " and FuelKindId = '" + ((ComboBoxItem)(cmbFuelKindName_BuyFuel.SelectedItem)).Name + "'";
            if (!string.IsNullOrEmpty(cmbMineName_BuyFuel.Text)) this.SqlWhere += " and SupplierId = '" + ((ComboBoxItem)(cmbMineName_BuyFuel.SelectedItem)).Name + "'";
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

        #region GridView

        private void superGridControl1_DataBindingComplete(object sender, DevComponents.DotNetBar.SuperGrid.GridDataBindingCompleteEventArgs e)
        {
            foreach (GridRow gridRow in e.GridPanel.Rows)
            {
                CmcsSaleFuelTransport entity = gridRow.DataItem as CmcsSaleFuelTransport;
                if (entity == null) return;
                CmcsSupplier supplier = Dbers.GetInstance().SelfDber.Get<CmcsSupplier>(entity.SupplierId);
                if (supplier != null)
                    gridRow.Cells["clmSupplier"].Value = supplier.Name;
                CmcsFuelKind fuelkind = Dbers.GetInstance().SelfDber.Get<CmcsFuelKind>(entity.FuelKindId);
                if (fuelkind != null)
                    gridRow.Cells["clmFuelKind"].Value = fuelkind.FuelName;
                CmcsTransportCompany company = Dbers.GetInstance().SelfDber.Get<CmcsTransportCompany>(entity.TransportCompanyId);
                if (company != null)
                    gridRow.Cells["clmTransportCompany"].Value = company.Name;

                if (entity.SupplierId == "�ϼ�")
                {
                    gridRow.Cells["clmSupplier"].Value = "�ϼ�";
                    gridRow.Cells["clmFuelKind"].Visible = false;
                    gridRow.Cells["clmTransportCompany"].Visible = false;
                    gridRow.Cells["clmGrossTime"].Visible = false;
                    gridRow.Cells["clmTareTime"].Visible = false;
                }
            }
        }

        /// <summary>
        /// �����к�
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void superGridControl_GetRowHeaderText(object sender, DevComponents.DotNetBar.SuperGrid.GridGetRowHeaderTextEventArgs e)
        {
            e.Text = (e.GridRow.RowIndex + 1).ToString();
        }

        #endregion
        /// <summary>
        /// ��ӡ����
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiPrint_Click(object sender, EventArgs e)
        {
            //this.wagonPrinter.Print(this.listCount, null, dtpStartTime.Value, dtpEndTime.Value);
        }

        //private void btnExport_Click(object sender, EventArgs e)
        //{
        //    ImportExcel import = new ImportExcel();
        //    import.DataGridViewToExcels(this.superGridControl1);
        //}

        #region ����Excel

        private void btnExport_Click(object sender, EventArgs e)
        {
            try
            {
                FileStream file = new FileStream("����ú̿���佻�����ĺױ�԰������ú����ͳ����ϸ����(����ú).xls", FileMode.Open, FileAccess.Read);
                HSSFWorkbook hssfworkbook = new HSSFWorkbook(file);
                HSSFSheet sheetl = (HSSFSheet)hssfworkbook.GetSheet("sheet1");

                if (this.listCount.Count == 0)
                {
                    MessageBox.Show("���Ȳ�ѯ����", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (folderBrowserDialog1.ShowDialog() != DialogResult.OK)
                    return;
                Mysheet1(sheetl, 1, 7, "���ڣ�" + DateTime.Now.ToString("yyyy-MM-dd"));
                for (int i = 0; i < listCount.Count; i++)
                {
                    CmcsSaleFuelTransport entity = listCount[i];

                    Mysheet1(sheetl, i + 3, 0, entity.CarNumber);
                    if (entity.SupplierId == "�ϼ�")
                    {
                        Mysheet1(sheetl, i + 3, 1, "�ϼ�");
                    }
                    else
                        Mysheet1(sheetl, i + 3, 1, entity.TheSupplier != null ? entity.TheSupplier.Name : "");
                    Mysheet1(sheetl, i + 3, 2, entity.TheFuelKind != null ? entity.TheFuelKind.FuelName : "");
                    Mysheet1(sheetl, i + 3, 3, entity.GrossTime.Year > 2000 ? entity.GrossTime.ToString("yyyy/MM/dd HH:mm:ss") : "");
                    Mysheet1(sheetl, i + 3, 4, entity.TareTime.Year > 2000 ? entity.TareTime.ToString("yyyy/MM/dd HH:mm:ss") : "");
                    Mysheet1(sheetl, i + 3, 5, entity.GrossWeight.ToString("f2"));
                    Mysheet1(sheetl, i + 3, 6, entity.TareWeight.ToString("f2"));
                    Mysheet1(sheetl, i + 3, 7, entity.SuttleWeight.ToString("f2"));
                    sheetl.GetRow(i + 3).Height = sheetl.GetRow(3).Height;
                    int cellStyle = 3;//�ϼƵ�2�У��ǺϼƵ�3��
                    if (entity.SupplierId == "�ϼ�")
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
                }
                Mysheet1(sheetl, listCount.Count + 3, 0, "�ͻ���");
                Mysheet1(sheetl, listCount.Count + 3, 7, "�������ģ�");
                sheetl.GetRow(listCount.Count + 3).Height = sheetl.GetRow(3).Height;
                #region �ϼ�
                //Mysheet1(sheetl, _CurrExportData.Count + 1, 0, "�ϼ�");
                //Mysheet1(sheetl, _CurrExportData.Count + 1, 2, _CurrExportData.Count + "��");
                //Mysheet1(sheetl, _CurrExportData.Count + 1, 9, Math.Round(_CurrExportData.Sum(a => a.TZ), 2).ToString());
                //Mysheet1(sheetl, _CurrExportData.Count + 1, 10, Math.Round(_CurrExportData.Sum(a => a.JZ), 2).ToString());
                //Mysheet1(sheetl, _CurrExportData.Count + 1, 11, Math.Round(_CurrExportData.Sum(a => a.KZ), 2).ToString());
                //Mysheet1(sheetl, _CurrExportData.Count + 1, 13, Math.Round(_CurrExportData.Sum(a => a.MZ), 2).ToString());
                //Mysheet1(sheetl, _CurrExportData.Count + 1, 19, Math.Round(_CurrExportData.Sum(a => a.PZ), 2).ToString());
                #endregion

                sheetl.ForceFormulaRecalculation = true;
                string fileName = "����ú̿���佻�����ĺױ�԰������ú����ͳ����ϸ����(����ú)_" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";
                GC.Collect();

                FileStream fs = File.OpenWrite(folderBrowserDialog1.SelectedPath + "\\" + fileName);
                hssfworkbook.Write(fs);   //��򿪵����xls�ļ���д������档  
                fs.Close();
                MessageBox.Show("�����ɹ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);

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
