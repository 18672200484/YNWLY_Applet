using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
//
using DevComponents.DotNetBar;
using CMCS.Common.DAO;
using CMCS.Common.Entities.CarTransport;
using CMCS.Common.Enums;
using CMCS.Common.Entities.Sys;
using CMCS.Common.Entities.Fuel;
using CMCS.Common;
using CMCS.Common.Entities.iEAA;
using CMCS.Common.Entities.CoalPot;

namespace CMCS.MobilePad.Win.Frms.CarShippChange
{
    public partial class FrmCarShippChange_Confirm : DevComponents.DotNetBar.Metro.MetroForm
    {
        string SalesOrderId;
        string Id;
        string LmybId;
        CmcsSaleFuelTransport entity;
        public FrmCarShippChange_Confirm(string salesOrderId, string id, string lmybid)
        {
            InitializeComponent();

            this.SalesOrderId = salesOrderId;
            this.Id = id;
            this.LmybId = lmybid;
            LoadUnLoadArea();
        }

        private void FrmCarShippChange_Confirm_Load(object sender, EventArgs e)
        {
            entity = CommonDAO.GetInstance().SelfDber.Get<CmcsSaleFuelTransport>(this.Id);
            if (entity == null)
            {
                this.Close();
            }

            txtCarNum.Text = entity.CarNumber;
            if (entity.StorageName != null)
            {
                cmbShippSpace.Text = entity.StorageName;
            }
            else
            {
                cmbShippSpace.Text = entity.CPCName;
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (this.entity != null)
            {
                ComboBoxItem cb = (ComboBoxItem)cmbShippSpace.SelectedItem;
                if (cmbShippSpace.Text.Contains("成品仓"))
                {
                    entity.CPCName = cb.Text;
                    entity.CPCId = cb.Name;
                    entity.StorageName = "";
                    entity.StorageId = "";
                }
                else
                {
                    entity.StorageName = cb.Text;
                    entity.StorageId = cb.Name;
                    entity.CPCName = "";
                    entity.CPCId = "";

                }
                Dbers.GetInstance().SelfDber.Update(entity);
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// 绑定成品仓
        /// </summary>
        private void LoadUnLoadArea()
        {
            IList<FulCoalPot> coalPotlist = CommonDAO.GetInstance().SelfDber.Entities<FulCoalPot>("where SalesOrderId=:SalesOrderId", new { SalesOrderId = this.SalesOrderId });
            if (coalPotlist == null || coalPotlist.Count == 0)
                coalPotlist = CommonDAO.GetInstance().SelfDber.Entities<FulCoalPot>("where PotName like '%成品仓%'");
            foreach (FulCoalPot item in coalPotlist)
            {
                this.cmbShippSpace.Items.Add(new ComboBoxItem(item.Id, item.PotName));
            }

            CmcsLMYB lmyb = CommonDAO.GetInstance().SelfDber.Get<CmcsLMYB>(this.LmybId);
            if (lmyb != null && !string.IsNullOrEmpty(lmyb.StorageName))
            {
                string[] StorageNames = lmyb.StorageName.Split('|');
                string[] StorageIds = lmyb.StorageId.Split('|');
                if (StorageNames.Length == StorageIds.Length)
                {
                    for (int i = 0; i < StorageNames.Length; i++)
                    {
                        this.cmbShippSpace.Items.Add(new ComboBoxItem(StorageIds[i], StorageNames[i]));
                    }
                }
            }
            this.cmbShippSpace.SelectedIndex = 0;

        }

    }
}