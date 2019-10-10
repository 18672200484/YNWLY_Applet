using System;
using System.Windows.Forms;
using System.Threading;
using System.Collections.Generic;
//
using DevComponents.DotNetBar.Metro;
using DevComponents.DotNetBar.SuperGrid;
using CMCS.Common;
using CMCS.Common.Entities.iEAA;
using DevComponents.DotNetBar;
using CMCS.Common.DAO;
using CMCS.FingerIdentify.Utilities;
using ZAZ.Finger;
using System.IO;
using System.Drawing;
using CMCS.FingerIdentify.DAO;
using CMCS.Common.Entities.Fuel;
using CMCS.Common.Utilities;
using CMCS.FingerIdentify.Enums;

namespace CMCS.FingerIdentify.Frms
{
	public partial class FrmFingerRegist : DevComponents.DotNetBar.Metro.MetroForm
	{
		public FrmFingerRegist()
		{
			InitializeComponent();
		}
		public FrmFingerRegist(User _user)
		{
			InitializeComponent();
			this.User = _user;
		}
		/// <summary>
		/// 窗体唯一标识符
		/// </summary>
		public static string UniqueKey = "FrmFingerRegist";

		#region 业务处理类
		CommonDAO commonDAO = CommonDAO.GetInstance();
		FingerIdentifyDAO fingerIdentifyDAO = FingerIdentifyDAO.GetInstance();
		TaskSimpleScheduler taskSimpleScheduler = new TaskSimpleScheduler();

		#endregion

		#region 公共Vars

		RTxtOutputer rTxtOutputer;

		/// <summary>
		/// 等待上传的抓拍
		/// </summary>
		Queue<string> waitForUpload = new Queue<string>();

		User user;
		/// <summary>
		/// 当前用户
		/// </summary>
		public User User
		{
			get { return user; }
			set
			{
				user = value;
				lbeUserName.Text = value.UserName;
			}
		}

		#endregion

		#region 设备属性
		Int32 fingerIndex;
		/// <summary>
		/// 当前指纹编号
		/// </summary>
		public Int32 FingerIndex
		{
			get { return fingerIndex; }
			set
			{
				fingerIndex = value;
			}
		}

		bool isConnect;
		/// <summary>
		/// 设备连接状态
		/// </summary>
		public bool IsConnect
		{
			get { return isConnect; }
			set
			{
				isConnect = value;
			}
		}

		/// <summary>
		/// 当前连接句柄
		/// </summary>
		IntPtr hHandle = new IntPtr();

		/// <summary>
		/// 设备地址
		/// </summary>
		private UInt32 nDevAddr = 0xffffffff;

		/// <summary>
		/// 图像大小
		/// </summary>
		public const int ImageSize = (256 * 288);

		/// <summary>
		/// 执行结果
		/// </summary>
		private int ret = 0;

		private Int32 timeout = 20;
		/// <summary>
		/// 等待超时
		/// </summary>
		public Int32 TimeOut
		{
			get { return timeout; }
			set
			{
				timeout = value;
			}
		}

		private string strFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Data");
		/// <summary>
		/// dat文件路径
		/// </summary>
		public string StrFile
		{
			get { return strFile; }
			set
			{
				strFile = value;
			}
		}
		#endregion

		/// <summary>
		/// 窗体初始化
		/// </summary>
		private void FormInit()
		{
			rTxtOutputer = new RTxtOutputer(rTxTMessageInfo);
		}

		private void FrmFingerRegist_Load(object sender, EventArgs e)
		{
			FormInit();
			superGridControl1.PrimaryGrid.AutoGenerateColumns = false;
			BindData();
			InitHardware();
			cmbFingerName.SelectedIndex = 0;
		}

		/// <summary>
		/// 初始化外接设备
		/// </summary>
		public void InitHardware()
		{
			int ret = 0;
			ret = Fingerdll.ZAZOpenDeviceEx(ref hHandle, 2, 0, 0, 2, 0);
			if (ret == 0)
			{
				this.IsConnect = true;
				ShowInfomation("设备打开成功");
				btn_eroll_Click(null, null);
			}
			else
			{
				this.IsConnect = false;
				ShowInfomation(Fingerdll.ZAZErr2Strt(ret));
			}
		}

