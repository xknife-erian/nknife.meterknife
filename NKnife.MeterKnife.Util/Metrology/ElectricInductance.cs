namespace NKnife.Metrology
{
    /// <summary>
    ///     电感
    /// </summary>
    public class ElectricInductance : IMetrology
    {
        #region Implementation of IMetrology

        public string[] Units { get; } = {"H", "mH", "μH", "nH"};

        #endregion
    }
}