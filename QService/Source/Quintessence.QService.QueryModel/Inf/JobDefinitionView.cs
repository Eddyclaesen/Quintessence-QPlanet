using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Quintessence.QService.QueryModel.Inf
{
    [DataContract(IsReference = true)]
    public class JobDefinitionView
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Assembly { get; set; }

        [DataMember]
        public string Class { get; set; }

        [DataMember]
        public bool IsEnabled { get; set; }

        [DataMember]
        public List<JobScheduleView> JobSchedules { get; set; }
    }
}
