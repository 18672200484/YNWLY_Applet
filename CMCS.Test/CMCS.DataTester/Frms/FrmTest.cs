using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CMCS.DataTester.DAO;
using System.IO.Ports;
using CMCS.Common.DAO;
using CMCS.Common.Entities.CarTransport;

namespace CMCS.DataTester.Frms
{
    public partial class FrmTest : Form
    {
        private SerialPort serialPort = new SerialPort();
        DataTesterDAO dataTesterDAO = DataTesterDAO.GetInstance();

        public FrmTest()
        {
            InitializeComponent();
        }

        private void FrmTest_Load(object sender, EventArgs e)
        {
            TestClass test1 = new TestClass();
            test1.Value1 = 1;
            test1.Value2 = "1";

            ChangeValue(test1);

            int value = test1.Value1;
            string value2 = test1.Value2;


            TestClass2 test2 = new TestClass2();
            test2.Value1 = 1;
            test2.Value2 = "1";

            ChangeValue2(test2);

            value = test2.Value1;
            value2 = test2.Value2;

        }

        private void ChangeValue(TestClass test)
        {
            test.Value1 = 2;
            test.Value2 = "2";

            IList<CmcsSaleFuelTransport> lits = CommonDAO.GetInstance().SelfDber.Entities<CmcsSaleFuelTransport>("where trunc(CreateDate)=trunc(sysdate-100)");

            int res = lits.Count();
        }

        private void ChangeValue2(TestClass2 test)
        {
            test.Value1 = 2;
            test.Value2 = "2";
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


    }

    public class TestClass
    {
        public int Value1 { get; set; }

        public string Value2 { get; set; }
    }

    public struct TestClass2
    {
        public int Value1 { get; set; }

        public string Value2 { get; set; }

        public string change()
        {
            return Value1.ToString() + Value2;
        }
    }
}
