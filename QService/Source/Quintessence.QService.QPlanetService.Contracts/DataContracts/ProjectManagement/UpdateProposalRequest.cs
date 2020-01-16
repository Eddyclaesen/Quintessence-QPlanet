using System;
using System.Runtime.Serialization;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.Shared;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class UpdateProposalRequest : UpdateRequestBase
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int ContactId { get; set; }

        [DataMember]
        public Guid? BusinessDeveloperId { get; set; }

        [DataMember]
        public Guid? ExecutorId { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public DateTime? DateReceived { get; set; }

        [DataMember]
        public DateTime? DateSent { get; set; }

        [DataMember]
        public DateTime? DateWon { get; set; }

        [DataMember]
        public DateTime? Deadline { get; set; }

        [DataMember]
        public decimal? PriceEstimation { get; set; }

        [DataMember]
        public decimal? Prognosis { get; set; }

        [DataMember]
        public decimal? FinalBudget { get; set; }

        [DataMember]
        public int StatusCode { get; set; }

        [DataMember]
        public string StatusReason { get; set; }

        [DataMember]
        public bool WrittenProposal { get; set; }
    }
}