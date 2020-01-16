using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ReportManagement
{
    [DataContract]
    public class GenerateReportResponse
    {
        [DataMember]
        public string Output { get; set; }

        [DataMember]
        public string ContentType { get; set; }
    }
}