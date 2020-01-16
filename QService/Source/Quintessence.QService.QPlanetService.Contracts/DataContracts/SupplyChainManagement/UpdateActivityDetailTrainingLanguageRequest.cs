using System.Runtime.Serialization;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.Shared;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.SupplyChainManagement
{
    [DataContract]
    public class UpdateActivityDetailTrainingLanguageRequest : UpdateRequestBase
    {
        [DataMember]
        public int SessionQuantity { get; set; }
    }
}