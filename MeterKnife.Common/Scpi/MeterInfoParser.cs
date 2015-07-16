using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using MeterKnife.Common.Base;
using MeterKnife.Common.Interfaces;
using NKnife.Interface;
using NKnife.IoC;
using ScpiKnife;

namespace MeterKnife.Common.Scpi
{
    public class MeterInfoParser : IParser<FileInfo, List<ScpiSubject>>
    {
        public bool TryParse(FileInfo fileInfo, out List<ScpiSubject> scpiSubjectList)
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
            var scpigroups = xmldoc.SelectSingleNode("scpigroups") as XmlElement;
            if (scpigroups == null)
            {
                scpiSubjectList = null;
                return false;
            }
            scpiSubjectList = new List<ScpiSubject>();

            foreach (var subjectNode in scpigroups.ChildNodes)
            {
                if (!(subjectNode is XmlElement))
                    continue;
                var scpiSubject = new ScpiSubject();

                var ele = subjectNode as XmlElement;
                var initGroupElement = ele.GetElementByName("init");
                scpiSubject.Preload = new ScpiGroup();
                foreach (XmlElement scpiElement in initGroupElement.ChildNodes)
                {
                    var scpiCommand = ScpiCommand.Parse(scpiElement);
                    if (scpiCommand == null)
                        continue;
                    scpiSubject.Preload.AddLast(scpiCommand);
                }

                var collectGroupElement = ele.GetElementByName("collect");
                scpiSubject.Collect = new ScpiGroup();
                foreach (XmlElement scpiElement in collectGroupElement.ChildNodes)
                {
                    var scpiCommand = ScpiCommand.Parse(scpiElement);
                    if (scpiCommand == null)
                        continue;
                    scpiSubject.Collect.AddLast(scpiCommand);
                }
                scpiSubjectList.Add(scpiSubject);
            }

            return true;
        }

        /*{
            var meterinfoElement = xmldoc.SelectSingleNode("\\meterinfo") as XmlElement;
            if (meterinfoElement == null)
            {
                scpiSubjects = null;
                return false;
            }
            var type = meterinfoElement.GetAttribute("type").ToLower();
            var brand = meterinfoElement.GetAttribute("brand");
            var name = meterinfoElement.GetAttribute("name");
            meter = DI.Get<BaseMeter>(type);
            meter.Brand = brand;
            meter.Name = name;
        }*/
    }
}
