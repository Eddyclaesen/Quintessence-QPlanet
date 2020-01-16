using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Base;

namespace Quintessence.QService.QueryModel.Prm
{
    [DataContract(IsReference = true)]
    public class TheoremListRequestView : ViewEntityBase
    {
        [DataMember]
        public int ContactId { get; set; }

        [DataMember]
        public int? CrmEmailId { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string Email { get; set; }

        public string FullName
        {
            get { return string.Format("{0} {1}", FirstName, LastName); }
        }

        //[DataMember]
        //public bool IsCompleted { get; set; }

        [DataMember]
        public DateTime RequestDate { get; set; }

        [DataMember]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? Deadline { get; set; }

        [DataMember]
        public string VerificationCode { get; set; }

        [DataMember]
        public bool IsMailSent { get; set; }

        [DataMember]
        public int TheoremListRequestTypeId { get; set; }

        [DataMember]
        public int Status { get; set; }

        [DataMember]
        public string TheoremListRequestType { get; set; }

        /// <summary>
        /// Gets or sets the candidate id.
        /// </summary>
        /// <value>
        /// The candidate id.
        /// </value>
        [DataMember]
        public Guid? CandidateId { get; set; }

        [DataMember]
        public Guid ProjectId { get; set; }

        [DataMember]
        public string ProjectName { get; set; }

        [DataMember]
        public string Description { get; set; }
    }
}