using System;

namespace InColUn
{
    public class ConsoleLogger : ILogger
    {
        public void Log(LogLevel logLevel, string message, Exception exception = null, object parameters = null)
        {
            Console.WriteLine($"{logLevel}: {message}");
        }
    }
}
