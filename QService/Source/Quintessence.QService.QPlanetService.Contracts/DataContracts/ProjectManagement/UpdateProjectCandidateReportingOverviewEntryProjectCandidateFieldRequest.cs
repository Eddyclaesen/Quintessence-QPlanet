using System.Runtime.Serialization;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.Shared;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class UpdateProjectCandidateReportingOverviewEntryProjectCandidateFieldRequest : UpdateRequestBase
    {
        [DataMember]
        public string PropertyName { get; set; }

        [DataMember]
        public string PropertyValue { get; set; }
    }
}