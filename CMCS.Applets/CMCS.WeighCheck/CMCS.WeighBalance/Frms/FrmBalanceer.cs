using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using CMCS.Common;
using CMCS.Common.DAO;
using CMCS.Common.Entities;
using CMCS.Common.Entities.AutoMaker;
using CMCS.Common.Entities.BaseInfo;
using CMCS.Common.Entities.BeltSampler;
using CMCS.Common.Entities.Fuel;
using CMCS.Common.Entities.Inf;
using CMCS.Common.Enums;
using CMCS.Common.Utilities;
using CMCS.Forms.UserControls;
using CMCS.WeighBalance.DAO;
using CMCS.WeighBalance.Enums;
using CMCS.WeighBalance.Frms;
using CMCS.WeighBalance.Utilities;
//
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Metro;
using DevComponents.DotNetBar.SuperGrid;
using CMCS.Common.Entities.Balance;
using DevComponents.DotNetBar.Controls;
using CMCS.DapperDber.Dbs.SqlServerDb;
using CMCS.WeighBalance.Entities;

namespace CMCS.WeighBalance.Frms
{
	public partial class FrmBalanceer : MetroForm
	{
		public FrmBalanceer()
		{
			InitializeComponent();
		}
		public FrmBalanceer(string machineCode)
		{
			InitializeComponent();
			this.MachineCode = machineCode;
		}

		/// <summary>
		/// 窗体唯一标识符
		/// </summary>
		public static string UniqueKey = "FrmBalanceer";

		#region Vars

		CommonDAO commonDAO = CommonDAO.GetInstance();
		BeltSamplerDAO beltSamplerDAO = BeltSamplerDAO.GetInstance();

		string machineCode;
		/// <summary>
		/// 当前设备编号
		/// </summary>
		public string MachineCode
		{
			get { return machineCode; }
			set { machineCode = value; }
		}

		InfBalanceRecord currentAssay;
		/// <summary>
		/// 当前选中的化验记录
		/// </summary>
		public InfBalanceRecord CurrentAssay
		{
			get { return currentAssay; }
			set
			{
				currentAssay = value;
				if (value != null)
				{
					// 加载明细
					LoadBalanceDetailList(superGridControl2, value.Id);
					foreach (GridRow item in superGridControl1.PrimaryGrid.Rows)
					{
						InfBalanceRecord entity = item.DataItem as InfBalanceRecord;
						if (entity.Id == this.CurrentAssay.Id)
						{
							item.Cells["clmCheck"].Value = 1;
						}
						else
							item.Cells["clmCheck"].Value = 0;
					}
				}
			}
		}

		RTxtOutputer rTxtOutputer;

		#endregion

		/// <summary>
		/// 窗体初始化
		/// </summary>
		private void FormInit()
		{
			rTxtOutputer = new RTxtOutputer(rTxTMessageInfo);
		}

		private void FrmUnloadSampler_Load(object sender, EventArgs e)
		{
			FormInit();
			InitHardware();
			LoadBalanceList(superGridControl1);
			InitAssayType(cmbType);
		}

		/// <summary>
		/// 初始化类型
		/// </summary>
		/// <param name="cmb"></param>
		private void InitAssayType(ComboBoxEx cmb)
		{
			cmb.Items.Clear();

			cmb.DisplayMember = "Text";
			cmb.ValueMember = "Value";

			cmb.Items.Add(new ComboBoxItem("FRL", "热值"));
			cmb.Items.Add(new ComboBoxItem("CLY", "测硫"));
			cmb.Items.Add(new ComboBoxItem("CHN", "碳氢氮"));

			cmb.SelectedIndex = 0;
		}

		private void Form1_FormClosing(object sender, FormClosingEventArgs e)
		{

		}

		#region 电子天平

		double currentWeight = 0;
		/// <summary>
		/// 电子秤当前重量
		/// </summary>
		public double CurrentWeight
		{
			get { return currentWeight; }
			set { currentWeight = value; }
		}

