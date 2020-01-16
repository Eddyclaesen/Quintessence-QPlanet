using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.Shared;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.SupplyChainManagement
{
    [DataContract]
    public class UpdateActivityRequest : UpdateRequestBase
    {
        [DataMember]
        public Guid ActivityTypeId { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public List<UpdateActivityProfileRequest> ActivityProfiles { get; set; }
    }
}