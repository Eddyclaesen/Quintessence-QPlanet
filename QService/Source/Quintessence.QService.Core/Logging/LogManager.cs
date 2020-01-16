using System;
using System.Text;
using Microsoft.Practices.EnterpriseLibrary.Logging;

namespace Quintessence.QService.Core.Logging
{
    public static class LogManager
    {
        public static void LogTrace(string message, params object[] args)
        {
            Logger.Write(string.Format(message, args), "Tracing");
        }

        public static void LogError(Exception exception)
        {
            var builder = new StringBuilder();

            var e = exception;

            while (e != null)
            {
                builder.AppendLine(e.Message).AppendLine(e.StackTrace).AppendLine();
                e = e.InnerException;
            }

            Logger.Write(builder.ToString(), "ExceptionHandling");
        }

        public static void LogDuration(string message, params object[] args)
        {
            Logger.Write(string.Format(message, args), "DurationLog");
        }
    }
}
