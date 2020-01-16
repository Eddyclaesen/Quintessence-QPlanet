using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Quintessence.QService.QueryModel.Dim
{
    [DataContract(IsReference = true)]
    public class DictionaryCompetenceImportView
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public int LanguageId { get; set; }

        [DataMember]
        public int Order { get; set; }

        [DataMember]
        public List<DictionaryLevelImportView> DictionaryLevels { get; set; }

        [DataMember]
        public List<DictionaryCompetenceTranslationImportView> DictionaryCompetenceTranslations { get; set; }
    }
}