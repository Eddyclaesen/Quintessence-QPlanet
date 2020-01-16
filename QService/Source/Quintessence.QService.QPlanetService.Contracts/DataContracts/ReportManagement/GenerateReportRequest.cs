using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ReportManagement
{
    [DataContract]
    public class GenerateReportRequest
    {
        [DataMember]
        public string ReportName { get; set; }

        [DataMember]
        public Dictionary<string,string> Parameters { get; set; }

        [DataMember]
        public ReportOutputFormat OutputFormat { get; set; }
    }
}