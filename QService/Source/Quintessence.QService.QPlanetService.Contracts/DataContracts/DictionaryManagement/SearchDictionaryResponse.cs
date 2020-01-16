using System.Collections.Generic;
using System.Runtime.Serialization;
using Quintessence.QService.QueryModel.Dim;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.DictionaryManagement
{
    [DataContract]
    public class SearchDictionaryResponse
    {
        [DataMember]
        public List<DictionaryView> Dictionaries { get; set; }
    }
}
