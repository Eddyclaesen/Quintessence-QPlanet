using System;
using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Base;

namespace Quintessence.QService.QueryModel.Scm
{
    [DataContract(IsReference = true)]
    public class ActivityDetailTrainingCandidateView : ViewEntityBase
    {
        [DataMember]
        public Guid ActivityDetailTrainingId { get; set; }

        [DataMember]
        public string ActivityTypeName { get; set; }

        [DataMember]
        public string ActivityDetailDescription { get; set; }

        [DataMember]
        public Guid CandidateId { get; set; }

        [DataMember]
        public string CandidateFirstName { get; set; }

        [DataMember]
        public string CandidateLastName { get; set; }

        public string CandidateFullName
        {
            get { return string.Format("{0} {1}", CandidateFirstName, CandidateLastName); }
        }

        [DataMember]
        public string CandidateEmail { get; set; }

        [DataMember]
        public string CandidateGender { get; set; }

        [DataMember]
        public int CandidateLanguageId { get; set; }

        [DataMember]
        public int CrmAppointmentId { get; set; }

        [DataMember]
        public int ContactId { get; set; }

        [DataMember]
        public string ContactName { get; set; }

        [DataMember]
        public string ContactDepartment { get; set; }

        public string ContactFullName
        {
            get
            {
                return string.IsNullOrWhiteSpace(ContactDepartment)
                    ? ContactName
                    : string.Format("{0} ({1})", ContactName, ContactDepartment);
            }
        }

        [DataMember]
        public decimal? InvoiceAmount { get; set; }

        [DataMember]
        public int InvoiceStatusCode { get; set; }

        [DataMember]
        public DateTime? InvoicedDate { get; set; }

        [DataMember]
        public bool IsCancelled { get; set; }

        [DataMember]
        public DateTime? CancelledDate { get; set; }

        [DataMember]
        public string CancelledReason { get; set; }
    }
}