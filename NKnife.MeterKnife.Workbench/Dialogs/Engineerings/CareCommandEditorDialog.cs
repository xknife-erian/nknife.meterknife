using System;
using System.Text;
using System.Windows.Forms;
using NKnife.MeterKnife.Common.Scpi;
using NKnife.Util;

namespace NKnife.MeterKnife.Workbench.Dialogs.Engineerings
{
    public partial class CareCommandEditorDialog : Form
    {
        private PoolCategory _category;

        public CareCommandEditorDialog()
        {
            InitializeComponent();
            _CommandTextBox.TextChanged += (s, e) => { _ConfirmButton.Enabled = _CommandTextBox.Text.Length > 0; };
            _ConfirmButton.Enabled = false;
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
                        _IsReturnCheckBox.Checked = true;
                        break;
                    case PoolCategory.Initializtion:
                        _IntervalNumericUpDown.Value = 50;
                        _IsReturnCheckBox.Checked = false;
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

        public bool IsReturn
        {
            get => _IsReturnCheckBox.Checked;
            set => _IsReturnCheckBox.Checked = value;
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