using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.DictionaryManagement
{
    [DataContract]
    public class ImportDictionaryLevelTranslationRequest
    {
        [DataMember]
        public int LanguageId { get; set; }

        [DataMember]
        public string Text { get; set; }
    }
}