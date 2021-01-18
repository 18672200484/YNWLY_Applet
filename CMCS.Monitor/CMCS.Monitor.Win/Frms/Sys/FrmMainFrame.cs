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
using CMCS.Common.Entities.BaseInfo;
using CMCS.Common.Entities.Inf;
using CMCS.Common.Entities.Sys;
using CMCS.Common.Enums;
using CMCS.Common.Utilities;
using CMCS.Forms.UserControls;
using CMCS.Monitor.Win.Core;
using CMCS.Monitor.Win.Frm.Sys;
using CMCS.Monitor.Win.Frms;
using CMCS.Monitor.Win.Frms.Sys;
using CMCS.Monitor.Win.Utilities;
//
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Metro;
using Xilium.CefGlue;

namespace CMCS.Monitor.Win.Frms.Sys
{
    public partial class FrmMainFrame : MetroForm
    {
         CommonDAO commonDAO = CommonDAO.GetInstance();

        public static SuperTabControlManager superTabControlManager;

        public FrmMainFrame()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //lblVersion.Text = new AU.Updater().Version;

            FrmMainFrame.superTabControlManager = new SuperTabControlManager(this.superTabControl1);
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            //if (SelfVars.LoginUser != null) lblLoginUserName.Text = SelfVars.LoginUser.UserName;

            CommonDAO.GetInstance().ResetAllSysMessageStatus();

            // 打开集中管控首页
            btnOpenMainPage_Click(null, null);

            InitEquipmentStatus();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (e.CloseReason == CloseReason.UserClosing)
            {
                if (MessageBoxEx.Show("确认退出系统？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    CefRuntime.Shutdown();
                    Application.Exit();
                }
                else
                {
                    e.Cancel = true;
                }
            }
        }

        //退出系统
        private void btnApplicationExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        //显示当前时间
        private void timer_CurrentTime_Tick(object sender, EventArgs e)
        {
            lblCurrentTime.Text = DateTime.Now.ToString("yyyy年MM月dd日 HH:mm:ss");
        }

        #region 打开/切换可视主界面

        #region 弹出窗体

        /// <summary>
        /// 打开集中管控首页1
        /// </summary>
        public void OpenHomePage1()
        {
            this.Invoke((Action)(() =>
            {
                string uniqueKey = FrmHomePage.UniqueKey;

                if (FrmMainFrame.superTabControlManager.GetTab(uniqueKey) == null)
                {
                    FrmHomePage Frm = new FrmHomePage();
                    FrmMainFrame.superTabControlManager.CreateTab(Frm.Text, uniqueKey, Frm, true);
                }
                else
                    FrmMainFrame.superTabControlManager.ChangeToTab(uniqueKey);
            }));
        }

        /// <summary>
        /// 打开集中管控首页2
        /// </summary>
        public void OpenHomePage2()
        {
            SetColoTable();

            this.btnOpenMainPage.ColorTable = eButtonColor.Blue;

            this.Invoke((Action)(() =>
           {
               string uniqueKey = FrmHomeYNWLY.UniqueKey;

               if (FrmMainFrame.superTabControlManager.GetTab(uniqueKey) == null)
               {
                   FrmHomeYNWLY Frm = new FrmHomeYNWLY();
                   FrmMainFrame.superTabControlManager.CreateTab(Frm.Text, uniqueKey, Frm, true);
               }
               else
                   FrmMainFrame.superTabControlManager.ChangeToTab(uniqueKey);
           }));
        }

        /// <summary>
        /// 打开火车入厂记录查询
        /// </summary>
        public void OpenWeightBridgeLoadToday()
        {
            SetColoTable();

            this.buttonX6.ColorTable = eButtonColor.Blue;

            this.Invoke((Action)(() =>
            {
                string uniqueKey = FrmWeightBridgeLoadToday.UniqueKey;

                if (FrmMainFrame.superTabControlManager.GetTab(uniqueKey) == null)
                {
                    FrmWeightBridgeLoadToday item = new FrmWeightBridgeLoadToday();
                    FrmMainFrame.superTabControlManager.CreateTab(item.Text, uniqueKey, item, true);
                }
                else
                    FrmMainFrame.superTabControlManager.ChangeToTab(uniqueKey);
            }));
        }

