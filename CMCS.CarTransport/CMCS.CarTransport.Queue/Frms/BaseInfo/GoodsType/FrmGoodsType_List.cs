using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using CMCS.Common;
using DevComponents.DotNetBar.SuperGrid;
using CMCS.CarTransport.DAO;
using CMCS.Common.DAO;
using CMCS.Common.Entities.CarTransport;
using CMCS.Common.Entities;

namespace CMCS.CarTransport.Queue.Frms.BaseInfo.GoodsType
{
    public partial class FrmGoodsType_List : DevComponents.DotNetBar.Metro.MetroForm
    {
        /// <summary>
        /// 窗体唯一标识符
        /// </summary>
        public static string UniqueKey = "FrmGoodsType_List";
        /// <summary>
        /// 选中的实体
        /// </summary>
        public CmcsGoodsType Output;

        /// <summary>
        /// 新增 修改 标识
        /// </summary>
        string strEditMode = string.Empty;

        CarTransportDAO carTransportDao = CarTransportDAO.GetInstance();

        public FrmGoodsType_List()
        {
            InitializeComponent();
        }

        private void FrmGoodsType_List_Shown(object sender, EventArgs e)
        {
            advTree1.Nodes.Clear();

            CmcsGoodsType rootEntity = Dbers.GetInstance().SelfDber.Entity<CmcsGoodsType>("where ParentId is null");
            DevComponents.AdvTree.Node rootNode = CreateNode(rootEntity);

            LoadData(rootEntity, rootNode);

            advTree1.Nodes.Add(rootNode);
            addCmcsGoodsType(rootEntity);
            CMCS.CarTransport.Queue.Utilities.Helper.ControlReadOnly(panelLeft);
            CMCS.CarTransport.Queue.Utilities.Helper.ControlReadOnly(panelRight);
        }

        private void FrmGoodsType_List_KeyUp(object sender, KeyEventArgs e)
        {
        }

        void LoadData(CmcsGoodsType entity, DevComponents.AdvTree.Node node)
        {
            if (entity == null || node == null) return;

            foreach (CmcsGoodsType item in Dbers.GetInstance().SelfDber.Entities<CmcsGoodsType>("where ParentId=:ParentId order by OrderNumber asc", new { ParentId = entity.Id }))
            {
                DevComponents.AdvTree.Node newNode = CreateNode(item);
                node.Nodes.Add(newNode);
                LoadData(item, newNode);
            }
        }

        DevComponents.AdvTree.Node CreateNode(CmcsGoodsType entity)
        {
            DevComponents.AdvTree.Node node = new DevComponents.AdvTree.Node(entity.GoodsName + (entity.IsValid == 1 ? "" : "(无效)"));
            node.Tag = entity;
            node.Expanded = true;
            return node;
        }

        private void advTree1_NodeDoubleClick(object sender, DevComponents.AdvTree.TreeNodeMouseEventArgs e)
        {
            //advTree1_NodeClick(sender, e);
        }

        private void advTree1_NodeClick(object sender, DevComponents.AdvTree.TreeNodeMouseEventArgs e)
        {
            Return();
        }
        void Return()
        {
            //if (advTree1.SelectedNode.Parent == null) return;
            this.Output = (advTree1.SelectedNode.Tag as CmcsGoodsType);
            addCmcsGoodsType(Output);
            strEditMode = "edit";
            EnableLeft();
        }

        void addCmcsGoodsType(CmcsGoodsType item)
        {
            txt_GoodsName.Text = item.GoodsName;
            txt_Remark.Text = item.Remark;
            dbi_OrderNumber.Text = item.OrderNumber.ToString();
            chb_IsUse.Checked = (item.IsValid == 1);
        }
        void EnableLeft()
        {
            CMCS.CarTransport.Queue.Utilities.Helper.NoControlReadOnly(panelLeft);
        }
        void EnableRight()
        {
            CMCS.CarTransport.Queue.Utilities.Helper.NoControlReadOnly(panelRight);
        }
        void Clear()
        {
            txt_GoodsName.ResetText();
            dbi_OrderNumber.Value = 0;
            txt_Remark.ResetText();
        }
        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (Output == null)
            {
                MessageBoxEx.Show("请先选择一条记录！，", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            this.strEditMode = "add";
            EnableRight();
            Clear();
            this.dbi_OrderNumber.Value = carTransportDao.GetGoodsTypeOrderNumBer(this.Output);
            this.btnSubmit.Enabled = true;
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (Output == null)
            {
                MessageBoxEx.Show("请先选择一条记录！，", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Output.Id == "-1")
            {
                MessageBoxEx.Show("根节点不能修改！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            this.strEditMode = "edit";
            EnableRight();
        }

        private void BtnDel_Click(object sender, EventArgs e)
        {
            if (Output == null)
            {
                MessageBoxEx.Show("请先选择一条记录！，", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Output.Id == "-1")
            {
                MessageBoxEx.Show("根节点不能删除！，", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBoxEx.Show("确定删除该条记录及其所有子节点？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (!carTransportDao.DelGoodsType(Output))
                    MessageBoxEx.Show("删除失败，有记录正在被使用！，", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                FrmGoodsType_List_Shown(null, null);
            }
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {

            if (strEditMode == "add")
            {
                CmcsGoodsType mine = new CmcsGoodsType()
                {

                    TreeCode = carTransportDao.GetGoodsTypeNewChildCode(Output.TreeCode),
                    GoodsName = txt_GoodsName.Text,
                    IsValid = chb_IsUse.Checked ? 1 : 0,
                    OrderNumber = dbi_OrderNumber.Value,
                    Remark = txt_Remark.Text,
                    ParentId = Output.Id
                };
                carTransportDao.InsertGoodsType(mine);
            }
            else
            {
                Output.GoodsName = txt_GoodsName.Text;
                Output.OrderNumber = dbi_OrderNumber.Value;
                Output.IsValid = chb_IsUse.Checked ? 1 : 0;
                Output.Remark = txt_Remark.Text;
                Output.IsSynch = "0";
                carTransportDao.InsertGoodsType(Output);
            }
            FrmGoodsType_List_Shown(null, null);
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            FrmGoodsType_List_Shown(null, null);
        }
    }
}