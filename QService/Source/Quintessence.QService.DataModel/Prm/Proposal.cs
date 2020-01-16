using System;
using Quintessence.Infrastructure.Model.DataModel;

namespace Quintessence.QService.DataModel.Prm
{
    public class Proposal : EntityBase
    {
        public string Name { get; set; }
        public int ContactId { get; set; }
        public string Description { get; set; }
        public DateTime? DateReceived { get; set; }
        public DateTime? Deadline { get; set; }
        public DateTime? DateSent { get; set; }
        public DateTime? DateWon { get; set; }
        public Guid? BusinessDeveloperId { get; set; }
        public Guid? ExecutorId { get; set; }
        public decimal? PriceEstimation { get; set; }
        public decimal? Prognosis { get; set; }
        public decimal? FinalBudget { get; set; }
        public int StatusCode { get; set; }
        public string StatusReason { get; set; }
        public bool WrittenProposal { get; set; }
    }
}