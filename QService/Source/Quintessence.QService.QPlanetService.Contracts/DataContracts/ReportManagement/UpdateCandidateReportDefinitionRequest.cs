using System.Runtime.Serialization;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.Shared;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ReportManagement
{
    [DataContract]
    public class UpdateCandidateReportDefinitionRequest : UpdateRequestBase
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Location { get; set; }

        [DataMember]
        public bool IsActive { get; set; }
    }
}