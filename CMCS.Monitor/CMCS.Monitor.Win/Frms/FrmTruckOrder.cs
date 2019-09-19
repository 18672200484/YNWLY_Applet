using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using Xilium.CefGlue.WindowsForms;
using CMCS.Monitor.Win.Core;
using CMCS.Common;
using CMCS.Monitor.Win.Html;
using CMCS.Common.DAO;
using CMCS.Common.Enums;
using CMCS.Common.Entities.CarTransport;

namespace CMCS.Monitor.Win.Frms
{
    public partial class FrmTruckOrder : DevComponents.DotNetBar.Metro.MetroForm
    {
        /// <summary>
        /// 窗体唯一标识符
        /// </summary>
        public static string UniqueKey = "FrmTruckOrder";

        CommonDAO commonDAO = CommonDAO.GetInstance();

        CefWebBrowser cefWebBrowser = new CefWebBrowser();

        public FrmTruckOrder()
        {
            InitializeComponent();
        }

        private void FrmTruckOrder_Load(object sender, EventArgs e)
        {
            FormInit();
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

            cefWebBrowser.StartUrl = SelfVars.Url_TruckOrder;
            cefWebBrowser.Dock = DockStyle.Fill;
            cefWebBrowser.LoadEnd += new EventHandler<LoadEndEventArgs>(cefWebBrowser_LoadEnd);
            panWebBrower.Controls.Add(cefWebBrowser);
        }

        void cefWebBrowser_LoadEnd(object sender, LoadEndEventArgs e)
        {
            timer1.Enabled = true;

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

        /// <summary>
        /// 请求数据
        /// </summary>
        void RequestData()
        {
            string value = string.Empty, machineCode = string.Empty;
            List<HtmlDataItem> datas = new List<HtmlDataItem>();

            datas.Clear();

            datas.Add(new HtmlDataItem("过衡程序_信号灯", ConvertBooleanToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QC_Order_1, eSignalDataName.系统.ToString())), eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("IO控制器_信号灯", ConvertBooleanToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QC_Order_1, eSignalDataName.IO控制器_连接状态.ToString())), eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("LED屏1_信号灯", ConvertBooleanToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QC_Order_1, eSignalDataName.LED屏1_连接状态.ToString())), eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("LED屏2_信号灯", ConvertBooleanToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QC_Order_1, eSignalDataName.LED屏2_连接状态.ToString())), eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("读卡器1_信号灯", ConvertBooleanToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QC_Order_1, eSignalDataName.车号识别1_连接状态.ToString())), eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("读卡器2_信号灯", ConvertBooleanToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QC_Order_1, eSignalDataName.车号识别2_连接状态.ToString())), eHtmlDataItemType.svg_color));

            // 根据信号“当前车Id"查询更多信息
            string currentAutotruckId = commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QC_Order_1, eSignalDataName.当前车Id.ToString());
            string currentTransportId = commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QC_Order_1, eSignalDataName.当前运输记录Id.ToString());
            decimal gross = 0, tare = 0;
            if (!string.IsNullOrEmpty(currentAutotruckId) && !string.IsNullOrEmpty(currentTransportId))
            {
                CmcsAutotruck autotruck = commonDAO.SelfDber.Get<CmcsAutotruck>(currentAutotruckId);
                if (autotruck != null)
                {
                    if (autotruck.CarType == eCarType.入场煤.ToString())
                    {
                        CmcsBuyFuelTransport transport = commonDAO.SelfDber.Get<CmcsBuyFuelTransport>(currentTransportId);
                        if (transport != null)
                        {
                            gross = transport.GrossWeight;
                            tare = transport.TareWeight;
                        }
                    }
                    if (autotruck.CarType == eCarType.入场煤.ToString())
                    {
                        CmcsSaleFuelTransport transport = commonDAO.SelfDber.Get<CmcsSaleFuelTransport>(currentTransportId);
                        if (transport != null)
                        {
                            gross = transport.GrossWeight;
                            tare = transport.TareWeight;
                        }
                    }
                    if (autotruck.CarType == eCarType.其他物资.ToString())
                    {
                        CmcsGoodsTransport transport = commonDAO.SelfDber.Get<CmcsGoodsTransport>(currentTransportId);
                        if (transport != null)
                        {
                            gross = transport.FirstWeight;
                            tare = transport.SecondWeight;
                        }
                    }
                }
            }
            datas.Add(new HtmlDataItem("卡车", (!string.IsNullOrEmpty(currentAutotruckId)).ToString(), eHtmlDataItemType.svg_visible));
            datas.Add(new HtmlDataItem("当前过衡车号", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QC_Order_1, eSignalDataName.当前车号.ToString()), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("皮重", tare.ToString() + " 吨", eHtmlDataItemType.svg_text));

            datas.Add(new HtmlDataItem("地感1信号", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QC_Order_1, eSignalDataName.地感1信号.ToString()).ToLower() == "1" ? ColorTranslator.ToHtml(EquipmentStatusColors.Working) : "#c0c0c0", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("地感2信号", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QC_Order_1, eSignalDataName.地感2信号.ToString()).ToLower() == "1" ? ColorTranslator.ToHtml(EquipmentStatusColors.Working) : "#c0c0c0", eHtmlDataItemType.svg_color));

            datas.Add(new HtmlDataItem("道闸1升杆", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QC_Order_1, eSignalDataName.道闸1升杆.ToString()), eHtmlDataItemType.svg_visible));
            datas.Add(new HtmlDataItem("道闸2升杆", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QC_Order_1, eSignalDataName.道闸2升杆.ToString()), eHtmlDataItemType.svg_visible));

            datas.Add(new HtmlDataItem("卡车", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QC_Order_1, eSignalDataName.上磅方向.ToString()), eHtmlDataItemType.svg_scare));
            // 添加更多...

            // 发送到页面
            cefWebBrowser.Browser.GetMainFrame().ExecuteJavaScript("requestData(" + Newtonsoft.Json.JsonConvert.SerializeObject(datas) + ");", "", 0);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // 界面不可见时，停止发送数据
            if (!this.Visible) return;

            RequestData();
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
            else if ("|正在运行|".Contains("|" + systemStatus + "|"))
                return ColorTranslator.ToHtml(EquipmentStatusColors.Working);
            else if ("|发生故障|".Contains("|" + systemStatus + "|"))
                return ColorTranslator.ToHtml(EquipmentStatusColors.Breakdown);
            else
                return ColorTranslator.ToHtml(EquipmentStatusColors.Forbidden);
        }

        /// <summary>
        /// 转换布尔类型状态为颜色值
        /// </summary>
        /// <param name="status">状态</param>
        /// <returns></returns>
        private string ConvertBooleanToColor(string status)
        {
            return status.ToLower() == "1" ? ColorTranslator.ToHtml(EquipmentStatusColors.BeReady) : ColorTranslator.ToHtml(EquipmentStatusColors.Breakdown);
        }
    }
}