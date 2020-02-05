using System;

namespace NKnife.MeterKnife.Common.Scpi.Parser
{
    public class ScpiException : Exception
    {

        public ScpiException(string value)
            : base(value)
        {
        }

    }
}
