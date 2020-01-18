using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace NKnife.MeterKnife.Util.Protocol.Generic
{
    /// <summary>
    ///     一个协议的具体内容。其中：
    ///     Tags,Infomations均属于协议的内容。
    ///     Tags:内容较大的数据，如序列化的对象等。
    ///     Infomations:固定数据，按协议规定的必须每次携带的数据。
    /// </summary>
    public class StringProtocolContent : IProtocolContent<string>
    {
        public StringProtocolContent()
        {
            Infomations = new Dictionary<string, string>(0);
            Tags = new List<object>(0);
        }

        /// <summary>
        ///     Returns a <see cref="System.String" /> that represents this instance.
        /// </summary>
        /// <returns>
        ///     A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            var sb = new StringBuilder();
            sb.Append("Command:").Append(Command).Append("[").Append(CommandParam).Append("] >>> \r\n");
            IEnumerator myEnumerator = Infomations.GetEnumerator();
            while ((myEnumerator.MoveNext()))
            {
                sb.Append("[").Append(myEnumerator.Current).Append("]");
            }
            sb.Append("\r\nTags Count:").Append(Tags.Count);
            return base.ToString();
        }

        public string Command { get; set; }
        public string CommandParam { get; set; }
        public Dictionary<string, string> Infomations { get; private set; }
        public List<object> Tags { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((StringProtocolContent) obj);
        }

        protected bool Equals(StringProtocolContent other)
        {
            return string.Equals(Command, other.Command) 
                && string.Equals(CommandParam, other.CommandParam)
                && Infomations.Compare(other.Infomations)
                && Tags.Compare(other.Tags);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hashCode = (Command != null ? Command.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (CommandParam != null ? CommandParam.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Infomations != null ? Infomations.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (Tags != null ? Tags.GetHashCode() : 0);
                return hashCode;
            }
        }
    }
}