        /// <summary>
        /// 打开汽车入厂记录查询
        /// </summary>
        public void OpenBuyFuelLoadToday()
        {
            SetColoTable();

            this.buttonX6.ColorTable = eButtonColor.Blue;

            this.Invoke((Action)(() =>
            {
                string uniqueKey = FrmBuyFuelLoadToday.UniqueKey;

                if (FrmMainFrame.superTabControlManager.GetTab(uniqueKey) == null)
                {
                    FrmBuyFuelLoadToday item = new FrmBuyFuelLoadToday();
                    FrmMainFrame.superTabControlManager.CreateTab(item.Text, uniqueKey, item, true);
                }
                else
                    FrmMainFrame.superTabControlManager.ChangeToTab(uniqueKey);
            }));
        }

        /// <summary>
        /// 打开皮带采样机监控
        /// </summary>
        public void OpenTrainBeltSampler()
        {
            SetColoTable();

            this.btnCZGK.ColorTable = eButtonColor.Blue;

            this.Invoke((Action)(() =>
            {
                string uniqueKey = FrmTrainBeltSampler.UniqueKey;

                if (FrmMainFrame.superTabControlManager.GetTab(uniqueKey) == null)
                {
                    FrmTrainBeltSampler frmTrainBeltSampler = new FrmTrainBeltSampler();
                    FrmMainFrame.superTabControlManager.CreateTab(frmTrainBeltSampler.Text, uniqueKey, frmTrainBeltSampler, true);
                }
                else
                    FrmMainFrame.superTabControlManager.ChangeToTab(uniqueKey);
            }));
        }

        //打开全自动制样机监控
        public void OpenAutoMaker(string par)
        {
            SetColoTable();

            this.btnOpenAutoMaker.ColorTable = eButtonColor.Blue;

            this.Invoke((Action)(() =>
            {
                string uniqueKey = FrmAutoMaker.UniqueKey;

                if (FrmMainFrame.superTabControlManager.GetTab(uniqueKey) == null)
                {
                    FrmAutoMaker item = new FrmAutoMaker();
                    FrmAutoMaker.Device = par;
                    FrmMainFrame.superTabControlManager.CreateTab(item.Text, uniqueKey, item, true);
                }
                else
                {
                    FrmAutoMaker.Device = par;
                    FrmMainFrame.superTabControlManager.ChangeToTab(uniqueKey);
                }
            }));
        }

        /// <summary>
        /// 打开智能存样柜与气动传输监控
        /// </summary>
        public void OpenAutoCupboardPneumaticTransfer(string par)
        {
            SetColoTable();

            this.btnOpenAutoCupboard.ColorTable = eButtonColor.Blue;

            this.Invoke((Action)(() =>
            {
                string uniqueKey = FrmAutoCupboardPneumaticTransfer.UniqueKey;

                if (FrmMainFrame.superTabControlManager.GetTab(uniqueKey) == null)
                {
                    FrmAutoCupboardPneumaticTransfer item = new FrmAutoCupboardPneumaticTransfer();
                    FrmAutoCupboardPneumaticTransfer.Device = par;
                    FrmMainFrame.superTabControlManager.CreateTab(item.Text, uniqueKey, item, true);
                }
                else
                {
                    FrmAutoCupboardPneumaticTransfer.Device = par;
                    FrmMainFrame.superTabControlManager.ChangeToTab(uniqueKey);
                }
            }));

        }

