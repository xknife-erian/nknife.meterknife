namespace NKnife.Scpi.Parser
{
    public class ScpiCommandCaller
    {
        readonly IScpiCommandHandler _Handler;
        readonly string[] _Args;

        public ScpiCommandCaller(IScpiCommandHandler handler, string[] args)
        {
            this._Handler = handler;
            this._Args = args;
        }

        public string Execute()
        {
            return _Handler.Handle(_Args);
        }
    }
}
