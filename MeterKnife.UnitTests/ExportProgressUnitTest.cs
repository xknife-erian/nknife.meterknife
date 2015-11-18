using System;
using System.Data;
using MeterKnife.Common.Controls.Dialogs;
using MeterKnife.Common.DataModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;

namespace MeterKnife.UnitTests
{
    [TestClass]
    public class ExportProgressUnitTest
    {
        [TestMethod]
        public void BuildWorkbookBaseTest()
        {
            const int SIZE = 512;

            ExportProgressDialogTestClass exportClass = new ExportProgressDialogTestClass();
            var dataSet = new DataSet();
            dataSet.Tables.Add(new DataTable("1#table"));
            dataSet.Tables.Add(new DataTable("2#table"));
            dataSet.Tables[1].Columns.Add(new DataColumn("column1", typeof (double)));
            for (int i = 0; i < SIZE; i++)
            {
                dataSet.Tables[1].Rows.Add(i);
            }
            HSSFWorkbook book;
            var success = exportClass.BuildWorkbookTest(dataSet, out book);
            Assert.IsTrue(success);
            Assert.AreEqual(SIZE - 1, book[0].LastRowNum);
        }

        [TestMethod]
        public void BuildWorkbookLargeDataTest1()
        {
            const int SIZE = 65536;
            ExportProgressDialogTestClass exportClass = new ExportProgressDialogTestClass();
            var dataSet = new DataSet();
            dataSet.Tables.Add(new DataTable("1#table"));
            dataSet.Tables.Add(new DataTable("2#table"));
            dataSet.Tables[1].Columns.Add(new DataColumn("column1", typeof(double)));
            for (int i = 0; i < SIZE; i++)
            {
                dataSet.Tables[1].Rows.Add(i);
            }
            HSSFWorkbook book;
            var success = exportClass.BuildWorkbookTest(dataSet, out book);
            Assert.IsTrue(success);
            Assert.AreEqual(SIZE - 1, book[0].LastRowNum);
        }

        [TestMethod]
        public void BuildWorkbookLargeDataTest2()
        {
            const int SIZE = 65536 + 1;
            ExportProgressDialogTestClass exportClass = new ExportProgressDialogTestClass();
            var dataSet = new DataSet();
            dataSet.Tables.Add(new DataTable("1#table"));
            dataSet.Tables.Add(new DataTable("2#table"));
            dataSet.Tables[1].Columns.Add(new DataColumn("column1", typeof(double)));
            for (int i = 0; i < SIZE; i++)
            {
                dataSet.Tables[1].Rows.Add(i);
            }
            HSSFWorkbook book;
            var success = exportClass.BuildWorkbookTest(dataSet, out book);
            Assert.IsTrue(success);
            Assert.AreEqual(2, book.Count);
            Assert.AreEqual(SIZE - 2, book[0].LastRowNum);
            Assert.AreEqual(SIZE - 1, book[1].LastRowNum);
        }

        class ExportProgressDialogTestClass : ExportProgressDialog
        {
            protected override Action GetInnerAction(int i)
            {
                return delegate {  };
            }

            public bool BuildWorkbookTest(DataSet dataSet, out HSSFWorkbook book)
            {
                return BuildWorkbook(dataSet, out book);
            }
        }
    }
}
