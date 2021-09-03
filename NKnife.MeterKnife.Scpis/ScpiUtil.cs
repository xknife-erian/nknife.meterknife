using System.IO;
using System.Windows.Forms;
using NKnife.Util;

namespace NKnife.MeterKnife.Scpis
{
    class ScpiUtil
    {

        private static readonly string _scpisPath = Path.Combine(Application.StartupPath, "Scpis\\Specified\\");

        public static string ScpisPath
        {
            get
            {
                if (!Directory.Exists(_scpisPath))
                    UtilFile.CreateDirectory(_scpisPath);
                return _scpisPath;
            }
        }
    }
}
