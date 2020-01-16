using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QueryModel.Inf
{
    [DataContract(IsReference = true)]
    public class JobView
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public Guid JobDefinitionId { get; set; }

        [DataMember]
        public Guid? JobScheduleId { get; set; }

        [DataMember]
        public DateTime RequestDate { get; set; }

        [DataMember]
        public DateTime? StartDate { get; set; }

        [DataMember]
        public DateTime? EndDate { get; set; }

        [DataMember]
        public bool? Success { get; set; }
    }
}