        /// <summary>
        /// 打开火车入厂翻车机监控
        /// </summary>
        public void OpenTrainTipper(string par)
        {
            SetColoTable();
            this.btnOpenTrainTipper.ColorTable = eButtonColor.Blue;

            this.Invoke((Action)(() =>
            {
                string uniqueKey = FrmTrainTipper.UniqueKey;

                if (FrmMainFrame.superTabControlManager.GetTab(uniqueKey) == null)
                {
                    FrmTrainUpender item = new FrmTrainUpender();
                    FrmTrainUpender.Device = par;
                    FrmMainFrame.superTabControlManager.CreateTab(item.Text, uniqueKey, item, true);
                }
                else
                {
                    FrmTrainUpender.Device = par;
                    FrmMainFrame.superTabControlManager.ChangeToTab(uniqueKey);
                }
            }));

        }
        /// <summary>
        /// 打开火车入厂翻车机监控
        /// </summary>
        public void OpenTrainUpender()
        {
            SetColoTable();
            this.btnOpenTrainTipper.ColorTable = eButtonColor.Blue;

            this.Invoke((Action)(() =>
            {
                string uniqueKey = FrmTrainUpender.UniqueKey;

                if (FrmMainFrame.superTabControlManager.GetTab(uniqueKey) == null)
                {
                    FrmTrainUpender frm = new FrmTrainUpender();
                    FrmMainFrame.superTabControlManager.CreateTab(frm.Text, uniqueKey, frm, true);
                }
                else
                    FrmMainFrame.superTabControlManager.ChangeToTab(uniqueKey);
            }));
        }

        /// <summary>
        /// 打开汽车机械采样机监控
        /// </summary>
        public void OpenTruckMachinerySampler(string par)
        {
            SetColoTable();
            this.btnCZGK.ColorTable = eButtonColor.Blue;

            this.Invoke((Action)(() =>
            {
                string uniqueKey = FrmCarSampler.UniqueKey;

                if (FrmMainFrame.superTabControlManager.GetTab(uniqueKey) == null)
                {
                    FrmCarSampler frm = new FrmCarSampler();
                    FrmMainFrame.superTabControlManager.CreateTab(frm.Text, uniqueKey, frm, true);
                }
                else
                    FrmMainFrame.superTabControlManager.ChangeToTab(uniqueKey);
            }));
        }

        /// <summary>
        /// 打开汽车过衡监控
        /// </summary>
        public void OpenTruckWeighter()
        {
            this.Invoke((Action)(() =>
            {
                string uniqueKey = FrmTruckWeighter.UniqueKey;

                if (FrmMainFrame.superTabControlManager.GetTab(uniqueKey) == null)
                {
                    FrmTruckWeighter frm = new FrmTruckWeighter();
                    FrmMainFrame.superTabControlManager.CreateTab(frm.Text, uniqueKey, frm, true);
                }
                else
                    FrmMainFrame.superTabControlManager.ChangeToTab(uniqueKey);
            }));
        }

        /// <summary>
        /// 打开汽车过衡监控
        /// </summary>
        public void OpenTruckOrder()
        {
            this.Invoke((Action)(() =>
            {
            string uniqueKey = FrmTruckOrder.UniqueKey;

            if (FrmMainFrame.superTabControlManager.GetTab(uniqueKey) == null)
            {
                FrmTruckOrder frm = new FrmTruckOrder();
                FrmMainFrame.superTabControlManager.CreateTab(frm.Text, uniqueKey, frm, true);
            }
            else
                FrmMainFrame.superTabControlManager.ChangeToTab(uniqueKey);
            }));
        }

        /// <summary>
        /// 打开汽车机械采样监控
        /// </summary>
        public void OpenCarSampler(string par)
        {
            SetColoTable();
            this.btnCZGK.ColorTable = eButtonColor.Blue;

            this.Invoke((Action)(() =>
           {
               string uniqueKey = FrmCarSampler.UniqueKey;

               if (FrmMainFrame.superTabControlManager.GetTab(uniqueKey) == null)
               {
                   FrmCarSampler frm = new FrmCarSampler();
                   FrmMainFrame.superTabControlManager.CreateTab(frm.Text, uniqueKey, frm, true);
               }
               else
                   FrmMainFrame.superTabControlManager.ChangeToTab(uniqueKey);
           }));
        }

