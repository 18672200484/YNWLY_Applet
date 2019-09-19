using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using CMCS.Common;
using CMCS.Common.Entities.CarTransport;
using CMCS.Common.Entities.Fuel;

namespace CMCS.CarTransport.Queue.Frms.BaseInfo.SupplyReceive
{
    public partial class FrmCustomer_Oper : DevComponents.DotNetBar.Metro.MetroForm
    {
        String id = String.Empty;
        bool edit = false;
        Customer Customer;
        public FrmCustomer_Oper()
        {
            InitializeComponent();
        }
        public FrmCustomer_Oper(String pId, bool pEdit)
        {
            InitializeComponent();
            id = pId;
            edit = pEdit;
        }
        private void FrmSupplyReceive_Oper_Load(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(id))
            {
                this.Customer = Dbers.GetInstance().SelfDber.Get<Customer>(this.id);
                txt_CustomerCode.Text = Customer.CustomerCode;
                txt_Email.Text = Customer.Email;
                txt_Linker.Text = Customer.Linker;
                txt_LinkPhone.Text = Customer.LinkPhone;
                txt_Address.Text = Customer.Address;
                txt_CustomerName.Text = Customer.CustomerName;
                chb_IsUse.Checked = (Customer.Valid == "1");
                txt_Remark.Text = Customer.Remark;
            }
            if (!edit)
            {
                btnSubmit.Enabled = false;
                CMCS.CarTransport.Queue.Utilities.Helper.ControlReadOnly(panelEx2);
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (txt_CustomerCode.Text.Length == 0)
            {
                MessageBoxEx.Show("客户编号不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            int count = Dbers.GetInstance().SelfDber.Entities<Customer>(" where CustomerCode=:CustomerCode", new { CustomerCode = txt_CustomerCode.Text }).Count;
            if ((Customer == null || Customer.CustomerCode != txt_CustomerCode.Text) && count > 0)
            {
                MessageBoxEx.Show("客户编号不可重复！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (txt_CustomerName.Text.Length == 0)
            {
                MessageBoxEx.Show("客户名称不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
            count = Dbers.GetInstance().SelfDber.Entities<Customer>(" where CustomerName=:CustomerName", new { CustomerName = txt_CustomerName.Text }).Count;
            if ((Customer == null || Customer.CustomerName != txt_CustomerName.Text) && count > 0)
            {
                MessageBoxEx.Show("客户名称不可重复！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Customer != null)
            {
                Customer.CustomerCode = txt_CustomerCode.Text;
                Customer.CustomerName = txt_CustomerName.Text;
                Customer.Email = txt_Email.Text;
                Customer.Linker = txt_Linker.Text;
                Customer.LinkPhone = txt_LinkPhone.Text;
                Customer.Address = txt_Address.Text;
                Customer.Valid = chb_IsUse.Checked ? "1" : "0";
                Customer.Remark = txt_Remark.Text;
                Dbers.GetInstance().SelfDber.Update(Customer);
            }
            else
            {
                Customer = new Customer();
                Customer.CustomerCode = txt_CustomerCode.Text;
                Customer.CustomerName = txt_CustomerName.Text;
                Customer.Email = txt_Email.Text;
                Customer.Linker = txt_Linker.Text;
                Customer.LinkPhone = txt_LinkPhone.Text;
                Customer.Address = txt_Address.Text;
                Customer.Valid = chb_IsUse.Checked ? "1" : "0";
                Customer.Remark = txt_Remark.Text;
                Dbers.GetInstance().SelfDber.Insert(Customer);
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    }
}