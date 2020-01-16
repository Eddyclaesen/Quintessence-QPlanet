using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.DictionaryManagement
{
    [DataContract]
    public class CreateNewDictionaryLevelRequest
    {
        [DataMember]
        public Guid DictionaryCompetenceId { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public int Level { get; set; }

        [DataMember]
        public int LanguageId { get; set; }

        [DataMember]
        public bool ApplyToAllCompetences { get; set; }
    }
}