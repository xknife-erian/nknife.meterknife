using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Common.Logging;
using MeterKnife.Common.DataModels;
using MeterKnife.Common.Util;
using NKnife.GUI.WinForm;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace MeterKnife.Common.Winforms.Dialogs
{
    public partial class ExportProgressDialog : SimpleForm
    {
        private static readonly ILog _logger = LogManager.GetLogger<ExportProgressDialog>();
        private FiguredData _FiguredData;

        private string _FileFullPath;

        public ExportProgressDialog()
        {
            InitializeComponent();
            _ConfirmButton.Enabled = false;
            Shown += (s, e) =>
            {
                var thread = new Thread(o =>
                {
                    Thread.Sleep(200);
                    Run(_FiguredData);
                });
                thread.Name = string.Format("ExportProgressThread-{0}", Guid.NewGuid().ToString().Substring(0, 6));
                thread.Start();
            };
        }

        public void SetPath(string path)
        {
            _FileFullPath = path;
            _PathTextBox.Text = path;
        }

        public void SetFigureData(FiguredData figuredData)
        {
            _FiguredData = figuredData;
            _GroupBox.Text = string.Format("数据量:{0}", _FiguredData.Count);
            _ProgressBar.Minimum = 0;
            _ProgressBar.Maximum = int.Parse(_FiguredData.Count) + 5*10;
            _ProgressBar.Step = 1;
        }

        private void _ConfirmButton_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void Run(FiguredData figuredData)
        {
            this.ThreadSafeInvoke(() =>
            {
                Cursor = Cursors.WaitCursor;
                _ProgressBar.Focus();
            });

            XSSFWorkbook book;

            var success = Excle.BuildWorkbook(figuredData.DataSet, GetUiAction(), out book);

            if (success)
            {
                foreach (var sheet in book)
                {
                    ((ISheet) sheet).AutoSizeColumn(0);
                }
                this.ThreadSafeInvoke(() =>
                {
                    Text = "导出数据结束，整理表格列： 第1列...";
                    _ProgressBar.Value = _ProgressBar.Value + 10;
                });
                foreach (var sheet in book)
                {
                    ((ISheet) sheet).AutoSizeColumn(1);
                }
                this.ThreadSafeInvoke(() =>
                {
                    Text = "导出数据结束，整理表格列： 第2列...";
                    _ProgressBar.Value = _ProgressBar.Value + 10;
                });
                foreach (var sheet in book)
                {
                    ((ISheet) sheet).AutoSizeColumn(2);
                }
                this.ThreadSafeInvoke(() =>
                {
                    Text = "导出数据结束，整理表格列： 第3列...";
                    _ProgressBar.Value = _ProgressBar.Value + 10;
                });
                foreach (var sheet in book)
                {
                    ((ISheet)sheet).AutoSizeColumn(3);
                }
                this.ThreadSafeInvoke(() =>
                {
                    Text = "导出数据结束，整理表格列： 第4列...";
                    _ProgressBar.Value = _ProgressBar.Value + 10;
                });

                using (var file = new FileStream(_FileFullPath, FileMode.Create))
                {
                    book.Write(file);
                    file.Close();
                }
                this.ThreadSafeInvoke(() => _ProgressBar.Value = _ProgressBar.Value + 10);
                this.ThreadSafeInvoke(() =>
                {
                    _GroupBox.Text = string.Format("{0},导出已完成", _GroupBox.Text);
                    Text = "导出全部完成，可关闭。";
                    _ConfirmButton.Enabled = true;
                    _ConfirmButton.Focus();
                    Cursor = Cursors.Default;
                });
            }
        }

        protected virtual Action<int> GetUiAction()
        {
            return n => { this.ThreadSafeInvoke(() => _ProgressBar.Value = (n + 1)); };
        }


    }
}