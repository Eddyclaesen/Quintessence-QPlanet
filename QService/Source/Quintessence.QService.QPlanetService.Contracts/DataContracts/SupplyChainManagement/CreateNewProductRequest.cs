using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.SupplyChainManagement
{
    [DataContract]
    public class CreateNewProductRequest
    {
        [DataMember]
        public Guid ProjectId { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public Guid ProductTypeId { get; set; }

        [DataMember]
        public decimal UnitPrice { get; set; }
    }
}