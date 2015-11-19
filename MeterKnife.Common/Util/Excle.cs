using System;
using System.Data;
using Common.Logging;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;

namespace MeterKnife.Common.Util
{
    public class Excle
    {
        public const int SINGLE_SHEET_ROWS = 65536*4;
        public const int SINGLE_MAX_ROWS = 1048576;
        private static readonly ILog _logger = LogManager.GetLogger<Excle>();

        public static bool BuildWorkbook(DataSet dataSet, Action<int> uiAction, out XSSFWorkbook book)
        {
            book = new XSSFWorkbook();

            var dateStyle = book.CreateCellStyle();
            dateStyle.Alignment = HorizontalAlignment.Left;
            var format = book.CreateDataFormat();
            dateStyle.DataFormat = format.GetFormat("yyyy/m/d HH:MM:ss");

            try
            {
                var tableRows = dataSet.Tables[1].Rows;
                if (tableRows.Count > SINGLE_MAX_ROWS - 1)
                    return false;
                var n = tableRows.Count/SINGLE_SHEET_ROWS;
                var sheets = new ISheet[n + 1];
                for (var i = 0; i < n + 1; i++)
                {
                    sheets[i] = book.CreateSheet(string.Format("测量数据{0}", i + 1));
                }
                for (var i = 0; i < tableRows.Count; i++)
                {
                    var k = i/SINGLE_SHEET_ROWS;
                    var sheet = sheets[k];
                    var row = sheet.CreateRow(i);
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
                            _logger.Warn(string.Format("导出数据有异常:{0}/{1} -->{2}", i, j, e.Message));
                        }
                    }
                    var flag = i;
                    uiAction.Invoke(flag);
                }
                return true;
            }
            catch (Exception e)
            {
                _logger.Error(string.Format("导出到Excle出现异常:{0}", e.Message), e);
                return false;
            }
        }
    }
}