using System;
using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Base;

namespace Quintessence.QService.QueryModel.Prm
{
    [DataContract]
    public class ProjectReportRecipientView : ViewEntityBase
    {
        [DataMember]
        public Guid ProjectId { get; set; }
        
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