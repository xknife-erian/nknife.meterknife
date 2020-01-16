using System.Globalization;
using System.Text;

namespace MeterKnife.Util.XML
{
    public abstract class AbstractXmlAttribute
    { 
        /// <summary>
        /// Xhtml节点的属性，类似：title="This is a Image!"。
        /// </summary>
        /// <param name="key">键，属性名。与大小写无关，全部强制转换成小写字母</param>
        /// <param name="value">值，属性值</param>
        internal AbstractXmlAttribute(string key, string value)
        {
            this.Key = key.ToLower(CultureInfo.CurrentCulture);
            this.Value = value;
        }

        /// <summary>
        /// 属性名
        /// </summary>
        public string Key { get; set; }
        /// <summary>
        /// 属性值
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Gean重写。
        /// </summary>
        public override bool Equals(object obj)
        {
            AbstractXmlAttribute att = (AbstractXmlAttribute)obj;
            if (this.Key != att.Key)
            {
                return false;
            }
            if (this.Value != att.Value)
            {
                return false;
            }
            return true;
        }
        /// <summary>
        /// Gean重写。
        /// </summary>
        public override int GetHashCode()
        {
            return unchecked(3 * (this.Key.GetHashCode() + this.Value.GetHashCode()));
        }
        /// <summary>
        /// Gean重写。生成真实的做为Xhtml中的属性的字符串格式。
        /// 属性名与大小写无关，全部转换成小写字母
        /// </summary>
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append(this.Key).Append("=\"").Append(this.Value).Append("\"");
            return sb.ToString();
        }

    }
}
