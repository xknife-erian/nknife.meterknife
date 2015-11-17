using System;
using System.Data;
using System.Threading;
using System.Windows.Forms;
using MeterKnife.Common.Interfaces;
using MeterKnife.Datas;
using MeterKnife.DemoApplication.Properties;
using MeterKnife.Scpis;
using NKnife.IoC;

namespace MeterKnife.DemoApplication
{
    public partial class Form1 : Form
    {
        private IMeterKernel _Kernel;

        public Form1()
        {
            InitializeComponent();
            Icon = Resources.demo;
            _Kernel = DI.Get<IMeterKernel>();
            //_CustomScpiGroupMenuItem.PerformClick();
        }

        private void _CustomScpiGroupMenuItem_Click(object sender, System.EventArgs e)
        {
            var dialog = new ScpiMangerForm();
            dialog.ShowDialog(this);
        }

        private void 数据库测试ToolStripMenuItem_Click(object sender, System.EventArgs e)
        {
            var dataService = new MeterDataService();
            var filename = Guid.NewGuid().ToString("N");
            var dataset = new DataSet();
            dataService.Save(string.Format("z:\\{0}.s3db", filename), dataset);
        }
    }
}
