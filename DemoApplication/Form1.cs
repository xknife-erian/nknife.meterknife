using System.Threading;
using System.Windows.Forms;
using MeterKnife.DemoApplication.Dialogs;
using MeterKnife.Instruments.Specified.Agilent;

namespace MeterKnife.DemoApplication
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            _CustomScpiGroupMenuItem.PerformClick();
        }

        private void _CustomScpiGroupMenuItem_Click(object sender, System.EventArgs e)
        {
            var dialog = new CustomScpiGroupDialog();
            dialog.ShowDialog(this);
        }
    }
}
