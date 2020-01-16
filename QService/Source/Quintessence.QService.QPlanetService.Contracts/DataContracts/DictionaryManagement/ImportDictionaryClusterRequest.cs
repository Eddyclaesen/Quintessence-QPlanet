using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.DictionaryManagement
{
    [DataContract]
    public class ImportDictionaryClusterRequest
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public int Order { get; set; }

        [DataMember]
        public List<ImportDictionaryCompetenceRequest> DictionaryCompetences { get; set; }

        [DataMember]
        public List<ImportDictionaryClusterTranslationRequest> DictionaryClusterTranslations { get; set; }
    }
}