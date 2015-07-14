using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using MeterKnife.Common.Interfaces;
using NKnife.Interface;

namespace MeterKnife.Common.Scpi
{
    public class MeterInfoParser : IParser<XmlDocument, IMeter>
    {
        protected XmlDocument _SourceXmlDocument;

        public IMeter Parse(XmlDocument source)
        {
            if(source==null)
                throw new ArgumentNullException("source");
            _SourceXmlDocument = source;
        }
    }
}
