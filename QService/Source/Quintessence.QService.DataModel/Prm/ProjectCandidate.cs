using System;
using Quintessence.QService.Core.Validation;
using Quintessence.Infrastructure.Model.DataModel;

namespace Quintessence.QService.DataModel.Prm
{
    public class ProjectCandidate : EntityBase, IInvoiceInfo
    {
        public Guid CandidateId { get; set; }
        public int CrmCandidateAppointmentId { get; set; }
        public Guid ProjectId { get; set; }

        [DateRange(ErrorMessage = "Report deadline must be between {1} and {2}.")]
        public DateTime ReportDeadline { get; set; }
        public int ReportLanguageId { get; set; }
        public Guid? ReportReviewerId { get; set; }
        public int ReportStatusId { get; set; }
        public bool IsCancelled { get; set; }
        public DateTime? CancelledDate { get; set; }
        public DateTime? CancelledAppointmentDate { get; set; }
        public string CancelledReason { get; set; }
        public decimal? InvoiceAmount { get; set; }
        public int InvoiceStatusCode { get; set; }
        public DateTime? InvoicedDate { get; set; }
        public string InvoiceRemarks { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public string InvoiceNumber { get; set; }
        public string Remarks { get; set; }
        public bool IsAccompaniedByCustomer { get; set; }
        public bool InternalCandidate { get; set; }
        public bool OnlineAssessment { get; set; }
        public Guid? ScoringCoAssessorId { get; set; }
        public bool FollowUpDone { get; set; }
        public DateTime? OrderConfirmationSentDate { get; set; }
        public DateTime? OrderConfirmationReceivedDate { get; set; }
        public DateTime? InvitationSentDate { get; set; }
        public DateTime? ReportMailSentDate { get; set; }
        public DateTime? LeafletSentDate { get; set; }
        public DateTime? DossierReadyDate { get; set; }

        public bool ReportDeadlineDone { get; set; }
        public bool OrderConfirmationSentDateDone { get; set; }
        public bool OrderConfirmationReceivedDateDone { get; set; }
        public bool InvitationSentDateDone { get; set; }
        public bool LeafletSentDateDone { get; set; }
        public bool ReportMailSentDateDone { get; set; }
        public bool DossierReadyDateDone { get; set; }

        public string Extra1 { get; set; }
        public string Extra2 { get; set; }
        public bool Extra1Done { get; set; }
        public bool Extra2Done { get; set; }

        public Guid? ProposalId { get; set; }

        #region Legacy properties
        public int CrmCandidateInfoId { get; set; }
        #endregion
    }
}
