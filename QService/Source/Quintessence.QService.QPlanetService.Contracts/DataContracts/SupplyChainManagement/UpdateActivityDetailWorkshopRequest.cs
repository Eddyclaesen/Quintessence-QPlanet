using System.Collections.Generic;
using System.Runtime.Serialization;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.Shared;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.SupplyChainManagement
{
    [DataContract]
    public class UpdateActivityDetailWorkshopRequest : UpdateRequestBase
    {
        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string TargetGroup { get; set; }

        [DataMember]
        public List<UpdateActivityDetailWorkshopLanguageRequest> ActivityDetailWorkshopLanguages { get; set; }
    }
}