		/// <summary>
		/// 电子秤
		/// </summary>
		WB.Sartorius.Balance.Sartorius_Balance wber = new WB.Sartorius.Balance.Sartorius_Balance();

		bool isUseWeight = true;
		/// <summary>
		/// 启用电子秤
		/// </summary>
		public bool IsUseWeight
		{
			get { return isUseWeight; }
			set
			{
				isUseWeight = value;

				lblWber.Visible = value;
				slightWber.Visible = value;
			}
		}

		bool wbRunStatus = false;
		/// <summary>
		/// 电子秤连接状态
		/// </summary>
		public bool WbRunStatus
		{
			get { return wbRunStatus; }
			set { wbRunStatus = value; }
		}

		/// <summary>
		/// 电子秤状态变化
		/// </summary>
		/// <param name="status"></param>
		void wber_OnStatusChange(string machineCode, bool status)
		{
			// 接收设备状态 
			InvokeEx(() =>
			{
				this.WbRunStatus = status;

				slightWber.LightColor = (status ? Color.Green : Color.Red);
			});
		}

		/// <summary>
		/// 电子秤重量变化
		/// </summary>
		/// <param name="status"></param>
		void wber_OnWeightChange(string machineCode, double weight)
		{
			// 接收重量 
			InvokeEx(() =>
			{
				if (weight > 0)
					JoinBalanceDetail(weight);
			});
		}

		#endregion

		#region 设备初始化与卸载

