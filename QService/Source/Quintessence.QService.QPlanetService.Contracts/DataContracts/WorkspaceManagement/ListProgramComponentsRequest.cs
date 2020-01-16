using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.WorkspaceManagement
{
    [DataContract]
    public class ListProgramComponentsRequest
    {
        [DataMember]
        public Guid UserId { get; set; }

        [DataMember]
        public DateTime Start { get; set; }

        [DataMember]
        public DateTime End { get; set; }
    }
}
