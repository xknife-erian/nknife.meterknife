using System;
using System.Xml.Serialization;

namespace NKnife.Entities
{
    [Serializable]
    public class AssemblyWrapper<T>
    {
        /// <summary>
        /// 是否加载
        /// </summary>
        public bool Enable { get; set; }

        /// <summary>
        /// 插件所在程序集
        /// </summary>
        public string Assembly { get; set; }

        /// <summary>
        /// 类别名
        /// </summary>
        public string TypeName { get; set; }

        /// <summary>
        /// 实例对象
        /// </summary>
        [XmlIgnore]
        public T ClassInstance { get; set; }

    }
}