using System;
using System.Linq;
using System.Collections.Generic;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using CMCS.Common;
using CMCS.Common.Entities.CarTransport;
using CMCS.Common.Entities.BaseInfo;
using CMCS.Common.Entities.Fuel;
using CMCS.Common.DAO;
using CMCS.Common.Utilities;
using CMCS.Common.Enums;
using DevComponents.DotNetBar.Controls;
using CMCS.Common.Entities.iEAA;
using DevComponents.DotNetBar.SuperGrid;
using CMCS.WeighCheck.SampleMake.Utilities;
using DevComponents.Editors.DateTimeAdv;
using CMCS.WeighCheck.DAO;
using CMCS.WeighCheck.SampleMake.Core;

namespace CMCS.WeighCheck.SampleMake.Frms.JDYMake
{
    public partial class FrmJDYMake_Oper : DevComponents.DotNetBar.Metro.MetroForm
    {
        String id = String.Empty;
        bool edit = false;
        CmcsRCMake cmcsMake;
        CodePrinterMake _CodePrinter = null;
        private IList<CmcsRCMakeDetail> _cmcsMakeDetail = new List<CmcsRCMakeDetail>();
        CommonDAO commonDAO = CommonDAO.GetInstance();
        IList<CmcsRCMakeDetail> cmcsMakeDetail
        {
            get { return _cmcsMakeDetail; }
            set
            {
                _cmcsMakeDetail = value;
                superGridControl1.PrimaryGrid.DataSource = value;
            }
        }

        public FrmJDYMake_Oper()
        {
            InitializeComponent();
        }
        public FrmJDYMake_Oper(String pId, bool pEdit)
        {
            InitializeComponent();
            id = pId;
            edit = pEdit;
        }

        private void FrmJDYMake_Oper_Load(object sender, EventArgs e)
        {
            InitFrom();
            superGridControl1.PrimaryGrid.AutoGenerateColumns = false;

            txt_GetPle.Text = SelfVars.LoginUser.UserName;
            txt_MakePle.Text = SelfVars.LoginUser.UserName;
            if (!String.IsNullOrEmpty(id))
            {
                this.cmcsMake = Dbers.GetInstance().SelfDber.Get<CmcsRCMake>(this.id);
                if (this.cmcsMake != null)
                {
                    txt_GetPle.Text = cmcsMake.GetPle;
                    txt_MakeCode.Text = cmcsMake.MakeCode;
                    txt_MakePle.Text = cmcsMake.MakePle;
                    txt_SendUnit.Text = cmcsMake.SendUnit;
                    txt_MakePle.Text = cmcsMake.MakePle;
                    dt_UseTime.Value = cmcsMake.UseTime;
                    dt_GetDate.Value = cmcsMake.GetDate;
                    dt_MakeEndTime.Value = cmcsMake.MakeEndTime;
                    dbi_GetBarrelWeight.Value = Convert.ToDouble(cmcsMake.GetBarrelWeight);
                    dt_MakeStartTime.Value = cmcsMake.MakeStartTime;
                    txt_Remark.Text = cmcsMake.Remark;
                    IList<CmcsRCMakeDetail> details = CommonDAO.GetInstance().SelfDber.Entities<CmcsRCMakeDetail>("where MakeId=:MakeId", new { MakeId = this.id });
                    this.cmcsMakeDetail = details;
                }
            }
            if (!edit)
            {
                btnSubmit.Enabled = false;
                btn_CreateMakeDetail.Enabled = false;
                CMCS.WeighCheck.SampleMake.Utilities.Helper.ControlReadOnly(panelEx2);
                CMCS.WeighCheck.SampleMake.Utilities.Helper.ControlReadOnly(superGridControl1);
            }
        }

        #region 设备初始化与卸载
        /// <summary>
        /// 初始化外接设备
        /// </summary>
        private void InitHardware()
        {
            try
            {
                bool success = false;
                if (!SelfVars.RfReadOpen)
                {
                    // 初始化-读卡器
                    success = Hardwarer.ReadRwer.OpenNetPort(commonDAO.GetAppletConfigString("读卡器IP"), commonDAO.GetAppletConfigInt32("读卡器端口"));
                    SelfVars.RfReadOpen = success;
                }
            }
            catch (Exception ex)
            {
                Log4Neter.Error("设备初始化", ex);
            }
        }

