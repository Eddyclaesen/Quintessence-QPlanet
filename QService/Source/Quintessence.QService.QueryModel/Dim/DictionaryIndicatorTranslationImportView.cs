using System.Runtime.Serialization;

namespace Quintessence.QService.QueryModel.Dim
{
    [DataContract(IsReference = true)]
    public class DictionaryIndicatorTranslationImportView
    {
        [DataMember]
        public int LanguageId { get; set; }

        [DataMember]
        public string Text { get; set; }
    }
}