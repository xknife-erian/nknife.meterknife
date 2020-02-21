namespace NKnife.Metrology
{
    /// <summary>
    ///     频率
    /// </summary>
    public class Frequency : IMetrology
    {
        #region Implementation of IMetrology

        public string[] Units { get; } = { "GHz", "MHz", "kHz", "Hz" };

        #endregion
    }
}