using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class UpdateProjectProductProposalRequest
    {
        [DataMember]
        public Guid ProjectProductId { get; set; }

        [DataMember]
        public Guid ProposalId { get; set; }
    }
}