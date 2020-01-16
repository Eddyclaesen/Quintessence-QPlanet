using System;
using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Prm;
using Quintessence.QService.QueryModel.Sec;

namespace Quintessence.QService.QueryModel.Sof
{
    [DataContract(IsReference = true)]
    public class CrmTimesheetUnregisteredEntryView
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public Guid UserId { get; set; }

        [DataMember]
        public UserView User { get; set; }

        [DataMember]
        public Guid ProjectId { get; set; }

        [DataMember]
        public ProjectView Project { get; set; }

        [DataMember]
        public DateTime StartDate { get; set; }

        [DataMember]
        public DateTime EndDate { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string Category { get; set; }
    }
}
