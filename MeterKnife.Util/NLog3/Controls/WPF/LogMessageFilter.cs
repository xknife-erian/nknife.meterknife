using System.Collections;
using System.Collections.Generic;
using NLog;

namespace NKnife.NLog3.Controls.WPF
{
    public class LogMessageFilter : IList<LogLevel>
    {
        private readonly List<LogLevel> _Levels = new List<LogLevel>(6);

        public LogMessageFilter()
        {
            _Levels.Add(LogLevel.Trace);
            _Levels.Add(LogLevel.Debug);
            _Levels.Add(LogLevel.Info);
            _Levels.Add(LogLevel.Warn);
            _Levels.Add(LogLevel.Error);
            _Levels.Add(LogLevel.Fatal);
        }

        public IEnumerator<LogLevel> GetEnumerator()
        {
            return _Levels.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(LogLevel item)
        {
            if (!_Levels.Contains(item))
                _Levels.Add(item);
        }

        public void Clear()
        {
            _Levels.Clear();
        }

        public bool Contains(LogLevel item)
        {
            return _Levels.Contains(item);
        }

        public void CopyTo(LogLevel[] array, int arrayIndex)
        {
            _Levels.CopyTo(array, arrayIndex);
        }

        public bool Remove(LogLevel item)
        {
            return _Levels.Remove(item);
        }

        public int Count
        {
            get { return _Levels.Count; }
        }

        bool ICollection<LogLevel>.IsReadOnly
        {
            get { return ((ICollection<LogLevel>) _Levels).IsReadOnly; }
        }

        public int IndexOf(LogLevel item)
        {
            return _Levels.IndexOf(item);
        }

        public void Insert(int index, LogLevel item)
        {
            _Levels.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            _Levels.RemoveAt(index);
        }

        public LogLevel this[int index]
        {
            get { return _Levels[index]; }
            set { _Levels[index] = value; }
        }
    }
}