		/// <summary>
		/// 初始化外接设备
		/// </summary>
		private void InitHardware()
		{
			try
			{
				bool success = false;

				wber.OnStatusChange += new WB.Sartorius.Balance.Sartorius_Balance.StatusChangeHandler(wber_OnStatusChange);
				wber.OnWeightChange += new WB.Sartorius.Balance.Sartorius_Balance.WeightChangeEventHandler(wber_OnWeightChange);

				success = wber.OpenCom(commonDAO.GetAppletConfigInt32(string.Format("{0}串口", MachineCode)), commonDAO.GetAppletConfigInt32(string.Format("{0}波特率", MachineCode)), commonDAO.GetAppletConfigInt32(string.Format("{0}数据位", MachineCode)), commonDAO.GetAppletConfigInt32(string.Format("{0}校验位", MachineCode)));

				timer1.Enabled = true;
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

			try
			{
				wber.CloseCom();
			}
			catch
			{ }
		}
		#endregion

		#region 业务

		/// <summary>
		/// 加载今日已录入化验数据
		/// </summary>
		/// <param name="superGridControl"></param>
		private void LoadBalanceList(SuperGridControl superGridControl)
		{
			List<InfBalanceRecord> list = commonDAO.SelfDber.Entities<InfBalanceRecord>("where trunc(OperDate)=trunc(sysdate) and MachineCode=:MachineCode order by CreateDate asc", new { MachineCode = this.MachineCode });
			superGridControl.PrimaryGrid.DataSource = list;
			if (list == null || list.Count == 0) return;
			this.CurrentAssay = list[0];
		}

		/// <summary>
		/// 根据天平主表Id加载详细重量信息
		/// </summary>
		/// <param name="superGridControl"></param>
		/// <param name="batchId"></param>
		private void LoadBalanceDetailList(SuperGridControl superGridControl, string balanceId)
		{
			List<InfBalanceRecordDetail> list = commonDAO.SelfDber.Entities<InfBalanceRecordDetail>("where BalanceRecordId=:BalanceRecordId order by CreateDate", new { BalanceRecordId = balanceId });
			superGridControl.PrimaryGrid.DataSource = list;
		}

		/// <summary>
		/// 录入化验数据
		/// </summary>
		/// <param name="assayCode"></param>
		/// <returns></returns>
		private bool JoinBalance(string assayCode)
		{
			InfBalanceRecord entity = commonDAO.SelfDber.Entity<InfBalanceRecord>("where AssayCode=:AssayCode and  MachineCode=:MachineCode order by CreateDate desc", new { AssayCode = assayCode, MachineCode = this.MachineCode });
			if (entity == null)
			{
				entity = new InfBalanceRecord();
				entity.CreateUser = SelfVars.LoginUser.UserAccount;
				entity.OperUser = entity.CreateUser;
				entity.MachineCode = this.MachineCode;
				entity.AssayCode = assayCode;
				entity.AssayType = ((ComboBoxItem)cmbType.SelectedItem).Name;
				commonDAO.SelfDber.Insert(entity);
			}
			else
			{
				entity.OperDate = DateTime.Now;
				entity.OperUser = entity.CreateUser;
				entity.MachineCode = this.MachineCode;
				entity.AssayType = ((ComboBoxItem)cmbType.SelectedItem).Name;
				commonDAO.SelfDber.Update(entity);
			}
			LoadBalanceList(superGridControl1);
			this.CurrentAssay = entity;
			return true;
		}

		/// <summary>
		/// 录入明细
		/// </summary>
		/// <param name="weight"></param>
		/// <returns></returns>
		private bool JoinBalanceDetail(double weight)
		{
			if (string.IsNullOrEmpty(this.txtGGJ.Text))
			{
				rTxtOutputer.Output("请填写坩埚架", eOutputType.Warn);
				return false;
			}
			if (this.CurrentAssay == null)
			{
				rTxtOutputer.Output("请选中一条化验记录", eOutputType.Error);
				return false;
			}
			string GGNo = "1";
			if (commonDAO.SelfDber.Count<InfBalanceRecordDetail>("where GGJCode=:GGJCode and SyncFlag=0", new { BalanceRecordId = this.CurrentAssay.Id, GGJCode = this.txtGGJ.Text.Trim() }) >= 12)
			{
				rTxtOutputer.Output("坩埚架：" + this.txtGGJ.Text + "的坩埚已用完", eOutputType.Warn);
				return false;
			}
			InfBalanceRecordDetail lastdetail = commonDAO.SelfDber.Entity<InfBalanceRecordDetail>("where GGJCode=:GGJCode and SyncFlag=0 order by to_number(GGCode) desc", new { BalanceRecordId = this.CurrentAssay.Id, GGJCode = this.txtGGJ.Text.Trim() });

			if (lastdetail != null)
			{
				GGNo = (Convert.ToInt32(lastdetail.GGCode) + 1).ToString();
			}
			InfBalanceRecordDetail detial = new InfBalanceRecordDetail();
			detial.CreateUser = SelfVars.LoginUser.UserAccount;
			detial.OperUser = detial.CreateUser;
			detial.BalanceRecordId = this.CurrentAssay.Id;
			detial.Weight = weight;
			detial.AssayCode = this.CurrentAssay.AssayCode;
			detial.MachineCode = this.MachineCode;
			detial.GGCode = GGNo;
			detial.GGJCode = this.txtGGJ.Text.Trim();
			detial.SyncFlag = 0;
			commonDAO.SelfDber.Insert(detial);
			LoadBalanceDetailList(superGridControl2, this.CurrentAssay.Id);
			return true;
		}

		private void txtInputAssayCode_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				if (!String.IsNullOrEmpty(txtInputAssayCode.Text.Trim()))
				{
					if (string.IsNullOrEmpty(this.txtGGJ.Text))
					{
						rTxtOutputer.Output("请填写坩埚架", eOutputType.Warn);
						return;
					}
					JoinBalance(txtInputAssayCode.Text.Trim());
				}
			}
		}
		#endregion

		#region SuperGridControl

		private void superGridControl1_Click(object sender, EventArgs e)
		{
			GridRow gridRow = (superGridControl1.ActiveRow as GridRow);
			InfBalanceRecord balanceRecord = gridRow.DataItem as InfBalanceRecord;
			if (!string.IsNullOrEmpty(balanceRecord.AssayCode))
			{
				this.CurrentAssay = balanceRecord;
			}
		}

