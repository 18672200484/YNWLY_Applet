namespace CMCS.UnloadSampler
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn1 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn2 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn3 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn4 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn5 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn6 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn7 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn8 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn9 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn10 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn11 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn12 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn13 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            DevComponents.DotNetBar.SuperGrid.GridColumn gridColumn14 = new DevComponents.DotNetBar.SuperGrid.GridColumn();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.superGridControl1 = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
            this.lypanSamplerButton = new System.Windows.Forms.FlowLayoutPanel();
            this.label1 = new System.Windows.Forms.Label();
            this.flpanUnloadType = new System.Windows.Forms.FlowLayoutPanel();
            this.rbtnToSubway = new System.Windows.Forms.RadioButton();
            this.rbtnToMaker = new System.Windows.Forms.RadioButton();
            this.btnSendLoadCmd = new DevComponents.DotNetBar.ButtonX();
            this.panelEx1 = new DevComponents.DotNetBar.PanelEx();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.lblCurrSampleCode = new System.Windows.Forms.Label();
            this.lblCurrSendTime = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.lblCurrUnloadType = new System.Windows.Forms.Label();
            this.lblCurrSamplerName = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.lblCurrResultCode = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.lblSampleType = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.lblSampleCode = new System.Windows.Forms.Label();
            this.label112 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.superGridControl2 = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
            this.panelEx2 = new DevComponents.DotNetBar.PanelEx();
            this.rTxTMessageInfo = new DevComponents.DotNetBar.Controls.RichTextBoxEx();
            this.timer2 = new System.Windows.Forms.Timer(this.components);
            this.metroStatusBar1 = new DevComponents.DotNetBar.Metro.MetroStatusBar();
            this.labelItem1 = new DevComponents.DotNetBar.LabelItem();
            this.lblVersion = new DevComponents.DotNetBar.LabelItem();
            this.labelItem3 = new DevComponents.DotNetBar.LabelItem();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.flpanEquState = new System.Windows.Forms.FlowLayoutPanel();
            this.panel1 = new System.Windows.Forms.Panel();
            this.superGridControl3 = new DevComponents.DotNetBar.SuperGrid.SuperGridControl();
            this.panelEx4 = new DevComponents.DotNetBar.PanelEx();
            this.btnSendMakeCmd = new DevComponents.DotNetBar.ButtonX();
            this.panelEx3 = new DevComponents.DotNetBar.PanelEx();
            this.label6 = new System.Windows.Forms.Label();
            this.lblCurrMakeMachine = new System.Windows.Forms.Label();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.flpanUnloadType.SuspendLayout();
            this.panelEx1.SuspendLayout();
            this.panelEx2.SuspendLayout();
            this.tableLayoutPanel1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.panelEx4.SuspendLayout();
            this.panelEx3.SuspendLayout();
            this.SuspendLayout();
            // 
            // superGridControl1
            // 
            this.superGridControl1.BackColor = System.Drawing.Color.White;
            this.superGridControl1.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
            this.superGridControl1.ForeColor = System.Drawing.Color.Black;
            this.superGridControl1.Location = new System.Drawing.Point(6, 53);
            this.superGridControl1.Name = "superGridControl1";
            this.superGridControl1.PrimaryGrid.Caption.Text = "集 样 罐 信 息";
            this.superGridControl1.PrimaryGrid.CheckBoxes = true;
            gridColumn1.MinimumWidth = 20;
            gridColumn1.Name = "";
            gridColumn1.Width = 20;
            gridColumn2.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gridColumn2.DataPropertyName = "BarrelNumber";
            gridColumn2.HeaderText = "罐号";
            gridColumn2.MinimumWidth = 50;
            gridColumn2.Name = "";
            gridColumn2.Width = 50;
            gridColumn3.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gridColumn3.DataPropertyName = "SampleCode";
            gridColumn3.HeaderText = "采样副码";
            gridColumn3.MinimumWidth = 100;
            gridColumn3.Name = "";
            gridColumn4.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gridColumn4.DataPropertyName = "SampleCount";
            gridColumn4.HeaderText = "子样数";
            gridColumn4.MinimumWidth = 80;
            gridColumn4.Name = "";
            gridColumn4.Width = 80;
            gridColumn5.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gridColumn5.DataPropertyName = "BarreStatus";
            gridColumn5.HeaderText = "桶满";
            gridColumn5.MinimumWidth = 60;
            gridColumn5.Name = "";
            gridColumn5.Width = 60;
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn1);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn2);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn3);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn4);
            this.superGridControl1.PrimaryGrid.Columns.Add(gridColumn5);
            this.superGridControl1.PrimaryGrid.DefaultVisualStyles.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            this.superGridControl1.Size = new System.Drawing.Size(386, 189);
            this.superGridControl1.TabIndex = 2;
            this.superGridControl1.Text = "superGridControl1";
            this.superGridControl1.CellClick += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridCellClickEventArgs>(this.superGridControl1_CellClick);
            this.superGridControl1.BeginEdit += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridEditEventArgs>(this.superGridControl1_BeginEdit);
            this.superGridControl1.GetCellStyle += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridGetCellStyleEventArgs>(this.superGridControl1_GetCellStyle);
            // 
            // lypanSamplerButton
            // 
            this.lypanSamplerButton.BackColor = System.Drawing.Color.Transparent;
            this.lypanSamplerButton.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.lypanSamplerButton.ForeColor = System.Drawing.Color.Black;
            this.lypanSamplerButton.Location = new System.Drawing.Point(6, 5);
            this.lypanSamplerButton.Name = "lypanSamplerButton";
            this.lypanSamplerButton.Size = new System.Drawing.Size(1071, 42);
            this.lypanSamplerButton.TabIndex = 4;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(16, 47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(67, 15);
            this.label1.TabIndex = 6;
            this.label1.Text = "卸样方式：";
            // 
            // flpanUnloadType
            // 
            this.flpanUnloadType.BackColor = System.Drawing.Color.Transparent;
            this.flpanUnloadType.Controls.Add(this.rbtnToSubway);
            this.flpanUnloadType.Controls.Add(this.rbtnToMaker);
            this.flpanUnloadType.ForeColor = System.Drawing.Color.Black;
            this.flpanUnloadType.Location = new System.Drawing.Point(14, 79);
            this.flpanUnloadType.Name = "flpanUnloadType";
            this.flpanUnloadType.Size = new System.Drawing.Size(284, 24);
            this.flpanUnloadType.TabIndex = 8;
            // 
            // rbtnToSubway
            // 
            this.rbtnToSubway.AutoSize = true;
            this.rbtnToSubway.Checked = true;
            this.rbtnToSubway.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnToSubway.ForeColor = System.Drawing.Color.Black;
            this.rbtnToSubway.Location = new System.Drawing.Point(3, 3);
            this.rbtnToSubway.Name = "rbtnToSubway";
            this.rbtnToSubway.Size = new System.Drawing.Size(73, 19);
            this.rbtnToSubway.TabIndex = 0;
            this.rbtnToSubway.TabStop = true;
            this.rbtnToSubway.Tag = "0";
            this.rbtnToSubway.Text = "旁路卸样";
            this.rbtnToSubway.UseVisualStyleBackColor = true;
            // 
            // rbtnToMaker
            // 
            this.rbtnToMaker.AutoSize = true;
            this.rbtnToMaker.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rbtnToMaker.ForeColor = System.Drawing.Color.Black;
            this.rbtnToMaker.Location = new System.Drawing.Point(82, 3);
            this.rbtnToMaker.Name = "rbtnToMaker";
            this.rbtnToMaker.Size = new System.Drawing.Size(97, 19);
            this.rbtnToMaker.TabIndex = 1;
            this.rbtnToMaker.Tag = "1";
            this.rbtnToMaker.Text = "卸样到制样机";
            this.rbtnToMaker.UseVisualStyleBackColor = true;
            // 
            // btnSendLoadCmd
            // 
            this.btnSendLoadCmd.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSendLoadCmd.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSendLoadCmd.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSendLoadCmd.Location = new System.Drawing.Point(14, 128);
            this.btnSendLoadCmd.Name = "btnSendLoadCmd";
            this.btnSendLoadCmd.Size = new System.Drawing.Size(284, 23);
            this.btnSendLoadCmd.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSendLoadCmd.TabIndex = 9;
            this.btnSendLoadCmd.Text = "发 送 卸 样 命 令";
            this.btnSendLoadCmd.Click += new System.EventHandler(this.btnSendLoadCmd_Click);
            // 
            // panelEx1
            // 
            this.panelEx1.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx1.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx1.Controls.Add(this.label10);
            this.panelEx1.Controls.Add(this.label9);
            this.panelEx1.Controls.Add(this.lblCurrSampleCode);
            this.panelEx1.Controls.Add(this.lblCurrSendTime);
            this.panelEx1.Controls.Add(this.label7);
            this.panelEx1.Controls.Add(this.lblCurrUnloadType);
            this.panelEx1.Controls.Add(this.lblCurrSamplerName);
            this.panelEx1.Controls.Add(this.label5);
            this.panelEx1.Controls.Add(this.label3);
            this.panelEx1.Controls.Add(this.label2);
            this.panelEx1.Controls.Add(this.lblCurrResultCode);
            this.panelEx1.Location = new System.Drawing.Point(400, 53);
            this.panelEx1.Name = "panelEx1";
            this.panelEx1.Size = new System.Drawing.Size(316, 140);
            this.panelEx1.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx1.Style.BackColor1.Color = System.Drawing.Color.Transparent;
            this.panelEx1.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx1.Style.BorderColor.Color = System.Drawing.Color.Gray;
            this.panelEx1.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx1.Style.GradientAngle = 90;
            this.panelEx1.TabIndex = 10;
            this.panelEx1.Text = "panelEx1";
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label10.ForeColor = System.Drawing.Color.Black;
            this.label10.Location = new System.Drawing.Point(15, 11);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(112, 21);
            this.label10.TabIndex = 15;
            this.label10.Text = "当前卸样任务";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label9.ForeColor = System.Drawing.Color.Black;
            this.label9.Location = new System.Drawing.Point(16, 77);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(72, 15);
            this.label9.TabIndex = 13;
            this.label9.Text = "采样副码：";
            // 
            // lblCurrSampleCode
            // 
            this.lblCurrSampleCode.AutoSize = true;
            this.lblCurrSampleCode.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrSampleCode.ForeColor = System.Drawing.Color.Black;
            this.lblCurrSampleCode.Location = new System.Drawing.Point(89, 77);
            this.lblCurrSampleCode.Name = "lblCurrSampleCode";
            this.lblCurrSampleCode.Size = new System.Drawing.Size(35, 15);
            this.lblCurrSampleCode.TabIndex = 14;
            this.lblCurrSampleCode.Text = "####";
            // 
            // lblCurrSendTime
            // 
            this.lblCurrSendTime.AutoSize = true;
            this.lblCurrSendTime.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrSendTime.ForeColor = System.Drawing.Color.Black;
            this.lblCurrSendTime.Location = new System.Drawing.Point(251, 77);
            this.lblCurrSendTime.Name = "lblCurrSendTime";
            this.lblCurrSendTime.Size = new System.Drawing.Size(35, 15);
            this.lblCurrSendTime.TabIndex = 12;
            this.lblCurrSendTime.Text = "####";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.Color.Black;
            this.label7.Location = new System.Drawing.Point(178, 77);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(72, 15);
            this.label7.TabIndex = 11;
            this.label7.Text = "发送时间：";
            // 
            // lblCurrUnloadType
            // 
            this.lblCurrUnloadType.AutoSize = true;
            this.lblCurrUnloadType.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrUnloadType.ForeColor = System.Drawing.Color.Black;
            this.lblCurrUnloadType.Location = new System.Drawing.Point(251, 47);
            this.lblCurrUnloadType.Name = "lblCurrUnloadType";
            this.lblCurrUnloadType.Size = new System.Drawing.Size(35, 15);
            this.lblCurrUnloadType.TabIndex = 10;
            this.lblCurrUnloadType.Text = "####";
            // 
            // lblCurrSamplerName
            // 
            this.lblCurrSamplerName.AutoSize = true;
            this.lblCurrSamplerName.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrSamplerName.ForeColor = System.Drawing.Color.Black;
            this.lblCurrSamplerName.Location = new System.Drawing.Point(89, 47);
            this.lblCurrSamplerName.Name = "lblCurrSamplerName";
            this.lblCurrSamplerName.Size = new System.Drawing.Size(35, 15);
            this.lblCurrSamplerName.TabIndex = 9;
            this.lblCurrSamplerName.Text = "####";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.Black;
            this.label5.Location = new System.Drawing.Point(15, 109);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(72, 15);
            this.label5.TabIndex = 16;
            this.label5.Text = "执行结果：";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.ForeColor = System.Drawing.Color.Black;
            this.label3.Location = new System.Drawing.Point(178, 47);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(72, 15);
            this.label3.TabIndex = 8;
            this.label3.Text = "卸样方式：";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.Black;
            this.label2.Location = new System.Drawing.Point(16, 47);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(71, 15);
            this.label2.TabIndex = 7;
            this.label2.Text = "采  样  机：";
            // 
            // lblCurrResultCode
            // 
            this.lblCurrResultCode.AutoSize = true;
            this.lblCurrResultCode.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrResultCode.ForeColor = System.Drawing.Color.Black;
            this.lblCurrResultCode.Location = new System.Drawing.Point(88, 109);
            this.lblCurrResultCode.Name = "lblCurrResultCode";
            this.lblCurrResultCode.Size = new System.Drawing.Size(35, 15);
            this.lblCurrResultCode.TabIndex = 17;
            this.lblCurrResultCode.Text = "####";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label15.ForeColor = System.Drawing.Color.Black;
            this.label15.Location = new System.Drawing.Point(15, 11);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(78, 21);
            this.label15.TabIndex = 25;
            this.label15.Text = "操作指令";
            // 
            // lblSampleType
            // 
            this.lblSampleType.AutoSize = true;
            this.lblSampleType.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSampleType.ForeColor = System.Drawing.Color.Black;
            this.lblSampleType.Location = new System.Drawing.Point(89, 77);
            this.lblSampleType.Name = "lblSampleType";
            this.lblSampleType.Size = new System.Drawing.Size(35, 15);
            this.lblSampleType.TabIndex = 22;
            this.lblSampleType.Text = "####";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label12.ForeColor = System.Drawing.Color.Black;
            this.label12.Location = new System.Drawing.Point(16, 77);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(72, 15);
            this.label12.TabIndex = 21;
            this.label12.Text = "采样方式：";
            // 
            // lblSampleCode
            // 
            this.lblSampleCode.AutoSize = true;
            this.lblSampleCode.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSampleCode.ForeColor = System.Drawing.Color.Black;
            this.lblSampleCode.Location = new System.Drawing.Point(89, 47);
            this.lblSampleCode.Name = "lblSampleCode";
            this.lblSampleCode.Size = new System.Drawing.Size(35, 15);
            this.lblSampleCode.TabIndex = 20;
            this.lblSampleCode.Text = "####";
            // 
            // label112
            // 
            this.label112.AutoSize = true;
            this.label112.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label112.ForeColor = System.Drawing.Color.Black;
            this.label112.Location = new System.Drawing.Point(16, 47);
            this.label112.Name = "label112";
            this.label112.Size = new System.Drawing.Size(72, 15);
            this.label112.TabIndex = 19;
            this.label112.Text = "采样主码：";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Segoe UI", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.Black;
            this.label4.Location = new System.Drawing.Point(15, 11);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 21);
            this.label4.TabIndex = 18;
            this.label4.Text = "当前采样单";
            // 
            // timer1
            // 
            this.timer1.Enabled = true;
            this.timer1.Interval = 3000;
            this.timer1.Tick += new System.EventHandler(this.timer1_Tick);
            // 
            // superGridControl2
            // 
            this.superGridControl2.BackColor = System.Drawing.Color.White;
            this.superGridControl2.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
            this.superGridControl2.ForeColor = System.Drawing.Color.Black;
            this.superGridControl2.Location = new System.Drawing.Point(6, 248);
            this.superGridControl2.Name = "superGridControl2";
            this.superGridControl2.PrimaryGrid.Caption.Text = "采 样 单 信 息";
            this.superGridControl2.PrimaryGrid.CheckBoxes = true;
            gridColumn6.MinimumWidth = 20;
            gridColumn6.Name = "";
            gridColumn6.SortIndicator = DevComponents.DotNetBar.SuperGrid.SortIndicator.None;
            gridColumn6.Width = 20;
            gridColumn7.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gridColumn7.DataPropertyName = "SampleCode";
            gridColumn7.HeaderText = "采样主码";
            gridColumn7.MinimumWidth = 100;
            gridColumn7.Name = "";
            gridColumn7.SortIndicator = DevComponents.DotNetBar.SuperGrid.SortIndicator.None;
            gridColumn8.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gridColumn8.DataPropertyName = "SamplingType";
            gridColumn8.FillWeight = 80;
            gridColumn8.HeaderText = "采样方式";
            gridColumn8.MinimumWidth = 80;
            gridColumn8.Name = "";
            gridColumn8.SortIndicator = DevComponents.DotNetBar.SuperGrid.SortIndicator.None;
            gridColumn8.Width = 80;
            gridColumn9.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gridColumn9.DataPropertyName = "SamplingDate";
            gridColumn9.HeaderText = "采样时间";
            gridColumn9.MinimumWidth = 130;
            gridColumn9.Name = "";
            gridColumn9.SortIndicator = DevComponents.DotNetBar.SuperGrid.SortIndicator.None;
            gridColumn9.Width = 130;
            this.superGridControl2.PrimaryGrid.Columns.Add(gridColumn6);
            this.superGridControl2.PrimaryGrid.Columns.Add(gridColumn7);
            this.superGridControl2.PrimaryGrid.Columns.Add(gridColumn8);
            this.superGridControl2.PrimaryGrid.Columns.Add(gridColumn9);
            this.superGridControl2.PrimaryGrid.DefaultVisualStyles.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            this.superGridControl2.Size = new System.Drawing.Size(386, 156);
            this.superGridControl2.TabIndex = 12;
            this.superGridControl2.Text = "superGridControl2";
            this.superGridControl2.CellClick += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridCellClickEventArgs>(this.superGridControl2_CellClick);
            this.superGridControl2.BeginEdit += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridEditEventArgs>(this.superGridControl2_BeginEdit);
            // 
            // panelEx2
            // 
            this.panelEx2.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx2.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx2.Controls.Add(this.rTxTMessageInfo);
            this.panelEx2.Location = new System.Drawing.Point(724, 53);
            this.panelEx2.Name = "panelEx2";
            this.panelEx2.Size = new System.Drawing.Size(353, 532);
            this.panelEx2.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx2.Style.BackColor1.Color = System.Drawing.Color.Transparent;
            this.panelEx2.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx2.Style.BorderColor.Color = System.Drawing.Color.Gray;
            this.panelEx2.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx2.Style.GradientAngle = 90;
            this.panelEx2.TabIndex = 13;
            this.panelEx2.Text = "panelEx2";
            // 
            // rTxTMessageInfo
            // 
            // 
            // 
            // 
            this.rTxTMessageInfo.BackgroundStyle.Class = "RichTextBoxBorder";
            this.rTxTMessageInfo.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.rTxTMessageInfo.Dock = System.Windows.Forms.DockStyle.Fill;
            this.rTxTMessageInfo.Font = new System.Drawing.Font("Segoe UI", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.rTxTMessageInfo.Location = new System.Drawing.Point(0, 0);
            this.rTxTMessageInfo.Name = "rTxTMessageInfo";
            this.rTxTMessageInfo.Size = new System.Drawing.Size(353, 532);
            this.rTxTMessageInfo.TabIndex = 0;
            // 
            // timer2
            // 
            this.timer2.Interval = 1000;
            this.timer2.Tick += new System.EventHandler(this.timer2_Tick);
            // 
            // metroStatusBar1
            // 
            this.metroStatusBar1.BackColor = System.Drawing.Color.White;
            // 
            // 
            // 
            this.metroStatusBar1.BackgroundStyle.CornerType = DevComponents.DotNetBar.eCornerType.Square;
            this.metroStatusBar1.ContainerControlProcessDialogKey = true;
            this.metroStatusBar1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.metroStatusBar1.Font = new System.Drawing.Font("Segoe UI", 10.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.metroStatusBar1.ForeColor = System.Drawing.Color.Black;
            this.metroStatusBar1.Items.AddRange(new DevComponents.DotNetBar.BaseItem[] {
            this.labelItem1,
            this.lblVersion,
            this.labelItem3});
            this.metroStatusBar1.Location = new System.Drawing.Point(0, 640);
            this.metroStatusBar1.Name = "metroStatusBar1";
            this.metroStatusBar1.Size = new System.Drawing.Size(1084, 22);
            this.metroStatusBar1.TabIndex = 14;
            this.metroStatusBar1.Text = "metroStatusBar1";
            // 
            // labelItem1
            // 
            this.labelItem1.Name = "labelItem1";
            this.labelItem1.Text = "版本：";
            // 
            // lblVersion
            // 
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Text = "1.0.0.0";
            // 
            // labelItem3
            // 
            this.labelItem3.ItemAlignment = DevComponents.DotNetBar.eItemAlignment.Center;
            this.labelItem3.Name = "labelItem3";
            this.labelItem3.PaddingLeft = 200;
            this.labelItem3.Text = "Copyright © 武汉博晟信息科技有限公司 All Rights Reserved";
            this.labelItem3.TextAlignment = System.Drawing.StringAlignment.Center;
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tableLayoutPanel1.ColumnCount = 1;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.Controls.Add(this.flpanEquState, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.panel1, 0, 0);
            this.tableLayoutPanel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tableLayoutPanel1.Location = new System.Drawing.Point(0, 0);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 55F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1084, 640);
            this.tableLayoutPanel1.TabIndex = 15;
            // 
            // flpanEquState
            // 
            this.flpanEquState.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flpanEquState.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpanEquState.Location = new System.Drawing.Point(3, 588);
            this.flpanEquState.Name = "flpanEquState";
            this.flpanEquState.Size = new System.Drawing.Size(1078, 49);
            this.flpanEquState.TabIndex = 10;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.superGridControl3);
            this.panel1.Controls.Add(this.panelEx4);
            this.panel1.Controls.Add(this.panelEx3);
            this.panel1.Controls.Add(this.lypanSamplerButton);
            this.panel1.Controls.Add(this.superGridControl1);
            this.panel1.Controls.Add(this.panelEx1);
            this.panel1.Controls.Add(this.superGridControl2);
            this.panel1.Controls.Add(this.panelEx2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Location = new System.Drawing.Point(3, 3);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1078, 579);
            this.panel1.TabIndex = 0;
            // 
            // superGridControl3
            // 
            this.superGridControl3.BackColor = System.Drawing.Color.White;
            this.superGridControl3.FilterExprColors.SysFunction = System.Drawing.Color.DarkRed;
            this.superGridControl3.ForeColor = System.Drawing.Color.Black;
            this.superGridControl3.Location = new System.Drawing.Point(6, 410);
            this.superGridControl3.Name = "superGridControl3";
            this.superGridControl3.PrimaryGrid.Caption.Text = "最 近 卸 样 记 录";
            this.superGridControl3.PrimaryGrid.CheckBoxes = true;
            gridColumn10.MinimumWidth = 20;
            gridColumn10.Name = "";
            gridColumn10.SortIndicator = DevComponents.DotNetBar.SuperGrid.SortIndicator.None;
            gridColumn10.Width = 20;
            gridColumn11.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gridColumn11.DataPropertyName = "MachineCode";
            gridColumn11.HeaderText = "采样机";
            gridColumn11.MinimumWidth = 60;
            gridColumn11.Name = "";
            gridColumn11.SortIndicator = DevComponents.DotNetBar.SuperGrid.SortIndicator.None;
            gridColumn11.Width = 120;
            gridColumn12.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gridColumn12.DataPropertyName = "SampleCode";
            gridColumn12.FillWeight = 70;
            gridColumn12.HeaderText = "采样副码";
            gridColumn12.MinimumWidth = 70;
            gridColumn12.Name = "";
            gridColumn12.SortIndicator = DevComponents.DotNetBar.SuperGrid.SortIndicator.None;
            gridColumn12.Width = 70;
            gridColumn13.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            gridColumn13.DataPropertyName = "UnloadType";
            gridColumn13.HeaderText = "卸样方式";
            gridColumn13.MinimumWidth = 70;
            gridColumn13.Name = "";
            gridColumn13.SortIndicator = DevComponents.DotNetBar.SuperGrid.SortIndicator.None;
            gridColumn13.Width = 70;
            gridColumn14.DataPropertyName = "ResultCode";
            gridColumn14.HeaderText = "执行结果";
            gridColumn14.Name = "";
            gridColumn14.Width = 60;
            this.superGridControl3.PrimaryGrid.Columns.Add(gridColumn10);
            this.superGridControl3.PrimaryGrid.Columns.Add(gridColumn11);
            this.superGridControl3.PrimaryGrid.Columns.Add(gridColumn12);
            this.superGridControl3.PrimaryGrid.Columns.Add(gridColumn13);
            this.superGridControl3.PrimaryGrid.Columns.Add(gridColumn14);
            this.superGridControl3.PrimaryGrid.DefaultVisualStyles.CellStyles.Default.Alignment = DevComponents.DotNetBar.SuperGrid.Style.Alignment.MiddleCenter;
            this.superGridControl3.Size = new System.Drawing.Size(386, 175);
            this.superGridControl3.TabIndex = 28;
            this.superGridControl3.Text = "superGridControl3";
            this.superGridControl3.CellClick += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridCellClickEventArgs>(this.superGridControl3_CellClick);
            this.superGridControl3.BeginEdit += new System.EventHandler<DevComponents.DotNetBar.SuperGrid.GridEditEventArgs>(this.superGridControl3_BeginEdit);
            // 
            // panelEx4
            // 
            this.panelEx4.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx4.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx4.Controls.Add(this.btnSendMakeCmd);
            this.panelEx4.Controls.Add(this.label15);
            this.panelEx4.Controls.Add(this.label1);
            this.panelEx4.Controls.Add(this.flpanUnloadType);
            this.panelEx4.Controls.Add(this.btnSendLoadCmd);
            this.panelEx4.Location = new System.Drawing.Point(400, 340);
            this.panelEx4.Name = "panelEx4";
            this.panelEx4.Size = new System.Drawing.Size(316, 245);
            this.panelEx4.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx4.Style.BackColor1.Color = System.Drawing.Color.Transparent;
            this.panelEx4.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx4.Style.BorderColor.Color = System.Drawing.Color.Gray;
            this.panelEx4.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx4.Style.GradientAngle = 90;
            this.panelEx4.TabIndex = 27;
            this.panelEx4.Text = "panelEx4";
            // 
            // btnSendMakeCmd
            // 
            this.btnSendMakeCmd.AccessibleRole = System.Windows.Forms.AccessibleRole.PushButton;
            this.btnSendMakeCmd.ColorTable = DevComponents.DotNetBar.eButtonColor.OrangeWithBackground;
            this.btnSendMakeCmd.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnSendMakeCmd.Location = new System.Drawing.Point(14, 172);
            this.btnSendMakeCmd.Name = "btnSendMakeCmd";
            this.btnSendMakeCmd.Size = new System.Drawing.Size(284, 23);
            this.btnSendMakeCmd.Style = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.btnSendMakeCmd.TabIndex = 26;
            this.btnSendMakeCmd.Text = "发 送 制 样 命 令";
            this.toolTip1.SetToolTip(this.btnSendMakeCmd, "自动发送制样计划失败时采用人工发送");
            this.btnSendMakeCmd.Click += new System.EventHandler(this.btnSendMakeCmd_Click);
            // 
            // panelEx3
            // 
            this.panelEx3.CanvasColor = System.Drawing.SystemColors.Control;
            this.panelEx3.ColorSchemeStyle = DevComponents.DotNetBar.eDotNetBarStyle.StyleManagerControlled;
            this.panelEx3.Controls.Add(this.label6);
            this.panelEx3.Controls.Add(this.lblCurrMakeMachine);
            this.panelEx3.Controls.Add(this.label4);
            this.panelEx3.Controls.Add(this.label12);
            this.panelEx3.Controls.Add(this.label112);
            this.panelEx3.Controls.Add(this.lblSampleType);
            this.panelEx3.Controls.Add(this.lblSampleCode);
            this.panelEx3.Location = new System.Drawing.Point(400, 198);
            this.panelEx3.Name = "panelEx3";
            this.panelEx3.Size = new System.Drawing.Size(316, 136);
            this.panelEx3.Style.Alignment = System.Drawing.StringAlignment.Center;
            this.panelEx3.Style.BackColor1.Color = System.Drawing.Color.Transparent;
            this.panelEx3.Style.Border = DevComponents.DotNetBar.eBorderType.SingleLine;
            this.panelEx3.Style.BorderColor.Color = System.Drawing.Color.Gray;
            this.panelEx3.Style.ForeColor.ColorSchemePart = DevComponents.DotNetBar.eColorSchemePart.PanelText;
            this.panelEx3.Style.GradientAngle = 90;
            this.panelEx3.TabIndex = 26;
            this.panelEx3.Text = "panelEx3";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.Black;
            this.label6.Location = new System.Drawing.Point(16, 107);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(71, 15);
            this.label6.TabIndex = 23;
            this.label6.Text = "制  样  机：";
            // 
            // lblCurrMakeMachine
            // 
            this.lblCurrMakeMachine.AutoSize = true;
            this.lblCurrMakeMachine.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCurrMakeMachine.ForeColor = System.Drawing.Color.Black;
            this.lblCurrMakeMachine.Location = new System.Drawing.Point(89, 107);
            this.lblCurrMakeMachine.Name = "lblCurrMakeMachine";
            this.lblCurrMakeMachine.Size = new System.Drawing.Size(35, 15);
            this.lblCurrMakeMachine.TabIndex = 24;
            this.lblCurrMakeMachine.Text = "####";
            // 
            // Form1
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1084, 662);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Controls.Add(this.metroStatusBar1);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MaximumSize = new System.Drawing.Size(1100, 700);
            this.MinimumSize = new System.Drawing.Size(1100, 700);
            this.Name = "Form1";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "武汉博晟燃料集中管控-卸样控制程序";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.Form1_FormClosing);
            this.Load += new System.EventHandler(this.FrmTrainBeltSamplerLoad_Load);
            this.flpanUnloadType.ResumeLayout(false);
            this.flpanUnloadType.PerformLayout();
            this.panelEx1.ResumeLayout(false);
            this.panelEx1.PerformLayout();
            this.panelEx2.ResumeLayout(false);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.panelEx4.ResumeLayout(false);
            this.panelEx4.PerformLayout();
            this.panelEx3.ResumeLayout(false);
            this.panelEx3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private DevComponents.DotNetBar.SuperGrid.SuperGridControl superGridControl1;
        private System.Windows.Forms.FlowLayoutPanel lypanSamplerButton;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.FlowLayoutPanel flpanUnloadType;
        private System.Windows.Forms.RadioButton rbtnToSubway;
        private System.Windows.Forms.RadioButton rbtnToMaker;
        private DevComponents.DotNetBar.ButtonX btnSendLoadCmd;
        private DevComponents.DotNetBar.PanelEx panelEx1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblCurrSendTime;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label lblCurrUnloadType;
        private System.Windows.Forms.Label lblCurrSamplerName;
        private System.Windows.Forms.Label lblCurrSampleCode;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label lblCurrResultCode;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Label label4;
        private DevComponents.DotNetBar.SuperGrid.SuperGridControl superGridControl2;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label lblSampleType;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label lblSampleCode;
        private System.Windows.Forms.Label label112;
        private DevComponents.DotNetBar.PanelEx panelEx2;
        private DevComponents.DotNetBar.Controls.RichTextBoxEx rTxTMessageInfo;
        private System.Windows.Forms.Timer timer2;
        private DevComponents.DotNetBar.Metro.MetroStatusBar metroStatusBar1;
        private DevComponents.DotNetBar.LabelItem labelItem1;
        private DevComponents.DotNetBar.LabelItem lblVersion;
        private DevComponents.DotNetBar.LabelItem labelItem3;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Panel panel1;
        private DevComponents.DotNetBar.PanelEx panelEx4;
        private DevComponents.DotNetBar.PanelEx panelEx3;
        private System.Windows.Forms.FlowLayoutPanel flpanEquState;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label lblCurrMakeMachine;
        private System.Windows.Forms.ToolTip toolTip1;
        private DevComponents.DotNetBar.SuperGrid.SuperGridControl superGridControl3;
        private DevComponents.DotNetBar.ButtonX btnSendMakeCmd;
    }
}