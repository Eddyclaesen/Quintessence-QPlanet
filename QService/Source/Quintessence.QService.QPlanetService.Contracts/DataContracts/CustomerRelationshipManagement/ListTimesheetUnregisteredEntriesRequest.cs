using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.CustomerRelationshipManagement
{
    [DataContract]
    public class ListTimesheetUnregisteredEntriesRequest
    {
        [DataMember]
        public Guid? ProjectId { get; set; }

        [DataMember]
        public DateTime? MonthDate { get; set; }

        [DataMember]
        public bool CurrentMonth { get; set; }

        [DataMember]
        public Guid? UserId { get; set; }

        [DataMember]
        public bool IsProjectManager { get; set; }
    }
}