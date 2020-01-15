using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using Ninject.Modules;
using NKnife.Adapters;
using NKnife.Interface;

namespace NKnife.Configuring
{
    /// <summary>
    ///     CoderSetting（程序员配置）服务管理器
    /// </summary>
    public abstract class CoderSettingModule : NinjectModule
    {
        private static readonly ILogger _Logger = LogFactory.GetCurrentClassLogger();
        protected readonly string _SettingFileBasePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, @"");//Configs\");

        protected CoderSettingModule()
        {
            SettingMap = new Dictionary<string, string>();
        }

        protected Dictionary<string, string> SettingMap { get; set; }

        public override void Load()
        {
            FileInfo file = GetSettingFile();
            if (!file.Exists)
            {
                _Logger.Warn(string.Format("配置文件{0}不存在", file.FullName));
                return;
            }

            PutSettingMap();
            foreach (var pair in SettingMap)
            {
                var type = GetBindType();
                Bind(typeof(string)).ToConstant(pair.Value).Named(pair.Key);
            }
        }

        protected abstract Type GetBindType();

        protected virtual void PutSettingMap()
        {
            var xml = new XmlDocument();
            try
            {
                xml.Load(GetSettingFile().FullName);
            }
            catch (Exception e)
            {
                _Logger.Warn(string.Format("Setting XML文件{0}载入异常", GetSettingFile().FullName), e);
                return;
            }
            if (xml.DocumentElement == null)
            {
                return;
            }
            XmlElement root = xml.DocumentElement;
            foreach (object childNode in root.ChildNodes)
            {
                if (!(childNode is XmlElement))
                    continue;
                var ele = (XmlElement) childNode;
                if (!SettingMap.ContainsKey(ele.Name))
                {
                    PutSetting(SettingMap, ele);
                }
            }
        }

        protected virtual void PutSetting(Dictionary<string, string> map, XmlElement ele)
        {
            map.Add(ele.Name, ele.InnerText);
        }

        protected abstract FileInfo GetSettingFile();
    }
}