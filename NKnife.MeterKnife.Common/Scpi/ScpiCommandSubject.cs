using System.Collections.Generic;
using System.Diagnostics;
using System.Xml;

namespace NKnife.MeterKnife.Common.Scpi
{
    /// <summary>
    /// 面向一个工作指令主题的指令集合
    /// </summary>
    public class ScpiCommandSubject
    {
        public ScpiCommandSubject()
        {
            Collect = new ScpiCommandPool { Category = PoolCategory.Collect};
            Initializtion = new ScpiCommandPool { Category = PoolCategory.Initializtion};
        }

        /// <summary>
        /// 工作指令主题的描述
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 初始化指令集合
        /// </summary>
        public ScpiCommandPool Initializtion { get; set; }
        /// <summary>
        /// 采集指令集合
        /// </summary>
        public ScpiCommandPool Collect { get; set; }

        /// <summary>
        /// 指令集合所在的主题集合
        /// </summary>
        public ScpiCommandSubjectList OwnerList { get; set; }

        public void Build(ref XmlElement element)
        {
            Debug.Assert(element.OwnerDocument != null, "element.OwnerDocument != null");
            var document = element.OwnerDocument;
            if (document == null)
                return;

            element.RemoveAll();
            element.SetAttribute("description", Name);

            var groupElement = document.CreateElement("group");
            //Initializtion.Build(ref groupElement);
            element.AppendChild(groupElement);

            groupElement = document.CreateElement("group");
            //Collect.Build(ref groupElement);
            element.AppendChild(groupElement);
        }

        public static IEnumerable<ScpiCommandSubject> Parse(XmlElement scpigroups)
        {
            var subjects = new List<ScpiCommandSubject>();
            foreach (var subjectNode in scpigroups.ChildNodes)
            {
                if (!(subjectNode is XmlElement))
                    continue;
                var scpiSubject = new ScpiCommandSubject();
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
                                //scpiSubject.Initializtion = ScpiCommandPool.Prase(groupElement);
                                break;
                            case "collect":
                                //scpiSubject.Collect = ScpiCommandPool.Prase(groupElement);
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