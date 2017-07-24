using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Xml;

namespace MeterKnife.Scpis
{
    /// <summary>
    ///     指令主题集合
    /// </summary>
    public class ScpiSubjectCollection : List<ScpiSubject>
    {
        protected ScpisXmlFile _ScpiFile;

        public ScpiSubjectCollection()
        {
            Version = new Version("1.0");
        }

        public Version Version { get; set; }

        /// <summary>
        ///     指令主题所属的仪器品牌
        /// </summary>
        public string Brand { get; set; }

        /// <summary>
        ///     指令主题所属的仪器型号
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     指令主题所属的仪器型号
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        ///     指令集合所在的Xml文件
        /// </summary>
        public void BuildScpiFile(string fileFullName)
        {
            _ScpiFile = new ScpisXmlFile(fileFullName); //如果文件不存在，会自动创建
        }

        public ScpisXmlFile GetXmlFile()
        {
            return _ScpiFile;
        }

        public bool Save()
        {
            Debug.Assert(_ScpiFile.DocumentElement != null, "_ScpiFile.DocumentElement != null");
            //仪器信息部份
            _ScpiFile.DocumentElement.SetAttribute("version", Version.ToString());

            var meterinfoNode = _ScpiFile.DocumentElement.SelectSingleNode("//information");
            if (meterinfoNode == null)
            {
                meterinfoNode = _ScpiFile.NewElement("information");
                _ScpiFile.DocumentElement.AppendChild(meterinfoNode);
            }
            var meterinfoElement = (XmlElement) meterinfoNode;
            meterinfoElement.SetAttribute("brand", Brand);
            meterinfoElement.SetAttribute("name", Name);
            meterinfoElement.SetAttribute("description", Description);

            //命令集部份
            var groups = _ScpiFile.Groups();
            if(groups == null)
            {
                groups = _ScpiFile.NewElement("scpigroups");
                _ScpiFile.DocumentElement.AppendChild(groups);
            }
            groups.RemoveAll();

            foreach (var scpiSubject in this)
            {
                var element = _ScpiFile.NewElement("subject");
                scpiSubject.Build(ref element);
                groups.AppendChild(element);
            }

            _ScpiFile.Save();
            return true;
        }

        public bool TryParse(IScpiFileVersionProcessor scpiFileVersionProcessor, bool isCompleteParse = true)
        {
            if (scpiFileVersionProcessor != null)
                _ScpiFile = scpiFileVersionProcessor.Update(_ScpiFile, true);

            //仪器信息部份
            Version = new Version("1.0");
            if (_ScpiFile.DocumentElement.HasAttribute("version"))
                Version = Version.Parse(_ScpiFile.DocumentElement.GetAttribute("version")); //Scpi文件格式版本

            var meterinfoElement = _ScpiFile.DocumentElement.SelectSingleNode("//information") as XmlElement;
            if (meterinfoElement == null)
                return false;
            Brand = meterinfoElement.GetAttribute("brand");
            Name = meterinfoElement.GetAttribute("name");
            Description = meterinfoElement.GetAttribute("description");

            if (isCompleteParse) //当有些时候只需要解析文件中仪器的基本信息时
            {
                //命令集部份-----------------
                var scpigroups = _ScpiFile.Groups();
                if (scpigroups == null)
                {
                    return false;
                }
                var array = ScpiSubject.Parse(scpigroups);
                foreach (var scpiSubject in array)
                {
                    scpiSubject.OwnerCollection = this;
                    Add(scpiSubject);
                }
            }

            Sort((x, y) =>
            {
                CultureInfo ci = CultureInfo.CurrentUICulture;
                return ci.CompareInfo.Compare(x.Name, y.Name);
            });

            return true;
        }
    }
}