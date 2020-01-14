using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Xml;
using NKnife.Configuring.Interfaces;
using NKnife.Configuring.Option;

namespace NKnife.Configuring.Common
{
    class UtilityOption
    {
        public static DataTable ParseTableNode(IOptionDataStore xmlDataStore, XmlNode childNode)
        {
            throw new NotImplementedException();
        }

        public static IOption ParseTableNode(IOptionDataStore xmlDataStore, string childNode)
        {
            throw new NotImplementedException();
        }
    }
}