        /// <summary>
        /// 打开化验室网络管理监控
        /// </summary>
        public void OpenAssayManage()
        {
            SetColoTable();
            this.btnOpenAssayManage.ColorTable = eButtonColor.Blue;

            this.Invoke((Action)(() =>
           {
               string uniqueKey = FrmAssayManage.UniqueKey;

               if (FrmMainFrame.superTabControlManager.GetTab(uniqueKey) == null)
               {
                   FrmAssayManage frm = new FrmAssayManage();
                   FrmMainFrame.superTabControlManager.CreateTab(frm.Text, uniqueKey, frm, true);
               }
               else
                   FrmMainFrame.superTabControlManager.ChangeToTab(uniqueKey);
           }));
        }

        /// <summary>
        /// 打开汽车监控界面
        /// </summary>
        public void OpenCarMonitor()
        {
            SetColoTable();
            this.btnCarMonitor.ColorTable = eButtonColor.Blue;

            this.Invoke((Action)(() =>
            {
                string uniqueKey = FrmCarMonitor.UniqueKey;

                if (FrmMainFrame.superTabControlManager.GetTab(uniqueKey) == null)
                {
                    FrmCarMonitor frm = new FrmCarMonitor();
                    FrmMainFrame.superTabControlManager.CreateTab(frm.Text, uniqueKey, frm, true);
                }
                else
                    FrmMainFrame.superTabControlManager.ChangeToTab(uniqueKey);
            }));
        }

        /// <summary>
        /// 打开设备异常查询
        /// </summary>
        public void OpenEquInfHitch()
        {
          this.Invoke((Action)(() =>
          {
              string uniqueKey = FrmEquInfHitch.UniqueKey;

              if (FrmMainFrame.superTabControlManager.GetTab(uniqueKey) == null)
              {
                  FrmEquInfHitch item = new FrmEquInfHitch();
                  FrmMainFrame.superTabControlManager.CreateTab(item.Text, uniqueKey, item, true);
              }
              else
                  FrmMainFrame.superTabControlManager.ChangeToTab(uniqueKey);
          }));
        }

        /// <summary>
        /// 打开门禁进出记录
        /// </summary>
        public void OpenGuardInfo()
        {
            this.Invoke((Action)(() =>
          {
              string uniqueKey = FrmInfGuardInfo.UniqueKey;

              if (FrmMainFrame.superTabControlManager.GetTab(uniqueKey) == null)
              {
                  FrmInfGuardInfo item = new FrmInfGuardInfo();
                  FrmMainFrame.superTabControlManager.CreateTab(item.Text, uniqueKey, item, true);
              }
              else
                  FrmMainFrame.superTabControlManager.ChangeToTab(uniqueKey);
          }));
        }

        /// <summary>
        /// 点击地磅记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenPoundInfo_Click(object sender, EventArgs e)
        {
            OpenPoundInfo("");
        }

        /// <summary>
        /// 打开地磅记录
        /// </summary>
        public void OpenPoundInfo(string par)
        {
            SetColoTable();

            this.buttonX6.ColorTable = eButtonColor.Blue;

            this.Invoke((Action)(() =>
            {
                string uniqueKey = FrmPoundInfo.UniqueKey;

                if (FrmMainFrame.superTabControlManager.GetTab(uniqueKey) == null)
                {
                    FrmPoundInfo item = new FrmPoundInfo();
                    FrmPoundInfo.Device = par;
                    FrmMainFrame.superTabControlManager.CreateTab(item.Text, uniqueKey, item, true);
                }
                else
                {
                    FrmPoundInfo.Device = par;
                    FrmMainFrame.superTabControlManager.ChangeToTab(uniqueKey);
                }
            }));
        }

     

