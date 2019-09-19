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
using CMCS.Common.Entities.Fuel;
using CMCS.Common;
using CMCS.Common.Entities.iEAA;

namespace CMCS.MobilePad.Win.Frms.UnLoadPlan
{
	public partial class FrmUnLoadPlan_Confirm : DevComponents.DotNetBar.Metro.MetroForm
	{
		List<FulUnLoadPlanDetail> Currentlist;

		public FrmUnLoadPlan_Confirm(List<FulUnLoadPlanDetail> list)
		{
			InitializeComponent();

			this.Currentlist = list;
			LoadUnLoadArea();
		}

		private void btnCancel_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.No;
			this.Close();
		}

		private void btnSave_Click(object sender, EventArgs e)
		{
			if (string.IsNullOrWhiteSpace(cmbUnLoadArea.Text))
			{
				MessageBoxEx.Show("��ѡ��жú����", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			try
			{
				foreach (FulUnLoadPlanDetail item in this.Currentlist)
				{
					item.UnLoadArea = this.cmbUnLoadArea.Text;
					Dbers.GetInstance().SelfDber.Update(item);
				}
				MessageBoxEx.Show("���óɹ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Information);
				this.DialogResult = DialogResult.OK;
				this.Close();
			}
			catch (Exception)
			{
				MessageBoxEx.Show("����ʧ��", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

		/// <summary>
		/// ��жú����
		/// </summary>
		private void LoadUnLoadArea()
		{
			//List<CodeContent> content = CommonDAO.GetInstance().GetCodeContentByKind("жú����");
			//foreach (CodeContent item in content)
			//{
			//    this.cmbUnLoadArea.Items.Add(item.Content);
			//}

			this.cmbUnLoadArea.Items.Add("1��жú��");
			this.cmbUnLoadArea.Items.Add("2��жú��");
			this.cmbUnLoadArea.Items.Add("3��жú��");
			this.cmbUnLoadArea.Items.Add("4��жú��");
		}
	}
}