using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CMCS.DataTester.Frms;

namespace CMCS.DataTester
{
    public partial class MDIParent1 : Form
    {
        public MDIParent1()
        {
            InitializeComponent();
        }

        #region 窗口

        private void CascadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.Cascade);
        }

        private void TileVerticalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileVertical);
        }

        private void TileHorizontalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            LayoutMdi(MdiLayout.TileHorizontal);
        }

        #endregion

        private void MDIParent1_Load(object sender, EventArgs e)
        {

        }

        private void MDIParent1_Shown(object sender, EventArgs e)
        {

        }

        private void MDIParent1_FormClosing(object sender, FormClosingEventArgs e)
        {

        }

        /// <summary>
        /// Invoke封装
        /// </summary>
        /// <param name="action"></param>
        public void InvokeEx(Action action)
        {
            if (this.IsDisposed || !this.IsHandleCreated) return;

            this.Invoke(action);
        }

        /// <summary>
        /// 火车入厂数据生成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenFrmBuildTrainWeightRecord_Click(object sender, EventArgs e)
        {
            new FrmBuildTrainWeightRecord
            {
                MdiParent = this
            }.Show();
        }

        /// <summary>
        /// 车号识别数据生成
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenFrmBuildTrainCarriagePass_Click(object sender, EventArgs e)
        {
            new FrmBuildTrainCarriagePass
           {
               MdiParent = this
           }.Show();
        }

        /// <summary>
        /// 精敏IO控制器模拟
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenFrmIOSimulator_Click(object sender, EventArgs e)
        {
            new FrmIOSimulator
           {
               MdiParent = this
           }.Show();
        }

        /// <summary>
        /// 托利多地磅IND245模拟工具
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenFrmWBSimulator_Click(object sender, EventArgs e)
        {
            new FrmWB245Simulator
           {
               MdiParent = this
           }.Show();
        }

        /// <summary>
        /// 托利多电子秤IND231模拟工具
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenFrmWB231Simulator_Click(object sender, EventArgs e)
        {
            new FrmWB231Simulator
            {
                MdiParent = this
            }.Show();
        }



        /// <summary>
        /// 托利多电子秤IND231模拟工具
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenFrmAutoCupBoard_Test_Click(object sender, EventArgs e)
        {
            new FrmAutoCupBoard_Test
            {
                MdiParent = this
            }.Show();
        }

        /// <summary>
        /// 皮带采样机模拟
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenFrmBeltSamplerSimulator_Click(object sender, EventArgs e)
        {
            new FrmBeltSamplerSimulator
            {
                MdiParent = this
            }.Show();
        }

        /// <summary>
        /// 全自动制样模拟
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenFrmAutoMakerSimulator_Click(object sender, EventArgs e)
        {
            new FrmAutoMakerSimulator
            {
                MdiParent = this
            }.Show();
        }

        /// <summary>
        /// 汽车机械采样机模拟
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenFrmCarJxSamplerSimulator_Click(object sender, EventArgs e)
        {
            new FrmCarJxSamplerSimulator
            {
                MdiParent = this
            }.Show();
        }

        /// <summary>
        /// 通通停车
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenThinkCamera_Click(object sender, EventArgs e)
        {
            new FrmThinkCamera
            {
                MdiParent = this
            }.Show();
        }

        private void 耀华地磅ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FrmWBYaoHua
            {
                MdiParent = this
            }.Show();
        }

        private void 进制转换ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FrmCodeChange
            {
                MdiParent = this
            }.Show();
        }

        private void HFReader_Click(object sender, EventArgs e)
        {
            new FrmHFReader
            {
                MdiParent = this
            }.Show();
        }

        private void QRCode_Click(object sender, EventArgs e)
        {
            new FrmQRCode
            {
                MdiParent = this
            }.Show();
        }

        private void btnOpenFinger_Click(object sender, EventArgs e)
        {
            new FrmFinger
            {
                MdiParent = this
            }.Show();
        }

        private void tmisTest_Click(object sender, EventArgs e)
        {
            new FrmTest
            {
                MdiParent = this
            }.Show();
        }

        private void pLC测试ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FrmPLCTest
            {
                MdiParent = this
            }.Show();
        }

        private void modBus测试ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FrmModBusTest
            {
                MdiParent = this
            }.Show();
        }

        private void 赛多利斯电子天平模拟工具ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FrmSartoriusBalance
            {
                MdiParent = this
            }.Show();
        }

        private void 添加管理凭据ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FrmNative
            {
                MdiParent = this
            }.Show();
        }

        private void 湘平电子秤ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FrmWBXiangPing
            {
                MdiParent = this
            }.Show();
        }

        private void 赛摩皮带采样机测试ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FrmBeltSamplerCmdTest
            {
                MdiParent = this
            }.Show();
        }

        private void 四舍六入测试ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FrmFourTest
            {
                MdiParent = this
            }.Show();
        }
    }
}
