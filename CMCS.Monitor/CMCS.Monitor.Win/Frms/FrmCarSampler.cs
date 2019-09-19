using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CMCS.Common;
using CMCS.Common.DAO;
using CMCS.Common.Entities;
using CMCS.Common.Entities.AutoMaker;
using CMCS.Common.Entities.Inf;
using CMCS.Monitor.DAO;
using CMCS.Monitor.Win.Core;
using CMCS.Monitor.Win.Html;
using DevComponents.DotNetBar.Metro;
using Xilium.CefGlue.WindowsForms;
using CMCS.Common.Entities.CarTransport;
using System.IO;
using CMCS.Common.Enums;

namespace CMCS.Monitor.Win.Frms
{
    public partial class FrmCarSampler : MetroForm
    {
        /// <summary>
        /// 窗体唯一标识符
        /// </summary>
        public static string UniqueKey = "FrmCarSampler";

        CefWebBrowser cefWebBrowser = new CefWebBrowser();

        public FrmCarSampler()
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
            cefWebBrowser.StartUrl = SelfVars.Url_CarSampler;
            cefWebBrowser.Dock = DockStyle.Fill;
            cefWebBrowser.LoadEnd += new EventHandler<LoadEndEventArgs>(cefWebBrowser_LoadEnd);
            panWebBrower.Controls.Add(cefWebBrowser);
        }

        void cefWebBrowser_LoadEnd(object sender, LoadEndEventArgs e)
        {
            timer1.Enabled = true;
        }

