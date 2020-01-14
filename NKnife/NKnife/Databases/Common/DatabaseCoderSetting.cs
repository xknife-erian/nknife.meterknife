using System.Collections.Generic;
using System.Xml;

namespace NKnife.Databases.Common
{
    public abstract class DatabaseCoderSetting //: CoderSettingModule
    {
        /// <summary>解析数据库相关的一些原始SQL语句
        /// </summary>
        /// <param name="parms"></param>
        /// <param name="itemEle"></param>
        private static void SchemaParse(DatabaseParms parms, XmlElement itemEle)
        {
            foreach (XmlNode item in itemEle.ChildNodes)
            {
                if (item.NodeType != XmlNodeType.Element)
                    continue;
                switch (item.LocalName.ToLower())
                {
                    #region case

                    case "table": //创建表
                        parms.Table = item.InnerText;
                        break;
                    case "index": //索引
                        parms.Index = item.InnerText;
                        break;
                    case "view": //视图
                        {
                            parms.View = new List<string>();
                            foreach (XmlNode view in item.ChildNodes)
                                parms.View.Add(view.InnerText.Trim());
                            break;
                        }
                    case "trigger": //触发器
                        {
                            parms.OnTable = new List<string>();
                            foreach (XmlNode view in item.ChildNodes)
                                parms.View.Add(view.InnerText.Trim());
                            break;
                        }
                    default:
                        break;

                    #endregion
                }
            }
            return;
        }
    }
}