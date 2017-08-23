using System;
using System.Windows.Forms;
using MeterKnife.Keysights;
using MeterKnife.Models;
using NKnife.Channels.Interfaces.Channels;
using WeifenLuo.WinFormsUI.Docking;

namespace MeterKnife.Plugins.ToolsMenu
{
    public partial class KeysightChannelToolView : DockContent
    {
        public KeysightChannelToolView()
        {
            SetStyle(ControlStyles.DoubleBuffer | ControlStyles.OptimizedDoubleBuffer | ControlStyles.AllPaintingInWmPaint, true);
            UpdateStyles();
            InitializeComponent();
            ControlStateCheck();
            ControlEventManager();
        }

        private void ControlEventManager()
        {
            _LoopEnableCheckBox.CheckedChanged += (s, e) => ControlStateCheck();
        }

        private void ControlStateCheck()
        {
            _LoopTimeBox.Enabled = _LoopEnableCheckBox.Checked;
        }

       private readonly KeysightChannel _KsChannel = new KeysightChannel();

        private void SendButton_Click(object sender, EventArgs e)
        {
            var isLoop = _LoopEnableCheckBox.Checked;
            if (isLoop)
            {
                _SendButton.Enabled = false;
                _StopButton.Enabled = true;
            }
            var command = _CommandComboBox.Text;
            if (!ContainCommand(command))
            {
                _CommandComboBox.Items.Insert(0, command);
            }
            var device = new Device("Keysight", "34401", "34401", (int) _AddressBox.Value);
            var group = new KeysightQuestionGroup();
            var question = new KeysightQuestion(_KsChannel, device, null, isLoop, command);
            group.Add(question);

            _KsChannel.TalkTotalTimeout = (uint) _LoopTimeBox.Value;
            _KsChannel.UpdateQuestionGroup(group);
            _KsChannel.Open();
            _KsChannel.SendReceiving(SendAction, ReceivedFunc);
        }

        private void _StopButton_Click(object sender, EventArgs e)
        {
            _KsChannel.StopSendReceiving();
            _SendButton.Enabled = true;
            _StopButton.Enabled = false;
        }

        private bool ContainCommand(string command)
        {
            foreach (var item in _CommandComboBox.Items)
            {
                if (item.Equals(command))
                    return true;
            }
            return false;
        }

        private void SendAction(IQuestion<string> question)
        {
            _ResultListBox.ThreadSafeInvoke(() => _ResultListBox.Items.Insert(0, $"> {question.Data}"));
        }

        private bool ReceivedFunc(IAnswer<string> answer)
        {
            _ResultListBox.ThreadSafeInvoke(() => _ResultListBox.Items.Insert(0, $"< {answer.Data}"));
            return true;
        }

    }
}