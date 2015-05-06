using System;
using System.Collections.Generic;
using System.Text;

namespace System.Data.SQLite
{
    public class SqliteTable
    {
        public string TableName = "";
        private SqliteColumnCollection _Columns = new SqliteColumnCollection();

        public SqliteColumnCollection Columns
        {
            get { return _Columns; }
            set { _Columns = value; }
        }

        public SqliteTable()
        { }

        public SqliteTable(string name)
        {
            TableName = name;
        }
    }
}