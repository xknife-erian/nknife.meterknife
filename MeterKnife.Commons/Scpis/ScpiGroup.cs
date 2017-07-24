using System.Collections.Generic;
using System.Diagnostics;
using System.Xml;

namespace MeterKnife.Scpis
{
    /// <summary>
    ///     一组指令，将按顺序执行
    /// </summary>
    public class ScpiGroup : List<ScpiCommand>
    {
        public ScpiCommandGroupCategory Category { get; set; }

        public bool UpItem(int index)
        {
            if (index <= 0 || index > Count - 1)
                return false;
            var item = this[index];
            RemoveAt(index);
            Insert(index - 1, item);
            return true;
        }

        public bool DownItem(int index)
        {
            if (index < 0 || index >= Count - 1)
                return false;
            var item = this[index];
            RemoveAt(index);
            Insert(index + 1, item);
            return true;
        }

        public static ScpiGroup Prase(XmlElement groupElement)
        {
            var group = new ScpiGroup();
            group.Category = ScpiCommandGroupCategory.None;
            if (groupElement.HasAttribute("way"))
            {
                var way = groupElement.GetAttribute("way");
                switch (way)
                {
                    case "init":
                        group.Category = ScpiCommandGroupCategory.Initializtion;
                        break;
                    case "collect":
                        group.Category = ScpiCommandGroupCategory.Collect;
                        break;
                }
            }
            foreach (XmlElement scpiElement in groupElement.ChildNodes)
            {
                var scpiCommand = ScpiCommand.Parse(scpiElement);
                if (scpiCommand == null)
                    continue;
                group.Add(scpiCommand);
            }
            return group;
        }

        public void Build(ref XmlElement element)
        {
            element.RemoveAll();
            switch (Category)
            {
                case ScpiCommandGroupCategory.Collect:
                    element.SetAttribute("way", "collect");
                    break;
                case ScpiCommandGroupCategory.Initializtion:
                    element.SetAttribute("way", "init");
                    break;
                case ScpiCommandGroupCategory.None:
                    element.SetAttribute("way", "none");
                    break;
            }
            foreach (var scpiCommand in this)
            {
                Debug.Assert(element.OwnerDocument != null, "element.OwnerDocument != null");
                var commandElement = element.OwnerDocument.CreateElement("scpi");
                scpiCommand.Build(ref commandElement);
                element.AppendChild(commandElement);
            }
        }
    }
}