using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using NKnife.Events;

namespace NKnife.Collections
{
    /// <summary>
    /// 一个灵活的针对键值对进行了扩展的集合类型。
    /// 本类型内部容器是一个 Dictionary{string, object}。
    /// 本类型对序列化已实现。
    /// 本类型实现了XmlReader的从文件读取的静态方法。
    /// 2009-12-19 14:42:08
    /// </summary>
    public class Definition : IEnumerable
    {
        #region Delegates

        public delegate void DefinitionChangedEventHandler(object sender, DefinitionChangedEventArgs e);

        #endregion

        protected Dictionary<string, object> _Definitions = new Dictionary<string, object>();

        public string this[string key]
        {
            get { return Convert.ToString(Get(key), CultureInfo.InvariantCulture); }
            set { Set(key, value); }
        }

        public string[] Elements
        {
            get
            {
                lock (_Definitions)
                {
                    return _Definitions.Select(property => property.Key).ToArray();
                }
            }
        }

        public int Count
        {
            get
            {
                lock (_Definitions)
                {
                    return _Definitions.Count;
                }
            }
        }

        public object Get(string key)
        {
            lock (_Definitions)
            {
                object val;
                _Definitions.TryGetValue(key, out val);
                return val;
            }
        }

        public void Set<T>(string key, T value)
        {
            T oldValue = default(T);
            lock (_Definitions)
            {
                if (!_Definitions.ContainsKey(key))
                {
                    _Definitions.Add(key, value);
                }
                else
                {
                    oldValue = Get(key, value);
                    _Definitions[key] = value;
                }
            }
            OnDefinitionChanged(new DefinitionChangedEventArgs(this, key, oldValue, value));
        }

        public bool Contains(string key)
        {
            lock (_Definitions)
            {
                return _Definitions.ContainsKey(key);
            }
        }

        public bool Remove(string key)
        {
            lock (_Definitions)
            {
                return _Definitions.Remove(key);
            }
        }

        public override string ToString()
        {
            lock (_Definitions)
            {
                var sb = new StringBuilder();
                sb.Append("[Properties:{");
                foreach (var entry in _Definitions)
                {
                    sb.Append(entry.Key);
                    sb.Append("=");
                    sb.Append(entry.Value);
                    sb.Append(",");
                }
                sb.Append("}]");
                return sb.ToString();
            }
        }

