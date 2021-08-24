using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace NKnife.MeterKnife.Scpis
{
    public partial class SubjectInfoDialog : Form
    {
        public SubjectInfoDialog()
        {
            InitializeComponent();
        }

        public string GroupName
        {
            get { return _GroupNameTextBox.Text; }
            set { _GroupNameTextBox.Text = value; }
        }

        private void _ConfirmButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_GroupNameTextBox.Text))
            {
                MessageBox.Show(this, "请为当前的功能主题命名", "功能主题命名", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
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
