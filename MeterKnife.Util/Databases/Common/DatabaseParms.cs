using System.Collections.Generic;
using System.Collections.Specialized;

namespace NKnife.Databases.Common
{
    public class DatabaseParms
    {
        public string Name { get; internal set; }
        public NameValueCollection Configs { get; internal set; }
        public string Table { get; internal set; }
        public string Index { get; internal set; }
        public List<string> View { get; internal set; }
        public List<string> OnTable { get; internal set; }
    }
}
