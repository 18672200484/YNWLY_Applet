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
using CMCS.CarTransport.DAO;
using CMCS.Common.Enums;
using CMCS.Common.Entities.Sys;

namespace CMCS.CarTransport.Queue.Frms
{
    public partial class FrmTransport_Confirm : DevComponents.DotNetBar.Metro.MetroForm
    {
        string autotruckId;

        string transportId;
        string carType;

        CommonDAO commonDAO = CommonDAO.GetInstance();

        public FrmTransport_Confirm(string transportId, string carType)
        {
            InitializeComponent();

            this.transportId = transportId;
            this.carType = carType;
            commonDAO.InsertConfirmEvent(eConfirmType.δ��������¼.ToString(), this.transportId);
            timer1.Enabled = true;
        }

        private void FrmTransport_Confirm_Shown(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(this.transportId)) return;

            if (this.carType == eCarType.�볡ú.ToString())
            {
                CmcsBuyFuelTransport transport = commonDAO.SelfDber.Get<CmcsBuyFuelTransport>(this.transportId);
                if (transport != null)
                {
                    this.autotruckId = transport.AutotruckId;

                    txtSerialNumber.Text = transport.SerialNumber;
                    txtCarNumber.Text = transport.CarNumber;
                    txtInFactoryTime.Text = transport.InFactoryTime.ToString("yyyy-MM-dd HH:mm");
                }
            }
            else if (this.carType == eCarType.����ú.ToString())
            {
                CmcsSaleFuelTransport transport = commonDAO.SelfDber.Get<CmcsSaleFuelTransport>(this.transportId);
                if (transport != null)
                {
                    this.autotruckId = transport.AutotruckId;

                    txtSerialNumber.Text = transport.SerialNumber;
                    txtCarNumber.Text = transport.CarNumber;
                    txtInFactoryTime.Text = transport.InFactoryTime.ToString("yyyy-MM-dd HH:mm");
                }
            }
            else if (this.carType == eCarType.��������.ToString())
            {
                CmcsGoodsTransport transport = commonDAO.SelfDber.Get<CmcsGoodsTransport>(this.transportId);
                if (transport != null)
                {
                    this.autotruckId = transport.AutotruckId;

                    txtSerialNumber.Text = transport.SerialNumber;
                    txtCarNumber.Text = transport.CarNumber;
                    txtInFactoryTime.Text = transport.InFactoryTime.ToString("yyyy-MM-dd HH:mm");
                }
            }
            else if (this.carType == eCarType.���ó���.ToString())
            {
                CmcsVisitTransport transport = commonDAO.SelfDber.Get<CmcsVisitTransport>(this.transportId);
                if (transport != null)
                {
                    this.autotruckId = transport.AutotruckId;

                    txtSerialNumber.Text = transport.SerialNumber;
                    txtCarNumber.Text = transport.CarNumber;
                    txtInFactoryTime.Text = transport.InFactoryTime.ToString("yyyy-MM-dd HH:mm");
                }
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            CarTransportDAO.GetInstance().ChangeUnFinishTransportToInvalid(this.autotruckId);
            this.DialogResult = DialogResult.Yes;
            this.Close();
        }

        /// <summary>
        /// ֱ�Ӹ������¼
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUseThis_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }

        /// <summary>
        /// ����ȷ�Ͻ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Stop();
            try
            {
                CmcsConfirm confirm = commonDAO.GetConfirm(this.transportId, eConfirmType.δ��������¼.ToString());
                if (confirm != null && !string.IsNullOrEmpty(confirm.ResultId))
                {
                    if (confirm.ResultId == this.transportId)//ʹ�õ�ǰ�����¼
                        this.DialogResult = DialogResult.OK;
                    else
                        this.DialogResult = DialogResult.Yes;
                    this.Close();
                }
            }
            catch { }
            timer1.Start();
        }
    }
}