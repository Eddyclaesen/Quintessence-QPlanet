using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.DictionaryManagement
{
    [DataContract]
    public class ImportDictionaryLevelRequest
    {
        [DataMember]
        public int Level { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public List<ImportDictionaryIndicatorRequest> DictionaryIndicators { get; set; }

        [DataMember]
        public List<ImportDictionaryLevelTranslationRequest> DictionaryLevelTranslations { get; set; }
    }
}