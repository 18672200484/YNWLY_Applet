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

            //RequestData();
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

            datas.Add(new HtmlDataItem("#�볧��_��բ1", ConvertBooleanToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QC_Queue_1, "��բ1����")), eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("#�볧��_��բ2", ConvertBooleanToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QC_Queue_1, "��բ2����")), eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("#�볧��_����", "�볡���ţ�" + commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QC_Queue_1, eSignalDataName.��ǰ����.ToString()), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#�볧��_����2", "�볡���ţ�" + commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QC_Queue_1, eSignalDataName.��ǰ����2.ToString()), eHtmlDataItemType.svg_text));

            datas.Add(new HtmlDataItem("#������_��բ1", ConvertBooleanToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QC_Out_1, "��բ1����")), eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("#������_��բ2", ConvertBooleanToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QC_Out_1, "��բ2����")), eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("#������_����", "�������ţ�" + commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QC_Out_1, eSignalDataName.��ǰ����.ToString()), eHtmlDataItemType.svg_text));

            datas.Add(new HtmlDataItem("#�����볧��_��բ1", ConvertBooleanToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_XSQC_Queue_1, "��բ1����")), eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("#�����볧��_��բ2", ConvertBooleanToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_XSQC_Queue_1, "��բ2����")), eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("#�����볧��_����", "�볡���ţ�" + commonDAO.GetSignalDataValue(GlobalVars.MachineCode_XSQC_Queue_1, eSignalDataName.��ǰ����.ToString()), eHtmlDataItemType.svg_text));

            GetDBZT(GlobalVars.MachineCode_QC_Weighter_1, datas, "1");
            GetDBZT(GlobalVars.MachineCode_QC_Weighter_2, datas, "2");
            GetDBZT(GlobalVars.MachineCode_QC_Weighter_3, datas, "3");
            GetDBZT(GlobalVars.MachineCode_QC_Weighter_4, datas, "4");
            GetDBZT(GlobalVars.MachineCode_QC_Weighter_5, datas, "5");
            GetDBZT(GlobalVars.MachineCode_QC_Weighter_6, datas, "6");
            GetDBZT(GlobalVars.MachineCode_QC_Weighter_7, datas, "7");
            GetDBZT(GlobalVars.MachineCode_QC_Weighter_8, datas, "8");

            datas.Add(new HtmlDataItem("#1������е������", ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QCJXCYJ_1, eSignalDataName.�豸״̬.ToString())), eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("#1������е����������", "�������ţ�" + commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QCJXCYJ_1, "���ƺ�"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#1������е�������Ѳɳ���","�Ѳɳ���:" + commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QCJXCYJ_1, "�Ѳɳ���"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#1������е������_��բ1", ConvertBooleanToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QCJXCYJ_1, "��բ1����")), eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("#1������е������_��բ2", ConvertBooleanToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QCJXCYJ_1, "��բ2����")), eHtmlDataItemType.svg_color));

            datas.Add(new HtmlDataItem("#2������е������", ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QCJXCYJ_2, eSignalDataName.�豸״̬.ToString())), eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("#2������е����������", "�������ţ�" + commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QCJXCYJ_2, "���ƺ�"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#2������е�������Ѳɳ���", "�Ѳɳ�����" + commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QCJXCYJ_2, "�Ѳɳ���"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#2������е������_��բ1", ConvertBooleanToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QCJXCYJ_2, "��բ1����")), eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("#2������е������_��բ2", ConvertBooleanToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QCJXCYJ_2, "��բ2����")), eHtmlDataItemType.svg_color));
                 
            datas.Add(new HtmlDataItem("#1������", ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_Recognition_1, eSignalDataName.ϵͳ.ToString())), eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("#1�������ѷ���", "�ѷ�����" + commonDAO.GetSignalDataValue(GlobalVars.MachineCode_Recognition_1, "�����ѹ�����"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#1��������ǰ����", "�������ţ�" + commonDAO.GetSignalDataValue(GlobalVars.MachineCode_Recognition_1, "��ǰ����"), eHtmlDataItemType.svg_text));

            datas.Add(new HtmlDataItem("#2������", ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_TrunOver_2, eSignalDataName.ϵͳ.ToString())), eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("#2�������ѷ���", "�ѷ�����" + commonDAO.GetSignalDataValue(GlobalVars.MachineCode_Recognition_2, "�����ѹ�����"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#2��������ǰ����", "�������ţ�" + commonDAO.GetSignalDataValue(GlobalVars.MachineCode_Recognition_2, "��ǰ����"), eHtmlDataItemType.svg_text));

            datas.Add(new HtmlDataItem("#1Ƥ��������", ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_InPDCYJ_1, eSignalDataName.�豸״̬.ToString())), eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("#1Ƥ������������", "#1�������ţ�" + commonDAO.GetSignalDataValue(GlobalVars.MachineCode_InPDCYJ_1, "���ƺ�"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#1Ƥ���������Ѳɳ���", "�Ѳɳ�����" + commonDAO.GetSignalDataValue(GlobalVars.MachineCode_InPDCYJ_1, "�Ѳɳ���"), eHtmlDataItemType.svg_text));

            datas.Add(new HtmlDataItem("#2Ƥ��������", ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_InPDCYJ_2, eSignalDataName.�豸״̬.ToString())), eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("#2Ƥ������������", "#2�������ţ�" + commonDAO.GetSignalDataValue(GlobalVars.MachineCode_InPDCYJ_2, "���ƺ�"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#2Ƥ���������Ѳɳ���", "�Ѳɳ�����" + commonDAO.GetSignalDataValue(GlobalVars.MachineCode_InPDCYJ_2, "�Ѳɳ���"), eHtmlDataItemType.svg_text));

            datas.Add(new HtmlDataItem("#3Ƥ��������", ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_OutPDCYJ_1, eSignalDataName.�豸״̬.ToString())), eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("#4Ƥ��������", ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_OutPDCYJ_1, eSignalDataName.�豸״̬.ToString())), eHtmlDataItemType.svg_color));

            datas.Add(new HtmlDataItem("dljimg1", ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_DouLunJi_1, eSignalDataName.�豸״̬.ToString())), eHtmlDataItemType.svg_State));
            datas.Add(new HtmlDataItem("dljimg2", ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_DouLunJi_2, eSignalDataName.�豸״̬.ToString())), eHtmlDataItemType.svg_State));
            datas.Add(new HtmlDataItem("dlj1", "" + commonDAO.GetSignalDataValue(GlobalVars.MachineCode_DouLunJi_1, "������λ��"), eHtmlDataItemType.svg_Place));
            datas.Add(new HtmlDataItem("dlj2", "" + commonDAO.GetSignalDataValue(GlobalVars.MachineCode_DouLunJi_2, "������λ��"), eHtmlDataItemType.svg_Place));

            datas.Add(new HtmlDataItem("#1ȫ�Զ�������", ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QZDZYJ_1, eSignalDataName.�豸״̬.ToString())), eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("#1ȫ�Զ�������������", "�����룺" + commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QZDZYJ_1, "��������"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#1ȫ�Զ���������������", "����������" + commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QZDZYJ_1, "��������"), eHtmlDataItemType.svg_text));

            datas.Add(new HtmlDataItem("#2ȫ�Զ�������", ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QZDZYJ_2, eSignalDataName.�豸״̬.ToString())), eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("#2ȫ�Զ�������������", "�����룺" + commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QZDZYJ_2, "��������"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#2ȫ�Զ���������������", "����������" + commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QZDZYJ_2, "��������"), eHtmlDataItemType.svg_text));

            datas.Add(new HtmlDataItem("#3ȫ�Զ�������", ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QZDZYJ_3, eSignalDataName.�豸״̬.ToString())), eHtmlDataItemType.svg_color));

            datas.Add(new HtmlDataItem("������", ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_CYG1, eSignalDataName.����״̬.ToString())), eHtmlDataItemType.svg_color));
           
            //datas.Add(new HtmlDataItem("#1Ƥ����", commonDAO.GetSignalDataValueDouble("#1Ƥ����", eSignalDataName.˲ʱ����.ToString()) > 0 ? ColorTranslator.ToHtml(EquipmentStatusColors.Working) : ColorTranslator.ToHtml(EquipmentStatusColors.BeReady), eHtmlDataItemType.svg_color));
            //datas.Add(new HtmlDataItem("#2Ƥ����", commonDAO.GetSignalDataValueDouble("#2Ƥ����", eSignalDataName.˲ʱ����.ToString()) > 0 ? ColorTranslator.ToHtml(EquipmentStatusColors.Working) : ColorTranslator.ToHtml(EquipmentStatusColors.BeReady), eHtmlDataItemType.svg_color));
            
            //��ͨƤ����
            datas.Add(new HtmlDataItem("#1Ƥ����ֵ", setPotValue("#1Ƥ����", eSignalDataName.˲ʱ����.ToString()), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#1Ƥ����ֵ�ۼ�", setPotValue("#1Ƥ����", eSignalDataName.�ۼ�����.ToString()), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#2Ƥ����ֵ", setPotValue("#2Ƥ����", eSignalDataName.˲ʱ����.ToString()), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#2Ƥ����ֵ�ۼ�", setPotValue("#2Ƥ����", eSignalDataName.�ۼ�����.ToString()), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#3Ƥ����ֵ", setPotValue("#3Ƥ����", eSignalDataName.˲ʱ����.ToString()), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#3Ƥ����ֵ�ۼ�", setPotValue("#3Ƥ����", eSignalDataName.�ۼ�����.ToString()), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#4Ƥ����ֵ", setPotValue("#4Ƥ����", eSignalDataName.˲ʱ����.ToString()), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#4Ƥ����ֵ�ۼ�", setPotValue("#4Ƥ����", eSignalDataName.�ۼ�����.ToString()), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#5Ƥ����ֵ", setPotValue("#5Ƥ����", eSignalDataName.˲ʱ����.ToString()), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#5Ƥ����ֵ�ۼ�", setPotValue("#5Ƥ����", eSignalDataName.�ۼ�����.ToString()), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#6Ƥ����ֵ", setPotValue("#6Ƥ����", eSignalDataName.˲ʱ����.ToString()), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#6Ƥ����ֵ�ۼ�", setPotValue("#6Ƥ����", eSignalDataName.�ۼ�����.ToString()), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#7Ƥ����ֵ", setPotValue("#7Ƥ����", eSignalDataName.˲ʱ����.ToString()), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#7Ƥ����ֵ�ۼ�", setPotValue("#7Ƥ����", eSignalDataName.�ۼ�����.ToString()), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#8Ƥ����ֵ", setPotValue("#8Ƥ����", eSignalDataName.˲ʱ����.ToString()), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#8Ƥ����ֵ�ۼ�", setPotValue("#8Ƥ����", eSignalDataName.�ۼ�����.ToString()), eHtmlDataItemType.svg_text));

            //�߾���Ƥ����
            datas.Add(new HtmlDataItem("#5�߾���Ƥ����", setPotValue("#5�߾���Ƥ����", eSignalDataName.˲ʱ����.ToString()), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#5�߾���Ƥ�����ۼ�", setPotValue("#5�߾���Ƥ����", eSignalDataName.�ۼ�����.ToString()), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#6a�߾���Ƥ����", setPotValue("#6a�߾���Ƥ����", eSignalDataName.˲ʱ����.ToString()), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#6a�߾���Ƥ�����ۼ�", setPotValue("#6a�߾���Ƥ����", eSignalDataName.�ۼ�����.ToString()), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#6b�߾���Ƥ����", setPotValue("#6b�߾���Ƥ����", eSignalDataName.˲ʱ����.ToString()), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#6b�߾���Ƥ�����ۼ�", setPotValue("#6b�߾���Ƥ����", eSignalDataName.�ۼ�����.ToString()), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#7a�߾���Ƥ����", setPotValue("#7a�߾���Ƥ����", eSignalDataName.˲ʱ����.ToString()), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#7a�߾���Ƥ�����ۼ�", setPotValue("#7a�߾���Ƥ����", eSignalDataName.�ۼ�����.ToString()), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#7b�߾���Ƥ����", setPotValue("#7b�߾���Ƥ����", eSignalDataName.˲ʱ����.ToString()), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#7b�߾���Ƥ�����ۼ�", setPotValue("#7b�߾���Ƥ����", eSignalDataName.�ۼ�����.ToString()), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#8�߾���Ƥ����", setPotValue("#8�߾���Ƥ����", eSignalDataName.˲ʱ����.ToString()), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#8�߾���Ƥ�����ۼ�", setPotValue("#8�߾���Ƥ����", eSignalDataName.�ۼ�����.ToString()), eHtmlDataItemType.svg_text));

            datas.Add(new HtmlDataItem("��������", boxTimeOut(), eHtmlDataItemType.svg_visible));

            //��Ʒ��
            datas.Add(new HtmlDataItem("#1��Ʒ����λ", "#1��Ʒ����λ��" + commonDAO.GetSignalDataValue(GlobalVars.Poduct_Pot_1, "��λ"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#2��Ʒ����λ", "#2��Ʒ����λ��" + commonDAO.GetSignalDataValue(GlobalVars.Poduct_Pot_1, "��λ"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#3��Ʒ����λ", "#3��Ʒ����λ��" + commonDAO.GetSignalDataValue(GlobalVars.Poduct_Pot_1, "��λ"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#1A��Ʒ�ֳ���", "#1A��Ʒ�ֳ��ţ�" + commonDAO.GetSignalDataValue(GlobalVars.Poduct_Pot_1, "��ǰ����1"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#1B��Ʒ�ֳ���", "#1B��Ʒ�ֳ��ţ�" + commonDAO.GetSignalDataValue(GlobalVars.Poduct_Pot_1, "��ǰ����2"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#2A��Ʒ�ֳ���", "#2A��Ʒ�ֳ��ţ�" + commonDAO.GetSignalDataValue(GlobalVars.Poduct_Pot_2, "��ǰ����1"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#2B��Ʒ�ֳ���", "#2B��Ʒ�ֳ��ţ�" + commonDAO.GetSignalDataValue(GlobalVars.Poduct_Pot_2, "��ǰ����2"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#3A��Ʒ�ֳ���", "#3A��Ʒ�ֳ��ţ�" + commonDAO.GetSignalDataValue(GlobalVars.Poduct_Pot_3, "��ǰ����1"), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#3B��Ʒ�ֳ���", "#3B��Ʒ�ֳ��ţ�" + commonDAO.GetSignalDataValue(GlobalVars.Poduct_Pot_3, "��ǰ����2"), eHtmlDataItemType.svg_text));

            //getPoleTemp(datas);


            getStorage(datas);

            //datas.Add(new HtmlDataItem("#1��̬����ս���������", string.Format("����:{0}��  ����:{1}��", commonDAO.GetSignalDataValue(GlobalVars.MachineCode_GDH_1, "���ս�������"), commonDAO.GetSignalDataValue(GlobalVars.MachineCode_GDH_1, "���ճ�������")), eHtmlDataItemType.svg_text));
            //string XMJS_QD_Color = ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_QD, eSignalDataName.����״̬.ToString()));
            //datas.Add(new HtmlDataItem("#1��������_����վ1", XMJS_QD_Color, eHtmlDataItemType.svg_color));
            //datas.Add(new HtmlDataItem("#1��������_����վ2", XMJS_QD_Color, eHtmlDataItemType.svg_color));
            //datas.Add(new HtmlDataItem("#1��������_����վ3", XMJS_QD_Color, eHtmlDataItemType.svg_color));
            //datas.Add(new HtmlDataItem("#1�𳵻�е������", ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HCJXCYJ_1, eSignalDataName.����״̬.ToString())), eHtmlDataItemType.svg_color));
            //datas.Add(new HtmlDataItem("#2�𳵻�е������", ConvertMachineStatusToColor(commonDAO.GetSignalDataValue(GlobalVars.MachineCode_HCJXCYJ_2, eSignalDataName.����״̬.ToString())), eHtmlDataItemType.svg_color));

            // ��Ӹ���...

            //GC.Collect();
            // ���͵�ҳ��
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
                    #region ����ú����ʼ��
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
                                        old.EndPoint = EndPoint;//�����ģ���ֱ���滻������  0-120
                                    }
                                    else if (old.StartPoint <= StartPoint && old.EndPoint >= EndPoint)
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        //�����������ҵ�ǰ��ʼ�������һ�������� 400-450
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
                        datas.Add(new HtmlDataItem("ú��ú��_" + i, pt.StartPoint
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
                    datas.Add(new HtmlDataItem("ú���¶Ȳ�����_" + i, dr["polecode"].ToString()
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
            string sql2 = @"select (b.configvalue) from cmcstbappletconfig b where b.configname = '������������'";

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
            string zl = commonDAO.GetSignalDataValue(MachineCode, eSignalDataName.�ذ��Ǳ�_ʵʱ����.ToString());
            string cxzt = commonDAO.GetSignalDataValue(MachineCode, eSignalDataName.����״̬.ToString());
            string sbzt = commonDAO.GetSignalDataValue(MachineCode, eSignalDataName.�豸״̬.ToString());

            if (cxzt == "1" && sbzt == "1")
            {
                if (Convert.ToDecimal(zl) > 0)
                {
                    datas.Add(new HtmlDataItem("#" + type + "��", ColorTranslator.ToHtml(EquipmentStatusColors.BeReady), eHtmlDataItemType.svg_color));
                }
                else
                {
                    datas.Add(new HtmlDataItem("#" + type + "��", ColorTranslator.ToHtml(EquipmentStatusColors.Working), eHtmlDataItemType.svg_color));
                }
            }
            else if (sbzt == "0")
            {
                datas.Add(new HtmlDataItem("#" + type + "��", ColorTranslator.ToHtml(EquipmentStatusColors.Breakdown), eHtmlDataItemType.svg_color));
            }
            else
            {
                datas.Add(new HtmlDataItem("#" + type + "��", ColorTranslator.ToHtml(EquipmentStatusColors.Forbidden), eHtmlDataItemType.svg_color));
            }

            datas.Add(new HtmlDataItem("#" + type + "��_����", "#" + type + "���������ţ�" + commonDAO.GetSignalDataValue(MachineCode, eSignalDataName.��ǰ����.ToString()), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#" + type + "��_����", "#" + type + "������������" + getZl(zl), eHtmlDataItemType.svg_text));
            datas.Add(new HtmlDataItem("#" + type + "��_��բ1", ConvertBooleanToColor(commonDAO.GetSignalDataValue(MachineCode, "��բ1����")), eHtmlDataItemType.svg_color));
            datas.Add(new HtmlDataItem("#" + type + "��_��բ2", ConvertBooleanToColor(commonDAO.GetSignalDataValue(MachineCode, "��բ2����")), eHtmlDataItemType.svg_color));

        }

        private string getZl(string str)
        {
            if (string.IsNullOrEmpty(str) || str == "0")
            {
                return "";
            }
            else
            {
                return str + "��";
            }
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
            if ("|��������|ϵͳ����|�ȴ�����|".Contains("|" + systemStatus + "|"))
                return ColorTranslator.ToHtml(EquipmentStatusColors.BeReady);//��ɫ
            else if ("|��������|����ж��|ϵͳ����|".Contains("|" + systemStatus + "|"))
                return ColorTranslator.ToHtml(EquipmentStatusColors.Working);//��ɫ
            else if ("|��������|ϵͳ����|".Contains("|" + systemStatus + "|"))
                return ColorTranslator.ToHtml(EquipmentStatusColors.Breakdown);//��ɫ
            else
                return ColorTranslator.ToHtml(EquipmentStatusColors.Forbidden);//
        }

        /// <summary>
        /// ת����������״̬Ϊ��ɫֵ
        /// </summary>
        /// <param name="status">״̬</param>
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
        /// ת����������״̬Ϊ��ɫֵ
        /// </summary>
        /// <param name="status">״̬</param>
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