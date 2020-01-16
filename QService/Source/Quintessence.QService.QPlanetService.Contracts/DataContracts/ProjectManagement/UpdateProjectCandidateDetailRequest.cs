using System;
using System.Runtime.Serialization;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.Shared;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class UpdateProjectCandidateDetailRequest : UpdateRequestBase
    {
        [DataMember]
        public Guid CandidateId { get; set; }

        [DataMember]
        public string CandidateEmail { get; set; }

        [DataMember]
        public string CandidatePhone { get; set; }

        [DataMember]
        public string CandidateGender { get; set; }

        [DataMember]
        public int CandidateLanguageId { get; set; }

        [DataMember]
        public DateTime ReportDeadline { get; set; }

        [DataMember]
        public int ReportLanguageId { get; set; }

        [DataMember]
        public Guid? ReportReviewerId { get; set; }

        [DataMember]
        public int ReportStatusId { get; set; }

        [DataMember]
        public decimal InvoiceAmount { get; set; }

        [DataMember]
        public string Remarks { get; set; }

        [DataMember]
        public bool IsAccompaniedByCustomer { get; set; }

        [DataMember]
        public bool InternalCandidate { get; set; }

        [DataMember]
        public bool OnlineAssessment { get; set; }
    }
}