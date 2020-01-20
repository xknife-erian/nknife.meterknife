using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Xml;
using NKnife.Interface;

namespace NKnife.MeterKnife.Util.Scpi
{
    /// <summary>
    ///     一组指令，将按顺序执行
    /// </summary>
    public class ScpiPool : List<Scpi>
    {
        public PoolCategory Category { get; set; }

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

        public static ScpiPool Prase(XmlElement groupElement)
        {
            var group = new ScpiPool();
            group.Category = PoolCategory.None;
            if (groupElement.HasAttribute("way"))
            {
                var way = groupElement.GetAttribute("way");
                switch (way)
                {
                    case "init":
                        group.Category = PoolCategory.Initializtion;
                        break;
                    case "collect":
                        group.Category = PoolCategory.Collect;
                        break;
                }
            }

            foreach (XmlElement scpiElement in groupElement.ChildNodes)
            {
                var scpiCommand = Scpi.Parse(scpiElement);
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
                case PoolCategory.Collect:
                    element.SetAttribute("way", "collect");
                    break;
                case PoolCategory.Initializtion:
                    element.SetAttribute("way", "init");
                    break;
                case PoolCategory.None:
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