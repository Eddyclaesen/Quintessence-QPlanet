using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.SupplyChainManagement
{
    [DataContract]
    public class CreateProductTypeRequest
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public decimal UnitPrice { get; set; }
    }
}
