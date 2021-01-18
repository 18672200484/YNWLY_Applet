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

        public string Device = string.Empty;

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

            List<HtmlDataItem> datas = new List<HtmlDataItem>();
            List<InfEquInfHitch> equInfHitchs = new List<InfEquInfHitch>();

            datas.Clear();


            #region 汽车机械采样机 #1

            GetmachineInfo(commonDAO, GlobalVars.MachineCode_QCJXCYJ_1, datas, "1");
 #endregion

            #region 汽车机械采样机 #2
            
            GetmachineInfo(commonDAO, GlobalVars.MachineCode_QCJXCYJ_2, datas, "2");

            #endregion

            // 发送到页面
            cefWebBrowser.Browser.GetMainFrame().ExecuteJavaScript("requestData(" + Newtonsoft.Json.JsonConvert.SerializeObject(datas) + ");", "", 0);
        }

        private void GetmachineInfo(CommonDAO commonDAO, string machineCode, List<HtmlDataItem> datas,string type)
        {
            string value = "", carNumber= "",  kfl = "" ;
            string cym = commonDAO.GetSignalDataValue(machineCode, "采样编码");
            string cphid = commonDAO.GetSignalDataValue(machineCode, "当前车Id");
            string sql = string.Format(@"select b.ticketweight from cmcstbrcsampling d 
                left join fultbinfactorybatch c on c.id = d.infactorybatchid
                left join fultbtransport b on b.infactorybatchid = c.id 
                where d.samplecode = '{0}' and b.id = '{1}' ", cym, cphid);
            DataTable dt = commonDAO.GetSqlDatas(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                kfl = dt.Rows[0][0].ToString();
            }

            datas.Add(new HtmlDataItem("采样机" + type + "_采样编码", cym, eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("采样机" + type + "_矿发量", kfl, eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("采样机" + type + "_车牌号", commonDAO.GetSignalDataValue(machineCode, "当前车号"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("采样机" + type + "_开始时间", commonDAO.GetSignalDataValue(machineCode, "采样人"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("采样机" + type + "_采样点数", commonDAO.GetSignalDataValue(machineCode, "采样点数"), eHtmlDataItemType.svg_text));

            value = commonDAO.GetSignalDataValue(machineCode, eSignalDataName.设备状态.ToString());
            if ("|就绪待机|系统就绪|准备就绪|".Contains("|" + value + "|"))
                datas.Add(new HtmlDataItem("采样机" + type + "_系统", "#00c000", eHtmlDataItemType.svg_color));
            else if ("|正在运行|正在卸样|系统运行|".Contains("|" + value + "|"))
                datas.Add(new HtmlDataItem("采样机" + type + "_系统", "#ff0000", eHtmlDataItemType.svg_color));
            else if ("|发生故障|系统故障|".Contains("|" + value + "|"))
                datas.Add(new HtmlDataItem("采样机" + type + "_系统", "#ffff00", eHtmlDataItemType.svg_color));
            else
                datas.Add(new HtmlDataItem("采样机" + type + "_系统", "#c0c0c0", eHtmlDataItemType.svg_color));

            carNumber = commonDAO.GetSignalDataValue(machineCode, "车牌号");
            if (string.IsNullOrEmpty(carNumber))
                datas.Add(new HtmlDataItem("Car" + type, "false", eHtmlDataItemType.svg_visible));
            else
            {
                CmcsAutotruck autotruck = CommonDAO.GetInstance().GetAutotruckByCarNumber(carNumber);
                if (autotruck != null)
                {
                    if (PreviewCarCarriage(autotruck, type))
                        datas.Add(new HtmlDataItem("Car" + type, "true", eHtmlDataItemType.svg_visible));
                    else
                        datas.Add(new HtmlDataItem("Car" + type, "false", eHtmlDataItemType.svg_visible));
                }
                else
                    datas.Add(new HtmlDataItem("Car" + type, "false", eHtmlDataItemType.svg_visible));
            }
            value = GetColor(commonDAO.GetSignalDataValue(machineCode, "采样头"));
            datas.Add(new HtmlDataItem("采样机" + type + "_小车1", value != "" ? value : "url(#SVGID_8_)", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("采样机" + type + "_小车2", value != "" ? value : "url(#SVGID_6_)", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("采样机" + type + "_小车3", value != "" ? value : "url(#SVGID_7_)", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("采样机" + type + "_小车4", value != "" ? value : "url(#SVGID_2_)", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("采样机" + type + "_小车5", value != "" ? value : "url(#SVGID_3_)", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("采样机" + type + "_小车6", value != "" ? value : "url(#SVGID_4_)", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("采样机" + type + "_小车7", value != "" ? value : "url(#SVGID_5_)", eHtmlDataItemType.svg_color));

            value = GetColor(commonDAO.GetSignalDataValue(machineCode, "采样头"));
            datas.Add(new HtmlDataItem("采样机" + type + "_接料斗1", value != "" ? value : "url(#_164344952_2_)", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("采样机" + type + "_接料斗2", value != "" ? value : "url(#_130855712_2_)", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("采样机" + type + "_接料斗3", value != "" ? value : "url(#_164355560_2_)", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("采样机" + type + "_接料斗4", value != "" ? value : "url(#_164351936_2_)", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("采样机" + type + "_接料斗5", value != "" ? value : "#A9A9A9", eHtmlDataItemType.svg_color));

            value = GetColor(commonDAO.GetSignalDataValue(machineCode, "给料皮带"));
            datas.Add(new HtmlDataItem("采样机" + type + "_给料皮带", value != "" ? value : "#808080", eHtmlDataItemType.svg_color));

            value = GetColor(commonDAO.GetSignalDataValue(machineCode, "锤式破碎机"));
            datas.Add(new HtmlDataItem("采样机" + type + "_锤式破碎机1", value != "" ? value : "url(#_125277864-0_2_)", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("采样机" + type + "_锤式破碎机2", value != "" ? value : "url(#_164348960_2_)", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("采样机" + type + "_锤式破碎机3", value != "" ? value : "url(#_130859336_2_)", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("采样机" + type + "_锤式破碎机4", value != "" ? value : "url(#_130854176_2_)", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("采样机" + type + "_锤式破碎机5", value != "" ? value : "url(#_130859336-4_2_)", eHtmlDataItemType.svg_color));

            value = GetColor(commonDAO.GetSignalDataValue(machineCode, "缩分皮带"));
            datas.Add(new HtmlDataItem("采样机" + type + "_缩分皮带", value != "" ? value : "#808080", eHtmlDataItemType.svg_color));

            // 集样罐   
            List<InfEquInfSampleBarrel> barrels2 = MonitorDAO.GetInstance().GetEquInfSampleBarrels(machineCode);
            datas.Add(new HtmlDataItem("采样机" + type+"_集样罐", Newtonsoft.Json.JsonConvert.SerializeObject(barrels2.Select(a => new { BarrelNumber = a.BarrelNumber, IsCurrent = a.IsCurrent, BarrelStatus = a.BarrelStatus, SampleCount = a.SampleCount })), eHtmlDataItemType.json_data));
         
        }



        private string GetColor(string value)
        {
            string color = string.Empty;
            switch (value)
            {
                case "就绪":
                case "0":
                    color = ColorTranslator.ToHtml(EquipmentStatusColors.BeReady);
                    break;
                case "运行":
                case "1":
                    color = "Red";
                    break;
                case "故障":
                    color = ColorTranslator.ToHtml(EquipmentStatusColors.Breakdown);
                    break;
                case "停止":
                    color = "";
                    break;
                default:
                    color = "";
                    break;

            }
            return color;
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
