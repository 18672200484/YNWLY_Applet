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
using CMCS.Common.Entities.BaseInfo;

namespace CMCS.CarTransport.Queue.Frms.BaseInfo.Mine
{
    public partial class FrmMine_List : DevComponents.DotNetBar.Metro.MetroForm
    {
        /// <summary>
        /// ����Ψһ��ʶ��
        /// </summary>
        public static string UniqueKey = "FrmMine_List";
        /// <summary>
        /// ���� �޸� ��ʶ
        /// </summary>
        string strEditMode = string.Empty;
        /// <summary>
        /// ѡ�е�ʵ��
        /// </summary>
        public CmcsMine Output;

        CarTransportDAO carTransportDao = CarTransportDAO.GetInstance();

        public FrmMine_List()
        {
            InitializeComponent();
        }

        private void FrmMine_List_Shown(object sender, EventArgs e)
        {
            advTree1.Nodes.Clear();

            CmcsMine rootEntity = Dbers.GetInstance().SelfDber.Entity<CmcsMine>("where ParentId is null");
            DevComponents.AdvTree.Node rootNode = CreateNode(rootEntity);

            LoadData(rootEntity, rootNode);

            advTree1.Nodes.Add(rootNode);
            addCmcsMine(rootEntity);
            CMCS.CarTransport.Queue.Utilities.Helper.ControlReadOnly(panelLeft);
            CMCS.CarTransport.Queue.Utilities.Helper.ControlReadOnly(panelRight);
            Clear();
            this.btnSubmit.Enabled = false;
        }

        private void FrmMine_List_KeyUp(object sender, KeyEventArgs e)
        {
        }

        void LoadData(CmcsMine entity, DevComponents.AdvTree.Node node)
        {
            if (entity == null || node == null) return;

            foreach (CmcsMine item in Dbers.GetInstance().SelfDber.Entities<CmcsMine>("where ParentId=:ParentId order by Sequence asc", new { ParentId = entity.Id }))
            {
                DevComponents.AdvTree.Node newNode = CreateNode(item);
                node.Nodes.Add(newNode);
                LoadData(item, newNode);
            }
        }

        DevComponents.AdvTree.Node CreateNode(CmcsMine entity)
        {
            DevComponents.AdvTree.Node node = new DevComponents.AdvTree.Node(entity.Name + ((entity.Valid == "��Ч") ? "" : "(��Ч)"));
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
            this.Output = (advTree1.SelectedNode.Tag as CmcsMine);
            addCmcsMine(Output);
            strEditMode = "edit";
            EnableLeft();
        }

        void addCmcsMine(CmcsMine item)
        {
            txt_Name.Text = item.Name;
            txt_ReMark.Text = item.ReMark;
            dbi_Sequence.Text = item.Sequence.ToString();
            chb_IsUse.Checked = (item.Valid == "��Ч");
        }
        void EnableLeft()
        {
            CMCS.CarTransport.Queue.Utilities.Helper.NoControlReadOnly(panelLeft);
        }
        void EnableRight()
        {
            CMCS.CarTransport.Queue.Utilities.Helper.NoControlReadOnly(panelRight);
            btnSubmit.Enabled = true;
        }
        void Clear()
        {
            txt_Name.ResetText();
            dbi_Sequence.Value = 0;
            txt_ReMark.ResetText();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            if (Output == null)
            {
                MessageBoxEx.Show("����ѡ��һ����¼����", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            this.strEditMode = "add";
            EnableRight();
            Clear();
            this.dbi_Sequence.Value = carTransportDao.GetMineOrderNumBer(this.Output);
            this.btnSubmit.Enabled = true;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {

            if (strEditMode == "add")
            {
                if (CommonDAO.GetInstance().SelfDber.Count<CmcsMine>(" where Name=:Name", new { Name = txt_Name.Text }) > 0)
                {
                    MessageBoxEx.Show("�ÿ�������Ѵ��ڣ���", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                CmcsMine mine = new CmcsMine()
                {
                    Code = carTransportDao.GetMineNewChildCode(Output.Code),
                    Name = txt_Name.Text,
                    Valid = chb_IsUse.Checked ? "��Ч" : "��Ч",
                    Sequence = dbi_Sequence.Value,
                    ReMark = txt_ReMark.Text,
                    ParentId = Output.Id
                };
                carTransportDao.InsertMine(mine);
            }
            else
            {
                CmcsMine mine_check = CommonDAO.GetInstance().SelfDber.Entity<CmcsMine>(" where Name=:Name", new { Name = txt_Name.Text });
                if (mine_check != null && mine_check.Id != Output.Id)
                {
                    MessageBoxEx.Show("�ÿ�������Ѵ��ڣ���", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
                //Output.Code = carTransportDao.GetMineNewChildCode(Output.Code);
                //Output.NodeCode = carTransportDao.GetMineNewChildCode(Output.Code);
                Output.Name = txt_Name.Text;
                Output.Sequence = dbi_Sequence.Value;
                Output.Valid = chb_IsUse.Checked ? "��Ч" : "��Ч";
                Output.ReMark = txt_ReMark.Text;
                Output.IsSynch = "0";
                carTransportDao.InsertMine(Output);
            }
            FrmMine_List_Shown(null, null);
        }

        private void BtnEdit_Click(object sender, EventArgs e)
        {
            if (Output == null)
            {
                MessageBoxEx.Show("����ѡ��һ����¼����", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Output.Id == "-1")
            {
                MessageBoxEx.Show("���ڵ㲻���޸ģ�", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            this.strEditMode = "edit";
            EnableRight();
        }

        private void BtnDel_Click(object sender, EventArgs e)
        {
            if (Output == null)
            {
                MessageBoxEx.Show("����ѡ��һ����¼����", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (Output.Id == "-1")
            {
                MessageBoxEx.Show("���ڵ㲻��ɾ������", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            if (MessageBoxEx.Show("ȷ��ɾ��������¼���������ӽڵ㣿", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (!carTransportDao.DelMine(Output))
                    MessageBoxEx.Show("ɾ��ʧ�ܣ��м�¼���ڱ�ʹ�ã���", "��ʾ", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                FrmMine_List_Shown(null, null);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            FrmMine_List_Shown(null, null);
        }
    }
}