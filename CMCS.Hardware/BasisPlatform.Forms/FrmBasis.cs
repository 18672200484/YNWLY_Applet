using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BasisPlatform.Forms
{
	public partial class FrmBasis : DevComponents.DotNetBar.Metro.MetroForm
	{
		private FrmIdentityVerify _FrmIdentityVerify = null;

		[Description("是否后台运行"), Category("自定义")]
		private bool isSecretRunning = true;
		[Description("是否在窗体关闭之前进行验证"), Category("自定义")]
		private bool verifyBeforeClose = true;

		public FrmBasis()
		{
			InitializeComponent();
		}

		//#region MyRegion

		//private bool _IsSecretRunning = true;

		//[Description("是否后台运行"), Category("自定义")]
		//public bool IsSecretRunning
		//{
		//    get { return _IsSecretRunning; }
		//    set { _IsSecretRunning = value; }
		//}

		//private bool _VerifyBeforeClose = true;

		//[Description("是否在窗体关闭之前进行验证"), Category("自定义")]
		//public bool VerifyBeforeClose
		//{
		//    get { return _VerifyBeforeClose; }
		//    set { _VerifyBeforeClose = value; }
		//}

		//private void FrmBasis_Resize(object sender, EventArgs e)
		//{
		//    if (IsSecretRunning && this.WindowState == FormWindowState.Minimized)
		//    {
		//        this.notifyIcon1.Visible = true;
		//        this.notifyIcon1.Text = this.Text;
		//        base.Visible = false;
		//    }
		//    else
		//    {
		//        this.notifyIcon1.Visible = false;
		//    }
		//}

		//private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
		//{
		//    this.notifyIcon1.Visible = false;
		//    base.Show();
		//    base.WindowState = FormWindowState.Normal;
		//}

		//private void FrmBasis_FormClosing(object sender, FormClosingEventArgs e)
		//{
		//    if (VerifyBeforeClose && new FrmIdentityVerify().ShowDialog() != DialogResult.OK)
		//        return;
		//}

		//protected void InvokeAction(Action action)
		//{
		//    action();
		//}

		//[Description("定义托盘图标"), Category("自定义")]
		//protected void ChangeNotifyIcon(Icon icon)
		//{
		//    this.notifyIcon1.Icon = icon;
		//}

		//private void 退出ToolStripMenuItem_Click(object sender, EventArgs e)
		//{
		//    if (VerifyBeforeClose && new FrmIdentityVerify().ShowDialog() == DialogResult.OK)
		//        this.Close();
		//}
		//#endregion


		private void FrmBasis_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (((e.CloseReason != CloseReason.ApplicationExitCall) && this.verifyBeforeClose) && (this.ShowFrmIdentityVerify() != DialogResult.Yes))
			{
				e.Cancel = true;
			}
		}

		private void FrmBasis_SizeChanged(object sender, EventArgs e)
		{
			this.notifyIcon1.Text = this.Text;
			if (this.isSecretRunning && (base.WindowState == FormWindowState.Minimized))
			{
				base.Hide();
				this.notifyIcon1.Visible = true;
				this.ShowInTaskbar = false;
			}
		}

		protected void InvokeAction(Action action)
		{
			if (base.Created && !base.Disposing)
			{
				base.Invoke(action);
			}
		}

		private void notifyIcon1_MouseDoubleClick(object sender, MouseEventArgs e)
		{
			if (this.isSecretRunning || (this.isSecretRunning && this.verifyBeforeClose && this.ShowFrmIdentityVerify() == DialogResult.Yes))
			{
				this.notifyIcon1.Visible = false;
				base.Show();
				base.WindowState = FormWindowState.Normal;
				this.ShowInTaskbar = true;
			}
		}

		private DialogResult ShowFrmIdentityVerify()
		{
			if (Application.OpenForms.OfType<Form>().All<Form>(a => a.GetType().FullName != typeof(FrmIdentityVerify).FullName))
			{
				this._FrmIdentityVerify = new FrmIdentityVerify();
				return this._FrmIdentityVerify.ShowDialog();
			}
			this._FrmIdentityVerify.Activate();
			return DialogResult.None;
		}

		[Description("定义托盘图标"), Category("自定义")]
		protected void ChangeNotifyIcon(Icon icon)
		{
			this.notifyIcon1.Icon = icon;
		}

		[Category("自定义"), Description("是否后台运行")]
		public bool IsSecretRunning
		{
			get
			{
				return this.isSecretRunning;
			}
			set
			{
				this.isSecretRunning = value;
			}
		}

		[Description("是否在窗体关闭之前进行验证"), Category("自定义")]
		public bool VerifyBeforeClose
		{
			get
			{
				return this.verifyBeforeClose;
			}
			set
			{
				this.verifyBeforeClose = value;
			}
		}

	}
}
