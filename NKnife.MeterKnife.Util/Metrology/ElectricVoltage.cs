namespace NKnife.Metrology
{
    /// <summary>
    ///     电压
    /// </summary>
    public class ElectricVoltage : BaseMetrology
    {
        #region Implementation of IMetrology

        public override string[] Units { get; } = {"kV", "V", "mV", "μV" };
        public override short UnitIndex { get; } = 1;
        public override string Name { get; set; } = "电压";

        #endregion
    }
}