using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.DictionaryManagement
{
    [DataContract]
    public class CreateNewDictionaryClusterRequest
    {
        [DataMember]
        public Guid DictionaryId { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string Color { get; set; }

        [DataMember]
        public int Order { get; set; }

        [DataMember]
        public string ImageName { get; set; }

        [DataMember]
        public int LanguageId { get; set; }
    }
}