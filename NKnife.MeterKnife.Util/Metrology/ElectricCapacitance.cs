namespace NKnife.Metrology
{
    /// <summary>
    ///     电容
    /// </summary>
    public class ElectricCapacitance : IMetrology
    {
        #region Implementation of IMetrology

        public string[] Units { get; } = {"F", "mF", "μF", "nF", "pF"};

        #endregion
    }
}