        /// <summary>
        /// 打开进出场记录
        /// </summary>
        public void OpenJoinBacthManag(string par)
        {
            SetColoTable();

            this.btnJoinBacthManage.ColorTable = eButtonColor.Blue;

            this.Invoke((Action)(() =>
            {
                string uniqueKey = FrmJoinBacthManage.UniqueKey;

                if (FrmMainFrame.superTabControlManager.GetTab(uniqueKey) == null)
                {
                    FrmJoinBacthManage item = new FrmJoinBacthManage();
                    FrmMainFrame.superTabControlManager.CreateTab(item.Text, uniqueKey, item, true);
                }
                else
                    FrmMainFrame.superTabControlManager.ChangeToTab(uniqueKey);
            }));


        }

        /// <summary>
        /// 点击地磅记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenInOutInfo_Click(object sender, EventArgs e)
        {
            OpenInOutInfo("");
        }


         /// <summary>
        /// 打开进出场记录
        /// </summary>
        public void OpenInOutInfo(string par)
        {
            SetColoTable();

            this.buttonX6.ColorTable = eButtonColor.Blue;

            this.Invoke((Action)(() =>
            {
                string uniqueKey = FrmInOutInfo.UniqueKey;

                if (FrmMainFrame.superTabControlManager.GetTab(uniqueKey) == null)
                {
                    FrmInOutInfo item = new FrmInOutInfo();
                    FrmInOutInfo.Device = par;
                    FrmMainFrame.superTabControlManager.CreateTab(item.Text, uniqueKey, item, true);
                }
                else
                {
                    FrmInOutInfo.Device = par;
                    FrmMainFrame.superTabControlManager.ChangeToTab(uniqueKey);
                }
            }));

           
        }

        /// <summary>
        /// 全部置为BlueWithBackground
        /// </summary>
        private void SetColoTable()
        {
            this.btnOpenMainPage.ColorTable = eButtonColor.BlueWithBackground;
            this.btnOpenAutoCupboard.ColorTable = eButtonColor.BlueWithBackground;
            this.btnOpenAssayManage.ColorTable = eButtonColor.BlueWithBackground;
            this.btnOpenAutoCupboard.ColorTable = eButtonColor.BlueWithBackground;
            this.btnCarMonitor.ColorTable = eButtonColor.BlueWithBackground;
            this.btnOpenGuardInfo.ColorTable = eButtonColor.BlueWithBackground;
            this.btnOpenEquInfHitch.ColorTable = eButtonColor.BlueWithBackground;
            this.btnOpenAutoMaker.ColorTable = eButtonColor.BlueWithBackground;
            this.btnCZGK.ColorTable = eButtonColor.BlueWithBackground;
            this.buttonX6.ColorTable = eButtonColor.BlueWithBackground;
            this.btnOpenTrainTipper.ColorTable = eButtonColor.BlueWithBackground;
            //this.btnPoundInfo.ColorTable = eButtonColor.BlueWithBackground;
            //this.btnInOutInfo.ColorTable = eButtonColor.BlueWithBackground;
            this.btnJoinBacthManage.ColorTable = eButtonColor.BlueWithBackground;
        }
        
        #endregion

        /// <summary>
        /// FrmCefTester
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCefTester_Click(object sender, EventArgs e)
        {
            this.InvokeEx(() =>
            {
                string uniqueKey = FrmCefTester.UniqueKey;

                if (FrmMainFrame.superTabControlManager.GetTab(uniqueKey) == null)
                {
                    FrmCefTester Frm = new FrmCefTester();
                    FrmMainFrame.superTabControlManager.CreateTab(Frm.Text, uniqueKey, Frm, true);
                }
                else
                    FrmMainFrame.superTabControlManager.ChangeToTab(uniqueKey);
            });
        }

        /// <summary>
        /// 打开集中管控首页
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenMainPage_Click(object sender, EventArgs e)
        {
            OpenHomePage2();
        }

        /// <summary>
        /// 打开集中管控首页1
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenHomePage1_Click(object sender, EventArgs e)
        {
            OpenHomePage1();
        }

