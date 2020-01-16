using System.Collections.Generic;
using System.Runtime.Serialization;
using Quintessence.QService.DataModel.Dim;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.DictionaryManagement
{
    [DataContract]
    public class ListQuintessenceDictionariesResponse
    {
        [DataMember]
        public List<Dictionary> Dictionaries { get; set; }
    }
}