		private void superGridControl1_DataBindingComplete(object sender, GridDataBindingCompleteEventArgs e)
		{
			//第一次加载时选中第一条
			foreach (GridRow item in superGridControl1.PrimaryGrid.Rows)
			{
				InfBalanceRecord entity = item.DataItem as InfBalanceRecord;
				if (entity.Id == this.CurrentAssay.Id)
				{
					item.Cells["clmCheck"].Value = 1;
				}
				else
					item.Cells["clmCheck"].Value = 0;
			}
		}

		private void superGridControl1_BeginEdit(object sender, GridEditEventArgs e)
		{
			// 取消编辑
			e.Cancel = true;
		}

		private void superGridControl2_BeginEdit(object sender, GridEditEventArgs e)
		{
			// 取消编辑
			e.Cancel = true;
		}

		/// <summary>
		/// 设置行号
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void superGridControl_GetRowHeaderText(object sender, GridGetRowHeaderTextEventArgs e)
		{
			e.Text = (e.GridRow.RowIndex + 1).ToString();
		}

		private void superGridControl1_CellMouseDown(object sender, GridCellMouseEventArgs e)
		{
			switch (superGridControl1.PrimaryGrid.Columns[e.GridCell.ColumnIndex].Name)
			{
				case "clmDelete":
					InfBalanceRecord entity = commonDAO.SelfDber.Get<InfBalanceRecord>(superGridControl1.PrimaryGrid.GetCell(e.GridCell.GridRow.Index, superGridControl1.PrimaryGrid.Columns["clmId"].ColumnIndex).Value.ToString());
					if (entity == null)
					{
						MessageBoxEx.Show("该记录已不存在，刷新重试！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return;
					}
					if (MessageBoxEx.Show("确定要删除该记录？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
					{
						try
						{
							commonDAO.SelfDber.DeleteBySQL<InfBalanceRecordDetail>("where BalanceRecordId=:BalanceRecordId", new { BalanceRecordId = entity.Id });
							commonDAO.SelfDber.Delete<InfBalanceRecord>(entity.Id);
							CommonDAO.GetInstance().SaveAppletLog(eAppletLogLevel.Warn, "删除天平数据", string.Format("化验码:{0};操作人:{1}", entity.AssayCode, SelfVars.LoginUser.UserName));
							LoadBalanceList(superGridControl1);
						}
						catch (Exception ex)
						{
							MessageBoxEx.Show("该记录正在使用中，禁止删除！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						}
					}
					break;
			}
		}

		private void superGridControl2_CellMouseDown(object sender, GridCellMouseEventArgs e)
		{
			switch (superGridControl2.PrimaryGrid.Columns[e.GridCell.ColumnIndex].Name)
			{
				case "clmDelete":
					InfBalanceRecordDetail entity = commonDAO.SelfDber.Get<InfBalanceRecordDetail>(superGridControl2.PrimaryGrid.GetCell(e.GridCell.GridRow.Index, superGridControl2.PrimaryGrid.Columns["clmId"].ColumnIndex).Value.ToString());
					if (entity == null)
					{
						MessageBoxEx.Show("该记录已不存在，刷新重试！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						return;
					}
					if (MessageBoxEx.Show("确定要删除该记录？", "提示", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
					{
						try
						{
							commonDAO.SelfDber.Delete<InfBalanceRecordDetail>(entity.Id);
							CommonDAO.GetInstance().SaveAppletLog(eAppletLogLevel.Warn, "删除天平明细数据", string.Format("化验码:{0};坩埚号:{1};操作人:{2}", entity.AssayCode, entity.GGCode, SelfVars.LoginUser.UserName));
							LoadBalanceDetailList(superGridControl2, this.CurrentAssay.Id);
						}
						catch (Exception ex)
						{
							MessageBoxEx.Show("删除失败：" + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
						}
					}
					break;
			}
		}

		private void superGridControl2_DataBindingComplete(object sender, GridDataBindingCompleteEventArgs e)
		{
			foreach (GridRow item in superGridControl2.PrimaryGrid.Rows)
			{
				InfBalanceRecordDetail entity = item.DataItem as InfBalanceRecordDetail;
				item.Cells["clmDetailStatus"].Value = entity.SyncFlag == 0 ? "未提交" : "已提交";
				if (entity.SyncFlag == 1)
					item.Cells["clmDetailCheck"].ReadOnly = true;
			}
		}

		#endregion

		#region 其他函数

		/// <summary>
		/// Invoke封装
		/// </summary>
		/// <param name="action"></param>
		public void InvokeEx(Action action)
		{
			if (this.IsDisposed || !this.IsHandleCreated) return;

			this.Invoke(action);
		}

		#endregion

		private void btnSubmit_Click(object sender, EventArgs e)
		{
			List<InfBalanceRecordDetail> details = new List<InfBalanceRecordDetail>();
			foreach (GridRow item in superGridControl2.PrimaryGrid.Rows)
			{
				InfBalanceRecordDetail entity = item.DataItem as InfBalanceRecordDetail;
				if (Convert.ToBoolean(item.Cells["clmDetailCheck"].Value))
				{
					details.Add(entity);
				}
			}
			if (details.Count == 0)
			{
				MessageBoxEx.Show("请选择数据！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				return;
			}
			try
			{
				int res = 0;
				SqlServerDapperDber sqlServerDapper = new SqlServerDapperDber(commonDAO.GetCommonAppletConfigString("开元天平接口数据连接字符串"));
				foreach (InfBalanceRecordDetail detail in details)
				{
					TPWeight weight = sqlServerDapper.Entity<TPWeight>("where S_NO=@S_NO and TP_Type=@TP_Type", new { S_NO = detail.TheBalanceRecord.AssayCode + "-" + detail.GGCode, TP_Type = detail.TheBalanceRecord.AssayType });
					if (weight == null)
					{
						weight = new TPWeight();
						weight.S_NO = detail.TheBalanceRecord.AssayCode + "-" + detail.GGCode;
						weight.TP_Type = detail.TheBalanceRecord.AssayType;
						weight.Weight = detail.Weight;
						weight.TP_NO = detail.TheBalanceRecord.MachineCode;
						weight.Stateop = 1;
						weight.SortNumber = detail.GGCode;
						weight.GG_NO = detail.GGCode;
						weight.Sample_NO = detail.TheBalanceRecord.AssayCode;
						weight.CreateTime = DateTime.Now;
						weight.Creator = detail.CreateUser;
						sqlServerDapper.Insert(weight);

						detail.SyncFlag = 1;
						commonDAO.SelfDber.Update(detail);
						res++;
					}
					else
					{
						if (MessageBoxEx.Show("该化验编码重量已提交，是否覆盖！", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning) == DialogResult.OK)
						{
							weight.S_NO = detail.TheBalanceRecord.AssayCode + "-" + detail.GGCode;
							weight.TP_Type = detail.TheBalanceRecord.AssayType;
							weight.Weight = detail.Weight;
							weight.TP_NO = detail.TheBalanceRecord.MachineCode;
							weight.Stateop = 1;
							weight.GG_NO = detail.GGCode;
							weight.Sample_NO = detail.TheBalanceRecord.AssayCode;
							sqlServerDapper.Update(weight);
							res++;
						}
					}
				}
				MessageBoxEx.Show("成功提交" + res + "条数据", "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
				LoadBalanceDetailList(superGridControl2, this.CurrentAssay.Id);
			}
			catch (Exception ex)
			{
				MessageBoxEx.Show("提交失败：" + Environment.NewLine + ex.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Warning);
			}
		}

	}
}
