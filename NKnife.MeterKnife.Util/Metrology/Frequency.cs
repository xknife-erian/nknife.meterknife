namespace NKnife.Metrology
{
    /// <summary>
    ///     频率
    /// </summary>
    public class Frequency : BaseMetrology
    {
        #region Implementation of IMetrology

        public override string[] Units { get; } = { "GHz", "MHz", "kHz", "Hz" };
        public override short UnitIndex { get; } = 3;
        public override string Name { get; set; } = "频率";

        #endregion
    }
}