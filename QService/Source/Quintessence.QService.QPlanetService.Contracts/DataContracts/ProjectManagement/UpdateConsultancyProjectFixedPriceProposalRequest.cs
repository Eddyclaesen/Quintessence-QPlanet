using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class UpdateConsultancyProjectFixedPriceProposalRequest
    {
        [DataMember]
        public Guid ConsultancyProjectFixedPriceId { get; set; }

        [DataMember]
        public Guid ProposalId { get; set; }
    }
}