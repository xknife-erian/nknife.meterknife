using System;
using System.Collections.Generic;
using System.Text;

namespace NKnife.MeterKnife.Util
{
    public static class CommandUtil
    {
        public static string ToDUTKey(this byte[] command)
        {
            if (command == null)
                throw new ArgumentNullException();
            StringBuilder sb = new StringBuilder();
            foreach (byte num in command)
                sb.Append(num.ToString("X2"));
            return sb.ToString();
        }
    }
}
