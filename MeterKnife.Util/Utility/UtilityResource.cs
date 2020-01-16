using System.Resources;

namespace MeterKnife.Util.Utility
{
    public class UtilityResource
    {
        public static string GetString(ResourceManager resourceManager, string keyString)
        {
            string tempStr = resourceManager.GetString(keyString, null);
            string result = tempStr ?? keyString;
            return result;
        }

    }
}
