using Microsoft.Practices.EnterpriseLibrary.Logging.Configuration;

namespace Quintessence.QPlanet.Infrastructure.Logging
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
