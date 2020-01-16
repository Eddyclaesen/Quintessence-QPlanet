using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Quintessence.QService.Core.Performance;
using Quintessence.QService.QueryModel.Cam;

namespace Quintessence.QService.QueryModel.Fin
{
    [KnownType(typeof(InvoicingProductSheetEntryView))]
    [KnownType(typeof(InvoicingTimesheetEntryView))]
    [KnownType(typeof(InvoicingConsultancyProjectFixedPriceEntryView))]
    [KnownType(typeof(InvoicingAssessmentDevelopmentProjectFixedPriceEntryView))]
    [KnownType(typeof(InvoicingProjectProductEntryView))]
    [KnownType(typeof(InvoicingProjectCandidateCategoryType1EntryView))]
    [KnownType(typeof(InvoicingProjectCandidateCategoryType2EntryView))]
    [KnownType(typeof(InvoicingProjectCandidateCategoryType3EntryView))]
    [KnownType(typeof(InvoicingProjectCandidateEntryView))]
    [KnownType(typeof(AccountantInvoicingBaseEntryView))]
    [DataContract(IsReference = true)]
    public class InvoicingBaseEntryView
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public Guid ProjectId { get; set; }

        [DataMember]
        public string ProjectName { get; set; }

        [DataMember]
        public int ContactId { get; set; }

        [DataMember]
        public string ContactName { get; set; }

        [DataMember]
        public decimal? InvoiceAmount { get; set; }

        [DataMember]
        public int InvoiceStatusCode { get; set; }

        [DataMember]
        public DateTime? InvoicedDate { get; set; }

        [DataMember]
        public string InvoiceRemarks { get; set; }

        [DataMember]
        public string PurchaseOrderNumber { get; set; }

        [DataMember]
        public string InvoiceNumber { get; set; }

        [DataMember]
        public string ProductName { get; set; }

        [DataMember]
        public string CustomerAssistantFirstName { get; set; }

        [DataMember]
        public string CustomerAssistantLastName { get; set; }

        public string CustomerAssistantFullName { get { return CustomerAssistantFirstName + ' ' + CustomerAssistantLastName; } }

        [DataMember]
        public string CustomerAssistantUserName { get; set; }

        public string CustomerAssistantInitials { get { return CustomerAssistantFullName.ToInitials(); } }

        [DataMember]
        public string ProjectManagerFirstName { get; set; }

        [DataMember]
        public string ProjectManagerLastName { get; set; }

        public string ProjectManagerFullName { get { return ProjectManagerFirstName + ' ' + ProjectManagerLastName; } }

        [DataMember]
        public string ProjectManagerUserName { get; set; }

        [DataMember]
        public string ConsultantFirstName { get; set; }

        [DataMember]
        public string ConsultantLastName { get; set; }

        public string ConsultantFullName { get { return ConsultantFirstName + ' ' + ConsultantLastName; } }

        [DataMember]
        public string ConsultantUserName { get; set; }

        [DataMember]
        public Guid? ProposalId { get; set; }

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