using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class ListProjectCandidateDetailsRequest
    {
        [DataMember]
        public DateTime? Date { get; set; }

        [DataMember]
        public Guid? CandidateId { get; set; }

        [DataMember]
        public Guid? ProjectId { get; set; }
    }
}