using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using CMCS.Common.DAO;
using CMCS.Common.Entities.CarTransport;
using CMCS.Common.Enums;
using CMCS.Common.Entities.Sys;

namespace CMCS.MobilePad.Win.Frms
{
    public partial class FrmCarManage_Confirm : DevComponents.DotNetBar.Metro.MetroForm
    {
        string autotruckId;
        string transportId;
        string carType;

        CommonDAO commonDAO = CommonDAO.GetInstance();

        public FrmCarManage_Confirm(string transportId)
        {
            InitializeComponent();

            LoadIsUse();

            this.transportId = transportId;
            CmcsAutotruck entity = commonDAO.SelfDber.Get<CmcsAutotruck>(this.transportId);
            if (entity != null)
            {
                txtCarNum.Text = entity.CarNumber;
                txtDriver.Text = entity.Driver;
                txtCellPhoneNumber.Text = entity.CellPhoneNumber;

                if (entity.IsUse == 0)
                {
                    ddlIsUse.SelectedIndex = 1;
                }
                else if (entity.IsUse == 1)
                {
                    ddlIsUse.SelectedIndex = 0;
                }
                else
                {
                    ddlIsUse.SelectedIndex = 2;
                }
                ((ComboBoxItem)ddlIsUse.SelectedItem).Name = entity.IsUse.ToString();

                txtCarriageLength.Text = entity.CarriageLength.ToString();
                txtCarriageWidth.Text = entity.CarriageWidth.ToString();
                txtCarriageBottomToFloor.Text = entity.CarriageBottomToFloor.ToString();

                txtLeftObstacle1.Text = entity.LeftObstacle1.ToString();
                txtLeftObstacle2.Text = entity.LeftObstacle2.ToString();
                txtLeftObstacle3.Text = entity.LeftObstacle3.ToString();
                txtLeftObstacle4.Text = entity.LeftObstacle4.ToString();
                txtLeftObstacle5.Text = entity.LeftObstacle5.ToString();
                txtLeftObstacle6.Text = entity.LeftObstacle6.ToString();

                txtRightObstacle1.Text = entity.RightObstacle1.ToString();
                txtRightObstacle2.Text = entity.RightObstacle2.ToString();
                txtRightObstacle3.Text = entity.RightObstacle3.ToString();
                txtRightObstacle4.Text = entity.RightObstacle4.ToString();
                txtRightObstacle5.Text = entity.RightObstacle5.ToString();
                txtRightObstacle6.Text = entity.RightObstacle6.ToString();

				txt_ReMark.Text = entity.ReMark;
                
            }
        } 

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            CmcsAutotruck entity = commonDAO.SelfDber.Get<CmcsAutotruck>(this.transportId);
            if (entity != null)
            {
                entity.Driver = txtDriver.Text;
                entity.CellPhoneNumber = txtCellPhoneNumber.Text;
                entity.CarriageWidth = Convert.ToInt32(txtCarriageWidth.Text);
                entity.CarriageLength = Convert.ToInt32(txtCarriageLength.Text);
                entity.CarriageBottomToFloor = Convert.ToInt32(txtCarriageBottomToFloor.Text);
                if (ddlIsUse.SelectedIndex == 0)
                {
                    entity.IsUse = 1;
                }
                else if (ddlIsUse.SelectedIndex == 1)
                {
                    entity.IsUse = 0;
                }
                else
                {
                    entity.IsUse = -1;
                }
                entity.LeftObstacle1 = Convert.ToInt32(txtLeftObstacle1.Text);
                entity.LeftObstacle2 = Convert.ToInt32(txtLeftObstacle2.Text);
                entity.LeftObstacle3 = Convert.ToInt32(txtLeftObstacle3.Text);
                entity.LeftObstacle4 = Convert.ToInt32(txtLeftObstacle4.Text);
                entity.LeftObstacle5 = Convert.ToInt32(txtLeftObstacle5.Text);
                entity.LeftObstacle6 = Convert.ToInt32(txtLeftObstacle6.Text);

                entity.RightObstacle1 = Convert.ToInt32(txtRightObstacle1.Text);
                entity.RightObstacle2 = Convert.ToInt32(txtRightObstacle2.Text);
                entity.RightObstacle3 = Convert.ToInt32(txtRightObstacle3.Text);
                entity.RightObstacle4 = Convert.ToInt32(txtRightObstacle4.Text);
                entity.RightObstacle5 = Convert.ToInt32(txtRightObstacle5.Text);
                entity.RightObstacle6 = Convert.ToInt32(txtRightObstacle6.Text);

                entity.ReMark = txt_ReMark.Text;

                commonDAO.SelfDber.Update<CmcsAutotruck>(entity);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void labelX19_Click(object sender, EventArgs e)
        {

        }

        private void doubleInput12_ValueChanged(object sender, EventArgs e)
        {

        }

        private void panelEx2_Click(object sender, EventArgs e)
        {

        }

        private void FrmCarManage_Confirm_Load(object sender, EventArgs e)
        {
            
        }

        private void LoadIsUse()
        {
            ddlIsUse.Items.Add(new ComboBoxItem("1", "有效"));
            ddlIsUse.Items.Add(new ComboBoxItem("0", "无效"));
            ddlIsUse.Items.Add(new ComboBoxItem("-1", "黑名单"));
            ddlIsUse.SelectedIndex = 0;
        }
    }
}