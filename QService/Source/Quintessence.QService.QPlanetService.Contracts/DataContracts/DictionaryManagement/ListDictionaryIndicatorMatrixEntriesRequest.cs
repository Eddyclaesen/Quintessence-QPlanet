using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.DictionaryManagement
{
    [DataContract]
    public class ListDictionaryIndicatorMatrixEntriesRequest
    {
        [DataMember]
        public Guid DictionaryId { get; set; }

        [DataMember]
        public int? LanguageId { get; set; }
    }
}