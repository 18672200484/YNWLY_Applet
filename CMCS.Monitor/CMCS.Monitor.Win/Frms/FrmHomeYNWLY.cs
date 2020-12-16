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
        /// ����Ψһ��ʶ��
        /// </summary>
        public static string UniqueKey = "FrmHomeYNWLY";

        CommonDAO commonDAO = CommonDAO.GetInstance();

        CefWebBrowserEx cefWebBrowser = new CefWebBrowserEx();

        public FrmHomeYNWLY()
        {
            InitializeComponent();
        }

        /// <summary>
        /// �����ʼ��
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
        /// ���� - ˢ��ҳ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRefresh_Click(object sender, EventArgs e)
        {
            cefWebBrowser.Browser.Reload();
        }

        /// <summary>
        /// ���� - ����ˢ��
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnRequestData_Click(object sender, EventArgs e)
        {
            RequestData();
        }

        /// <summary>
        /// ��������
        /// </summary>
        void RequestData()
        {
            string value = string.Empty, machineCode = string.Empty;
            List<HtmlDataItem> datas = new List<HtmlDataItem>();

            datas.Clear();
            datas.Add(new HtmlDataItem("#1������", ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_TrunOver_1, eSignalDataName.ϵͳ.ToString())), eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("#2������", ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_TrunOver_2, eSignalDataName.ϵͳ.ToString())), eHtmlDataItemType.svg_color));

            datas.Add(new HtmlDataItem("#1Ƥ��������", ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_InPDCYJ_1, eSignalDataName.ϵͳ.ToString())), eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("#2Ƥ��������", ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_InPDCYJ_2, eSignalDataName.ϵͳ.ToString())), eHtmlDataItemType.svg_color));

            datas.Add(new HtmlDataItem("�볧ú�س�#1", ConvertBooleanToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QC_Weighter_1, eSignalDataName.ϵͳ.ToString())), eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("�볧ú�س�#2", ConvertBooleanToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QC_Weighter_2, eSignalDataName.ϵͳ.ToString())), eHtmlDataItemType.svg_color));

            datas.Add(new HtmlDataItem("�볧ú�ᳵ#1", ConvertBooleanToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QC_Weighter_3, eSignalDataName.ϵͳ.ToString())), eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("�볧ú�ᳵ#2", ConvertBooleanToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QC_Weighter_4, eSignalDataName.ϵͳ.ToString())), eHtmlDataItemType.svg_color));

            datas.Add(new HtmlDataItem("#1������е������", ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QC_JxSampler_1, eSignalDataName.ϵͳ.ToString())), eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("#2������е������", ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QC_JxSampler_2, eSignalDataName.ϵͳ.ToString())), eHtmlDataItemType.svg_color));

            datas.Add(new HtmlDataItem("#1ȫ�Զ�������", ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QZDZYJ_1, eSignalDataName.ϵͳ.ToString())), eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("������", ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_CYG1, eSignalDataName.ϵͳ.ToString())), eHtmlDataItemType.svg_color));

            datas.Add(new HtmlDataItem("#1���ֻ�", ConvertMachineStatusToColor(commonDAO.GetSignalDataValue("#1���ֻ�", eSignalDataName.ϵͳ.ToString())), eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("#2���ֻ�", ConvertMachineStatusToColor(commonDAO.GetSignalDataValue("#2���ֻ�", eSignalDataName.ϵͳ.ToString())), eHtmlDataItemType.svg_color));

            datas.Add(new HtmlDataItem("#1Ƥ����", commonDAO.GetSignalDataValueDouble("#1Ƥ����", eSignalDataName.˲ʱ����.ToString()) > 0 ? ColorTranslator.ToHtml(EquipmentStatusColors.Working) : ColorTranslator.ToHtml(EquipmentStatusColors.BeReady), eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("#2Ƥ����", commonDAO.GetSignalDataValueDouble("#2Ƥ����", eSignalDataName.˲ʱ����.ToString()) > 0 ? ColorTranslator.ToHtml(EquipmentStatusColors.Working) : ColorTranslator.ToHtml(EquipmentStatusColors.BeReady), eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("#1Ƥ����ֵ", commonDAO.GetSignalDataValue("#1Ƥ����", eSignalDataName.˲ʱ����.ToString()) + "t", eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#2Ƥ����ֵ", commonDAO.GetSignalDataValue("#2Ƥ����", eSignalDataName.˲ʱ����.ToString()) + "t", eHtmlDataItemType.svg_text));




            datas.Add(new HtmlDataItem("�볧ú�س�#2_����", commonDAO.GetSignalDataValue("#2�س���", eSignalDataName.��ǰ����.ToString()), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("�볧ú�س�#1_����", commonDAO.GetSignalDataValue("#1�س���", eSignalDataName.��ǰ����.ToString()), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("�볧ú�ᳵ#2_����", commonDAO.GetSignalDataValue("#2�ᳵ��", eSignalDataName.��ǰ����.ToString()), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("�볧ú�ᳵ#1_����", commonDAO.GetSignalDataValue("#1�ᳵ��", eSignalDataName.��ǰ����.ToString()), eHtmlDataItemType.svg_text));

            datas.Add(new HtmlDataItem("�볧ú�س�#2_����", commonDAO.GetSignalDataValue("#2�س���", eSignalDataName.�ذ��Ǳ�_ʵʱ����.ToString()) + "��", eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("�볧ú�س�#1_����", commonDAO.GetSignalDataValue("#1�س���", eSignalDataName.�ذ��Ǳ�_ʵʱ����.ToString()) + "��", eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("�볧ú�ᳵ#2_����", commonDAO.GetSignalDataValue("#2�ᳵ��", eSignalDataName.�ذ��Ǳ�_ʵʱ����.ToString()) + "��", eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("�볧ú�ᳵ#1_����", commonDAO.GetSignalDataValue("#1�ᳵ��", eSignalDataName.�ذ��Ǳ�_ʵʱ����.ToString()) + "��", eHtmlDataItemType.svg_text));



            //datas.Add(new HtmlDataItem("#1��̬����ս���������", string.Format("����:{0}��  ����:{1}��", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_GDH_1, "���ս�������"), commonDAO.GetSignalDataValue(GlobalVars.MachineCode_GDH_1, "���ճ�������")), eHtmlDataItemType.svg_text));
            //string XMJS_QD_Color = ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QD, eSignalDataName.ϵͳ.ToString()));
            //datas.Add(new HtmlDataItem("#1��������_����վ1", XMJS_QD_Color, eHtmlDataItemType.svg_color));
            //datas.Add(new HtmlDataItem("#1��������_����վ2", XMJS_QD_Color, eHtmlDataItemType.svg_color));
            //datas.Add(new HtmlDataItem("#1��������_����վ3", XMJS_QD_Color, eHtmlDataItemType.svg_color));
            //datas.Add(new HtmlDataItem("#1�𳵻�е������", ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HCJXCYJ_1, eSignalDataName.ϵͳ.ToString())), eHtmlDataItemType.svg_color));
            //datas.Add(new HtmlDataItem("#2�𳵻�е������", ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HCJXCYJ_2, eSignalDataName.ϵͳ.ToString())), eHtmlDataItemType.svg_color));

            // ��Ӹ���...

            // ���͵�ҳ��
            cefWebBrowser.Browser.GetMainFrame().ExecuteJavaScript("requestData(" + Newtonsoft.Json.JsonConvert.SerializeObject(datas) + ");", "", 0);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            // ���治�ɼ�ʱ��ֹͣ��������
            if (!this.Visible) return;

            RequestData();
        }

        /// <summary>
        /// ת���豸ϵͳ״̬Ϊ��ɫֵ
        /// </summary>
        /// <param name="systemStatus">ϵͳ״̬</param>
        /// <returns></returns>
        private string ConvertMachineStatusToColor(string systemStatus)
        {
            if ("|��������|".Contains("|" + systemStatus + "|"))
                return ColorTranslator.ToHtml(EquipmentStatusColors.BeReady);
            else if ("|��������|����ж��|".Contains("|" + systemStatus + "|"))
                return ColorTranslator.ToHtml(EquipmentStatusColors.Working);
            else if ("|��������|".Contains("|" + systemStatus + "|"))
                return ColorTranslator.ToHtml(EquipmentStatusColors.Breakdown);
            else
                return ColorTranslator.ToHtml(EquipmentStatusColors.Forbidden);
        }

        /// <summary>
        /// ת����������״̬Ϊ��ɫֵ
        /// </summary>
        /// <param name="status">״̬</param>
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