		/// <summary>
		/// 关闭设备
		/// </summary>
		public void CloseHardware()
		{
			int ret = 0;
			ret = Fingerdll.ZAZCloseDeviceEx(hHandle);
			if (ret == 0)
			{
				this.IsConnect = true;
				ShowInfomation("设备关闭成功");
			}
			else
			{
				this.IsConnect = false;
				ShowInfomation(Fingerdll.ZAZErr2Strt(ret));
			}
		}

		#region Button事件
		private void BindData()
		{
			superGridControl1.PrimaryGrid.DataSource = fingerIdentifyDAO.GetFingerByUserId(this.User.PartyId);
		}

		private void btn_eroll_Click(object sender, EventArgs e)
		{
			//开启获取指纹图像和特征的线程
			this.TimeOut = 20;
			starterollhread();
		}

		private void btnOpen_Click(object sender, EventArgs e)
		{
			InitHardware();
		}

		private void btnClose_Click(object sender, EventArgs e)
		{
			CloseHardware();
		}

		private void FrmFingerRegist_FormClosing(object sender, FormClosingEventArgs e)
		{
			CloseHardware();
		}

		private void btnSubmit_Click(object sender, EventArgs e)
		{
			this.DialogResult = DialogResult.OK;
		}

		#endregion

		#region 指纹识别方法
		private void starterollhread()
		{
			Thread thread = new Thread(new ThreadStart(RegistFinger));
			thread.Start();
			thread.IsBackground = true;
			if (!thread.IsAlive)
			{
				Thread.Sleep(1000);
			}
		}
		/// <summary>
		/// 注册指纹
		/// </summary>
		private void RegistFinger()
		{
			if (!IsConnect)
			{
				ShowInfomation("请先打开设备...");
				return;
			}
			//生成特征A  
			if (GetFinger(1) != 1)
			{
				return;
			}
			Thread.Sleep(200);
			//生成特征B
			if (GetFinger(2) != 1)
			{
				return;
			}

			Thread.Sleep(200);
			/****************合成模板*********/
			ret = Fingerdll.ZAZRegModule(hHandle, nDevAddr);  //合并特征
			if (ret != 0)
			{
				ShowInfomation(Fingerdll.ZAZErr2Strt(ret));
				return;
			}
			else
			{
				ShowInfomation("合成指纹模板成功");
			}
			Thread.Sleep(200);

			//本例以存在在指纹设备库中进行
			//ret = Fingerdll.ZAZStoreChar(hHandle, nDevAddr, 1, FingerIndex);    //存放模板

			//StrFile = System.Windows.Forms.Application.StartupPath + "\\FTmpelet.dat";
			#region 存储指纹到服务器
			CmcsFinger userfinger = new CmcsFinger();
			InvokeEx(() =>
			{
				StrFile = commonDAO.GetCommonAppletConfigString("指纹识别数据存放路径");
				userfinger = fingerIdentifyDAO.GetFinerByFingerName(this.User.PartyId, cmbFingerName.Text);
				if (userfinger == null)
				{
					userfinger = new CmcsFinger();
					userfinger.FingerName = cmbFingerName.Text;
				}
				StrFile = Path.Combine(StrFile, this.User.UserAccount);

				if (!File.Exists(StrFile))
					Directory.CreateDirectory(StrFile);
				StrFile = Path.Combine(StrFile, userfinger.Id + ".dat");
				userfinger.FingerUrl = StrFile;

				#endregion
				//将指纹信息写入到dat文件
				ret = Fingerdll.ZAZUpChar2File(hHandle, nDevAddr, 1, strFile);
				if (ret != 0)
				{
					ShowInfomation(Fingerdll.ZAZErr2Strt(ret));
					return;
				}
				else
				{
					if (fingerIdentifyDAO.InsertFinger(this.User, userfinger) && File.Exists(StrFile))
					{
						ShowInfomation("存储指纹成功");
						//showFpdb();显示当前设备指纹存储信息
						FingerIndex++;
						BindData();
						return;
					}
				}
			});
		}

