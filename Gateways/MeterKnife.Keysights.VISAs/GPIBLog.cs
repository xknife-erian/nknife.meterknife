using System;

namespace MeterKnife.Keysights.VISAs
{
    public struct GpibLog
    {
        public GpibLog(GpibLogLevel logLevel, string message, Exception exception = null)
        {
            LogLevel = logLevel;
            Message = message;
            Exception = exception;
        }

        public GpibLogLevel LogLevel { get; set; }
        public string Message { get; set; }
        public Exception Exception { get; set; }
    }
}