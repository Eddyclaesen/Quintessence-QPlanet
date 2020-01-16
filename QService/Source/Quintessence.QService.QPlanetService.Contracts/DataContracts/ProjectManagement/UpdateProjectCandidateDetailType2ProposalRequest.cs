using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class UpdateProjectCandidateDetailType2ProposalRequest
    {
        [DataMember]
        public Guid ProjectCandidateDetailType2Id { get; set; }

        [DataMember]
        public Guid ProposalId { get; set; }
    }
}