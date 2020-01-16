using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using NKnife.Util;

namespace MeterKnife.Scpis
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
