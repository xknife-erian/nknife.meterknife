using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using iTextSharp.text;

namespace MeterKnife.Reports
{
    class Fonts
    {
        public static void Initialise()
        {
            var fontPath = Environment.GetFolderPath(Environment.SpecialFolder.Fonts);
            var count = FontFactory.RegisterDirectory(fontPath);
            Console.WriteLine($"Font count: {count}");
        }
    }
}
