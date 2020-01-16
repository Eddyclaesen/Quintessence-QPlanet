using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class UpdateProjectCandidateDetailType3ProposalRequest
    {
        [DataMember]
        public Guid ProjectCandidateDetailType3Id { get; set; }

        [DataMember]
        public Guid ProposalId { get; set; }
    }
}