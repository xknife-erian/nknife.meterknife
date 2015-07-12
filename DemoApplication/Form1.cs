using System.Threading;
using System.Windows.Forms;
using MeterKnife.Common.Interfaces;
using MeterKnife.DemoApplication.Dialogs;
using MeterKnife.Instruments.Specified.Agilent;
using NKnife.IoC;

namespace MeterKnife.DemoApplication
{
    public partial class Form1 : Form
    {
        private IMeterKernel _kernel;

        public Form1()
        {
            InitializeComponent();
            _kernel = DI.Get<IMeterKernel>();
            _CustomScpiGroupMenuItem.PerformClick();
        }

        private void _CustomScpiGroupMenuItem_Click(object sender, System.EventArgs e)
        {
            var dialog = new CustomScpiGroupDialog();
            dialog.ShowDialog(this);
        }
    }
}