        /// <summary>
        /// 打开集中管控首页2
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenHomePage2_Click(object sender, EventArgs e)
        {
            OpenHomePage2();
        }

        /// <summary>
        /// 打开火车入厂煤记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenWeightBridgeLoad_Click(object sender, EventArgs e)
        {
            OpenWeightBridgeLoadToday();
        }

        /// <summary>
        /// 打开汽车入厂煤记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenBuyFuelLoad_Click(object sender, EventArgs e)
        {
            OpenBuyFuelLoadToday();
        }

        /// <summary>
        /// 打开皮带采样机监控
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenTrainBeltSampler_Click(object sender, EventArgs e)
        {
            OpenTrainBeltSampler();
        }

        /// <summary>
        /// 打开火车桥式采样机监控
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenTrainBridgeSampler_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 打开全自动制样机监控
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenAutoMaker_Click(object sender, EventArgs e)
        {
            OpenAutoMaker("");
        }

        /// <summary>
        /// 打开气动传输界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenAutoCupboard_Click(object sender, EventArgs e)
        {
            OpenAutoCupboardPneumaticTransfer("");
        }

        /// <summary>
        /// 打开翻车机界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenTrainTipper_Click(object sender, EventArgs e)
        {
            OpenTrainTipper("");
        }

        /// <summary>
        /// 打开汽车机械采样机界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenCarSampler_Click(object sender, EventArgs e)
        {
            OpenCarSampler("");
        }

        /// <summary>
        /// 打开化验室网络管理界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenAssayManage_Click(object sender, EventArgs e)
        {
            OpenAssayManage();
        }

        /// <summary>
        /// 打开汽车监控界面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnCarMonitor_Click(object sender, EventArgs e)
        {
            OpenCarMonitor();
        }

        /// <summary>
        /// 打开设备异常查询
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenEquInfHitch_Click(object sender, EventArgs e)
        {
            SetColoTable();
            this.btnOpenEquInfHitch.ColorTable = eButtonColor.Blue;

            OpenEquInfHitch();
        }
        
        /// <summary>
        /// 打开门禁记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnOpenGuardInfo_Click(object sender, EventArgs e)
        {
            SetColoTable();
            this.btnOpenGuardInfo.ColorTable = eButtonColor.Blue;

            OpenGuardInfo();
        }

        /// <summary>
        /// 打开地磅记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnPoundInfo_Click(object sender, EventArgs e)
        {
            OpenPoundInfo("");
        }

        /// <summary>
        /// 打开进出场记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInOutInfo_Click(object sender, EventArgs e)
        {
            OpenInOutInfo("");
        }

         /// <summary>
        /// 打开合样归批机
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnJoinBacthManage_Click(object sender, EventArgs e)
        {
            OpenJoinBacthManag("");
        }
        

        #endregion

        #region 设备状态监控

        /// <summary>
        /// 初始化设备状态任务
        /// </summary>
        private void InitEquipmentStatus()
        {
            timer_EquipmentStatus.Enabled = true;

            List<CmcsCMEquipment> list = commonDAO.GetChildrenMachinesByCode("皮带采样机");
            list.AddRange(commonDAO.GetChildrenMachinesByCode("汽车机械采样机"));
            list.AddRange(commonDAO.GetChildrenMachinesByCode("全自动制样机"));
            list.AddRange(commonDAO.GetChildrenMachinesByCode("智能存样柜"));
            list.AddRange(commonDAO.GetChildrenMachinesByCode("气动传输"));
            CreateEquipmentStatus(list);

            // 更新设备状态
            RefreshEquipmentStatus();
        }