		/// <summary>
		/// 获取指纹
		/// </summary>
		/// <param name="buffer">指纹在设备中临时存放位置 1 2</param>
		/// <returns></returns>
		int GetFinger(int buffer)
		{
			TimeOut = 20;
			int ret = 0;
			byte[] ImgData = new byte[ImageSize];
			int[] ImgLen = new int[1];
			int iBuffer = buffer;

			BEIG1:
			ret = Fingerdll.ZAZGetImage(hHandle, nDevAddr);  //获取图象 
			if (ret == 0)
			{
				ShowInfomation("获取图像成功...");
			}
			else if (ret == 2)
			{
				//超时判断
				ShowInfomation("等待手指平放在传感器上-" + TimeOut.ToString() + "秒");
				if (TimeOut < 0)
				{
					ShowInfomation("等待超时");
					return 0;
				}
				TimeOut--;
				Thread.Sleep(1000);
				goto BEIG1;
			}
			else
			{
				ShowInfomation(Fingerdll.ZAZErr2Strt(ret));
				return 0;
			}

			//////////////////////////////////////////////////////////////////////////
			//不涉及图像，下面可以省略
			/****************上传图像*********/

			ShowInfomation("正在上传图像请等待...");
			ret = Fingerdll.ZAZUpImage(hHandle, nDevAddr, ImgData, ImgLen);  //上传图象
			if (ret != 0)
			{
				ShowInfomation(Fingerdll.ZAZErr2Strt(ret));
				return 0;
			}
			//strFile = System.Windows.Forms.Application.StartupPath + "\\ZAZFinger.bmp";
			strFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ZAZFinger.bmp");
			ret = Fingerdll.ZAZImgData2BMP(ImgData, strFile);
			if (ret != 0)
			{
				ShowInfomation(Fingerdll.ZAZErr2Strt(ret));
				return 0;
			}
			ShowImage(strFile);
			//ret = Fingerdll.ZAZShowFingerData(fpbmp.Handle, ref ImgData[0]);
			//////////////////////////////////////////////////////////////////////////
			/****************生成特征 *********/
			ret = Fingerdll.ZAZGenChar(hHandle, nDevAddr, iBuffer);  //生成模板
			if (ret != 0)
			{
				ShowInfomation(Fingerdll.ZAZErr2Strt(ret));
				return 0;
			}
			else
			{
				ShowInfomation("生成指纹特征" + buffer.ToString());
			}
			Thread.Sleep(10);
			BEIG2:
			if (ret == 0)
			{
				ret = Fingerdll.ZAZGetImage(hHandle, nDevAddr);  //获取图象 
				ShowInfomation("等待手指拿开-");
				goto BEIG2;
			}
			else if (ret == 1)
			{
				ShowInfomation(Fingerdll.ZAZErr2Strt(ret));
				return 0;
			}
			return 1;
		}

		/// <summary>
		/// 显示指纹图像
		/// </summary>
		/// <param name="bmpFileName"></param>
		private void ShowImage(string bmpFileName)
		{
			InvokeEx(() =>
			{
				FileStream pFileStream = new FileStream(bmpFileName, FileMode.Open, FileAccess.Read);
				picFinger.Image = Image.FromStream(pFileStream);
				pFileStream.Close();
				pFileStream.Dispose();
			});
		}

