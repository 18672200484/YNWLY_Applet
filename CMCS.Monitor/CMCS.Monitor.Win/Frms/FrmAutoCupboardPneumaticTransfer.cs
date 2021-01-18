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
using CMCS.Monitor.Win.Core;
using CMCS.Monitor.Win.Html;
using DevComponents.DotNetBar.Metro;
using Xilium.CefGlue.WindowsForms;
using CMCS.Common.Entities.AutoCupboard;
using CMCS.Common.Enums;

namespace CMCS.Monitor.Win.Frms
{
    public partial class FrmAutoCupboardPneumaticTransfer : MetroForm
    {
        /// <summary>
        /// 窗体唯一标识符
        /// </summary>
        public static string UniqueKey = "FrmAutoCupboardPneumaticTransfer";

        CefWebBrowser cefWebBrowser = new CefWebBrowser();
        public static string Device = string.Empty;


        public FrmAutoCupboardPneumaticTransfer()
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
            cefWebBrowser.StartUrl = SelfVars.Url_AutoCupboardPneumaticTransfer;
            cefWebBrowser.Dock = DockStyle.Fill;
            cefWebBrowser.LoadEnd += new EventHandler<LoadEndEventArgs>(cefWebBrowser_LoadEnd);
            panWebBrower.Controls.Add(cefWebBrowser);
        }

        void cefWebBrowser_LoadEnd(object sender, LoadEndEventArgs e)
        {
            timer1.Enabled = true;
        }

        private void FrmAutoCupboardPneumaticTransfer_Load(object sender, EventArgs e)
        {
            FormInit();
        }

