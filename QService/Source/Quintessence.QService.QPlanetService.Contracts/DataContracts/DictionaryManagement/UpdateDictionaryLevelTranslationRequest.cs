using System.Runtime.Serialization;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.Shared;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.DictionaryManagement
{
    [DataContract]
    public class UpdateDictionaryLevelTranslationRequest : UpdateRequestBase
    {
        [DataMember]
        public string Text { get; set; }
    }
}