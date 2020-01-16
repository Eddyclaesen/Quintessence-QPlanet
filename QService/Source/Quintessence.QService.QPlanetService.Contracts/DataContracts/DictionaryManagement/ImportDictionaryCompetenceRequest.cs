using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.DictionaryManagement
{
    [DataContract]
    public class ImportDictionaryCompetenceRequest
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public int Order { get; set; }

        [DataMember]
        public List<ImportDictionaryLevelRequest> DictionaryLevels { get; set; }

        [DataMember]
        public List<ImportDictionaryCompetenceTranslationRequest> DictionaryCompetenceTranslations { get; set; }
    }
}