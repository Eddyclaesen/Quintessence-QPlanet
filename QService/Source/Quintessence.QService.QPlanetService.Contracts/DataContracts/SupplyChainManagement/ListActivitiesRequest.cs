using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.SupplyChainManagement
{
    [DataContract]
    public class ListActivitiesRequest
    {
        [DataMember]
        public Guid? ProjectId { get; set; }

        [DataMember]
        public Guid? ProjectPlanId { get; set; }
    }
}