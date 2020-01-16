namespace NKnife.Protocol.Generic.TextPlain
{
    class TextPlainProtocolFlags
    {
        public static char _SplitFlag = '|';
        public static string _InfomationSplitFlag = "<~>";

        public static char SplitFlag
        {
            get { return _SplitFlag; }
            set { _SplitFlag = value; }
        }

        public static string InfomationSplitFlag
        {
            get { return _InfomationSplitFlag; }
            set { _InfomationSplitFlag = value; }
        }
    }
}
