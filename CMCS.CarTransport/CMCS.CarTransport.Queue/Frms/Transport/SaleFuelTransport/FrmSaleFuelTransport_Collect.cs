using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
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
using CMCS.CarTransport.Queue.Frms.Transport.Print.SaleFuelPrint;
using NPOI.HSSF.UserModel;
using System.IO;
using System.Collections;

namespace CMCS.CarTransport.Queue.Frms.Transport.SaleFuelTransport
{
    public partial class FrmSaleFuelTransport_Collect : MetroAppForm
    {
        /// <summary>
        /// ����Ψһ��ʶ��
        /// </summary>
        public static string UniqueKey = "FrmSaleFuelTransport_Collect";

        SaleFuelWagonPrinterCollect wagonPrinter = null;
        List<CmcsSaleFuelTransport> listCount = new List<CmcsSaleFuelTransport>();

        string SqlWhere = string.Empty;

        public FrmSaleFuelTransport_Collect()
        {
            InitializeComponent();
        }

        private void FrmSaleFuelTransport_Collect_Load(object sender, EventArgs e)
        {
            superGridControl1.PrimaryGrid.AutoGenerateColumns = false;

            dtpStartTime.Value = DateTime.Now;
            dtpEndTime.Value = DateTime.Now;
            LoadSupplier(cmbMineName_BuyFuel);
            LoadFuelkind(cmbFuelKindName_BuyFuel);
            this.wagonPrinter = new SaleFuelWagonPrinterCollect(printDocument1);

            btnSearch_Click(null, null);
        }
        /// <summary>
        /// ���ع�Ӧ��
        /// </summary>
        void LoadSupplier(ComboBoxEx comboBoxEx)
        {
            IList<CmcsSupplier> list = Dbers.GetInstance().SelfDber.Entities<CmcsSupplier>("where IsStop=0 order by Sequence");
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
            List<CmcsSaleFuelTransport> list = Dbers.GetInstance().SelfDber.Entities<CmcsSaleFuelTransport>(tempSqlWhere + " order by SerialNumber desc", param);
            //var all = from p in list group p by new { p.SupplierId, p.FuelKindId, p.TransportCompanyId } into g select new { SupplierId = g.Key.SupplierId, FuelKindId = g.Key.FuelKindId, TransportCompanyId = g.Key.TransportCompanyId };
            //if (!chkFuelSupplierTotal.Checked)
            //{
            //    all = from p in all group p by new { p.FuelKindId, p.TransportCompanyId } into g select new { SupplierId = "", FuelKindId = g.Key.FuelKindId, TransportCompanyId = g.Key.TransportCompanyId };
            //}
            //if (!chkFuelTransportCompanyTotal.Checked)
            //{
            //    all = from p in all group p by new { p.SupplierId, p.FuelKindId } into g select new { SupplierId = g.Key.SupplierId, FuelKindId = g.Key.FuelKindId, TransportCompanyId = "" };
            //}
            //if (!chkFuelKindTotal.Checked)
            //{
            //    all = from p in all group p by new { p.SupplierId, p.TransportCompanyId } into g select new { SupplierId = g.Key.SupplierId, FuelKindId = "", TransportCompanyId = g.Key.TransportCompanyId };
            //}

            //listCount.Clear();
            //foreach (var item in all)
            //{
            //    List<CmcsSaleFuelTransport> listone = list;
            //    if (!string.IsNullOrEmpty(item.SupplierId))
            //        listone = listone.Where(a => a.SupplierId == item.SupplierId).ToList();
            //    if (!string.IsNullOrEmpty(item.FuelKindId))
            //        listone = listone.Where(a => a.FuelKindId == item.FuelKindId).ToList();
            //    if (!string.IsNullOrEmpty(item.TransportCompanyId))
            //        listone = listone.Where(a => a.TransportCompanyId == item.TransportCompanyId).ToList();
            //    if (listone != null && listone.Count > 0)
            //    {
            //        CmcsSaleFuelTransport entity = new CmcsSaleFuelTransport();
            //        entity.SupplierName = listone[0].TheSupplier.Name;
            //        entity.FuelKindId = listone[0].TheFuelKind.FuelName;
            //        entity.TransportCompanyName = listone[0].TransportCompanyName;
            //        entity.GrossWeight = listone.Sum(a => a.GrossWeight);
            //        entity.TareWeight = listone.Sum(a => a.TareWeight);
            //        entity.SuttleWeight = listone.Sum(a => a.SuttleWeight);
            //        entity.IsFinish = listone.Count;//�ѳ����ŵ����״̬��������������
            //        listCount.Add(entity);
            //    }
            //}
            //listCount.OrderBy(a => a.FuelKindId);


            var minename = from p in list group p by new { p.SupplierId, p.TransportCompanyId } into g select new { SupplierId = g.Key.SupplierId, TransportCompanyId = g.Key.TransportCompanyId };

            foreach (var item in minename)
            {
                List<CmcsSaleFuelTransport> listone = list.Where(a => a.SupplierId == item.SupplierId && a.TransportCompanyId == item.TransportCompanyId).ToList();
                if (listone != null && listone.Count > 0)
                {
                    CmcsSaleFuelTransport entity = new CmcsSaleFuelTransport();
                    entity.SupplierName = listone[0].TheSupplier.Name;
                    entity.TransportCompanyName = listone[0].TransportCompanyName;
                    entity.FuelKindId = listone[0].TheFuelKind.FuelName;
                    entity.GrossWeight = listone.Sum(a => a.GrossWeight);
                    entity.TareWeight = listone.Sum(a => a.TareWeight);
                    entity.SuttleWeight = listone.Sum(a => a.SuttleWeight);
                    entity.IsFinish = listone.Count;//�ѳ����ŵ����״̬��������������
                    listCount.Add(entity);
                }
            }
            listCount.OrderBy(a => a.SupplierId);

            if (chkFuelKindTotal.Checked && !chkFuelSupplierTotal.Checked)
            {
                var fuelname = from p in list group p by new { p.FuelKindId } into g select new { FuelKindId = g.Key.FuelKindId };
                listCount.Clear();
                foreach (var item in fuelname)
                {
                    List<CmcsSaleFuelTransport> listone = list.Where(a => a.FuelKindId == item.FuelKindId).ToList();
                    if (listone != null && listone.Count > 0)
                    {
                        CmcsSaleFuelTransport entity = new CmcsSaleFuelTransport();
                        entity.SupplierName = listone[0].TheSupplier.Name;
                        entity.TransportCompanyName = listone[0].TransportCompanyName;
                        entity.FuelKindId = listone[0].TheFuelKind.FuelName;
                        entity.GrossWeight = listone.Sum(a => a.GrossWeight);
                        entity.TareWeight = listone.Sum(a => a.TareWeight);
                        entity.SuttleWeight = listone.Sum(a => a.SuttleWeight);
                        entity.IsFinish = listone.Count;//�ѳ����ŵ����״̬��������������
                        listCount.Add(entity);
                    }
                }
                listCount.OrderBy(a => a.FuelKindId);
            }
            else if (chkFuelSupplierTotal.Checked && chkFuelKindTotal.Checked)
            {
                var minefuelname = from p in list group p by new { p.SupplierId, p.TransportCompanyId, p.FuelKindId } into g select new { SupplierId = g.Key.SupplierId, TransportCompanyId = g.Key.TransportCompanyId, FuelKindName = g.Key.FuelKindId };
                listCount.Clear();
                foreach (var item in minefuelname)
                {
                    List<CmcsSaleFuelTransport> listone = list.Where(a => a.SupplierId == item.SupplierId && a.TransportCompanyId == item.TransportCompanyId && a.FuelKindId == item.FuelKindName).ToList();
                    if (listone != null && listone.Count > 0)
                    {
                        CmcsSaleFuelTransport entity = new CmcsSaleFuelTransport();
                        entity.SupplierName = listone[0].TheSupplier.Name;
                        entity.TransportCompanyName = listone[0].TransportCompanyName;
                        entity.FuelKindId = listone[0].TheFuelKind.FuelName;
                        entity.GrossWeight = listone.Sum(a => a.GrossWeight);
                        entity.TareWeight = listone.Sum(a => a.TareWeight);
                        entity.SuttleWeight = listone.Sum(a => a.SuttleWeight);
                        entity.IsFinish = listone.Count;//�ѳ����ŵ����״̬��������������
                        listCount.Add(entity);
                    }
                }
                listCount.OrderBy(a => a.SupplierId).OrderBy(a => a.FuelKindId);
            }

            CmcsSaleFuelTransport listTotal1 = new CmcsSaleFuelTransport();
            listTotal1.SupplierId = "�ϼ�";
            listTotal1.GrossWeight = list.Sum(a => a.GrossWeight);
            listTotal1.TareWeight = list.Sum(a => a.TareWeight);
            listTotal1.SuttleWeight = list.Sum(a => a.SuttleWeight);
            listTotal1.IsFinish = list.Count;//����
            listCount.Add(listTotal1);

            superGridControl1.PrimaryGrid.DataSource = listCount;
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            this.SqlWhere = " where TareWeight>0";
            if (!string.IsNullOrEmpty(dtpStartTime.Text)) this.SqlWhere += " and GrossTime >=:StartTime";
            if (!string.IsNullOrEmpty(dtpEndTime.Text)) this.SqlWhere += " and GrossTime<:EndTime";
            if (!string.IsNullOrEmpty(cmbFuelKindName_BuyFuel.Text)) this.SqlWhere += " and FuelKindId = '" + ((ComboBoxItem)(cmbFuelKindName_BuyFuel.SelectedItem)).Name + "'";
            if (!string.IsNullOrEmpty(cmbMineName_BuyFuel.Text)) this.SqlWhere += " and SupplierId = '" + ((ComboBoxItem)(cmbMineName_BuyFuel.SelectedItem)).Name + "'";
            BindData();
        }

