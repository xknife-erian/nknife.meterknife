using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NKnife.GUI.WinForm;

namespace MeterKnife.Workbench.Dialogs
{
    public partial class DataPathSetterDialog : SimpleForm
    {
        public DataPathSetterDialog()
        {
            InitializeComponent();
        }

        public string DataPath { get; private set; }

        private void _PathSelectButton_Click(object sender, EventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog(this)== DialogResult.OK)
            {
                _PathTextbox.Text = dialog.SelectedPath;
            }
        }

        private void _ConfirmButton_Click(object sender, EventArgs e)
        {
            DataPath = _PathTextbox.Text;
            DialogResult = DialogResult.OK;
        }

        private void _CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
