using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class ListReportDefinitionsRequest
    {
        [DataMember]
        public int? ReportTypeId { get; set; }

        [DataMember]
        public string Code { get; set; }
    }
}