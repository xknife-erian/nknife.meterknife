using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;

namespace NKnife.Utility
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
