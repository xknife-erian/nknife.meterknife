namespace NKnife.Metrology
{
    /// <summary>
    ///     电感
    /// </summary>
    public class ElectricInductance : BaseMetrology
    {
        #region Implementation of IMetrology

        public override string[] Units { get; } = {"H", "mH", "μH", "nH"};
        public override short UnitIndex { get; } = 1;
        public override string Name { get; set; } = "电感";

        #endregion
    }
}