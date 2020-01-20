using System.Collections.Generic;
using System.Diagnostics;
using System.Xml;

namespace NKnife.MeterKnife.Util.Scpi
{
    /// <summary>
    /// 面向一个工作指令主题的指令集合
    /// </summary>
    public class ScpiSubject
    {
        public ScpiSubject()
        {
            Collect = new ScpiPool {Category = PoolCategory.Collect};
            Initializtion = new ScpiPool {Category = PoolCategory.Initializtion};
        }

        /// <summary>
        /// 工作指令主题的描述
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 初始化指令集合
        /// </summary>
        public ScpiPool Initializtion { get; set; }
        /// <summary>
        /// 采集指令集合
        /// </summary>
        public ScpiPool Collect { get; set; }

        /// <summary>
        /// 指令集合所在的主题集合
        /// </summary>
        public ScpiSubjectCollection OwnerCollection { get; set; }

        public void Build(ref XmlElement element)
        {
            Debug.Assert(element.OwnerDocument != null, "element.OwnerDocument != null");
            var document = element.OwnerDocument;
            if (document == null)
                return;

            element.RemoveAll();
            element.SetAttribute("description", Name);

            var groupElement = document.CreateElement("group");
            Initializtion.Build(ref groupElement);
            element.AppendChild(groupElement);

            groupElement = document.CreateElement("group");
            Collect.Build(ref groupElement);
            element.AppendChild(groupElement);
        }

        public static IEnumerable<ScpiSubject> Parse(XmlElement scpigroups)
        {
            var subjects = new List<ScpiSubject>();
            foreach (var subjectNode in scpigroups.ChildNodes)
            {
                if (!(subjectNode is XmlElement))
                    continue;
                var scpiSubject = new ScpiSubject();
                var ele = subjectNode as XmlElement;
                scpiSubject.Name = ele.GetAttribute("description");

                var groupNodes = ele.SelectNodes("group");
                if (groupNodes == null)
                {
                    continue;
                }
                foreach (XmlNode groupNode in groupNodes)
                {
                    if (!(groupNode is XmlElement))
                        continue;
                    var groupElement = (XmlElement) groupNode;
                    if (groupElement.HasAttribute("way"))
                    {
                        var way = groupElement.GetAttribute("way");
                        switch (way)
                        {
                            case "init":
                                scpiSubject.Initializtion = ScpiPool.Prase(groupElement);
                                break;
                            case "collect":
                                scpiSubject.Collect = ScpiPool.Prase(groupElement);
                                break;
                        }
                    }
                }
                subjects.Add(scpiSubject);
            }
            return subjects;
        }

    }
}