namespace NKnife.Metrology
{
    /// <summary>
    ///     Time: Second (s) The basic unit of time is the second. The second is defined as the duration of 9,192,631,770
    ///     oscillations of radiation corresponding to the transition between the two hyperfine levels of cesium-133.
    ///     秒。等于铯 133 原子基态的两个超精细能 级之间跃迁所对应的辐射的 9192631770 个周期的持续时间。
    /// </summary>
    public class Time : BaseMetrology
    {
        #region Implementation of IMetrology

        public override string[] Units { get; } = {"y","M","d","h","m","s","ms","ns","ps"};
        public override short UnitIndex { get; } = 6;
        public override string Name { get; set; } = "时间";

        #endregion
    }
}