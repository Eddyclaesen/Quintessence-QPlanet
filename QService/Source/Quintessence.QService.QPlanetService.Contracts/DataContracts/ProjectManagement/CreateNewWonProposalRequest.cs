using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class CreateNewWonProposalRequest
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int? ContactId { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public DateTime? DateReceived { get; set; }

        [DataMember]
        public DateTime? DateWon { get; set; }

        [DataMember]
        public decimal? FinalBudget { get; set; }

        [DataMember]
        public bool WrittenProposal { get; set; }
    }
}