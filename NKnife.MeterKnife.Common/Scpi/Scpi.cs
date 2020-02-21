namespace NKnife.MeterKnife.Common.Scpi
{
    /// <summary>
    ///     针对SCPI标准命令的封装。
    ///     SCPI，可编程仪器标准命令，是一种建立在现有标准IEEE488.1 和 IEEE 488.2 基础上，
    ///     并遵循了IEEE754 标准中浮点运算规则、ISO646 信息交换7位编码符号（相当于Ascii编
    ///     程）等多种标准的标准化仪器编程语言。
    /// </summary>
    public class SCPI
    {
        /// <summary>
        ///     命令名称
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        ///     命令主体
        /// </summary>
        public string Command { get; set; }

        /// <summary>
        ///     命令的解释
        /// </summary>
        public string Description { get; set; }

        public override string ToString()
        {
            return $"{Name}/{Command}";
        }
    }
}