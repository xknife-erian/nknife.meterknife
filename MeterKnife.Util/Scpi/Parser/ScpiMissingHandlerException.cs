namespace NKnife.Scpi.Parser
{
    public class ScpiMissingHandlerException : ScpiException
    {
        private ScpiMissingHandlerException(string value)
            : base(value)
        {
        }
    }
}