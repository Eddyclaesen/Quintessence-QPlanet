using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.SupplyChainManagement
{
    [DataContract]
    public class ListActivityTypeProfilesRequest
    {
        [DataMember]
        public Guid? ActivityTypeId { get; set; }

        [DataMember]
        public Guid? ActivityId { get; set; }
    }
}