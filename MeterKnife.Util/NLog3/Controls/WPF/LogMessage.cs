using NLog;

namespace MeterKnife.Util.NLog3.Controls.WPF
{
    public struct LogMessage
    {
        public static LogMessage Build(LogEventInfo logEvent)
        {
            var lm = new LogMessage
            {
                Time = logEvent.TimeStamp.ToString("HH:mm:ss fff"),
                Source = logEvent.LoggerName.LastIndexOf('.') > 0
                    ? logEvent.LoggerName.Substring(logEvent.LoggerName.LastIndexOf('.') + 1)
                    : logEvent.LoggerName,
                Message = logEvent.FormattedMessage,
                Level = logEvent.Level.ToString()
            };
            if (logEvent.HasStackTrace)
                lm.Message += logEvent.StackTrace.ToString();
            return lm;
        }
        public string Time { get; private set; }
        public string Message { get; private set; }
        public string Source { get; private set; }
        public string Level { get; private set; }
    }
}