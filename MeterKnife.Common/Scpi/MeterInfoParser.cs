using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using MeterKnife.Common.Interfaces;
using NKnife.Interface;

namespace MeterKnife.Common.Scpi
{
    public class MeterInfoParser : IParser<FileInfo[], IMeter[]>
    {
        public IMeter[] Parse(FileInfo[] fileInfos)
        {
            if (fileInfos == null)
                throw new ArgumentNullException("fileInfos");

            return null;
        }
    }
}
