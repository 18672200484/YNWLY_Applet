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
		/// ����Ψһ��ʶ��
		/// </summary>
		public static string UniqueKey = "FrmFingerRegist";

		#region ҵ������
		CommonDAO commonDAO = CommonDAO.GetInstance();
		FingerIdentifyDAO fingerIdentifyDAO = FingerIdentifyDAO.GetInstance();
		TaskSimpleScheduler taskSimpleScheduler = new TaskSimpleScheduler();

		#endregion

		#region ����Vars

		RTxtOutputer rTxtOutputer;

		/// <summary>
		/// �ȴ��ϴ���ץ��
		/// </summary>
		Queue<string> waitForUpload = new Queue<string>();

		User user;
		/// <summary>
		/// ��ǰ�û�
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

		#region �豸����
		Int32 fingerIndex;
		/// <summary>
		/// ��ǰָ�Ʊ��
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
		/// �豸����״̬
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
		/// ��ǰ���Ӿ��
		/// </summary>
		IntPtr hHandle = new IntPtr();

		/// <summary>
		/// �豸��ַ
		/// </summary>
		private UInt32 nDevAddr = 0xffffffff;

		/// <summary>
		/// ͼ���С
		/// </summary>
		public const int ImageSize = (256 * 288);

		/// <summary>
		/// ִ�н��
		/// </summary>
		private int ret = 0;

		private Int32 timeout = 20;
		/// <summary>
		/// �ȴ���ʱ
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
		/// dat�ļ�·��
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
		/// �����ʼ��
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
		/// ��ʼ������豸
		/// </summary>
		public void InitHardware()
		{
			int ret = 0;
			ret = Fingerdll.ZAZOpenDeviceEx(ref hHandle, 2, 0, 0, 2, 0);
			if (ret == 0)
			{
				this.IsConnect = true;
				ShowInfomation("�豸�򿪳ɹ�");
				btn_eroll_Click(null, null);
			}
			else
			{
				this.IsConnect = false;
				ShowInfomation(Fingerdll.ZAZErr2Strt(ret));
			}
		}

		/// <summary>
		/// �ر��豸
		/// </summary>
		public void CloseHardware()
		{
			int ret = 0;
			ret = Fingerdll.ZAZCloseDeviceEx(hHandle);
			if (ret == 0)
			{
				this.IsConnect = true;
				ShowInfomation("�豸�رճɹ�");
			}
			else
			{
				this.IsConnect = false;
				ShowInfomation(Fingerdll.ZAZErr2Strt(ret));
			}
		}

		#region Button�¼�
		private void BindData()
		{
			superGridControl1.PrimaryGrid.DataSource = fingerIdentifyDAO.GetFingerByUserId(this.User.PartyId);
		}

		private void btn_eroll_Click(object sender, EventArgs e)
		{
			//������ȡָ��ͼ����������߳�
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

		#region ָ��ʶ�𷽷�
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
		/// ע��ָ��
		/// </summary>
		private void RegistFinger()
		{
			if (!IsConnect)
			{
				ShowInfomation("���ȴ��豸...");
				return;
			}
			//��������A  
			if (GetFinger(1) != 1)
			{
				return;
			}
			Thread.Sleep(200);
			//��������B
			if (GetFinger(2) != 1)
			{
				return;
			}

			Thread.Sleep(200);
			/****************�ϳ�ģ��*********/
			ret = Fingerdll.ZAZRegModule(hHandle, nDevAddr);  //�ϲ�����
			if (ret != 0)
			{
				ShowInfomation(Fingerdll.ZAZErr2Strt(ret));
				return;
			}
			else
			{
				ShowInfomation("�ϳ�ָ��ģ��ɹ�");
			}
			Thread.Sleep(200);

			//�����Դ�����ָ���豸���н���
			//ret = Fingerdll.ZAZStoreChar(hHandle, nDevAddr, 1, FingerIndex);    //���ģ��

			//StrFile = System.Windows.Forms.Application.StartupPath + "\\FTmpelet.dat";
			#region �洢ָ�Ƶ�������
			CmcsFinger userfinger = new CmcsFinger();
			InvokeEx(() =>
			{
				StrFile = commonDAO.GetCommonAppletConfigString("ָ��ʶ�����ݴ��·��");
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
				//��ָ����Ϣд�뵽dat�ļ�
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
						ShowInfomation("�洢ָ�Ƴɹ�");
						//showFpdb();��ʾ��ǰ�豸ָ�ƴ洢��Ϣ
						FingerIndex++;
						BindData();
						return;
					}
				}
			});
		}

		/// <summary>
		/// ��ȡָ��
		/// </summary>
		/// <param name="buffer">ָ�����豸����ʱ���λ�� 1 2</param>
		/// <returns></returns>
		int GetFinger(int buffer)
		{
			TimeOut = 20;
			int ret = 0;
			byte[] ImgData = new byte[ImageSize];
			int[] ImgLen = new int[1];
			int iBuffer = buffer;

			BEIG1:
			ret = Fingerdll.ZAZGetImage(hHandle, nDevAddr);  //��ȡͼ�� 
			if (ret == 0)
			{
				ShowInfomation("��ȡͼ��ɹ�...");
			}
			else if (ret == 2)
			{
				//��ʱ�ж�
				ShowInfomation("�ȴ���ָƽ���ڴ�������-" + TimeOut.ToString() + "��");
				if (TimeOut < 0)
				{
					ShowInfomation("�ȴ���ʱ");
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
			//���漰ͼ���������ʡ��
			/****************�ϴ�ͼ��*********/

			ShowInfomation("�����ϴ�ͼ����ȴ�...");
			ret = Fingerdll.ZAZUpImage(hHandle, nDevAddr, ImgData, ImgLen);  //�ϴ�ͼ��
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
			/****************�������� *********/
			ret = Fingerdll.ZAZGenChar(hHandle, nDevAddr, iBuffer);  //����ģ��
			if (ret != 0)
			{
				ShowInfomation(Fingerdll.ZAZErr2Strt(ret));
				return 0;
			}
			else
			{
				ShowInfomation("����ָ������" + buffer.ToString());
			}
			Thread.Sleep(10);
			BEIG2:
			if (ret == 0)
			{
				ret = Fingerdll.ZAZGetImage(hHandle, nDevAddr);  //��ȡͼ�� 
				ShowInfomation("�ȴ���ָ�ÿ�-");
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
		/// ��ʾָ��ͼ��
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
		/// ����ָ��dat�ļ�
		/// </summary>
		/// <param name="entity"></param>
		private void LoadFingerDat(CmcsFinger entity)
		{
			if (!IsConnect)
			{
				ShowInfomation("���ȴ��豸...");
				return;
			}
			ret = Fingerdll.ZAZDownCharFromFile(hHandle, nDevAddr, 1, entity.FingerUrl);
			ret = Fingerdll.ZAZDownCharFromFile(hHandle, nDevAddr, 2, entity.FingerUrl);
			if (ret == 0)
			{
				byte[] ImgData = new byte[ImageSize];
				int[] ImgLen = new int[1];
				ret = Fingerdll.ZAZUpImage(hHandle, nDevAddr, ImgData, ImgLen);  //�ϴ�ͼ��
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

		#region ��������
		/// <summary>
		/// �ϴ�ָ�����ݵ������������ļ���
		/// </summary>
		private void UploadCapturePicture()
		{
			string serverPath = commonDAO.GetCommonAppletConfigString("ָ��ʶ�����ݴ��·��");
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
							Log4Neter.Info(string.Format("�ϴ�ָ���ļ�{0}", fileName));
						}
					}
					catch (Exception ex)
					{
						Log4Neter.Error("�ϴ�ָ���ļ�", ex);

						break;
					}
				}

			} while (fileName != null);
		}

		/// <summary>
		/// Invoke��װ
		/// </summary>
		/// <param name="action"></param>
		public void InvokeEx(Action action)
		{
			if (this.IsDisposed || !this.IsHandleCreated) return;

			this.Invoke(action);
		}

		/// <summary>
		/// ��Ϣ���
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
			// ȡ���༭
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
					if (MessageBoxEx.Show("ȷ��ɾ��������¼��", "��ʾ", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
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
		/// �����к�
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
