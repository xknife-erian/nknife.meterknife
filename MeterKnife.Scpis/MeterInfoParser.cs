using System;
using System.IO;
using System.Xml;
using NKnife.Interface;
using ScpiKnife;

namespace MeterKnife.Scpis
{
    public class MeterInfoParser : IParser<FileInfo, ScpiSubjectCollection>
    {
        public string Brand { get; private set; }

        public string Name { get; private set; }

        public bool TryParse(FileInfo fileInfo, out ScpiSubjectCollection scpiSubjectList)
        {
            if (fileInfo == null)
                throw new ArgumentNullException("fileInfo");

            if (!fileInfo.Exists)
            {
                scpiSubjectList = null;
                return false;
            }
            var xmldoc = new XmlDocument();
            xmldoc.Load(fileInfo.FullName);

            var meterinfoElement = xmldoc.SelectSingleNode("//meterinfo") as XmlElement;
            if (meterinfoElement == null)
            {
                scpiSubjectList = null;
                return false;
            }
            Brand = meterinfoElement.GetAttribute("brand");
            Name = meterinfoElement.GetAttribute("name");

            var node = xmldoc.SelectSingleNode("//scpigroups");
            var scpigroups = node as XmlElement;
            if (scpigroups == null)
            {
                scpiSubjectList = null;
                return false;
            }
            scpiSubjectList = new ScpiSubjectCollection();

            foreach (var subjectNode in scpigroups.ChildNodes)
            {
                if (!(subjectNode is XmlElement))
                    continue;
                var scpiSubject = new ScpiSubject();
                var ele = subjectNode as XmlElement;
                scpiSubject.Description = ele.GetAttribute("description");

                var initGroupElement = ele.SelectSingleNode("//group[@way='init']") as XmlElement;
                scpiSubject.Preload = new ScpiGroup();
                if (initGroupElement != null)
                {
                    foreach (XmlElement scpiElement in initGroupElement.ChildNodes)
                    {
                        var scpiCommand = ScpiCommand.Parse(scpiElement);
                        if (scpiCommand == null)
                            continue;
                        scpiSubject.Preload.AddLast(scpiCommand);
                    }
                }

                var collectGroupElement = ele.SelectSingleNode("//group[@way='collect']") as XmlElement;
                scpiSubject.Collect = new ScpiGroup();
                if (collectGroupElement != null)
                {
                    foreach (XmlElement scpiElement in collectGroupElement.ChildNodes)
                    {
                        var scpiCommand = ScpiCommand.Parse(scpiElement);
                        if (scpiCommand == null)
                            continue;
                        scpiSubject.Collect.AddLast(scpiCommand);
                    }
                }
                scpiSubjectList.Add(scpiSubject);
            }

            return true;
        }
    }
}
