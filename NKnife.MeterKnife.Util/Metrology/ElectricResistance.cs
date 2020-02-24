namespace NKnife.Metrology
{
    /// <summary>
    ///     电阻
    /// </summary>
    public class ElectricResistance : BaseMetrology
    {
        #region Implementation of IMetrology

        public override string[] Units { get; } = { "MΩ", "kΩ", "Ω", "mΩ", "μΩ" };
        public override short UnitIndex { get; } = 2;
        public override string Name { get; set; } = "电阻";

        #endregion
    }
}