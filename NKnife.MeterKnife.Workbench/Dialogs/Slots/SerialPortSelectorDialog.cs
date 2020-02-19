using System.Windows.Forms;
using NKnife.MeterKnife.Util.Serial;
using NKnife.Win.Forms;

namespace NKnife.MeterKnife.Workbench.Dialogs
{
    public partial class SerialPortSelectorDialog : SimpleForm
    {
        public SerialPortSelectorDialog()
        {
            InitializeComponent();
            FillListView();
            _AcceptButton.Click += (sender, args) =>
            {
                if (_ListView.SelectedItems.Count > 0)
                {
                    var item = _ListView.SelectedItems[0];
                    var port = item.SubItems[1].Text.ToUpper().Trim().TrimStart('C', 'O', 'M');
                    short p = 0;
                    short.TryParse(port, out p);
                    SerialPort = p;
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    MessageBox.Show((IWin32Window) this, "请从本机串口列表中选择一个串口。", "未进行串口选择", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                }
            };
            _CancelButton.Click += (sender, args) =>
            {
                SerialPort = 0;
                DialogResult = DialogResult.Cancel;
            };
        }

        public short SerialPort { get; private set; }

        private void FillListView()
        {
            _ListView.SuspendLayout();
            _ListView.Items.Clear();
            foreach (var p in SerialHelper.LocalSerialPorts)
            {
                var item = new ListViewItem(new[] {"", p.Key, p.Value});
                _ListView.Items.Add(item);
            }
            _ListView.PerformLayout();
            _ListView.ResumeLayout(false);
        }

        private void _refreshButton_Click(object sender, System.EventArgs e)
        {
            SerialHelper.RefreshSerialPorts();
            FillListView();
        }

    }
}
