using System.Collections.Generic;
using System.Text;

namespace MeterKnife.Util.XML
{
    /// <summary>
    /// 一个快速格式化XML文件的工具类型
    /// </summary>
    public class XmlFormat
    {
        private Queue<string> _allChars = new Queue<string>(1000);
        private const int _front = 0x3C;
        private const int _end = 0x3E;
        private bool _isAdd = true;
        private int _pos = 0;

        /// <summary>
        /// 格式化一个指定的XML文件的文本(具体内容，而非XML文件)
        /// </summary>
        /// <param name="xml">The XML.</param>
        public void FormatXml(string xmlText)
        {
            string result = string.Empty;
            char[] cs = xmlText.ToCharArray();

            for (int i = 0; i < cs.Length; i++)
            {
                if (cs[i] == 0x20)
                    continue;

                switch ((int)cs[i])
                {
                    case _end:
                        result += (char)_end; int k = _pos;
                        if (_isAdd)
                            _pos++;
                        _allChars.Enqueue(AppendTab(k) + result);
                        result = string.Empty;
                        _isAdd = true;
                        continue;

                    case _front:
                        {
                            if (cs[i + 1] == 0x2F)
                            {
                                _isAdd = false;
                                if (!string.IsNullOrEmpty(result))
                                {
                                    _allChars.Enqueue(AppendTab(_pos) + result);
                                    result = string.Empty;
                                }
                                _pos--;
                            }
                            result += (char)_front;
                            continue;
                        }

                    default:
                        result += cs[i];
                        break;
                }
            }
        }

        private string AppendTab(int l)
        {
            StringBuilder sb = new StringBuilder("\r\n");
            for (int i = 0; i < l; i++)
                sb.Append("    ");
            return sb.ToString();
        }

        /// <summary>
        /// 生成格式友好的Xml文本
        /// </summary>
        /// <value>友好的Xml文本</value>
        public string ToFormattingXml()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var s in _allChars)
                sb.Append(s);
            return sb.ToString();
        }

        /// <summary>
        /// Returns a <see cref="System.String"/> that represents this instance.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String"/> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return this.ToFormattingXml();
        }
    }
}