		/// <summary>
		/// 加载指纹dat文件
		/// </summary>
		/// <param name="entity"></param>
		private void LoadFingerDat(CmcsFinger entity)
		{
			if (!IsConnect)
			{
				ShowInfomation("请先打开设备...");
				return;
			}
			ret = Fingerdll.ZAZDownCharFromFile(hHandle, nDevAddr, 1, entity.FingerUrl);
			ret = Fingerdll.ZAZDownCharFromFile(hHandle, nDevAddr, 2, entity.FingerUrl);
			if (ret == 0)
			{
				byte[] ImgData = new byte[ImageSize];
				int[] ImgLen = new int[1];
				ret = Fingerdll.ZAZUpImage(hHandle, nDevAddr, ImgData, ImgLen);  //上传图象
				if (ret != 0)
				{
					ShowInfomation(Fingerdll.ZAZErr2Strt(ret));
					return;
				}
				strFile = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "ZAZFinger.bmp");
				ret = Fingerdll.ZAZImgData2BMP(ImgData, strFile);
				if (ret != 0)
				{
					ShowInfomation(Fingerdll.ZAZErr2Strt(ret), eOutputType.Error);
					return;
				}
				ShowImage(strFile);
			}
		}
		#endregion

		#region 其它方法
		/// <summary>
		/// 上传指纹数据到服务器共享文件夹
		/// </summary>
		private void UploadCapturePicture()
		{
			string serverPath = commonDAO.GetCommonAppletConfigString("指纹识别数据存放路径");
			if (string.IsNullOrEmpty(serverPath) || this.waitForUpload == null || this.waitForUpload.Count == 0) return;

			string fileName = string.Empty;
			do
			{
				fileName = this.waitForUpload.Dequeue();
				if (!string.IsNullOrEmpty(fileName))
				{
					try
					{
						if (File.Exists(serverPath))
						{
							File.Copy(fileName, Path.Combine(serverPath, Path.GetFileName(fileName)), true);
							Log4Neter.Info(string.Format("上传指纹文件{0}", fileName));
						}
					}
					catch (Exception ex)
					{
						Log4Neter.Error("上传指纹文件", ex);

						break;
					}
				}

			} while (fileName != null);
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

		/// <summary>
		/// 信息输出
		/// </summary>
		/// <param name="messageStr"></param>
		public void ShowInfomation(string messageStr, eOutputType outputType = eOutputType.Normal)
		{
			InvokeEx(() =>
			{
				rTxtOutputer.Output(messageStr);
			});
		}
		#endregion

		#region DataGridView

		private void superGridControl1_BeginEdit(object sender, DevComponents.DotNetBar.SuperGrid.GridEditEventArgs e)
		{
			// 取消编辑
			e.Cancel = true;
		}

		private void superGridControl1_CellMouseDown(object sender, DevComponents.DotNetBar.SuperGrid.GridCellMouseEventArgs e)
		{
			CmcsFinger entity = Dbers.GetInstance().SelfDber.Get<CmcsFinger>(superGridControl1.PrimaryGrid.GetCell(e.GridCell.GridRow.Index, superGridControl1.PrimaryGrid.Columns["clmId"].ColumnIndex).Value.ToString());
			switch (superGridControl1.PrimaryGrid.Columns[e.GridCell.ColumnIndex].Name)
			{
				case "clmEdit":
					cmbFingerName.Text = entity.FingerName;
					LoadFingerDat(entity);
					btn_eroll_Click(null, null);
					break;
				case "clmDel":
					if (MessageBoxEx.Show("确定删除该条记录？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
					{
						fingerIdentifyDAO.DelFinger(entity);
						BindData();
					}
					break;
			}
		}

		private void superGridControl1_DataBindingComplete(object sender, DevComponents.DotNetBar.SuperGrid.GridDataBindingCompleteEventArgs e)
		{
			foreach (GridRow gridRow in e.GridPanel.Rows)
			{
				CmcsFinger entity = gridRow.DataItem as CmcsFinger;
				if (entity == null) return;
				gridRow.Cells["clmUserAccount"].Value = this.User.UserAccount;
				gridRow.Cells["clmUserName"].Value = this.User.UserName;
			}
		}
		/// <summary>
		/// 设置行号
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void superGridControl_GetRowHeaderText(object sender, DevComponents.DotNetBar.SuperGrid.GridGetRowHeaderTextEventArgs e)
		{
			e.Text = (e.GridRow.RowIndex + 1).ToString();
		}
		#endregion

	}
}
