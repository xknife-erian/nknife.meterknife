using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ScpiKnife.Parser
{
    public class ScpiTokenType
    {
        public static List<string> Values { get; set; }

        static ScpiTokenType()
        {
            Values = new List<string>();
            Values.Add(_COLON);
            Values.Add(_SEMICOLON);
            Values.Add(_QUOTEDstring);
            Values.Add(_COMMAND);
            Values.Add(_ARGUMENT);
            Values.Add(_WHITESPACE);
            Values.Add(_NEWLINE);
            Values.TrimExcess();
        }

        private const string _COLON = (":");
        private const string _SEMICOLON = (";");
        private const string _QUOTEDstring = "\"[^\"]*?\"";
        private const string _COMMAND = ("[a-zA-z*_?]+");
        private const string _ARGUMENT = ("[a-zA-z0-9\\.]+");
        private const string _WHITESPACE = ("[ \t]+");
        private const string _NEWLINE = ("[\r\n]+");

        public string COLON
        {
            get { return _COLON; }
        }

        public string SEMICOLON
        {
            get { return _SEMICOLON; }
        }

        public string QUOTEDstring
        {
            get { return _QUOTEDstring; }
        }

        public string COMMAND
        {
            get { return _COMMAND; }
        }

        public string ARGUMENT
        {
            get { return _ARGUMENT; }
        }

        public string WHITESPACE
        {
            get { return _WHITESPACE; }
        }

        public string NEWLINE
        {
            get { return _NEWLINE; }
        }
    }
}
