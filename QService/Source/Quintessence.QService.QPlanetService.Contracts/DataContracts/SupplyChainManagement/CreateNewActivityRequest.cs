using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.SupplyChainManagement
{
    [DataContract]
    public class CreateNewActivityRequest
    {
        [DataMember]
        public Guid ProjectId { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public Guid ActivityTypeId { get; set; }
    }
}
