using System.Linq;
using System.Text;

namespace NKnife.MeterKnife.Util.Tunnel.Generic
{
    public class FixedTailEncoder : StringDatagramEncoder
    {
        private byte[] _Tail = Encoding.Default.GetBytes("\r\n");

        public virtual byte[] Tail
        {
            get { return _Tail; }
            set { _Tail = value; }
        }

        protected virtual byte[] GetBytes(string replay)
        {
            return Encoding.Default.GetBytes(replay);
        }

        public override byte[] Execute(string replay)
        {
            if (string.IsNullOrEmpty(replay))
                return _Tail;
            var r = GetBytes(replay);
            return r.Concat(_Tail).ToArray();
        }
    }
}
