using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Quintessence.QService.Core.Performance;
using Quintessence.QService.QueryModel.Cam;

namespace Quintessence.QService.QueryModel.Prm
{
    [DataContract(IsReference = true)]
    public class ProjectCandidateOverviewEntryView
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public Guid ProjectId { get; set; }

        [DataMember]
        public string ProjectName { get; set; }

        [DataMember]
        public Guid? ProjectCandidateCategoryDetailTypeId { get; set; }

        [DataMember]
        public string CandidateFirstName { get; set; }

        [DataMember]
        public string CandidateLastName { get; set; }

        public string CandidateFullName
        {
            get { return string.Format("{0} {1}", CandidateFirstName, CandidateLastName); }
        }

        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public string ContactName { get; set; }

        [DataMember]
        public bool IsCancelled { get; set; }

        [DataMember]
        public decimal? InvoiceAmount { get; set; }

        [DataMember]
        public string Remarks { get; set; }

        [DataMember]
        public int StatusCode { get; set; }

        [DataMember]
        public DateTime? OrderConfirmationSentDate { get; set; }

        [DataMember]
        public DateTime? OrderConfirmationReceivedDate { get; set; }

        [DataMember]
        public DateTime? InvitationSentDate { get; set; }

        [DataMember]
        public DateTime ReportDeadline { get; set; }

        [DataMember]
        public int ReportDeadlineStep { get; set; }

        [DataMember]
        public DateTime? ReportMailSentDate { get; set; }

        [DataMember]
        public DateTime? DossierReadyDate { get; set; }

        [DataMember]
        public string Type { get; set; }

        [DataMember]
        public string Language { get; set; }

        [DataMember]
        public int LanguageId { get; set; }

        [DataMember]
        public string Function { get; set; }

        [DataMember]
        public DateTime? Date { get; set; }

        [DataMember]
        public string OfficeName { get; set; }

        [DataMember]
        public string Assessors { get; set; }

        [DataMember]
        public string AssessorUserNames { get; set; }

        [DataMember]
        public string CustomerAssistantFirstName { get; set; }

        [DataMember]
        public string CustomerAssistantLastName { get; set; }

        public string CustomerAssistantFullName { get { return CustomerAssistantFirstName + ' ' + CustomerAssistantLastName; } }

        [DataMember]
        public string CustomerAssistantUserName { get; set; }

        public string CustomerAssistantInitials { get { return CustomerAssistantFullName.ToInitials(); } }

        [DataMember]
        public bool? CulturalFit { get; set; }

        [DataMember]
        public bool? FollowUpDone { get; set; }

        [DataMember]
        public string Extra1 { get; set; }

        [DataMember]
        public string Extra2 { get; set; }

        [DataMember]
        public bool? Extra1Done { get; set; }

        [DataMember]
        public bool? Extra2Done { get; set; }

        [DataMember]
        public string ProjectManagerFirstName { get; set; }

        [DataMember]
        public string ProjectManagerLastName { get; set; }

        public string ProjectManagerFullName { get { return ProjectManagerFirstName + ' ' + ProjectManagerLastName; } }

        [DataMember]
        public string ProjectManagerUserName { get; set; }

        public string ProjectManagerInitials { get { return ProjectManagerFullName.ToInitials(); } }
        
        [DataMember]
        public string ReportRecipients { get; set; }

        [DataMember]
        public string ProjectTypeCategoryColor { get; set; }

        [DataMember]
        public bool? ReportDeadlineDone { get; set; }

        [DataMember]
        public bool? OrderConfirmationSentDateDone { get; set; }

        [DataMember]
        public bool? OrderConfirmationReceivedDateDone { get; set; }

        [DataMember]
        public bool? InvitationSentDateDone { get; set; }

        [DataMember]
        public bool? LeafletSentDateDone { get; set; }

        [DataMember]
        public bool? ReportMailSentDateDone { get; set; }

        [DataMember]
        public bool? DossierReadyDateDone { get; set; }
        
        
        [DataMember]
        public string AuditCreatedBy { get; set; }

        [DataMember]
        public DateTime AuditCreatedOn { get; set; }

        [DataMember]
        public string AuditModifiedBy { get; set; }

        [DataMember]
        public DateTime? AuditModifiedOn { get; set; }

        [DataMember]
        public string AuditDeletedBy { get; set; }

        [DataMember]
        public DateTime? AuditDeletedOn { get; set; }

        [DataMember]
        public bool AuditIsDeleted { get; set; }

        [DataMember]
        public Guid AuditVersionId { get; set; }

        [DataMember]
        public bool IsReserved { get; set; }
    }
}