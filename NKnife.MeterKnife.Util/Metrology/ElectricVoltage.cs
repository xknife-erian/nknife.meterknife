namespace NKnife.Metrology
{
    /// <summary>
    ///     电压
    /// </summary>
    public class ElectricVoltage : IMetrology
    {
        #region Implementation of IMetrology

        public string[] Units { get; } = {"kV", "V", "mV", "μV" };

        #endregion
    }
}