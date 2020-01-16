using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Quintessence.QService.QueryModel.Dim
{
    [DataContract(IsReference = true)]
    public class DictionaryClusterImportView
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
        public List<DictionaryCompetenceImportView> DictionaryCompetences { get; set; }

        [DataMember]
        public List<DictionaryClusterTranslationImportView> DictionaryClusterTranslations { get; set; }
    }
}