using System;
using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Base;

namespace Quintessence.QService.QueryModel.Prm
{
    [DataContract(IsReference = true)]
    [KnownType(typeof(ProjectCandidateCategoryDetailType1View))]
    [KnownType(typeof(ProjectCandidateCategoryDetailType2View))]
    [KnownType(typeof(ProjectCandidateCategoryDetailType3View))]
    public class ProjectCandidateCategoryDetailTypeView : ViewEntityBase, IProjectCandidateCategoryDetailTypeView
    {
        [DataMember]
        public Guid ProjectCandidateId { get; set; }

        [DataMember]
        public ProjectCandidateView ProjectCandidate { get; set; }

        [DataMember]
        public Guid ProjectCategoryDetailTypeId { get; set; }

        [DataMember]
        public string ProjectCategoryDetailTypeName { get; set; }

        [DataMember]
        public int SurveyPlanningId { get; set; }

        [DataMember]
        public int InvoiceStatusCode { get; set; }

        [DataMember]
        public decimal InvoiceAmount { get; set; }

        [DataMember]
        public DateTime? InvoicedDate { get; set; }

        [DataMember]
        public string InvoiceRemarks { get; set; }

        [DataMember]
        public string PurchaseOrderNumber { get; set; }

        [DataMember]
        public string InvoiceNumber { get; set; }

        [DataMember]
        public DateTime? InvitationSentDate { get; set; }

        [DataMember]
        public DateTime? DossierReadyDate { get; set; }

        [DataMember]
        public bool InvitationSentDateDone { get; set; }

        [DataMember]
        public bool DossierReadyDateDone { get; set; }

        [DataMember]
        public bool FollowUpDone { get; set; }

        [DataMember]
        public string Extra1 { get; set; }

        [DataMember]
        public string Extra2 { get; set; }

        [DataMember]
        public bool Extra1Done { get; set; }

        [DataMember]
        public bool Extra2Done { get; set; }

        [DataMember]
        public string BceEntity { get; set; }
    }
}