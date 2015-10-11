using System;
using System.IO;
using System.Threading;
using System.Windows.Forms;
using Common.Logging;
using MeterKnife.Common.DataModels;
using NKnife.GUI.WinForm;
using NPOI.HSSF.UserModel;
using HorizontalAlignment = NPOI.SS.UserModel.HorizontalAlignment;

namespace MeterKnife.Common.Controls.Dialogs
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
            _ProgressBar.Maximum = int.Parse(_FiguredData.Count) + 4*10;
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
            var book = new HSSFWorkbook();

            var sheet1 = book.CreateSheet("测量数据");
            
            var dateStyle = book.CreateCellStyle();
            dateStyle.Alignment = HorizontalAlignment.Left;
            var format = book.CreateDataFormat();
            dateStyle.DataFormat = format.GetFormat("yyyy/m/d HH:MM:ss");

            var tableRows = figuredData.DataSet.Tables[1].Rows;
            for (var i = 0; i < tableRows.Count; i++)
            {
                var row = sheet1.CreateRow(i);
                var array = tableRows[i].ItemArray;
                for (var j = 0; j < array.Length; j++)
                {
                    try
                    {
                        if (array[j] is DateTime)
                        {
                            var datetime = (DateTime) array[j];
                            var cell = row.CreateCell(j);
                            cell.CellStyle = dateStyle;
                            cell.SetCellValue(datetime);
                        }
                        else if (array[j] is double)
                        {
                            var d = (double) array[j];
                            row.CreateCell(j).SetCellValue(d);
                        }
                        else if (array[j] is int)
                        {
                            var d = (int) array[j];
                            row.CreateCell(j).SetCellValue(d);
                        }
                        else
                        {
                            row.CreateCell(j).SetCellValue(array[j].ToString());
                        }
                    }
                    catch (Exception e)
                    {
                        _logger.Warn(string.Format("导出数据有异常:{0}-{1} -->{2}", i, j, e.Message));
                    }
                }
                var flag = i;
                this.ThreadSafeInvoke(() => _ProgressBar.Value = flag + 1);
            }

            sheet1.AutoSizeColumn(0);
            this.ThreadSafeInvoke(() =>
            {
                Text = "导出数据结束，整理表格列1...";
                _ProgressBar.Value = _ProgressBar.Value + 10;
            });

            sheet1.AutoSizeColumn(1);
            this.ThreadSafeInvoke(() =>
            {
                Text = "导出数据结束，整理表格列2...";
                _ProgressBar.Value = _ProgressBar.Value + 10;
            });

            sheet1.AutoSizeColumn(2);
            this.ThreadSafeInvoke(() =>
            {
                Text = "导出数据结束，整理表格列3...";
                _ProgressBar.Value = _ProgressBar.Value + 10;
            });

            using (var file = new FileStream(_FileFullPath, FileMode.Create))
            {
                book.Write(file);
                file.Flush();
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
}