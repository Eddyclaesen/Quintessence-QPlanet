using System;
using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Prm;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class CreateNewProposalRequest
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
        public DateTime? Deadline { get; set; }

        [DataMember]
        public ProposalStatusType StatusCode { get; set; }

        [DataMember]
        public DateTime? DateWon { get; set; }

        [DataMember]
        public decimal FinalBudget { get; set; }

        [DataMember]
        public bool WrittenProposal { get; set; }
    }
}