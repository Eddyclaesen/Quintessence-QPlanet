using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.SupplyChainManagement
{
    [DataContract]
    public class CreateNewProfileRequest
    {
        [DataMember]
        public string Name { get; set; }
    }
}
