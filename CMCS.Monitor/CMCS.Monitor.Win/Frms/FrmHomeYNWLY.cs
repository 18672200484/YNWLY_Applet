using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using Xilium.CefGlue.WindowsForms;
using CMCS.Common;
using CMCS.Monitor.Win.Core;
using CMCS.Common.DAO;
using CMCS.Monitor.Win.Html;
using CMCS.Common.Enums;
using Xilium.CefGlue;
using CMCS.Monitor.Win.UserControls;

namespace CMCS.Monitor.Win.Frms
{
    public partial class FrmHomeYNWLY : DevComponents.DotNetBar.Metro.MetroForm
    {
        /// <summary>
        /// 窗体唯一标识符
        /// </summary>
        public static string UniqueKey = "FrmHomeYNWLY";

        CommonDAO commonDAO = CommonDAO.GetInstance();

        CefWebBrowserEx cefWebBrowser = new CefWebBrowserEx();

        public FrmHomeYNWLY()
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
            cefWebBrowser.StartUrl = SelfVars.Url_HomeYNWLY;
            cefWebBrowser.Dock = DockStyle.Fill;
            cefWebBrowser.WebClient = new HomeYNWLYCefWebClient(cefWebBrowser);
            cefWebBrowser.LoadEnd += new EventHandler<LoadEndEventArgs>(cefWebBrowser_LoadEnd);
            panWebBrower.Controls.Add(cefWebBrowser);
        }

        void cefWebBrowser_LoadEnd(object sender, LoadEndEventArgs e)
        {
            timer1.Enabled = true;

            RequestData();
        }

