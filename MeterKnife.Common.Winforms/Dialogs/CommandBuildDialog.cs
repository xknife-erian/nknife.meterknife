using System;
using System.Collections.Generic;
using System.Windows.Forms;
using NKnife.GUI.WinForm;

namespace MeterKnife.Common.Winforms.Dialogs
{
    public partial class CommandBuildDialog : SimpleForm
    {
        public CommandBuildDialog()
        {
            InitCommands = new List<string>(8);
            InitializeComponent();
        }

        public List<string> InitCommands { get; private set; }
        public string CollectCommand { get; private set; }

        private void _AcceptButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_CollectCommandComboBox.Text))
            {
                MessageBox.Show(this, "采集指令必须设置");
                return;
            }
            if (!string.IsNullOrEmpty(_CommandTextBox1.Text))
                InitCommands.Add(_CommandTextBox1.Text);
            if (!string.IsNullOrEmpty(_CommandTextBox2.Text))
                InitCommands.Add(_CommandTextBox2.Text);
            if (!string.IsNullOrEmpty(_CommandTextBox3.Text))
                InitCommands.Add(_CommandTextBox3.Text);
            if (!string.IsNullOrEmpty(_CommandTextBox4.Text))
                InitCommands.Add(_CommandTextBox4.Text);
            if (!string.IsNullOrEmpty(_CommandTextBox5.Text))
                InitCommands.Add(_CommandTextBox5.Text);
            if (!string.IsNullOrEmpty(_CommandTextBox6.Text))
                InitCommands.Add(_CommandTextBox6.Text);
            if (!string.IsNullOrEmpty(_CommandTextBox7.Text))
                InitCommands.Add(_CommandTextBox7.Text);
            if (!string.IsNullOrEmpty(_CommandTextBox8.Text))
                InitCommands.Add(_CommandTextBox8.Text);
            CollectCommand = _CollectCommandComboBox.Text;
            DialogResult = DialogResult.OK;
        }

        private void _CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
