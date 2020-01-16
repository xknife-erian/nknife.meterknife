using System;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace MeterKnife.Util.Utility
{
	/// <summary>
	/// System.Windows.Forms.Clipboard����չ
	/// </summary>
    public class UtilityClipboard
    {
        public static bool ContainsText
        {
            get
            {
                try { return Clipboard.ContainsText(); }
                catch (ExternalException) { return false; }
            }
        }

        public static string GetText()
        {
            try //retry 2 times should be enough for read access
            {
                return Clipboard.GetText();
            }
            catch (ExternalException)
            {
                return Clipboard.GetText();
            }
        }

        public static void SetText(string text)
        {
            DataObject data = new DataObject();
            data.SetData(DataFormats.UnicodeText, true, text);
            SetDataObject(data);
        }

        public static IDataObject GetDataObject()
        {
            try //retry 2 times should be enough for read access
            {
                return Clipboard.GetDataObject();
            }
            catch (ExternalException)
            {
                try
                {
                    return Clipboard.GetDataObject();
                }
                catch (ExternalException)
                {
                    return new DataObject();
                }
            }
        }

        public static void SetDataObject(object data)
        {
            SafeSetClipboard(data);
        }

        // Code duplication: TextAreaClipboardHandler.cs also has SafeSetClipboard
        [ThreadStatic]
        static int _safeSetClipboardDataVersion;

        static void SafeSetClipboard(object dataObject)
        {
            // Work around ExternalException bug. (SD2-426)
            // Best reproducable inside Virtual PC.
            int version = unchecked(++_safeSetClipboardDataVersion);
            try
            {
                Clipboard.SetDataObject(dataObject, true);
            }
            catch (ExternalException)
            {
                var timer = new Timer {Interval = 100};
                timer.Tick += delegate
                {
                    timer.Stop();
                    timer.Dispose();
                    if (_safeSetClipboardDataVersion == version)
                    {
                        try
                        {
                            Clipboard.SetDataObject(dataObject, true, 10, 50);
                        }
                        catch (ExternalException) { }
                    }
                };
                timer.Start();
            }
        }
    }
}
