using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.ProjectManagement
{
    [DataContract]
    public class SearchCrmProjectsRequest
    {
        [DataMember]
        public Guid? ProjectId { get; set; }

        [DataMember]
        public string ProjectName { get; set; }

        [DataMember]
        public bool WithPlannedStatus { get; set; }

        [DataMember]
        public bool WithRunningStatus { get; set; }

        [DataMember]
        public bool WithDoneStatus { get; set; }

        [DataMember]
        public bool WithStoppedStatus { get; set; }
    }
}
