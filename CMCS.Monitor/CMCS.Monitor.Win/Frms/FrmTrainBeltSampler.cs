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
    public partial class FrmTrainBeltSampler : MetroForm
    {
        /// <summary>
        /// 窗体唯一标识符
        /// </summary>
        public static string UniqueKey = "FrmTrainBeltSampler";

        CefWebBrowser cefWebBrowser = new CefWebBrowser();

        public FrmTrainBeltSampler()
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

            cefWebBrowser.StartUrl = SelfVars.Url_BeltSampler;
            cefWebBrowser.Dock = DockStyle.Fill;
            cefWebBrowser.LoadEnd += new EventHandler<LoadEndEventArgs>(cefWebBrowser_LoadEnd);
            panWebBrower.Controls.Add(cefWebBrowser);
        }

        void cefWebBrowser_LoadEnd(object sender, LoadEndEventArgs e)
        {
            timer1.Enabled = true;
            timer2.Enabled = true;
            //页面初始化完成
            ReadConfig();
        }

        private void FrmTrainBeltSampler_Load(object sender, EventArgs e)
        {
            FormInit();

        }

        private void superGridControl_CancelEdit_BeginEdit(object sender, GridEditEventArgs e)
        {
            // 取消编辑
            e.Cancel = true;
        }

        /// <summary>
        /// 打开卸样界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenUnload_Click(object sender, EventArgs e)
        {

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

            ReadConfig();
        }

        /// <summary>
        /// 请求数据
        /// </summary>
        void RequestData()
        {
            CommonDAO commonDAO = CommonDAO.GetInstance();

            string value = string.Empty, machineCode = string.Empty;
            List<HtmlDataItem> datas = new List<HtmlDataItem>();
            List<InfEquInfHitch> equInfHitchs = new List<InfEquInfHitch>();

            #region 皮带采样机 #1

            datas.Clear();
            machineCode = GlobalVars.MachineCode_PDCYJ_1;

            datas.Add(new HtmlDataItem("皮带采样机1_采样编码", commonDAO.GetSignalDataValue(machineCode, "采样编码"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("皮带采样机1_矿发量", commonDAO.GetSignalDataValue(machineCode, "矿发量"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("皮带采样机1_开始时间", commonDAO.GetSignalDataValue(machineCode, "开始时间"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("皮带采样机1_来煤车数", commonDAO.GetSignalDataValue(machineCode, "来煤车数"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("皮带采样机1_采样次数", commonDAO.GetSignalDataValue(machineCode, "采样次数"), eHtmlDataItemType.svg_text));

            // 集样罐   
            List<InfEquInfSampleBarrel> barrels1 = MonitorDAO.GetInstance().GetEquInfSampleBarrels(machineCode, eEquInfGatherType.底卸式.ToString());
            datas.Add(new HtmlDataItem("采样机1_底卸集样罐", Newtonsoft.Json.JsonConvert.SerializeObject(barrels1.Select(a => new { BarrelNumber = a.BarrelNumber, IsCurrent = a.IsCurrent, BarrelStatus = a.BarrelStatus, SampleCount = a.SampleCount })), eHtmlDataItemType.json_data));
            barrels1 = MonitorDAO.GetInstance().GetEquInfSampleBarrels(machineCode, eEquInfGatherType.密码罐.ToString());
            datas.Add(new HtmlDataItem("采样机1_密码集样罐", Newtonsoft.Json.JsonConvert.SerializeObject(barrels1.Select(a => new { BarrelNumber = a.BarrelNumber, IsCurrent = a.IsCurrent, BarrelStatus = a.BarrelStatus, SampleCount = a.SampleCount })), eHtmlDataItemType.json_data));

            value = commonDAO.GetSignalDataValue(machineCode, eSignalDataName.系统.ToString());
            if ("|就绪待机|".Contains("|" + value + "|"))
                datas.Add(new HtmlDataItem("皮带采样机1系统", "#00c000", eHtmlDataItemType.svg_color));
            else if ("|正在运行|正在卸样|".Contains("|" + value + "|"))
                datas.Add(new HtmlDataItem("皮带采样机1系统", "#ff0000", eHtmlDataItemType.svg_color));
            else if ("|发生故障|".Contains("|" + value + "|"))
                datas.Add(new HtmlDataItem("皮带采样机1系统", "#ffff00", eHtmlDataItemType.svg_color));
            else
                datas.Add(new HtmlDataItem("皮带采样机1系统", "#c0c0c0", eHtmlDataItemType.svg_color));

            value = commonDAO.GetSignalDataValue(machineCode, "弃料提升斗");
            datas.Add(new HtmlDataItem("皮带采样机1弃料提升斗", value == "1" ? "Red" : "url(#linearGradient6035-9)", eHtmlDataItemType.svg_color));
            value = commonDAO.GetSignalDataValue(machineCode, "输煤皮带");
            datas.Add(new HtmlDataItem("皮带采样机1输煤皮带", value == "1" ? "Red" : "#808080", eHtmlDataItemType.svg_color));
            value = commonDAO.GetSignalDataValue(machineCode, "初级给料");
            datas.Add(new HtmlDataItem("皮带采样机1初级给料", value == "1" ? "Red" : "#808080", eHtmlDataItemType.svg_color));
            value = commonDAO.GetSignalDataValue(machineCode, "初级破碎");
            datas.Add(new HtmlDataItem("皮带采样机1初级破碎1", value == "1" ? "Red" : "url(#linearGradient5969-9)", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("皮带采样机1初级破碎2", value == "1" ? "Red" : "url(#linearGradient5919-2)", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("皮带采样机1初级破碎3", value == "1" ? "Red" : "url(#linearGradient5921-4)", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("皮带采样机1初级破碎4", value == "1" ? "Red" : "url(#linearGradient5923-5)", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("皮带采样机1初级破碎5", value == "1" ? "Red" : "url(#linearGradient5925-5)", eHtmlDataItemType.svg_color));
            value = commonDAO.GetSignalDataValue(machineCode, "次级给料");
            datas.Add(new HtmlDataItem("皮带采样机1次级给料", value == "1" ? "Red" : "#808080", eHtmlDataItemType.svg_color));
            value = commonDAO.GetSignalDataValue(machineCode, "缩分器");
            datas.Add(new HtmlDataItem("皮带采样机1缩分器1", value == "1" ? "Red" : "url(#linearGradient7572)", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("皮带采样机1缩分器2", value == "1" ? "Red" : "url(#linearGradient5931-6)", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("皮带采样机1缩分器3", value == "1" ? "Red" : "url(#linearGradient6946)", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("皮带采样机1缩分器4", value == "1" ? "Red" : "url(#linearGradient5939-1)", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("皮带采样机1缩分器5", value == "1" ? "Red" : "url(#linearGradient5941-4)", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("皮带采样机1缩分器6", value == "1" ? "Red" : "url(#linearGradient5905-1)", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("皮带采样机1缩分器7", value == "1" ? "Red" : "url(#linearGradient7623)", eHtmlDataItemType.svg_color));

            //value = commonDAO.GetSignalDataValue(machineCode, "1A输送机");
            //datas.Add(new HtmlDataItem("采样机1_1A输送机", value == "1" ? "Red" : "Green", eHtmlDataItemType.svg_color));
            //value = commonDAO.GetSignalDataValue(machineCode, "缩分器皮带");
            //datas.Add(new HtmlDataItem("采样机1_缩分器皮带", value == "1" ? "Red" : "Green", eHtmlDataItemType.svg_color));
            //value = commonDAO.GetSignalDataValue(machineCode, "清扫器");
            //datas.Add(new HtmlDataItem("采样机1_清扫器", value == "1" ? "Red" : "Green", eHtmlDataItemType.svg_color));
            //value = commonDAO.GetSignalDataValue(machineCode, "缩分清扫器");
            //datas.Add(new HtmlDataItem("采样机1_缩分清扫器", value == "1" ? "Red" : "Green", eHtmlDataItemType.svg_color));

            // 添加更多...

            // 发送到页面
            cefWebBrowser.Browser.GetMainFrame().ExecuteJavaScript("requestData(" + Newtonsoft.Json.JsonConvert.SerializeObject(datas) + ");", "", 0);

            #endregion

            #region 皮带采样机 #2

            datas.Clear();
            machineCode = GlobalVars.MachineCode_PDCYJ_2;

            datas.Add(new HtmlDataItem("皮带采样机2_采样编码", commonDAO.GetSignalDataValue(machineCode, "采样编码"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("皮带采样机2_矿发量", commonDAO.GetSignalDataValue(machineCode, "矿发量"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("皮带采样机2_开始时间", commonDAO.GetSignalDataValue(machineCode, "开始时间"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("皮带采样机2_来煤车数", commonDAO.GetSignalDataValue(machineCode, "来煤车数"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("皮带采样机2_采样次数", commonDAO.GetSignalDataValue(machineCode, "采样次数"), eHtmlDataItemType.svg_text));

            // 集样罐   
            List<InfEquInfSampleBarrel> barrels2 = MonitorDAO.GetInstance().GetEquInfSampleBarrels(machineCode, eEquInfGatherType.底卸式.ToString());
            datas.Add(new HtmlDataItem("采样机2_底卸集样罐", Newtonsoft.Json.JsonConvert.SerializeObject(barrels2.Select(a => new { BarrelNumber = a.BarrelNumber, IsCurrent = a.IsCurrent, BarrelStatus = a.BarrelStatus, SampleCount = a.SampleCount })), eHtmlDataItemType.json_data));
            barrels2 = MonitorDAO.GetInstance().GetEquInfSampleBarrels(machineCode, eEquInfGatherType.密码罐.ToString());
            datas.Add(new HtmlDataItem("采样机2_密码集样罐", Newtonsoft.Json.JsonConvert.SerializeObject(barrels2.Select(a => new { BarrelNumber = a.BarrelNumber, IsCurrent = a.IsCurrent, BarrelStatus = a.BarrelStatus, SampleCount = a.SampleCount })), eHtmlDataItemType.json_data));

            value = commonDAO.GetSignalDataValue(machineCode, eSignalDataName.系统.ToString());
            if ("|就绪待机|".Contains("|" + value + "|"))
                datas.Add(new HtmlDataItem("皮带采样机2系统", "#00c000", eHtmlDataItemType.svg_color));
            else if ("|正在运行|正在卸样|".Contains("|" + value + "|"))
                datas.Add(new HtmlDataItem("皮带采样机2系统", "#ff0000", eHtmlDataItemType.svg_color));
            else if ("|发生故障|".Contains("|" + value + "|"))
                datas.Add(new HtmlDataItem("皮带采样机2系统", "#ffff00", eHtmlDataItemType.svg_color));
            else
                datas.Add(new HtmlDataItem("皮带采样机2系统", "#c0c0c0", eHtmlDataItemType.svg_color));

            value = commonDAO.GetSignalDataValue(machineCode, "弃料提升斗");
            datas.Add(new HtmlDataItem("皮带采样机2弃料提升斗", value == "1" ? "Red" : "url(#linearGradient6035-9-6)", eHtmlDataItemType.svg_color));
            value = commonDAO.GetSignalDataValue(machineCode, "输煤皮带");
            datas.Add(new HtmlDataItem("皮带采样机2输煤皮带", value == "1" ? "Red" : "#808080", eHtmlDataItemType.svg_color));
            value = commonDAO.GetSignalDataValue(machineCode, "初级给料");
            datas.Add(new HtmlDataItem("皮带采样机2初级给料", value == "1" ? "Red" : "#808080", eHtmlDataItemType.svg_color));
            value = commonDAO.GetSignalDataValue(machineCode, "初级破碎");
            datas.Add(new HtmlDataItem("皮带采样机2初级破碎1", value == "1" ? "Red" : "url(#linearGradient5969-9-7)", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("皮带采样机2初级破碎2", value == "1" ? "Red" : "url(#linearGradient5919-2-2)", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("皮带采样机2初级破碎3", value == "1" ? "Red" : "url(#linearGradient5921-4-3)", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("皮带采样机2初级破碎4", value == "1" ? "Red" : "url(#linearGradient5923-5-2)", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("皮带采样机2初级破碎5", value == "1" ? "Red" : "url(#linearGradient5925-5-2)", eHtmlDataItemType.svg_color));
            value = commonDAO.GetSignalDataValue(machineCode, "次级给料");
            datas.Add(new HtmlDataItem("皮带采样机2次级给料", value == "1" ? "Red" : "#808080", eHtmlDataItemType.svg_color));
            value = commonDAO.GetSignalDataValue(machineCode, "缩分器");
            datas.Add(new HtmlDataItem("皮带采样机2缩分器1", value == "1" ? "Red" : "url(#linearGradient13273)", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("皮带采样机2缩分器2", value == "1" ? "Red" : "url(#linearGradient5931-6-1)", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("皮带采样机2缩分器3", value == "1" ? "Red" : "url(#linearGradient7777)", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("皮带采样机2缩分器4", value == "1" ? "Red" : "url(#linearGradient5939-1-2)", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("皮带采样机2缩分器5", value == "1" ? "Red" : "url(#linearGradient5941-4-7)", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("皮带采样机2缩分器6", value == "1" ? "Red" : "url(#linearGradient5905-1-1)", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("皮带采样机2缩分器7", value == "1" ? "Red" : "url(#linearGradient13324)", eHtmlDataItemType.svg_color));

            // 添加更多...

            // 发送到页面
            cefWebBrowser.Browser.GetMainFrame().ExecuteJavaScript("requestData(" + Newtonsoft.Json.JsonConvert.SerializeObject(datas) + ");", "", 0);

            #endregion
        }

        void ReadConfig()
        {
            List<HtmlDataItem> datas = new List<HtmlDataItem>();
            CommonDAO commonDAO = CommonDAO.GetInstance();
            datas.Add(new HtmlDataItem("ckbfc1", commonDAO.GetAppletConfigString(GlobalVars.CommonAppletConfigName, "#1翻车机对应皮带采样机"), "Set"));
            datas.Add(new HtmlDataItem("ckbfc2", commonDAO.GetAppletConfigString(GlobalVars.CommonAppletConfigName, "#2翻车机对应皮带采样机"), "Set"));
            datas.Add(new HtmlDataItem("ckbjy1", commonDAO.GetAppletConfigString(GlobalVars.CommonAppletConfigName, "#1皮带采样机集样方式"), "Set"));
            datas.Add(new HtmlDataItem("ckbjy2", commonDAO.GetAppletConfigString(GlobalVars.CommonAppletConfigName, "#2皮带采样机集样方式"), "Set"));
            // 发送到页面
            cefWebBrowser.Browser.GetMainFrame().ExecuteJavaScript("requestData(" + Newtonsoft.Json.JsonConvert.SerializeObject(datas) + ");", "", 0);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // 界面不可见时，停止发送数据
            if (!this.Visible) return;

            RequestData();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            // 界面不可见时，停止发送数据
            if (!this.Visible) return;

            //30秒执行一次读取配置
            ReadConfig();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            // 发送到页面
            cefWebBrowser.Browser.GetMainFrame().ExecuteJavaScript("testColor();", "", 0);
        }

    }
}
