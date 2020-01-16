using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.DictionaryManagement
{
    [DataContract]
    public class CreateNewDictionaryIndicatorRequest
    {
        [DataMember]
        public Guid DictionaryLevelId { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int Order { get; set; }

        [DataMember]
        public bool IsStandard { get; set; }

        [DataMember]
        public bool IsDistinctive { get; set; }

        [DataMember]
        public int LanguageId { get; set; }
    }
}