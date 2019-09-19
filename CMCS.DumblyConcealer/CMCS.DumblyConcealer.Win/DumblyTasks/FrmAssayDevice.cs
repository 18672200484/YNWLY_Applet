﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CMCS.DumblyConcealer.Win.Core;
using CMCS.Common.Utilities;
using CMCS.DumblyConcealer.Tasks.AssayDevice;
using CMCS.DumblyConcealer.Enums;

namespace CMCS.DumblyConcealer.Win.DumblyTasks
{
    public partial class FrmAssayDevice : TaskForm
    {

        RTxtOutputer rTxtOutputer;
        TaskSimpleScheduler taskSimpleScheduler = new TaskSimpleScheduler();

        public FrmAssayDevice()
        {
            InitializeComponent();
        }

        private void FrmAssayDevice_Load(object sender, EventArgs e)
        {
            this.Text = "化验设备数据同步业务";

            this.rTxtOutputer = new RTxtOutputer(rtxtOutput);

            ExecuteAllTask();
        }

        /// <summary>
        /// 执行所有任务
        /// </summary>
        void ExecuteAllTask()
        {
            EquAssayDeviceDAO assayDevice_DAO = EquAssayDeviceDAO.GetInstance();

            taskSimpleScheduler.StartNewTask("生成标准测硫仪数据", () =>
            {
                assayDevice_DAO.CreateToSulfurStdAssay(this.rTxtOutputer.Output);

            }, 30000, OutputError);

            taskSimpleScheduler.StartNewTask("生成标准量热仪数据", () =>
            {
                assayDevice_DAO.CreateToHeatStdAssay(this.rTxtOutputer.Output);

            }, 30000, OutputError);

            taskSimpleScheduler.StartNewTask("生成标准水分仪数据", () =>
            {
                assayDevice_DAO.CreateToMoistureStdAssay(this.rTxtOutputer.Output);

            }, 30000, OutputError);

            taskSimpleScheduler.StartNewTask("生成标准工分仪数据", () =>
            {
                assayDevice_DAO.CreateToProximateStdAssay(this.rTxtOutputer.Output);

            }, 30000, OutputError);

            taskSimpleScheduler.StartNewTask("生成标准碳氢仪数据", () =>
            {
                assayDevice_DAO.CreateToHadStdAssay(this.rTxtOutputer.Output);

            }, 30000, OutputError);

            taskSimpleScheduler.StartNewTask("生成标准灰融仪数据", () =>
            {
                assayDevice_DAO.CreateToAshStdAssay(this.rTxtOutputer.Output);

            }, 30000, OutputError);

            //taskSimpleScheduler.StartNewTask("读取开元数据", () =>
            //{
            //    assayDevice_DAO.SaveToHeatAssay_CSKY(this.rTxtOutputer.Output);
            //    assayDevice_DAO.SaveToMoistureAssay_CSKY(this.rTxtOutputer.Output);
            //    assayDevice_DAO.SaveToSulfurStdAssay_CSKY(this.rTxtOutputer.Output);

            //}, 30000, OutputError);//开元数据同步暂时停掉

            //taskSimpleScheduler.StartNewTask("读取化验设备运行状态", () =>
            //{
            //    assayDevice_DAO.ReadHyMachine(this.rTxtOutputer.Output);

            //}, 10000, OutputError);//改为直接写数据库
        }

        /// <summary>
        /// 输出异常信息
        /// </summary>
        /// <param name="text"></param>
        /// <param name="ex"></param>
        void OutputError(string text, Exception ex)
        {
            this.rTxtOutputer.Output(text + Environment.NewLine + ex.Message, eOutputType.Error);
        }

        /// <summary>
        /// 窗体关闭后
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void FrmAssayDevice_FormClosed(object sender, FormClosedEventArgs e)
        {
            // 注意：必须取消任务
            this.taskSimpleScheduler.Cancal();
        }
    }
}