        private void btnAll_Click(object sender, EventArgs e)
        {
            this.SqlWhere = string.Empty;
            cmbMineName_BuyFuel.Text = string.Empty;
            cmbFuelKindName_BuyFuel.Text = string.Empty;
            BindData();
        }

        #region GridView

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
            this.wagonPrinter.Print(this.listCount, null, dtpStartTime.Value, dtpEndTime.Value);
        }

        //private void buttonX1_Click(object sender, EventArgs e)
        //{
        //    ImportExcel import = new ImportExcel();
        //    import.DataGridViewToExcels(this.superGridControl1);
        //}

        #region ����Excel

        private void btnXExport_Click(object sender, EventArgs e)
        {
            try
            {
                FileStream file = new FileStream("����ú̿���佻�����ĺױ�԰������ú����ͳ���ձ���(����ú).xls", FileMode.Open, FileAccess.Read);
                HSSFWorkbook hssfworkbook = new HSSFWorkbook(file);
                HSSFSheet sheetl = (HSSFSheet)hssfworkbook.GetSheet("sheet1");

                if (this.listCount.Count == 0)
                {
                    MessageBox.Show("���Ȳ�ѯ����", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                if (folderBrowserDialog1.ShowDialog() != DialogResult.OK)
                    return;
                Mysheet1(sheetl, 1, 3, "���ڣ�" + DateTime.Now.ToString("yyyy-MM-dd"));
                for (int i = 0; i < listCount.Count; i++)
                {
                    CmcsSaleFuelTransport entity = listCount[i];

                    Mysheet1(sheetl, i + 3, 0, entity.SupplierId);
                    Mysheet1(sheetl, i + 3, 1, entity.FuelKindId);
                    Mysheet1(sheetl, i + 3, 2, entity.IsFinish.ToString());
                    Mysheet1(sheetl, i + 3, 3, entity.GrossWeight.ToString("f2"));
                    Mysheet1(sheetl, i + 3, 4, entity.TareWeight.ToString("f2"));
                    Mysheet1(sheetl, i + 3, 5, entity.SuttleWeight.ToString("f2"));
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

                }
                Mysheet1(sheetl, listCount.Count + 3, 0, "�ͻ���");
                Mysheet1(sheetl, listCount.Count + 3, 6, "�������ģ�");
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
                string fileName = "����ú̿���佻�����ĺױ�԰������ú����ͳ���ձ���(����ú)_" + DateTime.Now.ToString("yyyy-MM-dd") + ".xls";
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
