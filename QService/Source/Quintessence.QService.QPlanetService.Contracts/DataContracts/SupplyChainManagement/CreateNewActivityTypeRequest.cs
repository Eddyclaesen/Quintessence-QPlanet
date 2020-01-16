using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.SupplyChainManagement
{
    [DataContract]
    public class CreateNewActivityTypeRequest
    {
        [DataMember]
        public string Name { get; set; }
    }
}