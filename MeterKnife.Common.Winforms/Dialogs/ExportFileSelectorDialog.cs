using System;
using System.Windows.Forms;
using NKnife.Win.Forms;

namespace MeterKnife.Common.Winforms.Dialogs
{
    public partial class ExportFileSelectorDialog : SimpleForm
    {
        public ExportFileSelectorDialog()
        {
            InitializeComponent();
        }

        private void _BrowserButton_Click(object sender, EventArgs e)
        {
            var dialog = new FolderBrowserDialog();
            if (dialog.ShowDialog(this) == DialogResult.OK)
            {
                _FilePathTextBox.Text = dialog.SelectedPath;
            }
        }

        public string SelectedPath { get; private set; }

        private void _ConfirmButton_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(_FilePathTextBox.Text))
            {
                MessageBox.Show(this, "请选择导出文件的存储目录。", "选择目录", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            SelectedPath = _FilePathTextBox.Text;
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
