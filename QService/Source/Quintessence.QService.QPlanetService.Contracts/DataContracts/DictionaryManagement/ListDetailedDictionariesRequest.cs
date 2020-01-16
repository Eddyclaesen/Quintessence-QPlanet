using System;
using System.Runtime.Serialization;

namespace Quintessence.QService.QPlanetService.Contracts.DataContracts.DictionaryManagement
{
    [DataContract]
    public class ListDetailedDictionariesRequest
    {
        [DataMember]
        public bool TillClusters { get; set; }

        [DataMember]
        public bool TillCompetences { get; set; }

        [DataMember]
        public bool TillLevels { get; set; }

        [DataMember]
        public bool TillIndicators { get; set; }

        [DataMember]
        public Guid ProjectRoleId { get; set; }
    }
}