        /// <summary>
        /// 卸载设备
        /// </summary>
        private void UnloadHardware()
        {
            // 注意此段代码
            Application.DoEvents();
            //if (!SelfVars.RfReadOpen)
            //    Hardwarer.ReadRwer.CloseNetPort();
        }
        #endregion

        /// <summary>
        /// 初始化
        /// </summary>
        public void InitFrom()
        {
            InitHardware();
            this._CodePrinter = new CodePrinterMake(printDocument1);

            // 打印编码按钮
            GridButtonXEditControl btnPrintCode = superGridControl1.PrimaryGrid.Columns["gclmPrintCode"].EditControl as GridButtonXEditControl;
            btnPrintCode.ColorTable = eButtonColor.BlueWithBackground;
            btnPrintCode.Click += new EventHandler(btnPrintCode_Click);
            // 写入编码按钮
            GridButtonXEditControl btnWriteCode = superGridControl1.PrimaryGrid.Columns["gclmWriteCode"].EditControl as GridButtonXEditControl;
            btnWriteCode.ColorTable = eButtonColor.BlueWithBackground;
            btnWriteCode.Click += new EventHandler(btnWriteCode_Click);
        }

        /// <summary>
        /// 打印编码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnPrintCode_Click(object sender, EventArgs e)
        {
            GridButtonXEditControl btn = sender as GridButtonXEditControl;
            if (btn == null) return;

            CmcsRCMakeDetail rCMakeDetail = btn.EditorCell.GridRow.DataItem as CmcsRCMakeDetail;
            if (rCMakeDetail == null) return;
            if (!string.IsNullOrEmpty(rCMakeDetail.BarrelCode))
                _CodePrinter.Print(rCMakeDetail.BarrelCode, rCMakeDetail.SampleType);
            else
                MessageBoxEx.Show("请先点击[生成]按钮生成样罐编码", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// 写入编码
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void btnWriteCode_Click(object sender, EventArgs e)
        {
            GridButtonXEditControl btn = sender as GridButtonXEditControl;
            if (btn == null) return;

            CmcsRCMakeDetail rCMakeDetail = btn.EditorCell.GridRow.DataItem as CmcsRCMakeDetail;
            if (rCMakeDetail == null) return;
            if (!string.IsNullOrEmpty(rCMakeDetail.BarrelCode) && WriteRf(rCMakeDetail.BarrelCode))
                MessageBoxEx.Show("写入成功！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            else
                MessageBoxEx.Show("写入失败！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        /// <summary>
        /// 写卡
        /// </summary>
        /// <returns></returns>
        private bool WriteRf(string rf)
        {
            byte SecNumber = Convert.ToByte(commonDAO.GetAppletConfigInt32("读卡器扇区"));
            byte BlockNumber = Convert.ToByte(commonDAO.GetAppletConfigInt32("读卡器块区"));

            if (Hardwarer.ReadRwer.WriteData(rf, Convert.ToInt32(SecNumber), Convert.ToInt32(BlockNumber)))
            {
                return Hardwarer.ReadRwer.RWRead14443A(Convert.ToInt32(SecNumber), Convert.ToInt32(BlockNumber)) == rf;
            }
            return false;
        }

        /// <summary>
        /// 验证输入
        /// </summary>
        /// <returns></returns>
        private bool CheckInPut()
        {
            if (txt_SendUnit.Text.Length == 0)
            {
                MessageBoxEx.Show("送样单位不能为空！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (dt_UseTime.Value == DateTime.MinValue)
            {
                MessageBoxEx.Show("请选择实际采样时间！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return false;
            }
            if (string.IsNullOrEmpty(txt_GetPle.Text))
                txt_GetPle.Text = SelfVars.LoginUser.UserName;

            if (string.IsNullOrEmpty(txt_MakeCode.Text))
                txt_MakeCode.Text = CommonDAO.GetInstance().CreateNewMakeCode(dt_UseTime.Value);

            if (string.IsNullOrEmpty(txt_MakePle.Text))
                txt_MakePle.Text = SelfVars.LoginUser.UserName;

            if (string.IsNullOrEmpty(dt_MakeStartTime.Text))
                dt_MakeStartTime.Value = DateTime.Now;

            if (string.IsNullOrEmpty(dt_MakeEndTime.Text))
                dt_MakeEndTime.Value = DateTime.Now;

            if (string.IsNullOrEmpty(dt_GetDate.Text))
                dt_GetDate.Value = DateTime.Now;

            return true;
        }

        private void btnSubmit_Click(object sender, EventArgs e)
        {
            if (!CheckInPut()) return;
            if (this.cmcsMake != null)
            {
                if (!CompareClass.CompareClassValue(this.cmcsMake, Dbers.GetInstance().SelfDber.Get<CmcsRCMake>(this.id)))
                {
                    MessageBoxEx.Show("数据已更改请重新打开页面修改保存！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                this.cmcsMake.CreateUser = SelfVars.LoginUser.UserAccount;
                this.cmcsMake.OperUser = SelfVars.LoginUser.UserAccount;
                this.cmcsMake.GetPle = txt_GetPle.Text;
                if (string.IsNullOrEmpty(txt_MakeCode.Text))
                    txt_MakeCode.Text = CommonDAO.GetInstance().CreateNewMakeCode(dt_UseTime.Value);
                this.cmcsMake.MakeCode = txt_MakeCode.Text;
                this.cmcsMake.MakePle = txt_MakePle.Text;
                this.cmcsMake.SendUnit = txt_SendUnit.Text;
                this.cmcsMake.MakePle = txt_MakePle.Text;
                this.cmcsMake.UseTime = dt_UseTime.Value;
                this.cmcsMake.GetDate = dt_GetDate.Value;
                this.cmcsMake.MakeEndTime = dt_MakeEndTime.Value;
                this.cmcsMake.MakeStartTime = dt_MakeStartTime.Value;
                this.cmcsMake.GetBarrelWeight = Convert.ToDecimal(dbi_GetBarrelWeight.Value);
                this.cmcsMake.IsSynch = "0";
                SaveAndUpdate(cmcsMake, GetDetails());
            }
            else
            {
                this.cmcsMake = new CmcsRCMake();

                this.cmcsMake.MakeType = "监督样制样";
                this.cmcsMake.MakeStyle = eMakeType.人工制样.ToString();
                this.cmcsMake.GetPle = txt_GetPle.Text;

                this.cmcsMake.MakeCode = txt_MakeCode.Text;
                this.cmcsMake.MakePle = txt_MakePle.Text;
                this.cmcsMake.SendUnit = txt_SendUnit.Text;
                this.cmcsMake.MakePle = txt_MakePle.Text;
                this.cmcsMake.UseTime = dt_UseTime.Value;
                this.cmcsMake.GetDate = dt_GetDate.Value;
                this.cmcsMake.MakeEndTime = dt_MakeEndTime.Value;
                this.cmcsMake.MakeStartTime = dt_MakeStartTime.Value;
                this.cmcsMake.GetBarrelWeight = Convert.ToDecimal(dbi_GetBarrelWeight.Value);
                this.cmcsMake.Remark = txt_Remark.Text + " 由人工制样室创建";
                this.cmcsMake.IsSynch = "0";
                if (this.cmcsMakeDetail.Count == 0)
                    btn_CreateMakeDetail_Click(null, null);
                SaveAndUpdate(cmcsMake, GetDetails().Count == 0 ? this.cmcsMakeDetail : GetDetails());
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        /// <summary>
        /// 生成制样明细
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btn_CreateMakeDetail_Click(object sender, EventArgs e)
        {
            if (!CheckInPut()) return;

            IList<CmcsRCMakeDetail> details = new List<CmcsRCMakeDetail>();
            //入厂煤制样明细  
            foreach (CodeContent item in CommonDAO.GetInstance().GetCodeContentByKind("监督样制样样品类型"))
            {
                CmcsRCMakeDetail rCMakeDetail = new CmcsRCMakeDetail();
                rCMakeDetail.BarrelCode = CommonDAO.GetInstance().CreateNewMakeBarrelCodeByMakeCode(txt_MakeCode.Text, item.Content);
                rCMakeDetail.SampleType = item.Content;
                rCMakeDetail.BarrelTime = DateTime.Now;
                details.Add(rCMakeDetail);
            }
            this.cmcsMakeDetail = details;
        }

        /// <summary>
        /// 获取明细信息
        /// </summary>
        /// <returns></returns>
        public IList<CmcsRCMakeDetail> GetDetails()
        {
            IList<CmcsRCMakeDetail> details = new List<CmcsRCMakeDetail>();
            foreach (GridRow gridRow in superGridControl1.PrimaryGrid.Rows)
            {
                CmcsRCMakeDetail entity = gridRow.DataItem as CmcsRCMakeDetail;
                if (entity != null)
                {
                    details.Add(entity);
                }
            }
            return details;
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        void SaveAndUpdate(CmcsRCMake rCMake, IList<CmcsRCMakeDetail> details)
        {
            CmcsRCMake oldmake = Dbers.GetInstance().SelfDber.Get<CmcsRCMake>(rCMake.Id);
            if (oldmake != null)
            {
                Dbers.GetInstance().SelfDber.Update(rCMake);
                CreateAssay(rCMake);
            }
            else
            {
                Dbers.GetInstance().SelfDber.Insert(rCMake);
                CreateAssay(rCMake);
            }

            List<CmcsRCMakeDetail> olds = Dbers.GetInstance().SelfDber.Entities<CmcsRCMakeDetail>(" where MakeId=:MakeId", new { MakeId = rCMake.Id });
            foreach (CmcsRCMakeDetail old in olds)
            {
                CmcsRCMakeDetail del = details.Where(a => a.Id == old.Id).FirstOrDefault();
                if (del == null)
                {
                    Dbers.GetInstance().SelfDber.Delete<CmcsBuyFuelTransportDeduct>(old.Id);
                }
            }
            foreach (var detail in details)
            {
                detail.MakeId = rCMake.Id;
                CmcsRCMakeDetail insertorupdate = olds.Where(a => a.Id == detail.Id).FirstOrDefault();
                if (insertorupdate == null)
                {
                    Dbers.GetInstance().SelfDber.Insert(detail);
                }
                else
                {
                    Dbers.GetInstance().SelfDber.Update(detail);
                }
            }
        }

        public bool CreateAssay(CmcsRCMake rCMake)
        {
            // 入厂煤化验
            CmcsRCAssay rCAssay = CommonDAO.GetInstance().SelfDber.Entity<CmcsRCAssay>("where MakeId=:MakeId and AssayWay='监督样化验'", new { MakeId = rCMake.Id });
            if (rCAssay == null)
            {
                // 入厂煤煤质

                CmcsFuelQuality fuelQuality = new CmcsFuelQuality()
                {
                    Id = Guid.NewGuid().ToString()
                };

                if (CommonDAO.GetInstance().SelfDber.Insert(fuelQuality) > 0)
                {
                    rCAssay = new CmcsRCAssay()
                    {
                        MakeId = rCMake.Id,
                        AssayType = eAssayType.三级编码化验.ToString(),
                        AssayWay = eAssayType.监督样化验.ToString(),
                        AssayCode = CommonDAO.GetInstance().CreateNewAssayCode(rCMake.CreateDate),
                        FuelQualityId = fuelQuality.Id,
                        AssayPle = "",
                        WfStatus = 0,
                    };
                    return CommonDAO.GetInstance().SelfDber.Insert(rCAssay) > 0;
                }
            }
            return false;
        }

        private void superGridControl1_BeginEdit(object sender, DevComponents.DotNetBar.SuperGrid.GridEditEventArgs e)
        {
            // 取消编辑
            e.Cancel = true;
        }

        private void btnSelectSendUnit_Click(object sender, EventArgs e)
        {
            FrmSendUnitSelect Frm = new FrmSendUnitSelect("order by SendUnitCode desc");
            Frm.ShowDialog();
            if (Frm.DialogResult == DialogResult.OK)
            {
                this.txt_SendUnit.Text = Frm.Output.SendUnitName;
            }
        }

        /// <summary>
        /// 选择时间
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dt_UseTime_TextChanged(object sender, EventArgs e)
        {
            DateTimeInput input = (DateTimeInput)sender;
            if (input.Value.Hour == 0)
                input.Value = Convert.ToDateTime(input.Value.ToString("yyyy-MM-dd") + DateTime.Now.ToString(" HH:mm:ss"));
        }

    }
}