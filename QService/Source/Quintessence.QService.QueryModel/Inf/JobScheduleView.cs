using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QueryModel.Inf
{
    [DataContract(IsReference = true)]
    public class JobScheduleView
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public Guid JobDefinitionId { get; set; }

        [DataMember]
        public JobDefinitionView JobDefinition { get; set; }

        [DataMember]
        public TimeSpan StartTime { get; set; }

        [DataMember]
        public TimeSpan EndTime { get; set; }

        [DataMember]
        public int Interval { get; set; }

        [DataMember]
        public bool IsEnabled { get; set; }
    }
}