        /// <summary>
        /// 创建设备状态控件
        /// </summary>
        /// <param name="list"></param>
        private void CreateEquipmentStatus(List<CmcsCMEquipment> list)
        {
            flpanEquipments.SuspendLayout();

            foreach (CmcsCMEquipment cMEquipment in list)
            {
                UCtrlSignalLight uCtrlSignalLight = new UCtrlSignalLight()
                {
                    Anchor = AnchorStyles.Left,
                    Width = 16,
                    Height = 16,
                    Tag = cMEquipment.EquipmentCode,
                    Padding = new System.Windows.Forms.Padding(10, 0, 0, 0)
                };
                SetSystemStatusToolTip(uCtrlSignalLight);

                flpanEquipments.Controls.Add(uCtrlSignalLight);

                LabelX lblMachineName = new LabelX()
                {
                    Text = cMEquipment.EquipmentName,
                    Tag = cMEquipment.EquipmentCode,
                    AutoSize = true,
                    Anchor = AnchorStyles.Left,
                    Font = new Font("Segoe UI", 10f, FontStyle.Regular)
                };

                flpanEquipments.Controls.Add(lblMachineName);
            }

            flpanEquipments.ResumeLayout();
        }

        /// <summary>
        /// 更新设备状态
        /// </summary>
        private void RefreshEquipmentStatus()
        {
            foreach (UCtrlSignalLight uCtrlSignalLight in flpanEquipments.Controls.OfType<UCtrlSignalLight>())
            {
                if (uCtrlSignalLight.Tag == null) continue;

                string machineCode = uCtrlSignalLight.Tag.ToString();
                if (string.IsNullOrEmpty(machineCode)) continue;

                string systemStatus = CommonDAO.GetInstance().GetSignalDataValue(machineCode, eSignalDataName.程序状态.ToString());
                if ("|就绪待机|".Contains("|" + systemStatus + "|"))
                    uCtrlSignalLight.LightColor = EquipmentStatusColors.BeReady;
                else if ("|正在运行|正在卸样|".Contains("|" + systemStatus + "|"))
                    uCtrlSignalLight.LightColor = EquipmentStatusColors.Working;
                else if ("|发生故障|".Contains("|" + systemStatus + "|"))
                    uCtrlSignalLight.LightColor = EquipmentStatusColors.Breakdown;
                else
                    uCtrlSignalLight.LightColor = EquipmentStatusColors.Forbidden;
            }
        }

        /// <summary>
        /// 设置ToolTip提示
        /// </summary>
        private void SetSystemStatusToolTip(Control control)
        {
            this.toolTip1.SetToolTip(control, "<绿色> 就绪待机\r\n<红色> 正在运行\r\n<黄色> 发生故障");
        }

        private void timer_EquipmentStatus_Tick(object sender, EventArgs e)
        {
            // 更新设备状态
            RefreshEquipmentStatus();
        }

        #endregion

        #region 显示消息框

        /// <summary>
        /// 显示消息框
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer_MsgTime_Tick(object sender, EventArgs e)
        {
            //timer_MsgTime.Stop();

            //if (DateTime.Now.Second % 30 == 0)
            //    //30秒获取一次异常信息表
            //    ShowEquInfHitch();
            //if (DateTime.Now.Second % 5 == 0)
            //    ShowSysMessage();           

            //timer_MsgTime.Start();
        }

        /// <summary>
        /// 显示设备异常消息框
        /// </summary>
        public void ShowEquInfHitch()
        {
            //List<InfEquInfHitch> listResult = CommonDAO.GetInstance().GetWarnEquInfHitch();
            //StringBuilder sbHitchDescribe = new StringBuilder();
            //if (listResult.Count > 0)
            //{
            //    foreach (InfEquInfHitch item in listResult)
            //    {
            //        sbHitchDescribe.Append("<font color='red' size='2'>");
            //        sbHitchDescribe.Append(item.HitchTime.ToString("HH:mm") + "   " + item.HitchDescribe + "<br>");
            //        sbHitchDescribe.Append("</font>");
            //        CommonDAO.GetInstance().UpdateReadEquInfHitch(item.Id);
            //    }
            //    //右下角显示
            //    FrmSysMsg frm_sysMsg = new FrmSysMsg(sbHitchDescribe.ToString(), false);
            //    frm_sysMsg.Show();
            //}
        }

