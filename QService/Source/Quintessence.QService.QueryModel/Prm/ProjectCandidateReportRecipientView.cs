using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QueryModel.Prm
{
    [DataContract]
    public class ProjectCandidateReportRecipientView
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public Guid? ProjectCandidateId { get; set; }

        [DataMember]
        public Guid? ProjectId { get; set; }
        
        [DataMember]
        public int CrmEmailId { get; set; }
        
        [DataMember]
        public string FirstName { get; set; }
        
        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string DirectPhone { get; set; }

        [DataMember]
        public string MobilePhone { get; set; }
    }
}