        /// <summary>
        /// 请求数据
        /// </summary>
        void RequestData()
        {
            CommonDAO commonDAO = CommonDAO.GetInstance();
            AutoMakerDAO automakerDAO = AutoMakerDAO.GetInstance();

            string value = string.Empty, machineCode = string.Empty;
            List<HtmlDataItem> datas = new List<HtmlDataItem>();
            List<InfEquInfHitch> equInfHitchs = new List<InfEquInfHitch>();

            #region 智能存样柜 #

            datas.Clear();

            GetBoxDatas(commonDAO, GlobalVars.MachineCode_CYG1, datas);

            GetBoxDatas(commonDAO, GlobalVars.MachineCode_CYG2, datas);

         

            machineCode = GlobalVars.MachineCode_QD;
            //datas.Add(new HtmlDataItem("Keys", commonDAO.GetSignalDataValue(machineCode, "风机") == "1" ? "气动传输_风机" : "", eHtmlDataItemType.svg_scroll3));

            value = commonDAO.GetSignalDataValue(machineCode, eSignalDataName.程序状态.ToString());
            if ("|就绪待机|".Contains("|" + value + "|"))
                datas.Add(new HtmlDataItem("气动传输_系统", "#00c000", eHtmlDataItemType.svg_color));
            else if ("|正在运行|正在卸样|".Contains("|" + value + "|"))
                datas.Add(new HtmlDataItem("气动传输_系统", "#ff0000", eHtmlDataItemType.svg_color));
            else if ("|发生故障|".Contains("|" + value + "|"))
                datas.Add(new HtmlDataItem("气动传输_系统", "#ffff00", eHtmlDataItemType.svg_color));
            else
                datas.Add(new HtmlDataItem("气动传输_系统", "#c0c0c0", eHtmlDataItemType.svg_color));

            //信号状态
            string keys = "";

            if (commonDAO.GetSignalDataValue(machineCode, "风机运转") == "1")
            {
                keys += "风机正转";
            }

            datas.Add(new HtmlDataItem(machineCode + "Keys", keys, eHtmlDataItemType.svg_scroll));

            datas.Add(new HtmlDataItem("1#换向器_位置1", commonDAO.GetSignalDataValue(machineCode, "1#换向器_位置1") == "1" ? "#ee4036" : "#ffffff", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("1#换向器_位置2", commonDAO.GetSignalDataValue(machineCode, "1#换向器_位置2") == "1" ? "#ee4036" : "#ffffff", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("1#换向器_位置3", commonDAO.GetSignalDataValue(machineCode, "1#换向器_位置3") == "1" ? "#ee4036" : "#ffffff", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("1#换向器_位置4", commonDAO.GetSignalDataValue(machineCode, "1#换向器_位置4") == "1" ? "#ee4036" : "#ffffff", eHtmlDataItemType.svg_color));

            datas.Add(new HtmlDataItem("3#换向器_位置1", commonDAO.GetSignalDataValue(machineCode, "3#换向器_位置1") == "1" ? "#ee4036" : "#ffffff", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("3#换向器_位置2", commonDAO.GetSignalDataValue(machineCode, "3#换向器_位置2") == "1" ? "#ee4036" : "#ffffff", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("3#换向器_位置3", commonDAO.GetSignalDataValue(machineCode, "3#换向器_位置3") == "1" ? "#ee4036" : "#ffffff", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("3#换向器_位置4", commonDAO.GetSignalDataValue(machineCode, "3#换向器_位置4") == "1" ? "#ee4036" : "#ffffff", eHtmlDataItemType.svg_color));

            datas.Add(new HtmlDataItem("6#换向器_位置1", commonDAO.GetSignalDataValue(machineCode, "6#换向器_位置1") == "1" ? "#ee4036" : "#ffffff", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("6#换向器_位置2", commonDAO.GetSignalDataValue(machineCode, "6#换向器_位置2") == "1" ? "#ee4036" : "#ffffff", eHtmlDataItemType.svg_color));

            datas.Add(new HtmlDataItem("存样柜1换向器_位置1", commonDAO.GetSignalDataValue(machineCode, "存样柜1换向器_位置1") == "1" ? "#ee4036" : "#ffffff", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("存样柜1换向器_位置2", commonDAO.GetSignalDataValue(machineCode, "存样柜1换向器_位置2") == "1" ? "#ee4036" : "#ffffff", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("存样柜1换向器_位置3", commonDAO.GetSignalDataValue(machineCode, "存样柜1换向器_位置3") == "1" ? "#ee4036" : "#ffffff", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("存样柜1换向器_位置4", commonDAO.GetSignalDataValue(machineCode, "存样柜1换向器_位置4") == "1" ? "#ee4036" : "#ffffff", eHtmlDataItemType.svg_color));

            datas.Add(new HtmlDataItem("存样柜2换向器_位置1", commonDAO.GetSignalDataValue(machineCode, "存样柜2换向器_位置1") == "1" ? "#ee4036" : "#ffffff", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("存样柜2换向器_位置2", commonDAO.GetSignalDataValue(machineCode, "存样柜2换向器_位置2") == "1" ? "#ee4036" : "#ffffff", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("存样柜2换向器_位置3", commonDAO.GetSignalDataValue(machineCode, "存样柜2换向器_位置3") == "1" ? "#ee4036" : "#ffffff", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("存样柜2换向器_位置4", commonDAO.GetSignalDataValue(machineCode, "存样柜2换向器_位置4") == "1" ? "#ee4036" : "#ffffff", eHtmlDataItemType.svg_color));

            datas.Add(new HtmlDataItem("1换向器_样瓶检测1", commonDAO.GetSignalDataValue(machineCode, "1#换向器_样瓶检测1") == "1" ? "#ee4036" : "#ffffff", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("1换向器_样瓶检测2", commonDAO.GetSignalDataValue(machineCode, "1#换向器_样瓶检测2") == "1" ? "#ee4036" : "#ffffff", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("1换向器_样瓶检测3", commonDAO.GetSignalDataValue(machineCode, "1#换向器_样瓶检测3") == "1" ? "#ee4036" : "#ffffff", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("1换向器_样瓶检测4", commonDAO.GetSignalDataValue(machineCode, "1#换向器_样瓶检测4") == "1" ? "#ee4036" : "#ffffff", eHtmlDataItemType.svg_color));

            datas.Add(new HtmlDataItem("3换向器_样瓶检测1", commonDAO.GetSignalDataValue(machineCode, "3#换向器_样瓶检测1") == "1" ? "#ee4036" : "#ffffff", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("3换向器_样瓶检测2", commonDAO.GetSignalDataValue(machineCode, "3#换向器_样瓶检测2") == "1" ? "#ee4036" : "#ffffff", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("3换向器_样瓶检测3", commonDAO.GetSignalDataValue(machineCode, "3#换向器_样瓶检测3") == "1" ? "#ee4036" : "#ffffff", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("3换向器_样瓶检测4", commonDAO.GetSignalDataValue(machineCode, "3#换向器_样瓶检测4") == "1" ? "#ee4036" : "#ffffff", eHtmlDataItemType.svg_color));

            datas.Add(new HtmlDataItem("6换向器_样瓶检测1", commonDAO.GetSignalDataValue(machineCode, "6#换向器_样瓶检测1") == "1" ? "#ee4036" : "#ffffff", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("6换向器_样瓶检测2", commonDAO.GetSignalDataValue(machineCode, "6#换向器_样瓶检测2") == "1" ? "#ee4036" : "#ffffff", eHtmlDataItemType.svg_color));

            datas.Add(new HtmlDataItem("存样柜1换向器_样瓶检测1", commonDAO.GetSignalDataValue(machineCode, "存样柜1换向器_样瓶检测1") == "1" ? "#ee4036" : "#ffffff", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("存样柜1换向器_样瓶检测2", commonDAO.GetSignalDataValue(machineCode, "存样柜1换向器_样瓶检测2") == "1" ? "#ee4036" : "#ffffff", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("存样柜1换向器_样瓶检测3", commonDAO.GetSignalDataValue(machineCode, "存样柜1换向器_样瓶检测3") == "1" ? "#ee4036" : "#ffffff", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("存样柜1换向器_样瓶检测4", commonDAO.GetSignalDataValue(machineCode, "存样柜1换向器_样瓶检测4") == "1" ? "#ee4036" : "#ffffff", eHtmlDataItemType.svg_color));

            datas.Add(new HtmlDataItem("存样柜2换向器_样瓶检测1", commonDAO.GetSignalDataValue(machineCode, "存样柜2换向器_样瓶检测1") == "1" ? "#ee4036" : "#ffffff", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("存样柜2换向器_样瓶检测2", commonDAO.GetSignalDataValue(machineCode, "存样柜2换向器_样瓶检测2") == "1" ? "#ee4036" : "#ffffff", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("存样柜2换向器_样瓶检测3", commonDAO.GetSignalDataValue(machineCode, "存样柜2换向器_样瓶检测3") == "1" ? "#ee4036" : "#ffffff", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("存样柜2换向器_样瓶检测4", commonDAO.GetSignalDataValue(machineCode, "存样柜2换向器_样瓶检测4") == "1" ? "#ee4036" : "#ffffff", eHtmlDataItemType.svg_color));

            datas.Add(new HtmlDataItem("1发送站1_样瓶检测", commonDAO.GetSignalDataValue(machineCode, "1#发送站1_样瓶检测") == "1" ? "#ee4036" : "#ffffff", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("1发送站1_管道物料检测", commonDAO.GetSignalDataValue(machineCode, "1#发送站1_管道物料检测") == "1" ? "#ee4036" : "#ffffff", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("1发送站2_样瓶检测", commonDAO.GetSignalDataValue(machineCode, "1#发送站2_样瓶检测") == "1" ? "#ee4036" : "#ffffff", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("1发送站2_管道物料检测", commonDAO.GetSignalDataValue(machineCode, "1#发送站2_管道物料检测") == "1" ? "#ee4036" : "#ffffff", eHtmlDataItemType.svg_color));

            datas.Add(new HtmlDataItem("3发送站1_样瓶检测", commonDAO.GetSignalDataValue(machineCode, "3#发送站1_样瓶检测") == "1" ? "#ee4036" : "#ffffff", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("3发送站1_管道物料检测", commonDAO.GetSignalDataValue(machineCode, "3#发送站1_管道物料检测") == "1" ? "#ee4036" : "#ffffff", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("3发送站2_样瓶检测", commonDAO.GetSignalDataValue(machineCode, "3#发送站2_样瓶检测") == "1" ? "#ee4036" : "#ffffff", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("3发送站2_管道物料检测", commonDAO.GetSignalDataValue(machineCode, "3#发送站2_管道物料检测") == "1" ? "#ee4036" : "#ffffff", eHtmlDataItemType.svg_color));

            datas.Add(new HtmlDataItem("6发送站1_样瓶检测", commonDAO.GetSignalDataValue(machineCode, "6#发送站1_样瓶检测") == "1" ? "#ee4036" : "#ffffff", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("6发送站1_管道物料检测", commonDAO.GetSignalDataValue(machineCode, "6#发送站1_管道物料检测") == "1" ? "#ee4036" : "#ffffff", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("6发送站2_样瓶检测", commonDAO.GetSignalDataValue(machineCode, "6#发送站2_样瓶检测") == "1" ? "#ee4036" : "#ffffff", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("6发送站2_管道物料检测", commonDAO.GetSignalDataValue(machineCode, "6#发送站2_管道物料检测") == "1" ? "#ee4036" : "#ffffff", eHtmlDataItemType.svg_color));
            
            datas.Add(new HtmlDataItem("化验室接收站_样瓶检测", commonDAO.GetSignalDataValue(machineCode, "化验室接收站_样瓶检测") == "1" ? "#ee4036" : "#ffffff", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("人工收发站_管道物料检测", commonDAO.GetSignalDataValue(machineCode, "制样人工收发站_管道物料检测") == "1" ? "#ee4036" : "#ffffff", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("人工收发站_样瓶检测", commonDAO.GetSignalDataValue(machineCode, "制样人工收发站_样瓶检测") == "1" ? "#ee4036" : "#ffffff", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("存样柜1收发站_管道物料检测", commonDAO.GetSignalDataValue(machineCode, "存样柜1收发站_管道物料检测") == "1" ? "#ee4036" : "#ffffff", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("存样柜1收发站_样瓶检测", commonDAO.GetSignalDataValue(machineCode, "存样柜1收发站_样瓶检测") == "1" ? "#ee4036" : "#ffffff", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("存样柜2收发站_管道物料检测", commonDAO.GetSignalDataValue(machineCode, "存样柜2收发站_管道物料检测") == "1" ? "#ee4036" : "#ffffff", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("存样柜2收发站_样瓶检测", commonDAO.GetSignalDataValue(machineCode, "存样柜2收发站_样瓶检测") == "1" ? "#ee4036" : "#ffffff", eHtmlDataItemType.svg_color));
           
            // 发送到页面
            cefWebBrowser.Browser.GetMainFrame().ExecuteJavaScript("requestData(" + Newtonsoft.Json.JsonConvert.SerializeObject(datas) + ");", "", 0);

            //出样信息
            //List<InfCYGControlCMDDetail> listMakerRecord = automakerDAO.GetCYGControlCMDDetailByTime(DateTime.Now);
            //cefWebBrowser.Browser.GetMainFrame().ExecuteJavaScript("LoadSampleInfo(" + Newtonsoft.Json.JsonConvert.SerializeObject(listMakerRecord.Select(a => new { UpdateTime = a.UpdateTime.Year < 2000 ? "" : a.UpdateTime.ToString("yyyy-MM-dd HH:mm"), Code = a.MakeCode, SamType = a. == null ? "" : a.SamType, Status = a.Status == null ? "" : a.Status })) + ");", "", 0);
            #endregion
        }

        private static void GetBoxDatas(CommonDAO commonDAO,string machineCode, List<HtmlDataItem> datas)
        {
            string value = "";
            datas.Add(new HtmlDataItem(machineCode + "_共有仓位", commonDAO.GetSignalDataValue(machineCode, "共有仓位"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem(machineCode + "_已存仓位", commonDAO.GetSignalDataValue(machineCode, "已存仓位"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem(machineCode + "_未存仓位", commonDAO.GetSignalDataValue(machineCode, "未存仓位"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem(machineCode + "_存样率", commonDAO.GetSignalDataValue(machineCode, "存样率"), eHtmlDataItemType.svg_text));

            datas.Add(new HtmlDataItem(machineCode + "_大瓶已存仓位", commonDAO.GetSignalDataValue(machineCode, "大瓶已存仓位"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem(machineCode + "_小瓶已存仓位", commonDAO.GetSignalDataValue(machineCode, "小瓶已存仓位"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem(machineCode + "_大瓶仓位", commonDAO.GetSignalDataValue(machineCode, "大瓶仓位"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem(machineCode + "_小瓶仓位", commonDAO.GetSignalDataValue(machineCode, "小瓶仓位"), eHtmlDataItemType.svg_text));

            string sql2 = @"select (b.configvalue) from cmcstbappletconfig b where b.configname = '存样柜超期天数'";
            DataTable dt1 = commonDAO.SelfDber.ExecuteDataTable(sql2);
            int configvalue = 90;
            if (dt1 != null && dt1.Rows.Count > 0)
            {
                configvalue = Convert.ToInt32(dt1.Rows[0][0].ToString());
            }


            string sql = string.Format(@"select
                        (select count(0) from inftbcygsam d where d.machinecode = '#1智能存样柜' and d.areanumber = 1 and d.isnew = 1) a1,
                        (select count(0) from inftbcygsam d where d.machinecode = '#1智能存样柜' and d.areanumber = 2 and d.isnew = 1) a2,
                        (select count(0) from inftbcygsam d where d.machinecode = '#1智能存样柜' and d.areanumber = 1 and d.isnew = 1 and d.updatetime + {0} < sysdate) a3,
                        (select count(0) from inftbcygsam d where d.machinecode = '#1智能存样柜' and d.areanumber = 2 and d.isnew = 1 and d.updatetime + {0} < sysdate) a4,
                        (select count(0) from inftbcygsam d where d.machinecode = '#2智能存样柜' and d.areanumber = 1 and d.isnew = 1) b1,
                        (select count(0) from inftbcygsam d where d.machinecode = '#2智能存样柜' and d.areanumber = 2 and d.isnew = 1) b2 ,
                        (select count(0) from inftbcygsam d where d.machinecode = '#2智能存样柜' and d.areanumber = 1 and d.isnew = 1 and d.updatetime + {0} < sysdate) b3,
                        (select count(0) from inftbcygsam d where d.machinecode = '#2智能存样柜' and d.areanumber = 2 and d.isnew = 1 and d.updatetime + {0} < sysdate) b4
                        from dual ", configvalue);

            DataTable dt = commonDAO.GetSqlDatas(sql);
            if (dt != null && dt.Rows.Count > 0)
            {
                datas.Add(new HtmlDataItem(machineCode + "_a1", "已存：" + dt.Rows[0]["a1"].ToString() + "  超期：" + dt.Rows[0]["a3"].ToString() + "  仓位：520", eHtmlDataItemType.svg_text));
                datas.Add(new HtmlDataItem(machineCode + "_a2", "已存：" + dt.Rows[0]["a2"].ToString() + "  超期：" + dt.Rows[0]["a4"].ToString() + "  仓位：520", eHtmlDataItemType.svg_text));
                datas.Add(new HtmlDataItem(machineCode + "_b1", "已存：" + dt.Rows[0]["b1"].ToString() + "  超期：" + dt.Rows[0]["b3"].ToString() + "  仓位：520", eHtmlDataItemType.svg_text));
                datas.Add(new HtmlDataItem(machineCode + "_b2", "已存：" + dt.Rows[0]["b2"].ToString() + "  超期：" + dt.Rows[0]["b4"].ToString() + "  仓位：520", eHtmlDataItemType.svg_text));

                datas.Add(new HtmlDataItem("div1-1", value, dt.Rows[0]["a1"].ToString(), "ShowDiv", eHtmlDataItemType.key_value));
                datas.Add(new HtmlDataItem("div1-2", value, dt.Rows[0]["a2"].ToString(), "ShowDiv", eHtmlDataItemType.key_value));
                datas.Add(new HtmlDataItem("div1-3", value, dt.Rows[0]["b1"].ToString(), "ShowDiv", eHtmlDataItemType.key_value));
                datas.Add(new HtmlDataItem("div1-4", value, dt.Rows[0]["b2"].ToString(), "ShowDiv", eHtmlDataItemType.key_value));
            }

//            sql = string.Format(@"select t.endtime,t.makecode,t.sampletype,t.result  from inftbqdcsrecord t 
//                    where to_char(t.endtime,'yyyy-mm-dd' ) = to_char(sysdate,'yyyy-mm-dd')   
//                    order by endtime desc   ");

//            dt = commonDAO.GetSqlDatas(sql);
            //string str = "";
            //if (dt != null && dt.Rows.Count > 0)
            //{
            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        str += dr["endtime"].ToString() + "," + dr["makecode"].ToString() + "," + dr["sampletype"].ToString() + "," + dr["result"].ToString() + "|";
            //    }
            //}

            //datas.Add(new HtmlDataItem("传输记录", str, eHtmlDataItemType.svg_text));

            value = commonDAO.GetSignalDataValue(machineCode, eSignalDataName.程序状态.ToString());
            if ("|就绪待机|自动|".Contains("|" + value + "|"))
                datas.Add(new HtmlDataItem(machineCode + "_系统", "#00c000", eHtmlDataItemType.svg_color));
            else if ("|正在运行|正在卸样|手动|".Contains("|" + value + "|"))
                datas.Add(new HtmlDataItem(machineCode + "_系统", "#ff0000", eHtmlDataItemType.svg_color));
            else if ("|发生故障|".Contains("|" + value + "|"))
                datas.Add(new HtmlDataItem(machineCode + "_系统", "#ffff00", eHtmlDataItemType.svg_color));
            else
                datas.Add(new HtmlDataItem(machineCode + "_系统", "#c0c0c0", eHtmlDataItemType.svg_color));
        }

        String nullif(String st)
        {
            if (String.IsNullOrEmpty(st))
                return null;
            else
                return st;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // 界面不可见时，停止发送数据
            if (!this.Visible) return;

            RequestData();
        }


        /// <summary>
        /// 刷新页面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefreshPage_Click(object sender, EventArgs e)
        {
            cefWebBrowser.Browser.Reload();
        }

        /// <summary>
        /// 刷新数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefreshData_Click(object sender, EventArgs e)
        {
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
            cefWebBrowser.Browser.GetMainFrame().ExecuteJavaScript("test();", "", 0);
        }
    }
}
