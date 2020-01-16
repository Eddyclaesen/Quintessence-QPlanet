using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class ListProjectCandidateReportingOverviewEntriesRequest
    {
        [DataMember]
        public DateTime? StartDate { get; set; }

        [DataMember]
        public Guid? CustomerAssistantId { get; set; }

    }
}