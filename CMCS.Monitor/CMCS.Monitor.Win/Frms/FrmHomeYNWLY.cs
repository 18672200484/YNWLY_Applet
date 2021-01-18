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
using CMCS.Common.Entities.BaseInfo;
using System.Linq;

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

            //RequestData();
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

            datas.Add(new HtmlDataItem("#入厂端_道闸1", ConvertBooleanToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QC_Queue_1, "道闸1升杆")), eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("#入厂端_道闸2", ConvertBooleanToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QC_Queue_1, "道闸2升杆")), eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("#入厂端_车号", "入场车号：" + commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QC_Queue_1, eSignalDataName.当前车号.ToString()), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#入厂端_车号2", "入场车号：" + commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QC_Queue_1, eSignalDataName.当前车号2.ToString()), eHtmlDataItemType.svg_text));

            datas.Add(new HtmlDataItem("#出厂端_道闸1", ConvertBooleanToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QC_Out_1, "道闸1升杆")), eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("#出厂端_道闸2", ConvertBooleanToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QC_Out_1, "道闸2升杆")), eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("#出厂端_车号", "出场车号：" + commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QC_Out_1, eSignalDataName.当前车号.ToString()), eHtmlDataItemType.svg_text));

            datas.Add(new HtmlDataItem("#销售入厂端_道闸1", ConvertBooleanToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_XSQC_Queue_1, "道闸1升杆")), eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("#销售入厂端_道闸2", ConvertBooleanToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_XSQC_Queue_1, "道闸2升杆")), eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("#销售入厂端_车号", "入场车号：" + commonDAO.GetSignalDataValue(GlobalVars.MachineCode_XSQC_Queue_1, eSignalDataName.当前车号.ToString()), eHtmlDataItemType.svg_text));

            GetDBZT(GlobalVars.MachineCode_QC_Weighter_1, datas, "1");
            GetDBZT(GlobalVars.MachineCode_QC_Weighter_2, datas, "2");
            GetDBZT(GlobalVars.MachineCode_QC_Weighter_3, datas, "3");
            GetDBZT(GlobalVars.MachineCode_QC_Weighter_4, datas, "4");
            GetDBZT(GlobalVars.MachineCode_QC_Weighter_5, datas, "5");
            GetDBZT(GlobalVars.MachineCode_QC_Weighter_6, datas, "6");
            GetDBZT(GlobalVars.MachineCode_QC_Weighter_7, datas, "7");
            GetDBZT(GlobalVars.MachineCode_QC_Weighter_8, datas, "8");

            datas.Add(new HtmlDataItem("#1汽车机械采样机", ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QCJXCYJ_1, eSignalDataName.设备状态.ToString())), eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("#1汽车机械采样机车号", "采样车号：" + commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QCJXCYJ_1, "车牌号"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#1汽车机械采样机已采车数","已采车数:" + commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QCJXCYJ_1, "已采车数"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#1汽车机械采样机_道闸1", ConvertBooleanToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QCJXCYJ_1, "道闸1升杆")), eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("#1汽车机械采样机_道闸2", ConvertBooleanToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QCJXCYJ_1, "道闸2升杆")), eHtmlDataItemType.svg_color));

            datas.Add(new HtmlDataItem("#2汽车机械采样机", ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QCJXCYJ_2, eSignalDataName.设备状态.ToString())), eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("#2汽车机械采样机车号", "采样车号：" + commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QCJXCYJ_2, "车牌号"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#2汽车机械采样机已采车数", "已采车数：" + commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QCJXCYJ_2, "已采车数"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#2汽车机械采样机_道闸1", ConvertBooleanToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QCJXCYJ_2, "道闸1升杆")), eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("#2汽车机械采样机_道闸2", ConvertBooleanToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QCJXCYJ_2, "道闸2升杆")), eHtmlDataItemType.svg_color));
                 
            datas.Add(new HtmlDataItem("#1翻车机", ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_Recognition_1, eSignalDataName.系统.ToString())), eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("#1翻车机已翻数", "已翻数：" + commonDAO.GetSignalDataValue(GlobalVars.MachineCode_Recognition_1, "当日已过车数"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#1翻车机当前车号", "翻车车号：" + commonDAO.GetSignalDataValue(GlobalVars.MachineCode_Recognition_1, "当前车号"), eHtmlDataItemType.svg_text));

            datas.Add(new HtmlDataItem("#2翻车机", ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_TrunOver_2, eSignalDataName.系统.ToString())), eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("#2翻车机已翻数", "已翻数：" + commonDAO.GetSignalDataValue(GlobalVars.MachineCode_Recognition_2, "当日已过车数"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#2翻车机当前车号", "翻车车号：" + commonDAO.GetSignalDataValue(GlobalVars.MachineCode_Recognition_2, "当前车号"), eHtmlDataItemType.svg_text));

            datas.Add(new HtmlDataItem("#1皮带采样机", ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_InPDCYJ_1, eSignalDataName.设备状态.ToString())), eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("#1皮带采样机车号", "#1采样车号：" + commonDAO.GetSignalDataValue(GlobalVars.MachineCode_InPDCYJ_1, "车牌号"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#1皮带采样机已采车数", "已采车数：" + commonDAO.GetSignalDataValue(GlobalVars.MachineCode_InPDCYJ_1, "已采车数"), eHtmlDataItemType.svg_text));

            datas.Add(new HtmlDataItem("#2皮带采样机", ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_InPDCYJ_2, eSignalDataName.设备状态.ToString())), eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("#2皮带采样机车号", "#2采样车号：" + commonDAO.GetSignalDataValue(GlobalVars.MachineCode_InPDCYJ_2, "车牌号"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#2皮带采样机已采车数", "已采车数：" + commonDAO.GetSignalDataValue(GlobalVars.MachineCode_InPDCYJ_2, "已采车数"), eHtmlDataItemType.svg_text));

            datas.Add(new HtmlDataItem("#3皮带采样机", ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_OutPDCYJ_1, eSignalDataName.设备状态.ToString())), eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("#4皮带采样机", ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_OutPDCYJ_1, eSignalDataName.设备状态.ToString())), eHtmlDataItemType.svg_color));

            datas.Add(new HtmlDataItem("dljimg1", ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_DouLunJi_1, eSignalDataName.设备状态.ToString())), eHtmlDataItemType.svg_State));
            datas.Add(new HtmlDataItem("dljimg2", ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_DouLunJi_2, eSignalDataName.设备状态.ToString())), eHtmlDataItemType.svg_State));
            datas.Add(new HtmlDataItem("dlj1", "" + commonDAO.GetSignalDataValue(GlobalVars.MachineCode_DouLunJi_1, "大车行走位置"), eHtmlDataItemType.svg_Place));
            datas.Add(new HtmlDataItem("dlj2", "" + commonDAO.GetSignalDataValue(GlobalVars.MachineCode_DouLunJi_2, "大车行走位置"), eHtmlDataItemType.svg_Place));

            datas.Add(new HtmlDataItem("#1全自动制样机", ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QZDZYJ_1, eSignalDataName.设备状态.ToString())), eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("#1全自动制样机制样码", "制样码：" + commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QZDZYJ_1, "制样编码"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#1全自动制样机已制样数", "已制样数：" + commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QZDZYJ_1, "已制样数"), eHtmlDataItemType.svg_text));

            datas.Add(new HtmlDataItem("#2全自动制样机", ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QZDZYJ_2, eSignalDataName.设备状态.ToString())), eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("#2全自动制样机制样码", "制样码：" + commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QZDZYJ_2, "制样编码"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#2全自动制样机已制样数", "已制样数：" + commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QZDZYJ_2, "已制样数"), eHtmlDataItemType.svg_text));

            datas.Add(new HtmlDataItem("#3全自动制样机", ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QZDZYJ_3, eSignalDataName.设备状态.ToString())), eHtmlDataItemType.svg_color));

            datas.Add(new HtmlDataItem("存样柜", ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_CYG1, eSignalDataName.程序状态.ToString())), eHtmlDataItemType.svg_color));
           
            //datas.Add(new HtmlDataItem("#1皮带秤", commonDAO.GetSignalDataValueDouble("#1皮带秤", eSignalDataName.瞬时流量.ToString()) > 0 ? ColorTranslator.ToHtml(EquipmentStatusColors.Working) : ColorTranslator.ToHtml(EquipmentStatusColors.BeReady), eHtmlDataItemType.svg_color));
            //datas.Add(new HtmlDataItem("#2皮带秤", commonDAO.GetSignalDataValueDouble("#2皮带秤", eSignalDataName.瞬时流量.ToString()) > 0 ? ColorTranslator.ToHtml(EquipmentStatusColors.Working) : ColorTranslator.ToHtml(EquipmentStatusColors.BeReady), eHtmlDataItemType.svg_color));
            
            //普通皮带秤
            datas.Add(new HtmlDataItem("#1皮带秤值", setPotValue("#1皮带秤", eSignalDataName.瞬时流量.ToString()), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#1皮带秤值累计", setPotValue("#1皮带秤", eSignalDataName.累计流量.ToString()), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#2皮带秤值", setPotValue("#2皮带秤", eSignalDataName.瞬时流量.ToString()), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#2皮带秤值累计", setPotValue("#2皮带秤", eSignalDataName.累计流量.ToString()), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#3皮带秤值", setPotValue("#3皮带秤", eSignalDataName.瞬时流量.ToString()), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#3皮带秤值累计", setPotValue("#3皮带秤", eSignalDataName.累计流量.ToString()), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#4皮带秤值", setPotValue("#4皮带秤", eSignalDataName.瞬时流量.ToString()), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#4皮带秤值累计", setPotValue("#4皮带秤", eSignalDataName.累计流量.ToString()), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#5皮带秤值", setPotValue("#5皮带秤", eSignalDataName.瞬时流量.ToString()), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#5皮带秤值累计", setPotValue("#5皮带秤", eSignalDataName.累计流量.ToString()), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#6皮带秤值", setPotValue("#6皮带秤", eSignalDataName.瞬时流量.ToString()), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#6皮带秤值累计", setPotValue("#6皮带秤", eSignalDataName.累计流量.ToString()), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#7皮带秤值", setPotValue("#7皮带秤", eSignalDataName.瞬时流量.ToString()), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#7皮带秤值累计", setPotValue("#7皮带秤", eSignalDataName.累计流量.ToString()), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#8皮带秤值", setPotValue("#8皮带秤", eSignalDataName.瞬时流量.ToString()), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#8皮带秤值累计", setPotValue("#8皮带秤", eSignalDataName.累计流量.ToString()), eHtmlDataItemType.svg_text));

            //高精度皮带秤
            datas.Add(new HtmlDataItem("#5高精度皮带秤", setPotValue("#5高精度皮带秤", eSignalDataName.瞬时流量.ToString()), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#5高精度皮带秤累计", setPotValue("#5高精度皮带秤", eSignalDataName.累计流量.ToString()), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#6a高精度皮带秤", setPotValue("#6a高精度皮带秤", eSignalDataName.瞬时流量.ToString()), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#6a高精度皮带秤累计", setPotValue("#6a高精度皮带秤", eSignalDataName.累计流量.ToString()), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#6b高精度皮带秤", setPotValue("#6b高精度皮带秤", eSignalDataName.瞬时流量.ToString()), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#6b高精度皮带秤累计", setPotValue("#6b高精度皮带秤", eSignalDataName.累计流量.ToString()), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#7a高精度皮带秤", setPotValue("#7a高精度皮带秤", eSignalDataName.瞬时流量.ToString()), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#7a高精度皮带秤累计", setPotValue("#7a高精度皮带秤", eSignalDataName.累计流量.ToString()), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#7b高精度皮带秤", setPotValue("#7b高精度皮带秤", eSignalDataName.瞬时流量.ToString()), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#7b高精度皮带秤累计", setPotValue("#7b高精度皮带秤", eSignalDataName.累计流量.ToString()), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#8高精度皮带秤", setPotValue("#8高精度皮带秤", eSignalDataName.瞬时流量.ToString()), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#8高精度皮带秤累计", setPotValue("#8高精度皮带秤", eSignalDataName.累计流量.ToString()), eHtmlDataItemType.svg_text));

            datas.Add(new HtmlDataItem("存样柜超期", boxTimeOut(), eHtmlDataItemType.svg_visible));

            //成品仓
            datas.Add(new HtmlDataItem("#1成品仓料位", "#1成品仓料位：" + commonDAO.GetSignalDataValue(GlobalVars.Poduct_Pot_1, "料位"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#2成品仓料位", "#2成品仓料位：" + commonDAO.GetSignalDataValue(GlobalVars.Poduct_Pot_1, "料位"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#3成品仓料位", "#3成品仓料位：" + commonDAO.GetSignalDataValue(GlobalVars.Poduct_Pot_1, "料位"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#1A成品仓车号", "#1A成品仓车号：" + commonDAO.GetSignalDataValue(GlobalVars.Poduct_Pot_1, "当前车号1"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#1B成品仓车号", "#1B成品仓车号：" + commonDAO.GetSignalDataValue(GlobalVars.Poduct_Pot_1, "当前车号2"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#2A成品仓车号", "#2A成品仓车号：" + commonDAO.GetSignalDataValue(GlobalVars.Poduct_Pot_2, "当前车号1"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#2B成品仓车号", "#2B成品仓车号：" + commonDAO.GetSignalDataValue(GlobalVars.Poduct_Pot_2, "当前车号2"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#3A成品仓车号", "#3A成品仓车号：" + commonDAO.GetSignalDataValue(GlobalVars.Poduct_Pot_3, "当前车号1"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#3B成品仓车号", "#3B成品仓车号：" + commonDAO.GetSignalDataValue(GlobalVars.Poduct_Pot_3, "当前车号2"), eHtmlDataItemType.svg_text));

            //getPoleTemp(datas);


            getStorage(datas);

            //datas.Add(new HtmlDataItem("#1动态衡今日进出厂数量", string.Format("进厂:{0}节  出厂:{1}节", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_GDH_1, "今日进厂数量"), commonDAO.GetSignalDataValue(GlobalVars.MachineCode_GDH_1, "今日出厂数量")), eHtmlDataItemType.svg_text));
            //string XMJS_QD_Color = ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QD, eSignalDataName.程序状态.ToString()));
            //datas.Add(new HtmlDataItem("#1气动传输_气动站1", XMJS_QD_Color, eHtmlDataItemType.svg_color));
            //datas.Add(new HtmlDataItem("#1气动传输_气动站2", XMJS_QD_Color, eHtmlDataItemType.svg_color));
            //datas.Add(new HtmlDataItem("#1气动传输_气动站3", XMJS_QD_Color, eHtmlDataItemType.svg_color));
            //datas.Add(new HtmlDataItem("#1火车机械采样机", ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HCJXCYJ_1, eSignalDataName.程序状态.ToString())), eHtmlDataItemType.svg_color));
            //datas.Add(new HtmlDataItem("#2火车机械采样机", ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HCJXCYJ_2, eSignalDataName.程序状态.ToString())), eHtmlDataItemType.svg_color));

            // 添加更多...

            //GC.Collect();
            // 发送到页面
            cefWebBrowser.Browser.GetMainFrame().ExecuteJavaScript("requestData(" + Newtonsoft.Json.JsonConvert.SerializeObject(datas) + ");", "", 0);
        }


        private void getStorage(List<HtmlDataItem> datas)
        {
            string sql1 = @"select a.id from stgtbfuelstorage a where a.id != '-1' order by a.unitorder ";

            DataTable dt1 = commonDAO.SelfDber.ExecuteDataTable(sql1);
            int j = 1;
            int i = 1;
            List<point> dicPoints2 = new List<point>();
       
            if (dt1 != null && dt1.Rows.Count > 0)
            {
                foreach (DataRow dr in dt1.Rows)
                {
                    string sql2 = string.Format(@"select distinct a.startpoint,a.endpoint,a.name,a.qtyhave,a.fuelstorageid from VIEW_STORAGEJKSY a where a.qtyhave> 0 and a.fuelstorageid = '{0}' order by a.startpoint", dr["id"].ToString());
                    List<point> dicPoints = new List<point>();
                    #region 设置煤块起始点
                    DataTable dt2 = commonDAO.SelfDber.ExecuteDataTable(sql2);
                  
                    if (dt2 != null && dt2.Rows.Count > 0)
                    {
                        foreach (DataRow dr2 in dt2.Rows)
                        {
                            point Points2 = new point();
                            string MineName = dr2["name"].ToString();
                            decimal StartPoint = Convert.ToDecimal(dr2["startpoint"].ToString());
                            decimal EndPoint = Convert.ToDecimal(dr2["endpoint"].ToString());
                            decimal qtyhave = Convert.ToDecimal(dr2["qtyhave"].ToString());
                            string storageId = dr2["fuelstorageid"].ToString();

                            Points2.StartPoint = StartPoint;
                            Points2.EndPoint = EndPoint;
                            Points2.MineName = MineName;
                            Points2.QtyHave = qtyhave;
                            Points2.storageId = storageId;
                            dicPoints2.Add(Points2);

                            if (dicPoints.Count == 0)
                            {
                                point model = new point();
                                model.StartPoint = StartPoint;
                                model.EndPoint = EndPoint;
                                model.MineName = MineName;
                                model.storageId = storageId;
                                dicPoints.Add(model);
                            }
                            else
                            {
                                point old = dicPoints.Where(t => t.MineName == MineName).OrderByDescending(t => t.EndPoint).FirstOrDefault();
                                if (old != null)
                                {
                                    if (old.EndPoint == StartPoint || (old.StartPoint == StartPoint && old.EndPoint < EndPoint))
                                    {
                                        old.EndPoint = EndPoint;//连续的，则直接替换结束点  0-120
                                    }
                                    else if (old.StartPoint <= StartPoint && old.EndPoint >= EndPoint)
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        //不连续，并且当前开始点大于上一个结束点 400-450
                                        point model = new point();
                                        model.StartPoint = StartPoint;
                                        model.EndPoint = EndPoint;
                                        model.MineName = MineName;
                                        model.storageId = storageId;
                                        dicPoints.Add(model);
                                    }
                                }
                                else
                                {
                                    point model = new point();
                                    model.StartPoint = StartPoint;
                                    model.EndPoint = EndPoint;
                                    model.MineName = MineName;
                                    model.storageId = storageId;
                                    dicPoints.Add(model);
                                }
                            }
                        }
                    }
                    #endregion


                    foreach (point pt in dicPoints)
                    {
                        decimal qtyHave = dicPoints2.Where(t => t.StartPoint >= pt.StartPoint && t.EndPoint <= pt.EndPoint).Sum(t => t.QtyHave);
                        datas.Add(new HtmlDataItem("煤场煤堆_" + i, pt.StartPoint
                                + "|" + pt.EndPoint
                                + "|" + pt.MineName
                                + "|" + j
                                + "|" + qtyHave
                                + "|" + pt.storageId
                                , eHtmlDataItemType.svg_Temp));
                        i++;
                    }
                    j++;
                }
            }
        }


        private void getPoleTemp(List<HtmlDataItem> datas)
        {
            string sql1 = @"select a.pointx,a.pointy,a.unitname,a.TEMPERATURE,a.polecode from stgtbstoragetemperature a";

            DataTable dt1 = commonDAO.SelfDber.ExecuteDataTable(sql1);

            if (dt1 != null && dt1.Rows.Count > 0)
            {
                int i = 1;
                foreach (DataRow dr in dt1.Rows)
                {
                    datas.Add(new HtmlDataItem("煤场温度测试仪_" + i, dr["polecode"].ToString()
                            + "|" + dr["TEMPERATURE"].ToString()
                            + "|" + dr["pointx"].ToString()
                            + "|" + dr["pointy"].ToString()
                            + "|" + dr["unitname"].ToString()
                            , eHtmlDataItemType.svg_Temp));
                    i++;
                }
            }
        }

        private string setPotValue(string str,string type)
        {
            var d = commonDAO.GetSignalDataValue(str, type);
            if(string.IsNullOrEmpty(d))
            {
                return "";
            }
            else
            {
                return d.ToString() + "t";
            }
        }

        private string boxTimeOut()
        {
            string sql1 = @"select min(a.updatetime) updatetime from inftbcygsam a where a.isnew = 1";
            string sql2 = @"select (b.configvalue) from cmcstbappletconfig b where b.configname = '存样柜超期天数'";

            DataTable dt1 = commonDAO.SelfDber.ExecuteDataTable(sql1);
            DataTable dt2 = commonDAO.SelfDber.ExecuteDataTable(sql2);

            if (dt1 != null && dt1.Rows.Count > 0 && dt2 != null && dt2.Rows.Count > 0)
            {
                DateTime minUpdatetime = Convert.ToDateTime(dt1.Rows[0][0].ToString());

                int configvalue = Convert.ToInt32(dt2.Rows[0][0].ToString());

                if (minUpdatetime.AddDays(configvalue) < DateTime.Now)
                {
                    return "true";
                }
            }
            return "false";
        }

        private void GetDBZT(string MachineCode, List<HtmlDataItem> datas,string type)
        {
            string zl = commonDAO.GetSignalDataValue(MachineCode, eSignalDataName.地磅仪表_实时重量.ToString());
            string cxzt = commonDAO.GetSignalDataValue(MachineCode, eSignalDataName.程序状态.ToString());
            string sbzt = commonDAO.GetSignalDataValue(MachineCode, eSignalDataName.设备状态.ToString());

            if (cxzt == "1" && sbzt == "1")
            {
                if (Convert.ToDecimal(zl) > 0)
                {
                    datas.Add(new HtmlDataItem("#" + type + "磅", ColorTranslator.ToHtml(EquipmentStatusColors.BeReady), eHtmlDataItemType.svg_color));
                }
                else
                {
                    datas.Add(new HtmlDataItem("#" + type + "磅", ColorTranslator.ToHtml(EquipmentStatusColors.Working), eHtmlDataItemType.svg_color));
                }
            }
            else if (sbzt == "0")
            {
                datas.Add(new HtmlDataItem("#" + type + "磅", ColorTranslator.ToHtml(EquipmentStatusColors.Breakdown), eHtmlDataItemType.svg_color));
            }
            else
            {
                datas.Add(new HtmlDataItem("#" + type + "磅", ColorTranslator.ToHtml(EquipmentStatusColors.Forbidden), eHtmlDataItemType.svg_color));
            }

            datas.Add(new HtmlDataItem("#" + type + "磅_车号", "#" + type + "磅过磅车号：" + commonDAO.GetSignalDataValue(MachineCode, eSignalDataName.当前车号.ToString()), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#" + type + "磅_重量", "#" + type + "磅过磅重量：" + getZl(zl), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#" + type + "磅_道闸1", ConvertBooleanToColor(commonDAO.GetSignalDataValue(MachineCode, "道闸1升杆")), eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("#" + type + "磅_道闸2", ConvertBooleanToColor(commonDAO.GetSignalDataValue(MachineCode, "道闸2升杆")), eHtmlDataItemType.svg_color));

        }

        private string getZl(string str)
        {
            if (string.IsNullOrEmpty(str) || str == "0")
            {
                return "";
            }
            else
            {
                return str + "吨";
            }
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
            if ("|就绪待机|系统就绪|等待就绪|".Contains("|" + systemStatus + "|"))
                return ColorTranslator.ToHtml(EquipmentStatusColors.BeReady);//灰色
            else if ("|正在运行|正在卸样|系统运行|".Contains("|" + systemStatus + "|"))
                return ColorTranslator.ToHtml(EquipmentStatusColors.Working);//绿色
            else if ("|发生故障|系统故障|".Contains("|" + systemStatus + "|"))
                return ColorTranslator.ToHtml(EquipmentStatusColors.Breakdown);//黄色
            else
                return ColorTranslator.ToHtml(EquipmentStatusColors.Forbidden);//
        }

        /// <summary>
        /// 转换布尔类型状态为颜色值
        /// </summary>
        /// <param name="status">状态</param>
        /// <returns></returns>
        private string ConvertBooleanToColor(string status)
        {
            if(status.ToLower()  == "0")
            {
                return ColorTranslator.ToHtml(EquipmentStatusColors.Working);
            }
            else if(status.ToLower()  == "1")
            {
                return ColorTranslator.ToHtml(EquipmentStatusColors.BeReady);
            }
            else
            {
                return ColorTranslator.ToHtml(EquipmentStatusColors.Breakdown);
            }
        }

        /// <summary>
        /// 转换布尔类型状态为颜色值
        /// </summary>
        /// <param name="status">状态</param>
        /// <returns></returns>
        private string ConvertBoolean(string status)
        {
            return status.ToLower() == "1" ? "true" : "false";
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
            string par = "";
            if ( message.Arguments.Count > 0)
            {
                par =  message.Arguments.GetString(0);
            }
            if (message.Name == "OpenTrainBeltSampler")
                SelfVars.MainFrameForm.OpenTrainBeltSampler();
            if (message.Name == "OpenOutTrainBeltSampler")
                SelfVars.MainFrameForm.OpenOutTrainBeltSampler();
            //else if (message.Name == "OpenTrainMachinerySampler")
                //SelfVars.MainFrameForm.OpenTrainMachinerySampler();
            else if (message.Name == "OpenAutoMaker")
                SelfVars.MainFrameForm.OpenAutoMaker(par);
            else if (message.Name == "OpenTrainTipper")
                SelfVars.MainFrameForm.OpenTrainTipper(par);
            else if (message.Name == "OpenWeightBridgeLoadToday")
                SelfVars.MainFrameForm.OpenWeightBridgeLoadToday();
            else if (message.Name == "OpenTruckWeighter")
                SelfVars.MainFrameForm.OpenTruckWeighter();
            else if (message.Name == "OpenTruckOrder")
                SelfVars.MainFrameForm.OpenTruckOrder();
            else if (message.Name == "OpenTruckMachinerySampler")
                SelfVars.MainFrameForm.OpenTruckMachinerySampler(par);
            else if (message.Name == "OpenAutoCupboardPneumaticTransfer")
                SelfVars.MainFrameForm.OpenAutoCupboardPneumaticTransfer(par);
            else if (message.Name == "OpenAssayManage")
                SelfVars.MainFrameForm.OpenAssayManage();
            else if (message.Name == "OpenEquInfHitch")
                SelfVars.MainFrameForm.OpenEquInfHitch();
            else if (message.Name == "OpenPoundInfo")
                SelfVars.MainFrameForm.OpenPoundInfo(par);
            else if (message.Name == "OpenInOutInfo")
                SelfVars.MainFrameForm.OpenInOutInfo(par);
            else if (message.Name == "OpenCarMonitor")
                SelfVars.MainFrameForm.OpenCarMonitor();
            

            return base.OnProcessMessageReceived(browser, sourceProcess, message);
        }
    }

    public class point
    {
        public decimal StartPoint { get; set; }
        public decimal EndPoint { get; set; }
        public string MineName { get; set; }
        public decimal QtyHave { get; set; }
        public string storageId { get; set; }
    }
}