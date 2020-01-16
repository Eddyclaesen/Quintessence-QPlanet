using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.DictionaryManagement
{
    [DataContract]
    public class ImportDictionaryIndicatorRequest
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int Order { get; set; }

        [DataMember]
        public List<ImportDictionaryIndicatorTranslationRequest> DictionaryIndicatorTranslations { get; set; }
    }
}