using System;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NKnife.MeterKnife.Base;
using NKnife.MeterKnife.Common.Scpi;
using NKnife.Util;

namespace NKnife.MeterKnife.Workbench.Dialogs.Engineerings
{
    public partial class CareCommandEditorDialog : Form
    {
        private readonly IDUTLogic _dutLogic;
        private PoolCategory _category;

        public CareCommandEditorDialog(IDUTLogic dutLogic)
        {
            _dutLogic = dutLogic;
            InitializeComponent();
            InitializeCommandTabControl();
            _ConfirmButton.Enabled = false;
            _SlotComboBox.SelectedIndex = 0;
            RespondToControlEvnet();
            Shown+= OnShown;
        }

        private void InitializeCommandTabControl()
        {
            _CommandTabControl.SuspendLayout();
            _CommandTabControl.TabPages.Clear();
            _CommandTabControl.TabPages.Add(_ScpiTabPage);
            _CommandTabControl.ResumeLayout(false);
            _CommandTabControl.PerformLayout();
        }

        private async void OnShown(object sender, EventArgs e)
        {
            var dutArray = await _dutLogic.GetAllDUTAsync();
            if (dutArray != null) 
                _DUTComboBox.Items.AddRange(dutArray.Cast<object>().ToArray());
        }

        private void RespondToControlEvnet()
        {
            _ScpiRadioButton.CheckedChanged += (sender, args) =>
            {
                _CommandTabControl.SuspendLayout();
                _CommandTabControl.TabPages.Clear();
                _CommandTabControl.TabPages.Add(_ScpiTabPage);
                _CommandTabControl.ResumeLayout(false);
                _CommandTabControl.PerformLayout();
            };
            _CareRadioButton.CheckedChanged += (sender, args) =>
            {
                _CommandTabControl.SuspendLayout();
                _CommandTabControl.TabPages.Clear();
                _CommandTabControl.TabPages.Add(_CareTabPage);
                _CommandTabControl.ResumeLayout(false);
                _CommandTabControl.PerformLayout();
            };
        }

        public PoolCategory Category
        {
            get => _category;
            set
            {
                _category = value;
                switch (value)
                {
                    case PoolCategory.Collect:
                        _IntervalNumericUpDown.Value = 400;
                        break;
                    case PoolCategory.Initializtion:
                        _IntervalNumericUpDown.Value = 50;
                        break;
                }
            }
        }

        public string Command
        {
            get => _CommandTextBox.Text;
            set => _CommandTextBox.Text = value;
        }

        public int Interval
        {
            get => (int) _IntervalNumericUpDown.Value;
            set
            {
                if (value < _IntervalNumericUpDown.Minimum || value > _IntervalNumericUpDown.Maximum)
                    _IntervalNumericUpDown.Value = 200;
                else
                    _IntervalNumericUpDown.Value = value;
            }
        }

        public bool IsHex
        {
            get => _HexEnableCheckBox.Checked;
            set => _HexEnableCheckBox.Checked = value;
        }

        private void _ConfirmButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void _CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void _HexEnableCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (_HexEnableCheckBox.Checked)
            {
                var t = _CommandTextBox.Text;
                _CommandTextBox.Text = Encoding.ASCII.GetBytes(t).ToHexString();
            }
            else
            {
                var t = _CommandTextBox.Text;
                var bs = UtilByte.ConvertToBytes(t);
                _CommandTextBox.Text = Encoding.ASCII.GetString(bs);
            }

            _CommandTextBox.Focus();
        }
    }
}