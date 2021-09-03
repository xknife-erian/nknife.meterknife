namespace NKnife.Metrology
{
    /// <summary>
    ///     Electric current: Ampere (A) The basic unit of electric current is the ampere. The ampere is defined as the
    ///     constant current that, if maintained in two infinitely long straight parallel conductors with a negligible circular
    ///     cross-section and placed 1 m apart in a vacuum, would produce a force between the conductors equal to 2 x 10-7
    ///     newtons per meter of length.
    ///     真空中相距1米的两根无限长且圆截面可忽略的平行直导线内通过一恒定电流，当两导线每米长度之间产生的力等于2×10－7牛顿时，则规定导线中通过的电流为1安培。
    ///     这个单位是在1946年国际计量委员会上得到批准，1960年第十一届国际计量大会上被正式采用为国际单位制的基本单位之一。
    ///     实际上，定义中的两根“无限长”导线是无法实现的，根据电动力学原理，可以用作用力相似等效的两个线圈替代。
    ///     1960年10月第11届国际计量大会决定采用安培为电学量的基本单位，并作为国际单位制的基本单位。
    ///     1国际安培=0.99985绝对安培。
    ///     2018年11月16日，国际计量大会通过决议，1安培定义为“1s内通过导体某一横截面的1/1.602176634×10^19个电荷移动所产生的电流强度”。
    /// </summary>
    public class ElectricCurrent : BaseMetrology
    {
        #region Implementation of IMetrology

        public override string[] Units { get; } = { "A", "mA", "μA", "nA" };
        public override short UnitIndex { get; } = 0;
        public override string Name { get; set; } = "电流";

        #endregion
    }
}