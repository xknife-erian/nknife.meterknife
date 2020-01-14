using System;
using System.IO;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;

namespace NKnife.Configuring
{
    public static class ProfileManager
    {
        /// <summary>
        /// 配置文件存储路径
        /// </summary>
        static readonly string _ProfilePath;
        static ProfileManager()
        {
            _ProfilePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        }

        #region 存取配置文件

        /// <summary>
        /// 加载配置
        /// </summary>
        /// <typeparam name="T">配置类型</typeparam>
        /// <returns></returns>
        public static T LoadConfig<T>(string filefullname) where T : class, new()
        {
            try
            {
                Type t = typeof (T);
                T obj = null;
                using (var sr = new StreamReader(filefullname, Encoding.Unicode))
                {
                    var xml = new XmlSerializer(t);
                    obj = xml.Deserialize(sr) as T;
                    sr.Close();
                }
                return obj;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        /// <summary>
        /// 加载配置
        /// </summary>
        /// <returns></returns>
        public static object LoadConfig(Type type, string configName)
        {
            if (type == null) throw new ArgumentNullException("type");

            string path = Path.Combine(_ProfilePath, configName + ".xml");
            if (!File.Exists(path)) 
                return null;
            else
            {
                object obj = null;
                using (var sr = new StreamReader(path, Encoding.Unicode))
                {
                    var xml = new XmlSerializer(type);
                    obj = xml.Deserialize(sr);
                    sr.Close();
                }
                return obj;
            }
        }

        /// <summary>
        /// 保存配置
        /// </summary>
        /// <param name="cfg">要保存的配置对象</param>
        /// <param name="fullpath">要保存的完整路径与文件名</param>
        public static void SaveConfig(object cfg, string fullpath)
        {
            if (cfg == null) throw new ArgumentNullException();

            Type t = cfg.GetType();
            using (var sr = new StreamWriter(fullpath, false, Encoding.Unicode))
            {
                var xml = new XmlSerializer(t);
                xml.Serialize(sr, cfg);
                sr.Close();
            }
        }

        /// <summary>
        /// 保存配置
        /// </summary>
        /// <param name="configName"></param>
        /// <param name="cfg">要保存的配置对象</param>
        public static void SaveConfig(string configName, object cfg)
        {
            //TxtLogExecute.AddTxtLog("调用profile->saveconfig");
            if (cfg == null) throw new ArgumentNullException();

            Type t = cfg.GetType();
            string path = Path.Combine(_ProfilePath, configName + ".xml");
            Directory.CreateDirectory(Path.GetDirectoryName(path));

            using (var sr = new StreamWriter(path, false, Encoding.Unicode))
            {
                var xml = new XmlSerializer(t);
                xml.Serialize(sr, cfg);
                sr.Close();
            }
        }

        #endregion
    }
}
