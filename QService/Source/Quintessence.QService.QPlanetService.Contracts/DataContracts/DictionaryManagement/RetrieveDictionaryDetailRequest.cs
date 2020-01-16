using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.DictionaryManagement
{
    [DataContract]
    public class RetrieveDictionaryDetailRequest
    {
        [DataMember]
        public Guid Id { get; set; }

        [DataMember]
        public List<int> LanguageIds{ get; set; }
    }
}