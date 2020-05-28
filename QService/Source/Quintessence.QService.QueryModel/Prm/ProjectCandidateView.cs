using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Base;
using Quintessence.QService.QueryModel.Cam;

namespace Quintessence.QService.QueryModel.Prm
{
    [DataContract(IsReference = true)]
    public class ProjectCandidateView : ViewEntityBase
    {
        [DataMember]
        public Guid ProjectId { get; set; }

        [DataMember]
        public ProjectView Project { get; set; }

        [DataMember]
        public Guid CandidateId { get; set; }

        [DataMember]
        public CandidateView Candidate { get; set; }

        [DataMember]
        public Guid ProjectCandidateDetailId { get; set; }

        [DataMember]
        public ProjectCandidateDetailView ProjectCandidateDetail { get; set; }

        [DataMember]
        public List<TheoremListRequestView> TheoremListRequests { get; set; }

        [DataMember]
        public int CrmCandidateAppointmentId { get; set; }

        [DataMember]
        public int CrmCandidateInfoId { get; set; }
        
        [DataMember]
        public string CandidateFirstName { get; set; }

        [DataMember]
        public string CandidateLastName { get; set; }

        [DataMember]
        public string CandidateEmail { get; set; }

        [DataMember]
        public string CandidatePhone { get; set; }

        [DataMember]
        public int OfficeId { get; set; }

        [DataMember]
        public string CandidateGender { get; set; }

        [DataMember]
        public int CandidateLanguageId { get; set; }

        [DataMember]
        public List<ProjectCandidateCategoryDetailTypeView> ProjectCandidateCategoryDetailTypes { get; set; }

        [DataMember]
        public List<ProjectCandidateProjectView> ProjectCandidateProjects { get; set; }

        public string CandidateFullName
        {
            get { return string.Format("{0} {1}", CandidateFirstName, CandidateLastName); }
        }

        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public DateTime ReportDeadline { get; set; }

        [DataMember]
        public int ReportLanguageId { get; set; }

        [DataMember]
        public Guid? ReportReviewerId { get; set; }

        [DataMember]
        public int ReportStatusId { get; set; }

        [DataMember]
        public bool IsCancelled { get; set; }

        [DataMember]
        public DateTime? CancelledDate { get; set; }

        [DataMember]
        public DateTime? CancelledAppointmentDate { get; set; }

        [DataMember]
        public string CancelledReason { get; set; }

        [DataMember]
        public decimal? InvoiceAmount { get; set; }

        [DataMember]
        public int InvoiceStatusCode { get; set; }

        [DataMember]
        public string InvoiceRemarks { get; set; }

        [DataMember]
        public string PurchaseOrderNumber { get; set; }

        [DataMember]
        public string InvoiceNumber { get; set; }

        [DataMember]
        public DateTime? InvoicedDate { get; set; }

        [DataMember]
        public string Remarks { get; set; }

        [DataMember]
        public bool IsAccompaniedByCustomer { get; set; }

        [DataMember]
        public bool InternalCandidate { get; set; }

        [DataMember]
        public bool OnlineAssessment { get; set; }

        [DataMember]
        public Guid? ScoringCoAssessorId { get; set; }

        [DataMember]
        public bool FollowUpDone { get; set; }

        [DataMember]
        public DateTime? OrderConfirmationSentDate { get; set; }

        [DataMember]
        public DateTime? OrderConfirmationReceivedDate { get; set; }

        [DataMember]
        public DateTime? InvitationSentDate { get; set; }

        [DataMember]
        public DateTime? ReportMailSentDate { get; set; }

        [DataMember]
        public DateTime? LeafletSentDate { get; set; }

        [DataMember]
        public DateTime? DossierReadyDate { get; set; }

        [DataMember]
        public string Extra1 { get; set; }

        [DataMember]
        public string Extra2 { get; set; }

        [DataMember]
        public bool Extra1Done { get; set; }

        [DataMember]
        public bool Extra2Done { get; set; }

        [DataMember]
        public bool CandidateHasQCandidateAccess { get; set; }

        [DataMember]
        public Guid? CandidateQCandidateUserId { get; set; }
    }
}
