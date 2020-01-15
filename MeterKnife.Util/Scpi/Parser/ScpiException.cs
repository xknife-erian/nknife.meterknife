using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScpiKnife.Parser
{
    public class ScpiException : Exception
    {

        public ScpiException(string value)
            : base(value)
        {
        }

    }
}
