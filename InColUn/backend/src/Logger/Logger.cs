using System;

namespace InColUn
{
    public enum LogLevel { Debug, Information, Warning, Exception, Error, Fatal, Custom };
    /// <summary>
    /// Common logging interface
    /// </summary>
    public interface ILogger
    {
        void Log(LogLevel logLevel, string message, Exception exception = null, object parameters = null);
    }

    public class Logger
    {
        public static ILogger Instance { get; set; }
    }
}
