using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CMCS.DumblyConcealer.Win.DumblyTasks;
using CMCS.DumblyConcealer.Win.Core;
using BasisPlatform.Util;

namespace CMCS.DumblyConcealer.Win
{
    public partial class MDIParent1 : BasisPlatform.Forms.FrmBasis
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

#if DEBUG
            timer1.Enabled = false;
#else
            this.IsSecretRunning = true;
            this.VerifyBeforeClose = true;
            this.ChangeNotifyIcon(this.Icon);
            timer1.Enabled = true;
#endif
        }

        private void MDIParent1_Shown(object sender, EventArgs e)
        {
            BasisPlatformUtil.StartNewTask("开机延迟启动", () =>
            {
                int minute = 5, surplus = minute;

                while (minute > 0)
                {
                    double d = minute - Environment.TickCount / 1000 / 60;
                    if (Environment.TickCount < 0 || d <= 0) break;

                    System.Threading.Thread.Sleep(60000);

                    surplus--;
                }
#if DEBUG

#else
                this.InvokeEx(() => { timer1.Enabled = true; });
#endif
            });
        }

        private void MDIParent1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                if (MessageBox.Show("确认退出系统？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {

                }
                else
                {
                    e.Cancel = true;
                }
            }
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
        /// 窗体是否打开
        /// </summary>
        /// <param name="form"></param>
        /// <param name="parentForm"></param>
        /// <returns></returns>
        private bool HaveOpened(Form form, Form parentForm)
        {
            bool bReturn = true;
            foreach (Form item in parentForm.MdiChildren)
            {
                if (form.GetType().Name == item.GetType().Name)
                {
                    item.BringToFront();
                    item.Focus();
                    bReturn = false;
                    break;
                }
            }
            return bReturn;
        }

        /// <summary>
        /// 任务索引
        /// </summary>
        int taskFormIndex = 0;

        private void timer1_Tick(object sender, EventArgs e)
        {
            switch (taskFormIndex)
            {
                case 0:
                    tsmiOpenFrmWeightBridger_Click(null, null);
                    break;
                case 1:
                    tsmiOpenFrmBeltSampler_Click(null, null);
                    break;
                case 2:
                    tsmiOpenFrmAutoMaker_NCGM_Click(null, null);
                    break;
                case 3:
                    tsmiOpenFrmAutoCupboard_NCGM_Click(null, null);
                    break;
                case 5:
                    //tsmiOpenFrmAssayDevice_Click(null, null);
                    break;
                case 7:
                    tsmiOpenFrmCarSampler_CSKY_Click(null, null);
                    break;
                case 8: tsmiOpenFrmCarSynchronous_Click(null, null);
                    break;

                case 9: tsmiOpenPackingBatch_Click(null, null);
                    break;

                case 10:
                    tsmiOpenPneumaticTransfer_Click(null, null);
                    break;

                case 11:
                    tsmiOpenPLC_Click(null, null);
                    break;

                case 13:
                    tsmiOpenTemperator_Click(null, null);
                    break;

                case 14:
                    tsmiOpenTemperatureHumidity_Click(null, null);
                    break;

                case 15:
                    tsmiOpenHighBeltWeight_Click(null, null);
                    break;
                case 16:
                    tsiOpenSignalData_Click(null, null);
                    break;
                case 17:
                    TileHorizontalToolStripMenuItem_Click(null, null);
                    break;
            }


            taskFormIndex++;
        }

        /// <summary>
        /// 01.同步轨道衡数据、入厂车号识别数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiOpenFrmWeightBridger_Click(object sender, EventArgs e)
        {
            TaskForm taskForm = new FrmWeightBridger();
            if (HaveOpened(taskForm, this))
            {
                taskForm.MdiParent = this;
                taskForm.Show();
            }
        }

        /// <summary>
        /// 02.皮带采样机接口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiOpenFrmBeltSampler_Click(object sender, EventArgs e)
        {
            TaskForm taskForm = new FrmBeltSampler();
            if (HaveOpened(taskForm, this))
            {
                taskForm.MdiParent = this;
                taskForm.Show();
            }
        }

        /// <summary>
        /// 03.全自动制样机接口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiOpenFrmAutoMaker_NCGM_Click(object sender, EventArgs e)
        {
            TaskForm taskForm = new FrmAutoMaker();
            if (HaveOpened(taskForm, this))
            {
                taskForm.MdiParent = this;
                taskForm.Show();
            }
        }

        /// <summary>
        /// 04.智能存样柜和气动传输接口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiOpenFrmAutoCupboard_NCGM_Click(object sender, EventArgs e)
        {
            TaskForm taskForm = new FrmAutoCupboard();
            if (HaveOpened(taskForm, this))
            {
                taskForm.MdiParent = this;
                taskForm.Show();
            }
        }

        /// <summary>
        /// 05.化验设备数据读取
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiOpenFrmAssayDevice_Click(object sender, EventArgs e)
        {
            TaskForm taskForm = new FrmAssayDevice();
            if (HaveOpened(taskForm, this))
            {
                taskForm.MdiParent = this;
                taskForm.Show();
            }
        }

        /// <summary>
        /// 07.汽车采样机接口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiOpenFrmCarSampler_CSKY_Click(object sender, EventArgs e)
        {
            TaskForm taskForm = new FrmCarSampler();
            if (HaveOpened(taskForm, this))
            {
                taskForm.MdiParent = this;
                taskForm.Show();
            }
        }

        /// <summary>
        /// 08.综合事件处理
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiOpenFrmCarSynchronous_Click(object sender, EventArgs e)
        {
            TaskForm taskForm = new FrmDataHandler();
            if (HaveOpened(taskForm, this))
            {
                taskForm.MdiParent = this;
                taskForm.Show();
            }
        }

        /// <summary>
        /// 09.封装归批机
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiOpenPackingBatch_Click(object sender, EventArgs e)
        {
            TaskForm taskForm = new FrmPackingBatch();
            if (HaveOpened(taskForm, this))
            {
                taskForm.MdiParent = this;
                taskForm.Show();
            }
        }

        /// <summary>
        /// 10.气动传输
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiOpenPneumaticTransfer_Click(object sender, EventArgs e)
        {
            TaskForm taskForm = new FrmPneumaticTransfer();
            if (HaveOpened(taskForm, this))
            {
                taskForm.MdiParent = this;
                taskForm.Show();
            }
        }

        /// <summary>
        /// 11.下位机
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiOpenPLC_Click(object sender, EventArgs e)
        {
            TaskForm taskForm = new FrmPLC();
            if (HaveOpened(taskForm, this))
            {
                taskForm.MdiParent = this;
                taskForm.Show();
            }
        }

        /// <summary>
        /// 13.煤厂测温仪
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiOpenTemperator_Click(object sender, EventArgs e)
        {
            TaskForm taskForm = new FrmTemperator();
            if (HaveOpened(taskForm, this))
            {
                taskForm.MdiParent = this;
                taskForm.Show();
            }
        }

        /// <summary>
        /// 温湿度仪接口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiOpenTemperatureHumidity_Click(object sender, EventArgs e)
        {
            TaskForm taskForm = new FrmTemperatureHumidity();
            if (HaveOpened(taskForm, this))
            {
                taskForm.MdiParent = this;
                taskForm.Show();
            }
        }

        /// <summary>
        /// 高精度皮带秤接口
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmiOpenHighBeltWeight_Click(object sender, EventArgs e)
        {
            TaskForm taskForm = new FrmBeltWeight();
            if (HaveOpened(taskForm, this))
            {
                taskForm.MdiParent = this;
                taskForm.Show();
            }
        }

        /// <summary>
        /// 实时信号同步
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsiOpenSignalData_Click(object sender, EventArgs e)
        {
            TaskForm taskForm = new FrmSignalDataSync();
            if (HaveOpened(taskForm, this))
            {
                taskForm.MdiParent = this;
                taskForm.Show();
            }
        }
    }
}
