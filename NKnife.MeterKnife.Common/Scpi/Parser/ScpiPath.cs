using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NKnife.MeterKnife.Common.Scpi.Parser
{
    public class ScpiPath
    {
        private readonly List<string> _Path = new List<string>();
        private int _HashCode = 5;

        public ScpiPath(string input)
        {
            _Path.AddRange(input.Split(new[] {"\\s*:\\s*"}, StringSplitOptions.None));
            UpdateHash();
        }

        public ScpiPath()
        {
            UpdateHash();
        }

        public IEnumerable<string> Iterator()
        {
            return _Path;
        }

        public void Clear()
        {
            _Path.Clear();
            UpdateHash();
        }

        public int Length()
        {
            return _Path.Count();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var path in _Path)
            {
                sb.Append(path).Append(':');
            }
            sb.Remove(sb.Length - 1, 1);
            return sb.ToString();
        }

        public ScpiPath Concat(ScpiPath otherPath)
        {
            _Path.AddRange(otherPath._Path);
            UpdateHash();
            return this;
        }

        public ScpiPath Copy()
        {
            var copy = new ScpiPath();
            copy._Path.AddRange(_Path);
            copy.UpdateHash();
            return copy;
        }

        public void Strip()
        {
            if (_Path.Any())
            {
                _Path.RemoveAt(_Path.Count() - 1);
                UpdateHash();
            }
        }

        public ScpiPath StripCopy()
        {
            var stripped = new ScpiPath();
            for (var i = 0; i < _Path.Count() - 2; i++)
            {
                stripped._Path.Add(_Path[i]);
            }
            stripped.UpdateHash();
            return stripped;
        }

        private void Append(string element)
        {
            _Path.Add(element);
            _HashCode = 53*GetHashCode() + element.GetHashCode();
        }

        private void UpdateHash()
        {
            var hash = _Path.Aggregate(5, (current, element) => 53*current + element.GetHashCode());
            _HashCode = hash;
        }

        public override int GetHashCode()
        {
            return _HashCode;
        }

        public override bool Equals(object obj)
        {
            if (obj == null)
            {
                return false;
            }
            if (obj is ScpiPath)
            {
                return false;
            }
            var other = (ScpiPath) obj;
            if (_HashCode != other._HashCode)
            {
                return false;
            }
            if (_Path.Count() != other._Path.Count())
            {
                return false;
            }
            for (var i = 0; i < _Path.Count(); i++)
            {
                if (!_Path[i].Equals(other._Path[i]))
                {
                    return false;
                }
            }
            return true;
        }
    }
}