        internal void ReadDefinition(XmlReader reader, string endElement)
        {
            if (reader.IsEmptyElement)
            {
                return;
            }
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.EndElement:
                        if (reader.LocalName == endElement)
                        {
                            return;
                        }
                        break;
                    case XmlNodeType.Element:
                        string propertyName = reader.LocalName;
                        if (propertyName == "Properties")
                        {
                            propertyName = reader.GetAttribute(0);
                            var p = new Definition();
                            p.ReadDefinition(reader, "Properties");
                            if (propertyName != null) _Definitions[propertyName] = p;
                        }
                        else if (propertyName == "Array")
                        {
                            propertyName = reader.GetAttribute(0);
                            if (propertyName != null) _Definitions[propertyName] = ReadArray(reader);
                        }
                        else if (propertyName == "SerializedValue")
                        {
                            propertyName = reader.GetAttribute(0);
                            if (propertyName != null) _Definitions[propertyName] = new SerializedValue(reader.ReadInnerXml());
                        }
                        else
                        {
                            _Definitions[propertyName] = reader.HasAttributes ? reader.GetAttribute(0) : null;
                        }
                        break;
                }
            }
        }

        private ArrayList ReadArray(XmlReader reader)
        {
            if (reader.IsEmptyElement)
                return new ArrayList(0);
            var list = new ArrayList();
            while (reader.Read())
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.EndElement:
                        if (reader.LocalName == "Array")
                        {
                            return list;
                        }
                        break;
                    case XmlNodeType.Element:
                        list.Add(reader.HasAttributes ? reader.GetAttribute(0) : null);
                        break;
                }
            }
            return list;
        }

        public void WriteDefinition(XmlWriter writer)
        {
            lock (_Definitions)
            {
                var sortedProperties = new List<KeyValuePair<string, object>>(_Definitions);
                sortedProperties.Sort((a, b) => StringComparer.OrdinalIgnoreCase.Compare(a.Key, b.Key));
                foreach (var entry in sortedProperties)
                {
                    object val = entry.Value;
                    if (val is Definition)
                    {
                        writer.WriteStartElement("Properties");
                        writer.WriteAttributeString("name", entry.Key);
                        ((Definition) val).WriteDefinition(writer);
                        writer.WriteEndElement();
                    }
                    else if (val is Array || val is ArrayList)
                    {
                        writer.WriteStartElement("Array");
                        writer.WriteAttributeString("name", entry.Key);
                        foreach (object o in (IEnumerable) val)
                        {
                            writer.WriteStartElement("Element");
                            WriteValue(writer, o);
                            writer.WriteEndElement();
                        }
                        writer.WriteEndElement();
                    }
                    else if (TypeDescriptor.GetConverter(val).CanConvertFrom(typeof (string)))
                    {
                        writer.WriteStartElement(entry.Key);
                        WriteValue(writer, val);
                        writer.WriteEndElement();
                    }
                    else if (val is SerializedValue)
                    {
                        writer.WriteStartElement("SerializedValue");
                        writer.WriteAttributeString("name", entry.Key);
                        writer.WriteRaw(((SerializedValue) val).Content);
                        writer.WriteEndElement();
                    }
                    else
                    {
                        writer.WriteStartElement("SerializedValue");
                        writer.WriteAttributeString("name", entry.Key);
                        var serializer = new XmlSerializer(val.GetType());
                        serializer.Serialize(writer, val, null);
                        writer.WriteEndElement();
                    }
                }
            }
        }

        private void WriteValue(XmlWriter writer, object val)
        {
            if (val != null)
            {
                if (val is string)
                {
                    writer.WriteAttributeString("value", val.ToString());
                }
                else
                {
                    TypeConverter c = TypeDescriptor.GetConverter(val.GetType());
                    writer.WriteAttributeString("value", c.ConvertToInvariantString(val));
                }
            }
        }

        public void Save(string fileName)
        {
            using (var writer = new XmlTextWriter(fileName, Encoding.UTF8))
            {
                writer.Formatting = Formatting.Indented;
                writer.WriteStartElement("Properties");
                WriteDefinition(writer);
                writer.WriteEndElement();
            }
        }

        /// <summary>
        /// [未实现的]
        /// </summary>
        /// <param name="writer">The writer.</param>
        [Obsolete]
        public void BinarySerialize(BinaryWriter writer)
        {
        }

        public static Definition ReadFromAttributes(XmlReader reader)
        {
            var properties = new Definition();
            if (reader.HasAttributes)
            {
                for (int i = 0; i < reader.AttributeCount; i++)
                {
                    reader.MoveToAttribute(i);
                    properties[reader.Name] = reader.Value;
                }
                reader.MoveToElement(); //Moves the reader back to the element node.
            }
            return properties;
        }

        public static Definition Load(string fileName)
        {
            if (!File.Exists(fileName))
            {
                return null;
            }
            using (var reader = new XmlTextReader(fileName))
            {
                while (reader.Read())
                {
                    if (reader.IsStartElement())
                    {
                        switch (reader.LocalName)
                        {
                            case "Properties":
                                var properties = new Definition();
                                properties.ReadDefinition(reader, "Properties");
                                return properties;
                        }
                    }
                }
            }
            return null;
        }

        public T Get<T>(string property, T defaultValue)
        {
            lock (_Definitions)
            {
                object o;
                if (!_Definitions.TryGetValue(property, out o))
                {
                    _Definitions.Add(property, defaultValue);
                    return defaultValue;
                }

                if (o is string && typeof (T) != typeof (string))
                {
                    TypeConverter c = TypeDescriptor.GetConverter(typeof (T));
                    try
                    {
                        o = c.ConvertFromInvariantString(o.ToString());
                    }
                    catch (Exception ex)
                    {
                        o = defaultValue;
                    }
                    _Definitions[property] = o; // store for future look up
                }
                else if (o is ArrayList && typeof (T).IsArray)
                {
                    var list = (ArrayList) o;
                    Type elementType = typeof (T).GetElementType();
                    Array arr = Array.CreateInstance(elementType, list.Count);
                    TypeConverter c = TypeDescriptor.GetConverter(elementType);
                    try
                    {
                        for (int i = 0; i < arr.Length; ++i)
                        {
                            if (list[i] != null)
                            {
                                arr.SetValue(c.ConvertFromInvariantString(list[i].ToString()), i);
                            }
                        }
                        o = arr;
                    }
                    catch (Exception ex)
                    {
                        o = defaultValue;
                    }
                    _Definitions[property] = o; // store for future look up
                }
                else if (!(o is string) && typeof (T) == typeof (string))
                {
                    TypeConverter c = TypeDescriptor.GetConverter(typeof (T));
                    if (c.CanConvertTo(typeof (string)))
                    {
                        o = c.ConvertToInvariantString(o);
                    }
                    else
                    {
                        o = o.ToString();
                    }
                }
                else if (o is SerializedValue)
                {
                    try
                    {
                        o = ((SerializedValue) o).Deserialize<T>();
                    }
                    catch (Exception ex)
                    {
                        o = defaultValue;
                    }
                    _Definitions[property] = o; // store for future look up
                }
                try
                {
                    return (T) o;
                }
                catch (NullReferenceException)
                {
                    // can happen when configuration is invalid -> o is null and a value type is expected
                    return defaultValue;
                }
            }
        }

        public event DefinitionChangedEventHandler DefinitionChangedEvent;

        protected virtual void OnDefinitionChanged(DefinitionChangedEventArgs e)
        {
            if (DefinitionChangedEvent != null)
                DefinitionChangedEvent(this, e);
        }

        #region Nested type: DefinitionChangedEventArgs

        public class DefinitionChangedEventArgs : ChangedEventArgs<object>
        {
            public DefinitionChangedEventArgs(Definition properties, string key, object oldValue, object newValue)
                : base(oldValue, newValue)
            {
                Definition = properties;
                Key = key;
            }

            /// <summary>
            /// Gets or sets 父级容器
            /// </summary>
            /// <value>The definition.</value>
            public Definition Definition { get; private set; }

            /// <summary>
            /// Gets or sets the key.
            /// </summary>
            /// <value>The key.</value>
            public string Key { get; private set; }
        }

        #endregion

        #region Nested type: SerializedValue

        /// <summary> 
        /// 需反序列化的特定对象
        /// </summary>
        private sealed class SerializedValue
        {
            private readonly string _Content;

            public SerializedValue(string content)
            {
                _Content = content;
            }

            public string Content
            {
                get { return _Content; }
            }

            public T Deserialize<T>()
            {
                var serializer = new XmlSerializer(typeof (T));
                return (T) serializer.Deserialize(new StringReader(_Content));
            }
        }

        #endregion

        #region IEnumerable 成员

        public IEnumerator GetEnumerator()
        {
            return _Definitions.GetEnumerator();
        }

        #endregion
    }
}