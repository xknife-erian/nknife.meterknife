using System;
using System.IO;
using System.Windows.Forms;
using NKnife.GUI.WinForm;

namespace MeterKnife.Common.Winforms.Dialogs
{
    public partial class DataPathSetterDialog : SimpleForm
    {
        public DataPathSetterDialog()
        {
            InitializeComponent();
            if (Directory.Exists(DataPath))
                _PathTextbox.Text = DataPath;
        }

        public string DataPath { get; private set; }

        private void _PathSelectButton_Click(object sender, EventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            dialog.RootFolder = Environment.SpecialFolder.MyComputer;
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                _PathTextbox.Text = dialog.SelectedPath;
            }
        }

        private void _ConfirmButton_Click(object sender, EventArgs e)
        {
            if (!Directory.Exists(_PathTextbox.Text))
            {
                MessageBox.Show(this, "请选择正确的数据存储路径.");
                return;
            }
            DataPath = _PathTextbox.Text;
            DialogResult = DialogResult.OK;
        }

        private void _CancelButton_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
        }
    }
}
