using System.Runtime.Serialization;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.Shared;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.SupplyChainManagement
{
    [DataContract]
    public class UpdateActivtyTypeRequest : UpdateRequestBase
    {
        [DataMember]
        public string Name { get; set; }
    }
}