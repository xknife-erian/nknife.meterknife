namespace NKnife.Metrology
{
    /// <summary>
    ///     电容
    /// </summary>
    public class ElectricCapacitance : BaseMetrology
    {
        #region Implementation of IMetrology

        public override string[] Units { get; } = {"F", "mF", "μF", "nF", "pF"};
        public override short UnitIndex { get; } = 2;
        public override string Name { get; set; } = "电容";

        #endregion
    }
}