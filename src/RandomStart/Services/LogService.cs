using Serilog;
using Serilog.Configuration;
using Serilog.Core;
using Serilog.Events;
using System;
using System.Globalization;

namespace RandomStart.Services
{
    public class LogService : ILogEventSink
    {
        public LogService()
        {
        }

        public void Emit(LogEvent logEvent)
        {
            var message = logEvent.RenderMessage(CultureInfo.CurrentCulture);
            Emitted?.Invoke(this, new LogEventArgs(message));
        }

        public event EventHandler<LogEventArgs> Emitted;
    }

    public static class LogServiceExtensions
    {
        public static LoggerConfiguration LogService(
            this LoggerSinkConfiguration loggerConfiguration)
        {
            return loggerConfiguration.Sink(new LogService());
        }
    }
    public class LogEventArgs : EventArgs
    {
        public string Message { get; set; }
        public LogEventArgs(string message)
        {
            Message = message;
        }
    }
}
