using System.Windows.Forms;
using Common.Logging;
using NKnife.Channels.Channels.Serials;
using NKnife.NLog.WinForm;

namespace Huaxin.MultiTemperature.App.Demo
{
    public partial class Workbench : Form
    {
        private static readonly ILog _logger = LogManager.GetLogger<Workbench>();

        private readonly WorkbenchViewModel _ViewModel = new WorkbenchViewModel();
        private bool _IsStart=false;

        public Workbench()
        {
            InitializeComponent();

            foreach (var serial in SerialUtils.LocalSerialPorts)
                _SerialPortComboBox.Items.Add(serial.Key);
            var loggerListView = LoggerListView.Instance;
            loggerListView.Dock = DockStyle.Fill;
            _LoggerPanel.Controls.Add(loggerListView);
            _logger.Info("日志面板添加成功……");

            ControlEventManager();
            ControlEnableManager();
        }

        private void ControlEventManager()
        {
            _SerialPortComboBox.SelectedIndexChanged += (s, e) =>
            {
                _OpenPortButton.Enabled = _SerialPortComboBox.SelectedItem != null;
                if (_SerialPortComboBox.SelectedItem != null)
                    _ViewModel.CurrentSerial = ushort.Parse(_SerialPortComboBox.SelectedItem.ToString().Trim('C', 'O', 'M'));
            };
            _PathCheckedListBox.ItemCheck += (s, e) =>
            {
                var path = ushort.Parse(_PathCheckedListBox.Items[e.Index].ToString());
                if (e.NewValue == CheckState.Checked)
                {
                    if (!_ViewModel.Paths.Contains(path))
                        _ViewModel.Paths.Add(path);
                }
                else
                {
                    if (_ViewModel.Paths.Contains(path))
                        _ViewModel.Paths.Remove(path);
                }
            };
            _OpenPortButton.Click += (s, e) =>
            {
                _ViewModel.OpenSerial();
                ControlEnableManager();
            };
            _ClosePortButton.Click += (s, e) =>
            {
                _ViewModel.CloseSerial();
                ControlEnableManager();
            };
            _StartButton.Click += (s, e) =>
            {
                if (!_IsStart)
                {
                    _ViewModel.Start(_CommandTextBox.Text, _IntervalNumericUpDown.Value);
                    _IsStart = true;
                    _StartButton.Text = "停止采集";
                }
                else
                {
                    _ViewModel.Stop();
                    _IsStart = false;
                    _StartButton.Text = "开始采集";
                }
            };
        }

        private void ControlEnableManager()
        {
            _OpenPortButton.Enabled = !_ViewModel.IsOpen;
            _ClosePortButton.Enabled = _ViewModel.IsOpen;
            _ClearSelectButton.Enabled = _ViewModel.IsOpen;
            _AllSelectButton.Enabled = _ViewModel.IsOpen;
            _StartButton.Enabled = _ViewModel.IsOpen;
        }

        private void _BuildPdfPrintButton_Click(object sender, System.EventArgs e)
        {
            _ViewModel.BuildPdfPrint();
        }
    }
}