using System;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace MeterKnife.Util.GUI
{
    public class Splasher
    {
        private static Form _SplashForm;
        private static ISplashForm _SplashInterface;
        private static Thread _SplashThread;
        private static string _TempStatus = string.Empty;

        /// <summary>
        /// set the loading Status
        /// </summary>
        public static string Status
        {
            set
            {
                if (_SplashInterface == null || _SplashForm == null || !_SplashForm.IsHandleCreated)
                {
                    _TempStatus = value;
                    return;
                }
                try
                {
                    var handle = new SplashStatusChangedHandle(str => _SplashInterface.SetStatusInfo(str));
                    _SplashForm.BeginInvoke(handle, new object[] { value });
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }
        }

        /// <summary>
        /// Show the SplashForm
        /// </summary>
        public static void Show(Type splashFormType)
        {
            if (_SplashThread != null)
                return;
            if (splashFormType == null)
            {
                throw (new Exception("splashFormType is null"));
            }
            _SplashThread = new Thread(
                delegate()
                {
                    CreateInstance(splashFormType);
                    _SplashForm.Shown += SplashFormShown;
                    _SplashForm.ShowDialog();
                }) {IsBackground = true};

            _SplashThread.SetApartmentState(ApartmentState.STA);
            _SplashThread.Start();
        }

        private static void SplashFormShown(object sender, EventArgs e)
        {
            OnShown();
        }


        /// <summary>
        /// 发生的事件
        /// </summary>
        public static event EventHandler ShownEvent;

        protected static void OnShown()
        {
            if (ShownEvent != null)
                ShownEvent(_SplashForm, EventArgs.Empty);
        }

        /// <summary>
        /// Colse the SplashForm
        /// </summary>
        public static void Close()
        {
            if (_SplashThread == null || _SplashForm == null)
                return;

            try
            {
                _SplashForm.BeginInvoke(new MethodInvoker(_SplashForm.Close));
            }
            finally
            {
                _SplashThread = null;
                _SplashForm = null;
            }
        }

        private static void CreateInstance(Type formType)
        {
            object obj = formType.InvokeMember
                (null,
                 BindingFlags.DeclaredOnly |
                 BindingFlags.Public | BindingFlags.NonPublic |
                 BindingFlags.Instance | BindingFlags.CreateInstance, null, null, null);
            _SplashForm = obj as Form;
            _SplashInterface = obj as ISplashForm;
            if (_SplashForm == null)
                throw (new Exception("Splash Screen must inherit from System.Windows.Forms.Form"));
            if (_SplashInterface == null)
                throw (new Exception("must implement interface ISplashForm"));

            if (!string.IsNullOrEmpty(_TempStatus))
                _SplashInterface.SetStatusInfo(_TempStatus);
        }

        #region Nested type: SplashStatusChangedHandle

        private delegate void SplashStatusChangedHandle(string newStatusInfo);

        #endregion
    }
}