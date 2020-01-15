using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using Common.Logging;
using NKnife.Configuring.Interfaces;
using NKnife.Interface;

namespace NKnife.Configuring.OptionCase
{
    /// <summary>
    /// 往往应用程序的选项可以是多份，每一份在匹配的场景或时段下被使用，在这里我们理解一份选项是一个广义的实例，
    /// 本类型描述多份选项实例的管理器。
    /// </summary>
    public abstract class AbstractOptionCaseManager : IOptionCaseManager
    {
        private static readonly ILog _logger = LogManager.GetCurrentClassLogger();
        private readonly IList<OptionCaseItem> _InnerList = new List<OptionCaseItem>();
        private readonly XmlSerializer _Serializer = new XmlSerializer(typeof (OptionCaseItem));

        protected abstract XmlElement GetInfoXmlElement();

        #region IOptionCaseManager Members

        /// <summary>
        /// 根据指定的含有管理器序列化信息的XML节点初始化选项实例的管理器。
        /// </summary>
        public void Initialize()
        {
            _InnerList.Clear();
            var element = GetInfoXmlElement();
            if (element == null || element.NodeType != XmlNodeType.Element || element.ChildNodes.Count <= 0)
            {
                _InnerList.Add(OptionCaseItem.GetBase());
                _logger.Warn(@"初始化选项实例管理器时不可以传入空。");
                return;
            }
            foreach (XmlElement node in element.ChildNodes)
            {
                XmlCDataSection cdnode = node.GetCDataElement();
                if (cdnode != null)
                {
                    string cd = cdnode.InnerText.Trim();
                    if (!string.IsNullOrWhiteSpace(cd))
                    {
                        TextReader textReader = new StringReader(cd);
                        object obj = null;
                        try
                        {
                            obj = _Serializer.Deserialize(textReader);
                        }
                        catch (Exception e)
                        {
                            _logger.Warn(string.Format("反序列化Solution对象失败。{0}", e.Message), e);
                        }
                        if (obj != null)
                        {
                            _InnerList.Add((OptionCaseItem)obj);
                        }
                    }
                }
                else if (!string.IsNullOrWhiteSpace(node.GetAttribute("Name")))
                {
                    var os = new OptionCaseItem {Name = node.GetAttribute("Name")};
                    Add(os);
                }
            }
        }

        /// <summary>按照当前的场景或时段匹配的OptionCaseItem
        /// </summary>
        public OptionCaseItem Current()
        {
            return _InnerList.FirstOrDefault(item => item.Matching(DateTime.Now));
        }

        #endregion

        #region Implementation of IEnumerable

        public IEnumerator<OptionCaseItem> GetEnumerator()
        {
            return _InnerList.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region Implementation of ICollection<Solution>

        public void Add(OptionCaseItem solution)
        {
            _InnerList.Add(solution);
        }

        public void Clear()
        {
            while (_InnerList.Count > 0)
                RemoveAt(0);
        }

        public bool Contains(OptionCaseItem item)
        {
            return _InnerList.Contains(item);
        }

        public void CopyTo(OptionCaseItem[] array, int arrayIndex)
        {
            _InnerList.CopyTo(array, arrayIndex);
        }

        public bool Remove(OptionCaseItem item)
        {
            int i = _InnerList.IndexOf(item);
            try
            {
                RemoveAt(i);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public int Count
        {
            get { return _InnerList.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        #endregion

        #region Implementation of IList<Solution>

        public int IndexOf(OptionCaseItem item)
        {
            return _InnerList.IndexOf(item);
        }

        public void Insert(int index, OptionCaseItem item)
        {
            Add(item);
        }

        public void RemoveAt(int index)
        {
            _InnerList.RemoveAt(index);
        }

        public OptionCaseItem this[int index]
        {
            get { return _InnerList[index]; }
            set
            {
                _InnerList[index] = value;
                RemoveAt(index);
                Add(value);
            }
        }

        #endregion
    }
}