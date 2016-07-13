using System;

namespace LoggerFacade
{
    public interface ILogger
    {
        void Log(LogEntry entry);
    }

    public enum LoggingEventType { Debug, Information, Warning, Error, Fatal };

    public class LogEntry
    {
        public readonly LoggingEventType EventType;
        public readonly string Message;
        public readonly Exception Exception;

        public LogEntry(LoggingEventType eventType, string message, Exception exception = null)
        {
            this.EventType = eventType;
            this.Message = message;
            this.Exception = exception;
        }
    }
}
