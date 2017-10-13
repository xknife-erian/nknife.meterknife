using NKnife.Interface;

namespace MeterKnife.Keysights
{
    public class KeysightDataConverter : IParser<string, double>
    {
        #region Implementation of IParser<in string,double>

        public bool TryParse(string source, out double result)
        {
            result = 0;
            return true;
        }

        #endregion
    }
}