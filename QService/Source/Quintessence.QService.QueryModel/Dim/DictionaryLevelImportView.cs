using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Quintessence.QService.QueryModel.Dim
{
    [DataContract(IsReference = true)]
    public class DictionaryLevelImportView
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public int Level { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int LanguageId { get; set; }

        [DataMember]
        public List<DictionaryIndicatorImportView> DictionaryIndicators { get; set; }

        [DataMember]
        public List<DictionaryLevelTranslationImportView> DictionaryLevelTranslations { get; set; }
    }
}