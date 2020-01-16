using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class CreateProjectComplaintRequest
    {
        [DataMember]
        public int CrmProjectId { get; set; }

        [DataMember]
        public string Subject { get; set; }

        [DataMember]
        public Guid SubmitterId { get; set; }

        [DataMember]
        public DateTime? ComplaintDate { get; set; }

        [DataMember]
        public string ComplaintDetails { get; set; }

        [DataMember]
        public int ComplaintSeverityTypeId { get; set; }

        [DataMember]
        public int ComplaintTypeId { get; set; }

        [DataMember]
        public int ComplaintStatusTypeId { get; set; }

        [DataMember]
        public string FollowUp { get; set; }
    }
}