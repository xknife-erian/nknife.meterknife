using System.Data;
using MeterKnife.Common.Util;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NPOI.XSSF.UserModel;

namespace MeterKnife.UnitTests
{
    [TestClass]
    public class ExcleUnitTest
    {
        [TestMethod]
        public void BuildWorkbookBaseTest()
        {
            const int SIZE = 512;

            var dataSet = new DataSet();
            dataSet.Tables.Add(new DataTable("1#table"));
            dataSet.Tables.Add(new DataTable("2#table"));
            dataSet.Tables[1].Columns.Add(new DataColumn("column1", typeof (double)));
            for (var i = 0; i < SIZE; i++)
            {
                dataSet.Tables[1].Rows.Add(i);
            }
            XSSFWorkbook book;
            var success = Excle.BuildWorkbook(dataSet, i => { }, out book);
            Assert.IsTrue(success);
            Assert.AreEqual(SIZE - 1, book[0].LastRowNum);
        }

        [TestMethod]
        public void BuildWorkbook65536DataTest1()
        {
            const int SIZE = 65536;
            var dataSet = new DataSet();
            dataSet.Tables.Add(new DataTable("1#table"));
            dataSet.Tables.Add(new DataTable("2#table"));
            dataSet.Tables[1].Columns.Add(new DataColumn("column1", typeof (double)));
            for (var i = 0; i < SIZE; i++)
            {
                dataSet.Tables[1].Rows.Add(i);
            }
            XSSFWorkbook book;
            var success = Excle.BuildWorkbook(dataSet, i => { }, out book);
            Assert.IsTrue(success);
            Assert.AreEqual(SIZE - 1, book[0].LastRowNum);
        }

        [TestMethod]
        public void BuildWorkbookLargeDataTest2()
        {
            const int SIZE = Excle.SINGLE_SHEET_ROWS + 1;
            var dataSet = new DataSet();
            dataSet.Tables.Add(new DataTable("1#table"));
            dataSet.Tables.Add(new DataTable("2#table"));
            dataSet.Tables[1].Columns.Add(new DataColumn("column1", typeof (double)));
            for (var i = 0; i < SIZE; i++)
            {
                dataSet.Tables[1].Rows.Add(i);
            }
            XSSFWorkbook book;
            var success = Excle.BuildWorkbook(dataSet, i => { }, out book);
            Assert.IsTrue(success);
            Assert.AreEqual(2, book.Count);
            Assert.AreEqual(SIZE - 2, book[0].LastRowNum);
            Assert.AreEqual(SIZE - 1, book[1].LastRowNum);
        }

        [TestMethod]
        public void BuildWorkbookLargeDataTest3()
        {
            const int COUNT = 3;
            const int SIZE = Excle.SINGLE_SHEET_ROWS * COUNT + 1;
            var dataSet = new DataSet();
            dataSet.Tables.Add(new DataTable("1#table"));
            dataSet.Tables.Add(new DataTable("2#table"));
            dataSet.Tables[1].Columns.Add(new DataColumn("column1", typeof (int)));
            for (var i = 0; i < SIZE; i++)
            {
                dataSet.Tables[1].Rows.Add(i);
            }
            XSSFWorkbook book;
            var success = Excle.BuildWorkbook(dataSet, i => { }, out book);
            Assert.IsTrue(success);
            Assert.AreEqual(COUNT + 1, book.Count);
        }
    }
}