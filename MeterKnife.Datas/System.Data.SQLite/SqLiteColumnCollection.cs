using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Data.SQLite
{

    public class SqliteColumnCollection : IList<SqliteColumn>
    {
        readonly List<SqliteColumn> _List = new List<SqliteColumn>();

        private void CheckColumnName(string colName)
        {
            if (_List.Any(t => t.ColumnName == colName))
            {
                throw new Exception("Column name of \"" + colName + "\" is already existed.");
            }
        }

        public int IndexOf(SqliteColumn item)
        {
            return _List.IndexOf(item);
        }

        public void Insert(int index, SqliteColumn item)
        {
            CheckColumnName(item.ColumnName);

            _List.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            _List.RemoveAt(index);
        }

        public SqliteColumn this[int index]
        {
            get
            {
                return _List[index];
            }
            set
            {
                if (_List[index].ColumnName != value.ColumnName)
                {
                    CheckColumnName(value.ColumnName);
                }

                _List[index] = value;
            }
        }

        public void Add(SqliteColumn item)
        {
            CheckColumnName(item.ColumnName);

            _List.Add(item);
        }

        public void Clear()
        {
            _List.Clear();
        }

        public bool Contains(SqliteColumn item)
        {
            return _List.Contains(item);
        }

        public void CopyTo(SqliteColumn[] array, int arrayIndex)
        {
            _List.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return _List.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(SqliteColumn item)
        {
            return _List.Remove(item);
        }

        public IEnumerator<SqliteColumn> GetEnumerator()
        {
            return _List.GetEnumerator();
        }

        Collections.IEnumerator Collections.IEnumerable.GetEnumerator()
        {
            return _List.GetEnumerator();
        }
    }

}
