using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
//
using Xilium.CefGlue.WindowsForms;
using Xilium.CefGlue;
using CMCS.Monitor.DAO;
using CMCS.Common.Entities;
using DevComponents.DotNetBar.SuperGrid;
using CMCS.Common;
using CMCS.Common.DAO;
using CMCS.Monitor.Win.CefGlue;
using CMCS.Monitor.Win.Html;
using DevComponents.DotNetBar.Metro;
using CMCS.Monitor.Win.Core;
using CMCS.Common.Entities.Inf;
using CMCS.Common.Enums;

namespace CMCS.Monitor.Win.Frms
{
    public partial class FrmTrainUpender : MetroForm
    {
        /// <summary>
        /// 窗体唯一标识符
        /// </summary>
        public static string UniqueKey = "FrmTrainUpender";

        CefWebBrowser cefWebBrowser = new CefWebBrowser();
        public static string Device = string.Empty;

        public FrmTrainUpender()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 窗体初始化
        /// </summary>
        private void FormInit()
        {
#if DEBUG
            gboxTest.Visible = true;
#else
            gboxTest.Visible = false;
#endif
            cefWebBrowser.StartUrl = SelfVars.Url_TrainUpender;
            cefWebBrowser.Dock = DockStyle.Fill;
            cefWebBrowser.LoadEnd += new EventHandler<LoadEndEventArgs>(cefWebBrowser_LoadEnd);
            panWebBrower.Controls.Add(cefWebBrowser);
        }

        void cefWebBrowser_LoadEnd(object sender, LoadEndEventArgs e)
        {
            timer1.Enabled = true;

            //RequestData();
        }

        private void FrmTrainUpender_Load(object sender, EventArgs e)
        {

            FormInit();
        }


