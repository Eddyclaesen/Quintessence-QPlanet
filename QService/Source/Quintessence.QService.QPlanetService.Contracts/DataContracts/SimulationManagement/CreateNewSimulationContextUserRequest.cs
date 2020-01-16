using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.SimulationManagement
{
    [DataContract]
    public class CreateNewSimulationContextUserRequest
    {
        [DataMember]
        public Guid SimulationContextId { get; set; }

        [DataMember]
        public string UserName { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public string ValidFrom { get; set; }

        [DataMember]
        public string ValidTo { get; set; }
    }
}