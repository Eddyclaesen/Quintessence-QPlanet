using Microsoft.Practices.EnterpriseLibrary.Logging.Configuration;

namespace Quintessence.QService.Core.Logging
{
    public class TraceRollingFlatFileTraceListenerData : RollingFlatFileTraceListenerData
    {
        public TraceRollingFlatFileTraceListenerData()
        {
            Header = string.Empty;
            Footer = string.Empty;
        }
    }
}
