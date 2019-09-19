using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CMCS.Common.DAO;
using DevComponents.DotNetBar.Controls;
using DevComponents.DotNetBar.Metro;
using DevComponents.DotNetBar.SuperGrid;
using CMCS.WeighCheck.DAO;
using CMCS.Common.Entities.Fuel;
using DevComponents.DotNetBar;
using CMCS.Common.Enums;
using CMCS.Common.Entities.BaseInfo;
using CMCS.WeighCheck.MakeChange.Utilities;
using CMCS.Common;

namespace CMCS.WeighCheck.MakeChange.Frms
{
    public partial class FrmAssayTypeSelect : MetroForm
    {
        CZYHandlerDAO cZYHandlerDAO = CZYHandlerDAO.GetInstance();

        FrmSpotCheck From;
        /// <summary>
        /// 当前化验码
        /// </summary>
        string AssayCode = "";

        string assayTarget = string.Empty;

        public FrmAssayTypeSelect()
        {
            InitializeComponent();
        }

        public FrmAssayTypeSelect(FrmSpotCheck from, string assayCode)
        {
            InitializeComponent();
            this.AssayCode = assayCode;
            this.From = from;
        }

        private void Frm_Batch_Select_Load(object sender, EventArgs e)
        {
            cmbAssayType.Items.Add("抽查样化验");
            cmbAssayType.SelectedIndex = 0;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            string Message = string.Empty;
            if (!cZYHandlerDAO.CheckSpotAssay(AssayCode, ref Message))
            {
                MessageBoxEx.Show(Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (string.IsNullOrWhiteSpace(this.assayTarget))
            {
                MessageBoxEx.Show("请选择化验指标！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            CmcsRCAssay entity = CommonDAO.GetInstance().SelfDber.Entity<CmcsRCAssay>("where AssayCode=:AssayCode order by CreateDate desc", new { AssayCode = AssayCode });
            if (cZYHandlerDAO.CreateSpotAssay(entity, SelfVars.LoginUserNames, SelfVars.LoginUserAccounts, this.assayTarget.TrimEnd(','), cmbAssayType.Text, ref this.From.CurrentAssay))
            {
                if (MessageBoxEx.Show("生成抽查样成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    this.Close();
                    this.DialogResult = DialogResult.OK;
                }
            }
            else
            {
                if (MessageBoxEx.Show("生成抽查样失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning) == DialogResult.OK)
                {
                    this.Close();
                    this.DialogResult = DialogResult.OK;
                }
            }
        }

        /// <summary>
        /// 选择化验指标
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void assayTarget_CheckedChanged(object sender, EventArgs e)
        {
            CheckBoxX check = (CheckBoxX)sender;
            if (check.Checked && !this.assayTarget.Contains(check.Text))
            {
                this.assayTarget += check.Text + ",";
            }
            else
            {
                this.assayTarget = this.assayTarget.Replace(check.Text + ",", "");
            }
        }

        /// <summary>
        /// 选择化验指标类型
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtnHandChoose_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton rbtn = (RadioButton)sender;
            if (rbtn.Tag.ToString() == "手选指标")
            {
                foreach (Control item in this.panelAssayTarget.Controls)
                {
                    CheckBoxX checkother = (CheckBoxX)item;
                    checkother.Checked = false;
                }
            }
            else if (rbtn.Tag.ToString() == "全指标")
            {
                foreach (Control item in this.panelAssayTarget.Controls)
                {
                    CheckBoxX checkother = (CheckBoxX)item;
                    checkother.Checked = true;
                    //this.assayTarget += checkother.Text + ",";
                }
            }
            else if (rbtn.Tag.ToString() == "日常分析")
            {
                foreach (Control item in this.panelAssayTarget.Controls)
                {
                    CheckBoxX checkother = (CheckBoxX)item;
                    if (checkother.Text == "氢值" || checkother.Text == "灰熔融")
                    {
                        checkother.Checked = false;
                        continue;
                    }
                    checkother.Checked = true;
                    //this.assayTarget += checkother.Text + ",";
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
