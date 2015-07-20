using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NKnife.GUI.WinForm;

namespace MeterKnife.Common.Scpi
{
    public partial class ScpiCommandEditorDialog : SimpleForm
    {
        public ScpiCommandGroupCategory Category { get; set; }

        public ScpiCommandEditorDialog()
        {
            InitializeComponent();
            _HexEnableCheckBox.CheckStateChanged += (s, e) =>
            {
                
            };
        }

        private void _ConfirmButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void _CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }

        private void _AddEnterFlagButton_Click(object sender, EventArgs e)
        {
            _CommandTextBox.Text = _CommandTextBox.Text.Insert(0, "\r\n");
            _CommandTextBox.Focus();
        }

        private void _InsertTailBrButton_Click(object sender, EventArgs e)
        {
            _CommandTextBox.AppendText("\r\n");
            _CommandTextBox.Focus();
        }
    }
}
