namespace NKnife.MeterKnife.Common.Scpi.Parser
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
