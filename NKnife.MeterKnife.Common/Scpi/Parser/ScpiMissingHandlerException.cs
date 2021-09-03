namespace NKnife.MeterKnife.Common.Scpi.Parser
{
    public class ScpiMissingHandlerException : ScpiException
    {
        private ScpiMissingHandlerException(string value)
            : base(value)
        {
        }
    }
}