        private void FrmCarSampler_Load(object sender, EventArgs e)
        {
            FormInit();
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

            #region 汽车机械采样机 #1

            datas.Clear();
            machineCode = GlobalVars.MachineCode_QC_JxSampler_1;

            datas.Add(new HtmlDataItem("采样机1_采样编码", commonDAO.GetSignalDataValue(machineCode, "采样编码"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("采样机1_矿发量", commonDAO.GetSignalDataValue(machineCode, "矿发量"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("采样机1_开始时间", commonDAO.GetSignalDataValue(machineCode, "开始时间"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("采样机1_车牌号", commonDAO.GetSignalDataValue(machineCode, "车牌号"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("采样机1_采样点数", commonDAO.GetSignalDataValue(machineCode, "采样点数"), eHtmlDataItemType.svg_text));

            value = commonDAO.GetSignalDataValue(machineCode, eSignalDataName.系统.ToString());
            if ("|就绪待机|".Contains("|" + value + "|"))
                datas.Add(new HtmlDataItem("采样机1_系统", "#00c000", eHtmlDataItemType.svg_color));
            else if ("|正在运行|正在卸样|".Contains("|" + value + "|"))
                datas.Add(new HtmlDataItem("采样机1_系统", "#ff0000", eHtmlDataItemType.svg_color));
            else if ("|发生故障|".Contains("|" + value + "|"))
                datas.Add(new HtmlDataItem("采样机1_系统", "#ffff00", eHtmlDataItemType.svg_color));
            else
                datas.Add(new HtmlDataItem("采样机1_系统", "#c0c0c0", eHtmlDataItemType.svg_color));

            datas.Add(new HtmlDataItem("采样机1_小车1", commonDAO.GetSignalDataValue(machineCode, "小车") == "1" ? "Red" : "url(#SVGID_16_)", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("采样机1_小车2", commonDAO.GetSignalDataValue(machineCode, "小车") == "1" ? "Red" : "url(#SVGID_14_)", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("采样机1_小车3", commonDAO.GetSignalDataValue(machineCode, "小车") == "1" ? "Red" : "url(#SVGID_15_)", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("采样机1_小车4", commonDAO.GetSignalDataValue(machineCode, "小车") == "1" ? "Red" : "url(#SVGID_10_)", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("采样机1_小车5", commonDAO.GetSignalDataValue(machineCode, "小车") == "1" ? "Red" : "url(#SVGID_11_)", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("采样机1_小车6", commonDAO.GetSignalDataValue(machineCode, "小车") == "1" ? "Red" : "url(#SVGID_12_)", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("采样机1_小车7", commonDAO.GetSignalDataValue(machineCode, "小车") == "1" ? "Red" : "url(#SVGID_13_)", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("采样机1_接料斗1", commonDAO.GetSignalDataValue(machineCode, "接料斗") == "1" ? "Red" : "url(#_164344952_3_)", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("采样机1_接料斗2", commonDAO.GetSignalDataValue(machineCode, "接料斗") == "1" ? "Red" : "url(#_130855712_3_)", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("采样机1_接料斗3", commonDAO.GetSignalDataValue(machineCode, "接料斗") == "1" ? "Red" : "url(#_164355560_3_)", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("采样机1_接料斗4", commonDAO.GetSignalDataValue(machineCode, "接料斗") == "1" ? "Red" : "url(#_164351936_3_)", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("采样机1_接料斗5", commonDAO.GetSignalDataValue(machineCode, "接料斗") == "1" ? "Red" : "#A9A9A9", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("采样机1_给料皮带", commonDAO.GetSignalDataValue(machineCode, "给料皮带") == "1" ? "Red" : "#A9A9A9", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("采样机1_锤式破碎机1", commonDAO.GetSignalDataValue(machineCode, "锤式破碎机") == "1" ? "Red" : "url(#_125277864-0_3_)", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("采样机1_锤式破碎机2", commonDAO.GetSignalDataValue(machineCode, "锤式破碎机") == "1" ? "Red" : "url(#_164348960_3_)", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("采样机1_锤式破碎机3", commonDAO.GetSignalDataValue(machineCode, "锤式破碎机") == "1" ? "Red" : "url(#_130859336_3_)", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("采样机1_锤式破碎机4", commonDAO.GetSignalDataValue(machineCode, "锤式破碎机") == "1" ? "Red" : "url(#_130854176_3_)", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("采样机1_锤式破碎机5", commonDAO.GetSignalDataValue(machineCode, "锤式破碎机") == "1" ? "Red" : "url(#_130859336-4_3_)", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("采样机1_缩分皮带", commonDAO.GetSignalDataValue(machineCode, "缩分皮带") == "1" ? "Red" : "#808080", eHtmlDataItemType.svg_color));

            string carNumber = commonDAO.GetSignalDataValue(machineCode, "车牌号");
            if (string.IsNullOrEmpty(carNumber))
                datas.Add(new HtmlDataItem("Car1", "false", eHtmlDataItemType.svg_visible));
            else
            {
                CmcsAutotruck autotruck = CommonDAO.GetInstance().GetAutotruckByCarNumber(carNumber);
                if (autotruck != null)
                {
                    if (PreviewCarCarriage(autotruck, "1"))
                        datas.Add(new HtmlDataItem("Car1", "true", eHtmlDataItemType.svg_visible));
                    else
                        datas.Add(new HtmlDataItem("Car1", "false", eHtmlDataItemType.svg_visible));
                }
                else
                    datas.Add(new HtmlDataItem("Car1", "false", eHtmlDataItemType.svg_visible));
            }

            // 集样罐   
            List<InfEquInfSampleBarrel> barrels1 = MonitorDAO.GetInstance().GetEquInfSampleBarrels(machineCode);
            datas.Add(new HtmlDataItem("采样机1_集样罐", Newtonsoft.Json.JsonConvert.SerializeObject(barrels1.Select(a => new { BarrelNumber = a.BarrelNumber, IsCurrent = a.IsCurrent, BarrelStatus = a.BarrelStatus, SampleCount = a.SampleCount })), eHtmlDataItemType.json_data));
            #endregion

            #region 汽车机械采样机 #2

            machineCode = GlobalVars.MachineCode_QC_JxSampler_2;

            datas.Add(new HtmlDataItem("采样机2_采样编码", commonDAO.GetSignalDataValue(machineCode, "采样编码"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("采样机2_矿发量", commonDAO.GetSignalDataValue(machineCode, "矿发量"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("采样机2_开始时间", commonDAO.GetSignalDataValue(machineCode, "开始时间"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("采样机2_车牌号", commonDAO.GetSignalDataValue(machineCode, "车牌号"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("采样机2_采样点数", commonDAO.GetSignalDataValue(machineCode, "采样点数"), eHtmlDataItemType.svg_text));

            value = commonDAO.GetSignalDataValue(machineCode, eSignalDataName.系统.ToString());
            if ("|就绪待机|".Contains("|" + value + "|"))
                datas.Add(new HtmlDataItem("采样机2_系统", "#00c000", eHtmlDataItemType.svg_color));
            else if ("|正在运行|正在卸样|".Contains("|" + value + "|"))
                datas.Add(new HtmlDataItem("采样机2_系统", "#ff0000", eHtmlDataItemType.svg_color));
            else if ("|发生故障|".Contains("|" + value + "|"))
                datas.Add(new HtmlDataItem("采样机2_系统", "#ffff00", eHtmlDataItemType.svg_color));
            else
                datas.Add(new HtmlDataItem("采样机2_系统", "#c0c0c0", eHtmlDataItemType.svg_color));

            datas.Add(new HtmlDataItem("采样机2_小车1", commonDAO.GetSignalDataValue(machineCode, "小车") == "1" ? "Red" : "url(#SVGID_8_)", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("采样机2_小车2", commonDAO.GetSignalDataValue(machineCode, "小车") == "1" ? "Red" : "url(#SVGID_6_)", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("采样机2_小车3", commonDAO.GetSignalDataValue(machineCode, "小车") == "1" ? "Red" : "url(#SVGID_7_)", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("采样机2_小车4", commonDAO.GetSignalDataValue(machineCode, "小车") == "1" ? "Red" : "url(#SVGID_2_)", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("采样机2_小车5", commonDAO.GetSignalDataValue(machineCode, "小车") == "1" ? "Red" : "url(#SVGID_3_)", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("采样机2_小车6", commonDAO.GetSignalDataValue(machineCode, "小车") == "1" ? "Red" : "url(#SVGID_4_)", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("采样机2_小车7", commonDAO.GetSignalDataValue(machineCode, "小车") == "1" ? "Red" : "url(#SVGID_5_)", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("采样机2_接料斗1", commonDAO.GetSignalDataValue(machineCode, "接料斗") == "1" ? "Red" : "url(#_164344952_2_)", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("采样机2_接料斗2", commonDAO.GetSignalDataValue(machineCode, "接料斗") == "1" ? "Red" : "url(#_130855712_2_)", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("采样机2_接料斗3", commonDAO.GetSignalDataValue(machineCode, "接料斗") == "1" ? "Red" : "url(#_164355560_2_)", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("采样机2_接料斗4", commonDAO.GetSignalDataValue(machineCode, "接料斗") == "1" ? "Red" : "url(#_164351936_2_)", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("采样机2_接料斗5", commonDAO.GetSignalDataValue(machineCode, "接料斗") == "1" ? "Red" : "#A9A9A9", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("采样机2_给料皮带", commonDAO.GetSignalDataValue(machineCode, "给料皮带") == "1" ? "Red" : "#808080", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("采样机2_锤式破碎机1", commonDAO.GetSignalDataValue(machineCode, "锤式破碎机") == "1" ? "Red" : "url(#_125277864-0_2_)", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("采样机2_锤式破碎机2", commonDAO.GetSignalDataValue(machineCode, "锤式破碎机") == "1" ? "Red" : "url(#_164348960_2_)", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("采样机2_锤式破碎机3", commonDAO.GetSignalDataValue(machineCode, "锤式破碎机") == "1" ? "Red" : "url(#_130859336_2_)", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("采样机2_锤式破碎机4", commonDAO.GetSignalDataValue(machineCode, "锤式破碎机") == "1" ? "Red" : "url(#_130854176_2_)", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("采样机2_锤式破碎机5", commonDAO.GetSignalDataValue(machineCode, "锤式破碎机") == "1" ? "Red" : "url(#_130859336-4_2_)", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("采样机2_缩分皮带", commonDAO.GetSignalDataValue(machineCode, "缩分皮带") == "1" ? "Red" : "#808080", eHtmlDataItemType.svg_color));

            carNumber = commonDAO.GetSignalDataValue(machineCode, "车牌号");
            if (string.IsNullOrEmpty(carNumber))
                datas.Add(new HtmlDataItem("Car2", "false", eHtmlDataItemType.svg_visible));
            else
            {
                CmcsAutotruck autotruck = CommonDAO.GetInstance().GetAutotruckByCarNumber(carNumber);
                if (autotruck != null)
                {
                    if (PreviewCarCarriage(autotruck, "2"))
                        datas.Add(new HtmlDataItem("Car2", "true", eHtmlDataItemType.svg_visible));
                    else
                        datas.Add(new HtmlDataItem("Car2", "false", eHtmlDataItemType.svg_visible));
                }
                else
                    datas.Add(new HtmlDataItem("Car2", "false", eHtmlDataItemType.svg_visible));
            }

            // 集样罐   
            List<InfEquInfSampleBarrel> barrels2 = MonitorDAO.GetInstance().GetEquInfSampleBarrels(machineCode);
            datas.Add(new HtmlDataItem("采样机2_集样罐", Newtonsoft.Json.JsonConvert.SerializeObject(barrels2.Select(a => new { BarrelNumber = a.BarrelNumber, IsCurrent = a.IsCurrent, BarrelStatus = a.BarrelStatus, SampleCount = a.SampleCount })), eHtmlDataItemType.json_data));
            #endregion

            // 发送到页面
            cefWebBrowser.Browser.GetMainFrame().ExecuteJavaScript("requestData(" + Newtonsoft.Json.JsonConvert.SerializeObject(datas) + ");", "", 0);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // 界面不可见时，停止发送数据
            if (!this.Visible) return;

            RequestData();
        }

        private void btnHistory_Click(object sender, EventArgs e)
        {
            FrmAutoMakerRecord frm = new FrmAutoMakerRecord();
            frm.ShowDialog();
        }

        /// <summary>
        /// 刷新页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            cefWebBrowser.Browser.Reload();
        }

        private void btnRequestData_Click(object sender, EventArgs e)
        {
            RequestData();
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            // 发送到页面
            cefWebBrowser.Browser.GetMainFrame().ExecuteJavaScript("testColor();", "", 0);

            CmcsAutotruck autotruck = CommonDAO.GetInstance().GetAutotruckByCarNumber("鄂ATM168");
            Bitmap res = new Bitmap(CMCS.Monitor.Win.Properties.Resources.Autotruck);
            PreviewCarBmp carBmp = new PreviewCarBmp(autotruck);
            Bitmap bmp = carBmp.GetPreviewBitmap(res, 249, 130);
            bmp.Save("Autotruck.bmp");
        }


        /// <summary>
        /// 预览车辆拉筋信息图
        /// </summary>
        /// <param name="autotruck"></param>
        private bool PreviewCarCarriage(CmcsAutotruck autotruck, string sampler)
        {
            if (autotruck != null && autotruck.CarriageLength > 0 && autotruck.CarriageWidth > 0)
            {
                Bitmap res = new Bitmap(CMCS.Monitor.Win.Properties.Resources.Autotruck);
                PreviewCarBmp carBmp = new PreviewCarBmp(autotruck);
                Bitmap bmp = carBmp.GetPreviewBitmap(res, 249, 130);
                bmp.Save("Autotruck_" + sampler + ".bmp");
                return true;
            }
            return false;
        }
    }
}
