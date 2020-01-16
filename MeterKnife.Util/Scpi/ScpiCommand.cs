using System;
using System.Text;
using System.Xml;
using MeterKnife.Util.Converts;
using MeterKnife.Util.XML;

namespace MeterKnife.Util.Scpi
{
    /// <summary>
    /// 针对SCPI标准命令的封装。
    /// SCPI，可编程仪器标准命令，是一种建立在现有标准IEEE488.1 和 IEEE 488.2 基础上，
    /// 并遵循了IEEE754 标准中浮点运算规则、ISO646 信息交换7位编码符号（相当于Ascii编
    /// 程）等多种标准的标准化仪器编程语言。
    /// </summary>
    public class ScpiCommand
    {
        /// <summary>
        /// 从一个XML节点解析SCPI命令
        /// </summary>
        /// <param name="element">一个按照规则制定描述SCPI命令的XML节点元素</param>
        /// <returns>SCPI命令类型</returns>
        public static ScpiCommand Parse(XmlElement element)
        {
            //样本
            //<scpi interval="200" hex="true" return="true" selected="false" description="读取万用表读数">
            //  <![CDATA[READ?]]>
            //</scpi>
            var command = new ScpiCommand();
            var cdata = element.GetCDataElement();
            if (cdata != null)
            {
                command.Command = cdata.InnerText;
            }
            int interval;
            if (!int.TryParse(element.GetAttribute("interval"), out interval))
                command.Interval = 200;
            command.Interval = interval;
            bool isHex;
            if (bool.TryParse(element.GetAttribute("hex"), out isHex))
                command.IsHex = isHex;
            bool selected;
            if (bool.TryParse(element.GetAttribute("selected"), out selected))
                command.Selected = selected;
            bool isReturn;
            if (bool.TryParse(element.GetAttribute("return"), out isReturn))
                command.IsReturn = isReturn;
            if (element.HasAttribute("description"))
                command.Description = element.GetAttribute("description");
            return command;
        }

        /// <summary>
        /// 根据当前值创建XML节点元素
        /// </summary>
        /// <param name="element">要返回的XML节点元素</param>
        public void Build(ref XmlElement element)
        {
            if (element == null)
                throw new ScpiParseException();
            element.SetAttribute("interval", Interval.ToString());
            element.SetAttribute("hex", IsHex.ToString());
            element.SetAttribute("return", IsReturn.ToString());
            if (!Selected)
                element.SetAttribute("selected", Selected.ToString());
            if (!string.IsNullOrEmpty(Description))
                element.SetAttribute("description", Description);
            element.SetCDataElement(Command);
        }

        public ScpiCommand()
        {
            Interval = 200;
            IsHex = false;
            IsReturn = true;
            Selected = true;
        }

        /// <summary>
        /// 命令主体
        /// </summary>
        public string Command { get; set; }

        /// <summary>
        /// 命令的等待周期,指等待仪器返回结果的超时时间
        /// </summary>
        public long Interval { get; set; }

        /// <summary>
        /// 命令需要仪器返回数据
        /// </summary>
        public bool IsReturn { get; set; }

        /// <summary>
        /// 命令主体是用原生字符串表达,还是16进制字符串表达
        /// </summary>
        public bool IsHex { get; set; }

        /// <summary>
        /// 命令是否被选择
        /// </summary>
        public bool Selected { get; set; }

        /// <summary>
        /// 命令的解释
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// 生成协议字节数组
        /// </summary>
        /// <param name="gpibAddress">协议体里的GPIB地址</param>
        /// <returns>协议字节数组</returns>
        public virtual byte[] GenerateProtocol(int gpibAddress)
        {
            byte mainCommand = IsReturn ? (byte) 0xAA : (byte) 0xAB;
            const byte SUB_COMMAND = 0x00;

            byte[] scpiBytes = IsHex ? UtilityConvert.HexToBytes(Command) : Encoding.ASCII.GetBytes(Command);

            var bs = new byte[] {0x08, (byte) gpibAddress, (byte) (scpiBytes.Length + 2), mainCommand, SUB_COMMAND};
            var result = new byte[bs.Length + scpiBytes.Length];
            Buffer.BlockCopy(bs, 0, result, 0, bs.Length);
            Buffer.BlockCopy(scpiBytes, 0, result, bs.Length, scpiBytes.Length);
            return result;
        }

        public override string ToString()
        {
            return string.Format("{0}\r\n{1}\r\n{2}", Command, Interval, Description);
        }
    }
}