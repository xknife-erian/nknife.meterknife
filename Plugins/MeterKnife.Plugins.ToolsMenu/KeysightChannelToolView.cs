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

        private void SendButton_Click(object sender, EventArgs e)
        {
            var command = _CommandComboBox.Text;
            if (!ContainCommand(command))
            {
                _CommandComboBox.Items.Insert(0, command);
            }
            var device = new Device("Keysight", "34401", "34401", (int) _AddressBox.Value);
            var ksChannel = new KeysightChannel();
            var group = new KeysightQuestionGroup();
            group.Add(new KeysightQuestion(ksChannel, device, null, _LoopEnableCheckBox.Checked, command));
            ksChannel.UpdateQuestionGroup(group);

            ksChannel.Open();
            ksChannel.SendReceiving(SendAction, ReceivedFunc);
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