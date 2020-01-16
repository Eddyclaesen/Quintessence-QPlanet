using System.Collections.Generic;
using System.Runtime.Serialization;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.Shared;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.DictionaryManagement
{
    [DataContract]
    public class UpdateDictionaryLevelRequest : UpdateRequestBase
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int Level { get; set; }

        [DataMember]
        public List<UpdateDictionaryLevelTranslationRequest> DictionaryLevelTranslations { get; set; }
    }
}