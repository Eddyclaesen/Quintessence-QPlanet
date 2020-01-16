using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class UpdateProjectCandidateDetailType1ProposalRequest
    {
        [DataMember]
        public Guid ProjectCandidateDetailType1Id { get; set; }

        [DataMember]
        public Guid ProposalId { get; set; }
    }
}