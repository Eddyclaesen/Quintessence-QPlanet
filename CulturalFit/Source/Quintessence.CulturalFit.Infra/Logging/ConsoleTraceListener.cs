using System;
using System.Diagnostics;
using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging;
using Microsoft.Practices.EnterpriseLibrary.Logging.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Logging.TraceListeners;

namespace Quintessence.CulturalFit.Infra.Logging
{
    [ConfigurationElementType(typeof(CustomTraceListenerData))]
    public class ConsoleTraceListener : CustomTraceListener
    {
        public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data)
        {
            if (data is LogEntry && Formatter != null)
            {
                LogEntry le = (LogEntry)data;
                WriteLine(Formatter.Format(le), le.Severity);
            }
            else
            {
                WriteLine(data.ToString());
            }
        }

        public override void Write(string message)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.Write(message);
        }

        public override void WriteLine(string message)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(message);
        }

        public void WriteLine(string message, TraceEventType severity)
        {
            ConsoleColor color;
            switch (severity)
            {
                case TraceEventType.Critical:
                case TraceEventType.Error:
                    color = ConsoleColor.Red;
                    break;
                case TraceEventType.Warning:
                    color = ConsoleColor.Yellow;
                    break;
                case TraceEventType.Information:
                    color = ConsoleColor.Cyan;
                    break;
                case TraceEventType.Verbose:
                default:
                    color = ConsoleColor.Gray;
                    break;
            }

            Console.ForegroundColor = color;
            Console.WriteLine(message);
        }
    }
}
