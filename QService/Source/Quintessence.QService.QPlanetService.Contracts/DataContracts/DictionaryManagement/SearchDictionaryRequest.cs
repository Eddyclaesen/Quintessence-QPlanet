using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.DictionaryManagement
{
    [DataContract]
    public class SearchDictionaryRequest
    {
        [DataMember]
        public string ContactName { get; set; }
    }
}
