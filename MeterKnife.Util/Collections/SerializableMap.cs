using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using NKnife.Utility;

namespace NKnife.Collections
{
    /// <summary>描述一个可序列化的Map类型, 网上流传的SerializableDictionary偏于简单，细节考虑不够
    /// </summary>
    /// <typeparam name="TK">The type of the K.</typeparam>
    /// <typeparam name="TV">The type of the V.</typeparam>
    [Serializable]
    public class SerializableMap<TK, TV> : Dictionary<TK, TV>, IXmlSerializable, ISerializable, ICloneable
    {
        #region 常量

        private const string ITEM_NODE_NAME = "Item";
        private const string KEY_NODE_NAME = "Key";
        private const string VALUE_NODE_NAME = "Value";

        #endregion

        #region 私有变量

        protected XmlSerializer ValueSerializer
        {
            get { return UtilitySerialize.GetSerializer(typeof(TV)); }
        }

        private XmlSerializer KeySerializer
        {
            get { return UtilitySerialize.GetSerializer(typeof(TK)); }
        }

        #endregion

        #region 构造函数

        public SerializableMap()
        {
        }

        public SerializableMap(IDictionary<TK, TV> dictionary)
            : base(dictionary)
        {
        }

        public SerializableMap(IEqualityComparer<TK> comparer)
            : base(comparer)
        {
        }

        public SerializableMap(int capacity)
            : base(capacity)
        {
        }

        public SerializableMap(IDictionary<TK, TV> dictionary, IEqualityComparer<TK> comparer)
            : base(dictionary, comparer)
        {
        }

        public SerializableMap(int capacity, IEqualityComparer<TK> comparer)
            : base(capacity, comparer)
        {
        }

        protected SerializableMap(SerializationInfo info, StreamingContext context)
        {
            int itemCount = info.GetInt32("ItemCount");
            for (int i = 0; i < itemCount; i++)
            {
                var kvp = (KeyValuePair<TK, TV>)info.GetValue(String.Format("Item{0}", i), typeof(KeyValuePair<TK, TV>));
                Add(kvp.Key, kvp.Value);
            }
        }

        #endregion

        #region ISerializable Members

        void ISerializable.GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("ItemCount", Count);
            int itemIdx = 0;
            foreach (var kvp in this)
            {
                info.AddValue(String.Format("Item{0}", itemIdx), kvp, typeof (KeyValuePair<TK, TV>));
                itemIdx++;
            }
        }

        #endregion

        #region IXmlSerializable Members

        void IXmlSerializable.WriteXml(XmlWriter writer)
        {
            foreach (var pair in this)
            {
                writer.WriteStartElement(ITEM_NODE_NAME);
                writer.WriteStartElement(KEY_NODE_NAME);
                KeySerializer.Serialize(writer, pair.Key);
                writer.WriteEndElement();
                writer.WriteStartElement(VALUE_NODE_NAME);
                ValueSerializer.Serialize(writer, pair.Value);
                writer.WriteEndElement();
                writer.WriteEndElement();
            }
        }

        void IXmlSerializable.ReadXml(XmlReader reader)
        {
            if (reader.IsEmptyElement)
                return;

            if (!reader.Read())
                throw new XmlException("XmlReader读取异常");

            while (reader.NodeType != XmlNodeType.EndElement && reader.NodeType != XmlNodeType.None)
            {
                reader.ReadStartElement(ITEM_NODE_NAME);
                reader.ReadStartElement(KEY_NODE_NAME);
                var key = (TK) KeySerializer.Deserialize(reader);
                reader.ReadEndElement();
                reader.ReadStartElement(VALUE_NODE_NAME);
                var value = (TV) ValueSerializer.Deserialize(reader);
                reader.ReadEndElement();
                reader.ReadEndElement();
                Add(key, value);
                reader.MoveToContent();
            }
            reader.ReadEndElement(); 
        }

        XmlSchema IXmlSerializable.GetSchema()
        {
            return null;
        }

        #endregion

        #region Implementation of ICloneable

        public object Clone()
        {
            var newObj = new SerializableMap<TK, TV>();
            foreach (var pair in this)
            {
                newObj.Add(pair.Key, pair.Value);
            }
            return newObj;
        }

        #endregion
    }
}