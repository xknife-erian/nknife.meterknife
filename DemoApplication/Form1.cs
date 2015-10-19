using System.Threading;
using System.Windows.Forms;
using MeterKnife.Common.Interfaces;
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
            _CustomScpiGroupMenuItem.PerformClick();
        }

        private void _CustomScpiGroupMenuItem_Click(object sender, System.EventArgs e)
        {
            var dialog = new ScpiMangerForm();
            dialog.ShowDialog(this);
        }
    }
}
