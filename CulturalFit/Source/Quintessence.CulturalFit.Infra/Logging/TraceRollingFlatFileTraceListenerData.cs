using Microsoft.Practices.EnterpriseLibrary.Logging.Configuration;

namespace Quintessence.CulturalFit.Infra.Logging
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