        /// <summary>
        /// 显示系统消息
        /// </summary>
        public void ShowSysMessage()
        {
            //List<CmcsSysMessage> Messages = CommonDAO.GetInstance().GetTodayTopSysMessage();
            //if (Messages != null)
            //{
            //    foreach (CmcsSysMessage item in Messages)
            //    {
            //        CommonDAO.GetInstance().ChangeSysMessageStatus(item.Id, eSysMessageStatus.处理中);                   
            //    }
            //    FrmSysMsg frmSysMsg = new FrmSysMsg(Messages);
            //    frmSysMsg.OnMsgHandler += new FrmSysMsg.MsgHandler(frmSysMsg_OnMsgHandler);
            //}
        }

        void frmSysMsg_OnMsgHandler(string msgId, string msgCode, string jsonStr, string buttonText, Form frmMsg)
        {
            switch (buttonText)
            {
                case "查看":

                    CommonDAO.GetInstance().ChangeSysMessageStatus(msgId, eSysMessageStatus.已处理);

                    switch (msgCode)
                    {
                        case "皮带采样机":
                            btnOpenTrainBeltSampler_Click(null, null);
                            break;
                        case "火车桥式采样机":
                            btnOpenTrainBridgeSampler_Click(null, null);
                            break;
                        case "汽车桥式采样机":
                            break;
                        case "全自动制样机":
                            btnOpenAutoMaker_Click(null, null);
                            break;
                        case "气动传输":
                            btnOpenAutoCupboard_Click(null, null);
                            break;
                        case "轨道衡":
                            btnOpenWeightBridgeLoad_Click(null, null);
                            break;
                    }

                    frmMsg.Close();
                    break;
                case "我知道了":
                    frmMsg.Close();
                    break;
                default:
                    frmMsg.Close();
                    break;
            }
        }
        #endregion

        private void buttonX1_Click(object sender, EventArgs e)
        {
            OpenTruckWeighter();
        }

        /// <summary>
        /// Invoke封装
        /// </summary>
        /// <param name="action"></param>
        public void InvokeEx(Action action)
        {
            if (this.IsDisposed || !this.IsHandleCreated) return;

            this.Invoke(action);
        }

        private void btnOpenOutBeltSampler_Click(object sender, EventArgs e)
        {
            OpenOutTrainBeltSampler();
        }

        //打开出场皮带采样机监控
        public void OpenOutTrainBeltSampler()
        {
            SetColoTable();

            this.btnCZGK.ColorTable = eButtonColor.Blue;

            this.Invoke((Action)(() =>
            {
                string uniqueKey = FrmOutBeltSampler.UniqueKey;

                if (FrmMainFrame.superTabControlManager.GetTab(uniqueKey) == null)
                {
                    FrmOutBeltSampler frmTrainBeltSampler = new FrmOutBeltSampler();
                    FrmMainFrame.superTabControlManager.CreateTab(frmTrainBeltSampler.Text, uniqueKey, frmTrainBeltSampler, true);
                }
                else
                    FrmMainFrame.superTabControlManager.ChangeToTab(uniqueKey);
            }));
        }

        private void btnOpenSaleFuelLoad_Click(object sender, EventArgs e)
        {
            OpenSaleFuelLoadToday();
        }



        /// <summary>
        /// 打开汽车入厂记录查询
        /// </summary>
        public void OpenSaleFuelLoadToday()
        {
            SetColoTable();

            this.buttonX6.ColorTable = eButtonColor.Blue;

            this.Invoke((Action)(() =>
            {
                string uniqueKey = FrmSaleFuelLoadToday.UniqueKey;

                if (FrmMainFrame.superTabControlManager.GetTab(uniqueKey) == null)
                {
                    FrmSaleFuelLoadToday item = new FrmSaleFuelLoadToday();
                    FrmMainFrame.superTabControlManager.CreateTab(item.Text, uniqueKey, item, true);
                }
                else
                    FrmMainFrame.superTabControlManager.ChangeToTab(uniqueKey);
            }));
        }
    }
}
