using System.Runtime.Serialization;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.Shared;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.DictionaryManagement
{
    [DataContract]
    public class UpdateDictionaryCompetenceTranslationRequest : UpdateRequestBase
    {
        [DataMember]
        public string Text { get; set; }

        [DataMember]
        public string Description { get; set; }
    }
}