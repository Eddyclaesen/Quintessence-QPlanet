using System;
using System.Runtime.Serialization;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.Shared;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.SupplyChainManagement
{
    [DataContract]
    public class UpdateProductRequest : UpdateRequestBase
    {
        [DataMember]
        public Guid ProductTypeId { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public decimal UnitPrice { get; set; }

        [DataMember]
        public string Description { get; set; }
    }
}