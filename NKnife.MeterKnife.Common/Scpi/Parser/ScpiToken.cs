namespace NKnife.MeterKnife.Common.Scpi.Parser
{
    public class ScpiToken
    {

        public ScpiTokenType _TokenType;
        public string _Data;

        public ScpiToken(ScpiTokenType tokenType, string data)
        {
            _TokenType = tokenType;
            _Data = data;
        }
    }
}
