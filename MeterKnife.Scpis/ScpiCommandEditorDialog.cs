using System;
using System.Text;
using System.Windows.Forms;
using NKnife.Converts;
using NKnife.GUI.WinForm;
using ScpiKnife;

namespace MeterKnife.Scpis
{
    public partial class ScpiCommandEditorDialog : SimpleForm
    {
        private ScpiCommandGroupCategory _Category;

        public ScpiCommandGroupCategory Category
        {
            get { return _Category; }
            set
            {
                _Category = value;
                switch (value)
                {
                    case ScpiCommandGroupCategory.Collect:
                        _IntervalNumericUpDown.Value = 400;
                        _IsReturnCheckBox.Checked = true;
                        break;
                    case ScpiCommandGroupCategory.Initializtion:
                        _IntervalNumericUpDown.Value = 50;
                        _IsReturnCheckBox.Checked = false;
                        break;
                }
            }
        }

        public string Command
        {
            get { return _CommandTextBox.Text; }
            set { _CommandTextBox.Text = value; }
        }

        public int Interval
        {
            get { return (int) _IntervalNumericUpDown.Value; }
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
            get { return _HexEnableCheckBox.Checked; }
            set { _HexEnableCheckBox.Checked = value; }
        }

        public bool IsReturn
        {
            get { return _IsReturnCheckBox.Checked; }
            set { _IsReturnCheckBox.Checked = value; }
        }

        public ScpiCommandEditorDialog()
        {
            InitializeComponent();
            _CommandTextBox.TextChanged += (s, e) =>
            {
                _ConfirmButton.Enabled = _CommandTextBox.Text.Length > 0;
            };
            _ConfirmButton.Enabled = false;
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
                var bs = UtilityConvert.HexToBytes(t);
                _CommandTextBox.Text = Encoding.ASCII.GetString(bs);
            }
            _CommandTextBox.Focus();
        }
    }
}
