using System.Collections.Generic;
using System.Runtime.Serialization;
using Quintessence.QService.QPlanetService.Contracts.DataContracts.Shared;
using Quintessence.QService.QueryModel.Dim;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.DictionaryManagement
{
    [DataContract]
    public class ListDictionariesResponse : ListResponseBase
    {
        [DataMember]
        public List<DictionaryView> Dictionaries { get; set; }
    }
}