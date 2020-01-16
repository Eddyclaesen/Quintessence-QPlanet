using System;
using System.Runtime.Serialization;
using Quintessence.QService.Core.Performance;

namespace Quintessence.QService.QueryModel.Prm
{
    [DataContract(IsReference = true)]
    public class ProjectCandidateReportingOverviewEntryView
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public Guid ProjectId { get; set; }

        [DataMember]
        public string ProjectName { get; set; }
        
        [DataMember]
        public string CandidateFirstName { get; set; }

        [DataMember]
        public string CandidateLastName { get; set; }

        public string CandidateFullName
        {
            get { return string.Format("{0} {1}", CandidateFirstName, CandidateLastName); }
        }

        [DataMember]
        public string ContactName { get; set; }

        [DataMember]
        public string ReportLanguage { get; set; }
        
        [DataMember]
        public DateTime ReportDeadline { get; set; }

        [DataMember]
        public int ReportStatusId { get; set; }

        [DataMember]
        public string CandidateRemarks { get; set; }

        [DataMember]
        public string ReportStatusName { get; set; }

        [DataMember]
        public string ReportRemarks { get; set; }

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

        [DataMember]
        public Guid? ReportReviewerId { get; set; }
        
        [DataMember]
        public string ProjectManagerFirstName { get; set; }

        [DataMember]
        public string ProjectManagerLastName { get; set; }

        public string ProjectManagerFullName { get { return ProjectManagerFirstName + ' ' + ProjectManagerLastName; } }

        [DataMember]
        public string ProjectManagerUserName { get; set; }

        public string ProjectManagerInitials { get { return ProjectManagerFullName.ToInitials(); } }
        
        [DataMember]
        public bool IsRevisionByPmRequired { get; set; }
        
        [DataMember]
        public bool SendReportToParticipant { get; set; }
        
        [DataMember]
        public string SendReportToParticipantRemarks { get; set; }

        [DataMember]
        public string ReportRecipients { get; set; }

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
    }
}