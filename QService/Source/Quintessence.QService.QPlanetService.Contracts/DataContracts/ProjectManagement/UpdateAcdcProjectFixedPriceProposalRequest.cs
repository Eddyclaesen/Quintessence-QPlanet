using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class UpdateAcdcProjectFixedPriceProposalRequest
    {
        [DataMember]
        public Guid AcdcProjectFixedPriceId { get; set; }

        [DataMember]
        public Guid ProposalId { get; set; }
    }
}