using System;
using Quintessence.Infrastructure.Model.DataModel;

namespace Quintessence.QService.DataModel.Scm
{
    public class ActivityDetailTrainingCandidate : EntityBase
    {
        public Guid ActivityDetailTrainingId { get; set; }
        public Guid CandidateId { get; set; }
        public int ContactId { get; set; }
        public int CrmAppointmentId { get; set; }
        public decimal? InvoiceAmount { get; set; }
        public int InvoiceStatusCode { get; set; }
        public DateTime? InvoicedDate { get; set; }
        public bool IsCancelled { get; set; }
        public DateTime? CancelledDate { get; set; }
        public string CancelledReason { get; set; }
    }
}
