namespace NKnife.Metrology
{
    /// <summary>
    ///     电阻
    /// </summary>
    public class ElectricResistance : IMetrology
    {
        #region Implementation of IMetrology

        public string[] Units { get; } = { "MΩ", "kΩ", "Ω", "mΩ", "μΩ" };

        #endregion
    }
}