using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace InColUn
{
    public static class LoggerExtensionscs
    {
        public static void LogError(this ILogger logger, string message)
        {
            if (logger == null) return;
            logger.Log(LogLevel.Error, message);
        }

        public static void LogException(this ILogger logger, string message, Exception exception)
        {
            if (logger == null) return;
            logger.Log(LogLevel.Exception, message, exception);
        }
    }
}
