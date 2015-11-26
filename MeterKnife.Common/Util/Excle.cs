using System;
using System.Data;
using System.Drawing;
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

            var font = (XSSFFont)book.CreateFont();
            font.FontName = "Arial";
            font.FontHeightInPoints = 9;

            var cnFont = (XSSFFont)book.CreateFont();
            font.FontName = "微软雅黑";
            font.FontHeightInPoints = 9;

            var dataTimeStyle = book.CreateCellStyle();
            dataTimeStyle.SetFont(font);
            dataTimeStyle.Alignment = HorizontalAlignment.Left;
            var dataTimeFormat = book.CreateDataFormat();
            dataTimeStyle.DataFormat = dataTimeFormat.GetFormat("yyyy/m/d HH:MM:ss");

            var baseNumberStyle = book.CreateCellStyle();
            baseNumberStyle.SetFont(font);
            baseNumberStyle.Alignment = HorizontalAlignment.Left;

            var doubleStyle = book.CreateCellStyle();
            doubleStyle.SetFont(font);
            doubleStyle.Alignment = HorizontalAlignment.Left;
            var numberFormat = book.CreateDataFormat();
            doubleStyle.DataFormat = numberFormat.GetFormat("0.000000000000");

            var cnStyle = book.CreateCellStyle();
            cnStyle.SetFont(cnFont);
            cnStyle.Alignment = HorizontalAlignment.Left;

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
                    var row = sheets[i].CreateRow(0);

                    var cell = row.CreateCell(0);
                    cell.CellStyle = cnStyle;
                    cell.SetCellValue("时间");
                    cell = row.CreateCell(1);
                    cell.CellStyle = cnStyle;
                    cell.SetCellValue("值");
                    cell = row.CreateCell(2);
                    cell.CellStyle = cnStyle;
                    cell.SetCellValue("温度");
                    cell = row.CreateCell(3);
                    cell.CellStyle = cnStyle;
                    cell.SetCellValue("离散系数");
                }
                for (var i = 0; i < tableRows.Count; i++)
                {
                    var k = i/SINGLE_SHEET_ROWS;
                    var sheet = sheets[k];
                    var row = sheet.CreateRow(i + 1);
                    var array = tableRows[i].ItemArray;
                    for (var j = 0; j < array.Length; j++)
                    {
                        if (j > 3)
                            continue;
                        try
                        {
                            if (array[j] is DateTime)
                            {
                                var datetime = (DateTime) array[j];
                                var cell = row.CreateCell(j);
                                cell.CellStyle = dataTimeStyle;
                                cell.SetCellValue(datetime);
                            }
                            else if (array[j] is double)
                            {
                                var d = (double) array[j];
                                var cell = row.CreateCell(j);
                                cell.CellStyle = baseNumberStyle;
                                cell.SetCellValue(d);
                            }
                            else if (array[j] is int)
                            {
                                var d = (int) array[j];
                                var cell = row.CreateCell(j);
                                cell.CellStyle = baseNumberStyle;
                                cell.SetCellValue(d);
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