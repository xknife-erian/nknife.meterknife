using System;

namespace MeterKnife.Util.Scpi.Parser
{
    public class ScpiException : Exception
    {

        public ScpiException(string value)
            : base(value)
        {
        }

    }
}
