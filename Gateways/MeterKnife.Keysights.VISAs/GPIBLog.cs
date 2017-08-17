using System;

namespace MeterKnife.Keysights.VISAs
{
    public struct GPIBLog
    {
        public GPIBLog(GPIBLogLevel logLevel, string message, Exception exception = null)
        {
            LogLevel = logLevel;
            Message = message;
            Exception = exception;
        }

        public GPIBLogLevel LogLevel { get; set; }
        public string Message { get; set; }
        public Exception Exception { get; set; }
    }
}