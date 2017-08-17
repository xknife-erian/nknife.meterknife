using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using MeterKnife.Keysights;
using MeterKnife.Models;
using NKnife.Channels.Interfaces.Channels;
using WeifenLuo.WinFormsUI.Docking;

namespace MeterKnife.MiscDemo
{
    public partial class KeysightChannelDemoView : DockContent
    {
        public KeysightChannelDemoView()
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
            var ksChannel = new KeysightChannel();
            ksChannel.Open();
            var group = new KeysightQuestionGroup();
            var device = new Device("", "", "", (int) _AddressBox.Value);
            group.Add(new KeysightQuestion(ksChannel, device, null, _LoopEnableCheckBox.Checked, _CommandTextBox.Text));
            ksChannel.UpdateQuestionGroup(group);
            ksChannel.SendReceiving(SendAction, ReceivedFunc);
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