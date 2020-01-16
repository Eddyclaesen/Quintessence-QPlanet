using Microsoft.Practices.EnterpriseLibrary.Logging.Formatters;
using Microsoft.Practices.EnterpriseLibrary.Logging.TraceListeners;

namespace Quintessence.CulturalFit.Infra.Logging
{
    public class TraceRollingFlatFileTraceListener : RollingFlatFileTraceListener
    {
        public TraceRollingFlatFileTraceListener(string fileName, string header, string footer, ILogFormatter formatter, int rollSizeKB, string timeStampPattern, RollFileExistsBehavior rollFileExistsBehavior, RollInterval rollInterval)
            : base(fileName, header, footer, formatter, rollSizeKB, timeStampPattern, rollFileExistsBehavior, rollInterval)
        {
        }

        public TraceRollingFlatFileTraceListener(string fileName, string header, string footer, ILogFormatter formatter, int rollSizeKB, string timeStampPattern, RollFileExistsBehavior rollFileExistsBehavior, RollInterval rollInterval, int maxArchivedFiles)
            : base(fileName, header, footer, formatter, rollSizeKB, timeStampPattern, rollFileExistsBehavior, rollInterval, maxArchivedFiles)
        {
        }
    }
}
