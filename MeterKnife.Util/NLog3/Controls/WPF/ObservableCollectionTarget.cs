using System;
using System.Diagnostics;
using System.Windows;
using MeterKnife.Util.IoC;
using NLog;
using NLog.Targets;

namespace MeterKnife.Util.NLog3.Controls.WPF
{
    /// <summary>
    ///     这是一个基于NLog的自定义的输出目标（Target），这个输出目标是一个WPF控件可绑定的ObservableCollection
    /// </summary>
    [Target("Log_Collection")]
    public class ObservableCollectionTarget : TargetWithLayout
    {
        private const int LOGGER_VIEW_COUNT = 500;

        private readonly LogMessageObservableCollection _LogList = LogMessageObservableCollection.Instance;

        protected override void Write(LogEventInfo logEvent)
        {
            try
            {
                if (Application.Current == null || Application.Current.Dispatcher == null)
                    return;
                if (Application.Current.Dispatcher.CheckAccess())
                {
                    AddLogMessage(logEvent);
                }
                else
                {
                    var logDelegate = new LogMessageWriter(AddLogMessage);
                    Application.Current.Dispatcher.BeginInvoke(logDelegate, new object[] {logEvent});
                }
            }
            catch (Exception e)
            {
                string error = string.Format("向控件写日志发生异常.{0}{1}", e.Message, e.StackTrace);
                Debug.Fail(error);
            }
        }

        protected void AddLogMessage(LogEventInfo logEvent)
        {
            TrimLogMessageCollection();
            if (DI.Get<LogMessageFilter>().Contains(logEvent.Level))
                _LogList.Insert(0, LogMessage.Build(logEvent));
        }

        private void TrimLogMessageCollection()
        {
            if (_LogList.Count >= LOGGER_VIEW_COUNT)
            {
                while (_LogList.Count >= LOGGER_VIEW_COUNT)
                    _LogList.RemoveAt(_LogList.Count - 1);
            }
        }

        private delegate void LogMessageWriter(LogEventInfo logEvent);
    }
}