        /// <summary>
        /// 转换设备系统状态为颜色值
        /// </summary>
        /// <param name="systemStatus">系统状态</param>
        /// <returns></returns>
        private string ConvertMachineStatusToColor(string systemStatus)
        {
            if ("|就绪待机|".Contains("|" + systemStatus + "|"))
                return ColorTranslator.ToHtml(EquipmentStatusColors.BeReady);
            else if ("|正在运行|正在卸样|".Contains("|" + systemStatus + "|"))
                return ColorTranslator.ToHtml(EquipmentStatusColors.Working);
            else if ("|发生故障|".Contains("|" + systemStatus + "|"))
                return ColorTranslator.ToHtml(EquipmentStatusColors.Breakdown);
            else
                return ColorTranslator.ToHtml(EquipmentStatusColors.Forbidden);
        }
        /// <summary>
        /// 请求数据
        /// </summary>
        void RequestData()
        {
            CommonDAO commonDAO = CommonDAO.GetInstance();
            MonitorDAO monitorDAO = MonitorDAO.GetInstance();
            string value = string.Empty, machineCode1 = string.Empty, machineCode2 = string.Empty;
            List<HtmlDataItem> datas = new List<HtmlDataItem>();
            List<InfEquInfHitch> equInfHitchs = new List<InfEquInfHitch>();

            #region 翻车机

            datas.Clear();
            machineCode1 = GlobalVars.MachineCode_TrunOver_1;
            machineCode2 = GlobalVars.MachineCode_TrunOver_2;

            datas.Add(new HtmlDataItem("翻车机1_正翻", ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(machineCode1, "正翻")), eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("翻车机1_反翻", ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(machineCode1, "反翻")), eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("翻车机1_重调原位", ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(machineCode1, "重调原位")), eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("翻车机1_正翻效果", ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(machineCode1, "正翻")), eHtmlDataItemType.svg_Temp));
            datas.Add(new HtmlDataItem("翻车机1_反翻效果", ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(machineCode1, "反翻")), eHtmlDataItemType.svg_Temp));
            datas.Add(new HtmlDataItem("翻车机1_转盘信号", ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(machineCode1, "转盘信号")), eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("翻车机1_当前翻车车号", commonDAO.GetSignalDataValue(machineCode1, eSignalDataName.当前车号.ToString()), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("翻车机1_待翻车数", "待翻车数:" + commonDAO.GetSignalDataValue(machineCode1, "待翻车数"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("翻车机1_已翻车数", "已翻车数:" + commonDAO.GetSignalDataValue(machineCode1, "已翻车数"), eHtmlDataItemType.svg_text));
            string trainWeightRecord1 = commonDAO.GetSignalDataValue(machineCode1, "当前车Id");
            // 此处 当前车Id 的值为火车入厂记录Id
            if (!string.IsNullOrEmpty(trainWeightRecord1))
            {
                DataTable dtl = monitorDAO.GetInFactoryBatchInfoByTrainWeightRecordId(trainWeightRecord1);
                if (dtl.Rows.Count > 0)
                {
                    datas.Add(new HtmlDataItem("翻车机1_供应商", dtl.Rows[0]["SupplierName"].ToString(), eHtmlDataItemType.svg_text));
                    datas.Add(new HtmlDataItem("翻车机1_发站", dtl.Rows[0]["StationName"].ToString(), eHtmlDataItemType.svg_text));
                    datas.Add(new HtmlDataItem("翻车机1_矿点", dtl.Rows[0]["MineName"].ToString(), eHtmlDataItemType.svg_text));
                    datas.Add(new HtmlDataItem("翻车机1_煤种", dtl.Rows[0]["FuelName"].ToString(), eHtmlDataItemType.svg_text));
                }
                else
                {
                    datas.Add(new HtmlDataItem("翻车机1_供应商", "", eHtmlDataItemType.svg_text));
                    datas.Add(new HtmlDataItem("翻车机1_发站", "", eHtmlDataItemType.svg_text));
                    datas.Add(new HtmlDataItem("翻车机1_矿点", "", eHtmlDataItemType.svg_text));
                    datas.Add(new HtmlDataItem("翻车机1_煤种", "", eHtmlDataItemType.svg_text));
                }
            }


            datas.Add(new HtmlDataItem("翻车机2_正翻", ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(machineCode2, "正翻")), eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("翻车机2_反翻", ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(machineCode2, "反翻")), eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("翻车机2_重调原位", ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(machineCode2, "重调原位")), eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("翻车机2_正翻效果", ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(machineCode1, "正翻")), eHtmlDataItemType.svg_Temp));
            datas.Add(new HtmlDataItem("翻车机2_反翻效果", ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(machineCode1, "反翻")), eHtmlDataItemType.svg_Temp));
            datas.Add(new HtmlDataItem("翻车机2_转盘信号", ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(machineCode2, "转盘信号")), eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("翻车机2_当前翻车车号", commonDAO.GetSignalDataValue(machineCode2, eSignalDataName.当前车号.ToString()), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("翻车机2_待翻车数", "待翻车数:" + commonDAO.GetSignalDataValue(machineCode2, "待翻车数"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("翻车机2_已翻车数", "已翻车数:" + commonDAO.GetSignalDataValue(machineCode2, "已翻车数"), eHtmlDataItemType.svg_text));
            string trainWeightRecord2 = commonDAO.GetSignalDataValue(machineCode2, "当前车Id");
            // 此处 当前车Id 的值为火车入厂记录Id
            if (!string.IsNullOrEmpty(trainWeightRecord2))
            {
                DataTable dtl = monitorDAO.GetInFactoryBatchInfoByTrainWeightRecordId(trainWeightRecord2);
                if (dtl.Rows.Count > 0)
                {
                    datas.Add(new HtmlDataItem("翻车机2_供应商", dtl.Rows[0]["SupplierName"].ToString(), eHtmlDataItemType.svg_text));
                    datas.Add(new HtmlDataItem("翻车机2_发站", dtl.Rows[0]["StationName"].ToString(), eHtmlDataItemType.svg_text));
                    datas.Add(new HtmlDataItem("翻车机2_矿点", dtl.Rows[0]["MineName"].ToString(), eHtmlDataItemType.svg_text));
                    datas.Add(new HtmlDataItem("翻车机2_煤种", dtl.Rows[0]["FuelName"].ToString(), eHtmlDataItemType.svg_text));
                }
                else
                {
                    datas.Add(new HtmlDataItem("翻车机2_供应商", "", eHtmlDataItemType.svg_text));
                    datas.Add(new HtmlDataItem("翻车机2_发站", "", eHtmlDataItemType.svg_text));
                    datas.Add(new HtmlDataItem("翻车机2_矿点", "", eHtmlDataItemType.svg_text));
                    datas.Add(new HtmlDataItem("翻车机2_煤种", "", eHtmlDataItemType.svg_text));
                }
            }
            // 发送到页面
            cefWebBrowser.Browser.GetMainFrame().ExecuteJavaScript("requestData(" + Newtonsoft.Json.JsonConvert.SerializeObject(datas) + ");", "", 0);

            #endregion
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // 界面不可见时，停止发送数据
            if (!this.Visible) return;

            RequestData();
        }

        /// <summary>
        /// 测试 - 刷新页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            cefWebBrowser.Browser.Reload();
        }

        /// <summary>
        /// 测试 - 数据刷新
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRequestData_Click(object sender, EventArgs e)
        {
            RequestData();
        }


    }
}
