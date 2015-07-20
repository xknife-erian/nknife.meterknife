using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NKnife.Converts;
using NKnife.GUI.WinForm;
using NKnife.Utility;

namespace MeterKnife.Common.Scpi
{
    public partial class ScpiCommandEditorDialog : SimpleForm
    {
        public ScpiCommandGroupCategory Category { get; set; }

        public string Command { get { return _CommandTextBox.Text; } }
        public int Range { get { return (int)_RangeNumericUpDown.Value; } }
        public bool IsHex { get { return _HexEnableCheckBox.Checked; } }

        public ScpiCommandEditorDialog()
        {
            InitializeComponent();
            _CommandTextBox.TextChanged += (s, e) =>
            {
                _ConfirmButton.Enabled = !_CommandTextBox.IsEmptyText();
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
