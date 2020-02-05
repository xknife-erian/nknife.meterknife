using System.Collections.Generic;

namespace NKnife.MeterKnife.Common.Scpi.Parser
{
    public class ScpiTokenType
    {
        public static List<string> Values { get; set; }

        static ScpiTokenType()
        {
            Values = new List<string>();
            Values.Add(COLON);
            Values.Add(SEMICOLON);
            Values.Add(QUOTED_STRING);
            Values.Add(COMMAND);
            Values.Add(ARGUMENT);
            Values.Add(WHITE_SPACE);
            Values.Add(NEWLINE);
            Values.TrimExcess();
        }

        public const string COLON = (":");
        public const string SEMICOLON = (";");
        public const string QUOTED_STRING = "\"[^\"]*?\"";
        public const string COMMAND = ("[a-zA-z*_?]+");
        public const string ARGUMENT = ("[a-zA-z0-9\\.]+");
        public const string WHITE_SPACE = ("[ \t]+");
        public const string NEWLINE = ("[\r\n]+");

    }
}
