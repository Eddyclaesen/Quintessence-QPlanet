using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QueryModel.Sof
{
    [DataContract(IsReference = true)]
    public class CrmTimesheetEntryView
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public Guid UserId { get; set; }

        [DataMember]
        public Guid ProjectId { get; set; }

        [DataMember]
        public DateTime StartDate { get; set; }

        [DataMember]
        public DateTime EndDate { get; set; }

        [DataMember]
        public string Description { get; set; }
    }
}
