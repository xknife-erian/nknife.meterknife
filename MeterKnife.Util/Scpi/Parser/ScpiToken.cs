using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScpiKnife.Parser
{
    public class ScpiToken
    {

        public ScpiTokenType _TokenType;
        public string _Data;

        ScpiToken(ScpiTokenType tokenType, string data)
        {
            this._TokenType = tokenType;
            this._Data = data;
        }
    }
}
