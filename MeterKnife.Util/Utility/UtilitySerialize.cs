using System;
using System.Collections.Concurrent;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace MeterKnife.Util.Utility
{
    public static class UtilitySerialize
    {
        private static readonly ConcurrentDictionary<string, XmlSerializer> _serializerMap = new ConcurrentDictionary<string, XmlSerializer>();

        /// <summary>XmlSerializer的实例的生成效率不高，故保存已生成的实例，以提高效率。
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        internal static XmlSerializer GetSerializer(Type type)
        {
            XmlSerializer serializer = null;
            if (type.FullName != null && !_serializerMap.TryGetValue(type.FullName, out serializer))
            {
                serializer = new XmlSerializer(type);
                _serializerMap.TryAdd(type.FullName, serializer);
            }
            return serializer;
        }

        public static string Serialize(object o)
        {
            var sb = new StringBuilder();
            Type type = o.GetType();
            XmlSerializer serializer = GetSerializer(type);
            var writer = new StringWriter(sb);
            serializer.Serialize(writer, o);
            return sb.ToString();
        }

        public static T Deserialize<T>(string xml)
        {
            Type type = typeof (T);
            XmlSerializer serializer = GetSerializer(type);
            var sr = new StringReader(xml);
            object obj = serializer.Deserialize(sr);
            return (T) obj;
        }

        public static object Deserialize(string xml, Type type)
        {
            XmlSerializer serializer = GetSerializer(type);
            var sr = new StringReader(xml);
            object obj = serializer.Deserialize(sr);
            return obj;
        }
    }
}