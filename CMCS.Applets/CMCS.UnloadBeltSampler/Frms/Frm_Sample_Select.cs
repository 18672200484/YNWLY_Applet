using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar.Metro;
using DevComponents.DotNetBar.Controls;
using CMCS.Common.DAO;
namespace CMCS.UnloadSampler.Frms
{
    public partial class Frm_Sample_Select : MetroAppForm
    {
        public Form1 _Form1;
        List<SampleInfo> listSampleInfo = new List<SampleInfo>();
        bool isExit = true;

        /// <summary>
        /// 当前日期
        /// </summary>
        DateTime CurrentDay = DateTime.Now;

        public Frm_Sample_Select()
        {
            InitializeComponent();
        }

        private void Frm_Batch_Select_Load(object sender, EventArgs e)
        {
            //_Form1 = this.Owner as Form1;
            dtInputStart.Value = dtInputEnd.Value = DateTime.Now;

            BindData(MakerDAO.GetInstance().GetSampleInfo(DateTime.Parse(dtInputStart.Text), DateTime.Parse(dtInputEnd.Text).AddDays(1)));
        }

        private void BindData(DataTable dt)
        {
            listSampleInfo.Clear();
            lvwInfactoryBatch.Items.Clear();
            if (dt != null)
            {
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
                        SamplingType = drSample["SamplingType"].ToString()
                    });
                }
            }

            foreach (SampleInfo infactorybatch in listSampleInfo)
            {
                ListViewItem item = new ListViewItem(infactorybatch.Batch);
                item.SubItems.Add(infactorybatch.SupplierName);
                item.SubItems.Add(infactorybatch.MineName);
                item.SubItems.Add(infactorybatch.SamplingDate.ToString("yyyy-MM-dd"));
                item.SubItems.Add(infactorybatch.TransportNumber.ToString());
                item.SubItems.Add(infactorybatch.KindName);
                item.SubItems.Add(infactorybatch.FactarriveDate.ToString("yyyy-MM-dd"));
                item.Tag = infactorybatch;
                lvwInfactoryBatch.Items.Add(item);
            }
        }

        private void lvwInfactoryBatch_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewEx lvw = sender as ListViewEx;
            if (lvw != null)
            {
                ListViewItem item = lvw.GetItemAt(e.X, e.Y);
                isExit = false;
                this.Close();
            }
        }

        private void Frm_SupplierUnit_Selet_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(dtInputStart.Text))
                return;
            if (string.IsNullOrEmpty(dtInputEnd.Text))
                return;

            BindData(MakerDAO.GetInstance().GetSampleInfo(DateTime.Parse(dtInputStart.Text), DateTime.Parse(dtInputEnd.Text).AddDays(1)));
        }

        private void btnPreDay_Click(object sender, EventArgs e)
        {
            CurrentDay = CurrentDay.AddDays(-1);
            ShowDateTime();
            BindData(MakerDAO.GetInstance().GetSampleInfo(DateTime.Parse(dtInputStart.Text), DateTime.Parse(dtInputEnd.Text).AddDays(1)));
        }

        private void btnNextDay_Click(object sender, EventArgs e)
        {
            CurrentDay = CurrentDay.AddDays(1);
            ShowDateTime();
            BindData(MakerDAO.GetInstance().GetSampleInfo(DateTime.Parse(dtInputStart.Text), DateTime.Parse(dtInputEnd.Text).AddDays(1)));
        }

        private void btnToday_Click(object sender, EventArgs e)
        {
            CurrentDay = DateTime.Now;
            ShowDateTime();
            BindData(MakerDAO.GetInstance().GetSampleInfo(DateTime.Parse(dtInputStart.Text), DateTime.Parse(dtInputEnd.Text).AddDays(1)));
        }

        #region DateTime
        private void ShowDateTime()
        {
            dtInputStart.Value = dtInputEnd.Value = DateTime.Parse(CurrentDay.ToString("yyyy-MM-dd"));
        }
        #endregion
    }

    public class SampleInfo
    {
        public string Id { get; set; }
        public string BatchId { get; set; }
        public string Batch { get; set; }
        public string SupplierName { get; set; }
        public string MineName { get; set; }
        public string KindName { get; set; }
        public string StationName { get; set; }
        public DateTime FactarriveDate { get; set; }
        public int TransportNumber { get; set; }
        public string SampleCode { get; set; }
        public DateTime SamplingDate { get; set; }
        public string SamplingType { get; set; }
    }
}
