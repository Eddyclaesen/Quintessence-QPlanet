using System.Collections.Generic;
using System.Runtime.Serialization;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.Shared;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.DictionaryManagement
{
    [DataContract]
    public class UpdateDictionaryClusterRequest : UpdateRequestBase
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string Color { get; set; }

        [DataMember]
        public int Order { get; set; }

        [DataMember]
        public string ImageName { get; set; }
        
        [DataMember]
        public List<UpdateDictionaryClusterTranslationRequest> DictionaryClusterTranslations { get; set; }
    }
}