        private void FrmHomeYNWLY_Load(object sender, EventArgs e)
        {
            FormInit();
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
            datas.Add(new HtmlDataItem("#1翻车机", ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_TrunOver_1, eSignalDataName.系统.ToString())), eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("#2翻车机", ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_TrunOver_2, eSignalDataName.系统.ToString())), eHtmlDataItemType.svg_color));

            datas.Add(new HtmlDataItem("#1皮带采样机", ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_InPDCYJ_1, eSignalDataName.系统.ToString())), eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("#2皮带采样机", ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_InPDCYJ_2, eSignalDataName.系统.ToString())), eHtmlDataItemType.svg_color));

            datas.Add(new HtmlDataItem("入厂煤重车#1", ConvertBooleanToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QC_Weighter_1, eSignalDataName.系统.ToString())), eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("入厂煤重车#2", ConvertBooleanToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QC_Weighter_2, eSignalDataName.系统.ToString())), eHtmlDataItemType.svg_color));

            datas.Add(new HtmlDataItem("入厂煤轻车#1", ConvertBooleanToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QC_Weighter_3, eSignalDataName.系统.ToString())), eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("入厂煤轻车#2", ConvertBooleanToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QC_Weighter_4, eSignalDataName.系统.ToString())), eHtmlDataItemType.svg_color));

            datas.Add(new HtmlDataItem("#1汽车机械采样机", ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QC_JxSampler_1, eSignalDataName.系统.ToString())), eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("#2汽车机械采样机", ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QC_JxSampler_2, eSignalDataName.系统.ToString())), eHtmlDataItemType.svg_color));

            datas.Add(new HtmlDataItem("#1全自动制样机", ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QZDZYJ_1, eSignalDataName.系统.ToString())), eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("存样柜", ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_CYG1, eSignalDataName.系统.ToString())), eHtmlDataItemType.svg_color));

            datas.Add(new HtmlDataItem("#1斗轮机", ConvertMachineStatusToColor(commonDAO.GetSignalDataValue("#1斗轮机", eSignalDataName.系统.ToString())), eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("#2斗轮机", ConvertMachineStatusToColor(commonDAO.GetSignalDataValue("#2斗轮机", eSignalDataName.系统.ToString())), eHtmlDataItemType.svg_color));

            datas.Add(new HtmlDataItem("#1皮带秤", commonDAO.GetSignalDataValueDouble("#1皮带秤", eSignalDataName.瞬时流量.ToString()) > 0 ? ColorTranslator.ToHtml(EquipmentStatusColors.Working) : ColorTranslator.ToHtml(EquipmentStatusColors.BeReady), eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("#2皮带秤", commonDAO.GetSignalDataValueDouble("#2皮带秤", eSignalDataName.瞬时流量.ToString()) > 0 ? ColorTranslator.ToHtml(EquipmentStatusColors.Working) : ColorTranslator.ToHtml(EquipmentStatusColors.BeReady), eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("#1皮带秤值", commonDAO.GetSignalDataValue("#1皮带秤", eSignalDataName.瞬时流量.ToString()) + "t", eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#2皮带秤值", commonDAO.GetSignalDataValue("#2皮带秤", eSignalDataName.瞬时流量.ToString()) + "t", eHtmlDataItemType.svg_text));




            datas.Add(new HtmlDataItem("入厂煤重车#2_车号", commonDAO.GetSignalDataValue("#2重车衡", eSignalDataName.当前车号.ToString()), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("入厂煤重车#1_车号", commonDAO.GetSignalDataValue("#1重车衡", eSignalDataName.当前车号.ToString()), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("入厂煤轻车#2_车号", commonDAO.GetSignalDataValue("#2轻车衡", eSignalDataName.当前车号.ToString()), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("入厂煤轻车#1_车号", commonDAO.GetSignalDataValue("#1轻车衡", eSignalDataName.当前车号.ToString()), eHtmlDataItemType.svg_text));

            datas.Add(new HtmlDataItem("入厂煤重车#2_重量", commonDAO.GetSignalDataValue("#2重车衡", eSignalDataName.地磅仪表_实时重量.ToString()) + "吨", eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("入厂煤重车#1_重量", commonDAO.GetSignalDataValue("#1重车衡", eSignalDataName.地磅仪表_实时重量.ToString()) + "吨", eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("入厂煤轻车#2_重量", commonDAO.GetSignalDataValue("#2轻车衡", eSignalDataName.地磅仪表_实时重量.ToString()) + "吨", eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("入厂煤轻车#1_重量", commonDAO.GetSignalDataValue("#1轻车衡", eSignalDataName.地磅仪表_实时重量.ToString()) + "吨", eHtmlDataItemType.svg_text));



            //datas.Add(new HtmlDataItem("#1动态衡今日进出厂数量", string.Format("进厂:{0}节  出厂:{1}节", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_GDH_1, "今日进厂数量"), commonDAO.GetSignalDataValue(GlobalVars.MachineCode_GDH_1, "今日出厂数量")), eHtmlDataItemType.svg_text));
            //string XMJS_QD_Color = ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QD, eSignalDataName.系统.ToString()));
            //datas.Add(new HtmlDataItem("#1气动传输_气动站1", XMJS_QD_Color, eHtmlDataItemType.svg_color));
            //datas.Add(new HtmlDataItem("#1气动传输_气动站2", XMJS_QD_Color, eHtmlDataItemType.svg_color));
            //datas.Add(new HtmlDataItem("#1气动传输_气动站3", XMJS_QD_Color, eHtmlDataItemType.svg_color));
            //datas.Add(new HtmlDataItem("#1火车机械采样机", ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HCJXCYJ_1, eSignalDataName.系统.ToString())), eHtmlDataItemType.svg_color));
            //datas.Add(new HtmlDataItem("#2火车机械采样机", ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HCJXCYJ_2, eSignalDataName.系统.ToString())), eHtmlDataItemType.svg_color));

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
            else if ("|正在运行|正在卸样|".Contains("|" + systemStatus + "|"))
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

    public class HomeYNWLYCefWebClient : CefWebClient
    {
        CefWebBrowser cefWebBrowser;

        public HomeYNWLYCefWebClient(CefWebBrowser cefWebBrowser)
            : base(cefWebBrowser)
        {
            this.cefWebBrowser = cefWebBrowser;
        }

        protected override bool OnProcessMessageReceived(CefBrowser browser, CefProcessId sourceProcess, CefProcessMessage message)
        {
            if (message.Name == "OpenTrainBeltSampler")
                SelfVars.MainFrameForm.OpenTrainBeltSampler();
            //else if (message.Name == "OpenTrainMachinerySampler")
                //SelfVars.MainFrameForm.OpenTrainMachinerySampler();
            else if (message.Name == "OpenAutoMaker")
                SelfVars.MainFrameForm.OpenAutoMaker();
            else if (message.Name == "OpenTrainTipper")
                SelfVars.MainFrameForm.OpenTrainTipper();
            else if (message.Name == "OpenWeightBridgeLoadToday")
                SelfVars.MainFrameForm.OpenWeightBridgeLoadToday();
            else if (message.Name == "OpenTruckWeighter")
                SelfVars.MainFrameForm.OpenTruckWeighter();
            else if (message.Name == "OpenTruckOrder")
                SelfVars.MainFrameForm.OpenTruckOrder();
            else if (message.Name == "OpenTruckMachinerySampler")
                SelfVars.MainFrameForm.OpenTruckMachinerySampler();
            else if (message.Name == "OpenAutoCupboardPneumaticTransfer")
                SelfVars.MainFrameForm.OpenAutoCupboardPneumaticTransfer();
            else if (message.Name == "OpenAssayManage")
                SelfVars.MainFrameForm.OpenAssayManage();

            return base.OnProcessMessageReceived(browser, sourceProcess, message);
        }
    }
}