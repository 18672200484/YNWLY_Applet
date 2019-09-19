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
        /// ����Ψһ��ʶ��
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
        /// �����ʼ��
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

            datas.Add(new HtmlDataItem("�������_�źŵ�", ConvertBooleanToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QC_Order_1, eSignalDataName.ϵͳ.ToString())), eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("IO������_�źŵ�", ConvertBooleanToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QC_Order_1, eSignalDataName.IO������_����״̬.ToString())), eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("LED��1_�źŵ�", ConvertBooleanToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QC_Order_1, eSignalDataName.LED��1_����״̬.ToString())), eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("LED��2_�źŵ�", ConvertBooleanToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QC_Order_1, eSignalDataName.LED��2_����״̬.ToString())), eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("������1_�źŵ�", ConvertBooleanToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QC_Order_1, eSignalDataName.����ʶ��1_����״̬.ToString())), eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("������2_�źŵ�", ConvertBooleanToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QC_Order_1, eSignalDataName.����ʶ��2_����״̬.ToString())), eHtmlDataItemType.svg_color));

            // �����źš���ǰ��Id"��ѯ������Ϣ
            string currentAutotruckId = commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QC_Order_1, eSignalDataName.��ǰ��Id.ToString());
            string currentTransportId = commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QC_Order_1, eSignalDataName.��ǰ�����¼Id.ToString());
            decimal gross = 0, tare = 0;
            if (!string.IsNullOrEmpty(currentAutotruckId) && !string.IsNullOrEmpty(currentTransportId))
            {
                CmcsAutotruck autotruck = commonDAO.SelfDber.Get<CmcsAutotruck>(currentAutotruckId);
                if (autotruck != null)
                {
                    if (autotruck.CarType == eCarType.�볡ú.ToString())
                    {
                        CmcsBuyFuelTransport transport = commonDAO.SelfDber.Get<CmcsBuyFuelTransport>(currentTransportId);
                        if (transport != null)
                        {
                            gross = transport.GrossWeight;
                            tare = transport.TareWeight;
                        }
                    }
                    if (autotruck.CarType == eCarType.�볡ú.ToString())
                    {
                        CmcsSaleFuelTransport transport = commonDAO.SelfDber.Get<CmcsSaleFuelTransport>(currentTransportId);
                        if (transport != null)
                        {
                            gross = transport.GrossWeight;
                            tare = transport.TareWeight;
                        }
                    }
                    if (autotruck.CarType == eCarType.��������.ToString())
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
            datas.Add(new HtmlDataItem("����", (!string.IsNullOrEmpty(currentAutotruckId)).ToString(), eHtmlDataItemType.svg_visible));
            datas.Add(new HtmlDataItem("��ǰ���⳵��", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QC_Order_1, eSignalDataName.��ǰ����.ToString()), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("Ƥ��", tare.ToString() + " ��", eHtmlDataItemType.svg_text));

            datas.Add(new HtmlDataItem("�ظ�1�ź�", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QC_Order_1, eSignalDataName.�ظ�1�ź�.ToString()).ToLower() == "1" ? ColorTranslator.ToHtml(EquipmentStatusColors.Working) : "#c0c0c0", eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("�ظ�2�ź�", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QC_Order_1, eSignalDataName.�ظ�2�ź�.ToString()).ToLower() == "1" ? ColorTranslator.ToHtml(EquipmentStatusColors.Working) : "#c0c0c0", eHtmlDataItemType.svg_color));

            datas.Add(new HtmlDataItem("��բ1����", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QC_Order_1, eSignalDataName.��բ1����.ToString()), eHtmlDataItemType.svg_visible));
            datas.Add(new HtmlDataItem("��բ2����", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QC_Order_1, eSignalDataName.��բ2����.ToString()), eHtmlDataItemType.svg_visible));

            datas.Add(new HtmlDataItem("����", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QC_Order_1, eSignalDataName.�ϰ�����.ToString()), eHtmlDataItemType.svg_scare));
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
            else if ("|��������|".Contains("|" + systemStatus + "|"))
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
}