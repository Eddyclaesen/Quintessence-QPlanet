using System;
using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Base;

namespace Quintessence.QService.QueryModel.Prm
{
    [DataContract(IsReference = true)]
    public class ProjectComplaintView : ViewEntityBase
    {
        [DataMember]
        public int CrmProjectId { get; set; }

        [DataMember]
        public string Subject { get; set; }

        [DataMember]
        public DateTime? ComplaintDate { get; set; }

        [DataMember]
        public string ComplaintDetails { get; set; }

        [DataMember]
        public string ComplaintStatusTypeName { get; set; }

        [DataMember]
        public string ComplaintSeverityTypeName { get; set; }

        [DataMember]
        public string ComplaintTypeName { get; set; }

        [DataMember]
        public string FollowUp { get; set; }

        [DataMember]
        public string SubmitterName { get; set; }

        [DataMember]
        public Guid SubmitterId { get; set; }
    }
}