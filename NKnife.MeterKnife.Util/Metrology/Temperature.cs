namespace NKnife.Metrology
{
    /// <summary>
    ///     Temperature: Kelvin (K) The Kelvin is the unit of thermodynamic temperature. It is the fraction 1/273.16 of the
    ///     thermodynamic temperature of the triple point of water. The Kelvin scale is an absolute scale, so there is no
    ///     degree.​
    ///     开尔文。以水的三相点温度为273.16K。开尔文一度等于水三相点热力学温度的 1/273.16。热力学温度 T 和摄氏温度 t 的关系为 T=t+273.15。
    /// </summary>
    public class Temperature : IMetrology
    {
        #region Implementation of IMetrology

        public string[] Units { get; } = { "℃", "℉" };

        #endregion
    }
}