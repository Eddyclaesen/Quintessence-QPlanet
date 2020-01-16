using System;
using Quintessence.Infrastructure.Model.DataModel;

namespace Quintessence.QService.DataModel.Prm
{
    public class ProjectCandidateCategoryDetailType3 : EntityBase, IProjectCandidateCategoryDetailType, IInvoiceInfo
    {
        public Guid ProjectCandidateId { get; set; }
        public Guid ProjectCategoryDetailType3Id { get; set; }
        public DateTime? Deadline { get; set; }
        public string LoginCode { get; set; }
        public int InvoiceStatusCode { get; set; }
        public decimal InvoiceAmount { get; set; }
        public DateTime? InvoicedDate { get; set; }
        public string InvoiceRemarks { get; set; }
        public string PurchaseOrderNumber { get; set; }
        public string InvoiceNumber { get; set; }
        public DateTime? InvitationSentDate { get; set; }
        public DateTime? DossierReadyDate { get; set; }
        public bool InvitationSentDateDone { get; set; }
        public bool DossierReadyDateDone { get; set; }
        public bool FollowUpDone { get; set; }
        public string Extra1 { get; set; }
        public string Extra2 { get; set; }
        public bool Extra1Done { get; set; }
        public bool Extra2Done { get; set; }

        public Guid? ProposalId { get; set; }
    }
}