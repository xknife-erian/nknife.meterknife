using System;
using NKnife.GUI.WinForm;

namespace MeterKnife.Instruments.Dialog
{
    public partial class ExportProgressDialog : SimpleForm
    {
        public ExportProgressDialog()
        {
            InitializeComponent();
            _ConfirmButton.Enabled = false;
        }

        public void SetTotalCount(uint count)
        {
            _GroupBox.Text = string.Format("数据量:{0}", count);
            _ProgressBar.Minimum = 0;
            _ProgressBar.Maximum = (int) count;
            _ProgressBar.Step = 1;
        }

        public void SetCurrentCount(int currentCount)
        {
            this.ThreadSafeInvoke(() => _ProgressBar.Value = currentCount + 1);
        }

        public void SetPath(string path)
        {
            _PathTextBox.Text = path;
        }

        private void _ConfirmButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        public void SetFinished()
        {
            _ConfirmButton.Enabled = true;
            _GroupBox.Text = string.Format("{0},导出已完成", _GroupBox.Text);
        }
    }
}
