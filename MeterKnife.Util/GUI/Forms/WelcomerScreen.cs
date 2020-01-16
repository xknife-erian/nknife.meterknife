using System;
using System.Reflection;
using System.Threading;
using System.Windows.Forms;

namespace MeterKnife.Util.GUI.Forms
{
    public class WelcomerScreen
    {
        private static Form _WelcomerForm;
        private static IWelcomer _WelcomerInterface;
        private static Thread _WelcomerThread;
        private static string _TempStatus = string.Empty;

        /// <summary>
        /// set the loading Status
        /// </summary>
        public static string Status
        {
            set
            {
                if (_WelcomerInterface == null || _WelcomerForm == null || !_WelcomerForm.IsHandleCreated)
                {
                    _TempStatus = value;
                    return;
                }
                var handle = new WelcomerStatusChangedHandle(str => _WelcomerInterface.SetStatusInfo(str));
                _WelcomerForm.Invoke(handle, new object[] {value});
            }
        }

        /// <summary>
        /// Show the WelcomerForm
        /// </summary>
        public static void Show(Type splashFormType)
        {
            if (_WelcomerThread != null)
                return;
            if (splashFormType == null)
            {
                throw (new Exception("splashFormType is null"));
            }
            _WelcomerThread = new Thread(
                delegate()
                    {
                        CreateInstance(splashFormType);
                        _WelcomerForm.Shown += WelcomerFormShown;
                        _WelcomerForm.ShowDialog();
                    }) {IsBackground = true};

            _WelcomerThread.SetApartmentState(ApartmentState.STA);
            _WelcomerThread.Start();
        }

        private static void WelcomerFormShown(object sender, EventArgs e)
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
                ShownEvent(null, EventArgs.Empty);
        }

        /// <summary>
        /// Colse the WelcomerForm
        /// </summary>
        public static void Close()
        {
            if (_WelcomerThread == null || _WelcomerForm == null)
                return;

            try
            {
                _WelcomerForm.Invoke(new MethodInvoker(_WelcomerForm.Close));
            }
            finally
            {
                _WelcomerThread = null;
                _WelcomerForm = null;
            }
        }

        private static void CreateInstance(Type formType)
        {
            object obj = formType.InvokeMember(null,
                 BindingFlags.DeclaredOnly |
                 BindingFlags.Public | BindingFlags.NonPublic |
                 BindingFlags.Instance | BindingFlags.CreateInstance, null, null, null);
            _WelcomerForm = obj as Form;
            _WelcomerInterface = obj as IWelcomer;
            if (_WelcomerForm == null)
                throw (new Exception("Welcomer Screen must inherit from System.Windows.Forms.Form"));
            if (_WelcomerInterface == null)
                throw (new Exception("must implement interface IWelcomerForm"));

            if (!string.IsNullOrEmpty(_TempStatus))
                _WelcomerInterface.SetStatusInfo(_TempStatus);
        }

        #region Nested type: WelcomerStatusChangedHandle

        private delegate void WelcomerStatusChangedHandle(string newStatusInfo);

        #endregion
    }
}