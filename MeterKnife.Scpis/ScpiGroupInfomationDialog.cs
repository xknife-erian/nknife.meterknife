using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NKnife.GUI.WinForm;

namespace MeterKnife.Scpis
{
    public partial class ScpiGroupInfomationDialog : SimpleForm
    {
        public ScpiGroupInfomationDialog()
        {
            InitializeComponent();
        }

        public string GroupText
        {
            get { return _GroupNameTextBox.Text; }
        }

        private void _ConfirmButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_GroupNameTextBox.Text))
            {
                MessageBox.Show(this, "指令集集合的名称不能为空,请填写.", "填写名称", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            DialogResult = DialogResult.OK;
            Close();
        }

        private void _CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
