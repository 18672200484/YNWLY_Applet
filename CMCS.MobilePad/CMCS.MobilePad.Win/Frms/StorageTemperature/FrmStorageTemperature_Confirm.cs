using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevComponents.DotNetBar;
using CMCS.Common.DAO;
using CMCS.Common.Entities.CarTransport;
using CMCS.Common.Enums;
using CMCS.Common.Entities.Sys;
using CMCS.Common.Entities.Storage;
using CMCS.Common.Entities.iEAA;
using System.Drawing;
using System.Drawing.Drawing2D;


namespace CMCS.MobilePad.Win.Frms
{
    public partial class FrmStorageTemperature_Confirm : DevComponents.DotNetBar.Metro.MetroForm
    {
        string autotruckId;
        string transportId;
        string carType;
        Graphics g;
        decimal x = 5.2m;
        decimal y = 4.6m;
        decimal height = 280m;

        CommonDAO commonDAO = CommonDAO.GetInstance();

        public FrmStorageTemperature_Confirm(string transportId)
        {
            InitializeComponent();
            g = panel1.CreateGraphics();
            List<CodeContent> list = CommonDAO.GetInstance().GetCodeContentByKind("煤场温度仪编号");
            foreach (var item in list)
            {
                ddlPoleCode.Items.Add(new ComboBoxItem(item.Code.ToString(), item.Code.ToString()));
            }
            List<StorageArea> StorageAreas = commonDAO.SelfDber.Entities<StorageArea>(" where 1=1 order by FuelStorageId,StartPoint");
            foreach (var item in StorageAreas)
            {
                ddlUnitName.Items.Add(new ComboBoxItem(item.AreaName.ToString(), item.AreaName.ToString()));
            }
            if (!string.IsNullOrEmpty(transportId))
            {
                this.transportId = transportId;
                StorageTemperature entity = commonDAO.SelfDber.Get<StorageTemperature>(this.transportId);
                if (entity != null)
                {
                    ddlPoleCode.Text = entity.PoleCode;
                    ddlUnitName.Text = entity.UnitName;
                    txtPointX.Text = entity.PointX.ToString("f2");
                    txtPointY.Text = entity.PointY.ToString("f2");
                    Temperature.Text = entity.Temperature.ToString("f2");
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.No;
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {

            if (string.IsNullOrEmpty(ddlUnitName.Text))
            {
                MessageBox.Show("煤场分区名称不能为空！");
                CreateImg();
                return;
            }
            if (string.IsNullOrEmpty(ddlPoleCode.Text))
            {
                MessageBox.Show("测温杆编号不能为空！");
                CreateImg();
                return;
            }
            if (commonDAO.SelfDber.Count<StorageTemperature>(" where PoleCode='" + ddlPoleCode.Text + "'") > 0)
            {
                MessageBox.Show("此测温杆编号已被设置！");
                CreateImg();
                return;
            }
            //if (commonDAO.SelfDber.Count<StorageTemperature>(" where UnitName='" + ddlUnitName.Text 
            //    + "' and Id != '" + this.transportId + "'"
            //    + "' and PointX != '" + txtPointX.Text + "'"
            //    + "' and PointY != '" + txtPointY.Text + "'") > 0)
            //{
            //    MessageBox.Show("此区域的(X,Y)坐标已设置了其他编号的测温杆！");
            //    return;
            //}  

            StorageTemperature entity = commonDAO.SelfDber.Get<StorageTemperature>(this.transportId);

            if (entity != null)
            {
                entity.UnitName = ddlUnitName.Text;
                entity.PoleCode = ddlPoleCode.Text;
                entity.PointX = Convert.ToDecimal(txtPointX.Text);
                entity.PointY = Convert.ToDecimal(txtPointY.Text);
                entity.Temperature = Convert.ToDecimal(Temperature.Text);
                commonDAO.SelfDber.Update<StorageTemperature>(entity);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                entity = new StorageTemperature();
                entity.UnitName = ddlUnitName.Text;
                entity.PoleCode = ddlPoleCode.Text;
                entity.PointX = Convert.ToDecimal(txtPointX.Text);
                entity.PointY = Convert.ToDecimal(txtPointY.Text);
                entity.Temperature = Convert.ToDecimal(Temperature.Text);
                commonDAO.SelfDber.Insert<StorageTemperature>(entity);

                this.DialogResult = DialogResult.OK;
                this.Close();
            }
        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            DrawEllipseLocation(e);
        }

        private void DrawEllipseLocation(MouseEventArgs e)
        {
            Point p = e.Location;
            g.Clear(Color.DarkGray);
            g.DrawEllipse(new Pen(Color.CadetBlue, 10), p.X - 5, p.Y - 5, 10, 10);

            decimal pointx = (p.X / x);
            decimal pointy = ((height - p.Y) / y);

            if (pointx < 0) pointx = 0;
            if (pointx > 50) pointx = 50;

            if (pointy < 0) pointy = 0;
            if (pointy > 60) pointy = 60;

            txtPointX.Text = pointx.ToString();
            txtPointY.Text = pointy.ToString();
        }

        private void FrmStorageTemperature_Confirm_Shown(object sender, EventArgs e)
        {
            decimal a = Convert.ToDecimal(txtPointX.Text);
            decimal b = Convert.ToDecimal(txtPointY.Text);

            int px = (int)(a * x);
            int py = (int)(-1.00m * ((y * b) - height));

            if (px >= 0 && py >= 0)
            {
                g.Clear(Color.DarkGray);
                g.DrawEllipse(new Pen(Color.CadetBlue, 10), px - 5, py - 5, 10, 10);
            }
        }

        private void CreateImg()
        {
            decimal a = Convert.ToDecimal(txtPointX.Text);
            decimal b = Convert.ToDecimal(txtPointY.Text);

            int px = (int)(a * x);
            int py = (int)(-1.00m * ((y * b) - height));

            if (px >= 0 && py >= 0)
            {
                g.Clear(Color.DarkGray);
                g.DrawEllipse(new Pen(Color.CadetBlue, 10), px - 5, py - 5, 10, 10);
            }
        }

        private void txtPointX_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtPointX.Value < 0) txtPointX.Value = 0;
            if (txtPointX.Value > 50) txtPointX.Value = 50;
            CreateImg();
        }

        private void txtPointY_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtPointY.Value < 0) txtPointY.Value = 0;
            if (txtPointY.Value > 60) txtPointY.Value = 60;
            CreateImg();
        }
    }
}